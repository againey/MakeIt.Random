/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.Randomization
{
	public static class Shuffle
	{
		#region Public Interface

		public static IList<T> InPlace<T>(IList<T> list)
		{
			return InPlace(list, DefaultRandomEngine.sharedInstance);
		}

		public static IList<T> InPlace<T>(IList<T> list, IRandomEngine engine)
		{
			T[] array = list as T[];
			if (array != null)
			{
				Knuth_ShuffleArray(array, engine);
			}
			else
			{
				Knuth_ShuffleList(list, engine);
			}
			return list;
		}

		public static IList<T> Into<T>(IEnumerable<T> source, IList<T> target)
		{
			return Into(source, target, DefaultRandomEngine.sharedInstance);
		}

		public static IList<T> Into<T>(IEnumerable<T> source, IList<T> target, IRandomEngine engine)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				Knuth_ShuffleListInto(list, target, engine);
			}
			else
			{
				Knuth_ShuffleEnumerableInto(source, target, engine);
			}
			return target;
		}

		#endregion

		#region Standard Shuffle (Knuth, Fisher-Yates Shuffle)

		private static T[] Knuth_ShuffleArray<T>(T[] array, IRandomEngine engine)
		{
			for (int i = array.Length - 1; i > 0; --i)
			{
				Utility.Swap(ref array[i], ref array[engine.NextLessThanOrEqual((uint)i)]);
			}
			return array;
		}

		private static IList<T> Knuth_ShuffleList<T>(IList<T> list, IRandomEngine engine)
		{
			for (int i = list.Count - 1; i > 0; --i)
			{
				int j = (int)engine.NextLessThanOrEqual((uint)i);
				T temp = list[i];
				list[i] = list[j];
				list[j] = temp;
			}
			return list;
		}

		private static IList<T> Knuth_ShuffleListInto<T>(IList<T> source, IList<T> target, IRandomEngine engine)
		{
			if (source.Count == 0) return target;
			if (target.Count == 0) return Knuth_ShuffleListAppendedInto(source, target, engine);
			if (target.Count < source.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source list.", "target");
			for (int i = 0; i < source.Count; ++i)
			{
				int j = (int)engine.NextLessThanOrEqual((uint)i);
				if (i != j)
				{
					target[i] = target[j];
				}
				target[j] = source[i];
			}
			return target;
		}

		private static IList<T> Knuth_ShuffleListAppendedInto<T>(IList<T> source, IList<T> target, IRandomEngine engine)
		{
			for (int i = 0; i < source.Count; ++i)
			{
				int j = (int)engine.NextLessThanOrEqual((uint)i);
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

		private static IList<T> Knuth_ShuffleEnumerableInto<T>(IEnumerable<T> source, IList<T> target, IRandomEngine engine)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			if (target.Count == 0) return Knuth_ShuffleEnumerableAppendedInto(enumerator, target, engine);

			int i = 0;
			target[i] = enumerator.Current;

			while (enumerator.MoveNext())
			{
				++i;
				if (i >= target.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source enumerable.", "target");
				int j = (int)engine.NextLessThanOrEqual((uint)i);
				if (i != j)
				{
					target[i] = target[j];
				}
				target[j] = enumerator.Current;
			}

			return target;
		}

		private static IList<T> Knuth_ShuffleEnumerableAppendedInto<T>(IEnumerable<T> source, IList<T> target, IRandomEngine engine)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			return Knuth_ShuffleEnumerableAppendedInto(enumerator, target, engine);
		}

		private static IList<T> Knuth_ShuffleEnumerableAppendedInto<T>(IEnumerator<T> enumerator, IList<T> target, IRandomEngine engine)
		{
			int i = 0;
			target.Add(enumerator.Current);

			while (enumerator.MoveNext())
			{
				++i;
				int j = (int)engine.NextLessThanOrEqual((uint)i);
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
	}

	public static class ShuffleCyclic
	{
		#region Public Interface

		public static IList<T> InPlace<T>(IList<T> list)
		{
			return InPlace(list, DefaultRandomEngine.sharedInstance);
		}

		public static IList<T> InPlace<T>(IList<T> list, IRandomEngine engine)
		{
			T[] array = list as T[];
			if (array != null)
			{
				Sattolo_ShuffleArray(array, engine);
			}
			else
			{
				Sattolo_ShuffleList(list, engine);
			}
			return list;
		}

		public static IList<T> Into<T>(IEnumerable<T> source, IList<T> target)
		{
			return Into(source, target, DefaultRandomEngine.sharedInstance);
		}

		public static IList<T> Into<T>(IEnumerable<T> source, IList<T> target, IRandomEngine engine)
		{
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				Sattolo_ShuffleListInto(list, target, engine);
			}
			else
			{
				Sattolo_ShuffleEnumerableInto(source, target, engine);
			}
			return target;
		}

		#endregion

		#region Cyclic Shuffle (Sattolo's Algorithm)

		private static T[] Sattolo_ShuffleArray<T>(T[] array, IRandomEngine engine)
		{
			for (int i = array.Length - 1; i > 0; --i)
			{
				Utility.Swap(ref array[i], ref array[engine.NextLessThan((uint)i)]);
			}
			return array;
		}

		private static IList<T> Sattolo_ShuffleList<T>(IList<T> list, IRandomEngine engine)
		{
			for (int i = list.Count - 1; i > 0; --i)
			{
				int j = (int)engine.NextLessThan((uint)i);
				T temp = list[i];
				list[i] = list[j];
				list[j] = temp;
			}
			return list;
		}

		private static IList<T> Sattolo_ShuffleListInto<T>(IList<T> source, IList<T> target, IRandomEngine engine)
		{
			if (source.Count == 0) return target;
			if (target.Count == 0) return Sattolo_ShuffleListAppendedInto(source, target, engine);
			if (target.Count < source.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source list.", "target");
			target[0] = source[0];
			for (int i = 1; i < source.Count; ++i)
			{
				int j = (int)engine.NextLessThan((uint)i);
				target[i] = target[j];
				target[j] = source[i];
			}
			return target;
		}

		private static IList<T> Sattolo_ShuffleListAppendedInto<T>(IList<T> source, IList<T> target, IRandomEngine engine)
		{
			target.Add(source[0]);
			for (int i = 1; i < source.Count; ++i)
			{
				int j = (int)engine.NextLessThan((uint)i);
				target.Add(target[j]);
				target[j] = source[i];
			}
			return target;
		}

		private static IList<T> Sattolo_ShuffleEnumerableInto<T>(IEnumerable<T> source, IList<T> target, IRandomEngine engine)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			if (target.Count == 0) return Sattolo_ShuffleEnumerableAppendedInto(enumerator, target, engine);

			int i = 0;
			target[i] = enumerator.Current;

			while (enumerator.MoveNext())
			{
				++i;
				if (i >= target.Count) throw new System.ArgumentException("The target list must either be empty or be at least as large as the source enumerable.", "target");
				int j = (int)engine.NextLessThan((uint)i);
				target[i] = target[j];
				target[j] = enumerator.Current;
			}

			return target;
		}

		private static IList<T> Sattolo_ShuffleEnumerableAppendedInto<T>(IEnumerable<T> source, IList<T> target, IRandomEngine engine)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext() == false) return target;
			return Sattolo_ShuffleEnumerableAppendedInto(enumerator, target, engine);
		}

		private static IList<T> Sattolo_ShuffleEnumerableAppendedInto<T>(IEnumerator<T> enumerator, IList<T> target, IRandomEngine engine)
		{
			target.Add(enumerator.Current);

			while (enumerator.MoveNext())
			{
				int j = (int)engine.NextLessThan((uint)target.Count);
				target.Add(target[j]);
				target[j] = enumerator.Current;
			}

			return target;
		}

		#endregion
	}

	public static class ShuffleExtensions
	{
		public static IList<T> Shuffle<T>(this IList<T> list)
		{
			return Randomization.Shuffle.InPlace(list, DefaultRandomEngine.sharedInstance);
		}

		public static IList<T> Shuffle<T>(this IList<T> list, IRandomEngine engine)
		{
			return Randomization.Shuffle.InPlace(list, engine);
		}

		public static IList<T> ShuffleInto<T>(this IEnumerable<T> source, IList<T> target)
		{
			return Randomization.Shuffle.Into(source, target, DefaultRandomEngine.sharedInstance);
		}

		public static IList<T> ShuffleInto<T>(this IEnumerable<T> source, IList<T> target, IRandomEngine engine)
		{
			return Randomization.Shuffle.Into(source, target, engine);
		}

		public static IList<T> ShuffleCyclic<T>(this IList<T> list)
		{
			return Randomization.ShuffleCyclic.InPlace(list, DefaultRandomEngine.sharedInstance);
		}

		public static IList<T> ShuffleCyclic<T>(this IList<T> list, IRandomEngine engine)
		{
			return Randomization.ShuffleCyclic.InPlace(list, engine);
		}

		public static IList<T> ShuffleCyclicInto<T>(this IEnumerable<T> source, IList<T> target)
		{
			return Randomization.ShuffleCyclic.Into(source, target, DefaultRandomEngine.sharedInstance);
		}

		public static IList<T> ShuffleCyclicInto<T>(this IEnumerable<T> source, IList<T> target, IRandomEngine engine)
		{
			return Randomization.ShuffleCyclic.Into(source, target, engine);
		}
	}
}
