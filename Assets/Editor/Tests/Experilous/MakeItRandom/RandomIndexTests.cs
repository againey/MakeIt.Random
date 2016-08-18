/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using NUnit.Framework;

namespace Experilous.MakeItRandom.Tests
{
	class RandomIndexTests
	{
		private const string seed = "random seed";

		private static void ValidateWeighted(int bucketCount, int itemsPerBucket, int weightMin, int weightMax, float tolerance, IRandom random)
		{
			var weights = new int[bucketCount];
			int weightSum = 0;
			var buckets = new int[bucketCount];

			for (int i = 0; i < bucketCount; ++i)
			{
				int weight = random.RangeCC(weightMin, weightMax);
				weights[i] = weight;
				weightSum += weight;
			}

			int iterations = 0;
			for (int i = 0; i < bucketCount; ++i)
			{
				buckets[i] = itemsPerBucket * bucketCount * weights[i] / weightSum;
				iterations += buckets[i];
			}

			for (int i = 0; i < iterations; ++i)
			{
				--buckets[random.WeightedIndex(weights, weightSum)];
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, 0), tolerance * itemsPerBucket);
		}

		[Test]
		public void EquallyWeighted_TwoBuckets()
		{
			ValidateWeighted(2, 5000, 3, 3, 0.05f, SystemRandom.Create(seed));
			ValidateWeighted(2, 5000, 3, 3, 0.05f, SplitMix64.Create(seed));
			ValidateWeighted(2, 5000, 3, 3, 0.05f, XorShift128Plus.Create(seed));
			ValidateWeighted(2, 5000, 3, 3, 0.05f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(2, 5000, 3, 3, 0.05f, XorShift1024Star.Create(seed));
		}

		[Test]
		public void EquallyWeighted_HundredBuckets()
		{
			ValidateWeighted(100, 100, 3, 3, 0.2f, SystemRandom.Create(seed));
			ValidateWeighted(100, 100, 3, 3, 0.2f, SplitMix64.Create(seed));
			ValidateWeighted(100, 100, 3, 3, 0.2f, XorShift128Plus.Create(seed));
			ValidateWeighted(100, 100, 3, 3, 0.2f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(100, 100, 3, 3, 0.2f, XorShift1024Star.Create(seed));
		}

		[Test]
		public void EquallyWeighted_ThousandBuckets()
		{
			ValidateWeighted(1000, 10, 3, 3, 0.4f, SystemRandom.Create(seed));
			ValidateWeighted(1000, 10, 3, 3, 0.4f, SplitMix64.Create(seed));
			ValidateWeighted(1000, 10, 3, 3, 0.4f, XorShift128Plus.Create(seed));
			ValidateWeighted(1000, 10, 3, 3, 0.4f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(1000, 10, 3, 3, 0.4f, XorShift1024Star.Create(seed));
		}

		[Test]
		public void AlmostEquallyWeighted_TwoBuckets()
		{
			ValidateWeighted(2, 5000, 99, 101, 0.05f, SystemRandom.Create(seed));
			ValidateWeighted(2, 5000, 99, 101, 0.05f, SplitMix64.Create(seed));
			ValidateWeighted(2, 5000, 99, 101, 0.05f, XorShift128Plus.Create(seed));
			ValidateWeighted(2, 5000, 99, 101, 0.05f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(2, 5000, 99, 101, 0.05f, XorShift1024Star.Create(seed));
		}

		[Test]
		public void AlmostEquallyWeighted_HundredBuckets()
		{
			ValidateWeighted(100, 100, 99, 101, 0.2f, SystemRandom.Create(seed));
			ValidateWeighted(100, 100, 99, 101, 0.2f, SplitMix64.Create(seed));
			ValidateWeighted(100, 100, 99, 101, 0.2f, XorShift128Plus.Create(seed));
			ValidateWeighted(100, 100, 99, 101, 0.2f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(100, 100, 99, 101, 0.2f, XorShift1024Star.Create(seed));
		}

		[Test]
		public void AlmostEquallyWeighted_ThousandBuckets()
		{
			ValidateWeighted(1000, 10, 99, 101, 0.4f, SystemRandom.Create(seed));
			ValidateWeighted(1000, 10, 99, 101, 0.4f, SplitMix64.Create(seed));
			ValidateWeighted(1000, 10, 99, 101, 0.4f, XorShift128Plus.Create(seed));
			ValidateWeighted(1000, 10, 99, 101, 0.4f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(1000, 10, 99, 101, 0.4f, XorShift1024Star.Create(seed));
		}

		[Test]
		public void RandomWeighted_TwoBuckets()
		{
			ValidateWeighted(2, 5000, 1, 10, 0.02f, SystemRandom.Create(seed));
			ValidateWeighted(2, 5000, 1, 10, 0.02f, SplitMix64.Create(seed));
			ValidateWeighted(2, 5000, 1, 10, 0.02f, XorShift128Plus.Create(seed));
			ValidateWeighted(2, 5000, 1, 10, 0.02f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(2, 5000, 1, 10, 0.02f, XorShift1024Star.Create(seed));
		}

		[Test]
		public void RandomWeighted_HundredBuckets()
		{
			ValidateWeighted(100, 100, 1, 10, 0.2f, SystemRandom.Create(seed));
			ValidateWeighted(100, 100, 1, 10, 0.2f, SplitMix64.Create(seed));
			ValidateWeighted(100, 100, 1, 10, 0.2f, XorShift128Plus.Create(seed));
			ValidateWeighted(100, 100, 1, 10, 0.2f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(100, 100, 1, 10, 0.2f, XorShift1024Star.Create(seed));
		}

		[Test]
		public void RandomWeighted_ThousandBuckets()
		{
			ValidateWeighted(1000, 10, 1, 10, 0.4f, SystemRandom.Create(seed));
			ValidateWeighted(1000, 10, 1, 10, 0.4f, SplitMix64.Create(seed));
			ValidateWeighted(1000, 10, 1, 10, 0.4f, XorShift128Plus.Create(seed));
			ValidateWeighted(1000, 10, 1, 10, 0.4f, XoroShiro128Plus.Create(seed));
			ValidateWeighted(1000, 10, 1, 10, 0.4f, XorShift1024Star.Create(seed));
		}
	}
}
#endif
