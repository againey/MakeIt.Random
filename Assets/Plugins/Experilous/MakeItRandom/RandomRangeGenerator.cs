/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	#region Interfaces

	public interface ISByteGenerator
	{
		sbyte Next();
	}

	public interface IByteGenerator
	{
		byte Next();
	}

	public interface IShortGenerator
	{
		short Next();
	}

	public interface IUShortGenerator
	{
		ushort Next();
	}

	public interface IIntGenerator
	{
		int Next();
	}

	public interface IUIntGenerator
	{
		uint Next();
	}

	public interface ILongGenerator
	{
		long Next();
	}

	public interface IULongGenerator
	{
		ulong Next();
	}

	#endregion

	/// <summary>
	/// A static class of extension methods for generating random numbers within custom ranges.
	/// </summary>
	public static class RandomRangeGenerator
	{
		#region Private Range Generator Base Classes

		private class BufferedAnyRangeGeneratorBase
		{
			IRandom _random;

			ulong _rangeMax;

			int _bitCountPerGroup;
			int _bitGroupCountMinus1Per64Bits;
			ulong _bitMask;

			int _bitGroupCount = 0;
			ulong _bits = 0UL;

			public BufferedAnyRangeGeneratorBase(IRandom random, ulong rangeMax, ulong bitMask)
			{
				_random = random;

				_rangeMax = rangeMax;
				_bitMask = bitMask;
				_bitCountPerGroup = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
				_bitGroupCountMinus1Per64Bits = 64 / _bitCountPerGroup - 1;
			}

			protected ulong Next64()
			{
				ulong n;
				do
				{
					if (_bitGroupCount > 0)
					{
						n = _bits & _bitMask;
						_bits = _bits >> _bitCountPerGroup;
						--_bitGroupCount;
					}
					else
					{
						_bits = _random.Next64();
						n = _bits & _bitMask;
						_bits = _bits >> _bitCountPerGroup;
						_bitGroupCount = _bitGroupCountMinus1Per64Bits;
					}
				} while (n > _rangeMax);
				return n;
			}
		}

		public class BufferedPow2RangeGeneratorBase
		{
			IRandom _random;

			int _bitCountPerGroup;
			int _bitGroupCountMinus1Per64Bits;
			ulong _bitMask;

			int _bitGroupCount;
			ulong _bits;

			public BufferedPow2RangeGeneratorBase(IRandom random, int bitCount, ulong bitMask)
			{
				_random = random;

				_bitMask = bitMask;
				_bitCountPerGroup = bitCount;
				_bitGroupCountMinus1Per64Bits = 64 / _bitCountPerGroup - 1;
			}

			protected ulong Next64()
			{
				if (_bitGroupCount > 0)
				{
					ulong n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
					--_bitGroupCount;
					return n;
				}
				else
				{
					_bits = _random.Next64();
					ulong n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
					_bitGroupCount = _bitGroupCountMinus1Per64Bits;
					return n;
				}
			}
		}

		public class BufferedPowPow2RangeGeneratorBase
		{
			IRandom _random;

			int _bitCountPerGroup;
			int _bitGroupCountMinus1Per64Bits;
			ulong _bitMask;

			int _bitGroupCount;
			ulong _bits;

			public BufferedPowPow2RangeGeneratorBase(IRandom random, int bitCount, ulong bitMask)
			{
				_random = random;

				_bitMask = bitMask;
				_bitCountPerGroup = bitCount;
				_bitGroupCountMinus1Per64Bits = 64 / _bitCountPerGroup - 1;
			}

			protected ulong Next64()
			{
				if (_bitGroupCount > 0)
				{
					ulong n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
					--_bitGroupCount;
					return n;
				}
				else
				{
					_bits = _random.Next64();
					ulong n = _bits & _bitMask;
					_bits = _bits >> _bitCountPerGroup;
					_bitGroupCount = _bitGroupCountMinus1Per64Bits;
					return n;
				}
			}
		}

		#endregion

		#region Private Concrete Range Generators

		private static class BufferedSByteRangeGenerator
		{
			public static ISByteGenerator Create(IRandom random)
			{
				return new PowPow2RangeGenerator(random, 8, 0xFFUL);
			}

			public static ISByteGenerator Create(IRandom random, byte rangeSize)
			{
				uint rangeMax = rangeSize - 1U;
				if (rangeMax > sbyte.MaxValue) throw new System.ArgumentOutOfRangeException("rangeSize");

				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((byte)rangeMax);

				if (rangeMax != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeMax, bitMask);
				}
				else
				{
					int bitCount = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
					if (!Detail.DeBruijnLookup.IsPowerOfTwo((byte)bitCount))
					{
						return new Pow2RangeGenerator(random, bitCount, bitMask);
					}
					else
					{
						return new PowPow2RangeGenerator(random, bitCount, bitMask);
					}
				}
			}

			public static ISByteGenerator Create(IRandom random, sbyte rangeMin, byte rangeSize)
			{
				if (rangeMin == 0) return Create(random, rangeSize);

				uint rangeSizeMinusOne = rangeSize - 1U;
				if (rangeMin + rangeSizeMinusOne > sbyte.MaxValue) throw new System.ArgumentOutOfRangeException("rangeSize");

				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((byte)rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new OffsetAnyRangeGenerator(random, rangeMin, rangeSizeMinusOne, bitMask);
				}
				else
				{
					int bitCount = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
					if (!Detail.DeBruijnLookup.IsPowerOfTwo((byte)bitCount))
					{
						return new OffsetPow2RangeGenerator(random, rangeMin, bitCount, bitMask);
					}
					else
					{
						return new OffsetPowPow2RangeGenerator(random, rangeMin, bitCount, bitMask);
					}
				}
			}

			private class AnyRangeGenerator : BufferedAnyRangeGeneratorBase, ISByteGenerator
			{
				public AnyRangeGenerator(IRandom random, ulong rangeMax, ulong bitMask) : base(random, rangeMax, bitMask) { }
				public sbyte Next() { return (sbyte)Next64(); }
			}

			private class Pow2RangeGenerator : BufferedPow2RangeGeneratorBase, ISByteGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public sbyte Next() { return (sbyte)Next64(); }
			}

			private class PowPow2RangeGenerator : BufferedPowPow2RangeGeneratorBase, ISByteGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public sbyte Next() { return (sbyte)Next64(); }
			}

			private class OffsetAnyRangeGenerator : BufferedAnyRangeGeneratorBase, ISByteGenerator
			{
				private ulong _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, sbyte rangeMin, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (ulong)rangeMin; }
				public sbyte Next() { return (sbyte)(Next64() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : BufferedPow2RangeGeneratorBase, ISByteGenerator
			{
				private ulong _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, sbyte rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = (ulong)rangeMin; }
				public sbyte Next() { return (sbyte)(Next64() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : BufferedPowPow2RangeGeneratorBase, ISByteGenerator
			{
				private ulong _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, sbyte rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = (ulong)rangeMin; }
				public sbyte Next() { return (sbyte)(Next64() + _rangeMin); }
			}
		}

		private static class BufferedByteRangeGenerator
		{
			public static IByteGenerator Create(IRandom random)
			{
				return new PowPow2RangeGenerator(random, 8, 0xFFUL);
			}

			public static IByteGenerator Create(IRandom random, byte rangeSize)
			{
				uint rangeMax = rangeSize - 1U;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((byte)rangeMax);

				if (rangeMax != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeMax, bitMask);
				}
				else
				{
					int bitCount = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
					if (!Detail.DeBruijnLookup.IsPowerOfTwo((byte)bitCount))
					{
						return new Pow2RangeGenerator(random, bitCount, bitMask);
					}
					else
					{
						return new PowPow2RangeGenerator(random, bitCount, bitMask);
					}
				}
			}

			public static IByteGenerator Create(IRandom random, byte rangeMin, byte rangeSize)
			{
				if (rangeMin == 0U) return Create(random, rangeSize);

				uint rangeSizeMinusOne = rangeSize - 1U;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((byte)rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new OffsetAnyRangeGenerator(random, rangeMin, rangeSizeMinusOne, bitMask);
				}
				else
				{
					int bitCount = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
					if (!Detail.DeBruijnLookup.IsPowerOfTwo((byte)bitCount))
					{
						return new OffsetPow2RangeGenerator(random, rangeMin, bitCount, bitMask);
					}
					else
					{
						return new OffsetPowPow2RangeGenerator(random, rangeMin, bitCount, bitMask);
					}
				}
			}

			private class AnyRangeGenerator : BufferedAnyRangeGeneratorBase, IByteGenerator
			{
				public AnyRangeGenerator(IRandom random, ulong rangeMax, ulong bitMask) : base(random, rangeMax, bitMask) { }
				public byte Next() { return (byte)Next64(); }
			}

			private class Pow2RangeGenerator : BufferedPow2RangeGeneratorBase, IByteGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public byte Next() { return (byte)Next64(); }
			}

			private class PowPow2RangeGenerator : BufferedPowPow2RangeGeneratorBase, IByteGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public byte Next() { return (byte)Next64(); }
			}

			private class OffsetAnyRangeGenerator : BufferedAnyRangeGeneratorBase, IByteGenerator
			{
				private ulong _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, byte rangeMin, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next64() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : BufferedPow2RangeGeneratorBase, IByteGenerator
			{
				private ulong _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, byte rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next64() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : BufferedPowPow2RangeGeneratorBase, IByteGenerator
			{
				private ulong _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, byte rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next64() + _rangeMin); }
			}
		}

		private static class BufferedIntRangeGenerator
		{
			public static IIntGenerator Create(IRandom random)
			{
				return new PowPow2RangeGenerator(random, 8, 0xFFUL);
			}

			public static IIntGenerator Create(IRandom random, uint rangeSize)
			{
				uint rangeMax = rangeSize - 1U;
				if (rangeMax > int.MaxValue) throw new System.ArgumentOutOfRangeException("rangeSize");

				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeMax);

				if (rangeMax != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeMax, bitMask);
				}
				else
				{
					int bitCount = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
					if (!Detail.DeBruijnLookup.IsPowerOfTwo((uint)bitCount))
					{
						return new Pow2RangeGenerator(random, bitCount, bitMask);
					}
					else
					{
						return new PowPow2RangeGenerator(random, bitCount, bitMask);
					}
				}
			}

			public static IIntGenerator Create(IRandom random, int rangeMin, uint rangeSize)
			{
				if (rangeMin == 0) return Create(random, rangeSize);

				uint rangeSizeMinusOne = rangeSize - 1U;
				if (rangeMin + rangeSizeMinusOne > int.MaxValue) throw new System.ArgumentOutOfRangeException("rangeSize");

				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new OffsetAnyRangeGenerator(random, rangeMin, rangeSizeMinusOne, bitMask);
				}
				else
				{
					int bitCount = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
					if (!Detail.DeBruijnLookup.IsPowerOfTwo((uint)bitCount))
					{
						return new OffsetPow2RangeGenerator(random, rangeMin, bitCount, bitMask);
					}
					else
					{
						return new OffsetPowPow2RangeGenerator(random, rangeMin, bitCount, bitMask);
					}
				}
			}

			private class AnyRangeGenerator : BufferedAnyRangeGeneratorBase, IIntGenerator
			{
				public AnyRangeGenerator(IRandom random, ulong rangeMax, ulong bitMask) : base(random, rangeMax, bitMask) { }
				public int Next() { return (int)Next64(); }
			}

			private class Pow2RangeGenerator : BufferedPow2RangeGeneratorBase, IIntGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public int Next() { return (int)Next64(); }
			}

			private class PowPow2RangeGenerator : BufferedPowPow2RangeGeneratorBase, IIntGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public int Next() { return (int)Next64(); }
			}

			private class OffsetAnyRangeGenerator : BufferedAnyRangeGeneratorBase, IIntGenerator
			{
				private ulong _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, int rangeMin, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (ulong)rangeMin; }
				public int Next() { return (int)(Next64() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : BufferedPow2RangeGeneratorBase, IIntGenerator
			{
				private ulong _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, int rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = (ulong)rangeMin; }
				public int Next() { return (int)(Next64() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : BufferedPowPow2RangeGeneratorBase, IIntGenerator
			{
				private ulong _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, int rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = (ulong)rangeMin; }
				public int Next() { return (int)(Next64() + _rangeMin); }
			}
		}

		private static class BufferedUIntRangeGenerator
		{
			public static IUIntGenerator Create(IRandom random)
			{
				return new PowPow2RangeGenerator(random, 8, 0xFFUL);
			}

			public static IUIntGenerator Create(IRandom random, uint rangeSize)
			{
				uint rangeMax = rangeSize - 1U;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeMax);

				if (rangeMax != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeMax, bitMask);
				}
				else
				{
					int bitCount = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
					if (!Detail.DeBruijnLookup.IsPowerOfTwo((uint)bitCount))
					{
						return new Pow2RangeGenerator(random, bitCount, bitMask);
					}
					else
					{
						return new PowPow2RangeGenerator(random, bitCount, bitMask);
					}
				}
			}

			public static IUIntGenerator Create(IRandom random, uint rangeMin, uint rangeSize)
			{
				if (rangeMin == 0U) return Create(random, rangeSize);

				uint rangeSizeMinusOne = rangeSize - 1U;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new OffsetAnyRangeGenerator(random, rangeMin, rangeSizeMinusOne, bitMask);
				}
				else
				{
					int bitCount = Detail.DeBruijnLookup.GetBitCountForBitMask(bitMask);
					if (!Detail.DeBruijnLookup.IsPowerOfTwo((uint)bitCount))
					{
						return new OffsetPow2RangeGenerator(random, rangeMin, bitCount, bitMask);
					}
					else
					{
						return new OffsetPowPow2RangeGenerator(random, rangeMin, bitCount, bitMask);
					}
				}
			}

			private class AnyRangeGenerator : BufferedAnyRangeGeneratorBase, IUIntGenerator
			{
				public AnyRangeGenerator(IRandom random, ulong rangeMax, ulong bitMask) : base(random, rangeMax, bitMask) { }
				public uint Next() { return (uint)Next64(); }
			}

			private class Pow2RangeGenerator : BufferedPow2RangeGeneratorBase, IUIntGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public uint Next() { return (uint)Next64(); }
			}

			private class PowPow2RangeGenerator : BufferedPowPow2RangeGeneratorBase, IUIntGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public uint Next() { return (uint)Next64(); }
			}

			private class OffsetAnyRangeGenerator : BufferedAnyRangeGeneratorBase, IUIntGenerator
			{
				private ulong _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, uint rangeMin, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return (uint)(Next64() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : BufferedPow2RangeGeneratorBase, IUIntGenerator
			{
				private ulong _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, uint rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return (uint)(Next64() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : BufferedPowPow2RangeGeneratorBase, IUIntGenerator
			{
				private ulong _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, uint rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return (uint)(Next64() + _rangeMin); }
			}
		}

		#endregion

		#region Public Extension Methods

		public static ISByteGenerator SByteGenerator(this IRandom random)
		{
			return BufferedSByteRangeGenerator.Create(random);
		}

		public static ISByteGenerator SByteGenerator(this IRandom random, sbyte rangeSize)
		{
			if (rangeSize <= 0) throw new System.ArgumentOutOfRangeException("rangeSize");
			return BufferedSByteRangeGenerator.Create(random, (byte)rangeSize);
		}

		public static ISByteGenerator SByteGenerator(this IRandom random, byte rangeSize)
		{
			return BufferedSByteRangeGenerator.Create(random, rangeSize);
		}

		public static ISByteGenerator SByteGenerator(this IRandom random, sbyte rangeMin, sbyte rangeSize)
		{
			if (rangeSize <= 0) throw new System.ArgumentOutOfRangeException("rangeSize");
			return BufferedSByteRangeGenerator.Create(random, rangeMin, (byte)rangeSize);
		}

		public static ISByteGenerator SByteGenerator(this IRandom random, sbyte rangeMin, byte rangeSize)
		{
			return BufferedSByteRangeGenerator.Create(random, rangeMin, rangeSize);
		}

		public static IByteGenerator ByteGenerator(this IRandom random)
		{
			return BufferedByteRangeGenerator.Create(random);
		}

		public static IByteGenerator ByteGenerator(this IRandom random, byte rangeSize)
		{
			return BufferedByteRangeGenerator.Create(random, rangeSize);
		}

		public static IByteGenerator ByteGenerator(this IRandom random, byte rangeMin, byte rangeSize)
		{
			return BufferedByteRangeGenerator.Create(random, rangeMin, rangeSize);
		}

		public static IIntGenerator IntGenerator(this IRandom random)
		{
			return BufferedIntRangeGenerator.Create(random);
		}

		public static IIntGenerator IntGenerator(this IRandom random, int rangeSize)
		{
			if (rangeSize <= 0) throw new System.ArgumentOutOfRangeException("rangeSize");
			return BufferedIntRangeGenerator.Create(random, (uint)rangeSize);
		}

		public static IIntGenerator IntGenerator(this IRandom random, uint rangeSize)
		{
			return BufferedIntRangeGenerator.Create(random, rangeSize);
		}

		public static IIntGenerator IntGenerator(this IRandom random, int rangeMin, int rangeSize)
		{
			if (rangeSize <= 0) throw new System.ArgumentOutOfRangeException("rangeSize");
			return BufferedIntRangeGenerator.Create(random, rangeMin, (uint)rangeSize);
		}

		public static IIntGenerator IntGenerator(this IRandom random, int rangeMin, uint rangeSize)
		{
			return BufferedIntRangeGenerator.Create(random, rangeMin, rangeSize);
		}

		public static IUIntGenerator UIntGenerator(this IRandom random)
		{
			return BufferedUIntRangeGenerator.Create(random);
		}

		public static IUIntGenerator UIntGenerator(this IRandom random, uint rangeSize)
		{
			return BufferedUIntRangeGenerator.Create(random, rangeSize);
		}

		public static IUIntGenerator UIntGenerator(this IRandom random, uint rangeMin, uint rangeSize)
		{
			return BufferedUIntRangeGenerator.Create(random, rangeMin, rangeSize);
		}

		#endregion
	}
}
