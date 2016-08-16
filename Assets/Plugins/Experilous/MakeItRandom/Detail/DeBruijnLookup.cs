/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom.Detail
{
	/// <summary>
	/// A static class of lookup tables and methods for quickly looking up common attributes associated with integer values.
	/// </summary>
	public static class DeBruijnLookup
	{
		public const uint multiplier32 = 0x07C4ACDDU;
		public const int shift32 = 27;

		public static readonly byte[] bitCountTable32 = new byte[]
		{
			 1, 10,  2, 11, 14, 22,  3, 30, 12, 15, 17, 19, 23, 26,  4, 31,
			 9, 13, 21, 29, 16, 18, 25,  8, 20, 28, 24,  7, 27,  6,  5, 32,
		};

		public const ulong multiplier64 = 0x03F6EAF2CD271461UL;
		public const int shift64 = 58;

		public static readonly byte[] bitCountTable64 = new byte[]
		{
			 1, 59,  2, 60, 48, 54,  3, 61, 40, 49, 28, 55, 34, 43,  4, 62,
			52, 38, 41, 50, 19, 29, 21, 56, 31, 35, 12, 44, 15, 23,  5, 63,
			58, 47, 53, 39, 27, 33, 42, 51, 37, 18, 20, 30, 11, 14, 22, 57,
			46, 26, 32, 36, 17, 10, 13, 45, 25, 16,  9, 24,  8,  7,  6, 64,
		};

		public static uint GetBitMaskForRangeMax(sbyte rangeMax)
		{
			return GetBitMaskForRangeMax((byte)rangeMax);
		}

		public static int GetBitCountForRangeMax(sbyte rangeMax)
		{
			return GetBitCountForRangeMax((byte)rangeMax);
		}

		public static void GetBitCountAndMaskForRangeMax(sbyte rangeMax, out int bitCount, out uint bitMask)
		{
			GetBitCountAndMaskForRangeMax((byte)rangeMax, out bitCount, out bitMask);
		}

		public static uint GetBitMaskForRangeSize(sbyte rangeSize)
		{
			return GetBitMaskForRangeMax((uint)rangeSize - 1U);
		}

		public static int GetBitCountForRangeSize(sbyte rangeSize)
		{
			return GetBitCountForRangeMax((uint)rangeSize - 1U);
		}

		public static void GetBitCountAndMaskForRangeSize(sbyte rangeSize, out int bitCount, out uint bitMask)
		{
			GetBitCountAndMaskForRangeMax((uint)rangeSize - 1U, out bitCount, out bitMask);
		}

		public static uint GetBitMaskForRangeMax(byte rangeMax)
		{
			uint bitMask = rangeMax;
			bitMask |= bitMask >> 1;
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			return bitMask;
		}

		public static int GetBitCountForRangeMax(byte rangeMax)
		{
			uint bitMask = rangeMax;
			bitMask |= bitMask >> 1;
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			return bitCountTable32[bitMask * multiplier32 >> shift32];
		}

		public static void GetBitCountAndMaskForRangeMax(byte rangeMax, out int bitCount, out uint bitMask)
		{
			bitMask = rangeMax;
			bitMask |= bitMask >> 1;
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitCount = bitCountTable32[bitMask * multiplier32 >> shift32];
		}

		public static uint GetBitMaskForRangeSize(byte rangeSize)
		{
			return GetBitMaskForRangeMax(rangeSize - 1U);
		}

		public static int GetBitCountForRangeSize(byte rangeSize)
		{
			return GetBitCountForRangeMax(rangeSize - 1U);
		}

		public static void GetBitCountAndMaskForRangeSize(byte rangeSize, out int bitCount, out uint bitMask)
		{
			GetBitCountAndMaskForRangeMax(rangeSize - 1U, out bitCount, out bitMask);
		}

		public static uint GetBitMaskForRangeMax(int rangeMax)
		{
			return GetBitMaskForRangeMax((uint)rangeMax);
		}

		public static int GetBitCountForRangeMax(int rangeMax)
		{
			return GetBitCountForRangeMax((uint)rangeMax);
		}

		public static void GetBitCountAndMaskForRangeMax(int rangeMax, out int bitCount, out uint bitMask)
		{
			GetBitCountAndMaskForRangeMax((uint)rangeMax, out bitCount, out bitMask);
		}

		public static uint GetBitMaskForRangeSize(int rangeSize)
		{
			return GetBitMaskForRangeMax((uint)rangeSize - 1U);
		}

		public static int GetBitCountForRangeSize(int rangeSize)
		{
			return GetBitCountForRangeMax((uint)rangeSize - 1U);
		}

		public static void GetBitCountAndMaskForRangeSize(int rangeSize, out int bitCount, out uint bitMask)
		{
			GetBitCountAndMaskForRangeMax((uint)rangeSize - 1U, out bitCount, out bitMask);
		}

		public static uint GetBitMaskForRangeMax(uint rangeMax)
		{
			uint bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			bitMask |= bitMask >> 16;
			return bitMask;
		}

		public static int GetBitCountForRangeMax(uint rangeMax)
		{
			uint bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			bitMask |= bitMask >> 16;
			return bitCountTable32[bitMask * multiplier32 >> shift32];
		}

		public static void GetBitCountAndMaskForRangeMax(uint rangeMax, out int bitCount, out uint bitMask)
		{
			bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			bitMask |= bitMask >> 16;
			bitCount = bitCountTable32[bitMask * multiplier32 >> shift32];
		}

		public static uint GetBitMaskForRangeSize(uint rangeSize)
		{
			return GetBitMaskForRangeMax(rangeSize - 1U);
		}

		public static int GetBitCountForRangeSize(uint rangeSize)
		{
			return GetBitCountForRangeMax(rangeSize - 1U);
		}

		public static void GetBitCountAndMaskForRangeSize(uint rangeSize, out int bitCount, out uint bitMask)
		{
			GetBitCountAndMaskForRangeMax(rangeSize - 1U, out bitCount, out bitMask);
		}

		public static int GetBitCountForBitMask(uint bitMask)
		{
			return bitCountTable32[bitMask * multiplier32 >> shift32];
		}

		public static ulong GetBitMaskForRangeMax(long rangeMax)
		{
			return GetBitMaskForRangeMax((ulong)rangeMax);
		}

		public static int GetBitCountForRangeMax(long rangeMax)
		{
			return GetBitCountForRangeMax((ulong)rangeMax);
		}

		public static void GetBitCountAndMaskForRangeMax(long rangeMax, out int bitCount, out ulong bitMask)
		{
			GetBitCountAndMaskForRangeMax((ulong)rangeMax, out bitCount, out bitMask);
		}

		public static ulong GetBitMaskForRangeSize(long rangeSize)
		{
			return GetBitMaskForRangeMax((ulong)rangeSize - 1U);
		}

		public static int GetBitCountForRangeSize(long rangeSize)
		{
			return GetBitCountForRangeMax((ulong)rangeSize - 1U);
		}

		public static void GetBitCountAndMaskForRangeSize(long rangeSize, out int bitCount, out ulong bitMask)
		{
			GetBitCountAndMaskForRangeMax((ulong)rangeSize - 1U, out bitCount, out bitMask);
		}

		public static ulong GetBitMaskForRangeMax(ulong rangeMax)
		{
			ulong bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			bitMask |= bitMask >> 16;
			bitMask |= bitMask >> 32;
			return bitMask;
		}

		public static int GetBitCountForRangeMax(ulong rangeMax)
		{
			ulong bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			bitMask |= bitMask >> 16;
			bitMask |= bitMask >> 32;
			return bitCountTable64[bitMask * multiplier64 >> shift64];
		}

		public static void GetBitCountAndMaskForRangeMax(ulong rangeMax, out int bitCount, out ulong bitMask)
		{
			bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			bitMask |= bitMask >> 16;
			bitMask |= bitMask >> 32;
			bitCount = bitCountTable64[bitMask * multiplier64 >> shift64];
		}

		public static ulong GetBitMaskForRangeSize(ulong rangeSize)
		{
			return GetBitMaskForRangeMax(rangeSize - 1U);
		}

		public static int GetBitCountForRangeSize(ulong rangeSize)
		{
			return GetBitCountForRangeMax(rangeSize - 1U);
		}

		public static void GetBitCountAndMaskForRangeSize(ulong rangeSize, out int bitCount, out ulong bitMask)
		{
			GetBitCountAndMaskForRangeMax(rangeSize - 1U, out bitCount, out bitMask);
		}

		public static int GetBitCountForBitMask(ulong bitMask)
		{
			return bitCountTable64[bitMask * multiplier64 >> shift64];
		}

		public static bool IsPowerOfTwo(byte rangeSize)
		{
			uint rangeMax = rangeSize - 1U;
			uint bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			return rangeMax == bitMask;
		}

		public static bool IsPowerOfTwo(ushort rangeSize)
		{
			uint rangeMax = rangeSize - 1U;
			uint bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			return rangeMax == bitMask;
		}

		public static bool IsPowerOfTwo(uint rangeSize)
		{
			uint rangeMax = rangeSize - 1U;
			uint bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			bitMask |= bitMask >> 16;
			return rangeMax == bitMask;
		}

		public static bool IsPowerOfTwo(ulong rangeSize)
		{
			ulong rangeMax = rangeSize - 1UL;
			ulong bitMask = rangeMax | (rangeMax >> 1);
			bitMask |= bitMask >> 2;
			bitMask |= bitMask >> 4;
			bitMask |= bitMask >> 8;
			bitMask |= bitMask >> 16;
			bitMask |= bitMask >> 32;
			return rangeMax == bitMask;
		}
	}
}
