/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random bits.
	/// </summary>
	public static class RandomBits
	{
		#region Private Generators

		private class SingleBitGenerator32 : Detail.BufferedBitGenerator, IRangeGenerator<uint>
		{
			public SingleBitGenerator32(IRandom random) : base(random) { }

			public uint Next()
			{
				return (uint)Next32();
			}
		}

		private class SingleBitGenerator64 : Detail.BufferedBitGenerator, IRangeGenerator<ulong>
		{
			public SingleBitGenerator64(IRandom random) : base(random) { }

			public ulong Next()
			{
				return Next64();
			}
		}

		private class MultiBitPow2Generator32 : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<uint>
		{
			public MultiBitPow2Generator32(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }

			public uint Next()
			{
				return (uint)Next32();
			}
		}

		private class MultiBitPowPow2Generator32 : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<uint>
		{
			public MultiBitPowPow2Generator32(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }

			public uint Next()
			{
				return (uint)Next32();
			}
		}

		private class MultiBitPow2Generator64 : Detail.BufferedPow2RangeGeneratorBase, IRangeGenerator<ulong>
		{
			public MultiBitPow2Generator64(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }

			public ulong Next()
			{
				return Next64();
			}
		}

		private class MultiBitPowPow2Generator64 : Detail.BufferedPowPow2RangeGeneratorBase, IRangeGenerator<ulong>
		{
			public MultiBitPowPow2Generator64(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }

			public ulong Next()
			{
				return Next64();
			}
		}

		private class MultiBitGeneratorUInt64 : IRangeGenerator<ulong>
		{
			private IRandom _random;

			public MultiBitGeneratorUInt64(IRandom random) { _random = random; }

			public ulong Next()
			{
				return _random.Next64();
			}
		}

		#endregion

		/// <summary>
		/// Returns a random unsigned integer with its lowest bit having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>An unsigned integer with its lowest bit set to either 1 or 0 with equal probability and all high bits set to 0.</returns>
		public static uint Bit(this IRandom random)
		{
			return random.Next32() >> 31;
		}

		/// <summary>
		/// Returns a bit generator which will produce a single bit per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A bit generator which will produce a single bit per call to generator.Next().</returns>
		/// <seealso cref="Bit(IRandom)"/>
		public static IRangeGenerator<uint> MakeBitGenerator(this IRandom random)
		{
			return new SingleBitGenerator32(random);
		}

		/// <summary>
		/// Returns a random 32-bit unsigned integer with every bit having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A 32-bit unsigned integer with every bit set to either 1 or 0 with equal probability.</returns>
		public static uint Bits32(this IRandom random)
		{
			return random.Next32();
		}

		/// <summary>
		/// Returns a bit generator which will produce 32 bits per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A bit generator which will produce 32 random bits per call to generator.Next().</returns>
		/// <seealso cref="Bits32(IRandom)"/>
		public static IRangeGenerator<uint> MakeBits32Generator(this IRandom random)
		{
			return new MultiBitPowPow2Generator32(random, 32, 0xFFFFFFFFUL);
		}

		/// <summary>
		/// Returns a random 32-bit unsigned integer with its lowest <paramref name="bitCount"/> bits having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="bitCount">The number of bits to generate.  Must be in the range [1, 32].</param>
		/// <returns>A 32-bit unsigned integer with its lowest <paramref name="bitCount"/> bits set to either 1 or 0 with equal probability and all higher bits set to 0.</returns>
		public static uint Bits32(this IRandom random, int bitCount)
		{
			return random.Next32() >> (32 - bitCount);
		}

		/// <summary>
		/// Returns a bit generator which will produce <paramref name="bitCount"/> bits per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="bitCount">The number of bits to generate.  Must be in the range [1, 32].</param>
		/// <returns>A bit generator which will produce <paramref name="bitCount"/> random bits per call to generator.Next().</returns>
		/// <seealso cref="Bits32(IRandom, int)"/>
		public static IRangeGenerator<uint> MakeBits32Generator(this IRandom random, int bitCount)
		{
			if (bitCount != 1)
			{
				if (Detail.DeBruijnLookup.IsPowerOfTwo((byte)bitCount))
				{
					return new MultiBitPowPow2Generator32(random, bitCount, 0xFFFFFFFFUL >> (32 - bitCount));
				}
				else
				{
					return new MultiBitPow2Generator32(random, bitCount, 0xFFFFFFFFUL >> (32 - bitCount));
				}
			}
			else
			{
				return new SingleBitGenerator32(random);
			}
		}

		/// <summary>
		/// Returns a random 64-bit unsigned integer with every bit having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A 64-bit unsigned integer with every bit set to either 1 or 0 with equal probability.</returns>
		public static ulong Bits64(this IRandom random)
		{
			return random.Next64();
		}

		/// <summary>
		/// Returns a bit generator which will produce 64 bits per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A bit generator which will produce 64 random bits per call to generator.Next().</returns>
		/// <seealso cref="Bits64(IRandom)"/>
		public static IRangeGenerator<ulong> MakeBits64Generator(this IRandom random)
		{
			return new MultiBitGeneratorUInt64(random);
		}

		/// <summary>
		/// Returns a random 64-bit unsigned integer with its lowest <paramref name="bitCount"/> bits having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="bitCount">The number of bits to generate.  Must be in the range [1, 64].</param>
		/// <returns>A 64-bit unsigned integer with its lowest <paramref name="bitCount"/> bits set to either 1 or 0 with equal probability and all higher bits set to 0.</returns>
		public static ulong Bits64(this IRandom random, int bitCount)
		{
			return random.Next64() >> (64 - bitCount);
		}

		/// <summary>
		/// Returns a bit generator which will produce <paramref name="bitCount"/> bits per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="bitCount">The number of bits to generate.  Must be in the range [1, 64].</param>
		/// <returns>A bit generator which will produce <paramref name="bitCount"/> random bits per call to generator.Next().</returns>
		/// <seealso cref="Bits64(IRandom, int)"/>
		public static IRangeGenerator<ulong> MakeBits64Generator(this IRandom random, int bitCount)
		{
			if (bitCount != 1)
			{
				if (bitCount < 64)
				{
					if (Detail.DeBruijnLookup.IsPowerOfTwo((byte)bitCount))
					{
						return new MultiBitPowPow2Generator64(random, bitCount, 0xFFFFFFFFFFFFFFFFUL >> (64 - bitCount));
					}
					else
					{
						return new MultiBitPow2Generator64(random, bitCount, 0xFFFFFFFFFFFFFFFFUL >> (64 - bitCount));
					}
				}
				else
				{
					return new MultiBitGeneratorUInt64(random);
				}
			}
			else
			{
				return new SingleBitGenerator64(random);
			}
		}
	}
}
