/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public static class RandomRange
	{
		#region Open

		public static int Open(int lowerExclusive, int upperExclusive)
		{
			return (int)DefaultRandomEngine.sharedInstance.NextLessThan((uint)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1;
		}

		public static int Open(int lowerExclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1;
		}

		public static int Open(int upperExclusive)
		{
			return (int)DefaultRandomEngine.sharedInstance.NextLessThan((uint)(upperExclusive - 1)) + 1;
		}

		public static int Open(int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperExclusive - 1)) + 1;
		}

		public static uint Open(uint lowerExclusive, uint upperExclusive)
		{
			return DefaultRandomEngine.sharedInstance.NextLessThan(upperExclusive - lowerExclusive - 1U) + lowerExclusive + 1U;
		}

		public static uint Open(uint lowerExclusive, uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - lowerExclusive - 1U) + lowerExclusive + 1U;
		}

		public static uint Open(uint upperExclusive)
		{
			return DefaultRandomEngine.sharedInstance.NextLessThan(upperExclusive - 1U) + 1U;
		}

		public static uint Open(uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - 1U) + 1U;
		}

		public static float Open(float lowerExclusive, float upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * RandomUnit.OpenFloat(DefaultRandomEngine.sharedInstance) + lowerExclusive;
		}

		public static float Open(float lowerExclusive, float upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerExclusive) * RandomUnit.OpenFloat(engine) + lowerExclusive;
		}

		public static float Open(float upperExclusive)
		{
			return upperExclusive * RandomUnit.OpenFloat(DefaultRandomEngine.sharedInstance);
		}

		public static float Open(float upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * RandomUnit.OpenFloat(engine);
		}

		public static double Open(double lowerExclusive, double upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * RandomUnit.OpenDouble(DefaultRandomEngine.sharedInstance) + lowerExclusive;
		}

		public static double Open(double lowerExclusive, double upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerExclusive) * RandomUnit.OpenDouble(engine) + lowerExclusive;
		}

		public static double Open(double upperExclusive)
		{
			return upperExclusive * RandomUnit.OpenDouble(DefaultRandomEngine.sharedInstance);
		}

		public static double Open(double upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * RandomUnit.OpenDouble(engine);
		}

		#endregion

		#region HalfOpen

		public static int HalfOpen(int lowerInclusive, int upperExclusive)
		{
			return (int)DefaultRandomEngine.sharedInstance.NextLessThan((uint)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int HalfOpen(int lowerInclusive, int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int HalfOpen(int upperExclusive)
		{
			return (int)DefaultRandomEngine.sharedInstance.NextLessThan((uint)upperExclusive);
		}

		public static int HalfOpen(int upperExclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)upperExclusive);
		}

		public static uint HalfOpen(uint lowerInclusive, uint upperExclusive)
		{
			return DefaultRandomEngine.sharedInstance.NextLessThan(upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public static uint HalfOpen(uint lowerInclusive, uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public static uint HalfOpen(uint upperExclusive)
		{
			return DefaultRandomEngine.sharedInstance.NextLessThan(upperExclusive);
		}

		public static uint HalfOpen(uint upperExclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperExclusive);
		}

		public static float HalfOpen(float lowerInclusive, float upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * RandomUnit.HalfOpenFloat(DefaultRandomEngine.sharedInstance) + lowerInclusive;
		}

		public static float HalfOpen(float lowerInclusive, float upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerInclusive) * RandomUnit.HalfOpenFloat(engine) + lowerInclusive;
		}

		public static float HalfOpen(float upperExclusive)
		{
			return upperExclusive * RandomUnit.HalfOpenFloat(DefaultRandomEngine.sharedInstance);
		}

		public static float HalfOpen(float upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * RandomUnit.HalfOpenFloat(engine);
		}

		public static double HalfOpen(double lowerInclusive, double upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * RandomUnit.HalfOpenDouble(DefaultRandomEngine.sharedInstance) + lowerInclusive;
		}

		public static double HalfOpen(double lowerInclusive, double upperExclusive, IRandomEngine engine)
		{
			return (upperExclusive - lowerInclusive) * RandomUnit.HalfOpenDouble(engine) + lowerInclusive;
		}

		public static double HalfOpen(double upperExclusive)
		{
			return upperExclusive * RandomUnit.HalfOpenDouble(DefaultRandomEngine.sharedInstance);
		}

		public static double HalfOpen(double upperExclusive, IRandomEngine engine)
		{
			return upperExclusive * RandomUnit.HalfOpenDouble(engine);
		}

		#endregion

		#region HalfClosed

		public static int HalfClosed(int lowerExclusive, int upperInclusive)
		{
			return (int)DefaultRandomEngine.sharedInstance.NextLessThan((uint)(upperInclusive - lowerExclusive)) + lowerExclusive + 1;
		}

		public static int HalfClosed(int lowerExclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)(upperInclusive - lowerExclusive)) + lowerExclusive + 1;
		}

		public static int HalfClosed(int upperInclusive)
		{
			return (int)DefaultRandomEngine.sharedInstance.NextLessThan((uint)upperInclusive) + 1;
		}

		public static int HalfClosed(int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)upperInclusive) + 1;
		}

		public static uint HalfClosed(uint lowerExclusive, uint upperInclusive)
		{
			return DefaultRandomEngine.sharedInstance.NextLessThan(upperInclusive - lowerExclusive) + lowerExclusive + 1U;
		}

		public static uint HalfClosed(uint lowerExclusive, uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperInclusive - lowerExclusive) + lowerExclusive + 1U;
		}

		public static uint HalfClosed(uint upperInclusive)
		{
			return DefaultRandomEngine.sharedInstance.NextLessThan(upperInclusive) + 1U;
		}

		public static uint HalfClosed(uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThan(upperInclusive) + 1U;
		}

		public static float HalfClosed(float lowerExclusive, float upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * RandomUnit.HalfClosedFloat(DefaultRandomEngine.sharedInstance) + lowerExclusive;
		}

		public static float HalfClosed(float lowerExclusive, float upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerExclusive) * RandomUnit.HalfClosedFloat(engine) + lowerExclusive;
		}

		public static float HalfClosed(float upperInclusive)
		{
			return upperInclusive * RandomUnit.HalfClosedFloat(DefaultRandomEngine.sharedInstance);
		}

		public static float HalfClosed(float upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.HalfClosedFloat(engine);
		}

		public static double HalfClosed(double lowerExclusive, double upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * RandomUnit.HalfClosedDouble(DefaultRandomEngine.sharedInstance) + lowerExclusive;
		}

		public static double HalfClosed(double lowerExclusive, double upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerExclusive) * RandomUnit.HalfClosedDouble(engine) + lowerExclusive;
		}

		public static double HalfClosed(double upperInclusive)
		{
			return upperInclusive * RandomUnit.HalfClosedDouble(DefaultRandomEngine.sharedInstance);
		}

		public static double HalfClosed(double upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.HalfClosedDouble(engine);
		}

		#endregion

		#region Closed

		public static int Closed(int lowerInclusive, int upperInclusive)
		{
			return (int)DefaultRandomEngine.sharedInstance.NextLessThanOrEqual((uint)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int Closed(int lowerInclusive, int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThanOrEqual((uint)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public static int Closed(int upperInclusive)
		{
			return (int)DefaultRandomEngine.sharedInstance.NextLessThanOrEqual((uint)upperInclusive);
		}

		public static int Closed(int upperInclusive, IRandomEngine engine)
		{
			return (int)engine.NextLessThanOrEqual((uint)upperInclusive);
		}

		public static uint Closed(uint lowerInclusive, uint upperInclusive)
		{
			return DefaultRandomEngine.sharedInstance.NextLessThanOrEqual(upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public static uint Closed(uint lowerInclusive, uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThanOrEqual(upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public static uint Closed(uint upperInclusive)
		{
			return DefaultRandomEngine.sharedInstance.NextLessThanOrEqual(upperInclusive);
		}

		public static uint Closed(uint upperInclusive, IRandomEngine engine)
		{
			return engine.NextLessThanOrEqual(upperInclusive);
		}

		public static float Closed(float lowerInclusive, float upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedFloat(DefaultRandomEngine.sharedInstance) + lowerInclusive;
		}

		public static float Closed(float lowerInclusive, float upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedFloat(engine) + lowerInclusive;
		}

		public static float Closed(float upperInclusive)
		{
			return upperInclusive * RandomUnit.ClosedFloat(DefaultRandomEngine.sharedInstance);
		}

		public static float Closed(float upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.ClosedFloat(engine);
		}

		public static double Closed(double lowerInclusive, double upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedDouble(DefaultRandomEngine.sharedInstance) + lowerInclusive;
		}

		public static double Closed(double lowerInclusive, double upperInclusive, IRandomEngine engine)
		{
			return (upperInclusive - lowerInclusive) * RandomUnit.ClosedDouble(engine) + lowerInclusive;
		}

		public static double Closed(double upperInclusive)
		{
			return upperInclusive * RandomUnit.ClosedDouble(DefaultRandomEngine.sharedInstance);
		}

		public static double Closed(double upperInclusive, IRandomEngine engine)
		{
			return upperInclusive * RandomUnit.ClosedDouble(engine);
		}

		#endregion
	}
}
