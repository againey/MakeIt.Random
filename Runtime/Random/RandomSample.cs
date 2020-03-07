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

using UnityEngine;
using System;

namespace MakeIt.Random
{
	/// <summary>
	/// An interface for any generator of numeric data sampled according to a probability distribution, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	/// <typeparam name="TNumber">The numeric type returned by the sample generator.</typeparam>
	public interface ISampleGenerator<TNumber>
	{
		/// <summary>
		/// Get the next number produced by the generator.
		/// </summary>
		/// <returns>The next number in the sequence determined by the generator implementation.</returns>
		TNumber Next();
	}

	/// <summary>
	/// A static class of extension methods for generating random numbers according to various probability distributions.
	/// </summary>
	public static class RandomSample
	{
		#region Uniform Distribution

		/// <summary>
		/// Returns a random value sampled from a uniform probability distribution with the given range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower inclusive bound of the probability distribution's range.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The upper inclusive bound of the probability distribution's range.  Must be strictly greater than <paramref name="x0"/>.</param>
		/// <returns>A random value from within the given range.</returns>
		/// <remarks>
		/// <note type="note"><para>There is a small amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeUniformSampleGenerator(IRandom, float, float)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float UniformSample(this IRandom random, float x0, float x1)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
#endif

			return random.RangeCC(x0, x1);
		}

		private class FloatUniformSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _x0;
			private float _range;

			public FloatUniformSampleGenerator(IRandom random, float x0, float x1)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");

				_random = random;
				_x0 = x0;
				_range = x1 - x0;
			}

			public float Next()
			{
				return _random.FloatCC() * _range + _x0;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a uniform probability distribution with the given range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower inclusive bound of the probability distribution's range.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The upper inclusive bound of the probability distribution's range.  Must be strictly greater than <paramref name="x0"/>.</param>
		/// <returns>A sample generator producing random values from within the given range.</returns>
		public static ISampleGenerator<float> MakeUniformSampleGenerator(this IRandom random, float x0, float x1)
		{
			return new FloatUniformSampleGenerator(random, x0, x1);
		}

		/// <summary>
		/// Returns a random value sampled from a uniform probability distribution with the given range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower inclusive bound of the probability distribution's range.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The upper inclusive bound of the probability distribution's range.  Must be strictly greater than <paramref name="x0"/>.</param>
		/// <returns>A random value from within the given range.</returns>
		/// <remarks>
		/// <note type="note"><para>There is a small amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeUniformSampleGenerator(IRandom, double, double)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double UniformSample(this IRandom random, double x0, double x1)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
#endif

			return random.RangeCC(x0, x1);
		}

		private class DoubleUniformSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _x0;
			private double _range;

			public DoubleUniformSampleGenerator(IRandom random, double x0, double x1)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");

				_random = random;
				_x0 = x0;
				_range = x1 - x0;
			}

			public double Next()
			{
				return _random.DoubleCC() * _range + _x0;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower inclusive bound of the probability distribution's range.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The upper inclusive bound of the probability distribution's range.  Must be strictly greater than <paramref name="x0"/>.</param>
		/// <returns>A sample generator producing random values from within the given range.</returns>
		public static ISampleGenerator<double> MakeUniformSampleGenerator(this IRandom random, double x0, double x1)
		{
			return new DoubleUniformSampleGenerator(random, x0, x1);
		}

		#endregion

		#region Triangular Distribution

		/// <summary>
		/// Returns a random value sampled from a triangular probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The most common value within the probability distribution.  This is the x value of the triangular PDF's peak.  Must be strictly greater than <paramref name="x0"/> and strictly less than <paramref name="x2"/>.</param>
		/// <param name="x2">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <returns>A random value from within the given triangular distribution.</returns>
		/// <remarks>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeTriangularSampleGenerator(IRandom, float, float, float)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float TriangularSample(this IRandom random, float x0, float x1, float x2)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The mode must be greater than the lower range boundary.");
			if (x1 >= x2) throw new ArgumentOutOfRangeException("x2", x2, "The upper range boundary must be greater than the mode boundary.");
#endif

			float n = random.FloatOO();
			float range = x2 - x0;
			float lowerRange = x1 - x0;
			float split = lowerRange / range;
			return n < split ? x0 + Mathf.Sqrt(n * range * lowerRange) : x2 - Mathf.Sqrt((1f - n) * range * (x2 - x1));
		}

		private class FloatTriangularSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _split;
			private float _x0;
			private float _x1;
			private float _rangeLowerRange;
			private float _rangeUpperRange;

			public FloatTriangularSampleGenerator(IRandom random, float x0, float x1, float x2)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The mode must be greater than the lower range boundary.");
				if (x1 >= x2) throw new ArgumentOutOfRangeException("x2", x2, "The upper range boundary must be greater than the mode boundary.");

				_random = random;

				float range = x2 - x0;
				float lowerRange = x1 - x0;
				float upperRange = x2 - x1;

				_x0 = x0;
				_x1 = x2;
				_rangeLowerRange = range * lowerRange;
				_rangeUpperRange = range * upperRange;
				_split = lowerRange / range;
			}

			public float Next()
			{
				float n = _random.FloatOO();
				return n < _split ? _x0 + Mathf.Sqrt(n * _rangeLowerRange) : _x1 - Mathf.Sqrt((1f - n) * _rangeUpperRange);
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a triangular probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The most common value within the probability distribution.  This is the x value of the triangular PDF's peak.  Must be strictly greater than <paramref name="x0"/> and strictly less than <paramref name="x2"/>.</param>
		/// <param name="x2">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <returns>A sample generator producing random values from within the given triangular distribution.</returns>
		public static ISampleGenerator<float> MakeTriangularSampleGenerator(this IRandom random, float x0, float x1, float x2)
		{
			return new FloatTriangularSampleGenerator(random, x0, x1, x2);
		}

		/// <summary>
		/// Returns a random value sampled from a triangular probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The most common value within the probability distribution.  This is the x value of the triangular PDF's peak.  Must be strictly greater than <paramref name="x0"/> and strictly less than <paramref name="x2"/>.</param>
		/// <param name="x2">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <returns>A random value from within the given triangular distribution.</returns>
		/// <remarks>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeTriangularSampleGenerator(IRandom, double, double, double)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double TriangularSample(this IRandom random, double x0, double x1, double x2)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The mode must be greater than the lower range boundary.");
			if (x1 >= x2) throw new ArgumentOutOfRangeException("x2", x2, "The upper range boundary must be greater than the mode boundary.");
#endif

			double n = random.DoubleOO();
			double range = x2 - x0;
			double lowerRange = x1 - x0;
			double split = lowerRange / range;
			return n < split ? x0 + Math.Sqrt(n * range * lowerRange) : x2 - Math.Sqrt((1d - n) * range * (x2 - x1));
		}

		private class DoubleTriangularSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _split;
			private double _x0;
			private double _x2;
			private double _rangeLowerRange;
			private double _rangeUpperRange;

			public DoubleTriangularSampleGenerator(IRandom random, double x0, double x1, double x2)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The mode must be greater than the lower range boundary.");
				if (x1 >= x2) throw new ArgumentOutOfRangeException("x2", x2, "The upper range boundary must be greater than the mode boundary.");

				_random = random;

				double range = x2 - x0;
				double lowerRange = x1 - x0;
				double upperRange = x2 - x1;

				_x0 = x0;
				_x2 = x2;
				_rangeLowerRange = range * lowerRange;
				_rangeUpperRange = range * upperRange;
				_split = lowerRange / range;
			}

			public double Next()
			{
				double n = _random.DoubleOO();
				return n < _split ? _x0 + Math.Sqrt(n * _rangeLowerRange) : _x2 - Math.Sqrt((1d - n) * _rangeUpperRange);
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a triangular probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The most common value within the probability distribution.  This is the x value of the triangular PDF's peak.  Must be strictly greater than <paramref name="x0"/> and strictly less than <paramref name="x2"/>.</param>
		/// <param name="x2">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <returns>A sample generator producing random values from within the given triangular distribution.</returns>
		public static ISampleGenerator<double> MakeTriangularSampleGenerator(this IRandom random, double x0, double x1, double x2)
		{
			return new DoubleTriangularSampleGenerator(random, x0, x1, x2);
		}

		#endregion

		#region Trapezoidal Distribution

		/// <summary>
		/// Returns a random value sampled from a trapezoidal probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The lower bound of the most common range within the probability distribution.  This is the leftmost x value of the trapezoidal PDF's plateau.  Must be strictly greater than <paramref name="x0"/> and strictly less than <paramref name="x2"/>.</param>
		/// <param name="x2">The upper bound of the most common range within the probability distribution.  This is the rightmost x value of the trapezoidal PDF's plateau.  Must be strictly greater than <paramref name="x1"/> and strictly less than <paramref name="x3"/>.</param>
		/// <param name="x3">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x2"/>.</param>
		/// <returns>A random value from within the given trapezoidal distribution.</returns>
		/// <remarks>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeTrapezoidalSampleGenerator(IRandom, float, float, float, float)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float TrapezoidalSample(this IRandom random, float x0, float x1, float x2, float x3)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The lower mode boundary must be greater than the lower range boundary.");
			if (x1 >= x2) throw new ArgumentOutOfRangeException("x2", x2, "The upper mode boundary must be greater than the lower mode boundary.");
			if (x2 >= x3) throw new ArgumentOutOfRangeException("x3", x3, "The upper range boundary must be greater than the upper mode boundary.");
#endif

			float n = random.FloatOO();

			float range = x3 + x2 - x1 - x0;

			float lowerRange = x1 - x0;
			float lowerSplit = lowerRange / range;
			if (n < lowerSplit) return x0 + Mathf.Sqrt(n * range * lowerRange); // Within lower triangle.

			float midRange = x2 - x1;
			float upperSplit = (midRange + midRange + lowerRange) / range;
			if (n > upperSplit) return x3 - Mathf.Sqrt((1f - n) * range * (x3 - x2)); // Within upper triangle.

			return x1 + (n - lowerSplit) / (upperSplit - lowerSplit) * midRange; // Within middle rectangle.
		}

		private class FloatTrapezoidalSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _lowerSplit;
			private float _upperSplit;
			private float _x0;
			private float _x1;
			private float _x3;
			private float _rangeLowerRange;
			private float _rangeUpperRange;
			private float _modeScale;

			public FloatTrapezoidalSampleGenerator(IRandom random, float x0, float x1, float x2, float x3)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The lower mode boundary must be greater than the lower range boundary.");
				if (x1 >= x2) throw new ArgumentOutOfRangeException("x2", x2, "The upper mode boundary must be greater than the lower mode boundary.");
				if (x2 >= x3) throw new ArgumentOutOfRangeException("x3", x3, "The upper range boundary must be greater than the upper mode boundary.");

				_random = random;

				float range = x3 + x2 - x1 - x0;
				float lowerRange = x1 - x0;
				float midRange = x2 - x1;
				float upperRange = x3 - x2;

				_x0 = x0;
				_x1 = x1;
				_x3 = x3;
				_rangeLowerRange = range * lowerRange;
				_rangeUpperRange = range * upperRange;
				_lowerSplit = lowerRange / range;
				_upperSplit = (midRange + midRange + lowerRange) / range;
				_modeScale = midRange / (_upperSplit - _lowerSplit);
			}

			public float Next()
			{
				float n = _random.FloatOO();
				if (n < _lowerSplit) return _x0 + Mathf.Sqrt(n * _rangeLowerRange); // Within lower triangle.
				if (n > _upperSplit) return _x3 - Mathf.Sqrt((1f - n) * _rangeUpperRange); // Within upper triangle.
				return _x1 + (n - _lowerSplit) * _modeScale; // Within middle rectangle.
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a trapezoidal probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The lower bound of the most common range within the probability distribution.  This is the leftmost x value of the trapezoidal PDF's plateau.  Must be strictly greater than <paramref name="x0"/> and strictly less than <paramref name="x2"/>.</param>
		/// <param name="x2">The upper bound of the most common range within the probability distribution.  This is the rightmost x value of the trapezoidal PDF's plateau.  Must be strictly greater than <paramref name="x1"/> and strictly less than <paramref name="x3"/>.</param>
		/// <param name="x3">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x2"/>.</param>
		/// <returns>A sample generator producing random values from within the given trapezoidal distribution.</returns>
		public static ISampleGenerator<float> MakeTrapezoidalSampleGenerator(this IRandom random, float x0, float x1, float x2, float x3)
		{
			return new FloatTrapezoidalSampleGenerator(random, x0, x1, x2, x3);
		}

		/// <summary>
		/// Returns a random value sampled from a trapezoidal probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The lower bound of the most common range within the probability distribution.  This is the leftmost x value of the trapezoidal PDF's plateau.  Must be strictly greater than <paramref name="x0"/> and strictly less than <paramref name="x2"/>.</param>
		/// <param name="x2">The upper bound of the most common range within the probability distribution.  This is the rightmost x value of the trapezoidal PDF's plateau.  Must be strictly greater than <paramref name="x1"/> and strictly less than <paramref name="x3"/>.</param>
		/// <param name="x3">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x2"/>.</param>
		/// <returns>A random value from within the given trapezoidal distribution.</returns>
		/// <remarks>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeTrapezoidalSampleGenerator(IRandom, double, double, double, double)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double TrapezoidalSample(this IRandom random, double x0, double x1, double x2, double x3)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The lower mode boundary must be greater than the lower range boundary.");
			if (x1 >= x2) throw new ArgumentOutOfRangeException("x2", x2, "The upper mode boundary must be greater than the lower mode boundary.");
			if (x2 >= x3) throw new ArgumentOutOfRangeException("x3", x3, "The upper range boundary must be greater than the upper mode boundary.");
#endif

			double n = random.DoubleOO();

			double range = x3 + x2 - x1 - x0;

			double lowerRange = x1 - x0;
			double lowerSplit = lowerRange / range;
			if (n < lowerSplit) return x0 + Math.Sqrt(n * range * lowerRange); // Within lower triangle.

			double midRange = x2 - x1;
			double upperSplit = (midRange + midRange + lowerRange) / range;
			if (n > upperSplit) return x3 - Math.Sqrt((1d - n) * range * (x3 - x2)); // Within upper triangle.

			return x1 + (n - lowerSplit) / (upperSplit - lowerSplit) * midRange; // Within middle rectangle.
		}

		private class DoubleTrapezoidalSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _lowerSplit;
			private double _upperSplit;
			private double _x0;
			private double _x1;
			private double _x3;
			private double _rangeLowerRange;
			private double _rangeUpperRange;
			private double _modeScale;

			public DoubleTrapezoidalSampleGenerator(IRandom random, double x0, double x1, double x2, double x3)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The lower mode boundary must be greater than the lower range boundary.");
				if (x1 >= x2) throw new ArgumentOutOfRangeException("x2", x2, "The upper mode boundary must be greater than the lower mode boundary.");
				if (x2 >= x3) throw new ArgumentOutOfRangeException("x3", x3, "The upper range boundary must be greater than the upper mode boundary.");

				_random = random;

				double range = x3 + x2 - x1 - x0;
				double lowerRange = x1 - x0;
				double midRange = x2 - x1;
				double upperRange = x3 - x2;

				_x0 = x0;
				_x1 = x1;
				_x3 = x3;
				_rangeLowerRange = range * lowerRange;
				_rangeUpperRange = range * upperRange;
				_lowerSplit = lowerRange / range;
				_upperSplit = (midRange + midRange + lowerRange) / range;
				_modeScale = midRange / (_upperSplit - _lowerSplit);
			}

			public double Next()
			{
				double n = _random.DoubleOO();
				if (n < _lowerSplit) return _x0 + Math.Sqrt(n * _rangeLowerRange); // Within lower triangle.
				if (n > _upperSplit) return _x3 - Math.Sqrt((1d - n) * _rangeUpperRange); // Within upper triangle.
				return _x1 + (n - _lowerSplit) * _modeScale; // Within middle rectangle.
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a trapezoidal probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="x1">The lower bound of the most common range within the probability distribution.  This is the leftmost x value of the trapezoidal PDF's plateau.  Must be strictly greater than <paramref name="x0"/> and strictly less than <paramref name="x2"/>.</param>
		/// <param name="x2">The upper bound of the most common range within the probability distribution.  This is the rightmost x value of the trapezoidal PDF's plateau.  Must be strictly greater than <paramref name="x1"/> and strictly less than <paramref name="x3"/>.</param>
		/// <param name="x3">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x2"/>.</param>
		/// <returns>A sample generator producing random values from within the given trapezoidal distribution.</returns>
		public static ISampleGenerator<double> MakeTrapezoidalSampleGenerator(this IRandom random, double x0, double x1, double x2, double x3)
		{
			return new DoubleTrapezoidalSampleGenerator(random, x0, x1, x2, x3);
		}

		#endregion

		#region Linear Distribution

		/// <summary>
		/// Returns a random value sampled from a linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="y0">The weight of the probability distribution at the lower bound.  Must not be negative.  Can be 0, but not if <paramref name="y1"/> is 0.</param>
		/// <param name="x1">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <param name="y1">The weight of the probability distribution at the upper bound.  Must not be negative.  Can be 0, but not if <paramref name="y0"/> is 0.</param>
		/// <returns>A random value from within the given linear distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the line does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeLinearSampleGenerator(IRandom, float, float, float, float)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float LinearSample(this IRandom random, float x0, float y0, float x1, float y1)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
			if (y0 < 0f) throw new ArgumentOutOfRangeException("y0", y0, "The domain must be entirely non-negative.");
			if (y1 < 0f) throw new ArgumentOutOfRangeException("y1", y1, "The domain must be entirely non-negative.");
			if (y0 == 0f && y1 == 0f) throw new ArgumentException("The probability distribution must have a positive area.", "y1");
#endif

			return random.LinearSample(x0, y0, x1, y1, random.FloatCC());
		}

		/// <summary>
		/// Returns a random value sampled from a linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p0">The lower bound (x) and weight (y) of the probability distribution.  The x component must be strictly less than <paramref name="p1"/>.x.  The y component must not be negative; it can be 0, but not if <paramref name="p1"/>.y is 0.</param>
		/// <param name="p1">The upper bound (x) and weight (y) of the probability distribution.  The x component must be strictly greater than <paramref name="p0"/>.x.  The y component must not be negative; it can be 0, but not if <paramref name="p0"/>.y is 0.</param>
		/// <returns>A random value from within the given linear distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the line does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeLinearSampleGenerator(IRandom, Vector2, Vector2)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float LinearSample(this IRandom random, Vector2 p0, Vector2 p1)
		{
			return random.LinearSample(p0.x, p0.y, p1.x, p1.y);
		}

		private static float LinearSample(this IRandom random, float x0, float y0, float x1, float y1, float n)
		{
			float xDelta = x1 - x0;

			if (y0 == y1) return n * xDelta + x0;

			float yDelta = y1 - y0;
			float ySum = y0 + y1;
			float area = 0.5f * xDelta * ySum;

			float a = 0.5f * yDelta;
			float b = x1 * y0 - x0 * y1;
			float c = -(a * x0 + b) * x0 - area * xDelta * n;

			float sqrt = Mathf.Sqrt(b * b - 4f * a * c);
			if (b >= 0f)
			{
				return -2f * c / (b + sqrt); // Citardauq
			}
			else
			{
				return -0.5f * (b - sqrt) / a; // Quadratic
			}
		}

		private abstract class FloatLinearSampleGenerator : ISampleGenerator<float>
		{
			protected IRandom _random;
			protected float _a;
			protected float _aTimesFour;
			protected float _b;
			protected float _bSquared;
			protected float _c0;
			protected float _scaledArea;

			public static ISampleGenerator<float> Create(IRandom random, float x0, float y0, float x1, float y1)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (y0 < 0f) throw new ArgumentOutOfRangeException("y0", y0, "The domain must be entirely non-negative.");
				if (y1 < 0f) throw new ArgumentOutOfRangeException("y1", y1, "The domain must be entirely non-negative.");
				if (y0 == 0f && y1 == 0f) throw new ArgumentException("The probability distribution must have a positive area.", "y1");

				if (y0 == y1) return random.MakeUniformSampleGenerator(x0, x1);

				FloatLinearSampleGenerator generator;
				float b = x1 * y0 - x0 * y1;

				if (b >= 0f)
				{
					generator = new PositiveYInterceptFloatLinearSampleGenerator();
				}
				else
				{
					generator = new NegativeYInterceptFloatLinearSampleGenerator();
				}

				generator._random = random;

				float xDelta = x1 - x0;
				float yDelta = y1 - y0;
				float ySum = y0 + y1;
				float area = 0.5f * xDelta * ySum;

				generator._a = 0.5f * yDelta;
				generator._aTimesFour = yDelta * 2f;
				generator._b = b;
				generator._bSquared = generator._b * generator._b;
				generator._c0 = -(generator._a * x0 + generator._b) * x0;
				generator._scaledArea = area * xDelta;

				return generator;
			}

			public abstract float Next();
		}

		private class PositiveYInterceptFloatLinearSampleGenerator : FloatLinearSampleGenerator
		{
			public override float Next()
			{
				float c = _c0 - _scaledArea * _random.FloatCC();
				float sqrt = Mathf.Sqrt(_bSquared - _aTimesFour * c);
				return -2f * c / (_b + sqrt); // Citardauq
			}
		}

		private class NegativeYInterceptFloatLinearSampleGenerator : FloatLinearSampleGenerator
		{
			public override float Next()
			{
				float c = _c0 - _scaledArea * _random.FloatCC();
				float sqrt = Mathf.Sqrt(_bSquared - _aTimesFour * c);
				return -0.5f * (_b - sqrt) / _a; // Quadratic
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="y0">The weight of the probability distribution at the lower bound.  Must not be negative.  Can be 0, but not if <paramref name="y1"/> is 0.</param>
		/// <param name="x1">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <param name="y1">The weight of the probability distribution at the upper bound.  Must not be negative.  Can be 0, but not if <paramref name="y0"/> is 0.</param>
		/// <returns>A sample generator producing random values from within the given linear distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the line does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakeLinearSampleGenerator(this IRandom random, float x0, float y0, float x1, float y1)
		{
			return FloatLinearSampleGenerator.Create(random, x0, y0, x1, y1);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p0">The lower bound (x) and weight (y) of the probability distribution.  The x component must be strictly less than <paramref name="p1"/>.x.  The y component must not be negative; it can be 0, but not if <paramref name="p1"/>.y is 0.</param>
		/// <param name="p1">The upper bound (x) and weight (y) of the probability distribution.  The x component must be strictly greater than <paramref name="p0"/>.x.  The y component must not be negative; it can be 0, but not if <paramref name="p0"/>.y is 0.</param>
		/// <returns>A sample generator producing random values from within the given linear distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the line does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakeLinearSampleGenerator(this IRandom random, Vector2 p0, Vector2 p1)
		{
			return FloatLinearSampleGenerator.Create(random, p0.x, p0.y, p1.x, p1.y);
		}

		/// <summary>
		/// Returns a random value sampled from a linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="y0">The weight of the probability distribution at the lower bound.  Must not be negative.  Can be 0, but not if <paramref name="y1"/> is 0.</param>
		/// <param name="x1">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <param name="y1">The weight of the probability distribution at the upper bound.  Must not be negative.  Can be 0, but not if <paramref name="y0"/> is 0.</param>
		/// <returns>A random value from within the given linear distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the line does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeLinearSampleGenerator(IRandom, double, double, double, double)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double LinearSample(this IRandom random, double x0, double y0, double x1, double y1)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
			if (y0 < 0d) throw new ArgumentOutOfRangeException("y0", y0, "The domain must be entirely non-negative.");
			if (y1 < 0d) throw new ArgumentOutOfRangeException("y1", y1, "The domain must be entirely non-negative.");
			if (y0 == 0d && y1 == 0d) throw new ArgumentException("The probability distribution must have a positive area.", "y1");
#endif

			return random.LinearSample(x0, y0, x1, y1, random.DoubleCC());
		}

		private static double LinearSample(this IRandom random, double x0, double y0, double x1, double y1, double n)
		{
			double xDelta = x1 - x0;

			if (y0 == y1) return n * xDelta + x0;

			double yDelta = y1 - y0;
			double ySum = y0 + y1;
			double area = 0.5d * xDelta * ySum;

			double a = 0.5d * yDelta;
			double b = x1 * y0 - x0 * y1;
			double c = -(a * x0 + b) * x0 - area * xDelta * n;

			double sqrt = Math.Sqrt(b * b - 4d * a * c);
			if (b >= 0d)
			{
				return -2d * c / (b + sqrt); // Citardauq
			}
			else
			{
				return -0.5d * (b - sqrt) / a; // Quadratic
			}
		}

		private abstract class DoubleLinearSampleGenerator : ISampleGenerator<double>
		{
			protected IRandom _random;
			protected double _a;
			protected double _aTimesFour;
			protected double _b;
			protected double _bSquared;
			protected double _c0;
			protected double _scaledArea;

			public static ISampleGenerator<double> Create(IRandom random, double x0, double y0, double x1, double y1)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (y0 < 0d) throw new ArgumentOutOfRangeException("y0", y0, "The domain must be entirely non-negative.");
				if (y1 < 0d) throw new ArgumentOutOfRangeException("y1", y1, "The domain must be entirely non-negative.");
				if (y0 == 0d && y1 == 0d) throw new ArgumentException("The probability distribution must have a positive area.", "y1");

				if (y0 == y1) return random.MakeUniformSampleGenerator(x0, x1);

				DoubleLinearSampleGenerator generator;
				double b = x1 * y0 - x0 * y1;

				if (b >= 0d)
				{
					generator = new PositiveYInterceptDoubleLinearSampleGenerator();
				}
				else
				{
					generator = new NegativeYInterceptDoubleLinearSampleGenerator();
				}

				generator._random = random;

				double xDelta = x1 - x0;
				double yDelta = y1 - y0;
				double ySum = y0 + y1;
				double area = 0.5d * xDelta * ySum;

				generator._a = 0.5d * yDelta;
				generator._aTimesFour = yDelta * 2d;
				generator._b = b;
				generator._bSquared = generator._b * generator._b;
				generator._c0 = -(generator._a * x0 + generator._b) * x0;
				generator._scaledArea = area * xDelta;

				return generator;
			}

			public abstract double Next();
		}

		private class PositiveYInterceptDoubleLinearSampleGenerator : DoubleLinearSampleGenerator
		{
			public override double Next()
			{
				double c = _c0 - _scaledArea * _random.DoubleCC();
				double sqrt = Math.Sqrt(_bSquared - _aTimesFour * c);
				return -2d * c / (_b + sqrt); // Citardauq
			}
		}

		private class NegativeYInterceptDoubleLinearSampleGenerator : DoubleLinearSampleGenerator
		{
			public override double Next()
			{
				double c = _c0 - _scaledArea * _random.DoubleCC();
				double sqrt = Math.Sqrt(_bSquared - _aTimesFour * c);
				return -0.5d * (_b - sqrt) / _a; // Quadratic
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="y0">The weight of the probability distribution at the lower bound.  Must not be negative.  Can be 0, but not if <paramref name="y1"/> is 0.</param>
		/// <param name="x1">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <param name="y1">The weight of the probability distribution at the upper bound.  Must not be negative.  Can be 0, but not if <paramref name="y0"/> is 0.</param>
		/// <returns>A sample generator producing random values from within the given linear distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the line does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// </remarks>
		public static ISampleGenerator<double> MakeLinearSampleGenerator(this IRandom random, double x0, double y0, double x1, double y1)
		{
			return DoubleLinearSampleGenerator.Create(random, x0, y0, x1, y1);
		}

		#endregion

		#region Hermite Spline Distribution

		private static void CalculateHermiteSplineCDFCoefficients(float x0, float y0, float m0, float x1, float y1, float m1, out float k4, out float k3, out float k2, out float k1, out float area)
		{
			float xDelta = x1 - x0;
			float yDelta = y1 - y0;

			float a = -2f * yDelta + (m0 + m1) * xDelta;
			float b = 3f * yDelta - (2f * m0 + m1) * xDelta;
			float c = m0 * xDelta;
			float d = y0;

			k4 = a / 4f;
			k3 = b / 3f;
			k2 = c / 2f;
			k1 = d;
			area = k4 + k3 + k2 + k1;
		}

		private static float FindRoot(float k4, float k3, float k2, float k1, float area, float t)
		{
			float x = t;
			float k0 = -area * t;
			float k4t4 = 4f * k4;
			float k3t3 = 3f * k3;
			float k2t2 = 2f * k2;
			float k4t12 = 12f * k4;
			float k3t6 = 6f * k3;

			for (int i = 0; i < 32; ++i)
			{
				float f0 = (((k4 * x + k3) * x + k2) * x + k1) * x + k0;
				float f1 = ((k4t4 * x + k3t3) * x + k2t2) * x + k1;
				float f2 = (k4t12 * x + k3t6) * x + k2t2;
				float d = 2f * f1 * f1 - f0 * f2;
				if (d == 0f) return x;
				float n = 2f * f0 * f1;
				float x1 = x - n / d;
				if (Mathf.Abs(x1 - x) < 5.9604644775390625E-8f) return x1;
				x = x1;
			}

			return x;
		}

		/// <summary>
		/// Returns a random value sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="y0">The weight of the probability distribution at the lower bound.  Must not be negative.  Can be 0, but not if <paramref name="y1"/> is 0.</param>
		/// <param name="m0">The slope of the probability distribution at the lower bound.</param>
		/// <param name="x1">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <param name="y1">The weight of the probability distribution at the upper bound.  Must not be negative.  Can be 0, but not if <paramref name="y0"/> is 0.</param>
		/// <param name="m1">The slope of the probability distribution at the upper bound.</param>
		/// <returns>A random value from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeHermiteSplineSampleGenerator(IRandom, float, float, float, float, float, float)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float HermiteSplineSample(this IRandom random, float x0, float y0, float m0, float x1, float y1, float m1)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
			if (y0 < 0f) throw new ArgumentOutOfRangeException("y0", y0, "The domain must be entirely non-negative.");
			if (y1 < 0f) throw new ArgumentOutOfRangeException("y1", y1, "The domain must be entirely non-negative.");
			if (y0 == 0f && m0 < 0f) throw new ArgumentOutOfRangeException("m0", m0, "The domain must be entirely non-negative.");
			if (y1 == 0f && m1 > 0f) throw new ArgumentOutOfRangeException("m1", m1, "The domain must be entirely non-negative.");
			if (y0 == 0f && m0 == 0f && y1 == 0f && m1 == 0f) throw new ArgumentException("The area under the spline must be positive.", "m1");
#endif

			return random.HermiteSplineSample(x0, y0, m0, x1, y1, m1, random.FloatCC());
		}

		/// <summary>
		/// Returns a random value sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p0">The lower bound (x) and weight (y) of the probability distribution.  The x component must be strictly less than <paramref name="p1"/>.x.  The y component must not be negative; it can be 0, but not if <paramref name="p1"/>.y is 0.</param>
		/// <param name="m0">The slope of the probability distribution at the lower bound.</param>
		/// <param name="p1">The upper bound (x) and weight (y) of the probability distribution.  The x component must be strictly greater than <paramref name="p0"/>.x.  The y component must not be negative; it can be 0, but not if <paramref name="p0"/>.y is 0.</param>
		/// <param name="m1">The slope of the probability distribution at the upper bound.</param>
		/// <returns>A random value from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeHermiteSplineSampleGenerator(IRandom, Vector2, float, Vector2, float)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float HermiteSplineSample(this IRandom random, Vector2 p0, float m0, Vector2 p1, float m1)
		{
			return random.HermiteSplineSample(p0.x, p0.y, m0, p1.x, p1.y, m1);
		}

		/// <summary>
		/// Returns a random value sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="kf0">The key frame at the lower bound of the probability distribution.  The time component must be strictly less than <paramref name="kf1"/>.time.  The value component must not be negative; it can be 0, but not if <paramref name="kf1"/>.value is 0.</param>
		/// <param name="kf1">The key frame at the upper bound of the probability distribution.  The time component must be strictly greater than <paramref name="kf0"/>.time.  The value component must not be negative; it can be 0, but not if <paramref name="kf0"/>.value is 0.</param>
		/// <returns>A random value from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeHermiteSplineSampleGenerator(IRandom, Keyframe, Keyframe)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float HermiteSplineSample(this IRandom random, Keyframe kf0, Keyframe kf1)
		{
			return random.HermiteSplineSample(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent);
		}

		/// <summary>
		/// Returns a random value sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="curve">The curve describing the bounds, weights, and slopes of the Hermite spline that defines the probability distribution function.</param>
		/// <param name="segmentIndex">The segment of the provided <paramref name="curve"/> to use.  Must be less than <paramref name="curve"/>.length - 1.  Defaults to the first segment, index 0.</param>
		/// <returns>A random value from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeHermiteSplineSampleGenerator(IRandom, AnimationCurve, int)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float HermiteSplineSample(this IRandom random, AnimationCurve curve, int segmentIndex = 0)
		{
			var kf0 = curve[segmentIndex];
			var kf1 = curve[segmentIndex + 1];
			return random.HermiteSplineSample(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent);
		}

		private static float HermiteSplineSample(this IRandom random, float x0, float y0, float m0, float x1, float y1, float m1, float t)
		{
			float k4, k3, k2, k1, area;
			CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);

			return FindRoot(k4, k3, k2, k1, area, t) * (x1 - x0) + x0;
		}

		private class FloatHermiteSplineSampleGenerator : ISampleGenerator<float>
		{
			protected IRandom _random;
			protected float _xDelta;
			protected float _x0;
			protected float _k4;
			protected float _k3;
			protected float _k2;
			protected float _k1;
			protected float _area;

			public static ISampleGenerator<float> Create(IRandom random, float x0, float y0, float m0, float x1, float y1, float m1)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (y0 < 0f) throw new ArgumentOutOfRangeException("y0", y0, "The domain must be entirely non-negative.");
				if (y1 < 0f) throw new ArgumentOutOfRangeException("y1", y1, "The domain must be entirely non-negative.");
				if (y0 == 0f && m0 < 0f) throw new ArgumentOutOfRangeException("m0", m0, "The domain must be entirely non-negative.");
				if (y1 == 0f && m1 > 0f) throw new ArgumentOutOfRangeException("m1", m1, "The domain must be entirely non-negative.");
				if (y0 == 0f && m0 == 0f && y1 == 0f && m1 == 0f) throw new ArgumentException("The area under the spline must be positive.", "m1");

				float k4, k3, k2, k1, area;
				CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);

				if (k4 == 0f && k3 == 0f) return random.MakeLinearSampleGenerator(x0, y0, x1, y1);

				var generator = new FloatHermiteSplineSampleGenerator();
				generator._random = random;
				generator._x0 = x0;
				generator._xDelta = x1 - x0;
				generator._k4 = k4;
				generator._k3 = k3;
				generator._k2 = k2;
				generator._k1 = k1;
				generator._area = k4 + k3 + k2 + k1;
				
				return generator;
			}

			public float Next()
			{
				return FindRoot(_k4, _k3, _k2, _k1, _area, _random.FloatCC()) * _xDelta + _x0;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="y0">The weight of the probability distribution at the lower bound.  Must not be negative.  Can be 0, but not if <paramref name="y1"/> is 0.</param>
		/// <param name="m0">The slope of the probability distribution at the lower bound.</param>
		/// <param name="x1">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <param name="y1">The weight of the probability distribution at the upper bound.  Must not be negative.  Can be 0, but not if <paramref name="y0"/> is 0.</param>
		/// <param name="m1">The slope of the probability distribution at the upper bound.</param>
		/// <returns>A sample generator producing random values from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakeHermiteSplineSampleGenerator(this IRandom random, float x0, float y0, float m0, float x1, float y1, float m1)
		{
			return FloatHermiteSplineSampleGenerator.Create(random, x0, y0, m0, x1, y1, m1);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p0">The lower bound (x) and weight (y) of the probability distribution.  The x component must be strictly less than <paramref name="p1"/>.x.  The y component must not be negative; it can be 0, but not if <paramref name="p1"/>.y is 0.</param>
		/// <param name="m0">The slope of the probability distribution at the lower bound.</param>
		/// <param name="p1">The upper bound (x) and weight (y) of the probability distribution.  The x component must be strictly greater than <paramref name="p0"/>.x.  The y component must not be negative; it can be 0, but not if <paramref name="p0"/>.y is 0.</param>
		/// <param name="m1">The slope of the probability distribution at the upper bound.</param>
		/// <returns>A sample generator producing random values from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakeHermiteSplineSampleGenerator(this IRandom random, Vector2 p0, float m0, Vector2 p1, float m1)
		{
			return FloatHermiteSplineSampleGenerator.Create(random, p0.x, p0.y, m0, p1.x, p1.y, m1);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="kf0">The key frame at the lower bound of the probability distribution.  The time component must be strictly less than <paramref name="kf1"/>.time.  The value component must not be negative; it can be 0, but not if <paramref name="kf1"/>.value is 0.</param>
		/// <param name="kf1">The key frame at the upper bound of the probability distribution.  The time component must be strictly greater than <paramref name="kf0"/>.time.  The value component must not be negative; it can be 0, but not if <paramref name="kf0"/>.value is 0.</param>
		/// <returns>A sample generator producing random values from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakeHermiteSplineSampleGenerator(this IRandom random, Keyframe kf0, Keyframe kf1)
		{
			return FloatHermiteSplineSampleGenerator.Create(random, kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="curve">The curve describing the bounds, weights, and slopes of the Hermite spline that defines the probability distribution function.</param>
		/// <param name="segmentIndex">The segment of the provided <paramref name="curve"/> to use.  Must be less than <paramref name="curve"/>.length - 1.  Defaults to the first segment, index 0.</param>
		/// <returns>A sample generator producing random values from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakeHermiteSplineSampleGenerator(this IRandom random, AnimationCurve curve, int segmentIndex = 0)
		{
			var kf0 = curve[segmentIndex];
			var kf1 = curve[segmentIndex + 1];
			return FloatHermiteSplineSampleGenerator.Create(random, kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent);
		}

		private static void CalculateHermiteSplineCDFCoefficients(double x0, double y0, double m0, double x1, double y1, double m1, out double k4, out double k3, out double k2, out double k1, out double area)
		{
			double xDelta = x1 - x0;
			double yDelta = y1 - y0;

			double a = -2d * yDelta + (m0 + m1) * xDelta;
			double b = 3d * yDelta - (2d * m0 + m1) * xDelta;
			double c = m0 * xDelta;
			double d = y0;

			k4 = a / 4d;
			k3 = b / 3d;
			k2 = c / 2d;
			k1 = d;
			area = k4 + k3 + k2 + k1;
		}

		private static double FindRoot(double k4, double k3, double k2, double k1, double area, double t)
		{
			double x = t;
			double k0 = -area * t;
			double k4t4 = 4d * k4;
			double k3t3 = 3d * k3;
			double k2t2 = 2d * k2;
			double k4t12 = 12d * k4;
			double k3t6 = 6d * k3;

			for (int i = 0; i < 32; ++i)
			{
				double f0 = (((k4 * x + k3) * x + k2) * x + k1) * x + k0;
				double f1 = ((k4t4 * x + k3t3) * x + k2t2) * x + k1;
				double f2 = (k4t12 * x + k3t6) * x + k2t2;
				double d = 2d * f1 * f1 - f0 * f2;
				if (d == 0f) return x;
				double n = 2d * f0 * f1;
				double x1 = x - n / d;
				if (Math.Abs(x1 - x) < 2.3283064365386962890625E-10d) return x1;
				x = x1;
			}

			return x;
		}

		/// <summary>
		/// Returns a random value sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="y0">The weight of the probability distribution at the lower bound.  Must not be negative.  Can be 0, but not if <paramref name="y1"/> is 0.</param>
		/// <param name="m0">The slope of the probability distribution at the lower bound.</param>
		/// <param name="x1">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <param name="y1">The weight of the probability distribution at the upper bound.  Must not be negative.  Can be 0, but not if <paramref name="y0"/> is 0.</param>
		/// <param name="m1">The slope of the probability distribution at the upper bound.</param>
		/// <returns>A random value from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="note"><para>There is a moderate amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakeHermiteSplineSampleGenerator(IRandom, double, double, double, double, double, double)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double HermiteSplineSample(this IRandom random, double x0, double y0, double m0, double x1, double y1, double m1)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
			if (y0 < 0d) throw new ArgumentOutOfRangeException("y0", y0, "The domain must be entirely non-negative.");
			if (y1 < 0d) throw new ArgumentOutOfRangeException("y1", y1, "The domain must be entirely non-negative.");
			if (y0 == 0d && m0 < 0d) throw new ArgumentOutOfRangeException("m0", m0, "The domain must be entirely non-negative.");
			if (y1 == 0d && m1 > 0d) throw new ArgumentOutOfRangeException("m1", m1, "The domain must be entirely non-negative.");
			if (y0 == 0d && m0 == 0d && y1 == 0d && m1 == 0d) throw new ArgumentException("The area under the spline must be positive.", "m1");
#endif

			return random.HermiteSplineSample(x0, y0, m0, x1, y1, m1, random.DoubleCC());
		}

		private static double HermiteSplineSample(this IRandom random, double x0, double y0, double m0, double x1, double y1, double m1, double t)
		{
			double k4, k3, k2, k1, area;
			CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);

			return FindRoot(k4, k3, k2, k1, area, t) * (x1 - x0) + x0;
		}

		private class DoubleHermiteSplineSampleGenerator : ISampleGenerator<double>
		{
			protected IRandom _random;
			protected double _xDelta;
			protected double _x0;
			protected double _k4;
			protected double _k3;
			protected double _k2;
			protected double _k1;
			protected double _area;

			public static ISampleGenerator<double> Create(IRandom random, double x0, double y0, double m0, double x1, double y1, double m1)
			{
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x1", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (y0 < 0d) throw new ArgumentOutOfRangeException("y0", y0, "The domain must be entirely non-negative.");
				if (y1 < 0d) throw new ArgumentOutOfRangeException("y1", y1, "The domain must be entirely non-negative.");
				if (y0 == 0d && m0 < 0d) throw new ArgumentOutOfRangeException("m0", m0, "The domain must be entirely non-negative.");
				if (y1 == 0d && m1 > 0d) throw new ArgumentOutOfRangeException("m1", m1, "The domain must be entirely non-negative.");
				if (y0 == 0d && m0 == 0d && y1 == 0d && m1 == 0d) throw new ArgumentException("The area under the spline must be positive.", "m1");

				double k4, k3, k2, k1, area;
				CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);

				if (k4 == 0d && k3 == 0d) return random.MakeLinearSampleGenerator(x0, y0, x1, y1);

				var generator = new DoubleHermiteSplineSampleGenerator();
				generator._random = random;
				generator._x0 = x0;
				generator._xDelta = x1 - x0;
				generator._k4 = k4;
				generator._k3 = k3;
				generator._k2 = k2;
				generator._k1 = k1;
				generator._area = k4 + k3 + k2 + k1;
				
				return generator;
			}

			public double Next()
			{
				return FindRoot(_k4, _k3, _k2, _k1, _area, _random.DoubleCC()) * _xDelta + _x0;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x0">The lower bound of the probability distribution.  Must be strictly less than <paramref name="x1"/>.</param>
		/// <param name="y0">The weight of the probability distribution at the lower bound.  Must not be negative.  Can be 0, but not if <paramref name="y1"/> is 0.</param>
		/// <param name="m0">The slope of the probability distribution at the lower bound.</param>
		/// <param name="x1">The upper bound of the probability distribution.  Must be strictly greater than <paramref name="x1"/>.</param>
		/// <param name="y1">The weight of the probability distribution at the upper bound.  Must not be negative.  Can be 0, but not if <paramref name="y0"/> is 0.</param>
		/// <param name="m1">The slope of the probability distribution at the upper bound.</param>
		/// <returns>A sample generator producing random values from within the given Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// </remarks>
		public static ISampleGenerator<double> MakeHermiteSplineSampleGenerator(this IRandom random, double x0, double y0, double m0, double x1, double y1, double m1)
		{
			return DoubleHermiteSplineSampleGenerator.Create(random, x0, y0, m0, x1, y1, m1);
		}

		#endregion

		#region Normal Distribution

		/// <summary>
		/// Returns a random value sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <returns>A random value from within the given normal distribution.</returns>
		public static float NormalSample(this IRandom random, float mean, float standardDeviation)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (standardDeviation <= 0f) throw new ArgumentOutOfRangeException("standardDeviation", standardDeviation, "The standard deviation must be greater than zero.");
#endif

			return Detail.Distributions.NormalFloat.Sample(random, Detail.Distributions.NormalFloat.zigguratTable) * standardDeviation + mean;
		}

		/// <summary>
		/// Returns a random value sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="min">The minimum value to which the distribution will be constrained.  Must be less than or equal to the <paramref name="mean"/>, and together with <paramref name="max"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to the <paramref name="mean"/>, and together with <paramref name="min"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <returns>A random value from within the given normal distribution.</returns>
		public static float NormalSample(this IRandom random, float mean, float standardDeviation, float min, float max)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (standardDeviation <= 0f) throw new ArgumentOutOfRangeException("standardDeviation", standardDeviation, "The standard deviation must be greater than zero.");
			if (min > mean) throw new ArgumentOutOfRangeException("min", min, "The minimum value of the constrained range must not be greater than the mean.");
			if (max < mean) throw new ArgumentOutOfRangeException("max", max, "The maximum value of the constrained range must not be less than the mean.");
			if (max - min < standardDeviation) throw new ArgumentException("The constrained range must not be smaller than one standard deviation in size.", "max");
#endif

			float sample;
			do
			{
				sample = Detail.Distributions.NormalFloat.Sample(random, Detail.Distributions.NormalFloat.zigguratTable) * standardDeviation + mean;
			} while (sample < min || sample > max);

			return sample;
		}

		private class FloatNormalSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _mean;
			private float _standardDeviation;
			private Detail.Distributions.TwoSidedSymmetricFloatZigguratTable _zigguratTable;

			public FloatNormalSampleGenerator(IRandom random, float mean, float standardDeviation, Detail.Distributions.TwoSidedSymmetricFloatZigguratTable zigguratTable)
			{
				if (standardDeviation <= 0f) throw new ArgumentOutOfRangeException("standardDeviation", standardDeviation, "The standard deviation must be greater than zero.");

				_random = random;
				_mean = mean;
				_standardDeviation = standardDeviation;
				_zigguratTable = zigguratTable;
			}

			public float Next()
			{
				return Detail.Distributions.NormalFloat.Sample(_random, _zigguratTable) * _standardDeviation + _mean;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<float> MakeNormalSampleGenerator(this IRandom random, float mean, float standardDeviation)
		{
			return new FloatNormalSampleGenerator(random, mean, standardDeviation, Detail.Distributions.NormalFloat.zigguratTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="lookupTable">The pre-computed lookup table to use when applying the ziggurat algorithm for generating samples.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<float> MakeNormalSampleGenerator(this IRandom random, float mean, float standardDeviation, Detail.Distributions.TwoSidedSymmetricFloatZigguratTable lookupTable)
		{
			return new FloatNormalSampleGenerator(random, mean, standardDeviation, lookupTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="lookupTableSize">The size to use when pre-computing the lookup table to use when applying the ziggurat algorithm for generating samples.  Must be a power of 2..</param>
		/// <param name="epsilon">The precision required during the pre-computation of the lookup table.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<float> MakeNormalSampleGenerator(this IRandom random, float mean, float standardDeviation, int lookupTableSize, double epsilon = 0.0000000001d)
		{
			var lookupTableSizeMagnitude = Detail.DeBruijnLookup.GetBitCountForRangeSize(lookupTableSize);
			if (lookupTableSize != (1 << lookupTableSizeMagnitude)) throw new ArgumentException("Lookup table size must be an exact power of two.", "lookupTableSize");

			var lookupTable = Detail.Distributions.GenerateTwoSidedSymmetricFloatZigguratTable(
				lookupTableSizeMagnitude,
				Detail.Distributions.NormalDouble.F,
				Detail.Distributions.NormalDouble.Inv,
				Detail.Distributions.NormalDouble.CDF,
				Detail.Distributions.NormalDouble.totalArea,
				epsilon);

			return new FloatNormalSampleGenerator(random, mean, standardDeviation, lookupTable);
		}

		private class TruncatedFloatNormalSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _mean;
			private float _standardDeviation;
			private float _min;
			private float _max;
			private Detail.Distributions.TwoSidedSymmetricFloatZigguratTable _zigguratTable;

			public TruncatedFloatNormalSampleGenerator(IRandom random, float mean, float standardDeviation, float min, float max, Detail.Distributions.TwoSidedSymmetricFloatZigguratTable zigguratTable)
			{
				if (standardDeviation <= 0f) throw new ArgumentOutOfRangeException("standardDeviation", standardDeviation, "The standard deviation must be greater than zero.");
				if (min > mean) throw new ArgumentOutOfRangeException("min", min, "The minimum value of the constrained range must not be greater than the mean.");
				if (max < mean) throw new ArgumentOutOfRangeException("max", max, "The maximum value of the constrained range must not be less than the mean.");
				if (max - min < standardDeviation) throw new ArgumentException("The constrained range must not be smaller than one standard deviation in size.", "max");

				_random = random;
				_mean = mean;
				_min = min;
				_max = max;
				_standardDeviation = standardDeviation;
				_zigguratTable = zigguratTable;
			}

			public float Next()
			{
				float sample;
				do
				{
					sample = Detail.Distributions.NormalFloat.Sample(_random, _zigguratTable) * _standardDeviation + _mean;
				} while (sample < _min || sample > _max);

				return sample;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="min">The minimum value to which the distribution will be constrained.  Must be less than or equal to the <paramref name="mean"/>, and together with <paramref name="max"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to the <paramref name="mean"/>, and together with <paramref name="min"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<float> MakeNormalSampleGenerator(this IRandom random, float mean, float standardDeviation, float min, float max)
		{
			return new TruncatedFloatNormalSampleGenerator(random, mean, standardDeviation, min, max, Detail.Distributions.NormalFloat.zigguratTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="min">The minimum value to which the distribution will be constrained.  Must be less than or equal to the <paramref name="mean"/>, and together with <paramref name="max"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to the <paramref name="mean"/>, and together with <paramref name="min"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="lookupTable">The pre-computed lookup table to use when applying the ziggurat algorithm for generating samples.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<float> MakeNormalSampleGenerator(this IRandom random, float mean, float standardDeviation, float min, float max, Detail.Distributions.TwoSidedSymmetricFloatZigguratTable lookupTable)
		{
			return new TruncatedFloatNormalSampleGenerator(random, mean, standardDeviation, min, max, lookupTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="min">The minimum value to which the distribution will be constrained.  Must be less than or equal to the <paramref name="mean"/>, and together with <paramref name="max"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to the <paramref name="mean"/>, and together with <paramref name="min"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="lookupTableSize">The size to use when pre-computing the lookup table to use when applying the ziggurat algorithm for generating samples.  Must be a power of 2..</param>
		/// <param name="epsilon">The precision required during the pre-computation of the lookup table.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<float> MakeNormalSampleGenerator(this IRandom random, float mean, float standardDeviation, float min, float max, int lookupTableSize, double epsilon = 0.0000000001d)
		{
			var lookupTableSizeMagnitude = Detail.DeBruijnLookup.GetBitCountForRangeSize(lookupTableSize);
			if (lookupTableSize != (1 << lookupTableSizeMagnitude)) throw new ArgumentException("Lookup table size must be an exact power of two.", "lookupTableSize");

			var lookupTable = Detail.Distributions.GenerateTwoSidedSymmetricFloatZigguratTable(
				lookupTableSizeMagnitude,
				Detail.Distributions.NormalDouble.F,
				Detail.Distributions.NormalDouble.Inv,
				Detail.Distributions.NormalDouble.CDF,
				Detail.Distributions.NormalDouble.totalArea,
				epsilon);

			return new TruncatedFloatNormalSampleGenerator(random, mean, standardDeviation, min, max, lookupTable);
		}

		/// <summary>
		/// Returns a random value sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <returns>A random value from within the given normal distribution.</returns>
		public static double NormalSample(this IRandom random, double mean, double standardDeviation)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (standardDeviation <= 0d) throw new ArgumentOutOfRangeException("standardDeviation", standardDeviation, "The standard deviation must be greater than zero.");
#endif

			return Detail.Distributions.NormalDouble.Sample(random, Detail.Distributions.NormalDouble.zigguratTable) * standardDeviation + mean;
		}

		/// <summary>
		/// Returns a random value sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="min">The minimum value to which the distribution will be constrained.  Must be less than or equal to the <paramref name="mean"/>, and together with <paramref name="max"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to the <paramref name="mean"/>, and together with <paramref name="min"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <returns>A random value from within the given normal distribution.</returns>
		public static double NormalSample(this IRandom random, double mean, double standardDeviation, double min, double max)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (standardDeviation <= 0d) throw new ArgumentOutOfRangeException("standardDeviation", standardDeviation, "The standard deviation must be greater than zero.");
			if (min > mean) throw new ArgumentOutOfRangeException("min", min, "The minimum value of the constrained range must not be greater than the mean.");
			if (max < mean) throw new ArgumentOutOfRangeException("max", max, "The maximum value of the constrained range must not be less than the mean.");
			if (max - min < standardDeviation) throw new ArgumentException("The constrained range must not be smaller than one standard deviation in size.", "max");
#endif

			double sample;
			do
			{
				sample = Detail.Distributions.NormalDouble.Sample(random, Detail.Distributions.NormalDouble.zigguratTable) * standardDeviation + mean;
			} while (sample < min || sample > max);

			return sample;
		}

		private class DoubleNormalSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _mean;
			private double _standardDeviation;
			private Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable _zigguratTable;

			public DoubleNormalSampleGenerator(IRandom random, double mean, double standardDeviation, Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable zigguratTable)
			{
				if (standardDeviation <= 0d) throw new ArgumentOutOfRangeException("standardDeviation", standardDeviation, "The standard deviation must be greater than zero.");

				_random = random;
				_mean = mean;
				_standardDeviation = standardDeviation;
				_zigguratTable = zigguratTable;
			}

			public double Next()
			{
				return Detail.Distributions.NormalDouble.Sample(_random, _zigguratTable) * _standardDeviation + _mean;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<double> MakeNormalSampleGenerator(this IRandom random, double mean, double standardDeviation)
		{
			return new DoubleNormalSampleGenerator(random, mean, standardDeviation, Detail.Distributions.NormalDouble.zigguratTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="lookupTable">The pre-computed lookup table to use when applying the ziggurat algorithm for generating samples.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<double> MakeNormalSampleGenerator(this IRandom random, double mean, double standardDeviation, Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable lookupTable)
		{
			return new DoubleNormalSampleGenerator(random, mean, standardDeviation, lookupTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="lookupTableSize">The size to use when pre-computing the lookup table to use when applying the ziggurat algorithm for generating samples.  Must be a power of 2..</param>
		/// <param name="epsilon">The precision required during the pre-computation of the lookup table.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<double> MakeNormalSampleGenerator(this IRandom random, double mean, double standardDeviation, int lookupTableSize, double epsilon = 0.0000000001d)
		{
			var lookupTableSizeMagnitude = Detail.DeBruijnLookup.GetBitCountForRangeSize(lookupTableSize);
			if (lookupTableSize != (1 << lookupTableSizeMagnitude)) throw new ArgumentException("Lookup table size must be an exact power of two.", "lookupTableSize");

			var lookupTable = Detail.Distributions.GenerateTwoSidedSymmetricDoubleZigguratTable(
				lookupTableSizeMagnitude,
				Detail.Distributions.NormalDouble.F,
				Detail.Distributions.NormalDouble.Inv,
				Detail.Distributions.NormalDouble.CDF,
				Detail.Distributions.NormalDouble.totalArea,
				epsilon);

			return new DoubleNormalSampleGenerator(random, mean, standardDeviation, lookupTable);
		}

		private class TruncatedDoubleNormalSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _mean;
			private double _standardDeviation;
			private double _min;
			private double _max;
			private Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable _zigguratTable;

			public TruncatedDoubleNormalSampleGenerator(IRandom random, double mean, double standardDeviation, double min, double max, Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable zigguratTable)
			{
				if (standardDeviation <= 0d) throw new ArgumentOutOfRangeException("standardDeviation", standardDeviation, "The standard deviation must be greater than zero.");
				if (min > mean) throw new ArgumentOutOfRangeException("min", min, "The minimum value of the constrained range must not be greater than the mean.");
				if (max < mean) throw new ArgumentOutOfRangeException("max", max, "The maximum value of the constrained range must not be less than the mean.");
				if (max - min < standardDeviation) throw new ArgumentException("The constrained range must not be smaller than one standard deviation in size.", "max");

				_random = random;
				_mean = mean;
				_min = min;
				_max = max;
				_standardDeviation = standardDeviation;
				_zigguratTable = zigguratTable;
			}

			public double Next()
			{
				double sample;
				do
				{
					sample = Detail.Distributions.NormalDouble.Sample(_random, _zigguratTable) * _standardDeviation + _mean;
				} while (sample < _min || sample > _max);

				return sample;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="min">The minimum value to which the distribution will be constrained.  Must be less than or equal to the <paramref name="mean"/>, and together with <paramref name="max"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to the <paramref name="mean"/>, and together with <paramref name="min"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<double> MakeNormalSampleGenerator(this IRandom random, double mean, double standardDeviation, double min, double max)
		{
			return new TruncatedDoubleNormalSampleGenerator(random, mean, standardDeviation, min, max, Detail.Distributions.NormalDouble.zigguratTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="min">The minimum value to which the distribution will be constrained.  Must be less than or equal to the <paramref name="mean"/>, and together with <paramref name="max"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to the <paramref name="mean"/>, and together with <paramref name="min"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="lookupTable">The pre-computed lookup table to use when applying the ziggurat algorithm for generating samples.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<double> MakeNormalSampleGenerator(this IRandom random, double mean, double standardDeviation, double min, double max, Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable lookupTable)
		{
			return new TruncatedDoubleNormalSampleGenerator(random, mean, standardDeviation, min, max, lookupTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a normal (gaussian/bell curve) probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="mean">The mean (average) of the probability distribution.</param>
		/// <param name="standardDeviation">The standard deviation of the probability distribution.  Must be positive.</param>
		/// <param name="min">The minimum value to which the distribution will be constrained.  Must be less than or equal to the <paramref name="mean"/>, and together with <paramref name="max"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to the <paramref name="mean"/>, and together with <paramref name="min"/>, must cover a range at least one <paramref name="standardDeviation"/> in size.</param>
		/// <param name="lookupTableSize">The size to use when pre-computing the lookup table to use when applying the ziggurat algorithm for generating samples.  Must be a power of 2..</param>
		/// <param name="epsilon">The precision required during the pre-computation of the lookup table.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given normal distribution.</returns>
		public static ISampleGenerator<double> MakeNormalSampleGenerator(this IRandom random, double mean, double standardDeviation, double min, double max, int lookupTableSize, double epsilon = 0.0000000001d)
		{
			var lookupTableSizeMagnitude = Detail.DeBruijnLookup.GetBitCountForRangeSize(lookupTableSize);
			if (lookupTableSize != (1 << lookupTableSizeMagnitude)) throw new ArgumentException("Lookup table size must be an exact power of two.", "lookupTableSize");

			var lookupTable = Detail.Distributions.GenerateTwoSidedSymmetricDoubleZigguratTable(
				lookupTableSizeMagnitude,
				Detail.Distributions.NormalDouble.F,
				Detail.Distributions.NormalDouble.Inv,
				Detail.Distributions.NormalDouble.CDF,
				Detail.Distributions.NormalDouble.totalArea,
				epsilon);

			return new TruncatedDoubleNormalSampleGenerator(random, mean, standardDeviation, min, max, lookupTable);
		}

		#endregion

		#region Exponential Distribution

		/// <summary>
		/// Returns a random value sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <returns>A random value from within the given exponential distribution.</returns>
		public static float ExponentialSample(this IRandom random, float eventRate)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (eventRate <= 0f) throw new ArgumentOutOfRangeException("eventRate", eventRate, "The event rate must be greater than zero.");
#endif
			return Detail.Distributions.ExponentialFloat.Sample(random, Detail.Distributions.ExponentialFloat.zigguratTable) / eventRate;
		}

		/// <summary>
		/// Returns a random value sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to approximately 0.693 divided by <paramref name="eventRate"/>, guaranteeing that at least half of distribution is included.</param>
		/// <returns>A random value from within the given exponential distribution.</returns>
		public static float ExponentialSample(this IRandom random, float eventRate, float max)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (eventRate <= 0f) throw new ArgumentOutOfRangeException("eventRate", eventRate, "The event rate must be greater than zero.");
			if (max * eventRate < 0.69314718f) throw new ArgumentOutOfRangeException("max", max, "The constrained range must not be smaller than one half of the overall distribution.");
#endif

			float sample;
			do
			{
				sample = Detail.Distributions.ExponentialFloat.Sample(random, Detail.Distributions.ExponentialFloat.zigguratTable) / eventRate;
			} while (sample > max);

			return sample;
		}

		private class FloatExponentialSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _eventRate;
			private Detail.Distributions.OneSidedFloatZigguratTable _zigguratTable;

			public FloatExponentialSampleGenerator(IRandom random, float eventRate, Detail.Distributions.OneSidedFloatZigguratTable zigguratTable)
			{
				if (eventRate <= 0f) throw new ArgumentOutOfRangeException("eventRate", eventRate, "The event rate must be greater than zero.");

				_random = random;
				_eventRate = eventRate;
				_zigguratTable = zigguratTable;
			}

			public float Next()
			{
				return Detail.Distributions.ExponentialFloat.Sample(_random, _zigguratTable) / _eventRate;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<float> MakeExponentialSampleGenerator(this IRandom random, float eventRate)
		{
			return new FloatExponentialSampleGenerator(random, eventRate, Detail.Distributions.ExponentialFloat.zigguratTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="lookupTable">The pre-computed lookup table to use when applying the ziggurat algorithm for generating samples.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<float> MakeExponentialSampleGenerator(this IRandom random, float eventRate, Detail.Distributions.OneSidedFloatZigguratTable lookupTable)
		{
			return new FloatExponentialSampleGenerator(random, eventRate, lookupTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="lookupTableSize">The size to use when pre-computing the lookup table to use when applying the ziggurat algorithm for generating samples.  Must be a power of 2.</param>
		/// <param name="epsilon">The precision required during the pre-computation of the lookup table.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<float> MakeExponentialSampleGenerator(this IRandom random, float eventRate, int lookupTableSize, double epsilon = 0.0000000001d)
		{
			var lookupTableSizeMagnitude = Detail.DeBruijnLookup.GetBitCountForRangeSize(lookupTableSize);
			if (lookupTableSize != (1 << lookupTableSizeMagnitude)) throw new ArgumentException("Lookup table size must be an exact power of two.", "lookupTableSize");

			var lookupTable = Detail.Distributions.GenerateOneSidedFloatZigguratTable(
				lookupTableSizeMagnitude,
				Detail.Distributions.ExponentialDouble.F,
				Detail.Distributions.ExponentialDouble.Inv,
				Detail.Distributions.ExponentialDouble.CDF,
				Detail.Distributions.ExponentialDouble.totalArea,
				epsilon);

			return new FloatExponentialSampleGenerator(random, eventRate, lookupTable);
		}

		private class TruncatedFloatExponentialSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _eventRate;
			private float _max;
			private Detail.Distributions.OneSidedFloatZigguratTable _zigguratTable;

			public TruncatedFloatExponentialSampleGenerator(IRandom random, float eventRate, float max, Detail.Distributions.OneSidedFloatZigguratTable zigguratTable)
			{
				if (eventRate <= 0f) throw new ArgumentOutOfRangeException("eventRate", eventRate, "The event rate must be greater than zero.");
				if (max * eventRate < 0.69314718f) throw new ArgumentOutOfRangeException("max", max, "The constrained range must not be smaller than one half of the overall distribution.");

				_random = random;
				_eventRate = eventRate;
				_max = max;
				_zigguratTable = zigguratTable;
			}

			public float Next()
			{
				float sample;
				do
				{
					sample = Detail.Distributions.ExponentialFloat.Sample(_random, _zigguratTable) / _eventRate;
				} while (sample > _max);

				return sample;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="max">The maximum value of the truncated distribution.  Must be positive; recommended to be at least twice as large as eventRate.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<float> MakeExponentialSampleGenerator(this IRandom random, float eventRate, float max)
		{
			return new TruncatedFloatExponentialSampleGenerator(random, eventRate, max, Detail.Distributions.ExponentialFloat.zigguratTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="max">The maximum value of the truncated distribution.  Must be positive; recommended to be at least twice as large as eventRate.</param>
		/// <param name="lookupTable">The pre-computed lookup table to use when applying the ziggurat algorithm for generating samples.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<float> MakeExponentialSampleGenerator(this IRandom random, float eventRate, float max, Detail.Distributions.OneSidedFloatZigguratTable lookupTable)
		{
			return new TruncatedFloatExponentialSampleGenerator(random, eventRate, max, lookupTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="max">The maximum value of the truncated distribution.  Must be positive; recommended to be at least twice as large as eventRate.</param>
		/// <param name="lookupTableSize">The size to use when pre-computing the lookup table to use when applying the ziggurat algorithm for generating samples.  Must be a power of 2.</param>
		/// <param name="epsilon">The precision required during the pre-computation of the lookup table.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<float> MakeExponentialSampleGenerator(this IRandom random, float eventRate, float max, int lookupTableSize, double epsilon = 0.0000000001d)
		{
			var lookupTableSizeMagnitude = Detail.DeBruijnLookup.GetBitCountForRangeSize(lookupTableSize);
			if (lookupTableSize != (1 << lookupTableSizeMagnitude)) throw new ArgumentException("Lookup table size must be an exact power of two.", "lookupTableSize");

			var lookupTable = Detail.Distributions.GenerateOneSidedFloatZigguratTable(
				lookupTableSizeMagnitude,
				Detail.Distributions.ExponentialDouble.F,
				Detail.Distributions.ExponentialDouble.Inv,
				Detail.Distributions.ExponentialDouble.CDF,
				Detail.Distributions.ExponentialDouble.totalArea,
				epsilon);

			return new TruncatedFloatExponentialSampleGenerator(random, eventRate, max, lookupTable);
		}

		/// <summary>
		/// Returns a random value sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <returns>A random value from within the given exponential distribution.</returns>
		public static double ExponentialSample(this IRandom random, double eventRate)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (eventRate <= 0d) throw new ArgumentOutOfRangeException("eventRate", eventRate, "The event rate must be greater than zero.");
#endif
			return Detail.Distributions.ExponentialDouble.Sample(random, Detail.Distributions.ExponentialDouble.zigguratTable) / eventRate;
		}

		/// <summary>
		/// Returns a random value sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="max">The maximum value to which the distribution will be constrained.  Must be greater than or equal to approximately 0.693 divided by <paramref name="eventRate"/>, guaranteeing that at least half of distribution is included.</param>
		/// <returns>A random value from within the given exponential distribution.</returns>
		public static double ExponentialSample(this IRandom random, double eventRate, double max)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (eventRate <= 0d) throw new ArgumentOutOfRangeException("eventRate", eventRate, "The event rate must be greater than zero.");
			if (max * eventRate < 0.69314718055994531d) throw new ArgumentOutOfRangeException("max", max, "The constrained range must not be smaller than one half of the overall distribution.");
#endif

			double sample;
			do
			{
				sample = Detail.Distributions.ExponentialDouble.Sample(random, Detail.Distributions.ExponentialDouble.zigguratTable) / eventRate;
			} while (sample > max);

			return sample;
		}

		private class DoubleExponentialSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _eventRate;
			private Detail.Distributions.OneSidedDoubleZigguratTable _zigguratTable;

			public DoubleExponentialSampleGenerator(IRandom random, double eventRate, Detail.Distributions.OneSidedDoubleZigguratTable zigguratTable)
			{
				if (eventRate <= 0d) throw new ArgumentOutOfRangeException("eventRate", eventRate, "The event rate must be greater than zero.");

				_random = random;
				_eventRate = eventRate;
				_zigguratTable = zigguratTable;
			}

			public double Next()
			{
				return Detail.Distributions.ExponentialDouble.Sample(_random, _zigguratTable) / _eventRate;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<double> MakeExponentialSampleGenerator(this IRandom random, double eventRate)
		{
			return new DoubleExponentialSampleGenerator(random, eventRate, Detail.Distributions.ExponentialDouble.zigguratTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="lookupTable">The pre-computed lookup table to use when applying the ziggurat algorithm for generating samples.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<double> MakeExponentialSampleGenerator(this IRandom random, double eventRate, Detail.Distributions.OneSidedDoubleZigguratTable lookupTable)
		{
			return new DoubleExponentialSampleGenerator(random, eventRate, lookupTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="lookupTableSize">The size to use when pre-computing the lookup table to use when applying the ziggurat algorithm for generating samples.  Must be a power of 2.</param>
		/// <param name="epsilon">The precision required during the pre-computation of the lookup table.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<double> MakeExponentialSampleGenerator(this IRandom random, double eventRate, int lookupTableSize, double epsilon = 0.0000000001d)
		{
			var lookupTableSizeMagnitude = Detail.DeBruijnLookup.GetBitCountForRangeSize(lookupTableSize);
			if (lookupTableSize != (1 << lookupTableSizeMagnitude)) throw new ArgumentException("Lookup table size must be an exact power of two.", "lookupTableSize");

			var lookupTable = Detail.Distributions.GenerateOneSidedDoubleZigguratTable(
				lookupTableSizeMagnitude,
				Detail.Distributions.ExponentialDouble.F,
				Detail.Distributions.ExponentialDouble.Inv,
				Detail.Distributions.ExponentialDouble.CDF,
				Detail.Distributions.ExponentialDouble.totalArea,
				epsilon);

			return new DoubleExponentialSampleGenerator(random, eventRate, lookupTable);
		}

		private class TruncatedDoubleExponentialSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _eventRate;
			private double _max;
			private Detail.Distributions.OneSidedDoubleZigguratTable _zigguratTable;

			public TruncatedDoubleExponentialSampleGenerator(IRandom random, double eventRate, double max, Detail.Distributions.OneSidedDoubleZigguratTable zigguratTable)
			{
				if (eventRate <= 0d) throw new ArgumentOutOfRangeException("eventRate", eventRate, "The event rate must be greater than zero.");
				if (max * eventRate < 0.69314718055994531d) throw new ArgumentOutOfRangeException("max", max, "The constrained range must not be smaller than one half of the overall distribution.");

				_random = random;
				_eventRate = eventRate;
				_max = max;
				_zigguratTable = zigguratTable;
			}

			public double Next()
			{
				double sample;
				do
				{
					sample = Detail.Distributions.ExponentialDouble.Sample(_random, _zigguratTable) / _eventRate;
				} while (sample > _max);

				return sample;
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="max">The maximum value of the truncated distribution.  Must be positive; recommended to be at least twice as large as eventRate.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<double> MakeExponentialSampleGenerator(this IRandom random, double eventRate, double max)
		{
			return new TruncatedDoubleExponentialSampleGenerator(random, eventRate, max, Detail.Distributions.ExponentialDouble.zigguratTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="max">The maximum value of the truncated distribution.  Must be positive; recommended to be at least twice as large as eventRate.</param>
		/// <param name="lookupTable">The pre-computed lookup table to use when applying the ziggurat algorithm for generating samples.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<double> MakeExponentialSampleGenerator(this IRandom random, double eventRate, double max, Detail.Distributions.OneSidedDoubleZigguratTable lookupTable)
		{
			return new TruncatedDoubleExponentialSampleGenerator(random, eventRate, max, lookupTable);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a an exponential probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="eventRate">The event rate of the probability distribution.  Must be positive.</param>
		/// <param name="max">The maximum value of the truncated distribution.  Must be positive; recommended to be at least twice as large as eventRate.</param>
		/// <param name="lookupTableSize">The size to use when pre-computing the lookup table to use when applying the ziggurat algorithm for generating samples.  Must be a power of 2.</param>
		/// <param name="epsilon">The precision required during the pre-computation of the lookup table.  Must be positive.</param>
		/// <returns>A sample generator producing random values from within the given exponential distribution.</returns>
		public static ISampleGenerator<double> MakeExponentialSampleGenerator(this IRandom random, double eventRate, double max, int lookupTableSize, double epsilon = 0.0000000001d)
		{
			var lookupTableSizeMagnitude = Detail.DeBruijnLookup.GetBitCountForRangeSize(lookupTableSize);
			if (lookupTableSize != (1 << lookupTableSizeMagnitude)) throw new ArgumentException("Lookup table size must be an exact power of two.", "lookupTableSize");

			var lookupTable = Detail.Distributions.GenerateOneSidedDoubleZigguratTable(
				lookupTableSizeMagnitude,
				Detail.Distributions.ExponentialDouble.F,
				Detail.Distributions.ExponentialDouble.Inv,
				Detail.Distributions.ExponentialDouble.CDF,
				Detail.Distributions.ExponentialDouble.totalArea,
				epsilon);

			return new TruncatedDoubleExponentialSampleGenerator(random, eventRate, max, lookupTable);
		}

		#endregion

		#region Piecewise Utilities

		private static int BinarySearch(uint n, uint[] cdf)
		{
			int iLower = 0;
			int iUpper = cdf.Length;

			do
			{
				int iMid = (iLower + iUpper) >> 1;
				if (n < cdf[iMid])
				{
					iUpper = iMid;
				}
				else
				{
					iLower = iMid + 1;
				}
			} while (iLower < iUpper);

			return iLower;
		}

		private static int BinarySearch(ulong n, ulong[] cdf)
		{
			int iLower = 0;
			int iUpper = cdf.Length;

			do
			{
				int iMid = (iLower + iUpper) >> 1;
				if (n < cdf[iMid])
				{
					iUpper = iMid;
				}
				else
				{
					iLower = iMid + 1;
				}
			} while (iLower < iUpper);

			return iLower;
		}

		#endregion

		#region Piecewise Uniform Distribution

		/// <summary>
		/// Returns a random value sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The relative weights of the probability distribution pieces.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A random value from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly one more element in <paramref name="x"/> than in
		/// <paramref name="y"/>.  For any piece i, it will be bounded by x[i] and x[i + 1].</para>
		/// <para>The actual weight of each piece is determined by multiplying the relative weight
		/// of that piece by the width of that piece's range, producing an area for that piece.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseUniformSampleGenerator(IRandom, float[], float[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseUniformSample(this IRandom random, float[] x, float[] y)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
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
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (h < 0f) throw new ArgumentOutOfRangeException("y", h, "The relative weight of a segment must not be negative.");
#endif
				totalArea += (x1 - x0) * h;
				x0 = x1;
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (totalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "y");
#endif

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

		/// <summary>
		/// Returns a random value sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p">The lower range bounds (x) and relative weights (y) of the probability distribution pieces.  The x components must be in strictly increasing order.  The y components must all be non-negative, and at least one must be positive.</param>
		/// <param name="xLast">The upper range bound of the very last piece.  Must be strictly greater than the lower bound of the last piece.</param>
		/// <returns>A random value from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>The lower range bound of each piece serves as the upper bound of the next piece.</para>
		/// <para>The actual weight of each piece is determined by multiplying the relative weight
		/// of that piece by the width of that piece's range, producing an area for that piece.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseUniformSampleGenerator(IRandom, Vector2[], float)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseUniformSample(this IRandom random, Vector2[] p, float xLast)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (p.Length < 1) throw new ArgumentException("The array of vectors must have at least one element.", "p");
#endif

			float totalArea = 0f;
			Vector2 p0 = p[0];
			for (int i = 1; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (p0.x >= p1.x) throw new ArgumentOutOfRangeException("p", p1.x, "The upper range boundary must be greater than the lower range boundary.");
				if (p0.y < 0f) throw new ArgumentOutOfRangeException("p", p0.y, "The relative weight of a segment must not be negative.");
#endif
				totalArea += (p1.x - p0.x) * p0.y;
				p0 = p1;
			}
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (p0.x >= xLast) throw new ArgumentOutOfRangeException("xLast", xLast, "The upper range boundary must be greater than the lower range boundary.");
			if (p0.y < 0f) throw new ArgumentOutOfRangeException("p", p0.y, "The relative weight of a segment must not be negative.");
#endif
			totalArea += (xLast - p0.x) * p0.y;

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (totalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "p");
#endif

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

		/// <summary>
		/// Returns a random value sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="weights">The absolute weights of the probability distribution pieces.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A random value from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total weights of all the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly one more element in <paramref name="x"/> than in
		/// <paramref name="weights"/>.  For any piece i, it will be bounded by x[i] and
		/// x[i + 1].</para>
		/// <para>The weights provided are used directly, and are not adjusted by the width of
		/// width of piece ranges.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseWeightedUniformSampleGenerator(IRandom, float[], float[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseWeightedUniformSample(this IRandom random, float[] x, float[] weights)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length - weights.Length != 1) throw new ArgumentException("The array of weights must have exactly one fewer element than the array of x values.", "weights");
#endif

			float weightSum = 0f;
			for (int i = 0; i < weights.Length; ++i)
			{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (x[i] >= x[i + 1]) throw new ArgumentOutOfRangeException("x", x[i + 1], "The upper range boundary must be greater than the lower range boundary.");
				if (weights[i] < 0f) throw new ArgumentOutOfRangeException("weights", weights[i], "The weight of a segment must not be negative.");
#endif
				weightSum += weights[i];
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (weightSum <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "weights");
#endif

			float n = random.RangeCC(weightSum);
			for (int i = 0; i < weights.Length; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return random.RangeCO(x[i], x[i + 1]);
			}
			return x[weights.Length];
		}

		private class FloatPiecewiseUniformSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float[] _x;
			private uint[] _cdf;

			public static FloatPiecewiseUniformSampleGenerator FromPositions(IRandom random, float[] x, float[] y)
			{
				if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
				if (x.Length - y.Length != 1) throw new ArgumentException("The array of y values must have exactly one fewer element than the array of x values.", "y");

				var generator = new FloatPiecewiseUniformSampleGenerator();
				generator._random = random;

				generator._x = new float[x.Length];
				Array.Copy(x, generator._x, x.Length);

				double totalWeight = 0d;
				for (int i = 0; i < y.Length; ++i)
				{
					if (x[i] >= x[i + 1]) throw new ArgumentOutOfRangeException("x", x[i + 1], "The upper range boundary must be greater than the lower range boundary.");
					if (y[i] < 0f) throw new ArgumentOutOfRangeException("y", y[i], "The relative weight of a segment must not be negative.");

					totalWeight += (x[i + 1] - x[i]) * y[i];
				}

				if (totalWeight <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "y");

				double weightToIntScale = uint.MaxValue + 1d;
				totalWeight += 1d / weightToIntScale;

				generator._cdf = new uint[y.Length];

				double weightSum = 0d;
				for (int i = 0; i < y.Length; ++i)
				{
					weightSum += (x[i + 1] - x[i]) * y[i];
					generator._cdf[i] = (uint)Math.Floor(weightSum / totalWeight * weightToIntScale);
				}

				return generator;
			}

			public static FloatPiecewiseUniformSampleGenerator FromPositions(IRandom random, Vector2[] p, float xLast)
			{
				if (p.Length < 1) throw new ArgumentException("The array of vectors must have at least one element.", "p");

				var generator = new FloatPiecewiseUniformSampleGenerator();
				generator._random = random;

				generator._x = new float[p.Length + 1];

				double totalWeight = 0d;
				for (int i = 0; i < p.Length - 1; ++i)
				{
					if (p[i].x >= p[i + 1].x) throw new ArgumentOutOfRangeException("p", p[i + 1].x, "The upper range boundary must be greater than the lower range boundary.");
					if (p[i].y < 0f) throw new ArgumentOutOfRangeException("p", p[i].y, "The relative weight of a segment must not be negative.");

					generator._x[i] = p[i].x;
					totalWeight += (p[i + 1].x - p[i].x) * p[i].y;
				}

				if (p[p.Length - 1].x >= xLast) throw new ArgumentOutOfRangeException("p", p[p.Length - 1].x, "The upper range boundary must be greater than the lower range boundary.");
				if (p[p.Length - 1].y < 0f) throw new ArgumentOutOfRangeException("p", p[p.Length - 1].y, "The relative weight of a segment must not be negative.");

				generator._x[p.Length - 1] = p[p.Length - 1].x;
				totalWeight += (xLast - p[p.Length - 1].x) * p[p.Length - 1].y;

				generator._x[p.Length] = xLast;

				if (totalWeight <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "p");

				double weightToIntScale = uint.MaxValue + 1d;
				totalWeight += 1d / weightToIntScale;

				generator._cdf = new uint[p.Length];

				double weightSum = 0d;
				for (int i = 0; i < p.Length - 1; ++i)
				{
					weightSum += (p[i + 1].x - p[i].x) * p[i].y;
					generator._cdf[i] = (uint)Math.Floor(weightSum / totalWeight * weightToIntScale);
				}

				weightSum += (xLast - p[p.Length - 1].x) * p[p.Length - 1].y;
				generator._cdf[p.Length - 1] = (uint)Math.Floor(weightSum / totalWeight * weightToIntScale);

				return generator;
			}

			public static FloatPiecewiseUniformSampleGenerator FromWeights(IRandom random, float[] x, float[] weights)
			{
				if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
				if (x.Length - weights.Length != 1) throw new ArgumentException("The array of weights must have exactly one fewer element than the array of x values.", "weights");

				var generator = new FloatPiecewiseUniformSampleGenerator();
				generator._random = random;

				generator._x = new float[x.Length];
				Array.Copy(x, generator._x, x.Length);

				double totalWeight = 0d;
				for (int i = 0; i < weights.Length; ++i)
				{
					if (x[i] >= x[i + 1]) throw new ArgumentOutOfRangeException("x", x[i + 1], "The upper range boundary must be greater than the lower range boundary.");
					if (weights[i] < 0f) throw new ArgumentOutOfRangeException("weights", weights[i], "The relative weight of a segment must not be negative.");

					totalWeight += weights[i];
				}

				if (totalWeight <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "weights");

				double weightToIntScale = uint.MaxValue + 1d;
				totalWeight += 1d / weightToIntScale;

				generator._cdf = new uint[weights.Length];

				double weightSum = 0d;
				for (int i = 0; i < weights.Length; ++i)
				{
					weightSum += weights[i];
					generator._cdf[i] = (uint)Math.Floor(weightSum / totalWeight * weightToIntScale);
				}

				return generator;
			}

			public float Next()
			{
				int i = BinarySearch(_random.Next32(), _cdf);

				if (i < _cdf.Length)
				{
					return _random.RangeCO(_x[i], _x[i + 1]);
				}
				else
				{
					return _x[_cdf.Length];
				}
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The relative weights of the probability distribution pieces.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A sample generator producing random values from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly one more element in <paramref name="x"/> than in
		/// <paramref name="y"/>.  For any piece i, it will be bounded by x[i] and x[i + 1].</para>
		/// <para>The actual weight of each piece is determined by multiplying the relative weight
		/// of that piece by the width of that piece's range, producing an area for that piece.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseUniformSampleGenerator(this IRandom random, float[] x, float[] y)
		{
			return FloatPiecewiseUniformSampleGenerator.FromPositions(random, x, y);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p">The lower range bounds (x) and relative weights (y) of the probability distribution pieces.  The x components must be in strictly increasing order.  The y components must all be non-negative, and at least one must be positive.</param>
		/// <param name="xLast">The upper range bound of the very last piece.  Must be strictly greater than the lower bound of the last piece.</param>
		/// <returns>A sample generator producing random values from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>The lower range bound of each piece serves as the upper bound of the next piece.</para>
		/// <para>The actual weight of each piece is determined by multiplying the relative weight
		/// of that piece by the width of that piece's range, producing an area for that piece.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseUniformSampleGenerator(this IRandom random, Vector2[] p, float xLast)
		{
			return FloatPiecewiseUniformSampleGenerator.FromPositions(random, p, xLast);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="weights">The absolute weights of the probability distribution pieces.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A sample generator producing random values from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total weights of all the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly one more element in <paramref name="x"/> than in
		/// <paramref name="weights"/>.  For any piece i, it will be bounded by x[i] and
		/// x[i + 1].</para>
		/// <para>The weights provided are used directly, and are not adjusted by the width of
		/// width of piece ranges.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseWeightedUniformSampleGenerator(this IRandom random, float[] x, float[] weights)
		{
			return FloatPiecewiseUniformSampleGenerator.FromWeights(random, x, weights);
		}

		/// <summary>
		/// Returns a random value sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The relative weights of the probability distribution pieces.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A random value from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly one more element in <paramref name="x"/> than in
		/// <paramref name="y"/>.  For any piece i, it will be bounded by x[i] and x[i + 1].</para>
		/// <para>The actual weight of each piece is determined by multiplying the relative weight
		/// of that piece by the width of that piece's range, producing an area for that piece.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseUniformSampleGenerator(IRandom, double[], double[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double PiecewiseUniformSample(this IRandom random, double[] x, double[] y)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length - y.Length != 1) throw new ArgumentException("The array of y values must have exactly one fewer element than the array of x values.", "y");
#endif

			double totalArea = 0f;
			int i = 0;
			double x0 = x[0];
			while (i < y.Length)
			{
				double h = y[i];
				++i;
				double x1 = x[i];
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (h < 0d) throw new ArgumentOutOfRangeException("y", h, "The relative weight of a segment must not be negative.");
#endif
				totalArea += (x1 - x0) * h;
				x0 = x1;
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (totalArea <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "y");
#endif

			double n = random.RangeCC(totalArea);
			i = 0;
			x0 = x[0];
			while (i < y.Length)
			{
				double h = y[i];
				++i;
				double x1 = x[i];
				totalArea -= (x1 - x0) * h;
				if (totalArea < n) return random.RangeCO(x0, x1);
				x0 = x1;
			}
			return x0;
		}

		/// <summary>
		/// Returns a random value sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="weights">The absolute weights of the probability distribution pieces.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A random value from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total weights of all the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly one more element in <paramref name="x"/> than in
		/// <paramref name="weights"/>.  For any piece i, it will be bounded by x[i] and
		/// x[i + 1].</para>
		/// <para>The weights provided are used directly, and are not adjusted by the width of
		/// width of piece ranges.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseWeightedUniformSampleGenerator(IRandom, double[], double[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double PiecewiseWeightedUniformSample(this IRandom random, double[] x, double[] weights)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length - weights.Length != 1) throw new ArgumentException("The array of weights must have exactly one fewer element than the array of x values.", "weights");
#endif

			double weightSum = 0f;
			for (int i = 0; i < weights.Length; ++i)
			{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (x[i] >= x[i + 1]) throw new ArgumentOutOfRangeException("x", x[i + 1], "The upper range boundary must be greater than the lower range boundary.");
				if (weights[i] < 0d) throw new ArgumentOutOfRangeException("weights", weights[i], "The weight of a segment must not be negative.");
#endif
				weightSum += weights[i];
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (weightSum <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "weights");
#endif

			double n = random.RangeCC(weightSum);
			for (int i = 0; i < weights.Length; ++i)
			{
				weightSum -= weights[i];
				if (weightSum <= n) return random.RangeCO(x[i], x[i + 1]);
			}
			return x[weights.Length];
		}

		private class DoublePiecewiseUniformSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double[] _x;
			private ulong[] _cdf;

			public static DoublePiecewiseUniformSampleGenerator FromPositions(IRandom random, double[] x, double[] y)
			{
				if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
				if (x.Length - y.Length != 1) throw new ArgumentException("The array of y values must have exactly one fewer element than the array of x values.", "y");

				var generator = new DoublePiecewiseUniformSampleGenerator();
				generator._random = random;

				generator._x = new double[x.Length];
				Array.Copy(x, generator._x, x.Length);

				double totalWeight = 0d;
				for (int i = 0; i < y.Length; ++i)
				{
					if (x[i] >= x[i + 1]) throw new ArgumentOutOfRangeException("x", x[i + 1], "The upper range boundary must be greater than the lower range boundary.");
					if (y[i] < 0d) throw new ArgumentOutOfRangeException("y", y[i], "The relative weight of a segment must not be negative.");

					totalWeight += (x[i + 1] - x[i]) * y[i];
				}

				if (totalWeight <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "y");

				double weightToIntScale = 0xFFFFFFFFFFFFF800UL;

				generator._cdf = new ulong[y.Length];

				double weightSum = 0d;
				for (int i = 0; i < y.Length; ++i)
				{
					double weight = (x[i + 1] - x[i]) * y[i];
					weightSum += weight;
					generator._cdf[i] = (ulong)Math.Floor(weightSum / totalWeight * weightToIntScale);
				}

				ulong remainder = ulong.MaxValue - generator._cdf[y.Length - 1];
				for (int i = 0; i < y.Length - 1; ++i)
				{
					double weight = (x[i + 1] - x[i]) * y[i];
					ulong extra = (ulong)Math.Round(weight / weightSum * remainder);
					generator._cdf[i] += extra;
					remainder -= extra;
					weightSum -= weight;
				}
				generator._cdf[y.Length - 1] = ulong.MaxValue;

				return generator;
			}

			public static DoublePiecewiseUniformSampleGenerator FromWeights(IRandom random, double[] x, double[] weights)
			{
				if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
				if (x.Length - weights.Length != 1) throw new ArgumentException("The array of weights must have exactly one fewer element than the array of x values.", "weights");

				var generator = new DoublePiecewiseUniformSampleGenerator();
				generator._random = random;

				generator._x = new double[x.Length];
				Array.Copy(x, generator._x, x.Length);

				double totalWeight = 0d;
				for (int i = 0; i < weights.Length; ++i)
				{
					if (x[i] >= x[i + 1]) throw new ArgumentOutOfRangeException("x", x[i + 1], "The upper range boundary must be greater than the lower range boundary.");
					if (weights[i] < 0f) throw new ArgumentOutOfRangeException("weights", weights[i], "The relative weight of a segment must not be negative.");

					totalWeight += weights[i];
				}

				if (totalWeight <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "weights");

				double weightToIntScale = 0xFFFFFFFFFFFFF800UL;

				generator._cdf = new ulong[weights.Length];

				double weightSum = 0d;
				for (int i = 0; i < weights.Length; ++i)
				{
					weightSum += weights[i];
					generator._cdf[i] = (ulong)Math.Floor(weightSum / totalWeight * weightToIntScale);
				}

				ulong remainder = ulong.MaxValue - generator._cdf[weights.Length - 1];
				for (int i = 0; i < weights.Length - 1; ++i)
				{
					ulong extra = (ulong)Math.Round(weights[i] / weightSum * remainder);
					generator._cdf[i] += extra;
					remainder -= extra;
					weightSum -= weights[i];
				}
				generator._cdf[weights.Length - 1] = ulong.MaxValue;

				return generator;
			}

			public double Next()
			{
				int i = BinarySearch(_random.Next64(), _cdf);

				if (i < _cdf.Length)
				{
					return _random.RangeCO(_x[i], _x[i + 1]);
				}
				else
				{
					return _x[_cdf.Length];
				}
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The relative weights of the probability distribution pieces.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A sample generator producing random values from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly one more element in <paramref name="x"/> than in
		/// <paramref name="y"/>.  For any piece i, it will be bounded by x[i] and x[i + 1].</para>
		/// <para>The actual weight of each piece is determined by multiplying the relative weight
		/// of that piece by the width of that piece's range, producing an area for that piece.</para>
		/// </remarks>
		public static ISampleGenerator<double> MakePiecewiseUniformSampleGenerator(this IRandom random, double[] x, double[] y)
		{
			return DoublePiecewiseUniformSampleGenerator.FromPositions(random, x, y);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise uniform probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="weights">The absolute weights of the probability distribution pieces.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A sample generator producing random values from within the given piecewise uniform distribution.</returns>
		/// <remarks>
		/// <para>The total weights of all the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly one more element in <paramref name="x"/> than in
		/// <paramref name="weights"/>.  For any piece i, it will be bounded by x[i] and
		/// x[i + 1].</para>
		/// <para>The weights provided are used directly, and are not adjusted by the width of
		/// width of piece ranges.</para>
		/// </remarks>
		public static ISampleGenerator<double> MakePiecewiseWeightedUniformSampleGenerator(this IRandom random, double[] x, double[] weights)
		{
			return DoublePiecewiseUniformSampleGenerator.FromWeights(random, x, weights);
		}

		#endregion

		#region Piecewise Linear Distribution

		/// <summary>
		/// Returns a random value sampled from a piecewise linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The weights of the probability distribution at the range bounds.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A random value from within the given piecewise linear distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly the same number of elements in <paramref name="x"/> and
		/// <paramref name="y"/>.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseLinearSampleGenerator(IRandom, float[], float[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseLinearSample(this IRandom random, float[] x, float[] y)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
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
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (y0 < 0f) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
#endif
				doubleTotalArea += (x1 - x0) * (y0 + y1); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
				x0 = x1;
				y0 = y1;
			}
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (y[y.Length - 1] < 0f) throw new ArgumentOutOfRangeException("y", y[y.Length - 1], "The domain must be entirely non-negative.");
#endif

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (doubleTotalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "y");
#endif

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

		/// <summary>
		/// Returns a random value sampled from a piecewise linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p">The range bounds (x) and weights (y) of the probability distribution pieces.  The x components must be in strictly increasing order.  The y components must all be non-negative, and at least one must be positive.</param>
		/// <returns>A random value from within the given piecewise linear distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseLinearSampleGenerator(IRandom, Vector2[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseLinearSample(this IRandom random, Vector2[] p)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (p.Length < 2) throw new ArgumentException("The array of vectors must have at least two elements.", "p");
#endif

			float doubleTotalArea = 0f;
			Vector2 p0 = p[0];
			for (int i = 1; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (p0.x >= p1.x) throw new ArgumentOutOfRangeException("p", p1.x, "The upper range boundary must be greater than the lower range boundary.");
				if (p0.y < 0f) throw new ArgumentOutOfRangeException("p", p0.y, "The domain must be entirely non-negative.");
#endif
				doubleTotalArea += (p1.x - p0.x) * (p0.y + p1.y); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
				p0 = p1;
			}
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (p[p.Length - 1].y < 0f) throw new ArgumentOutOfRangeException("p", p[p.Length - 1].y, "The domain must be entirely non-negative.");
#endif

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (doubleTotalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "p");
#endif

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

		private class FloatPiecewiseLinearSampleGenerator : ISampleGenerator<float>
		{
			private struct SegmentData
			{
				public float a;
				public float aTimesFour;
				public float b;
				public float bSquared;
				public float c0;
				public float scaledArea;

				public SegmentData(float x0, float y0, float x1, float y1)
				{
					float xDelta = x1 - x0;
					float yDelta = y1 - y0;
					float ySum = y0 + y1;
					float area = 0.5f * xDelta * ySum;

					a = 0.5f * yDelta;
					aTimesFour = yDelta * 2f;
					b = x1 * y0 - x0 * y1;
					bSquared = b * b;
					c0 = -(a * x0 + b) * x0;
					scaledArea = area * xDelta;
				}
			}

			private IRandom _random;
			private SegmentData[] _segments;
			private uint[] _cdf;

			public FloatPiecewiseLinearSampleGenerator(IRandom random, float[] x, float[] y)
			{
				if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
				if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");

				_random = random;

				double doubleTotalArea = 0f;
				int segmentCount = 0;
				for (int i = 1; i < x.Length; ++i)
				{
					float x0 = x[i - 1];
					float y0 = y[i - 1];
					float x1 = x[i];
					float y1 = y[i];
					if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
					if (y0 < 0f) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
					float area = (x1 - x0) * (y0 + y1);
					if (area == 0f) continue;
					doubleTotalArea += (x1 - x0) * (y0 + y1); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
					++segmentCount;
				}
				if (y[y.Length - 1] < 0f) throw new ArgumentOutOfRangeException("y", y[y.Length - 1], "The domain must be entirely non-negative.");

				if (doubleTotalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "y");

				double areaToIntScale = uint.MaxValue + 1d;
				doubleTotalArea += 1d / areaToIntScale;

				_segments = new SegmentData[segmentCount];
				_cdf = new uint[segmentCount];

				double doubleAreaSum = 0d;
				for (int i = 1, j = 0; i < x.Length; ++i)
				{
					float x0 = x[i - 1];
					float y0 = y[i - 1];
					float x1 = x[i];
					float y1 = y[i];
					float area = (x1 - x0) * (y0 + y1);
					if (area == 0f) continue;
					doubleAreaSum += area;
					_cdf[j] = (uint)Math.Floor(doubleAreaSum / doubleTotalArea * areaToIntScale);
					_segments[j] = new SegmentData(x0, y0, x1, y1);
					++j;
				}
			}

			public FloatPiecewiseLinearSampleGenerator(IRandom random, Vector2[] p)
			{
				if (p.Length < 2) throw new ArgumentException("The array of vectors must have at least two elements.", "p");

				_random = random;

				double doubleTotalArea = 0f;
				int segmentCount = 0;
				for (int i = 1; i < p.Length; ++i)
				{
					Vector2 p0 = p[i - 1];
					Vector2 p1 = p[i];
					if (p0.x >= p1.x) throw new ArgumentOutOfRangeException("p", p1.x, "The upper range boundary must be greater than the lower range boundary.");
					if (p0.y < 0f) throw new ArgumentOutOfRangeException("p", p0.y, "The domain must be entirely non-negative.");
					float doubleArea = (p1.x - p0.x) * (p0.y + p1.y);
					if (doubleArea == 0f) continue;
					doubleTotalArea += doubleArea; // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
					++segmentCount;
				}
				if (p[p.Length - 1].y < 0f) throw new ArgumentOutOfRangeException("p", p[p.Length - 1].y, "The domain must be entirely non-negative.");

				if (doubleTotalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "p");

				double areaToIntScale = uint.MaxValue + 1d;
				doubleTotalArea += 1d / areaToIntScale;

				_segments = new SegmentData[segmentCount];
				_cdf = new uint[segmentCount];

				double doubleAreaSum = 0d;
				for (int i = 1, j = 0; i < p.Length; ++i)
				{
					Vector2 p0 = p[i - 1];
					Vector2 p1 = p[i];
					float doubleArea = (p1.x - p0.x) * (p0.y + p1.y);
					if (doubleArea == 0f) continue;
					doubleAreaSum += doubleArea;
					_segments[j] = new SegmentData(p0.x, p0.y, p1.x, p1.y);
					_cdf[j] = (uint)Math.Floor(doubleAreaSum / doubleTotalArea * areaToIntScale);
					++j;
				}
			}

			public float Next()
			{
				int i = BinarySearch(_random.Next32(), _cdf);

				if (i < _cdf.Length)
				{
					var segment = _segments[i];

					if (segment.a != 0f)
					{
						// A sloped segment.
						float c = segment.c0 - segment.scaledArea * _random.FloatCO();
						float sqrt = Mathf.Sqrt(segment.bSquared - segment.aTimesFour * c);
						if (segment.b >= 0f)
						{
							return -2f * c / (segment.b + sqrt); // Citardauq
						}
						else
						{
							return -0.5f * (segment.b - sqrt) / segment.a; // Quadratic
						}
					}
					else
					{
						// A flat segment.
						float c = segment.c0 - segment.scaledArea * _random.FloatCO();
						return -c / segment.b;
					}
				}
				else
				{
					var segment = _segments[_cdf.Length - 1];

					if (segment.a != 0f)
					{
						// A sloped segment.
						float sqrt = Mathf.Sqrt(segment.bSquared - segment.aTimesFour * segment.c0);
						if (segment.b >= 0f)
						{
							return -2f * segment.c0 / (segment.b + sqrt); // Citardauq
						}
						else
						{
							return -0.5f * (segment.b - sqrt) / segment.a; // Quadratic
						}
					}
					else
					{
						// A flat segment.
						return -segment.c0 / segment.b;
					}
				}
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The weights of the probability distribution at the range bounds.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A sample generator producing random values from within the given piecewise linear distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly the same number of elements in <paramref name="x"/> and
		/// <paramref name="y"/>.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseLinearSampleGenerator(this IRandom random, float[] x, float[] y)
		{
			return new FloatPiecewiseLinearSampleGenerator(random, x, y);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p">The range bounds (x) and weights (y) of the probability distribution pieces.  The x components must be in strictly increasing order.  The y components must all be non-negative, and at least one must be positive.</param>
		/// <returns>A sample generator producing random values from within the given piecewise linear distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseLinearSampleGenerator(this IRandom random, Vector2[] p)
		{
			return new FloatPiecewiseLinearSampleGenerator(random, p);
		}

		/// <summary>
		/// Returns a random value sampled from a piecewise linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The weights of the probability distribution at the range bounds.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A random value from within the given piecewise linear distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly the same number of elements in <paramref name="x"/> and
		/// <paramref name="y"/>.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseLinearSampleGenerator(IRandom, double[], double[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double PiecewiseLinearSample(this IRandom random, double[] x, double[] y)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");
#endif

			double doubleTotalArea = 0f;
			double x0 = x[0];
			double y0 = y[0];
			for (int i = 1; i < x.Length; ++i)
			{
				double x1 = x[i];
				double y1 = y[i];
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (y0 < 0d) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
#endif
				doubleTotalArea += (x1 - x0) * (y0 + y1); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
				x0 = x1;
				y0 = y1;
			}
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (y0 < 0d) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
#endif

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (doubleTotalArea <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "y");
#endif

			double n = random.RangeCC(doubleTotalArea);
			x0 = x[0];
			y0 = y[0];
			for (int i = 1; i < x.Length; ++i)
			{
				double x1 = x[i];
				double y1 = y[i];
				doubleTotalArea -= (x1 - x0) * (y0 + y1);
				if (doubleTotalArea < n) return random.LinearSample(x0, y0, x1, y1, random.DoubleCO());
				x0 = x1;
				y0 = y1;
			}
			return x0;
		}

		private class DoublePiecewiseLinearSampleGenerator : ISampleGenerator<double>
		{
			private struct SegmentData
			{
				public double a;
				public double aTimesFour;
				public double b;
				public double bSquared;
				public double c0;
				public double scaledArea;

				public SegmentData(double x0, double y0, double x1, double y1)
				{
					double xDelta = x1 - x0;
					double yDelta = y1 - y0;
					double ySum = y0 + y1;
					double area = 0.5f * xDelta * ySum;

					a = 0.5f * yDelta;
					aTimesFour = yDelta * 2f;
					b = x1 * y0 - x0 * y1;
					bSquared = b * b;
					c0 = -(a * x0 + b) * x0;
					scaledArea = area * xDelta;
				}
			}

			private IRandom _random;
			private SegmentData[] _segments;
			private ulong[] _cdf;

			public DoublePiecewiseLinearSampleGenerator(IRandom random, double[] x, double[] y)
			{
				if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
				if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");

				_random = random;

				double doubleTotalArea = 0f;
				int segmentCount = 0;
				for (int i = 1; i < x.Length; ++i)
				{
					double x0 = x[i - 1];
					double y0 = y[i - 1];
					double x1 = x[i];
					double y1 = y[i];
					double doubleArea = (x1 - x0) * (y0 + y1);
					if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
					if (y0 < 0d) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
					if (doubleArea == 0f) continue;
					doubleTotalArea += (x1 - x0) * (y0 + y1); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
					++segmentCount;
				}
				if (y[y.Length - 1] < 0d) throw new ArgumentOutOfRangeException("y", y[y.Length - 1], "The domain must be entirely non-negative.");

				if (doubleTotalArea <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "y");

				double areaToIntScale = 0xFFFFFFFFFFFFF800UL;

				_segments = new SegmentData[segmentCount];
				_cdf = new ulong[segmentCount];

				double doubleAreaSum = 0d;
				for (int i = 1, j = 0; i < x.Length; ++i)
				{
					double x0 = x[i - 1];
					double y0 = y[i - 1];
					double x1 = x[i];
					double y1 = y[i];
					double doubleArea = (x1 - x0) * (y0 + y1);
					if (doubleArea == 0f) continue;
					doubleAreaSum += doubleArea;
					_cdf[j] = (ulong)Math.Floor(doubleAreaSum / doubleTotalArea * areaToIntScale);
					_segments[j] = new SegmentData(x0, y0, x1, y1);
					++j;
				}

				ulong remainder = ulong.MaxValue - _cdf[segmentCount - 1];
				for (int i = 1, j = 0; i < x.Length; ++i)
				{
					double x0 = x[i - 1];
					double y0 = y[i - 1];
					double x1 = x[i];
					double y1 = y[i];
					double doubleArea = (x1 - x0) * (y0 + y1);
					if (doubleArea == 0f) continue;
					ulong extra = (ulong)Math.Round(doubleArea / doubleAreaSum * remainder);
					_cdf[j] += extra;
					remainder -= extra;
					doubleAreaSum -= doubleArea;
					++j;
				}
				_cdf[segmentCount - 1] = ulong.MaxValue;
			}

			public double Next()
			{
				int i = BinarySearch(_random.Next64(), _cdf);

				if (i < _cdf.Length)
				{
					var segment = _segments[i];

					if (segment.a != 0f)
					{
						// A sloped segment.
						double c = segment.c0 - segment.scaledArea * _random.DoubleCO();
						double sqrt = Math.Sqrt(segment.bSquared - segment.aTimesFour * c);
						if (segment.b >= 0f)
						{
							return -2f * c / (segment.b + sqrt); // Citardauq
						}
						else
						{
							return -0.5f * (segment.b - sqrt) / segment.a; // Quadratic
						}
					}
					else
					{
						// A flat segment.
						double c = segment.c0 - segment.scaledArea * _random.DoubleCO();
						return -c / segment.b;
					}
				}
				else
				{
					var segment = _segments[_cdf.Length - 1];

					if (segment.a != 0f)
					{
						// A sloped segment.
						double sqrt = Math.Sqrt(segment.bSquared - segment.aTimesFour * segment.c0);
						if (segment.b >= 0f)
						{
							return -2f * segment.c0 / (segment.b + sqrt); // Citardauq
						}
						else
						{
							return -0.5f * (segment.b - sqrt) / segment.a; // Quadratic
						}
					}
					else
					{
						// A flat segment.
						return -segment.c0 / segment.b;
					}
				}
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise linear probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The weights of the probability distribution at the range bounds.  Must all be non-negative, and at least one must be positive.</param>
		/// <returns>A sample generator producing random values from within the given piecewise linear distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// </remarks>
		public static ISampleGenerator<double> MakePiecewiseLinearSampleGenerator(this IRandom random, double[] x, double[] y)
		{
			return new DoublePiecewiseLinearSampleGenerator(random, x, y);
		}

		#endregion

		#region Piecewise Hermite Spline Distribution

		/// <summary>
		/// Returns a random value sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The weights of the probability distribution at the range bounds.  Must all be non-negative, and at least one must be positive.</param>
		/// <param name="m">The slopes of the probability distribution at the range bounds.</param>
		/// <returns>A random value from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <para>There should be exactly the same number of elements in <paramref name="x"/> and
		/// <paramref name="y"/>, and exactly two less than double the number of elements in
		/// <paramref name="m"/> as there are in <paramref name="x"/> and <paramref name="y"/>.
		/// For any piece i, it will be bounded by x[i] and x[i + 1], will have weights y[i] and
		/// y[i + 1] at those bounds, and slopes m[2i] and m[2i + 1] also at those bounds.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseHermiteSampleGenerator(IRandom, float[], float[], float[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseHermiteSample(this IRandom random, float[] x, float[] y, float[] m)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");
			if (m.Length != (x.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of x values.", "m");
#endif

			float totalArea = 0f;
			float x0 = x[0];
			float y0 = y[0];
			for (int i = 1, j = 0; i < x.Length; ++i)
			{
				float x1 = x[i];
				float y1 = y[i];
				float m0 = m[j++];
				float m1 = m[j++];

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (y0 < 0f) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
#endif

				float xDelta = x1 - x0;
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
					if (y1 < 0f) throw new ArgumentOutOfRangeException("y", y1, "The domain must be entirely non-negative.");
					if (y0 == 0f && m0 < 0f) throw new ArgumentOutOfRangeException("m", m0, "The domain must be entirely non-negative.");
					if (y1 == 0f && m1 > 0f) throw new ArgumentOutOfRangeException("m", m1, "The domain must be entirely non-negative.");
#endif

					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += y0 * xDelta;
				}
				x0 = x1;
				y0 = y1;
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (totalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "y");
#endif

			float n = random.RangeCC(totalArea);
			x0 = x[0];
			y0 = y[0];
			for (int i = 1, j = 0; i < x.Length; ++i)
			{
				float x1 = x[i];
				float y1 = y[i];
				float m0 = m[j++];
				float m1 = m[j++];
				float xDelta = x1 - x0;
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
					totalArea -= area * xDelta;
					if (totalArea < n) return FindRoot(k4, k3, k2, k1, area, random.FloatCO()) * xDelta + x0;
				}
				else
				{
					// Uniform Segment
					totalArea -= y0 * xDelta;
					if (totalArea < n) return random.RangeCO(x0, x1);
				}
				x0 = x1;
				y0 = y1;
			}
			return x0;
		}

		/// <summary>
		/// Returns a random value sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p">The range bounds (x) and weights (y) of the probability distribution pieces.  The x components must be in strictly increasing order.  The y components must all be non-negative, and at least one must be positive.</param>
		/// <param name="m">The slopes of the probability distribution at the range bounds.</param>
		/// <returns>A random value from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <para>There should be exactly two less than double the number of elements in
		/// <paramref name="m"/> as there are in <paramref name="p"/>.  For any piece i, it will
		/// be bounded by and have the weights of p[i] and p[i + 1], and will have slopes m[2i]
		/// and m[2i + 1] at those bounds.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseHermiteSampleGenerator(IRandom, Vector2[], float[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseHermiteSample(this IRandom random, Vector2[] p, float[] m)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (p.Length < 2) throw new ArgumentException("The array of positions must have at least two elements.", "p");
			if (m.Length != (p.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of positions.", "m");
#endif

			float totalArea = 0f;
			Vector2 p0 = p[0];
			for (int i = 1, j = 0; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
				float m0 = m[j++];
				float m1 = m[j++];

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (p0.x >= p1.x) throw new ArgumentOutOfRangeException("p", p1.x, "The upper range boundary must be greater than the lower range boundary.");
				if (p0.y < 0f) throw new ArgumentOutOfRangeException("p", p0.y, "The domain must be entirely non-negative.");
#endif

				float xDelta = p1.x - p0.x;
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
					if (p1.y < 0f) throw new ArgumentOutOfRangeException("p", p1.y, "The domain must be entirely non-negative.");
					if (p0.y == 0f && m0 < 0f) throw new ArgumentOutOfRangeException("m", m0, "The domain must be entirely non-negative.");
					if (p1.y == 0f && m1 > 0f) throw new ArgumentOutOfRangeException("m", m1, "The domain must be entirely non-negative.");
#endif

					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(p0.x, p0.y, m0, p1.x, p1.y, m1, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += p0.y * xDelta;
				}
				p0 = p1;
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (totalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "p");
#endif

			float n = random.RangeCC(totalArea);
			p0 = p[0];
			for (int i = 1, j = 0; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
				float m0 = m[j++];
				float m1 = m[j++];
				float xDelta = p1.x - p0.x;
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(p0.x, p0.y, m0, p1.x, p1.y, m1, out k4, out k3, out k2, out k1, out area);
					totalArea -= area * xDelta;
					if (totalArea < n) return FindRoot(k4, k3, k2, k1, area, random.FloatCO()) * xDelta + p0.x;
				}
				else
				{
					// Uniform Segment
					totalArea -= p0.y * xDelta;
					if (totalArea < n) return random.RangeCO(p0.x, p1.x);
				}
				p0 = p1;
			}
			return p0.x;
		}

		/// <summary>
		/// Returns a random value sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="keyframes">The key frames of the curve describing the bounds, weights, and slopes of the Hermite spline segments that define the probability distribution function.</param>
		/// <returns>A random value from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseHermiteSampleGenerator(IRandom, Keyframe[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseHermiteSample(this IRandom random, Keyframe[] keyframes)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (keyframes.Length < 2) throw new ArgumentException("The curve must have at least two keyframes.", "keyframes");
#endif

			float totalArea = 0f;
			Keyframe kf0 = keyframes[0];
			for (int i = 1; i < keyframes.Length; ++i)
			{
				Keyframe kf1 = keyframes[i];

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (kf0.time >= kf1.time) throw new ArgumentOutOfRangeException("keyframes", kf1.time, "The upper range boundary must be greater than the lower range boundary.");
				if (kf0.value < 0f) throw new ArgumentOutOfRangeException("keyframes", kf0.value, "The domain must be entirely non-negative.");
#endif

				float xDelta = kf1.time - kf0.time;
				if (!float.IsInfinity(kf0.outTangent) && !float.IsInfinity(kf1.inTangent))
				{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
					if (kf1.value < 0f) throw new ArgumentOutOfRangeException("keyframes", kf1.value, "The domain must be entirely non-negative.");
					if (kf0.value == 0f && kf0.outTangent < 0f) throw new ArgumentOutOfRangeException("keyframes", kf0.outTangent, "The domain must be entirely non-negative.");
					if (kf1.value == 0f && kf1.inTangent > 0f) throw new ArgumentOutOfRangeException("keyframes", kf1.inTangent, "The domain must be entirely non-negative.");
#endif

					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += kf0.value * xDelta;
				}
				kf0 = kf1;
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (totalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "keyframes");
#endif

			float n = random.RangeCC(totalArea);
			kf0 = keyframes[0];
			for (int i = 1; i < keyframes.Length; ++i)
			{
				Keyframe kf1 = keyframes[i];
				float xDelta = kf1.time - kf0.time;
				if (!float.IsInfinity(kf0.outTangent) && !float.IsInfinity(kf1.inTangent))
				{
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent, out k4, out k3, out k2, out k1, out area);
					totalArea -= area * xDelta;
					if (totalArea < n) return FindRoot(k4, k3, k2, k1, area, random.FloatCO()) * xDelta + kf0.time;
				}
				else
				{
					// Uniform Segment
					totalArea -= kf0.value * xDelta;
					if (totalArea < n) return random.RangeCO(kf0.time, kf1.time);
				}
				kf0 = kf1;
			}
			return kf0.time;
		}

		/// <summary>
		/// Returns a random value sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="curve">The curve describing the bounds, weights, and slopes of the Hermite spline segments that define the probability distribution function.</param>
		/// <returns>A random value from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseHermiteSampleGenerator(IRandom, AnimationCurve)"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static float PiecewiseHermiteSample(this IRandom random, AnimationCurve curve)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (curve.length < 2) throw new ArgumentException("The curve must have at least two keyframes.", "curve");
#endif

			float totalArea = 0f;
			Keyframe kf0 = curve[0];
			for (int i = 1; i < curve.length; ++i)
			{
				Keyframe kf1 = curve[i];

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (kf0.time >= kf1.time) throw new ArgumentOutOfRangeException("curve", kf1.time, "The upper range boundary must be greater than the lower range boundary.");
				if (kf0.value < 0f) throw new ArgumentOutOfRangeException("curve", kf0.value, "The domain must be entirely non-negative.");
#endif

				float xDelta = kf1.time - kf0.time;
				if (!float.IsInfinity(kf0.outTangent) && !float.IsInfinity(kf1.inTangent))
				{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
					if (kf1.value < 0f) throw new ArgumentOutOfRangeException("curve", kf1.value, "The domain must be entirely non-negative.");
					if (kf0.value == 0f && kf0.outTangent < 0f) throw new ArgumentOutOfRangeException("curve", kf0.outTangent, "The domain must be entirely non-negative.");
					if (kf1.value == 0f && kf1.inTangent > 0f) throw new ArgumentOutOfRangeException("curve", kf1.inTangent, "The domain must be entirely non-negative.");
#endif

					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += kf0.value * xDelta;
				}
				kf0 = kf1;
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (totalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "curve");
#endif

			float n = random.RangeCC(totalArea);
			kf0 = curve[0];
			for (int i = 1; i < curve.length; ++i)
			{
				Keyframe kf1 = curve[i];
				float xDelta = kf1.time - kf0.time;
				if (!float.IsInfinity(kf0.outTangent) && !float.IsInfinity(kf1.inTangent))
				{
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent, out k4, out k3, out k2, out k1, out area);
					totalArea -= area * xDelta;
					if (totalArea < n) return FindRoot(k4, k3, k2, k1, area, random.FloatCO()) * xDelta + kf0.time;
				}
				else
				{
					// Uniform Segment
					totalArea -= kf0.value * xDelta;
					if (totalArea < n) return random.RangeCO(kf0.time, kf1.time);
				}
				kf0 = kf1;
			}
			return kf0.time;
		}

		private class FloatPiecewiseHermiteSampleGenerator : ISampleGenerator<float>
		{
			private struct SegmentData
			{
				public float xDelta;
				public float x0;
				public float k4;
				public float k3;
				public float k2;
				public float k1;
				public float area;

				public SegmentData(float x0, float y0, float m0, float x1, float y1, float m1)
				{
					xDelta = x1 - x0;
					this.x0 = x0;

					CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
				}
			}

			private IRandom _random;
			private SegmentData[] _segments;
			private uint[] _cdf;

			public FloatPiecewiseHermiteSampleGenerator(IRandom random, float[] x, float[] y, float[] m)
			{
				if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
				if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");
				if (m.Length != (x.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of x values.", "m");

				_random = random;

				try
				{
					Initialize(x.Length,
						(int index, out float x0, out float y0, out float m0) =>
						{
							x0 = x[index];
							y0 = y[index];
							m0 = m[index * 2];
						},
						(int index, out float x1, out float y1, out float m1) =>
						{
							x1 = x[index];
							y1 = y[index];
							m1 = m[index * 2 - 1];
						});
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentOutOfRangeException(ex.ParamName, ex.ActualValue, ex.Message);
				}
			}

			public FloatPiecewiseHermiteSampleGenerator(IRandom random, Vector2[] p, float[] m)
			{
				if (p.Length < 2) throw new ArgumentException("The array of positions must have at least two elements.", "p");
				if (m.Length != (p.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of positions.", "m");

				_random = random;

				try
				{
					Initialize(p.Length,
						(int index, out float x0, out float y0, out float m0) =>
						{
							x0 = p[index].x;
							y0 = p[index].y;
							m0 = m[index * 2];
						},
						(int index, out float x1, out float y1, out float m1) =>
						{
							x1 = p[index].x;
							y1 = p[index].y;
							m1 = m[index * 2 - 1];
						});
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentOutOfRangeException(ex.ParamName == "m" ? "m" : "p", ex.ActualValue, ex.Message);
				}
			}

			public FloatPiecewiseHermiteSampleGenerator(IRandom random, Keyframe[] keyframes)
			{
				if (keyframes.Length < 2) throw new ArgumentException("The curve must have at least two keyframes.", "keyframes");

				_random = random;

				try
				{
					Initialize(keyframes.Length,
						(int index, out float x, out float y, out float m) =>
						{
							var keyframe = keyframes[index];
							x = keyframe.time;
							y = keyframe.value;
							m = keyframe.outTangent;
						},
						(int index, out float x, out float y, out float m) =>
						{
							var keyframe = keyframes[index];
							x = keyframe.time;
							y = keyframe.value;
							m = keyframe.inTangent;
						});
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentOutOfRangeException("keyframes", ex.ActualValue, ex.Message);
				}
			}

			public FloatPiecewiseHermiteSampleGenerator(IRandom random, AnimationCurve curve)
			{
				if (curve.length < 2) throw new ArgumentException("The curve must have at least two keyframes.", "curve");

				_random = random;

				try
				{
					Initialize(curve.length,
						(int index, out float x, out float y, out float m) =>
						{
							var keyframe = curve[index];
							x = keyframe.time;
							y = keyframe.value;
							m = keyframe.outTangent;
						},
						(int index, out float x, out float y, out float m) =>
						{
							var keyframe = curve[index];
							x = keyframe.time;
							y = keyframe.value;
							m = keyframe.inTangent;
						});
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentOutOfRangeException("curve", ex.ActualValue, ex.Message);
				}
			}

			private delegate void GetFrameDelegate(int index, out float x, out float y, out float m);

			private void Initialize(int frameCount, GetFrameDelegate getFront, GetFrameDelegate getBack)
			{
				double totalArea = 0d;
				int segmentCount = 0;
				for (int i = 1; i < frameCount; ++i)
				{
					float x0, y0, m0, x1, y1, m1;
					getFront(i - 1, out x0, out y0, out m0);
					getBack(i, out x1, out y1, out m1);

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
					if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
					if (y0 < 0f) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
#endif

					float xDelta = x1 - x0;
					if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
					{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
						if (y1 < 0f) throw new ArgumentOutOfRangeException("y", y1, "The domain must be entirely non-negative.");
						if (y0 == 0f && m0 < 0f) throw new ArgumentOutOfRangeException("m", m0, "The domain must be entirely non-negative.");
						if (y1 == 0f && m1 > 0f) throw new ArgumentOutOfRangeException("m", m1, "The domain must be entirely non-negative.");
#endif

						// Hermite Segment
						if (y0 <= 0f && y1 <= 0f && m0 <= 0f && m1 >= 0f) continue;

						float k4, k3, k2, k1, area;
						CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
						totalArea += area * xDelta;
					}
					else
					{
						// Uniform Segment
						if (y0 <= 0f) continue;

						totalArea += y0 * xDelta;
					}
					++segmentCount;
				}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (totalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "y");
#endif

				double areaToIntScale = uint.MaxValue + 1d;
				totalArea += 1d / areaToIntScale;

				_segments = new SegmentData[segmentCount];
				_cdf = new uint[segmentCount];

				double areaSum = 0d;
				for (int i = 1, j = 0; i < frameCount; ++i)
				{
					float x0, y0, m0, x1, y1, m1;
					getFront(i - 1, out x0, out y0, out m0);
					getBack(i, out x1, out y1, out m1);
					if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
					{
						// Hermite Segment
						if (y0 <= 0f && y1 <= 0f && m0 <= 0f && m1 >= 0f) continue;

						var segmentData = new SegmentData(x0, y0, m0, x1, y1, m1);

						areaSum += segmentData.area * segmentData.xDelta;
						_segments[j] = segmentData;
						_cdf[j] = (uint)Math.Floor(areaSum / totalArea * areaToIntScale);
					}
					else
					{
						// Uniform Segment
						if (y0 <= 0f) continue;

						var segmentData = new SegmentData(x0, y0, 0f, x1, y0, 0f);
						areaSum += segmentData.area * segmentData.xDelta;
						_segments[j] = segmentData;
						_cdf[j] = (uint)Math.Floor(areaSum / totalArea * areaToIntScale);
					}
					++j;
				}
			}

			public float Next()
			{
				int i = BinarySearch(_random.Next32(), _cdf);

				if (i < _cdf.Length)
				{
					var segment = _segments[i];
					return FindRoot(segment.k4, segment.k3, segment.k2, segment.k1, segment.area, _random.FloatCO()) * segment.xDelta + segment.x0;
				}
				else
				{
					var segment = _segments[_cdf.Length - 1];
					return segment.x0 + segment.xDelta;
				}
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The weights of the probability distribution at the range bounds.  Must all be non-negative, and at least one must be positive.</param>
		/// <param name="m">The slopes of the probability distribution at the range bounds.</param>
		/// <returns>A sample generator producing random values from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly the same number of elements in <paramref name="x"/> and
		/// <paramref name="y"/>, and exactly two less than double the number of elements in
		/// <paramref name="m"/> as there are in <paramref name="x"/> and <paramref name="y"/>.
		/// For any piece i, it will be bounded by x[i] and x[i + 1], will have weights y[i] and
		/// y[i + 1] at those bounds, and slopes m[2i] and m[2i + 1] also at those bounds.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseHermiteSampleGenerator(this IRandom random, float[] x, float[] y, float[] m)
		{
			return new FloatPiecewiseHermiteSampleGenerator(random, x, y, m);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="p">The range bounds (x) and weights (y) of the probability distribution pieces.  The x components must be in strictly increasing order.  The y components must all be non-negative, and at least one must be positive.</param>
		/// <param name="m">The slopes of the probability distribution at the range bounds.</param>
		/// <returns>A sample generator producing random values from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly two less than double the number of elements in
		/// <paramref name="m"/> as there are in <paramref name="p"/>.  For any piece i, it will
		/// be bounded by and have the weights of p[i] and p[i + 1], and will have slopes m[2i]
		/// and m[2i + 1] at those bounds.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseHermiteSampleGenerator(this IRandom random, Vector2[] p, float[] m)
		{
			return new FloatPiecewiseHermiteSampleGenerator(random, p, m);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="keyframes">The key frames of the curve describing the bounds, weights, and slopes of the Hermite spline segments that define the probability distribution function.</param>
		/// <returns>A sample generator producing random values from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseHermiteSampleGenerator(this IRandom random, Keyframe[] keyframes)
		{
			return new FloatPiecewiseHermiteSampleGenerator(random, keyframes);
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="curve">The curve describing the bounds, weights, and slopes of the Hermite spline segments that define the probability distribution function.</param>
		/// <returns>A sample generator producing random values from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// </remarks>
		public static ISampleGenerator<float> MakePiecewiseHermiteSampleGenerator(this IRandom random, AnimationCurve curve)
		{
			return new FloatPiecewiseHermiteSampleGenerator(random, curve);
		}

		/// <summary>
		/// Returns a random value sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The weights of the probability distribution at the range bounds.  Must all be non-negative, and at least one must be positive.</param>
		/// <param name="m">The slopes of the probability distribution at the range bounds.</param>
		/// <returns>A random value from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The area underneath the spline does not need to equal 1, as it will automatically
		/// be normalized into a proper probability distribution function.  It should however have
		/// a positive area, and at no point within the given range should the function evaluate
		/// to a negative value.</para>
		/// <para>There should be exactly the same number of elements in <paramref name="x"/> and
		/// <paramref name="y"/>, and exactly two less than double the number of elements in
		/// <paramref name="m"/> as there are in <paramref name="x"/> and <paramref name="y"/>.
		/// For any piece i, it will be bounded by x[i] and x[i + 1], will have weights y[i] and
		/// y[i + 1] at those bounds, and slopes m[2i] and m[2i + 1] also at those bounds.</para>
		/// <note type="important"><para>There is a large amount of precomputation that can be done to
		/// gain performance if you need to request many samples from a single distribution.  Use
		/// <see cref="MakePiecewiseHermiteSampleGenerator(IRandom, double[], double[], double[])"/> to create a generator
		/// that will perform and utilize this precomputation for you.</para></note>
		/// </remarks>
		public static double PiecewiseHermiteSample(this IRandom random, double[] x, double[] y, double[] m)
		{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");
			if (m.Length != (x.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of x values.", "m");
#endif

			double totalArea = 0d;
			double x0 = x[0];
			double y0 = y[0];
			for (int i = 1, j = 0; i < x.Length; ++i)
			{
				double x1 = x[i];
				double y1 = y[i];
				double m0 = m[j++];
				double m1 = m[j++];

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
				if (y0 < 0d) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
#endif

				double xDelta = x1 - x0;
				if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
				{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
					if (y1 < 0d) throw new ArgumentOutOfRangeException("y", y1, "The domain must be entirely non-negative.");
					if (y0 == 0d && m0 < 0d) throw new ArgumentOutOfRangeException("m", m0, "The domain must be entirely non-negative.");
					if (y1 == 0d && m1 > 0d) throw new ArgumentOutOfRangeException("m", m1, "The domain must be entirely non-negative.");
#endif

					// Hermite Segment
					double k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += y0 * xDelta;
				}
				x0 = x1;
				y0 = y1;
			}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
			if (totalArea <= 0d) throw new ArgumentException("The total area of the distribution must be positive.", "y");
#endif

			double n = random.RangeCC(totalArea);
			x0 = x[0];
			y0 = y[0];
			for (int i = 1, j = 0; i < x.Length; ++i)
			{
				double x1 = x[i];
				double y1 = y[i];
				double m0 = m[j++];
				double m1 = m[j++];
				double xDelta = x1 - x0;
				if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
				{
					// Hermite Segment
					double k4, k3, k2, k1, area;
					CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
					totalArea -= area * xDelta;
					if (totalArea < n) return FindRoot(k4, k3, k2, k1, area, random.DoubleCO()) * xDelta + x0;
				}
				else
				{
					// Uniform Segment
					totalArea -= y0 * xDelta;
					if (totalArea < n) return random.RangeCO(x0, x1);
				}
				x0 = x1;
				y0 = y1;
			}
			return x0;
		}

		private class DoublePiecewiseHermiteSampleGenerator : ISampleGenerator<double>
		{
			private struct SegmentData
			{
				public double xDelta;
				public double x0;
				public double k4;
				public double k3;
				public double k2;
				public double k1;
				public double area;

				public SegmentData(double x0, double y0, double m0, double x1, double y1, double m1)
				{
					xDelta = x1 - x0;
					this.x0 = x0;

					CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
				}
			}

			private IRandom _random;
			private SegmentData[] _segments;
			private ulong[] _cdf;

			public DoublePiecewiseHermiteSampleGenerator(IRandom random, double[] x, double[] y, double[] m)
			{
				if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
				if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");
				if (m.Length != (x.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of x values.", "m");

				_random = random;

				try
				{
					Initialize(x.Length,
						(int index, out double x0, out double y0, out double m0) =>
						{
							x0 = x[index];
							y0 = y[index];
							m0 = m[index * 2];
						},
						(int index, out double x1, out double y1, out double m1) =>
						{
							x1 = x[index];
							y1 = y[index];
							m1 = m[index * 2 - 1];
						});
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentOutOfRangeException(ex.ParamName, ex.ActualValue, ex.Message);
				}
			}

			private delegate void GetFrameDelegate(int index, out double x, out double y, out double m);

			private void Initialize(int frameCount, GetFrameDelegate getFront, GetFrameDelegate getBack)
			{
				double totalArea = 0d;
				int segmentCount = 0;
				for (int i = 1; i < frameCount; ++i)
				{
					double x0, y0, m0, x1, y1, m1;
					getFront(i - 1, out x0, out y0, out m0);
					getBack(i, out x1, out y1, out m1);

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
					if (x0 >= x1) throw new ArgumentOutOfRangeException("x", x1, "The upper range boundary must be greater than the lower range boundary.");
					if (y0 < 0d) throw new ArgumentOutOfRangeException("y", y0, "The domain must be entirely non-negative.");
#endif

					double xDelta = x1 - x0;
					if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
					{
#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
						if (y1 < 0d) throw new ArgumentOutOfRangeException("y", y1, "The domain must be entirely non-negative.");
						if (y0 == 0d && m0 < 0d) throw new ArgumentOutOfRangeException("m", m0, "The domain must be entirely non-negative.");
						if (y1 == 0d && m1 > 0d) throw new ArgumentOutOfRangeException("m", m1, "The domain must be entirely non-negative.");
#endif

						// Hermite Segment
						if (y0 <= 0d && y1 <= 0d && m0 <= 0d && m1 >= 0d) continue;

						double k4, k3, k2, k1, area;
						CalculateHermiteSplineCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
						totalArea += area * xDelta;
					}
					else
					{
						// Uniform Segment
						if (y0 <= 0d) continue;

						totalArea += y0 * xDelta;
					}
					++segmentCount;
				}

#if UNITY_EDITOR && !MAKEITRANDOM_SKIPEDITORARGCHECKS
				if (totalArea <= 0f) throw new ArgumentException("The total area of the distribution must be positive.", "y");
#endif

				double areaToIntScale = 0xFFFFFFFFFFFFF800UL;

				_segments = new SegmentData[segmentCount];
				_cdf = new ulong[segmentCount];

				double areaSum = 0d;
				for (int i = 1, j = 0; i < frameCount; ++i)
				{
					double x0, y0, m0, x1, y1, m1;
					getFront(i - 1, out x0, out y0, out m0);
					getBack(i, out x1, out y1, out m1);
					if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
					{
						// Hermite Segment
						if (y0 <= 0f && y1 <= 0f && m0 <= 0f && m1 >= 0f) continue;

						var segmentData = new SegmentData(x0, y0, m0, x1, y1, m1);

						areaSum += segmentData.area * segmentData.xDelta;
						_segments[j] = segmentData;
						_cdf[j] = (ulong)Math.Floor(areaSum / totalArea * areaToIntScale);
					}
					else
					{
						// Uniform Segment
						if (y0 <= 0f) continue;

						var segmentData = new SegmentData(x0, y0, 0f, x1, y0, 0f);
						areaSum += segmentData.area * segmentData.xDelta;
						_segments[j] = segmentData;
						_cdf[j] = (ulong)Math.Floor(areaSum / totalArea * areaToIntScale);
					}
					++j;
				}

				ulong remainder = ulong.MaxValue - _cdf[segmentCount - 1];
				for (int i = 1, j = 0; i < frameCount; ++i)
				{
					double x0, y0, m0, x1, y1, m1;
					getFront(i - 1, out x0, out y0, out m0);
					getBack(i, out x1, out y1, out m1);
					if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
					{
						// Hermite Segment
						if (y0 <= 0f && y1 <= 0f && m0 <= 0f && m1 >= 0f) continue;

						var segmentData = new SegmentData(x0, y0, m0, x1, y1, m1);

						var area = segmentData.area * segmentData.xDelta;
						ulong extra = (ulong)Math.Round(area / areaSum * remainder);
						_cdf[j] += extra;
						remainder -= extra;
						areaSum -= area;
					}
					else
					{
						// Uniform Segment
						if (y0 <= 0f) continue;

						var segmentData = new SegmentData(x0, y0, 0f, x1, y0, 0f);

						var area = segmentData.area * segmentData.xDelta;
						ulong extra = (ulong)Math.Round(area / areaSum * remainder);
						_cdf[j] += extra;
						remainder -= extra;
						areaSum -= area;
					}
					++j;
				}
				_cdf[segmentCount - 1] = ulong.MaxValue;
			}

			public double Next()
			{
				int i = BinarySearch(_random.Next64(), _cdf);

				if (i < _cdf.Length)
				{
					var segment = _segments[i];
					return FindRoot(segment.k4, segment.k3, segment.k2, segment.k1, segment.area, _random.DoubleCO()) * segment.xDelta + segment.x0;
				}
				else
				{
					var segment = _segments[_cdf.Length - 1];
					return segment.x0 + segment.xDelta;
				}
			}
		}

		/// <summary>
		/// Returns a sample generator which will produce values sampled from a piecewise Hermite spline probability distribution.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="x">The range bounds of the probability distribution pieces.  Must be in strictly increasing order.</param>
		/// <param name="y">The weights of the probability distribution at the range bounds.  Must all be non-negative, and at least one must be positive.</param>
		/// <param name="m">The slopes of the probability distribution at the range bounds.</param>
		/// <returns>A sample generator producing random values from within the given piecewise Hermite spline distribution.</returns>
		/// <remarks>
		/// <para>The total area underneath all of the pieces does not need to equal 1, as it will
		/// automatically be normalized into a proper probability distribution function.</para>
		/// <para>There should be exactly the same number of elements in <paramref name="x"/> and
		/// <paramref name="y"/>, and exactly two less than double the number of elements in
		/// <paramref name="m"/> as there are in <paramref name="x"/> and <paramref name="y"/>.
		/// For any piece i, it will be bounded by x[i] and x[i + 1], will have weights y[i] and
		/// y[i + 1] at those bounds, and slopes m[2i] and m[2i + 1] also at those bounds.</para>
		/// </remarks>
		public static ISampleGenerator<double> MakePiecewiseHermiteSampleGenerator(this IRandom random, double[] x, double[] y, double[] m)
		{
			return new DoublePiecewiseHermiteSampleGenerator(random, x, y, m);
		}

		#endregion
	}
}
