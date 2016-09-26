/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// An interface for any generator of numeric data within a range, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IRangeGenerator<TNumber>
	{
		/// <summary>
		/// Get the next number produced by the generator.
		/// </summary>
		/// <returns>The next number in the sequence determined by the generator implementation.</returns>
		TNumber Next();
	}

	/// <summary>
	/// A static class of extension methods for generating random numbers within custom ranges.
	/// </summary>
	public static class RandomRangeGenerator
	{
		#region Private Concrete Range Generators

		private static class BufferedSByteRangeGenerator
		{
			public static IRangeGenerator<sbyte> Create(IRandom random, sbyte rangeMin, sbyte rangeMax)
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

			public static IRangeGenerator<sbyte> Create(IRandom random, sbyte rangeMax)
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<sbyte>
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public sbyte Next() { return (sbyte)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<sbyte>
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public sbyte Next() { return (sbyte)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<sbyte>
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public sbyte Next() { return (sbyte)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<sbyte>
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, sbyte rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (uint)rangeMin; }
				public sbyte Next() { return (sbyte)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<sbyte>
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, sbyte rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public sbyte Next() { return (sbyte)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<sbyte>
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, sbyte rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public sbyte Next() { return (sbyte)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedByteRangeGenerator
		{
			public static IRangeGenerator<byte> Create(IRandom random, byte rangeMin, byte rangeMax)
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

			public static IRangeGenerator<byte> Create(IRandom random, byte rangeMax)
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<byte>
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public byte Next() { return (byte)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<byte>
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public byte Next() { return (byte)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<byte>
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public byte Next() { return (byte)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<byte>
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, byte rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<byte>
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, byte rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<byte>
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, byte rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public byte Next() { return (byte)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedShortRangeGenerator
		{
			public static IRangeGenerator<short> Create(IRandom random, short rangeMin, short rangeMax)
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

			public static IRangeGenerator<short> Create(IRandom random, short rangeMax)
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<short>
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public short Next() { return (short)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<short>
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public short Next() { return (short)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<short>
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public short Next() { return (short)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<short>
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, short rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (uint)rangeMin; }
				public short Next() { return (short)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<short>
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, short rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public short Next() { return (short)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<short>
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, short rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public short Next() { return (short)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedUShortRangeGenerator
		{
			public static IRangeGenerator<ushort> Create(IRandom random, ushort rangeMin, ushort rangeMax)
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

			public static IRangeGenerator<ushort> Create(IRandom random, ushort rangeMax)
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<ushort>
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public ushort Next() { return (ushort)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<ushort>
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public ushort Next() { return (ushort)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<ushort>
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public ushort Next() { return (ushort)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<ushort>
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, ushort rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public ushort Next() { return (ushort)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<ushort>
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, ushort rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public ushort Next() { return (ushort)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<ushort>
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, ushort rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public ushort Next() { return (ushort)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedIntRangeGenerator
		{
			public static IRangeGenerator<int> Create(IRandom random, int rangeMin, int rangeMax)
			{
				if (rangeMin == 0) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				uint rangeSizeMinusOne = (uint)(rangeMax - rangeMin);

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

			public static IRangeGenerator<int> Create(IRandom random, int rangeMax)
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<int>
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public int Next() { return (int)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<int>
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public int Next() { return (int)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<int>
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public int Next() { return (int)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<int>
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, int rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (uint)rangeMin; }
				public int Next() { return (int)(Next32() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<int>
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, int rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public int Next() { return (int)(Next32() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<int>
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, int rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = (uint)rangeMin; }
				public int Next() { return (int)(Next32() + _rangeMin); }
			}
		}

		private static class BufferedUIntRangeGenerator
		{
			public static IRangeGenerator<uint> Create(IRandom random, uint rangeMin, uint rangeMax)
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

			public static IRangeGenerator<uint> Create(IRandom random, uint rangeMax)
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<uint>
			{
				public AnyRangeGenerator(IRandom random, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public uint Next() { return (uint)Next32(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<uint>
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public uint Next() { return (uint)Next32(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<uint>
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { }
				public uint Next() { return (uint)Next32(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<uint>
			{
				private uint _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, uint rangeMin, uint rangeSizeMinusOne, uint bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return (uint)Next32() + _rangeMin; }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<uint>
			{
				private uint _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, uint rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return (uint)Next32() + _rangeMin; }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<uint>
			{
				private uint _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, uint rangeMin, int bitCount, uint bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public uint Next() { return (uint)Next32() + _rangeMin; }
			}
		}

		private static class BufferedLongRangeGenerator
		{
			public static IRangeGenerator<long> Create(IRandom random, long rangeMin, long rangeMax)
			{
				if (rangeMin == 0) return Create(random, rangeMax);
				if (rangeMax < rangeMin) throw new System.ArgumentException("The range maximum cannot be smaller than the range minimum.", "rangeMax");

				ulong rangeSizeMinusOne = (ulong)(rangeMax - rangeMin);

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

			public static IRangeGenerator<long> Create(IRandom random, long rangeMax)
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<long>
			{
				public AnyRangeGenerator(IRandom random, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public long Next() { return (long)Next64(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<long>
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public long Next() { return (long)Next64(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<long>
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public long Next() { return (long)Next64(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<long>
			{
				private ulong _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, long rangeMin, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = (ulong)rangeMin; }
				public long Next() { return (long)(Next64() + _rangeMin); }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<long>
			{
				private ulong _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, long rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = (ulong)rangeMin; }
				public long Next() { return (long)(Next64() + _rangeMin); }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<long>
			{
				private ulong _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, long rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = (ulong)rangeMin; }
				public long Next() { return (long)(Next64() + _rangeMin); }
			}
		}

		private static class BufferedULongRangeGenerator
		{
			public static IRangeGenerator<ulong> Create(IRandom random, ulong rangeMin, ulong rangeMax)
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

			public static IRangeGenerator<ulong> Create(IRandom random, ulong rangeMax)
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

			private class AnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<ulong>
			{
				public AnyRangeGenerator(IRandom random, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { }
				public ulong Next() { return Next64(); }
			}

			private class Pow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<ulong>
			{
				public Pow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public ulong Next() { return Next64(); }
			}

			private class PowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<ulong>
			{
				public PowPow2RangeGenerator(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }
				public ulong Next() { return Next64(); }
			}

			private class OffsetAnyRangeGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<ulong>
			{
				private ulong _rangeMin;
				public OffsetAnyRangeGenerator(IRandom random, ulong rangeMin, ulong rangeSizeMinusOne, ulong bitMask) : base(random, rangeSizeMinusOne, bitMask) { _rangeMin = rangeMin; }
				public ulong Next() { return Next64() + _rangeMin; }
			}

			private class OffsetPow2RangeGenerator : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<ulong>
			{
				private ulong _rangeMin;
				public OffsetPow2RangeGenerator(IRandom random, ulong rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public ulong Next() { return Next64() + _rangeMin; }
			}

			private class OffsetPowPow2RangeGenerator : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<ulong>
			{
				private ulong _rangeMin;
				public OffsetPowPow2RangeGenerator(IRandom random, ulong rangeMin, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { _rangeMin = rangeMin; }
				public ulong Next() { return Next64() + _rangeMin; }
			}
		}

		private static class FloatRangeGenerator
		{
			public static IRangeGenerator<float> CreateOO(IRandom random, float rangeMin, float rangeMax)
			{
				return new RangeOOGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<float> CreateOO(IRandom random, float rangeMax)
			{
				return new RangeZOOGenerator(random, rangeMax);
			}

			public static IRangeGenerator<float> CreateOO(IRandom random)
			{
				return new UnitOOGenerator(random);
			}

			public static IRangeGenerator<float> CreateCO(IRandom random, float rangeMin, float rangeMax)
			{
				return new RangeCOGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<float> CreateCO(IRandom random, float rangeMax)
			{
				return new RangeZCOGenerator(random, rangeMax);
			}

			public static IRangeGenerator<float> CreateCO(IRandom random)
			{
				return new UnitCOGenerator(random);
			}

			public static IRangeGenerator<float> CreateOC(IRandom random, float rangeMin, float rangeMax)
			{
				return new RangeOCGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<float> CreateOC(IRandom random, float rangeMax)
			{
				return new RangeZOCGenerator(random, rangeMax);
			}

			public static IRangeGenerator<float> CreateOC(IRandom random)
			{
				return new UnitOCGenerator(random);
			}

			public static IRangeGenerator<float> CreateCC(IRandom random, float rangeMin, float rangeMax)
			{
				return new RangeCCGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<float> CreateCC(IRandom random, float rangeMax)
			{
				return new RangeZCCGenerator(random, rangeMax);
			}

			public static IRangeGenerator<float> CreateCC(IRandom random)
			{
				return new UnitCCGenerator(random);
			}

			public static IRangeGenerator<float> CreateSignedOO(IRandom random)
			{
				return new SignedOOGenerator(random);
			}

			public static IRangeGenerator<float> CreateSignedCO(IRandom random)
			{
				return new SignedCOGenerator(random);
			}

			public static IRangeGenerator<float> CreateSignedOC(IRandom random)
			{
				return new SignedOCGenerator(random);
			}

			public static IRangeGenerator<float> CreateSignedCC(IRandom random)
			{
				return new SignedCCGenerator(random);
			}

			public static IRangeGenerator<float> CreateC1O2(IRandom random)
			{
				return new UnitC1O2Generator(random);
			}

			public static IRangeGenerator<float> CreateC2O4(IRandom random)
			{
				return new UnitC2O4Generator(random);
			}

			public static IRangeGenerator<float> CreatePreciseOO(IRandom random, float rangeMin, float rangeMax)
			{
				return new PreciseRangeOOGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<float> CreatePreciseOO(IRandom random, float rangeMax)
			{
				return new PreciseRangeZOOGenerator(random, rangeMax);
			}

			public static IRangeGenerator<float> CreatePreciseOO(IRandom random)
			{
				return new PreciseUnitOOGenerator(random);
			}

			public static IRangeGenerator<float> CreatePreciseCO(IRandom random, float rangeMin, float rangeMax)
			{
				return new PreciseRangeCOGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<float> CreatePreciseCO(IRandom random, float rangeMax)
			{
				return new PreciseRangeZCOGenerator(random, rangeMax);
			}

			public static IRangeGenerator<float> CreatePreciseCO(IRandom random)
			{
				return new PreciseUnitCOGenerator(random);
			}

			public static IRangeGenerator<float> CreatePreciseOC(IRandom random, float rangeMin, float rangeMax)
			{
				return new PreciseRangeOCGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<float> CreatePreciseOC(IRandom random, float rangeMax)
			{
				return new PreciseRangeZOCGenerator(random, rangeMax);
			}

			public static IRangeGenerator<float> CreatePreciseOC(IRandom random)
			{
				return new PreciseUnitOCGenerator(random);
			}

			public static IRangeGenerator<float> CreatePreciseCC(IRandom random, float rangeMin, float rangeMax)
			{
				return new PreciseRangeCCGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<float> CreatePreciseCC(IRandom random, float rangeMax)
			{
				return new PreciseRangeZCCGenerator(random, rangeMax);
			}

			public static IRangeGenerator<float> CreatePreciseCC(IRandom random)
			{
				return new PreciseUnitCCGenerator(random);
			}

			public static IRangeGenerator<float> CreatePreciseSignedOO(IRandom random)
			{
				return new PreciseSignedOOGenerator(random);
			}

			public static IRangeGenerator<float> CreatePreciseSignedCO(IRandom random)
			{
				return new PreciseSignedCOGenerator(random);
			}

			public static IRangeGenerator<float> CreatePreciseSignedOC(IRandom random)
			{
				return new PreciseSignedOCGenerator(random);
			}

			public static IRangeGenerator<float> CreatePreciseSignedCC(IRandom random)
			{
				return new PreciseSignedCCGenerator(random);
			}

			private class UnitOOGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public UnitOOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatOO(); }
			}

			private class UnitCOGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public UnitCOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatCO(); }
			}

			private class UnitOCGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public UnitOCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatOC(); }
			}

			private class UnitCCGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public UnitCCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatCC(); }
			}

			private class SignedOOGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public SignedOOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.SignedFloatOO(); }
			}

			private class SignedCOGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public SignedCOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.SignedFloatCO(); }
			}

			private class SignedOCGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public SignedOCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.SignedFloatOC(); }
			}

			private class SignedCCGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public SignedCCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.SignedFloatCC(); }
			}

			private class UnitC1O2Generator : IRangeGenerator<float>
			{
				private IRandom _random;
				public UnitC1O2Generator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatC1O2(); }
			}

			private class UnitC2O4Generator : IRangeGenerator<float>
			{
				private IRandom _random;
				public UnitC2O4Generator(IRandom random) { _random = random; }
				public float Next() { return _random.FloatC2O4(); }
			}

			private class PreciseUnitOOGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public PreciseUnitOOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseFloatOO(); }
			}

			private class PreciseUnitCOGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public PreciseUnitCOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseFloatCO(); }
			}

			private class PreciseUnitOCGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public PreciseUnitOCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseFloatOC(); }
			}

			private class PreciseUnitCCGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public PreciseUnitCCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseFloatCC(); }
			}

			private class PreciseSignedOOGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public PreciseSignedOOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseSignedFloatOO(); }
			}

			private class PreciseSignedCOGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public PreciseSignedCOGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseSignedFloatCO(); }
			}

			private class PreciseSignedOCGenerator : IRangeGenerator<float>
			{
				private IRandom _random;
				public PreciseSignedOCGenerator(IRandom random) { _random = random; }
				public float Next() { return _random.PreciseSignedFloatOC(); }
			}

			private class PreciseSignedCCGenerator : IRangeGenerator<float>
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

			private class RangeOOGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				private float _rangeMin;
				public RangeOOGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.RangeOO(_rangeMin, _rangeMax); }
			}

			private class RangeZOOGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				public RangeZOOGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.RangeOO(_rangeMax); }
			}

			private class RangeCOGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				private float _rangeMin;
				public RangeCOGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.RangeCO(_rangeMin, _rangeMax); }
			}

			private class RangeZCOGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				public RangeZCOGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.RangeCO(_rangeMax); }
			}

			private class RangeOCGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				private float _rangeMin;
				public RangeOCGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.RangeOC(_rangeMin, _rangeMax); }
			}

			private class RangeZOCGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				public RangeZOCGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.RangeOC(_rangeMax); }
			}

			private class RangeCCGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				private float _rangeMin;
				public RangeCCGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.RangeCC(_rangeMin, _rangeMax); }
			}

			private class RangeZCCGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				public RangeZCCGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.RangeCC(_rangeMax); }
			}

			private class PreciseRangeOOGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				private float _rangeMin;
				public PreciseRangeOOGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.PreciseRangeOO(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZOOGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				public PreciseRangeZOOGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.PreciseRangeOO(_rangeMax); }
			}

			private class PreciseRangeCOGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				private float _rangeMin;
				public PreciseRangeCOGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.PreciseRangeCO(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZCOGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				public PreciseRangeZCOGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.PreciseRangeCO(_rangeMax); }
			}

			private class PreciseRangeOCGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				private float _rangeMin;
				public PreciseRangeOCGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.PreciseRangeOC(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZOCGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				public PreciseRangeZOCGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.PreciseRangeOC(_rangeMax); }
			}

			private class PreciseRangeCCGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				private float _rangeMin;
				public PreciseRangeCCGenerator(IRandom random, float rangeMin, float rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public float Next() { return _random.PreciseRangeCC(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZCCGenerator : RangeGeneratorBase, IRangeGenerator<float>
			{
				public PreciseRangeZCCGenerator(IRandom random, float rangeMax) : base(random, rangeMax) { }
				public float Next() { return _random.PreciseRangeCC(_rangeMax); }
			}
		}

		private static class DoubleRangeGenerator
		{
			public static IRangeGenerator<double> CreateOO(IRandom random, double rangeMin, double rangeMax)
			{
				return new RangeOOGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<double> CreateOO(IRandom random, double rangeMax)
			{
				return new RangeZOOGenerator(random, rangeMax);
			}

			public static IRangeGenerator<double> CreateOO(IRandom random)
			{
				return new UnitOOGenerator(random);
			}

			public static IRangeGenerator<double> CreateCO(IRandom random, double rangeMin, double rangeMax)
			{
				return new RangeCOGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<double> CreateCO(IRandom random, double rangeMax)
			{
				return new RangeZCOGenerator(random, rangeMax);
			}

			public static IRangeGenerator<double> CreateCO(IRandom random)
			{
				return new UnitCOGenerator(random);
			}

			public static IRangeGenerator<double> CreateOC(IRandom random, double rangeMin, double rangeMax)
			{
				return new RangeOCGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<double> CreateOC(IRandom random, double rangeMax)
			{
				return new RangeZOCGenerator(random, rangeMax);
			}

			public static IRangeGenerator<double> CreateOC(IRandom random)
			{
				return new UnitOCGenerator(random);
			}

			public static IRangeGenerator<double> CreateCC(IRandom random, double rangeMin, double rangeMax)
			{
				return new RangeCCGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<double> CreateCC(IRandom random, double rangeMax)
			{
				return new RangeZCCGenerator(random, rangeMax);
			}

			public static IRangeGenerator<double> CreateCC(IRandom random)
			{
				return new UnitCCGenerator(random);
			}

			public static IRangeGenerator<double> CreateSignedOO(IRandom random)
			{
				return new SignedOOGenerator(random);
			}

			public static IRangeGenerator<double> CreateSignedCO(IRandom random)
			{
				return new SignedCOGenerator(random);
			}

			public static IRangeGenerator<double> CreateSignedOC(IRandom random)
			{
				return new SignedOCGenerator(random);
			}

			public static IRangeGenerator<double> CreateSignedCC(IRandom random)
			{
				return new SignedCCGenerator(random);
			}

			public static IRangeGenerator<double> CreateC1O2(IRandom random)
			{
				return new UnitC1O2Generator(random);
			}

			public static IRangeGenerator<double> CreateC2O4(IRandom random)
			{
				return new UnitC2O4Generator(random);
			}

			public static IRangeGenerator<double> CreatePreciseOO(IRandom random, double rangeMin, double rangeMax)
			{
				return new PreciseRangeOOGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<double> CreatePreciseOO(IRandom random, double rangeMax)
			{
				return new PreciseRangeZOOGenerator(random, rangeMax);
			}

			public static IRangeGenerator<double> CreatePreciseOO(IRandom random)
			{
				return new PreciseUnitOOGenerator(random);
			}

			public static IRangeGenerator<double> CreatePreciseCO(IRandom random, double rangeMin, double rangeMax)
			{
				return new PreciseRangeCOGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<double> CreatePreciseCO(IRandom random, double rangeMax)
			{
				return new PreciseRangeZCOGenerator(random, rangeMax);
			}

			public static IRangeGenerator<double> CreatePreciseCO(IRandom random)
			{
				return new PreciseUnitCOGenerator(random);
			}

			public static IRangeGenerator<double> CreatePreciseOC(IRandom random, double rangeMin, double rangeMax)
			{
				return new PreciseRangeOCGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<double> CreatePreciseOC(IRandom random, double rangeMax)
			{
				return new PreciseRangeZOCGenerator(random, rangeMax);
			}

			public static IRangeGenerator<double> CreatePreciseOC(IRandom random)
			{
				return new PreciseUnitOCGenerator(random);
			}

			public static IRangeGenerator<double> CreatePreciseCC(IRandom random, double rangeMin, double rangeMax)
			{
				return new PreciseRangeCCGenerator(random, rangeMin, rangeMax);
			}

			public static IRangeGenerator<double> CreatePreciseCC(IRandom random, double rangeMax)
			{
				return new PreciseRangeZCCGenerator(random, rangeMax);
			}

			public static IRangeGenerator<double> CreatePreciseCC(IRandom random)
			{
				return new PreciseUnitCCGenerator(random);
			}

			public static IRangeGenerator<double> CreatePreciseSignedOO(IRandom random)
			{
				return new PreciseSignedOOGenerator(random);
			}

			public static IRangeGenerator<double> CreatePreciseSignedCO(IRandom random)
			{
				return new PreciseSignedCOGenerator(random);
			}

			public static IRangeGenerator<double> CreatePreciseSignedOC(IRandom random)
			{
				return new PreciseSignedOCGenerator(random);
			}

			public static IRangeGenerator<double> CreatePreciseSignedCC(IRandom random)
			{
				return new PreciseSignedCCGenerator(random);
			}

			private class UnitOOGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public UnitOOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleOO(); }
			}

			private class UnitCOGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public UnitCOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleCO(); }
			}

			private class UnitOCGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public UnitOCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleOC(); }
			}

			private class UnitCCGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public UnitCCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleCC(); }
			}

			private class SignedOOGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public SignedOOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.SignedDoubleOO(); }
			}

			private class SignedCOGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public SignedCOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.SignedDoubleCO(); }
			}

			private class SignedOCGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public SignedOCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.SignedDoubleOC(); }
			}

			private class SignedCCGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public SignedCCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.SignedDoubleCC(); }
			}

			private class UnitC1O2Generator : IRangeGenerator<double>
			{
				private IRandom _random;
				public UnitC1O2Generator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleC1O2(); }
			}

			private class UnitC2O4Generator : IRangeGenerator<double>
			{
				private IRandom _random;
				public UnitC2O4Generator(IRandom random) { _random = random; }
				public double Next() { return _random.DoubleC2O4(); }
			}

			private class PreciseUnitOOGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public PreciseUnitOOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseDoubleOO(); }
			}

			private class PreciseUnitCOGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public PreciseUnitCOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseDoubleCO(); }
			}

			private class PreciseUnitOCGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public PreciseUnitOCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseDoubleOC(); }
			}

			private class PreciseUnitCCGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public PreciseUnitCCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseDoubleCC(); }
			}

			private class PreciseSignedOOGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public PreciseSignedOOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseSignedDoubleOO(); }
			}

			private class PreciseSignedCOGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public PreciseSignedCOGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseSignedDoubleCO(); }
			}

			private class PreciseSignedOCGenerator : IRangeGenerator<double>
			{
				private IRandom _random;
				public PreciseSignedOCGenerator(IRandom random) { _random = random; }
				public double Next() { return _random.PreciseSignedDoubleOC(); }
			}

			private class PreciseSignedCCGenerator : IRangeGenerator<double>
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

			private class RangeOOGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				private double _rangeMin;
				public RangeOOGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.RangeOO(_rangeMin, _rangeMax); }
			}

			private class RangeZOOGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				public RangeZOOGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.RangeOO(_rangeMax); }
			}

			private class RangeCOGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				private double _rangeMin;
				public RangeCOGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.RangeCO(_rangeMin, _rangeMax); }
			}

			private class RangeZCOGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				public RangeZCOGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.RangeCO(_rangeMax); }
			}

			private class RangeOCGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				private double _rangeMin;
				public RangeOCGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.RangeOC(_rangeMin, _rangeMax); }
			}

			private class RangeZOCGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				public RangeZOCGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.RangeOC(_rangeMax); }
			}

			private class RangeCCGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				private double _rangeMin;
				public RangeCCGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.RangeCC(_rangeMin, _rangeMax); }
			}

			private class RangeZCCGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				public RangeZCCGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.RangeCC(_rangeMax); }
			}

			private class PreciseRangeOOGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				private double _rangeMin;
				public PreciseRangeOOGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.PreciseRangeOO(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZOOGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				public PreciseRangeZOOGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.PreciseRangeOO(_rangeMax); }
			}

			private class PreciseRangeCOGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				private double _rangeMin;
				public PreciseRangeCOGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.PreciseRangeCO(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZCOGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				public PreciseRangeZCOGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.PreciseRangeCO(_rangeMax); }
			}

			private class PreciseRangeOCGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				private double _rangeMin;
				public PreciseRangeOCGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.PreciseRangeOC(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZOCGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				public PreciseRangeZOCGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.PreciseRangeOC(_rangeMax); }
			}

			private class PreciseRangeCCGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				private double _rangeMin;
				public PreciseRangeCCGenerator(IRandom random, double rangeMin, double rangeMax) : base(random, rangeMax) { _rangeMin = rangeMin; }
				public double Next() { return _random.PreciseRangeCC(_rangeMin, _rangeMax); }
			}

			private class PreciseRangeZCCGenerator : RangeGeneratorBase, IRangeGenerator<double>
			{
				public PreciseRangeZCCGenerator(IRandom random, double rangeMax) : base(random, rangeMax) { }
				public double Next() { return _random.PreciseRangeCC(_rangeMax); }
			}
		}

		#endregion

		#region Full Range Int Generators

		/// <summary>
		/// Returns a range generator which will produce signed bytes greater than or equal to either 0 or <see cref="sbyte.MinValue"/> and less than or equal to <see cref="sbyte.MaxValue"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="excludeNegative">Specifies if the range should be limited just non-negative values.  Defaults to false.</param>
		/// <returns>A range generator producing random signed bytes in the range .</returns>
		/// <remarks>If <paramref name="excludeNegative"/> is false, the return value will be in the range
		/// [<see cref="sbyte.MinValue"/>, <see cref="sbyte.MaxValue"/>].  If <paramref name="excludeNegative"/>
		/// is true, the return value will be in the range [0, <see cref="sbyte.MaxValue"/>].</remarks>
		/// <seealso cref="RandomInteger.SByte(IRandom)"/>
		/// <seealso cref="RandomInteger.SByteNonNegative(IRandom)"/>
		public static IRangeGenerator<sbyte> MakeSByteGenerator(this IRandom random, bool excludeNegative = false)
		{
			return BufferedSByteRangeGenerator.Create(random, excludeNegative ? (sbyte)0 : sbyte.MinValue, sbyte.MaxValue);
		}

		/// <summary>
		/// Returns a range generator which will produce bytes greater than or equal to <see cref="byte.MinValue"/> and less than or equal to <see cref="byte.MaxValue"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random bytes in the range [<see cref="byte.MinValue"/>, <see cref="byte.MaxValue"/>].</returns>
		/// <seealso cref="RandomInteger.Byte(IRandom)"/>
		public static IRangeGenerator<byte> MakeByteGenerator(this IRandom random)
		{
			return BufferedByteRangeGenerator.Create(random, byte.MinValue, byte.MaxValue);
		}

		/// <summary>
		/// Returns a range generator which will produce short integers greater than or equal to either 0 or <see cref="short.MinValue"/> and less than or equal to <see cref="short.MaxValue"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="excludeNegative">Specifies if the range should be limited just non-negative values.  Defaults to false.</param>
		/// <returns>A range generator producing random short integers in the range .</returns>
		/// <remarks>If <paramref name="excludeNegative"/> is false, the return value will be in the range
		/// [<see cref="short.MinValue"/>, <see cref="short.MaxValue"/>].  If <paramref name="excludeNegative"/>
		/// is true, the return value will be in the range [0, <see cref="short.MaxValue"/>].</remarks>
		/// <seealso cref="RandomInteger.Short(IRandom)"/>
		/// <seealso cref="RandomInteger.ShortNonNegative(IRandom)"/>
		public static IRangeGenerator<short> MakeShortGenerator(this IRandom random, bool excludeNegative = false)
		{
			return BufferedShortRangeGenerator.Create(random, excludeNegative ? (short)0 : short.MinValue, short.MaxValue);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned short integers greater than or equal to <see cref="ushort.MinValue"/> and less than or equal to <see cref="ushort.MaxValue"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random unsigned short integers in the range [<see cref="ushort.MinValue"/>, <see cref="ushort.MaxValue"/>].</returns>
		/// <seealso cref="RandomInteger.UShort(IRandom)"/>
		public static IRangeGenerator<ushort> MakeUShortGenerator(this IRandom random)
		{
			return BufferedUShortRangeGenerator.Create(random, ushort.MinValue, ushort.MaxValue);
		}

		/// <summary>
		/// Returns a range generator which will produce integers greater than or equal to either 0 or <see cref="int.MinValue"/> and less than or equal to <see cref="int.MaxValue"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="excludeNegative">Specifies if the range should be limited just non-negative values.  Defaults to false.</param>
		/// <returns>A range generator producing random integers in the range .</returns>
		/// <remarks>If <paramref name="excludeNegative"/> is false, the return value will be in the range
		/// [<see cref="int.MinValue"/>, <see cref="int.MaxValue"/>].  If <paramref name="excludeNegative"/>
		/// is true, the return value will be in the range [0, <see cref="int.MaxValue"/>].</remarks>
		/// <seealso cref="RandomInteger.Int(IRandom)"/>
		/// <seealso cref="RandomInteger.IntNonNegative(IRandom)"/>
		public static IRangeGenerator<int> MakeIntGenerator(this IRandom random, bool excludeNegative = false)
		{
			return BufferedIntRangeGenerator.Create(random, excludeNegative ? 0 : int.MinValue, int.MaxValue);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned integers greater than or equal to <see cref="uint.MinValue"/> and less than or equal to <see cref="uint.MaxValue"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random unsigned integers in the range [<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].</returns>
		/// <seealso cref="RandomInteger.UInt(IRandom)"/>
		public static IRangeGenerator<uint> MakeUIntGenerator(this IRandom random)
		{
			return BufferedUIntRangeGenerator.Create(random, uint.MinValue, uint.MaxValue);
		}

		/// <summary>
		/// Returns a range generator which will produce long integers greater than or equal to either 0 or <see cref="long.MinValue"/> and less than or equal to <see cref="long.MaxValue"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="excludeNegative">Specifies if the range should be limited just non-negative values.  Defaults to false.</param>
		/// <returns>A range generator producing random long integers in the range .</returns>
		/// <remarks>If <paramref name="excludeNegative"/> is false, the return value will be in the range
		/// [<see cref="long.MinValue"/>, <see cref="long.MaxValue"/>].  If <paramref name="excludeNegative"/>
		/// is true, the return value will be in the range [0, <see cref="long.MaxValue"/>].</remarks>
		/// <seealso cref="RandomInteger.Long(IRandom)"/>
		/// <seealso cref="RandomInteger.LongNonNegative(IRandom)"/>
		public static IRangeGenerator<long> MakeLongGenerator(this IRandom random, bool excludeNegative = false)
		{
			return BufferedLongRangeGenerator.Create(random, excludeNegative ? 0L : long.MinValue, long.MaxValue);
		}

		/// <summary>
		/// Returns a range generator which will produce unsigned long integers greater than or equal to <see cref="ulong.MinValue"/> and less than or equal to <see cref="ulong.MaxValue"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random unsigned long integers in the range [<see cref="ulong.MinValue"/>, <see cref="ulong.MaxValue"/>].</returns>
		/// <seealso cref="RandomInteger.ULong(IRandom)"/>
		public static IRangeGenerator<ulong> MakeULongGenerator(this IRandom random)
		{
			return BufferedULongRangeGenerator.Create(random, ulong.MinValue, ulong.MaxValue);
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
		public static IRangeGenerator<sbyte> MakeRangeOOGenerator(this IRandom random, sbyte lowerExclusive, sbyte upperExclusive)
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
		public static IRangeGenerator<sbyte> MakeRangeOOGenerator(this IRandom random, sbyte upperExclusive)
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
		public static IRangeGenerator<byte> MakeRangeOOGenerator(this IRandom random, byte lowerExclusive, byte upperExclusive)
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
		public static IRangeGenerator<byte> MakeRangeOOGenerator(this IRandom random, byte upperExclusive)
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
		public static IRangeGenerator<short> MakeRangeOOGenerator(this IRandom random, short lowerExclusive, short upperExclusive)
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
		public static IRangeGenerator<short> MakeRangeOOGenerator(this IRandom random, short upperExclusive)
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
		public static IRangeGenerator<ushort> MakeRangeOOGenerator(this IRandom random, ushort lowerExclusive, ushort upperExclusive)
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
		public static IRangeGenerator<ushort> MakeRangeOOGenerator(this IRandom random, ushort upperExclusive)
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
		public static IRangeGenerator<int> MakeRangeOOGenerator(this IRandom random, int lowerExclusive, int upperExclusive)
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
		public static IRangeGenerator<int> MakeRangeOOGenerator(this IRandom random, int upperExclusive)
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
		public static IRangeGenerator<uint> MakeRangeOOGenerator(this IRandom random, uint lowerExclusive, uint upperExclusive)
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
		public static IRangeGenerator<uint> MakeRangeOOGenerator(this IRandom random, uint upperExclusive)
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
		public static IRangeGenerator<long> MakeRangeOOGenerator(this IRandom random, long lowerExclusive, long upperExclusive)
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
		public static IRangeGenerator<long> MakeRangeOOGenerator(this IRandom random, long upperExclusive)
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
		public static IRangeGenerator<ulong> MakeRangeOOGenerator(this IRandom random, ulong lowerExclusive, ulong upperExclusive)
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
		public static IRangeGenerator<ulong> MakeRangeOOGenerator(this IRandom random, ulong upperExclusive)
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
		public static IRangeGenerator<float> MakeRangeOOGenerator(this IRandom random, float lowerExclusive, float upperExclusive)
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
		public static IRangeGenerator<float> MakeRangeOOGenerator(this IRandom random, float upperExclusive)
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
		public static IRangeGenerator<float> MakePreciseRangeOOGenerator(this IRandom random, float lowerExclusive, float upperExclusive)
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
		public static IRangeGenerator<float> MakePreciseRangeOOGenerator(this IRandom random, float upperExclusive)
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
		public static IRangeGenerator<double> MakePreciseRangeOOGenerator(this IRandom random, double lowerExclusive, double upperExclusive)
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
		public static IRangeGenerator<double> MakePreciseRangeOOGenerator(this IRandom random, double upperExclusive)
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
		public static IRangeGenerator<sbyte> MakeRangeCOGenerator(this IRandom random, sbyte lowerInclusive, sbyte upperExclusive)
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
		public static IRangeGenerator<sbyte> MakeRangeCOGenerator(this IRandom random, sbyte upperExclusive)
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
		public static IRangeGenerator<byte> MakeRangeCOGenerator(this IRandom random, byte lowerInclusive, byte upperExclusive)
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
		public static IRangeGenerator<byte> MakeRangeCOGenerator(this IRandom random, byte upperExclusive)
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
		public static IRangeGenerator<short> MakeRangeCOGenerator(this IRandom random, short lowerInclusive, short upperExclusive)
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
		public static IRangeGenerator<short> MakeRangeCOGenerator(this IRandom random, short upperExclusive)
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
		public static IRangeGenerator<ushort> MakeRangeCOGenerator(this IRandom random, ushort lowerInclusive, ushort upperExclusive)
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
		public static IRangeGenerator<ushort> MakeRangeCOGenerator(this IRandom random, ushort upperExclusive)
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
		public static IRangeGenerator<int> MakeRangeCOGenerator(this IRandom random, int lowerInclusive, int upperExclusive)
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
		public static IRangeGenerator<int> MakeRangeCOGenerator(this IRandom random, int upperExclusive)
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
		public static IRangeGenerator<uint> MakeRangeCOGenerator(this IRandom random, uint lowerInclusive, uint upperExclusive)
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
		public static IRangeGenerator<uint> MakeRangeCOGenerator(this IRandom random, uint upperExclusive)
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
		public static IRangeGenerator<long> MakeRangeCOGenerator(this IRandom random, long lowerInclusive, long upperExclusive)
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
		public static IRangeGenerator<long> MakeRangeCOGenerator(this IRandom random, long upperExclusive)
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
		public static IRangeGenerator<ulong> MakeRangeCOGenerator(this IRandom random, ulong lowerInclusive, ulong upperExclusive)
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
		public static IRangeGenerator<ulong> MakeRangeCOGenerator(this IRandom random, ulong upperExclusive)
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
		public static IRangeGenerator<float> MakeRangeCOGenerator(this IRandom random, float lowerInclusive, float upperExclusive)
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
		public static IRangeGenerator<float> MakeRangeCOGenerator(this IRandom random, float upperExclusive)
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
		public static IRangeGenerator<double> MakeRangeCOGenerator(this IRandom random, double lowerInclusive, double upperExclusive)
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
		public static IRangeGenerator<double> MakeRangeCOGenerator(this IRandom random, double upperExclusive)
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
		public static IRangeGenerator<float> MakePreciseRangeCOGenerator(this IRandom random, float lowerInclusive, float upperExclusive)
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
		public static IRangeGenerator<float> MakePreciseRangeCOGenerator(this IRandom random, float upperExclusive)
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
		public static IRangeGenerator<double> MakePreciseRangeCOGenerator(this IRandom random, double lowerInclusive, double upperExclusive)
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
		public static IRangeGenerator<double> MakePreciseRangeCOGenerator(this IRandom random, double upperExclusive)
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
		public static IRangeGenerator<sbyte> MakeRangeOCGenerator(this IRandom random, sbyte lowerExclusive, sbyte upperInclusive)
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
		public static IRangeGenerator<sbyte> MakeRangeOCGenerator(this IRandom random, sbyte upperInclusive)
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
		public static IRangeGenerator<byte> MakeRangeOCGenerator(this IRandom random, byte lowerExclusive, byte upperInclusive)
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
		public static IRangeGenerator<byte> MakeRangeOCGenerator(this IRandom random, byte upperInclusive)
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
		public static IRangeGenerator<short> MakeRangeOCGenerator(this IRandom random, short lowerExclusive, short upperInclusive)
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
		public static IRangeGenerator<short> MakeRangeOCGenerator(this IRandom random, short upperInclusive)
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
		public static IRangeGenerator<ushort> MakeRangeOCGenerator(this IRandom random, ushort lowerExclusive, ushort upperInclusive)
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
		public static IRangeGenerator<ushort> MakeRangeOCGenerator(this IRandom random, ushort upperInclusive)
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
		public static IRangeGenerator<int> MakeRangeOCGenerator(this IRandom random, int lowerExclusive, int upperInclusive)
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
		public static IRangeGenerator<int> MakeRangeOCGenerator(this IRandom random, int upperInclusive)
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
		public static IRangeGenerator<uint> MakeRangeOCGenerator(this IRandom random, uint lowerExclusive, uint upperInclusive)
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
		public static IRangeGenerator<uint> MakeRangeOCGenerator(this IRandom random, uint upperInclusive)
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
		public static IRangeGenerator<long> MakeRangeOCGenerator(this IRandom random, long lowerExclusive, long upperInclusive)
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
		public static IRangeGenerator<long> MakeRangeOCGenerator(this IRandom random, long upperInclusive)
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
		public static IRangeGenerator<ulong> MakeRangeOCGenerator(this IRandom random, ulong lowerExclusive, ulong upperInclusive)
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
		public static IRangeGenerator<ulong> MakeRangeOCGenerator(this IRandom random, ulong upperInclusive)
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
		public static IRangeGenerator<float> MakeRangeOCGenerator(this IRandom random, float lowerExclusive, float upperInclusive)
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
		public static IRangeGenerator<float> MakeRangeOCGenerator(this IRandom random, float upperInclusive)
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
		public static IRangeGenerator<double> MakeRangeOCGenerator(this IRandom random, double lowerExclusive, double upperInclusive)
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
		public static IRangeGenerator<double> MakeRangeOCGenerator(this IRandom random, double upperInclusive)
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
		public static IRangeGenerator<float> MakePreciseRangeOCGenerator(this IRandom random, float lowerExclusive, float upperInclusive)
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
		public static IRangeGenerator<float> MakePreciseRangeOCGenerator(this IRandom random, float upperInclusive)
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
		public static IRangeGenerator<double> MakePreciseRangeOCGenerator(this IRandom random, double lowerExclusive, double upperInclusive)
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
		public static IRangeGenerator<double> MakePreciseRangeOCGenerator(this IRandom random, double upperInclusive)
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
		public static IRangeGenerator<sbyte> MakeRangeCCGenerator(this IRandom random, sbyte lowerInclusive, sbyte upperInclusive)
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
		public static IRangeGenerator<sbyte> MakeRangeCCGenerator(this IRandom random, sbyte upperInclusive)
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
		public static IRangeGenerator<byte> MakeRangeCCGenerator(this IRandom random, byte lowerInclusive, byte upperInclusive)
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
		public static IRangeGenerator<byte> MakeRangeCCGenerator(this IRandom random, byte upperInclusive)
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
		public static IRangeGenerator<short> MakeRangeCCGenerator(this IRandom random, short lowerInclusive, short upperInclusive)
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
		public static IRangeGenerator<short> MakeRangeCCGenerator(this IRandom random, short upperInclusive)
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
		public static IRangeGenerator<ushort> MakeRangeCCGenerator(this IRandom random, ushort lowerInclusive, ushort upperInclusive)
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
		public static IRangeGenerator<ushort> MakeRangeCCGenerator(this IRandom random, ushort upperInclusive)
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
		public static IRangeGenerator<int> MakeRangeCCGenerator(this IRandom random, int lowerInclusive, int upperInclusive)
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
		public static IRangeGenerator<int> MakeRangeCCGenerator(this IRandom random, int upperInclusive)
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
		public static IRangeGenerator<uint> MakeRangeCCGenerator(this IRandom random, uint lowerInclusive, uint upperInclusive)
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
		public static IRangeGenerator<uint> MakeRangeCCGenerator(this IRandom random, uint upperInclusive)
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
		public static IRangeGenerator<long> MakeRangeCCGenerator(this IRandom random, long lowerInclusive, long upperInclusive)
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
		public static IRangeGenerator<long> MakeRangeCCGenerator(this IRandom random, long upperInclusive)
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
		public static IRangeGenerator<ulong> MakeRangeCCGenerator(this IRandom random, ulong lowerInclusive, ulong upperInclusive)
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
		public static IRangeGenerator<ulong> MakeRangeCCGenerator(this IRandom random, ulong upperInclusive)
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
		public static IRangeGenerator<float> MakeRangeCCGenerator(this IRandom random, float lowerInclusive, float upperInclusive)
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
		public static IRangeGenerator<float> MakeRangeCCGenerator(this IRandom random, float upperInclusive)
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
		public static IRangeGenerator<double> MakeRangeCCGenerator(this IRandom random, double lowerInclusive, double upperInclusive)
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
		public static IRangeGenerator<double> MakeRangeCCGenerator(this IRandom random, double upperInclusive)
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
		public static IRangeGenerator<float> MakePreciseRangeCCGenerator(this IRandom random, float lowerInclusive, float upperInclusive)
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
		public static IRangeGenerator<float> MakePreciseRangeCCGenerator(this IRandom random, float upperInclusive)
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
		public static IRangeGenerator<double> MakePreciseRangeCCGenerator(this IRandom random, double lowerInclusive, double upperInclusive)
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
		public static IRangeGenerator<double> MakePreciseRangeCCGenerator(this IRandom random, double upperInclusive)
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
		public static IRangeGenerator<float> MakeFloatOOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.FloatCO(IRandom)"/>
		public static IRangeGenerator<float> MakeFloatCOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.FloatOC(IRandom)"/>
		public static IRangeGenerator<float> MakeFloatOCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.FloatCC(IRandom)"/>
		public static IRangeGenerator<float> MakeFloatCCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateCC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatOO(IRandom)"/>
		public static IRangeGenerator<float> MakeSignedFloatOOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatCO(IRandom)"/>
		public static IRangeGenerator<float> MakeSignedFloatCOGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatOC(IRandom)"/>
		public static IRangeGenerator<float> MakeSignedFloatOCGenerator(this IRandom random)
		{
			return FloatRangeGenerator.CreateSignedOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedFloatCC(IRandom)"/>
		public static IRangeGenerator<float> MakeSignedFloatCCGenerator(this IRandom random)
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
		public static IRangeGenerator<float> MakeFloatC1O2Generator(this IRandom random)
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
		public static IRangeGenerator<float> MakeFloatC2O4Generator(this IRandom random)
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
		public static IRangeGenerator<float> MakePreciseFloatOOGenerator(this IRandom random)
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
		public static IRangeGenerator<float> MakePreciseFloatCOGenerator(this IRandom random)
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
		public static IRangeGenerator<float> MakePreciseFloatOCGenerator(this IRandom random)
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
		public static IRangeGenerator<float> MakePreciseFloatCCGenerator(this IRandom random)
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
		public static IRangeGenerator<float> MakePreciseSignedFloatOOGenerator(this IRandom random)
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
		public static IRangeGenerator<float> MakePreciseSignedFloatCOGenerator(this IRandom random)
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
		public static IRangeGenerator<float> MakePreciseSignedFloatOCGenerator(this IRandom random)
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
		public static IRangeGenerator<float> MakePreciseSignedFloatCCGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakeDoubleOOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1).</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleCO(IRandom)"/>
		public static IRangeGenerator<double> MakeDoubleCOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleOC(IRandom)"/>
		public static IRangeGenerator<double> MakeDoubleOCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1].</returns>
		/// <seealso cref="RandomFloatingPoint.DoubleCC(IRandom)"/>
		public static IRangeGenerator<double> MakeDoubleCCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateCC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleOO(IRandom)"/>
		public static IRangeGenerator<double> MakeSignedDoubleOOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedOO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [-1, +1).</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleCO(IRandom)"/>
		public static IRangeGenerator<double> MakeSignedDoubleCOGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedCO(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleOC(IRandom)"/>
		public static IRangeGenerator<double> MakeSignedDoubleOCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedOC(random);
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [-1, +1].</returns>
		/// <seealso cref="RandomFloatingPoint.SignedDoubleCC(IRandom)"/>
		public static IRangeGenerator<double> MakeSignedDoubleCCGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakeDoubleC1O2Generator(this IRandom random)
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
		public static IRangeGenerator<double> MakeDoubleC2O4Generator(this IRandom random)
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
		public static IRangeGenerator<double> MakePreciseDoubleOOGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakePreciseDoubleCOGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakePreciseDoubleOCGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakePreciseDoubleCCGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakePreciseSignedDoubleOOGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakePreciseSignedDoubleCOGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakePreciseSignedDoubleOCGenerator(this IRandom random)
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
		public static IRangeGenerator<double> MakePreciseSignedDoubleCCGenerator(this IRandom random)
		{
			return DoubleRangeGenerator.CreateSignedCC(random);
		}

		#endregion
	}
}
