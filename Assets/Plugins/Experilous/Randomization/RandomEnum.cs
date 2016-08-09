/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.Randomization
{
	public struct RandomEnum
	{
		private IRandomEngine _random;

		public RandomEnum(IRandomEngine random)
		{
			_random = random;
		}

		public T PickValue<T>() where T : struct
		{
			return (T)System.Enum.GetValues(typeof(T)).RandomElement(_random);
		}

		public T PickDistinctValue<T>() where T : struct
		{
			return BuildDistinctValuesArray<T>().RandomElement(_random);
		}

		public string PickName<T>() where T : struct
		{
			return System.Enum.GetNames(typeof(T)).RandomElement(_random);
		}

		public delegate T EnumGeneratorDelegate<T>() where T : struct;

		private static T[] BuildValuesArray<T>() where T : struct
		{
			var values = System.Enum.GetValues(typeof(T));
			if (values.Length == 0) throw new System.ArgumentException();

			T[] valuesArray = new T[values.Length];
			for (int i = 0; i < values.Length; ++i)
			{
				valuesArray[i] = (T)values.GetValue(i);
			}

			return valuesArray;
		}

		private static T[] BuildDistinctValuesArray<T>() where T : struct
		{
			var values = System.Enum.GetValues(typeof(T));
			if (values.Length == 0) throw new System.ArgumentException();

			int distinctCount = 1;
			T prevValue = (T)values.GetValue(0);
			for (int i = 1; i < values.Length; ++i)
			{
				T nextValue = (T)values.GetValue(i);
				if (!prevValue.Equals(nextValue))
				{
					++distinctCount;
					prevValue = nextValue;
				}
			}

			T[] valuesArray = new T[distinctCount];
			int index = 0;
			prevValue = (T)values.GetValue(0);
			valuesArray[index] = prevValue;
			for (int i = 1; i < values.Length; ++i)
			{
				T nextValue = (T)values.GetValue(i);
				if (!prevValue.Equals(nextValue))
				{
					valuesArray[++index] = nextValue;
					prevValue = nextValue;
				}
			}

			return valuesArray;
		}

		private static TWeight[] BuildWeightsArray<T, TWeight>(Dictionary<string, TWeight> weights) where T : struct
		{
			var names = System.Enum.GetNames(typeof(T));
			if (names.Length == 0) throw new System.ArgumentException();

			TWeight[] weightsArray = new TWeight[names.Length];
			for (int i = 0; i < names.Length; ++i)
			{
				TWeight weight;
				if (weights.TryGetValue(names[i], out weight))
				{
					weightsArray[i] = weight;
				}
			}

			return weightsArray;
		}

		private static TWeight[] BuildDistinctWeightsArray<T, TWeight>(T[] valuesArray, Dictionary<T, TWeight> weights) where T : struct
		{
			TWeight[] weightsArray = new TWeight[valuesArray.Length];
			for (int i = 0; i < valuesArray.Length; ++i)
			{
				TWeight weight;
				if (weights.TryGetValue(valuesArray[i], out weight))
				{
					weightsArray[i] = weight;
				}
			}

			return weightsArray;
		}

		public EnumGeneratorDelegate<T> Prepare<T>() where T : struct
		{
			IRandomEngine random = _random;
			T[] valuesArray = BuildValuesArray<T>();
			return () => valuesArray.RandomElement(random);
		}

		public EnumGeneratorDelegate<T> PrepareDistinct<T>() where T : struct
		{
			IRandomEngine random = _random;
			T[] valuesArray = BuildDistinctValuesArray<T>();
			return () => valuesArray.RandomElement(random);
		}

		public EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, int> weights) where T : struct
		{
			RandomIndex randomIndex = _random.Index();
			T[] valuesArray = BuildValuesArray<T>();
			int[] weightsArray = BuildWeightsArray<T, int>(weights);
			return () => valuesArray[randomIndex.Weighted(weightsArray)];
		}

		public EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, uint> weights) where T : struct
		{
			RandomIndex randomIndex = _random.Index();
			T[] valuesArray = BuildValuesArray<T>();
			uint[] weightsArray = BuildWeightsArray<T, uint>(weights);
			return () => valuesArray[randomIndex.Weighted(weightsArray)];
		}

		public EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, float> weights) where T : struct
		{
			RandomIndex randomIndex = _random.Index();
			T[] valuesArray = BuildValuesArray<T>();
			float[] weightsArray = BuildWeightsArray<T, float>(weights);
			return () => valuesArray[randomIndex.Weighted(weightsArray)];
		}

		public EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, double> weights) where T : struct
		{
			RandomIndex randomIndex = _random.Index();
			T[] valuesArray = BuildValuesArray<T>();
			double[] weightsArray = BuildWeightsArray<T, double>(weights);
			return () => valuesArray[randomIndex.Weighted(weightsArray)];
		}

		public EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, int> weights) where T : struct
		{
			RandomIndex randomIndex = _random.Index();
			T[] valuesArray = BuildDistinctValuesArray<T>();
			int[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return () => valuesArray[randomIndex.Weighted(weightsArray)];
		}

		public EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, uint> weights) where T : struct
		{
			RandomIndex randomIndex = _random.Index();
			T[] valuesArray = BuildDistinctValuesArray<T>();
			uint[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return () => valuesArray[randomIndex.Weighted(weightsArray)];
		}

		public EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, float> weights) where T : struct
		{
			RandomIndex randomIndex = _random.Index();
			T[] valuesArray = BuildDistinctValuesArray<T>();
			float[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return () => valuesArray[randomIndex.Weighted(weightsArray)];
		}

		public EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, double> weights) where T : struct
		{
			RandomIndex randomIndex = _random.Index();
			T[] valuesArray = BuildDistinctValuesArray<T>();
			double[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return () => valuesArray[randomIndex.Weighted(weightsArray)];
		}
	}

	public static class RandomEnumExtensions
	{
		public static RandomEnum Enum(this IRandomEngine random)
		{
			return new RandomEnum(random);
		}
	}
}
