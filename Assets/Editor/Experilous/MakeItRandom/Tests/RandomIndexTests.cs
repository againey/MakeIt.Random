/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;

namespace Experilous.MakeItRandom.Tests
{
	class RandomIndexTests
	{
		private const string seed = "random seed";
		private const string weightSeed = "random seed for creating weights";

		#region Helper Utilities

		private static float CalculateStandardDeviation(int[] sampleCounts, float[] expectedSampleCountsPerBatch, int batchSize, int batchCount)
		{
			float varianceSum = 0;
			for (int i = 0; i < sampleCounts.Length; ++i)
			{
				float expectedSampleCounts = expectedSampleCountsPerBatch[i] * batchCount;
				if (expectedSampleCounts > 0f)
				{
					var delta = sampleCounts[i] - expectedSampleCountsPerBatch[i] * batchCount;
					varianceSum += delta * delta;
				}
			}
			return UnityEngine.Mathf.Sqrt(varianceSum) / (sampleCounts.Length * batchSize * batchCount);
		}

		private static void ValidateWeighted(float[] expectedSampleCountsPerBatch, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, System.Func<int> sample, int sampleSizePercentage)
		{
			var sampleCounts = new int[expectedSampleCountsPerBatch.Length];

			for (int i = 0; i < minBatchCount; ++i)
			{
				for (int j = 0; j < batchSize; ++j)
				{
					sampleCounts[sample()] += 1;
				}
			}

			var standardDeviation = CalculateStandardDeviation(sampleCounts, expectedSampleCountsPerBatch, batchSize, minBatchCount);
			if (standardDeviation <= tolerance) return;

			var smallestStandardDeviation = standardDeviation;

			for (int i = minBatchCount; i < maxBatchCount; ++i)
			{
				for (int j = 0; j < batchSize; ++j)
				{
					sampleCounts[sample()] += 1;
				}

				standardDeviation = CalculateStandardDeviation(sampleCounts, expectedSampleCountsPerBatch, batchSize, i + 1);
				smallestStandardDeviation = UnityEngine.Mathf.Min(smallestStandardDeviation, standardDeviation);

				if (standardDeviation <= tolerance) return;

				Assert.Less(standardDeviation, smallestStandardDeviation * 2d, string.Format("Measured standard deviation was {0:F6}, and the measured error does not appear to be converging to zero after {1} batches.", standardDeviation, i + 1));
			}

			Assert.LessOrEqual(standardDeviation, tolerance, string.Format("Measured standard deviation was {0:F6}, greater than the target tolerance of {1:F6}.", standardDeviation, tolerance));
		}

		private static void ValidateWeighted(int elementCount, sbyte weightMin, sbyte weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new sbyte[elementCount];
			var weightSum = 0;
			var cumulativeWeightSums = new int[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new sbyte[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new int[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, byte weightMin, byte weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new byte[elementCount];
			var weightSum = 0U;
			var cumulativeWeightSums = new uint[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0U);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new byte[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new uint[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (uint)(i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, short weightMin, short weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new short[elementCount];
			var weightSum = 0;
			var cumulativeWeightSums = new int[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new short[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new int[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, ushort weightMin, ushort weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new ushort[elementCount];
			var weightSum = 0U;
			var cumulativeWeightSums = new uint[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0U);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new ushort[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new uint[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (uint)(i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, int weightMin, int weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new int[elementCount];
			var weightSum = 0;
			var cumulativeWeightSums = new int[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new int[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new int[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, uint weightMin, uint weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new uint[elementCount];
			var weightSum = 0U;
			var cumulativeWeightSums = new uint[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0U);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new uint[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new uint[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (uint)(i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, long weightMin, long weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new long[elementCount];
			var weightSum = 0L;
			var cumulativeWeightSums = new long[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0L);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new long[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new long[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, ulong weightMin, ulong weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new ulong[elementCount];
			var weightSum = 0UL;
			var cumulativeWeightSums = new ulong[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0UL);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new ulong[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new ulong[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (ulong)(i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, float weightMin, float weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new float[elementCount];
			var weightSum = 0f;
			var cumulativeWeightSums = new float[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0f);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)weights[i] / weightSum * batchSize;
			}

			var extraLongWeights = new float[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new float[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		private static void ValidateWeighted(int elementCount, double weightMin, double weightMax, int batchSize, int minBatchCount, int maxBatchCount, float tolerance, int sampleSizePercentage)
		{
			batchSize = (batchSize * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var indices = new int[elementCount];
			var weights = new double[elementCount];
			var weightSum = 0d;
			var cumulativeWeightSums = new double[elementCount];

			var weightRandom = MIRandom.CreateStandard(weightSeed);

			do
			{
				for (int i = 0; i < elementCount; ++i)
				{
					indices[i] = i;
					var weight = weightRandom.RangeCC(weightMin, weightMax);
					weights[i] = weight;
					weightSum += weight;
					cumulativeWeightSums[i] = weightSum;
				}
			} while (weightSum == 0d);

			var expectedSampleCountsPerBatch = new float[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				expectedSampleCountsPerBatch[i] = (float)(weights[i] / weightSum * batchSize);
			}

			var extraLongWeights = new double[elementCount * 3 / 2];
			System.Array.Copy(weights, extraLongWeights, elementCount);
			var extraLongCumulativeWeightSums = new double[elementCount * 3 / 2];
			System.Array.Copy(cumulativeWeightSums, extraLongCumulativeWeightSums, elementCount);
			for (int i = elementCount; i < extraLongWeights.Length; ++i)
			{
				extraLongWeights[i] = weightMax;
				extraLongCumulativeWeightSums[i] = weightSum + weightMax * (i - elementCount + 1);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i]), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, weights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElement(indices, (int i) => weights[i], weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedElementBinarySearch(indices, cumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, weights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedElementGenerator(indices, (int i) => weights[i]);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndex(elementCount, extraLongWeights, weightSum), sampleSizePercentage);
			}

			{
				var random = MIRandom.CreateStandard(seed);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => random.WeightedIndexBinarySearch(elementCount, extraLongCumulativeWeightSums, weightSum), sampleSizePercentage);
			}

			{
				var generator = MIRandom.CreateStandard(seed).MakeWeightedIndexGenerator(elementCount, extraLongWeights);
				ValidateWeighted(expectedSampleCountsPerBatch, batchSize, minBatchCount, maxBatchCount, tolerance, () => generator.Next(), sampleSizePercentage);
			}
		}

		#endregion

		#region EquallyWeighted_TwoElements

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_SByte(int sampleSizePercentage)
		{
			ValidateWeighted(2, (sbyte)3, (sbyte)3, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_Byte(int sampleSizePercentage)
		{
			ValidateWeighted(2, (byte)3U, (byte)3U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_Short(int sampleSizePercentage)
		{
			ValidateWeighted(2, (short)3, (short)3, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_UShort(int sampleSizePercentage)
		{
			ValidateWeighted(2, (ushort)3U, (ushort)3U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_Int(int sampleSizePercentage)
		{
			ValidateWeighted(2, 3, 3, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_UInt(int sampleSizePercentage)
		{
			ValidateWeighted(2, 3U, 3U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_Long(int sampleSizePercentage)
		{
			ValidateWeighted(2, 3L, 3L, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_ULong(int sampleSizePercentage)
		{
			ValidateWeighted(2, 3UL, 3UL, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_Float(int sampleSizePercentage)
		{
			ValidateWeighted(2, 3f, 3f, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoElements_Double(int sampleSizePercentage)
		{
			ValidateWeighted(2, 3d, 3d, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		#endregion

		#region EquallyWeighted_TwoHundredElements

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_SByte(int sampleSizePercentage)
		{
			ValidateWeighted(200, (sbyte)3, (sbyte)3, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_Byte(int sampleSizePercentage)
		{
			ValidateWeighted(200, (byte)3U, (byte)3U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_Short(int sampleSizePercentage)
		{
			ValidateWeighted(200, (short)3, (short)3, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_UShort(int sampleSizePercentage)
		{
			ValidateWeighted(200, (ushort)3U, (ushort)3U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_Int(int sampleSizePercentage)
		{
			ValidateWeighted(200, 3, 3, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_UInt(int sampleSizePercentage)
		{
			ValidateWeighted(200, 3U, 3U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_Long(int sampleSizePercentage)
		{
			ValidateWeighted(200, 3L, 3L, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_ULong(int sampleSizePercentage)
		{
			ValidateWeighted(200, 3UL, 3UL, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_Float(int sampleSizePercentage)
		{
			ValidateWeighted(200, 3f, 3f, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void EquallyWeighted_TwoHundredElements_Double(int sampleSizePercentage)
		{
			ValidateWeighted(200, 3d, 3d, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		#endregion

		#region AlmostEquallyWeighted_TwoElements

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_SByte(int sampleSizePercentage)
		{
			ValidateWeighted(2, (sbyte)99, (sbyte)101, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_Byte(int sampleSizePercentage)
		{
			ValidateWeighted(2, (byte)99U, (byte)101U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_Short(int sampleSizePercentage)
		{
			ValidateWeighted(2, (short)99, (short)101, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_UShort(int sampleSizePercentage)
		{
			ValidateWeighted(2, (ushort)99U, (ushort)101U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_Int(int sampleSizePercentage)
		{
			ValidateWeighted(2, 99, 101, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_UInt(int sampleSizePercentage)
		{
			ValidateWeighted(2, 99U, 101U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_Long(int sampleSizePercentage)
		{
			ValidateWeighted(2, 99L, 101L, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_ULong(int sampleSizePercentage)
		{
			ValidateWeighted(2, 99UL, 101UL, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_Float(int sampleSizePercentage)
		{
			ValidateWeighted(2, 99f, 101f, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoElements_Double(int sampleSizePercentage)
		{
			ValidateWeighted(2, 99d, 101d, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		#endregion

		#region AlmostEquallyWeighted_TwoHundredElements

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_SByte(int sampleSizePercentage)
		{
			ValidateWeighted(200, (sbyte)99, (sbyte)101, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_Byte(int sampleSizePercentage)
		{
			ValidateWeighted(200, (byte)99U, (byte)101U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_Short(int sampleSizePercentage)
		{
			ValidateWeighted(200, (short)99, (short)101, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_UShort(int sampleSizePercentage)
		{
			ValidateWeighted(200, (ushort)99U, (ushort)101U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_Int(int sampleSizePercentage)
		{
			ValidateWeighted(200, 99, 101, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_UInt(int sampleSizePercentage)
		{
			ValidateWeighted(200, 99U, 101U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_Long(int sampleSizePercentage)
		{
			ValidateWeighted(200, 99L, 101L, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_ULong(int sampleSizePercentage)
		{
			ValidateWeighted(200, 99UL, 101UL, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_Float(int sampleSizePercentage)
		{
			ValidateWeighted(200, 99f, 101f, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void AlmostEquallyWeighted_TwoHundredElements_Double(int sampleSizePercentage)
		{
			ValidateWeighted(200, 99d, 101d, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		#endregion

		#region RandomlyWeighted_TwoElements

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_SByte(int sampleSizePercentage)
		{
			ValidateWeighted(2, (sbyte)10, (sbyte)99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_Byte(int sampleSizePercentage)
		{
			ValidateWeighted(2, (byte)10U, (byte)99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_Short(int sampleSizePercentage)
		{
			ValidateWeighted(2, (short)10, (short)99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_UShort(int sampleSizePercentage)
		{
			ValidateWeighted(2, (ushort)10U, (ushort)99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_Int(int sampleSizePercentage)
		{
			ValidateWeighted(2, 10, 99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_UInt(int sampleSizePercentage)
		{
			ValidateWeighted(2, 10U, 99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_Long(int sampleSizePercentage)
		{
			ValidateWeighted(2, 10L, 99L, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_ULong(int sampleSizePercentage)
		{
			ValidateWeighted(2, 10UL, 99UL, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_Float(int sampleSizePercentage)
		{
			ValidateWeighted(2, 10f, 99f, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoElements_Double(int sampleSizePercentage)
		{
			ValidateWeighted(2, 10d, 99d, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		#endregion

		#region RandomlyWeighted_TwoHundredElements

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_SByte(int sampleSizePercentage)
		{
			ValidateWeighted(200, (sbyte)10, (sbyte)99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_Byte(int sampleSizePercentage)
		{
			ValidateWeighted(200, (byte)10U, (byte)99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_Short(int sampleSizePercentage)
		{
			ValidateWeighted(200, (short)10, (short)99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_UShort(int sampleSizePercentage)
		{
			ValidateWeighted(200, (ushort)10U, (ushort)99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_Int(int sampleSizePercentage)
		{
			ValidateWeighted(200, 10, 99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_UInt(int sampleSizePercentage)
		{
			ValidateWeighted(200, 10U, 99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_Long(int sampleSizePercentage)
		{
			ValidateWeighted(200, 10L, 99L, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_ULong(int sampleSizePercentage)
		{
			ValidateWeighted(200, 10UL, 99UL, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_Float(int sampleSizePercentage)
		{
			ValidateWeighted(200, 10f, 99f, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoHundredElements_Double(int sampleSizePercentage)
		{
			ValidateWeighted(200, 10d, 99d, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		#endregion

		#region RandomlyWeighted_TwoThousandElements

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_SByte(int sampleSizePercentage)
		{
			ValidateWeighted(2000, (sbyte)10, (sbyte)99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_Byte(int sampleSizePercentage)
		{
			ValidateWeighted(2000, (byte)10U, (byte)99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_Short(int sampleSizePercentage)
		{
			ValidateWeighted(2000, (short)10, (short)99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_UShort(int sampleSizePercentage)
		{
			ValidateWeighted(2000, (ushort)10U, (ushort)99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_Int(int sampleSizePercentage)
		{
			ValidateWeighted(2000, 10, 99, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_UInt(int sampleSizePercentage)
		{
			ValidateWeighted(2000, 10U, 99U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_Long(int sampleSizePercentage)
		{
			ValidateWeighted(2000, 10L, 99L, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_ULong(int sampleSizePercentage)
		{
			ValidateWeighted(2000, 10UL, 99UL, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_Float(int sampleSizePercentage)
		{
			ValidateWeighted(2000, 10f, 99f, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeighted_TwoThousandElements_Double(int sampleSizePercentage)
		{
			ValidateWeighted(2000, 10d, 99d, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		#endregion

		#region RandomlyWeightedWithZeros_TwoHundredElements

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_SByte(int sampleSizePercentage)
		{
			ValidateWeighted(200, (sbyte)0, (sbyte)5, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_Byte(int sampleSizePercentage)
		{
			ValidateWeighted(200, (byte)0U, (byte)5U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_Short(int sampleSizePercentage)
		{
			ValidateWeighted(200, (short)0, (short)5, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_UShort(int sampleSizePercentage)
		{
			ValidateWeighted(200, (ushort)0U, (ushort)5U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_Int(int sampleSizePercentage)
		{
			ValidateWeighted(200, 0, 5, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_UInt(int sampleSizePercentage)
		{
			ValidateWeighted(200, 0U, 5U, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_Long(int sampleSizePercentage)
		{
			ValidateWeighted(200, 0L, 5L, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_ULong(int sampleSizePercentage)
		{
			ValidateWeighted(200, 0UL, 5UL, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_Float(int sampleSizePercentage)
		{
			ValidateWeighted(200, 0f, 5f, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "StatisticalSmoke")]
		public void RandomlyWeightedWithZeros_TwoHundredElements_Double(int sampleSizePercentage)
		{
			ValidateWeighted(200, 0d, 5d, 50000, 2, 5, 0.005f, sampleSizePercentage);
		}

		#endregion
	}
}
#endif
