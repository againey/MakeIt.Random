/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Randomization.Tests
{
	class RandomVectorTests
	{
		private const string seed = "random seed";

		[Test]
		public void UnitVector2sAreLength1()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var v = RandomVector.UnitVector2(random);
				Assert.AreEqual(1.0, v.sqrMagnitude, 0.0001);
			}
		}

		[Test]
		public void UnitVector3sAreLength1()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var v = RandomVector.UnitVector3(random);
				Assert.AreEqual(1.0, v.sqrMagnitude, 0.0001);
			}
		}

		[Test]
		public void UnitVector4sAreLength1()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var v = RandomVector.UnitVector4(random);
				Assert.AreEqual(1.0, v.sqrMagnitude, 0.0001);
			}
		}

		[Test]
		public void UnitVector2sUniformlyDistributed()
		{
			var bucketCount = 72;
			var hitsPerBucket = 144;
			var tolerance = 0.1f;

			var random = XorShift128Plus.Create(seed);
			var buckets = new int[bucketCount];
			var twoPi = Mathf.PI * 2f;
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var v = RandomVector.UnitVector2(random);
				var a = Mathf.Atan2(v.y, v.x);
				buckets[Mathf.FloorToInt((a + Mathf.PI) * bucketCount / twoPi)] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		[Test]
		public void ScaledVector2sAreLengthR()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var l = RandomRange.Closed(2f, 8f, random);
				var l2 = l * l;
				var v = RandomVector.ScaledVector2(l, random);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}

		[Test]
		public void ScaledVector3sAreLengthR()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var l = RandomRange.Closed(2f, 8f, random);
				var l2 = l * l;
				var v = RandomVector.ScaledVector3(l, random);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}

		[Test]
		public void ScaledVector4sAreLengthR()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var l = RandomRange.Closed(2f, 8f, random);
				var l2 = l * l;
				var v = RandomVector.ScaledVector4(l, random);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}
	}
}
#endif
