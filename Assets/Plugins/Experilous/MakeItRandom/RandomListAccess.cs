/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;
using IListNonGeneric = System.Collections.IList;

namespace Experilous.MakeItRandom
{
	#region Generator Interfaces

	/// <summary>
	/// An interface for any generator of list element access.
	/// </summary>
	/// <typeparam name="TElement">The element type contained by the referenced list and returned by the generator.</typeparam>
	public interface IElementGenerator<TElement>
	{
		/// <summary>
		/// Get the next random element selected by the generator.
		/// </summary>
		/// <returns>The next list element randomly selected according to the generator implementation.</returns>
		TElement Next();

		/// <summary>
		/// Get the next random index selected by the generator.
		/// </summary>
		/// <returns>The next list element index randomly selected according to the generator implementation.</returns>
		int NextIndex();

		/// <summary>
		/// Get the next random element selected by the generator.
		/// </summary>
		/// <param name="index">The index of the list element selected according to the generator implementation and returned by the function.</param>
		/// <returns>The next list element randomly selected according to the generator implementation.</returns>
		TElement Next(out int index);
	}

	/// <summary>
	/// An interface for any generator of list element indices with non-uniform distribution.
	/// </summary>
	/// <typeparam name="TWeight">The numeric type of the weights.</typeparam>
	/// <typeparam name="TWeightSum">The numeric type of the summation of weights</typeparam>
	public interface IWeightedIndexGenerator<TWeight, TWeightSum>
	{
		/// <summary>
		/// The sum of all weights.
		/// </summary>
		TWeightSum weightSum { get; }

		/// <summary>
		/// Get the next random index selected by the generator.
		/// </summary>
		/// <returns>The next list element index randomly selected according to the generator implementation.</returns>
		int Next();

		/// <summary>
		/// Get the next random index selected by the generator.
		/// </summary>
		/// <param name="weight">The weight of the element index selected according to the generator implementation and returned by the function.</param>
		/// <returns>The next list element index randomly selected according to the generator implementation.</returns>
		int Next(out TWeight weight);

		/// <summary>
		/// Informs the generator reread the weights and update any internal state accordingly.
		/// </summary>
		void UpdateWeights();

		/// <summary>
		/// Informs the generator to use the specified <paramref name="weights"/> and udpate any internal state accordingly.
		/// </summary>
		/// <param name="weights">The new weight values to be used for specifying the non-uniform distribution of element indices.</param>
		void UpdateWeights(TWeight[] weights);

		/// <summary>
		/// Informs the generator to use the weights provided by <paramref name="weightsAccessor"/> and udpate any internal state accordingly.
		/// </summary>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		void UpdateWeights(int elementCount, System.Func<int, TWeight> weightsAccessor);
	}

	/// <summary>
	/// An interface for any generator of list element access with non-uniform distribution.
	/// </summary>
	/// <typeparam name="TElement">The element type contained by the referenced list and returned by the generator.</typeparam>
	/// <typeparam name="TWeight">The numeric type of the weights.</typeparam>
	/// <typeparam name="TWeightSum">The numeric type of the summation of weights</typeparam>
	public interface IWeightedElementGenerator<TElement, TWeight, TWeightSum> : IElementGenerator<TElement>
	{
		/// <summary>
		/// The sum of all weights.
		/// </summary>
		TWeightSum weightSum { get; }

		/// <summary>
		/// Get the next random index selected by the generator.
		/// </summary>
		/// <param name="weight">The weight of the element selected according to the generator implementation and returned by the function.</param>
		/// <returns>The next list element randomly selected according to the generator implementation.</returns>
		TElement Next(out TWeight weight);

		/// <summary>
		/// Get the next random index selected by the generator.
		/// </summary>
		/// <param name="weight">The weight of the element index selected according to the generator implementation and returned by the function.</param>
		/// <returns>The next list element index randomly selected according to the generator implementation.</returns>
		int NextIndex(out TWeight weight);

		/// <summary>
		/// Get the next random index selected by the generator.
		/// </summary>
		/// <param name="index">The index of the list element selected according to the generator implementation and returned by the function.</param>
		/// <param name="weight">The weight of the element selected according to the generator implementation and returned by the function.</param>
		/// <returns>The next list element randomly selected according to the generator implementation.</returns>
		TElement Next(out int index, out TWeight weight);

		/// <summary>
		/// Informs the generator reread the weights and update any internal state accordingly.
		/// </summary>
		void UpdateWeights();

		/// <summary>
		/// Informs the generator to use the specified <paramref name="weights"/> and udpate any internal state accordingly.
		/// </summary>
		/// <param name="weights">The new weight values to be used for specifying the non-uniform distribution of element indices.</param>
		void UpdateWeights(TWeight[] weights);

		/// <summary>
		/// Informs the generator to use the weights provided by <paramref name="weightsAccessor"/> and udpate any internal state accordingly.
		/// </summary>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		void UpdateWeights(int elementCount, System.Func<int, TWeight> weightsAccessor);
	}

	#endregion

	/// <summary>
	/// A static class of extension methods for generating random indices, typically for accessing collections using a zero-based indexing scheme.
	/// </summary>
	public static class RandomListAccess
	{
		#region Private Generator Classes

		#region Uniform

		private class VariableLengthArrayIndexGenerator<TElement> : IRangeGenerator<int>
		{
			private IRandom _random;
			private TElement[] _array;

			public VariableLengthArrayIndexGenerator(IRandom random, TElement[] array)
			{
				_random = random;
				_array = array;
			}

			public int Next()
			{
				return _random.RangeCO(_array.Length);
			}
		}

		private class VariableLengthListIndexGenerator<TElement> : IRangeGenerator<int>
		{
			private IRandom _random;
			private IList<TElement> _list;

			public VariableLengthListIndexGenerator(IRandom random, IList<TElement> list)
			{
				_random = random;
				_list = list;
			}

			public int Next()
			{
				return _random.RangeCO(_list.Count);
			}
		}

		private class FixedLengthArrayElementGenerator<TElement> : IElementGenerator<TElement>
		{
			private IRangeGenerator<int> _indexGenerator;
			private TElement[] _array;

			public FixedLengthArrayElementGenerator(IRandom random, TElement[] array)
			{
				_indexGenerator = random.MakeIndexGenerator(array.Length);
				_array = array;
			}

			public TElement Next()
			{
				return _array[_indexGenerator.Next()];
			}

			public int NextIndex()
			{
				return _indexGenerator.Next();
			}

			public TElement Next(out int index)
			{
				index = _indexGenerator.Next();
				return _array[index];
			}
		}

		private class FixedLengthListElementGenerator<TElement> : IElementGenerator<TElement>
		{
			private IRangeGenerator<int> _indexGenerator;
			private IList<TElement> _list;

			public FixedLengthListElementGenerator(IRandom random, IList<TElement> list)
			{
				_indexGenerator = random.MakeIndexGenerator(list.Count);
				_list = list;
			}

			public TElement Next()
			{
				return _list[_indexGenerator.Next()];
			}

			public int NextIndex()
			{
				return _indexGenerator.Next();
			}

			public TElement Next(out int index)
			{
				index = _indexGenerator.Next();
				return _list[index];
			}
		}

		private class VariableLengthArrayElementGenerator<TElement> : IElementGenerator<TElement>
		{
			private IRandom _random;
			private TElement[] _array;

			public VariableLengthArrayElementGenerator(IRandom random, TElement[] array)
			{
				_random = random;
				_array = array;
			}

			public TElement Next()
			{
				return _array[_random.RangeCO(_array.Length)];
			}

			public int NextIndex()
			{
				return _random.RangeCO(_array.Length);
			}

			public TElement Next(out int index)
			{
				index = _random.RangeCO(_array.Length);
				return _array[index];
			}
		}

		private class VariableLengthListElementGenerator<TElement> : IElementGenerator<TElement>
		{
			private IRandom _random;
			private IList<TElement> _list;

			public VariableLengthListElementGenerator(IRandom random, IList<TElement> list)
			{
				_random = random;
				_list = list;
			}

			public TElement Next()
			{
				return _list[_random.RangeCO(_list.Count)];
			}

			public int NextIndex()
			{
				return _random.RangeCO(_list.Count);
			}

			public TElement Next(out int index)
			{
				index = _random.RangeCO(_list.Count);
				return _list[index];
			}
		}

		#endregion

		#region Weighted

#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
		private abstract class WeightedIndexGeneratorBase<TWeight, TWeightSum>
		{
			protected IRandom _random;
			protected TWeightSum _weightSum;
			protected int _elementCount;
			protected TWeight[] _weights;
			protected System.Func<int, TWeight> _weightsAccessor;

			protected WeightedIndexGeneratorBase(IRandom random, int elementCount, TWeight[] weights)
			{
				_random = random;
				_elementCount = elementCount;
				_weights = weights;
				SumWeights();
			}

			public WeightedIndexGeneratorBase(IRandom random, int elementCount, System.Func<int, TWeight> weightsAccessor)
			{
				_random = random;
				_elementCount = elementCount;
				_weights = new TWeight[elementCount];
				_weightsAccessor = weightsAccessor;
				for (int i = 0; i < _elementCount; ++i)
				{
					_weights[i] = weightsAccessor(i);
				}
				SumWeights();
			}

			protected abstract void SumWeights();

			public TWeightSum weightSum { get { return _weightSum; } }

			public abstract int NextIndex();

			public virtual void UpdateWeights(TWeight[] weights)
			{
				_elementCount = weights.Length;
				_weights = weights;
				_weightsAccessor = null;
				SumWeights();
			}

			public virtual void UpdateWeights(int elementCount, System.Func<int, TWeight> weightsAccessor)
			{
				int capacity = _weights.Length;
				if (capacity < elementCount)
				{
					capacity = capacity * 3 / 2;
					if (capacity < elementCount) capacity = elementCount;
					_weights = new TWeight[capacity];
				}
				_elementCount = elementCount;
				_weightsAccessor = weightsAccessor;
				for (int i = 0; i < _elementCount; ++i)
				{
					_weights[i] = weightsAccessor(i);
				}
				SumWeights();
			}
		}

		private abstract class WeightedIndexGenerator<TWeight, TWeightSum> : WeightedIndexGeneratorBase<TWeight, TWeightSum>, IWeightedIndexGenerator<TWeight, TWeightSum>
		{
			public WeightedIndexGenerator(IRandom random, int elementCount, TWeight[] weights) : base(random, elementCount, weights) { }
			public WeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, TWeight> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public int Next()
			{
				return NextIndex();
			}

			public int Next(out TWeight weight)
			{
				int index = NextIndex();
				weight = _weights[index];
				return index;
			}

			public void UpdateWeights()
			{
				if (_weightsAccessor == null)
				{
					UpdateWeights(_weights);
				}
				else
				{
					UpdateWeights(_elementCount, _weightsAccessor);
				}
			}
		}

		private abstract class WeightedElementGenerator<TElement, TWeight, TWeightSum> : WeightedIndexGeneratorBase<TWeight, TWeightSum>, IWeightedElementGenerator<TElement, TWeight, TWeightSum>
		{
			private IList<TElement> _list;

			public WeightedElementGenerator(IRandom random, IList<TElement> list, TWeight[] weights) : base(random, list.Count, weights)
			{
				_list = list;
			}

			public WeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, TWeight[] weights) : base(random, elementCount, weights)
			{
				_list = list;
			}

			public WeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, TWeight> weightsAccessor) : base(random, list.Count, weightsAccessor)
			{
				_list = list;
			}

			public TElement Next()
			{
				return _list[NextIndex()];
			}

			public TElement Next(out TWeight weight)
			{
				int index = NextIndex();
				weight = _weights[index];
				return _list[index];
			}

			TElement IElementGenerator<TElement>.Next(out int index)
			{
				index = NextIndex();
				return _list[index];
			}

			public int NextIndex(out TWeight weight)
			{
				int index = NextIndex();
				weight = _weights[index];
				return index;
			}

			public TElement Next(out int index, out TWeight weight)
			{
				index = NextIndex();
				weight = _weights[index];
				return _list[index];
			}

			public void UpdateWeights()
			{
				if (_weightsAccessor == null)
				{
					UpdateWeights(_weights);
				}
				else
				{
					UpdateWeights(_weightsAccessor);
				}
			}

			public override void UpdateWeights(TWeight[] weights)
			{
				if (weights.Length != _list.Count) throw new System.ArgumentException("There must be an identical number of weights as there are elements in the associated collection.", "weights");
				base.UpdateWeights(weights);
			}

			public void UpdateWeights(System.Func<int, TWeight> weightsAccessor)
			{
				base.UpdateWeights(_list.Count, weightsAccessor);
			}
		}

		private class SByteWeightedIndexGenerator : WeightedIndexGenerator<sbyte, int>
		{
			public SByteWeightedIndexGenerator(IRandom random, int elementCount, sbyte[] weights) : base(random, elementCount, weights) { }
			public SByteWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class SByteWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, sbyte, int>
		{
			public SByteWeightedElementGenerator(IRandom random, IList<TElement> list, sbyte[] weights) : base(random, list, weights) { }
			public SByteWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, sbyte[] weights) : base(random, list, elementCount, weights) { }
			public SByteWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, sbyte> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class ByteWeightedIndexGenerator : WeightedIndexGenerator<byte, uint>
		{
			public ByteWeightedIndexGenerator(IRandom random, int elementCount, byte[] weights) : base(random, elementCount, weights) { }
			public ByteWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, byte> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class ByteWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, byte, uint>
		{
			public ByteWeightedElementGenerator(IRandom random, IList<TElement> list, byte[] weights) : base(random, list, weights) { }
			public ByteWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, byte[] weights) : base(random, list, elementCount, weights) { }
			public ByteWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, byte> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class ShortWeightedIndexGenerator : WeightedIndexGenerator<short, int>
		{
			public ShortWeightedIndexGenerator(IRandom random, int elementCount, short[] weights) : base(random, elementCount, weights) { }
			public ShortWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, short> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class ShortWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, short, int>
		{
			public ShortWeightedElementGenerator(IRandom random, IList<TElement> list, short[] weights) : base(random, list, weights) { }
			public ShortWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, short[] weights) : base(random, list, elementCount, weights) { }
			public ShortWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, short> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class UShortWeightedIndexGenerator : WeightedIndexGenerator<ushort, uint>
		{
			public UShortWeightedIndexGenerator(IRandom random, int elementCount, ushort[] weights) : base(random, elementCount, weights) { }
			public UShortWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class UShortWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, ushort, uint>
		{
			public UShortWeightedElementGenerator(IRandom random, IList<TElement> list, ushort[] weights) : base(random, list, weights) { }
			public UShortWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, ushort[] weights) : base(random, list, elementCount, weights) { }
			public UShortWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, ushort> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class IntWeightedIndexGenerator : WeightedIndexGenerator<int, int>
		{
			public IntWeightedIndexGenerator(IRandom random, int elementCount, int[] weights) : base(random, elementCount, weights) { }
			public IntWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, int> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class IntWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, int, int>
		{
			public IntWeightedElementGenerator(IRandom random, IList<TElement> list, int[] weights) : base(random, list, weights) { }
			public IntWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, int[] weights) : base(random, list, elementCount, weights) { }
			public IntWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, int> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class UIntWeightedIndexGenerator : WeightedIndexGenerator<uint, uint>
		{
			public UIntWeightedIndexGenerator(IRandom random, int elementCount, uint[] weights) : base(random, elementCount, weights) { }
			public UIntWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, uint> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class UIntWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, uint, uint>
		{
			public UIntWeightedElementGenerator(IRandom random, IList<TElement> list, uint[] weights) : base(random, list, weights) { }
			public UIntWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, uint[] weights) : base(random, list, elementCount, weights) { }
			public UIntWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, uint> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class LongWeightedIndexGenerator : WeightedIndexGenerator<long, long>
		{
			public LongWeightedIndexGenerator(IRandom random, int elementCount, long[] weights) : base(random, elementCount, weights) { }
			public LongWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, long> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class LongWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, long, long>
		{
			public LongWeightedElementGenerator(IRandom random, IList<TElement> list, long[] weights) : base(random, list, weights) { }
			public LongWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, long[] weights) : base(random, list, elementCount, weights) { }
			public LongWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, long> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class ULongWeightedIndexGenerator : WeightedIndexGenerator<ulong, ulong>
		{
			public ULongWeightedIndexGenerator(IRandom random, int elementCount, ulong[] weights) : base(random, elementCount, weights) { }
			public ULongWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class ULongWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, ulong, ulong>
		{
			public ULongWeightedElementGenerator(IRandom random, IList<TElement> list, ulong[] weights) : base(random, list, weights) { }
			public ULongWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, ulong[] weights) : base(random, list, elementCount, weights) { }
			public ULongWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, ulong> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class FloatWeightedIndexGenerator : WeightedIndexGenerator<float, float>
		{
			public FloatWeightedIndexGenerator(IRandom random, int elementCount, float[] weights) : base(random, elementCount, weights) { }
			public FloatWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, float> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class FloatWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, float, float>
		{
			public FloatWeightedElementGenerator(IRandom random, IList<TElement> list, float[] weights) : base(random, list, weights) { }
			public FloatWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, float[] weights) : base(random, list, elementCount, weights) { }
			public FloatWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, float> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class DoubleWeightedIndexGenerator : WeightedIndexGenerator<double, double>
		{
			public DoubleWeightedIndexGenerator(IRandom random, int elementCount, double[] weights) : base(random, elementCount, weights) { }
			public DoubleWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, double> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}

		private class DoubleWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, double, double>
		{
			public DoubleWeightedElementGenerator(IRandom random, IList<TElement> list, double[] weights) : base(random, list, weights) { }
			public DoubleWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, double[] weights) : base(random, list, elementCount, weights) { }
			public DoubleWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, double> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_elementCount, _weights, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights);
			}
		}
#else
		private abstract class WeightedIndexGeneratorBase<TWeight, TWeightSum, TCumulativeWeightSum>
		{
			protected IRandom _random;
			protected TWeightSum _weightSum;
			protected int _elementCount;
			protected TWeight[] _weights;
			protected TCumulativeWeightSum[] _cumulativeWeightSums;
			protected System.Func<int, TWeight> _weightsAccessor;

			protected WeightedIndexGeneratorBase(IRandom random, int elementCount, TWeight[] weights)
			{
				_random = random;
				_elementCount = elementCount;
				_weights = weights;
				_cumulativeWeightSums = new TCumulativeWeightSum[_weights.Length];
				SumWeights();
			}

			public WeightedIndexGeneratorBase(IRandom random, int elementCount, System.Func<int, TWeight> weightsAccessor)
			{
				_random = random;
				_elementCount = elementCount;
				_weights = new TWeight[elementCount];
				_weightsAccessor = weightsAccessor;
				for (int i = 0; i < _elementCount; ++i)
				{
					_weights[i] = weightsAccessor(i);
				}
				_cumulativeWeightSums = new TCumulativeWeightSum[_weights.Length];
				SumWeights();
			}

			protected abstract void SumWeights();

			public TWeightSum weightSum { get { return _weightSum; } }

			public abstract int NextIndex();

			public virtual void UpdateWeights(TWeight[] weights)
			{
				_elementCount = weights.Length;
				int capacity = _cumulativeWeightSums.Length;
				if (capacity < _elementCount)
				{
					capacity = capacity * 3 / 2;
					if (capacity < _elementCount) capacity = _elementCount;
					_cumulativeWeightSums = new TCumulativeWeightSum[capacity];
				}
				_weights = weights;
				_weightsAccessor = null;
				SumWeights();
			}

			public virtual void UpdateWeights(int elementCount, System.Func<int, TWeight> weightsAccessor)
			{
				_elementCount = elementCount;
				int capacity = _weights.Length;
				if (capacity < _elementCount)
				{
					capacity = capacity * 3 / 2;
					if (capacity < _elementCount) capacity = _elementCount;
					_weights = new TWeight[capacity];
				}
				capacity = _cumulativeWeightSums.Length;
				if (capacity < _elementCount)
				{
					capacity = capacity * 3 / 2;
					if (capacity < _elementCount) capacity = _elementCount;
					_cumulativeWeightSums = new TCumulativeWeightSum[capacity];
				}
				_weightsAccessor = weightsAccessor;
				for (int i = 0; i < _elementCount; ++i)
				{
					_weights[i] = weightsAccessor(i);
				}
				SumWeights();
			}
		}

		private abstract class WeightedIndexGenerator<TWeight, TWeightSum, TCumulativeWeightSum> : WeightedIndexGeneratorBase<TWeight, TWeightSum, TCumulativeWeightSum>, IWeightedIndexGenerator<TWeight, TWeightSum>
		{
			public WeightedIndexGenerator(IRandom random, int elementCount, TWeight[] weights) : base(random, elementCount, weights) { }
			public WeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, TWeight> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public int Next()
			{
				return NextIndex();
			}

			public int Next(out TWeight weight)
			{
				int index = NextIndex();
				weight = _weights[index];
				return index;
			}

			public void UpdateWeights()
			{
				if (_weightsAccessor == null)
				{
					UpdateWeights(_weights);
				}
				else
				{
					UpdateWeights(_elementCount, _weightsAccessor);
				}
			}
		}

		private abstract class WeightedElementGenerator<TElement, TWeight, TWeightSum, TCumulativeWeightSum> : WeightedIndexGeneratorBase<TWeight, TWeightSum, TCumulativeWeightSum>, IWeightedElementGenerator<TElement, TWeight, TWeightSum>
		{
			private IList<TElement> _list;

			public WeightedElementGenerator(IRandom random, IList<TElement> list, TWeight[] weights) : base(random, list.Count, weights)
			{
				_list = list;
			}

			public WeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, TWeight[] weights) : base(random, elementCount, weights)
			{
				_list = list;
			}

			public WeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, TWeight> weightsAccessor) : base(random, list.Count, weightsAccessor)
			{
				_list = list;
			}

			public TElement Next()
			{
				return _list[NextIndex()];
			}

			public TElement Next(out TWeight weight)
			{
				int index = NextIndex();
				weight = _weights[index];
				return _list[index];
			}

			TElement IElementGenerator<TElement>.Next(out int index)
			{
				index = NextIndex();
				return _list[index];
			}

			public int NextIndex(out TWeight weight)
			{
				int index = NextIndex();
				weight = _weights[index];
				return index;
			}

			public TElement Next(out int index, out TWeight weight)
			{
				index = NextIndex();
				weight = _weights[index];
				return _list[index];
			}

			public void UpdateWeights()
			{
				if (_weightsAccessor == null)
				{
					UpdateWeights(_weights);
				}
				else
				{
					UpdateWeights(_weightsAccessor);
				}
			}

			public override void UpdateWeights(TWeight[] weights)
			{
				if (weights.Length != _list.Count) throw new System.ArgumentException("There must be an identical number of weights as there are elements in the associated collection.", "weights");
				base.UpdateWeights(weights);
			}

			public void UpdateWeights(System.Func<int, TWeight> weightsAccessor)
			{
				base.UpdateWeights(_list.Count, weightsAccessor);
			}
		}

		private class SByteWeightedIndexGenerator : WeightedIndexGenerator<sbyte, int, int>
		{
			public SByteWeightedIndexGenerator(IRandom random, int elementCount, sbyte[] weights) : base(random, elementCount, weights) { }
			public SByteWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class SByteWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, sbyte, int, int>
		{
			public SByteWeightedElementGenerator(IRandom random, IList<TElement> list, sbyte[] weights) : base(random, list, weights) { }
			public SByteWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, sbyte[] weights) : base(random, list, elementCount, weights) { }
			public SByteWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, sbyte> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class ByteWeightedIndexGenerator : WeightedIndexGenerator<byte, uint, uint>
		{
			public ByteWeightedIndexGenerator(IRandom random, int elementCount, byte[] weights) : base(random, elementCount, weights) { }
			public ByteWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, byte> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class ByteWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, byte, uint, uint>
		{
			public ByteWeightedElementGenerator(IRandom random, IList<TElement> list, byte[] weights) : base(random, list, weights) { }
			public ByteWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, byte[] weights) : base(random, list, elementCount, weights) { }
			public ByteWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, byte> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class ShortWeightedIndexGenerator : WeightedIndexGenerator<short, int, int>
		{
			public ShortWeightedIndexGenerator(IRandom random, int elementCount, short[] weights) : base(random, elementCount, weights) { }
			public ShortWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, short> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class ShortWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, short, int, int>
		{
			public ShortWeightedElementGenerator(IRandom random, IList<TElement> list, short[] weights) : base(random, list, weights) { }
			public ShortWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, short[] weights) : base(random, list, elementCount, weights) { }
			public ShortWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, short> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class UShortWeightedIndexGenerator : WeightedIndexGenerator<ushort, uint, uint>
		{
			public UShortWeightedIndexGenerator(IRandom random, int elementCount, ushort[] weights) : base(random, elementCount, weights) { }
			public UShortWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class UShortWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, ushort, uint, uint>
		{
			public UShortWeightedElementGenerator(IRandom random, IList<TElement> list, ushort[] weights) : base(random, list, weights) { }
			public UShortWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, ushort[] weights) : base(random, list, elementCount, weights) { }
			public UShortWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, ushort> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class IntWeightedIndexGenerator : WeightedIndexGenerator<int, int, int>
		{
			public IntWeightedIndexGenerator(IRandom random, int elementCount, int[] weights) : base(random, elementCount, weights) { }
			public IntWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, int> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class IntWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, int, int, int>
		{
			public IntWeightedElementGenerator(IRandom random, IList<TElement> list, int[] weights) : base(random, list, weights) { }
			public IntWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, int[] weights) : base(random, list, elementCount, weights) { }
			public IntWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, int> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class UIntWeightedIndexGenerator : WeightedIndexGenerator<uint, uint, uint>
		{
			public UIntWeightedIndexGenerator(IRandom random, int elementCount, uint[] weights) : base(random, elementCount, weights) { }
			public UIntWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, uint> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class UIntWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, uint, uint, uint>
		{
			public UIntWeightedElementGenerator(IRandom random, IList<TElement> list, uint[] weights) : base(random, list, weights) { }
			public UIntWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, uint[] weights) : base(random, list, elementCount, weights) { }
			public UIntWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, uint> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class LongWeightedIndexGenerator : WeightedIndexGenerator<long, long, long>
		{
			public LongWeightedIndexGenerator(IRandom random, int elementCount, long[] weights) : base(random, elementCount, weights) { }
			public LongWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, long> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class LongWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, long, long, long>
		{
			public LongWeightedElementGenerator(IRandom random, IList<TElement> list, long[] weights) : base(random, list, weights) { }
			public LongWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, long[] weights) : base(random, list, elementCount, weights) { }
			public LongWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, long> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class ULongWeightedIndexGenerator : WeightedIndexGenerator<ulong, ulong, ulong>
		{
			public ULongWeightedIndexGenerator(IRandom random, int elementCount, ulong[] weights) : base(random, elementCount, weights) { }
			public ULongWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class ULongWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, ulong, ulong, ulong>
		{
			public ULongWeightedElementGenerator(IRandom random, IList<TElement> list, ulong[] weights) : base(random, list, weights) { }
			public ULongWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, ulong[] weights) : base(random, list, elementCount, weights) { }
			public ULongWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, ulong> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class FloatWeightedIndexGenerator : WeightedIndexGenerator<float, float, uint>
		{
			public FloatWeightedIndexGenerator(IRandom random, int elementCount, float[] weights) : base(random, elementCount, weights) { }
			public FloatWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, float> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, uint.MaxValue);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class FloatWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, float, float, uint>
		{
			public FloatWeightedElementGenerator(IRandom random, IList<TElement> list, float[] weights) : base(random, list, weights) { }
			public FloatWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, float[] weights) : base(random, list, elementCount, weights) { }
			public FloatWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, float> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, uint.MaxValue);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class DoubleWeightedIndexGenerator : WeightedIndexGenerator<double, double, ulong>
		{
			public DoubleWeightedIndexGenerator(IRandom random, int elementCount, double[] weights) : base(random, elementCount, weights) { }
			public DoubleWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, double> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, ulong.MaxValue);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}

		private class DoubleWeightedElementGenerator<TElement> : WeightedElementGenerator<TElement, double, double, ulong>
		{
			public DoubleWeightedElementGenerator(IRandom random, IList<TElement> list, double[] weights) : base(random, list, weights) { }
			public DoubleWeightedElementGenerator(IRandom random, IList<TElement> list, int elementCount, double[] weights) : base(random, list, elementCount, weights) { }
			public DoubleWeightedElementGenerator(IRandom random, IList<TElement> list, System.Func<int, double> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndexBinarySearch(_elementCount, _cumulativeWeightSums, ulong.MaxValue);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_elementCount, _weights, _cumulativeWeightSums);
			}
		}
#endif

		#endregion

		#endregion

		#region Private Weight Summation

		private static int SumWeights(int elementCount, sbyte[] weights)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static int SumWeights(int elementCount, sbyte[] weights, int[] cumulativeWeightSums)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
				cumulativeWeightSums[i] = weightSum;
			}
			return weightSum;
		}

		private static int SumWeights(int elementCount, System.Func<int, sbyte> weightsAccessor)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, byte[] weights)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, byte[] weights, uint[] cumulativeWeightSums)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
				cumulativeWeightSums[i] = weightSum;
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, System.Func<int, byte> weightsAccessor)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static int SumWeights(int elementCount, short[] weights)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static int SumWeights(int elementCount, short[] weights, int[] cumulativeWeightSums)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
				cumulativeWeightSums[i] = weightSum;
			}
			return weightSum;
		}

		private static int SumWeights(int elementCount, System.Func<int, short> weightsAccessor)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, ushort[] weights)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, ushort[] weights, uint[] cumulativeWeightSums)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
				cumulativeWeightSums[i] = weightSum;
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, System.Func<int, ushort> weightsAccessor)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static int SumWeights(int elementCount, int[] weights)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static int SumWeights(int elementCount, int[] weights, int[] cumulativeWeightSums)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
				cumulativeWeightSums[i] = weightSum;
			}
			return weightSum;
		}

		private static int SumWeights(int elementCount, System.Func<int, int> weightsAccessor)
		{
			int weightSum = 0;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, uint[] weights)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, uint[] weights, uint[] cumulativeWeightSums)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
				cumulativeWeightSums[i] = weightSum;
			}
			return weightSum;
		}

		private static uint SumWeights(int elementCount, System.Func<int, uint> weightsAccessor)
		{
			uint weightSum = 0U;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static long SumWeights(int elementCount, long[] weights)
		{
			long weightSum = 0L;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static long SumWeights(int elementCount, long[] weights, long[] cumulativeWeightSums)
		{
			long weightSum = 0L;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
				cumulativeWeightSums[i] = weightSum;
			}
			return weightSum;
		}

		private static long SumWeights(int elementCount, System.Func<int, long> weightsAccessor)
		{
			long weightSum = 0L;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static ulong SumWeights(int elementCount, ulong[] weights)
		{
			ulong weightSum = 0UL;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static ulong SumWeights(int elementCount, ulong[] weights, ulong[] cumulativeWeightSums)
		{
			ulong weightSum = 0UL;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
				cumulativeWeightSums[i] = weightSum;
			}
			return weightSum;
		}

		private static ulong SumWeights(int elementCount, System.Func<int, ulong> weightsAccessor)
		{
			ulong weightSum = 0UL;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static float SumWeights(int elementCount, float[] weights)
		{
			float weightSum = 0f;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static float SumWeights(int elementCount, float[] weights, uint[] cumulativeWeightSums)
		{
			float weightSum = 0f;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}

			float weightToIntScale = 0xFFFFFF00U;
			float cumulativeWeightSum = 0f;
			for (int i = 0; i < elementCount; ++i)
			{
				cumulativeWeightSum += weights[i];
				cumulativeWeightSums[i] = (uint)UnityEngine.Mathf.Floor(cumulativeWeightSum / weightSum * weightToIntScale);
			}

			uint remainder = uint.MaxValue - cumulativeWeightSums[elementCount - 1];
			for (int i = 0; i < elementCount; ++i)
			{
				uint extra = (uint)UnityEngine.Mathf.Round(weights[i] / cumulativeWeightSum * remainder);
				cumulativeWeightSums[i] += extra;
				remainder -= extra;
				cumulativeWeightSum -= weights[i];
			}
			cumulativeWeightSums[elementCount - 1] = uint.MaxValue;

			return weightSum;
		}

		private static float SumWeights(int elementCount, System.Func<int, float> weightsAccessor)
		{
			float weightSum = 0f;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		private static double SumWeights(int elementCount, double[] weights)
		{
			double weightSum = 0d;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}
			return weightSum;
		}

		private static double SumWeights(int elementCount, double[] weights, ulong[] cumulativeWeightSums)
		{
			double weightSum = 0d;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weights[i];
			}

			double weightToIntScale = 0xFFFFFFFFFFFFF800UL;
			double cumulativeWeightSum = 0d;
			for (int i = 0; i < elementCount; ++i)
			{
				cumulativeWeightSum += weights[i];
				cumulativeWeightSums[i] = (uint)System.Math.Floor(cumulativeWeightSum / weightSum * weightToIntScale);
			}

			ulong remainder = ulong.MaxValue - cumulativeWeightSums[elementCount - 1];
			for (int i = 0; i < elementCount; ++i)
			{
				ulong extra = (ulong)System.Math.Round(weights[i] / cumulativeWeightSum * remainder);
				cumulativeWeightSums[i] += extra;
				remainder -= extra;
				cumulativeWeightSum -= weights[i];
			}
			cumulativeWeightSums[elementCount - 1] = ulong.MaxValue;

			return weightSum;
		}
		private static double SumWeights(int elementCount, System.Func<int, double> weightsAccessor)
		{
			double weightSum = 0d;
			for (int i = 0; i < elementCount; ++i)
			{
				weightSum += weightsAccessor(i);
			}
			return weightSum;
		}

		#endregion

		#region Uniform

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A random index in the range [0, <paramref name="length"/>).</returns>
		public static int Index(this IRandom random, int length)
		{
			return random.RangeCO(length);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A random index in the range [0, <paramref name="length"/>).</returns>
		public static uint Index(this IRandom random, uint length)
		{
			return random.RangeCO(length);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A random index in the range [0, <paramref name="length"/>).</returns>
		public static long Index(this IRandom random, long length)
		{
			return random.RangeCO(length);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A random index in the range [0, <paramref name="length"/>).</returns>
		public static ulong Index(this IRandom random, ulong length)
		{
			return random.RangeCO(length);
		}

		/// <summary>
		/// Returns a uniformly distributed random index in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection for which a random index will be generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		public static int Index<TElement>(this IRandom random, IList<TElement> list)
		{
			return random.RangeCO(list.Count);
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		public static int RandomIndex<TElement>(this IList<TElement> list, IRandom random)
		{
			return random.RangeCO(list.Count);
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		public static TElement Element<TElement>(this IRandom random, IList<TElement> list)
		{
			return list[random.RangeCO(list.Count)];
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		public static TElement RandomElement<TElement>(this IList<TElement> list, IRandom random)
		{
			return list[random.RangeCO(list.Count)];
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="length"/>).</returns>
		public static IRangeGenerator<int> MakeIndexGenerator(this IRandom random, int length)
		{
			return random.MakeRangeCOGenerator(length);
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="length"/>).</returns>
		public static IRangeGenerator<uint> MakeIndexGenerator(this IRandom random, uint length)
		{
			return random.MakeRangeCOGenerator(length);
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="length"/>).</returns>
		public static IRangeGenerator<long> MakeIndexGenerator(this IRandom random, long length)
		{
			return random.MakeRangeCOGenerator(length);
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="length"/>).</returns>
		public static IRangeGenerator<ulong> MakeIndexGenerator(this IRandom random, ulong length)
		{
			return random.MakeRangeCOGenerator(length);
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection for which the range generator will produce random indices.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>If <paramref name="list"/> is marked as having a fixed size, then the range generator returned has the opportunity
		/// to be more efficient, because the generated indices will always come from the same range.  Otherwise, each index generated
		/// will come from the range [0, <paramref name="list"/>.Count) according to the value of <paramref name="list"/>.Count at the
		/// time the index is generated.</remarks>
		public static IRangeGenerator<int> MakeIndexGenerator<TElement>(this IRandom random, IList<TElement> list)
		{
			IListNonGeneric nonGenericList = list as IListNonGeneric;
			if (nonGenericList != null && nonGenericList.IsFixedSize)
			{
				return random.MakeRangeCOGenerator(list.Count);
			}
			else
			{
				TElement[] array = list as TElement[];
				if (array != null)
				{
					return new VariableLengthArrayIndexGenerator<TElement>(random, array);
				}
				else
				{
					return new VariableLengthListIndexGenerator<TElement>(random, list);
				}
			}
		}

		/// <summary>
		/// Returns an element generator which will return elements from <paramref name="list"/> with an equally weighted distribution.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <returns>An element generator which returns random elements from <paramref name="list"/>.</returns>
		/// <remarks>If <paramref name="list"/> is marked as having a fixed size, then the element generator returned has the opportunity
		/// to be more efficient, because the internally generated indices will always come from the same range.</remarks>
		public static IElementGenerator<TElement> MakeElementGenerator<TElement>(this IRandom random, IList<TElement> list)
		{
			IListNonGeneric nonGenericList = list as IListNonGeneric;
			TElement[] array = list as TElement[];
			if (nonGenericList != null && nonGenericList.IsFixedSize)
			{
				if (array != null)
				{
					return new FixedLengthArrayElementGenerator<TElement>(random, array);
				}
				else
				{
					return new FixedLengthListElementGenerator<TElement>(random, list);
				}
			}
			else
			{
				if (array != null)
				{
					return new VariableLengthArrayElementGenerator<TElement>(random, array);
				}
				else
				{
					return new VariableLengthListElementGenerator<TElement>(random, list);
				}
			}
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection for which the range generator will produce random indices.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>If <paramref name="list"/> is marked as having a fixed size, then the range generator returned has the opportunity
		/// to be more efficient, because the generated indices will always come from the same range.  Otherwise, each index generated
		/// will come from the range [0, <paramref name="list"/>.Count) according to the value of <paramref name="list"/>.Count at the
		/// time the index is generated.</remarks>
		public static IRangeGenerator<int> MakeRandomIndexGenerator<TElement>(this IList<TElement> list, IRandom random)
		{
			return random.MakeIndexGenerator(list);
		}

		/// <summary>
		/// Returns an element generator which will return elements from <paramref name="list"/> with an equally weighted distribution.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>An element generator which returns random elements from <paramref name="list"/>.</returns>
		/// <remarks>If <paramref name="list"/> is marked as having a fixed size, then the element generator returned has the opportunity
		/// to be more efficient, because the internally generated indices will always come from the same range.</remarks>
		public static IElementGenerator<TElement> MakeRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random)
		{
			return random.MakeElementGenerator(list);
		}

		#endregion

		#region Weighted Index

		#region sbyte

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, sbyte[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, sbyte[], int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, sbyte[])"/>
		public static int WeightedIndex(this IRandom random, sbyte[] weights)
		{
			int weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, sbyte[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, sbyte[], int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, sbyte[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, sbyte[] weights)
		{
			int weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, sbyte[])"/>
		public static int WeightedIndex(this IRandom random, sbyte[] weights, int weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, sbyte[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, sbyte[] weights, int weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			int n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, sbyte}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, sbyte}, int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor)
		{
			int weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor, int weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			int n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, sbyte[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, sbyte[], int)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, sbyte[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, sbyte[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, sbyte[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, sbyte[] weights, int weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, sbyte}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, sbyte}, int)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, sbyte> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, sbyte> weightsAccessor, int weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		#endregion

		#region byte

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, byte[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, byte[], uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, byte[])"/>
		public static int WeightedIndex(this IRandom random, byte[] weights)
		{
			uint weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, byte[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, byte[], uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, byte[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, byte[] weights)
		{
			uint weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, byte[])"/>
		public static int WeightedIndex(this IRandom random, byte[] weights, uint weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, byte[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, byte[] weights, uint weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			uint n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, byte}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, byte}, uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, byte> weightsAccessor)
		{
			uint weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, byte> weightsAccessor, uint weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			uint n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, byte[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, byte[], uint)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, byte[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, byte[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, byte[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, byte[] weights, uint weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, byte}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, byte}, uint)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, byte> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, byte> weightsAccessor, uint weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		#endregion

		#region short

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, short[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, short[], int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, short[])"/>
		public static int WeightedIndex(this IRandom random, short[] weights)
		{
			int weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, short[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, short[], int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, short[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, short[] weights)
		{
			int weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, short[])"/>
		public static int WeightedIndex(this IRandom random, short[] weights, int weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, short[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, short[] weights, int weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			int n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, short}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, short}, int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, short> weightsAccessor)
		{
			int weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, short> weightsAccessor, int weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			int n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, short[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, short[], int)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, short[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, short[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, short[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, short[] weights, int weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, short}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, short}, int)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, short> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, short> weightsAccessor, int weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		#endregion

		#region ushort

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, ushort[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, ushort[], uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, ushort[])"/>
		public static int WeightedIndex(this IRandom random, ushort[] weights)
		{
			uint weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, ushort[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, ushort[], uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, ushort[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, ushort[] weights)
		{
			uint weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, ushort[])"/>
		public static int WeightedIndex(this IRandom random, ushort[] weights, uint weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, ushort[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, ushort[] weights, uint weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			uint n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, ushort}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, ushort}, uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor)
		{
			uint weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor, uint weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			uint n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, ushort[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, ushort[], uint)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, ushort[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, ushort[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, ushort[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, ushort[] weights, uint weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, ushort}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, ushort}, uint)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ushort> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ushort> weightsAccessor, uint weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		#endregion

		#region int

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int[], int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int[])"/>
		public static int WeightedIndex(this IRandom random, int[] weights)
		{
			int weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, int[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, int[], int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, int[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, int[] weights)
		{
			int weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int[])"/>
		public static int WeightedIndex(this IRandom random, int[] weights, int weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, int[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, int[] weights, int weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			int n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, int}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, int}, int)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, int> weightsAccessor)
		{
			int weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, int> weightsAccessor, int weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			int n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights.</param>
		/// <returns>A random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, int[] cumulativeWeightSums, int weightSum)
		{
			return random.WeightedIndexBinarySearch(cumulativeWeightSums.Length, cumulativeWeightSums, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="cumulativeWeightSums"/> to consider.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> weights.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, int elementCount, int[] cumulativeWeightSums, int weightSum)
		{
			int n = random.RangeCO(weightSum);

			int iLower = 0;
			int iUpper = elementCount;

			do
			{
				int iMid = (iLower + iUpper) >> 1;
				if (n < cumulativeWeightSums[iMid])
				{
					iUpper = iMid;
				}
				else
				{
					iLower = iMid + 1;
				}
			} while (iLower < iUpper);

			return iLower;
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, int[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, int[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, int[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, int[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, int[] weights, int weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, int}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, int}, int)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, int> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, int> weightsAccessor, int weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="cumulativeWeightSums"/>.Length weights.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedRandomIndexBinarySearch<TElement>(this IList<TElement> list, IRandom random, int[] cumulativeWeightSums, int weightSum)
		{
			return random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region uint

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, uint[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, uint[], uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, uint[])"/>
		public static int WeightedIndex(this IRandom random, uint[] weights)
		{
			uint weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, uint[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, uint[], uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, uint[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, uint[] weights)
		{
			uint weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, uint[])"/>
		public static int WeightedIndex(this IRandom random, uint[] weights, uint weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, uint[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, uint[] weights, uint weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			uint n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, uint}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, uint}, uint)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, uint> weightsAccessor)
		{
			uint weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			uint n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights.</param>
		/// <returns>A random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, uint[] cumulativeWeightSums, uint weightSum)
		{
			return random.WeightedIndexBinarySearch(cumulativeWeightSums.Length, cumulativeWeightSums, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="cumulativeWeightSums"/> to consider.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> weights.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, int elementCount, uint[] cumulativeWeightSums, uint weightSum)
		{
			uint n = random.RangeCO(weightSum);

			int iLower = 0;
			int iUpper = elementCount;

			do
			{
				int iMid = (iLower + iUpper) >> 1;
				if (n < cumulativeWeightSums[iMid])
				{
					iUpper = iMid;
				}
				else
				{
					iLower = iMid + 1;
				}
			} while (iLower < iUpper);

			return iLower;
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, uint[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, uint[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, uint[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, uint[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, uint[] weights, uint weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, uint}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, uint}, uint)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, uint> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="cumulativeWeightSums"/>.Length weights.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedRandomIndexBinarySearch<TElement>(this IList<TElement> list, IRandom random, uint[] cumulativeWeightSums, uint weightSum)
		{
			return random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region long

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, long[], long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, long[], long)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, long[])"/>
		public static int WeightedIndex(this IRandom random, long[] weights)
		{
			long weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, long[], long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, long[], long)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, long[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, long[] weights)
		{
			long weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, long[])"/>
		public static int WeightedIndex(this IRandom random, long[] weights, long weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, long[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, long[] weights, long weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			long n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, long}, long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, long}, long)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, long> weightsAccessor)
		{
			long weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, long> weightsAccessor, long weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			long n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights.</param>
		/// <returns>A random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, long[] cumulativeWeightSums, long weightSum)
		{
			return random.WeightedIndexBinarySearch(cumulativeWeightSums.Length, cumulativeWeightSums, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="cumulativeWeightSums"/> to consider.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> weights.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, int elementCount, long[] cumulativeWeightSums, long weightSum)
		{
			long n = random.RangeCO(weightSum);

			int iLower = 0;
			int iUpper = elementCount;

			do
			{
				int iMid = (iLower + iUpper) >> 1;
				if (n < cumulativeWeightSums[iMid])
				{
					iUpper = iMid;
				}
				else
				{
					iLower = iMid + 1;
				}
			} while (iLower < iUpper);

			return iLower;
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, long[], long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, long[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, long[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, long[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, long[] weights, long weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, long}, long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, long}, long)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, long> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, long> weightsAccessor, long weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="cumulativeWeightSums"/>.Length weights.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedRandomIndexBinarySearch<TElement>(this IList<TElement> list, IRandom random, long[] cumulativeWeightSums, long weightSum)
		{
			return random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region ulong

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, ulong[], ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, ulong[], ulong)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, ulong[])"/>
		public static int WeightedIndex(this IRandom random, ulong[] weights)
		{
			ulong weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, ulong[], ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, ulong[], ulong)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, ulong[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, ulong[] weights)
		{
			ulong weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, ulong[])"/>
		public static int WeightedIndex(this IRandom random, ulong[] weights, ulong weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, ulong[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, ulong[] weights, ulong weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			ulong n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, ulong}, ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, ulong}, ulong)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor)
		{
			ulong weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor, ulong weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			ulong n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights.</param>
		/// <returns>A random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, ulong[] cumulativeWeightSums, ulong weightSum)
		{
			return random.WeightedIndexBinarySearch(cumulativeWeightSums.Length, cumulativeWeightSums, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="cumulativeWeightSums"/> to consider.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> weights.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, int elementCount, ulong[] cumulativeWeightSums, ulong weightSum)
		{
			ulong n = random.RangeCO(weightSum);

			int iLower = 0;
			int iUpper = elementCount;

			do
			{
				int iMid = (iLower + iUpper) >> 1;
				if (n < cumulativeWeightSums[iMid])
				{
					iUpper = iMid;
				}
				else
				{
					iLower = iMid + 1;
				}
			} while (iLower < iUpper);

			return iLower;
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, ulong[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, ulong[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, ulong[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, ulong[] weights, ulong weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, ulong}, ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, ulong}, ulong)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ulong> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ulong> weightsAccessor, ulong weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="cumulativeWeightSums"/>.Length weights.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedRandomIndexBinarySearch<TElement>(this IList<TElement> list, IRandom random, ulong[] cumulativeWeightSums, ulong weightSum)
		{
			return random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region float

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, float[], float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, float[], float)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, float[])"/>
		public static int WeightedIndex(this IRandom random, float[] weights)
		{
			float weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, float[], float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, float[], float)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, float[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, float[] weights)
		{
			float weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, float[])"/>
		public static int WeightedIndex(this IRandom random, float[] weights, float weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, float[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, float[] weights, float weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			float n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, float}, float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, float}, float)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, float> weightsAccessor)
		{
			float weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, float> weightsAccessor, float weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			float n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights.</param>
		/// <returns>A random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, float[] cumulativeWeightSums, float weightSum)
		{
			return random.WeightedIndexBinarySearch(cumulativeWeightSums.Length, cumulativeWeightSums, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="cumulativeWeightSums"/> to consider.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> weights.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, int elementCount, float[] cumulativeWeightSums, float weightSum)
		{
			float n = random.RangeCO(weightSum);

			int iLower = 0;
			int iUpper = elementCount;

			do
			{
				int iMid = (iLower + iUpper) >> 1;
				if (n < cumulativeWeightSums[iMid])
				{
					iUpper = iMid;
				}
				else
				{
					iLower = iMid + 1;
				}
			} while (iLower < iUpper);

			return iLower;
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, float[], float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, float[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, float[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, float[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, float[] weights, float weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, float}, float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, float}, float)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, float> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, float> weightsAccessor, float weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="cumulativeWeightSums"/>.Length weights.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedRandomIndexBinarySearch<TElement>(this IList<TElement> list, IRandom random, float[] cumulativeWeightSums, float weightSum)
		{
			return random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region double

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, double[], double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, double[], double)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, double[])"/>
		public static int WeightedIndex(this IRandom random, double[] weights)
		{
			double weightSum = SumWeights(weights.Length, weights);
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, double[], double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, double[], double)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, double[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, double[] weights)
		{
			double weightSum = SumWeights(elementCount, weights);
			return random.WeightedIndex(elementCount, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, double[])"/>
		public static int WeightedIndex(this IRandom random, double[] weights, double weightSum)
		{
			return random.WeightedIndex(weights.Length, weights, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, double[])"/>
		public static int WeightedIndex(this IRandom random, int elementCount, double[] weights, double weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return index;
				}

				weightSum -= weights[index++];
			}
			return index;
#else
			double n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedIndex(IRandom, int, System.Func{int, double}, double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, double}, double)"/>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, double> weightsAccessor)
		{
			double weightSum = SumWeights(elementCount, weightsAccessor);
			return random.WeightedIndex(elementCount, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedIndexBinarySearch(IRandom, int, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedIndexBinarySearch(IRandom, int, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/>
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, double> weightsAccessor, double weightSum)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V1_0
			int index = 0;
			int lastIndex = elementCount - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return index;
				}

				weightSum -= weightsAccessor(index++);
			}
			return index;
#else
			double n = random.RangeCO(weightSum);
			int lastIndex = elementCount - 1;
			for (int i = 0; i < lastIndex; ++i)
			{
				weightSum -= weightsAccessor(i);
				if (weightSum <= n) return i;
			}
			return lastIndex;
#endif
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights.</param>
		/// <returns>A random index in the range [0, <paramref name="cumulativeWeightSums"/>.Length).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, double[] cumulativeWeightSums, double weightSum)
		{
			return random.WeightedIndexBinarySearch(cumulativeWeightSums.Length, cumulativeWeightSums, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="cumulativeWeightSums"/> to consider.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> weights.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedIndexBinarySearch(this IRandom random, int elementCount, double[] cumulativeWeightSums, double weightSum)
		{
			double n = random.RangeCO(weightSum);

			int iLower = 0;
			int iUpper = elementCount;

			do
			{
				int iMid = (iLower + iUpper) >> 1;
				if (n < cumulativeWeightSums[iMid])
				{
					iUpper = iMid;
				}
				else
				{
					iLower = iMid + 1;
				}
			} while (iLower < iUpper);

			return iLower;
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, double[], double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, double[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, double[] weights)
		{
			return random.WeightedIndex(list.Count, weights);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, double[])"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, double[] weights, double weightSum)
		{
			return random.WeightedIndex(list.Count, weights, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, double}, double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndex{TElement}(IList{TElement}, IRandom, System.Func{int, double}, double)"/>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, double> weightsAccessor)
		{
			return random.WeightedIndex(list.Count, weightsAccessor);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct index to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomIndexBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/>
		public static int WeightedRandomIndex<TElement>(this IList<TElement> list, IRandom random, System.Func<int, double> weightsAccessor, double weightSum)
		{
			return random.WeightedIndex(list.Count, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a random element from <paramref name="list"/>, non-uniformly selected according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="cumulativeWeightSums"/>.Length weights.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected index in logarithmic time.</para>
		/// </remarks>
		public static int WeightedRandomIndexBinarySearch<TElement>(this IList<TElement> list, IRandom random, double[] cumulativeWeightSums, double weightSum)
		{
			return random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum);
		}

		#endregion

		#endregion

		#region Weighted Element

		#region sbyte

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, sbyte[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, sbyte[], int)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, sbyte[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, sbyte[] weights)
		{
			int weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, sbyte[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, sbyte[] weights, int weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte}, int)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, sbyte> weightsAccessor)
		{
			int weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, sbyte> weightsAccessor, int weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, sbyte[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, sbyte[], int)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, sbyte[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, sbyte[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, sbyte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, sbyte[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, sbyte[] weights, int weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, sbyte}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, sbyte}, int)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, sbyte> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, sbyte})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, sbyte> weightsAccessor, int weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		#endregion

		#region byte

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, byte[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, byte[], uint)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, byte[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, byte[] weights)
		{
			uint weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, byte[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, byte[] weights, uint weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, byte}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, byte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, byte}, uint)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, byte})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, byte> weightsAccessor)
		{
			uint weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, byte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, byte})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, byte> weightsAccessor, uint weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, byte[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, byte[], uint)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, byte[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, byte[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, byte[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, byte[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, byte[] weights, uint weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, byte}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, byte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, byte}, uint)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, byte})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, byte> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, byte})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, byte})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, byte> weightsAccessor, uint weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		#endregion

		#region short

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, short[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, short[], int)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, short[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, short[] weights)
		{
			int weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, short[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, short[] weights, int weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, short}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, short})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, short}, int)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, short})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, short> weightsAccessor)
		{
			int weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, short})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, short})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, short> weightsAccessor, int weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, short[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, short[], int)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, short[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, short[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, short[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, short[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, short[] weights, int weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, short}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, short})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, short}, int)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, short})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, short> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, short})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, short})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, short> weightsAccessor, int weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		#endregion

		#region ushort

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, ushort[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, ushort[], uint)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, ushort[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, ushort[] weights)
		{
			uint weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, ushort[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, ushort[] weights, uint weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, ushort}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ushort})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, ushort}, uint)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ushort})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, ushort> weightsAccessor)
		{
			uint weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ushort})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ushort})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, ushort> weightsAccessor, uint weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, ushort[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, ushort[], uint)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, ushort[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, ushort[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, ushort[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, ushort[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, ushort[] weights, uint weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, ushort}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ushort})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, ushort}, uint)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ushort})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ushort> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ushort})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ushort})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ushort> weightsAccessor, uint weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		#endregion

		#region int

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, int[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, int[] weights)
		{
			int weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, int[] weights, int weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, int}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, int})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, int}, int)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, int})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, int> weightsAccessor)
		{
			int weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, int})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, int})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, int> weightsAccessor, int weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		public static TElement WeightedElementBinarySearch<TElement>(this IRandom random, IList<TElement> list, int[] cumulativeWeightSums, int weightSum)
		{
			return list[random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, int[], int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, int[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, int[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, int[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, int[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, int[] weights, int weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, int}, int)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, int})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, int}, int)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, int})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, int> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, int})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, int})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, int> weightsAccessor, int weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		public static TElement WeightedRandomElementBinarySearch<TElement>(this IList<TElement> list, IRandom random, int[] cumulativeWeightSums, int weightSum)
		{
			return random.WeightedElementBinarySearch(list, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region uint

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, uint[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, uint[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, uint[] weights)
		{
			uint weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, uint[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, uint[] weights, uint weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, uint}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, uint})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, uint}, uint)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, uint})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, uint> weightsAccessor)
		{
			uint weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, uint})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, uint})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		public static TElement WeightedElementBinarySearch<TElement>(this IRandom random, IList<TElement> list, uint[] cumulativeWeightSums, uint weightSum)
		{
			return list[random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, uint[], uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, uint[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, uint[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, uint[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, uint[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, uint[] weights, uint weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, uint}, uint)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, uint})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, uint}, uint)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, uint})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, uint> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, uint})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, uint})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		public static TElement WeightedRandomElementBinarySearch<TElement>(this IList<TElement> list, IRandom random, uint[] cumulativeWeightSums, uint weightSum)
		{
			return random.WeightedElementBinarySearch(list, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region long

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, long[], long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, long[], long)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, long[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, long[] weights)
		{
			long weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, long[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, long[] weights, long weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, long}, long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, long})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, long}, long)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, long})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, long> weightsAccessor)
		{
			long weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, long})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, long})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, long> weightsAccessor, long weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		public static TElement WeightedElementBinarySearch<TElement>(this IRandom random, IList<TElement> list, long[] cumulativeWeightSums, long weightSum)
		{
			return list[random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, long[], long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, long[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, long[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, long[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, long[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, long[] weights, long weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, long}, long)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, long})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, long}, long)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, long})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, long> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, long})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, long})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, long> weightsAccessor, long weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		public static TElement WeightedRandomElementBinarySearch<TElement>(this IList<TElement> list, IRandom random, long[] cumulativeWeightSums, long weightSum)
		{
			return random.WeightedElementBinarySearch(list, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region ulong

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, ulong[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, ulong[] weights)
		{
			ulong weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, ulong[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, ulong[] weights, ulong weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, ulong}, ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ulong})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, ulong}, ulong)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ulong})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, ulong> weightsAccessor)
		{
			ulong weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ulong})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ulong})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, ulong> weightsAccessor, ulong weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		public static TElement WeightedElementBinarySearch<TElement>(this IRandom random, IList<TElement> list, ulong[] cumulativeWeightSums, ulong weightSum)
		{
			return list[random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, ulong[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, ulong[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, ulong[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, ulong[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, ulong[] weights, ulong weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, ulong}, ulong)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ulong})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, ulong}, ulong)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ulong})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ulong> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ulong})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, ulong})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ulong> weightsAccessor, ulong weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		public static TElement WeightedRandomElementBinarySearch<TElement>(this IList<TElement> list, IRandom random, ulong[] cumulativeWeightSums, ulong weightSum)
		{
			return random.WeightedElementBinarySearch(list, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region float

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, float[], float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, float[], float)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, float[], float)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, float[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, float[] weights)
		{
			float weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, float[], float)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, float[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, float[] weights, float weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, float}, float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, float})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, float}, float)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, float[], float)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, float})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, float> weightsAccessor)
		{
			float weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, float})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, float[], float)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, float})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, float> weightsAccessor, float weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		public static TElement WeightedElementBinarySearch<TElement>(this IRandom random, IList<TElement> list, float[] cumulativeWeightSums, float weightSum)
		{
			return list[random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, float[], float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, float[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, float[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, float[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, float[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, float[] weights, float weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, float}, float)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, float})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, float}, float)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, float})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, float> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, float})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, float})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, float> weightsAccessor, float weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		public static TElement WeightedRandomElementBinarySearch<TElement>(this IList<TElement> list, IRandom random, float[] cumulativeWeightSums, float weightSum)
		{
			return random.WeightedElementBinarySearch(list, cumulativeWeightSums, weightSum);
		}

		#endregion

		#region double

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, double[], double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, double[], double)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, double[], double)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, double[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, double[] weights)
		{
			double weightSum = SumWeights(weights.Length, weights);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, double[], double)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, double[])"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, double[] weights, double weightSum)
		{
			return list[random.WeightedIndex(list.Count, weights, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, double}, double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, double})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElement{TElement}(IRandom, IList{TElement}, System.Func{int, double}, double)"/>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, double[], double)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, double})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, double> weightsAccessor)
		{
			double weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, double})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, double[], double)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, double})"/>
		public static TElement WeightedElement<TElement>(this IRandom random, IList<TElement> list, System.Func<int, double> weightsAccessor, double weightSum)
		{
			return list[random.WeightedIndex(list.Count, weightsAccessor, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		public static TElement WeightedElementBinarySearch<TElement>(this IRandom random, IList<TElement> list, double[] cumulativeWeightSums, double weightSum)
		{
			return list[random.WeightedIndexBinarySearch(list.Count, cumulativeWeightSums, weightSum)];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, double[], double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, double[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, double[] weights)
		{
			return random.WeightedElement(list, weights);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, double[])"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, int, double[])"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, double[] weights, double weightSum)
		{
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to sum all the weights each time
		/// it is called. If called frequently, it is strongly recommended that the sum be first pre-computed
		/// and saved, and then the overload <see cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, double}, double)"/> can be
		/// used to avoid recomputing the sum.  Calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/> will be even faster, especially
		/// if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, double})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElement{TElement}(IList{TElement}, IRandom, System.Func{int, double}, double)"/>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, double})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, double> weightsAccessor)
		{
			return random.WeightedElement(list, weightsAccessor);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution"><para>This function needs to perform a linear search
		/// through the weights to determine the correct element to return.  Additional performance
		/// can be obtained by calculating an array of cumulative weight sums and calling
		/// <see cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>,
		/// especially if there are a large number of items.</para>
		/// <para>As an alternative, consider using <see cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, double})"/>
		/// to automate the pre-calculation process.</para></note></remarks>
		/// <seealso cref="WeightedRandomElementBinarySearch{TElement}(IList{TElement}, IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedElementGenerator{TElement}(IRandom, IList{TElement}, System.Func{int, double})"/>
		public static TElement WeightedRandomElement<TElement>(this IList<TElement> list, IRandom random, System.Func<int, double> weightsAccessor, double weightSum)
		{
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights from which <paramref name="cumulativeWeightSums"/> is derived.
		/// </summary>
		/// <typeparam name="TElement">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="cumulativeWeightSums">The cumulative weight sums of all weights from the first up to and including each sum's index that will determine the distribution by which indices are generated.  Must all be non-negative and sorted in increasing order.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="list"/>.Count weights.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks>
		/// <para>By using a pre-computed array of the sum of all weights for each element up to and including the weight
		/// for that element, the random selection process is able to forego scanning linearly through the weight list
		/// and can instead perform a binary search to find the selected element in logarithmic time.</para>
		/// </remarks>
		/// <seealso cref="WeightedElementBinarySearch{TElement}(IRandom, IList{TElement}, int[], int)"/>
		public static TElement WeightedRandomElementBinarySearch<TElement>(this IList<TElement> list, IRandom random, double[] cumulativeWeightSums, double weightSum)
		{
			return random.WeightedElementBinarySearch(list, cumulativeWeightSums, weightSum);
		}

		#endregion

		#endregion

		#region Weighted Index and Element Generators

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<sbyte, int> MakeWeightedIndexGenerator(this IRandom random, sbyte[] weights)
		{
			return new SByteWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<sbyte, int> MakeWeightedIndexGenerator(this IRandom random, int elementCount, sbyte[] weights)
		{
			return new SByteWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<sbyte, int> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor)
		{
			return new SByteWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, sbyte, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, sbyte[] weights)
		{
			return new SByteWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, sbyte, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, sbyte[] weights)
		{
			return new SByteWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, sbyte, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, sbyte> weightsAccessor)
		{
			return new SByteWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, sbyte, int> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, sbyte[] weights)
		{
			return new SByteWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, sbyte, int> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, sbyte> weightsAccessor)
		{
			return new SByteWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<byte, uint> MakeWeightedIndexGenerator(this IRandom random, byte[] weights)
		{
			return new ByteWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<byte, uint> MakeWeightedIndexGenerator(this IRandom random, int elementCount, byte[] weights)
		{
			return new ByteWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<byte, uint> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, byte> weightsAccessor)
		{
			return new ByteWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, byte, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, byte[] weights)
		{
			return new ByteWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, byte, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, byte[] weights)
		{
			return new ByteWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, byte, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, byte> weightsAccessor)
		{
			return new ByteWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, byte, uint> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, byte[] weights)
		{
			return new ByteWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, byte, uint> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, byte> weightsAccessor)
		{
			return new ByteWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<short, int> MakeWeightedIndexGenerator(this IRandom random, short[] weights)
		{
			return new ShortWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<short, int> MakeWeightedIndexGenerator(this IRandom random, int elementCount, short[] weights)
		{
			return new ShortWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<short, int> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, short> weightsAccessor)
		{
			return new ShortWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, short, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, short[] weights)
		{
			return new ShortWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, short, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, short[] weights)
		{
			return new ShortWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, short, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, short> weightsAccessor)
		{
			return new ShortWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, short, int> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, short[] weights)
		{
			return new ShortWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, short, int> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, short> weightsAccessor)
		{
			return new ShortWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<ushort, uint> MakeWeightedIndexGenerator(this IRandom random, ushort[] weights)
		{
			return new UShortWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<ushort, uint> MakeWeightedIndexGenerator(this IRandom random, int elementCount, ushort[] weights)
		{
			return new UShortWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<ushort, uint> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor)
		{
			return new UShortWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ushort, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, ushort[] weights)
		{
			return new UShortWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ushort, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, ushort[] weights)
		{
			return new UShortWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ushort, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, ushort> weightsAccessor)
		{
			return new UShortWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ushort, uint> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, ushort[] weights)
		{
			return new UShortWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ushort, uint> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ushort> weightsAccessor)
		{
			return new UShortWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<int, int> MakeWeightedIndexGenerator(this IRandom random, int[] weights)
		{
			return new IntWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<int, int> MakeWeightedIndexGenerator(this IRandom random, int elementCount, int[] weights)
		{
			return new IntWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<int, int> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, int> weightsAccessor)
		{
			return new IntWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, int, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int[] weights)
		{
			return new IntWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, int, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, int[] weights)
		{
			return new IntWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, int, int> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, int> weightsAccessor)
		{
			return new IntWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, int, int> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, int[] weights)
		{
			return new IntWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, int, int> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, int> weightsAccessor)
		{
			return new IntWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<uint, uint> MakeWeightedIndexGenerator(this IRandom random, uint[] weights)
		{
			return new UIntWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<uint, uint> MakeWeightedIndexGenerator(this IRandom random, int elementCount, uint[] weights)
		{
			return new UIntWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<uint, uint> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, uint> weightsAccessor)
		{
			return new UIntWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, uint, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, uint[] weights)
		{
			return new UIntWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, uint, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, uint[] weights)
		{
			return new UIntWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, uint, uint> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, uint> weightsAccessor)
		{
			return new UIntWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, uint, uint> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, uint[] weights)
		{
			return new UIntWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, uint, uint> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, uint> weightsAccessor)
		{
			return new UIntWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<long, long> MakeWeightedIndexGenerator(this IRandom random, long[] weights)
		{
			return new LongWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<long, long> MakeWeightedIndexGenerator(this IRandom random, int elementCount, long[] weights)
		{
			return new LongWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<long, long> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, long> weightsAccessor)
		{
			return new LongWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, long, long> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, long[] weights)
		{
			return new LongWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, long, long> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, long[] weights)
		{
			return new LongWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, long, long> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, long> weightsAccessor)
		{
			return new LongWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, long, long> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, long[] weights)
		{
			return new LongWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, long, long> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, long> weightsAccessor)
		{
			return new LongWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<ulong, ulong> MakeWeightedIndexGenerator(this IRandom random, ulong[] weights)
		{
			return new ULongWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<ulong, ulong> MakeWeightedIndexGenerator(this IRandom random, int elementCount, ulong[] weights)
		{
			return new ULongWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<ulong, ulong> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor)
		{
			return new ULongWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ulong, ulong> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, ulong[] weights)
		{
			return new ULongWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ulong, ulong> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, ulong[] weights)
		{
			return new ULongWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ulong, ulong> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, ulong> weightsAccessor)
		{
			return new ULongWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ulong, ulong> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, ulong[] weights)
		{
			return new ULongWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, ulong, ulong> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, ulong> weightsAccessor)
		{
			return new ULongWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<float, float> MakeWeightedIndexGenerator(this IRandom random, float[] weights)
		{
			return new FloatWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<float, float> MakeWeightedIndexGenerator(this IRandom random, int elementCount, float[] weights)
		{
			return new FloatWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<float, float> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, float> weightsAccessor)
		{
			return new FloatWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, float, float> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, float[] weights)
		{
			return new FloatWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, float, float> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, float[] weights)
		{
			return new FloatWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, float, float> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, float> weightsAccessor)
		{
			return new FloatWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, float, float> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, float[] weights)
		{
			return new FloatWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, float, float> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, float> weightsAccessor)
		{
			return new FloatWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<double, double> MakeWeightedIndexGenerator(this IRandom random, double[] weights)
		{
			return new DoubleWeightedIndexGenerator(random, weights.Length, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<double, double> MakeWeightedIndexGenerator(this IRandom random, int elementCount, double[] weights)
		{
			return new DoubleWeightedIndexGenerator(random, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted index generator which will produce random indices in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A weighted index generator which produces random indices in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="important">The indices mapped by <paramref name="weightsAccessor"/> and the weight values that it returns must not change
		/// without a corresponding call to <see cref="IWeightedIndexGenerator{TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedIndexGenerator<double, double> MakeWeightedIndexGenerator(this IRandom random, int elementCount, System.Func<int, double> weightsAccessor)
		{
			return new DoubleWeightedIndexGenerator(random, elementCount, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, double, double> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, double[] weights)
		{
			return new DoubleWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, double, double> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, int elementCount, double[] weights)
		{
			return new DoubleWeightedElementGenerator<TElement>(random, list, elementCount, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, double, double> MakeWeightedElementGenerator<TElement>(this IRandom random, IList<TElement> list, System.Func<int, double> weightsAccessor)
		{
			return new DoubleWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must have at least as many elements as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, double, double> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, double[] weights)
		{
			return new DoubleWeightedElementGenerator<TElement>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="TElement">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{TElement, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<TElement, double, double> MakeWeightedRandomElementGenerator<TElement>(this IList<TElement> list, IRandom random, System.Func<int, double> weightsAccessor)
		{
			return new DoubleWeightedElementGenerator<TElement>(random, list, weightsAccessor);
		}

		#endregion
	}
}
