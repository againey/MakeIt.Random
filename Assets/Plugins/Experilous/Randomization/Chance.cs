/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	public struct Chance
	{
		private IRandomEngine _random;

		public Chance(IRandomEngine random)
		{
			_random = random;
		}

		#region Ratio

		public bool Ratio(int ratioTrue, int ratioFalse)
		{
			return _random.Range().HalfOpen(ratioTrue + ratioFalse) < ratioTrue;
		}

		public bool Ratio(uint ratioTrue, uint ratioFalse)
		{
			return _random.Range().HalfOpen(ratioTrue + ratioFalse) < ratioTrue;
		}

		public bool Ratio(float ratioTrue, float ratioFalse)
		{
			return _random.Range().HalfOpen(ratioTrue + ratioFalse) < ratioTrue;
		}

		public bool Ratio(double ratioTrue, double ratioFalse)
		{
			return _random.Range().HalfOpen(ratioTrue + ratioFalse) < ratioTrue;
		}

		#endregion

		#region Probability

		public bool Probability(int numerator, int denominator)
		{
			return _random.Range().HalfOpen(denominator) < numerator;
		}

		public bool Probability(uint numerator, uint denominator)
		{
			return _random.Range().HalfOpen(denominator) < numerator;
		}

		public bool Probability(float probability)
		{
			return _random.Unit().HalfOpenFloat() < probability;
		}

		public bool Probability(double probability)
		{
			return _random.Unit().HalfOpenDouble() < probability;
		}

		#endregion
	}

	public static class ChanceExtensions
	{
		public static Chance Chance(this IRandomEngine random)
		{
			return new Chance(random);
		}
	}
}
