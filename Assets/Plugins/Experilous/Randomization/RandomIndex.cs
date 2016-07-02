/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public static class RandomIndex
	{
		#region Uniform

		public static int Uniform(int length, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(length, engine);
		}

		public static uint Uniform(uint length, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(length, engine);
		}

		public static int Uniform<T>(T[] array, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(array.Length, engine);
		}

		public static int Uniform(System.Collections.IList list, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(list.Count, engine);
		}

		public static int Uniform(System.Collections.ArrayList list, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(list.Count, engine);
		}

		public static int Uniform<T>(System.Collections.Generic.IList<T> list, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(list.Count, engine);
		}

		public static int Uniform<T>(System.Collections.Generic.List<T> list, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(list.Count, engine);
		}

		#endregion

		#region RandomElement (extensions)

		public static T RandomElement<T>(this T[] array, IRandomEngine engine)
		{
			return array[Uniform(array, engine)];
		}

		public static object RandomElement(this System.Collections.IList list, IRandomEngine engine)
		{
			return list[Uniform(list, engine)];
		}

		public static object RandomElement(this System.Collections.ArrayList list, IRandomEngine engine)
		{
			return list[Uniform(list, engine)];
		}

		public static T RandomElement<T>(this System.Collections.Generic.IList<T> list, IRandomEngine engine)
		{
			return list[Uniform(list, engine)];
		}

		public static T RandomElement<T>(this System.Collections.Generic.List<T> list, IRandomEngine engine)
		{
			return list[Uniform(list, engine)];
		}

		#endregion

		#region Weighted

		public static int Weighted(int[] weights, IRandomEngine engine)
		{
			int weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return Weighted(weights, weightSum, engine);
		}

		public static int Weighted(int[] weights, int weightSum, IRandomEngine engine)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (RandomRange.HalfOpen(weightSum, engine) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public static int Weighted(uint[] weights, IRandomEngine engine)
		{
			uint weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return Weighted(weights, weightSum, engine);
		}

		public static int Weighted(uint[] weights, uint weightSum, IRandomEngine engine)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (RandomRange.HalfOpen(weightSum, engine) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public static int Weighted(float[] weights, IRandomEngine engine)
		{
			float weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return Weighted(weights, weightSum, engine);
		}

		public static int Weighted(float[] weights, float weightSum, IRandomEngine engine)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (RandomRange.HalfOpen(weightSum, engine) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		public static int Weighted(double[] weights, IRandomEngine engine)
		{
			double weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return Weighted(weights, weightSum, engine);
		}

		public static int Weighted(double[] weights, double weightSum, IRandomEngine engine)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (RandomRange.HalfOpen(weightSum, engine) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		#endregion
	}
}
