/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using NUnit.Framework;

namespace MakeIt.Random.Tests
{
	class RandomVectorTests
	{
		private const string seed = "random seed";

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void UnitVector2sAreLength1(int sampleSizePercentage)
		{
			int count = (10000 * sampleSizePercentage) / 100;

			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < count; ++i)
			{
				var v = random.UnitVector2();
				Assert.AreEqual(1.0, v.sqrMagnitude, 0.0001);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void UnitVector3sAreLength1(int sampleSizePercentage)
		{
			int count = (10000 * sampleSizePercentage) / 100;

			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < count; ++i)
			{
				var v = random.UnitVector3();
				Assert.AreEqual(1.0, v.sqrMagnitude, 0.0001);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void UnitVector4sAreLength1(int sampleSizePercentage)
		{
			int count = (10000 * sampleSizePercentage) / 100;

			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < count; ++i)
			{
				var v = random.UnitVector4();
				Assert.AreEqual(1.0, v.sqrMagnitude, 0.0001);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void UnitVector2sUniformlyDistributed(int sampleSizePercentage)
		{
			var bucketCount = 72;
			var hitsPerBucket = 144;
			var tolerance = 0.1f;

			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

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

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ScaledVector2sAreLengthR(int sampleSizePercentage)
		{
			int count = (10000 * sampleSizePercentage) / 100;

			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < count; ++i)
			{
				var l = random.RangeCC(2f, 8f);
				var l2 = l * l;
				var v = random.ScaledVector2(l);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ScaledVector3sAreLengthR(int sampleSizePercentage)
		{
			int count = (10000 * sampleSizePercentage) / 100;

			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < count; ++i)
			{
				var l = random.RangeCC(2f, 8f);
				var l2 = l * l;
				var v = random.ScaledVector3(l);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ScaledVector4sAreLengthR(int sampleSizePercentage)
		{
			int count = (10000 * sampleSizePercentage) / 100;

			var random = XorShift128Plus.Create(seed);
			for (int i = 0; i < count; ++i)
			{
				var l = random.RangeCC(2f, 8f);
				var l2 = l * l;
				var v = random.ScaledVector4(l);
				Assert.AreEqual(l2, v.sqrMagnitude, l2 * 0.0001);
			}
		}
	}
}
#endif
