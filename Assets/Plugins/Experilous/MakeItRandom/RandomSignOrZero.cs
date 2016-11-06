/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random integers from the set { -1, 0, +1 }.
	/// </summary>
	public static class RandomSignOrZero
	{
		#region Private Generators

		#region Uniform { -1, 0, +1 } Generators

		private class OneOrZeroGenerator : Detail.BufferedBitGenerator, IRangeGenerator<int>
		{
			public OneOrZeroGenerator(IRandom random) : base(random) { }

			public int Next()
			{
				return (int)Next32();
			}
		}

		private class SignGenerator : Detail.BufferedBitGenerator, IRangeGenerator<int>
		{
			public SignGenerator(IRandom random) : base(random) { }

			public int Next()
			{
				return (int)(Next32() << 1) - 1;
			}
		}

		private class SignOrZeroGenerator : Detail.BufferedAnyRangeGeneratorBase, IRangeGenerator<int>
		{
			public SignOrZeroGenerator(IRandom random) : base(random, 2UL, 3UL) { }

			public int Next()
			{
				return (int)Next32() - 1;
			}
		}

		#endregion

		#region Weighted { 0, +1 } Generators

		private class IntWeightedOneProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<int> _rangeGenerator;
			private int _numerator;

			public IntWeightedOneProbabilityGenerator(IRandom random, int numerator)
			{
				_rangeGenerator = random.MakeIntGenerator(true);
				_numerator = numerator;
			}

			public IntWeightedOneProbabilityGenerator(IRandom random, int numerator, int denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 1 : 0;
			}
		}

		private class UIntWeightedOneProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<uint> _rangeGenerator;
			private uint _numerator;

			public UIntWeightedOneProbabilityGenerator(IRandom random, uint numerator)
			{
				_rangeGenerator = random.MakeUIntGenerator();
				_numerator = numerator;
			}

			public UIntWeightedOneProbabilityGenerator(IRandom random, uint numerator, uint denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 1 : 0;
			}
		}

		private class LongWeightedOneProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<long> _rangeGenerator;
			private long _numerator;

			public LongWeightedOneProbabilityGenerator(IRandom random, long numerator)
			{
				_rangeGenerator = random.MakeLongGenerator(true);
				_numerator = numerator;
			}

			public LongWeightedOneProbabilityGenerator(IRandom random, long numerator, long denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 1 : 0;
			}
		}

		private class ULongWeightedOneProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<ulong> _rangeGenerator;
			private ulong _numerator;

			public ULongWeightedOneProbabilityGenerator(IRandom random, ulong numerator)
			{
				_rangeGenerator = random.MakeULongGenerator();
				_numerator = numerator;
			}

			public ULongWeightedOneProbabilityGenerator(IRandom random, ulong numerator, ulong denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 1 : 0;
			}
		}

		private class FloatWeightedOneProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<float> _rangeGenerator;
			private float _numerator;

			public FloatWeightedOneProbabilityGenerator(IRandom random, float probability)
			{
				_rangeGenerator = random.MakeFloatCOGenerator();
				_numerator = probability;
			}

			public FloatWeightedOneProbabilityGenerator(IRandom random, float numerator, float denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 1 : 0;
			}
		}

		private class DoubleWeightedOneProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<double> _rangeGenerator;
			private double _numerator;

			public DoubleWeightedOneProbabilityGenerator(IRandom random, double probability)
			{
				_rangeGenerator = random.MakeDoubleCOGenerator();
				_numerator = probability;
			}

			public DoubleWeightedOneProbabilityGenerator(IRandom random, double numerator, double denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 1 : 0;
			}
		}

		private class IntWeightedZeroProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<int> _rangeGenerator;
			private int _numerator;

			public IntWeightedZeroProbabilityGenerator(IRandom random, int numerator)
			{
				_rangeGenerator = random.MakeIntGenerator(true);
				_numerator = numerator;
			}

			public IntWeightedZeroProbabilityGenerator(IRandom random, int numerator, int denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 0 : 1;
			}
		}

		private class UIntWeightedZeroProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<uint> _rangeGenerator;
			private uint _numerator;

			public UIntWeightedZeroProbabilityGenerator(IRandom random, uint numerator)
			{
				_rangeGenerator = random.MakeUIntGenerator();
				_numerator = numerator;
			}

			public UIntWeightedZeroProbabilityGenerator(IRandom random, uint numerator, uint denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 0 : 1;
			}
		}

		private class LongWeightedZeroProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<long> _rangeGenerator;
			private long _numerator;

			public LongWeightedZeroProbabilityGenerator(IRandom random, long numerator)
			{
				_rangeGenerator = random.MakeLongGenerator(true);
				_numerator = numerator;
			}

			public LongWeightedZeroProbabilityGenerator(IRandom random, long numerator, long denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 0 : 1;
			}
		}

		private class ULongWeightedZeroProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<ulong> _rangeGenerator;
			private ulong _numerator;

			public ULongWeightedZeroProbabilityGenerator(IRandom random, ulong numerator)
			{
				_rangeGenerator = random.MakeULongGenerator();
				_numerator = numerator;
			}

			public ULongWeightedZeroProbabilityGenerator(IRandom random, ulong numerator, ulong denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 0 : 1;
			}
		}

		private class FloatWeightedZeroProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<float> _rangeGenerator;
			private float _numerator;

			public FloatWeightedZeroProbabilityGenerator(IRandom random, float probability)
			{
				_rangeGenerator = random.MakeFloatCOGenerator();
				_numerator = probability;
			}

			public FloatWeightedZeroProbabilityGenerator(IRandom random, float numerator, float denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 0 : 1;
			}
		}

		private class DoubleWeightedZeroProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<double> _rangeGenerator;
			private double _numerator;

			public DoubleWeightedZeroProbabilityGenerator(IRandom random, double probability)
			{
				_rangeGenerator = random.MakeDoubleCOGenerator();
				_numerator = probability;
			}

			public DoubleWeightedZeroProbabilityGenerator(IRandom random, double numerator, double denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? 0 : 1;
			}
		}

		#endregion

		#region Weighted { -1, +1 } Generators

		private class IntWeightedPositiveProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<int> _rangeGenerator;
			private int _numerator;

			public IntWeightedPositiveProbabilityGenerator(IRandom random, int numerator)
			{
				_rangeGenerator = random.MakeIntGenerator(true);
				_numerator = numerator;
			}

			public IntWeightedPositiveProbabilityGenerator(IRandom random, int numerator, int denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? +1 : -1;
			}
		}

		private class UIntWeightedPositiveProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<uint> _rangeGenerator;
			private uint _numerator;

			public UIntWeightedPositiveProbabilityGenerator(IRandom random, uint numerator)
			{
				_rangeGenerator = random.MakeUIntGenerator();
				_numerator = numerator;
			}

			public UIntWeightedPositiveProbabilityGenerator(IRandom random, uint numerator, uint denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? +1 : -1;
			}
		}

		private class LongWeightedPositiveProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<long> _rangeGenerator;
			private long _numerator;

			public LongWeightedPositiveProbabilityGenerator(IRandom random, long numerator)
			{
				_rangeGenerator = random.MakeLongGenerator(true);
				_numerator = numerator;
			}

			public LongWeightedPositiveProbabilityGenerator(IRandom random, long numerator, long denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? +1 : -1;
			}
		}

		private class ULongWeightedPositiveProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<ulong> _rangeGenerator;
			private ulong _numerator;

			public ULongWeightedPositiveProbabilityGenerator(IRandom random, ulong numerator)
			{
				_rangeGenerator = random.MakeULongGenerator();
				_numerator = numerator;
			}

			public ULongWeightedPositiveProbabilityGenerator(IRandom random, ulong numerator, ulong denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? +1 : -1;
			}
		}

		private class FloatWeightedPositiveProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<float> _rangeGenerator;
			private float _numerator;

			public FloatWeightedPositiveProbabilityGenerator(IRandom random, float probability)
			{
				_rangeGenerator = random.MakeFloatCOGenerator();
				_numerator = probability;
			}

			public FloatWeightedPositiveProbabilityGenerator(IRandom random, float numerator, float denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? +1 : -1;
			}
		}

		private class DoubleWeightedPositiveProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<double> _rangeGenerator;
			private double _numerator;

			public DoubleWeightedPositiveProbabilityGenerator(IRandom random, double probability)
			{
				_rangeGenerator = random.MakeDoubleCOGenerator();
				_numerator = probability;
			}

			public DoubleWeightedPositiveProbabilityGenerator(IRandom random, double numerator, double denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? +1 : -1;
			}
		}

		private class IntWeightedNegativeProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<int> _rangeGenerator;
			private int _numerator;

			public IntWeightedNegativeProbabilityGenerator(IRandom random, int numerator)
			{
				_rangeGenerator = random.MakeIntGenerator(true);
				_numerator = numerator;
			}

			public IntWeightedNegativeProbabilityGenerator(IRandom random, int numerator, int denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? -1 : +1;
			}
		}

		private class UIntWeightedNegativeProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<uint> _rangeGenerator;
			private uint _numerator;

			public UIntWeightedNegativeProbabilityGenerator(IRandom random, uint numerator)
			{
				_rangeGenerator = random.MakeUIntGenerator();
				_numerator = numerator;
			}

			public UIntWeightedNegativeProbabilityGenerator(IRandom random, uint numerator, uint denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? -1 : +1;
			}
		}

		private class LongWeightedNegativeProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<long> _rangeGenerator;
			private long _numerator;

			public LongWeightedNegativeProbabilityGenerator(IRandom random, long numerator)
			{
				_rangeGenerator = random.MakeLongGenerator(true);
				_numerator = numerator;
			}

			public LongWeightedNegativeProbabilityGenerator(IRandom random, long numerator, long denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? -1 : +1;
			}
		}

		private class ULongWeightedNegativeProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<ulong> _rangeGenerator;
			private ulong _numerator;

			public ULongWeightedNegativeProbabilityGenerator(IRandom random, ulong numerator)
			{
				_rangeGenerator = random.MakeULongGenerator();
				_numerator = numerator;
			}

			public ULongWeightedNegativeProbabilityGenerator(IRandom random, ulong numerator, ulong denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? -1 : +1;
			}
		}

		private class FloatWeightedNegativeProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<float> _rangeGenerator;
			private float _numerator;

			public FloatWeightedNegativeProbabilityGenerator(IRandom random, float probability)
			{
				_rangeGenerator = random.MakeFloatCOGenerator();
				_numerator = probability;
			}

			public FloatWeightedNegativeProbabilityGenerator(IRandom random, float numerator, float denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? -1 : +1;
			}
		}

		private class DoubleWeightedNegativeProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<double> _rangeGenerator;
			private double _numerator;

			public DoubleWeightedNegativeProbabilityGenerator(IRandom random, double probability)
			{
				_rangeGenerator = random.MakeDoubleCOGenerator();
				_numerator = probability;
			}

			public DoubleWeightedNegativeProbabilityGenerator(IRandom random, double numerator, double denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public int Next()
			{
				return _rangeGenerator.Next() < _numerator ? -1 : +1;
			}
		}

		#endregion

		#region Weighted { -1, 0, +1 } Generators

		private class IntWeightedSignProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<int> _rangeGenerator;
			private int _numeratorNonZero;
			private int _numeratorPositive;

			public IntWeightedSignProbabilityGenerator(IRandom random, int numeratorPositive, int numeratorNegative)
			{
				_rangeGenerator = random.MakeIntGenerator(true);
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public IntWeightedSignProbabilityGenerator(IRandom random, int numeratorPositive, int numeratorNegative, int denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public int Next()
			{
				int n = _rangeGenerator.Next();
				return n < _numeratorNonZero ? (n < _numeratorPositive ? +1 : -1) : 0;
			}
		}

		private class UIntWeightedSignProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<uint> _rangeGenerator;
			private uint _numeratorNonZero;
			private uint _numeratorPositive;

			public UIntWeightedSignProbabilityGenerator(IRandom random, uint numeratorPositive, uint numeratorNegative)
			{
				_rangeGenerator = random.MakeUIntGenerator();
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public UIntWeightedSignProbabilityGenerator(IRandom random, uint numeratorPositive, uint numeratorNegative, uint denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public int Next()
			{
				uint n = _rangeGenerator.Next();
				return n < _numeratorNonZero ? (n < _numeratorPositive ? +1 : -1) : 0;
			}
		}

		private class LongWeightedSignProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<long> _rangeGenerator;
			private long _numeratorNonZero;
			private long _numeratorPositive;

			public LongWeightedSignProbabilityGenerator(IRandom random, long numeratorPositive, long numeratorNegative)
			{
				_rangeGenerator = random.MakeLongGenerator(true);
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public LongWeightedSignProbabilityGenerator(IRandom random, long numeratorPositive, long numeratorNegative, long denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public int Next()
			{
				long n = _rangeGenerator.Next();
				return n < _numeratorNonZero ? (n < _numeratorPositive ? +1 : -1) : 0;
			}
		}

		private class ULongWeightedSignProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<ulong> _rangeGenerator;
			private ulong _numeratorNonZero;
			private ulong _numeratorPositive;

			public ULongWeightedSignProbabilityGenerator(IRandom random, ulong numeratorPositive, ulong numeratorNegative)
			{
				_rangeGenerator = random.MakeULongGenerator();
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public ULongWeightedSignProbabilityGenerator(IRandom random, ulong numeratorPositive, ulong numeratorNegative, ulong denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public int Next()
			{
				ulong n = _rangeGenerator.Next();
				return n < _numeratorNonZero ? (n < _numeratorPositive ? +1 : -1) : 0;
			}
		}

		private class FloatWeightedSignProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<float> _rangeGenerator;
			private float _numeratorNonZero;
			private float _numeratorPositive;

			public FloatWeightedSignProbabilityGenerator(IRandom random, float probabilityPositive, float probabilityNegative)
			{
				_rangeGenerator = random.MakeFloatCOGenerator();
				_numeratorNonZero = probabilityPositive + probabilityNegative;
				_numeratorPositive = probabilityPositive;
			}

			public FloatWeightedSignProbabilityGenerator(IRandom random, float numeratorPositive, float numeratorNegative, float denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public int Next()
			{
				float n = _rangeGenerator.Next();
				return n < _numeratorNonZero ? (n < _numeratorPositive ? +1 : -1) : 0;
			}
		}

		private class DoubleWeightedSignProbabilityGenerator : IRangeGenerator<int>
		{
			private IRangeGenerator<double> _rangeGenerator;
			private double _numeratorNonZero;
			private double _numeratorPositive;

			public DoubleWeightedSignProbabilityGenerator(IRandom random, double probabilityPositive, double probabilityNegative)
			{
				_rangeGenerator = random.MakeDoubleCOGenerator();
				_numeratorNonZero = probabilityPositive + probabilityNegative;
				_numeratorPositive = probabilityPositive;
			}

			public DoubleWeightedSignProbabilityGenerator(IRandom random, double numeratorPositive, double numeratorNegative, double denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numeratorNonZero = numeratorPositive + numeratorNegative;
				_numeratorPositive = numeratorPositive;
			}

			public int Next()
			{
				double n = _rangeGenerator.Next();
				return n < _numeratorNonZero ? (n < _numeratorPositive ? +1 : -1) : 0;
			}
		}

		#endregion

		#endregion

		#region {-1, 0, +1}, Evenly Weighted

		/// <summary>
		/// Returns a random integer with exacty a half and half chance of being positive one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random uniformly distributed integer from the set { 0, +1 }.</returns>
		public static int OneOrZero(this IRandom random)
		{
			return (int)(random.Next32() >> 31);
		}

		/// <summary>
		/// Returns an integer generator which will produce numbers with exacty a half and half chance of being positive one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>An integer generator which will produce numbers with exacty a half and half chance of being positive one or zero.</returns>
		/// <seealso cref="OneOrZero(IRandom)"/>
		public static IRangeGenerator<int> MakeOneOrZeroGenerator(this IRandom random)
		{
			return new OneOrZeroGenerator(random);
		}

		/// <summary>
		/// Returns a random integer with exacty a half and half chance of being positive one or negative one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random uniformly distributed integer from the set { -1, +1 }.</returns>
		public static int Sign(this IRandom random)
		{
			return (int)((random.Next32() >> 30) & 2U) - 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce numbers with exacty a half and half chance of being positive one or negative one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>An integer generator which will produce numbers with exacty a half and half chance of being positive one or negative one.</returns>
		/// <seealso cref="Sign(IRandom)"/>
		public static IRangeGenerator<int> MakeSignGenerator(this IRandom random)
		{
			return new SignGenerator(random);
		}

		/// <summary>
		/// Returns a random integer with exacty a one third chance each of being positive one, zero, or negative one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random uniformly distributed integer from the set { -1, 0, +1 }.</returns>
		public static int SignOrZero(this IRandom random)
		{
			uint n;
			do
			{
				n = random.Next32();
			}
			while (n >= 0xC0000000U);
			return (int)(n >> 30) - 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce numbers with exacty a one third chance each of being positive one, zero, or negative one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>An integer generator which will produce numbers with exacty a one third chance each of being positive one, zero, or negative one.</returns>
		/// <seealso cref="SignOrZero(IRandom)"/>
		public static IRangeGenerator<int> MakeSignOrZeroGenerator(this IRandom random)
		{
			return new SignOrZeroGenerator(random);
		}

		#endregion

		#region {-1, 0, +1}, Unevenly Weighted

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int OneOrZero(this IRandom random, int ratioOne, int ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneOrZero(IRandom, int, int)"/>
		public static IRangeGenerator<int> MakeOneOrZeroGenerator(this IRandom random, int ratioOne, int ratioZero)
		{
			return new IntWeightedOneProbabilityGenerator(random, ratioOne, ratioOne + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int OneOrZero(this IRandom random, uint ratioOne, uint ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneOrZero(IRandom, uint, uint)"/>
		public static IRangeGenerator<int> MakeOneOrZeroGenerator(this IRandom random, uint ratioOne, uint ratioZero)
		{
			return new UIntWeightedOneProbabilityGenerator(random, ratioOne, ratioOne + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int OneOrZero(this IRandom random, long ratioOne, long ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneOrZero(IRandom, long, long)"/>
		public static IRangeGenerator<int> MakeOneOrZeroGenerator(this IRandom random, long ratioOne, long ratioZero)
		{
			return new LongWeightedOneProbabilityGenerator(random, ratioOne, ratioOne + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int OneOrZero(this IRandom random, ulong ratioOne, ulong ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneOrZero(IRandom, ulong, ulong)"/>
		public static IRangeGenerator<int> MakeOneOrZeroGenerator(this IRandom random, ulong ratioOne, ulong ratioZero)
		{
			return new ULongWeightedOneProbabilityGenerator(random, ratioOne, ratioOne + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int OneOrZero(this IRandom random, float ratioOne, float ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneOrZero(IRandom, float, float)"/>
		public static IRangeGenerator<int> MakeOneOrZeroGenerator(this IRandom random, float ratioOne, float ratioZero)
		{
			return new FloatWeightedOneProbabilityGenerator(random, ratioOne, ratioOne + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int OneOrZero(this IRandom random, double ratioOne, double ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneOrZero(IRandom, double, double)"/>
		public static IRangeGenerator<int> MakeOneOrZeroGenerator(this IRandom random, double ratioOne, double ratioZero)
		{
			return new DoubleWeightedOneProbabilityGenerator(random, ratioOne, ratioOne + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int Sign(this IRandom random, int ratioPositive, int ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="Sign(IRandom, int, int)"/>
		public static IRangeGenerator<int> MakeSignGenerator(this IRandom random, int ratioPositive, int ratioNegative)
		{
			return new IntWeightedPositiveProbabilityGenerator(random, ratioPositive, ratioPositive + ratioNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int Sign(this IRandom random, uint ratioPositive, uint ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="Sign(IRandom, uint, uint)"/>
		public static IRangeGenerator<int> MakeSignGenerator(this IRandom random, uint ratioPositive, uint ratioNegative)
		{
			return new UIntWeightedPositiveProbabilityGenerator(random, ratioPositive, ratioPositive + ratioNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int Sign(this IRandom random, long ratioPositive, long ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="Sign(IRandom, long, long)"/>
		public static IRangeGenerator<int> MakeSignGenerator(this IRandom random, long ratioPositive, long ratioNegative)
		{
			return new LongWeightedPositiveProbabilityGenerator(random, ratioPositive, ratioPositive + ratioNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int Sign(this IRandom random, ulong ratioPositive, ulong ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="Sign(IRandom, ulong, ulong)"/>
		public static IRangeGenerator<int> MakeSignGenerator(this IRandom random, ulong ratioPositive, ulong ratioNegative)
		{
			return new ULongWeightedPositiveProbabilityGenerator(random, ratioPositive, ratioPositive + ratioNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int Sign(this IRandom random, float ratioPositive, float ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="Sign(IRandom, float, float)"/>
		public static IRangeGenerator<int> MakeSignGenerator(this IRandom random, float ratioPositive, float ratioNegative)
		{
			return new FloatWeightedPositiveProbabilityGenerator(random, ratioPositive, ratioPositive + ratioNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int Sign(this IRandom random, double ratioPositive, double ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="Sign(IRandom, double, double)"/>
		public static IRangeGenerator<int> MakeSignGenerator(this IRandom random, double ratioPositive, double ratioNegative)
		{
			return new DoubleWeightedPositiveProbabilityGenerator(random, ratioPositive, ratioPositive + ratioNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, int ratioPositive, int ratioNegative, int ratioZero)
		{
			int ratioNonZero = ratioPositive + ratioNegative;
			int n = random.RangeCO(ratioNonZero + ratioZero);
			return n < ratioNonZero ? (n < ratioPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignOrZero(IRandom, int, int, int)"/>
		public static IRangeGenerator<int> MakeSignOrZeroGenerator(this IRandom random, int ratioPositive, int ratioNegative, int ratioZero)
		{
			return new IntWeightedSignProbabilityGenerator(random, ratioPositive, ratioNegative, ratioPositive + ratioNegative + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, uint ratioPositive, uint ratioNegative, uint ratioZero)
		{
			uint ratioNonZero = ratioPositive + ratioNegative;
			uint n = random.RangeCO(ratioNonZero + ratioZero);
			return n < ratioNonZero ? (n < ratioPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignOrZero(IRandom, uint, uint, uint)"/>
		public static IRangeGenerator<int> MakeSignOrZeroGenerator(this IRandom random, uint ratioPositive, uint ratioNegative, uint ratioZero)
		{
			return new UIntWeightedSignProbabilityGenerator(random, ratioPositive, ratioNegative, ratioPositive + ratioNegative + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, long ratioPositive, long ratioNegative, long ratioZero)
		{
			long ratioNonZero = ratioPositive + ratioNegative;
			long n = random.RangeCO(ratioNonZero + ratioZero);
			return n < ratioNonZero ? (n < ratioPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignOrZero(IRandom, long, long, long)"/>
		public static IRangeGenerator<int> MakeSignOrZeroGenerator(this IRandom random, long ratioPositive, long ratioNegative, long ratioZero)
		{
			return new LongWeightedSignProbabilityGenerator(random, ratioPositive, ratioNegative, ratioPositive + ratioNegative + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, ulong ratioPositive, ulong ratioNegative, ulong ratioZero)
		{
			ulong ratioNonZero = ratioPositive + ratioNegative;
			ulong n = random.RangeCO(ratioNonZero + ratioZero);
			return n < ratioNonZero ? (n < ratioPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignOrZero(IRandom, ulong, ulong, ulong)"/>
		public static IRangeGenerator<int> MakeSignOrZeroGenerator(this IRandom random, ulong ratioPositive, ulong ratioNegative, ulong ratioZero)
		{
			return new ULongWeightedSignProbabilityGenerator(random, ratioPositive, ratioNegative, ratioPositive + ratioNegative + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, float ratioPositive, float ratioNegative, float ratioZero)
		{
			float ratioNonZero = ratioPositive + ratioNegative;
			float n = random.RangeCO(ratioNonZero + ratioZero);
			return n < ratioNonZero ? (n < ratioPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignOrZero(IRandom, float, float, float)"/>
		public static IRangeGenerator<int> MakeSignOrZeroGenerator(this IRandom random, float ratioPositive, float ratioNegative, float ratioZero)
		{
			return new FloatWeightedSignProbabilityGenerator(random, ratioPositive, ratioNegative, ratioPositive + ratioNegative + ratioZero);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, double ratioPositive, double ratioNegative, double ratioZero)
		{
			double ratioNonZero = ratioPositive + ratioNegative;
			double n = random.RangeCO(ratioNonZero + ratioZero);
			return n < ratioNonZero ? (n < ratioPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignOrZero(IRandom, double, double, double)"/>
		public static IRangeGenerator<int> MakeSignOrZeroGenerator(this IRandom random, double ratioPositive, double ratioNegative, double ratioZero)
		{
			return new DoubleWeightedSignProbabilityGenerator(random, ratioPositive, ratioNegative, ratioPositive + ratioNegative + ratioZero);
		}

		#endregion

		#region {-1, 0, +1}, Probability

		#region One Probability

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^31 that the result will be +1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, int numerator)
		{
			return random.IntNonNegative() < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^31 that the result will be +1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, int)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, int numerator)
		{
			return new IntWeightedOneProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^32 that the result will be +1.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, uint numerator)
		{
			return random.UInt() < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^32 that the result will be +1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, uint)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, uint numerator)
		{
			return new UIntWeightedOneProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be +1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, long numerator)
		{
			return random.LongNonNegative() < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be +1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, long)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, long numerator)
		{
			return new LongWeightedOneProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^64 that the result will be +1.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, ulong numerator)
		{
			return random.ULong() < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^64 that the result will be +1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, ulong)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, ulong numerator)
		{
			return new ULongWeightedOneProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, float probability)
		{
			return random.FloatCO() < probability ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, float)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, float probability)
		{
			return new FloatWeightedOneProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, double probability)
		{
			return random.DoubleCO() < probability ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, double)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, double probability)
		{
			return new DoubleWeightedOneProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, int numerator, int denominator)
		{
			return random.RangeCO(denominator) < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, int, int)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, int numerator, int denominator)
		{
			return new IntWeightedOneProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, uint numerator, uint denominator)
		{
			return random.RangeCO(denominator) < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, uint, uint)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, uint numerator, uint denominator)
		{
			return new UIntWeightedOneProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, long numerator, long denominator)
		{
			return random.RangeCO(denominator) < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, long, long)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, long numerator, long denominator)
		{
			return new LongWeightedOneProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, ulong numerator, ulong denominator)
		{
			return random.RangeCO(denominator) < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, ulong, ulong)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, ulong numerator, ulong denominator)
		{
			return new ULongWeightedOneProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, float numerator, float denominator)
		{
			return random.RangeCO(denominator) < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, float, float)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, float numerator, float denominator)
		{
			return new FloatWeightedOneProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, double numerator, double denominator)
		{
			return random.RangeCO(denominator) < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, double, double)"/>
		public static IRangeGenerator<int> MakeOneProbabilityGenerator(this IRandom random, double numerator, double denominator)
		{
			return new DoubleWeightedOneProbabilityGenerator(random, numerator, denominator);
		}

		#endregion

		#region Zero Probability

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^31 that the result will be 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, int numerator)
		{
			return random.IntNonNegative() < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^31 that the result will be 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, int)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, int numerator)
		{
			return new IntWeightedZeroProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^32 that the result will be 0.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, uint numerator)
		{
			return random.UInt() < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^32 that the result will be 0.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, uint)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, uint numerator)
		{
			return new UIntWeightedZeroProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, long numerator)
		{
			return random.LongNonNegative() < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be 0.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, long)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, long numerator)
		{
			return new LongWeightedZeroProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^64 that the result will be 0.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, ulong numerator)
		{
			return random.ULong() < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^64 that the result will be 0.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, ulong)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, ulong numerator)
		{
			return new ULongWeightedZeroProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a zero result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, float probability)
		{
			return random.FloatCO() < probability ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a zero result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, float)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, float probability)
		{
			return new FloatWeightedZeroProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a zero result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, double probability)
		{
			return random.DoubleCO() < probability ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a zero result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, double)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, double probability)
		{
			return new DoubleWeightedZeroProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, int numerator, int denominator)
		{
			return random.RangeCO(denominator) < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, int, int)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, int numerator, int denominator)
		{
			return new IntWeightedZeroProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, uint numerator, uint denominator)
		{
			return random.RangeCO(denominator) < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, uint, uint)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, uint numerator, uint denominator)
		{
			return new UIntWeightedZeroProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, long numerator, long denominator)
		{
			return random.RangeCO(denominator) < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, long, long)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, long numerator, long denominator)
		{
			return new LongWeightedZeroProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, ulong numerator, ulong denominator)
		{
			return random.RangeCO(denominator) < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, ulong, ulong)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, ulong numerator, ulong denominator)
		{
			return new ULongWeightedZeroProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, float numerator, float denominator)
		{
			return random.RangeCO(denominator) < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, float, float)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, float numerator, float denominator)
		{
			return new FloatWeightedZeroProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int ZeroProbability(this IRandom random, double numerator, double denominator)
		{
			return random.RangeCO(denominator) < numerator ? 0 : 1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a zero result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be 0.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="ZeroProbability(IRandom, double, double)"/>
		public static IRangeGenerator<int> MakeZeroProbabilityGenerator(this IRandom random, double numerator, double denominator)
		{
			return new DoubleWeightedZeroProbabilityGenerator(random, numerator, denominator);
		}

		#endregion

		#region Positive Probability

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^31 that the result will be +1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, int numerator)
		{
			return random.IntNonNegative() < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^31 that the result will be +1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, int)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, int numerator)
		{
			return new IntWeightedPositiveProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^32 that the result will be +1.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, uint numerator)
		{
			return random.UInt() < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^32 that the result will be +1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, uint)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, uint numerator)
		{
			return new UIntWeightedPositiveProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be +1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, long numerator)
		{
			return random.LongNonNegative() < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be +1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, long)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, long numerator)
		{
			return new LongWeightedPositiveProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^64 that the result will be +1.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, ulong numerator)
		{
			return random.ULong() < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^64 that the result will be +1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, ulong)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, ulong numerator)
		{
			return new ULongWeightedPositiveProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, float probability)
		{
			return random.FloatCO() < probability ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, float)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, float probability)
		{
			return new FloatWeightedPositiveProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, double probability)
		{
			return random.DoubleCO() < probability ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, double)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, double probability)
		{
			return new DoubleWeightedPositiveProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, int numerator, int denominator)
		{
			return random.RangeCO(denominator) < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, int, int)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, int numerator, int denominator)
		{
			return new IntWeightedPositiveProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, uint numerator, uint denominator)
		{
			return random.RangeCO(denominator) < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, uint, uint)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, uint numerator, uint denominator)
		{
			return new UIntWeightedPositiveProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, long numerator, long denominator)
		{
			return random.RangeCO(denominator) < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, long, long)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, long numerator, long denominator)
		{
			return new LongWeightedPositiveProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, ulong numerator, ulong denominator)
		{
			return random.RangeCO(denominator) < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, ulong, ulong)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, ulong numerator, ulong denominator)
		{
			return new ULongWeightedPositiveProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, float numerator, float denominator)
		{
			return random.RangeCO(denominator) < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, float, float)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, float numerator, float denominator)
		{
			return new FloatWeightedPositiveProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, double numerator, double denominator)
		{
			return random.RangeCO(denominator) < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, double, double)"/>
		public static IRangeGenerator<int> MakePositiveProbabilityGenerator(this IRandom random, double numerator, double denominator)
		{
			return new DoubleWeightedPositiveProbabilityGenerator(random, numerator, denominator);
		}

		#endregion

		#region Negative Probability

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^31 that the result will be -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, int numerator)
		{
			return random.IntNonNegative() < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^31 that the result will be -1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, int)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, int numerator)
		{
			return new IntWeightedNegativeProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^32 that the result will be -1.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, uint numerator)
		{
			return random.UInt() < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^32 that the result will be -1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, uint)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, uint numerator)
		{
			return new UIntWeightedNegativeProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, long numerator)
		{
			return random.LongNonNegative() < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be -1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, long)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, long numerator)
		{
			return new LongWeightedNegativeProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of 2^64 that the result will be -1.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, ulong numerator)
		{
			return random.ULong() < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^64 that the result will be -1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, ulong)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, ulong numerator)
		{
			return new ULongWeightedNegativeProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, float probability)
		{
			return random.FloatCO() < probability ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, float)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, float probability)
		{
			return new FloatWeightedNegativeProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, double probability)
		{
			return random.DoubleCO() < probability ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, double)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, double probability)
		{
			return new DoubleWeightedNegativeProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, int numerator, int denominator)
		{
			return random.RangeCO(denominator) < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, int, int)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, int numerator, int denominator)
		{
			return new IntWeightedNegativeProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, uint numerator, uint denominator)
		{
			return random.RangeCO(denominator) < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, uint, uint)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, uint numerator, uint denominator)
		{
			return new UIntWeightedNegativeProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, long numerator, long denominator)
		{
			return random.RangeCO(denominator) < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, long, long)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, long numerator, long denominator)
		{
			return new LongWeightedNegativeProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, ulong numerator, ulong denominator)
		{
			return random.RangeCO(denominator) < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, ulong, ulong)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, ulong numerator, ulong denominator)
		{
			return new ULongWeightedNegativeProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, float numerator, float denominator)
		{
			return random.RangeCO(denominator) < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, float, float)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, float numerator, float denominator)
		{
			return new FloatWeightedNegativeProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, double numerator, double denominator)
		{
			return random.RangeCO(denominator) < numerator ? -1 : +1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a negative one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="NegativeProbability(IRandom, double, double)"/>
		public static IRangeGenerator<int> MakeNegativeProbabilityGenerator(this IRandom random, double numerator, double denominator)
		{
			return new DoubleWeightedNegativeProbabilityGenerator(random, numerator, denominator);
		}

		#endregion

		#region Sign Probability

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/2^31 and the probability of a negative one result is <paramref name="numeratorNegative"/>/2^31.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of 2^31 that the result will be +1.  Must be non-negative.</param>
		/// <param name="numeratorNegative">The average number of times out of 2^31 that the result will be -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, int numeratorPositive, int numeratorNegative)
		{
			int numeratorNonZero = numeratorPositive + numeratorNegative;
			int n = random.IntNonNegative();
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/2^31 and the probability of a negative one result is <paramref name="numeratorNegative"/>/2^31.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of 2^31 that the result will be +1.  Must be non-negative.</param>
		/// <param name="numeratorNegative">The average number of times out of 2^31 that the result will be -1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, int, int)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, int numeratorPositive, int numeratorNegative)
		{
			return new IntWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/2^32 and the probability of a negative one result is <paramref name="numeratorNegative"/>/2^32.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of 2^32 that the result will be +1.</param>
		/// <param name="numeratorNegative">The average number of times out of 2^32 that the result will be -1.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, uint numeratorPositive, uint numeratorNegative)
		{
			uint numeratorNonZero = numeratorPositive + numeratorNegative;
			uint n = random.UInt();
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/2^32 and the probability of a negative one result is <paramref name="numeratorNegative"/>/2^32.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of 2^32 that the result will be +1.</param>
		/// <param name="numeratorNegative">The average number of times out of 2^32 that the result will be -1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, uint, uint)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, uint numeratorPositive, uint numeratorNegative)
		{
			return new UIntWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/2^63 and the probability of a negative one result is <paramref name="numeratorNegative"/>/2^63.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of 2^63 that the result will be +1.  Must be non-negative.</param>
		/// <param name="numeratorNegative">The average number of times out of 2^63 that the result will be -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, long numeratorPositive, long numeratorNegative)
		{
			long numeratorNonZero = numeratorPositive + numeratorNegative;
			long n = random.LongNonNegative();
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/2^63 and the probability of a negative one result is <paramref name="numeratorNegative"/>/2^63.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of 2^63 that the result will be +1.  Must be non-negative.</param>
		/// <param name="numeratorNegative">The average number of times out of 2^63 that the result will be -1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, long, long)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, long numeratorPositive, long numeratorNegative)
		{
			return new LongWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/2^64 and the probability of a negative one result is <paramref name="numeratorNegative"/>/2^64.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of 2^64 that the result will be +1.</param>
		/// <param name="numeratorNegative">The average number of times out of 2^64 that the result will be -1.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, ulong numeratorPositive, ulong numeratorNegative)
		{
			ulong numeratorNonZero = numeratorPositive + numeratorNegative;
			ulong n = random.ULong();
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/2^64 and the probability of a negative one result is <paramref name="numeratorNegative"/>/2^64.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of 2^64 that the result will be +1.</param>
		/// <param name="numeratorNegative">The average number of times out of 2^64 that the result will be -1.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, ulong, ulong)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, ulong numeratorPositive, ulong numeratorNegative)
		{
			return new ULongWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probabilityPositive"/> and the probability of a positive one result is set by the parameter <paramref name="probabilityNegative"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probabilityPositive">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <param name="probabilityNegative">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to 1.</remarks>
		public static int SignProbability(this IRandom random, float probabilityPositive, float probabilityNegative)
		{
			float probabilityNonZero = probabilityPositive + probabilityNegative;
			float n = random.FloatCO();
			return n < probabilityNonZero ? (n < probabilityPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probabilityPositive"/> and the probability of a positive one result is set by the parameter <paramref name="probabilityNegative"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probabilityPositive">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <param name="probabilityNegative">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, float, float)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, float probabilityPositive, float probabilityNegative)
		{
			return new FloatWeightedSignProbabilityGenerator(random, probabilityPositive, probabilityNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probabilityPositive"/> and the probability of a positive one result is set by the parameter <paramref name="probabilityNegative"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probabilityPositive">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <param name="probabilityNegative">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to 1.</remarks>
		public static int SignProbability(this IRandom random, double probabilityPositive, double probabilityNegative)
		{
			double probabilityNonZero = probabilityPositive + probabilityNegative;
			double n = random.DoubleCO();
			return n < probabilityNonZero ? (n < probabilityPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probabilityPositive"/> and the probability of a positive one result is set by the parameter <paramref name="probabilityNegative"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probabilityPositive">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <param name="probabilityNegative">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, double, double)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, double probabilityPositive, double probabilityNegative)
		{
			return new DoubleWeightedSignProbabilityGenerator(random, probabilityPositive, probabilityNegative);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, int numeratorPositive, int numeratorNegative, int denominator)
		{
			int numeratorNonZero = numeratorPositive + numeratorNegative;
			int n = random.RangeCO(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, int, int, int)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, int numeratorPositive, int numeratorNegative, int denominator)
		{
			return new IntWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, uint numeratorPositive, uint numeratorNegative, uint denominator)
		{
			uint numeratorNonZero = numeratorPositive + numeratorNegative;
			uint n = random.RangeCO(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, uint, uint, uint)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, uint numeratorPositive, uint numeratorNegative, uint denominator)
		{
			return new UIntWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, long numeratorPositive, long numeratorNegative, long denominator)
		{
			long numeratorNonZero = numeratorPositive + numeratorNegative;
			long n = random.RangeCO(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, long, long, long)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, long numeratorPositive, long numeratorNegative, long denominator)
		{
			return new LongWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, ulong numeratorPositive, ulong numeratorNegative, ulong denominator)
		{
			ulong numeratorNonZero = numeratorPositive + numeratorNegative;
			ulong n = random.RangeCO(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, ulong, ulong, ulong)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, ulong numeratorPositive, ulong numeratorNegative, ulong denominator)
		{
			return new ULongWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, float numeratorPositive, float numeratorNegative, float denominator)
		{
			float numeratorNonZero = numeratorPositive + numeratorNegative;
			float n = random.RangeCO(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, float, float, float)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, float numeratorPositive, float numeratorNegative, float denominator)
		{
			return new FloatWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative, denominator);
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, double numeratorPositive, double numeratorNegative, double denominator)
		{
			double numeratorNonZero = numeratorPositive + numeratorNegative;
			double n = random.RangeCO(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numeratorPositive"/> and <paramref name="numeratorNegative"/> are assessed.  Must be positive.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, 0, +1 }.</returns>
		/// <seealso cref="SignProbability(IRandom, double, double, double)"/>
		public static IRangeGenerator<int> MakeSignProbabilityGenerator(this IRandom random, double numeratorPositive, double numeratorNegative, double denominator)
		{
			return new DoubleWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative, denominator);
		}

		#endregion

		#endregion
	}
}
