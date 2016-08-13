/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.MakeItRandom.Tests
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
				var v = random.UnitVector2();
				Assert.AreEqual(1.0, v.sqrMagnitude, 0.0001);
			}
		}

		[Test]
		public void UnitVector3sAreLength1()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var v = random.UnitVector3();
				Assert.AreEqual(1.0, v.sqrMagnitude, 0.0001);
			}
		}

		[Test]
		public void UnitVector4sAreLength1()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var v = random.UnitVector4();
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
				var v = random.UnitVector2();
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
				var l = random.ClosedRange(2f, 8f);
				var l2 = l * l;
				var v = random.ScaledVector2(l);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}

		[Test]
		public void ScaledVector3sAreLengthR()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var l = random.ClosedRange(2f, 8f);
				var l2 = l * l;
				var v = random.ScaledVector3(l);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}

		[Test]
		public void ScaledVector4sAreLengthR()
		{
			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < 10000; ++i)
			{
				var l = random.ClosedRange(2f, 8f);
				var l2 = l * l;
				var v = random.ScaledVector4(l);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}
	}
}
#endif
