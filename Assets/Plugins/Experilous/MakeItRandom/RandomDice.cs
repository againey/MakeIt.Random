/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for simulating random dice rolls.
	/// </summary>
	public static class RandomDice
	{
		#region Die Generator

		/// <summary>
		/// Prepares an efficient range generator which will generate dice rolls, one at a time, with values greater than or equal to 1 and less than or equal to <paramref name="sides"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generated values of the returned generator are derived.</param>
		/// <param name="sides">The number of sides of the die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <returns></returns>
		public static IIntGenerator MakeDiceGenerator(this IRandom random, int sides)
		{
			return random.MakeRangeOCGenerator(sides);
		}

		#endregion

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
		/// Generates a random die roll, simulating a die with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <returns>The numeric value of the simulated die roll, in the range [1, <paramref name="sides"/>].</returns>
		public static int RollDie(this IIntGenerator diceGenerator)
		{
			return diceGenerator.Next();
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <returns>An array of the numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		public static int[] RollDice(this IIntGenerator diceGenerator, int quantity)
		{
			var dice = new int[quantity];
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = diceGenerator.Next();
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A pre-allocated array into which the numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDice(this IIntGenerator diceGenerator, int quantity, int[] dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = diceGenerator.Next();
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A list into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.  After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>, and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDice(this IIntGenerator diceGenerator, int quantity, List<int> dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			while (dice.Count < quantity)
			{
				dice.Add(diceGenerator.Next());
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		public static int SumRollDice(this IIntGenerator diceGenerator, int quantity)
		{
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				sum += diceGenerator.Next();
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A an output array into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDice(this IIntGenerator diceGenerator, int quantity, out int[] dice)
		{
			dice = new int[quantity];
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = diceGenerator.Next();
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A pre-allocated array into which the numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDice(this IIntGenerator diceGenerator, int quantity, int[] dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = diceGenerator.Next();
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dice">A list into which the numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.  After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/>, and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDice(this IIntGenerator diceGenerator, int quantity, List<int> dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			int sum = 0;
			while (dice.Count < quantity)
			{
				int die = diceGenerator.Next();
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

		private static void RollAdditionalKeepHighest(this IIntGenerator diceGenerator, int additionalQuantity, IList<int> dice)
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
					die = diceGenerator.Next();
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

		private static void RollAdditionalKeepHighest(this IIntGenerator diceGenerator, int additionalQuantity, IList<int> dice, int[] discardedDice)
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

					die = diceGenerator.Next();
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

		private static void RollAdditionalKeepHighest(this IIntGenerator diceGenerator, int additionalQuantity, IList<int> dice, List<int> discardedDice)
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

					die = diceGenerator.Next();
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

		private static void RollAdditionalKeepLowest(this IIntGenerator diceGenerator, int additionalQuantity, IList<int> dice)
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
					die = diceGenerator.Next();
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

		private static void RollAdditionalKeepLowest(this IIntGenerator diceGenerator, int additionalQuantity, IList<int> dice, int[] discardedDice)
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

					die = diceGenerator.Next();
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

		private static void RollAdditionalKeepLowest(this IIntGenerator diceGenerator, int additionalQuantity, IList<int> dice, List<int> discardedDice)
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

					die = diceGenerator.Next();
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
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the highest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		public static int[] RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDice(keepQuantity, sides);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice);
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the highest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		public static int[] RollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity)
		{
			int[] dice = diceGenerator.RollDice(keepQuantity);
			diceGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice);
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, int[] dice)
		{
			diceGenerator.RollDice(keepQuantity, dice);
			diceGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, int[] dice, int[] discardedDice)
		{
			diceGenerator.RollDice(keepQuantity, dice);
			diceGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, List<int> dice)
		{
			diceGenerator.RollDice(keepQuantity, dice);
			diceGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			diceGenerator.RollDice(keepQuantity, dice);
			diceGenerator.RollAdditionalKeepHighest(quantity - keepQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the lowest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		public static int[] RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDice(keepQuantity, sides);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice);
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the lowest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		public static int[] RollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity)
		{
			int[] dice = diceGenerator.RollDice(keepQuantity);
			diceGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice);
			return dice;
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, int[] dice)
		{
			diceGenerator.RollDice(keepQuantity, dice);
			diceGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, int[] dice, int[] discardedDice)
		{
			diceGenerator.RollDice(keepQuantity, dice);
			diceGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, List<int> dice)
		{
			diceGenerator.RollDice(keepQuantity, dice);
			diceGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static void RollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			diceGenerator.RollDice(keepQuantity, dice);
			diceGenerator.RollAdditionalKeepLowest(quantity - keepQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the lowest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int)"/>.</note></remarks>
		public static int[] RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the lowest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int)"/>.</note></remarks>
		public static int[] RollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity)
		{
			return diceGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static void RollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, int[] dice)
		{
			diceGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static void RollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, int[] dice, int[] discardedDice)
		{
			diceGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, List`1{int})"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, List`1{int})"/>.</note></remarks>
		public static void RollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, List<int> dice)
		{
			diceGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, List`1{int}, List`1{int})"/>.</note></remarks>
		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepLowest(IRandom, int, int, int, List`1{int}, List`1{int})"/>.</note></remarks>
		public static void RollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			diceGenerator.RollDiceKeepLowest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the highest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int)"/>.</note></remarks>
		public static int[] RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>An array of the highest numeric values of the simulated dice rolls, each element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int)"/>.</note></remarks>
		public static int[] RollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity)
		{
			return diceGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static void RollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, int[] dice)
		{
			diceGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static void RollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, int[] dice, int[] discardedDice)
		{
			diceGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, List`1{int})"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, List`1{int})"/>.</note></remarks>
		public static void RollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, List<int> dice)
		{
			diceGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, List`1{int}, List`1{int})"/>.</note></remarks>
		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="RollDiceKeepHighest(IRandom, int, int, int, List`1{int}, List`1{int})"/>.</note></remarks>
		public static void RollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			diceGenerator.RollDiceKeepHighest(quantity, quantity - dropQuantity, dice, discardedDice);
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
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDiceKeepHighest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		public static int SumRollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity)
		{
			int[] dice = diceGenerator.RollDiceKeepHighest(quantity, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, out int[] dice)
		{
			dice = diceGenerator.RollDiceKeepHighest(quantity, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, out int[] dice, out int[] discardedDice)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			diceGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, int[] dice)
		{
			diceGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, int[] dice, int[] discardedDice)
		{
			diceGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, List<int> dice)
		{
			diceGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the highest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepHighest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			diceGenerator.RollDiceKeepHighest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDiceKeepLowest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		public static int SumRollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity)
		{
			int[] dice = diceGenerator.RollDiceKeepLowest(quantity, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, out int[] dice)
		{
			dice = diceGenerator.RollDiceKeepLowest(quantity, keepQuantity);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, out int[] dice, out int[] discardedDice)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			diceGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, int[] dice)
		{
			diceGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="keepQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="keepQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, int[] dice, int[] discardedDice)
		{
			diceGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, List<int> dice)
		{
			diceGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// keeping only the lowest <paramref name="keepQuantity"/> dice and discarding the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="keepQuantity">The number of dice to keep.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="keepQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="quantity"/> - <paramref name="keepQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</remarks>
		public static int SumRollDiceKeepLowest(this IIntGenerator diceGenerator, int quantity, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			diceGenerator.RollDiceKeepLowest(quantity, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int)"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int)"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity)
		{
			return diceGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, out int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, out int[] dice)
		{
			return diceGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, out dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, out int[], out int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, out int[] dice, out int[] discardedDice)
		{
			return diceGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, out dice, out discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, int[] dice)
		{
			return diceGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the lowest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the highest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, int[] dice, int[] discardedDice)
		{
			return diceGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, List`1{int})"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, List`1{int})"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, List<int> dice)
		{
			return diceGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, List`1{int}, List`1{int})"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the lowest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the lowest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the highest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the lowest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepLowest(IRandom, int, int, int, List`1{int}, List`1{int})"/>.</note></remarks>
		public static int SumRollDiceDropHighest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return diceGenerator.SumRollDiceKeepLowest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int)"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int)"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity)
		{
			return diceGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A an output array into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, out int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, out int[] dice)
		{
			return diceGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, out dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, out int[], out int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, out int[] dice, out int[] discardedDice)
		{
			return diceGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, out dice, out discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, int[] dice)
		{
			return diceGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
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
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A pre-allocated array into which the highest numeric values of the simulated dice rolls will be stored.  Its length must be equal to <paramref name="quantity"/> - <paramref name="dropQuantity"/>.</param>
		/// <param name="discardedDice">A pre-allocated array into which the lowest numeric values of the discarded simulated dice rolls will be stored.  Its length must be equal to <paramref name="dropQuantity"/>.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>After the function is complete, each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, int[], int[])"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, int[] dice, int[] discardedDice)
		{
			return diceGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, List`1{int})"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, List`1{int})"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, List<int> dice)
		{
			return diceGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, dice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="sides">The number of sides of each die to roll.  Must be positive, but does not need to correspond to physical die shape.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, List`1{int}, List`1{int})"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		/// <summary>
		/// Generates random dice rolls, simulating the specified <paramref name="quantity"/> of dice each with the specified number of <paramref name="sides"/>,
		/// dropping the highest <paramref name="dropQuantity"/> dice and keeping the rest.
		/// </summary>
		/// <param name="diceGenerator">The prepared range generator that will be used to simulate dice rolls using dice with an already specified number of sides.</param>
		/// <param name="quantity">The number of dice to roll.  Must be positive.</param>
		/// <param name="dropQuantity">The number of dice to drop.  Must be non-negative and less than or equal to <paramref name="quantity"/>.</param>
		/// <param name="dice">A list into which the highest numeric values of the simulated dice rolls will be stored.</param>
		/// <param name="discardedDice">A list into which the lowest numeric values of the discarded simulated dice rolls will be stored.</param>
		/// <returns>The sum of all the highest numeric values of the simulated dice rolls, with each summed element being in the range [1, <paramref name="sides"/>].</returns>
		/// <remarks><para>All existing elements in <paramref name="dice"/> and <paramref name="discardedDice"/> will be removed by this function.
		/// After the function is complete, the length of <paramref name="dice"/> will be <paramref name="quantity"/> - <paramref name="dropQuantity"/>,
		/// the length of <paramref name="discardedDice"/> will be <paramref name="dropQuantity"/>,
		/// and each element of <paramref name="dice"/> and <paramref name="discardedDice"/> will be in the range [1, <paramref name="sides"/>].</para>
		/// <note type="note">This function is essentially identical to <see cref="SumRollDiceKeepHighest(IRandom, int, int, int, List`1{int}, List`1{int})"/>.</note></remarks>
		public static int SumRollDiceDropLowest(this IIntGenerator diceGenerator, int quantity, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return diceGenerator.SumRollDiceKeepHighest(quantity, quantity - dropQuantity, dice, discardedDice);
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

		#region String Notation

		/// <summary>
		/// Regex for parsing dice notation
		/// </summary>
		/// <remarks>
		/// Examples:
		/// d6 (roll one 6-sided die)
		/// 3d4 (roll three 4-sided dice)
		/// 1d10+2 (roll one 10-sided die, add 2)
		/// 4d20-4 (roll four 20-sided dice, subtract 4)
		/// d6*2 (roll one 6-sided die, multiply by 2)
		/// 2d8 x3 - 4 (roll two 8-sided dice, multiply by 3, subtract 4)
		/// 100d10 / 10 (roll one hundred 10-sided dice, divide by 10)
		/// 5d10/5+2 (roll five 10-sided dice, divide by 5, add 2)
		/// 4d12kH (roll four 12-sided dice, keep 1 highest)
		/// 5d8k2h (roll five 8-sided dice, keep 2 highest)
		/// 16d4-6L-10 (roll sixteen 4-sided dice, drop 6 lowest, subtract 10)
		/// 4d4d3lx2-1 (roll four 4-sided dice, drop 3 lowest, multiply by 2, subtract 1)
		/// </remarks>
		private static System.Text.RegularExpressions.Regex _diceNotationRegex = new System.Text.RegularExpressions.Regex(
			@"\A(?<quantity>[1-9][0-9]*)?(?:d|D)(?<sides>[1-9][0-9]*)(?:\s*(?<keepDrop>k|K|d|D|\-)(?<keepDropQuantity>[1-9][0-9]*)?(?<keepDropWhat>h|H|l|L))?(?:\s*(?<mulDiv>\*|x|/)\s*(?<mulDivAmount>[1-9][0-9]*))?(?:\s*(?<addSub>\+|\-)\s*(?<addSubAmount>[1-9][0-9]*))?\z");

		public static int SumRoll(this IRandom random, string dNotation)
		{
			return Prepare(dNotation)(random);
		}

		public delegate int DiceDelegate(IRandom random);

		public static DiceDelegate Prepare(string dNotation)
		{
			var match = _diceNotationRegex.Match(dNotation);
			if (match.Success)
			{
				int quantity;
				int sides;
				int keepQuantity;
				bool keepHigh;
				int mul;
				int div;
				int add;

				#region Consume Regex Results

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
					if (match.Groups["keepDropWhat"].Value == "l" || match.Groups["keepDropWhat"].Value == "L")
					{
						keepHigh = false;
					}
					else
					{
						keepHigh = true;
					}

					if (match.Groups["keepDrop"].Value == "k" || match.Groups["keepDrop"].Value == "K")
					{
						if (match.Groups["keepDropQuantity"].Success)
						{
							keepQuantity = int.Parse(match.Groups["keepDropQuantity"].Value);
						}
						else
						{
							keepQuantity = 1;
						}
					}
					else
					{
						keepHigh = !keepHigh;

						if (match.Groups["keepDropQuantity"].Success)
						{
							keepQuantity = quantity - int.Parse(match.Groups["keepDropQuantity"].Value);
						}
						else
						{
							keepQuantity = quantity - 1;
						}
					}
				}
				else
				{
					keepQuantity = 0;
					keepHigh = false;
				}

				if (match.Groups["mulDiv"].Success)
				{
					if (match.Groups["mulDiv"].Value == "/")
					{
						div = int.Parse(match.Groups["mulDivAmount"].Value);
						mul = 1;
					}
					else
					{
						mul = int.Parse(match.Groups["mulDivAmount"].Value);
						div = 1;
					}
				}
				else
				{
					mul = 1;
					div = 1;
				}

				if (match.Groups["addSub"].Success)
				{
					if (match.Groups["addSub"].Value == "+")
					{
						add = int.Parse(match.Groups["addSubAmount"].Value);
					}
					else
					{
						add = -int.Parse(match.Groups["addSubAmount"].Value);
					}
				}
				else
				{
					add = 0;
				}

				#endregion

				if (keepQuantity == 0)
				{
					#region No Keep or Drop
					if (mul == 1 && div == 1)
					{
						if (add == 0)
						{
							if (quantity == 1)
							{
								return (IRandom random) => random.RollDie(sides);
							}
							else
							{
								return (IRandom random) => random.SumRollDice(quantity, sides);
							}
						}
						else
						{
							if (quantity == 1)
							{
								return (IRandom random) => random.RollDie(sides) + add;
							}
							else
							{
								return (IRandom random) => random.SumRollDice(quantity, sides) + add;
							}
						}
					}
					else if (mul != 1)
					{
						if (add == 0)
						{
							if (quantity == 1)
							{
								return (IRandom random) => random.RollDie(sides) * mul;
							}
							else
							{
								return (IRandom random) => random.SumRollDice(quantity, sides) * mul;
							}
						}
						else
						{
							if (quantity == 1)
							{
								return (IRandom random) => random.RollDie(sides) * mul + add;
							}
							else
							{
								return (IRandom random) => random.SumRollDice(quantity, sides) * mul + add;
							}
						}
					}
					else
					{
						if (add == 0)
						{
							if (quantity == 1)
							{
								return (IRandom random) => random.RollDie(sides) / div;
							}
							else
							{
								return (IRandom random) => random.SumRollDice(quantity, sides) / div;
							}
						}
						else
						{
							if (quantity == 1)
							{
								return (IRandom random) => random.RollDie(sides) / div + add;
							}
							else
							{
								return (IRandom random) => random.SumRollDice(quantity, sides) / div + add;
							}
						}
					}
					#endregion
				}
				else
				{
					#region Keep or Drop
					if (quantity == 1 || keepQuantity >= quantity)
					{
						throw new System.ArgumentException();
					}

					if (mul == 1 && div == 1)
					{
						if (add == 0)
						{
							if (keepHigh)
							{
								return (IRandom random) => random.SumRollDiceKeepHighest(quantity, sides, keepQuantity);
							}
							else
							{
								return (IRandom random) => random.SumRollDiceKeepLowest(quantity, sides, keepQuantity);
							}
						}
						else
						{
							if (keepHigh)
							{
								return (IRandom random) => random.SumRollDiceKeepHighest(quantity, sides, keepQuantity) + add;
							}
							else
							{
								return (IRandom random) => random.SumRollDiceKeepLowest(quantity, sides, keepQuantity) + add;
							}
						}
					}
					else if (mul != 1)
					{
						if (add == 0)
						{
							if (keepHigh)
							{
								return (IRandom random) => random.SumRollDiceKeepHighest(quantity, sides, keepQuantity) * mul;
							}
							else
							{
								return (IRandom random) => random.SumRollDiceKeepLowest(quantity, sides, keepQuantity) * mul;
							}
						}
						else
						{
							if (keepHigh)
							{
								return (IRandom random) => random.SumRollDiceKeepHighest(quantity, sides, keepQuantity) * mul + add;
							}
							else
							{
								return (IRandom random) => random.SumRollDiceKeepLowest(quantity, sides, keepQuantity) * mul + add;
							}
						}
					}
					else
					{
						if (add == 0)
						{
							if (keepHigh)
							{
								return (IRandom random) => random.SumRollDiceKeepHighest(quantity, sides, keepQuantity) / div;
							}
							else
							{
								return (IRandom random) => random.SumRollDiceKeepLowest(quantity, sides, keepQuantity) / div;
							}
						}
						else
						{
							if (keepHigh)
							{
								return (IRandom random) => random.SumRollDiceKeepHighest(quantity, sides, keepQuantity) / div + add;
							}
							else
							{
								return (IRandom random) => random.SumRollDiceKeepLowest(quantity, sides, keepQuantity) / div + add;
							}
						}
					}
					#endregion
				}
			}

			throw new System.ArgumentException();
		}

		#endregion
	}
}
