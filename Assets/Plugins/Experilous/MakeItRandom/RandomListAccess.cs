/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;
using IListNonGeneric = System.Collections.IList;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// An interface for any generator of list element access.
	/// </summary>
	/// <typeparam name="T">The element type contained by the referenced list and returned by the generator.</typeparam>
	public interface IElementGenerator<T>
	{
		/// <summary>
		/// Get the next random element selected by the generator.
		/// </summary>
		/// <returns>The next list element randomly selected according to the generator implementation.</returns>
		T Next();

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
		T Next(out int index);
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
	/// <typeparam name="T">The element type contained by the referenced list and returned by the generator.</typeparam>
	/// <typeparam name="TWeight">The numeric type of the weights.</typeparam>
	/// <typeparam name="TWeightSum">The numeric type of the summation of weights</typeparam>
	public interface IWeightedElementGenerator<T, TWeight, TWeightSum> : IElementGenerator<T>
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
		T Next(out TWeight weight);

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
		T Next(out int index, out TWeight weight);

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
	/// A static class of extension methods for generating random indices, typically for accessing collections using a zero-based indexing scheme.
	/// </summary>
	public static class RandomListAccess
	{
		#region Private Generator Classes

		#region Uniform

		private class VariableLengthArrayIndexGenerator<T> : IIntGenerator
		{
			private IRandom _random;
			private T[] _array;

			public VariableLengthArrayIndexGenerator(IRandom random, T[] array)
			{
				_random = random;
				_array = array;
			}

			public int Next()
			{
				return _random.RangeCO(_array.Length);
			}
		}

		private class VariableLengthListIndexGenerator<T> : IIntGenerator
		{
			private IRandom _random;
			private IList<T> _list;

			public VariableLengthListIndexGenerator(IRandom random, IList<T> list)
			{
				_random = random;
				_list = list;
			}

			public int Next()
			{
				return _random.RangeCO(_list.Count);
			}
		}

		private class FixedLengthArrayElementGenerator<T> : IElementGenerator<T>
		{
			private IIntGenerator _indexGenerator;
			private T[] _array;

			public FixedLengthArrayElementGenerator(IRandom random, T[] array)
			{
				_indexGenerator = random.MakeIndexGenerator(array.Length);
				_array = array;
			}

			public T Next()
			{
				return _array[_indexGenerator.Next()];
			}

			public int NextIndex()
			{
				return _indexGenerator.Next();
			}

			public T Next(out int index)
			{
				index = _indexGenerator.Next();
				return _array[index];
			}
		}

		private class FixedLengthListElementGenerator<T> : IElementGenerator<T>
		{
			private IIntGenerator _indexGenerator;
			private IList<T> _list;

			public FixedLengthListElementGenerator(IRandom random, IList<T> list)
			{
				_indexGenerator = random.MakeIndexGenerator(list.Count);
				_list = list;
			}

			public T Next()
			{
				return _list[_indexGenerator.Next()];
			}

			public int NextIndex()
			{
				return _indexGenerator.Next();
			}

			public T Next(out int index)
			{
				index = _indexGenerator.Next();
				return _list[index];
			}
		}

		private class VariableLengthArrayElementGenerator<T> : IElementGenerator<T>
		{
			private IRandom _random;
			private T[] _array;

			public VariableLengthArrayElementGenerator(IRandom random, T[] array)
			{
				_random = random;
				_array = array;
			}

			public T Next()
			{
				return _array[_random.RangeCO(_array.Length)];
			}

			public int NextIndex()
			{
				return _random.RangeCO(_array.Length);
			}

			public T Next(out int index)
			{
				index = _random.RangeCO(_array.Length);
				return _array[index];
			}
		}

		private class VariableLengthListElementGenerator<T> : IElementGenerator<T>
		{
			private IRandom _random;
			private IList<T> _list;

			public VariableLengthListElementGenerator(IRandom random, IList<T> list)
			{
				_random = random;
				_list = list;
			}

			public T Next()
			{
				return _list[_random.RangeCO(_list.Count)];
			}

			public int NextIndex()
			{
				return _random.RangeCO(_list.Count);
			}

			public T Next(out int index)
			{
				index = _random.RangeCO(_list.Count);
				return _list[index];
			}
		}

		#endregion

		#region Weighted

		#region Weight Summation

		private static int SumWeights(sbyte[] weights, int length)
		{
			int weightSum = 0;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
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

		private static uint SumWeights(byte[] weights, int length)
		{
			uint weightSum = 0U;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
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

		private static int SumWeights(short[] weights, int length)
		{
			int weightSum = 0;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
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

		private static uint SumWeights(ushort[] weights, int length)
		{
			uint weightSum = 0U;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
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

		private static int SumWeights(int[] weights, int length)
		{
			int weightSum = 0;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
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

		private static uint SumWeights(uint[] weights, int length)
		{
			uint weightSum = 0U;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
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

		private static long SumWeights(long[] weights, int length)
		{
			long weightSum = 0L;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
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

		private static ulong SumWeights(ulong[] weights, int length)
		{
			ulong weightSum = 0UL;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
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

		private static float SumWeights(float[] weights, int length)
		{
			float weightSum = 0f;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
			}
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

		private static double SumWeights(double[] weights, int length)
		{
			double weightSum = 0d;
			for (int i = 0; i < length; ++i)
			{
				weightSum += weights[i];
			}
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

		private abstract class WeightedIndexGeneratorBase<TWeight, TWeightSum>
		{
			protected IRandom _random;
			protected TWeightSum _weightSum;
			protected int _length;
			protected TWeight[] _weights;
			protected System.Func<int, TWeight> _weightsAccessor;

			protected WeightedIndexGeneratorBase(IRandom random, TWeight[] weights)
			{
				_random = random;
				_length = weights.Length;
				_weights = weights;
				SumWeights();
			}

			public WeightedIndexGeneratorBase(IRandom random, int elementCount, System.Func<int, TWeight> weightsAccessor)
			{
				_random = random;
				_length = elementCount;
				_weights = new TWeight[elementCount];
				_weightsAccessor = weightsAccessor;
				for (int i = 0; i < _length; ++i)
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
				_length = weights.Length;
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
				_length = elementCount;
				_weightsAccessor = weightsAccessor;
				for (int i = 0; i < _length; ++i)
				{
					_weights[i] = weightsAccessor(i);
				}
				SumWeights();
			}
		}

		private abstract class WeightedIndexGenerator<TWeight, TWeightSum> : WeightedIndexGeneratorBase<TWeight, TWeightSum>, IWeightedIndexGenerator<TWeight, TWeightSum>
		{
			public WeightedIndexGenerator(IRandom random, TWeight[] weights) : base(random, weights) { }
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
					UpdateWeights(_length, _weightsAccessor);
				}
			}
		}

		private abstract class WeightedElementGenerator<T, TWeight, TWeightSum> : WeightedIndexGeneratorBase<TWeight, TWeightSum>, IWeightedElementGenerator<T, TWeight, TWeightSum>
		{
			private IList<T> _list;

			public WeightedElementGenerator(IRandom random, IList<T> list, TWeight[] weights) : base(random, weights)
			{
				_list = list;
			}

			public WeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, TWeight> weightsAccessor) : base(random, list.Count, weightsAccessor)
			{
				_list = list;
			}

			public T Next()
			{
				return _list[NextIndex()];
			}

			public T Next(out TWeight weight)
			{
				int index = NextIndex();
				weight = _weights[index];
				return _list[index];
			}

			T IElementGenerator<T>.Next(out int index)
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

			public T Next(out int index, out TWeight weight)
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
			public SByteWeightedIndexGenerator(IRandom random, sbyte[] weights) : base(random, weights) { }
			public SByteWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class SByteWeightedElementGenerator<T> : WeightedElementGenerator<T, sbyte, int>
		{
			public SByteWeightedElementGenerator(IRandom random, IList<T> list, sbyte[] weights) : base(random, list, weights) { }
			public SByteWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, sbyte> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class ByteWeightedIndexGenerator : WeightedIndexGenerator<byte, uint>
		{
			public ByteWeightedIndexGenerator(IRandom random, byte[] weights) : base(random, weights) { }
			public ByteWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, byte> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class ByteWeightedElementGenerator<T> : WeightedElementGenerator<T, byte, uint>
		{
			public ByteWeightedElementGenerator(IRandom random, IList<T> list, byte[] weights) : base(random, list, weights) { }
			public ByteWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, byte> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class ShortWeightedIndexGenerator : WeightedIndexGenerator<short, int>
		{
			public ShortWeightedIndexGenerator(IRandom random, short[] weights) : base(random, weights) { }
			public ShortWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, short> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class ShortWeightedElementGenerator<T> : WeightedElementGenerator<T, short, int>
		{
			public ShortWeightedElementGenerator(IRandom random, IList<T> list, short[] weights) : base(random, list, weights) { }
			public ShortWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, short> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class UShortWeightedIndexGenerator : WeightedIndexGenerator<ushort, uint>
		{
			public UShortWeightedIndexGenerator(IRandom random, ushort[] weights) : base(random, weights) { }
			public UShortWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class UShortWeightedElementGenerator<T> : WeightedElementGenerator<T, ushort, uint>
		{
			public UShortWeightedElementGenerator(IRandom random, IList<T> list, ushort[] weights) : base(random, list, weights) { }
			public UShortWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, ushort> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class IntWeightedIndexGenerator : WeightedIndexGenerator<int, int>
		{
			public IntWeightedIndexGenerator(IRandom random, int[] weights) : base(random, weights) { }
			public IntWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, int> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class IntWeightedElementGenerator<T> : WeightedElementGenerator<T, int, int>
		{
			public IntWeightedElementGenerator(IRandom random, IList<T> list, int[] weights) : base(random, list, weights) { }
			public IntWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, int> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class UIntWeightedIndexGenerator : WeightedIndexGenerator<uint, uint>
		{
			public UIntWeightedIndexGenerator(IRandom random, uint[] weights) : base(random, weights) { }
			public UIntWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, uint> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class UIntWeightedElementGenerator<T> : WeightedElementGenerator<T, uint, uint>
		{
			public UIntWeightedElementGenerator(IRandom random, IList<T> list, uint[] weights) : base(random, list, weights) { }
			public UIntWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, uint> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class LongWeightedIndexGenerator : WeightedIndexGenerator<long, long>
		{
			public LongWeightedIndexGenerator(IRandom random, long[] weights) : base(random, weights) { }
			public LongWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, long> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class LongWeightedElementGenerator<T> : WeightedElementGenerator<T, long, long>
		{
			public LongWeightedElementGenerator(IRandom random, IList<T> list, long[] weights) : base(random, list, weights) { }
			public LongWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, long> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class ULongWeightedIndexGenerator : WeightedIndexGenerator<ulong, ulong>
		{
			public ULongWeightedIndexGenerator(IRandom random, ulong[] weights) : base(random, weights) { }
			public ULongWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class ULongWeightedElementGenerator<T> : WeightedElementGenerator<T, ulong, ulong>
		{
			public ULongWeightedElementGenerator(IRandom random, IList<T> list, ulong[] weights) : base(random, list, weights) { }
			public ULongWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, ulong> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class FloatWeightedIndexGenerator : WeightedIndexGenerator<float, float>
		{
			public FloatWeightedIndexGenerator(IRandom random, float[] weights) : base(random, weights) { }
			public FloatWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, float> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class FloatWeightedElementGenerator<T> : WeightedElementGenerator<T, float, float>
		{
			public FloatWeightedElementGenerator(IRandom random, IList<T> list, float[] weights) : base(random, list, weights) { }
			public FloatWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, float> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class DoubleWeightedIndexGenerator : WeightedIndexGenerator<double, double>
		{
			public DoubleWeightedIndexGenerator(IRandom random, double[] weights) : base(random, weights) { }
			public DoubleWeightedIndexGenerator(IRandom random, int elementCount, System.Func<int, double> weightsAccessor) : base(random, elementCount, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		private class DoubleWeightedElementGenerator<T> : WeightedElementGenerator<T, double, double>
		{
			public DoubleWeightedElementGenerator(IRandom random, IList<T> list, double[] weights) : base(random, list, weights) { }
			public DoubleWeightedElementGenerator(IRandom random, IList<T> list, System.Func<int, double> weightsAccessor) : base(random, list, weightsAccessor) { }

			public override int NextIndex()
			{
				return _random.WeightedIndex(_weights, _length, _weightSum);
			}

			protected override void SumWeights()
			{
				_weightSum = RandomListAccess.SumWeights(_weights, _length);
			}
		}

		#endregion

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
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection for which a random index will be generated.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		public static int Index<T>(this IRandom random, IList<T> list)
		{
			return random.RangeCO(list.Count);
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random index in the range [0, <paramref name="list"/>.Count).</returns>
		public static int RandomIndex<T>(this IList<T> list, IRandom random)
		{
			return random.RangeCO(list.Count);
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		public static T Element<T>(this IRandom random, IList<T> list)
		{
			return list[random.RangeCO(list.Count)];
		}

		/// <summary>
		/// Returns a uniformly selected random element from <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		public static T RandomElement<T>(this IList<T> list, IRandom random)
		{
			return list[random.RangeCO(list.Count)];
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="length"/>).</returns>
		public static IIntGenerator MakeIndexGenerator(this IRandom random, int length)
		{
			return random.MakeRangeCOGenerator(length);
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="length"/>).</returns>
		public static IUIntGenerator MakeIndexGenerator(this IRandom random, uint length)
		{
			return random.MakeRangeCOGenerator(length);
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="length"/>).</returns>
		public static ILongGenerator MakeIndexGenerator(this IRandom random, long length)
		{
			return random.MakeRangeCOGenerator(length);
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="length"/>), suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="length">The length of a suitable collection whose elements can be accessed using the generated index.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="length"/>).</returns>
		public static IULongGenerator MakeIndexGenerator(this IRandom random, ulong length)
		{
			return random.MakeRangeCOGenerator(length);
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection for which the range generator will produce random indices.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>If <paramref name="list"/> is marked as having a fixed size, then the range generator returned has the opportunity
		/// to be more efficient, because the generated indices will always come from the same range.  Otherwise, each index generated
		/// will come from the range [0, <paramref name="list"/>.Count) according to the value of <paramref name="list"/>.Count at the
		/// time the index is generated.</remarks>
		public static IIntGenerator MakeIndexGenerator<T>(this IRandom random, IList<T> list)
		{
			IListNonGeneric nonGenericList = list as IListNonGeneric;
			if (nonGenericList != null && nonGenericList.IsFixedSize)
			{
				return random.MakeRangeCOGenerator(list.Count);
			}
			else
			{
				T[] array = list as T[];
				if (array != null)
				{
					return new VariableLengthArrayIndexGenerator<T>(random, array);
				}
				else
				{
					return new VariableLengthListIndexGenerator<T>(random, list);
				}
			}
		}

		/// <summary>
		/// Returns an element generator which will return elements from <paramref name="list"/> with an equally weighted distribution.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <returns>An element generator which returns random elements from <paramref name="list"/>.</returns>
		/// <remarks>If <paramref name="list"/> is marked as having a fixed size, then the element generator returned has the opportunity
		/// to be more efficient, because the internally generated indices will always come from the same range.</remarks>
		public static IElementGenerator<T> MakeElementGenerator<T>(this IRandom random, IList<T> list)
		{
			IListNonGeneric nonGenericList = list as IListNonGeneric;
			T[] array = list as T[];
			if (nonGenericList != null && nonGenericList.IsFixedSize)
			{
				if (array != null)
				{
					return new FixedLengthArrayElementGenerator<T>(random, array);
				}
				else
				{
					return new FixedLengthListElementGenerator<T>(random, list);
				}
			}
			else
			{
				if (array != null)
				{
					return new VariableLengthArrayElementGenerator<T>(random, array);
				}
				else
				{
					return new VariableLengthListElementGenerator<T>(random, list);
				}
			}
		}

		/// <summary>
		/// Returns a range generator which will produce uniformly distributed random indices in the range [0, <paramref name="list"/>.Count), suitable for indexing into <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The collection for which the range generator will produce random indices.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A range generator which produces random indices in the range [0, <paramref name="list"/>.Count).</returns>
		/// <remarks>If <paramref name="list"/> is marked as having a fixed size, then the range generator returned has the opportunity
		/// to be more efficient, because the generated indices will always come from the same range.  Otherwise, each index generated
		/// will come from the range [0, <paramref name="list"/>.Count) according to the value of <paramref name="list"/>.Count at the
		/// time the index is generated.</remarks>
		public static IIntGenerator MakeRandomIndexGenerator<T>(this IList<T> list, IRandom random)
		{
			return random.MakeIndexGenerator(list);
		}

		/// <summary>
		/// Returns an element generator which will return elements from <paramref name="list"/> with an equally weighted distribution.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>An element generator which returns random elements from <paramref name="list"/>.</returns>
		/// <remarks>If <paramref name="list"/> is marked as having a fixed size, then the element generator returned has the opportunity
		/// to be more efficient, because the internally generated indices will always come from the same range.</remarks>
		public static IElementGenerator<T> MakeRandomElementGenerator<T>(this IList<T> list, IRandom random)
		{
			return random.MakeElementGenerator(list);
		}

		#endregion

		#region Weighted Index

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, sbyte[], int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, sbyte[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, sbyte[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, sbyte[])"/>
		public static int WeightedIndex(this IRandom random, sbyte[] weights)
		{
			int weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, sbyte[] weights, int elementCount, int weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, sbyte}, int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, sbyte})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, sbyte}, int)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, sbyte> weightsAccessor, int weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, byte[], uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, byte[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, byte[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, byte[])"/>
		public static int WeightedIndex(this IRandom random, byte[] weights)
		{
			uint weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, byte[] weights, int elementCount, uint weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, byte}, uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, byte})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, byte}, uint)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, byte> weightsAccessor, uint weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, short[], int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, short[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, short[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, short[])"/>
		public static int WeightedIndex(this IRandom random, short[] weights)
		{
			int weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, short[] weights, int elementCount, int weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, short}, int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, short})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, short}, int)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, short> weightsAccessor, int weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, ushort[], uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, ushort[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, ushort[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, ushort[])"/>
		public static int WeightedIndex(this IRandom random, ushort[] weights)
		{
			uint weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, ushort[] weights, int elementCount, uint weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, ushort}, uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ushort})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, ushort}, uint)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ushort> weightsAccessor, uint weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int[], int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int[], int)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, int[])"/>
		public static int WeightedIndex(this IRandom random, int[] weights)
		{
			int weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, int[] weights, int elementCount, int weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, int}, int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, int})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, int}, int)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, int> weightsAccessor, int weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, uint[], uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, uint[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, uint[], uint)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, uint[])"/>
		public static int WeightedIndex(this IRandom random, uint[] weights)
		{
			uint weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, uint[] weights, int elementCount, uint weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, uint}, uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, uint})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, uint}, uint)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, long[], long)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, long[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, long[], long)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, long[])"/>
		public static int WeightedIndex(this IRandom random, long[] weights)
		{
			long weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, long[] weights, int elementCount, long weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, long}, long)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, long})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, long}, long)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, long> weightsAccessor, long weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, ulong[], ulong)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, ulong[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, ulong[])"/>
		public static int WeightedIndex(this IRandom random, ulong[] weights)
		{
			ulong weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, ulong[] weights, int elementCount, ulong weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, ulong}, ulong)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, ulong})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, ulong}, ulong)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, ulong> weightsAccessor, ulong weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, float[], float)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, float[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, float[], float)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, float[])"/>
		public static int WeightedIndex(this IRandom random, float[] weights)
		{
			float weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, float[] weights, int elementCount, float weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, float}, float)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, float})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, float}, float)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, float> weightsAccessor, float weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="weights"/>.Length), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <returns>A random index in the range [0, <paramref name="weights"/>.Length).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, double[], double)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, double[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, double[], double)"/>
		/// <seealso cref="MakeWeightedIndexGenerator(IRandom, double[])"/>
		public static int WeightedIndex(this IRandom random, double[] weights)
		{
			double weightSum = SumWeights(weights, weights.Length);
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
			return random.WeightedIndex(weights, weights.Length, weightSum);
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to <paramref name="weights"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.</param>
		/// <param name="elementCount">The number of elements from <paramref name="weights"/> to consider.</param>
		/// <param name="weightSum">The pre-calculated sum of the first <paramref name="elementCount"/> values in <paramref name="weights"/>.</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		public static int WeightedIndex(this IRandom random, double[] weights, int elementCount, double weightSum)
		{
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
		}

		/// <summary>
		/// Returns a random index in the range [0, <paramref name="elementCount"/>), non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>, suitable for indexing into a collection with a corresponding length.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="elementCount">The number of elements that <paramref name="weightsAccessor"/> can map to weight values.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="elementCount"/>).</param>
		/// <returns>A random index in the range [0, <paramref name="elementCount"/>).</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedIndex(IRandom, int, System.Func{int, double}, double)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedIndexGenerator(IRandom, int, System.Func{int, double})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedIndex(IRandom, int, System.Func{int, double}, double)"/>
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
		public static int WeightedIndex(this IRandom random, int elementCount, System.Func<int, double> weightsAccessor, double weightSum)
		{
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
		}

		#endregion

		#region Weighted Element

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, sbyte[], int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, sbyte[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, sbyte[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, sbyte[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, sbyte[] weights)
		{
			int weightSum = SumWeights(weights, weights.Length);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, sbyte[])"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, sbyte[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, sbyte[] weights, int weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return list[index];
				}

				weightSum -= weights[index++];
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, sbyte}, int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, sbyte})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, sbyte}, int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, sbyte})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, sbyte> weightsAccessor)
		{
			int weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, sbyte})"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, sbyte})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, sbyte> weightsAccessor, int weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return list[index];
				}

				weightSum -= weightsAccessor(index++);
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, byte[], uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, byte[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, byte[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, byte[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, byte[] weights)
		{
			uint weightSum = SumWeights(weights, weights.Length);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, byte[])"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, byte[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, byte[] weights, uint weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return list[index];
				}

				weightSum -= weights[index++];
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, byte}, uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, byte})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, byte}, uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, byte})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, byte> weightsAccessor)
		{
			uint weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, byte})"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, byte})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, byte> weightsAccessor, uint weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return list[index];
				}

				weightSum -= weightsAccessor(index++);
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, short[], int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, short[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, short[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, short[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, short[] weights)
		{
			int weightSum = SumWeights(weights, weights.Length);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, short[])"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, short[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, short[] weights, int weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return list[index];
				}

				weightSum -= weights[index++];
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, short}, int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, short})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, short}, int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, short})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, short> weightsAccessor)
		{
			int weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, short})"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, short})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, short> weightsAccessor, int weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return list[index];
				}

				weightSum -= weightsAccessor(index++);
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, ushort[], uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, ushort[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, ushort[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, ushort[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, ushort[] weights)
		{
			uint weightSum = SumWeights(weights, weights.Length);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, ushort[])"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, ushort[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, ushort[] weights, uint weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return list[index];
				}

				weightSum -= weights[index++];
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, ushort}, uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, ushort})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, ushort}, uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, ushort})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, ushort> weightsAccessor)
		{
			uint weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, ushort})"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, ushort})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, ushort> weightsAccessor, uint weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return list[index];
				}

				weightSum -= weightsAccessor(index++);
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, int[], int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, int[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, int[], int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, int[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, int[] weights)
		{
			int weightSum = SumWeights(weights, weights.Length);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, int[])"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, int[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, int[] weights, int weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return list[index];
				}

				weightSum -= weights[index++];
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, int}, int)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, int})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, int}, int)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, int})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, int> weightsAccessor)
		{
			int weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, int})"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, int})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, int> weightsAccessor, int weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return list[index];
				}

				weightSum -= weightsAccessor(index++);
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, uint[], uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, uint[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, uint[], uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, uint[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, uint[] weights)
		{
			uint weightSum = SumWeights(weights, weights.Length);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, uint[])"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, uint[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, uint[] weights, uint weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return list[index];
				}

				weightSum -= weights[index++];
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, uint}, uint)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, uint})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, uint}, uint)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, uint})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, uint> weightsAccessor)
		{
			uint weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, uint})"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, uint})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, uint> weightsAccessor, uint weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return list[index];
				}

				weightSum -= weightsAccessor(index++);
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, long[], long)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, long[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, long[], long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, long[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, long[] weights)
		{
			long weightSum = SumWeights(weights, weights.Length);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, long[])"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, long[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, long[] weights, long weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return list[index];
				}

				weightSum -= weights[index++];
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, long}, long)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, long})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, long}, long)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, long})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, long> weightsAccessor)
		{
			long weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, long})"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, long})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, long> weightsAccessor, long weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return list[index];
				}

				weightSum -= weightsAccessor(index++);
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, ulong[], ulong)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, ulong[])"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, ulong[], ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, ulong[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, ulong[] weights)
		{
			ulong weightSum = SumWeights(weights, weights.Length);
			return random.WeightedElement(list, weights, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <param name="weightSum">The pre-calculated sum of all the values in <paramref name="weights"/>.</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, ulong[])"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, ulong[])"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, ulong[] weights, ulong weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weights[index])
				{
					return list[index];
				}

				weightSum -= weights[index++];
			}
			return list[index];
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <remarks><note type="caution">This function needs to sum all the weights each time it is called.
		/// If called frequently, it is strongly recommended that the sum be first pre-computed and saved, and then the overload
		/// <see cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, ulong}, ulong)"/> can be used to avoid recomputing the sum.
		/// As an alternative, consider using <see cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, ulong})"/> to
		/// automate the process.</note></remarks>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, ulong}, ulong)"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, ulong})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, ulong> weightsAccessor)
		{
			ulong weightSum = SumWeights(list.Count, weightsAccessor);
			return random.WeightedElement(list, weightsAccessor, weightSum);
		}

		/// <summary>
		/// Returns a randomly selected element from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which a random element will be selected.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <param name="weightSum">The pre-calculated sum of all the weights returned by <paramref name="weightsAccessor"/> when called with indices in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A random element from <paramref name="list"/>.</returns>
		/// <seealso cref="WeightedElement{T}(IRandom, IList{T}, System.Func{int, ulong})"/>
		/// <seealso cref="MakeWeightedElementGenerator{T}(IRandom, IList{T}, System.Func{int, ulong})"/>
		public static T WeightedElement<T>(this IRandom random, IList<T> list, System.Func<int, ulong> weightsAccessor, ulong weightSum)
		{
			int index = 0;
			int lastIndex = list.Count - 1;
			while (index < lastIndex)
			{
				if (random.RangeCO(weightSum) < weightsAccessor(index))
				{
					return list[index];
				}

				weightSum -= weightsAccessor(index++);
			}
			return list[index];
		}

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
			return new SByteWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, sbyte, int> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, sbyte[] weights)
		{
			return new SByteWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, sbyte, int> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, sbyte> weightsAccessor)
		{
			return new SByteWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, sbyte, int> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, sbyte[] weights)
		{
			return new SByteWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, sbyte, int> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, sbyte> weightsAccessor)
		{
			return new SByteWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new ByteWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, byte, uint> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, byte[] weights)
		{
			return new ByteWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, byte, uint> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, byte> weightsAccessor)
		{
			return new ByteWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, byte, uint> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, byte[] weights)
		{
			return new ByteWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, byte, uint> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, byte> weightsAccessor)
		{
			return new ByteWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new ShortWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, short, int> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, short[] weights)
		{
			return new ShortWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, short, int> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, short> weightsAccessor)
		{
			return new ShortWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, short, int> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, short[] weights)
		{
			return new ShortWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, short, int> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, short> weightsAccessor)
		{
			return new ShortWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new UShortWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, ushort, uint> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, ushort[] weights)
		{
			return new UShortWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, ushort, uint> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, ushort> weightsAccessor)
		{
			return new UShortWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, ushort, uint> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, ushort[] weights)
		{
			return new UShortWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, ushort, uint> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, ushort> weightsAccessor)
		{
			return new UShortWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new IntWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, int, int> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, int[] weights)
		{
			return new IntWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, int, int> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, int> weightsAccessor)
		{
			return new IntWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, int, int> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, int[] weights)
		{
			return new IntWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, int, int> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, int> weightsAccessor)
		{
			return new IntWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new UIntWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, uint, uint> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, uint[] weights)
		{
			return new UIntWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, uint, uint> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, uint> weightsAccessor)
		{
			return new UIntWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, uint, uint> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, uint[] weights)
		{
			return new UIntWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, uint, uint> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, uint> weightsAccessor)
		{
			return new UIntWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new LongWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, long, long> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, long[] weights)
		{
			return new LongWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, long, long> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, long> weightsAccessor)
		{
			return new LongWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, long, long> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, long[] weights)
		{
			return new LongWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, long, long> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, long> weightsAccessor)
		{
			return new LongWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new ULongWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, ulong, ulong> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, ulong[] weights)
		{
			return new ULongWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, ulong, ulong> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, ulong> weightsAccessor)
		{
			return new ULongWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, ulong, ulong> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, ulong[] weights)
		{
			return new ULongWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, ulong, ulong> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, ulong> weightsAccessor)
		{
			return new ULongWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new FloatWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, float, float> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, float[] weights)
		{
			return new FloatWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, float, float> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, float> weightsAccessor)
		{
			return new FloatWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, float, float> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, float[] weights)
		{
			return new FloatWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, float, float> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, float> weightsAccessor)
		{
			return new FloatWeightedElementGenerator<T>(random, list, weightsAccessor);
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
			return new DoubleWeightedIndexGenerator(random, weights);
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
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, double, double> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, double[] weights)
		{
			return new DoubleWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, double, double> MakeWeightedElementGenerator<T>(this IRandom random, IList<T> list, System.Func<int, double> weightsAccessor)
		{
			return new DoubleWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to <paramref name="weights"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.  Must be the same length as <paramref name="weights"/>.</param>
		/// <param name="weights">The weights that will determine the distribution by which indices are generated.  Must be the same length as <paramref name="list"/>.</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The values in the <paramref name="weights"/> array must not be changed without a corresponding
		/// call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/> to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, double, double> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, double[] weights)
		{
			return new DoubleWeightedElementGenerator<T>(random, list, weights);
		}

		/// <summary>
		/// Returns a weighted element generator which will return random elements from <paramref name="list"/>, non-uniformly distributed according to the weights provided by <paramref name="weightsAccessor"/>.
		/// </summary>
		/// <typeparam name="T">The element type contained by <paramref name="list"/> and returned by the generator.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="list">The collection from which the element generator will select random elements.</param>
		/// <param name="weightsAccessor">The delegate that maps collection indices to the weights that will determine the distribution by which indices are generated.  Must return valid weight values for all index inputs in the range [0, <paramref name="list"/>.Count).</param>
		/// <returns>A weighted element generator which will return random elements from <paramref name="list"/>.</returns>
		/// <remarks><note type="important">The size of <paramref name="list"/>, the indices mapped by <paramref name="weightsAccessor"/>,
		/// and the weight values that it returns must not change without a corresponding call to <see cref="IWeightedElementGenerator{T, TWeight, TWeightSum}.UpdateWeights()"/>
		/// to update the generator's internal state.</note></remarks>
		public static IWeightedElementGenerator<T, double, double> MakeWeightedRandomElementGenerator<T>(this IList<T> list, IRandom random, System.Func<int, double> weightsAccessor)
		{
			return new DoubleWeightedElementGenerator<T>(random, list, weightsAccessor);
		}

		#endregion
	}
}
