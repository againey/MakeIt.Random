/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public static class RandomRange
	{
		#region Open

		public static int Open(int lowerExclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)HalfOpen((uint)(upperExclusive - lowerExclusive - 1), engine) + lowerExclusive + 1;
		}

		public static int Open(int upperExclusive, IRandomEngine engine)
		{
			return (int)HalfOpen((uint)(upperExclusive - 1), engine) + 1;
		}

		public static uint Open(uint lowerExclusive, uint upperExclusive, IRandomEngine engine)
		{
			return HalfOpen(upperExclusive - lowerExclusive - 1U, engine) + lowerExclusive + 1U;
		}

		public static uint Open(uint upperExclusive, IRandomEngine engine)
		{
			return HalfOpen(upperExclusive - 1U, engine) + 1U;
		}

		public static long Open(long lowerExclusive, long upperExclusive, IRandomEngine engine)
		{
			return (long)HalfOpen((ulong)(upperExclusive - lowerExclusive - 1L), engine) + lowerExclusive + 1L;
		}

		public static long Open(long upperExclusive, IRandomEngine engine)
		{
			return (long)HalfOpen((ulong)(upperExclusive - 1L), engine) + 1L;
		}

		public static ulong Open(ulong lowerExclusive, ulong upperExclusive, IRandomEngine engine)
		{
			return HalfOpen(upperExclusive - lowerExclusive - 1UL, engine) + lowerExclusive + 1UL;
		}

		public static ulong Open(ulong upperExclusive, IRandomEngine engine)
		{
			return HalfOpen(upperExclusive - 1UL, engine) + 1UL;
		}

		public static float Open(float lowerExclusive, float upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerExclusive) * RandomUnit.OpenFloat(engine) + lowerExclusive;
		}

		public static float Open(float upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * RandomUnit.OpenFloat(engine);
		}

		public static double Open(double lowerExclusive, double upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerExclusive) * RandomUnit.OpenDouble(engine) + lowerExclusive;
		}

		public static double Open(double upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * RandomUnit.OpenDouble(engine);
		}

		#endregion

		#region HalfOpen

		public static int HalfOpen(int lowerInclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)HalfOpen((uint)(upperExclusive - lowerInclusive), engine) + lowerInclusive;
		}

		public static int HalfOpen(int upperExclusive, IRandomEngine engine)
		{
			return (int)HalfOpen((uint)(upperExclusive), engine);
		}

		public static uint HalfOpen(uint lowerInclusive, uint upperExclusive, IRandomEngine engine)
		{
			return HalfOpen(upperExclusive - lowerInclusive, engine) + lowerInclusive;
		}

		public static uint HalfOpen(uint upperExclusive, IRandomEngine engine)
		{
			if (upperExclusive == 0) throw new System.ArgumentOutOfRangeException("upperBound");
			uint mask = upperExclusive - 1U;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			uint random;
			do
			{
				random = engine.Next32() & mask;
			}
			while (random >= upperExclusive);
			return random;
		}

		public static long HalfOpen(long lowerInclusive, long upperExclusive, IRandomEngine engine)
		{
			return (long)HalfOpen((ulong)(upperExclusive - lowerInclusive), engine) + lowerInclusive;
		}

		public static long HalfOpen(long upperExclusive, IRandomEngine engine)
		{
			return (long)HalfOpen((ulong)(upperExclusive), engine);
		}

		public static ulong HalfOpen(ulong lowerInclusive, ulong upperExclusive, IRandomEngine engine)
		{
			return HalfOpen(upperExclusive - lowerInclusive, engine) + lowerInclusive;
		}

		public static ulong HalfOpen(ulong upperExclusive, IRandomEngine engine)
		{
			if (upperExclusive == 0) throw new System.ArgumentOutOfRangeException("upperBound");
			ulong mask = upperExclusive - 1UL;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			ulong random;
			do
			{
				random = engine.Next64() & mask;
			}
			while (random >= upperExclusive);
			return random;
		}

		public static float HalfOpen(float lowerInclusive, float upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerInclusive) * RandomUnit.HalfOpenFloat(engine) + lowerInclusive;
		}

		public static float HalfOpen(float upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * RandomUnit.HalfOpenFloat(engine);
		}

		public static double HalfOpen(double lowerInclusive, double upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerInclusive) * RandomUnit.HalfOpenDouble(engine) + lowerInclusive;
		}

		public static double HalfOpen(double upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * RandomUnit.HalfOpenDouble(engine);
		}

		#endregion

		#region HalfClosed

		public static int HalfClosed(int lowerExclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)HalfOpen((uint)(upperInclusive - lowerExclusive), engine) + lowerExclusive + 1;
		}

		public static int HalfClosed(int upperInclusive, IRandomEngine engine)
		{
			return (int)HalfOpen((uint)(upperInclusive), engine) + 1;
		}

		public static uint HalfClosed(uint lowerExclusive, uint upperInclusive, IRandomEngine engine)
		{
			return HalfOpen(upperInclusive - lowerExclusive, engine) + lowerExclusive + 1U;
		}

		public static uint HalfClosed(uint upperInclusive, IRandomEngine engine)
		{
			return HalfOpen(upperInclusive, engine) + 1U;
		}

		public static long HalfClosed(long lowerExclusive, long upperInclusive, IRandomEngine engine)
		{
			return (long)HalfOpen((ulong)(upperInclusive - lowerExclusive), engine) + lowerExclusive + 1L;
		}

		public static long HalfClosed(long upperInclusive, IRandomEngine engine)
		{
			return (long)HalfOpen((ulong)(upperInclusive), engine) + 1L;
		}

		public static ulong HalfClosed(ulong lowerExclusive, ulong upperInclusive, IRandomEngine engine)
		{
			return HalfOpen(upperInclusive - lowerExclusive, engine) + lowerExclusive + 1UL;
		}

		public static ulong HalfClosed(ulong upperInclusive, IRandomEngine engine)
		{
			return HalfOpen(upperInclusive, engine) + 1UL;
		}

		public static float HalfClosed(float lowerExclusive, float upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerExclusive) * RandomUnit.HalfClosedFloat(engine) + lowerExclusive;
		}

		public static float HalfClosed(float upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.HalfClosedFloat(engine);
		}

		public static double HalfClosed(double lowerExclusive, double upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerExclusive) * RandomUnit.HalfClosedDouble(engine) + lowerExclusive;
		}

		public static double HalfClosed(double upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.HalfClosedDouble(engine);
		}

		#endregion

		#region Closed

		public static int Closed(int lowerInclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)Closed((uint)(upperInclusive - lowerInclusive), engine) + lowerInclusive;
		}

		public static int Closed(int upperInclusive, IRandomEngine engine)
		{
			return (int)Closed((uint)(upperInclusive), engine);
		}

		public static uint Closed(uint lowerInclusive, uint upperInclusive, IRandomEngine engine)
		{
			return Closed(upperInclusive - lowerInclusive, engine) + lowerInclusive;
		}

		public static uint Closed(uint upperInclusive, IRandomEngine engine)
		{
			uint mask = upperInclusive;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			uint random;
			do
			{
				random = engine.Next32() & mask;
			}
			while (random > upperInclusive);
			return random;
		}

		public static long Closed(long lowerInclusive, long upperInclusive, IRandomEngine engine)
		{
			return (long)Closed((ulong)(upperInclusive - lowerInclusive), engine) + lowerInclusive;
		}

		public static long Closed(long upperInclusive, IRandomEngine engine)
		{
			return (long)Closed((ulong)(upperInclusive), engine);
		}

		public static ulong Closed(ulong lowerInclusive, ulong upperInclusive, IRandomEngine engine)
		{
			return Closed(upperInclusive - lowerInclusive, engine) + lowerInclusive;
		}

		public static ulong Closed(ulong upperInclusive, IRandomEngine engine)
		{
			ulong mask = upperInclusive;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
			ulong random;
			do
			{
				random = engine.Next64() & mask;
			}
			while (random > upperInclusive);
			return random;
		}

		public static float Closed(float lowerInclusive, float upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedFloat(engine) + lowerInclusive;
		}

		public static float Closed(float upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.ClosedFloat(engine);
		}

		public static double Closed(double lowerInclusive, double upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedDouble(engine) + lowerInclusive;
		}

		public static double Closed(double upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.ClosedDouble(engine);
		}

		#endregion
	}
}
