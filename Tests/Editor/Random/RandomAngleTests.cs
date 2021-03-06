﻿/******************************************************************************\
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
using NUnit.Framework;
using UnityEngine;
using System;

namespace MakeIt.Random.Tests
{
	class RandomAngleTests
	{
		private const string seed = "random seed";

		private void ValidateAngleRange(int count, Func<float> generator, float min, float max, Action<float, float> assertLowerBoundary, Action<float, float> assertUpperBoundary, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				float a = generator();
				assertLowerBoundary(a, min);
				assertUpperBoundary(a, max);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ValidateAngleDeg(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleDegOO(), 0f, 360f, Assert.Greater, Assert.Less, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeAngleDegOOGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, 360f, Assert.Greater, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleDegCO(), 0f, 360f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCOGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, 360f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleDegOC(), 0f, 360f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegOCGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, 360f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleDegCC(), 0f, 360f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCCGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, 360f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ValidateAngleRad(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleRadOO(), 0f, Mathf.PI * 2f, Assert.Greater, Assert.Less, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeAngleRadOOGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, Mathf.PI * 2f, Assert.Greater, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleRadCO(), 0f, Mathf.PI * 2f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCOGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, Mathf.PI * 2f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleRadOC(), 0f, Mathf.PI * 2f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadOCGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, Mathf.PI * 2f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleRadCC(), 0f, Mathf.PI * 2f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCCGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, Mathf.PI * 2f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ValidateSignedAngleDeg(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleDegOO(), -180f, +180f, Assert.Greater, Assert.Less, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeAngleDegOOGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -180f, +180f, Assert.Greater, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleDegCO(), -180f, +180f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCOGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -180f, +180f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleDegOC(), -180f, +180f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegOCGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -180f, +180f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleDegCC(), -180f, +180f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCCGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -180f, +180f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ValidateSignedAngleRad(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleRadOO(), -Mathf.PI, +Mathf.PI, Assert.Greater, Assert.Less, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeAngleRadOOGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI, +Mathf.PI, Assert.Greater, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleRadCO(), -Mathf.PI, +Mathf.PI, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCOGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI, +Mathf.PI, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleRadOC(), -Mathf.PI, +Mathf.PI, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadOCGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI, +Mathf.PI, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleRadCC(), -Mathf.PI, +Mathf.PI, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCCGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI, +Mathf.PI, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ValidateHalfAngleDeg(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleDegOO(), 0f, +180f, Assert.Greater, Assert.Less, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeAngleDegOOGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +180f, Assert.Greater, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleDegCO(), 0f, +180f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCOGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +180f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleDegOC(), 0f, +180f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegOCGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +180f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleDegCC(), 0f, +180f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCCGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +180f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ValidateHalfAngleRad(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleRadOO(), 0f, +Mathf.PI, Assert.Greater, Assert.Less, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeAngleRadOOGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +Mathf.PI, Assert.Greater, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleRadCO(), 0f, +Mathf.PI, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCOGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +Mathf.PI, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleRadOC(), 0f, +Mathf.PI, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadOCGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +Mathf.PI, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleRadCC(), 0f, +Mathf.PI, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCCGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +Mathf.PI, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ValidateSignedHalfAngleDeg(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleDegOO(), -90f, +90f, Assert.Greater, Assert.Less, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeAngleDegOOGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -90f, +90f, Assert.Greater, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleDegCO(), -90f, +90f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCOGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -90f, +90f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleDegOC(), -90f, +90f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegOCGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -90f, +90f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleDegCC(), -90f, +90f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCCGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -90f, +90f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void ValidateSignedHalfAngleRad(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleRadOO(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.Greater, Assert.Less, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeAngleRadOOGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.Greater, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleRadCO(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCOGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleRadOC(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadOCGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleRadCC(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCCGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);
		}
	}
}
#endif
