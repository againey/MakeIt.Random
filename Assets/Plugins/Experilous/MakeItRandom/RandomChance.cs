/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	public static class RandomChance
	{
		#region Evenly Weighted

		public static bool Chance(this IRandom random)
		{
			return random.Next32() >= 0x80000000U;
		}

		#endregion

		#region Unevenly Weighted

		public static bool Chance(this IRandom random, int ratioTrue, int ratioFalse)
		{
			return random.HalfOpenRange(ratioTrue + ratioFalse) < ratioTrue;
		}

		public static bool Chance(this IRandom random, uint ratioTrue, uint ratioFalse)
		{
			return random.HalfOpenRange(ratioTrue + ratioFalse) < ratioTrue;
		}

		public static bool Chance(this IRandom random, float ratioTrue, float ratioFalse)
		{
			return random.HalfOpenRange(ratioTrue + ratioFalse) < ratioTrue;
		}

		public static bool Chance(this IRandom random, double ratioTrue, double ratioFalse)
		{
			return random.HalfOpenRange(ratioTrue + ratioFalse) < ratioTrue;
		}

		#endregion

		#region Probability

		public static bool Probability(this IRandom random, int numerator, int denominator)
		{
			return random.HalfOpenRange(denominator) < numerator;
		}

		public static bool Probability(this IRandom random, uint numerator, uint denominator)
		{
			return random.HalfOpenRange(denominator) < numerator;
		}

		public static bool Probability(this IRandom random, float numerator, float denominator)
		{
			return random.HalfOpenRange(denominator) < numerator;
		}

		public static bool Probability(this IRandom random, double numerator, double denominator)
		{
			return random.HalfOpenRange(denominator) < numerator;
		}

		public static bool Probability(this IRandom random, float probability)
		{
			return random.HalfOpenFloatUnit() < probability;
		}

		public static bool Probability(this IRandom random, double probability)
		{
			return random.HalfOpenDoubleUnit() < probability;
		}

		#endregion
	}
}
