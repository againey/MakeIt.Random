/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.Randomization
{
	public struct Shuffle
	{
		private IRandomEngine _random;

		public Shuffle(IRandomEngine random)
		{
			_random = random;
		}

		#region Public Interface

		public IList<T> InPlace<T>(IList<T> list)
		{
			T[] array = list as T[];
			if (array != null)
			{
				Knuth_ShuffleArray(array);
			}
			else
			{
				Knuth_ShuffleList(list);
			}
			return list;
		}

		public IList<T> Into<T>(IEnumerable<T> source, IList<T> target)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				Knuth_ShuffleListInto(list, target);
			}
			else
			{
				Knuth_ShuffleEnumerableInto(source, target);
			}
			return target;
		}

		public IList<T> InPlaceCyclic<T>(IList<T> list)
		{
			T[] array = list as T[];
			if (array != null)
			{
				Sattolo_ShuffleArray(array);
			}
			else
			{
				Sattolo_ShuffleList(list);
			}
			return list;
		}

		public IList<T> IntoCyclic<T>(IEnumerable<T> source, IList<T> target)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				Sattolo_ShuffleListInto(list, target);
			}
			else
			{
				Sattolo_ShuffleEnumerableInto(source, target);
			}
			return target;
		}

		#endregion

		#region Standard Shuffle (Knuth, Fisher-Yates Shuffle)

		private T[] Knuth_ShuffleArray<T>(T[] array)
		{
			for (int i = array.Length - 1; i > 0; --i)
			{
				Utility.Swap(ref array[i], ref array[_random.Range().Closed(i)]);
			}
			return array;
		}

		private IList<T> Knuth_ShuffleList<T>(IList<T> list)
		{
			for (int i = list.Count - 1; i > 0; --i)
			{
				int j = _random.Range().Closed(i);
				T temp = list[i];
				list[i] = list[j];
				list[j] = temp;
			}
			return list;
		}

		private IList<T> Knuth_ShuffleListInto<T>(IList<T> source, IList<T> target)
		{
			if (source.Count == 0) return target;
			if (target.Count == 0) return Knuth_ShuffleListAppendedInto(source, target);
			if (target.Count < source.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source list.", "target");
			for (int i = 0; i < source.Count; ++i)
			{
				int j = _random.Range().Closed(i);
				if (i != j)
				{
					target[i] = target[j];
				}
				target[j] = source[i];
			}
			return target;
		}

		private IList<T> Knuth_ShuffleListAppendedInto<T>(IList<T> source, IList<T> target)
		{
			for (int i = 0; i < source.Count; ++i)
			{
				int j = _random.Range().Closed(i);
				if (i != j)
				{
					target.Add(target[j]);
					target[j] = source[i];
				}
				else
				{
					target.Add(source[i]);
				}
			}
			return target;
		}

		private IList<T> Knuth_ShuffleEnumerableInto<T>(IEnumerable<T> source, IList<T> target)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			if (target.Count == 0) return Knuth_ShuffleEnumerableAppendedInto(enumerator, target);

			int i = 0;
			target[i] = enumerator.Current;

			while (enumerator.MoveNext())
			{
				++i;
				if (i >= target.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source enumerable.", "target");
				int j = _random.Range().Closed(i);
				if (i != j)
				{
					target[i] = target[j];
				}
				target[j] = enumerator.Current;
			}

			return target;
		}

		private IList<T> Knuth_ShuffleEnumerableAppendedInto<T>(IEnumerable<T> source, IList<T> target)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			return Knuth_ShuffleEnumerableAppendedInto(enumerator, target);
		}

		private IList<T> Knuth_ShuffleEnumerableAppendedInto<T>(IEnumerator<T> enumerator, IList<T> target)
		{
			int i = 0;
			target.Add(enumerator.Current);

			while (enumerator.MoveNext())
			{
				++i;
				int j = _random.Range().Closed(i);
				if (i != j)
				{
					target.Add(target[j]);
					target[j] = enumerator.Current;
				}
				else
				{
					target.Add(enumerator.Current);
				}
			}

			return target;
		}

		#endregion

		#region Cyclic Shuffle (Sattolo's Algorithm)

		private T[] Sattolo_ShuffleArray<T>(T[] array)
		{
			for (int i = array.Length - 1; i > 0; --i)
			{
				Utility.Swap(ref array[i], ref array[_random.Range().Closed(i)]);
			}
			return array;
		}

		private IList<T> Sattolo_ShuffleList<T>(IList<T> list)
		{
			for (int i = list.Count - 1; i > 0; --i)
			{
				int j = _random.Range().Closed(i);
				T temp = list[i];
				list[i] = list[j];
				list[j] = temp;
			}
			return list;
		}

		private IList<T> Sattolo_ShuffleListInto<T>(IList<T> source, IList<T> target)
		{
			if (source.Count == 0) return target;
			if (target.Count == 0) return Sattolo_ShuffleListAppendedInto(source, target);
			if (target.Count < source.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source list.", "target");
			target[0] = source[0];
			for (int i = 1; i < source.Count; ++i)
			{
				int j = _random.Range().Closed(i);
				target[i] = target[j];
				target[j] = source[i];
			}
			return target;
		}

		private IList<T> Sattolo_ShuffleListAppendedInto<T>(IList<T> source, IList<T> target)
		{
			target.Add(source[0]);
			for (int i = 1; i < source.Count; ++i)
			{
				int j = _random.Range().Closed(i);
				target.Add(target[j]);
				target[j] = source[i];
			}
			return target;
		}

		private IList<T> Sattolo_ShuffleEnumerableInto<T>(IEnumerable<T> source, IList<T> target)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			if (target.Count == 0) return Sattolo_ShuffleEnumerableAppendedInto(enumerator, target);

			int i = 0;
			target[i] = enumerator.Current;

			while (enumerator.MoveNext())
			{
				++i;
				if (i >= target.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source enumerable.", "target");
				int j = _random.Range().Closed(i);
				target[i] = target[j];
				target[j] = enumerator.Current;
			}

			return target;
		}

		private IList<T> Sattolo_ShuffleEnumerableAppendedInto<T>(IEnumerable<T> source, IList<T> target)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			return Sattolo_ShuffleEnumerableAppendedInto(enumerator, target);
		}

		private IList<T> Sattolo_ShuffleEnumerableAppendedInto<T>(IEnumerator<T> enumerator, IList<T> target)
		{
			target.Add(enumerator.Current);

			while (enumerator.MoveNext())
			{
				int j = _random.Range().Closed(target.Count);
				target.Add(target[j]);
				target[j] = enumerator.Current;
			}

			return target;
		}

		#endregion
	}

	public static class ShuffleExtensions
	{
		public static IList<T> Shuffle<T>(this IList<T> list, IRandomEngine random)
		{
			return random.Shuffle().InPlace(list);
		}

		public static IList<T> ShuffleInto<T>(this IEnumerable<T> source, IList<T> target, IRandomEngine random)
		{
			return random.Shuffle().Into(source, target);
		}

		public static IList<T> ShuffleCyclic<T>(this IList<T> list, IRandomEngine random)
		{
			return random.Shuffle().InPlaceCyclic(list);
		}

		public static IList<T> ShuffleIntoCyclic<T>(this IEnumerable<T> source, IList<T> target, IRandomEngine random)
		{
			return random.Shuffle().IntoCyclic(source, target);
		}

		public static Shuffle Shuffle(this IRandomEngine random)
		{
			return new Shuffle(random);
		}
	}
}
