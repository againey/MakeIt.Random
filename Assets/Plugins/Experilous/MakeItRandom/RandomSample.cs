/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;

namespace Experilous.MakeItRandom
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
	/// A static class of extension methods for generating random numbers according to non-uniform distributions.
	/// </summary>
	public static class RandomSample
	{
		#region Uniform Distribution

		public static float UniformSample(this IRandom random, float x0, float x1)
		{
#if UNITY_EDITOR
			if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary.", "x1");
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
				if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary.", "x1");

				_random = random;
				_x0 = x0;
				_range = x1 - x0;
			}

			public float Next()
			{
				return _random.FloatCC() * _range + _x0;
			}
		}

		public static ISampleGenerator<float> MakeUniformSampleGenerator(this IRandom random, float x0, float x1)
		{
			return new FloatUniformSampleGenerator(random, x0, x1);
		}

		public static double UniformSample(this IRandom random, double x0, double x1)
		{
#if UNITY_EDITOR
			if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary.", "x1");
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
				if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary.", "x1");

				_random = random;
				_x0 = x0;
				_range = x1 - x0;
			}

			public double Next()
			{
				return _random.DoubleCC() * _range + _x0;
			}
		}

		public static ISampleGenerator<double> MakeUniformSampleGenerator(this IRandom random, double x0, double x1)
		{
			return new DoubleUniformSampleGenerator(random, x0, x1);
		}

		#endregion

		#region Normal Distribution

		public static float NormalSample(this IRandom random, float mean, float standardDeviation)
		{
#if UNITY_EDITOR
			if (standardDeviation <= 0f) throw new ArgumentException("The standard deviation must be greater than zero.", "standardDeviation");
#endif

			float sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.NormalFloat.zigguratTable,
				Detail.Distributions.NormalFloat.F,
				Detail.Distributions.NormalFloat.SampleFallback);

			return sample * standardDeviation + mean;
		}

		private class FloatNormalSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _mean;
			private float _standardDeviation;
			private Detail.Distributions.TwoSidedSymmetricFloatZigguratTable _zigguratTable;

			public FloatNormalSampleGenerator(IRandom random, float mean, float standardDeviation, Detail.Distributions.TwoSidedSymmetricFloatZigguratTable zigguratTable)
			{
				if (standardDeviation <= 0f) throw new ArgumentException("The standard deviation must be greater than zero.", "standardDeviation");

				_random = random;
				_mean = mean;
				_standardDeviation = standardDeviation;
				_zigguratTable = zigguratTable;
			}

			public float Next()
			{
				return Detail.Distributions.SampleZiggurat(_random, _zigguratTable, Detail.Distributions.NormalFloat.F, Detail.Distributions.NormalFloat.SampleFallback) * _standardDeviation + _mean;
			}
		}

		public static ISampleGenerator<float> MakeNormalSampleGenerator(this IRandom random, float mean, float standardDeviation)
		{
			return new FloatNormalSampleGenerator(random, mean, standardDeviation, Detail.Distributions.NormalFloat.zigguratTable);
		}

		public static ISampleGenerator<float> MakeNormalSampleGenerator(this IRandom random, float mean, float standardDeviation, Detail.Distributions.TwoSidedSymmetricFloatZigguratTable lookupTable)
		{
			return new FloatNormalSampleGenerator(random, mean, standardDeviation, lookupTable);
		}

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

		public static double NormalSample(this IRandom random, double mean, double standardDeviation)
		{
#if UNITY_EDITOR
			if (standardDeviation <= 0d) throw new ArgumentException("The standard deviation must be greater than zero.", "standardDeviation");
#endif

			double sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.NormalDouble.zigguratTable,
				Detail.Distributions.NormalDouble.F,
				Detail.Distributions.NormalDouble.SampleFallback);

			return sample * standardDeviation + mean;
		}

		private class DoubleNormalSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _mean;
			private double _standardDeviation;
			private Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable _zigguratTable;

			public DoubleNormalSampleGenerator(IRandom random, double mean, double standardDeviation, Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable zigguratTable)
			{
				if (standardDeviation <= 0d) throw new ArgumentException("The standard deviation must be greater than zero.", "standardDeviation");

				_random = random;
				_mean = mean;
				_standardDeviation = standardDeviation;
				_zigguratTable = zigguratTable;
			}

			public double Next()
			{
				return Detail.Distributions.SampleZiggurat(_random, _zigguratTable, Detail.Distributions.NormalDouble.F, Detail.Distributions.NormalDouble.SampleFallback) * _standardDeviation + _mean;
			}
		}

		public static ISampleGenerator<double> MakeNormalSampleGenerator(this IRandom random, double mean, double standardDeviation)
		{
			return new DoubleNormalSampleGenerator(random, mean, standardDeviation, Detail.Distributions.NormalDouble.zigguratTable);
		}

		public static ISampleGenerator<double> MakeNormalSampleGenerator(this IRandom random, double mean, double standardDeviation, Detail.Distributions.TwoSidedSymmetricDoubleZigguratTable lookupTable)
		{
			return new DoubleNormalSampleGenerator(random, mean, standardDeviation, lookupTable);
		}

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

		#endregion

		#region Exponential Distribution

		public static float ExponentialSample(this IRandom random, float eventRate)
		{
#if UNITY_EDITOR
			if (eventRate <= 0f) throw new ArgumentException("The event rate must be greater than zero.", "eventRate");
#endif

			float sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.ExponentialFloat.zigguratTable,
				Detail.Distributions.ExponentialFloat.F,
				Detail.Distributions.ExponentialFloat.SampleFallback);

			return sample / eventRate;
		}

		private class FloatExponentialSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _eventRate;
			private Detail.Distributions.OneSidedFloatZigguratTable _zigguratTable;

			public FloatExponentialSampleGenerator(IRandom random, float eventRate, Detail.Distributions.OneSidedFloatZigguratTable zigguratTable)
			{
				if (eventRate <= 0f) throw new ArgumentException("The event rate must be greater than zero.", "eventRate");

				_random = random;
				_eventRate = eventRate;
				_zigguratTable = zigguratTable;
			}

			public float Next()
			{
				return Detail.Distributions.SampleZiggurat(_random, _zigguratTable, Detail.Distributions.ExponentialFloat.F, Detail.Distributions.ExponentialFloat.SampleFallback) / _eventRate;
			}
		}

		public static ISampleGenerator<float> MakeExponentialSampleGenerator(this IRandom random, float eventRate)
		{
			return new FloatExponentialSampleGenerator(random, eventRate, Detail.Distributions.ExponentialFloat.zigguratTable);
		}

		public static ISampleGenerator<float> MakeExponentialSampleGenerator(this IRandom random, float eventRate, Detail.Distributions.OneSidedFloatZigguratTable lookupTable)
		{
			return new FloatExponentialSampleGenerator(random, eventRate, lookupTable);
		}

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

		public static double ExponentialSample(this IRandom random, double eventRate)
		{
#if UNITY_EDITOR
			if (eventRate <= 0f) throw new ArgumentException("The event rate must be greater than zero.", "eventRate");
#endif

			double sample = Detail.Distributions.SampleZiggurat(random,
				Detail.Distributions.ExponentialDouble.zigguratTable,
				Detail.Distributions.ExponentialDouble.F,
				Detail.Distributions.ExponentialDouble.SampleFallback);

			return sample / eventRate;
		}

		private class DoubleExponentialSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _eventRate;
			private Detail.Distributions.OneSidedDoubleZigguratTable _zigguratTable;

			public DoubleExponentialSampleGenerator(IRandom random, double eventRate, Detail.Distributions.OneSidedDoubleZigguratTable zigguratTable)
			{
				if (eventRate <= 0f) throw new ArgumentException("The event rate must be greater than zero.", "eventRate");

				_random = random;
				_eventRate = eventRate;
				_zigguratTable = zigguratTable;
			}

			public double Next()
			{
				return Detail.Distributions.SampleZiggurat(_random, _zigguratTable, Detail.Distributions.ExponentialDouble.F, Detail.Distributions.ExponentialDouble.SampleFallback) / _eventRate;
			}
		}

		public static ISampleGenerator<double> MakeExponentialSampleGenerator(this IRandom random, double eventRate)
		{
			return new DoubleExponentialSampleGenerator(random, eventRate, Detail.Distributions.ExponentialDouble.zigguratTable);
		}

		public static ISampleGenerator<double> MakeExponentialSampleGenerator(this IRandom random, double eventRate, Detail.Distributions.OneSidedDoubleZigguratTable lookupTable)
		{
			return new DoubleExponentialSampleGenerator(random, eventRate, lookupTable);
		}

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

		#endregion

		#region Triangular Distribution

		public static float TriangularSample(this IRandom random, float lower, float mode, float upper)
		{
#if UNITY_EDITOR
			if (lower >= mode) throw new ArgumentException("The mode must be greater than the lower range boundary.", "mode");
			if (mode >= upper) throw new ArgumentException("The upper range boundary must be greater than the mode boundary.", "upper");
#endif

			float n = random.FloatOO();
			float range = upper - lower;
			float lowerRange = mode - lower;
			float split = lowerRange / range;
			return n < split ? lower + Mathf.Sqrt(n * range * lowerRange) : upper - Mathf.Sqrt((1f - n) * range * (upper - mode));
		}

		private class FloatTriangularSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _split;
			private float _lower;
			private float _upper;
			private float _rangeLowerRange;
			private float _rangeUpperRange;

			public FloatTriangularSampleGenerator(IRandom random, float lower, float mode, float upper)
			{
				if (lower >= mode) throw new ArgumentException("The mode must be greater than the lower range boundary.", "mode");
				if (mode >= upper) throw new ArgumentException("The upper range boundary must be greater than the mode boundary.", "upper");

				_random = random;

				float range = upper - lower;
				float lowerRange = mode - lower;
				float upperRange = upper - mode;

				_lower = lower;
				_upper = upper;
				_rangeLowerRange = range * lowerRange;
				_rangeUpperRange = range * upperRange;
				_split = lowerRange / range;
			}

			public float Next()
			{
				float n = _random.FloatOO();
				return n < _split ? _lower + Mathf.Sqrt(n * _rangeLowerRange) : _upper - Mathf.Sqrt((1f - n) * _rangeUpperRange);
			}
		}

		public static ISampleGenerator<float> MakeTriangularSampleGenerator(this IRandom random, float lower, float mode, float upper)
		{
			return new FloatTriangularSampleGenerator(random, lower, mode, upper);
		}

		public static double TriangularSample(this IRandom random, double lower, double mode, double upper)
		{
#if UNITY_EDITOR
			if (lower >= mode) throw new ArgumentException("The mode must be greater than the lower range boundary.", "mode");
			if (mode >= upper) throw new ArgumentException("The upper range boundary must be greater than the mode boundary.", "upper");
#endif

			double n = random.DoubleOO();
			double range = upper - lower;
			double lowerRange = mode - lower;
			double split = lowerRange / range;
			return n < split ? lower + Math.Sqrt(n * range * lowerRange) : upper - Math.Sqrt((1d - n) * range * (upper - mode));
		}

		private class DoubleTriangularSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _split;
			private double _lower;
			private double _upper;
			private double _rangeLowerRange;
			private double _rangeUpperRange;

			public DoubleTriangularSampleGenerator(IRandom random, double lower, double mode, double upper)
			{
				if (lower >= mode) throw new ArgumentException("The mode must be greater than the lower range boundary.", "mode");
				if (mode >= upper) throw new ArgumentException("The upper range boundary must be greater than the mode boundary.", "upper");

				_random = random;

				double range = upper - lower;
				double lowerRange = mode - lower;
				double upperRange = upper - mode;

				_lower = lower;
				_upper = upper;
				_rangeLowerRange = range * lowerRange;
				_rangeUpperRange = range * upperRange;
				_split = lowerRange / range;
			}

			public double Next()
			{
				double n = _random.DoubleOO();
				return n < _split ? _lower + Math.Sqrt(n * _rangeLowerRange) : _upper - Math.Sqrt((1d - n) * _rangeUpperRange);
			}
		}

		public static ISampleGenerator<double> MakeTriangularSampleGenerator(this IRandom random, double lower, double mode, double upper)
		{
			return new DoubleTriangularSampleGenerator(random, lower, mode, upper);
		}

		#endregion

		#region Trapezoidal Distribution

		public static float TrapezoidalSample(this IRandom random, float lower, float lowerMode, float upperMode, float upper)
		{
#if UNITY_EDITOR
			if (lower >= lowerMode) throw new ArgumentException("The lower mode boundary must be greater than the lower range boundary.", "lowerMode");
			if (lowerMode >= upperMode) throw new ArgumentException("The upper mode boundary must be greater than the lower mode boundary.", "upperMode");
			if (upperMode >= upper) throw new ArgumentException("The upper range boundary must be greater than the upper mode boundary.", "upper");
#endif

			float n = random.FloatOO();

			float range = upper + upperMode - lowerMode - lower;

			float lowerRange = lowerMode - lower;
			float lowerSplit = lowerRange / range;
			if (n < lowerSplit) return lower + Mathf.Sqrt(n * range * lowerRange); // Within lower triangle.

			float midRange = upperMode - lowerMode;
			float upperSplit = (midRange + midRange + lowerRange) / range;
			if (n > upperSplit) return upper - Mathf.Sqrt((1f - n) * range * (upper - upperMode)); // Within upper triangle.

			return lowerMode + (n - lowerSplit) / (upperSplit - lowerSplit) * midRange; // Within middle rectangle.
		}

		private class FloatTrapezoidalSampleGenerator : ISampleGenerator<float>
		{
			private IRandom _random;
			private float _lowerSplit;
			private float _upperSplit;
			private float _lower;
			private float _lowerMode;
			private float _upper;
			private float _rangeLowerRange;
			private float _rangeUpperRange;
			private float _modeScale;

			public FloatTrapezoidalSampleGenerator(IRandom random, float lower, float lowerMode, float upperMode, float upper)
			{
				if (lower >= lowerMode) throw new ArgumentException("The lower mode boundary must be greater than the lower range boundary.", "lowerMode");
				if (lowerMode >= upperMode) throw new ArgumentException("The upper mode boundary must be greater than the lower mode boundary.", "upperMode");
				if (upperMode >= upper) throw new ArgumentException("The upper range boundary must be greater than the upper mode boundary.", "upper");

				_random = random;

				float range = upper + upperMode - lowerMode - lower;
				float lowerRange = lowerMode - lower;
				float midRange = upperMode - lowerMode;
				float upperRange = upper - upperMode;

				_lower = lower;
				_lowerMode = lowerMode;
				_upper = upper;
				_rangeLowerRange = range * lowerRange;
				_rangeUpperRange = range * upperRange;
				_lowerSplit = lowerRange / range;
				_upperSplit = (midRange + midRange + lowerRange) / range;
				_modeScale = midRange / (_upperSplit - _lowerSplit);
			}

			public float Next()
			{
				float n = _random.FloatOO();
				if (n < _lowerSplit) return _lower + Mathf.Sqrt(n * _rangeLowerRange); // Within lower triangle.
				if (n > _upperSplit) return _upper - Mathf.Sqrt((1f - n) * _rangeUpperRange); // Within upper triangle.
				return _lowerMode + (n - _lowerSplit) * _modeScale; // Within middle rectangle.
			}
		}

		public static ISampleGenerator<float> MakeTrapezoidalSampleGenerator(this IRandom random, float lower, float lowerMode, float upperMode, float upper)
		{
			return new FloatTrapezoidalSampleGenerator(random, lower, lowerMode, upperMode, upper);
		}

		public static double TrapezoidalSample(this IRandom random, double lower, double lowerMode, double upperMode, double upper)
		{
#if UNITY_EDITOR
			if (lower >= lowerMode) throw new ArgumentException("The lower mode boundary must be greater than the lower range boundary.", "lowerMode");
			if (lowerMode >= upperMode) throw new ArgumentException("The upper mode boundary must be greater than the lower mode boundary.", "upperMode");
			if (upperMode >= upper) throw new ArgumentException("The upper range boundary must be greater than the upper mode boundary.", "upper");
#endif

			double n = random.DoubleOO();

			double range = upper + upperMode - lowerMode - lower;

			double lowerRange = lowerMode - lower;
			double lowerSplit = lowerRange / range;
			if (n < lowerSplit) return lower + Math.Sqrt(n * range * lowerRange); // Within lower triangle.

			double midRange = upperMode - lowerMode;
			double upperSplit = (midRange + midRange + lowerRange) / range;
			if (n > upperSplit) return upper - Math.Sqrt((1d - n) * range * (upper - upperMode)); // Within upper triangle.

			return lowerMode + (n - lowerSplit) / (upperSplit - lowerSplit) * midRange; // Within middle rectangle.
		}

		private class DoubleTrapezoidalSampleGenerator : ISampleGenerator<double>
		{
			private IRandom _random;
			private double _lowerSplit;
			private double _upperSplit;
			private double _lower;
			private double _lowerMode;
			private double _upper;
			private double _rangeLowerRange;
			private double _rangeUpperRange;
			private double _modeScale;

			public DoubleTrapezoidalSampleGenerator(IRandom random, double lower, double lowerMode, double upperMode, double upper)
			{
				if (lower >= lowerMode) throw new ArgumentException("The lower mode boundary must be greater than the lower range boundary.", "lowerMode");
				if (lowerMode >= upperMode) throw new ArgumentException("The upper mode boundary must be greater than the lower mode boundary.", "upperMode");
				if (upperMode >= upper) throw new ArgumentException("The upper range boundary must be greater than the upper mode boundary.", "upper");

				_random = random;

				double range = upper + upperMode - lowerMode - lower;
				double lowerRange = lowerMode - lower;
				double midRange = upperMode - lowerMode;
				double upperRange = upper - upperMode;

				_lower = lower;
				_lowerMode = lowerMode;
				_upper = upper;
				_rangeLowerRange = range * lowerRange;
				_rangeUpperRange = range * upperRange;
				_lowerSplit = lowerRange / range;
				_upperSplit = (midRange + midRange + lowerRange) / range;
				_modeScale = (_upperSplit - _lowerSplit) * midRange;
			}

			public double Next()
			{
				double n = _random.DoubleOO();
				if (n < _lowerSplit) return _lower + Math.Sqrt(n * _rangeLowerRange); // Within lower triangle.
				if (n > _upperSplit) return _upper - Math.Sqrt((1d - n) * _rangeUpperRange); // Within upper triangle.
				return _lowerMode + (n - _lowerSplit) / _modeScale; // Within middle rectangle.
			}
		}

		public static ISampleGenerator<double> MakeTrapezoidalSampleGenerator(this IRandom random, double lower, double lowerMode, double upperMode, double upper)
		{
			return new DoubleTrapezoidalSampleGenerator(random, lower, lowerMode, upperMode, upper);
		}

		#endregion

		#region Linear Distribution

		public static float LinearSample(this IRandom random, Vector2 p0, Vector2 p1)
		{
			return random.LinearSample(p0.x, p0.y, p1.x, p1.y);
		}

		public static float LinearSample(this IRandom random, float x0, float y0, float x1, float y1)
		{
#if UNITY_EDITOR
			if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary.", "x1");
			if (y0 < 0f) throw new ArgumentException("The domain must be entirely non-negative.", "y0");
			if (y1 < 0f) throw new ArgumentException("The domain must be entirely non-negative.", "y1");
			if (y0 == 0f && y1 == 0f) throw new ArgumentException("The probability distribution must have a positive area.", "y1");
#endif

			return random.LinearSample(x0, y0, x1, y1, random.FloatCC());
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
				if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary.", "x1");
				if (y0 < 0f) throw new ArgumentException("The domain must be entirely non-negative.", "y0");
				if (y1 < 0f) throw new ArgumentException("The domain must be entirely non-negative.", "y1");
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

		public static ISampleGenerator<float> MakeLinearSampleGenerator(this IRandom random, float x0, float y0, float x1, float y1)
		{
			return FloatLinearSampleGenerator.Create(random, x0, y0, x1, y1);
		}

		public static ISampleGenerator<float> MakeLinearSampleGenerator(this IRandom random, Vector2 p0, Vector2 p1)
		{
			return FloatLinearSampleGenerator.Create(random, p0.x, p0.y, p1.x, p1.y);
		}

		public static double LinearSample(this IRandom random, double x0, double y0, double x1, double y1)
		{
#if UNITY_EDITOR
			if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary.", "x1");
			if (y0 < 0d) throw new ArgumentException("The domain must be entirely non-negative.", "y0");
			if (y1 < 0d) throw new ArgumentException("The domain must be entirely non-negative.", "y1");
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
				if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary.", "x1");
				if (y0 < 0d) throw new ArgumentException("The domain must be entirely non-negative.", "y0");
				if (y1 < 0d) throw new ArgumentException("The domain must be entirely non-negative.", "y1");
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

		public static ISampleGenerator<double> MakeLinearSampleGenerator(this IRandom random, double x0, double y0, double x1, double y1)
		{
			return DoubleLinearSampleGenerator.Create(random, x0, y0, x1, y1);
		}

		#endregion

		#region Hermite Curve Distribution

		private static void CalculateHermiteCDFCoefficients(float x0, float y0, float m0, float x1, float y1, float m1, out float k4, out float k3, out float k2, out float k1, out float area)
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
			float k4, k3, k2, k1, area;
			CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);

			return FindRoot(k4, k3, k2, k1, area, t) * (x1 - x0) + x0;
		}

		private class FloatHermiteSampleGenerator : ISampleGenerator<float>
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
				if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary", "x1");
				if (y0 < 0f) throw new ArgumentException("The domain must be entirely non-negative", "y0");
				if (y1 < 0f) throw new ArgumentException("The domain must be entirely non-negative", "y1");

				float k4, k3, k2, k1, area;
				CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);

				if (k4 == 0f && k3 == 0f) return random.MakeLinearSampleGenerator(x0, y0, x1, y1);

				var generator = new FloatHermiteSampleGenerator();
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

		public static ISampleGenerator<float> MakeHermiteSampleGenerator(this IRandom random, Vector2 p0, float m0, Vector2 p1, float m1)
		{
			return FloatHermiteSampleGenerator.Create(random, p0.x, p0.y, m0, p1.x, p1.y, m1);
		}

		public static ISampleGenerator<float> MakeHermiteSampleGenerator(this IRandom random, float x0, float y0, float m0, float x1, float y1, float m1)
		{
			return FloatHermiteSampleGenerator.Create(random, x0, y0, m0, x1, y1, m1);
		}

		private static void CalculateHermiteCDFCoefficients(double x0, double y0, double m0, double x1, double y1, double m1, out double k4, out double k3, out double k2, out double k1, out double area)
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

		public static double HermiteSample(this IRandom random, double x0, double y0, double m0, double x1, double y1, double m1)
		{
#if UNITY_EDITOR
			if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary", "x1");
			if (y0 < 0d) throw new ArgumentException("The domain must be entirely non-negative", "y0");
			if (y1 < 0d) throw new ArgumentException("The domain must be entirely non-negative", "y1");
#endif

			return random.HermiteSample(x0, y0, m0, x1, y1, m1, random.DoubleCC());
		}

		private static double HermiteSample(this IRandom random, double x0, double y0, double m0, double x1, double y1, double m1, double t)
		{
			double k4, k3, k2, k1, area;
			CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);

			return FindRoot(k4, k3, k2, k1, area, t) * (x1 - x0) + x0;
		}

		private class DoubleHermiteSampleGenerator : ISampleGenerator<double>
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
				if (x0 >= x1) throw new ArgumentException("The upper range boundary must be greater than the lower range boundary", "x1");
				if (y0 < 0d) throw new ArgumentException("The domain must be entirely non-negative", "y0");
				if (y1 < 0d) throw new ArgumentException("The domain must be entirely non-negative", "y1");

				double k4, k3, k2, k1, area;
				CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);

				if (k4 == 0d && k3 == 0d) return random.MakeLinearSampleGenerator(x0, y0, x1, y1);

				var generator = new DoubleHermiteSampleGenerator();
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

		public static ISampleGenerator<double> MakeHermiteSampleGenerator(this IRandom random, double x0, double y0, double m0, double x1, double y1, double m1)
		{
			return DoubleHermiteSampleGenerator.Create(random, x0, y0, m0, x1, y1, m1);
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

		public static float PiecewiseUniformSample(this IRandom random, float[] x, float[] y)
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

		public static float PiecewiseUniformSample(this IRandom random, Vector2[] p, float xLast)
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

		public static float PiecewiseWeightedUniformSample(this IRandom random, float[] x, float[] weights)
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

				generator._cdf = new uint[y.Length];
				double totalWeight = 0d;
				for (int i = 0; i < y.Length; ++i)
				{
					totalWeight += (x[i + 1] - x[i]) * y[i];
				}

				double weightToIntScale = uint.MaxValue + 1d;
				totalWeight += 1d / weightToIntScale;

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

				generator._cdf = new uint[p.Length];
				double totalWeight = 0d;
				for (int i = 0; i < p.Length; ++i)
				{
					generator._x[i] = p[i].x;
					totalWeight += (p[i + 1].x - p[i].x) * p[i].y;
				}
				generator._x[p.Length] = xLast;

				double weightToIntScale = uint.MaxValue + 1d;
				totalWeight += 1d / weightToIntScale;

				double weightSum = 0d;
				for (int i = 0; i < p.Length; ++i)
				{
					weightSum += (p[i + 1].x - p[i].x) * p[i].y;
					generator._cdf[i] = (uint)Math.Floor(weightSum / totalWeight * weightToIntScale);
				}

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

				generator._cdf = new uint[weights.Length];
				double totalWeight = 0d;
				for (int i = 0; i < weights.Length; ++i)
				{
					totalWeight += weights[i];
				}

				double weightToIntScale = uint.MaxValue + 1d;
				totalWeight += 1d / weightToIntScale;

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

		public static ISampleGenerator<float> MakePiecewiseUniformSampleGenerator(this IRandom random, float[] x, float[] y)
		{
			return FloatPiecewiseUniformSampleGenerator.FromPositions(random, x, y);
		}

		public static ISampleGenerator<float> MakePiecewiseUniformSampleGenerator(this IRandom random, Vector2[] p, float xLast)
		{
			return FloatPiecewiseUniformSampleGenerator.FromPositions(random, p, xLast);
		}

		public static ISampleGenerator<float> MakePiecewiseWeightedUniformSampleGenerator(this IRandom random, float[] x, float[] weights)
		{
			return FloatPiecewiseUniformSampleGenerator.FromWeights(random, x, weights);
		}

		public static double PiecewiseUniformSample(this IRandom random, double[] x, double[] y)
		{
#if UNITY_EDITOR
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
				totalArea += (x1 - x0) * h;
				x0 = x1;
			}

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

		public static double PiecewiseWeightedUniformSample(this IRandom random, double[] x, double[] weights)
		{
#if UNITY_EDITOR
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length - weights.Length != 1) throw new ArgumentException("The array of weights must have exactly one fewer element than the array of x values.", "weights");
#endif

			double weightSum = 0f;
			for (int i = 0; i < weights.Length; ++i)
			{
				weightSum += weights[i];
			}

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

				generator._cdf = new ulong[y.Length];
				double totalWeight = 0d;
				for (int i = 0; i < y.Length; ++i)
				{
					totalWeight += (x[i + 1] - x[i]) * y[i];
				}

				double weightToIntScale = ulong.MaxValue + 1d;
				totalWeight += 1d / weightToIntScale;

				double weightSum = 0d;
				for (int i = 0; i < y.Length; ++i)
				{
					weightSum += (x[i + 1] - x[i]) * y[i];
					generator._cdf[i] = (ulong)Math.Floor(weightSum / totalWeight * weightToIntScale);
				}

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

				generator._cdf = new ulong[weights.Length];
				double totalWeight = 0d;
				for (int i = 0; i < weights.Length; ++i)
				{
					totalWeight += weights[i];
				}

				double weightToIntScale = ulong.MaxValue + 1d;
				totalWeight += 1d / weightToIntScale;

				double weightSum = 0d;
				for (int i = 0; i < weights.Length; ++i)
				{
					weightSum += weights[i];
					generator._cdf[i] = (ulong)Math.Floor(weightSum / totalWeight * weightToIntScale);
				}

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

		public static ISampleGenerator<double> MakePiecewiseUniformSampleGenerator(this IRandom random, double[] x, double[] y)
		{
			return DoublePiecewiseUniformSampleGenerator.FromPositions(random, x, y);
		}

		public static ISampleGenerator<double> MakePiecewiseWeightedUniformSampleGenerator(this IRandom random, double[] x, double[] weights)
		{
			return DoublePiecewiseUniformSampleGenerator.FromWeights(random, x, weights);
		}

		#endregion

		#region Piecewise Linear Distribution

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
					float area = (x1 - x0) * (y0 + y1);
					if (area == 0f) continue;
					doubleTotalArea += (x1 - x0) * (y0 + y1); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
					++segmentCount;
				}

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
					float area = (p1.x - p0.x) * (p0.y + p1.y);
					if (area == 0f) continue;
					doubleTotalArea += area; // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
					++segmentCount;
				}

				double areaToIntScale = uint.MaxValue + 1d;
				doubleTotalArea += 1d / areaToIntScale;

				_segments = new SegmentData[segmentCount];
				_cdf = new uint[segmentCount];

				double doubleAreaSum = 0d;
				for (int i = 1, j = 0; i < p.Length; ++i)
				{
					Vector2 p0 = p[i - 1];
					Vector2 p1 = p[i];
					float area = (p1.x - p0.x) * (p0.y + p1.y);
					if (area == 0f) continue;
					doubleAreaSum += area;
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

		public static ISampleGenerator<float> MakePiecewiseLinearSampleGenerator(this IRandom random, float[] x, float[] y)
		{
			return new FloatPiecewiseLinearSampleGenerator(random, x, y);
		}

		public static ISampleGenerator<float> MakePiecewiseLinearSampleGenerator(this IRandom random, Vector2[] p)
		{
			return new FloatPiecewiseLinearSampleGenerator(random, p);
		}

		public static double PiecewiseLinearSample(this IRandom random, double[] x, double[] y)
		{
#if UNITY_EDITOR
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
				doubleTotalArea += (x1 - x0) * (y0 + y1); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
				x0 = x1;
				y0 = y1;
			}

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
					double area = (x1 - x0) * (y0 + y1);
					if (area == 0f) continue;
					doubleTotalArea += (x1 - x0) * (y0 + y1); // Double the area of a trapeoid; no need to scale by one half, since it's all relative.
					++segmentCount;
				}

				double areaToIntScale = ulong.MaxValue + 1d;
				doubleTotalArea += 1d / areaToIntScale;

				_segments = new SegmentData[segmentCount];
				_cdf = new ulong[segmentCount];

				double doubleAreaSum = 0d;
				for (int i = 1, j = 0; i < x.Length; ++i)
				{
					double x0 = x[i - 1];
					double y0 = y[i - 1];
					double x1 = x[i];
					double y1 = y[i];
					double area = (x1 - x0) * (y0 + y1);
					if (area == 0f) continue;
					doubleAreaSum += area;
					_cdf[j] = (ulong)Math.Floor(doubleAreaSum / doubleTotalArea * areaToIntScale);
					_segments[j] = new SegmentData(x0, y0, x1, y1);
					++j;
				}
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

		public static ISampleGenerator<double> MakePiecewiseLinearSampleGenerator(this IRandom random, double[] x, double[] y)
		{
			return new DoublePiecewiseLinearSampleGenerator(random, x, y);
		}

		#endregion

		#region Piecewise Hermite Curve Distribution

		public static float PiecewiseHermiteSample(this IRandom random, float[] x, float[] y, float[] m)
		{
#if UNITY_EDITOR
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");
			if (m.Length != (x.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of x values.", "m");
#endif

			float totalArea = 0f;
			float x0 = x[0];
			float y0 = y[0];
			float m0 = m[0];
			for (int i = 1, j = 1; i < x.Length; ++i)
			{
				float x1 = x[i];
				float y1 = y[i];
				float m1 = m[j++];
				float xDelta = x1 - x0;
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += y0 * xDelta;
				}
				x0 = x1;
				y0 = y1;
				m0 = m[j++];
			}

			float n = random.RangeCC(totalArea);
			x0 = x[0];
			y0 = y[0];
			m0 = m[0];
			for (int i = 1, j = 1; i < x.Length; ++i)
			{
				float x1 = x[i];
				float y1 = y[i];
				float m1 = m[j++];
				float xDelta = x1 - x0;
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
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
				m0 = m[j++];
			}
			return x0;
		}

		public static float PiecewiseHermiteSample(this IRandom random, Vector2[] p, float[] m)
		{
#if UNITY_EDITOR
			if (p.Length < 2) throw new ArgumentException("The array of positions must have at least two elements.", "p");
			if (m.Length != (p.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of positions.", "m");
#endif

			float totalArea = 0f;
			Vector2 p0 = p[0];
			float m0 = m[0];
			for (int i = 1, j = 1; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
				float m1 = m[j++];
				float xDelta = p1.x - p0.x;
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteCDFCoefficients(p0.x, p0.y, m0, p1.x, p1.y, m1, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += p0.y * xDelta;
				}
				p0 = p1;
				m0 = m[j++];
			}

			float n = random.RangeCC(totalArea);
			p0 = p[0];
			m0 = m[0];
			for (int i = 1, j = 1; i < p.Length; ++i)
			{
				Vector2 p1 = p[i];
				float m1 = m[j++];
				float xDelta = p1.x - p0.x;
				if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
				{
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteCDFCoefficients(p0.x, p0.y, m0, p1.x, p1.y, m1, out k4, out k3, out k2, out k1, out area);
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
				m0 = m[j++];
			}
			return p0.x;
		}

		public static float PiecewiseHermiteSample(this IRandom random, AnimationCurve curve)
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
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteCDFCoefficients(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += kf0.value * xDelta;
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
					// Hermite Segment
					float k4, k3, k2, k1, area;
					CalculateHermiteCDFCoefficients(kf0.time, kf0.value, kf0.outTangent, kf1.time, kf1.value, kf1.inTangent, out k4, out k3, out k2, out k1, out area);
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

					CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
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

			public FloatPiecewiseHermiteSampleGenerator(IRandom random, Vector2[] p, float[] m)
			{
				if (p.Length < 2) throw new ArgumentException("The array of positions must have at least two elements.", "p");
				if (m.Length != (p.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of positions.", "m");

				_random = random;

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

			public FloatPiecewiseHermiteSampleGenerator(IRandom random, AnimationCurve curve)
			{
				if (curve.length < 2) throw new ArgumentException("The curve must have at least two keyframes.", "curve");

				_random = random;

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
					float xDelta = x1 - x0;
					if (!float.IsInfinity(m0) && !float.IsInfinity(m1))
					{
						// Hermite Segment
						if (y0 <= 0f && y1 <= 0f && m0 <= 0f && m1 >= 0f) continue;

						float k4, k3, k2, k1, area;
						CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
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

		public static ISampleGenerator<float> MakePiecewiseHermiteSampleGenerator(this IRandom random, float[] x, float[] y, float[] slopes)
		{
			return new FloatPiecewiseHermiteSampleGenerator(random, x, y, slopes);
		}

		public static ISampleGenerator<float> MakePiecewiseHermiteSampleGenerator(this IRandom random, Vector2[] p, float[] slopes)
		{
			return new FloatPiecewiseHermiteSampleGenerator(random, p, slopes);
		}

		public static ISampleGenerator<float> MakePiecewiseHermiteSampleGenerator(this IRandom random, AnimationCurve curve)
		{
			return new FloatPiecewiseHermiteSampleGenerator(random, curve);
		}

		public static double PiecewiseHermiteSample(this IRandom random, double[] x, double[] y, double[] m)
		{
#if UNITY_EDITOR
			if (x.Length < 2) throw new ArgumentException("The array of x values must have at least two elements.", "x");
			if (x.Length != y.Length) throw new ArgumentException("The array of y values must have exactly the same number of elements as the array of x values.", "y");
			if (m.Length != (x.Length - 1) * 2) throw new ArgumentException("The array of slopes must have exactly two less than double the number of elements as the array of x values.", "m");
#endif

			double totalArea = 0d;
			double x0 = x[0];
			double y0 = y[0];
			double m0 = m[0];
			for (int i = 1, j = 1; i < x.Length; ++i)
			{
				double x1 = x[i];
				double y1 = y[i];
				double m1 = m[j++];
				double xDelta = x1 - x0;
				if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
				{
					// Hermite Segment
					double k4, k3, k2, k1, area;
					CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
					totalArea += area * xDelta;
				}
				else
				{
					// Uniform Segment
					totalArea += y0 * xDelta;
				}
				x0 = x1;
				y0 = y1;
				m0 = m[j++];
			}

			double n = random.RangeCC(totalArea);
			x0 = x[0];
			y0 = y[0];
			m0 = m[0];
			for (int i = 1, j = 1; i < x.Length; ++i)
			{
				double x1 = x[i];
				double y1 = y[i];
				double m1 = m[j++];
				double xDelta = x1 - x0;
				if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
				{
					// Hermite Segment
					double k4, k3, k2, k1, area;
					CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
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
				m0 = m[j++];
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

					CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
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
					double xDelta = x1 - x0;
					if (!double.IsInfinity(m0) && !double.IsInfinity(m1))
					{
						// Hermite Segment
						if (y0 <= 0d && y1 <= 0d && m0 <= 0d && m1 >= 0d) continue;

						double k4, k3, k2, k1, area;
						CalculateHermiteCDFCoefficients(x0, y0, m0, x1, y1, m1, out k4, out k3, out k2, out k1, out area);
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

				double areaToIntScale = ulong.MaxValue + 1d;
				totalArea += 1d / areaToIntScale;

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
						_cdf[j] = (ulong)Math.Floor(areaSum / totalArea * areaToIntScale);
					}
					++j;
				}
			}

			public double Next()
			{
				int i = BinarySearch(_random.Next32(), _cdf);

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

		public static ISampleGenerator<double> MakePiecewiseHermiteSampleGenerator(this IRandom random, double[] x, double[] y, double[] slopes)
		{
			return new DoublePiecewiseHermiteSampleGenerator(random, x, y, slopes);
		}

		#endregion
	}
}
