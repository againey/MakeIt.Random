/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	#region Interfaces

	/// <summary>
	/// An interface for any generator of signed bytes, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface ISByteGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next signed byte in the sequence determined by the generator implementation.</returns>
		sbyte Next();
	}

	/// <summary>
	/// An interface for any generator of bytes, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IByteGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next byte in the sequence determined by the generator implementation.</returns>
		byte Next();
	}

	/// <summary>
	/// An interface for any generator of short integers, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IShortGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next short integer in the sequence determined by the generator implementation.</returns>
		short Next();
	}

	/// <summary>
	/// An interface for any generator of unsigned short integers, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IUShortGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next unsigned short integer in the sequence determined by the generator implementation.</returns>
		ushort Next();
	}

	/// <summary>
	/// An interface for any generator of integers, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IIntGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next integer in the sequence determined by the generator implementation.</returns>
		int Next();
	}

	/// <summary>
	/// An interface for any generator of unsigned integers, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IUIntGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next unsigned integer in the sequence determined by the generator implementation.</returns>
		uint Next();
	}

	/// <summary>
	/// An interface for any generator of long integers, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface ILongGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next long integer in the sequence determined by the generator implementation.</returns>
		long Next();
	}

	/// <summary>
	/// An interface for any generator of unsigned long integers, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IULongGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next unsigned long integer in the sequence determined by the generator implementation.</returns>
		ulong Next();
	}

	/// <summary>
	/// An interface for any generator of floats, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IFloatGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next float in the sequence determined by the generator implementation.</returns>
		float Next();
	}

	/// <summary>
	/// An interface for any generator of doubles, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IDoubleGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next double in the sequence determined by the generator implementation.</returns>
		double Next();
	}

	#endregion

	/// <summary>
	/// A static class of extension methods for generating random numbers within custom ranges.
	/// </summary>
	public static class RandomRangeGenerator
	{
		#region Private Concrete Range Generators

		private static class BufferedSByteRangeGenerator
		{
			public static ISByteGenerator Create(IRandom random, sbyte rangeMin, sbyte rangeMax)
			{
				if (rangeMin == 0) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				uint rangeSizeMinusOne = (uint)(rangeMax - rangeMin);

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

			public static ISByteGenerator Create(IRandom random, sbyte rangeMax)
			{
				if (rangeMax < 0) throw new System.ArgumentException("The range maximum cannot be smaller than zero.", "rangeMax");

				uint rangeSizeMinusOne = (uint)rangeMax;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((byte)rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeSizeMinusOne, bitMask);
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, ISByteGenerator
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public sbyte Next() { return (sbyte)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, ISByteGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public sbyte Next() { return (sbyte)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, ISByteGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public sbyte Next() { return (sbyte)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, ISByteGenerator
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, sbyte rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (uint)rangeMin; }
				public sbyte Next() { return (sbyte)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, ISByteGenerator
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, sbyte rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public sbyte Next() { return (sbyte)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, ISByteGenerator
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, sbyte rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public sbyte Next() { return (sbyte)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedByteRangeGenerator
		{
			public static IByteGenerator Create(IRandom random, byte rangeMin, byte rangeMax)
			{
				if (rangeMin == 0U) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				uint rangeSizeMinusOne = (uint)(rangeMax - rangeMin);
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

			public static IByteGenerator Create(IRandom random, byte rangeMax)
			{
				uint rangeSizeMinusOne = rangeMax;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeMax);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeSizeMinusOne, bitMask);
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IByteGenerator
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public byte Next() { return (byte)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IByteGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public byte Next() { return (byte)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IByteGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public byte Next() { return (byte)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IByteGenerator
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, byte rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IByteGenerator
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, byte rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IByteGenerator
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, byte rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedShortRangeGenerator
		{
			public static IShortGenerator Create(IRandom random, short rangeMin, short rangeMax)
			{
				if (rangeMin == 0) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				uint rangeSizeMinusOne = (uint)(rangeMax - rangeMin);

				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((ushort)rangeSizeMinusOne);

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

			public static IShortGenerator Create(IRandom random, short rangeMax)
			{
				if (rangeMax < 0) throw new System.ArgumentException("The range maximum cannot be smaller than zero.", "rangeMax");

				uint rangeSizeMinusOne = (uint)rangeMax;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((ushort)rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeSizeMinusOne, bitMask);
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IShortGenerator
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public short Next() { return (short)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IShortGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public short Next() { return (short)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IShortGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public short Next() { return (short)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IShortGenerator
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, short rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (uint)rangeMin; }
				public short Next() { return (short)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IShortGenerator
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, short rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public short Next() { return (short)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IShortGenerator
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, short rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public short Next() { return (short)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedUShortRangeGenerator
		{
			public static IUShortGenerator Create(IRandom random, ushort rangeMin, ushort rangeMax)
			{
				if (rangeMin == 0U) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				uint rangeSizeMinusOne = (uint)(rangeMax - rangeMin);
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((ushort)rangeSizeMinusOne);

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

			public static IUShortGenerator Create(IRandom random, ushort rangeMax)
			{
				uint rangeSizeMinusOne = rangeMax;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeMax);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeSizeMinusOne, bitMask);
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IUShortGenerator
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public ushort Next() { return (ushort)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IUShortGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public ushort Next() { return (ushort)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IUShortGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public ushort Next() { return (ushort)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IUShortGenerator
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, ushort rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public ushort Next() { return (ushort)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IUShortGenerator
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, ushort rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public ushort Next() { return (ushort)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IUShortGenerator
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, ushort rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public ushort Next() { return (ushort)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedIntRangeGenerator
		{
			public static IIntGenerator Create(IRandom random, int rangeMin, int rangeMax)
			{
				if (rangeMin == 0) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				uint rangeSizeMinusOne = (uint)(rangeMax - rangeMin);

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

			public static IIntGenerator Create(IRandom random, int rangeMax)
			{
				if (rangeMax < 0) throw new System.ArgumentException("The range maximum cannot be smaller than zero.", "rangeMax");

				uint rangeSizeMinusOne = (uint)rangeMax;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeSizeMinusOne, bitMask);
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IIntGenerator
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public int Next() { return (int)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IIntGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public int Next() { return (int)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IIntGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public int Next() { return (int)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IIntGenerator
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, int rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (uint)rangeMin; }
				public int Next() { return (int)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IIntGenerator
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, int rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public int Next() { return (int)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IIntGenerator
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, int rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public int Next() { return (int)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedUIntRangeGenerator
		{
			public static IUIntGenerator Create(IRandom random, uint rangeMin, uint rangeMax)
			{
				if (rangeMin == 0U) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				uint rangeSizeMinusOne = rangeMax - rangeMin;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeSizeMinusOne);

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

			public static IUIntGenerator Create(IRandom random, uint rangeMax)
			{
				uint rangeSizeMinusOne = rangeMax;
				uint bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeSizeMinusOne, bitMask);
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IUIntGenerator
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public uint Next() { return Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IUIntGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public uint Next() { return Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IUIntGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public uint Next() { return Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase32, IUIntGenerator
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, uint rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return Next32() + _rangeMin; }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase32, IUIntGenerator
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, uint rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return Next32() + _rangeMin; }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase32, IUIntGenerator
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, uint rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return Next32() + _rangeMin; }
			}
		}

		private static class BufferedLongRangeGenerator
		{
			public static ILongGenerator Create(IRandom random, long rangeMin, long rangeMax)
			{
				if (rangeMin == 0) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				ulong rangeSizeMinusOne = (ulong)(rangeMax - rangeMin);

				ulong bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax((byte)rangeSizeMinusOne);

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

			public static ILongGenerator Create(IRandom random, long rangeMax)
			{
				if (rangeMax < 0) throw new System.ArgumentException("The range maximum cannot be smaller than zero.", "rangeMax");

				ulong rangeSizeMinusOne = (ulong)rangeMax;
				ulong bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeSizeMinusOne, bitMask);
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase64, ILongGenerator
			{
				public AnyRangeGenerator(IRandom random, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public long Next() { return (long)Next64(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase64, ILongGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public long Next() { return (long)Next64(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase64, ILongGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public long Next() { return (long)Next64(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase64, ILongGenerator
			{
				private ulong _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, long rangeMin, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (ulong)rangeMin; }
				public long Next() { return (long)(Next64() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase64, ILongGenerator
			{
				private ulong _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, long rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = (ulong)rangeMin; }
				public long Next() { return (long)(Next64() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase64, ILongGenerator
			{
				private ulong _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, long rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = (ulong)rangeMin; }
				public long Next() { return (long)(Next64() + _rangeMin); }
			}
		}

		private static class BufferedULongRangeGenerator
		{
			public static IULongGenerator Create(IRandom random, ulong rangeMin, ulong rangeMax)
			{
				if (rangeMin == 0U) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				ulong rangeSizeMinusOne = rangeMax - rangeMin;
				ulong bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeSizeMinusOne);

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

			public static IULongGenerator Create(IRandom random, ulong rangeMax)
			{
				ulong rangeSizeMinusOne = rangeMax;
				ulong bitMask = Detail.DeBruijnLookup.GetBitMaskForRangeMax(rangeSizeMinusOne);

				if (rangeSizeMinusOne != bitMask) // The range size is not a power of 2.
				{
					return new AnyRangeGenerator(random, rangeSizeMinusOne, bitMask);
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase64, IULongGenerator
			{
				public AnyRangeGenerator(IRandom random, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public ulong Next() { return Next64(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase64, IULongGenerator
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public ulong Next() { return Next64(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase64, IULongGenerator
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public ulong Next() { return Next64(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase64, IULongGenerator
			{
				private ulong _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, ulong rangeMin, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public ulong Next() { return Next64() + _rangeMin; }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase64, IULongGenerator
			{
				private ulong _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, ulong rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public ulong Next() { return Next64() + _rangeMin; }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase64, IULongGenerator
			{
				private ulong _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, ulong rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public ulong Next() { return Next64() + _rangeMin; }
			}
		}

		private static class FloatRangeGenerator
		{
			public static IFloatGenerator CreateOO(IRandom random, float rangeMin, float rangeMax)
			{
				return new RangeOOGenerator(random, rangeMin, rangeMax);
			}

			public static IFloatGenerator CreateOO(IRandom random, float rangeMax)
			{
				return new RangeZOOGenerator(random, rangeMax);
			}

			public static IFloatGenerator CreateOO(IRandom random)
			{
				return new UnitOOGenerator(random);
			}

			public static IFloatGenerator CreateCO(IRandom random, float rangeMin, float rangeMax)
			{
				return new RangeCOGenerator(random, rangeMin, rangeMax);
			}

			public static IFloatGenerator CreateCO(IRandom random, float rangeMax)
			{
				return new RangeZCOGenerator(random, rangeMax);
			}

			public static IFloatGenerator CreateCO(IRandom random)
			{
				return new UnitCOGenerator(random);
			}

			public static IFloatGenerator CreateOC(IRandom random, float rangeMin, float rangeMax)
			{
				return new RangeOCGenerator(random, rangeMin, rangeMax);
			}

			public static IFloatGenerator CreateOC(IRandom random, float rangeMax)
			{
				return new RangeZOCGenerator(random, rangeMax);
			}

			public static IFloatGenerator CreateOC(IRandom random)
			{
				return new UnitOCGenerator(random);
			}

			public static IFloatGenerator CreateCC(IRandom random, float rangeMin, float rangeMax)
			{
				return new RangeCCGenerator(random, rangeMin, rangeMax);
			}

			public static IFloatGenerator CreateCC(IRandom random, float rangeMax)
			{
				return new RangeZCCGenerator(random, rangeMax);
			}

			public static IFloatGenerator CreateCC(IRandom random)
			{
				return new UnitCCGenerator(random);
			}

			public static IFloatGenerator CreateSignedOO(IRandom random)
			{
				return new SignedOOGenerator(random);
			}

			public static IFloatGenerator CreateSignedCO(IRandom random)
			{
				return new SignedCOGenerator(random);
			}

			public static IFloatGenerator CreateSignedOC(IRandom random)
			{
				return new SignedOCGenerator(random);
			}

			public static IFloatGenerator CreateSignedCC(IRandom random)
			{
				return new SignedCCGenerator(random);
			}

			public static IFloatGenerator CreateC1O2(IRandom random)
			{
				return new UnitC1O2Generator(random);
			}

			public static IFloatGenerator CreateC2O4(IRandom random)
			{
				return new UnitC2O4Generator(random);
			}

			public static IFloatGenerator CreatePreciseOO(IRandom random, float rangeMin, float rangeMax)
			{
				return new PreciseRangeOOGenerator(random, rangeMin, rangeMax);
			}

			public static IFloatGenerator CreatePreciseOO(IRandom random, float rangeMax)
			{
				return new PreciseRangeZOOGenerator(random, rangeMax);
			}

			public static IFloatGenerator CreatePreciseOO(IRandom random)
			{
				return new PreciseUnitOOGenerator(random);
			}

			public static IFloatGenerator CreatePreciseCO(IRandom random, float rangeMin, float rangeMax)
			{
				return new PreciseRangeCOGenerator(random, rangeMin, rangeMax);
			}

			public static IFloatGenerator CreatePreciseCO(IRandom random, float rangeMax)
			{
				return new PreciseRangeZCOGenerator(random, rangeMax);
			}

			public static IFloatGenerator CreatePreciseCO(IRandom random)
			{
				return new PreciseUnitCOGenerator(random);
			}

			public static IFloatGenerator CreatePreciseOC(IRandom random, float rangeMin, float rangeMax)
			{
				return new PreciseRangeOCGenerator(random, rangeMin, rangeMax);
			}

			public static IFloatGenerator CreatePreciseOC(IRandom random, float rangeMax)
			{
				return new PreciseRangeZOCGenerator(random, rangeMax);
			}

			public static IFloatGenerator CreatePreciseOC(IRandom random)
			{
				return new PreciseUnitOCGenerator(random);
			}

			public static IFloatGenerator CreatePreciseCC(IRandom random, float rangeMin, float rangeMax)
			{
				return new PreciseRangeCCGenerator(random, rangeMin, rangeMax);
			}

			public static IFloatGenerator CreatePreciseCC(IRandom random, float rangeMax)
			{
				return new PreciseRangeZCCGenerator(random, rangeMax);
			}

			public static IFloatGenerator CreatePreciseCC(IRandom random)
			{
				return new PreciseUnitCCGenerator(random);
			}

			public static IFloatGenerator CreatePreciseSignedOO(IRandom random)
			{
				return new PreciseSignedOOGenerator(random);
			}

			public static IFloatGenerator CreatePreciseSignedCO(IRandom random)
			{
				return new PreciseSignedCOGenerator(random);
			}

			public static IFloatGenerator CreatePreciseSignedOC(IRandom random)
			{
				return new PreciseSignedOCGenerator(random);
			}

			public static IFloatGenerator CreatePreciseSignedCC(IRandom random)
			{
				return new PreciseSignedCCGenerator(random);
			}

			private class UnitOOGenerator : IFloatGenerator
			{
				private IRandom _random;
				public UnitOOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatOO(); }
			}

			private class UnitCOGenerator : IFloatGenerator
			{
				private IRandom _random;
				public UnitCOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatCO(); }
			}

			private class UnitOCGenerator : IFloatGenerator
			{
				private IRandom _random;
				public UnitOCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatOC(); }
			}

			private class UnitCCGenerator : IFloatGenerator
			{
				private IRandom _random;
				public UnitCCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatCC(); }
			}

			private class SignedOOGenerator : IFloatGenerator
			{
				private IRandom _random;
				public SignedOOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.SignedFloatOO(); }
			}

			private class SignedCOGenerator : IFloatGenerator
			{
				private IRandom _random;
				public SignedCOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.SignedFloatCO(); }
			}

			private class SignedOCGenerator : IFloatGenerator
			{
				private IRandom _random;
				public SignedOCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.SignedFloatOC(); }
			}

			private class SignedCCGenerator : IFloatGenerator
			{
				private IRandom _random;
				public SignedCCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.SignedFloatCC(); }
			}

			private class UnitC1O2Generator : IFloatGenerator
			{
				private IRandom _random;
				public UnitC1O2Generator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatC1O2(); }
			}

			private class UnitC2O4Generator : IFloatGenerator
			{
				private IRandom _random;
				public UnitC2O4Generator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatC2O4(); }
			}

			private class PreciseUnitOOGenerator : IFloatGenerator
			{
				private IRandom _random;
				public PreciseUnitOOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseFloatOO(); }
			}

			private class PreciseUnitCOGenerator : IFloatGenerator
			{
				private IRandom _random;
				public PreciseUnitCOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseFloatCO(); }
			}

			private class PreciseUnitOCGenerator : IFloatGenerator
			{
				private IRandom _random;
				public PreciseUnitOCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseFloatOC(); }
			}

			private class PreciseUnitCCGenerator : IFloatGenerator
			{
				private IRandom _random;
				public PreciseUnitCCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseFloatCC(); }
			}

			private class PreciseSignedOOGenerator : IFloatGenerator
			{
				private IRandom _random;
				public PreciseSignedOOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseSignedFloatOO(); }
			}

			private class PreciseSignedCOGenerator : IFloatGenerator
			{
				private IRandom _random;
				public PreciseSignedCOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseSignedFloatCO(); }
			}

			private class PreciseSignedOCGenerator : IFloatGenerator
			{
				private IRandom _random;
				public PreciseSignedOCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseSignedFloatOC(); }
			}

			private class PreciseSignedCCGenerator : IFloatGenerator
			{
				private IRandom _random;
				public PreciseSignedCCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseSignedFloatCC(); }
			}

			private class RangeGeneratorBase
			{
				protected IRandom _random;
				protected float _rangeMax;

				protected RangeGeneratorBase(IRandom random, float rangeMax)
				{
					_random = random;
					_rangeMax = rangeMax;
				}
			}

			private class RangeOOGenerator : RangeGeneratorBase, IFloatGenerator
			{
				private float _rangeMin;
				public RangeOOGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.RangeOO(_rangeMin, _rangeMax); }
			}

			private class RangeZOOGenerator : RangeGeneratorBase, IFloatGenerator
			{
				public RangeZOOGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.RangeOO(_rangeMax); }
			}

			private class RangeCOGenerator : RangeGeneratorBase, IFloatGenerator
			{
				private float _rangeMin;
				public RangeCOGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.RangeCO(_rangeMin, _rangeMax); }
			}

			private class RangeZCOGenerator : RangeGeneratorBase, IFloatGenerator
			{
				public RangeZCOGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.RangeCO(_rangeMax); }
			}

			private class RangeOCGenerator : RangeGeneratorBase, IFloatGenerator
			{
				private float _rangeMin;
				public RangeOCGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.RangeOC(_rangeMin, _rangeMax); }
			}

			private class RangeZOCGenerator : RangeGeneratorBase, IFloatGenerator
			{
				public RangeZOCGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.RangeOC(_rangeMax); }
			}

			private class RangeCCGenerator : RangeGeneratorBase, IFloatGenerator
			{
				private float _rangeMin;
				public RangeCCGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.RangeCC(_rangeMin, _rangeMax); }
			}

			private class RangeZCCGenerator : RangeGeneratorBase, IFloatGenerator
			{
				public RangeZCCGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.RangeCC(_rangeMax); }
			}

			private class PreciseRangeOOGenerator : RangeGeneratorBase, IFloatGenerator
			{
				private float _rangeMin;
				public PreciseRangeOOGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.PreciseRangeOO(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZOOGenerator : RangeGeneratorBase, IFloatGenerator
			{
				public PreciseRangeZOOGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.PreciseRangeOO(_rangeMax); }
			}

			private class PreciseRangeCOGenerator : RangeGeneratorBase, IFloatGenerator
			{
				private float _rangeMin;
				public PreciseRangeCOGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.PreciseRangeCO(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZCOGenerator : RangeGeneratorBase, IFloatGenerator
			{
				public PreciseRangeZCOGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.PreciseRangeCO(_rangeMax); }
			}

			private class PreciseRangeOCGenerator : RangeGeneratorBase, IFloatGenerator
			{
				private float _rangeMin;
				public PreciseRangeOCGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.PreciseRangeOC(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZOCGenerator : RangeGeneratorBase, IFloatGenerator
			{
				public PreciseRangeZOCGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.PreciseRangeOC(_rangeMax); }
			}

			private class PreciseRangeCCGenerator : RangeGeneratorBase, IFloatGenerator
			{
				private float _rangeMin;
				public PreciseRangeCCGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.PreciseRangeCC(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZCCGenerator : RangeGeneratorBase, IFloatGenerator
			{
				public PreciseRangeZCCGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.PreciseRangeCC(_rangeMax); }
			}
		}

		private static class DoubleRangeGenerator
		{
			public static IDoubleGenerator CreateOO(IRandom random, double rangeMin, double rangeMax)
			{
				return new RangeOOGenerator(random, rangeMin, rangeMax);
			}

			public static IDoubleGenerator CreateOO(IRandom random, double rangeMax)
			{
				return new RangeZOOGenerator(random, rangeMax);
			}

			public static IDoubleGenerator CreateOO(IRandom random)
			{
				return new UnitOOGenerator(random);
			}

			public static IDoubleGenerator CreateCO(IRandom random, double rangeMin, double rangeMax)
			{
				return new RangeCOGenerator(random, rangeMin, rangeMax);
			}

			public static IDoubleGenerator CreateCO(IRandom random, double rangeMax)
			{
				return new RangeZCOGenerator(random, rangeMax);
			}

			public static IDoubleGenerator CreateCO(IRandom random)
			{
				return new UnitCOGenerator(random);
			}

			public static IDoubleGenerator CreateOC(IRandom random, double rangeMin, double rangeMax)
			{
				return new RangeOCGenerator(random, rangeMin, rangeMax);
			}

			public static IDoubleGenerator CreateOC(IRandom random, double rangeMax)
			{
				return new RangeZOCGenerator(random, rangeMax);
			}

			public static IDoubleGenerator CreateOC(IRandom random)
			{
				return new UnitOCGenerator(random);
			}

			public static IDoubleGenerator CreateCC(IRandom random, double rangeMin, double rangeMax)
			{
				return new RangeCCGenerator(random, rangeMin, rangeMax);
			}

			public static IDoubleGenerator CreateCC(IRandom random, double rangeMax)
			{
				return new RangeZCCGenerator(random, rangeMax);
			}

			public static IDoubleGenerator CreateCC(IRandom random)
			{
				return new UnitCCGenerator(random);
			}

			public static IDoubleGenerator CreateSignedOO(IRandom random)
			{
				return new SignedOOGenerator(random);
			}

			public static IDoubleGenerator CreateSignedCO(IRandom random)
			{
				return new SignedCOGenerator(random);
			}

			public static IDoubleGenerator CreateSignedOC(IRandom random)
			{
				return new SignedOCGenerator(random);
			}

			public static IDoubleGenerator CreateSignedCC(IRandom random)
			{
				return new SignedCCGenerator(random);
			}

			public static IDoubleGenerator CreateC1O2(IRandom random)
			{
				return new UnitC1O2Generator(random);
			}

			public static IDoubleGenerator CreateC2O4(IRandom random)
			{
				return new UnitC2O4Generator(random);
			}

			public static IDoubleGenerator CreatePreciseOO(IRandom random, double rangeMin, double rangeMax)
			{
				return new PreciseRangeOOGenerator(random, rangeMin, rangeMax);
			}

			public static IDoubleGenerator CreatePreciseOO(IRandom random, double rangeMax)
			{
				return new PreciseRangeZOOGenerator(random, rangeMax);
			}

			public static IDoubleGenerator CreatePreciseOO(IRandom random)
			{
				return new PreciseUnitOOGenerator(random);
			}

			public static IDoubleGenerator CreatePreciseCO(IRandom random, double rangeMin, double rangeMax)
			{
				return new PreciseRangeCOGenerator(random, rangeMin, rangeMax);
			}

			public static IDoubleGenerator CreatePreciseCO(IRandom random, double rangeMax)
			{
				return new PreciseRangeZCOGenerator(random, rangeMax);
			}

			public static IDoubleGenerator CreatePreciseCO(IRandom random)
			{
				return new PreciseUnitCOGenerator(random);
			}

			public static IDoubleGenerator CreatePreciseOC(IRandom random, double rangeMin, double rangeMax)
			{
				return new PreciseRangeOCGenerator(random, rangeMin, rangeMax);
			}

			public static IDoubleGenerator CreatePreciseOC(IRandom random, double rangeMax)
			{
				return new PreciseRangeZOCGenerator(random, rangeMax);
			}

			public static IDoubleGenerator CreatePreciseOC(IRandom random)
			{
				return new PreciseUnitOCGenerator(random);
			}

			public static IDoubleGenerator CreatePreciseCC(IRandom random, double rangeMin, double rangeMax)
			{
				return new PreciseRangeCCGenerator(random, rangeMin, rangeMax);
			}

			public static IDoubleGenerator CreatePreciseCC(IRandom random, double rangeMax)
			{
				return new PreciseRangeZCCGenerator(random, rangeMax);
			}

			public static IDoubleGenerator CreatePreciseCC(IRandom random)
			{
				return new PreciseUnitCCGenerator(random);
			}

			public static IDoubleGenerator CreatePreciseSignedOO(IRandom random)
			{
				return new PreciseSignedOOGenerator(random);
			}

			public static IDoubleGenerator CreatePreciseSignedCO(IRandom random)
			{
				return new PreciseSignedCOGenerator(random);
			}

			public static IDoubleGenerator CreatePreciseSignedOC(IRandom random)
			{
				return new PreciseSignedOCGenerator(random);
			}

			public static IDoubleGenerator CreatePreciseSignedCC(IRandom random)
			{
				return new PreciseSignedCCGenerator(random);
			}

			private class UnitOOGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public UnitOOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleOO(); }
			}

			private class UnitCOGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public UnitCOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleCO(); }
			}

			private class UnitOCGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public UnitOCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleOC(); }
			}

			private class UnitCCGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public UnitCCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleCC(); }
			}

			private class SignedOOGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public SignedOOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.SignedDoubleOO(); }
			}

			private class SignedCOGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public SignedCOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.SignedDoubleCO(); }
			}

			private class SignedOCGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public SignedOCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.SignedDoubleOC(); }
			}

			private class SignedCCGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public SignedCCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.SignedDoubleCC(); }
			}

			private class UnitC1O2Generator : IDoubleGenerator
			{
				private IRandom _random;
				public UnitC1O2Generator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleC1O2(); }
			}

			private class UnitC2O4Generator : IDoubleGenerator
			{
				private IRandom _random;
				public UnitC2O4Generator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleC2O4(); }
			}

			private class PreciseUnitOOGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public PreciseUnitOOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseDoubleOO(); }
			}

			private class PreciseUnitCOGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public PreciseUnitCOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseDoubleCO(); }
			}

			private class PreciseUnitOCGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public PreciseUnitOCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseDoubleOC(); }
			}

			private class PreciseUnitCCGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public PreciseUnitCCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseDoubleCC(); }
			}

			private class PreciseSignedOOGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public PreciseSignedOOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseSignedDoubleOO(); }
			}

			private class PreciseSignedCOGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public PreciseSignedCOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseSignedDoubleCO(); }
			}

			private class PreciseSignedOCGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public PreciseSignedOCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseSignedDoubleOC(); }
			}

			private class PreciseSignedCCGenerator : IDoubleGenerator
			{
				private IRandom _random;
				public PreciseSignedCCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseSignedDoubleCC(); }
			}

			private class RangeGeneratorBase
			{
				protected IRandom _random;
				protected double _rangeMax;

				protected RangeGeneratorBase(IRandom random, double rangeMax)
				{
					_random = random;
					_rangeMax = rangeMax;
				}
			}

			private class RangeOOGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				private double _rangeMin;
				public RangeOOGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.RangeOO(_rangeMin, _rangeMax); }
			}

			private class RangeZOOGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				public RangeZOOGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.RangeOO(_rangeMax); }
			}

			private class RangeCOGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				private double _rangeMin;
				public RangeCOGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.RangeCO(_rangeMin, _rangeMax); }
			}

			private class RangeZCOGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				public RangeZCOGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.RangeCO(_rangeMax); }
			}

			private class RangeOCGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				private double _rangeMin;
				public RangeOCGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.RangeOC(_rangeMin, _rangeMax); }
			}

			private class RangeZOCGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				public RangeZOCGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.RangeOC(_rangeMax); }
			}

			private class RangeCCGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				private double _rangeMin;
				public RangeCCGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.RangeCC(_rangeMin, _rangeMax); }
			}

			private class RangeZCCGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				public RangeZCCGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.RangeCC(_rangeMax); }
			}

			private class PreciseRangeOOGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				private double _rangeMin;
				public PreciseRangeOOGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.PreciseRangeOO(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZOOGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				public PreciseRangeZOOGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.PreciseRangeOO(_rangeMax); }
			}

			private class PreciseRangeCOGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				private double _rangeMin;
				public PreciseRangeCOGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.PreciseRangeCO(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZCOGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				public PreciseRangeZCOGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.PreciseRangeCO(_rangeMax); }
			}

			private class PreciseRangeOCGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				private double _rangeMin;
				public PreciseRangeOCGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.PreciseRangeOC(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZOCGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				public PreciseRangeZOCGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.PreciseRangeOC(_rangeMax); }
			}

			private class PreciseRangeCCGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				private double _rangeMin;
				public PreciseRangeCCGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.PreciseRangeCC(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZCCGenerator : RangeGeneratorBase, IDoubleGenerator
			{
				public PreciseRangeZCCGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.PreciseRangeCC(_rangeMax); }
			}
		}

		#endregion

		#region Make Range Open/Open Generator

		/// <summary>
		/// Returns a range generator which will produce signed bytes strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random signed bytes in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, sbyte, sbyte)"/>
		public static ISByteGenerator MakeRangeOOGenerator(this IRandom random, sbyte lowerExclusive, sbyte upperExclusive)
		{
			return BufferedSByteRangeGenerator.Create(random, (sbyte)(lowerExclusive + 1), (sbyte)(upperExclusive - 1));
		}

		/// <summary>
		/// Returns a range generator which will produce signed bytes strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random signed bytes in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, sbyte)"/>
		public static ISByteGenerator MakeRangeOOGenerator(this IRandom random, sbyte upperExclusive)
		{
			return BufferedSByteRangeGenerator.Create(random, (sbyte)1, (sbyte)(upperExclusive - 1));
		}

		/// <summary>
		/// Returns a range generator which will produce bytes strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random bytes in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, byte, byte)"/>
		public static IByteGenerator MakeRangeOOGenerator(this IRandom random, byte lowerExclusive, byte upperExclusive)
		{
			return BufferedByteRangeGenerator.Create(random, (byte)(lowerExclusive + 1U), (byte)(upperExclusive - 1U));
		}

		/// <summary>
		/// Returns a range generator which will produce bytes strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random bytes in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, byte)"/>
		public static IByteGenerator MakeRangeOOGenerator(this IRandom random, byte upperExclusive)
		{
			return BufferedByteRangeGenerator.Create(random, (byte)1U, (byte)(upperExclusive - 1U));
		}

		/// <summary>
		/// Returns a range generator which will produce short integers strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random short integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, short, short)"/>
		public static IShortGenerator MakeRangeOOGenerator(this IRandom random, short lowerExclusive, short upperExclusive)
		{
			return BufferedShortRangeGenerator.Create(random, (short)(lowerExclusive + 1), (short)(upperExclusive - 1));
		}

		/// <summary>
		/// Returns a range generator which will produce short integers strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random short integers in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, short)"/>
		public static IShortGenerator MakeRangeOOGenerator(this IRandom random, short upperExclusive)
		{
			return BufferedShortRangeGenerator.Create(random, (short)1, (short)(upperExclusive - 1));
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned short integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, ushort, ushort)"/>
		public static IUShortGenerator MakeRangeOOGenerator(this IRandom random, ushort lowerExclusive, ushort upperExclusive)
		{
			return BufferedUShortRangeGenerator.Create(random, (ushort)(lowerExclusive + 1U), (ushort)(upperExclusive - 1U));
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned short integers in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, ushort)"/>
		public static IUShortGenerator MakeRangeOOGenerator(this IRandom random, ushort upperExclusive)
		{
			return BufferedUShortRangeGenerator.Create(random, (ushort)1U, (ushort)(upperExclusive - 1U));
		}

		/// <summary>
		/// Returns a range generator which will produce integers strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, int, int)"/>
		public static IIntGenerator MakeRangeOOGenerator(this IRandom random, int lowerExclusive, int upperExclusive)
		{
			return BufferedIntRangeGenerator.Create(random, lowerExclusive + 1, upperExclusive - 1);
		}

		/// <summary>
		/// Returns a range generator which will produce integers strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random integers in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, int)"/>
		public static IIntGenerator MakeRangeOOGenerator(this IRandom random, int upperExclusive)
		{
			return BufferedIntRangeGenerator.Create(random, 1, upperExclusive - 1);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, uint, uint)"/>
		public static IUIntGenerator MakeRangeOOGenerator(this IRandom random, uint lowerExclusive, uint upperExclusive)
		{
			return BufferedUIntRangeGenerator.Create(random, lowerExclusive + 1U, upperExclusive - 1U);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned integers in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, uint)"/>
		public static IUIntGenerator MakeRangeOOGenerator(this IRandom random, uint upperExclusive)
		{
			return BufferedUIntRangeGenerator.Create(random, 1U, upperExclusive - 1U);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random long integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, long, long)"/>
		public static ILongGenerator MakeRangeOOGenerator(this IRandom random, long lowerExclusive, long upperExclusive)
		{
			return BufferedLongRangeGenerator.Create(random, lowerExclusive + 1L, upperExclusive - 1L);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random long integers in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, long)"/>
		public static ILongGenerator MakeRangeOOGenerator(this IRandom random, long upperExclusive)
		{
			return BufferedLongRangeGenerator.Create(random, 1L, upperExclusive - 1L);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned long integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, ulong, ulong)"/>
		public static IULongGenerator MakeRangeOOGenerator(this IRandom random, ulong lowerExclusive, ulong upperExclusive)
		{
			return BufferedULongRangeGenerator.Create(random, lowerExclusive + 1UL, upperExclusive - 1UL);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned long integers in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeOO(IRandom, ulong)"/>
		public static IULongGenerator MakeRangeOOGenerator(this IRandom random, ulong upperExclusive)
		{
			return BufferedULongRangeGenerator.Create(random, 1UL, upperExclusive - 1UL);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random floats in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.RangeOO(IRandom, float, float)"/>
		public static IFloatGenerator MakeRangeOOGenerator(this IRandom random, float lowerExclusive, float upperExclusive)
		{
			return FloatRangeGenerator.CreateOO(random, lowerExclusive, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random floats in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.RangeOO(IRandom, float)"/>
		public static IFloatGenerator MakeRangeOOGenerator(this IRandom random, float upperExclusive)
		{
			return FloatRangeGenerator.CreateOO(random, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random floats in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeOO(IRandom, float, float)"/>
		public static IFloatGenerator MakePreciseRangeOOGenerator(this IRandom random, float lowerExclusive, float upperExclusive)
		{
			return FloatRangeGenerator.CreatePreciseOO(random, lowerExclusive, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random floats in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeOO(IRandom, float)"/>
		public static IFloatGenerator MakePreciseRangeOOGenerator(this IRandom random, float upperExclusive)
		{
			return FloatRangeGenerator.CreatePreciseOO(random, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random doubles in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeOO(IRandom, double, double)"/>
		public static IDoubleGenerator MakePreciseRangeOOGenerator(this IRandom random, double lowerExclusive, double upperExclusive)
		{
			return DoubleRangeGenerator.CreatePreciseOO(random, lowerExclusive, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random doubles in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeOO(IRandom, double)"/>
		public static IDoubleGenerator MakePreciseRangeOOGenerator(this IRandom random, double upperExclusive)
		{
			return DoubleRangeGenerator.CreatePreciseOO(random, upperExclusive);
		}

		#endregion

		#region Make Range Closed/Open Generator

		/// <summary>
		/// Returns a range generator which will produce signed bytes greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random signed bytes in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, sbyte, sbyte)"/>
		public static ISByteGenerator MakeRangeCOGenerator(this IRandom random, sbyte lowerInclusive, sbyte upperExclusive)
		{
			return BufferedSByteRangeGenerator.Create(random, lowerInclusive, (sbyte)(upperExclusive - 1));
		}

		/// <summary>
		/// Returns a range generator which will produce signed bytes greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random signed bytes in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, sbyte)"/>
		public static ISByteGenerator MakeRangeCOGenerator(this IRandom random, sbyte upperExclusive)
		{
			return BufferedSByteRangeGenerator.Create(random, (sbyte)(upperExclusive - 1));
		}

		/// <summary>
		/// Returns a range generator which will produce bytes greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random bytes in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, byte, byte)"/>
		public static IByteGenerator MakeRangeCOGenerator(this IRandom random, byte lowerInclusive, byte upperExclusive)
		{
			return BufferedByteRangeGenerator.Create(random, lowerInclusive, (byte)(upperExclusive - 1U));
		}

		/// <summary>
		/// Returns a range generator which will produce bytes greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random bytes in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, byte)"/>
		public static IByteGenerator MakeRangeCOGenerator(this IRandom random, byte upperExclusive)
		{
			return BufferedByteRangeGenerator.Create(random, (byte)(upperExclusive - 1U));
		}

		/// <summary>
		/// Returns a range generator which will produce short integers greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random short integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, short, short)"/>
		public static IShortGenerator MakeRangeCOGenerator(this IRandom random, short lowerInclusive, short upperExclusive)
		{
			return BufferedShortRangeGenerator.Create(random, lowerInclusive, (short)(upperExclusive - 1));
		}

		/// <summary>
		/// Returns a range generator which will produce short integers greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random short integers in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, short)"/>
		public static IShortGenerator MakeRangeCOGenerator(this IRandom random, short upperExclusive)
		{
			return BufferedShortRangeGenerator.Create(random, (short)(upperExclusive - 1));
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned short integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, ushort, ushort)"/>
		public static IUShortGenerator MakeRangeCOGenerator(this IRandom random, ushort lowerInclusive, ushort upperExclusive)
		{
			return BufferedUShortRangeGenerator.Create(random, lowerInclusive, (ushort)(upperExclusive - 1U));
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned short integers in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, ushort)"/>
		public static IUShortGenerator MakeRangeCOGenerator(this IRandom random, ushort upperExclusive)
		{
			return BufferedUShortRangeGenerator.Create(random, (ushort)(upperExclusive - 1U));
		}

		/// <summary>
		/// Returns a range generator which will produce integers greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, int, int)"/>
		public static IIntGenerator MakeRangeCOGenerator(this IRandom random, int lowerInclusive, int upperExclusive)
		{
			return BufferedIntRangeGenerator.Create(random, lowerInclusive, upperExclusive - 1);
		}

		/// <summary>
		/// Returns a range generator which will produce integers greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random integers in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, int)"/>
		public static IIntGenerator MakeRangeCOGenerator(this IRandom random, int upperExclusive)
		{
			return BufferedIntRangeGenerator.Create(random, upperExclusive - 1);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, uint, uint)"/>
		public static IUIntGenerator MakeRangeCOGenerator(this IRandom random, uint lowerInclusive, uint upperExclusive)
		{
			return BufferedUIntRangeGenerator.Create(random, lowerInclusive, upperExclusive - 1U);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned integers in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, uint)"/>
		public static IUIntGenerator MakeRangeCOGenerator(this IRandom random, uint upperExclusive)
		{
			return BufferedUIntRangeGenerator.Create(random, upperExclusive - 1U);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random long integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, long, long)"/>
		public static ILongGenerator MakeRangeCOGenerator(this IRandom random, long lowerInclusive, long upperExclusive)
		{
			return BufferedLongRangeGenerator.Create(random, lowerInclusive, upperExclusive - 1L);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random long integers in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, long)"/>
		public static ILongGenerator MakeRangeCOGenerator(this IRandom random, long upperExclusive)
		{
			return BufferedLongRangeGenerator.Create(random, upperExclusive - 1L);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned long integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, ulong, ulong)"/>
		public static IULongGenerator MakeRangeCOGenerator(this IRandom random, ulong lowerInclusive, ulong upperExclusive)
		{
			return BufferedULongRangeGenerator.Create(random, lowerInclusive, upperExclusive - 1UL);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random unsigned long integers in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, ulong)"/>
		public static IULongGenerator MakeRangeCOGenerator(this IRandom random, ulong upperExclusive)
		{
			return BufferedULongRangeGenerator.Create(random, upperExclusive - 1UL);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random floats in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.RangeCO(IRandom, float, float)"/>
		public static IFloatGenerator MakeRangeCOGenerator(this IRandom random, float lowerInclusive, float upperExclusive)
		{
			return FloatRangeGenerator.CreateCO(random, lowerInclusive, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random floats in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.RangeCO(IRandom, float)"/>
		public static IFloatGenerator MakeRangeCOGenerator(this IRandom random, float upperExclusive)
		{
			return FloatRangeGenerator.CreateCO(random, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random doubles in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.RangeCO(IRandom, double, double)"/>
		public static IDoubleGenerator MakeRangeCOGenerator(this IRandom random, double lowerInclusive, double upperExclusive)
		{
			return DoubleRangeGenerator.CreateCO(random, lowerInclusive, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random doubles in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.RangeCO(IRandom, double)"/>
		public static IDoubleGenerator MakeRangeCOGenerator(this IRandom random, double upperExclusive)
		{
			return DoubleRangeGenerator.CreateCO(random, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random floats in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeCO(IRandom, float, float)"/>
		public static IFloatGenerator MakePreciseRangeCOGenerator(this IRandom random, float lowerInclusive, float upperExclusive)
		{
			return FloatRangeGenerator.CreatePreciseCO(random, lowerInclusive, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random floats in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeCO(IRandom, float)"/>
		public static IFloatGenerator MakePreciseRangeCOGenerator(this IRandom random, float upperExclusive)
		{
			return FloatRangeGenerator.CreatePreciseCO(random, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random doubles in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeCO(IRandom, double, double)"/>
		public static IDoubleGenerator MakePreciseRangeCOGenerator(this IRandom random, double lowerInclusive, double upperExclusive)
		{
			return DoubleRangeGenerator.CreatePreciseCO(random, lowerInclusive, upperExclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  Generated numbers will be strictly less than this value.</param>
		/// <returns>A range generator producing random doubles in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeCO(IRandom, double)"/>
		public static IDoubleGenerator MakePreciseRangeCOGenerator(this IRandom random, double upperExclusive)
		{
			return DoubleRangeGenerator.CreatePreciseCO(random, upperExclusive);
		}

		#endregion

		#region Make Range Open/Closed Generator

		/// <summary>
		/// Returns a range generator which will produce signed bytes strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random signed bytes in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, sbyte, sbyte)"/>
		public static ISByteGenerator MakeRangeOCGenerator(this IRandom random, sbyte lowerExclusive, sbyte upperInclusive)
		{
			return BufferedSByteRangeGenerator.Create(random, (sbyte)(lowerExclusive + 1), upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce signed bytes strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random signed bytes in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, sbyte)"/>
		public static ISByteGenerator MakeRangeOCGenerator(this IRandom random, sbyte upperInclusive)
		{
			return BufferedSByteRangeGenerator.Create(random, (sbyte)1, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce bytes strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random bytes in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, byte, byte)"/>
		public static IByteGenerator MakeRangeOCGenerator(this IRandom random, byte lowerExclusive, byte upperInclusive)
		{
			return BufferedByteRangeGenerator.Create(random, (byte)(lowerExclusive + 1U), upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce bytes strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random bytes in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, byte)"/>
		public static IByteGenerator MakeRangeOCGenerator(this IRandom random, byte upperInclusive)
		{
			return BufferedByteRangeGenerator.Create(random, (byte)1U, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce short integers strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random short integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, short, short)"/>
		public static IShortGenerator MakeRangeOCGenerator(this IRandom random, short lowerExclusive, short upperInclusive)
		{
			return BufferedShortRangeGenerator.Create(random, (short)(lowerExclusive + 1), upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce short integers strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random short integers in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, short)"/>
		public static IShortGenerator MakeRangeOCGenerator(this IRandom random, short upperInclusive)
		{
			return BufferedShortRangeGenerator.Create(random, (short)1, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned short integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, ushort, ushort)"/>
		public static IUShortGenerator MakeRangeOCGenerator(this IRandom random, ushort lowerExclusive, ushort upperInclusive)
		{
			return BufferedUShortRangeGenerator.Create(random, (ushort)(lowerExclusive + 1U), upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned short integers in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, ushort)"/>
		public static IUShortGenerator MakeRangeOCGenerator(this IRandom random, ushort upperInclusive)
		{
			return BufferedUShortRangeGenerator.Create(random, (ushort)1U, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce integers strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, int, int)"/>
		public static IIntGenerator MakeRangeOCGenerator(this IRandom random, int lowerExclusive, int upperInclusive)
		{
			return BufferedIntRangeGenerator.Create(random, lowerExclusive + 1, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce integers strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random integers in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, int)"/>
		public static IIntGenerator MakeRangeOCGenerator(this IRandom random, int upperInclusive)
		{
			return BufferedIntRangeGenerator.Create(random, 1, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, uint, uint)"/>
		public static IUIntGenerator MakeRangeOCGenerator(this IRandom random, uint lowerExclusive, uint upperInclusive)
		{
			return BufferedUIntRangeGenerator.Create(random, lowerExclusive + 1U, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned integers in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, uint)"/>
		public static IUIntGenerator MakeRangeOCGenerator(this IRandom random, uint upperInclusive)
		{
			return BufferedUIntRangeGenerator.Create(random, 1U, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random long integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, long, long)"/>
		public static ILongGenerator MakeRangeOCGenerator(this IRandom random, long lowerExclusive, long upperInclusive)
		{
			return BufferedLongRangeGenerator.Create(random, lowerExclusive + 1L, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random long integers in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, long)"/>
		public static ILongGenerator MakeRangeOCGenerator(this IRandom random, long upperInclusive)
		{
			return BufferedLongRangeGenerator.Create(random, 1L, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned long integers in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, ulong, ulong)"/>
		public static IULongGenerator MakeRangeOCGenerator(this IRandom random, ulong lowerExclusive, ulong upperInclusive)
		{
			return BufferedULongRangeGenerator.Create(random, lowerExclusive + 1UL, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned long integers in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeOC(IRandom, ulong)"/>
		public static IULongGenerator MakeRangeOCGenerator(this IRandom random, ulong upperInclusive)
		{
			return BufferedULongRangeGenerator.Create(random, 1UL, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random floats in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.RangeOC(IRandom, float, float)"/>
		public static IFloatGenerator MakeRangeOCGenerator(this IRandom random, float lowerExclusive, float upperInclusive)
		{
			return FloatRangeGenerator.CreateOC(random, lowerExclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random floats in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.RangeOC(IRandom, float)"/>
		public static IFloatGenerator MakeRangeOCGenerator(this IRandom random, float upperInclusive)
		{
			return FloatRangeGenerator.CreateOC(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random doubles in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.RangeOC(IRandom, double, double)"/>
		public static IDoubleGenerator MakeRangeOCGenerator(this IRandom random, double lowerExclusive, double upperInclusive)
		{
			return DoubleRangeGenerator.CreateOC(random, lowerExclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random doubles in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.RangeOC(IRandom, double)"/>
		public static IDoubleGenerator MakeRangeOCGenerator(this IRandom random, double upperInclusive)
		{
			return DoubleRangeGenerator.CreateOC(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random floats in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeOC(IRandom, float, float)"/>
		public static IFloatGenerator MakePreciseRangeOCGenerator(this IRandom random, float lowerExclusive, float upperInclusive)
		{
			return FloatRangeGenerator.CreatePreciseOC(random, lowerExclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random floats in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeOC(IRandom, float)"/>
		public static IFloatGenerator MakePreciseRangeOCGenerator(this IRandom random, float upperInclusive)
		{
			return FloatRangeGenerator.CreatePreciseOC(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  Generated numbers will be strictly greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random doubles in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeOC(IRandom, double, double)"/>
		public static IDoubleGenerator MakePreciseRangeOCGenerator(this IRandom random, double lowerExclusive, double upperInclusive)
		{
			return DoubleRangeGenerator.CreatePreciseOC(random, lowerExclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random doubles in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeOC(IRandom, double)"/>
		public static IDoubleGenerator MakePreciseRangeOCGenerator(this IRandom random, double upperInclusive)
		{
			return DoubleRangeGenerator.CreatePreciseOC(random, upperInclusive);
		}

		#endregion

		#region Make Range Closed/Closed Generator

		/// <summary>
		/// Returns a range generator which will produce signed bytes greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random signed bytes in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, sbyte, sbyte)"/>
		public static ISByteGenerator MakeRangeCCGenerator(this IRandom random, sbyte lowerInclusive, sbyte upperInclusive)
		{
			return BufferedSByteRangeGenerator.Create(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce signed bytes greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random signed bytes in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, sbyte)"/>
		public static ISByteGenerator MakeRangeCCGenerator(this IRandom random, sbyte upperInclusive)
		{
			return BufferedSByteRangeGenerator.Create(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce bytes greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random bytes in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, byte, byte)"/>
		public static IByteGenerator MakeRangeCCGenerator(this IRandom random, byte lowerInclusive, byte upperInclusive)
		{
			return BufferedByteRangeGenerator.Create(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce bytes greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random bytes in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, byte)"/>
		public static IByteGenerator MakeRangeCCGenerator(this IRandom random, byte upperInclusive)
		{
			return BufferedByteRangeGenerator.Create(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce short integers greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random short integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, short, short)"/>
		public static IShortGenerator MakeRangeCCGenerator(this IRandom random, short lowerInclusive, short upperInclusive)
		{
			return BufferedShortRangeGenerator.Create(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce short integers greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random short integers in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, short)"/>
		public static IShortGenerator MakeRangeCCGenerator(this IRandom random, short upperInclusive)
		{
			return BufferedShortRangeGenerator.Create(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned short integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, ushort, ushort)"/>
		public static IUShortGenerator MakeRangeCCGenerator(this IRandom random, ushort lowerInclusive, ushort upperInclusive)
		{
			return BufferedUShortRangeGenerator.Create(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned short integers in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, ushort)"/>
		public static IUShortGenerator MakeRangeCCGenerator(this IRandom random, ushort upperInclusive)
		{
			return BufferedUShortRangeGenerator.Create(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce integers greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, int, int)"/>
		public static IIntGenerator MakeRangeCCGenerator(this IRandom random, int lowerInclusive, int upperInclusive)
		{
			return BufferedIntRangeGenerator.Create(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce integers greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random integers in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, int)"/>
		public static IIntGenerator MakeRangeCCGenerator(this IRandom random, int upperInclusive)
		{
			return BufferedIntRangeGenerator.Create(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, uint, uint)"/>
		public static IUIntGenerator MakeRangeCCGenerator(this IRandom random, uint lowerInclusive, uint upperInclusive)
		{
			return BufferedUIntRangeGenerator.Create(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned integers in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, uint)"/>
		public static IUIntGenerator MakeRangeCCGenerator(this IRandom random, uint upperInclusive)
		{
			return BufferedUIntRangeGenerator.Create(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random long integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, long, long)"/>
		public static ILongGenerator MakeRangeCCGenerator(this IRandom random, long lowerInclusive, long upperInclusive)
		{
			return BufferedLongRangeGenerator.Create(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random long integers in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, long)"/>
		public static ILongGenerator MakeRangeCCGenerator(this IRandom random, long upperInclusive)
		{
			return BufferedLongRangeGenerator.Create(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned long integers in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, ulong, ulong)"/>
		public static IULongGenerator MakeRangeCCGenerator(this IRandom random, ulong lowerInclusive, ulong upperInclusive)
		{
			return BufferedULongRangeGenerator.Create(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random unsigned long integers in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomInteger.RangeCC(IRandom, ulong)"/>
		public static IULongGenerator MakeRangeCCGenerator(this IRandom random, ulong upperInclusive)
		{
			return BufferedULongRangeGenerator.Create(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random floats in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.RangeCC(IRandom, float, float)"/>
		public static IFloatGenerator MakeRangeCCGenerator(this IRandom random, float lowerInclusive, float upperInclusive)
		{
			return FloatRangeGenerator.CreateCC(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random floats in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.RangeCC(IRandom, float)"/>
		public static IFloatGenerator MakeRangeCCGenerator(this IRandom random, float upperInclusive)
		{
			return FloatRangeGenerator.CreateCC(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random doubles in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.RangeCC(IRandom, double, double)"/>
		public static IDoubleGenerator MakeRangeCCGenerator(this IRandom random, double lowerInclusive, double upperInclusive)
		{
			return DoubleRangeGenerator.CreateCC(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random doubles in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.RangeCC(IRandom, double)"/>
		public static IDoubleGenerator MakeRangeCCGenerator(this IRandom random, double upperInclusive)
		{
			return DoubleRangeGenerator.CreateCC(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random floats in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeCC(IRandom, float, float)"/>
		public static IFloatGenerator MakePreciseRangeCCGenerator(this IRandom random, float lowerInclusive, float upperInclusive)
		{
			return FloatRangeGenerator.CreatePreciseCC(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random floats in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeCC(IRandom, float)"/>
		public static IFloatGenerator MakePreciseRangeCCGenerator(this IRandom random, float upperInclusive)
		{
			return FloatRangeGenerator.CreatePreciseCC(random, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  Generated numbers will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random doubles in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeCC(IRandom, double, double)"/>
		public static IDoubleGenerator MakePreciseRangeCCGenerator(this IRandom random, double lowerInclusive, double upperInclusive)
		{
			return DoubleRangeGenerator.CreatePreciseCC(random, lowerInclusive, upperInclusive);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  Generated numbers will be less than or equal to this value.</param>
		/// <returns>A range generator producing random doubles in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <seealso cref="RandomFloatingPoint.PreciseRangeCC(IRandom, double)"/>
		public static IDoubleGenerator MakePreciseRangeCCGenerator(this IRandom random, double upperInclusive)
		{
			return DoubleRangeGenerator.CreatePreciseCC(random, upperInclusive);
		}

		#endregion

		#region Float Generators

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.FloatOO(IRandom)"/>
		public static IFloatGenerator MakeFloatOOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.FloatCO(IRandom)"/>
		public static IFloatGenerator MakeFloatCOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.FloatOC(IRandom)"/>
		public static IFloatGenerator MakeFloatOCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.FloatCC(IRandom)"/>
		public static IFloatGenerator MakeFloatCCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateCC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatOO(IRandom)"/>
		public static IFloatGenerator MakeSignedFloatOOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatCO(IRandom)"/>
		public static IFloatGenerator MakeSignedFloatCOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatOC(IRandom)"/>
		public static IFloatGenerator MakeSignedFloatOCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatCC(IRandom)"/>
		public static IFloatGenerator MakeSignedFloatCCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedCC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to one and strictly less than two.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [1, 2).</returns>
		/// <remarks><para>Given the implementation details, this function is slightly faster than the other unit ranges.</para></remarks>
		/// <seealso cref="RandomFloatingPoint.FloatC1O2(IRandom)"/>
		public static IFloatGenerator MakeFloatC1O2Generator(this IRandom random)
		{
			return FloatRangeGenerator.CreateC1O2(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to two and strictly less than four.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [2, 4).</returns>
		/// <para>Given the implementation details, this function is a quick way to generate a number with a range of 2.</para>
		/// <seealso cref="RandomFloatingPoint.FloatC2O4(IRandom)"/>
		public static IFloatGenerator MakeFloatC2O4Generator(this IRandom random)
		{
			return FloatRangeGenerator.CreateC2O4(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.FloatOO(IRandom)"/>
		public static IFloatGenerator MakePreciseFloatOOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.FloatCO(IRandom)"/>
		public static IFloatGenerator MakePreciseFloatCOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.FloatOC(IRandom)"/>
		public static IFloatGenerator MakePreciseFloatOCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.FloatCC(IRandom)"/>
		public static IFloatGenerator MakePreciseFloatCCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateCC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatOO(IRandom)"/>
		public static IFloatGenerator MakePreciseSignedFloatOOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatCO(IRandom)"/>
		public static IFloatGenerator MakePreciseSignedFloatCOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatOC(IRandom)"/>
		public static IFloatGenerator MakePreciseSignedFloatOCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatCC(IRandom)"/>
		public static IFloatGenerator MakePreciseSignedFloatCCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedCC(random);
		}

		#endregion

		#region Double Generators

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleOO(IRandom)"/>
		public static IDoubleGenerator MakeDoubleOOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleCO(IRandom)"/>
		public static IDoubleGenerator MakeDoubleCOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleOC(IRandom)"/>
		public static IDoubleGenerator MakeDoubleOCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleCC(IRandom)"/>
		public static IDoubleGenerator MakeDoubleCCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateCC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleOO(IRandom)"/>
		public static IDoubleGenerator MakeSignedDoubleOOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleCO(IRandom)"/>
		public static IDoubleGenerator MakeSignedDoubleCOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleOC(IRandom)"/>
		public static IDoubleGenerator MakeSignedDoubleOCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleCC(IRandom)"/>
		public static IDoubleGenerator MakeSignedDoubleCCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedCC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to one and strictly less than two.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [1, 2).</returns>
		/// <remarks><para>Given the implementation details, this function is slightly faster than the other unit ranges.</para></remarks>
		/// <seealso cref="RandomFloatingPoint.DoubleC1O2(IRandom)"/>
		public static IDoubleGenerator MakeDoubleC1O2Generator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateC1O2(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to two and strictly less than four.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [2, 4).</returns>
		/// <para>Given the implementation details, this function is a quick way to generate a number with a range of 2.</para>
		/// <seealso cref="RandomFloatingPoint.DoubleC2O4(IRandom)"/>
		public static IDoubleGenerator MakeDoubleC2O4Generator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateC2O4(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleOO(IRandom)"/>
		public static IDoubleGenerator MakePreciseDoubleOOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleCO(IRandom)"/>
		public static IDoubleGenerator MakePreciseDoubleCOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleOC(IRandom)"/>
		public static IDoubleGenerator MakePreciseDoubleOCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleCC(IRandom)"/>
		public static IDoubleGenerator MakePreciseDoubleCCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateCC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleOO(IRandom)"/>
		public static IDoubleGenerator MakePreciseSignedDoubleOOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleCO(IRandom)"/>
		public static IDoubleGenerator MakePreciseSignedDoubleCOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleOC(IRandom)"/>
		public static IDoubleGenerator MakePreciseSignedDoubleOCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleCC(IRandom)"/>
		public static IDoubleGenerator MakePreciseSignedDoubleCCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedCC(random);
		}

		#endregion
	}
}
