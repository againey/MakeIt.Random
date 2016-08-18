/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using NUnit.Framework;
using NSubstitute;
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

		private static void ValidateByValueEnumDistributesUniformly<TEnum>(int itemsPerBucket, float tolerance) where TEnum : struct
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

		private static void ValidateByNameEnumDistributesUniformly<TEnum>(int itemsPerBucket, float tolerance) where TEnum : struct
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

		private static void ValidateByValueEnumDistributesWithCorrectWeights<TEnum>(int itemsPerBucket, float tolerance) where TEnum : struct
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

		private static void ValidateByNameEnumDistributesWithCorrectWeights<TEnum>(int itemsPerBucket, float tolerance) where TEnum : struct
		{
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

		[Test]
		public void PrepareByValueWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => Substitute.For<IRandom>().MakeEnumGenerator<EmptyEnum>(true));
		}

		[Test]
		public void PrepareByNameWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => Substitute.For<IRandom>().MakeEnumGenerator<EmptyEnum>(true));
		}

		[Test]
		public void PrepareByValueWeightedWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => Substitute.For<IRandom>().MakeWeightedEnumGenerator<EmptyEnum>((EmptyEnum value) => 0));
		}

		[Test]
		public void PrepareByNameWeightedWithEmptyEnumThrows()
		{
			Assert.Throws<System.ArgumentException>(() => Substitute.For<IRandom>().MakeWeightedEnumGenerator<EmptyEnum>((string name) => 0));
		}

		[Test]
		public void PrepareWithOneItemEnumDistributesUniformly()
		{
			ValidateByNameEnumDistributesUniformly<OneItemEnum>(1000, 0.001f);
		}

		[Test]
		public void PrepareWithTwoItemEnumDistributesUniformly()
		{
			ValidateByNameEnumDistributesUniformly<TwoItemEnum>(10000, 0.05f);
		}

		[Test]
		public void PrepareWithTwentyItemEnumDistributesUniformly()
		{
			ValidateByNameEnumDistributesUniformly<TwentyItemEnum>(1000, 0.05f);
		}

		[Test]
		public void PrepareWithTenItemEnumWithHolesDistributesUniformly()
		{
			ValidateByNameEnumDistributesUniformly<TenItemEnumWithHoles>(2000, 0.05f);
		}

		[Test]
		public void PrepareWithFiveValueEightItemEnumWithHolesDistributesUniformly()
		{
			ValidateByNameEnumDistributesUniformly<FiveValueEightItemEnumWithHoles>(4000, 0.05f);
		}

		[Test]
		public void PrepareByValueWithOneItemEnumDistributesUniformly()
		{
			ValidateByValueEnumDistributesUniformly<OneItemEnum>(1000, 0.001f);
		}

		[Test]
		public void PrepareByValueWithTwoItemEnumDistributesUniformly()
		{
			ValidateByValueEnumDistributesUniformly<TwoItemEnum>(10000, 0.05f);
		}

		[Test]
		public void PrepareByValueWithTwentyItemEnumDistributesUniformly()
		{
			ValidateByValueEnumDistributesUniformly<TwentyItemEnum>(1000, 0.05f);
		}

		[Test]
		public void PrepareByValueWithTenItemEnumWithHolesDistributesUniformly()
		{
			ValidateByValueEnumDistributesUniformly<TenItemEnumWithHoles>(2000, 0.05f);
		}

		[Test]
		public void PrepareByValueWithFiveValueEightItemEnumWithHolesDistributesUniformly()
		{
			ValidateByValueEnumDistributesUniformly<FiveValueEightItemEnumWithHoles>(4000, 0.05f);
		}

		[Test]
		public void PrepareWeightedWithOneItemEnumDistributesWithCorrectWeights()
		{
			ValidateByNameEnumDistributesWithCorrectWeights<OneItemEnum>(1000, 0.001f);
		}

		[Test]
		public void PrepareWeightedWithTwoItemEnumDistributesWithCorrectWeights()
		{
			ValidateByNameEnumDistributesWithCorrectWeights<TwoItemEnum>(10000, 0.05f);
		}

		[Test]
		public void PrepareWeightedWithTwentyItemEnumDistributesWithCorrectWeights()
		{
			ValidateByNameEnumDistributesWithCorrectWeights<TwentyItemEnum>(1000, 0.05f);
		}

		[Test]
		public void PrepareWeightedWithTenItemEnumWithHolesDistributesWithCorrectWeights()
		{
			ValidateByNameEnumDistributesWithCorrectWeights<TenItemEnumWithHoles>(2000, 0.05f);
		}

		[Test]
		public void PrepareWeightedWithFiveValueEightItemEnumWithHolesDistributesWithCorrectWeights()
		{
			ValidateByNameEnumDistributesWithCorrectWeights<FiveValueEightItemEnumWithHoles>(4000, 0.05f);
		}

		[Test]
		public void PrepareByValueWeightedWithOneItemEnumDistributesWithCorrectWeights()
		{
			ValidateByValueEnumDistributesWithCorrectWeights<OneItemEnum>(1000, 0.001f);
		}

		[Test]
		public void PrepareByValueWeightedWithTwoItemEnumDistributesWithCorrectWeights()
		{
			ValidateByValueEnumDistributesWithCorrectWeights<TwoItemEnum>(10000, 0.05f);
		}

		[Test]
		public void PrepareByValueWeightedWithTwentyItemEnumDistributesWithCorrectWeights()
		{
			ValidateByValueEnumDistributesWithCorrectWeights<TwentyItemEnum>(1000, 0.05f);
		}

		[Test]
		public void PrepareByValueWeightedWithTenItemEnumWithHolesDistributesWithCorrectWeights()
		{
			ValidateByValueEnumDistributesWithCorrectWeights<TenItemEnumWithHoles>(2000, 0.05f);
		}

		[Test]
		public void PrepareByValueWeightedWithFiveValueEightItemEnumWithHolesDistributesWithCorrectWeights()
		{
			ValidateByValueEnumDistributesWithCorrectWeights<FiveValueEightItemEnumWithHoles>(4000, 0.05f);
		}
	}
}
#endif
