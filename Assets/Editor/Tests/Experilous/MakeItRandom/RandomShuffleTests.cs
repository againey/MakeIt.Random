/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using NUnit.Framework;
using System.Collections.Generic;

namespace Experilous.MakeIt.Random.Tests
{
	class RandomShuffleTests
	{
		private const string seed = "random seed";

		private static int[] CreateLinearArray(int count)
		{
			int[] array = new int[count];
			for (int i = 0; i < count; ++i)
			{
				array[i] = i;
			}
			return array;
		}

		private static List<int> CreateLinearList(int count)
		{
			List<int> list = new List<int>(count);
			for (int i = 0; i < count; ++i)
			{
				list.Add(i);
			}
			return list;
		}

		private static LinkedList<int> CreateLinearLinkedList(int count)
		{
			LinkedList<int> list = new LinkedList<int>();
			for (int i = 0; i < count; ++i)
			{
				list.AddLast(i);
			}
			return list;
		}

		private static void ValidateItemExistenceAndUniqueness(IList<int> list)
		{
			for (int i = 0; i < list.Count; ++i)
			{
				int count = 0;
				for (int j = 0; j < list.Count; ++j)
				{
					if (list[j] == i)
					{
						++count;
					}
				}
				Assert.AreEqual(1, count);
			}
		}

		private static void ValidateShuffleDistributesUniformly(int bucketCount, int iterations, float tolerance, System.Func<IRandom, IList<int>> shuffle)
		{
			var hitsPerBucket = (bucketCount / 2) * iterations;

			int[] sumArray = new int[bucketCount];
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < iterations; ++i)
			{
				IList<int> list = shuffle(random);

				for (int j = 0; j < list.Count; ++j)
				{
					sumArray[j] += list[j];
				}
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(sumArray, hitsPerBucket), tolerance * hitsPerBucket);
		}

		private static void ValidateIsCycle(IList<int> list)
		{
			int count = 1;
			int index = list[0];
			while (count < list.Count && index != 0)
			{
				index = list[index];
				++count;
			}
			Assert.AreEqual(list.Count, count);
		}

		[Test]
		public void ShuffleArrayInPlaceLosesDupesNoElements()
		{
			int[] array = CreateLinearArray(100);
			array.Shuffle(XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(array);
		}

		[Test]
		public void ShuffleListInPlaceLosesDupesNoElements()
		{
			List<int> list = CreateLinearList(100);
			list.Shuffle(XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(list);
		}

		[Test]
		public void ShuffleArrayCopyLosesDupesNoElements()
		{
			int[] array = CreateLinearArray(100);
			int[] shuffledArray = (int[])array.ShuffleInto(new int[100], XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledArray);
		}

		[Test]
		public void ShuffleListCopyLosesDupesNoElements()
		{
			List<int> list = CreateLinearList(100);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[100], XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledArray);
		}

		[Test]
		public void ShuffleLinkedListCopyLosesDupesNoElements()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[100], XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledArray);
		}

		[Test]
		public void ShuffleArrayCopyAppendLosesDupesNoElements()
		{
			int[] array = CreateLinearArray(100);
			List<int> shuffledList = (List<int>)array.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledList);
		}

		[Test]
		public void ShuffleListCopyAppendLosesDupesNoElements()
		{
			List<int> list = CreateLinearList(100);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledList);
		}

		[Test]
		public void ShuffleLinkedListCopyAppendLosesDupesNoElements()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledList);
		}

		[Test]
		public void ShuffleArrayInPlaceDistributesUniformly()
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearArray(101).Shuffle(random));
		}

		[Test]
		public void ShuffleListInPlaceDistributesUniformly()
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearList(101).Shuffle(random));
		}

		[Test]
		public void ShuffleArrayCopyDistributesUniformly()
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearArray(101).ShuffleInto(new int[101], random));
		}

		[Test]
		public void ShuffleListCopyDistributesUniformly()
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearList(101).ShuffleInto(new int[101], random));
		}

		[Test]
		public void ShuffleLinkedListCopyDistributesUniformly()
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearLinkedList(101).ShuffleInto(new int[101], random));
		}

		[Test]
		public void ShuffleArrayCopyAppendDistributesUniformly()
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearArray(101).ShuffleInto(new List<int>(101), random));
		}

		[Test]
		public void ShuffleListCopyAppendDistributesUniformly()
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearList(101).ShuffleInto(new List<int>(101), random));
		}

		[Test]
		public void ShuffleLinkedListCopyAppendDistributesUniformly()
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearLinkedList(101).ShuffleInto(new List<int>(101), random));
		}

		[Test]
		public void CycleShuffleArrayInPlaceMaintainsCycle()
		{
			int[] array = CreateLinearArray(100);
			array.ShuffleCyclic(XorShift128Plus.Create(seed));
			ValidateIsCycle(array);
		}

		[Test]
		public void CycleShuffleListInPlaceMaintainsCycle()
		{
			List<int> list = CreateLinearList(100);
			list.ShuffleCyclic(XorShift128Plus.Create(seed));
			ValidateIsCycle(list);
		}

		[Test]
		public void CycleShuffleArrayCopyMaintainsCycle()
		{
			int[] array = CreateLinearArray(100);
			int[] shuffledArray = (int[])array.ShuffleCyclicInto(new int[100], XorShift128Plus.Create(seed));
			ValidateIsCycle(shuffledArray);
		}

		[Test]
		public void CycleShuffleListCopyMaintainsCycle()
		{
			List<int> list = CreateLinearList(100);
			int[] shuffledArray = (int[])list.ShuffleCyclicInto(new int[100], XorShift128Plus.Create(seed));
			ValidateIsCycle(shuffledArray);
		}

		[Test]
		public void CycleShuffleLinkedListCopyMaintainsCycle()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			int[] shuffledArray = (int[])list.ShuffleCyclicInto(new int[100], XorShift128Plus.Create(seed));
			ValidateIsCycle(shuffledArray);
		}

		[Test]
		public void CycleShuffleArrayCopyAppendMaintainsCycle()
		{
			int[] array = CreateLinearArray(100);
			List<int> shuffledList = (List<int>)array.ShuffleCyclicInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateIsCycle(shuffledList);
		}

		[Test]
		public void CycleShuffleListCopyAppendMaintainsCycle()
		{
			List<int> list = CreateLinearList(100);
			List<int> shuffledList = (List<int>)list.ShuffleCyclicInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateIsCycle(shuffledList);
		}

		[Test]
		public void CycleShuffleLinkedListCopyAppendMaintainsCycle()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			List<int> shuffledList = (List<int>)list.ShuffleCyclicInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateIsCycle(shuffledList);
		}

		[Test]
		public void ShuffleArrayCopyThrowsForTooSmallTarget()
		{
			int[] array = CreateLinearArray(100);
			Assert.Throws<System.ArgumentException>(() => array.ShuffleInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[Test]
		public void ShuffleListCopyThrowsForTooSmallTarget()
		{
			List<int> list = CreateLinearList(100);
			Assert.Throws<System.ArgumentException>(() => list.ShuffleInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[Test]
		public void ShuffleLinkedListCopyThrowsForTooSmallTarget()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			Assert.Throws<System.ArgumentException>(() => list.ShuffleInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[Test]
		public void CycleShuffleArrayCopyThrowsForTooSmallTarget()
		{
			int[] array = CreateLinearArray(100);
			Assert.Throws<System.ArgumentException>(() => array.ShuffleCyclicInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[Test]
		public void CycleShuffleListCopyThrowsForTooSmallTarget()
		{
			List<int> list = CreateLinearList(100);
			Assert.Throws<System.ArgumentException>(() => list.ShuffleCyclicInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[Test]
		public void CycleShuffleLinkedListCopyThrowsForTooSmallTarget()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			Assert.Throws<System.ArgumentException>(() => list.ShuffleCyclicInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[Test]
		public void ShuffleArrayInPlaceEmptyCollection()
		{
			int[] array = CreateLinearArray(0);
			array.Shuffle(XorShift128Plus.Create(seed));
			Assert.IsNotNull(array);
			Assert.AreEqual(0, array.Length);
		}

		[Test]
		public void ShuffleListInPlaceEmptyCollection()
		{
			List<int> list = CreateLinearList(0);
			list.Shuffle(XorShift128Plus.Create(seed));
			Assert.IsNotNull(list);
			Assert.AreEqual(0, list.Count);
		}

		[Test]
		public void ShuffleArrayCopyEmptyCollection()
		{
			int[] array = CreateLinearArray(0);
			int[] shuffledArray = (int[])array.ShuffleInto(new int[0], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(0, shuffledArray.Length);
		}

		[Test]
		public void ShuffleListCopyEmptyCollection()
		{
			List<int> list = CreateLinearList(0);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[0], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(0, shuffledArray.Length);
		}

		[Test]
		public void ShuffleLinkedListCopyEmptyCollection()
		{
			LinkedList<int> list = CreateLinearLinkedList(0);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[0], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(0, shuffledArray.Length);
		}

		[Test]
		public void ShuffleArrayCopyAppendEmptyCollection()
		{
			int[] array = CreateLinearArray(0);
			List<int> shuffledList = (List<int>)array.ShuffleInto(new List<int>(0), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(0, shuffledList.Count);
		}

		[Test]
		public void ShuffleListCopyAppendEmptyCollection()
		{
			List<int> list = CreateLinearList(0);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(0), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(0, shuffledList.Count);
		}

		[Test]
		public void ShuffleLinkedListCopyAppendEmptyCollection()
		{
			LinkedList<int> list = CreateLinearLinkedList(0);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(0), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(0, shuffledList.Count);
		}

		[Test]
		public void ShuffleArrayInPlaceOneItemCollection()
		{
			int[] array = CreateLinearArray(1);
			array.Shuffle(XorShift128Plus.Create(seed));
			Assert.IsNotNull(array);
			Assert.AreEqual(1, array.Length);
			Assert.AreEqual(0, array[0]);
		}

		[Test]
		public void ShuffleListInPlaceOneItemCollection()
		{
			List<int> list = CreateLinearList(1);
			list.Shuffle(XorShift128Plus.Create(seed));
			Assert.IsNotNull(list);
			Assert.AreEqual(1, list.Count);
			Assert.AreEqual(0, list[0]);
		}

		[Test]
		public void ShuffleArrayCopyOneItemCollection()
		{
			int[] array = CreateLinearArray(1);
			int[] shuffledArray = (int[])array.ShuffleInto(new int[1], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(1, shuffledArray.Length);
			Assert.AreEqual(0, shuffledArray[0]);
		}

		[Test]
		public void ShuffleListCopyOneItemCollection()
		{
			List<int> list = CreateLinearList(1);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[1], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(1, shuffledArray.Length);
			Assert.AreEqual(0, shuffledArray[0]);
		}

		[Test]
		public void ShuffleLinkedListCopyOneItemCollection()
		{
			LinkedList<int> list = CreateLinearLinkedList(1);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[1], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(1, shuffledArray.Length);
			Assert.AreEqual(0, shuffledArray[0]);
		}

		[Test]
		public void ShuffleArrayCopyAppendOneItemCollection()
		{
			int[] array = CreateLinearArray(1);
			List<int> shuffledList = (List<int>)array.ShuffleInto(new List<int>(1), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(1, shuffledList.Count);
			Assert.AreEqual(0, shuffledList[0]);
		}

		[Test]
		public void ShuffleListCopyAppendOneItemCollection()
		{
			List<int> list = CreateLinearList(1);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(1), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(1, shuffledList.Count);
			Assert.AreEqual(0, shuffledList[0]);
		}

		[Test]
		public void ShuffleLinkedListCopyAppendOneItemCollection()
		{
			LinkedList<int> list = CreateLinearLinkedList(1);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(1), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(1, shuffledList.Count);
			Assert.AreEqual(0, shuffledList[0]);
		}
	}
}
#endif
