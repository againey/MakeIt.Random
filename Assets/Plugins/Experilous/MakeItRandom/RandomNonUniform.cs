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
		#region Ziggurat Algorithm

		private class ZigguratFloatTable
		{
			private struct Segment
			{
				public uint n;
				public float s;

				public Segment(uint n, float s)
				{
					this.n = n;
					this.s = s;
				}
			}

			private struct StopInfiniteLoop
			{
				private int _iterationCount;
				private int _maxIterationCount;

				public StopInfiniteLoop(int maxIterationCount)
				{
					_iterationCount = 0;
					_maxIterationCount = maxIterationCount;
				}

				public void Iterate()
				{
					if (++_iterationCount > _maxIterationCount)
					{
						throw new InvalidOperationException("Possible infinite loop detected!");
					}
				}
			}

			private Segment[] _segments;
			private float[] _y;
			private int _intThreshold;
			private int _intShift;
			private int _indexMask;
			private Func<float, float> _f;
			private Func<IRandom, float, float> _fallback;

			public static ZigguratFloatTable CreateNormal(int sizeMagnitude)
			{
				var table = new ZigguratFloatTable();
				int segmentCount = 1 << sizeMagnitude;
				table._segments = new Segment[segmentCount];
				table._y = new float[segmentCount];
				table._intThreshold = int.MinValue + segmentCount;
				table._intShift = sizeMagnitude;
				table._indexMask = segmentCount - 1;
				table._f = Normal;
				table._fallback = FromNormalFallback;
				table.BuildTables(Normal, NormalInverse, NormalCDF, 2.506628274631d, 0.00000001d);
				return table;
			}

			public float Sample(IRandom random)
			{
				var outerLoop = new StopInfiniteLoop(100);
				var innerLoop = new StopInfiniteLoop(100);
				do
				{
					int n;
					do
					{
						n = (int)random.Next32();
						innerLoop.Iterate();
					} while (n < _intThreshold);
					int i = n & _indexMask;
					n = n >> _intShift;
					var segment = _segments[i];

					float x = n * segment.s;

					if (Math.Abs(n) < segment.n)
					{
						return x;
					}

					if (i > 0)
					{
						float y = random.RangeCO(_y[i - 1], _y[i]);
						if (y < _f(x))
						{
							return x;
						}
					}
					else
					{
						return _fallback(random, (1L << (31 - _intShift)) * _segments[1].s * (n > 0L ? +1f : -1f));
					}
					outerLoop.Iterate();
				} while (true);
			}

			private static float Normal(float x)
			{
				return Mathf.Exp(-x * x * 0.5f);
			}

			private static double Normal(double x)
			{
				return Math.Exp(-x * x * 0.5d);
			}

			private static double NormalInverse(double y)
			{
				return Math.Sqrt(Math.Log(1d / (y * y)));
			}

			private static double NormalCDF(double x)
			{
				// https://en.wikipedia.org/wiki/Normal_distribution#Numerical_approximations_for_the_normal_CDF
				double t = 1d / (1d + 0.2316419d * x);
				return 2.506628274631d - Normal(x) * ((((1.330274429d * t - 1.821255978d) * t + 1.781477937d) * t - 0.356563782d) * t + 0.319381530d) * t;
			}

			private static float FromNormalFallback(IRandom random, float xMin)
			{
				var loop = new StopInfiniteLoop(100);
				// https://en.wikipedia.org/wiki/Ziggurat_algorithm#Fallback_algorithms_for_the_tail
				float x, y;
				do
				{
					x = -Mathf.Log(random.PreciseFloatOO()) / xMin;
					y = -Mathf.Log(random.PreciseFloatOO());
					loop.Iterate();
				} while (y * -2f < x * x);
				return xMin + x;
			}

			private void BuildTables(Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double acceptableError)
			{
				var firstLoop = new StopInfiniteLoop(100);
				var secondLoop = new StopInfiniteLoop(100);
				var x = new double[_segments.Length];
				double a = fCDF(0d) / _segments.Length;
				double rMin = fInv(f(0d) / _segments.Length);
				double rMax = rMin;
				do
				{
					rMax = fInv(f(rMax) * 0.5d);
					firstLoop.Iterate();
				} while (rMax * f(rMax) + totalArea - fCDF(rMax) > a);
				double tableError;
				do
				{
					double rAvg = (rMin + rMax) * 0.5d;
					tableError = CalculateTableError(rAvg, _segments.Length, f, fInv, fCDF, totalArea, x);
					if (double.IsNaN(tableError) || tableError > 0d)
					{
						rMin = rAvg;
					}
					else
					{
						rMax = rAvg;
					}
					secondLoop.Iterate();
				} while (double.IsNaN(tableError) || Math.Abs(tableError) > acceptableError);

				double intToFloatScale = 1 << (31 - _intShift);

				double y0 = f(x[0]);
				double a0 = x[0] * y0;
				double v = a0 + totalArea - fCDF(x[0]);
				uint n0 = (uint)Math.Floor(a0 / v * intToFloatScale);
				_segments[0] = new Segment(n0, (float)(v / y0 / intToFloatScale));
				_y[0] = (float)f(x[0]);

				for (int i = 1; i < _segments.Length; ++i)
				{
					uint n = (uint)Math.Floor(x[i] / x[i - 1] * intToFloatScale);
					_segments[i] = new Segment(n, (float)(x[i - 1] / intToFloatScale));
					_y[i] = (float)f(x[i]);
				}
			}

			private static double CalculateTableError(double r, int segmentCount, Func<double, double> f, Func<double, double> fInv, Func<double, double> fCDF, double totalArea, double[] x)
			{
				x[0] = r;
				x[segmentCount - 1] = 0d;
				double v = r * f(r) + totalArea - fCDF(r);
				double xPrev = r;
				for (int i = 1; i < segmentCount - 1; ++i)
				{
					x[i] = xPrev = fInv(v / xPrev + f(xPrev));
				}
				return v - xPrev * (f(0d) - f(xPrev));
			}

#if UNITY_EDITOR
			[UnityEditor.Callbacks.DidReloadScripts]
			private static void TestZiggurat()
			{
				var table = CreateNormal(8);
				Action<int> PrintSegment = (int i) => Debug.LogFormat("Segment[{4}] = {0}, {1:E8}, {2:E8}, {3:E8}", table._segments[i].n, table._segments[i].s, table._segments[i].n * table._segments[i].s, table._y[i], i);
				PrintSegment(0);
				PrintSegment(1);
				PrintSegment(2);
				PrintSegment(3);
				PrintSegment(64);
				PrintSegment(254);
				PrintSegment(255);
			}
#endif
		}

		#endregion
	}
}
