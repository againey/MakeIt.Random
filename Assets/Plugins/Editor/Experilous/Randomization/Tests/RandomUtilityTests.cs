/******************************************************************************\
 *  Copyright (C) 2016 Experilous <againey@experilous.com>
 *  
 *  This file is subject to the terms and conditions defined in the file
 *  'Assets/Plugins/Experilous/License.txt', which is a part of this package.
 *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Randomization.Tests
{
	class RandomUtilityTests
	{
		public static void ValidateHalfOpenFloatUnitRange(IRandomEngine engine)
		{
			for (int i = 0; i < 10000; ++i)
			{
				var random = RandomUtility.HalfOpenFloatUnit(engine);
				Assert.GreaterOrEqual(random, 0.0f);
				Assert.Less(random, 1.0f);
			}
		}

		public static void ValidateHalfOpenDoubleUnitRange(IRandomEngine engine)
		{
			for (int i = 0; i < 10000; ++i)
			{
				var random = RandomUtility.HalfOpenDoubleUnit(engine);
				Assert.GreaterOrEqual(random, 0.0);
				Assert.Less(random, 1.0);
			}
		}

		public static void ValidateClosedFloatUnitRange(IRandomEngine engine)
		{
			for (int i = 0; i < 10000; ++i)
			{
				var random = RandomUtility.ClosedFloatUnit(engine);
				Assert.GreaterOrEqual(random, 0.0f);
				Assert.LessOrEqual(random, 1.0f);
			}
		}

		public static void ValidateClosedDoubleUnitRange(IRandomEngine engine)
		{
			for (int i = 0; i < 10000; ++i)
			{
				var random = RandomUtility.ClosedDoubleUnit(engine);
				Assert.GreaterOrEqual(random, 0.0);
				Assert.LessOrEqual(random, 1.0);
			}
		}

		public static float CalculateStandardDeviation(int[] buckets, int hitsPerBucket)
		{
			float devianceSum = 0f;

			foreach (var bucket in buckets)
			{
				float difference = bucket - hitsPerBucket;
				devianceSum += difference * difference;
			}

			return Mathf.Sqrt(devianceSum / buckets.Length);
		}

		public static void ValidateHalfOpenFloatUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUtility.HalfOpenFloatUnit(engine);
				buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenDoubleUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUtility.HalfOpenDoubleUnit(engine);
				buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedFloatUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUtility.ClosedFloatUnit(engine);
				if (random != 1.0f) buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedDoubleUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUtility.ClosedDoubleUnit(engine);
				if (random != 1.0) buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		[Test]
		public void HalfOpenFloatUnitRange()
		{
			ValidateHalfOpenFloatUnitRange(NativeRandomEngine.Create(0));
			ValidateHalfOpenFloatUnitRange(SplitMix64.Create(0));
			ValidateHalfOpenFloatUnitRange(XorShift128Plus.Create(0));
		}

		[Test]
		public void HalfOpenDoubleUnitRange()
		{
			ValidateHalfOpenDoubleUnitRange(NativeRandomEngine.Create(0));
			ValidateHalfOpenDoubleUnitRange(SplitMix64.Create(0));
			ValidateHalfOpenDoubleUnitRange(XorShift128Plus.Create(0));
		}

		[Test]
		public void ClosedFloatUnitRange()
		{
			ValidateClosedFloatUnitRange(NativeRandomEngine.Create(0));
			ValidateClosedFloatUnitRange(SplitMix64.Create(0));
			ValidateClosedFloatUnitRange(XorShift128Plus.Create(0));
		}

		[Test]
		public void ClosedDoubleUnitRange()
		{
			ValidateClosedDoubleUnitRange(NativeRandomEngine.Create(0));
			ValidateClosedDoubleUnitRange(SplitMix64.Create(0));
			ValidateClosedDoubleUnitRange(XorShift128Plus.Create(0));
		}

		[Test]
		public void HalfOpenFloatUnitTenBucketDistribution()
		{
			ValidateHalfOpenFloatUnitBucketDistribution(NativeRandomEngine.Create(0), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(0), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(0), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenFloatUnitThousandBucketDistribution()
		{
			ValidateHalfOpenFloatUnitBucketDistribution(NativeRandomEngine.Create(0), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(0), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(0), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenDoubleUnitTenBucketDistribution()
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(NativeRandomEngine.Create(0), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(0), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(0), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenDoubleUnitThousandBucketDistribution()
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(NativeRandomEngine.Create(0), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(0), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(0), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedFloatUnitTenBucketDistribution()
		{
			ValidateClosedFloatUnitBucketDistribution(NativeRandomEngine.Create(0), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(0), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(0), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedFloatUnitThousandBucketDistribution()
		{
			ValidateClosedFloatUnitBucketDistribution(NativeRandomEngine.Create(0), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(0), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(0), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedDoubleUnitTenBucketDistribution()
		{
			ValidateClosedDoubleUnitBucketDistribution(NativeRandomEngine.Create(0), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(0), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(0), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedDoubleUnitThousandBucketDistribution()
		{
			ValidateClosedDoubleUnitBucketDistribution(NativeRandomEngine.Create(0), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(0), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(0), 1000, 100, 0.15f);
		}
	}
}
#endif
