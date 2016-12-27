/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using System;
using NUnit.Framework;

namespace Experilous.MakeItRandom.Tests
{
	class RandomSampleTests
	{
		private const string seed = "random seed";

		#region Helper Utility Functions

		private void TestDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float expectedMean, float expectedStandardDeviation, float meanTolerance, float standardDeviationTolerance)
		{
			double sum = 0d;
			double squaredSum = 0d;

			for (int i = 0; i < minBatchCount; ++i)
			{
				for (int j = 0; j < batchSize; ++j)
				{
					float sample = f();
					sum += sample;
					sample -= expectedMean;
					squaredSum += sample * sample;
				}
			}

			double iterationCount = (double)minBatchCount * batchSize;
			double measuredMean = sum / iterationCount;
			double measuredStandardDeviation = Math.Sqrt(squaredSum / iterationCount);
			double relativeMeanDifference = Math.Abs(measuredMean - expectedMean) / expectedStandardDeviation;
			double relativeStandardDeviationDifference = Math.Abs(measuredStandardDeviation / expectedStandardDeviation - 1d);
			double smallestRelativeMeanDifference = relativeMeanDifference;
			double smallestRelativeStandardDeviationDifference = relativeStandardDeviationDifference;

			if (relativeMeanDifference <= meanTolerance && relativeStandardDeviationDifference <= standardDeviationTolerance)
			{
				return;
			}

			for (int i = minBatchCount; i < maxBatchCount; ++i)
			{
				for (int j = 0; j < batchSize; ++j)
				{
					float sample = f();
					sum += sample;
					sample -= expectedMean;
					squaredSum += sample * sample;
				}

				iterationCount = (double)(i + 1) * batchSize;
				measuredMean = sum / iterationCount;
				measuredStandardDeviation = Math.Sqrt(squaredSum / iterationCount);
				relativeMeanDifference = Math.Abs(measuredMean - expectedMean) / expectedStandardDeviation;
				relativeStandardDeviationDifference = Math.Abs(measuredStandardDeviation / expectedStandardDeviation - 1d);

				if (relativeMeanDifference <= meanTolerance && relativeStandardDeviationDifference <= standardDeviationTolerance)
				{
					return;
				}

				if (relativeMeanDifference > meanTolerance)
				{
					Assert.Less(relativeMeanDifference, smallestRelativeMeanDifference * 2d, string.Format("Expected mean was {0:F6}, but measured mean was {1:F12}, and the measured error does not appear to be converging to zero after {2} batches; relative mean error is {3:F12} and relative standard deviation error is {4:F12}.", expectedMean, measuredMean, i + 1, relativeMeanDifference, relativeStandardDeviationDifference));
				}

				if (relativeStandardDeviationDifference > standardDeviationTolerance)
				{
					Assert.Less(relativeStandardDeviationDifference, smallestRelativeStandardDeviationDifference * 2d, string.Format("Expected standard deviation was {0:F6}, but measured standard deviation was {1:F12}, and the measured error does not appear to be converging to zero after {2} batches; relative mean error is {3:F12} and relative standard deviation error is {4:F12}.", expectedStandardDeviation, measuredStandardDeviation, i + 1, relativeMeanDifference, relativeStandardDeviationDifference));
				}
			}

			iterationCount = (double)maxBatchCount * batchSize;
			measuredMean = sum / iterationCount;
			measuredStandardDeviation = Math.Sqrt(squaredSum / iterationCount);
			relativeMeanDifference = Math.Abs(measuredMean - expectedMean) / expectedStandardDeviation;
			relativeStandardDeviationDifference = Math.Abs(measuredStandardDeviation / expectedStandardDeviation - 1d);

			Assert.LessOrEqual(relativeMeanDifference, meanTolerance, string.Format("Expected mean was {0:F6}, but measured mean was {1:F12}.", expectedMean, measuredMean));
			Assert.LessOrEqual(relativeStandardDeviationDifference, standardDeviationTolerance, string.Format("Expected standard deviation was {0:F6}, but measured standard deviation was {1:F12}.", expectedStandardDeviation, measuredStandardDeviation));
		}

		private void TestDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double expectedMean, double expectedStandardDeviation, double meanTolerance, double standardDeviationTolerance)
		{
			double sum = 0d;
			double squaredSum = 0d;

			for (int i = 0; i < minBatchCount; ++i)
			{
				for (int j = 0; j < batchSize; ++j)
				{
					double sample = f();
					sum += sample;
					sample -= expectedMean;
					squaredSum += sample * sample;
				}
			}

			double iterationCount = (double)minBatchCount * batchSize;
			double measuredMean = sum / iterationCount;
			double measuredStandardDeviation = Math.Sqrt(squaredSum / iterationCount);
			double relativeMeanDifference = Math.Abs(measuredMean - expectedMean) / expectedStandardDeviation;
			double relativeStandardDeviationDifference = Math.Abs(measuredStandardDeviation / expectedStandardDeviation - 1d);
			double smallestRelativeMeanDifference = relativeMeanDifference;
			double smallestRelativeStandardDeviationDifference = relativeStandardDeviationDifference;

			if (relativeMeanDifference <= meanTolerance && relativeStandardDeviationDifference <= standardDeviationTolerance)
			{
				return;
			}

			for (int i = minBatchCount; i < maxBatchCount; ++i)
			{
				for (int j = 0; j < batchSize; ++j)
				{
					double sample = f();
					sum += sample;
					sample -= expectedMean;
					squaredSum += sample * sample;
				}

				iterationCount = (double)(i + 1) * batchSize;
				measuredMean = sum / iterationCount;
				measuredStandardDeviation = Math.Sqrt(squaredSum / iterationCount);
				relativeMeanDifference = Math.Abs(measuredMean - expectedMean) / expectedStandardDeviation;
				relativeStandardDeviationDifference = Math.Abs(measuredStandardDeviation / expectedStandardDeviation - 1d);

				if (relativeMeanDifference <= meanTolerance && relativeStandardDeviationDifference <= standardDeviationTolerance)
				{
					return;
				}

				if (relativeMeanDifference > meanTolerance)
				{
					Assert.Less(relativeMeanDifference, smallestRelativeMeanDifference * 2d, string.Format("Expected mean was {0:F6}, but measured mean was {1:F12}, and the measured error does not appear to be converging to zero after {2} batches; relative mean error is {3:F12} and relative standard deviation error is {4:F12}.", expectedMean, measuredMean, i + 1, relativeMeanDifference, relativeStandardDeviationDifference));
				}

				if (relativeStandardDeviationDifference > standardDeviationTolerance)
				{
					Assert.Less(relativeStandardDeviationDifference, smallestRelativeStandardDeviationDifference * 2d, string.Format("Expected standard deviation was {0:F6}, but measured standard deviation was {1:F12}, and the measured error does not appear to be converging to zero after {2} batches; relative mean error is {3:F12} and relative standard deviation error is {4:F12}.", expectedStandardDeviation, measuredStandardDeviation, i + 1, relativeMeanDifference, relativeStandardDeviationDifference));
				}
			}

			iterationCount = (double)maxBatchCount * batchSize;
			measuredMean = sum / iterationCount;
			measuredStandardDeviation = Math.Sqrt(squaredSum / iterationCount);
			relativeMeanDifference = Math.Abs(measuredMean - expectedMean) / expectedStandardDeviation;
			relativeStandardDeviationDifference = Math.Abs(measuredStandardDeviation / expectedStandardDeviation - 1d);

			Assert.LessOrEqual(relativeMeanDifference, meanTolerance, string.Format("Expected mean was {0:F6}, but measured mean was {1:F12}.", expectedMean, measuredMean));
			Assert.LessOrEqual(relativeStandardDeviationDifference, standardDeviationTolerance, string.Format("Expected standard deviation was {0:F6}, but measured standard deviation was {1:F12}.", expectedStandardDeviation, measuredStandardDeviation));
		}

		private void TestDistributionRange(Func<float> f, int iterationCount, float min, float max)
		{
			for (int i = 0; i < iterationCount; ++i)
			{
				float sample = f();
				Assert.GreaterOrEqual(sample, min);
				Assert.LessOrEqual(sample, max);
			}
		}

		private void TestDistributionRange(Func<double> f, int iterationCount, double min, double max)
		{
			for (int i = 0; i < iterationCount; ++i)
			{
				double sample = f();
				Assert.GreaterOrEqual(sample, min);
				Assert.LessOrEqual(sample, max);
			}
		}

		#endregion

		#region Uniform Distribution

		private void TestUniformDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float x0, float x1, float meanTolerance, float stdDevTolerance)
		{
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, (x0 + x1) * 0.5f, (x1 - x0) * 0.2886751346f, meanTolerance, stdDevTolerance);
		}

		private void TestUniformDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double x0, double x1, double meanTolerance, double stdDevTolerance)
		{
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, (x0 + x1) * 0.5d, (x1 - x0) * 0.28867513459481288d, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestUniformDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestUniformDistribution(() => random.UniformSample(0f, 1f), 50000, 2, 20, 0f, 1f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestUniformDistribution(() => random.UniformSample(-1f, 5f), 50000, 2, 20, -1f, 5f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestUniformDistribution(() => random.UniformSample(-172.3f, -0.2346f), 50000, 2, 20, -172.3f, -0.2346f, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.UniformSample(0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.UniformSample(0f, -1f));
		}

		[Test]
		public void TestUniformDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeUniformSampleGenerator(0f, 1f);
			TestUniformDistribution(() => generator.Next(), 50000, 2, 20, 0f, 1f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeUniformSampleGenerator(-1f, 5f);
			TestUniformDistribution(() => generator.Next(), 50000, 2, 20, -1f, 5f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeUniformSampleGenerator(-172.3f, -0.2346f);
			TestUniformDistribution(() => generator.Next(), 50000, 2, 20, -172.3f, -0.2346f, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeUniformSampleGenerator(0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeUniformSampleGenerator(0f, -1f));
		}

		[Test]
		public void TestUniformDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestUniformDistribution(() => random.UniformSample(0d, 1d), 50000, 2, 20, 0d, 1d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestUniformDistribution(() => random.UniformSample(-1d, 5d), 50000, 2, 20, -1d, 5d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestUniformDistribution(() => random.UniformSample(-172.3d, -0.2346d), 50000, 2, 20, -172.3d, -0.2346d, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.UniformSample(0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.UniformSample(0d, -1d));
		}

		[Test]
		public void TestUniformDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeUniformSampleGenerator(0d, 1d);
			TestUniformDistribution(() => generator.Next(), 50000, 2, 20, 0d, 1d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeUniformSampleGenerator(-1d, 5d);
			TestUniformDistribution(() => generator.Next(), 50000, 2, 20, -1d, 5d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeUniformSampleGenerator(-172.3d, -0.2346d);
			TestUniformDistribution(() => generator.Next(), 50000, 2, 20, -172.3d, -0.2346d, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeUniformSampleGenerator(0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeUniformSampleGenerator(0d, -1d));
		}

		#endregion

		#region Normal Distribution

		private void TestNormalDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float mean, float standardDeviation, float meanTolerance, float stdDevTolerance)
		{
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestNormalDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double mean, double standardDeviation, double meanTolerance, double stdDevTolerance)
		{
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestNormalDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestNormalDistribution(() => random.NormalSample(0f, 1f), 50000, 2, 20, 0f, 1f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestNormalDistribution(() => random.NormalSample(2f, 5f), 50000, 2, 20, 2f, 5f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestNormalDistribution(() => random.NormalSample(-172.3f, 0.2346f), 50000, 2, 20, -172.3f, 0.2346f, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.NormalSample(0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.NormalSample(0f, -1f));
		}

		[Test]
		public void TestNormalDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(0f, 1f);
			TestNormalDistribution(() => generator.Next(), 50000, 2, 20, 0f, 1f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(2f, 5f);
			TestNormalDistribution(() => generator.Next(), 50000, 2, 20, 2f, 5f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(-172.3f, 0.2346f);
			TestNormalDistribution(() => generator.Next(), 50000, 2, 20, -172.3f, 0.2346f, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeNormalSampleGenerator(0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeNormalSampleGenerator(0f, -1f));
		}

		[Test]
		public void TestNormalDistribution_ConstrainedRange_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestDistributionRange(() => random.NormalSample(0f, 1f, -2f, 3f), 100000, -2f, 3f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.NormalSample(7f, 3f, 8f, 20f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.NormalSample(7f, 3f, -15f, 5f));
			Assert.Throws<ArgumentException>(() => random.NormalSample(7f, 3f, 6f, 8f));
		}

		[Test]
		public void TestNormalDistribution_SampleGenerator_ContrainedRange_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(0f, 1f, -2f, 3f);
			TestDistributionRange(() => generator.Next(), 100000, -2f, 3f);

			Assert.Throws<ArgumentOutOfRangeException>(() => MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(7f, 3f, 8f, 20f));
			Assert.Throws<ArgumentOutOfRangeException>(() => MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(7f, 3f, -15f, 5f));
			Assert.Throws<ArgumentException>(() => MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(7f, 3f, 6f, 8f));
		}

		[Test]
		public void TestNormalDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestNormalDistribution(() => random.NormalSample(0d, 1d), 50000, 2, 20, 0d, 1d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestNormalDistribution(() => random.NormalSample(2d, 5d), 50000, 2, 20, 2d, 5d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestNormalDistribution(() => random.NormalSample(-172.3d, 0.2346d), 50000, 2, 20, -172.3d, 0.2346d, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.NormalSample(0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.NormalSample(0d, -1d));
		}

		[Test]
		public void TestNormalDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(0d, 1d);
			TestNormalDistribution(() => generator.Next(), 50000, 2, 20, 0d, 1d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(2d, 5d);
			TestNormalDistribution(() => generator.Next(), 50000, 2, 20, 2d, 5d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(-172.3d, 0.2346d);
			TestNormalDistribution(() => generator.Next(), 50000, 2, 20, -172.3d, 0.2346d, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeNormalSampleGenerator(0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeNormalSampleGenerator(0d, -1d));
		}

		[Test]
		public void TestNormalDistribution_ConstrainedRange_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestDistributionRange(() => random.NormalSample(0d, 1d, -2d, 3d), 100000, -2d, 3d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.NormalSample(7d, 3d, 8d, 20d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.NormalSample(7d, 3d, -15d, 5d));
			Assert.Throws<ArgumentException>(() => random.NormalSample(7d, 3d, 6d, 8d));
		}

		[Test]
		public void TestNormalDistribution_SampleGenerator_ContrainedRange_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(0d, 1d, -2d, 3d);
			TestDistributionRange(() => generator.Next(), 100000, -2d, 3d);

			Assert.Throws<ArgumentOutOfRangeException>(() => MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(7d, 3d, 8d, 20d));
			Assert.Throws<ArgumentOutOfRangeException>(() => MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(7d, 3d, -15d, 5d));
			Assert.Throws<ArgumentException>(() => MIRandom.CreateStandard(seed).MakeNormalSampleGenerator(7d, 3d, 6d, 8d));
		}

		#endregion

		#region Exponential Distribution

		private void TestExponentialDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float eventRate, float meanTolerance, float stdDevTolerance)
		{
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, 1f / eventRate, 1f / eventRate, meanTolerance, stdDevTolerance);
		}

		private void TestExponentialDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double eventRate, double meanTolerance, double stdDevTolerance)
		{
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, 1d / eventRate, 1d / eventRate, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestExponentialDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestExponentialDistribution(() => random.ExponentialSample(1f), 50000, 2, 20, 1f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestExponentialDistribution(() => random.ExponentialSample(3.21f), 50000, 2, 20, 3.21f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestExponentialDistribution(() => random.ExponentialSample(0.474f), 50000, 2, 20, 0.474f, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.ExponentialSample(0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.ExponentialSample(0f, -1f));
		}

		[Test]
		public void TestExponentialDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(1f);
			TestExponentialDistribution(() => generator.Next(), 50000, 2, 20, 1f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(3.21f);
			TestExponentialDistribution(() => generator.Next(), 50000, 2, 20, 3.21f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(0.474f);
			TestExponentialDistribution(() => generator.Next(), 50000, 2, 20, 0.474f, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeExponentialSampleGenerator(0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeExponentialSampleGenerator(0f, -1f));
		}

		[Test]
		public void TestExponentialDistribution_ConstrainedRange_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestDistributionRange(() => random.ExponentialSample(1f, 4f), 100000, 0f, 4f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.ExponentialSample(1.1f, -3f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.ExponentialSample(1.1f, 0.3f));
		}

		[Test]
		public void TestExponentialDistribution_SampleGenerator_ContrainedRange_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(1f, 4f);
			TestDistributionRange(() => generator.Next(), 100000, 0f, 4f);

			Assert.Throws<ArgumentOutOfRangeException>(() => MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(1.1f, -3f));
			Assert.Throws<ArgumentOutOfRangeException>(() => MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(1.1f, 0.3f));
		}

		[Test]
		public void TestExponentialDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestExponentialDistribution(() => random.ExponentialSample(1d), 50000, 2, 20, 1d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestExponentialDistribution(() => random.ExponentialSample(3.21d), 50000, 2, 20, 3.21d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestExponentialDistribution(() => random.ExponentialSample(0.474d), 50000, 2, 20, 0.474d, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.ExponentialSample(0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.ExponentialSample(0d, -1d));
		}

		[Test]
		public void TestExponentialDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(1d);
			TestExponentialDistribution(() => generator.Next(), 50000, 2, 20, 1d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(3.21d);
			TestExponentialDistribution(() => generator.Next(), 50000, 2, 20, 3.21d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(0.474d);
			TestExponentialDistribution(() => generator.Next(), 50000, 2, 20, 0.474d, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeExponentialSampleGenerator(0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeExponentialSampleGenerator(0d, -1d));
		}

		[Test]
		public void TestExponentialDistribution_ConstrainedRange_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestDistributionRange(() => random.ExponentialSample(1d, 4d), 100000, 0f, 4d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.ExponentialSample(1.1d, -3d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.ExponentialSample(1.1d, 0.3d));
		}

		[Test]
		public void TestExponentialDistribution_SampleGenerator_ContrainedRange_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(1d, 4d);
			TestDistributionRange(() => generator.Next(), 100000, 0f, 4d);

			Assert.Throws<ArgumentOutOfRangeException>(() => MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(1.1d, -3d));
			Assert.Throws<ArgumentOutOfRangeException>(() => MIRandom.CreateStandard(seed).MakeExponentialSampleGenerator(1.1d, 0.3d));
		}

		#endregion

		#region Triangular Distribution

		private void TestTriangularDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float lower, float mode, float upper, float meanTolerance, float stdDevTolerance)
		{
			float a = lower;
			float b = mode;
			float c = upper;
			float mean = (a + b + c) / 3f;
			float variance = (a * a + b * b + c * c - a * b - a * c - b * c) / 18f;
			Assert.GreaterOrEqual(variance, 0f);
			float standardDeviation = Mathf.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestTriangularDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double lower, double mode, double upper, double meanTolerance, double stdDevTolerance)
		{
			double a = lower;
			double b = mode;
			double c = upper;
			double mean = (a + b + c) / 3f;
			double variance = (a * a + b * b + c * c - a * b - a * c - b * c) / 18f;
			Assert.GreaterOrEqual(variance, 0d);
			double standardDeviation = Math.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestTriangularDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestTriangularDistribution(() => random.TriangularSample(-1f, 0f, 1f), 50000, 2, 20, -1f, 0f, 1f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestTriangularDistribution(() => random.TriangularSample(-1f, 1f, 5f), 50000, 2, 20, -1f, 1f, 5f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestTriangularDistribution(() => random.TriangularSample(-172.3f, -9.803f, -0.2346f), 50000, 2, 20, -172.3f, -9.803f, -0.2346f, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0f, 0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0f, 0f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0f, 1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0f, -1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0f, 1f, -1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0f, 2f, 1f));
		}

		[Test]
		public void TestTriangularDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeTriangularSampleGenerator(-1f, 0f, 1f);
			TestTriangularDistribution(() => generator.Next(), 50000, 2, 20, -1f, 0f, 1f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeTriangularSampleGenerator(-1f, 1f, 5f);
			TestTriangularDistribution(() => generator.Next(), 50000, 2, 20, -1f, 1f, 5f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeTriangularSampleGenerator(-172.3f, -9.803f, -0.2346f);
			TestTriangularDistribution(() => generator.Next(), 50000, 2, 20, -172.3f, -9.803f, -0.2346f, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0f, 0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0f, 0f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0f, 1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0f, -1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0f, 1f, -1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0f, 2f, 1f));
		}

		[Test]
		public void TestTriangularDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestTriangularDistribution(() => random.TriangularSample(-1d, 0d, 1d), 50000, 2, 20, -1d, 0d, 1d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestTriangularDistribution(() => random.TriangularSample(-1d, 1d, 5d), 50000, 2, 20, -1d, 1d, 5d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestTriangularDistribution(() => random.TriangularSample(-172.3d, -9.803d, -0.2346d), 50000, 2, 20, -172.3d, -9.803d, -0.2346d, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0d, 0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0d, 0d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0d, 1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0d, -1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0d, 1d, -1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TriangularSample(0d, 2d, 1d));
		}

		[Test]
		public void TestTriangularDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeTriangularSampleGenerator(-1d, 0d, 1d);
			TestTriangularDistribution(() => generator.Next(), 50000, 2, 20, -1d, 0d, 1d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeTriangularSampleGenerator(-1d, 1d, 5d);
			TestTriangularDistribution(() => generator.Next(), 50000, 2, 20, -1d, 1d, 5d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeTriangularSampleGenerator(-172.3d, -9.803d, -0.2346d);
			TestTriangularDistribution(() => generator.Next(), 50000, 2, 20, -172.3d, -9.803d, -0.2346d, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0d, 0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0d, 0d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0d, 1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0d, -1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0d, 1d, -1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTriangularSampleGenerator(0d, 2d, 1d));
		}

		#endregion

		#region Trapezoidal Distribution

		private void TestTrapezoidalDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float x0, float x1, float x2, float x3, float meanTolerance, float stdDevTolerance)
		{
			float x0sqr = x0 * x0;
			float x1sqr = x1 * x1;
			float x2sqr = x2 * x2;
			float x3sqr = x3 * x3;
			float x0cub = x0sqr * x0;
			float x1cub = x1sqr * x1;
			float x2cub = x2sqr * x2;
			float x3cub = x3sqr * x3;
			float n = (x3 + x2) - (x1 + x0);
			float mean = ((x2sqr + x2 * x3 + x3sqr) - (x0sqr + x0 * x1 + x1sqr)) / (3f * n);
			float variance = ((x2cub + x2sqr * x3 + x2 * x3sqr + x3cub) - (x0cub + x0sqr * x1 + x0 * x1sqr + x1cub)) / (6f * n) - mean * mean;
			Assert.GreaterOrEqual(variance, 0f);
			float standardDeviation = Mathf.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestTrapezoidalDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double x0, double x1, double x2, double x3, double meanTolerance, double stdDevTolerance)
		{
			double x0sqr = x0 * x0;
			double x1sqr = x1 * x1;
			double x2sqr = x2 * x2;
			double x3sqr = x3 * x3;
			double x0cub = x0sqr * x0;
			double x1cub = x1sqr * x1;
			double x2cub = x2sqr * x2;
			double x3cub = x3sqr * x3;
			double n = (x3 + x2) - (x1 + x0);
			double mean = ((x2sqr + x2 * x3 + x3sqr) - (x0sqr + x0 * x1 + x1sqr)) / (3d * n);
			double variance = ((x2cub + x2sqr * x3 + x2 * x3sqr + x3cub) - (x0cub + x0sqr * x1 + x0 * x1sqr + x1cub)) / (6d * n) - mean * mean;
			Assert.GreaterOrEqual(variance, 0d);
			double standardDeviation = Math.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestTrapezoidalDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestTrapezoidalDistribution(() => random.TrapezoidalSample(-2f, -1f, 1f, 2f), 50000, 2, 20, -2f, -1f, 1f, 2f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestTrapezoidalDistribution(() => random.TrapezoidalSample(-1f, 1f, 5f, 13f), 50000, 2, 20, -1f, 1f, 5f, 13f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestTrapezoidalDistribution(() => random.TrapezoidalSample(-172.3f, -60.60f, -9.803f, -0.2346f), 50000, 2, 20, -172.3f, -60.60f, -9.803f, -0.2346f, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0f, 0f, 0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0f, 0f, 0f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0f, 0f, 1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0f, 1f, 1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0f, 1f, 2f, 2f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0f, 1f, 1f, 2f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0f, 0f, 1f, 2f));

			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2f, 1f, 6f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2f, 4f, 3f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2f, 4f, 6f, 5f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(5f, 4f, 6f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2f, 7f, 6f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2f, 4f, 9f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2f, 4f, 1f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2f, 4f, 6f, 3f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(7f, 4f, 6f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2f, 9f, 6f, 8f));
		}

		[Test]
		public void TestTrapezoidalDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeTrapezoidalSampleGenerator(-2f, -1f, 1f, 2f);
			TestTrapezoidalDistribution(() => generator.Next(), 50000, 2, 20, -2f, -1f, 1f, 2f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeTrapezoidalSampleGenerator(-1f, 1f, 5f, 13f);
			TestTrapezoidalDistribution(() => generator.Next(), 50000, 2, 20, -1f, 1f, 5f, 13f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeTrapezoidalSampleGenerator(-172.3f, -60.60f, -9.803f, -0.2346f);
			TestTrapezoidalDistribution(() => generator.Next(), 50000, 2, 20, -172.3f, -60.60f, -9.803f, -0.2346f, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0f, 0f, 0f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0f, 0f, 0f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0f, 0f, 1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0f, 1f, 1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0f, 1f, 2f, 2f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0f, 1f, 1f, 2f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0f, 0f, 1f, 2f));

			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2f, 1f, 6f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2f, 4f, 3f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2f, 4f, 6f, 5f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(5f, 4f, 6f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2f, 7f, 6f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2f, 4f, 9f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2f, 4f, 1f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2f, 4f, 6f, 3f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(7f, 4f, 6f, 8f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2f, 9f, 6f, 8f));
		}

		[Test]
		public void TestTrapezoidalDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestTrapezoidalDistribution(() => random.TrapezoidalSample(-2d, -1d, 1d, 2d), 50000, 2, 20, -2d, -1d, 1d, 2d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestTrapezoidalDistribution(() => random.TrapezoidalSample(-1d, 1d, 5d, 13d), 50000, 2, 20, -1d, 1d, 5d, 13d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestTrapezoidalDistribution(() => random.TrapezoidalSample(-172.3d, -60.60d, -9.803d, -0.2346d), 50000, 2, 20, -172.3d, -60.60d, -9.803d, -0.2346d, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0d, 0d, 0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0d, 0d, 0d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0d, 0d, 1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0d, 1d, 1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0d, 1d, 2d, 2d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0d, 1d, 1d, 2d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(0d, 0d, 1d, 2d));

			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2d, 1d, 6d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2d, 4d, 3d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2d, 4d, 6d, 5d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(5d, 4d, 6d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2d, 7d, 6d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2d, 4d, 9d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2d, 4d, 1d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2d, 4d, 6d, 3d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(7d, 4d, 6d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.TrapezoidalSample(2d, 9d, 6d, 8d));
		}

		[Test]
		public void TestTrapezoidalDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeTrapezoidalSampleGenerator(-2d, -1d, 1d, 2d);
			TestTrapezoidalDistribution(() => generator.Next(), 50000, 2, 20, -2d, -1d, 1d, 2d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeTrapezoidalSampleGenerator(-1d, 1d, 5d, 13d);
			TestTrapezoidalDistribution(() => generator.Next(), 50000, 2, 20, -1d, 1d, 5d, 13d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeTrapezoidalSampleGenerator(-172.3d, -60.60d, -9.803d, -0.2346d);
			TestTrapezoidalDistribution(() => generator.Next(), 50000, 2, 20, -172.3d, -60.60d, -9.803d, -0.2346d, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0d, 0d, 0d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0d, 0d, 0d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0d, 0d, 1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0d, 1d, 1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0d, 1d, 2d, 2d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0d, 1d, 1d, 2d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(0d, 0d, 1d, 2d));

			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2d, 1d, 6d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2d, 4d, 3d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2d, 4d, 6d, 5d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(5d, 4d, 6d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2d, 7d, 6d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2d, 4d, 9d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2d, 4d, 1d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2d, 4d, 6d, 3d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(7d, 4d, 6d, 8d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeTrapezoidalSampleGenerator(2d, 9d, 6d, 8d));
		}

		#endregion

		#region Linear Distribution

		private void TestLinearDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float x0, float y0, float x1, float y1, float meanTolerance, float stdDevTolerance)
		{
			float dx = x1 - x0;
			float dy = y1 - y0;
			float n = x1 * y0 - x0 * y1;
			float area = 0.5f * dx * (y0 + y1);
			float x2 = x0 + x1;
			float x3 = x0 * x0 + x1 * x1;
			float x4 = x3 + x0 * x1;
			float mean = (x4 * dy / 3f + x2 * n / 2f) / area;
			float variance = (x3 * x2 * dy / 4f + x4 * n / 3f) / area - mean * mean;
			Assert.GreaterOrEqual(variance, 0f);
			float standardDeviation = Mathf.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestLinearDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double x0, double y0, double x1, double y1, double meanTolerance, double stdDevTolerance)
		{
			double dx = x1 - x0;
			double dy = y1 - y0;
			double n = x1 * y0 - x0 * y1;
			double area = 0.5d * dx * (y0 + y1);
			double x2 = x0 + x1;
			double x3 = x0 * x0 + x1 * x1;
			double x4 = x3 + x0 * x1;
			double mean = (x4 * dy / 3d + x2 * n / 2d) / area;
			double variance = (x3 * x2 * dy / 4d + x4 * n / 3d) / area - mean * mean;
			Assert.GreaterOrEqual(variance, 0d);
			double standardDeviation = Math.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestLinearDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(2f, 1f, 4f, 2f), 50000, 2, 20, 2f, 1f, 4f, 2f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(-1f, 4f, 5f, 4f), 50000, 2, 20, -1f, 4f, 5f, 4f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(-172.3f, 60.60f, -96.83f, 0.2346f), 50000, 2, 20, -172.3f, 60.60f, -96.83f, 0.2346f, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(0f, 1f, 0f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(1f, 1f, 0f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(0f, -1f, 1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(0f, 1f, 1f, -1f));
			Assert.Throws<ArgumentException>(() => random.LinearSample(0f, 0f, 1f, 0f));
		}

		[Test]
		public void TestLinearDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(2f, 1f, 4f, 2f);
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, 2f, 1f, 4f, 2f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(-1f, 4f, 5f, 4f);
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, -1f, 4f, 5f, 4f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(-172.3f, 60.60f, -96.83f, 0.2346f);
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, -172.3f, 60.60f, -96.83f, 0.2346f, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(0f, 1f, 0f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(1f, 1f, 0f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(0f, -1f, 1f, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(0f, 1f, 1f, -1f));
			Assert.Throws<ArgumentException>(() => generator = random.MakeLinearSampleGenerator(0f, 0f, 1f, 0f));
		}

		[Test]
		public void TestLinearDistribution_Vector2()
		{
			var random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(new Vector2(2f, 1f), new Vector2(4f, 2f)), 50000, 2, 20, 2f, 1f, 4f, 2f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(new Vector2(-1f, 4f), new Vector2(5f, 4f)), 50000, 2, 20, -1f, 4f, 5f, 4f, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(new Vector2(-172.3f, 60.60f), new Vector2(-96.83f, 0.2346f)), 50000, 2, 20, -172.3f, 60.60f, -96.83f, 0.2346f, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(new Vector2(0f, 1f), new Vector2(0f, 1f)));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(new Vector2(1f, 1f), new Vector2(0f, 1f)));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(new Vector2(0f, -1f), new Vector2(1f, 1f)));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(new Vector2(0f, 1f), new Vector2(1f, -1f)));
			Assert.Throws<ArgumentException>(() => random.LinearSample(new Vector2(0f, 0f), new Vector2(1f, 0f)));
		}

		[Test]
		public void TestLinearDistribution_SampleGenerator_Vector2()
		{
			var generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(new Vector2(2f, 1f), new Vector2(4f, 2f));
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, 2f, 1f, 4f, 2f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(new Vector2(-1f, 4f), new Vector2(5f, 4f));
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, -1f, 4f, 5f, 4f, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(new Vector2(-172.3f, 60.60f), new Vector2(-96.83f, 0.2346f));
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, -172.3f, 60.60f, -96.83f, 0.2346f, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(new Vector2(0f, 1f), new Vector2(0f, 1f)));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(new Vector2(1f, 1f), new Vector2(0f, 1f)));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(new Vector2(0f, -1f), new Vector2(1f, 1f)));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(new Vector2(0f, 1f), new Vector2(1f, -1f)));
			Assert.Throws<ArgumentException>(() => generator = random.MakeLinearSampleGenerator(new Vector2(0f, 0f), new Vector2(1f, 0f)));
		}

		[Test]
		public void TestLinearDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(2d, 1d, 4d, 2d), 50000, 2, 20, 2d, 1d, 4d, 2d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(-1d, 4d, 5d, 4d), 50000, 2, 20, -1d, 4d, 5d, 4d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestLinearDistribution(() => random.LinearSample(-172.3d, 60.60d, -96.83d, 0.2346d), 50000, 2, 20, -172.3d, 60.60d, -96.83d, 0.2346d, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(0d, 1d, 0d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(1d, 1d, 0d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(0d, -1d, 1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.LinearSample(0d, 1d, 1d, -1d));
			Assert.Throws<ArgumentException>(() => random.LinearSample(0d, 0d, 1d, 0d));
		}

		[Test]
		public void TestLinearDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(2d, 1d, 4d, 2d);
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, 2d, 1d, 4d, 2d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(-1d, 4d, 5d, 4d);
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, -1d, 4d, 5d, 4d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeLinearSampleGenerator(-172.3d, 60.60d, -96.83d, 0.2346d);
			TestLinearDistribution(() => generator.Next(), 50000, 2, 20, -172.3d, 60.60d, -96.83d, 0.2346d, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(0d, 1d, 0d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(1d, 1d, 0d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(0d, -1d, 1d, 1d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeLinearSampleGenerator(0d, 1d, 1d, -1d));
			Assert.Throws<ArgumentException>(() => generator = random.MakeLinearSampleGenerator(0d, 0d, 1d, 0d));
		}

		#endregion

		#region Hermite Distribution

		private void TestHermiteDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float x0, float y0, float m0, float x1, float y1, float m1, float meanTolerance, float stdDevTolerance)
		{
			float dx = x1 - x0;
			float dy = y1 - y0;
			float a = (m0 + m1) * dx - 2f * dy;
			float b = 3f * dy - (2f * m0 + m1) * dx;
			float c = m0 * dx;
			float d = y0;
			float area = a / 4f + b / 3f + c / 2f + d;
			float mean = (a / 5f + b / 4f + c / 3f + d / 2f) / area;
			float variance = (a / 6f + b / 5f + c / 4f + d / 3f) / area - mean * mean;
			Assert.GreaterOrEqual(variance, 0f);
			float standardDeviation = Mathf.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean * dx + x0, standardDeviation * dx, meanTolerance, stdDevTolerance);
		}

		private void TestHermiteDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double x0, double y0, double m0, double x1, double y1, double m1, double meanTolerance, double stdDevTolerance)
		{
			double dx = x1 - x0;
			double dy = y1 - y0;
			double a = (m0 + m1) * dx - 2d * dy;
			double b = 3d * dy - (2d * m0 + m1) * dx;
			double c = m0 * dx;
			double d = y0;
			double area = a / 4d + b / 3d + c / 2d + d;
			double mean = (a / 5d + b / 4d + c / 3d + d / 2d) / area;
			double variance = (a / 6d + b / 5d + c / 4d + d / 3d) / area - mean * mean;
			Assert.GreaterOrEqual(variance, 0d);
			double standardDeviation = Math.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean * dx + x0, standardDeviation * dx, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestHermiteDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(2f, 1f, 1f, 4f, 2f, -1f), 50000, 2, 20, 2f, 1f, 1f, 4f, 2f, -1f, 0.005d, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(-1f, 4f, 2f, 5f, 4f, 1f), 50000, 2, 20, -1f, 4f, 2f, 5f, 4f, 1f, 0.005d, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(-172.3f, 60.60f, -0.07283f, -96.83f, 0.2346f, -0.2481f), 50000, 2, 20, -172.3f, 60.60f, -0.07283f, -96.83f, 0.2346f, -0.2481f, 0.005d, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0f, 1f, 0f, 0f, 1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(1f, 1f, 0f, 0f, 1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0f, -1f, 0f, 1f, 1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0f, 1f, 0f, 1f, -1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0f, 0f, -1f, 1f, 1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0f, 1f, 0f, 1f, 0f, 1f));
			Assert.Throws<ArgumentException>(() => random.HermiteSample(0f, 0f, 0f, 1f, 0f, 0f));
			Assert.DoesNotThrow(() => random.HermiteSample(0f, 0f, 1f, 1f, 0f, -1f));
		}

		[Test]
		public void TestHermiteDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(2f, 1f, 1f, 4f, 2f, -1f);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, 2f, 1f, 1f, 4f, 2f, -1f, 0.005d, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(-1f, 4f, 2f, 5f, 4f, 1f);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, -1f, 4f, 2f, 5f, 4f, 1f, 0.005d, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(-172.3f, 60.60f, -0.07283f, -96.83f, 0.2346f, -0.2481f);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, -172.3f, 60.60f, -0.07283f, -96.83f, 0.2346f, -0.2481f, 0.005d, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0f, 1f, 0f, 0f, 1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(1f, 1f, 0f, 0f, 1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0f, -1f, 0f, 1f, 1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0f, 1f, 0f, 1f, -1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0f, 0f, -1f, 1f, 1f, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0f, 1f, 0f, 1f, 0f, 1f));
			Assert.Throws<ArgumentException>(() => generator = random.MakeHermiteSampleGenerator(0f, 0f, 0f, 1f, 0f, 0f));
			Assert.DoesNotThrow(() => generator = random.MakeHermiteSampleGenerator(0f, 0f, 1f, 1f, 0f, -1f));
		}

		[Test]
		public void TestHermiteDistribution_Vector2()
		{
			var random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(new Vector2(2f, 1f), 1f, new Vector2(4f, 2f), -1f), 50000, 2, 20, 2f, 1f, 1f, 4f, 2f, -1f, 0.005d, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(new Vector2(-1f, 4f), 2f, new Vector2(5f, 4f), 1f), 50000, 2, 20, -1f, 4f, 2f, 5f, 4f, 1f, 0.005d, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(new Vector2(-172.3f, 60.60f), -0.07283f, new Vector2(-96.83f, 0.2346f), -0.2481f), 50000, 2, 20, -172.3f, 60.60f, -0.07283f, -96.83f, 0.2346f, -0.2481f, 0.005d, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(new Vector2(0f, 1f), 0f, new Vector2(0f, 1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(new Vector2(1f, 1f), 0f, new Vector2(0f, 1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(new Vector2(0f, -1f), 0f, new Vector2(1f, 1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(new Vector2(0f, 1f), 0f, new Vector2(1f, -1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(new Vector2(0f, 0f), -1f, new Vector2(1f, 1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(new Vector2(0f, 1f), 0f, new Vector2(1f, 0f), 1f));
			Assert.Throws<ArgumentException>(() => random.HermiteSample(new Vector2(0f, 0f), 0f, new Vector2(1f, 0f), 0f));
			Assert.DoesNotThrow(() => random.HermiteSample(new Vector2(0f, 0f), 1f, new Vector2(1f, 0f), -1f));
		}

		[Test]
		public void TestHermiteDistribution_SampleGenerator_Vector2()
		{
			var generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(new Vector2(2f, 1f), 1f, new Vector2(4f, 2f), -1f);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, 2f, 1f, 1f, 4f, 2f, -1f, 0.005d, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(new Vector2(-1f, 4f), 2f, new Vector2(5f, 4f), 1f);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, -1f, 4f, 2f, 5f, 4f, 1f, 0.005d, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(new Vector2(-172.3f, 60.60f), -0.07283f, new Vector2(-96.83f, 0.2346f), -0.2481f);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, -172.3f, 60.60f, -0.07283f, -96.83f, 0.2346f, -0.2481f, 0.005d, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(new Vector2(0f, 1f), 0f, new Vector2(0f, 1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(new Vector2(1f, 1f), 0f, new Vector2(0f, 1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(new Vector2(0f, -1f), 0f, new Vector2(1f, 1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(new Vector2(0f, 1f), 0f, new Vector2(1f, -1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(new Vector2(0f, 0f), -1f, new Vector2(1f, 1f), 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(new Vector2(0f, 1f), 0f, new Vector2(1f, 0f), 1f));
			Assert.Throws<ArgumentException>(() => generator = random.MakeHermiteSampleGenerator(new Vector2(0f, 0f), 0f, new Vector2(1f, 0f), 0f));
			Assert.DoesNotThrow(() => generator = random.MakeHermiteSampleGenerator(new Vector2(0f, 0f), 1f, new Vector2(1f, 0f), -1f));
		}

		[Test]
		public void TestHermiteDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(2d, 1d, 1d, 4d, 2d, -1d), 50000, 2, 20, 2d, 1d, 1d, 4d, 2d, -1d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(-1d, 4d, 2d, 5d, 4d, 1d), 50000, 2, 20, -1d, 4d, 2d, 5d, 4d, 1d, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestHermiteDistribution(() => random.HermiteSample(-172.3d, 60.60d, -0.07283d, -96.83d, 0.2346d, -0.2481d), 50000, 2, 20, -172.3d, 60.60d, -0.07283d, -96.83d, 0.2346d, -0.2481d, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0d, 1d, 0d, 0d, 1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(1d, 1d, 0d, 0d, 1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0d, -1d, 0d, 1d, 1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0d, 1d, 0d, 1d, -1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0d, 0d, -1d, 1d, 1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.HermiteSample(0d, 1d, 0d, 1d, 0d, 1d));
			Assert.Throws<ArgumentException>(() => random.HermiteSample(0d, 0d, 0d, 1d, 0d, 0d));
			Assert.DoesNotThrow(() => random.HermiteSample(0d, 0d, 1d, 1d, 0d, -1d));
		}

		[Test]
		public void TestHermiteDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(2d, 1d, 1d, 4d, 2d, -1d);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, 2d, 1d, 1d, 4d, 2d, -1d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(-1d, 4d, 2d, 5d, 4d, 1d);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, -1d, 4d, 2d, 5d, 4d, 1d, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakeHermiteSampleGenerator(-172.3d, 60.60d, -0.07283d, -96.83d, 0.2346d, -0.2481d);
			TestHermiteDistribution(() => generator.Next(), 50000, 2, 20, -172.3d, 60.60d, -0.07283d, -96.83d, 0.2346d, -0.2481d, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0d, 1d, 0d, 0d, 1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(1d, 1d, 0d, 0d, 1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0d, -1d, 0d, 1d, 1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0d, 1d, 0d, 1d, -1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0d, 0d, -1d, 1d, 1d, 0d));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakeHermiteSampleGenerator(0d, 1d, 0d, 1d, 0d, 1d));
			Assert.Throws<ArgumentException>(() => generator = random.MakeHermiteSampleGenerator(0d, 0d, 0d, 1d, 0d, 0d));
			Assert.DoesNotThrow(() => generator = random.MakeHermiteSampleGenerator(0d, 0d, 1d, 1d, 0d, -1d));
		}

		#endregion

		#region Piecewise Uniform Distribution

		private void TestPiecewiseUniformDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float[] x, float[] y, float meanTolerance, float stdDevTolerance)
		{
			var areas = new float[y.Length];

			float totalArea = 0f;
			for (int i = 0; i < y.Length; ++i)
			{
				float x0 = x[i];
				float x1 = x[i + 1];
				float h = y[i];
				float area = (x1 - x0) * h;
				areas[i] = area;
				totalArea += area;
			}

			float meanSum = 0f;
			for (int i = 0; i < y.Length; ++i)
			{
				float x0 = x[i];
				float x1 = x[i + 1];
				meanSum += (x0 + x1) / 2f * areas[i];
			}

			float mean = meanSum / totalArea;

			float varianceSum = 0f;
			for (int i = 0; i < y.Length; ++i)
			{
				float x0 = x[i];
				float x1 = x[i + 1];
				float xDelta = x1 - x0;
				float segmentMean = (x0 + x1) / 2f;
				varianceSum += (xDelta * xDelta / 12f + segmentMean * segmentMean) * areas[i];
			}

			float variance = varianceSum / totalArea - mean * mean;
			Assert.GreaterOrEqual(variance, 0f);
			float standardDeviation = Mathf.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestPiecewiseWeightedUniformDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float[] x, float[] weights, float meanTolerance, float stdDevTolerance)
		{
			float totalArea = 0f;
			for (int i = 0; i < weights.Length; ++i)
			{
				totalArea += weights[i];
			}

			float meanSum = 0f;
			for (int i = 0; i < weights.Length; ++i)
			{
				float x0 = x[i];
				float x1 = x[i + 1];
				meanSum += (x0 + x1) / 2f * weights[i];
			}

			float mean = meanSum / totalArea;

			float varianceSum = 0f;
			for (int i = 0; i < weights.Length; ++i)
			{
				float x0 = x[i];
				float x1 = x[i + 1];
				float xDelta = x1 - x0;
				float segmentMean = (x0 + x1) / 2f;
				varianceSum += (xDelta * xDelta / 12f + segmentMean * segmentMean) * weights[i];
			}

			float variance = varianceSum / totalArea - mean * mean;
			Assert.GreaterOrEqual(variance, 0f);
			float standardDeviation = Mathf.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestPiecewiseUniformDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double[] x, double[] y, double meanTolerance, double stdDevTolerance)
		{
			var areas = new double[y.Length];

			double totalArea = 0d;
			for (int i = 0; i < y.Length; ++i)
			{
				double x0 = x[i];
				double x1 = x[i + 1];
				double h = y[i];
				double area = (x1 - x0) * h;
				areas[i] = area;
				totalArea += area;
			}

			double meanSum = 0d;
			for (int i = 0; i < y.Length; ++i)
			{
				double x0 = x[i];
				double x1 = x[i + 1];
				meanSum += (x0 + x1) / 2d * areas[i];
			}

			double mean = meanSum / totalArea;

			double varianceSum = 0d;
			for (int i = 0; i < y.Length; ++i)
			{
				double x0 = x[i];
				double x1 = x[i + 1];
				double xDelta = x1 - x0;
				double segmentMean = (x0 + x1) / 2d;
				varianceSum += (xDelta * xDelta / 12d + segmentMean * segmentMean) * areas[i];
			}

			double variance = varianceSum / totalArea - mean * mean;
			Assert.GreaterOrEqual(variance, 0d);
			double standardDeviation = Math.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestPiecewiseWeightedUniformDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double[] x, double[] weights, double meanTolerance, double stdDevTolerance)
		{
			double totalArea = 0d;
			for (int i = 0; i < weights.Length; ++i)
			{
				totalArea += weights[i];
			}

			double meanSum = 0d;
			for (int i = 0; i < weights.Length; ++i)
			{
				double x0 = x[i];
				double x1 = x[i + 1];
				meanSum += (x0 + x1) / 2d * weights[i];
			}

			double mean = meanSum / totalArea;

			double varianceSum = 0d;
			for (int i = 0; i < weights.Length; ++i)
			{
				double x0 = x[i];
				double x1 = x[i + 1];
				double xDelta = x1 - x0;
				double segmentMean = (x0 + x1) / 2d;
				varianceSum += (xDelta * xDelta / 12d + segmentMean * segmentMean) * weights[i];
			}

			double variance = varianceSum / totalArea - mean * mean;
			Assert.GreaterOrEqual(variance, 0d);
			double standardDeviation = Math.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestPiecewiseUniformDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f }), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f }), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f }), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f }, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new float[] { 1f, 2f, 0f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new float[] { 0f, 2f, 1f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new float[] { 2f, 1f, 0f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new float[] { 1f, 0f, 2f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new float[] { 0f, 1f, 2f }, new float[]{ -1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new float[] { 0f, 1f, 2f }, new float[]{ 1f, -1f }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseUniformSample(new float[] { 0f, 1f, 2f }, new float[]{ 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f });
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f });
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f });
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f }, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new float[] { 1f, 2f, 0f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new float[] { 0f, 2f, 1f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new float[] { 2f, 1f, 0f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new float[] { 1f, 0f, 2f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ -1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 1f, -1f }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_Vector2()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 2f), new Vector2(2f, 3f) }, 3f), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(-6f, 5f), new Vector2(-2f, 1f), new Vector2(1f, 3f), new Vector2(2f, 0f), new Vector2(3f, 2f) }, 6f), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(-188.8f, 0.125f), new Vector2(-172.3f, 0.839f), new Vector2(-96.83f, 0.001f), new Vector2(-60.60f, 0.487f), new Vector2(-47.03f, 0.972f), new Vector2(-10.38f, 0.738f) }, -0.2346f), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f }, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(1f, 1f), new Vector2(2f, 1f) }, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(2f, 1f) }, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(2f, 1f), new Vector2(1f, 1f) }, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(1f, 1f), new Vector2(0f, 1f) }, 2f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(0f, -1f), new Vector2(1f, 1f) }, 2f));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, -1f) }, 2f));
			Assert.Throws<ArgumentException>(() => random.PiecewiseUniformSample(new Vector2 [] { new Vector2(0f, 0f), new Vector2(1f, 0f) }, 2f));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_SampleGenerator_Vector2()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 2f), new Vector2(2f, 3f) }, 3f);
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(-6f, 5f), new Vector2(-2f, 1f), new Vector2(1f, 3f), new Vector2(2f, 0f), new Vector2(3f, 2f) }, 6f);
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(-188.8f, 0.125f), new Vector2(-172.3f, 0.839f), new Vector2(-96.83f, 0.001f), new Vector2(-60.60f, 0.487f), new Vector2(-47.03f, 0.972f), new Vector2(-10.38f, 0.738f) }, -0.2346f);
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f }, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(1f, 1f), new Vector2(2f, 1f) }, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(2f, 1f) }, 1f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(2f, 1f), new Vector2(1f, 1f) }, 0f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(1f, 1f), new Vector2(0f, 1f) }, 2f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(0f, -1f), new Vector2(1f, 1f) }, 2f));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, -1f) }, 2f));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new Vector2 [] { new Vector2(0f, 0f), new Vector2(1f, 0f) }, 2f));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_FloatWeights()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseWeightedUniformDistribution(() => random.PiecewiseWeightedUniformSample(new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f }), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseWeightedUniformDistribution(() => random.PiecewiseWeightedUniformSample(new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f }), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseWeightedUniformDistribution(() => random.PiecewiseWeightedUniformSample(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f }), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f }, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new float[] { 1f, 2f, 0f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new float[] { 0f, 2f, 1f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new float[] { 2f, 1f, 0f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new float[] { 1f, 0f, 2f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new float[] { 0f, 1f, 2f }, new float[]{ -1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new float[] { 0f, 1f, 2f }, new float[]{ 1f, -1f }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseWeightedUniformSample(new float[] { 0f, 1f, 2f }, new float[]{ 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_SampleGenerator_FloatWeights()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseWeightedUniformSampleGenerator(new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f });
			TestPiecewiseWeightedUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 2f, 3f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseWeightedUniformSampleGenerator(new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f });
			TestPiecewiseWeightedUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseWeightedUniformSampleGenerator(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f });
			TestPiecewiseWeightedUniformDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f }, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new float[] { 1f, 2f, 0f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new float[] { 0f, 2f, 1f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new float[] { 2f, 1f, 0f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new float[] { 1f, 0f, 2f }, new float[]{ 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ -1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 1f, -1f }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 2d, 3d }), 50000, 2, 20, new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 2d, 3d }, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d }), 50000, 2, 20, new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d }, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseUniformDistribution(() => random.PiecewiseUniformSample(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d }), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d }, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new double[] { 1d, 2d, 0d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new double[] { 0d, 2d, 1d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new double[] { 2d, 1d, 0d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new double[] { 1d, 0d, 2d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new double[] { 0d, 1d, 2d }, new double[]{ -1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseUniformSample(new double[] { 0d, 1d, 2d }, new double[]{ 1d, -1d }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseUniformSample(new double[] { 0d, 1d, 2d }, new double[]{ 0d, 0d }));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 2d, 3d });
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 2d, 3d }, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d });
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d }, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseUniformSampleGenerator(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d });
			TestPiecewiseUniformDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d }, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new double[] { 1d, 2d, 0d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new double[] { 0d, 2d, 1d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new double[] { 2d, 1d, 0d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new double[] { 1d, 0d, 2d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ -1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 1d, -1d }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseUniformSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 0d, 0d }));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_DoubleWeights()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseWeightedUniformDistribution(() => random.PiecewiseWeightedUniformSample(new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 2d, 3d }), 50000, 2, 20, new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 2d, 3d }, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseWeightedUniformDistribution(() => random.PiecewiseWeightedUniformSample(new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d }), 50000, 2, 20, new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d }, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseWeightedUniformDistribution(() => random.PiecewiseWeightedUniformSample(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d }), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d }, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new double[] { 1d, 2d, 0d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new double[] { 0d, 2d, 1d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new double[] { 2d, 1d, 0d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new double[] { 1d, 0d, 2d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new double[] { 0d, 1d, 2d }, new double[]{ -1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseWeightedUniformSample(new double[] { 0d, 1d, 2d }, new double[]{ 1d, -1d }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseWeightedUniformSample(new double[] { 0d, 1d, 2d }, new double[]{ 0d, 0d }));
		}

		[Test]
		public void TestPiecewiseUniformDistribution_SampleGenerator_DoubleWeights()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseWeightedUniformSampleGenerator(new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 2d, 3d });
			TestPiecewiseWeightedUniformDistribution(() => generator.Next(), 50000, 2, 20, new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 2d, 3d }, 0.01d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseWeightedUniformSampleGenerator(new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d });
			TestPiecewiseWeightedUniformDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d }, 0.02d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseWeightedUniformSampleGenerator(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d });
			TestPiecewiseWeightedUniformDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d }, 0.01d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new double[] { 1d, 2d, 0d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new double[] { 0d, 2d, 1d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new double[] { 2d, 1d, 0d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new double[] { 1d, 0d, 2d }, new double[]{ 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ -1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 1d, -1d }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseWeightedUniformSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 0d, 0d }));
		}

		#endregion

		#region Piecewise Linear Distribution

		private void TestPiecewiseLinearDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float[] x, float[] y, float meanTolerance, float stdDevTolerance)
		{
			float totalArea = 0f;
			float meanSum = 0f;
			for (int i = 1; i < x.Length; ++i)
			{
				float x0 = x[i - 1];
				float y0 = y[i - 1];
				float x1 = x[i];
				float y1 = y[i];
				float dy = y1 - y0;
				float n = x1 * y0 - x0 * y1;
				float x2 = x0 + x1;
				float x3 = x0 * x0 + x1 * x1;
				float x4 = x3 + x0 * x1;
				float area = (x1 - x0) * (y0 + y1) / 2f;
				totalArea += area;
				meanSum += x4 * dy / 3f + x2 * n / 2f;
			}

			float mean = meanSum / totalArea;

			float varianceSum = 0f;
			for (int i = 1; i < x.Length; ++i)
			{
				float x0 = x[i - 1];
				float y0 = y[i - 1];
				float x1 = x[i];
				float y1 = y[i];
				float dy = y1 - y0;
				float n = x1 * y0 - x0 * y1;
				float x2 = x0 + x1;
				float x3 = x0 * x0 + x1 * x1;
				float x4 = x3 + x0 * x1;
				varianceSum += x3 * x2 * dy / 4f + x4 * n / 3f;
			}

			float variance = varianceSum / totalArea - mean * mean;
			Assert.GreaterOrEqual(variance, 0f);
			float standardDeviation = Mathf.Sqrt(variance);

			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestPiecewiseLinearDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double[] x, double[] y, double meanTolerance, double stdDevTolerance)
		{
			double totalArea = 0d;
			double meanSum = 0d;
			for (int i = 1; i < x.Length; ++i)
			{
				double x0 = x[i - 1];
				double y0 = y[i - 1];
				double x1 = x[i];
				double y1 = y[i];
				double dy = y1 - y0;
				double n = x1 * y0 - x0 * y1;
				double x2 = x0 + x1;
				double x3 = x0 * x0 + x1 * x1;
				double x4 = x3 + x0 * x1;
				double area = (x1 - x0) * (y0 + y1) / 2d;
				totalArea += area;
				meanSum += x4 * dy / 3d + x2 * n / 2d;
			}

			double mean = meanSum / totalArea;

			double varianceSum = 0d;
			for (int i = 1; i < x.Length; ++i)
			{
				double x0 = x[i - 1];
				double y0 = y[i - 1];
				double x1 = x[i];
				double y1 = y[i];
				double dy = y1 - y0;
				double n = x1 * y0 - x0 * y1;
				double x2 = x0 + x1;
				double x3 = x0 * x0 + x1 * x1;
				double x4 = x3 + x0 * x1;
				varianceSum += x3 * x2 * dy / 4d + x4 * n / 3d;
			}

			double variance = varianceSum / totalArea - mean * mean;
			Assert.GreaterOrEqual(variance, 0d);
			double standardDeviation = Math.Sqrt(variance);
			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestPiecewiseLinearDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new float[] { 1f, 2f, 0f }, new float[]{ 1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new float[] { 0f, 2f, 1f }, new float[]{ 1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new float[] { 2f, 1f, 0f }, new float[]{ 1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new float[] { 1f, 0f, 2f }, new float[]{ 1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new float[] { 0f, 1f, 2f }, new float[]{ -1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new float[] { 0f, 1f, 2f }, new float[]{ 1f, -1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new float[] { 0f, 1f, 2f }, new float[]{ 1f, 1f, -1f }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseLinearSample(new float[] { 0f, 1f, 2f }, new float[]{ 0f, 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseLinearDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new float[] { 1f, 2f, 0f }, new float[]{ 1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new float[] { 0f, 2f, 1f }, new float[]{ 1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new float[] { 2f, 1f, 0f }, new float[]{ 1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new float[] { 1f, 0f, 2f }, new float[]{ 1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ -1f, 1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 1f, -1f, 1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 1f, 1f, -1f }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 0f, 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseLinearDistribution_Vector2()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 3f), new Vector2(2f, 2f), new Vector2(3f, 4f) }), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(-6f, 5f), new Vector2(-2f, 1f), new Vector2(1f, 3f), new Vector2(2f, 0f), new Vector2(3f, 2f), new Vector2(6f, 0f) }), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(-188.8f, 0.125f), new Vector2(-172.3f, 0.839f), new Vector2(-96.83f, 0.001f), new Vector2(-60.60f, 0.487f), new Vector2(-47.03f, 0.972f), new Vector2(-10.38f, 0.738f), new Vector2(-0.2346f, 0.738f) }), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(1f, 1f), new Vector2(2f, 1f), new Vector2(0f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(2f, 1f), new Vector2(1f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(2f, 1f), new Vector2(1f, 1f), new Vector2(0f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(1f, 1f), new Vector2(0f, 1f), new Vector2(2f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(0f, -1f), new Vector2(1f, 1f), new Vector2(2f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, -1f), new Vector2(2f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(2f, -1f) }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseLinearSample(new Vector2 [] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(2f, 0f) }));
		}

		[Test]
		public void TestPiecewiseLinearDistribution_SampleGenerator_Vector2()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 3f), new Vector2(2f, 2f), new Vector2(3f, 4f) });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(-6f, 5f), new Vector2(-2f, 1f), new Vector2(1f, 3f), new Vector2(2f, 0f), new Vector2(3f, 2f), new Vector2(6f, 0f) });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(-188.8f, 0.125f), new Vector2(-172.3f, 0.839f), new Vector2(-96.83f, 0.001f), new Vector2(-60.60f, 0.487f), new Vector2(-47.03f, 0.972f), new Vector2(-10.38f, 0.738f), new Vector2(-0.2346f, 0.738f) });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(1f, 1f), new Vector2(2f, 1f), new Vector2(0f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(2f, 1f), new Vector2(1f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(2f, 1f), new Vector2(1f, 1f), new Vector2(0f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(1f, 1f), new Vector2(0f, 1f), new Vector2(2f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(0f, -1f), new Vector2(1f, 1f), new Vector2(2f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, -1f), new Vector2(2f, 1f) }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(2f, -1f) }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new Vector2 [] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(2f, 0f) }));
		}

		[Test]
		public void TestPiecewiseLinearDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 3d, 2d, 4d }), 50000, 2, 20, new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 3d, 2d, 4d }, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d, 0d }), 50000, 2, 20, new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d, 0d }, 0.01d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseLinearDistribution(() => random.PiecewiseLinearSample(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d, 0.738d }), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d, 0.738d }, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new double[] { 1d, 2d, 0d }, new double[]{ 1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new double[] { 0d, 2d, 1d }, new double[]{ 1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new double[] { 2d, 1d, 0d }, new double[]{ 1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new double[] { 1d, 0d, 2d }, new double[]{ 1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new double[] { 0d, 1d, 2d }, new double[]{ -1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new double[] { 0d, 1d, 2d }, new double[]{ 1d, -1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseLinearSample(new double[] { 0d, 1d, 2d }, new double[]{ 1d, 1d, -1d }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseLinearSample(new double[] { 0d, 1d, 2d }, new double[]{ 0d, 0d, 0d }));
		}

		[Test]
		public void TestPiecewiseLinearDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 3d, 2d, 4d });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 3d, 2d, 4d }, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d, 0d });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d, 0d }, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseLinearSampleGenerator(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d, 0.738d });
			TestPiecewiseLinearDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d, 0.738d }, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new double[] { 1d, 2d, 0d }, new double[]{ 1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new double[] { 0d, 2d, 1d }, new double[]{ 1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new double[] { 2d, 1d, 0d }, new double[]{ 1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new double[] { 1d, 0d, 2d }, new double[]{ 1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ -1d, 1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 1d, -1d, 1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 1d, 1d, -1d }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseLinearSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 0d, 0d, 0d }));
		}

		#endregion

		#region Piecewise Hermite Distribution

		private void TestPiecewiseHermiteDistribution(Func<float> f, int batchSize, int minBatchCount, int maxBatchCount, float[] x, float[] y, float[] m, float meanTolerance, float stdDevTolerance)
		{
			float totalArea = 0f;
			float meanSum = 0f;
			for (int i = 1; i < x.Length; ++i)
			{
				float x0 = x[i - 1];
				float y0 = y[i - 1];
				float m0 = m[i * 2 - 2];
				float x1 = x[i];
				float y1 = y[i];
				float m1 = m[i * 2 - 1];
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
					float dx = x1 - x0;
					float dy = y1 - y0;
					float a = (m0 + m1) * dx - 2f * dy;
					float b = 3f * dy - (2f * m0 + m1) * dx;
					float c = m0 * dx;
					float d = y0;
					float area = a / 4f + b / 3f + c / 2f + d;
					float segmentMean = (a / 5f + b / 4f + c / 3f + d / 2f) / area;
					area *= dx;
					totalArea += area;
					segmentMean = segmentMean * dx + x0;
					meanSum += segmentMean * area;
				}
				else
				{
					float area = (x1 - x0) * y0;
					float segmentMean = (x0 + x1) / 2f;
					totalArea += area;
					meanSum += segmentMean * area;
				}
			}

			float mean = meanSum / totalArea;

			float varianceSum = 0f;
			for (int i = 1; i < x.Length; ++i)
			{
				float x0 = x[i - 1];
				float y0 = y[i - 1];
				float m0 = m[i * 2 - 2];
				float x1 = x[i];
				float y1 = y[i];
				float m1 = m[i * 2 - 1];
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
					float dx = x1 - x0;
					float dy = y1 - y0;
					float a = (m0 + m1) * dx - 2f * dy;
					float b = 3f * dy - (2f * m0 + m1) * dx;
					float c = m0 * dx;
					float d = y0;
					float area = a / 4f + b / 3f + c / 2f + d;
					float segmentMean = (a / 5f + b / 4f + c / 3f + d / 2f) / area;
					float segmentVariance = (a / 6f + b / 5f + c / 4f + d / 3f) / area - segmentMean * segmentMean;
					area *= dx;
					segmentMean = segmentMean * dx + x0;
					segmentVariance = segmentVariance * dx * dx + segmentMean * segmentMean;
					varianceSum += segmentVariance * area;
				}
				else
				{
					float dx = x1 - x0;
					float area = dx * y0;
					float segmentMean = (x0 + x1) / 2f;
					float segmentVariance = dx * dx / 12f;
					varianceSum += (segmentVariance + segmentMean * segmentMean) * area;
				}
			}

			float variance = varianceSum / totalArea - mean * mean;
			Assert.GreaterOrEqual(variance, 0f);
			float standardDeviation = Mathf.Sqrt(variance);

			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		private void TestPiecewiseHermiteDistribution(Func<double> f, int batchSize, int minBatchCount, int maxBatchCount, double[] x, double[] y, double[] m, double meanTolerance, double stdDevTolerance)
		{
			double totalArea = 0d;
			double meanSum = 0d;
			for (int i = 1; i < x.Length; ++i)
			{
				double x0 = x[i - 1];
				double y0 = y[i - 1];
				double m0 = m[i * 2 - 2];
				double x1 = x[i];
				double y1 = y[i];
				double m1 = m[i * 2 - 1];
				if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
				{
					double dx = x1 - x0;
					double dy = y1 - y0;
					double a = (m0 + m1) * dx - 2d * dy;
					double b = 3d * dy - (2d * m0 + m1) * dx;
					double c = m0 * dx;
					double d = y0;
					double area = a / 4d + b / 3d + c / 2d + d;
					double segmentMean = (a / 5d + b / 4d + c / 3d + d / 2d) / area;
					area *= dx;
					totalArea += area;
					segmentMean = segmentMean * dx + x0;
					meanSum += segmentMean * area;
				}
				else
				{
					double area = (x1 - x0) * y0;
					double segmentMean = (x0 + x1) / 2d;
					totalArea += area;
					meanSum += segmentMean * area;
				}
			}

			double mean = meanSum / totalArea;

			double varianceSum = 0d;
			for (int i = 1; i < x.Length; ++i)
			{
				double x0 = x[i - 1];
				double y0 = y[i - 1];
				double m0 = m[i * 2 - 2];
				double x1 = x[i];
				double y1 = y[i];
				double m1 = m[i * 2 - 1];
				if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
				{
					double dx = x1 - x0;
					double dy = y1 - y0;
					double a = (m0 + m1) * dx - 2d * dy;
					double b = 3d * dy - (2d * m0 + m1) * dx;
					double c = m0 * dx;
					double d = y0;
					double area = a / 4d + b / 3d + c / 2d + d;
					double segmentMean = (a / 5d + b / 4d + c / 3d + d / 2d) / area;
					double segmentVariance = (a / 6d + b / 5d + c / 4d + d / 3d) / area - segmentMean * segmentMean;
					area *= dx;
					segmentMean = segmentMean * dx + x0;
					segmentVariance = segmentVariance * dx * dx + segmentMean * segmentMean;
					varianceSum += segmentVariance * area;
				}
				else
				{
					double dx = x1 - x0;
					double area = dx * y0;
					double segmentMean = (x0 + x1) / 2d;
					double segmentVariance = dx * dx / 12d;
					varianceSum += (segmentVariance + segmentMean * segmentMean) * area;
				}
			}

			double variance = varianceSum / totalArea - mean * mean;
			Assert.GreaterOrEqual(variance, 0d);
			double standardDeviation = Math.Sqrt(variance);

			TestDistribution(f, batchSize, minBatchCount, maxBatchCount, mean, standardDeviation, meanTolerance, stdDevTolerance);
		}

		[Test]
		public void TestPiecewiseHermiteDistribution_Float()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f }), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f }), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f }), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f }), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f }, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 1f, 2f, 0f }, new float[]{ 1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 0f, 2f, 1f }, new float[]{ 1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 2f, 1f, 0f }, new float[]{ 1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 1f, 0f, 2f }, new float[]{ 1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 0f, 1f, 2f }, new float[]{ -1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 0f, 1f, 2f }, new float[]{ 1f, -1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 0f, 1f, 2f }, new float[]{ 1f, 1f, -1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 0f, 1f, 2f }, new float[]{ 1f, 0f, 1f }, new float[]{ 1f, -1f, -1f, -1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new float[] { 0f, 1f, 2f }, new float[]{ 1f, 0f, 1f }, new float[]{ 1f, 1f, 1f, -1f }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseHermiteSample(new float[] { 0f, 1f, 2f }, new float[]{ 0f, 0f, 0f }, new float[]{ 0f, 0f, 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseHermiteDistribution_SampleGenerator_Float()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f }, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 1f, 2f, 0f }, new float[]{ 1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 0f, 2f, 1f }, new float[]{ 1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 2f, 1f, 0f }, new float[]{ 1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 1f, 0f, 2f }, new float[]{ 1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ -1f, 1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 1f, -1f, 1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 1f, 1f, -1f }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 1f, 0f, 1f }, new float[]{ 1f, -1f, -1f, -1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 1f, 0f, 1f }, new float[]{ 1f, 1f, 1f, -1f }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new float[] { 0f, 1f, 2f }, new float[]{ 0f, 0f, 0f }, new float[]{ 0f, 0f, 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseHermiteDistribution_Vector2()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 3f), new Vector2(2f, 2f), new Vector2(3f, 4f) }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f }), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(-6f, 5f), new Vector2(-2f, 1f), new Vector2(1f, 3f), new Vector2(2f, 0f), new Vector2(3f, 2f), new Vector2(6f, 0f) }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f }), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(-188.8f, 0.125f), new Vector2(-172.3f, 0.839f), new Vector2(-96.83f, 0.001f), new Vector2(-60.60f, 0.487f), new Vector2(-47.03f, 0.972f), new Vector2(-10.38f, 0.738f), new Vector2(-0.2346f, 0.738f) }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f }), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(-188.8f, 0.125f), new Vector2(-172.3f, 0.839f), new Vector2(-96.83f, 0f), new Vector2(-60.60f, 0.487f), new Vector2(-47.03f, 0.972f), new Vector2(-10.38f, 0.738f), new Vector2(-0.2346f, 0.738f) }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f }), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f }, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(1f, 1f), new Vector2(2f, 1f), new Vector2(0f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(2f, 1f), new Vector2(1f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(2f, 1f), new Vector2(1f, 1f), new Vector2(0f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(1f, 1f), new Vector2(0f, 1f), new Vector2(2f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(0f, -1f), new Vector2(1f, 1f), new Vector2(2f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, -1f), new Vector2(2f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(2f, -1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 0f), new Vector2(2f, 1f) }, new float[]{ 1f, -1f, -1f, -1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 0f), new Vector2(2f, 1f) }, new float[]{ 1f, 1f, 1f, -1f }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseHermiteSample(new Vector2 [] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(2f, 0f) }, new float[]{ 0f, 0f, 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseHermiteDistribution_SampleGenerator_Vector2()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 3f), new Vector2(2f, 2f), new Vector2(3f, 4f) }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(-6f, 5f), new Vector2(-2f, 1f), new Vector2(1f, 3f), new Vector2(2f, 0f), new Vector2(3f, 2f), new Vector2(6f, 0f) }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(-188.8f, 0.125f), new Vector2(-172.3f, 0.839f), new Vector2(-96.83f, 0.001f), new Vector2(-60.60f, 0.487f), new Vector2(-47.03f, 0.972f), new Vector2(-10.38f, 0.738f), new Vector2(-0.2346f, 0.738f) }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(-188.8f, 0.125f), new Vector2(-172.3f, 0.839f), new Vector2(-96.83f, 0f), new Vector2(-60.60f, 0.487f), new Vector2(-47.03f, 0.972f), new Vector2(-10.38f, 0.738f), new Vector2(-0.2346f, 0.738f) }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f }, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(1f, 1f), new Vector2(2f, 1f), new Vector2(0f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(2f, 1f), new Vector2(1f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(2f, 1f), new Vector2(1f, 1f), new Vector2(0f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(1f, 1f), new Vector2(0f, 1f), new Vector2(2f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(0f, -1f), new Vector2(1f, 1f), new Vector2(2f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, -1f), new Vector2(2f, 1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(2f, -1f) }, new float[]{ 0f, 0f, 0f, 0f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 0f), new Vector2(2f, 1f) }, new float[]{ 1f, -1f, -1f, -1f }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(0f, 1f), new Vector2(1f, 0f), new Vector2(2f, 1f) }, new float[]{ 1f, 1f, 1f, -1f }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new Vector2 [] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(2f, 0f) }, new float[]{ 0f, 0f, 0f, 0f }));
		}

		[Test]
		public void TestPiecewiseHermiteDistribution_AnimationCurve()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 0.5f), new Keyframe(1f, 3f, -1f, -1f), new Keyframe(2f, 2f, -2f, 0f), new Keyframe(3f, 4f, 0f, 0f) })), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(-6f, 5f, 0f, -2f), new Keyframe(-2f, 1f, -3f, 0f), new Keyframe(1f, 3f, 0f, -1f), new Keyframe(2f, 0f, -1f, 0f), new Keyframe(3f, 2f, 0f, -1f), new Keyframe(6f, 0f, 0f, 0f) })), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(-188.8f, 0.125f, 0f, 0.1f), new Keyframe(-172.3f, 0.839f, -0.2f, -0.03f), new Keyframe(-96.83f, 0.001f, -0.04f, 0.25f), new Keyframe(-60.60f, 0.487f, 0.1f, 0.1f), new Keyframe(-47.03f, 0.972f, 0.1f, 1f), new Keyframe(-10.38f, 0.738f, 0.01f, 0.21f), new Keyframe(-0.2346f, 0.738f, -0.21f, 0f) })), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f }, 0.005f, 0.005f);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(-188.8f, 0.125f, 0f, 0.1f), new Keyframe(-172.3f, 0.839f, -0.2f, -0.03f), new Keyframe(-96.83f, 0f, -0.04f, float.PositiveInfinity), new Keyframe(-60.60f, 0.487f, 0.1f, 0.1f), new Keyframe(-47.03f, 0.972f, 0.1f, 1f), new Keyframe(-10.38f, 0.738f, float.NegativeInfinity, 0.21f), new Keyframe(-0.2346f, 0.738f, -0.21f, 0f) })), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f }, 0.005f, 0.005f);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(0f, -1f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f), new Keyframe(2f, 1f, 0f, 0f) })));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 0f), new Keyframe(1f, -1f, 0f, 0f), new Keyframe(2f, 1f, 0f, 0f) })));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f), new Keyframe(2f, -1f, 0f, 0f) })));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 1f), new Keyframe(1f, 0f, -1f, -1f), new Keyframe(2f, 1f, -1f, 0f) })));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 1f), new Keyframe(1f, 0f, 1f, 1f), new Keyframe(2f, 1f, -1f, 0f) })));
			Assert.Throws<ArgumentException>(() => random.PiecewiseHermiteSample(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 0f, 0f, 0f), new Keyframe(2f, 0f, 0f, 0f) })));
		}

		[Test]
		public void TestPiecewiseHermiteDistribution_SampleGenerator_AnimationCurve()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 0.5f), new Keyframe(1f, 3f, -1f, -1f), new Keyframe(2f, 2f, -2f, 0f), new Keyframe(3f, 4f, 0f, 0f) }));
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { 0f, 1f, 2f, 3f }, new float[] { 1f, 3f, 2f, 4f }, new float[] { 0.5f, -1f, -1f, -2f, 0f, 0f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(-6f, 5f, 0f, -2f), new Keyframe(-2f, 1f, -3f, 0f), new Keyframe(1f, 3f, 0f, -1f), new Keyframe(2f, 0f, -1f, 0f), new Keyframe(3f, 2f, 0f, -1f), new Keyframe(6f, 0f, 0f, 0f) }));
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -6f, -2f, 1f, 2f, 3f, 6f }, new float[] { 5f, 1f, 3f, 0f, 2f, 0f }, new float[] { -2f, -3f, 0f, 0f, -1f, -1f, 0f, 0f, -1f, 0f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(-188.8f, 0.125f, 0f, 0.1f), new Keyframe(-172.3f, 0.839f, -0.2f, -0.03f), new Keyframe(-96.83f, 0.001f, -0.04f, 0.25f), new Keyframe(-60.60f, 0.487f, 0.1f, 0.1f), new Keyframe(-47.03f, 0.972f, 0.1f, 1f), new Keyframe(-10.38f, 0.738f, 0.01f, 0.21f), new Keyframe(-0.2346f, 0.738f, -0.21f, 0f) }));
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0.001f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, 0.25f, 0.1f, 0.1f, 0.1f, 1f, 0.01f, 0.21f, -0.21f }, 0.005f, 0.005f);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(-188.8f, 0.125f, 0f, 0.1f), new Keyframe(-172.3f, 0.839f, -0.2f, -0.03f), new Keyframe(-96.83f, 0f, -0.04f, float.PositiveInfinity), new Keyframe(-60.60f, 0.487f, 0.1f, 0.1f), new Keyframe(-47.03f, 0.972f, 0.1f, 1f), new Keyframe(-10.38f, 0.738f, float.NegativeInfinity, 0.21f), new Keyframe(-0.2346f, 0.738f, -0.21f, 0f) }));
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new float[] { -188.8f, -172.3f, -96.83f, -60.60f, -47.03f, -10.38f, -0.2346f }, new float[] { 0.125f, 0.839f, 0f, 0.487f, 0.972f, 0.738f, 0.738f }, new float[] { 0.1f, -0.2f, -0.03f, -0.04f, float.PositiveInfinity, 0.1f, 0.1f, 0.1f, 1f, float.NegativeInfinity, 0.21f, -0.21f }, 0.005f, 0.005f);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(0f, -1f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f), new Keyframe(2f, 1f, 0f, 0f) })));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 0f), new Keyframe(1f, -1f, 0f, 0f), new Keyframe(2f, 1f, 0f, 0f) })));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f), new Keyframe(2f, -1f, 0f, 0f) })));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 1f), new Keyframe(1f, 0f, -1f, -1f), new Keyframe(2f, 1f, -1f, 0f) })));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f, 0f, 1f), new Keyframe(1f, 0f, 1f, 1f), new Keyframe(2f, 1f, -1f, 0f) })));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 0f, 0f, 0f), new Keyframe(2f, 0f, 0f, 0f) })));
		}

		[Test]
		public void TestPiecewiseHermiteDistribution_Double()
		{
			var random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 3d, 2d, 4d }, new double[] { 0.5d, -1d, -1d, -2d, 0d, 0d }), 50000, 2, 20, new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 3d, 2d, 4d }, new double[] { 0.5d, -1d, -1d, -2d, 0d, 0d }, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d, 0d }, new double[] { -2d, -3d, 0d, 0d, -1d, -1d, 0d, 0d, -1d, 0d }), 50000, 2, 20, new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d, 0d }, new double[] { -2d, -3d, 0d, 0d, -1d, -1d, 0d, 0d, -1d, 0d }, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d, 0.738d }, new double[] { 0.1d, -0.2d, -0.03d, -0.04d, 0.25d, 0.1d, 0.1d, 0.1d, 1d, 0.01d, 0.21d, -0.21d }), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d, 0.738d }, new double[] { 0.1d, -0.2d, -0.03d, -0.04d, 0.25d, 0.1d, 0.1d, 0.1d, 1d, 0.01d, 0.21d, -0.21d }, 0.005d, 0.005d);

			random = MIRandom.CreateStandard(seed);
			TestPiecewiseHermiteDistribution(() => random.PiecewiseHermiteSample(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0d, 0.487d, 0.972d, 0.738d, 0.738d }, new double[] { 0.1d, -0.2d, -0.03d, -0.04d, double.PositiveInfinity, 0.1d, 0.1d, 0.1d, 1d, double.NegativeInfinity, 0.21d, -0.21d }), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0d, 0.487d, 0.972d, 0.738d, 0.738d }, new double[] { 0.1d, -0.2d, -0.03d, -0.04d, double.PositiveInfinity, 0.1d, 0.1d, 0.1d, 1d, double.NegativeInfinity, 0.21d, -0.21d }, 0.005d, 0.005d);

			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 1d, 2d, 0d }, new double[]{ 1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 0d, 2d, 1d }, new double[]{ 1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 2d, 1d, 0d }, new double[]{ 1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 1d, 0d, 2d }, new double[]{ 1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 0d, 1d, 2d }, new double[]{ -1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 0d, 1d, 2d }, new double[]{ 1d, -1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 0d, 1d, 2d }, new double[]{ 1d, 1d, -1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 0d, 1d, 2d }, new double[]{ 1d, 0d, 1d }, new double[]{ 1d, -1d, -1d, -1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => random.PiecewiseHermiteSample(new double[] { 0d, 1d, 2d }, new double[]{ 1d, 0d, 1d }, new double[]{ 1d, 1d, 1d, -1d }));
			Assert.Throws<ArgumentException>(() => random.PiecewiseHermiteSample(new double[] { 0d, 1d, 2d }, new double[]{ 0d, 0d, 0d }, new double[]{ 0d, 0d, 0d, 0d }));
		}

		[Test]
		public void TestPiecewiseHermiteDistribution_SampleGenerator_Double()
		{
			var generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 3d, 2d, 4d }, new double[] { 0.5d, -1d, -1d, -2d, 0d, 0d });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new double[] { 0d, 1d, 2d, 3d }, new double[] { 1d, 3d, 2d, 4d }, new double[] { 0.5d, -1d, -1d, -2d, 0d, 0d }, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d, 0d }, new double[] { -2d, -3d, 0d, 0d, -1d, -1d, 0d, 0d, -1d, 0d });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -6d, -2d, 1d, 2d, 3d, 6d }, new double[] { 5d, 1d, 3d, 0d, 2d, 0d }, new double[] { -2d, -3d, 0d, 0d, -1d, -1d, 0d, 0d, -1d, 0d }, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d, 0.738d }, new double[] { 0.1d, -0.2d, -0.03d, -0.04d, 0.25d, 0.1d, 0.1d, 0.1d, 1d, 0.01d, 0.21d, -0.21d });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0.001d, 0.487d, 0.972d, 0.738d, 0.738d }, new double[] { 0.1d, -0.2d, -0.03d, -0.04d, 0.25d, 0.1d, 0.1d, 0.1d, 1d, 0.01d, 0.21d, -0.21d }, 0.005d, 0.005d);

			generator = MIRandom.CreateStandard(seed).MakePiecewiseHermiteSampleGenerator(new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0d, 0.487d, 0.972d, 0.738d, 0.738d }, new double[] { 0.1d, -0.2d, -0.03d, -0.04d, double.PositiveInfinity, 0.1d, 0.1d, 0.1d, 1d, double.NegativeInfinity, 0.21d, -0.21d });
			TestPiecewiseHermiteDistribution(() => generator.Next(), 50000, 2, 20, new double[] { -188.8d, -172.3d, -96.83d, -60.60d, -47.03d, -10.38d, -0.2346d }, new double[] { 0.125d, 0.839d, 0d, 0.487d, 0.972d, 0.738d, 0.738d }, new double[] { 0.1d, -0.2d, -0.03d, -0.04d, double.PositiveInfinity, 0.1d, 0.1d, 0.1d, 1d, double.NegativeInfinity, 0.21d, -0.21d }, 0.005d, 0.005d);

			var random = MIRandom.CreateStandard(seed);
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 1d, 2d, 0d }, new double[]{ 1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 0d, 2d, 1d }, new double[]{ 1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 2d, 1d, 0d }, new double[]{ 1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 1d, 0d, 2d }, new double[]{ 1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ -1d, 1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 1d, -1d, 1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 1d, 1d, -1d }, new double[]{ 0d, 0d, 0d, 0d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 1d, 0d, 1d }, new double[]{ 1d, -1d, -1d, -1d }));
			Assert.Throws<ArgumentOutOfRangeException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 1d, 0d, 1d }, new double[]{ 1d, 1d, 1d, -1d }));
			Assert.Throws<ArgumentException>(() => generator = random.MakePiecewiseHermiteSampleGenerator(new double[] { 0d, 1d, 2d }, new double[]{ 0d, 0d, 0d }, new double[]{ 0d, 0d, 0d, 0d }));
		}

		#endregion
	}
}
#endif
