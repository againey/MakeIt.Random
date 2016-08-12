/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public static class RandomChance
	{
		#region Evenly Weighted

		public static bool Chance(this IRandomEngine random)
		{
			return random.Next32() >= 0x80000000U;
		}

		#endregion

		#region Unevenly Weighted

		public static bool Chance(this IRandomEngine random, int ratioTrue, int ratioFalse)
		{
			return random.HalfOpenRange(ratioTrue + ratioFalse) < ratioTrue;
		}

		public static bool Chance(this IRandomEngine random, uint ratioTrue, uint ratioFalse)
		{
			return random.HalfOpenRange(ratioTrue + ratioFalse) < ratioTrue;
		}

		public static bool Chance(this IRandomEngine random, float ratioTrue, float ratioFalse)
		{
			return random.HalfOpenRange(ratioTrue + ratioFalse) < ratioTrue;
		}

		public static bool Chance(this IRandomEngine random, double ratioTrue, double ratioFalse)
		{
			return random.HalfOpenRange(ratioTrue + ratioFalse) < ratioTrue;
		}

		#endregion

		#region Probability

		public static bool Probability(this IRandomEngine random, int numerator, int denominator)
		{
			return random.HalfOpenRange(denominator) < numerator;
		}

		public static bool Probability(this IRandomEngine random, uint numerator, uint denominator)
		{
			return random.HalfOpenRange(denominator) < numerator;
		}

		public static bool Probability(this IRandomEngine random, float numerator, float denominator)
		{
			return random.HalfOpenRange(denominator) < numerator;
		}

		public static bool Probability(this IRandomEngine random, double numerator, double denominator)
		{
			return random.HalfOpenRange(denominator) < numerator;
		}

		public static bool Probability(this IRandomEngine random, float probability)
		{
			return random.HalfOpenFloatUnit() < probability;
		}

		public static bool Probability(this IRandomEngine random, double probability)
		{
			return random.HalfOpenDoubleUnit() < probability;
		}

		#endregion
	}
}
