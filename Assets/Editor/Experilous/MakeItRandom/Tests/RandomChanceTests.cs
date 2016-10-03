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

		private void ValidateDistribution(int count, Func<bool> generator, float expectedTrue, float tolerance)
		{
			int trueCount = 0;
			for (int i = 0; i < count; ++i)
			{
				trueCount += generator() ? 1 : 0;
			}

			Assert.LessOrEqual(Mathf.Abs((float)trueCount / count - expectedTrue), tolerance);
		}

		[Test]
		public void UniformChance()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(), 0.5f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator();
			ValidateDistribution(10000, () => generator.Next(), 0.5f, 0.02f);
		}

		[Test]
		public void WeightedChanceInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37, 60), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37, 60);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedChanceUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37U, 60U), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37U, 60U);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedChanceInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37L, 60L), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37L, 60L);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedChanceUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37UL, 60UL), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37UL, 60UL);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedChanceFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37f, 60f), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37f, 60f);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedChanceDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Chance(37d, 60d), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeChanceGenerator(37d, 60d);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void ProbabilityNumeratorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(819143247), 0.381443298887f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(819143247);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298887f, 0.02f);
		}

		[Test]
		public void ProbabilityNumeratorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(1638286494U), 0.381443298887f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(1638286494U);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298887f, 0.02f);
		}

		[Test]
		public void ProbabilityNumeratorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(3518193457356310735L), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(3518193457356310735L);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void ProbabilityNumeratorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(7036386914712621470UL), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(7036386914712621470UL);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void ProbabilityNumeratorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(0.381443298969f), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(0.381443298969f);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void ProbabilityNumeratorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(0.381443298969d), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(0.381443298969d);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedProbabilityDenominatorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37, 97), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37, 97);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedProbabilityDenominatorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37U, 97U), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37U, 97U);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedProbabilityDenominatorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37L, 97L), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37L, 97L);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedProbabilityDenominatorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37UL, 97UL), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37UL, 97UL);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedProbabilityDenominatorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37f, 97f), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37f, 97f);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}

		[Test]
		public void WeightedProbabilityDenominatorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateDistribution(10000, () => random.Probability(37d, 97d), 0.381443298969f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeProbabilityGenerator(37d, 97d);
			ValidateDistribution(10000, () => generator.Next(), 0.381443298969f, 0.02f);
		}
	}
}
#endif
