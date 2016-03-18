/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Randomization.Tests
{
	class RandomUtilityTests
	{
		private static string seed = "random seed";

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

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
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

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateNextLessThanBucketDistribution(IRandomEngine engine, int bucketCount, int hitsPerBucket, float tolerance)
		{
			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = engine.NextLessThan((uint)bucketCount);
				buckets[random] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateChanceDistribution(IRandomEngine engine, int numerator, int denominator, int trialCount, float tolerance)
		{
			int falseCount = 0;
			int trueCount = 0;
			for (int i = 0; i < trialCount; ++i)
			{
				if (RandomUtility.Chance(numerator, denominator, engine))
				{
					++trueCount;
				}
				else
				{
					++falseCount;
				}
			}

			Assert.LessOrEqual(Mathf.Abs((float)numerator / denominator - (float)trueCount / trialCount), tolerance, string.Format("{0}/{1}", numerator, denominator));
		}

		[Test]
		public void HalfOpenFloatUnitRange()
		{
			ValidateHalfOpenFloatUnitRange(NativeRandomEngine.Create(seed));
			ValidateHalfOpenFloatUnitRange(SplitMix64.Create(seed));
			ValidateHalfOpenFloatUnitRange(XorShift128Plus.Create(seed));
		}

		[Test]
		public void HalfOpenDoubleUnitRange()
		{
			ValidateHalfOpenDoubleUnitRange(NativeRandomEngine.Create(seed));
			ValidateHalfOpenDoubleUnitRange(SplitMix64.Create(seed));
			ValidateHalfOpenDoubleUnitRange(XorShift128Plus.Create(seed));
		}

		[Test]
		public void ClosedFloatUnitRange()
		{
			ValidateClosedFloatUnitRange(NativeRandomEngine.Create(seed));
			ValidateClosedFloatUnitRange(SplitMix64.Create(seed));
			ValidateClosedFloatUnitRange(XorShift128Plus.Create(seed));
		}

		[Test]
		public void ClosedDoubleUnitRange()
		{
			ValidateClosedDoubleUnitRange(NativeRandomEngine.Create(seed));
			ValidateClosedDoubleUnitRange(SplitMix64.Create(seed));
			ValidateClosedDoubleUnitRange(XorShift128Plus.Create(seed));
		}

		[Test]
		public void HalfOpenFloatUnitTenBucketDistribution()
		{
			ValidateHalfOpenFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenFloatUnitThousandBucketDistribution()
		{
			ValidateHalfOpenFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void HalfOpenDoubleUnitTenBucketDistribution()
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void HalfOpenDoubleUnitThousandBucketDistribution()
		{
			ValidateHalfOpenDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateHalfOpenDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedFloatUnitTenBucketDistribution()
		{
			ValidateClosedFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedFloatUnitThousandBucketDistribution()
		{
			ValidateClosedFloatUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedFloatUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void ClosedDoubleUnitTenBucketDistribution()
		{
			ValidateClosedDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 10, 10000, 0.015f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 10, 10000, 0.015f);
		}

		[Test]
		public void ClosedDoubleUnitThousandBucketDistribution()
		{
			ValidateClosedDoubleUnitBucketDistribution(NativeRandomEngine.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(SplitMix64.Create(seed), 1000, 100, 0.15f);
			ValidateClosedDoubleUnitBucketDistribution(XorShift128Plus.Create(seed), 1000, 100, 0.15f);
		}

		[Test]
		public void Next4BitsDistribution()
		{
			ValidateNext32BitsDistribution(NativeRandomEngine.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(SplitMix64.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(XorShift128Plus.Create(seed), 4, 10000, 0.02f);
		}

		[Test]
		public void Next5BitsDistribution()
		{
			ValidateNext32BitsDistribution(NativeRandomEngine.Create(seed), 5, 10000, 0.02f);
			ValidateNext32BitsDistribution(SplitMix64.Create(seed), 5, 10000, 0.02f);
			ValidateNext32BitsDistribution(XorShift128Plus.Create(seed), 5, 10000, 0.02f);
		}

		[Test]
		public void NextLessThanPowerOfTwoBucketDistribution()
		{
			ValidateNextLessThanBucketDistribution(NativeRandomEngine.Create(seed), 32, 3000, 0.05f);
			ValidateNextLessThanBucketDistribution(SplitMix64.Create(seed), 32, 3000, 0.05f);
			ValidateNextLessThanBucketDistribution(XorShift128Plus.Create(seed), 32, 3000, 0.05f);
		}

		[Test]
		public void NextLessThanNonPowerOfTwoBucketDistribution()
		{
			ValidateNextLessThanBucketDistribution(NativeRandomEngine.Create(seed), 25, 4000, 0.04f);
			ValidateNextLessThanBucketDistribution(SplitMix64.Create(seed), 25, 4000, 0.04f);
			ValidateNextLessThanBucketDistribution(XorShift128Plus.Create(seed), 25, 4000, 0.04f);
		}

		[Test]
		public void ChancePowerOfTwoDenominatorDistribution()
		{
			ValidateChanceDistribution(NativeRandomEngine.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(SplitMix64.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(XorShift128Plus.Create(seed), 25, 32, 100000, 0.002f);
		}

		[Test]
		public void ChanceNonPowerOfTwoDenominatorDistribution()
		{
			ValidateChanceDistribution(NativeRandomEngine.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(SplitMix64.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(XorShift128Plus.Create(seed), 17, 25, 100000, 0.003f);
		}

		[Test]
		public void ChanceDistribution()
		{
			System.Action<int, int> validateRatio = (int numerator, int denominator) =>
			{
				ValidateChanceDistribution(NativeRandomEngine.Create(seed), numerator, denominator, 1000, 0.04f);
				ValidateChanceDistribution(SplitMix64.Create(seed), numerator, denominator, 1000, 0.04f);
				ValidateChanceDistribution(XorShift128Plus.Create(seed), numerator, denominator, 10000, 0.04f);
			};

			for (int denominator = 1; denominator < 20; ++denominator)
			{
				for (int numerator = 0; numerator <= denominator; ++numerator)
				{
					validateRatio(numerator, denominator);
				}
			}

			System.Action<int, int> validateDenominatorRange = (int averageDenominator, int range) =>
			{
				for (int denominator = averageDenominator - range; denominator <= averageDenominator + range; ++denominator)
				{
					for (int numerator = 0; numerator <= range * 2; ++numerator)
					{
						validateRatio(numerator, denominator);
					}

					for (int numerator = averageDenominator / 5; numerator <= averageDenominator / 5 + range * 2; ++numerator)
					{
						validateRatio(numerator, denominator);
					}

					for (int numerator = averageDenominator / 2 - range; numerator <= averageDenominator / 2 + range; ++numerator)
					{
						validateRatio(numerator, denominator);
					}

					for (int numerator = averageDenominator * 4 / 5 - range * 2; numerator <= averageDenominator * 4 / 5; ++numerator)
					{
						validateRatio(numerator, denominator);
					}

					for (int numerator = averageDenominator - range * 2; numerator <= averageDenominator; ++numerator)
					{
						validateRatio(numerator, denominator);
					}
				}
			};

			validateDenominatorRange(197, 2);
			validateDenominatorRange(544, 2);
			validateDenominatorRange(1934, 2);
			validateDenominatorRange(70000, 2);
			validateDenominatorRange(16000000, 2);
			validateDenominatorRange(17000000, 2);
			validateDenominatorRange(500000000, 2);
		}
	}
}
#endif
