/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace MakeIt.Random
{
	/// <summary>
	/// A static class of extension methods for shuffling sequences of elements.
	/// </summary>
	public static class RandomShuffle
	{
		#region Public Interface

		/// <summary>
		/// Randomly shuffles in place all the elements in the <paramref name="list"/> provided.
		/// </summary>
		/// <typeparam name="T">The type of the elements to be shuffled in place.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits to determine the shuffle sequence.</param>
		/// <param name="list">The list of elements to be shuffled.</param>
		/// <param name="forceMoveAll">If true, no element is allowed to remain in its original location, unless there is only one element.</param>
		/// <returns>A reference to the shuffled list.</returns>
		/// <remarks>
		/// Uses the <see href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm">Knuth shuffle</see> algorithm.
		/// If <paramref name="forceMoveAll"/> is true, then adapts the algorithm to become
		/// <see href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#Sattolo.27s_algorithm">Sattolo's shuffle</see> algorithm.
		/// </remarks>
		public static IList<T> Shuffle<T>(this IRandom random, IList<T> list, bool forceMoveAll = false)
		{
			T[] array = list as T[];
			if (array != null)
			{
				if (!forceMoveAll)
				{
					Knuth_ShuffleArray(array, random);
				}
				else
				{
					Sattolo_ShuffleArray(array, random);
				}
			}
			else
			{
				if (!forceMoveAll)
				{
					Knuth_ShuffleList(list, random);
				}
				else
				{
					Sattolo_ShuffleList(list, random);
				}
			}
			return list;
		}

		/// <summary>
		/// Randomly shuffles all the elements in <paramref name="source"/>, placing them in shuffled order into <paramref name="target"/>.
		/// The elements in <paramref name="source"/> keep their original order.
		/// </summary>
		/// <typeparam name="T">The type of the elements to be shuffled.</typeparam>
		/// <param name="random">The pseudo-random engine that will be used to generate bits to determine the shuffle sequence.</param>
		/// <param name="source">The enumerable sequence of elements to be shuffled.</param>
		/// <param name="target">The list into which the shuffled elements will be place.</param>
		/// <param name="forceMoveAll">If true, no element is allowed to remain in its original location, unless there is only one element.</param>
		/// <returns>A reference to the shuffled list.</returns>
		/// <remarks>
		/// Uses the <see href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_.22inside-out.22_algorithm">Knuth shuffle</see> algorithm.
		/// If <paramref name="forceMoveAll"/> is true, then adapts the algorithm to become
		/// <see href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#Sattolo.27s_algorithm">Sattolo's shuffle</see> algorithm.
		/// </remarks>
		public static IList<T> ShuffleInto<T>(this IRandom random, IEnumerable<T> source, IList<T> target, bool forceMoveAll = false)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				if (!forceMoveAll)
				{
					Knuth_ShuffleListInto(list, target, random);
				}
				else
				{
					Sattolo_ShuffleListInto(list, target, random);
				}
			}
			else
			{
				if (!forceMoveAll)
				{
					Knuth_ShuffleEnumerableInto(source, target, random);
				}
				else
				{
					Sattolo_ShuffleEnumerableInto(source, target, random);
				}
			}
			return target;
		}

		#endregion

		#region Container Extensions

		/// <summary>
		/// Randomly shuffles in place all the elements in the <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of the elements to be shuffled.</typeparam>
		/// <param name="list">The list of elements to be shuffled in place.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits to determine the shuffle sequence.</param>
		/// <param name="forceMoveAll">If true, no element is allowed to remain in its original location, unless there is only one element.</param>
		/// <returns>A reference to the shuffled list.</returns>
		/// <remarks>
		/// Uses the <see href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm">Knuth shuffle</see> algorithm.
		/// If <paramref name="forceMoveAll"/> is true, then adapts the algorithm to become
		/// <see href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#Sattolo.27s_algorithm">Sattolo's shuffle</see> algorithm.
		/// </remarks>
		public static IList<T> Shuffle<T>(this IList<T> list, IRandom random, bool forceMoveAll = false)
		{
			return random.Shuffle(list, forceMoveAll);
		}

		/// <summary>
		/// Randomly shuffles all the elements in <paramref name="source"/>, placing them in shuffled order into <paramref name="target"/>.
		/// The elements in <paramref name="source"/> keep their original order.
		/// </summary>
		/// <typeparam name="T">The type of the elements to be shuffled.</typeparam>
		/// <param name="source">The enumerable sequence of elements to be shuffled.</param>
		/// <param name="target">The list into which the shuffled elements will be place.</param>
		/// <param name="random">The pseudo-random engine that will be used to generate bits to determine the shuffle sequence.</param>
		/// <param name="forceMoveAll">If true, no element is allowed to remain in its original location, unless there is only one element.</param>
		/// <returns>A reference to the shuffled list.</returns>
		/// <remarks>
		/// Uses the <see href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_.22inside-out.22_algorithm">Knuth shuffle</see> algorithm.
		/// If <paramref name="forceMoveAll"/> is true, then adapts the algorithm to become
		/// <see href="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#Sattolo.27s_algorithm">Sattolo's shuffle</see> algorithm.
		/// </remarks>
		public static IList<T> ShuffleInto<T>(this IEnumerable<T> source, IList<T> target, IRandom random, bool forceMoveAll = false)
		{
			return random.ShuffleInto(source, target, forceMoveAll);
		}

		#endregion

		#region Standard Shuffle (Knuth, Fisher-Yates Shuffle)

		private static T[] Knuth_ShuffleArray<T>(T[] array, IRandom random)
		{
			for (int i = array.Length - 1; i > 0; --i)
			{
				int j = random.RangeCC(i);
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
				int j = random.RangeCC(i);
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
				int j = random.RangeCC(i);
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
				int j = random.RangeCC(i);
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
				int j = random.RangeCC(i);
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
				int j = random.RangeCC(i);
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

		#region Force Move All Shuffle (Sattolo's Algorithm)

		private static T[] Sattolo_ShuffleArray<T>(T[] array, IRandom random)
		{
			for (int i = array.Length - 1; i > 0; --i)
			{
				int j = random.RangeCO(i);
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
				int j = random.RangeCO(i);
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
				int j = random.RangeCO(i);
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
				int j = random.RangeCO(i);
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
				int j = random.RangeCO(i);
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
				int j = random.RangeCO(target.Count);
				target.Add(target[j]);
				target[j] = enumerator.Current;
			}

			return target;
		}

		#endregion
	}
}
