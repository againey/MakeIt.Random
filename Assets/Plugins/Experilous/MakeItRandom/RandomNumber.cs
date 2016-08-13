/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random numbers within specific commonly useful ranges.
	/// </summary>
	public static class RandomNumber
	{
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
		/// Returns a random 32-bit unsigned integer with every bit having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A 32-bit unsigned integer with every bit set to either 1 or 0 with equal probability.</returns>
		public static uint Bits32(this IRandom random)
		{
			return random.Next32();
		}

		/// <summary>
		/// Returns a random 32-bit unsigned integer with its lowest <paramref name="bitCount"/> bits having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="bitCount">The number of bits to generate with equal probability of being 1 or 0.</param>
		/// <returns>A 32-bit unsigned integer with its lowest <paramref name="bitCount"/> bits set to either 1 or 0 with equal probability and all higher bits set to 0.</returns>
		public static uint Bits32(this IRandom random, int bitCount)
		{
			return random.Next32() >> (32 - bitCount);
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
		/// Returns a random 64-bit unsigned integer with its lowest <paramref name="bitCount"/> bits having exacty a half and half chance of being one or zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="bitCount">The number of bits to generate with equal probability of being 1 or 0.</param>
		/// <returns>A 64-bit unsigned integer with its lowest <paramref name="bitCount"/> bits set to either 1 or 0 with equal probability and all higher bits set to 0.</returns>
		public static ulong Bits64(this IRandom random, int bitCount)
		{
			return random.Next64() >> (64 - bitCount);
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
		/// Returns a random integer with exacty a half and half chance of being positive one or negative one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random uniformly distributed integer from the set { -1, +1 }.</returns>
		public static int Sign(this IRandom random)
		{
			return (int)((random.Next32() >> 30) & 2U) - 1;
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
			while (n >= 3U);
			return (int)n - 1;
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
		/// Returns a random integer from the set { 0, +1 } where on average the ratio of positive one results to zero results will be <paramref name="ratioOne"/>:<paramref name="ratioZero"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioOne">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int OneOrZero(this IRandom random, uint ratioOne, uint ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
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
		/// Returns a random integer from the set { -1, +1 } where on average the ratio of positive one results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int Sign(this IRandom random, int ratioPositive, int ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? 1 : 0;
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
			return random.Chance(ratioPositive, ratioNegative) ? 1 : 0;
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
			return random.Chance(ratioPositive, ratioNegative) ? 1 : 0;
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
			return random.Chance(ratioPositive, ratioNegative) ? 1 : 0;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, int ratioPositive, int ratioZero, int ratioNegative)
		{
			int ratioNonNegative = ratioPositive + ratioZero;
			int n = random.HalfOpenRange(ratioNonNegative + ratioNegative);
			return n < ratioNonNegative ? (n < ratioPositive ? +1 : 0) : -1;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, uint ratioPositive, uint ratioZero, uint ratioNegative)
		{
			uint ratioNonNegative = ratioPositive + ratioZero;
			uint n = random.HalfOpenRange(ratioNonNegative + ratioNegative);
			return n < ratioNonNegative ? (n < ratioPositive ? +1 : 0) : -1;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, float ratioPositive, float ratioZero, float ratioNegative)
		{
			float ratioNonNegative = ratioPositive + ratioZero;
			float n = random.HalfOpenRange(ratioNonNegative + ratioNegative);
			return n < ratioNonNegative ? (n < ratioPositive ? +1 : 0) : -1;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where on average the ratio of positive one results to zero results to negative one results will be <paramref name="ratioPositive"/>:<paramref name="ratioZero"/>:<paramref name="ratioNegative"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioPositive">The weight determining the probability of a result being +1.  Must be non-negative.</param>
		/// <param name="ratioZero">The weight determining the probability of a result being 0.  Must be non-negative.</param>
		/// <param name="ratioNegative">The weight determining the probability of a result being -1.  Must be non-negative.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static int SignOrZero(this IRandom random, double ratioPositive, double ratioZero, double ratioNegative)
		{
			double ratioNonNegative = ratioPositive + ratioZero;
			double n = random.HalfOpenRange(ratioNonNegative + ratioNegative);
			return n < ratioNonNegative ? (n < ratioPositive ? +1 : 0) : -1;
		}

		#endregion

		#region {-1, 0, +1}, Probability

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is <paramref name="numerator"/>/<paramref name="denominator"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, int numerator, int denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? 1 : 0;
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
			return random.HalfOpenRange(denominator) < numerator ? 1 : 0;
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
			return random.HalfOpenRange(denominator) < numerator ? 1 : 0;
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
			return random.HalfOpenRange(denominator) < numerator ? 1 : 0;
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, float probability)
		{
			return random.HalfOpenFloatUnit() < probability ? 1 : 0;
		}

		/// <summary>
		/// Returns a random integer from the set { 0, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { 0, +1 } weighted according to the probability set by the parameters.</returns>
		public static int OneProbability(this IRandom random, double probability)
		{
			return random.HalfOpenDoubleUnit() < probability ? 1 : 0;
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
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
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
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
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
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
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
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, float probability)
		{
			return random.HalfOpenFloatUnit() < probability ? +1 : -1;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a positive one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a positive one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int PositiveProbability(this IRandom random, double probability)
		{
			return random.HalfOpenDoubleUnit() < probability ? +1 : -1;
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
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
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
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
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
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
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
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, float probability)
		{
			return random.HalfOpenFloatUnit() < probability ? +1 : -1;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, +1 } where the probability of a negative one result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a negative one result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random integer from the set { -1, +1 } weighted according to the probability set by the parameters.</returns>
		public static int NegativeProbability(this IRandom random, double probability)
		{
			return random.HalfOpenDoubleUnit() < probability ? +1 : -1;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, int numeratorPositive, int numeratorNegative, int denominator)
		{
			int numeratorNonZero = numeratorPositive + numeratorNegative;
			int n = random.HalfOpenRange(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, uint numeratorPositive, uint numeratorNegative, uint denominator)
		{
			uint numeratorNonZero = numeratorPositive + numeratorNegative;
			uint n = random.HalfOpenRange(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, float numeratorPositive, float numeratorNegative, float denominator)
		{
			float numeratorNonZero = numeratorPositive + numeratorNegative;
			float n = random.HalfOpenRange(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		/// <summary>
		/// Returns a random integer from the set { -1, 0, +1 } where the probability of a positive one result is <paramref name="numeratorPositive"/>/<paramref name="denominator"/> and the probability of a negative one result is <paramref name="numeratorNegative"/>/<paramref name="denominator"/>.  The probability of a zero result is whatever probability is left over.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numeratorPositive">The average number of times out of <paramref name="denominator"/> that the result will be +1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="numeratorNegative">The average number of times out of <paramref name="denominator"/> that the result will be -1.  Must be in the range [0, <paramref name="denominator"/>].</param>
		/// <param name="denominator">The scale by which <paramref name="numerator"/> is assessed.  Must be positive.</param>
		/// <returns>A random integer from the set { -1, 0, +1 } weighted according to the probability set by the parameters.</returns>
		/// <remarks>The sum of the the two numerator parameters must be less than or equal to the denominator.</remarks>
		public static int SignProbability(this IRandom random, double numeratorPositive, double numeratorNegative, double denominator)
		{
			double numeratorNonZero = numeratorPositive + numeratorNegative;
			double n = random.HalfOpenRange(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
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
			float n = random.HalfOpenFloatUnit();
			return n < probabilityNonZero ? (n < probabilityPositive ? +1 : -1) : 0;
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
			double n = random.HalfOpenDoubleUnit();
			return n < probabilityNonZero ? (n < probabilityPositive ? +1 : -1) : 0;
		}

		#endregion

		#region Angle

		private const float _floatDegreesPerTurn = 360.0f;
		private const double _doubleDegreesPerTurn = 360.0;

		private const float _floatDegreesPerHalfTurn = 180.0f;
		private const double _doubleDegreesPerHalfTurn = 180.0;

		private const float _floatRadiansPerTurn = 6.283185307179586476925286766559f;
		private const double _doubleRadiansPerTurn = 6.283185307179586476925286766559;

		private const float _floatRadiansPerHalfTurn = 3.1415926535897932384626433832795f;
		private const double _doubleRadiansPerHalfTurn = 3.1415926535897932384626433832795;

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, from 0 degrees up to but not including 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 360).</returns>
		public static float FloatDegrees(this IRandom random)
		{
			return random.HalfOpenFloatUnit() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, from -180 degrees up to but not including +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-180, +180).</returns>
		public static float SignedFloatDegrees(this IRandom random)
		{
			return random.HalfOpenFloatUnit() * _floatDegreesPerTurn - _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, from 0 radians up to but not including 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, 2π).</returns>
		public static float FloatRadians(this IRandom random)
		{
			return random.HalfOpenFloatUnit() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, from -π radians up to but not including +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π, +π).</returns>
		public static float SignedFloatRadians(this IRandom random)
		{
			return random.HalfOpenFloatUnit() * _floatRadiansPerTurn - _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, from 0 degrees up to but not including 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 360).</returns>
		public static double DoubleDegrees(this IRandom random)
		{
			return random.HalfOpenDoubleUnit() * _doubleDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, from -180 degrees up to but not including +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-180, +180).</returns>
		public static double SignedDoubleDegrees(this IRandom random)
		{
			return random.HalfOpenDoubleUnit() * _doubleDegreesPerTurn - _doubleDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, from 0 radians up to but not including 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, 2π).</returns>
		public static double DoubleRadians(this IRandom random)
		{
			return random.HalfOpenDoubleUnit() * _doubleRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, from -π radians up to but not including +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π, +π).</returns>
		public static double SignedDoubleRadians(this IRandom random)
		{
			return random.HalfOpenDoubleUnit() * _doubleRadiansPerTurn - _doubleRadiansPerHalfTurn;
		}

		#endregion
	}
}
