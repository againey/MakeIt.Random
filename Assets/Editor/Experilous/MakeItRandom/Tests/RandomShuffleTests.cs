/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;
using System.Collections.Generic;

namespace Experilous.MakeItRandom.Tests
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

		private static void ValidateShuffleDistributesUniformly(int bucketCount, int iterations, float tolerance, System.Func<IRandom, IList<int>> shuffle, int sampleSizePercentage)
		{
			iterations = (iterations * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

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

		private static void ValidateAllAreMoved(IList<int> list)
		{
			for (int i = 0; i < list.Count; ++i)
			{
				Assert.AreNotEqual(i, list[i]);
			}
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

		[TestCase(Category = "Normal")]
		public void ShuffleArrayInPlaceLosesDupesNoElements()
		{
			int[] array = CreateLinearArray(100);
			array.Shuffle(XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(array);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListInPlaceLosesDupesNoElements()
		{
			List<int> list = CreateLinearList(100);
			list.Shuffle(XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(list);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayCopyLosesDupesNoElements()
		{
			int[] array = CreateLinearArray(100);
			int[] shuffledArray = (int[])array.ShuffleInto(new int[100], XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListCopyLosesDupesNoElements()
		{
			List<int> list = CreateLinearList(100);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[100], XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleLinkedListCopyLosesDupesNoElements()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[100], XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayCopyAppendLosesDupesNoElements()
		{
			int[] array = CreateLinearArray(100);
			List<int> shuffledList = (List<int>)array.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledList);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListCopyAppendLosesDupesNoElements()
		{
			List<int> list = CreateLinearList(100);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledList);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleLinkedListCopyAppendLosesDupesNoElements()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed));
			ValidateItemExistenceAndUniqueness(shuffledList);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ShuffleArrayInPlaceDistributesUniformly(int sampleSizePercentage)
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearArray(101).Shuffle(random), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ShuffleListInPlaceDistributesUniformly(int sampleSizePercentage)
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearList(101).Shuffle(random), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ShuffleArrayCopyDistributesUniformly(int sampleSizePercentage)
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearArray(101).ShuffleInto(new int[101], random), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ShuffleListCopyDistributesUniformly(int sampleSizePercentage)
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearList(101).ShuffleInto(new int[101], random), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ShuffleLinkedListCopyDistributesUniformly(int sampleSizePercentage)
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearLinkedList(101).ShuffleInto(new int[101], random), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ShuffleArrayCopyAppendDistributesUniformly(int sampleSizePercentage)
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearArray(101).ShuffleInto(new List<int>(101), random), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ShuffleListCopyAppendDistributesUniformly(int sampleSizePercentage)
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearList(101).ShuffleInto(new List<int>(101), random), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ShuffleLinkedListCopyAppendDistributesUniformly(int sampleSizePercentage)
		{
			ValidateShuffleDistributesUniformly(101, 2000, 0.02f, (IRandom random) => CreateLinearLinkedList(101).ShuffleInto(new List<int>(101), random), sampleSizePercentage);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllArrayInPlaceMovesAll()
		{
			int[] array = CreateLinearArray(100);
			array.Shuffle(XorShift128Plus.Create(seed), true);
			ValidateAllAreMoved(array);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllListInPlaceMovesAll()
		{
			List<int> list = CreateLinearList(100);
			list.Shuffle(XorShift128Plus.Create(seed), true);
			ValidateAllAreMoved(list);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllArrayCopyMovesAll()
		{
			int[] array = CreateLinearArray(100);
			int[] shuffledArray = (int[])array.ShuffleInto(new int[100], XorShift128Plus.Create(seed), true);
			ValidateAllAreMoved(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllListCopyMovesAll()
		{
			List<int> list = CreateLinearList(100);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[100], XorShift128Plus.Create(seed), true);
			ValidateAllAreMoved(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllLinkedListCopyMovesAll()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[100], XorShift128Plus.Create(seed), true);
			ValidateAllAreMoved(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllArrayCopyAppendMovesAll()
		{
			int[] array = CreateLinearArray(100);
			List<int> shuffledList = (List<int>)array.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed), true);
			ValidateAllAreMoved(shuffledList);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllListCopyAppendMovesAll()
		{
			List<int> list = CreateLinearList(100);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed), true);
			ValidateAllAreMoved(shuffledList);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllLinkedListCopyAppendMovesAll()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed), true);
			ValidateAllAreMoved(shuffledList);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllArrayInPlaceMaintainsCycle()
		{
			int[] array = CreateLinearArray(100);
			array.Shuffle(XorShift128Plus.Create(seed), true);
			ValidateIsCycle(array);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllListInPlaceMaintainsCycle()
		{
			List<int> list = CreateLinearList(100);
			list.Shuffle(XorShift128Plus.Create(seed), true);
			ValidateIsCycle(list);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllArrayCopyMaintainsCycle()
		{
			int[] array = CreateLinearArray(100);
			int[] shuffledArray = (int[])array.ShuffleInto(new int[100], XorShift128Plus.Create(seed), true);
			ValidateIsCycle(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllListCopyMaintainsCycle()
		{
			List<int> list = CreateLinearList(100);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[100], XorShift128Plus.Create(seed), true);
			ValidateIsCycle(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllLinkedListCopyMaintainsCycle()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[100], XorShift128Plus.Create(seed), true);
			ValidateIsCycle(shuffledArray);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllArrayCopyAppendMaintainsCycle()
		{
			int[] array = CreateLinearArray(100);
			List<int> shuffledList = (List<int>)array.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed), true);
			ValidateIsCycle(shuffledList);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllListCopyAppendMaintainsCycle()
		{
			List<int> list = CreateLinearList(100);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed), true);
			ValidateIsCycle(shuffledList);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllLinkedListCopyAppendMaintainsCycle()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(100), XorShift128Plus.Create(seed), true);
			ValidateIsCycle(shuffledList);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayCopyThrowsForTooSmallTarget()
		{
			int[] array = CreateLinearArray(100);
			Assert.Throws<System.ArgumentException>(() => array.ShuffleInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListCopyThrowsForTooSmallTarget()
		{
			List<int> list = CreateLinearList(100);
			Assert.Throws<System.ArgumentException>(() => list.ShuffleInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[TestCase(Category = "Normal")]
		public void ShuffleLinkedListCopyThrowsForTooSmallTarget()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			Assert.Throws<System.ArgumentException>(() => list.ShuffleInto(new int[50], XorShift128Plus.Create(seed)));
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllArrayCopyThrowsForTooSmallTarget()
		{
			int[] array = CreateLinearArray(100);
			Assert.Throws<System.ArgumentException>(() => array.ShuffleInto(new int[50], XorShift128Plus.Create(seed), true));
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllListCopyThrowsForTooSmallTarget()
		{
			List<int> list = CreateLinearList(100);
			Assert.Throws<System.ArgumentException>(() => list.ShuffleInto(new int[50], XorShift128Plus.Create(seed), true));
		}

		[TestCase(Category = "Normal")]
		public void ShuffleMoveAllLinkedListCopyThrowsForTooSmallTarget()
		{
			LinkedList<int> list = CreateLinearLinkedList(100);
			Assert.Throws<System.ArgumentException>(() => list.ShuffleInto(new int[50], XorShift128Plus.Create(seed), true));
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayInPlaceEmptyCollection()
		{
			int[] array = CreateLinearArray(0);
			array.Shuffle(XorShift128Plus.Create(seed));
			Assert.IsNotNull(array);
			Assert.AreEqual(0, array.Length);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListInPlaceEmptyCollection()
		{
			List<int> list = CreateLinearList(0);
			list.Shuffle(XorShift128Plus.Create(seed));
			Assert.IsNotNull(list);
			Assert.AreEqual(0, list.Count);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayCopyEmptyCollection()
		{
			int[] array = CreateLinearArray(0);
			int[] shuffledArray = (int[])array.ShuffleInto(new int[0], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(0, shuffledArray.Length);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListCopyEmptyCollection()
		{
			List<int> list = CreateLinearList(0);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[0], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(0, shuffledArray.Length);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleLinkedListCopyEmptyCollection()
		{
			LinkedList<int> list = CreateLinearLinkedList(0);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[0], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(0, shuffledArray.Length);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayCopyAppendEmptyCollection()
		{
			int[] array = CreateLinearArray(0);
			List<int> shuffledList = (List<int>)array.ShuffleInto(new List<int>(0), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(0, shuffledList.Count);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListCopyAppendEmptyCollection()
		{
			List<int> list = CreateLinearList(0);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(0), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(0, shuffledList.Count);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleLinkedListCopyAppendEmptyCollection()
		{
			LinkedList<int> list = CreateLinearLinkedList(0);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(0), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(0, shuffledList.Count);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayInPlaceOneItemCollection()
		{
			int[] array = CreateLinearArray(1);
			array.Shuffle(XorShift128Plus.Create(seed));
			Assert.IsNotNull(array);
			Assert.AreEqual(1, array.Length);
			Assert.AreEqual(0, array[0]);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListInPlaceOneItemCollection()
		{
			List<int> list = CreateLinearList(1);
			list.Shuffle(XorShift128Plus.Create(seed));
			Assert.IsNotNull(list);
			Assert.AreEqual(1, list.Count);
			Assert.AreEqual(0, list[0]);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayCopyOneItemCollection()
		{
			int[] array = CreateLinearArray(1);
			int[] shuffledArray = (int[])array.ShuffleInto(new int[1], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(1, shuffledArray.Length);
			Assert.AreEqual(0, shuffledArray[0]);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListCopyOneItemCollection()
		{
			List<int> list = CreateLinearList(1);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[1], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(1, shuffledArray.Length);
			Assert.AreEqual(0, shuffledArray[0]);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleLinkedListCopyOneItemCollection()
		{
			LinkedList<int> list = CreateLinearLinkedList(1);
			int[] shuffledArray = (int[])list.ShuffleInto(new int[1], XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledArray);
			Assert.AreEqual(1, shuffledArray.Length);
			Assert.AreEqual(0, shuffledArray[0]);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleArrayCopyAppendOneItemCollection()
		{
			int[] array = CreateLinearArray(1);
			List<int> shuffledList = (List<int>)array.ShuffleInto(new List<int>(1), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(1, shuffledList.Count);
			Assert.AreEqual(0, shuffledList[0]);
		}

		[TestCase(Category = "Normal")]
		public void ShuffleListCopyAppendOneItemCollection()
		{
			List<int> list = CreateLinearList(1);
			List<int> shuffledList = (List<int>)list.ShuffleInto(new List<int>(1), XorShift128Plus.Create(seed));
			Assert.IsNotNull(shuffledList);
			Assert.AreEqual(1, shuffledList.Count);
			Assert.AreEqual(0, shuffledList[0]);
		}

		[TestCase(Category = "Normal")]
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
