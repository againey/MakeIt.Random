/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random numbers according to non-uniform distributions.
	/// </summary>
	public static class RandomNonUniform
	{
		public static float NormalSample(this IRandom random, float mean, float standardDeviation)
		{
			float sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.NormalFloat.zigguratTable,
				Detail.Distributions.NormalFloat.F,
				Detail.Distributions.NormalFloat.SampleFallback);

			return sample * standardDeviation + mean;
		}

		public static float ExponentialSample(this IRandom random, float eventRate)
		{
			float sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.ExponentialFloat.zigguratTable,
				Detail.Distributions.ExponentialFloat.F,
				Detail.Distributions.ExponentialFloat.SampleFallback);

			return sample / eventRate;
		}

		public static float TriangularSample(this IRandom random, float lower, float mode, float upper)
		{
			float n = random.FloatOO();
			float range = upper - lower;
			float lowerRange = mode - lower;
			float split = lowerRange / range;
			return n < split ? lower + Mathf.Sqrt(n * range * lowerRange) : upper - Mathf.Sqrt((1f - n) * range * (upper - mode));
		}

		public static float TrapezoidalSample(this IRandom random, float lower, float lowerMode, float upperMode, float upper)
		{
			float n = random.FloatOO();

			float range = upper + upperMode - lowerMode - lower;

			float lowerRange = lowerMode - lower;
			float lowerSplit = lowerRange / range;
			if (n < lowerSplit) return lower + Mathf.Sqrt(n * range * lowerRange); // Within lower triangle.

			float midRange = upperMode - lowerMode;
			float upperSplit = (midRange + midRange + lowerRange) / range;
			if (n > upperSplit) return upper - Mathf.Sqrt((1 - n) * range * (upper - upperMode)); // Within upper triangle.

			return lowerMode + (n - lowerSplit) / (upperSplit - lowerSplit) * (upperMode - lowerMode); // Within middle rectangle.
		}

		public static float LinearSample(this IRandom random, float x0, float y0, float x1, float y1)
		{
			float n = random.FloatCC();
			
			float xDelta = x1 - x0;
			float yDelta = y1 - y0;
			float ySum = y0 + y1;

			float cross = x0 * y1 - x1 * y0;
			float x0yDelta = x0 * yDelta;
			float square = cross * cross + x0yDelta * (x0yDelta - 2f * cross) + xDelta * xDelta * yDelta * ySum * n;
			return (cross + Mathf.Sqrt(square)) / yDelta;
		}
	}
}
