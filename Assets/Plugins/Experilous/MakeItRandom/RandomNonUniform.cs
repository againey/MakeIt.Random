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

			float n = random.FloatCC();
			
			float xDelta = x1 - x0;
			float yDelta = y1 - y0;
			float ySum = y0 + y1;

			float cross = x0 * y1 - x1 * y0;
			float x0yDelta = x0 * yDelta;
			float square = cross * cross + x0yDelta * (x0yDelta - 2f * cross) + xDelta * xDelta * yDelta * ySum * n;
			return (cross + Mathf.Sqrt(square)) / yDelta;
		}

		private static int SolveQuadratic(float a, float b, float c, out float t0, out float t1, float epsilon = 0.0001f)
		{
			// Conditionally uses the quadratic formula or the Citardauq formula to minimize precision loss.
			if (a != 0f)
			{
				var sqr = b * b - 4f * a * c;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					var sqrt = Mathf.Sqrt(sqr);
					// Make sure the smaller t value is stored in t0, the larger in t1.
					if (b >= 0f)
					{
						t0 = -0.5f * (b + sqrt) / a; // Quadratic
						t1 = -2f * c / (b + sqrt); // Citardauq
						return 2;
					}
					else
					{
						t0 = -2f * c / (b - sqrt); // Citardauq
						t1 = -0.5f * (b - sqrt) / a; // Quadratic
						return 2;
					}
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -epsilon)
				{
					// Make sure the larger value is in the denominator.
					if (Mathf.Abs(a) > Mathf.Abs(b))
					{
						t0 = t1 = -0.5f * b / a; // Quadratic
						return 1;
					}
					else
					{
						t0 = t1 = -2f * c / b; // Citardauq
						return 1;
					}
				}
				// If the square is definitely negative, there is no solution.
				else
				{
					t0 = t1 = float.NaN;
					return 0;
				}
			}
			// This is actually at most a linear equation, no need to mess with square roots.
			else
			{
				// If the linear component is not zero, then it must have exactly one root.
				if (b != 0f)
				{
					t0 = t1 = -c / b;
					return 1;
				}
				// Else, it is a constant function; if it is a non-zero constant, it has no root.
				else if (c != 0f)
				{
					t0 = t1 = float.NaN;
					return 0;
				}
				// Else, it has infinite roots.
				else
				{
					t0 = float.NegativeInfinity;
					t1 = float.PositiveInfinity;
					return 0; // Return 0 as the number of roots anyway.  If the caller cares about this case, t0 and t1 can be checked.
				}
			}
		}

		private static float SolveQuadratic(float a0, float b0, float c0, float epsilon = 0.0001f)
		{
			if (a0 != 0f)
			{
				var sqr = b0 * b0 - 4f * a0 * c0;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					var sqrt = Mathf.Sqrt(sqr);
					// Make sure the larger t is used; use the Citardauq formula if b0 isn't negative, the quadratic formula if it is.
					return (b0 >= 0f) ? (-2f * c0 / (b0 + sqrt)) : (-0.5f * (b0 - sqrt) / a0);
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -epsilon)
				{
					// Use the quadratic formula if a0's magnitude is larger than b0's, the Citardauq formula if it isn't.
					return (Mathf.Abs(a0) > Mathf.Abs(b0)) ? (-0.5f * b0 / a0) : (-2f * c0 / b0);
				}
				// If the square is definitely negative, try the other pair of solutions to the quartic.
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
				// Else, it is a constant function; if it is a non-zero constant, it has no root.
				else if (c0 != 0f)
				{
					return float.NaN;
				}
				// Else, it has infinite roots.
				else
				{
					return float.NaN;
				}
			}
		}

		private static float SolveQuadratic(float a0, float b0, float c0, float a1, float b1, float c1, float epsilon = 0.0001f)
		{
			if (a0 != 0f)
			{
				var sqr = b0 * b0 - 4f * a0 * c0;
				// If the square is positive, we can take the square root and use the standard formulas.
				if (sqr > 0f)
				{
					var sqrt = Mathf.Sqrt(sqr);
					// Make sure the larger t is used; use the Citardauq formula if b0 isn't negative, the quadratic formula if it is.
					return (b0 >= 0f) ? (-2f * c0 / (b0 + sqrt)) : (-0.5f * (b0 - sqrt) / a0);
				}
				// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
				else if (sqr > -epsilon)
				{
					// Use the quadratic formula if a0's magnitude is larger than b0's, the Citardauq formula if it isn't.
					return (Mathf.Abs(a0) > Mathf.Abs(b0)) ? (-0.5f * b0 / a0) : (-2f * c0 / b0);
				}
				// If the square is definitely negative, try the other pair of solutions to the quartic.
				else
				{
					return SolveQuadratic(a1, b1, c1, epsilon);
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
				// Else, it is a constant function; if it is a non-zero constant, it has no root.
				else if (c0 != 0f)
				{
					return SolveQuadratic(a1, b1, c1, epsilon);
				}
				// Else, it has infinite roots.
				else
				{
					return SolveQuadratic(a1, b1, c1, epsilon);
				}
			}
		}

		public static float HermiteSample(this IRandom random, Vector2 p0, Vector2 p1, float m0, float m1)
		{
#if UNITY_EDITOR
			if (p0.x >= p1.x) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary", "p1");
			if (p0.y < 0f) throw new ArgumentException("The domain must be entirely non-negative", "p0");
			if (p1.y < 0f) throw new ArgumentException("The domain must be entirely non-negative", "p1");
#endif

			float xDelta = p1.x - p0.x;
			float yDelta = p1.y - p0.y;
			float x0Squared = p0.x * p0.x;
			float x1Squared = p1.x * p1.x;
			float xSquaredSum = x0Squared + x1Squared;

			// Hermite curve formula:  f(x) = ax^3 + bx^2 + cx + d
			float a = -2f * yDelta + (m0 + m1) * xDelta;
			float b = 3f * yDelta - (2f * m0 + m1) * xDelta;
			float c = m0 * xDelta;
			float d = p0.y;

			// Coefficients from taking the integral of f(x), also the coefficients of the quartic to be solved.
			float k4 = a / 4f;
			float k3 = b / 3f;
			float k2 = c / 2f;
			float k1 = d;

			// Area under curve = Integral of f(x) = ax^3 + bx^2 + cx + d from x0 to x1.
			//float area = ((k4 * xSquaredSum + k2) * (p1.x + p0.x) + k3 * (xSquaredSum + p0.x * p1.x) + k1) * xDelta;
			float area = k4 + k2 + k3 + k1;

			float k0 = -area * random.FloatCC();

			//Debug.LogFormat("C:  {0:F2}, {1:F2}, {2:F2}, {3:F2}", a, b, c, d);
			//Debug.LogFormat("A:  {0:F2}  T:  {1:F6}", area, t);
			//Debug.LogFormat("K:  {0:F2}, {1:F2}, {2:F2}, {3:F2}, {4:F6}", k4, k3, k2, k1, k0);

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
				//Debug.LogFormat("N:  {0:E6}, {1:E6}, {2:E6}, {3:E6}, {4:E6}, {5:E6}", n0, n1, n2, n3, n4, n5);

				float s = v * n4 * n4 + n5 * (n5 - u * n4);
				float du = (n4 * n3 - n5 * n2) / s;
				float dv = ((u * n4 - n5) * n3 - v * n4 * n2) / s;
				u -= du;
				v -= dv;
				delta = Mathf.Sqrt((du * du) / (u * u + 1f) + (dv * dv) / (v * v + 1f));
				//Debug.LogFormat("D:  {0:E6}, {1:E6}, {2:E6}, {3:E6}, {4:E6}, {5:E6}", delta, u, v, du, dv, s);
				loopGuard.Iterate();
			}

			{
				float n0 = k3 - u * k4;
				float n1 = k2 - u * n0 - v * k4;

				return SolveQuadratic(k4, n0, n1, 1f, u, v) * xDelta + p0.x;

				/*
				if (k4 != 0f)
				{
					var sqr = n0 * n0 - 4f * k4 * n1;
					// If the square is positive, we can take the square root and use the standard formulas.
					if (sqr > 0f)
					{
						var sqrt = Mathf.Sqrt(sqr);
						// Make sure the larger t is used; use the Citardauq formula if n0 isn't negative, the quadratic formula if it is.
						float t = (n0 >= 0f) ? (-2f * n1 / (n0 + sqrt)) : (-0.5f * (n0 - sqrt) / k4);
						return t * xDelta + p0.x;
					}
					// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
					else if (sqr > -epsilon)
					{
						// Use the quadratic formula if k4's magnitude is larger than n0's, the Citardauq formula if it isn't.
						float t = (Mathf.Abs(k4) > Mathf.Abs(n0)) ? (-0.5f * n0 / k4) : (-2f * n1 / n0);
						return t * xDelta + p0.x;
					}
					// If the square is definitely negative, try the other pair of solutions to the quartic.
					else
					{
						sqr = u * u - 4f * v;
						// If the square is positive, we can take the square root and use the standard formulas.
						if (sqr > 0f)
						{
							var sqrt = Mathf.Sqrt(sqr);
							// Make sure the larger t is used; use the Citardauq formula if u isn't negative, the quadratic formula if it is.
							float t = (u >= 0f) ? (-2f * v / (u + sqrt)) : (-0.5f * (u - sqrt));
							return t * xDelta + p0.x;
						}
						// If it is at least within epsilon of being zero, we will treat the square root portion as exactly zero.
						else if (sqr > -epsilon)
						{
							// Use the quadratic formula if u's magnitude is smaller than 1, the Citardauq formula if it isn't.
							float t = (Mathf.Abs(u) < 1f) ? (-0.5f * u) : (-2f * v / u);
							return t * xDelta + p0.x;
						}
						// If the square is definitely negative, try the other pair of solutions to the quartic.
						else
						{
							return float.NaN;
						}
					}
				}
				// This is actually at most a linear equation, no need to mess with square roots.
				else
				{
					// If the linear component is not zero, then it must have exactly one root.
					if (n0 != 0f)
					{
						float t = -n1 / n0;
						return t * xDelta + p0.x;
					}
					// Else, it is a constant function; if it is a non-zero constant, it has no root.
					else if (c != 0f)
					{
						t0 = t1 = float.NaN;
						return 0;
					}
					// Else, it has infinite roots.
					else
					{
						t0 = float.NegativeInfinity;
						t1 = float.PositiveInfinity;
						return 0; // Return 0 as the number of roots anyway.  If the caller cares about this case, t0 and t1 can be checked.
					}
				}
				*/

				//float t0, t1, t2, t3;
				//SolveQuadratic(k4, n0, n1, out t2, out t3);
				//if (!float.IsNaN(t3)) return t3 * xDelta + p0.x;
				//SolveQuadratic(1f, u, v, out t0, out t1);
				//return t1 * xDelta + p0.x;
				//Debug.LogFormat("U:  {0:E6}, {1:E6}, {2:E6}, {3:E6}", u, v, n0, n1);
				//Debug.LogFormat("T:  {0:E6}, {1:E6}, {2:E6}, {3:E6}", t0, t1, t2, t3);
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

			double n = random.RangeCC(totalArea);
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

			double n = random.RangeCC(totalArea);
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

			double n = random.RangeCC(doubleTotalArea);
			x0 = x[0];
			y0 = y[0];
			for (int i = 1; i < x.Length; ++i)
			{
				float x1 = x[i];
				float y1 = y[i];
				doubleTotalArea -= (x1 - x0) * (y0 + y1);
				if (doubleTotalArea < n) return random.RangeCO(x0, x1);
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
				if (doubleTotalArea < n) return random.RangeCO(p0.x, p1.x);
				p0 = p1;
			}
			return p0.x;
		}

		public static float PiecewiseCurveSample(this IRandom random, AnimationCurve curve)
		{
			throw new NotImplementedException();
		}
	}
}
