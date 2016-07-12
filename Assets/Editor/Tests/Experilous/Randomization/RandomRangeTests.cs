/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Randomization.Tests
{
	class RandomRangeTests
	{
		private const string seed = "random seed";

		public static void ValidateOpenRange(int count, int max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Open(max, engine);
				Assert.Greater(random, 0);
				Assert.Less(random, max);
			}
		}

		public static void ValidateOpenRange(int count, uint max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Open(max, engine);
				Assert.Greater(random, 0U);
				Assert.Less(random, max);
			}
		}

		public static void ValidateOpenRange(int count, long max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Open(max, engine);
				Assert.Greater(random, 0L);
				Assert.Less(random, max);
			}
		}

		public static void ValidateOpenRange(int count, ulong max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Open(max, engine);
				Assert.Greater(random, 0UL);
				Assert.Less(random, max);
			}
		}

		public static void ValidateOpenRange(int count, int min, int max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Open(min, max, engine);
				Assert.Greater(random, min);
				Assert.Less(random, max);
			}
		}

		public static void ValidateOpenRange(int count, uint min, uint max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Open(min, max, engine);
				Assert.Greater(random, min);
				Assert.Less(random, max);
			}
		}

		public static void ValidateOpenRange(int count, long min, long max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Open(min, max, engine);
				Assert.Greater(random, min);
				Assert.Less(random, max);
			}
		}

		public static void ValidateOpenRange(int count, ulong min, ulong max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Open(min, max, engine);
				Assert.Greater(random, min);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfOpenRange(int count, int max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfOpen(max, engine);
				Assert.GreaterOrEqual(random, 0);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfOpenRange(int count, uint max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfOpen(max, engine);
				Assert.GreaterOrEqual(random, 0U);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfOpenRange(int count, long max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfOpen(max, engine);
				Assert.GreaterOrEqual(random, 0L);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfOpenRange(int count, ulong max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfOpen(max, engine);
				Assert.GreaterOrEqual(random, 0UL);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfOpenRange(int count, int min, int max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfOpen(min, max, engine);
				Assert.GreaterOrEqual(random, min);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfOpenRange(int count, uint min, uint max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfOpen(min, max, engine);
				Assert.GreaterOrEqual(random, min);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfOpenRange(int count, long min, long max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfOpen(min, max, engine);
				Assert.GreaterOrEqual(random, min);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfOpenRange(int count, ulong min, ulong max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfOpen(min, max, engine);
				Assert.GreaterOrEqual(random, min);
				Assert.Less(random, max);
			}
		}

		public static void ValidateHalfClosedRange(int count, int max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfClosed(max, engine);
				Assert.Greater(random, 0);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateHalfClosedRange(int count, uint max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfClosed(max, engine);
				Assert.Greater(random, 0U);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateHalfClosedRange(int count, long max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfClosed(max, engine);
				Assert.Greater(random, 0L);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateHalfClosedRange(int count, ulong max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfClosed(max, engine);
				Assert.Greater(random, 0UL);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateHalfClosedRange(int count, int min, int max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfClosed(min, max, engine);
				Assert.Greater(random, min);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateHalfClosedRange(int count, uint min, uint max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfClosed(min, max, engine);
				Assert.Greater(random, min);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateHalfClosedRange(int count, long min, long max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfClosed(min, max, engine);
				Assert.Greater(random, min);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateHalfClosedRange(int count, ulong min, ulong max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.HalfClosed(min, max, engine);
				Assert.Greater(random, min);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateClosedRange(int count, int max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Closed(max, engine);
				Assert.GreaterOrEqual(random, 0);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateClosedRange(int count, uint max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Closed(max, engine);
				Assert.GreaterOrEqual(random, 0U);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateClosedRange(int count, long max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Closed(max, engine);
				Assert.GreaterOrEqual(random, 0L);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateClosedRange(int count, ulong max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Closed(max, engine);
				Assert.GreaterOrEqual(random, 0UL);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateClosedRange(int count, int min, int max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Closed(min, max, engine);
				Assert.GreaterOrEqual(random, min);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateClosedRange(int count, uint min, uint max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Closed(min, max, engine);
				Assert.GreaterOrEqual(random, min);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateClosedRange(int count, long min, long max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Closed(min, max, engine);
				Assert.GreaterOrEqual(random, min);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateClosedRange(int count, ulong min, ulong max, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomRange.Closed(min, max, engine);
				Assert.GreaterOrEqual(random, min);
				Assert.LessOrEqual(random, max);
			}
		}

		public static void ValidateOpenBucketDistribution(int max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - 1);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Open(max, engine);
				buckets[(uint)System.Math.Floor((random - 1) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(int min, int max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min - 1);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Open(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min - 1) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(uint max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - 1U);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Open(max, engine);
				buckets[(uint)System.Math.Floor((random - 1U) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(uint min, uint max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min - 1U);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Open(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min - 1U) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(long max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - 1L);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Open(max, engine);
				buckets[(uint)System.Math.Floor((random - 1L) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(long min, long max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min - 1L);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Open(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min - 1L) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(ulong max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - 1UL);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Open(max, engine);
				buckets[(uint)System.Math.Floor((random - 1UL) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenBucketDistribution(ulong min, ulong max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min - 1UL);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Open(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min - 1UL) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(int max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfOpen(max, engine);
				buckets[(uint)System.Math.Floor((random) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(int min, int max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfOpen(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(uint max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfOpen(max, engine);
				buckets[(uint)System.Math.Floor((random) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(uint min, uint max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfOpen(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(long max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfOpen(max, engine);
				buckets[(uint)System.Math.Floor((random) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(long min, long max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfOpen(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(ulong max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfOpen(max, engine);
				buckets[(uint)System.Math.Floor((random) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenBucketDistribution(ulong min, ulong max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfOpen(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(int max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfClosed(max, engine);
				buckets[(uint)System.Math.Floor((random - 1) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(int min, int max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfClosed(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min - 1) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(uint max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfClosed(max, engine);
				buckets[(uint)System.Math.Floor((random - 1U) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(uint min, uint max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfClosed(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min - 1U) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(long max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfClosed(max, engine);
				buckets[(uint)System.Math.Floor((random - 1L) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(long min, long max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfClosed(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min - 1L) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(ulong max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfClosed(max, engine);
				buckets[(uint)System.Math.Floor((random - 1UL) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedBucketDistribution(ulong min, ulong max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.HalfClosed(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min - 1UL) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(int max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max + 1);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Closed(max, engine);
				buckets[(uint)System.Math.Floor((random) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(int min, int max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min + 1);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Closed(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(uint max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max + 1U);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Closed(max, engine);
				buckets[(uint)System.Math.Floor((random) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(uint min, uint max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min + 1U);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Closed(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(long max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max + 1L);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Closed(max, engine);
				buckets[(uint)System.Math.Floor((random) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(long min, long max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min + 1L);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Closed(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(ulong max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max + 1UL);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Closed(max, engine);
				buckets[(uint)System.Math.Floor((random) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedBucketDistribution(ulong min, ulong max, IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			var scale = (double)bucketCount / (max - min + 1UL);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomRange.Closed(min, max, engine);
				buckets[(uint)System.Math.Floor((random - min) * scale)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		[Test]
		public void OpenInt32Range()
		{
			ValidateOpenRange(10000, 257, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 257, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 257, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 257, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 257, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 127, 384, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 127, 384, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 127, 384, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 127, 384, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 127, 384, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 43, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 43, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 43, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 43, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 43, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 140, 183, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 140, 183, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 140, 183, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 140, 183, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 140, 183, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenUInt32Range()
		{
			ValidateOpenRange(10000, 257U, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 257U, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 257U, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 257U, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 257U, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 127U, 384U, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 127U, 384U, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 127U, 384U, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 127U, 384U, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 127U, 384U, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 43U, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 43U, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 43U, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 43U, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 43U, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 140U, 183U, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 140U, 183U, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 140U, 183U, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 140U, 183U, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 140U, 183U, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenInt64Range()
		{
			ValidateOpenRange(10000, 257L, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 257L, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 257L, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 257L, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 257L, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 127L, 384L, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 127L, 384L, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 127L, 384L, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 127L, 384L, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 127L, 384L, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 2340982340892342L, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 2340982340892342L, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 2340982340892342L, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 2340982340892342L, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 2340982340892342L, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 43L, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 43L, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 43L, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 43L, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 43L, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 140L, 183L, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 140L, 183L, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 140L, 183L, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 140L, 183L, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 140L, 183L, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenUInt64Range()
		{
			ValidateOpenRange(10000, 257UL, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 257UL, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 257UL, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 257UL, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 257UL, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 127UL, 384UL, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 127UL, 384UL, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 127UL, 384UL, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 127UL, 384UL, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 127UL, 384UL, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 2340982340892342UL, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 2340982340892342UL, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 2340982340892342UL, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 2340982340892342UL, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 2340982340892342UL, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 43UL, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 43UL, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 43UL, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 43UL, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 43UL, XorShift1024Star.Create(seed));

			ValidateOpenRange(10000, 140UL, 183UL, SystemRandomEngine.Create(seed));
			ValidateOpenRange(10000, 140UL, 183UL, SplitMix64.Create(seed));
			ValidateOpenRange(10000, 140UL, 183UL, XorShift128Plus.Create(seed));
			ValidateOpenRange(10000, 140UL, 183UL, XoroShiro128Plus.Create(seed));
			ValidateOpenRange(10000, 140UL, 183UL, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenInt32Range()
		{
			ValidateHalfOpenRange(10000, 256, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 256, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 256, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 256, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 256, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 128, 384, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 128, 384, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 128, 384, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 128, 384, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 128, 384, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 43, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 43, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 43, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 43, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 43, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 140, 183, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 140, 183, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 140, 183, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 140, 183, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 140, 183, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenUInt32Range()
		{
			ValidateHalfOpenRange(10000, 256U, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 256U, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 256U, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 256U, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 256U, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 128U, 384U, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 128U, 384U, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 128U, 384U, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 128U, 384U, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 128U, 384U, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 43U, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 43U, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 43U, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 43U, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 43U, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 140U, 183U, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 140U, 183U, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 140U, 183U, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 140U, 183U, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 140U, 183U, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenInt64Range()
		{
			ValidateHalfOpenRange(10000, 256L, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 256L, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 256L, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 256L, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 256L, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 128L, 384L, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 128L, 384L, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 128L, 384L, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 128L, 384L, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 128L, 384L, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 2340982340892342L, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 2340982340892342L, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 2340982340892342L, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 2340982340892342L, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 2340982340892342L, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 43L, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 43L, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 43L, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 43L, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 43L, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 140L, 183L, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 140L, 183L, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 140L, 183L, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 140L, 183L, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 140L, 183L, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenUInt64Range()
		{
			ValidateHalfOpenRange(10000, 256UL, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 256UL, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 256UL, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 256UL, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 256UL, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 128UL, 384UL, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 128UL, 384UL, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 128UL, 384UL, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 128UL, 384UL, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 128UL, 384UL, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 2340982340892342UL, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 2340982340892342UL, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 2340982340892342UL, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 2340982340892342UL, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 2340982340892342UL, XorShift1024Star.Create(seed));
			ValidateHalfOpenRange(10000, 43UL, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 43UL, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 43UL, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 43UL, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 43UL, XorShift1024Star.Create(seed));

			ValidateHalfOpenRange(10000, 140UL, 183UL, SystemRandomEngine.Create(seed));
			ValidateHalfOpenRange(10000, 140UL, 183UL, SplitMix64.Create(seed));
			ValidateHalfOpenRange(10000, 140UL, 183UL, XorShift128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 140UL, 183UL, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenRange(10000, 140UL, 183UL, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedInt32Range()
		{
			ValidateHalfClosedRange(10000, 256, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 256, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 256, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 256, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 256, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 128, 384, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 128, 384, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 128, 384, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 128, 384, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 128, 384, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 43, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 43, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 43, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 43, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 43, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 140, 183, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 140, 183, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 140, 183, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 140, 183, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 140, 183, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedUInt32Range()
		{
			ValidateHalfClosedRange(10000, 256U, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 256U, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 256U, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 256U, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 256U, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 128U, 384U, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 128U, 384U, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 128U, 384U, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 128U, 384U, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 128U, 384U, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 43U, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 43U, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 43U, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 43U, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 43U, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 140U, 183U, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 140U, 183U, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 140U, 183U, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 140U, 183U, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 140U, 183U, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedInt64Range()
		{
			ValidateHalfClosedRange(10000, 256L, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 256L, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 256L, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 256L, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 256L, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 128L, 384L, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 128L, 384L, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 128L, 384L, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 128L, 384L, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 128L, 384L, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 2340982340892342L, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 2340982340892342L, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 2340982340892342L, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 2340982340892342L, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 2340982340892342L, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 43L, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 43L, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 43L, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 43L, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 43L, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 140L, 183L, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 140L, 183L, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 140L, 183L, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 140L, 183L, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 140L, 183L, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedUInt64Range()
		{
			ValidateHalfClosedRange(10000, 256UL, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 256UL, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 256UL, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 256UL, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 256UL, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 128UL, 384UL, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 128UL, 384UL, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 128UL, 384UL, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 128UL, 384UL, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 128UL, 384UL, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 2340982340892342UL, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 2340982340892342UL, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 2340982340892342UL, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 2340982340892342UL, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 2340982340892342UL, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 43UL, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 43UL, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 43UL, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 43UL, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 43UL, XorShift1024Star.Create(seed));

			ValidateHalfClosedRange(10000, 140UL, 183UL, SystemRandomEngine.Create(seed));
			ValidateHalfClosedRange(10000, 140UL, 183UL, SplitMix64.Create(seed));
			ValidateHalfClosedRange(10000, 140UL, 183UL, XorShift128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 140UL, 183UL, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedRange(10000, 140UL, 183UL, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedInt32Range()
		{
			ValidateClosedRange(10000, 255, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 255, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 255, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 255, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 255, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 128, 383, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 128, 383, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 128, 383, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 128, 383, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 128, 383, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 43, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 43, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 43, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 43, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 43, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 140, 183, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 140, 183, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 140, 183, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 140, 183, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 140, 183, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedUInt32Range()
		{
			ValidateClosedRange(10000, 255U, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 255U, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 255U, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 255U, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 255U, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 128U, 383U, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 128U, 383U, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 128U, 383U, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 128U, 383U, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 128U, 383U, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 43U, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 43U, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 43U, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 43U, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 43U, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 140U, 183U, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 140U, 183U, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 140U, 183U, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 140U, 183U, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 140U, 183U, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedInt64Range()
		{
			ValidateClosedRange(10000, 255L, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 255L, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 255L, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 255L, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 255L, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 128L, 383L, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 128L, 383L, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 128L, 383L, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 128L, 383L, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 128L, 383L, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 2340982340892342L, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 2340982340892342L, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 2340982340892342L, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 2340982340892342L, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 2340982340892342L, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 43L, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 43L, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 43L, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 43L, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 43L, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 140L, 183L, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 140L, 183L, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 140L, 183L, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 140L, 183L, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 140L, 183L, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedUInt64Range()
		{
			ValidateClosedRange(10000, 255UL, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 255UL, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 255UL, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 255UL, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 255UL, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 128UL, 383UL, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 128UL, 383UL, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 128UL, 383UL, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 128UL, 383UL, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 128UL, 383UL, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 2340982340892342UL, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 2340982340892342UL, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 2340982340892342UL, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 2340982340892342UL, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 2340982340892342UL, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 43UL, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 43UL, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 43UL, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 43UL, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 43UL, XorShift1024Star.Create(seed));

			ValidateClosedRange(10000, 140UL, 183UL, SystemRandomEngine.Create(seed));
			ValidateClosedRange(10000, 140UL, 183UL, SplitMix64.Create(seed));
			ValidateClosedRange(10000, 140UL, 183UL, XorShift128Plus.Create(seed));
			ValidateClosedRange(10000, 140UL, 183UL, XoroShiro128Plus.Create(seed));
			ValidateClosedRange(10000, 140UL, 183UL, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenInt32TenBucketDistribution()
		{
			ValidateOpenBucketDistribution(11, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(100, 111, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100, 111, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100, 111, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100, 111, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100, 111, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(2841, 8394327, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(16777217, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenInt32ThousandBucketDistribution()
		{
			ValidateOpenBucketDistribution(2841, 8394327, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateOpenBucketDistribution(16777217, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenUInt32TenBucketDistribution()
		{
			ValidateOpenBucketDistribution(11U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(100U, 111U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100U, 111U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100U, 111U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100U, 111U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100U, 111U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(2841U, 8394327U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(16777217U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenUInt32ThousandBucketDistribution()
		{
			ValidateOpenBucketDistribution(2841U, 8394327U, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateOpenBucketDistribution(16777217U, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217U, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217U, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217U, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenInt64TenBucketDistribution()
		{
			ValidateOpenBucketDistribution(11L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(100L, 111L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100L, 111L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100L, 111L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100L, 111L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100L, 111L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(2841L, 8394327L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(16777217L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(9823498734502L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(9823498734502L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenInt64ThousandBucketDistribution()
		{
			ValidateOpenBucketDistribution(2841L, 8394327L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateOpenBucketDistribution(16777217L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateOpenBucketDistribution(9823498734502L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(9823498734502L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenUInt64TenBucketDistribution()
		{
			ValidateOpenBucketDistribution(11UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(11UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(100UL, 111UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100UL, 111UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100UL, 111UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100UL, 111UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(100UL, 111UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(2841UL, 8394327UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(16777217UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(16777217UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateOpenBucketDistribution(9823498734502UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenUInt64ThousandBucketDistribution()
		{
			ValidateOpenBucketDistribution(2841UL, 8394327UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateOpenBucketDistribution(16777217UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(16777217UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateOpenBucketDistribution(9823498734502UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenInt32TenBucketDistribution()
		{
			ValidateHalfOpenBucketDistribution(10, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(100, 110, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100, 110, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100, 110, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100, 110, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100, 110, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(2841, 8394327, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(16777216, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenInt32ThousandBucketDistribution()
		{
			ValidateHalfOpenBucketDistribution(2841, 8394327, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfOpenBucketDistribution(16777216, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenUInt32TenBucketDistribution()
		{
			ValidateHalfOpenBucketDistribution(10U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(100U, 110U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100U, 110U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100U, 110U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100U, 110U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100U, 110U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(2841U, 8394327U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(16777216U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenUInt32ThousandBucketDistribution()
		{
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfOpenBucketDistribution(16777216U, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216U, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216U, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216U, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenInt64TenBucketDistribution()
		{
			ValidateHalfOpenBucketDistribution(10L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(100L, 110L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100L, 110L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100L, 110L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100L, 110L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100L, 110L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(2841L, 8394327L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(16777216L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(9823498734502L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(9823498734502L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenInt64ThousandBucketDistribution()
		{
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfOpenBucketDistribution(16777216L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfOpenBucketDistribution(9823498734502L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(9823498734502L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenUInt64TenBucketDistribution()
		{
			ValidateHalfOpenBucketDistribution(10UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(10UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(100UL, 110UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100UL, 110UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100UL, 110UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100UL, 110UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(100UL, 110UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(16777216UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(16777216UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfOpenBucketDistribution(9823498734502UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenUInt64ThousandBucketDistribution()
		{
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfOpenBucketDistribution(16777216UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(16777216UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfOpenBucketDistribution(9823498734502UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfClosedInt32TenBucketDistribution()
		{
			ValidateHalfClosedBucketDistribution(10, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(100, 110, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100, 110, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100, 110, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100, 110, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100, 110, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(2841, 8394327, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(16777216, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfClosedInt32ThousandBucketDistribution()
		{
			ValidateHalfClosedBucketDistribution(2841, 8394327, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfClosedBucketDistribution(16777216, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfClosedUInt32TenBucketDistribution()
		{
			ValidateHalfClosedBucketDistribution(10U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(100U, 110U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100U, 110U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100U, 110U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100U, 110U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100U, 110U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(2841U, 8394327U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(16777216U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfClosedUInt32ThousandBucketDistribution()
		{
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfClosedBucketDistribution(16777216U, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216U, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216U, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216U, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfClosedInt64TenBucketDistribution()
		{
			ValidateHalfClosedBucketDistribution(10L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(100L, 110L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100L, 110L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100L, 110L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100L, 110L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100L, 110L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(2841L, 8394327L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(16777216L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(9823498734502L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(9823498734502L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfClosedInt64ThousandBucketDistribution()
		{
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfClosedBucketDistribution(16777216L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfClosedBucketDistribution(9823498734502L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(9823498734502L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfClosedUInt64TenBucketDistribution()
		{
			ValidateHalfClosedBucketDistribution(10UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(10UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(100UL, 110UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100UL, 110UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100UL, 110UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100UL, 110UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(100UL, 110UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(16777216UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(16777216UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateHalfClosedBucketDistribution(9823498734502UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfClosedUInt64ThousandBucketDistribution()
		{
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfClosedBucketDistribution(16777216UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(16777216UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateHalfClosedBucketDistribution(9823498734502UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedInt32TenBucketDistribution()
		{
			ValidateClosedBucketDistribution(9, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(100, 109, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100, 109, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100, 109, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100, 109, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100, 109, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(2841, 8394327, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(16777215, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedInt32ThousandBucketDistribution()
		{
			ValidateClosedBucketDistribution(2841, 8394327, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841, 8394327, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841, 8394327, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841, 8394327, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841, 8394327, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateClosedBucketDistribution(16777215, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedUInt32TenBucketDistribution()
		{
			ValidateClosedBucketDistribution(9U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(100U, 109U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100U, 109U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100U, 109U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100U, 109U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100U, 109U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(2841U, 8394327U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(16777215U, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215U, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215U, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215U, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215U, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedUInt32ThousandBucketDistribution()
		{
			ValidateClosedBucketDistribution(2841U, 8394327U, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841U, 8394327U, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841U, 8394327U, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841U, 8394327U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841U, 8394327U, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateClosedBucketDistribution(16777215U, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215U, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215U, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215U, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215U, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedInt64TenBucketDistribution()
		{
			ValidateClosedBucketDistribution(9L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(100L, 109L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100L, 109L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100L, 109L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100L, 109L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100L, 109L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(2841L, 8394327L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(16777215L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(9823498734502L, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9823498734502L, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedInt64ThousandBucketDistribution()
		{
			ValidateClosedBucketDistribution(2841L, 8394327L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841L, 8394327L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841L, 8394327L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841L, 8394327L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841L, 8394327L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateClosedBucketDistribution(16777215L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateClosedBucketDistribution(9823498734502L, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(9823498734502L, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(9823498734502L, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(9823498734502L, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(9823498734502L, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedUInt64TenBucketDistribution()
		{
			ValidateClosedBucketDistribution(9UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(100UL, 109UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100UL, 109UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100UL, 109UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100UL, 109UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(100UL, 109UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(2841UL, 8394327UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(16777215UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(16777215UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);

			ValidateClosedBucketDistribution(9823498734502UL, SystemRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedUInt64ThousandBucketDistribution()
		{
			ValidateClosedBucketDistribution(2841UL, 8394327UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(2841UL, 8394327UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateClosedBucketDistribution(16777215UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(16777215UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);

			ValidateClosedBucketDistribution(9823498734502UL, SystemRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(9823498734502UL, SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(9823498734502UL, XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(9823498734502UL, XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedBucketDistribution(9823498734502UL, XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}
	}
}
#endif
