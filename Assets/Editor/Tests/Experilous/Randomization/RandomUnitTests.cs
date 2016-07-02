/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Experilous.Randomization.Tests
{
	class RandomUnitTests
	{
		private const string seed = "random seed";

		public static void ValidateOpenFloatUnitRange(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.OpenFloat(engine);
				Assert.Greater(random, 0.0f);
				Assert.Less(random, 1.0f);
			}
		}

		public static void ValidateOpenFloatUnitRangeFast32(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.OpenFloatFast32(engine);
				Assert.Greater(random, 0.0f);
				Assert.Less(random, 1.0f);
			}
		}

		public static void ValidateOpenFloatUnitRangeFast64(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.OpenFloatFast64(engine);
				Assert.Greater(random, 0.0f);
				Assert.Less(random, 1.0f);
			}
		}

		public static void ValidateOpenDoubleUnitRange(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.OpenDouble(engine);
				Assert.Greater(random, 0.0);
				Assert.Less(random, 1.0);
			}
		}

		public static void ValidateOpenDoubleUnitRangeFast32(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.OpenDoubleFast32(engine);
				Assert.Greater(random, 0.0);
				Assert.Less(random, 1.0);
			}
		}

		public static void ValidateOpenDoubleUnitRangeFast64(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.OpenDoubleFast64(engine);
				Assert.Greater(random, 0.0);
				Assert.Less(random, 1.0);
			}
		}

		public static void ValidateHalfOpenFloatUnitRange(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.HalfOpenFloat(engine);
				Assert.GreaterOrEqual(random, 0.0f);
				Assert.Less(random, 1.0f);
			}
		}

		public static void ValidateHalfOpenDoubleUnitRange(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.HalfOpenDouble(engine);
				Assert.GreaterOrEqual(random, 0.0);
				Assert.Less(random, 1.0);
			}
		}

		public static void ValidateHalfClosedFloatUnitRange(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.HalfClosedFloat(engine);
				Assert.Greater(random, 0.0f);
				Assert.LessOrEqual(random, 1.0f);
			}
		}

		public static void ValidateHalfClosedDoubleUnitRange(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.HalfClosedDouble(engine);
				Assert.Greater(random, 0.0);
				Assert.LessOrEqual(random, 1.0);
			}
		}

		public static void ValidateClosedFloatUnitRange(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.ClosedFloat(engine);
				Assert.GreaterOrEqual(random, 0.0f);
				Assert.LessOrEqual(random, 1.0f);
			}
		}

		public static void ValidateClosedFloatUnitRangeFast32(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.ClosedFloatFast32(engine);
				Assert.GreaterOrEqual(random, 0.0f);
				Assert.LessOrEqual(random, 1.0f);
			}
		}

		public static void ValidateClosedFloatUnitRangeFast64(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.ClosedFloatFast64(engine);
				Assert.GreaterOrEqual(random, 0.0f);
				Assert.LessOrEqual(random, 1.0f);
			}
		}

		public static void ValidateClosedDoubleUnitRange(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.ClosedDouble(engine);
				Assert.GreaterOrEqual(random, 0.0);
				Assert.LessOrEqual(random, 1.0);
			}
		}

		public static void ValidateClosedDoubleUnitRangeFast32(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.ClosedDoubleFast32(engine);
				Assert.GreaterOrEqual(random, 0.0);
				Assert.LessOrEqual(random, 1.0);
			}
		}

		public static void ValidateClosedDoubleUnitRangeFast64(int count, IRandomEngine engine)
		{
			for (int i = 0; i < count; ++i)
			{
				var random = RandomUnit.ClosedDoubleFast64(engine);
				Assert.GreaterOrEqual(random, 0.0);
				Assert.LessOrEqual(random, 1.0);
			}
		}

		public static void ValidateOpenFloatUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.OpenFloat(engine);
				buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenFloatUnitBucketDistributionFast32(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.OpenFloatFast32(engine);
				buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenFloatUnitBucketDistributionFast64(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.OpenFloatFast64(engine);
				buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenDoubleUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.OpenDouble(engine);
				buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenDoubleUnitBucketDistributionFast32(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.OpenDoubleFast32(engine);
				buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateOpenDoubleUnitBucketDistributionFast64(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.OpenDoubleFast64(engine);
				buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenFloatUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.HalfOpenFloat(engine);
				buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfOpenDoubleUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.HalfOpenDouble(engine);
				buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedFloatUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.HalfClosedFloat(engine);
				buckets[Mathf.CeilToInt(random * bucketCount) - 1] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateHalfClosedDoubleUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.HalfClosedDouble(engine);
				buckets[(int)System.Math.Ceiling(random * bucketCount) - 1] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedFloatUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.ClosedFloat(engine);
				if (random != 1.0f) buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedFloatUnitBucketDistributionFast32(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.ClosedFloatFast32(engine);
				if (random != 1.0f) buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedFloatUnitBucketDistributionFast64(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.ClosedFloatFast64(engine);
				if (random != 1.0f) buckets[Mathf.FloorToInt(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedDoubleUnitBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.ClosedDouble(engine);
				if (random != 1.0) buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedDoubleUnitBucketDistributionFast32(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.ClosedDoubleFast32(engine);
				if (random != 1.0) buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateClosedDoubleUnitBucketDistributionFast64(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = RandomUnit.ClosedDoubleFast64(engine);
				if (random != 1.0) buckets[(int)System.Math.Floor(random * bucketCount)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateNext32BitsDistribution(IRandomEngine engine, int bitCount, int hitsPerBucket, float tolerance)
		{
			int bucketCount = 1;
			for (int i = 0; i < bitCount; ++i) bucketCount = bucketCount * 2;
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = engine.Next32(bitCount);
				buckets[random] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateNext64BitsDistribution(IRandomEngine engine, int bitCount, int hitsPerBucket, float tolerance)
		{
			int bucketCount = 1;
			for (int i = 0; i < bitCount; ++i) bucketCount = bucketCount * 2;
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = engine.Next64(bitCount);
				buckets[random] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateNextLessThanBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = engine.NextLessThan((uint)bucketCount);
				buckets[random] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		[Test]
		public void OpenFloatUnitRange()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateOpenFloatUnitRange(10, mock);
			ValidateOpenFloatUnitRange(10000, NativeRandomEngine.Create(seed));
			ValidateOpenFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateOpenFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateOpenFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenFloatUnitRangeFast32()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateOpenFloatUnitRangeFast32(10, mock);
			ValidateOpenFloatUnitRangeFast32(10000, NativeRandomEngine.Create(seed));
			ValidateOpenFloatUnitRangeFast32(10000, SplitMix64.Create(seed));
			ValidateOpenFloatUnitRangeFast32(10000, XorShift128Plus.Create(seed));
			ValidateOpenFloatUnitRangeFast32(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenFloatUnitRangeFast32(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenFloatUnitRangeFast64()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateOpenFloatUnitRangeFast64(10, mock);
			ValidateOpenFloatUnitRangeFast64(10000, NativeRandomEngine.Create(seed));
			ValidateOpenFloatUnitRangeFast64(10000, SplitMix64.Create(seed));
			ValidateOpenFloatUnitRangeFast64(10000, XorShift128Plus.Create(seed));
			ValidateOpenFloatUnitRangeFast64(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenFloatUnitRangeFast64(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenDoubleUnitRange()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateOpenDoubleUnitRange(10, mock);
			ValidateOpenDoubleUnitRange(10000, NativeRandomEngine.Create(seed));
			ValidateOpenDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenDoubleUnitRangeFast32()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateOpenDoubleUnitRangeFast32(10, mock);
			ValidateOpenDoubleUnitRangeFast32(10000, NativeRandomEngine.Create(seed));
			ValidateOpenDoubleUnitRangeFast32(10000, SplitMix64.Create(seed));
			ValidateOpenDoubleUnitRangeFast32(10000, XorShift128Plus.Create(seed));
			ValidateOpenDoubleUnitRangeFast32(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenDoubleUnitRangeFast32(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenDoubleUnitRangeFast64()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateOpenDoubleUnitRangeFast64(10, mock);
			ValidateOpenDoubleUnitRangeFast64(10000, NativeRandomEngine.Create(seed));
			ValidateOpenDoubleUnitRangeFast64(10000, SplitMix64.Create(seed));
			ValidateOpenDoubleUnitRangeFast64(10000, XorShift128Plus.Create(seed));
			ValidateOpenDoubleUnitRangeFast64(10000, XoroShiro128Plus.Create(seed));
			ValidateOpenDoubleUnitRangeFast64(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenFloatUnitRange()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateHalfOpenFloatUnitRange(10, mock);
			ValidateHalfOpenFloatUnitRange(10000, NativeRandomEngine.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfOpenDoubleUnitRange()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateHalfOpenDoubleUnitRange(10, mock);
			ValidateHalfOpenDoubleUnitRange(10000, NativeRandomEngine.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfOpenDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedFloatUnitRange()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateHalfClosedFloatUnitRange(10, mock);
			ValidateHalfClosedFloatUnitRange(10000, NativeRandomEngine.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void HalfClosedDoubleUnitRange()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateHalfClosedDoubleUnitRange(10, mock);
			ValidateHalfClosedDoubleUnitRange(10000, NativeRandomEngine.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateHalfClosedDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedFloatUnitRange()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateClosedFloatUnitRange(10, mock);
			ValidateClosedFloatUnitRange(10000, NativeRandomEngine.Create(seed));
			ValidateClosedFloatUnitRange(10000, SplitMix64.Create(seed));
			ValidateClosedFloatUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateClosedFloatUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedFloatUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedFloatUnitRangeFast32()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateClosedFloatUnitRangeFast32(10, mock);
			ValidateClosedFloatUnitRangeFast32(10000, NativeRandomEngine.Create(seed));
			ValidateClosedFloatUnitRangeFast32(10000, SplitMix64.Create(seed));
			ValidateClosedFloatUnitRangeFast32(10000, XorShift128Plus.Create(seed));
			ValidateClosedFloatUnitRangeFast32(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedFloatUnitRangeFast32(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedFloatUnitRangeFast64()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateClosedFloatUnitRangeFast64(10, mock);
			ValidateClosedFloatUnitRangeFast64(10000, NativeRandomEngine.Create(seed));
			ValidateClosedFloatUnitRangeFast64(10000, SplitMix64.Create(seed));
			ValidateClosedFloatUnitRangeFast64(10000, XorShift128Plus.Create(seed));
			ValidateClosedFloatUnitRangeFast64(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedFloatUnitRangeFast64(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedDoubleUnitRange()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateClosedDoubleUnitRange(10, mock);
			ValidateClosedDoubleUnitRange(10000, NativeRandomEngine.Create(seed));
			ValidateClosedDoubleUnitRange(10000, SplitMix64.Create(seed));
			ValidateClosedDoubleUnitRange(10000, XorShift128Plus.Create(seed));
			ValidateClosedDoubleUnitRange(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedDoubleUnitRange(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedDoubleUnitRangeFast32()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next32().Returns(0x00000000U, 0xFFFFFFFFU, 0xFFFF0000U, 0x0000FFFFU, 0x3FFFFFFFU, 0x40000000U, 0x7FFFFFFFU, 0x80000000U, 0xBFFFFFFFU, 0xC0000000U);
			ValidateClosedDoubleUnitRangeFast32(10, mock);
			ValidateClosedDoubleUnitRangeFast32(10000, NativeRandomEngine.Create(seed));
			ValidateClosedDoubleUnitRangeFast32(10000, SplitMix64.Create(seed));
			ValidateClosedDoubleUnitRangeFast32(10000, XorShift128Plus.Create(seed));
			ValidateClosedDoubleUnitRangeFast32(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedDoubleUnitRangeFast32(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void ClosedDoubleUnitRangeFast64()
		{
			var mock = Substitute.For<IRandomEngine>();
			mock.Next64().Returns(0x0000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFF00000000UL, 0x00000000FFFFFFFFUL, 0x3FFFFFFFFFFFFFFFU, 0x4000000000000000UL, 0x7FFFFFFFFFFFFFFFUL, 0x8000000000000000UL, 0xBFFFFFFFFFFFFFFFUL, 0xC000000000000000UL);
			ValidateClosedDoubleUnitRangeFast64(10, mock);
			ValidateClosedDoubleUnitRangeFast64(10000, NativeRandomEngine.Create(seed));
			ValidateClosedDoubleUnitRangeFast64(10000, SplitMix64.Create(seed));
			ValidateClosedDoubleUnitRangeFast64(10000, XorShift128Plus.Create(seed));
			ValidateClosedDoubleUnitRangeFast64(10000, XoroShiro128Plus.Create(seed));
			ValidateClosedDoubleUnitRangeFast64(10000, XorShift1024Star.Create(seed));
		}

		[Test]
		public void OpenFloatUnitTenBucketDistribution()
		{
			ValidateOpenFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenFloatUnitTenBucketDistributionFast32()
		{
			ValidateOpenFloatUnitBucketDistributionFast32(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistributionFast32(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistributionFast32(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistributionFast32(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenFloatUnitBucketDistributionFast32(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenFloatUnitTenBucketDistributionFast64()
		{
			ValidateOpenFloatUnitBucketDistributionFast64(NativeRandomEngine.Create(seed), 10, 10000, 0.02f);
			ValidateOpenFloatUnitBucketDistributionFast64(SplitMix64.Create(seed), 10, 10000, 0.02f);
			ValidateOpenFloatUnitBucketDistributionFast64(XorShift128Plus.Create(seed), 10, 10000, 0.02f);
			ValidateOpenFloatUnitBucketDistributionFast64(XoroShiro128Plus.Create(seed), 10, 10000, 0.02f);
			ValidateOpenFloatUnitBucketDistributionFast64(XorShift1024Star.Create(seed), 10, 10000, 0.02f);
		}

		[Test]
		public void OpenFloatUnitThousandBucketDistribution()
		{
			ValidateOpenFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenFloatUnitThousandBucketDistributionFast32()
		{
			ValidateOpenFloatUnitBucketDistributionFast32(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistributionFast32(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistributionFast32(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistributionFast32(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistributionFast32(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenFloatUnitThousandBucketDistributionFast64()
		{
			ValidateOpenFloatUnitBucketDistributionFast64(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistributionFast64(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistributionFast64(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistributionFast64(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenFloatUnitBucketDistributionFast64(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenDoubleUnitTenBucketDistribution()
		{
			ValidateOpenDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.020f);
			ValidateOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenDoubleUnitTenBucketDistributionFast32()
		{
			ValidateOpenDoubleUnitBucketDistributionFast32(NativeRandomEngine.Create(seed), 10, 10000, 0.020f);
			ValidateOpenDoubleUnitBucketDistributionFast32(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistributionFast32(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistributionFast32(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistributionFast32(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenDoubleUnitTenBucketDistributionFast64()
		{
			ValidateOpenDoubleUnitBucketDistributionFast64(NativeRandomEngine.Create(seed), 10, 10000, 0.020f);
			ValidateOpenDoubleUnitBucketDistributionFast64(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistributionFast64(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistributionFast64(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateOpenDoubleUnitBucketDistributionFast64(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void OpenDoubleUnitThousandBucketDistribution()
		{
			ValidateOpenDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenDoubleUnitThousandBucketDistributionFast32()
		{
			ValidateOpenDoubleUnitBucketDistributionFast32(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistributionFast32(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistributionFast32(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistributionFast32(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistributionFast32(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void OpenDoubleUnitThousandBucketDistributionFast64()
		{
			ValidateOpenDoubleUnitBucketDistributionFast64(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistributionFast64(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistributionFast64(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistributionFast64(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateOpenDoubleUnitBucketDistributionFast64(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenFloatUnitTenBucketDistribution()
		{
			ValidateHalfOpenFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenFloatUnitThousandBucketDistribution()
		{
			ValidateHalfOpenFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenDoubleUnitTenBucketDistribution()
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenDoubleUnitThousandBucketDistribution()
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfClosedFloatUnitTenBucketDistribution()
		{
			ValidateHalfClosedFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfClosedFloatUnitThousandBucketDistribution()
		{
			ValidateHalfClosedFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfClosedDoubleUnitTenBucketDistribution()
		{
			ValidateHalfClosedDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfClosedDoubleUnitThousandBucketDistribution()
		{
			ValidateHalfClosedDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateHalfClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedFloatUnitTenBucketDistribution()
		{
			ValidateClosedFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedFloatUnitTenBucketDistributionFast32()
		{
			ValidateClosedFloatUnitBucketDistributionFast32(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistributionFast32(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistributionFast32(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistributionFast32(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistributionFast32(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedFloatUnitTenBucketDistributionFast64()
		{
			ValidateClosedFloatUnitBucketDistributionFast64(NativeRandomEngine.Create(seed), 10, 10000, 0.02f);
			ValidateClosedFloatUnitBucketDistributionFast64(SplitMix64.Create(seed), 10, 10000, 0.02f);
			ValidateClosedFloatUnitBucketDistributionFast64(XorShift128Plus.Create(seed), 10, 10000, 0.02f);
			ValidateClosedFloatUnitBucketDistributionFast64(XoroShiro128Plus.Create(seed), 10, 10000, 0.02f);
			ValidateClosedFloatUnitBucketDistributionFast64(XorShift1024Star.Create(seed), 10, 10000, 0.02f);
		}

		[Test]
		public void ClosedFloatUnitThousandBucketDistribution()
		{
			ValidateClosedFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedFloatUnitThousandBucketDistributionFast32()
		{
			ValidateClosedFloatUnitBucketDistributionFast32(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistributionFast32(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistributionFast32(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistributionFast32(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistributionFast32(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedFloatUnitThousandBucketDistributionFast64()
		{
			ValidateClosedFloatUnitBucketDistributionFast64(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistributionFast64(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistributionFast64(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistributionFast64(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistributionFast64(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedDoubleUnitTenBucketDistribution()
		{
			ValidateClosedDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedDoubleUnitTenBucketDistributionFast32()
		{
			ValidateClosedDoubleUnitBucketDistributionFast32(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistributionFast32(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistributionFast32(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistributionFast32(XoroShiro128Plus.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistributionFast32(XorShift1024Star.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedDoubleUnitTenBucketDistributionFast64()
		{
			ValidateClosedDoubleUnitBucketDistributionFast64(NativeRandomEngine.Create(seed), 10, 10000, 0.02f);
			ValidateClosedDoubleUnitBucketDistributionFast64(SplitMix64.Create(seed), 10, 10000, 0.02f);
			ValidateClosedDoubleUnitBucketDistributionFast64(XorShift128Plus.Create(seed), 10, 10000, 0.02f);
			ValidateClosedDoubleUnitBucketDistributionFast64(XoroShiro128Plus.Create(seed), 10, 10000, 0.02f);
			ValidateClosedDoubleUnitBucketDistributionFast64(XorShift1024Star.Create(seed), 10, 10000, 0.02f);
		}

		[Test]
		public void ClosedDoubleUnitThousandBucketDistribution()
		{
			ValidateClosedDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedDoubleUnitThousandBucketDistributionFast32()
		{
			ValidateClosedDoubleUnitBucketDistributionFast32(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistributionFast32(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistributionFast32(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistributionFast32(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistributionFast32(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedDoubleUnitThousandBucketDistributionFast64()
		{
			ValidateClosedDoubleUnitBucketDistributionFast64(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistributionFast64(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistributionFast64(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistributionFast64(XoroShiro128Plus.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistributionFast64(XorShift1024Star.Create(seed), 1000, 100, 0.15f);
		}
	}
}
#endif
