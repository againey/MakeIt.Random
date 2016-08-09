﻿/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public static class RandomRange
	{
		#region Open

		public static int OpenRange(this IRandomEngine random, int lowerExclusive, int upperExclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1;
		}

		public static int OpenRange(this IRandomEngine random, int upperExclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperExclusive - 1)) + 1;
		}

		public static uint OpenRange(this IRandomEngine random, uint lowerExclusive, uint upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - lowerExclusive - 1U) + lowerExclusive + 1U;
		}

		public static uint OpenRange(this IRandomEngine random, uint upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - 1U) + 1U;
		}

		public static long OpenRange(this IRandomEngine random, long lowerExclusive, long upperExclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperExclusive - lowerExclusive - 1L)) + lowerExclusive + 1L;
		}

		public static long OpenRange(this IRandomEngine random, long upperExclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperExclusive - 1L)) + 1L;
		}

		public static ulong OpenRange(this IRandomEngine random, ulong lowerExclusive, ulong upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - lowerExclusive - 1UL) + lowerExclusive + 1UL;
		}

		public static ulong OpenRange(this IRandomEngine random, ulong upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - 1UL) + 1UL;
		}

		public static float OpenRange(this IRandomEngine random, float lowerExclusive, float upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * random.OpenFloatUnit() + lowerExclusive;
		}

		public static float OpenRange(this IRandomEngine random, float upperExclusive)
		{
			return upperExclusive * random.OpenFloatUnit();
		}

		public static double OpenRange(this IRandomEngine random, double lowerExclusive, double upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * random.OpenDoubleUnit() + lowerExclusive;
		}

		public static double OpenRange(this IRandomEngine random, double upperExclusive)
		{
			return upperExclusive * random.OpenDoubleUnit();
		}

		#endregion

		#region HalfOpen

		public static int HalfOpenRange(this IRandomEngine random, int lowerInclusive, int upperExclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int HalfOpenRange(this IRandomEngine random, int upperExclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperExclusive));
		}

		public static uint HalfOpenRange(this IRandomEngine random, uint lowerInclusive, uint upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public static uint HalfOpenRange(this IRandomEngine random, uint upperExclusive)
		{
			if (upperExclusive == 0) throw new System.ArgumentOutOfRangeException("upperBound");
			uint mask = upperExclusive - 1U;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			uint n;
			do
			{
				n = random.Next32() & mask;
			}
			while (n >= upperExclusive);
			return n;
		}

		public static long HalfOpenRange(this IRandomEngine random, long lowerInclusive, long upperExclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static long HalfOpenRange(this IRandomEngine random, long upperExclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperExclusive));
		}

		public static ulong HalfOpenRange(this IRandomEngine random, ulong lowerInclusive, ulong upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public static ulong HalfOpenRange(this IRandomEngine random, ulong upperExclusive)
		{
			if (upperExclusive == 0) throw new System.ArgumentOutOfRangeException("upperBound");
			ulong mask = upperExclusive - 1UL;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			ulong n;
			do
			{
				n = random.Next64() & mask;
			}
			while (n >= upperExclusive);
			return n;
		}

		public static float HalfOpenRange(this IRandomEngine random, float lowerInclusive, float upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * random.HalfOpenFloatUnit() + lowerInclusive;
		}

		public static float HalfOpenRange(this IRandomEngine random, float upperExclusive)
		{
			return upperExclusive * random.HalfOpenFloatUnit();
		}

		public static double HalfOpenRange(this IRandomEngine random, double lowerInclusive, double upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * random.HalfOpenDoubleUnit() + lowerInclusive;
		}

		public static double HalfOpenRange(this IRandomEngine random, double upperExclusive)
		{
			return upperExclusive * random.HalfOpenDoubleUnit();
		}

		#endregion

		#region HalfClosed

		public static int HalfClosedRange(this IRandomEngine random, int lowerExclusive, int upperInclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperInclusive - lowerExclusive)) + lowerExclusive + 1;
		}

		public static int HalfClosedRange(this IRandomEngine random, int upperInclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperInclusive)) + 1;
		}

		public static uint HalfClosedRange(this IRandomEngine random, uint lowerExclusive, uint upperInclusive)
		{
			return HalfOpenRange(random, upperInclusive - lowerExclusive) + lowerExclusive + 1U;
		}

		public static uint HalfClosedRange(this IRandomEngine random, uint upperInclusive)
		{
			return HalfOpenRange(random, upperInclusive) + 1U;
		}

		public static long HalfClosedRange(this IRandomEngine random, long lowerExclusive, long upperInclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperInclusive - lowerExclusive)) + lowerExclusive + 1L;
		}

		public static long HalfClosedRange(this IRandomEngine random, long upperInclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperInclusive)) + 1L;
		}

		public static ulong HalfClosedRange(this IRandomEngine random, ulong lowerExclusive, ulong upperInclusive)
		{
			return HalfOpenRange(random, upperInclusive - lowerExclusive) + lowerExclusive + 1UL;
		}

		public static ulong HalfClosedRange(this IRandomEngine random, ulong upperInclusive)
		{
			return HalfOpenRange(random, upperInclusive) + 1UL;
		}

		public static float HalfClosedRange(this IRandomEngine random, float lowerExclusive, float upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * random.HalfClosedFloatUnit() + lowerExclusive;
		}

		public static float HalfClosedRange(this IRandomEngine random, float upperInclusive)
		{
			return upperInclusive * random.HalfClosedFloatUnit();
		}

		public static double HalfClosedRange(this IRandomEngine random, double lowerExclusive, double upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * random.HalfClosedDoubleUnit() + lowerExclusive;
		}

		public static double HalfClosedRange(this IRandomEngine random, double upperInclusive)
		{
			return upperInclusive * random.HalfClosedDoubleUnit();
		}

		#endregion

		#region Closed

		public static int ClosedRange(this IRandomEngine random, int lowerInclusive, int upperInclusive)
		{
			return (int)ClosedRange(random, (uint)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int ClosedRange(this IRandomEngine random, int upperInclusive)
		{
			return (int)ClosedRange(random, (uint)(upperInclusive));
		}

		public static uint ClosedRange(this IRandomEngine random, uint lowerInclusive, uint upperInclusive)
		{
			return ClosedRange(random, upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public static uint ClosedRange(this IRandomEngine random, uint upperInclusive)
		{
			uint mask = upperInclusive;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			uint n;
			do
			{
				n = random.Next32() & mask;
			}
			while (n > upperInclusive);
			return n;
		}

		public static long ClosedRange(this IRandomEngine random, long lowerInclusive, long upperInclusive)
		{
			return (long)ClosedRange(random, (ulong)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public static long ClosedRange(this IRandomEngine random, long upperInclusive)
		{
			return (long)ClosedRange(random, (ulong)(upperInclusive));
		}

		public static ulong ClosedRange(this IRandomEngine random, ulong lowerInclusive, ulong upperInclusive)
		{
			return ClosedRange(random, upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public static ulong ClosedRange(this IRandomEngine random, ulong upperInclusive)
		{
			ulong mask = upperInclusive;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			ulong n;
			do
			{
				n = random.Next64() & mask;
			}
			while (n > upperInclusive);
			return n;
		}

		public static float ClosedRange(this IRandomEngine random, float lowerInclusive, float upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * random.ClosedFloatUnit() + lowerInclusive;
		}

		public static float ClosedRange(this IRandomEngine random, float upperInclusive)
		{
			return upperInclusive * random.ClosedFloatUnit();
		}

		public static double ClosedRange(this IRandomEngine random, double lowerInclusive, double upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * random.ClosedDoubleUnit() + lowerInclusive;
		}

		public static double ClosedRange(this IRandomEngine random, double upperInclusive)
		{
			return upperInclusive * random.ClosedDoubleUnit();
		}

		#endregion
	}
}
