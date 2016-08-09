/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public struct RandomIndex
	{
		private IRandomEngine _random;

		public RandomIndex(IRandomEngine random)
		{
			_random = random;
		}

		#region Uniform

		public int Uniform(int length)
		{
			return _random.Range().HalfOpen(length);
		}

		public uint Uniform(uint length)
		{
			return _random.Range().HalfOpen(length);
		}

		public int Uniform<T>(T[] array)
		{
			return _random.Range().HalfOpen(array.Length);
		}

		public int Uniform(System.Collections.IList list)
		{
			return _random.Range().HalfOpen(list.Count);
		}

		public int Uniform(System.Collections.ArrayList list)
		{
			return _random.Range().HalfOpen(list.Count);
		}

		public int Uniform<T>(System.Collections.Generic.IList<T> list)
		{
			return _random.Range().HalfOpen(list.Count);
		}

		public int Uniform<T>(System.Collections.Generic.List<T> list)
		{
			return _random.Range().HalfOpen(list.Count);
		}

		#endregion

		#region Weighted

		public int Weighted(int[] weights)
		{
			int weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return Weighted(weights, weightSum);
		}

		public int Weighted(int[] weights, int weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (_random.Range().HalfOpen(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public int Weighted(uint[] weights)
		{
			uint weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return Weighted(weights, weightSum);
		}

		public int Weighted(uint[] weights, uint weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (_random.Range().HalfOpen(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public int Weighted(float[] weights)
		{
			float weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return Weighted(weights, weightSum);
		}

		public int Weighted(float[] weights, float weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (_random.Range().HalfOpen(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public int Weighted(double[] weights)
		{
			double weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return Weighted(weights, weightSum);
		}

		public int Weighted(double[] weights, double weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (_random.Range().HalfOpen(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public int Weighted(int elementCount, System.Func<int, int> weightsAccessor)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return Weighted(elementCount, weightsAccessor, weightSum);
		}

		public int Weighted(int elementCount, System.Func<int, int> weightsAccessor, int weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (_random.Range().HalfOpen(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		public int Weighted(int elementCount, System.Func<int, uint> weightsAccessor)
		{
			uint weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return Weighted(elementCount, weightsAccessor, weightSum);
		}

		public int Weighted(int elementCount, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (_random.Range().HalfOpen(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		public int Weighted(int elementCount, System.Func<int, float> weightsAccessor)
		{
			float weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return Weighted(elementCount, weightsAccessor, weightSum);
		}

		public int Weighted(int elementCount, System.Func<int, float> weightsAccessor, float weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (_random.Range().HalfOpen(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		public int Weighted(int elementCount, System.Func<int, double> weightsAccessor)
		{
			double weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return Weighted(elementCount, weightsAccessor, weightSum);
		}

		public int Weighted(int elementCount, System.Func<int, double> weightsAccessor, double weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (_random.Range().HalfOpen(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		#endregion
	}

	public static class RandomIndexExtensions
	{
		public static RandomIndex Index(this IRandomEngine random)
		{
			return new RandomIndex(random);
		}

		public static T RandomElement<T>(this T[] array, IRandomEngine random)
		{
			return array[random.Index().Uniform(array)];
		}

		public static object RandomElement(this System.Collections.IList list, IRandomEngine random)
		{
			return list[random.Index().Uniform(list)];
		}

		public static object RandomElement(this System.Collections.ArrayList list, IRandomEngine random)
		{
			return list[random.Index().Uniform(list)];
		}

		public static T RandomElement<T>(this System.Collections.Generic.IList<T> list, IRandomEngine random)
		{
			return list[random.Index().Uniform(list)];
		}

		public static T RandomElement<T>(this System.Collections.Generic.List<T> list, IRandomEngine random)
		{
			return list[random.Index().Uniform(list)];
		}
	}
}
