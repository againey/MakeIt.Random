/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Experilous.MakeIt.Random.Tests
{
	class RandomUnitTests
	{
		private const string seed = "random seed";

		public static void ValidateOpenFloatUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.OpenFloatUnit();
				Assert.Greater(n, 0.0f);
				Assert.Less(n, 1.0f);
			}
		}

		public static void ValidateOpenDoubleUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.OpenDoubleUnit();
				Assert.Greater(n, 0.0);
				Assert.Less(n, 1.0);
			}
		}

		public static void ValidateHalfOpenFloatUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.HalfOpenFloatUnit();
				Assert.GreaterOrEqual(n, 0.0f);
				Assert.Less(n, 1.0f);
			}
		}

		public static void ValidateHalfOpenDoubleUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.HalfOpenDoubleUnit();
				Assert.GreaterOrEqual(n, 0.0);
				Assert.Less(n, 1.0);
			}
		}

		public static void ValidateHalfClosedFloatUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.HalfClosedFloatUnit();
				Assert.Greater(n, 0.0f);
				Assert.LessOrEqual(n, 1.0f);
			}
		}

		public static void ValidateHalfClosedDoubleUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.HalfClosedDoubleUnit();
				Assert.Greater(n, 0.0);
				Assert.LessOrEqual(n, 1.0);
			}
		}

		public static void ValidateClosedFloatUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.ClosedFloatUnit();
				Assert.GreaterOrEqual(n, 0.0f);
				Assert.LessOrEqual(n, 1.0f);
			}
		}

		public static void ValidateClosedDoubleUnitRange(int count, IRandom random)
		{
			for (int i = 0; i < count; ++i)
			{
				var n = random.ClosedDoubleUnit();
				Assert.GreaterOrEqual(n, 0.0);
				Assert.LessOrEqual(n, 1.0);
			}
		}

		public static void ValidateOpenFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.OpenFloatUnit();
				buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.OpenDoubleUnit();
				buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.HalfOpenFloatUnit();
				buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.HalfOpenDoubleUnit();
				buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.HalfClosedFloatUnit();
				buckets[Mathf.CeilToInt(n * bucketCount) - 1] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.HalfClosedDoubleUnit();
				buckets[(int)System.Math.Ceiling(n * bucketCount) - 1] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedFloatUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.ClosedFloatUnit();
				if (n != 1.0f) buckets[Mathf.FloorToInt(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedDoubleUnitBucketDistribution(IRandom random, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.ClosedDoubleUnit();
				if (n != 1.0) buckets[(int)System.Math.Floor(n * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		[Test]
		public void OpenFloatUnitRange()
		{
			var mock = Substitute.For<IRandom>();
			mock.Next32().Returns(0x00000000U, 0x00000000U, 0x00000000U, 0x00000001U, 0x00800000U, 0x00800000U, 0x00800000U, 0x007FFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U, 0xFFFFFFFFU);
			ValidateOpenFloatUnitRange(8, mock);
			ValidateOpenFloatUnitRange(10000, SystemRandom.Create(seed));
			ValidateOpenFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateOpenFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateOpenFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenDoubleUnitRange()
		{
			var mock = Substitute.For<IRandom>();
			mock.Next64().Returns(0x0000000000000000UL, 0x0000000000000000UL, 0x0000000000000000UL, 0x0000000000000001UL, 0x0010000000000000UL, 0x0010000000000000UL, 0x0010000000000000UL, 0x000FFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL, 0xFFFFFFFFFFFFFFFFUL);
			ValidateOpenDoubleUnitRange(8, mock);
			ValidateOpenDoubleUnitRange(10000, SystemRandom.Create(seed));
			ValidateOpenDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenFloatUnitRange()
		{
			var mock = Substitute.For<IRandom>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateHalfOpenFloatUnitRange(10, mock);
			ValidateHalfOpenFloatUnitRange(10000, SystemRandom.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenDoubleUnitRange()
		{
			var mock = Substitute.For<IRandom>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateHalfOpenDoubleUnitRange(10, mock);
			ValidateHalfOpenDoubleUnitRange(10000, SystemRandom.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedFloatUnitRange()
		{
			var mock = Substitute.For<IRandom>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateHalfClosedFloatUnitRange(10, mock);
			ValidateHalfClosedFloatUnitRange(10000, SystemRandom.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedDoubleUnitRange()
		{
			var mock = Substitute.For<IRandom>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateHalfClosedDoubleUnitRange(10, mock);
			ValidateHalfClosedDoubleUnitRange(10000, SystemRandom.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedFloatUnitRange()
		{
			var mock = Substitute.For<IRandom>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0x00000000U, 0xFFFF0000U, 0x00000000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U, 0xFFE00001U, 0x007FF800U, 0xFFE00001U, 0x007FF801U, 0xFFE00001U, 0x00800000U, 0xFFE00001U, 0x00800001U, 0x00800000U);
			ValidateClosedFloatUnitRange(14, mock);
			ValidateClosedFloatUnitRange(10000, SystemRandom.Create(seed));
			ValidateClosedFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateClosedFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateClosedFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedDoubleUnitRange()
		{
			var mock = Substitute.For<IRandom>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0x0000000000000000UL, 0xFFFFFFFF00000000UL, 0x0000000000000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL, 0xFFF0000000000001UL, 0x000FFFFFFFFFF000UL, 0xFFF0000000000001UL, 0x000FFFFFFFFFF001UL, 0xFFF0000000000001UL, 0x0010000000000000UL, 0xFFF0000000000001UL, 0x0010000000000001UL, 0x0010000000000000UL);
			ValidateClosedDoubleUnitRange(14, mock);
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
