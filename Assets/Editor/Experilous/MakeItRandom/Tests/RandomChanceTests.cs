/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;
using UnityEngine;
using System;

namespace Experilous.MakeItRandom.Tests
{
	class RandomChanceTests
	{
		private const string seed = "random seed";

		private void ValidateDistribution(int count, Func<bool> generator, float expectedTrue, float tolerance, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			int trueCount = 0;
			for (int i = 0; i < count; ++i)
			{
				trueCount += generator() ? 1 : 0;
			}

			Assert.LessOrEqual(Mathf.Abs((float)trueCount / count - expectedTrue), tolerance);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void UniformChance(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(), 0.5f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator();
			ValidateDistribution(10000, () => generator.Next(), 0.5f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedChanceInt32(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37, 60), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37, 60);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedChanceUInt32(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37U, 60U), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37U, 60U);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedChanceInt64(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37L, 60L), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37L, 60L);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedChanceUInt64(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37UL, 60UL), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37UL, 60UL);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedChanceFloat(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37f, 60f), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37f, 60f);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedChanceDouble(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37d, 60d), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37d, 60d);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ProbabilityNumeratorInt32(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(819143247), 0.381443298887f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(819143247);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298887f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ProbabilityNumeratorUInt32(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(1638286494U), 0.381443298887f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(1638286494U);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298887f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ProbabilityNumeratorInt64(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(3518193457356310735L), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(3518193457356310735L);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ProbabilityNumeratorUInt64(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(7036386914712621470UL), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(7036386914712621470UL);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ProbabilityNumeratorFloat(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(0.381443298969f), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(0.381443298969f);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ProbabilityNumeratorDouble(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(0.381443298969d), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(0.381443298969d);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedProbabilityDenominatorInt32(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37, 97), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37, 97);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedProbabilityDenominatorUInt32(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37U, 97U), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37U, 97U);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedProbabilityDenominatorInt64(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37L, 97L), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37L, 97L);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedProbabilityDenominatorUInt64(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37UL, 97UL), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37UL, 97UL);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedProbabilityDenominatorFloat(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37f, 97f), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37f, 97f);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void WeightedProbabilityDenominatorDouble(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37d, 97d), 0.381443298969f, 0.02f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37d, 97d);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f, sampleSizePercentage);
		}
	}
}
#endif
