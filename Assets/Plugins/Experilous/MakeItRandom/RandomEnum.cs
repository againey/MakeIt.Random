/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for randomly selecting items from enumeration types.
	/// </summary>
	public static class RandomEnum
	{
		/// <summary>
		/// Returns a random enumeration item uniformly selected by name from the enumeration type <typeparam name="T"/>.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random enumeration value belonging to <typeparam name="T"/>.</returns>
		/// <remarks><para>The uniform distribution for this function is based upon enumeration <em>names</em>, not <em>values</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then each of these names will still count
		/// as separate item, for the purposes of this function.</para>
		/// <note type="warning">This function is potentially slow.  If called more than once, it is advisable to use
		/// <see cref="Prepare`1{T}()"/> instead to pay only once for any internal structure initialization cost and
		/// obtain a more efficient reusable delegate.</note></remarks>
		public static T Enum<T>(this IRandom random) where T : struct
		{
			return (T)System.Enum.GetValues(typeof(T)).RandomElement(random);
		}

		/// <summary>
		/// Returns a random enumeration item's string representation uniformly selected by name from the enumeration type <typeparam name="T"/>.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random enumeration value belonging to <typeparam name="T"/>.</returns>
		/// <remarks><para>The uniform distribution for this function is based upon enumeration <em>names</em>, not <em>values</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then each of these names will still count
		/// as separate item, for the purposes of this function.</para>
		/// <note type="warning">This function is potentially slow.  If called more than once, it is advisable to use
		/// <see cref="Prepare`1{T}()"/> instead to pay only once for any internal structure initialization cost and
		/// obtain a more efficient reusable delegate.</note></remarks>
		public static string EnumName<T>(this IRandom random) where T : struct
		{
			return System.Enum.GetNames(typeof(T)).RandomElement(random);
		}

		/// <summary>
		/// Returns a random enumeration item uniformly selected by value from the enumeration type <typeparam name="T"/>.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random enumeration value belonging to <typeparam name="T"/>.</returns>
		/// <remarks><para>The uniform distribution for this function is based upon enumeration <em>values</em>, not <em>names</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then all of these names will essentially
		/// count as one item, for the purposes of this function.</para>
		/// <note type="warning">This function is potentially slow.  If called more than once, it is advisable to use
		/// <see cref="PrepareDistinct`1{T}()"/> instead to pay only once for any internal structure initialization cost
		/// and obtain a more efficient reusable delegate.</note></remarks>
		public static T DistinctEnum<T>(this IRandom random) where T : struct
		{
			return BuildDistinctValuesArray<T>().RandomElement(random);
		}

		/// <summary>
		/// A delegate signature for a pre-constructed enumeration generator, based on whatever weighting criteria were chosen at the time of creation.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random enumeration value belonging to <typeparam name="T"/>.</returns>
		public delegate T EnumGeneratorDelegate<T>(IRandom random) where T : struct;

		/// <summary>
		/// Returns a random enumeration item selected according to the criteria which were chosen when the provided <paramref name="generator"/> was constructed.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="generator">The generator which was constructed to produce enumeration items according to a particular distribution.</param>
		/// <returns>A random enumeration value belonging to <typeparam name="T"/>.</returns>
		public static T Enum<T>(this IRandom random, EnumGeneratorDelegate<T> generator) where T : struct
		{
			return generator(random);
		}

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

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items uniformly selected by name from the enumeration type <typeparam name="T"/>.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <returns>An enumeration item generator which uniformly selects items by name.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The uniform distribution for this function is based upon enumeration <em>names</em>, not <em>values</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then each of these names will still count
		/// as separate item, for the purposes of this function.</para></remarks>
		public static EnumGeneratorDelegate<T> Prepare<T>() where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			return (IRandom random) => valuesArray.RandomElement(random);
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items uniformly selected by value from the enumeration type <typeparam name="T"/>.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <returns>An enumeration item generator which uniformly selects items by value.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <remarks><para>The uniform distribution for this function is based upon enumeration <em>values</em>, not <em>names</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then all of these names will essentially
		/// count as one item, for the purposes of this function.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareDistinct<T>() where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			return (IRandom random) => valuesArray.RandomElement(random);
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items selected by name from the enumeration type <typeparam name="T"/>, using the weights specified to determine the distribution.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="weights">The weights that will determine the distribution with which enumeration items are selected.</param>
		/// <returns>An enumeration item generator which selects items by name, using a weighted distribution.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The weighted distribution for this function is based upon enumeration <em>names</em>, not <em>values</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then each of these names will still count
		/// as separate item, for the purposes of this function.</para>
		/// <para>Any enumeration item whose name is not found in the provided <paramref name="weights"/> dictionary will automatically
		/// receive a weight of zero, and will therefore never be generated.  Any item in the <paramref name="weights"/> dictionary that
		/// does not correspond to any item in the enumeration will be ignored.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, int> weights) where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			int[] weightsArray = BuildWeightsArray<T, int>(weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items selected by name from the enumeration type <typeparam name="T"/>, using the weights specified to determine the distribution.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="weights">The weights that will determine the distribution with which enumeration items are selected.</param>
		/// <returns>An enumeration item generator which selects items by name, using a weighted distribution.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The weighted distribution for this function is based upon enumeration <em>names</em>, not <em>values</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then each of these names will still count
		/// as separate item, for the purposes of this function.</para>
		/// <para>Any enumeration item whose name is not found in the provided <paramref name="weights"/> dictionary will automatically
		/// receive a weight of zero, and will therefore never be generated.  Any item in the <paramref name="weights"/> dictionary that
		/// does not correspond to any item in the enumeration will be ignored.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, uint> weights) where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			uint[] weightsArray = BuildWeightsArray<T, uint>(weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items selected by name from the enumeration type <typeparam name="T"/>, using the weights specified to determine the distribution.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="weights">The weights that will determine the distribution with which enumeration items are selected.</param>
		/// <returns>An enumeration item generator which selects items by name, using a weighted distribution.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The weighted distribution for this function is based upon enumeration <em>names</em>, not <em>values</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then each of these names will still count
		/// as separate item, for the purposes of this function.</para>
		/// <para>Any enumeration item whose name is not found in the provided <paramref name="weights"/> dictionary will automatically
		/// receive a weight of zero, and will therefore never be generated.  Any item in the <paramref name="weights"/> dictionary that
		/// does not correspond to any item in the enumeration will be ignored.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, float> weights) where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			float[] weightsArray = BuildWeightsArray<T, float>(weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items selected by name from the enumeration type <typeparam name="T"/>, using the weights specified to determine the distribution.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="weights">The weights that will determine the distribution with which enumeration items are selected.</param>
		/// <returns>An enumeration item generator which selects items by name, using a weighted distribution.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The weighted distribution for this function is based upon enumeration <em>names</em>, not <em>values</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then each of these names will still count
		/// as separate item, for the purposes of this function.</para>
		/// <para>Any enumeration item whose name is not found in the provided <paramref name="weights"/> dictionary will automatically
		/// receive a weight of zero, and will therefore never be generated.  Any item in the <paramref name="weights"/> dictionary that
		/// does not correspond to any item in the enumeration will be ignored.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareWeighted<T>(Dictionary<string, double> weights) where T : struct
		{
			T[] valuesArray = BuildValuesArray<T>();
			double[] weightsArray = BuildWeightsArray<T, double>(weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items selected by value from the enumeration type <typeparam name="T"/>, using the weights specified to determine the distribution.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="weights">The weights that will determine the distribution with which enumeration items are selected.</param>
		/// <returns>An enumeration item generator which selects items by value, using a weighted distribution.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The uniform distribution for this function is based upon enumeration <em>values</em>, not <em>names</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then all of these names will essentially
		/// count as one item, for the purposes of this function.</para>
		/// <para>Any enumeration item whose value is not found in the provided <paramref name="weights"/> dictionary will automatically
		/// receive a weight of zero, and will therefore never be generated.  Any item in the <paramref name="weights"/> dictionary with
		/// an invalid value that does not correspond to any item in the enumeration will be ignored.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, int> weights) where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			int[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items selected by value from the enumeration type <typeparam name="T"/>, using the weights specified to determine the distribution.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="weights">The weights that will determine the distribution with which enumeration items are selected.</param>
		/// <returns>An enumeration item generator which selects items by value, using a weighted distribution.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The uniform distribution for this function is based upon enumeration <em>values</em>, not <em>names</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then all of these names will essentially
		/// count as one item, for the purposes of this function.</para>
		/// <para>Any enumeration item whose value is not found in the provided <paramref name="weights"/> dictionary will automatically
		/// receive a weight of zero, and will therefore never be generated.  Any item in the <paramref name="weights"/> dictionary with
		/// an invalid value that does not correspond to any item in the enumeration will be ignored.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, uint> weights) where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			uint[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items selected by value from the enumeration type <typeparam name="T"/>, using the weights specified to determine the distribution.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="weights">The weights that will determine the distribution with which enumeration items are selected.</param>
		/// <returns>An enumeration item generator which selects items by value, using a weighted distribution.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The uniform distribution for this function is based upon enumeration <em>values</em>, not <em>names</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then all of these names will essentially
		/// count as one item, for the purposes of this function.</para>
		/// <para>Any enumeration item whose value is not found in the provided <paramref name="weights"/> dictionary will automatically
		/// receive a weight of zero, and will therefore never be generated.  Any item in the <paramref name="weights"/> dictionary with
		/// an invalid value that does not correspond to any item in the enumeration will be ignored.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, float> weights) where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			float[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}

		/// <summary>
		/// Prepares an enumeration item generator which will produce random items selected by value from the enumeration type <typeparam name="T"/>, using the weights specified to determine the distribution.
		/// </summary>
		/// <typeparam name="T">The enumeration type from which items will be randomly selected.</typeparam>
		/// <param name="weights">The weights that will determine the distribution with which enumeration items are selected.</param>
		/// <returns>An enumeration item generator which selects items by value, using a weighted distribution.</returns>
		/// <remarks><para>This function will initialize internal data structures which are used to speed up all future requests for
		/// random enumeration items using this generator.  This is ideal when many such requests will need to be made for a specific
		/// enumeration type, and with a specific distribution.</para>
		/// <para>The uniform distribution for this function is based upon enumeration <em>values</em>, not <em>names</em>.
		/// If the enumeration happens to have the same value assigned to multiple names, then all of these names will essentially
		/// count as one item, for the purposes of this function.</para>
		/// <para>Any enumeration item whose value is not found in the provided <paramref name="weights"/> dictionary will automatically
		/// receive a weight of zero, and will therefore never be generated.  Any item in the <paramref name="weights"/> dictionary with
		/// an invalid value that does not correspond to any item in the enumeration will be ignored.</para></remarks>
		public static EnumGeneratorDelegate<T> PrepareDistinctWeighted<T>(Dictionary<T, double> weights) where T : struct
		{
			T[] valuesArray = BuildDistinctValuesArray<T>();
			double[] weightsArray = BuildDistinctWeightsArray(valuesArray, weights);
			return (IRandom random) => valuesArray[random.WeightedIndex(weightsArray)];
		}
	}
}
