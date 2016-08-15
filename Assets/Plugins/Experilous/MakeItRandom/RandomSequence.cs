/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random numbers within custom ranges.
	/// </summary>
	public static class RandomSequence
	{
		#region Private Helper Tables

		private static readonly byte[] _bitCountTable32 = new byte[]
		{
			 1, 10,  2, 11, 14, 22,  3, 30, 12, 15, 17, 19, 23, 26,  4, 31,
			 9, 13, 21, 29, 16, 18, 25,  8, 20, 28, 24,  7, 27,  6,  5, 32,
		};

		private static readonly byte[] _bitCountTable64 = new byte[]
		{
			 1, 59,  2, 60, 48, 54,  3, 61, 40, 49, 28, 55, 34, 43,  4, 62,
			52, 38, 41, 50, 19, 29, 21, 56, 31, 35, 12, 44, 15, 23,  5, 63,
			58, 47, 53, 39, 27, 33, 42, 51, 37, 18, 20, 30, 11, 14, 22, 57,
			46, 26, 32, 36, 17, 10, 13, 45, 25, 16,  9, 24,  8,  7,  6, 64,
		};

		private const uint _deBruijnMultiplier32 = 0x07C4ACDDU;
		private const int _deBruijnShift32 = 27;

		private const ulong _deBruijnMultiplier64 = 0x03F6EAF2CD271461UL;
		private const int _deBruijnShift64 = 58;

		#endregion

		#region Private Helper Functions

		private static sbyte ULongToSByte(ulong n)
		{
			return (sbyte)n;
		}

		private static byte ULongToByte(ulong n)
		{
			return (byte)n;
		}

		private static short ULongToShort(ulong n)
		{
			return (short)n;
		}

		private static ushort ULongToUShort(ulong n)
		{
			return (ushort)n;
		}

		private static int ULongToInt(ulong n)
		{
			return (int)n;
		}

		private static uint ULongToUInt(ulong n)
		{
			return (uint)n;
		}

		private static long ULongToLong(ulong n)
		{
			return (long)n;
		}

		private static ulong ULongToULong(ulong n)
		{
			return n;
		}

		private static void SequencePow2Range<T>(this IRandom random, IList<T> buffer, int start, int length, int elementBitCount, ulong elementBitMask, System.Func<ulong, T> converter)
		{
			if (length <= 0) return;

			IEnumerator<T> enumerator = random.SequencePow2Range(length, elementBitCount, elementBitMask, converter);
			int i = start;
			while (enumerator.MoveNext())
			{
				buffer[i++] = enumerator.Current;
			}
		}

		private static void SequenceNonPow2Range<T>(this IRandom random, IList<T> buffer, int start, int length, ulong rangeSize, int elementBitCount, ulong elementBitMask, System.Func<ulong, T> converter)
		{
			if (length <= 0) return;

			IEnumerator<T> enumerator = random.SequenceNonPow2Range(length, rangeSize, elementBitCount, elementBitMask, converter);
			int i = start;
			while (enumerator.MoveNext())
			{
				buffer[i++] = enumerator.Current;
			}
		}

		private static IEnumerator<T> SequencePow2Range<T>(this IRandom random, int length, int elementBitCount, ulong elementBitMask, System.Func<ulong, T> converter)
		{
			if (length <= 0) yield break;

			int elementCountIn32Bits = 32 / elementBitCount;
			if (length <= elementCountIn32Bits)
			{
				ulong bits = random.Next32();
				for (int i = 0; i < length; ++i)
				{
					yield return converter(bits & elementBitMask);
					bits = bits >> elementBitCount;
				}
			}
			else
			{
				int elementCountIn64Bits = 64 / elementBitCount;
				if (length <= elementCountIn64Bits)
				{
					ulong bits = random.Next64();
					for (int i = 0; i < length; ++i)
					{
						yield return converter(bits & elementBitMask);
						bits = bits >> elementBitCount;
					}
				}
				else
				{
					IEnumerator<T> enumerator = random.InfiniteSequencePow2Range(elementBitCount, elementBitMask, elementCountIn64Bits, converter);
					for (int i = 0; i < length; ++i)
					{
						enumerator.MoveNext();
						yield return enumerator.Current;
					}
				}
			}
		}

		private static IEnumerator<T> SequenceNonPow2Range<T>(this IRandom random, int length, ulong rangeSize, int elementBitCount, ulong elementBitMask, System.Func<ulong, T> converter)
		{
			if (length <= 0) yield break;

			IEnumerator<T> enumerator = random.InfiniteSequenceNonPow2Range(rangeSize, elementBitCount, elementBitMask, converter);
			for (int i = 0; i < length; ++i)
			{
				enumerator.MoveNext();
				yield return enumerator.Current;
			}
		}

		private static IEnumerator<T> InfiniteSequencePow2Range<T>(this IRandom random, int elementBitCount, ulong elementBitMask, int elementCountIn64Bits, System.Func<ulong, T> converter)
		{
			int surplusBitCountIn64Bits = 64 - elementBitCount * elementCountIn64Bits; //never larger than 31
			ulong surplusBitMask = (1UL << surplusBitCountIn64Bits) - 1UL;
			ulong surplusBits = 0;
			int surplusBitCount = 0;

			while (true)
			{
				ulong bits = random.Next64();
				for (int j = 0; j < elementCountIn64Bits; ++j)
				{
					yield return converter(bits & elementBitMask);
					bits = bits >> elementBitCount;
				}

				surplusBits = (surplusBits << surplusBitCountIn64Bits) | ((uint)bits & surplusBitMask);
				surplusBitCount += surplusBitCountIn64Bits;

				while (surplusBitCount >= elementBitCount)
				{
					yield return converter(surplusBits & elementBitMask);
					surplusBits = surplusBits >> elementBitCount;
					surplusBitCount -= elementBitCount;
				}
			}
		}

		private static IEnumerator<T> InfiniteSequenceNonPow2Range<T>(this IRandom random, ulong rangeSize, int elementBitCount, ulong elementBitMask, System.Func<ulong, T> converter)
		{
			int elementCountIn64Bits = 64 / elementBitCount;

			int surplusBitCountIn64Bits = 64 - elementBitCount * elementCountIn64Bits; //never larger than 31
			ulong surplusBitMask = (1UL << surplusBitCountIn64Bits) - 1UL;
			ulong surplusBits = 0;
			int surplusBitCount = 0;

			while (true)
			{
				ulong bits = random.Next64();
				for (int j = 0; j < elementCountIn64Bits; ++j)
				{
					ulong n = bits & elementBitMask;
					bits = bits >> elementBitCount;
					if (n < rangeSize)
					{
						yield return converter(n);
					}
				}

				surplusBits = (surplusBits << surplusBitCountIn64Bits) | ((uint)bits & surplusBitMask);
				surplusBitCount += surplusBitCountIn64Bits;

				while (surplusBitCount >= elementBitCount)
				{
					ulong n = surplusBits & elementBitMask;
					surplusBits = surplusBits >> elementBitCount;
					surplusBitCount -= elementBitCount;
					if (n < rangeSize)
					{
						yield return converter(n);
					}
				}
			}
		}

		#endregion

		#region SByte Sequence

		public static void Sequence(this IRandom random, IList<sbyte> buffer, int start, int length)
		{
			random.SequencePow2Range(buffer, start, length, 8, 0xFFUL, ULongToSByte);
		}

		public static void Sequence(this IRandom random, IList<sbyte> buffer, int start, int length, sbyte rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				random.SequencePow2Range(buffer, start, length, bitCount, mask, ULongToSByte);
			}
			else
			{
				random.SequenceNonPow2Range(buffer, start, length, (ulong)rangeSize, bitCount, mask, ULongToSByte);
			}
		}

		public static IEnumerator<sbyte> SByteSequence(this IRandom random, int length)
		{
			return random.SequencePow2Range<sbyte>(length, 8, 0xFFUL, ULongToSByte);
		}

		public static IEnumerator<sbyte> SByteSequence(this IRandom random, int length, sbyte rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.SequencePow2Range<sbyte>(length, bitCount, mask, ULongToSByte);
			}
			else
			{
				return random.SequenceNonPow2Range<sbyte>(length, (ulong)rangeSize, bitCount, mask, ULongToSByte);
			}
		}

		public static IEnumerator<sbyte> InfiniteSByteSequence(this IRandom random)
		{
			return random.InfiniteSequencePow2Range<sbyte>(8, 0xFFUL, 8, ULongToSByte);
		}

		public static IEnumerator<sbyte> InfiniteSByteSequence(this IRandom random, sbyte rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.InfiniteSequencePow2Range<sbyte>(bitCount, mask, 64 / bitCount, ULongToSByte);
			}
			else
			{
				return random.InfiniteSequenceNonPow2Range<sbyte>((ulong)rangeSize, bitCount, mask, ULongToSByte);
			}
		}

		#endregion

		#region Byte Sequence

		public static void Sequence(this IRandom random, IList<byte> buffer, int start, int length)
		{
			random.SequencePow2Range(buffer, start, length, 8, 0xFFUL, ULongToByte);
		}

		public static void Sequence(this IRandom random, IList<byte> buffer, int start, int length, byte rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				random.SequencePow2Range(buffer, start, length, bitCount, mask, ULongToByte);
			}
			else
			{
				random.SequenceNonPow2Range(buffer, start, length, rangeSize, bitCount, mask, ULongToByte);
			}
		}

		public static IEnumerator<byte> ByteSequence(this IRandom random, int length)
		{
			return random.SequencePow2Range<byte>(length, 8, 0xFFUL, ULongToByte);
		}

		public static IEnumerator<byte> ByteSequence(this IRandom random, int length, byte rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.SequencePow2Range<byte>(length, bitCount, mask, ULongToByte);
			}
			else
			{
				return random.SequenceNonPow2Range<byte>(length, rangeSize, bitCount, mask, ULongToByte);
			}
		}

		public static IEnumerator<byte> InfiniteByteSequence(this IRandom random)
		{
			return random.InfiniteSequencePow2Range<byte>(8, 0xFFUL, 8, ULongToByte);
		}

		public static IEnumerator<byte> InfiniteByteSequence(this IRandom random, byte rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.InfiniteSequencePow2Range<byte>(bitCount, mask, 64 / bitCount, ULongToByte);
			}
			else
			{
				return random.InfiniteSequenceNonPow2Range<byte>(rangeSize, bitCount, mask, ULongToByte);
			}
		}

		#endregion

		#region Short Sequence

		public static void Sequence(this IRandom random, IList<short> buffer, int start, int length)
		{
			random.SequencePow2Range(buffer, start, length, 16, 0xFFFFUL, ULongToShort);
		}

		public static void Sequence(this IRandom random, IList<short> buffer, int start, int length, short rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				random.SequencePow2Range(buffer, start, length, bitCount, mask, ULongToShort);
			}
			else
			{
				random.SequenceNonPow2Range(buffer, start, length, (ulong)rangeSize, bitCount, mask, ULongToShort);
			}
		}

		public static IEnumerator<short> ShortSequence(this IRandom random, int length)
		{
			return random.SequencePow2Range<short>(length, 16, 0xFFFFUL, ULongToShort);
		}

		public static IEnumerator<short> ShortSequence(this IRandom random, int length, short rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.SequencePow2Range<short>(length, bitCount, mask, ULongToShort);
			}
			else
			{
				return random.SequenceNonPow2Range<short>(length, (ulong)rangeSize, bitCount, mask, ULongToShort);
			}
		}

		public static IEnumerator<short> InfiniteShortSequence(this IRandom random)
		{
			return random.InfiniteSequencePow2Range<short>(16, 0xFFFFUL, 4, ULongToShort);
		}

		public static IEnumerator<short> InfiniteShortSequence(this IRandom random, short rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.InfiniteSequencePow2Range<short>(bitCount, mask, 64 / bitCount, ULongToShort);
			}
			else
			{
				return random.InfiniteSequenceNonPow2Range<short>((ulong)rangeSize, bitCount, mask, ULongToShort);
			}
		}

		#endregion

		#region UShort Sequence

		public static void Sequence(this IRandom random, IList<ushort> buffer, int start, int length)
		{
			random.SequencePow2Range(buffer, start, length, 16, 0xFFFFUL, ULongToUShort);
		}

		public static void Sequence(this IRandom random, IList<ushort> buffer, int start, int length, ushort rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				random.SequencePow2Range(buffer, start, length, bitCount, mask, ULongToUShort);
			}
			else
			{
				random.SequenceNonPow2Range(buffer, start, length, rangeSize, bitCount, mask, ULongToUShort);
			}
		}

		public static IEnumerator<ushort> UShortSequence(this IRandom random, int length)
		{
			return random.SequencePow2Range<ushort>(length, 16, 0xFFFFUL, ULongToUShort);
		}

		public static IEnumerator<ushort> UShortSequence(this IRandom random, int length, ushort rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.SequencePow2Range<ushort>(length, bitCount, mask, ULongToUShort);
			}
			else
			{
				return random.SequenceNonPow2Range<ushort>(length, rangeSize, bitCount, mask, ULongToUShort);
			}
		}

		public static IEnumerator<ushort> InfiniteUShortSequence(this IRandom random)
		{
			return random.InfiniteSequencePow2Range<ushort>(16, 0xFFFFUL, 4, ULongToUShort);
		}

		public static IEnumerator<ushort> InfiniteUShortSequence(this IRandom random, ushort rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.InfiniteSequencePow2Range<ushort>(bitCount, mask, 64 / bitCount, ULongToUShort);
			}
			else
			{
				return random.InfiniteSequenceNonPow2Range<ushort>(rangeSize, bitCount, mask, ULongToUShort);
			}
		}

		#endregion

		#region Int Sequence

		public static void Sequence(this IRandom random, IList<int> buffer, int start, int length)
		{
			random.SequencePow2Range(buffer, start, length, 32, 0xFFFFFFFFUL, ULongToInt);
		}

		public static void Sequence(this IRandom random, IList<int> buffer, int start, int length, int rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == (int)mask)
			{
				random.SequencePow2Range(buffer, start, length, bitCount, mask, ULongToInt);
			}
			else
			{
				random.SequenceNonPow2Range(buffer, start, length, (ulong)rangeSize, bitCount, mask, ULongToInt);
			}
		}

		public static IEnumerator<int> IntSequence(this IRandom random, int length)
		{
			return random.SequencePow2Range<int>(length, 32, 0xFFFFFFFFUL, ULongToInt);
		}

		public static IEnumerator<int> IntSequence(this IRandom random, int length, int rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == (int)mask)
			{
				return random.SequencePow2Range<int>(length, bitCount, mask, ULongToInt);
			}
			else
			{
				return random.SequenceNonPow2Range<int>(length, (ulong)rangeSize, bitCount, mask, ULongToInt);
			}
		}

		public static IEnumerator<int> InfiniteIntSequence(this IRandom random)
		{
			return random.InfiniteSequencePow2Range<int>(32, 0xFFFFFFFFUL, 2, ULongToInt);
		}

		public static IEnumerator<int> InfiniteIntSequence(this IRandom random, int rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == (int)mask)
			{
				return random.InfiniteSequencePow2Range<int>(bitCount, mask, 64 / bitCount, ULongToInt);
			}
			else
			{
				return random.InfiniteSequenceNonPow2Range<int>((ulong)rangeSize, bitCount, mask, ULongToInt);
			}
		}

		#endregion

		#region UInt Sequence

		public static void Sequence(this IRandom random, IList<uint> buffer, int start, int length)
		{
			random.SequencePow2Range(buffer, start, length, 32, 0xFFFFFFFFUL, ULongToUInt);
		}

		public static void Sequence(this IRandom random, IList<uint> buffer, int start, int length, uint rangeSize)
		{
			uint mask = rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				random.SequencePow2Range(buffer, start, length, bitCount, mask, ULongToUInt);
			}
			else
			{
				random.SequenceNonPow2Range(buffer, start, length, rangeSize, bitCount, mask, ULongToUInt);
			}
		}

		public static IEnumerator<uint> UIntSequence(this IRandom random, int length)
		{
			return random.SequencePow2Range<uint>(length, 32, 0xFFFFFFFFUL, ULongToUInt);
		}

		public static IEnumerator<uint> UIntSequence(this IRandom random, int length, uint rangeSize)
		{
			uint mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.SequencePow2Range<uint>(length, bitCount, mask, ULongToUInt);
			}
			else
			{
				return random.SequenceNonPow2Range<uint>(length, rangeSize, bitCount, mask, ULongToUInt);
			}
		}

		public static IEnumerator<uint> InfiniteUIntSequence(this IRandom random)
		{
			return random.InfiniteSequencePow2Range<uint>(32, 0xFFFFFFFFUL, 2, ULongToUInt);
		}

		public static IEnumerator<uint> InfiniteUIntSequence(this IRandom random, uint rangeSize)
		{
			uint mask = rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			int bitCount = _bitCountTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];

			if (rangeSize - 1 == mask)
			{
				return random.InfiniteSequencePow2Range<uint>(bitCount, mask, 64 / bitCount, ULongToUInt);
			}
			else
			{
				return random.InfiniteSequenceNonPow2Range<uint>(rangeSize, bitCount, mask, ULongToUInt);
			}
		}

		#endregion

		#region Long Sequence

		public static void Sequence(this IRandom random, IList<long> buffer, int start, int length)
		{
			random.SequencePow2Range(buffer, start, length, 64, 0xFFFFFFFFFFFFFFFFUL, ULongToLong);
		}

		public static void Sequence(this IRandom random, IList<long> buffer, int start, int length, long rangeSize)
		{
			ulong mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			int bitCount = _bitCountTable64[mask * _deBruijnMultiplier64 >> _deBruijnShift64];

			if (rangeSize - 1 == (long)mask)
			{
				random.SequencePow2Range(buffer, start, length, bitCount, mask, ULongToLong);
			}
			else
			{
				random.SequenceNonPow2Range(buffer, start, length, (ulong)rangeSize, bitCount, mask, ULongToLong);
			}
		}

		public static IEnumerator<long> LongSequence(this IRandom random, int length)
		{
			return random.SequencePow2Range<long>(length, 64, 0xFFFFFFFFFFFFFFFFUL, ULongToLong);
		}

		public static IEnumerator<long> LongSequence(this IRandom random, int length, long rangeSize)
		{
			ulong mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			int bitCount = _bitCountTable64[mask * _deBruijnMultiplier64 >> _deBruijnShift64];

			if (rangeSize - 1 == (long)mask)
			{
				return random.SequencePow2Range<long>(length, bitCount, mask, ULongToLong);
			}
			else
			{
				return random.SequenceNonPow2Range<long>(length, (ulong)rangeSize, bitCount, mask, ULongToLong);
			}
		}

		public static IEnumerator<long> InfiniteLongSequence(this IRandom random)
		{
			return random.InfiniteSequencePow2Range<long>(64, 0xFFFFFFFFFFFFFFFFUL, 1, ULongToLong);
		}

		public static IEnumerator<long> InfiniteLongSequence(this IRandom random, long rangeSize)
		{
			ulong mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			int bitCount = _bitCountTable64[mask * _deBruijnMultiplier64 >> _deBruijnShift64];

			if (rangeSize - 1 == (long)mask)
			{
				return random.InfiniteSequencePow2Range<long>(bitCount, mask, 64 / bitCount, ULongToLong);
			}
			else
			{
				return random.InfiniteSequenceNonPow2Range<long>((ulong)rangeSize, bitCount, mask, ULongToLong);
			}
		}

		#endregion

		#region ULong Sequence

		public static void Sequence(this IRandom random, IList<ulong> buffer, int start, int length)
		{
			random.SequencePow2Range(buffer, start, length, 64, 0xFFFFFFFFFFFFFFFFUL, ULongToULong);
		}

		public static void Sequence(this IRandom random, IList<ulong> buffer, int start, int length, ulong rangeSize)
		{
			ulong mask = rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			int bitCount = _bitCountTable64[mask * _deBruijnMultiplier64 >> _deBruijnShift64];

			if (rangeSize - 1 == mask)
			{
				random.SequencePow2Range(buffer, start, length, bitCount, mask, ULongToULong);
			}
			else
			{
				random.SequenceNonPow2Range(buffer, start, length, rangeSize, bitCount, mask, ULongToULong);
			}
		}

		public static IEnumerator<ulong> ULongSequence(this IRandom random, int length)
		{
			return random.SequencePow2Range<ulong>(length, 64, 0xFFFFFFFFFFFFFFFFUL, ULongToULong);
		}

		public static IEnumerator<ulong> ULongSequence(this IRandom random, int length, ulong rangeSize)
		{
			ulong mask = (uint)rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			int bitCount = _bitCountTable64[mask * _deBruijnMultiplier64 >> _deBruijnShift64];

			if (rangeSize - 1 == mask)
			{
				return random.SequencePow2Range<ulong>(length, bitCount, mask, ULongToULong);
			}
			else
			{
				return random.SequenceNonPow2Range<ulong>(length, rangeSize, bitCount, mask, ULongToULong);
			}
		}

		public static IEnumerator<ulong> InfiniteULongSequence(this IRandom random)
		{
			return random.InfiniteSequencePow2Range<ulong>(64, 0xFFFFFFFFFFFFFFFFUL, 1, ULongToULong);
		}

		public static IEnumerator<ulong> InfiniteULongSequence(this IRandom random, ulong rangeSize)
		{
			ulong mask = rangeSize - 1;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			int bitCount = _bitCountTable64[mask * _deBruijnMultiplier64 >> _deBruijnShift64];

			if (rangeSize - 1 == mask)
			{
				return random.InfiniteSequencePow2Range<ulong>(bitCount, mask, 64 / bitCount, ULongToULong);
			}
			else
			{
				return random.InfiniteSequenceNonPow2Range<ulong>(rangeSize, bitCount, mask, ULongToULong);
			}
		}

		#endregion
	}
}
