/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.MakeIt.Random
{
	public static class RandomDice
	{
		#region Roll

		public static int RollDice(this IRandom random, int sides)
		{
			return random.HalfOpenRange(sides) + 1;
		}

		public static int[] RollDice(this IRandom random, int quantity, int sides)
		{
			var dice = new int[quantity];
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = random.HalfOpenRange(sides) + 1;
			}
			return dice;
		}

		public static void RollDice(this IRandom random, int quantity, int sides, int[] dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = random.HalfOpenRange(sides) + 1;
			}
		}

		public static void RollDice(this IRandom random, int quantity, int sides, List<int> dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			while (dice.Count < quantity)
			{
				dice.Add(random.HalfOpenRange(sides) + 1);
			}
		}

		#endregion

		#region SumRoll

		public static int SumRollDice(this IRandom random, int quantity, int sides)
		{
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				sum += random.HalfOpenRange(sides);
			}
			return sum + quantity;
		}

		public static int SumRollDice(this IRandom random, int quantity, int sides, out int[] dice)
		{
			dice = new int[quantity];
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = random.HalfOpenRange(sides) + 1;
				dice[i] = die;
				sum += die;
			}
			return sum;
		}

		public static int SumRollDice(this IRandom random, int quantity, int sides, int[] dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = random.HalfOpenRange(sides) + 1;
				dice[i] = die;
				sum += die;
			}
			return sum;
		}

		public static int SumRollDice(this IRandom random, int quantity, int sides, List<int> dice)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			int sum = 0;
			while (dice.Count < quantity)
			{
				int die = random.HalfOpenRange(sides) + 1;
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
					die = random.HalfOpenRange(sides) + 1;
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

					die = random.HalfOpenRange(sides) + 1;
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

					die = random.HalfOpenRange(sides) + 1;
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
					die = random.HalfOpenRange(sides) + 1;
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

					die = random.HalfOpenRange(sides) + 1;
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

					die = random.HalfOpenRange(sides) + 1;
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

		public static int[] RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDice(keepQuantity, sides);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice);
			return dice;
		}

		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice);
		}

		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice);
		}

		public static void RollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		public static int[] RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDice(keepQuantity, sides);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice);
			return dice;
		}

		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice);
		}

		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice);
		}

		public static void RollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDice(keepQuantity, sides, dice);
			random.RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, discardedDice);
		}

		public static int[] RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity);
		}

		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		public static void RollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		public static int[] RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity);
		}

		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		public static void RollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		#endregion

		#region SumRollKeep/Drop

		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDiceKeepHighest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, out int[] dice)
		{
			dice = random.RollDiceKeepHighest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, out int[] dice, out int[] discardedDice)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepHighest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepHighest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity)
		{
			int[] dice = random.RollDiceKeepLowest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, out int[] dice)
		{
			dice = random.RollDiceKeepLowest(quantity, sides, keepQuantity);
			return Sum(dice);
		}

		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, out int[] dice, out int[] discardedDice)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice);
			return Sum(dice);
		}

		public static int SumRollDiceKeepLowest(this IRandom random, int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice)
		{
			random.RollDiceKeepLowest(quantity, sides, keepQuantity, dice, discardedDice);
			return Sum(dice);
		}

		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity);
		}

		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, out int[] dice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, out dice);
		}

		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, out int[] dice, out int[] discardedDice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, out dice, out discardedDice);
		}

		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice);
		}

		public static int SumRollDiceDropHighest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return random.SumRollDiceKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity);
		}

		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, out int[] dice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, out dice);
		}

		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, out int[] dice, out int[] discardedDice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, out dice, out discardedDice);
		}

		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice);
		}

		public static int SumRollDiceDropLowest(this IRandom random, int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice)
		{
			return random.SumRollDiceKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice);
		}

		#endregion

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
								return (IRandom random) => random.RollDice(sides);
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
								return (IRandom random) => random.RollDice(sides) + add;
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
								return (IRandom random) => random.RollDice(sides) * mul;
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
								return (IRandom random) => random.RollDice(sides) * mul + add;
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
								return (IRandom random) => random.RollDice(sides) / div;
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
								return (IRandom random) => random.RollDice(sides) / div + add;
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

		public static int RollD4(this IRandom random)
		{
			return random.ClosedRange(1, 4);
		}

		public static int RollD6(this IRandom random)
		{
			return random.ClosedRange(1, 6);
		}

		public static int RollD8(this IRandom random)
		{
			return random.ClosedRange(1, 8);
		}

		public static int RollD10(this IRandom random)
		{
			return random.ClosedRange(1, 10);
		}

		public static int RollD12(this IRandom random)
		{
			return random.ClosedRange(1, 12);
		}

		public static int RollD20(this IRandom random)
		{
			return random.ClosedRange(1, 20);
		}
	}
}
