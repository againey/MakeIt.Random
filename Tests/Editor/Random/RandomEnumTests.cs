/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
#if MAKEIT_TEST_USE_NSUBSTITUTE
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace MakeIt.Random.Tests
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

		private static void ValidateByValueEnumDistributesUniformly<TEnum>(int itemsPerBucket, float tolerance, int sampleSizePercentage) where TEnum : struct
		{
			itemsPerBucket = (itemsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

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
			var random = XorShift128Plus.Create(seed);
			var generator = random.MakeEnumGenerator<TEnum>(false);
			for (int i = 0; i < iterations; ++i)
			{
				int currentTarget;
				TEnum enumValue = generator.Next();
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

		private static void ValidateByNameEnumDistributesUniformly<TEnum>(int itemsPerBucket, float tolerance, int sampleSizePercentage) where TEnum : struct
		{
			itemsPerBucket = (itemsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

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
			var random = XorShift128Plus.Create(seed);
			var generator = random.MakeEnumGenerator<TEnum>(true);
			for (int i = 0; i < iterations; ++i)
			{
				int currentTarget;
				TEnum enumValue = generator.Next();
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

		private static void ValidateByValueEnumDistributesWithCorrectWeights<TEnum>(int itemsPerBucket, float tolerance, int sampleSizePercentage) where TEnum : struct
		{
			itemsPerBucket = (itemsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

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
					int weight = random.RangeCC(3, 10);
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
			var generator = random.MakeWeightedEnumGenerator<TEnum>((TEnum value) => weights[value]);
			for (int i = 0; i < iterations; ++i)
			{
				int currentTarget;
				TEnum enumValue = generator.Next();
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

		private static void ValidateByNameEnumDistributesWithCorrectWeights<TEnum>(int itemsPerBucket, float tolerance, int sampleSizePercentage) where TEnum : struct
		{
			itemsPerBucket = (itemsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var random = XorShift128Plus.Create(seed);
			var weights = new Dictionary<string, int>();
			int weightSum = 0;
			var buckets = new Dictionary<TEnum, int>();
			var enumNames = System.Enum.GetNames(typeof(TEnum));
			foreach (var name in enumNames)
			{
				int weight = random.RangeCC(3, 10);
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
			var generator = random.MakeWeightedEnumGenerator<TEnum>((System.Func<string, int>)((string name) => weights[name]));
			for (int i = 0; i < iterations; ++i)
			{
				int currentTarget;
				TEnum enumValue = generator.Next();
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

		[TestCase(Category = "Normal")]
		public void PrepareByValueWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => Substitute.For<IRandom>().MakeEnumGenerator<EmptyEnum>(true));
		}

		[TestCase(Category = "Normal")]
		public void PrepareByNameWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => Substitute.For<IRandom>().MakeEnumGenerator<EmptyEnum>(true));
		}

		[TestCase(Category = "Normal")]
		public void PrepareByValueWeightedWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => Substitute.For<IRandom>().MakeWeightedEnumGenerator<EmptyEnum>((EmptyEnum value) => 0));
		}

		[TestCase(Category = "Normal")]
		public void PrepareByNameWeightedWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => Substitute.For<IRandom>().MakeWeightedEnumGenerator<EmptyEnum>((string name) => 0));
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWithOneItemEnumDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesUniformly<OneItemEnum>(1000, 0.001f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWithTwoItemEnumDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesUniformly<TwoItemEnum>(10000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWithTwentyItemEnumDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesUniformly<TwentyItemEnum>(1000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWithTenItemEnumWithHolesDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesUniformly<TenItemEnumWithHoles>(2000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWithFiveValueEightItemEnumWithHolesDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesUniformly<FiveValueEightItemEnumWithHoles>(4000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWithOneItemEnumDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesUniformly<OneItemEnum>(1000, 0.001f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWithTwoItemEnumDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesUniformly<TwoItemEnum>(10000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWithTwentyItemEnumDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesUniformly<TwentyItemEnum>(1000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWithTenItemEnumWithHolesDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesUniformly<TenItemEnumWithHoles>(2000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWithFiveValueEightItemEnumWithHolesDistributesUniformly(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesUniformly<FiveValueEightItemEnumWithHoles>(4000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWeightedWithOneItemEnumDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesWithCorrectWeights<OneItemEnum>(1000, 0.001f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWeightedWithTwoItemEnumDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesWithCorrectWeights<TwoItemEnum>(10000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWeightedWithTwentyItemEnumDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesWithCorrectWeights<TwentyItemEnum>(1000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWeightedWithTenItemEnumWithHolesDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesWithCorrectWeights<TenItemEnumWithHoles>(2000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareWeightedWithFiveValueEightItemEnumWithHolesDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByNameEnumDistributesWithCorrectWeights<FiveValueEightItemEnumWithHoles>(4000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWeightedWithOneItemEnumDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesWithCorrectWeights<OneItemEnum>(1000, 0.001f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWeightedWithTwoItemEnumDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesWithCorrectWeights<TwoItemEnum>(10000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWeightedWithTwentyItemEnumDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesWithCorrectWeights<TwentyItemEnum>(1000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWeightedWithTenItemEnumWithHolesDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesWithCorrectWeights<TenItemEnumWithHoles>(2000, 0.05f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PrepareByValueWeightedWithFiveValueEightItemEnumWithHolesDistributesWithCorrectWeights(int sampleSizePercentage)
		{
			ValidateByValueEnumDistributesWithCorrectWeights<FiveValueEightItemEnumWithHoles>(4000, 0.05f, sampleSizePercentage);
		}
	}
}
#endif
#endif
