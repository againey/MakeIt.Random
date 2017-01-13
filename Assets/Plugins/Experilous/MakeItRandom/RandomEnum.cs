/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// An interface for any generator of enumeration items.
	/// </summary>
	/// <typeparam name="TEnum">The enumeration type which this generator selects from and returns.</typeparam>
	public interface IEnumGenerator<TEnum> where TEnum : struct
	{
		/// <summary>
		/// Get the next random enumeration item selected by the generator.
		/// </summary>
		/// <returns>The next random enumeration item randomly selected according to the generator implementation.</returns>
		TEnum Next();

		/// <summary>
		/// Get the name of the next random enumeration item selected by the generator.
		/// </summary>
		/// <returns>The name of the next random enumeration item randomly selected according to the generator implementation.</returns>
		/// <remarks>This function is particularly useful when the enumeration contains multiple items with the same value.</remarks>
		string NextName();

		/// <summary>
		/// Get the next random enumeration item selected by the generator.
		/// </summary>
		/// <param name="name">The name of the next random enumeration item randomly selected according to the generator implementation and returned by the function.</param>
		/// <returns>The next random enumeration item randomly selected according to the generator implementation.</returns>
		/// <remarks>This function is particularly useful when the enumeration contains multiple items with the same value.</remarks>
		TEnum Next(out string name);
	}

	/// <summary>
	/// A static class of extension methods for randomly selecting items from enumeration types.
	/// </summary>
	public static class RandomEnum
	{
		#region Private Generator Classes

		#region By Value

		private abstract class ByValueEnumGeneratorBase<TEnum> : IEnumGenerator<TEnum> where TEnum : struct
		{
			protected TEnum[] _values;

			public ByValueEnumGeneratorBase()
			{
				System.Array values = System.Enum.GetValues(typeof(TEnum));
				if (values.Length == 0) throw new System.ArgumentException(string.Format("The generic type argument {0} must have at least one enumeration item.", typeof(TEnum).Name), "TEnum");

				int distinctCount = 1;
				TEnum prevValue = (TEnum)values.GetValue(0);
				for (int i = 1; i < values.Length; ++i)
				{
					TEnum nextValue = (TEnum)values.GetValue(i);
					if (!prevValue.Equals(nextValue))
					{
						++distinctCount;
						prevValue = nextValue;
					}
				}

				if (distinctCount == values.Length)
				{
					_values = values as TEnum[];
					if (_values == null)
					{
						_values = new TEnum[values.Length];
						for (int i = 0; i < values.Length; ++i)
						{
							_values[i] = (TEnum)values.GetValue(i);
						}
					}
				}
				else
				{
					_values = new TEnum[distinctCount];
					int index = 0;
					prevValue = (TEnum)values.GetValue(0);
					_values[index] = prevValue;
					for (int i = 1; i < values.Length; ++i)
					{
						TEnum nextValue = (TEnum)values.GetValue(i);
						if (!prevValue.Equals(nextValue))
						{
							_values[++index] = nextValue;
							prevValue = nextValue;
						}
					}
				}
			}

			public abstract TEnum Next();
			public abstract string NextName();
			public abstract TEnum Next(out string name);
		}

		private class ByValueEnumGenerator<TEnum> : ByValueEnumGeneratorBase<TEnum>, IEnumGenerator<TEnum> where TEnum : struct
		{
			private IRangeGenerator<int> _indexGenerator;

			public ByValueEnumGenerator(IRandom random)
			{
				_indexGenerator = random.MakeRangeCOGenerator(_values.Length);
			}

			public override TEnum Next()
			{
				return _values[_indexGenerator.Next()];
			}

			public override string NextName()
			{
				return _values[_indexGenerator.Next()].ToString();
			}

			public override TEnum Next(out string name)
			{
				TEnum value = _values[_indexGenerator.Next()];
				name = value.ToString();
				return value;
			}
		}

		private abstract class ByValueWeightedEnumGenerator<TEnum, TWeight, TWeightSum> : ByValueEnumGeneratorBase<TEnum> where TEnum : struct
		{
			protected IWeightedIndexGenerator<TWeight, TWeightSum> _indexGenerator;

			public override TEnum Next()
			{
				return _values[_indexGenerator.Next()];
			}

			public override string NextName()
			{
				return _values[_indexGenerator.Next()].ToString();
			}

			public override TEnum Next(out string name)
			{
				TEnum value = _values[_indexGenerator.Next()];
				name = value.ToString();
				return value;
			}
		}

		private class ByValueSByteWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, sbyte, int> where TEnum : struct
		{
			public ByValueSByteWeightedEnumGenerator(IRandom random, System.Func<TEnum, sbyte> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueByteWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, byte, uint> where TEnum : struct
		{
			public ByValueByteWeightedEnumGenerator(IRandom random, System.Func<TEnum, byte> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueShortWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, short, int> where TEnum : struct
		{
			public ByValueShortWeightedEnumGenerator(IRandom random, System.Func<TEnum, short> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueUShortWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, ushort, uint> where TEnum : struct
		{
			public ByValueUShortWeightedEnumGenerator(IRandom random, System.Func<TEnum, ushort> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueIntWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, int, int> where TEnum : struct
		{
			public ByValueIntWeightedEnumGenerator(IRandom random, System.Func<TEnum, int> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueUIntWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, uint, uint> where TEnum : struct
		{
			public ByValueUIntWeightedEnumGenerator(IRandom random, System.Func<TEnum, uint> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueLongWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, long, long> where TEnum : struct
		{
			public ByValueLongWeightedEnumGenerator(IRandom random, System.Func<TEnum, long> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueULongWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, ulong, ulong> where TEnum : struct
		{
			public ByValueULongWeightedEnumGenerator(IRandom random, System.Func<TEnum, ulong> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueFloatWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, float, float> where TEnum : struct
		{
			public ByValueFloatWeightedEnumGenerator(IRandom random, System.Func<TEnum, float> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		private class ByValueDoubleWeightedEnumGenerator<TEnum> : ByValueWeightedEnumGenerator<TEnum, double, double> where TEnum : struct
		{
			public ByValueDoubleWeightedEnumGenerator(IRandom random, System.Func<TEnum, double> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_values.Length, (int index) => weightsAccessor(_values[index]));
			}
		}

		#endregion

		#region By Name

		private abstract class ByNameEnumGeneratorBase<TEnum> : IEnumGenerator<TEnum> where TEnum : struct
		{
			protected TEnum[] _values;
			protected string[] _names;

			public ByNameEnumGeneratorBase()
			{
				System.Array values = System.Enum.GetValues(typeof(TEnum));
				if (values.Length == 0) throw new System.ArgumentException(string.Format("The generic type argument {0} must have at least one enumeration item.", typeof(TEnum).Name), "TEnum");

				_values = values as TEnum[];
				if (_values == null)
				{
					_values = new TEnum[values.Length];
					for (int i = 0; i < values.Length; ++i)
					{
						_values[i] = (TEnum)values.GetValue(i);
					}
				}

				_names = System.Enum.GetNames(typeof(TEnum));
			}

			public abstract TEnum Next();
			public abstract string NextName();
			public abstract TEnum Next(out string name);
		}

		private class ByNameEnumGenerator<TEnum> : ByNameEnumGeneratorBase<TEnum> where TEnum : struct
		{
			private IRangeGenerator<int> _indexGenerator;

			public ByNameEnumGenerator(IRandom random)
			{
				_indexGenerator = random.MakeRangeCOGenerator(_values.Length);
			}

			public override TEnum Next()
			{
				return _values[_indexGenerator.Next()];
			}

			public override string NextName()
			{
				return _names[_indexGenerator.Next()];
			}

			public override TEnum Next(out string name)
			{
				int index = _indexGenerator.Next();
				name = _names[index];
				return _values[index];
			}
		}

		private abstract class ByNameWeightedEnumGenerator<TEnum, TWeight, TWeightSum> : ByNameEnumGeneratorBase<TEnum> where TEnum : struct
		{
			protected IWeightedIndexGenerator<TWeight, TWeightSum> _indexGenerator;

			public override TEnum Next()
			{
				return _values[_indexGenerator.Next()];
			}

			public override string NextName()
			{
				return _names[_indexGenerator.Next()];
			}

			public override TEnum Next(out string name)
			{
				int index = _indexGenerator.Next();
				name = _names[index];
				return _values[index];
			}
		}

		private class ByNameSByteWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, sbyte, int> where TEnum : struct
		{
			public ByNameSByteWeightedEnumGenerator(IRandom random, System.Func<string, sbyte> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameByteWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, byte, uint> where TEnum : struct
		{
			public ByNameByteWeightedEnumGenerator(IRandom random, System.Func<string, byte> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameShortWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, short, int> where TEnum : struct
		{
			public ByNameShortWeightedEnumGenerator(IRandom random, System.Func<string, short> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameUShortWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, ushort, uint> where TEnum : struct
		{
			public ByNameUShortWeightedEnumGenerator(IRandom random, System.Func<string, ushort> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameIntWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, int, int> where TEnum : struct
		{
			public ByNameIntWeightedEnumGenerator(IRandom random, System.Func<string, int> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameUIntWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, uint, uint> where TEnum : struct
		{
			public ByNameUIntWeightedEnumGenerator(IRandom random, System.Func<string, uint> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameLongWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, long, long> where TEnum : struct
		{
			public ByNameLongWeightedEnumGenerator(IRandom random, System.Func<string, long> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameULongWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, ulong, ulong> where TEnum : struct
		{
			public ByNameULongWeightedEnumGenerator(IRandom random, System.Func<string, ulong> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameFloatWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, float, float> where TEnum : struct
		{
			public ByNameFloatWeightedEnumGenerator(IRandom random, System.Func<string, float> weightsAccessor) : base()
			{
				_indexGenerator = random.MakeWeightedIndexGenerator(_names.Length, (int index) => weightsAccessor(_names[index]));
			}
		}

		private class ByNameDoubleWeightedEnumGenerator<TEnum> : ByNameWeightedEnumGenerator<TEnum, double, double> where TEnum : struct
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
		/// Returns an enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, uniformly distributed either by unique value or by name.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="byName">If false, indicates that multiple enumeration items with identical values should be treated as a single item.  Otherwise, each item name will be treated as a distinct item.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		public static IEnumGenerator<TEnum> MakeEnumGenerator<TEnum>(this IRandom random, bool byName = false) where TEnum : struct
		{
			if (byName == false)
			{
				return new ByValueEnumGenerator<TEnum>(random);
			}
			else
			{
				return new ByNameEnumGenerator<TEnum>(random);
			}
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, sbyte})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, sbyte})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, sbyte> weightsAccessor) where TEnum : struct
		{
			return new ByValueSByteWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, sbyte})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, sbyte})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, sbyte> weightsAccessor) where TEnum : struct
		{
			return new ByNameSByteWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, byte})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, byte})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, byte> weightsAccessor) where TEnum : struct
		{
			return new ByValueByteWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, byte})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, byte})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, byte> weightsAccessor) where TEnum : struct
		{
			return new ByNameByteWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, short})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, short})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, short> weightsAccessor) where TEnum : struct
		{
			return new ByValueShortWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, short})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, short})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, short> weightsAccessor) where TEnum : struct
		{
			return new ByNameShortWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, ushort})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, ushort})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, ushort> weightsAccessor) where TEnum : struct
		{
			return new ByValueUShortWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, ushort})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, ushort})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, ushort> weightsAccessor) where TEnum : struct
		{
			return new ByNameUShortWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, int})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, int})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, int> weightsAccessor) where TEnum : struct
		{
			return new ByValueIntWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, int})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, int})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, int> weightsAccessor) where TEnum : struct
		{
			return new ByNameIntWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, uint})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, uint})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, uint> weightsAccessor) where TEnum : struct
		{
			return new ByValueUIntWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, uint})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, uint})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, uint> weightsAccessor) where TEnum : struct
		{
			return new ByNameUIntWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, long})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, long})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, long> weightsAccessor) where TEnum : struct
		{
			return new ByValueLongWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, long})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, long})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, long> weightsAccessor) where TEnum : struct
		{
			return new ByNameLongWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, ulong})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, ulong})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, ulong> weightsAccessor) where TEnum : struct
		{
			return new ByValueULongWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, ulong})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, ulong})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, ulong> weightsAccessor) where TEnum : struct
		{
			return new ByNameULongWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, float})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, float})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, float> weightsAccessor) where TEnum : struct
		{
			return new ByValueFloatWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, float})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, float})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, float> weightsAccessor) where TEnum : struct
		{
			return new ByNameFloatWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration values to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>values</em>, any set of enumeration items with
		/// different names but identical values will be treated as a single item.  If you would rather treat them each as a distinct
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, double})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{string, double})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<TEnum, double> weightsAccessor) where TEnum : struct
		{
			return new ByValueDoubleWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted enum generator which will return random enumeration items from <typeparamref name="TEnum"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type for which the generate will be created.  Must contain at least one enumeration item.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps enumeration names to the weights that will determine the distribution by which enumeration items are produced.  Must return valid weight values for all enumeration items in <typeparamref name="TEnum"/>.</param>
		/// <returns>An enum generator which will return random enumeration items from <typeparamref name="TEnum"/>.</returns>
		/// <remarks>Because the weights accessor delegate maps from enumeration <em>names</em>, any set of enumeration items with
		/// identical values but different names will be treated as distinct items.  If you would rather treat them all as a single
		/// item, then use <see cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, double})"/> instead.</remarks>
		/// <seealso cref="MakeWeightedEnumGenerator{TEnum}(IRandom, System.Func{TEnum, double})"/>
		public static IEnumGenerator<TEnum> MakeWeightedEnumGenerator<TEnum>(this IRandom random, System.Func<string, double> weightsAccessor) where TEnum : struct
		{
			return new ByNameDoubleWeightedEnumGenerator<TEnum>(random, weightsAccessor);
		}

		#endregion
	}
}
