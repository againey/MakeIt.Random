/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public struct RandomRange
	{
		private IRandomEngine _random;

		public RandomRange(IRandomEngine random)
		{
			_random = random;
		}

		#region Open

		public int Open(int lowerExclusive, int upperExclusive)
		{
			return (int)HalfOpen((uint)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1;
		}

		public int Open(int upperExclusive)
		{
			return (int)HalfOpen((uint)(upperExclusive - 1)) + 1;
		}

		public uint Open(uint lowerExclusive, uint upperExclusive)
		{
			return HalfOpen(upperExclusive - lowerExclusive - 1U) + lowerExclusive + 1U;
		}

		public uint Open(uint upperExclusive)
		{
			return HalfOpen(upperExclusive - 1U) + 1U;
		}

		public long Open(long lowerExclusive, long upperExclusive)
		{
			return (long)HalfOpen((ulong)(upperExclusive - lowerExclusive - 1L)) + lowerExclusive + 1L;
		}

		public long Open(long upperExclusive)
		{
			return (long)HalfOpen((ulong)(upperExclusive - 1L)) + 1L;
		}

		public ulong Open(ulong lowerExclusive, ulong upperExclusive)
		{
			return HalfOpen(upperExclusive - lowerExclusive - 1UL) + lowerExclusive + 1UL;
		}

		public ulong Open(ulong upperExclusive)
		{
			return HalfOpen(upperExclusive - 1UL) + 1UL;
		}

		public float Open(float lowerExclusive, float upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * _random.Unit().OpenFloat() + lowerExclusive;
		}

		public float Open(float upperExclusive)
		{
			return upperExclusive * _random.Unit().OpenFloat();
		}

		public double Open(double lowerExclusive, double upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * _random.Unit().OpenDouble() + lowerExclusive;
		}

		public double Open(double upperExclusive)
		{
			return upperExclusive * _random.Unit().OpenDouble();
		}

		#endregion

		#region HalfOpen

		public int HalfOpen(int lowerInclusive, int upperExclusive)
		{
			return (int)HalfOpen((uint)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public int HalfOpen(int upperExclusive)
		{
			return (int)HalfOpen((uint)(upperExclusive));
		}

		public uint HalfOpen(uint lowerInclusive, uint upperExclusive)
		{
			return HalfOpen(upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public uint HalfOpen(uint upperExclusive)
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
				random = _random.Next32() & mask;
			}
			while (random >= upperExclusive);
			return random;
		}

		public long HalfOpen(long lowerInclusive, long upperExclusive)
		{
			return (long)HalfOpen((ulong)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		public long HalfOpen(long upperExclusive)
		{
			return (long)HalfOpen((ulong)(upperExclusive));
		}

		public ulong HalfOpen(ulong lowerInclusive, ulong upperExclusive)
		{
			return HalfOpen(upperExclusive - lowerInclusive) + lowerInclusive;
		}

		public ulong HalfOpen(ulong upperExclusive)
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
				random = _random.Next64() & mask;
			}
			while (random >= upperExclusive);
			return random;
		}

		public float HalfOpen(float lowerInclusive, float upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * _random.Unit().HalfOpenFloat() + lowerInclusive;
		}

		public float HalfOpen(float upperExclusive)
		{
			return upperExclusive * _random.Unit().HalfOpenFloat();
		}

		public double HalfOpen(double lowerInclusive, double upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * _random.Unit().HalfOpenDouble() + lowerInclusive;
		}

		public double HalfOpen(double upperExclusive)
		{
			return upperExclusive * _random.Unit().HalfOpenDouble();
		}

		#endregion

		#region HalfClosed

		public int HalfClosed(int lowerExclusive, int upperInclusive)
		{
			return (int)HalfOpen((uint)(upperInclusive - lowerExclusive)) + lowerExclusive + 1;
		}

		public int HalfClosed(int upperInclusive)
		{
			return (int)HalfOpen((uint)(upperInclusive)) + 1;
		}

		public uint HalfClosed(uint lowerExclusive, uint upperInclusive)
		{
			return HalfOpen(upperInclusive - lowerExclusive) + lowerExclusive + 1U;
		}

		public uint HalfClosed(uint upperInclusive)
		{
			return HalfOpen(upperInclusive) + 1U;
		}

		public long HalfClosed(long lowerExclusive, long upperInclusive)
		{
			return (long)HalfOpen((ulong)(upperInclusive - lowerExclusive)) + lowerExclusive + 1L;
		}

		public long HalfClosed(long upperInclusive)
		{
			return (long)HalfOpen((ulong)(upperInclusive)) + 1L;
		}

		public ulong HalfClosed(ulong lowerExclusive, ulong upperInclusive)
		{
			return HalfOpen(upperInclusive - lowerExclusive) + lowerExclusive + 1UL;
		}

		public ulong HalfClosed(ulong upperInclusive)
		{
			return HalfOpen(upperInclusive) + 1UL;
		}

		public float HalfClosed(float lowerExclusive, float upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * _random.Unit().HalfClosedFloat() + lowerExclusive;
		}

		public float HalfClosed(float upperInclusive)
		{
			return upperInclusive * _random.Unit().HalfClosedFloat();
		}

		public double HalfClosed(double lowerExclusive, double upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * _random.Unit().HalfClosedDouble() + lowerExclusive;
		}

		public double HalfClosed(double upperInclusive)
		{
			return upperInclusive * _random.Unit().HalfClosedDouble();
		}

		#endregion

		#region Closed

		public int Closed(int lowerInclusive, int upperInclusive)
		{
			return (int)Closed((uint)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public int Closed(int upperInclusive)
		{
			return (int)Closed((uint)(upperInclusive));
		}

		public uint Closed(uint lowerInclusive, uint upperInclusive)
		{
			return Closed(upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public uint Closed(uint upperInclusive)
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
				random = _random.Next32() & mask;
			}
			while (random > upperInclusive);
			return random;
		}

		public long Closed(long lowerInclusive, long upperInclusive)
		{
			return (long)Closed((ulong)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		public long Closed(long upperInclusive)
		{
			return (long)Closed((ulong)(upperInclusive));
		}

		public ulong Closed(ulong lowerInclusive, ulong upperInclusive)
		{
			return Closed(upperInclusive - lowerInclusive) + lowerInclusive;
		}

		public ulong Closed(ulong upperInclusive)
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
				random = _random.Next64() & mask;
			}
			while (random > upperInclusive);
			return random;
		}

		public float Closed(float lowerInclusive, float upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * _random.Unit().ClosedFloat() + lowerInclusive;
		}

		public float Closed(float upperInclusive)
		{
			return upperInclusive * _random.Unit().ClosedFloat();
		}

		public double Closed(double lowerInclusive, double upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * _random.Unit().ClosedDouble() + lowerInclusive;
		}

		public double Closed(double upperInclusive)
		{
			return upperInclusive * _random.Unit().ClosedDouble();
		}

		#endregion
	}

	public static class RandomRangeExtensions
	{
		public static RandomRange Range(this IRandomEngine random)
		{
			return new RandomRange(random);
		}
	}
}
