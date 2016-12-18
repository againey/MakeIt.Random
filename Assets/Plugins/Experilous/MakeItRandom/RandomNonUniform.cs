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
		//TODO:  Rename this class to RandomSample?  Add UniformSample(), even though it's just a duplicate of RandomFloatingPoint.Range()?

		public static float NormalSample(this IRandom random, float mean, float standardDeviation)
		{
#if UNITY_EDITOR
			if (standardDeviation <= 0f) throw new ArgumentException("The standard deviation must be greater than zero", "standardDeviation");
#endif

			float sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.NormalFloat.zigguratTable,
				Detail.Distributions.NormalFloat.F,
				Detail.Distributions.NormalFloat.SampleFallback);

			return sample * standardDeviation + mean;
		}

		public static float ExponentialSample(this IRandom random, float eventRate)
		{
#if UNITY_EDITOR
			if (eventRate <= 0f) throw new ArgumentException("The event rate must be greater than zero", "eventRate");
#endif

			float sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.ExponentialFloat.zigguratTable,
				Detail.Distributions.ExponentialFloat.F,
				Detail.Distributions.ExponentialFloat.SampleFallback);

			return sample / eventRate;
		}

		public static float TriangularSample(this IRandom random, float lower, float mode, float upper)
		{
#if UNITY_EDITOR
			if (lower >= mode) throw new ArgumentException("The mode must be greater than the lower range boundary", "mode");
			if (mode >= upper) throw new ArgumentException("The upper range boundary must be greater than the mode boundary", "upper");
#endif

			float n = random.FloatOO();
			float range = upper - lower;
			float lowerRange = mode - lower;
			float split = lowerRange / range;
			return n < split ? lower + Mathf.Sqrt(n * range * lowerRange) : upper - Mathf.Sqrt((1f - n) * range * (upper - mode));
		}

		public static float TrapezoidalSample(this IRandom random, float lower, float lowerMode, float upperMode, float upper)
		{
#if UNITY_EDITOR
			if (lower >= lowerMode) throw new ArgumentException("The lower mode boundary must be greater than the lower range boundary", "lowerMode");
			if (lowerMode >= upperMode) throw new ArgumentException("The upper mode boundary must be greater than the lower mode boundary", "upperMode");
			if (upperMode >= upper) throw new ArgumentException("The upper range boundary must be greater than the upper mode boundary", "upper");
#endif

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

		public static float LinearSample(this IRandom random, Vector2 p0, Vector2 p1)
		{
			return random.LinearSample(p0.x, p0.y, p1.x, p1.y);
		}

		public static float LinearSample(this IRandom random, float x0, float y0, float x1, float y1)
		{
#if UNITY_EDITOR
			if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary", "x1");
			if (y0 < 0f) throw new ArgumentException("The domain must be entirely non-negative", "y0");
			if (y1 < 0f) throw new ArgumentException("The domain must be entirely non-negative", "y1");
#endif

			return random.LinearSample(x0, y0, x1, y1, random.FloatCC());
		}

		private static float LinearSample(this IRandom random, float x0, float y0, float x1, float y1, float n)
		{
			float xDelta = x1 - x0;
			float yDelta = y1 - y0;
			float ySum = y0 + y1;
			float area = 0.5f * xDelta * ySum;

			float a = 0.5f * yDelta;
			float b = x1 * y0 - x0 * y1;
			float c = -(a * x0 + b) * x0 - area * xDelta * n;

			// Solve the quadratic equation.
			if (a != 0f)
			{
				float sqr = b * b - 4f * a * c;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					float sqrt = Mathf.Sqrt(sqr);
					// Check the larger t first, then the smaller.
					if (b >= 0f)
					{
						return -2f * c / (b + sqrt); // Citardauq
						//if (t >= x0 && t <= x1) return t;
						//return -0.5f * (b + sqrt) / a; // Quadratic
					}
					else
					{
						return -0.5f * (b - sqrt) / a; // Quadratic
						//if (t >= x0 && t <= x1) return t;
						//return -2f * c / (b - sqrt); // Citardauq
					}
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -0.0001f)
				{
					// Make sure the larger value is in the denominator.
					if (Mathf.Abs(a) > Mathf.Abs(b))
					{
						return -0.5f * b / a; // Quadratic
					}
					else
					{
						return -2f * c / b; // Citardauq
					}
				}
				// If the square is definitely negative, there is no solution.
				else
				{
					return float.NaN;
				}
			}
			// This is actually at most a linear equation, no need to mess with square roots.
			// If the linear component is not zero, then it must have exactly one root.
			else if (b != 0f)
			{
				return -c / b;
			}
				// Else, it is a constant function; if it is a non-zero constant, it has no root, otherwise it has infinite roots; either way, useless.
			else
			{
				return float.NaN;
			}
		}

		private static float SolveUnitQuadratic(float a0, float b0, float c0, float epsilon = 0.0001f)
		{
			if (a0 != 0f)
			{
				var sqr = b0 * b0 - 4f * a0 * c0;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					var sqrt = Mathf.Sqrt(sqr);
					// Check the larger t first, then the smaller.
					if (b0 >= 0f)
					{
						float t = -2f * c0 / (b0 + sqrt); // Citardauq
						if (t >= 0f && t <= 1f) return t;
						return -0.5f * (b0 + sqrt) / a0; // Quadratic
					}
					else
					{
						float t = -0.5f * (b0 - sqrt) / a0; // Quadratic
						if (t >= 0f && t <= 1f) return t;
						return -2f * c0 / (b0 - sqrt); // Citardauq
					}
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -epsilon)
				{
					// Make sure the larger value is in the denominator.
					if (Mathf.Abs(a0) > Mathf.Abs(b0))
					{
						return -0.5f * b0 / a0; // Quadratic
					}
					else
					{
						return -2f * c0 / b0; // Citardauq
					}
				}
				// If the square is definitely negative, there is no solution.
				else
				{
					return float.NaN;
				}
			}
			// This is actually at most a linear equation, no need to mess with square roots.
			else
			{
				// If the linear component is not zero, then it must have exactly one root.
				if (b0 != 0f)
				{
					return -c0 / b0;
				}
				// Else, it is a constant function; if it is a non-zero constant, it has no root, otherwise it has infinite roots; either way, useless.
				else
				{
					return float.NaN;
				}
			}
		}

		private static float SolveUnitQuadratic(float a0, float b0, float c0, float a1, float b1, float c1, float epsilon = 0.0001f)
		{
			if (a0 != 0f)
			{
				var sqr = b0 * b0 - 4f * a0 * c0;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					var sqrt = Mathf.Sqrt(sqr);
					// Check the larger t first, then the smaller.
					if (b0 >= 0f)
					{
						float t = -2f * c0 / (b0 + sqrt); // Citardauq
						if (t >= 0f && t <= 1f) return t;
						return -0.5f * (b0 + sqrt) / a0; // Quadratic
					}
					else
					{
						float t = -0.5f * (b0 - sqrt) / a0; // Quadratic
						if (t >= 0f && t <= 1f) return t;
						return -2f * c0 / (b0 - sqrt); // Citardauq
					}
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -epsilon)
				{
					// Make sure the larger value is in the denominator.
					if (Mathf.Abs(a0) > Mathf.Abs(b0))
					{
						return -0.5f * b0 / a0; // Quadratic
					}
					else
					{
						return -2f * c0 / b0; // Citardauq
					}
				}
				// If the square is definitely negative, there is no solution.
				else
				{
					return SolveUnitQuadratic(a1, b1, c1, epsilon);
				}
			}
			// This is actually at most a linear equation, no need to mess with square roots.
			else
			{
				// If the linear component is not zero, then it must have exactly one root.
				if (b0 != 0f)
				{
					return -c0 / b0;
				}
				// Else, it is a constant function; if it is a non-zero constant, it has no root, otherwise it has infinite roots; either way, useless.
				else
				{
					return SolveUnitQuadratic(a1, b1, c1, epsilon);
				}
			}
		}

		public static float HermiteSample(this IRandom random, Vector2 p0, float m0, Vector2 p1, float m1)
		{
			return random.HermiteSample(p0.x, p0.y, m0, p1.x, p1.y, m1);
		}

		public static float HermiteSample(this IRandom random, float x0, float y0, float m0, float x1, float y1, float m1)
		{
#if UNITY_EDITOR
			if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary", "x1");
			if (y0 < 0f) throw new ArgumentException("The domain must be entirely non-negative", "y0");
			if (y1 < 0f) throw new ArgumentException("The domain must be entirely non-negative", "y1");
#endif

			return random.HermiteSample(x0, y0, m0, x1, y1, m1, random.FloatCC());
		}

		private static float HermiteSample(this IRandom random, float x0, float y0, float m0, float x1, float y1, float m1, float t)
		{
			float xDelta = x1 - x0;
			float yDelta = y1 - y0;

			// Hermite curve formula:  f(x) = ax^3 + bx^2 + cx + d
			float a = -2f * yDelta + (m0 + m1) * xDelta;
			float b = 3f * yDelta - (2f * m0 + m1) * xDelta;
			float c = m0 * xDelta;
			float d = y0;

			// Coefficients from taking the integral of f(x), also the coefficients of the quartic to be solved.
			float k4 = a / 4f;
			float k3 = b / 3f;
			float k2 = c / 2f;
			float k1 = d;

			// Area under curve = Integral of f(x) = ax^3 + bx^2 + cx + d from 0 to 1.
			float area = k4 + k3 + k2 + k1;

			float k0 = -area * t;

			// Solve the quartic k4*t^4 + k3*t^3 + k2*t^2 + k1*t + k0 = 0
			float u = k3 / k4;
			float v = k2 / k4;

			float delta = float.PositiveInfinity;
			float epsilon = 0.0001f;
			var loopGuard = new Core.InfiniteLoopGuard(1000);
			while (delta > epsilon)
			{
				float n0 = k3 - u * k4;
				float n1 = k2 - u * n0 - v * k4;
				float n2 = k1 - u * n1 - v * n0;
				float n3 = k0 - v * n1;
				float n4 = n0 - u * k4;
				float n5 = n1 - v * k4;

				float s = v * n4 * n4 + n5 * (n5 - u * n4);
				float du = (n4 * n3 - n5 * n2) / s;
				float dv = ((u * n4 - n5) * n3 - v * n4 * n2) / s;
				u -= du;
				v -= dv;
				delta = Mathf.Sqrt((du * du) / (u * u + 1f) + (dv * dv) / (v * v + 1f));
				loopGuard.Iterate();
			}

			{
				float n0 = k3 - u * k4;
				float n1 = k2 - u * n0 - v * k4;

				return SolveUnitQuadratic(k4, n0, n1, 1f, u, v) * xDelta + x0;
			}
		}

		public static float PiecewiseConstantSample(this IRandom random, float[] x, float[] y)
		{
#if UNITY_EDITOR
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length - y.Length != 1) throw new ArgumentException("The array of y values must have exactly one fewer element than the array of x values.", "y");
#endif

			float totalArea = 0f;
			int i = 0;
			float x0 = x[0];
			while (i < y.Length)
			{
				float h = y[i];
				++i;
				float x1 = x[i];
				totalArea += (x1 - x0) * h;
				x0 = x1;
			}

			float n = random.RangeCC(totalArea);
			i = 0;
			x0 = x[0];
			while (i < y.Length)
			{
				float h = y[i];
				++i;
				float x1 = x[i];
				totalArea -= (x1 - x0) * h;
				if (totalArea < n) return random.RangeCO(x0, x1);
				x0 = x1;
			}
			return x0;
		}

		public static float PiecewiseConstantSample(this IRandom random, Vector2[] p, float xLast)
		{
#if UNITY_EDITOR
			if (p.Length < 1) throw new ArgumentException("The array of vectors must have at least one element.", "p");
#endif

			float totalArea = 0f;
			Vector2 p0 = p[0];
			for (int i = 1; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
				totalArea += (p1.x - p0.x) * p0.y;
				p0 = p1;
			}
			totalArea += (xLast - p0.x) * p0.y;

			float n = random.RangeCC(totalArea);
			p0 = p[0];
			for (int i = 1; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
				totalArea -= (p1.x - p0.x) * p0.y;
				if (totalArea < n) return random.RangeCO(p0.x, p1.x);
				p0 = p1;
			}
			totalArea -= (xLast - p0.x) * p0.y;
			if (totalArea < n) return random.RangeCO(p0.x, xLast);
			return xLast;
		}

		public static float PiecewiseWeightedConstantSample(this IRandom random, float[] x, float[] weights)
		{
#if UNITY_EDITOR
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length - weights.Length != 1) throw new ArgumentException("The array of weights must have exactly one fewer element than the array of x values.", "weights");
#endif

			float weightSum = 0f;
			for (int i = 0; i < weights.Length; ++i)
			{
				weightSum += weights[i];
			}

			float n = random.RangeCC(weightSum);
			for (int i = 0; i < weights.Length; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return random.RangeCO(x[i], x[i + 1]);
			}
			return x[weights.Length];
		}

		public static float PiecewiseLinearSample(this IRandom random, float[] x, float[] y)
		{
#if UNITY_EDITOR
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");
#endif

			float doubleTotalArea = 0f;
			float x0 = x[0];
			float y0 = y[0];
			for (int i = 1; i < x.Length; ++i)
			{
				float x1 = x[i];
				float y1 = y[i];
				doubleTotalArea += (x1 - x0) * (y0 + y1); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
				x0 = x1;
				y0 = y1;
			}

			float n = random.RangeCC(doubleTotalArea);
			x0 = x[0];
			y0 = y[0];
			for (int i = 1; i < x.Length; ++i)
			{
				float x1 = x[i];
				float y1 = y[i];
				doubleTotalArea -= (x1 - x0) * (y0 + y1);
				if (doubleTotalArea < n) return random.LinearSample(x0, y0, x1, y1, random.FloatCO());
				x0 = x1;
				y0 = y1;
			}
			return x0;
		}

		public static float PiecewiseLinearSample(this IRandom random, Vector2[] p)
		{
#if UNITY_EDITOR
			if (p.Length < 2) throw new ArgumentException("The array of vectors must have at least two elements.", "p");
#endif

			float doubleTotalArea = 0f;
			Vector2 p0 = p[0];
			for (int i = 1; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
				doubleTotalArea += (p1.x - p0.x) * (p0.y + p1.y); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
				p0 = p1;
			}

			double n = random.RangeCC(doubleTotalArea);
			p0 = p[0];
			for (int i = 1; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
				doubleTotalArea -= (p1.x - p0.x) * (p0.y + p1.y);
				if (doubleTotalArea < n) return random.LinearSample(p0.x, p0.y, p1.x, p1.y, random.FloatCO());
				p0 = p1;
			}
			return p0.x;
		}

		public static float PiecewiseCurveSample(this IRandom random, AnimationCurve curve)
		{
#if UNITY_EDITOR
			if (curve.length < 2) throw new ArgumentException("The curve must have at least two keyframes.", "curve");
#endif

			float totalArea = 0f;
			Keyframe kf0 = curve[0];
			for (int i = 1; i < curve.length; ++i)
			{
				Keyframe kf1 = curve[i];
				float xDelta = kf1.time - kf0.time;
				if (!float.IsInfinity(kf0.outTangent) && !float.IsInfinity(kf1.inTangent))
				{
					float yDelta = kf1.value - kf0.value;
					if (kf0.outTangent == kf1.inTangent && Mathf.Abs(xDelta * kf0.outTangent - yDelta) < 0.000001f)
					{
						// Linear Segment
						totalArea += 0.5f * (kf0.value + kf1.value) * xDelta;
					}
					else
					{
						// Hermite Segment
						float a = -2f * yDelta + (kf0.outTangent + kf1.inTangent) * xDelta;
						float b = 3f * yDelta - (2f * kf0.outTangent + kf1.inTangent) * xDelta;
						float c = kf0.outTangent * xDelta;
						float d = kf0.value;

						float k4 = a / 4f;
						float k3 = b / 3f;
						float k2 = c / 2f;
						float k1 = d;

						// Area under curve = Integral of f(x) = ax^3 + bx^2 + cx + d from x0 to x1.
						totalArea += (k4 + k3 + k2 + k1) * xDelta;
					}
				}
				else
				{
					// Uniform Segment
					totalArea += xDelta * kf0.value;
				}
				kf0 = kf1;
			}

			float n = random.RangeCC(totalArea);
			kf0 = curve[0];
			for (int i = 1; i < curve.length; ++i)
			{
				Keyframe kf1 = curve[i];

				float xDelta = kf1.time - kf0.time;
				if (!float.IsInfinity(kf0.outTangent) && !float.IsInfinity(kf1.inTangent))
				{
					float yDelta = kf1.value - kf0.value;
					if (kf0.outTangent == kf1.inTangent && Mathf.Abs(xDelta * kf0.outTangent - yDelta) < 0.000001f)
					{
						// Linear Segment
						totalArea -= 0.5f * (kf0.value + kf1.value) * xDelta;
						if (totalArea < n) return random.LinearSample(kf0.time, kf0.value, kf1.time, kf1.value, random.FloatCO());
					}
					else
					{
						// Hermite Segment
						float a = -2f * yDelta + (kf0.outTangent + kf1.inTangent) * xDelta;
						float b = 3f * yDelta - (2f * kf0.outTangent + kf1.inTangent) * xDelta;
						float c = kf0.outTangent * xDelta;
						float d = kf0.value;

						float k4 = a / 4f;
						float k3 = b / 3f;
						float k2 = c / 2f;
						float k1 = d;

						// Area under curve = Integral of f(x) = ax^3 + bx^2 + cx + d from x0 to x1.
						totalArea -= (k4 + k3 + k2 + k1) * xDelta;
						if (totalArea < n) return random.HermiteSample(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent, random.FloatCO());
					}
				}
				else
				{
					// Uniform Segment
					totalArea -= xDelta * kf0.value;
					if (totalArea < n) return random.RangeCO(kf0.time, kf1.time);
				}
				kf0 = kf1;
			}
			return kf0.time;
		}
	}
}
