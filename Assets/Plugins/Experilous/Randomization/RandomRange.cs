/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

// Uncomment this if you want perfect uniformity at the expense of performance in
// some cases, in particular all integer-based and closed floating point ranges.
#define FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY

// Uncomment both this and the above define if you want the above define to apply
// to generation of floating point ranges regardless of the state of the similar
// setting in the RandomUnit file.
#define DEFER_TO_RANDOM_UNIT_IMPLEMENTATION

namespace Experilous.Randomization
{
	public static class RandomRange
	{
		#region Open

		public static int Open(int lowerExclusive, int upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return OpenFast(lowerExclusive, upperExclusive, engine);
#else
			return OpenPerfect(lowerExclusive, upperExclusive, engine);
#endif
		}

		public static int OpenPerfect(int lowerExclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1;
		}

		public static int OpenFast(int lowerExclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1;
		}

		public static int Open(int upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return OpenFast(upperExclusive, engine);
#else
			return OpenPerfect(upperExclusive, engine);
#endif
		}

		public static int OpenPerfect(int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperExclusive - 1)) + 1;
		}

		public static int OpenFast(int upperExclusive, IRandomEngine engine)
		{
			return (int)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - 1)) + 1;
		}

		public static uint Open(uint lowerExclusive, uint upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return OpenFast(lowerExclusive, upperExclusive, engine);
#else
			return OpenPerfect(lowerExclusive, upperExclusive, engine);
#endif
		}

		public static uint OpenPerfect(uint lowerExclusive, uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - lowerExclusive - 1U) + lowerExclusive + 1U;
		}

		public static uint OpenFast(uint lowerExclusive, uint upperExclusive, IRandomEngine engine)
		{
			return (uint)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - lowerExclusive - 1U)) + lowerExclusive + 1U;
		}

		public static uint Open(uint upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return OpenFast(upperExclusive, engine);
#else
			return OpenPerfect(upperExclusive, engine);
#endif
		}

		public static uint OpenPerfect(uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - 1U) + 1U;
		}

		public static uint OpenFast(uint upperExclusive, IRandomEngine engine)
		{
			return (uint)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - 1U)) + 1U;
		}

		public static long Open(long lowerExclusive, long upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return OpenFast(lowerExclusive, upperExclusive, engine);
#else
			return OpenPerfect(lowerExclusive, upperExclusive, engine);
#endif
		}

		public static long OpenPerfect(long lowerExclusive, long upperExclusive, IRandomEngine engine)
		{
			return (long)engine.NextLessThan((ulong)(upperExclusive - lowerExclusive - 1L)) + lowerExclusive + 1L;
		}

		public static long OpenFast(long lowerExclusive, long upperExclusive, IRandomEngine engine)
		{
			return (long)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - lowerExclusive - 1L)) + lowerExclusive + 1L;
		}

		public static long Open(long upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return OpenFast(upperExclusive, engine);
#else
			return OpenPerfect(upperExclusive, engine);
#endif
		}

		public static long OpenPerfect(long upperExclusive, IRandomEngine engine)
		{
			return (long)engine.NextLessThan((ulong)(upperExclusive - 1L)) + 1L;
		}

		public static long OpenFast(long upperExclusive, IRandomEngine engine)
		{
			return (long)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - 1L)) + 1L;
		}

		public static ulong Open(ulong lowerExclusive, ulong upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return OpenFast(lowerExclusive, upperExclusive, engine);
#else
			return OpenPerfect(lowerExclusive, upperExclusive, engine);
#endif
		}

		public static ulong OpenPerfect(ulong lowerExclusive, ulong upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - lowerExclusive - 1UL) + lowerExclusive + 1UL;
		}

		public static ulong OpenFast(ulong lowerExclusive, ulong upperExclusive, IRandomEngine engine)
		{
			return (ulong)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - lowerExclusive - 1UL)) + lowerExclusive + 1UL;
		}

		public static ulong Open(ulong upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return OpenFast(upperExclusive, engine);
#else
			return OpenPerfect(upperExclusive, engine);
#endif
		}

		public static ulong OpenPerfect(ulong upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - 1UL) + 1UL;
		}

		public static ulong OpenFast(ulong upperExclusive, IRandomEngine engine)
		{
			return (ulong)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - 1UL)) + 1UL;
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
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfOpenFast(lowerInclusive, upperExclusive, engine);
#else
			return HalfOpenPerfect(lowerInclusive, upperExclusive, engine);
#endif
		}

		public static int HalfOpenPerfect(int lowerInclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int HalfOpenFast(int lowerInclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int HalfOpen(int upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfOpenFast(upperExclusive, engine);
#else
			return HalfOpenPerfect(upperExclusive, engine);
#endif
		}

		public static int HalfOpenPerfect(int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperExclusive));
		}

		public static int HalfOpenFast(int upperExclusive, IRandomEngine engine)
		{
			return (int)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * upperExclusive);
		}

		public static uint HalfOpen(uint lowerInclusive, uint upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfOpenFast(lowerInclusive, upperExclusive, engine);
#else
			return HalfOpenPerfect(lowerInclusive, upperExclusive, engine);
#endif
		}

		public static uint HalfOpenPerfect(uint lowerInclusive, uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public static uint HalfOpenFast(uint lowerInclusive, uint upperExclusive, IRandomEngine engine)
		{
			return (uint)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static uint HalfOpen(uint upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfOpenFast(upperExclusive, engine);
#else
			return HalfOpenPerfect(upperExclusive, engine);
#endif
		}

		public static uint HalfOpenPerfect(uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive);
		}

		public static uint HalfOpenFast(uint upperExclusive, IRandomEngine engine)
		{
			return (uint)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * upperExclusive);
		}

		public static long HalfOpen(long lowerInclusive, long upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfOpenFast(lowerInclusive, upperExclusive, engine);
#else
			return HalfOpenPerfect(lowerInclusive, upperExclusive, engine);
#endif
		}

		public static long HalfOpenPerfect(long lowerInclusive, long upperExclusive, IRandomEngine engine)
		{
			return (long)engine.NextLessThan((ulong)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static long HalfOpenFast(long lowerInclusive, long upperExclusive, IRandomEngine engine)
		{
			return (long)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static long HalfOpen(long upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfOpenFast(upperExclusive, engine);
#else
			return HalfOpenPerfect(upperExclusive, engine);
#endif
		}

		public static long HalfOpenPerfect(long upperExclusive, IRandomEngine engine)
		{
			return (long)engine.NextLessThan((ulong)(upperExclusive));
		}

		public static long HalfOpenFast(long upperExclusive, IRandomEngine engine)
		{
			return (long)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * upperExclusive);
		}

		public static ulong HalfOpen(ulong lowerInclusive, ulong upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfOpenFast(lowerInclusive, upperExclusive, engine);
#else
			return HalfOpenPerfect(lowerInclusive, upperExclusive, engine);
#endif
		}

		public static ulong HalfOpenPerfect(ulong lowerInclusive, ulong upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public static ulong HalfOpenFast(ulong lowerInclusive, ulong upperExclusive, IRandomEngine engine)
		{
			return (ulong)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static ulong HalfOpen(ulong upperExclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfOpenFast(upperExclusive, engine);
#else
			return HalfOpenPerfect(upperExclusive, engine);
#endif
		}

		public static ulong HalfOpenPerfect(ulong upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive);
		}

		public static ulong HalfOpenFast(ulong upperExclusive, IRandomEngine engine)
		{
			return (ulong)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * upperExclusive);
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
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfClosedFast(lowerExclusive, upperInclusive, engine);
#else
			return HalfClosedPerfect(lowerExclusive, upperInclusive, engine);
#endif
		}

		public static int HalfClosedPerfect(int lowerExclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperInclusive - lowerExclusive)) + lowerExclusive + 1;
		}

		public static int HalfClosedFast(int lowerExclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive - lowerExclusive)) + lowerExclusive + 1;
		}

		public static int HalfClosed(int upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfClosedFast(upperInclusive, engine);
#else
			return HalfClosedPerfect(upperInclusive, engine);
#endif
		}

		public static int HalfClosedPerfect(int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperInclusive)) + 1;
		}

		public static int HalfClosedFast(int upperInclusive, IRandomEngine engine)
		{
			return (int)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * upperInclusive) + 1;
		}

		public static uint HalfClosed(uint lowerExclusive, uint upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfClosedFast(lowerExclusive, upperInclusive, engine);
#else
			return HalfClosedPerfect(lowerExclusive, upperInclusive, engine);
#endif
		}

		public static uint HalfClosedPerfect(uint lowerExclusive, uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperInclusive - lowerExclusive) + lowerExclusive + 1U;
		}

		public static uint HalfClosedFast(uint lowerExclusive, uint upperInclusive, IRandomEngine engine)
		{
			return (uint)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive - lowerExclusive)) + lowerExclusive + 1U;
		}

		public static uint HalfClosed(uint upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfClosedFast(upperInclusive, engine);
#else
			return HalfClosedPerfect(upperInclusive, engine);
#endif
		}

		public static uint HalfClosedPerfect(uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperInclusive) + 1U;
		}

		public static uint HalfClosedFast(uint upperInclusive, IRandomEngine engine)
		{
			return (uint)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * upperInclusive) + 1U;
		}

		public static long HalfClosed(long lowerExclusive, long upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfClosedFast(lowerExclusive, upperInclusive, engine);
#else
			return HalfClosedPerfect(lowerExclusive, upperInclusive, engine);
#endif
		}

		public static long HalfClosedPerfect(long lowerExclusive, long upperInclusive, IRandomEngine engine)
		{
			return (long)engine.NextLessThan((ulong)(upperInclusive - lowerExclusive)) + lowerExclusive + 1L;
		}

		public static long HalfClosedFast(long lowerExclusive, long upperInclusive, IRandomEngine engine)
		{
			return (long)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive - lowerExclusive)) + lowerExclusive + 1L;
		}

		public static long HalfClosed(long upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfClosedFast(upperInclusive, engine);
#else
			return HalfClosedPerfect(upperInclusive, engine);
#endif
		}

		public static long HalfClosedPerfect(long upperInclusive, IRandomEngine engine)
		{
			return (long)engine.NextLessThan((ulong)(upperInclusive)) + 1L;
		}

		public static long HalfClosedFast(long upperInclusive, IRandomEngine engine)
		{
			return (long)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * upperInclusive) + 1L;
		}

		public static ulong HalfClosed(ulong lowerExclusive, ulong upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfClosedFast(lowerExclusive, upperInclusive, engine);
#else
			return HalfClosedPerfect(lowerExclusive, upperInclusive, engine);
#endif
		}

		public static ulong HalfClosedPerfect(ulong lowerExclusive, ulong upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperInclusive - lowerExclusive) + lowerExclusive + 1UL;
		}

		public static ulong HalfClosedFast(ulong lowerExclusive, ulong upperInclusive, IRandomEngine engine)
		{
			return (ulong)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive - lowerExclusive)) + lowerExclusive + 1UL;
		}

		public static ulong HalfClosed(ulong upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return HalfClosedFast(upperInclusive, engine);
#else
			return HalfClosedPerfect(upperInclusive, engine);
#endif
		}

		public static ulong HalfClosedPerfect(ulong upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperInclusive) + 1UL;
		}

		public static ulong HalfClosedFast(ulong upperInclusive, IRandomEngine engine)
		{
			return (ulong)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * upperInclusive) + 1UL;
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
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(lowerInclusive, upperInclusive, engine);
#else
			return ClosedPerfect(lowerInclusive, upperInclusive, engine);
#endif
		}

		public static int ClosedPerfect(int lowerInclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThanOrEqual((uint)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int ClosedFast(int lowerInclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive - lowerInclusive + 1)) + lowerInclusive;
		}

		public static int Closed(int upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(upperInclusive, engine);
#else
			return ClosedPerfect(upperInclusive, engine);
#endif
		}

		public static int ClosedPerfect(int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThanOrEqual((uint)(upperInclusive));
		}

		public static int ClosedFast(int upperInclusive, IRandomEngine engine)
		{
			return (int)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive + 1));
		}

		public static uint Closed(uint lowerInclusive, uint upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(lowerInclusive, upperInclusive, engine);
#else
			return ClosedPerfect(lowerInclusive, upperInclusive, engine);
#endif
		}

		public static uint ClosedPerfect(uint lowerInclusive, uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThanOrEqual(upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public static uint ClosedFast(uint lowerInclusive, uint upperInclusive, IRandomEngine engine)
		{
			return (uint)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive - lowerInclusive + 1U)) + lowerInclusive;
		}

		public static uint Closed(uint upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(upperInclusive, engine);
#else
			return ClosedPerfect(upperInclusive, engine);
#endif
		}

		public static uint ClosedPerfect(uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThanOrEqual(upperInclusive);
		}

		public static uint ClosedFast(uint upperInclusive, IRandomEngine engine)
		{
			return (uint)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive + 1U));
		}

		public static long Closed(long lowerInclusive, long upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(lowerInclusive, upperInclusive, engine);
#else
			return ClosedPerfect(lowerInclusive, upperInclusive, engine);
#endif
		}

		public static long ClosedPerfect(long lowerInclusive, long upperInclusive, IRandomEngine engine)
		{
			return (long)engine.NextLessThanOrEqual((ulong)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public static long ClosedFast(long lowerInclusive, long upperInclusive, IRandomEngine engine)
		{
			return (long)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive - lowerInclusive + 1L)) + lowerInclusive;
		}

		public static long Closed(long upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(upperInclusive, engine);
#else
			return ClosedPerfect(upperInclusive, engine);
#endif
		}

		public static long ClosedPerfect(long upperInclusive, IRandomEngine engine)
		{
			return (long)engine.NextLessThanOrEqual((ulong)(upperInclusive));
		}

		public static long ClosedFast(long upperInclusive, IRandomEngine engine)
		{
			return (long)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive + 1L));
		}

		public static ulong Closed(ulong lowerInclusive, ulong upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(lowerInclusive, upperInclusive, engine);
#else
			return ClosedPerfect(lowerInclusive, upperInclusive, engine);
#endif
		}

		public static ulong ClosedPerfect(ulong lowerInclusive, ulong upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThanOrEqual(upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public static ulong ClosedFast(ulong lowerInclusive, ulong upperInclusive, IRandomEngine engine)
		{
			return (ulong)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive - lowerInclusive + 1UL)) + lowerInclusive;
		}

		public static ulong Closed(ulong upperInclusive, IRandomEngine engine)
		{
#if FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(upperInclusive, engine);
#else
			return ClosedPerfect(upperInclusive, engine);
#endif
		}

		public static ulong ClosedPerfect(ulong upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThanOrEqual(upperInclusive);
		}

		public static ulong ClosedFast(ulong upperInclusive, IRandomEngine engine)
		{
			return (ulong)System.Math.Floor(RandomUnit.HalfOpenDouble(engine) * (upperInclusive + 1UL));
		}

		public static float Closed(float lowerInclusive, float upperInclusive, IRandomEngine engine)
		{
#if DEFER_TO_RANDOM_UNIT_IMPLEMENTATION
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedFloat(engine) + lowerInclusive;
#elif FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(lowerInclusive, upperInclusive, engine);
#else
			return ClosedPerfect(lowerInclusive, upperInclusive, engine);
#endif
		}

		public static float ClosedPerfect(float lowerInclusive, float upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedFloatPerfect(engine) + lowerInclusive;
		}

		public static float ClosedFast(float lowerInclusive, float upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedFloatFast(engine) + lowerInclusive;
		}

		public static float Closed(float upperInclusive, IRandomEngine engine)
		{
#if DEFER_TO_RANDOM_UNIT_IMPLEMENTATION
			return upperInclusive * RandomUnit.ClosedFloat(engine);
#elif FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(upperInclusive, engine);
#else
			return ClosedPerfect(upperInclusive, engine);
#endif
		}

		public static float ClosedPerfect(float upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.ClosedFloatPerfect(engine);
		}

		public static float ClosedFast(float upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.ClosedFloatFast(engine);
		}

		public static double Closed(double lowerInclusive, double upperInclusive, IRandomEngine engine)
		{
#if DEFER_TO_RANDOM_UNIT_IMPLEMENTATION
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedDouble(engine) + lowerInclusive;
#elif FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(lowerInclusive, upperInclusive, engine);
#else
			return ClosedPerfect(lowerInclusive, upperInclusive, engine);
#endif
		}

		public static double ClosedPerfect(double lowerInclusive, double upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedDoublePerfect(engine) + lowerInclusive;
		}

		public static double ClosedFast(double lowerInclusive, double upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedDoubleFast(engine) + lowerInclusive;
		}

		public static double Closed(double upperInclusive, IRandomEngine engine)
		{
#if DEFER_TO_RANDOM_UNIT_IMPLEMENTATION
			return upperInclusive * RandomUnit.ClosedDouble(engine);
#elif FAVOR_CONSISTENT_SPEED_OVER_PERFECT_UNIFORMITY
			return ClosedFast(upperInclusive, engine);
#else
			return ClosedPerfect(upperInclusive, engine);
#endif
		}

		public static double ClosedPerfect(double upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.ClosedDoublePerfect(engine);
		}

		public static double ClosedFast(double upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.ClosedDoubleFast(engine);
		}

#endregion
	}
}
