/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;

namespace MakeIt.Random.Tests
{
	class RandomRangeTests
	{
		private const string seed = "random seed";

		public static void ValidateRangeOO(int count, int max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOO(max);
				Assert.Greater(n, 0);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeOO(int count, uint max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOO(max);
				Assert.Greater(n, 0U);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeOO(int count, long max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOO(max);
				Assert.Greater(n, 0L);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeOO(int count, ulong max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOO(max);
				Assert.Greater(n, 0UL);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeOO(int count, int min, int max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOO(min, max);
				Assert.Greater(n, min);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeOO(int count, uint min, uint max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOO(min, max);
				Assert.Greater(n, min);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeOO(int count, long min, long max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOO(min, max);
				Assert.Greater(n, min);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeOO(int count, ulong min, ulong max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOO(min, max);
				Assert.Greater(n, min);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeCO(int count, int max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCO(max);
				Assert.GreaterOrEqual(n, 0);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeCO(int count, uint max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCO(max);
				Assert.GreaterOrEqual(n, 0U);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeCO(int count, long max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCO(max);
				Assert.GreaterOrEqual(n, 0L);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeCO(int count, ulong max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCO(max);
				Assert.GreaterOrEqual(n, 0UL);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeCO(int count, int min, int max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCO(min, max);
				Assert.GreaterOrEqual(n, min);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeCO(int count, uint min, uint max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCO(min, max);
				Assert.GreaterOrEqual(n, min);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeCO(int count, long min, long max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCO(min, max);
				Assert.GreaterOrEqual(n, min);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeCO(int count, ulong min, ulong max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCO(min, max);
				Assert.GreaterOrEqual(n, min);
				Assert.Less(n, max);
			}
		}

		public static void ValidateRangeOC(int count, int max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOC(max);
				Assert.Greater(n, 0);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeOC(int count, uint max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOC(max);
				Assert.Greater(n, 0U);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeOC(int count, long max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOC(max);
				Assert.Greater(n, 0L);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeOC(int count, ulong max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOC(max);
				Assert.Greater(n, 0UL);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeOC(int count, int min, int max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOC(min, max);
				Assert.Greater(n, min);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeOC(int count, uint min, uint max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOC(min, max);
				Assert.Greater(n, min);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeOC(int count, long min, long max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOC(min, max);
				Assert.Greater(n, min);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeOC(int count, ulong min, ulong max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeOC(min, max);
				Assert.Greater(n, min);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeCC(int count, int max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCC(max);
				Assert.GreaterOrEqual(n, 0);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeCC(int count, uint max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCC(max);
				Assert.GreaterOrEqual(n, 0U);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeCC(int count, long max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCC(max);
				Assert.GreaterOrEqual(n, 0L);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeCC(int count, ulong max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCC(max);
				Assert.GreaterOrEqual(n, 0UL);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeCC(int count, int min, int max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCC(min, max);
				Assert.GreaterOrEqual(n, min);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeCC(int count, uint min, uint max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCC(min, max);
				Assert.GreaterOrEqual(n, min);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeCC(int count, long min, long max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCC(min, max);
				Assert.GreaterOrEqual(n, min);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateRangeCC(int count, ulong min, ulong max, IRandom random, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = random.RangeCC(min, max);
				Assert.GreaterOrEqual(n, min);
				Assert.LessOrEqual(n, max);
			}
		}

		public static void ValidateOpenBucketDistribution(int max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - 1);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOO(max);
				buckets[(uint)System.Math.Floor((n - 1) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(int min, int max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min - 1);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOO(min, max);
				buckets[(uint)System.Math.Floor((n - min - 1) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(uint max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - 1U);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOO(max);
				buckets[(uint)System.Math.Floor((n - 1U) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(uint min, uint max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min - 1U);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOO(min, max);
				buckets[(uint)System.Math.Floor((n - min - 1U) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(long max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - 1L);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOO(max);
				buckets[(uint)System.Math.Floor((n - 1L) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(long min, long max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min - 1L);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOO(min, max);
				buckets[(uint)System.Math.Floor((n - min - 1L) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(ulong max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - 1UL);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOO(max);
				buckets[(uint)System.Math.Floor((n - 1UL) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(ulong min, ulong max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min - 1UL);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOO(min, max);
				buckets[(uint)System.Math.Floor((n - min - 1UL) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(int max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCO(max);
				buckets[(uint)System.Math.Floor((n) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(int min, int max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCO(min, max);
				buckets[(uint)System.Math.Floor((n - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(uint max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCO(max);
				buckets[(uint)System.Math.Floor((n) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(uint min, uint max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCO(min, max);
				buckets[(uint)System.Math.Floor((n - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(long max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCO(max);
				buckets[(uint)System.Math.Floor((n) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(long min, long max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCO(min, max);
				buckets[(uint)System.Math.Floor((n - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(ulong max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCO(max);
				buckets[(uint)System.Math.Floor((n) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(ulong min, ulong max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCO(min, max);
				buckets[(uint)System.Math.Floor((n - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(int max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOC(max);
				buckets[(uint)System.Math.Floor((n - 1) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(int min, int max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOC(min, max);
				buckets[(uint)System.Math.Floor((n - min - 1) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(uint max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOC(max);
				buckets[(uint)System.Math.Floor((n - 1U) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(uint min, uint max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOC(min, max);
				buckets[(uint)System.Math.Floor((n - min - 1U) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(long max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOC(max);
				buckets[(uint)System.Math.Floor((n - 1L) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(long min, long max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOC(min, max);
				buckets[(uint)System.Math.Floor((n - min - 1L) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(ulong max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOC(max);
				buckets[(uint)System.Math.Floor((n - 1UL) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(ulong min, ulong max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeOC(min, max);
				buckets[(uint)System.Math.Floor((n - min - 1UL) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(int max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max + 1);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCC(max);
				buckets[(uint)System.Math.Floor((n) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(int min, int max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min + 1);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCC(min, max);
				buckets[(uint)System.Math.Floor((n - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(uint max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max + 1U);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCC(max);
				buckets[(uint)System.Math.Floor((n) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(uint min, uint max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min + 1U);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCC(min, max);
				buckets[(uint)System.Math.Floor((n - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(long max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max + 1L);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCC(max);
				buckets[(uint)System.Math.Floor((n) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(long min, long max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min + 1L);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCC(min, max);
				buckets[(uint)System.Math.Floor((n - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(ulong max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max + 1UL);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCC(max);
				buckets[(uint)System.Math.Floor((n) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(ulong min, ulong max, IRandom random, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min + 1UL);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.RangeCC(min, max);
				buckets[(uint)System.Math.Floor((n - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenInt32Range(int sampleSizePercentage)
		{
			ValidateRangeOO(10000, 257, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 127, 384, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127, 384, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127, 384, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127, 384, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127, 384, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 43, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 140, 183, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140, 183, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140, 183, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140, 183, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140, 183, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenUInt32Range(int sampleSizePercentage)
		{
			ValidateRangeOO(10000, 257U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 127U, 384U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127U, 384U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127U, 384U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127U, 384U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127U, 384U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 43U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 140U, 183U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140U, 183U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140U, 183U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140U, 183U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140U, 183U, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenInt64Range(int sampleSizePercentage)
		{
			ValidateRangeOO(10000, 257L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 127L, 384L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127L, 384L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127L, 384L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127L, 384L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127L, 384L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 2340982340892342L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 2340982340892342L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 2340982340892342L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 2340982340892342L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 2340982340892342L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 43L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 140L, 183L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140L, 183L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140L, 183L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140L, 183L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140L, 183L, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenUInt64Range(int sampleSizePercentage)
		{
			ValidateRangeOO(10000, 257UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 257UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 127UL, 384UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127UL, 384UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127UL, 384UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127UL, 384UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 127UL, 384UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 2340982340892342UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 2340982340892342UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 2340982340892342UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 2340982340892342UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 2340982340892342UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 43UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 43UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOO(10000, 140UL, 183UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140UL, 183UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140UL, 183UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140UL, 183UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOO(10000, 140UL, 183UL, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenInt32Range(int sampleSizePercentage)
		{
			ValidateRangeCO(10000, 256, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 128, 384, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128, 384, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128, 384, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128, 384, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128, 384, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 43, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 140, 183, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140, 183, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140, 183, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140, 183, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140, 183, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenUInt32Range(int sampleSizePercentage)
		{
			ValidateRangeCO(10000, 256U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 128U, 384U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128U, 384U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128U, 384U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128U, 384U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128U, 384U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 43U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 140U, 183U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140U, 183U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140U, 183U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140U, 183U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140U, 183U, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenInt64Range(int sampleSizePercentage)
		{
			ValidateRangeCO(10000, 256L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 128L, 384L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128L, 384L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128L, 384L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128L, 384L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128L, 384L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 2340982340892342L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 2340982340892342L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 2340982340892342L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 2340982340892342L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 2340982340892342L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 43L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 140L, 183L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140L, 183L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140L, 183L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140L, 183L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140L, 183L, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenUInt64Range(int sampleSizePercentage)
		{
			ValidateRangeCO(10000, 256UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 256UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 128UL, 384UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128UL, 384UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128UL, 384UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128UL, 384UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 128UL, 384UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 2340982340892342UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 2340982340892342UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 2340982340892342UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 2340982340892342UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 2340982340892342UL, XorShift1024Star.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 43UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCO(10000, 140UL, 183UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140UL, 183UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140UL, 183UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140UL, 183UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCO(10000, 140UL, 183UL, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedInt32Range(int sampleSizePercentage)
		{
			ValidateRangeOC(10000, 256, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 128, 384, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128, 384, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128, 384, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128, 384, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128, 384, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 43, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 140, 183, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140, 183, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140, 183, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140, 183, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140, 183, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedUInt32Range(int sampleSizePercentage)
		{
			ValidateRangeOC(10000, 256U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 128U, 384U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128U, 384U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128U, 384U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128U, 384U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128U, 384U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 43U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 140U, 183U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140U, 183U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140U, 183U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140U, 183U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140U, 183U, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedInt64Range(int sampleSizePercentage)
		{
			ValidateRangeOC(10000, 256L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 128L, 384L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128L, 384L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128L, 384L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128L, 384L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128L, 384L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 2340982340892342L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 2340982340892342L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 2340982340892342L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 2340982340892342L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 2340982340892342L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 43L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 140L, 183L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140L, 183L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140L, 183L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140L, 183L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140L, 183L, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedUInt64Range(int sampleSizePercentage)
		{
			ValidateRangeOC(10000, 256UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 256UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 128UL, 384UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128UL, 384UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128UL, 384UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128UL, 384UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 128UL, 384UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 2340982340892342UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 2340982340892342UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 2340982340892342UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 2340982340892342UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 2340982340892342UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 43UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 43UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeOC(10000, 140UL, 183UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140UL, 183UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140UL, 183UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140UL, 183UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeOC(10000, 140UL, 183UL, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedInt32Range(int sampleSizePercentage)
		{
			ValidateRangeCC(10000, 255, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 128, 383, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128, 383, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128, 383, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128, 383, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128, 383, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 43, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 140, 183, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140, 183, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140, 183, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140, 183, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140, 183, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedUInt32Range(int sampleSizePercentage)
		{
			ValidateRangeCC(10000, 255U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 128U, 383U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128U, 383U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128U, 383U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128U, 383U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128U, 383U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 43U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43U, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 140U, 183U, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140U, 183U, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140U, 183U, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140U, 183U, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140U, 183U, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedInt64Range(int sampleSizePercentage)
		{
			ValidateRangeCC(10000, 255L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 128L, 383L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128L, 383L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128L, 383L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128L, 383L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128L, 383L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 2340982340892342L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 2340982340892342L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 2340982340892342L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 2340982340892342L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 2340982340892342L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 43L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43L, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 140L, 183L, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140L, 183L, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140L, 183L, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140L, 183L, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140L, 183L, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedUInt64Range(int sampleSizePercentage)
		{
			ValidateRangeCC(10000, 255UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 255UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 128UL, 383UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128UL, 383UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128UL, 383UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128UL, 383UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 128UL, 383UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 2340982340892342UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 2340982340892342UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 2340982340892342UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 2340982340892342UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 2340982340892342UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 43UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 43UL, XorShift1024Star.Create(seed), sampleSizePercentage);

			ValidateRangeCC(10000, 140UL, 183UL, SystemRandom.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140UL, 183UL, SplitMix64.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140UL, 183UL, XorShift128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140UL, 183UL, XoroShiro128Plus.Create(seed), sampleSizePercentage);
			ValidateRangeCC(10000, 140UL, 183UL, XorShift1024Star.Create(seed), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenInt32TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenBucketDistribution(11, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(100, 111, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100, 111, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100, 111, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100, 111, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100, 111, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(2841, 8394327, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(16777217, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenInt32ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenBucketDistribution(2841, 8394327, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateOpenBucketDistribution(16777217, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenUInt32TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenBucketDistribution(11U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(100U, 111U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100U, 111U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100U, 111U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100U, 111U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100U, 111U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(2841U, 8394327U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(16777217U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenUInt32ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenBucketDistribution(2841U, 8394327U, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateOpenBucketDistribution(16777217U, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217U, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217U, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217U, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenInt64TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenBucketDistribution(11L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(100L, 111L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100L, 111L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100L, 111L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100L, 111L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100L, 111L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(2841L, 8394327L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(16777217L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(9823498734502L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenInt64ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenBucketDistribution(2841L, 8394327L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateOpenBucketDistribution(16777217L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateOpenBucketDistribution(9823498734502L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenUInt64TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenBucketDistribution(11UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(11UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(100UL, 111UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100UL, 111UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100UL, 111UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100UL, 111UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(100UL, 111UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(2841UL, 8394327UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(16777217UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateOpenBucketDistribution(9823498734502UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void OpenUInt64ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateOpenBucketDistribution(2841UL, 8394327UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateOpenBucketDistribution(16777217UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(16777217UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateOpenBucketDistribution(9823498734502UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateOpenBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenInt32TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenBucketDistribution(10, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(100, 110, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100, 110, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100, 110, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100, 110, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100, 110, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(2841, 8394327, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(16777216, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenInt32ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenBucketDistribution(2841, 8394327, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(16777216, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenUInt32TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenBucketDistribution(10U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(100U, 110U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100U, 110U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100U, 110U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100U, 110U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100U, 110U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(2841U, 8394327U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(16777216U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenUInt32ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(16777216U, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216U, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216U, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216U, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenInt64TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenBucketDistribution(10L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(100L, 110L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100L, 110L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100L, 110L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100L, 110L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100L, 110L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(2841L, 8394327L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(16777216L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(9823498734502L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenInt64ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(16777216L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(9823498734502L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenUInt64TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenBucketDistribution(10UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(10UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(100UL, 110UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100UL, 110UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100UL, 110UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100UL, 110UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(100UL, 110UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(16777216UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(9823498734502UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfOpenUInt64ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(16777216UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(16777216UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfOpenBucketDistribution(9823498734502UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedInt32TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedBucketDistribution(10, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(100, 110, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100, 110, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100, 110, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100, 110, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100, 110, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(2841, 8394327, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(16777216, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedInt32ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedBucketDistribution(2841, 8394327, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(16777216, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedUInt32TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedBucketDistribution(10U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(100U, 110U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100U, 110U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100U, 110U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100U, 110U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100U, 110U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(2841U, 8394327U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(16777216U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedUInt32ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(16777216U, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216U, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216U, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216U, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedInt64TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedBucketDistribution(10L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(100L, 110L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100L, 110L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100L, 110L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100L, 110L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100L, 110L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(2841L, 8394327L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(16777216L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(9823498734502L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedInt64ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(16777216L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(9823498734502L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedUInt64TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedBucketDistribution(10UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(10UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(100UL, 110UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100UL, 110UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100UL, 110UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100UL, 110UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(100UL, 110UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(16777216UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(9823498734502UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void HalfClosedUInt64ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(16777216UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(16777216UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateHalfClosedBucketDistribution(9823498734502UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedInt32TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedBucketDistribution(9, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(100, 109, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100, 109, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100, 109, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100, 109, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100, 109, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(2841, 8394327, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(16777215, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedInt32ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedBucketDistribution(2841, 8394327, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateClosedBucketDistribution(16777215, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedUInt32TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedBucketDistribution(9U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(100U, 109U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100U, 109U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100U, 109U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100U, 109U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100U, 109U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(2841U, 8394327U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(16777215U, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215U, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215U, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215U, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedUInt32ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedBucketDistribution(2841U, 8394327U, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateClosedBucketDistribution(16777215U, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215U, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215U, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215U, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedInt64TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedBucketDistribution(9L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(100L, 109L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100L, 109L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100L, 109L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100L, 109L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100L, 109L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(2841L, 8394327L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(16777215L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(9823498734502L, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502L, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedInt64ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedBucketDistribution(2841L, 8394327L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateClosedBucketDistribution(16777215L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateClosedBucketDistribution(9823498734502L, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502L, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedUInt64TenBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedBucketDistribution(9UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(100UL, 109UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100UL, 109UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100UL, 109UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100UL, 109UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(100UL, 109UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(2841UL, 8394327UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(16777215UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);

			ValidateClosedBucketDistribution(9823498734502UL, SystemRandom.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ClosedUInt64ThousandBucketDistribution(int sampleSizePercentage)
		{
			ValidateClosedBucketDistribution(2841UL, 8394327UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateClosedBucketDistribution(16777215UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(16777215UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);

			ValidateClosedBucketDistribution(9823498734502UL, SystemRandom.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
			ValidateClosedBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f, sampleSizePercentage);
		}
	}
}
#endif
