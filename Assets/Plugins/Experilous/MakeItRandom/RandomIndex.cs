/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeIt.Random
{
	public static class RandomIndex
	{
		#region Uniform

		public static int UniformIndex(this IRandomEngine random, int length)
		{
			return random.HalfOpenRange(length);
		}

		public static uint UniformIndex(this IRandomEngine random, uint length)
		{
			return random.HalfOpenRange(length);
		}

		public static int UniformIndex<T>(this IRandomEngine random, T[] array)
		{
			return random.HalfOpenRange(array.Length);
		}

		public static int UniformIndex(this IRandomEngine random, System.Collections.IList list)
		{
			return random.HalfOpenRange(list.Count);
		}

		public static int UniformIndex(this IRandomEngine random, System.Collections.ArrayList list)
		{
			return random.HalfOpenRange(list.Count);
		}

		public static int UniformIndex<T>(this IRandomEngine random, System.Collections.Generic.IList<T> list)
		{
			return random.HalfOpenRange(list.Count);
		}

		public static int UniformIndex<T>(this IRandomEngine random, System.Collections.Generic.List<T> list)
		{
			return random.HalfOpenRange(list.Count);
		}

		#endregion

		#region Weighted

		public static int WeightedIndex(this IRandomEngine random, int[] weights)
		{
			int weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		public static int WeightedIndex(this IRandomEngine random, int[] weights, int weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.HalfOpenRange(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public static int WeightedIndex(this IRandomEngine random, uint[] weights)
		{
			uint weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		public static int WeightedIndex(this IRandomEngine random, uint[] weights, uint weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.HalfOpenRange(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public static int WeightedIndex(this IRandomEngine random, float[] weights)
		{
			float weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		public static int WeightedIndex(this IRandomEngine random, float[] weights, float weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.HalfOpenRange(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public static int WeightedIndex(this IRandomEngine random, double[] weights)
		{
			double weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		public static int WeightedIndex(this IRandomEngine random, double[] weights, double weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.HalfOpenRange(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public static int WeightedIndex(this IRandomEngine random, int elementCount, System.Func<int, int> weightsAccessor)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		public static int WeightedIndex(this IRandomEngine random, int elementCount, System.Func<int, int> weightsAccessor, int weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.HalfOpenRange(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		public static int WeightedIndex(this IRandomEngine random, int elementCount, System.Func<int, uint> weightsAccessor)
		{
			uint weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		public static int WeightedIndex(this IRandomEngine random, int elementCount, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.HalfOpenRange(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		public static int WeightedIndex(this IRandomEngine random, int elementCount, System.Func<int, float> weightsAccessor)
		{
			float weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		public static int WeightedIndex(this IRandomEngine random, int elementCount, System.Func<int, float> weightsAccessor, float weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.HalfOpenRange(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		public static int WeightedIndex(this IRandomEngine random, int elementCount, System.Func<int, double> weightsAccessor)
		{
			double weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		public static int WeightedIndex(this IRandomEngine random, int elementCount, System.Func<int, double> weightsAccessor, double weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.HalfOpenRange(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		#endregion

		#region RandomElement (container extensions)

		public static T RandomElement<T>(this T[] array, IRandomEngine random)
		{
			return array[random.UniformIndex(array)];
		}

		public static object RandomElement(this System.Collections.IList list, IRandomEngine random)
		{
			return list[random.UniformIndex(list)];
		}

		public static object RandomElement(this System.Collections.ArrayList list, IRandomEngine random)
		{
			return list[random.UniformIndex(list)];
		}

		public static T RandomElement<T>(this System.Collections.Generic.IList<T> list, IRandomEngine random)
		{
			return list[random.UniformIndex(list)];
		}

		public static T RandomElement<T>(this System.Collections.Generic.List<T> list, IRandomEngine random)
		{
			return list[random.UniformIndex(list)];
		}

		#endregion
	}
}
