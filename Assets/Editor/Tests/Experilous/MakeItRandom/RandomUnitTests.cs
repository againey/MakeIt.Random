/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if (UNITY_64 || MAKEITRANDOM_64) && !MAKEITRANDOM_32
#define OPTIMIZE_FOR_64
#else
#define OPTIMIZE_FOR_32
#endif

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using NUnit.Framework;

namespace Experilous.MakeItRandom.Tests
{
	class RandomUnitTests
	{
		private const string seed = "random seed";

		public static void ValidateOpenFloatUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.FloatOO();
				Assert.Greater(n, 0.0f);
				Assert.Less(n, 1.0f);
			}
		}

		public static void ValidateOpenDoubleUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.DoubleOO();
				Assert.Greater(n, 0.0);
				Assert.Less(n, 1.0);
			}
		}

		public static void ValidateHalfOpenFloatUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.FloatCO();
				Assert.GreaterOrEqual(n, 0.0f);
				Assert.Less(n, 1.0f);
			}
		}

		public static void ValidateHalfOpenDoubleUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.DoubleCO();
				Assert.GreaterOrEqual(n, 0.0);
				Assert.Less(n, 1.0);
			}
		}

		public static void ValidateHalfClosedFloatUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.FloatOC();
				Assert.Greater(n, 0.0f);
				Assert.LessOrEqual(n, 1.0f);
			}
		}

		public static void ValidateHalfClosedDoubleUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.DoubleOC();
				Assert.Greater(n, 0.0);
				Assert.LessOrEqual(n, 1.0);
			}
		}

		public static void ValidateClosedFloatUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.FloatCC();
				Assert.GreaterOrEqual(n, 0.0f);
				Assert.LessOrEqual(n, 1.0f);
			}
		}

		public static void ValidateClosedDoubleUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.DoubleCC();
				Assert.GreaterOrEqual(n, 0.0);
				Assert.LessOrEqual(n, 1.0);
			}
		}

		public static void ValidateOpenFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.FloatOO();
				buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.DoubleOO();
				buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.FloatCO();
				buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.DoubleCO();
				buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.FloatOC();
				buckets[Mathf.CeilToInt(n * bucketCount) - 1] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.DoubleOC();
				buckets[(int)System.Math.Ceiling(n * bucketCount) - 1] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.FloatCC();
				if (n != 1.0f) buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.DoubleCC();
				if (n != 1.0) buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		[Test]
		public void OpenFloatUnitRange()
		{
			ValidateOpenFloatUnitRange(10000, SystemRandom.Create(seed));
			ValidateOpenFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateOpenFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateOpenFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenDoubleUnitRange()
		{
			ValidateOpenDoubleUnitRange(10000, SystemRandom.Create(seed));
			ValidateOpenDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenFloatUnitRange()
		{
			ValidateHalfOpenFloatUnitRange(10000, SystemRandom.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenDoubleUnitRange()
		{
			ValidateHalfOpenDoubleUnitRange(10000, SystemRandom.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedFloatUnitRange()
		{
			ValidateHalfClosedFloatUnitRange(10000, SystemRandom.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedDoubleUnitRange()
		{
			ValidateHalfClosedDoubleUnitRange(10000, SystemRandom.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedFloatUnitRange()
		{
			ValidateClosedFloatUnitRange(10000, SystemRandom.Create(seed));
			ValidateClosedFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateClosedFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateClosedFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedDoubleUnitRange()
		{
			ValidateClosedDoubleUnitRange(10000, SystemRandom.Create(seed));
			ValidateClosedDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateClosedDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateClosedDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenFloatUnitTenBucketDistribution()
		{
			ValidateOpenFloatUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenFloatUnitThousandBucketDistribution()
		{
			ValidateOpenFloatUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenDoubleUnitTenBucketDistribution()
		{
			ValidateOpenDoubleUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.020f);
			ValidateOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenDoubleUnitThousandBucketDistribution()
		{
			ValidateOpenDoubleUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenFloatUnitTenBucketDistribution()
		{
			ValidateHalfOpenFloatUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenFloatUnitThousandBucketDistribution()
		{
			ValidateHalfOpenFloatUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenDoubleUnitTenBucketDistribution()
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenDoubleUnitThousandBucketDistribution()
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfClosedFloatUnitTenBucketDistribution()
		{
			ValidateHalfClosedFloatUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfClosedFloatUnitThousandBucketDistribution()
		{
			ValidateHalfClosedFloatUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfClosedDoubleUnitTenBucketDistribution()
		{
			ValidateHalfClosedDoubleUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfClosedDoubleUnitThousandBucketDistribution()
		{
			ValidateHalfClosedDoubleUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedFloatUnitTenBucketDistribution()
		{
			ValidateClosedFloatUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedFloatUnitThousandBucketDistribution()
		{
			ValidateClosedFloatUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedDoubleUnitTenBucketDistribution()
		{
			ValidateClosedDoubleUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedDoubleUnitThousandBucketDistribution()
		{
			ValidateClosedDoubleUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}
	}
}
#endif
