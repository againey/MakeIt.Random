/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random indices, typically for accessing collections using a zero-based indexing scheme.
	/// </summary>
	public static class RandomIndex
	{
		#region Uniform

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random index in the range [0, <paramref name="length"/>).</returns>
		public static int UniformIndex(this IRandom random, int length)
		{
			return random.RangeCO(length);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random index in the range [0, <paramref name="length"/>).</returns>
		public static uint UniformIndex(this IRandom random, uint length)
		{
			return random.RangeCO(length);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="array"/>.Length), suitable for indexing into <paramref name="array"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the array.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="array">The collection for which a random index will be generated.</param>
		/// <returns>A random index in the range [0, <paramref name="array"/>.Length).</returns>
		public static int UniformIndex<T>(this IRandom random, T[] array)
		{
			return random.RangeCO(array.Length);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection for which a random index will be generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		public static int UniformIndex(this IRandom random, System.Collections.IList list)
		{
			return random.RangeCO(list.Count);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection for which a random index will be generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		public static int UniformIndex(this IRandom random, System.Collections.ArrayList list)
		{
			return random.RangeCO(list.Count);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection for which a random index will be generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		public static int UniformIndex<T>(this IRandom random, System.Collections.Generic.IList<T> list)
		{
			return random.RangeCO(list.Count);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection for which a random index will be generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		public static int UniformIndex<T>(this IRandom random, System.Collections.Generic.List<T> list)
		{
			return random.RangeCO(list.Count);
		}

		#endregion

		#region Weighted

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, sbyte[] weights)
		{
			int weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, sbyte[] weights, int weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, byte[] weights)
		{
			uint weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, byte[] weights, uint weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, short[] weights)
		{
			int weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, short[] weights, int weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, ushort[] weights)
		{
			uint weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, ushort[] weights, uint weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, int[] weights)
		{
			int weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, int[] weights, int weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, uint[] weights)
		{
			uint weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, uint[] weights, uint weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, long[] weights)
		{
			long weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, long[] weights, long weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, ulong[] weights)
		{
			ulong weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, ulong[] weights, ulong weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, float[] weights)
		{
			float weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, float[] weights, float weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, double[] weights)
		{
			double weightSum = 0;
			foreach (var weight in weights)
			{
				weightSum += weight;
			}
			return random.WeightedIndex(weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		public static int WeightedIndex(this IRandom random, double[] weights, double weightSum)
		{
			int index = 0;
			while (index < weights.Length)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor, int weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, byte> weightsAccessor)
		{
			uint weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, byte> weightsAccessor, uint weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, short> weightsAccessor)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, short> weightsAccessor, int weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor)
		{
			uint weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor, uint weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, int> weightsAccessor)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, int> weightsAccessor, int weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, uint> weightsAccessor)
		{
			uint weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, long> weightsAccessor)
		{
			long weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, long> weightsAccessor, long weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor)
		{
			ulong weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor, ulong weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, float> weightsAccessor)
		{
			float weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, float> weightsAccessor, float weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, double> weightsAccessor)
		{
			double weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, double> weightsAccessor, double weightSum)
		{
			int index = 0;
			while (index < elementCount)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
		}

		#endregion

		#region RandomElement (container extensions)

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="array"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the array.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random element from <paramref name="array"/>.</returns>
		public static T RandomElement<T>(this T[] array, IRandom random)
		{
			return array[random.UniformIndex(array)];
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		public static object RandomElement(this System.Collections.IList list, IRandom random)
		{
			return list[random.UniformIndex(list)];
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		public static object RandomElement(this System.Collections.ArrayList list, IRandom random)
		{
			return list[random.UniformIndex(list)];
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		public static T RandomElement<T>(this System.Collections.Generic.IList<T> list, IRandom random)
		{
			return list[random.UniformIndex(list)];
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		public static T RandomElement<T>(this System.Collections.Generic.List<T> list, IRandom random)
		{
			return list[random.UniformIndex(list)];
		}

		#endregion
	}
}
