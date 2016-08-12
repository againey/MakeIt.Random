/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeIt.Random
{
	public static class RandomNumber
	{
		#region Bits

		public static uint Bit(this IRandomEngine random)
		{
			return random.Next32() >> 31;
		}

		public static uint Bits32(this IRandomEngine random)
		{
			return random.Next32();
		}

		public static uint Bits32(this IRandomEngine random, int bitCount)
		{
			return random.Next32() >> (32 - bitCount);
		}

		public static ulong Bits64(this IRandomEngine random)
		{
			return random.Next64();
		}

		public static ulong Bits64(this IRandomEngine random, int bitCount)
		{
			return random.Next64() >> (64 - bitCount);
		}

		#endregion

		#region {-1, 0, +1}, Evenly Weighted

		public static int OneOrZero(this IRandomEngine random)
		{
			return (int)(random.Next32() >> 31);
		}

		public static int Sign(this IRandomEngine random)
		{
			return (int)((random.Next32() >> 30) & 2U) - 1;
		}

		public static int SignOrZero(this IRandomEngine random)
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

		public static int OneOrZero(this IRandomEngine random, int ratioOne, int ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		public static int OneOrZero(this IRandomEngine random, uint ratioOne, uint ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		public static int OneOrZero(this IRandomEngine random, float ratioOne, float ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		public static int OneOrZero(this IRandomEngine random, double ratioOne, double ratioZero)
		{
			return random.Chance(ratioOne, ratioZero) ? 1 : 0;
		}

		public static int Sign(this IRandomEngine random, int ratioPositive, int ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? 1 : 0;
		}

		public static int Sign(this IRandomEngine random, uint ratioPositive, uint ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? 1 : 0;
		}

		public static int Sign(this IRandomEngine random, float ratioPositive, float ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? 1 : 0;
		}

		public static int Sign(this IRandomEngine random, double ratioPositive, double ratioNegative)
		{
			return random.Chance(ratioPositive, ratioNegative) ? 1 : 0;
		}

		public static int SignOrZero(this IRandomEngine random, int ratioPositive, int ratioZero, int ratioNegative)
		{
			int ratioNonNegative = ratioPositive + ratioZero;
			int n = random.HalfOpenRange(ratioNonNegative + ratioNegative);
			return n < ratioNonNegative ? (n < ratioPositive ? +1 : 0) : -1;
		}

		public static int SignOrZero(this IRandomEngine random, uint ratioPositive, uint ratioZero, uint ratioNegative)
		{
			uint ratioNonNegative = ratioPositive + ratioZero;
			uint n = random.HalfOpenRange(ratioNonNegative + ratioNegative);
			return n < ratioNonNegative ? (n < ratioPositive ? +1 : 0) : -1;
		}

		public static int SignOrZero(this IRandomEngine random, float ratioPositive, float ratioZero, float ratioNegative)
		{
			float ratioNonNegative = ratioPositive + ratioZero;
			float n = random.HalfOpenRange(ratioNonNegative + ratioNegative);
			return n < ratioNonNegative ? (n < ratioPositive ? +1 : 0) : -1;
		}

		public static int SignOrZero(this IRandomEngine random, double ratioPositive, double ratioZero, double ratioNegative)
		{
			double ratioNonNegative = ratioPositive + ratioZero;
			double n = random.HalfOpenRange(ratioNonNegative + ratioNegative);
			return n < ratioNonNegative ? (n < ratioPositive ? +1 : 0) : -1;
		}

		#endregion

		#region {-1, 0, +1}, Probability

		public static int OneProbability(this IRandomEngine random, int numerator, int denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? 1 : 0;
		}

		public static int OneProbability(this IRandomEngine random, uint numerator, uint denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? 1 : 0;
		}

		public static int OneProbability(this IRandomEngine random, float numerator, float denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? 1 : 0;
		}

		public static int OneProbability(this IRandomEngine random, double numerator, double denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? 1 : 0;
		}

		public static int OneProbability(this IRandomEngine random, float probability)
		{
			return random.HalfOpenFloatUnit() < probability ? 1 : 0;
		}

		public static int OneProbability(this IRandomEngine random, double probability)
		{
			return random.HalfOpenDoubleUnit() < probability ? 1 : 0;
		}

		public static int PositiveProbability(this IRandomEngine random, int numerator, int denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
		}

		public static int PositiveProbability(this IRandomEngine random, uint numerator, uint denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
		}

		public static int PositiveProbability(this IRandomEngine random, float numerator, float denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
		}

		public static int PositiveProbability(this IRandomEngine random, double numerator, double denominator)
		{
			return random.HalfOpenRange(denominator) < numerator ? +1 : -1;
		}

		public static int PositiveProbability(this IRandomEngine random, float probability)
		{
			return random.HalfOpenFloatUnit() < probability ? +1 : -1;
		}

		public static int PositiveProbability(this IRandomEngine random, double probability)
		{
			return random.HalfOpenDoubleUnit() < probability ? +1 : -1;
		}

		public static int SignProbability(this IRandomEngine random, int numeratorPositive, int numeratorNegative, int denominator)
		{
			int numeratorNonZero = numeratorPositive + numeratorNegative;
			int n = random.HalfOpenRange(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		public static int SignProbability(this IRandomEngine random, uint numeratorPositive, uint numeratorNegative, uint denominator)
		{
			uint numeratorNonZero = numeratorPositive + numeratorNegative;
			uint n = random.HalfOpenRange(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		public static int SignProbability(this IRandomEngine random, float numeratorPositive, float numeratorNegative, float denominator)
		{
			float numeratorNonZero = numeratorPositive + numeratorNegative;
			float n = random.HalfOpenRange(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		public static int SignProbability(this IRandomEngine random, double numeratorPositive, double numeratorNegative, double denominator)
		{
			double numeratorNonZero = numeratorPositive + numeratorNegative;
			double n = random.HalfOpenRange(denominator);
			return n < numeratorNonZero ? (n < numeratorPositive ? +1 : -1) : 0;
		}

		public static int SignProbability(this IRandomEngine random, float probabilityPositive, float probabilityNegative)
		{
			float probabilityNonZero = probabilityPositive + probabilityNegative;
			float n = random.HalfOpenFloatUnit();
			return n < probabilityNonZero ? (n < probabilityPositive ? +1 : -1) : 0;
		}

		public static int SignProbability(this IRandomEngine random, double probabilityPositive, double probabilityNegative)
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

		public static float FloatDegrees(this IRandomEngine random)
		{
			return random.HalfOpenFloatUnit() * _floatDegreesPerTurn;
		}

		public static float SignedFloatDegrees(this IRandomEngine random)
		{
			return random.HalfOpenFloatUnit() * _floatDegreesPerTurn - _floatDegreesPerHalfTurn;
		}

		public static float FloatRadians(this IRandomEngine random)
		{
			return random.HalfOpenFloatUnit() * _floatRadiansPerTurn;
		}

		public static float SignedFloatRadians(this IRandomEngine random)
		{
			return random.HalfOpenFloatUnit() * _floatRadiansPerTurn - _floatRadiansPerHalfTurn;
		}

		public static double DoubleDegrees(this IRandomEngine random)
		{
			return random.HalfOpenDoubleUnit() * _doubleDegreesPerTurn;
		}

		public static double SignedDoubleDegrees(this IRandomEngine random)
		{
			return random.HalfOpenDoubleUnit() * _doubleDegreesPerTurn - _doubleDegreesPerHalfTurn;
		}

		public static double DoubleRadians(this IRandomEngine random)
		{
			return random.HalfOpenDoubleUnit() * _doubleRadiansPerTurn;
		}

		public static double SignedDoubleRadians(this IRandomEngine random)
		{
			return random.HalfOpenDoubleUnit() * _doubleRadiansPerTurn - _doubleRadiansPerHalfTurn;
		}

		#endregion
	}
}
