/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom
{
	public interface IEnumGenerator<T> where T : struct
	{
		T Next();
		string NextName();
		T Next(out string name);
	}

	/// <summary>
	/// A static class of extension methods for randomly selecting items from enumeration types.
	/// </summary>
	public static class RandomEnum
	{
		#region Private Generator Classes

		#region By Value

		private abstract class ByValueEnumGeneratorBase<T> : IEnumGenerator<T> where T : struct
		{
			protected T[] _values;

			public ByValueEnumGeneratorBase()
			{
				System.Array values = System.Enum.GetValues(typeof(T));
				if (values.Length == 0) throw new System.ArgumentException(string.Format("The generic type argument {0} must have at least one enumeration item.", typeof(T).Name), "T");

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

				if (distinctCount == values.Length)
				{
					_values = values as T[];
					if (_values == null)
					{
						_values = new T[values.Length];
						for (int i = 0; i < values.Length; ++i)
						{
							_values[i] = (T)values.GetValue(i);
						}
					}
				}
				else
				{
					_values = new T[distinctCount];
					int index = 0;
					prevValue = (T)values.GetValue(0);
					_values[index] = prevValue;
					for (int i = 1; i < values.Length; ++i)
					{
						T nextValue = (T)values.GetValue(i);
						if (!prevValue.Equals(nextValue))
						{
							_values[++index] = nextValue;
							prevValue = nextValue;
						}
					}
				}
			}

			public abstract T Next();
			public abstract string NextName();
			public abstract T Next(out string name);
		}

		private class ByValueEnumGenerator<T> : ByValueEnumGeneratorBase<T>, IEnumGenerator<T> where T : struct
		{
			private IIntGenerator _indexGenerator;

			public ByValueEnumGenerator(IRandom random)
			{
				_indexGenerator = random.MakeRangeCOGenerator(_values.Length);
			}

			public override T Next()
			{
				return _values[_indexGenerator.Next()];
			}

			public override string NextName()
			{
				return _values[_indexGenerator.Next()].ToString();
			}

			public override T Next(out string name)
			{
				T value = _values[_indexGenerator.Next()];
				name = value.ToString();
				return value;
			}
		}

		private abstract class ByValueWeightedEnumGenerator<T, TWeight, TWeightSum> : ByValueEnumGeneratorBase<T> where T : struct
		{
			protected IWeightedIndexGenerator<TWeight, TWeightSum> _indexGenerator;

			public override T Next()
			{
				return _values[_indexGenerator.Next()];
			}

			public override string NextName()
			{
				return _values[_indexGenerator.Next()].ToString();
			}

			public override T Next(out string name)
			{
				T value = _values[_indexGenerator.Next()];
				name = value.ToString();
				return value;
			}
		}

		private class ByValueSByteWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, sbyte, int> where T : struct
		{
			public ByValueSByteWeightedEnumGenerator(IRandom random, System.Func<T, sbyte> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueByteWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, byte, uint> where T : struct
		{
			public ByValueByteWeightedEnumGenerator(IRandom random, System.Func<T, byte> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueShortWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, short, int> where T : struct
		{
			public ByValueShortWeightedEnumGenerator(IRandom random, System.Func<T, short> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueUShortWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, ushort, uint> where T : struct
		{
			public ByValueUShortWeightedEnumGenerator(IRandom random, System.Func<T, ushort> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueIntWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, int, int> where T : struct
		{
			public ByValueIntWeightedEnumGenerator(IRandom random, System.Func<T, int> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueUIntWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, uint, uint> where T : struct
		{
			public ByValueUIntWeightedEnumGenerator(IRandom random, System.Func<T, uint> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueLongWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, long, long> where T : struct
		{
			public ByValueLongWeightedEnumGenerator(IRandom random, System.Func<T, long> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueULongWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, ulong, ulong> where T : struct
		{
			public ByValueULongWeightedEnumGenerator(IRandom random, System.Func<T, ulong> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueFloatWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, float, float> where T : struct
		{
			public ByValueFloatWeightedEnumGenerator(IRandom random, System.Func<T, float> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueDoubleWeightedEnumGenerator<T> : ByValueWeightedEnumGenerator<T, double, double> where T : struct
		{
			public ByValueDoubleWeightedEnumGenerator(IRandom random, System.Func<T, double> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		#endregion

		#region By Name

		private abstract class ByNameEnumGeneratorBase<T> : IEnumGenerator<T> where T : struct
		{
			protected T[] _values;
			protected string[] _names;

			public ByNameEnumGeneratorBase()
			{
				System.Array values = System.Enum.GetValues(typeof(T));
				if (values.Length == 0) throw new System.ArgumentException(string.Format("The generic type argument {0} must have at least one enumeration item.", typeof(T).Name), "T");

				_values = values as T[];
				if (_values == null)
				{
					_values = new T[values.Length];
					for (int i = 0; i < values.Length; ++i)
					{
						_values[i] = (T)values.GetValue(i);
					}
				}

				_names = System.Enum.GetNames(typeof(T));
			}

			public abstract T Next();
			public abstract string NextName();
			public abstract T Next(out string name);
		}

		private class ByNameEnumGenerator<T> : ByNameEnumGeneratorBase<T> where T : struct
		{
			private IIntGenerator _indexGenerator;

			public ByNameEnumGenerator(IRandom random)
			{
				_indexGenerator = random.MakeRangeCOGenerator(_values.Length);
			}

			public override T Next()
			{
				return _values[_indexGenerator.Next()];
			}

			public override string NextName()
			{
				return _names[_indexGenerator.Next()];
			}

			public override T Next(out string name)
			{
				int index = _indexGenerator.Next();
				name = _names[index];
				return _values[index];
			}
		}

		private abstract class ByNameWeightedEnumGenerator<T, TWeight, TWeightSum> : ByNameEnumGeneratorBase<T> where T : struct
		{
			protected IWeightedIndexGenerator<TWeight, TWeightSum> _indexGenerator;

			public override T Next()
			{
				return _values[_indexGenerator.Next()];
			}

			public override string NextName()
			{
				return _names[_indexGenerator.Next()];
			}

			public override T Next(out string name)
			{
				int index = _indexGenerator.Next();
				name = _names[index];
				return _values[index];
			}
		}

		private class ByNameSByteWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, sbyte, int> where T : struct
		{
			public ByNameSByteWeightedEnumGenerator(IRandom random, System.Func<string, sbyte> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameByteWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, byte, uint> where T : struct
		{
			public ByNameByteWeightedEnumGenerator(IRandom random, System.Func<string, byte> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameShortWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, short, int> where T : struct
		{
			public ByNameShortWeightedEnumGenerator(IRandom random, System.Func<string, short> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameUShortWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, ushort, uint> where T : struct
		{
			public ByNameUShortWeightedEnumGenerator(IRandom random, System.Func<string, ushort> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameIntWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, int, int> where T : struct
		{
			public ByNameIntWeightedEnumGenerator(IRandom random, System.Func<string, int> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameUIntWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, uint, uint> where T : struct
		{
			public ByNameUIntWeightedEnumGenerator(IRandom random, System.Func<string, uint> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameLongWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, long, long> where T : struct
		{
			public ByNameLongWeightedEnumGenerator(IRandom random, System.Func<string, long> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameULongWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, ulong, ulong> where T : struct
		{
			public ByNameULongWeightedEnumGenerator(IRandom random, System.Func<string, ulong> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameFloatWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, float, float> where T : struct
		{
			public ByNameFloatWeightedEnumGenerator(IRandom random, System.Func<string, float> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameDoubleWeightedEnumGenerator<T> : ByNameWeightedEnumGenerator<T, double, double> where T : struct
		{
			public ByNameDoubleWeightedEnumGenerator(IRandom random, System.Func<string, double> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		#endregion

		#endregion

		#region Public Extension Methods

		/// <summary>
		/// Returns an enum generator which will return random enumeration items from <paramref name="T"/>, uniformly distributed either by unique value or by name.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="byName">If false, indicates that multiple enumeration items with identical values should be treated as a single item.  Otherwise, each item name will be treated as a distinct item.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		public static IEnumGenerator<T> MakeEnumGenerator<T>(this IRandom random, bool byName = false) where T : struct
		{
			if (byName == false)
			{
				return new ByValueEnumGenerator<T>(random);
			}
			else
			{
				return new ByNameEnumGenerator<T>(random);
			}
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, sbyte})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, sbyte})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, sbyte> weightsAccessor) where T : struct
		{
			return new ByValueSByteWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, sbyte})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, sbyte})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, sbyte> weightsAccessor) where T : struct
		{
			return new ByNameSByteWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, byte})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, byte})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, byte> weightsAccessor) where T : struct
		{
			return new ByValueByteWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, byte})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, byte})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, byte> weightsAccessor) where T : struct
		{
			return new ByNameByteWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, short})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, short})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, short> weightsAccessor) where T : struct
		{
			return new ByValueShortWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, short})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, short})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, short> weightsAccessor) where T : struct
		{
			return new ByNameShortWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, ushort})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, ushort})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, ushort> weightsAccessor) where T : struct
		{
			return new ByValueUShortWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, ushort})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, ushort})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, ushort> weightsAccessor) where T : struct
		{
			return new ByNameUShortWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, int})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, int})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, int> weightsAccessor) where T : struct
		{
			return new ByValueIntWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, int})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, int})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, int> weightsAccessor) where T : struct
		{
			return new ByNameIntWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, uint})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, uint})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, uint> weightsAccessor) where T : struct
		{
			return new ByValueUIntWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, uint})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, uint})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, uint> weightsAccessor) where T : struct
		{
			return new ByNameUIntWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, long})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, long})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, long> weightsAccessor) where T : struct
		{
			return new ByValueLongWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, long})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, long})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, long> weightsAccessor) where T : struct
		{
			return new ByNameLongWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, ulong})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, ulong})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, ulong> weightsAccessor) where T : struct
		{
			return new ByValueULongWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, ulong})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, ulong})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, ulong> weightsAccessor) where T : struct
		{
			return new ByNameULongWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, float})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, float})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, float> weightsAccessor) where T : struct
		{
			return new ByValueFloatWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, float})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, float})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, float> weightsAccessor) where T : struct
		{
			return new ByNameFloatWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, double})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{string, double})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<T, double> weightsAccessor) where T : struct
		{
			return new ByValueDoubleWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <paramref name="T"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <paramref name="T"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <paramref name="T"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, double})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator`1{T}(IRandom, System.Func`2{T, double})"/>
		public static IEnumGenerator<T> MakeWeightedEnumGenerator<T>(this IRandom random, System.Func<string, double> weightsAccessor) where T : struct
		{
			return new ByNameDoubleWeightedEnumGenerator<T>(random, weightsAccessor);
		}

		#endregion
	}
}
