/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using NUnit.Framework;

namespace Experilous.MakeItRandom.Tests
{
	class RandomUnitTests
	{
		private const string seed = "random seed";

		public static void ValidateOpenFloatUnitRange(int count, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.FloatOO();
				Assert.Greater(n, 0.0f);
				Assert.Less(n, 1.0f);
			}
		}

		public static void ValidateOpenDoubleUnitRange(int count, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.DoubleOO();
				Assert.Greater(n, 0.0);
				Assert.Less(n, 1.0);
			}
		}

		public static void ValidateHalfOpenFloatUnitRange(int count, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.FloatCO();
				Assert.GreaterOrEqual(n, 0.0f);
				Assert.Less(n, 1.0f);
			}
		}

		public static void ValidateHalfOpenDoubleUnitRange(int count, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.DoubleCO();
				Assert.GreaterOrEqual(n, 0.0);
				Assert.Less(n, 1.0);
			}
		}

		public static void ValidateHalfClosedFloatUnitRange(int count, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.FloatOC();
				Assert.Greater(n, 0.0f);
				Assert.LessOrEqual(n, 1.0f);
			}
		}

		public static void ValidateHalfClosedDoubleUnitRange(int count, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.DoubleOC();
				Assert.Greater(n, 0.0);
				Assert.LessOrEqual(n, 1.0);
			}
		}

		public static void ValidateClosedFloatUnitRange(int count, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.FloatCC();
				Assert.GreaterOrEqual(n, 0.0f);
				Assert.LessOrEqual(n, 1.0f);
			}
		}

		public static void ValidateClosedDoubleUnitRange(int count, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.DoubleCC();
				Assert.GreaterOrEqual(n, 0.0);
				Assert.LessOrEqual(n, 1.0);
			}
		}

		public static void ValidateOpenFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.FloatOO();
				buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.DoubleOO();
				buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.FloatCO();
				buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.DoubleCO();
				buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.FloatOC();
				buckets[Mathf.CeilToInt(n * bucketCount) - 1] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.DoubleOC();
				buckets[(int)System.Math.Ceiling(n * bucketCount) - 1] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.FloatCC();
				if (n != 1.0f) buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.DoubleCC();
				if (n != 1.0) buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenFloatUnitRange(int sampleSizePercentage)
		{
			ValidateOpenFloatUnitRange(10000, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateOpenFloatUnitRange(10000, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateOpenFloatUnitRange(10000, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateOpenFloatUnitRange(10000, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateOpenFloatUnitRange(10000, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenDoubleUnitRange(int sampleSizePercentage)
		{
			ValidateOpenDoubleUnitRange(10000, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateOpenDoubleUnitRange(10000, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateOpenDoubleUnitRange(10000, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateOpenDoubleUnitRange(10000, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateOpenDoubleUnitRange(10000, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenFloatUnitRange(int sampleSizePercentage)
		{
			ValidateHalfOpenFloatUnitRange(10000, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateHalfOpenFloatUnitRange(10000, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateHalfOpenFloatUnitRange(10000, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateHalfOpenFloatUnitRange(10000, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateHalfOpenFloatUnitRange(10000, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenDoubleUnitRange(int sampleSizePercentage)
		{
			ValidateHalfOpenDoubleUnitRange(10000, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateHalfOpenDoubleUnitRange(10000, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateHalfOpenDoubleUnitRange(10000, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateHalfOpenDoubleUnitRange(10000, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateHalfOpenDoubleUnitRange(10000, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedFloatUnitRange(int sampleSizePercentage)
		{
			ValidateHalfClosedFloatUnitRange(10000, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateHalfClosedFloatUnitRange(10000, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateHalfClosedFloatUnitRange(10000, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateHalfClosedFloatUnitRange(10000, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateHalfClosedFloatUnitRange(10000, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedDoubleUnitRange(int sampleSizePercentage)
		{
			ValidateHalfClosedDoubleUnitRange(10000, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateHalfClosedDoubleUnitRange(10000, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateHalfClosedDoubleUnitRange(10000, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateHalfClosedDoubleUnitRange(10000, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateHalfClosedDoubleUnitRange(10000, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedFloatUnitRange(int sampleSizePercentage)
		{
			ValidateClosedFloatUnitRange(10000, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateClosedFloatUnitRange(10000, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateClosedFloatUnitRange(10000, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateClosedFloatUnitRange(10000, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateClosedFloatUnitRange(10000, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedDoubleUnitRange(int sampleSizePercentage)
		{
			ValidateClosedDoubleUnitRange(10000, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateClosedDoubleUnitRange(10000, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateClosedDoubleUnitRange(10000, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateClosedDoubleUnitRange(10000, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateClosedDoubleUnitRange(10000, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenFloatUnitTenBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenFloatUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenFloatUnitThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenFloatUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenDoubleUnitTenBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenDoubleUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.020f, sampleSizePercentage);
			ValidateOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenDoubleUnitThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenDoubleUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenFloatUnitTenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenFloatUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenFloatUnitThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenFloatUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenDoubleUnitTenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenDoubleUnitThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedFloatUnitTenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedFloatUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedFloatUnitThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedFloatUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedDoubleUnitTenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedDoubleUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedDoubleUnitThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedDoubleUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedFloatUnitTenBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedFloatUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedFloatUnitThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedFloatUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedDoubleUnitTenBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedDoubleUnitBucketDistribution(SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedDoubleUnitThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedDoubleUnitBucketDistribution(SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}
	}
}
#endif
