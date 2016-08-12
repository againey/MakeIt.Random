/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeIt.Random
{
	public static class RandomShuffle
	{
		#region Public Interface

		public static IList<T> Shuffle<T>(this IRandom random, IList<T> list)
		{
			T[] array = list as T[];
			if (array != null)
			{
				Knuth_ShuffleArray(array, random);
			}
			else
			{
				Knuth_ShuffleList(list, random);
			}
			return list;
		}

		public static IList<T> ShuffleInto<T>(this IRandom random, IEnumerable<T> source, IList<T> target)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				Knuth_ShuffleListInto(list, target, random);
			}
			else
			{
				Knuth_ShuffleEnumerableInto(source, target, random);
			}
			return target;
		}

		public static IList<T> ShuffleCyclic<T>(this IRandom random, IList<T> list)
		{
			T[] array = list as T[];
			if (array != null)
			{
				Sattolo_ShuffleArray(array, random);
			}
			else
			{
				Sattolo_ShuffleList(list, random);
			}
			return list;
		}

		public static IList<T> ShuffleCyclicInto<T>(this IRandom random, IEnumerable<T> source, IList<T> target)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				Sattolo_ShuffleListInto(list, target, random);
			}
			else
			{
				Sattolo_ShuffleEnumerableInto(source, target, random);
			}
			return target;
		}

		#endregion

		#region Container Extensions

		public static IList<T> Shuffle<T>(this IList<T> list, IRandom random)
		{
			return random.Shuffle(list);
		}

		public static IList<T> ShuffleInto<T>(this IEnumerable<T> source, IList<T> target, IRandom random)
		{
			return random.ShuffleInto(source, target);
		}

		public static IList<T> ShuffleCyclic<T>(this IList<T> list, IRandom random)
		{
			return random.ShuffleCyclic(list);
		}

		public static IList<T> ShuffleCyclicInto<T>(this IEnumerable<T> source, IList<T> target, IRandom random)
		{
			return random.ShuffleCyclicInto(source, target);
		}

		#endregion

		#region Standard Shuffle (Knuth, Fisher-Yates Shuffle)

		private static T[] Knuth_ShuffleArray<T>(T[] array, IRandom random)
		{
			for (int i = array.Length - 1; i > 0; --i)
			{
				int j = random.ClosedRange(i);
				T swap = array[i];
				array[i] = array[j];
				array[j] = swap;
			}
			return array;
		}

		private static IList<T> Knuth_ShuffleList<T>(IList<T> list, IRandom random)
		{
			for (int i = list.Count - 1; i > 0; --i)
			{
				int j = random.ClosedRange(i);
				T swap = list[i];
				list[i] = list[j];
				list[j] = swap;
			}
			return list;
		}

		private static IList<T> Knuth_ShuffleListInto<T>(IList<T> source, IList<T> target, IRandom random)
		{
			if (source.Count == 0) return target;
			if (target.Count == 0) return Knuth_ShuffleListAppendedInto(source, target, random);
			if (target.Count < source.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source list.", "target");
			for (int i = 0; i < source.Count; ++i)
			{
				int j = random.ClosedRange(i);
				if (i != j)
				{
					target[i] = target[j];
				}
				target[j] = source[i];
			}
			return target;
		}

		private static IList<T> Knuth_ShuffleListAppendedInto<T>(IList<T> source, IList<T> target, IRandom random)
		{
			for (int i = 0; i < source.Count; ++i)
			{
				int j = random.ClosedRange(i);
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

		private static IList<T> Knuth_ShuffleEnumerableInto<T>(IEnumerable<T> source, IList<T> target, IRandom random)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			if (target.Count == 0) return Knuth_ShuffleEnumerableAppendedInto(enumerator, target, random);

			int i = 0;
			target[i] = enumerator.Current;

			while (enumerator.MoveNext())
			{
				++i;
				if (i >= target.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source enumerable.", "target");
				int j = random.ClosedRange(i);
				if (i != j)
				{
					target[i] = target[j];
				}
				target[j] = enumerator.Current;
			}

			return target;
		}

		private static IList<T> Knuth_ShuffleEnumerableAppendedInto<T>(IEnumerable<T> source, IList<T> target, IRandom random)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			return Knuth_ShuffleEnumerableAppendedInto(enumerator, target, random);
		}

		private static IList<T> Knuth_ShuffleEnumerableAppendedInto<T>(IEnumerator<T> enumerator, IList<T> target, IRandom random)
		{
			int i = 0;
			target.Add(enumerator.Current);

			while (enumerator.MoveNext())
			{
				++i;
				int j = random.ClosedRange(i);
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

		private static T[] Sattolo_ShuffleArray<T>(T[] array, IRandom random)
		{
			for (int i = array.Length - 1; i > 0; --i)
			{
				int j = random.HalfOpenRange(i);
				T swap = array[i];
				array[i] = array[j];
				array[j] = swap;
			}
			return array;
		}

		private static IList<T> Sattolo_ShuffleList<T>(IList<T> list, IRandom random)
		{
			for (int i = list.Count - 1; i > 0; --i)
			{
				int j = random.HalfOpenRange(i);
				T swap = list[i];
				list[i] = list[j];
				list[j] = swap;
			}
			return list;
		}

		private static IList<T> Sattolo_ShuffleListInto<T>(IList<T> source, IList<T> target, IRandom random)
		{
			if (source.Count == 0) return target;
			if (target.Count == 0) return Sattolo_ShuffleListAppendedInto(source, target, random);
			if (target.Count < source.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source list.", "target");
			target[0] = source[0];
			for (int i = 1; i < source.Count; ++i)
			{
				int j = random.HalfOpenRange(i);
				target[i] = target[j];
				target[j] = source[i];
			}
			return target;
		}

		private static IList<T> Sattolo_ShuffleListAppendedInto<T>(IList<T> source, IList<T> target, IRandom random)
		{
			target.Add(source[0]);
			for (int i = 1; i < source.Count; ++i)
			{
				int j = random.HalfOpenRange(i);
				target.Add(target[j]);
				target[j] = source[i];
			}
			return target;
		}

		private static IList<T> Sattolo_ShuffleEnumerableInto<T>(IEnumerable<T> source, IList<T> target, IRandom random)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			if (target.Count == 0) return Sattolo_ShuffleEnumerableAppendedInto(enumerator, target, random);

			int i = 0;
			target[i] = enumerator.Current;

			while (enumerator.MoveNext())
			{
				++i;
				if (i >= target.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source enumerable.", "target");
				int j = random.HalfOpenRange(i);
				target[i] = target[j];
				target[j] = enumerator.Current;
			}

			return target;
		}

		private static IList<T> Sattolo_ShuffleEnumerableAppendedInto<T>(IEnumerable<T> source, IList<T> target, IRandom random)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			return Sattolo_ShuffleEnumerableAppendedInto(enumerator, target, random);
		}

		private static IList<T> Sattolo_ShuffleEnumerableAppendedInto<T>(IEnumerator<T> enumerator, IList<T> target, IRandom random)
		{
			target.Add(enumerator.Current);

			while (enumerator.MoveNext())
			{
				int j = random.HalfOpenRange(target.Count);
				target.Add(target[j]);
				target[j] = enumerator.Current;
			}

			return target;
		}

		#endregion
	}
}
