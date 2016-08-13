/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using NUnit.Framework;
using System.Collections.Generic;

namespace Experilous.MakeItRandom.Tests
{
	class RandomEnumTests
	{
		private const string seed = "random seed";

		private enum EmptyEnum { }
		private enum OneItemEnum { One, }
		private enum TwoItemEnum { One, Two, }
		private enum TwentyItemEnum { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Eleven, Twelve, Thirteen, Fourteen, Fifteen, Sixteen, Seventeen, Eighteen, Nineteen, Twenty, }
		private enum TenItemEnumWithHoles { One = 1, Two = 2, Three = 3, Six = 6, Eight = 8, Fourteen= 14, Fifteen = 15, Sixteen = 16, Nineteen = 19, Twenty = 20, }
		private enum FiveValueEightItemEnumWithHoles { One = 1, Two = 2, Three = 3, Five = 5, NineThousand = 9000, Dos = 2, OnePlusOne = 2, Tres = 3, }

		private static void ValidateEnumDistributesUniformly<TEnum>(int itemsPerBucket, float tolerance) where TEnum : struct
		{
			var buckets = new Dictionary<TEnum, int>();
			var enumValues = System.Enum.GetValues(typeof(TEnum));
			foreach (var value in enumValues)
			{
				TEnum enumValue = (TEnum)value;
				int currentTarget;
				if (buckets.TryGetValue(enumValue, out currentTarget))
				{
					buckets[enumValue] = currentTarget + itemsPerBucket;
				}
				else
				{
					buckets.Add(enumValue, itemsPerBucket);
				}
			}
			int iterations = itemsPerBucket * enumValues.Length;
			var randomEnum = RandomEnum.Prepare<TEnum>();
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < iterations; ++i)
			{
				int currentTarget;
				TEnum enumValue = randomEnum(random);
				if (buckets.TryGetValue(enumValue, out currentTarget))
				{
					buckets[enumValue] = currentTarget - 1;
				}
				else
				{
					Assert.Fail("An invalid enum value was generated.");
				}
			}

			int[] bucketsArray = new int[buckets.Count];
			buckets.Values.CopyTo(bucketsArray, 0);
			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(bucketsArray, 0), tolerance * itemsPerBucket);
		}

		private static void ValidateDistinctEnumDistributesUniformly<TEnum>(int itemsPerBucket, float tolerance) where TEnum : struct
		{
			var buckets = new Dictionary<TEnum, int>();
			var enumValues = System.Enum.GetValues(typeof(TEnum));
			foreach (var value in enumValues)
			{
				TEnum enumValue = (TEnum)value;
				if (!buckets.ContainsKey(enumValue))
				{
					buckets.Add(enumValue, itemsPerBucket);
				}
			}
			int iterations = itemsPerBucket * buckets.Count;
			var randomEnum = RandomEnum.PrepareDistinct<TEnum>();
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < iterations; ++i)
			{
				int currentTarget;
				TEnum enumValue = randomEnum(random);
				if (buckets.TryGetValue(enumValue, out currentTarget))
				{
					buckets[enumValue] = currentTarget - 1;
				}
				else
				{
					Assert.Fail("An invalid enum value was generated.");
				}
			}

			int[] bucketsArray = new int[buckets.Count];
			buckets.Values.CopyTo(bucketsArray, 0);
			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(bucketsArray, 0), tolerance * itemsPerBucket);
		}

		private static void ValidateEnumDistributesWithCorrectWeights<TEnum>(int itemsPerBucket, float tolerance) where TEnum : struct
		{
			var random = XorShift128Plus.Create(seed);
			var weights = new Dictionary<string, int>();
			int weightSum = 0;
			var buckets = new Dictionary<TEnum, int>();
			var enumNames = System.Enum.GetNames(typeof(TEnum));
			foreach (var name in enumNames)
			{
				int weight = random.ClosedRange(3, 10);
				weights.Add(name, weight);
				weightSum += weight;
			}

			foreach (var name in enumNames)
			{
				TEnum enumValue = (TEnum)System.Enum.Parse(typeof(TEnum), name);
				int currentTarget;
				if (buckets.TryGetValue(enumValue, out currentTarget))
				{
					buckets[enumValue] = currentTarget + itemsPerBucket * enumNames.Length * weights[name] / weightSum;
				}
				else
				{
					buckets.Add(enumValue, itemsPerBucket * enumNames.Length * weights[name] / weightSum);
				}
			}

			int iterations = 0;
			foreach (var target in buckets.Values)
			{
				iterations += target;
			}
			var randomEnum = RandomEnum.PrepareWeighted<TEnum>(weights);
			for (int i = 0; i < iterations; ++i)
			{
				int currentTarget;
				TEnum enumValue = randomEnum(random);
				if (buckets.TryGetValue(enumValue, out currentTarget))
				{
					buckets[enumValue] = currentTarget - 1;
				}
				else
				{
					Assert.Fail("An invalid enum value was generated.");
				}
			}

			int[] bucketsArray = new int[buckets.Count];
			buckets.Values.CopyTo(bucketsArray, 0);
			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(bucketsArray, 0), tolerance * itemsPerBucket);
		}

		private static void ValidateDistinctEnumDistributesWithCorrectWeights<TEnum>(int itemsPerBucket, float tolerance) where TEnum : struct
		{
			var random = XorShift128Plus.Create(seed);
			var weights = new Dictionary<TEnum, int>();
			int weightSum = 0;
			var buckets = new Dictionary<TEnum, int>();
			var enumValues = System.Enum.GetValues(typeof(TEnum));
			foreach (var value in enumValues)
			{
				TEnum enumValue = (TEnum)value;
				if (!weights.ContainsKey(enumValue))
				{
					int weight = random.ClosedRange(3, 10);
					weights.Add(enumValue, weight);
					weightSum += weight;
				}
			}

			foreach (var value in enumValues)
			{
				TEnum enumValue = (TEnum)value;
				if (!buckets.ContainsKey(enumValue))
				{
					buckets.Add(enumValue, itemsPerBucket * weights.Count * weights[enumValue] / weightSum);
				}
			}

			int iterations = 0;
			foreach (var target in buckets.Values)
			{
				iterations += target;
			}
			var randomEnum = RandomEnum.PrepareDistinctWeighted<TEnum>(weights);
			for (int i = 0; i < iterations; ++i)
			{
				int currentTarget;
				TEnum enumValue = randomEnum(random);
				if (buckets.TryGetValue(enumValue, out currentTarget))
				{
					buckets[enumValue] = currentTarget - 1;
				}
				else
				{
					Assert.Fail("An invalid enum value was generated.");
				}
			}

			int[] bucketsArray = new int[buckets.Count];
			buckets.Values.CopyTo(bucketsArray, 0);
			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(bucketsArray, 0), tolerance * itemsPerBucket);
		}

		[Test]
		public void PrepareWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => RandomEnum.Prepare<EmptyEnum>());
		}

		[Test]
		public void PrepareDistinctWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => RandomEnum.PrepareDistinct<EmptyEnum>());
		}

		[Test]
		public void PrepareWeightedWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => RandomEnum.PrepareWeighted<EmptyEnum>(new Dictionary<string, int>()));
		}

		[Test]
		public void PrepareDistinctWeightedWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => RandomEnum.PrepareDistinctWeighted(new Dictionary<EmptyEnum, int>()));
		}

		[Test]
		public void PrepareWithOneItemEnumDistributesUniformly()
		{
			ValidateEnumDistributesUniformly<OneItemEnum>(1000, 0.001f);
		}

		[Test]
		public void PrepareWithTwoItemEnumDistributesUniformly()
		{
			ValidateEnumDistributesUniformly<TwoItemEnum>(10000, 0.05f);
		}

		[Test]
		public void PrepareWithTwentyItemEnumDistributesUniformly()
		{
			ValidateEnumDistributesUniformly<TwentyItemEnum>(1000, 0.05f);
		}

		[Test]
		public void PrepareWithTenItemEnumWithHolesDistributesUniformly()
		{
			ValidateEnumDistributesUniformly<TenItemEnumWithHoles>(2000, 0.05f);
		}

		[Test]
		public void PrepareWithFiveValueEightItemEnumWithHolesDistributesUniformly()
		{
			ValidateEnumDistributesUniformly<FiveValueEightItemEnumWithHoles>(4000, 0.05f);
		}

		[Test]
		public void PrepareDistinctWithOneItemEnumDistributesUniformly()
		{
			ValidateDistinctEnumDistributesUniformly<OneItemEnum>(1000, 0.001f);
		}

		[Test]
		public void PrepareDistinctWithTwoItemEnumDistributesUniformly()
		{
			ValidateDistinctEnumDistributesUniformly<TwoItemEnum>(10000, 0.05f);
		}

		[Test]
		public void PrepareDistinctWithTwentyItemEnumDistributesUniformly()
		{
			ValidateDistinctEnumDistributesUniformly<TwentyItemEnum>(1000, 0.05f);
		}

		[Test]
		public void PrepareDistinctWithTenItemEnumWithHolesDistributesUniformly()
		{
			ValidateDistinctEnumDistributesUniformly<TenItemEnumWithHoles>(2000, 0.05f);
		}

		[Test]
		public void PrepareDistinctWithFiveValueEightItemEnumWithHolesDistributesUniformly()
		{
			ValidateDistinctEnumDistributesUniformly<FiveValueEightItemEnumWithHoles>(4000, 0.05f);
		}

		[Test]
		public void PrepareWeightedWithOneItemEnumDistributesWithCorrectWeights()
		{
			ValidateEnumDistributesWithCorrectWeights<OneItemEnum>(1000, 0.001f);
		}

		[Test]
		public void PrepareWeightedWithTwoItemEnumDistributesWithCorrectWeights()
		{
			ValidateEnumDistributesWithCorrectWeights<TwoItemEnum>(10000, 0.05f);
		}

		[Test]
		public void PrepareWeightedWithTwentyItemEnumDistributesWithCorrectWeights()
		{
			ValidateEnumDistributesWithCorrectWeights<TwentyItemEnum>(1000, 0.05f);
		}

		[Test]
		public void PrepareWeightedWithTenItemEnumWithHolesDistributesWithCorrectWeights()
		{
			ValidateEnumDistributesWithCorrectWeights<TenItemEnumWithHoles>(2000, 0.05f);
		}

		[Test]
		public void PrepareWeightedWithFiveValueEightItemEnumWithHolesDistributesWithCorrectWeights()
		{
			ValidateEnumDistributesWithCorrectWeights<FiveValueEightItemEnumWithHoles>(4000, 0.05f);
		}

		[Test]
		public void PrepareDistinctWeightedWithOneItemEnumDistributesWithCorrectWeights()
		{
			ValidateDistinctEnumDistributesWithCorrectWeights<OneItemEnum>(1000, 0.001f);
		}

		[Test]
		public void PrepareDistinctWeightedWithTwoItemEnumDistributesWithCorrectWeights()
		{
			ValidateDistinctEnumDistributesWithCorrectWeights<TwoItemEnum>(10000, 0.05f);
		}

		[Test]
		public void PrepareDistinctWeightedWithTwentyItemEnumDistributesWithCorrectWeights()
		{
			ValidateDistinctEnumDistributesWithCorrectWeights<TwentyItemEnum>(1000, 0.05f);
		}

		[Test]
		public void PrepareDistinctWeightedWithTenItemEnumWithHolesDistributesWithCorrectWeights()
		{
			ValidateDistinctEnumDistributesWithCorrectWeights<TenItemEnumWithHoles>(2000, 0.05f);
		}

		[Test]
		public void PrepareDistinctWeightedWithFiveValueEightItemEnumWithHolesDistributesWithCorrectWeights()
		{
			ValidateDistinctEnumDistributesWithCorrectWeights<FiveValueEightItemEnumWithHoles>(4000, 0.05f);
		}
	}
}
#endif
