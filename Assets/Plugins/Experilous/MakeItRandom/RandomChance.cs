/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random boolean values based on various probabilities.
	/// </summary>
	public static class RandomChance
	{
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
		/// Returns a random bool where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static bool Chance(this IRandom random, uint ratioTrue, uint ratioFalse)
		{
			return random.RangeCO(ratioTrue + ratioFalse) < ratioTrue;
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
		/// Returns a random bool where on average the ratio of true results to false results will be <paramref name="ratioTrue"/>:<paramref name="ratioFalse"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="ratioTrue">The weight determining the probability of a result being true.  Must be non-negative.</param>
		/// <param name="ratioFalse">The weight determining the probability of a result being false.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the ratio of the parameters.</returns>
		/// <remarks>The sum of the ratio parameters must be positive.</remarks>
		public static bool Chance(this IRandom random, ulong ratioTrue, ulong ratioFalse)
		{
			return random.RangeCO(ratioTrue + ratioFalse) < ratioTrue;
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
			return (int)(random.Next32() & 0x7FFFFFFFU) < numerator;
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/2^32.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of the full range of <c>uint</c> (2^32) that the result will be true.</param>
		/// <returns>A random bool weighted according to the probability set by the parameter and the full range of an unsigned integer.</returns>
		public static bool Probability(this IRandom random, uint numerator)
		{
			return random.Next32() < numerator;
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/2^63.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of the full non-negative range of <c>long</c> (2^63) that the result will be true.  Must be non-negative.</param>
		/// <returns>A random bool weighted according to the probability set by the parameter and the non-negative range of a long integer.</returns>
		public static bool Probability(this IRandom random, long numerator)
		{
			return (int)(random.Next64() & 0x7FFFFFFFFFFFFFFFU) < numerator;
		}

		/// <summary>
		/// Returns a random bool where the probability of a true result is <paramref name="numerator"/>/2^64.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="numerator">The average number of times out of the full range of <c>ulong</c> (2^64) that the result will be true.</param>
		/// <returns>A random bool weighted according to the probability set by the parameter and the full range of an unsigned long integer.</returns>
		public static bool Probability(this IRandom random, ulong numerator)
		{
			return random.Next64() < numerator;
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
		/// Returns a random bool where the probability of a true result is set by the parameter <paramref name="probability"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="probability">The probability of a true result being generated.  Must be in the range [0, 1].</param>
		/// <returns>A random bool weighted according to the probability set by the parameters.</returns>
		public static bool Probability(this IRandom random, double probability)
		{
			return random.DoubleCO() < probability;
		}

		#endregion
	}
}
