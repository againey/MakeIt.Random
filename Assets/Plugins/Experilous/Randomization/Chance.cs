﻿/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public static class Chance
	{
		#region Ratio

		public static bool Ratio(int ratioTrue, int ratioFalse)
		{
			return RandomRange.HalfOpen(ratioTrue + ratioFalse, DefaultRandomEngine.sharedInstance) < ratioTrue;
		}

		public static bool Ratio(int ratioTrue, int ratioFalse, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(ratioTrue + ratioFalse, engine) < ratioTrue;
		}

		public static bool Ratio(uint ratioTrue, uint ratioFalse)
		{
			return RandomRange.HalfOpen(ratioTrue + ratioFalse, DefaultRandomEngine.sharedInstance) < ratioTrue;
		}

		public static bool Ratio(uint ratioTrue, uint ratioFalse, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(ratioTrue + ratioFalse, engine) < ratioTrue;
		}

		public static bool Ratio(float ratioTrue, float ratioFalse)
		{
			return RandomRange.HalfOpen(ratioTrue + ratioFalse, DefaultRandomEngine.sharedInstance) < ratioTrue;
		}

		public static bool Ratio(float ratioTrue, float ratioFalse, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(ratioTrue + ratioFalse, engine) < ratioTrue;
		}

		public static bool Ratio(double ratioTrue, double ratioFalse)
		{
			return RandomRange.HalfOpen(ratioTrue + ratioFalse, DefaultRandomEngine.sharedInstance) < ratioTrue;
		}

		public static bool Ratio(double ratioTrue, double ratioFalse, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(ratioTrue + ratioFalse, engine) < ratioTrue;
		}

		#endregion

		#region Probability

		public static bool Probability(int numerator, int denominator)
		{
			return RandomRange.HalfOpen(denominator, DefaultRandomEngine.sharedInstance) < numerator;
		}

		public static bool Probability(int numerator, int denominator, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(denominator, engine) < numerator;
		}

		public static bool Probability(uint numerator, uint denominator)
		{
			return RandomRange.HalfOpen(denominator, DefaultRandomEngine.sharedInstance) < numerator;
		}

		public static bool Probability(uint numerator, uint denominator, IRandomEngine engine)
		{
			return RandomRange.HalfOpen(denominator, engine) < numerator;
		}

		public static bool Probability(float probability)
		{
			return RandomUnit.HalfOpenFloat(DefaultRandomEngine.sharedInstance) < probability;
		}

		public static bool Probability(float probability, IRandomEngine engine)
		{
			return RandomUnit.HalfOpenFloat(engine) < probability;
		}

		public static bool Probability(double probability)
		{
			return RandomUnit.HalfOpenDouble(DefaultRandomEngine.sharedInstance) < probability;
		}

		public static bool Probability(double probability, IRandomEngine engine)
		{
			return RandomUnit.HalfOpenDouble(engine) < probability;
		}

		#endregion
	}
}