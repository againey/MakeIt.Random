/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// An interface for any generator of dice rolls, with the quantity, side count, and other dice attributes to be determined by the implementation.
	/// </summary>
	public interface IDiceGenerator
	{
		/// <summary>
		/// Simulates a roll of dice according to the properties represented by the generator.
		/// </summary>
		/// <returns>The sum of the kept dice rolls.</returns>
		int Roll();

		/// <summary>
		/// An array of the kept dice rolls from the most recent call to <see cref="Roll()"/>.
		/// </summary>
		/// <remarks>The value of this field is undefined if <see cref="Roll()"/> has not yet been called at least once on this generator.</remarks>
		int[] dice { get; }

		/// <summary>
		/// An array of the discarded dice rolls from the most recent call to <see cref="Roll()"/>.
		/// </summary>
		/// <remarks>The value of this field is undefined if <see cref="Roll()"/> has not yet been called at least once on this generator.</remarks>
		int[] discardedDice { get; }
	}

	/// <summary>
	/// A static class of extension methods for simulating random dice rolls.
	/// </summary>
	public static class RandomDice
	{
		#region Roll

		/// <summary>
		/// Generates a random die roll, simulating a die with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="sides">The number of sides of the die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <returns>The numeric value of the simulated die roll, in the range [1, <paramref name="sides"/>].</returns>
		public static int RollDie(this IRandom random, int sides)
		{
			return random.RangeCO(sides) + 1;
		}

		/// <summary>
		/// Generates a random die roll, simulating a die with the number of sides determined by the specified <paramref name="dieGenerator"/>.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <returns>The numeric value of the simulated die roll, in the range determined by <paramref name="dieGenerator"/>.</returns>
		public static int RollDie(this IRangeGenerator<int> dieGenerator)
		{
			return dieGenerator.Next();
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <returns>An array of the numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		public static int[] RollDice(this IRandom random, int quantity, int sides)
		{
			var dice = new int[quantity];
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = random.RangeCO(sides) + 1;
			}
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <returns>An array of the numeric values of the simulated dice rolls, each element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		public static int[] RollDice(this IRangeGenerator<int> dieGenerator, int quantity)
		{
			var dice = new int[quantity];
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = dieGenerator.Next();
			}
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dice">A pre-allocated array into which the numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDice(this IRandom random, int quantity, int sides, int[] dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = random.RangeCO(sides) + 1;
			}
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A pre-allocated array into which the numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDice(this IRangeGenerator<int> dieGenerator, int quantity, int[] dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = dieGenerator.Next();
			}
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dice">A list into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.  After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>, and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDice(this IRandom random, int quantity, int sides, List<int> dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			while (dice.Count < quantity)
			{
				dice.Add(random.RangeCO(sides) + 1);
			}
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A list into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.  After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>, and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDice(this IRangeGenerator<int> dieGenerator, int quantity, List<int> dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			while (dice.Count < quantity)
			{
				dice.Add(dieGenerator.Next());
			}
		}

		#endregion

		#region SumRoll

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		public static int SumRollDice(this IRandom random, int quantity, int sides)
		{
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				sum += random.RangeCO(sides);
			}
			return sum + quantity;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		public static int SumRollDice(this IRangeGenerator<int> dieGenerator, int quantity)
		{
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				sum += dieGenerator.Next();
			}
			return sum;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dice">A an output array into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDice(this IRandom random, int quantity, int sides, out int[] dice)
		{
			dice = new int[quantity];
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = random.RangeCO(sides) + 1;
				dice[i] = die;
				sum += die;
			}
			return sum;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A an output array into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDice(this IRangeGenerator<int> dieGenerator, int quantity, out int[] dice)
		{
			dice = new int[quantity];
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = dieGenerator.Next();
				dice[i] = die;
				sum += die;
			}
			return sum;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dice">A pre-allocated array into which the numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDice(this IRandom random, int quantity, int sides, int[] dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = random.RangeCO(sides) + 1;
				dice[i] = die;
				sum += die;
			}
			return sum;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A pre-allocated array into which the numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDice(this IRangeGenerator<int> dieGenerator, int quantity, int[] dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = dieGenerator.Next();
				dice[i] = die;
				sum += die;
			}
			return sum;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dice">A list into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.  After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>, and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDice(this IRandom random, int quantity, int sides, List<int> dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			int sum = 0;
			while (dice.Count < quantity)
			{
				int die = random.RangeCO(sides) + 1;
				dice.Add(die);
				sum += die;
			}
			return sum;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A list into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.  After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>, and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDice(this IRangeGenerator<int> dieGenerator, int quantity, List<int> dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			int sum = 0;
			while (dice.Count < quantity)
			{
				int die = dieGenerator.Next();
				dice.Add(die);
				sum += die;
			}
			return sum;
		}

		#endregion

		#region Private Keep/Drop Helper Functions

		private static int Sum(int[] dice)
		{
			int sum = 0;
			for (int i = 0; i < dice.Length; ++i)
			{
				sum += dice[i];
			}
			return sum;
		}

		private static int Sum(List<int> dice)
		{
			int sum = 0;
			for (int i = 0; i < dice.Count; ++i)
			{
				sum += dice[i];
			}
			return sum;
		}

		private static int FindMinIndex(IList<int> dice)
		{
			int minIndex = 0;
			int min = dice[0];
			for (int i = 1; i < dice.Count; ++i)
			{
				if (dice[i] < min)
				{
					minIndex = i;
					min = dice[i];
				}
			}
			return minIndex;
		}

		private static int FindMaxIndex(IList<int> dice)
		{
			int maxIndex = 0;
			int max = dice[0];
			for (int i = 1; i < dice.Count; ++i)
			{
				if (dice[i] > max)
				{
					maxIndex = i;
					max = dice[i];
				}
			}
			return maxIndex;
		}

		private static void RollAdditionalKeepHighest(this IRandom random, int additionalQuantity, int sides, IList<int> dice)
		{
			int i = 0;
			while (i < additionalQuantity)
			{
				int minIndex = FindMinIndex(dice);
				int min = dice[minIndex];
				int die;
				do
				{
					if (i >= additionalQuantity) return;
					die = random.RangeCO(sides) + 1;
					++i;
				} while (die <= min);

				dice[minIndex] = die;
			}
		}

		private static void RollAdditionalKeepHighest(this IRangeGenerator<int> dieGenerator, int additionalQuantity, IList<int> dice)
		{
			int i = 0;
			while (i < additionalQuantity)
			{
				int minIndex = FindMinIndex(dice);
				int min = dice[minIndex];
				int die;
				do
				{
					if (i >= additionalQuantity) return;
					die = dieGenerator.Next();
					++i;
				} while (die <= min);

				dice[minIndex] = die;
			}
		}

		private static void RollAdditionalKeepHighest(this IRandom random, int additionalQuantity, int sides, IList<int> dice, int[] discardedDice)
		{
			if (discardedDice == null) throw new System.ArgumentNullException("discardedDice");
			if (discardedDice.Length != additionalQuantity) throw new System.ArgumentException("The discardedDice parameter must be the same length as the number of dice requested to be discarded.", "discardedDice");

			int i = 0;
			while (i < additionalQuantity)
			{
				int minIndex = FindMinIndex(dice);
				int min = dice[minIndex];
				int die;
				while (true)
				{
					if (i >= additionalQuantity) return;

					die = random.RangeCO(sides) + 1;
					if (die <= min)
					{
						discardedDice[i++] = die;
					}
					else
					{
						discardedDice[i++] = min;
						dice[minIndex] = die;
						break;
					}
				}
			}
		}

		private static void RollAdditionalKeepHighest(this IRangeGenerator<int> dieGenerator, int additionalQuantity, IList<int> dice, int[] discardedDice)
		{
			if (discardedDice == null) throw new System.ArgumentNullException("discardedDice");
			if (discardedDice.Length != additionalQuantity) throw new System.ArgumentException("The discardedDice parameter must be the same length as the number of dice requested to be discarded.", "discardedDice");

			int i = 0;
			while (i < additionalQuantity)
			{
				int minIndex = FindMinIndex(dice);
				int min = dice[minIndex];
				int die;
				while (true)
				{
					if (i >= additionalQuantity) return;

					die = dieGenerator.Next();
					if (die <= min)
					{
						discardedDice[i++] = die;
					}
					else
					{
						discardedDice[i++] = min;
						dice[minIndex] = die;
						break;
					}
				}
			}
		}

		private static void RollAdditionalKeepHighest(this IRandom random, int additionalQuantity, int sides, IList<int> dice, List<int> discardedDice)
		{
			discardedDice.Clear();

			while (discardedDice.Count < additionalQuantity)
			{
				int minIndex = FindMinIndex(dice);
				int min = dice[minIndex];
				int die;
				while (true)
				{
					
					if (discardedDice.Count >= additionalQuantity) return;

					die = random.RangeCO(sides) + 1;
					if (die <= min)
					{
						discardedDice.Add(die);
					}
					else
					{
						discardedDice.Add(min);
						dice[minIndex] = die;
						break;
					}
				}
			}
		}

		private static void RollAdditionalKeepHighest(this IRangeGenerator<int> dieGenerator, int additionalQuantity, IList<int> dice, List<int> discardedDice)
		{
			discardedDice.Clear();

			while (discardedDice.Count < additionalQuantity)
			{
				int minIndex = FindMinIndex(dice);
				int min = dice[minIndex];
				int die;
				while (true)
				{
					
					if (discardedDice.Count >= additionalQuantity) return;

					die = dieGenerator.Next();
					if (die <= min)
					{
						discardedDice.Add(die);
					}
					else
					{
						discardedDice.Add(min);
						dice[minIndex] = die;
						break;
					}
				}
			}
		}

		private static void RollAdditionalKeepLowest(this IRandom random, int additionalQuantity, int sides, IList<int> dice)
		{
			int i = 0;
			while (i < additionalQuantity)
			{
				int maxIndex = FindMaxIndex(dice);
				int max = dice[maxIndex];
				int die;
				do
				{
					if (i >= additionalQuantity) return;
					die = random.RangeCO(sides) + 1;
					++i;
				} while (die >= max);

				dice[maxIndex] = die;
			}
		}

		private static void RollAdditionalKeepLowest(this IRangeGenerator<int> dieGenerator, int additionalQuantity, IList<int> dice)
		{
			int i = 0;
			while (i < additionalQuantity)
			{
				int maxIndex = FindMaxIndex(dice);
				int max = dice[maxIndex];
				int die;
				do
				{
					if (i >= additionalQuantity) return;
					die = dieGenerator.Next();
					++i;
				} while (die >= max);

				dice[maxIndex] = die;
			}
		}

		private static void RollAdditionalKeepLowest(this IRandom random, int additionalQuantity, int sides, IList<int> dice, int[] discardedDice)
		{
			if (discardedDice == null) throw new System.ArgumentNullException("discardedDice");
			if (discardedDice.Length != additionalQuantity) throw new System.ArgumentException("The discardedDice parameter must be the same length as the number of dice requested to be discarded.", "discardedDice");

			int i = 0;
			while (i < additionalQuantity)
			{
				int maxIndex = FindMaxIndex(dice);
				int max = dice[maxIndex];
				int die;
				while (true)
				{
					if (i >= additionalQuantity) return;

					die = random.RangeCO(sides) + 1;
					if (die >= max)
					{
						discardedDice[i++] = die;
					}
					else
					{
						discardedDice[i++] = max;
						dice[maxIndex] = die;
						break;
					}
				}
			}
		}

		private static void RollAdditionalKeepLowest(this IRangeGenerator<int> dieGenerator, int additionalQuantity, IList<int> dice, int[] discardedDice)
		{
			if (discardedDice == null) throw new System.ArgumentNullException("discardedDice");
			if (discardedDice.Length != additionalQuantity) throw new System.ArgumentException("The discardedDice parameter must be the same length as the number of dice requested to be discarded.", "discardedDice");

			int i = 0;
			while (i < additionalQuantity)
			{
				int maxIndex = FindMaxIndex(dice);
				int max = dice[maxIndex];
				int die;
				while (true)
				{
					if (i >= additionalQuantity) return;

					die = dieGenerator.Next();
					if (die >= max)
					{
						discardedDice[i++] = die;
					}
					else
					{
						discardedDice[i++] = max;
						dice[maxIndex] = die;
						break;
					}
				}
			}
		}

		private static void RollAdditionalKeepLowest(this IRandom random, int additionalQuantity, int sides, IList<int> dice, List<int> discardedDice)
		{
			discardedDice.Clear();

			while (discardedDice.Count < additionalQuantity)
			{
				int maxIndex = FindMaxIndex(dice);
				int max = dice[maxIndex];
				int die;
				while (true)
				{
					
					if (discardedDice.Count >= additionalQuantity) return;

					die = random.RangeCO(sides) + 1;
					if (die >= max)
					{
						discardedDice.Add(die);
					}
					else
					{
						discardedDice.Add(max);
						dice[maxIndex] = die;
						break;
					}
				}
			}
		}

		private static void RollAdditionalKeepLowest(this IRangeGenerator<int> dieGenerator, int additionalQuantity, IList<int> dice, List<int> discardedDice)
		{
			discardedDice.Clear();

			while (discardedDice.Count < additionalQuantity)
			{
				int maxIndex = FindMaxIndex(dice);
				int max = dice[maxIndex];
				int die;
				while (true)
				{
					
					if (discardedDice.Count >= additionalQuantity) return;

					die = dieGenerator.Next();
					if (die >= max)
					{
						discardedDice.Add(die);
					}
					else
					{
						discardedDice.Add(max);
						dice[maxIndex] = die;
						break;
					}
				}
			}
		}

		#endregion

		#region RollKeep/Drop

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the highest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		public static int[] RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDice(keepQuantity, sides);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice);
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the highest numeric values of the simulated dice rolls, each element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		public static int[] RollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity)
		{
			int[] dice = dieGenerator.RollDice(keepQuantity);
			dieGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice);
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, int[] dice)
		{
			dieGenerator.RollDice(keepQuantity, dice);
			dieGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, int[] dice, int[] discardedDice)
		{
			dieGenerator.RollDice(keepQuantity, dice);
			dieGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, List<int> dice)
		{
			dieGenerator.RollDice(keepQuantity, dice);
			dieGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			dieGenerator.RollDice(keepQuantity, dice);
			dieGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the lowest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		public static int[] RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDice(keepQuantity, sides);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice);
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the lowest numeric values of the simulated dice rolls, each element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		public static int[] RollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity)
		{
			int[] dice = dieGenerator.RollDice(keepQuantity);
			dieGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice);
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, int[] dice)
		{
			dieGenerator.RollDice(keepQuantity, dice);
			dieGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, int[] dice, int[] discardedDice)
		{
			dieGenerator.RollDice(keepQuantity, dice);
			dieGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, List<int> dice)
		{
			dieGenerator.RollDice(keepQuantity, dice);
			dieGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static void RollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			dieGenerator.RollDice(keepQuantity, dice);
			dieGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>An array of the lowest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int)"/>.</note></remarks>
		public static int[] RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>An array of the lowest numeric values of the simulated dice rolls, each element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int)"/>.</note></remarks>
		public static int[] RollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity)
		{
			return dieGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, int[] dice)
		{
			dieGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, int[] dice, int[] discardedDice)
		{
			dieGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, List{int})"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, List{int})"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, List<int> dice)
		{
			dieGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, List{int}, List{int})"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, List{int}, List{int})"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			dieGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>An array of the highest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int)"/>.</note></remarks>
		public static int[] RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>An array of the highest numeric values of the simulated dice rolls, each element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int)"/>.</note></remarks>
		public static int[] RollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity)
		{
			return dieGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, int[] dice)
		{
			dieGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, int[] dice, int[] discardedDice)
		{
			dieGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, List{int})"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, List{int})"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, List<int> dice)
		{
			dieGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, List{int}, List{int})"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, List{int}, List{int})"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			dieGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		#endregion

		#region SumRollKeep/Drop

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDiceKeepHighest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		public static int SumRollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity)
		{
			int[] dice = dieGenerator.RollDiceKeepHighest(quantity, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, out int[] dice)
		{
			dice = random.RollDiceKeepHighest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, out int[] dice)
		{
			dice = dieGenerator.RollDiceKeepHighest(quantity, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, out int[] dice, out int[] discardedDice)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, out int[] dice, out int[] discardedDice)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			dieGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, int[] dice)
		{
			dieGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, int[] dice, int[] discardedDice)
		{
			dieGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, List<int> dice)
		{
			dieGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepHighest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			dieGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDiceKeepLowest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		public static int SumRollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity)
		{
			int[] dice = dieGenerator.RollDiceKeepLowest(quantity, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, out int[] dice)
		{
			dice = random.RollDiceKeepLowest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, out int[] dice)
		{
			dice = dieGenerator.RollDiceKeepLowest(quantity, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, out int[] dice, out int[] discardedDice)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, out int[] dice, out int[] discardedDice)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			dieGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, int[] dice)
		{
			dieGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, int[] dice, int[] discardedDice)
		{
			dieGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, List<int> dice)
		{
			dieGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</remarks>
		public static int SumRollDiceKeepLowest(this IRangeGenerator<int> dieGenerator, int quantity, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			dieGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int)"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int)"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity)
		{
			return dieGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, out int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, out int[] dice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, out dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, out int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, out int[] dice)
		{
			return dieGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, out dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, out int[], out int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, out int[] dice, out int[] discardedDice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, out dice, out discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, out int[], out int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, out int[] dice, out int[] discardedDice)
		{
			return dieGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, out dice, out discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, int[] dice)
		{
			return dieGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, int[] dice, int[] discardedDice)
		{
			return dieGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, List{int})"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, List{int})"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, List<int> dice)
		{
			return dieGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, List{int}, List{int})"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, List{int}, List{int})"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return dieGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int)"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int)"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity)
		{
			return dieGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, out int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, out int[] dice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, out dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, out int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, out int[] dice)
		{
			return dieGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, out dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, out int[], out int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, out int[] dice, out int[] discardedDice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, out dice, out discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, out int[], out int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, out int[] dice, out int[] discardedDice)
		{
			return dieGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, out dice, out discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, int[] dice)
		{
			return dieGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, int[] dice, int[] discardedDice)
		{
			return dieGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, List{int})"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, List{int})"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, List<int> dice)
		{
			return dieGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, List{int}, List{int})"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the number of sides determined by the specified <paramref name="dieGenerator"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="dieGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range determined by <paramref name="dieGenerator"/>.</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range determined by <paramref name="dieGenerator"/>.</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, List{int}, List{int})"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRangeGenerator<int> dieGenerator, int quantity, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return dieGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		#endregion

		#region Common Die Sizes

		/// <summary>
		/// Generates a random die roll, simulating a 4 sided die.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>The numeric value of the simulated die roll, in the range [1, 4].</returns>
		public static int RollD4(this IRandom random)
		{
			return random.RangeCC(1, 4);
		}

		/// <summary>
		/// Generates a random die roll, simulating a 6 sided die.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>The numeric value of the simulated die roll, in the range [1, 6].</returns>
		public static int RollD6(this IRandom random)
		{
			return random.RangeCC(1, 6);
		}

		/// <summary>
		/// Generates a random die roll, simulating a 8 sided die.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>The numeric value of the simulated die roll, in the range [1, 8].</returns>
		public static int RollD8(this IRandom random)
		{
			return random.RangeCC(1, 8);
		}

		/// <summary>
		/// Generates a random die roll, simulating a 10 sided die.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>The numeric value of the simulated die roll, in the range [1, 10].</returns>
		public static int RollD10(this IRandom random)
		{
			return random.RangeCC(1, 10);
		}

		/// <summary>
		/// Generates a random die roll, simulating a 12 sided die.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>The numeric value of the simulated die roll, in the range [1, 12].</returns>
		public static int RollD12(this IRandom random)
		{
			return random.RangeCC(1, 12);
		}

		/// <summary>
		/// Generates a random die roll, simulating a 20 sided die.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>The numeric value of the simulated die roll, in the range [1, 20].</returns>
		public static int RollD20(this IRandom random)
		{
			return random.RangeCC(1, 20);
		}

		#endregion

		#region Dice Generator

		/// <summary>
		/// Prepares an efficient range generator which will generate dice rolls, one at a time, with values greater than or equal to 1 and less than or equal to <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generated values of the returned generator are derived.</param>
		/// <param name="sides">The number of sides of the die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <returns>A range generator which produces integers in the range [1, <paramref name="sides"/>].</returns>
		public static IRangeGenerator<int> MakeDieGenerator(this IRandom random, int sides)
		{
			return random.MakeRangeOCGenerator(sides);
		}

		private class DiceGeneratorBase
		{
			protected readonly IRangeGenerator<int> _dieGenerator;
			protected readonly int[] _dice;
			protected readonly int[] _discardedDice;

			public DiceGeneratorBase(IRandom random, int sides, int keepQuantity, int dropQuantity)
			{
				_dieGenerator = random.MakeDieGenerator(sides);
				_dice = new int[keepQuantity];
				_discardedDice = new int[dropQuantity];
			}

			public int[] dice { get { return _dice; } }
			public int[] discardedDice { get { return _discardedDice; } }
		}

		private class SimpleDiceGenerator : DiceGeneratorBase, IDiceGenerator
		{
			public SimpleDiceGenerator(IRandom random, int quantity, int sides) : base(random, sides, quantity, 0) { }
			public int Roll() { return SumRollDice(_dieGenerator, _dice.Length, _dice); }
		}

		private class KeepHighestDiceGenerator : DiceGeneratorBase, IDiceGenerator
		{
			public KeepHighestDiceGenerator(IRandom random, int quantity, int sides, int keepQuantity) : base(random, sides, keepQuantity, quantity - keepQuantity) { }
			public int Roll() { return SumRollDiceKeepHighest(_dieGenerator, _dice.Length + _discardedDice.Length, _dice.Length, _dice, _discardedDice); }
		}

		private class KeepLowestDiceGenerator : DiceGeneratorBase, IDiceGenerator
		{
			public KeepLowestDiceGenerator(IRandom random, int quantity, int sides, int keepQuantity) : base(random, sides, keepQuantity, quantity - keepQuantity) { }
			public int Roll() { return SumRollDiceKeepLowest(_dieGenerator, _dice.Length + _discardedDice.Length, _dice.Length, _dice, _discardedDice); }
		}

		/// <summary>
		/// Prepares an efficient dice generator which will generate dice rolls, one batch at a time.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generated values of the returned generator are derived.</param>
		/// <param name="quantity">The number of dice to roll in a single batch.  Must be positive.</param>
		/// <param name="sides">The number of sides of the dice to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <returns>A dice generator which will roll <paramref name="quantity"/> dice at a time, with values in the range [1, <paramref name="sides"/>].</returns>
		public static IDiceGenerator MakeDiceGenerator(this IRandom random, int quantity, int sides)
		{
			return new SimpleDiceGenerator(random, quantity, sides);
		}

		/// <summary>
		/// Prepares an efficient dice generator which will generate dice rolls, one batch at a time,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generated values of the returned generator are derived.</param>
		/// <param name="quantity">The number of dice to roll in a single batch.  Must be positive.</param>
		/// <param name="sides">The number of sides of the dice to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep in each batch.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>A dice generator which will roll <paramref name="quantity"/> dice at a time, keeping the highest <paramref name="keepQuantity"/>, with values in the range [1, <paramref name="sides"/>].</returns>
		public static IDiceGenerator MakeDiceGeneratorKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			return new KeepHighestDiceGenerator(random, quantity, sides, keepQuantity);
		}

		/// <summary>
		/// Prepares an efficient dice generator which will generate dice rolls, one batch at a time,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generated values of the returned generator are derived.</param>
		/// <param name="quantity">The number of dice to roll in a single batch.  Must be positive.</param>
		/// <param name="sides">The number of sides of the dice to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep in each batch.  Must be positive and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>A dice generator which will roll <paramref name="quantity"/> dice at a time, keeping the lowest <paramref name="keepQuantity"/>, with values in the range [1, <paramref name="sides"/>].</returns>
		public static IDiceGenerator MakeDiceGeneratorKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			return new KeepLowestDiceGenerator(random, quantity, sides, keepQuantity);
		}

		/// <summary>
		/// Prepares an efficient dice generator which will generate dice rolls, one batch at a time,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generated values of the returned generator are derived.</param>
		/// <param name="quantity">The number of dice to roll in a single batch.  Must be positive.</param>
		/// <param name="sides">The number of sides of the dice to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop from each batch.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>A dice generator which will roll <paramref name="quantity"/> dice at a time, dropping the highest <paramref name="dropQuantity"/>, with values in the range [1, <paramref name="sides"/>].</returns>
		public static IDiceGenerator MakeDiceGeneratorDropHighest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return new KeepLowestDiceGenerator(random, quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Prepares an efficient dice generator which will generate dice rolls, one batch at a time,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generated values of the returned generator are derived.</param>
		/// <param name="quantity">The number of dice to roll in a single batch.  Must be positive.</param>
		/// <param name="sides">The number of sides of the dice to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop from each batch.  Must be non-negative and less than <paramref name="quantity"/>.</param>
		/// <returns>A dice generator which will roll <paramref name="quantity"/> dice at a time, dropping the lowest <paramref name="dropQuantity"/>, with values in the range [1, <paramref name="sides"/>].</returns>
		public static IDiceGenerator MakeDiceGeneratorDropLowest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return new KeepHighestDiceGenerator(random, quantity, sides, quantity - dropQuantity);
		}

		private static System.Text.RegularExpressions.Regex _diceNotationRegex = new System.Text.RegularExpressions.Regex(
			@"\A(?<quantity>[1-9][0-9]*)?(?:d|D)(?<sides>[1-9][0-9]*)(?:\s*(?<keepDrop>k|K|d|D)(?<keepDropWhat>h|H|l|L)?(?<keepDropQuantity>[1-9][0-9]*)?)\z");

		/// <summary>
		/// Prepares an efficient dice generator which will generate dice rolls, one batch at a time,
		/// according to the rules determined by the specified <paramref name="notation"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generated values of the returned generator are derived.</param>
		/// <param name="notation">Describes the rules by which dice will be rolled, using common dice notation.</param>
		/// <returns>A dice generator which will roll one or more dice at a time according to the rules determined by the specified <paramref name="notation"/>.</returns>
		/// <remarks>
		/// <para>The notation supports specifying the quantity of dice to roll, how many sides the dice have, and whether to keep or drop a specified number of highest or lowest rolls.</para>
		/// <para>At a bare minimum, the number of sides must be specified.  This is done using the letter 'd' followed by the number of sides, such as "d6" to roll a six-sided die.</para>
		/// <para>To specify the quantity of dice, prefix 'd' with the desired amount.  "3d8" will roll a total of three dice, each with eight sides.</para>
		/// <para>If you wish to keep the highest die or drop the lowest die, append either 'k' for keep or 'd' for drop after the number of sides.
		/// "4d6k" will roll four six-sided dice and keep the single highest, while "4d6d" will roll the same but drop the single lowest.</para>
		/// <para>To be explicit about whether the highest or lowest is kept or dropped, you may optionally append either 'h' for highest or 'l' for lowest.
		/// "4d6kl" will keep the single lowest, while "4d6dh" will drop the single highest.</para>
		/// <para>Finally, you may optionally include a number to indicate how many to keep or drop, if you want to keep or drop more than just
		/// a single die as in the examples above.  "4d6k3" will keep the three highest dice, as will "4d6kh3".  "4d6kl3" will keep the lowest three.</para>
		/// <para>Whitespace is allowed between the number of sides and the keep or drop specifier.
		/// Unless noted otherwise, both lower case and upper case is allowed and interpreted identically.</para>
		/// </remarks>
		public static IDiceGenerator MakeDiceGenerator(this IRandom random, string notation)
		{
			var match = _diceNotationRegex.Match(notation);
			if (match.Success)
			{
				int quantity;
				int sides;
				int keepQuantity;
				bool keepHigh;

				if (match.Groups["quantity"].Success)
				{
					quantity = int.Parse(match.Groups["quantity"].Value);
				}
				else
				{
					quantity = 1;
				}

				sides = int.Parse(match.Groups["sides"].Value);

				if (match.Groups["keepDrop"].Success)
				{
					bool keep;
					if (match.Groups["keepDrop"].Value == "k" || match.Groups["keepDrop"].Value == "K")
					{
						keep = true;
					}
					else
					{
						keep = false;
					}

					if (match.Groups["keepDropWhat"].Success)
					{
						if (match.Groups["keepDropWhat"].Value == "h" || match.Groups["keepDropWhat"].Value == "H")
						{
							keepHigh = keep;
						}
						else
						{
							keepHigh = !keep;
						}
					}
					else
					{
						keepHigh = true;
					}

					if (match.Groups["keepDropQuantity"].Success)
					{
						keepQuantity = int.Parse(match.Groups["keepDropQuantity"].Value);
						if (!keep)
						{
							keepQuantity = quantity - keepQuantity;
						}
					}
					else
					{
						keepQuantity = 1;
						if (!keep)
						{
							keepQuantity = quantity - keepQuantity;
						}
					}
				}
				else
				{
					keepQuantity = quantity;
					keepHigh = false;
				}

				if (keepQuantity == quantity)
				{
					return new SimpleDiceGenerator(random, quantity, sides);
				}
				else
				{
					if (keepQuantity > quantity) throw new System.ArgumentException("You cannot keep more dice than you roll.", "notation");
					if (keepQuantity < 1) throw new System.ArgumentException("You must keep at least one die.", "notation");
					if (quantity == 1) throw new System.ArgumentException("You cannot roll only one die and discard it.", "notation");

					if (keepHigh)
					{
						return new KeepHighestDiceGenerator(random, quantity, sides, keepQuantity);
					}
					else
					{
						return new KeepLowestDiceGenerator(random, quantity, sides, keepQuantity);
					}
				}
			}
			else
			{
				throw new System.ArgumentException("The provided dice notation is invalid.", "notation");
			}
		}

		#endregion
	}
}
