/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random numbers within specific commonly useful ranges.
	/// </summary>
	public static class RandomNumber
	{
		#region Private Generators

		#region Bit Generators

		private class SingleBitGenerator : Detail.BufferedBitGenerator, IUIntGenerator
		{
			public SingleBitGenerator(IRandom random) : base(random) { }

			public uint Next()
			{
				return (uint)Next32();
			}
		}

		private class MultiBitGenerator32 : Detail.BufferedPowPow2RangeGeneratorBase, IUIntGenerator
		{
			public MultiBitGenerator32(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }

			public uint Next()
			{
				return (uint)Next32();
			}
		}

		private class MultiBitGenerator64 : Detail.BufferedPowPow2RangeGeneratorBase, IULongGenerator
		{
			public MultiBitGenerator64(IRandom random, int bitCount, ulong bitMask) : base(random, bitCount, bitMask) { }

			public ulong Next()
			{
				return Next64();
			}
		}

		#endregion

		#region Uniform { -1, 0, +1 } Generators

		private class OneOrZeroGenerator : Detail.BufferedBitGenerator, IIntGenerator
		{
			public OneOrZeroGenerator(IRandom random) : base(random) { }

			public int Next()
			{
				return (int)Next32();
			}
		}

		private class SignGenerator : Detail.BufferedBitGenerator, IIntGenerator
		{
			public SignGenerator(IRandom random) : base(random) { }

			public int Next()
			{
				return (int)(Next32() << 1) - 1;
			}
		}

		private class SignOrZeroGenerator : Detail.BufferedAnyRangeGeneratorBase, IIntGenerator
		{
			public SignOrZeroGenerator(IRandom random) : base(random, 2UL, 3UL) { }

			public int Next()
			{
				return (int)Next32() - 1;
			}
		}

		#endregion

		#region Weighted { 0, +1 } Generators

		private class IntWeightedOneProbabilityGenerator : IIntGenerator
		{
			private IIntGenerator _rangeGenerator;
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

		private class UIntWeightedOneProbabilityGenerator : IIntGenerator
		{
			private IUIntGenerator _rangeGenerator;
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

		private class LongWeightedOneProbabilityGenerator : IIntGenerator
		{
			private ILongGenerator _rangeGenerator;
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

		private class ULongWeightedOneProbabilityGenerator : IIntGenerator
		{
			private IULongGenerator _rangeGenerator;
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

		private class FloatWeightedOneProbabilityGenerator : IIntGenerator
		{
			private IFloatGenerator _rangeGenerator;
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

		private class DoubleWeightedOneProbabilityGenerator : IIntGenerator
		{
			private IDoubleGenerator _rangeGenerator;
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

		#endregion

		#region Weighted { -1, +1 } Generators

		private class IntWeightedPositiveProbabilityGenerator : IIntGenerator
		{
			private IIntGenerator _rangeGenerator;
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

		private class UIntWeightedPositiveProbabilityGenerator : IIntGenerator
		{
			private IUIntGenerator _rangeGenerator;
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

		private class LongWeightedPositiveProbabilityGenerator : IIntGenerator
		{
			private ILongGenerator _rangeGenerator;
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

		private class ULongWeightedPositiveProbabilityGenerator : IIntGenerator
		{
			private IULongGenerator _rangeGenerator;
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

		private class FloatWeightedPositiveProbabilityGenerator : IIntGenerator
		{
			private IFloatGenerator _rangeGenerator;
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

		private class DoubleWeightedPositiveProbabilityGenerator : IIntGenerator
		{
			private IDoubleGenerator _rangeGenerator;
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

		private class IntWeightedNegativeProbabilityGenerator : IIntGenerator
		{
			private IIntGenerator _rangeGenerator;
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

		private class UIntWeightedNegativeProbabilityGenerator : IIntGenerator
		{
			private IUIntGenerator _rangeGenerator;
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

		private class LongWeightedNegativeProbabilityGenerator : IIntGenerator
		{
			private ILongGenerator _rangeGenerator;
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

		private class ULongWeightedNegativeProbabilityGenerator : IIntGenerator
		{
			private IULongGenerator _rangeGenerator;
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

		private class FloatWeightedNegativeProbabilityGenerator : IIntGenerator
		{
			private IFloatGenerator _rangeGenerator;
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

		private class DoubleWeightedNegativeProbabilityGenerator : IIntGenerator
		{
			private IDoubleGenerator _rangeGenerator;
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

		private class IntWeightedSignProbabilityGenerator : IIntGenerator
		{
			private IIntGenerator _rangeGenerator;
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

		private class UIntWeightedSignProbabilityGenerator : IIntGenerator
		{
			private IUIntGenerator _rangeGenerator;
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

		private class LongWeightedSignProbabilityGenerator : IIntGenerator
		{
			private ILongGenerator _rangeGenerator;
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

		private class ULongWeightedSignProbabilityGenerator : IIntGenerator
		{
			private IULongGenerator _rangeGenerator;
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

		private class FloatWeightedSignProbabilityGenerator : IIntGenerator
		{
			private IFloatGenerator _rangeGenerator;
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

		private class DoubleWeightedSignProbabilityGenerator : IIntGenerator
		{
			private IDoubleGenerator _rangeGenerator;
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

		#region Angle Generators

		private class AngleGenerator : IFloatGenerator
		{
			private IFloatGenerator _rangeGenerator;
			private float _scale;

			private AngleGenerator(IFloatGenerator rangeGenerator, float scale)
			{
				_rangeGenerator = rangeGenerator;
				_scale = scale;
			}

			public static AngleGenerator CreateOO(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeFloatOOGenerator(), scale);
			}

			public static AngleGenerator CreateSignedOO(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeSignedFloatOOGenerator(), scale);
			}

			public static AngleGenerator CreateCO(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeFloatCOGenerator(), scale);
			}

			public static AngleGenerator CreateSignedCO(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeSignedFloatCOGenerator(), scale);
			}

			public static AngleGenerator CreateOC(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeFloatOCGenerator(), scale);
			}

			public static AngleGenerator CreateSignedOC(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeSignedFloatOCGenerator(), scale);
			}

			public static AngleGenerator CreateCC(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeFloatCCGenerator(), scale);
			}

			public static AngleGenerator CreateSignedCC(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeSignedFloatCCGenerator(), scale);
			}

			public float Next()
			{
				return _rangeGenerator.Next() * _scale;
			}
		}
		#endregion

		#endregion

		#region Bits

		/// <summary>
		/// Returns a random unsigned integer with its lowest bit having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>An unsigned integer with its lowest bit set to either 1 or 0 with equal probability and all high bits set to 0.</returns>
		public static uint Bit(this IRandom random)
		{
			return random.Next32() >> 31;
		}

		/// <summary>
		/// Returns a bit generator which will produce a single bit per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A bit generator which will produce a single bit per call to generator.Next().</returns>
		/// <seealso cref="Bit(IRandom)"/>
		public static IUIntGenerator MakeBitGenerator(this IRandom random)
		{
			return new SingleBitGenerator(random);
		}

		/// <summary>
		/// Returns a random 32-bit unsigned integer with every bit having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A 32-bit unsigned integer with every bit set to either 1 or 0 with equal probability.</returns>
		public static uint Bits32(this IRandom random)
		{
			return random.Next32();
		}

		/// <summary>
		/// Returns a bit generator which will produce 32 bits per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A bit generator which will produce 32 random bits per call to generator.Next().</returns>
		/// <seealso cref="Bits32(IRandom)"/>
		public static IUIntGenerator MakeBits32Generator(this IRandom random)
		{
			return new MultiBitGenerator32(random, 32, 0xFFFFFFFFUL);
		}

		/// <summary>
		/// Returns a random 32-bit unsigned integer with its lowest <paramref name="bitCount"/> bits having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="bitCount">The number of bits to generate.  Must be in the range [1, 32].</param>
		/// <returns>A 32-bit unsigned integer with its lowest <paramref name="bitCount"/> bits set to either 1 or 0 with equal probability and all higher bits set to 0.</returns>
		public static uint Bits32(this IRandom random, int bitCount)
		{
			return random.Next32() >> (32 - bitCount);
		}

		/// <summary>
		/// Returns a bit generator which will produce <paramref name="bitCount"/> bits per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="bitCount">The number of bits to generate.  Must be in the range [1, 32].</param>
		/// <returns>A bit generator which will produce <paramref name="bitCount"/> random bits per call to generator.Next().</returns>
		/// <seealso cref="Bits32(IRandom, int)"/>
		public static IUIntGenerator MakeBits32Generator(this IRandom random, int bitCount)
		{
			return new MultiBitGenerator32(random, bitCount, 0xFFFFFFFFUL >> (32 - bitCount));
		}

		/// <summary>
		/// Returns a random 64-bit unsigned integer with every bit having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A 64-bit unsigned integer with every bit set to either 1 or 0 with equal probability.</returns>
		public static ulong Bits64(this IRandom random)
		{
			return random.Next64();
		}

		/// <summary>
		/// Returns a bit generator which will produce 64 bits per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A bit generator which will produce 64 random bits per call to generator.Next().</returns>
		/// <seealso cref="Bits64(IRandom)"/>
		public static IULongGenerator MakeBits64Generator(this IRandom random)
		{
			return new MultiBitGenerator64(random, 64, 0xFFFFFFFFFFFFFFFFUL);
		}

		/// <summary>
		/// Returns a random 64-bit unsigned integer with its lowest <paramref name="bitCount"/> bits having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="bitCount">The number of bits to generate.  Must be in the range [1, 64].</param>
		/// <returns>A 64-bit unsigned integer with its lowest <paramref name="bitCount"/> bits set to either 1 or 0 with equal probability and all higher bits set to 0.</returns>
		public static ulong Bits64(this IRandom random, int bitCount)
		{
			return random.Next64() >> (64 - bitCount);
		}

		/// <summary>
		/// Returns a bit generator which will produce <paramref name="bitCount"/> bits per call to generator.Next().
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="bitCount">The number of bits to generate.  Must be in the range [1, 64].</param>
		/// <returns>A bit generator which will produce <paramref name="bitCount"/> random bits per call to generator.Next().</returns>
		/// <seealso cref="Bits64(IRandom, int)"/>
		public static IULongGenerator MakeBits64Generator(this IRandom random, int bitCount)
		{
			return new MultiBitGenerator64(random, bitCount, 0xFFFFFFFFFFFFFFFFUL >> (64 - bitCount));
		}

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
		public static IIntGenerator MakeOneOrZeroGenerator(this IRandom random)
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
		public static IIntGenerator MakeSignGenerator(this IRandom random)
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
		public static IIntGenerator MakeSignOrZeroGenerator(this IRandom random)
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
		public static IIntGenerator MakeOneOrZeroGenerator(this IRandom random, int ratioOne, int ratioZero)
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
		public static IIntGenerator MakeOneOrZeroGenerator(this IRandom random, uint ratioOne, uint ratioZero)
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
		public static IIntGenerator MakeOneOrZeroGenerator(this IRandom random, long ratioOne, long ratioZero)
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
		public static IIntGenerator MakeOneOrZeroGenerator(this IRandom random, ulong ratioOne, ulong ratioZero)
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
		public static IIntGenerator MakeOneOrZeroGenerator(this IRandom random, float ratioOne, float ratioZero)
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
		public static IIntGenerator MakeOneOrZeroGenerator(this IRandom random, double ratioOne, double ratioZero)
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
		public static IIntGenerator MakeSignGenerator(this IRandom random, int ratioPositive, int ratioNegative)
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
		public static IIntGenerator MakeSignGenerator(this IRandom random, uint ratioPositive, uint ratioNegative)
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
		public static IIntGenerator MakeSignGenerator(this IRandom random, long ratioPositive, long ratioNegative)
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
		public static IIntGenerator MakeSignGenerator(this IRandom random, ulong ratioPositive, ulong ratioNegative)
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
		public static IIntGenerator MakeSignGenerator(this IRandom random, float ratioPositive, float ratioNegative)
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
		public static IIntGenerator MakeSignGenerator(this IRandom random, double ratioPositive, double ratioNegative)
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
		public static IIntGenerator MakeSignOrZeroGenerator(this IRandom random, int ratioPositive, int ratioNegative, int ratioZero)
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
		public static IIntGenerator MakeSignOrZeroGenerator(this IRandom random, uint ratioPositive, uint ratioNegative, uint ratioZero)
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
		public static IIntGenerator MakeSignOrZeroGenerator(this IRandom random, long ratioPositive, long ratioNegative, long ratioZero)
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
		public static IIntGenerator MakeSignOrZeroGenerator(this IRandom random, ulong ratioPositive, ulong ratioNegative, ulong ratioZero)
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
		public static IIntGenerator MakeSignOrZeroGenerator(this IRandom random, float ratioPositive, float ratioNegative, float ratioZero)
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
		public static IIntGenerator MakeSignOrZeroGenerator(this IRandom random, double ratioPositive, double ratioNegative, double ratioZero)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, int numerator)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, uint numerator)
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
			return random.Long() < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be +1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { 0, +1 }.</returns>
		/// <seealso cref="OneProbability(IRandom, long)"/>
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, long numerator)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, ulong numerator)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, float probability)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, double probability)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, int numerator, int denominator)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, uint numerator, uint denominator)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, long numerator, long denominator)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, ulong numerator, ulong denominator)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, float numerator, float denominator)
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
		public static IIntGenerator MakeOneProbabilityGenerator(this IRandom random, double numerator, double denominator)
		{
			return new DoubleWeightedOneProbabilityGenerator(random, numerator, denominator);
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, int numerator)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, uint numerator)
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
			return random.IntNonNegative() < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns an integer generator which will produce random numbers from the set { -1, +1 } where the probability of a positive one result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of 2^63 that the result will be +1.  Must be non-negative.</param>
		/// <returns>An integer generator which will produce weighted random numbers from the set { -1, +1 }.</returns>
		/// <seealso cref="PositiveProbability(IRandom, long)"/>
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, long numerator)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, ulong numerator)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, float probability)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, double probability)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, int numerator, int denominator)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, uint numerator, uint denominator)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, long numerator, long denominator)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, ulong numerator, ulong denominator)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, float numerator, float denominator)
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
		public static IIntGenerator MakePositiveProbabilityGenerator(this IRandom random, double numerator, double denominator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, int numerator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, uint numerator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, long numerator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, ulong numerator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, float probability)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, double probability)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, int numerator, int denominator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, uint numerator, uint denominator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, long numerator, long denominator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, ulong numerator, ulong denominator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, float numerator, float denominator)
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
		public static IIntGenerator MakeNegativeProbabilityGenerator(this IRandom random, double numerator, double denominator)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, int numeratorPositive, int numeratorNegative)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, uint numeratorPositive, uint numeratorNegative)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, long numeratorPositive, long numeratorNegative)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, ulong numeratorPositive, ulong numeratorNegative)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, float probabilityPositive, float probabilityNegative)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, double probabilityPositive, double probabilityNegative)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, int numeratorPositive, int numeratorNegative, int denominator)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, uint numeratorPositive, uint numeratorNegative, uint denominator)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, long numeratorPositive, long numeratorNegative, long denominator)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, ulong numeratorPositive, ulong numeratorNegative, ulong denominator)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, float numeratorPositive, float numeratorNegative, float denominator)
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
		public static IIntGenerator MakeSignProbabilityGenerator(this IRandom random, double numeratorPositive, double numeratorNegative, double denominator)
		{
			return new DoubleWeightedSignProbabilityGenerator(random, numeratorPositive, numeratorNegative, denominator);
		}

		#endregion

		#endregion

		#region Angle

		private const float _floatDegreesPerTurn = 360f;
		private const float _floatDegreesPerHalfTurn = 180f;
		private const float _floatDegreesPerQuarterTurn = 90f;

		private const float _floatRadiansPerTurn = 6.283185307179586476925286766559f;
		private const float _floatRadiansPerHalfTurn = 3.1415926535897932384626433832795f;
		private const float _floatRadiansPerQuarterTurn = 1.5707963267948966192313216916398f;

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, strictly greater than 0 degrees and strictly less than 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (0, 360).</returns>
		public static float AngleDegOO(this IRandom random)
		{
			return random.FloatOO() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, strictly greater than -180 degrees and strictly less than +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (-180, +180).</returns>
		public static float SignedAngleDegOO(this IRandom random)
		{
			return random.SignedFloatOO() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, strictly greater than 0 degrees and strictly less than 180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (0, 180).</returns>
		public static float HalfAngleDegOO(this IRandom random)
		{
			return random.FloatOO() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, half a turn in either direction, strictly greater than -90 degrees and strictly less than +90 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (-90, +90).</returns>
		public static float SignedHalfAngleDegOO(this IRandom random)
		{
			return random.SignedFloatOO() * _floatDegreesPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random degree values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <returns>An angle generator producing random degree values within the specified range.</returns>
		/// <seealso cref="AngleDegOO(IRandom)"/>
		/// <seealso cref="SignedAngleDegOO(IRandom)"/>
		/// <seealso cref="HalfAngleDegOO(IRandom)"/>
		/// <seealso cref="SignedHalfAngleDegOO(IRandom)"/>
		public static IFloatGenerator MakeAngleDegOOGenerator(this IRandom random, bool half = false, bool signed = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedOO(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
			else
			{
				return AngleGenerator.CreateOO(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, strictly greater than 0 radians and strictly less than 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (0, 2π).</returns>
		public static float AngleRadOO(this IRandom random)
		{
			return random.FloatOO() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, strictly greater than -π radians and strictly less than +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (-π, +π).</returns>
		public static float SignedAngleRadOO(this IRandom random)
		{
			return random.SignedFloatOO() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, strictly greater than 0 radians and strictly less than π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (0, π).</returns>
		public static float HalfAngleRadOO(this IRandom random)
		{
			return random.FloatOO() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, half a turn in either direction, strictly greater than -π/2 radians and strictly less than +π/2 radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (-π/2, +π/2).</returns>
		public static float SignedHalfAngleRadOO(this IRandom random)
		{
			return random.SignedFloatOO() * _floatRadiansPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random radian values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <returns>An angle generator producing random radian values within the specified range.</returns>
		/// <seealso cref="AngleRadOO(IRandom)"/>
		/// <seealso cref="SignedAngleRadOO(IRandom)"/>
		/// <seealso cref="HalfAngleRadOO(IRandom)"/>
		/// <seealso cref="SignedHalfAngleRadOO(IRandom)"/>
		public static IFloatGenerator MakeAngleRadOOGenerator(this IRandom random, bool half = false, bool signed = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedOO(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
			else
			{
				return AngleGenerator.CreateOO(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, greater than or equal to 0 degrees and strictly less than 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 360).</returns>
		public static float AngleDegCO(this IRandom random)
		{
			return random.FloatCO() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, greater than or equal to -180 degrees and strictly less than +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-180, +180).</returns>
		public static float SignedAngleDegCO(this IRandom random)
		{
			return random.SignedFloatCO() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, greater than or equal to 0 degrees and strictly less than 180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 180).</returns>
		public static float HalfAngleDegCO(this IRandom random)
		{
			return random.FloatCO() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, half a turn in either direction, greater than or equal to -90 degrees and strictly less than +90 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-90, +90).</returns>
		public static float SignedHalfAngleDegCO(this IRandom random)
		{
			return random.SignedFloatCO() * _floatDegreesPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random degree values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <returns>An angle generator producing random degree values within the specified range.</returns>
		/// <seealso cref="AngleDegCO(IRandom)"/>
		/// <seealso cref="SignedAngleDegCO(IRandom)"/>
		/// <seealso cref="HalfAngleDegCO(IRandom)"/>
		/// <seealso cref="SignedHalfAngleDegCO(IRandom)"/>
		public static IFloatGenerator MakeAngleDegCOGenerator(this IRandom random, bool half = false, bool signed = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedCO(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
			else
			{
				return AngleGenerator.CreateCO(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, greater than or equal to 0 radians and strictly less than 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, 2π).</returns>
		public static float AngleRadCO(this IRandom random)
		{
			return random.FloatCO() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, greater than or equal to -π radians and strictly less than +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π, +π).</returns>
		public static float SignedAngleRadCO(this IRandom random)
		{
			return random.SignedFloatCO() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, greater than or equal to 0 radians and strictly less than π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, π).</returns>
		public static float HalfAngleRadCO(this IRandom random)
		{
			return random.FloatCO() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, half a turn in either direction, greater than or equal to -π/2 radians and strictly less than +π/2 radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π/2, +π/2).</returns>
		public static float SignedHalfAngleRadCO(this IRandom random)
		{
			return random.SignedFloatCO() * _floatRadiansPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random radian values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <returns>An angle generator producing random radian values within the specified range.</returns>
		/// <seealso cref="AngleRadCO(IRandom)"/>
		/// <seealso cref="SignedAngleRadCO(IRandom)"/>
		/// <seealso cref="HalfAngleRadCO(IRandom)"/>
		/// <seealso cref="SignedHalfAngleRadCO(IRandom)"/>
		public static IFloatGenerator MakeAngleRadCOGenerator(this IRandom random, bool half = false, bool signed = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedCO(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
			else
			{
				return AngleGenerator.CreateCO(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, strictly greater than 0 degrees and less than or equal to 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (0, 360].</returns>
		public static float AngleDegOC(this IRandom random)
		{
			return random.FloatOC() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, strictly greater than -180 degrees and less than or equal to +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (-180, +180].</returns>
		public static float SignedAngleDegOC(this IRandom random)
		{
			return random.SignedFloatOC() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, strictly greater than 0 degrees and less than or equal to 180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (0, 180].</returns>
		public static float HalfAngleDegOC(this IRandom random)
		{
			return random.FloatOC() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, half a turn in either direction, strictly greater than -90 degrees and less than or equal to +90 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (-90, +90].</returns>
		public static float SignedHalfAngleDegOC(this IRandom random)
		{
			return random.SignedFloatOC() * _floatDegreesPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random degree values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <returns>An angle generator producing random degree values within the specified range.</returns>
		/// <seealso cref="AngleDegOC(IRandom)"/>
		/// <seealso cref="SignedAngleDegOC(IRandom)"/>
		/// <seealso cref="HalfAngleDegOC(IRandom)"/>
		/// <seealso cref="SignedHalfAngleDegOC(IRandom)"/>
		public static IFloatGenerator MakeAngleDegOCGenerator(this IRandom random, bool half = false, bool signed = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedOC(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
			else
			{
				return AngleGenerator.CreateOC(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, strictly greater than 0 radians and less than or equal to 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (0, 2π].</returns>
		public static float AngleRadOC(this IRandom random)
		{
			return random.FloatOC() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, strictly greater than -π radians and less than or equal to +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (-π, +π].</returns>
		public static float SignedAngleRadOC(this IRandom random)
		{
			return random.SignedFloatOC() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, strictly greater than 0 radians and less than or equal to π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (0, π].</returns>
		public static float HalfAngleRadOC(this IRandom random)
		{
			return random.FloatOC() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, half a turn in either direction, strictly greater than -π/2 radians and less than or equal to +π/2 radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (-π/2, +π/2].</returns>
		public static float SignedHalfAngleRadOC(this IRandom random)
		{
			return random.SignedFloatOC() * _floatRadiansPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random radian values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <returns>An angle generator producing random radian values within the specified range.</returns>
		/// <seealso cref="AngleRadOC(IRandom)"/>
		/// <seealso cref="SignedAngleRadOC(IRandom)"/>
		/// <seealso cref="HalfAngleRadOC(IRandom)"/>
		/// <seealso cref="SignedHalfAngleRadOC(IRandom)"/>
		public static IFloatGenerator MakeAngleRadOCGenerator(this IRandom random, bool half = false, bool signed = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedOC(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
			else
			{
				return AngleGenerator.CreateOC(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, greater than or equal to 0 degrees and less than or equal to 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 360].</returns>
		public static float AngleDegCC(this IRandom random)
		{
			return random.FloatCC() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, greater than or equal to -180 degrees and less than or equal to +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-180, +180].</returns>
		public static float SignedAngleDegCC(this IRandom random)
		{
			return random.SignedFloatCC() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, greater than or equal to 0 degrees and less than or equal to 180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 180].</returns>
		public static float HalfAngleDegCC(this IRandom random)
		{
			return random.FloatCC() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, half a turn in either direction, greater than or equal to -90 degrees and less than or equal to +90 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-90, +90].</returns>
		public static float SignedHalfAngleDegCC(this IRandom random)
		{
			return random.SignedFloatCC() * _floatDegreesPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random degree values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <returns>An angle generator producing random degree values within the specified range.</returns>
		/// <seealso cref="AngleDegCC(IRandom)"/>
		/// <seealso cref="SignedAngleDegCC(IRandom)"/>
		/// <seealso cref="HalfAngleDegCC(IRandom)"/>
		/// <seealso cref="SignedHalfAngleDegCC(IRandom)"/>
		public static IFloatGenerator MakeAngleDegCCGenerator(this IRandom random, bool half = false, bool signed = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedCC(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
			else
			{
				return AngleGenerator.CreateCC(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, greater than or equal to 0 radians and less than or equal to 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, 2π].</returns>
		public static float AngleRadCC(this IRandom random)
		{
			return random.FloatCC() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, greater than or equal to -π radians and less than or equal to +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π, +π].</returns>
		public static float SignedAngleRadCC(this IRandom random)
		{
			return random.SignedFloatCC() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, greater than or equal to 0 radians and less than or equal to π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, π].</returns>
		public static float HalfAngleRadCC(this IRandom random)
		{
			return random.FloatCC() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, half a turn in either direction, greater than or equal to -π/2 radians and less than or equal to +π/2 radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π/2, +π/2].</returns>
		public static float SignedHalfAngleRadCC(this IRandom random)
		{
			return random.SignedFloatCC() * _floatRadiansPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random radian values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <returns>An angle generator producing random radian values within the specified range.</returns>
		/// <seealso cref="AngleRadCC(IRandom)"/>
		/// <seealso cref="SignedAngleRadCC(IRandom)"/>
		/// <seealso cref="HalfAngleRadCC(IRandom)"/>
		/// <seealso cref="SignedHalfAngleRadCC(IRandom)"/>
		public static IFloatGenerator MakeAngleRadCCGenerator(this IRandom random, bool half = false, bool signed = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedCC(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
			else
			{
				return AngleGenerator.CreateCC(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
		}

		#endregion
	}
}
