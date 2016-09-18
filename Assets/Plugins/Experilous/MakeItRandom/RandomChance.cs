/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	#region Interfaces

	/// <summary>
	/// An interface for any generator of boolean values, with the pattern or distribution of values to be determined by the implementation.
	/// </summary>
	public interface IBooleanGenerator
	{
		/// <summary>
		/// Get the next value produced by the generator.
		/// </summary>
		/// <returns>The next boolean value in the sequence determined by the generator implementation.</returns>
		bool Next();
	}

	#endregion

	/// <summary>
	/// A static class of extension methods for generating random boolean values based on various probabilities.
	/// </summary>
	public static class RandomChance
	{
		#region Private Generators

		private class UniformChanceGenerator : Detail.BufferedBitGenerator, IBooleanGenerator
		{
			public UniformChanceGenerator(IRandom random) : base(random) { }

			public bool Next()
			{
				return Next32() != 0;
			}
		}

		private class IntWeightedProbabilityGenerator : IBooleanGenerator
		{
			private IIntGenerator _rangeGenerator;
			private int _numerator;

			public IntWeightedProbabilityGenerator(IRandom random, int numerator)
			{
				_rangeGenerator = random.MakeIntGenerator(true);
				_numerator = numerator;
			}

			public IntWeightedProbabilityGenerator(IRandom random, int numerator, int denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public bool Next()
			{
				return _rangeGenerator.Next() < _numerator;
			}
		}

		private class UIntWeightedProbabilityGenerator : IBooleanGenerator
		{
			private IUIntGenerator _rangeGenerator;
			private uint _numerator;

			public UIntWeightedProbabilityGenerator(IRandom random, uint numerator)
			{
				_rangeGenerator = random.MakeUIntGenerator();
				_numerator = numerator;
			}

			public UIntWeightedProbabilityGenerator(IRandom random, uint numerator, uint denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public bool Next()
			{
				return _rangeGenerator.Next() < _numerator;
			}
		}

		private class LongWeightedProbabilityGenerator : IBooleanGenerator
		{
			private ILongGenerator _rangeGenerator;
			private long _numerator;

			public LongWeightedProbabilityGenerator(IRandom random, long numerator)
			{
				_rangeGenerator = random.MakeLongGenerator(true);
				_numerator = numerator;
			}

			public LongWeightedProbabilityGenerator(IRandom random, long numerator, long denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public bool Next()
			{
				return _rangeGenerator.Next() < _numerator;
			}
		}

		private class ULongWeightedProbabilityGenerator : IBooleanGenerator
		{
			private IULongGenerator _rangeGenerator;
			private ulong _numerator;

			public ULongWeightedProbabilityGenerator(IRandom random, ulong numerator)
			{
				_rangeGenerator = random.MakeULongGenerator();
				_numerator = numerator;
			}

			public ULongWeightedProbabilityGenerator(IRandom random, ulong numerator, ulong denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public bool Next()
			{
				return _rangeGenerator.Next() < _numerator;
			}
		}

		private class FloatWeightedProbabilityGenerator : IBooleanGenerator
		{
			private IFloatGenerator _rangeGenerator;
			private float _numerator;

			public FloatWeightedProbabilityGenerator(IRandom random, float numerator)
			{
				_rangeGenerator = random.MakeFloatCOGenerator();
				_numerator = numerator;
			}

			public FloatWeightedProbabilityGenerator(IRandom random, float numerator, float denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public bool Next()
			{
				return _rangeGenerator.Next() < _numerator;
			}
		}

		private class DoubleWeightedProbabilityGenerator : IBooleanGenerator
		{
			private IDoubleGenerator _rangeGenerator;
			private double _numerator;

			public DoubleWeightedProbabilityGenerator(IRandom random, double numerator)
			{
				_rangeGenerator = random.MakeDoubleCOGenerator();
				_numerator = numerator;
			}

			public DoubleWeightedProbabilityGenerator(IRandom random, double numerator, double denominator)
			{
				_rangeGenerator = random.MakeRangeCOGenerator(denominator);
				_numerator = numerator;
			}

			public bool Next()
			{
				return _rangeGenerator.Next() < _numerator;
			}
		}

		#endregion

		#region Evenly Weighted

		/// <summary>
		/// Returns a random bool with exacty a half and half chance of being true or false.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random uniformly distributed bool.</returns>
		public static bool Chance(this IRandom random)
		{
			return random.Next32() >= 0x80000000U;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values with exacty a half and half chance of being true or false.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A boolean generator which will produce random boolean values with exacty a half and half chance of being true or false.</returns>
		/// <seealso cref="Chance(IRandom)"/>
		public static IBooleanGenerator MakeChanceGenerator(this IRandom random)
		{
			return new UniformChanceGenerator(random);
		}

		#endregion

		#region Unevenly Weighted

		/// <summary>
		/// Returns a random bool where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static bool Chance(this IRandom random, int ratioTrue, int ratioFalse)
		{
			return random.RangeCO(ratioTrue + ratioFalse) < ratioTrue;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Chance(IRandom, int, int)"/>
		public static IBooleanGenerator MakeChanceGenerator(this IRandom random, int ratioTrue, int ratioFalse)
		{
			return new IntWeightedProbabilityGenerator(random, ratioTrue, ratioTrue + ratioFalse);
		}

		/// <summary>
		/// Returns a random bool where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.</param>
		/// <returns>A random bool weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static bool Chance(this IRandom random, uint ratioTrue, uint ratioFalse)
		{
			return random.RangeCO(ratioTrue + ratioFalse) < ratioTrue;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Chance(IRandom, uint, uint)"/>
		public static IBooleanGenerator MakeChanceGenerator(this IRandom random, uint ratioTrue, uint ratioFalse)
		{
			return new UIntWeightedProbabilityGenerator(random, ratioTrue, ratioTrue + ratioFalse);
		}

		/// <summary>
		/// Returns a random bool where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static bool Chance(this IRandom random, long ratioTrue, long ratioFalse)
		{
			return random.RangeCO(ratioTrue + ratioFalse) < ratioTrue;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Chance(IRandom, long, long)"/>
		public static IBooleanGenerator MakeChanceGenerator(this IRandom random, long ratioTrue, long ratioFalse)
		{
			return new LongWeightedProbabilityGenerator(random, ratioTrue, ratioTrue + ratioFalse);
		}

		/// <summary>
		/// Returns a random bool where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.</param>
		/// <returns>A random bool weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static bool Chance(this IRandom random, ulong ratioTrue, ulong ratioFalse)
		{
			return random.RangeCO(ratioTrue + ratioFalse) < ratioTrue;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Chance(IRandom, ulong, ulong)"/>
		public static IBooleanGenerator MakeChanceGenerator(this IRandom random, ulong ratioTrue, ulong ratioFalse)
		{
			return new ULongWeightedProbabilityGenerator(random, ratioTrue, ratioTrue + ratioFalse);
		}

		/// <summary>
		/// Returns a random bool where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static bool Chance(this IRandom random, float ratioTrue, float ratioFalse)
		{
			return random.RangeCO(ratioTrue + ratioFalse) < ratioTrue;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Chance(IRandom, float, float)"/>
		public static IBooleanGenerator MakeChanceGenerator(this IRandom random, float ratioTrue, float ratioFalse)
		{
			return new FloatWeightedProbabilityGenerator(random, ratioTrue, ratioTrue + ratioFalse);
		}

		/// <summary>
		/// Returns a random bool where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static bool Chance(this IRandom random, double ratioTrue, double ratioFalse)
		{
			return random.RangeCO(ratioTrue + ratioFalse) < ratioTrue;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Chance(IRandom, double, double)"/>
		public static IBooleanGenerator MakeChanceGenerator(this IRandom random, double ratioTrue, double ratioFalse)
		{
			return new DoubleWeightedProbabilityGenerator(random, ratioTrue, ratioTrue + ratioFalse);
		}

		#endregion

		#region Probability

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of the full non-negative range of <c>int</c> (2^31) that the result will be true.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the probability set by the parameter and the non-negative range of an integer.</returns>
		public static bool Probability(this IRandom random, int numerator)
		{
			return random.IntNonNegative() < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/2^31.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of the full non-negative range of <c>int</c> (2^31) that the result will be true.  Must be non-negative.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, int)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, int numerator)
		{
			return new IntWeightedProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of the full range of <c>uint</c> (2^32) that the result will be true.</param>
		/// <returns>A random bool weighted according to the probability set by the parameter and the full range of an unsigned integer.</returns>
		public static bool Probability(this IRandom random, uint numerator)
		{
			return random.UInt() < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of the full non-negative range of <c>int</c> (2^32) that the result will be true. </param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, uint)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, uint numerator)
		{
			return new UIntWeightedProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of the full non-negative range of <c>long</c> (2^63) that the result will be true.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the probability set by the parameter and the non-negative range of a long integer.</returns>
		public static bool Probability(this IRandom random, long numerator)
		{
			return random.LongNonNegative() < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of the full non-negative range of <c>int</c> (2^63) that the result will be true.  Must be non-negative.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, long)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, long numerator)
		{
			return new LongWeightedProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of the full range of <c>ulong</c> (2^64) that the result will be true.</param>
		/// <returns>A random bool weighted according to the probability set by the parameter and the full range of an unsigned long integer.</returns>
		public static bool Probability(this IRandom random, ulong numerator)
		{
			return random.ULong() < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of the full non-negative range of <c>int</c> (2^64) that the result will be true.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, ulong)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, ulong numerator)
		{
			return new ULongWeightedProbabilityGenerator(random, numerator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, int numerator, int denominator)
		{
			return random.RangeCO(denominator) < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, int, int)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, int numerator, int denominator)
		{
			return new IntWeightedProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, uint numerator, uint denominator)
		{
			return random.RangeCO(denominator) < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, uint, uint)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, uint numerator, uint denominator)
		{
			return new UIntWeightedProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, long numerator, long denominator)
		{
			return random.RangeCO(denominator) < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, long, long)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, long numerator, long denominator)
		{
			return new LongWeightedProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, ulong numerator, ulong denominator)
		{
			return random.RangeCO(denominator) < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, ulong, ulong)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, ulong numerator, ulong denominator)
		{
			return new ULongWeightedProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, float numerator, float denominator)
		{
			return random.RangeCO(denominator) < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, float, float)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, float numerator, float denominator)
		{
			return new FloatWeightedProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, double numerator, double denominator)
		{
			return random.RangeCO(denominator) < numerator;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be true.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, double, double)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, double numerator, double denominator)
		{
			return new DoubleWeightedProbabilityGenerator(random, numerator, denominator);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a true result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, float probability)
		{
			return random.FloatCO() < probability;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a true result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, float)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, float probability)
		{
			return new FloatWeightedProbabilityGenerator(random, probability);
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a true result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, double probability)
		{
			return random.DoubleCO() < probability;
		}

		/// <summary>
		/// Returns a boolean generator which will produce random boolean values where the probability of a true result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="probability">The probability of a true result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A boolean generator which will produce weighted random boolean values.</returns>
		/// <seealso cref="Probability(IRandom, double)"/>
		public static IBooleanGenerator MakeProbabilityGenerator(this IRandom random, double probability)
		{
			return new DoubleWeightedProbabilityGenerator(random, probability);
		}

		#endregion
	}
}
