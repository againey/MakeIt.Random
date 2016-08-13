/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom
{
	public static class RandomEnum
	{
		public static T Enum<T>(this IRandom random) where T : struct
		{
			return (T)System.Enum.GetValues(typeof(T)).RandomElement(random);
		}

		public static T Enum<T>(this IRandom random, EnumGeneratorDelegate<T> generator) where T : struct
		{
			return generator(random);
		}

		public static T DistinctEnum<T>(this IRandom random) where T : struct
		{
			return BuildDistinctValuesArray<T>().RandomElement(random);
		}

		public static string EnumName<T>(this IRandom random) where T : struct
		{
			return System.Enum.GetNames(typeof(T)).RandomElement(random);
		}

		public delegate T EnumGeneratorDelegate<T>(IRandom random) where T : struct;

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

		public static EnumGeneratorDelegate<T> Prepare<T>() where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			return (IRandom random) => valuesArray.RandomElement(random);
		}

		public static EnumGeneratorDelegate<T> PrepareDistinct<T>() where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			return (IRandom random) => valuesArray.RandomElement(random);
		}

		public static EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, int> weights) where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			int[] weightsArray = BuildWeightsArray<T, int>(weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		public static EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, uint> weights) where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			uint[] weightsArray = BuildWeightsArray<T, uint>(weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		public static EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, float> weights) where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			float[] weightsArray = BuildWeightsArray<T, float>(weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		public static EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, double> weights) where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			double[] weightsArray = BuildWeightsArray<T, double>(weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		public static EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, int> weights) where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			int[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		public static EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, uint> weights) where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			uint[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		public static EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, float> weights) where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			float[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		public static EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, double> weights) where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			double[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}
	}
}
