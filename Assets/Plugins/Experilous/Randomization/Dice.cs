/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Collections.Generic;

namespace Experilous.Randomization
{
	public static class Dice
	{
		#region Roll

		public static int Roll(int sides, IRandomEngine engine)
		{
			return (int)engine.NextLessThan((uint)sides) + 1;
		}

		public static int[] Roll(int quantity, int sides, IRandomEngine engine)
		{
			var dice = new int[quantity];
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = (int)engine.NextLessThan((uint)sides) + 1;
			}
			return dice;
		}

		public static void Roll(int quantity, int sides, int[] dice, IRandomEngine engine)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			for (int i = 0; i < quantity; ++i)
			{
				dice[i] = (int)engine.NextLessThan((uint)sides) + 1;
			}
		}

		public static void Roll(int quantity, int sides, List<int> dice, IRandomEngine engine)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			while (dice.Count < quantity)
			{
				dice.Add((int)engine.NextLessThan((uint)sides) + 1);
			}
		}

		#endregion

		#region SumRoll

		public static int SumRoll(int quantity, int sides, IRandomEngine engine)
		{
			uint sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				sum += engine.NextLessThan((uint)sides);
			}
			return (int)sum + quantity;
		}

		public static int SumRoll(int quantity, int sides, out int[] dice, IRandomEngine engine)
		{
			dice = new int[quantity];
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = (int)engine.NextLessThan((uint)sides) + 1;
				dice[i] = die;
				sum += die;
			}
			return sum;
		}

		public static int SumRoll(int quantity, int sides, int[] dice, IRandomEngine engine)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			if (dice.Length != quantity) throw new System.ArgumentException("The dice parameter must be the same length as the number of dice requested to be rolled.", "dice");
			int sum = 0;
			for (int i = 0; i < quantity; ++i)
			{
				int die = (int)engine.NextLessThan((uint)sides) + 1;
				dice[i] = die;
				sum += die;
			}
			return sum;
		}

		public static int SumRoll(int quantity, int sides, List<int> dice, IRandomEngine engine)
		{
			if (dice == null) throw new System.ArgumentNullException("dice");
			dice.Clear();
			int sum = 0;
			while (dice.Count < quantity)
			{
				int die = (int)engine.NextLessThan((uint)sides) + 1;
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

		private static void RollAdditionalKeepHighest(int additionalQuantity, int sides, IList<int> dice, IRandomEngine engine)
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
					die = (int)engine.NextLessThan((uint)sides) + 1;
					++i;
				} while (die <= min);

				dice[minIndex] = die;
			}
		}

		private static void RollAdditionalKeepHighest(int additionalQuantity, int sides, IList<int> dice, int[] discardedDice, IRandomEngine engine)
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

					die = (int)engine.NextLessThan((uint)sides) + 1;
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

		private static void RollAdditionalKeepHighest(int additionalQuantity, int sides, IList<int> dice, List<int> discardedDice, IRandomEngine engine)
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

					die = (int)engine.NextLessThan((uint)sides) + 1;
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

		private static void RollAdditionalKeepLowest(int additionalQuantity, int sides, IList<int> dice, IRandomEngine engine)
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
					die = (int)engine.NextLessThan((uint)sides) + 1;
					++i;
				} while (die >= max);

				dice[maxIndex] = die;
			}
		}

		private static void RollAdditionalKeepLowest(int additionalQuantity, int sides, IList<int> dice, int[] discardedDice, IRandomEngine engine)
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

					die = (int)engine.NextLessThan((uint)sides) + 1;
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

		private static void RollAdditionalKeepLowest(int additionalQuantity, int sides, IList<int> dice, List<int> discardedDice, IRandomEngine engine)
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

					die = (int)engine.NextLessThan((uint)sides) + 1;
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

		public static int[] RollKeepHighest(int quantity, int sides, int keepQuantity, IRandomEngine engine)
		{
			int[] dice = Roll(keepQuantity, sides, engine);
			RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, engine);
			return dice;
		}

		public static void RollKeepHighest(int quantity, int sides, int keepQuantity, int[] dice, IRandomEngine engine)
		{
			Roll(keepQuantity, sides, dice, engine);
			RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, engine);
		}

		public static void RollKeepHighest(int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice, IRandomEngine engine)
		{
			Roll(keepQuantity, sides, dice, engine);
			RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, discardedDice, engine);
		}

		public static void RollKeepHighest(int quantity, int sides, int keepQuantity, List<int> dice, IRandomEngine engine)
		{
			Roll(keepQuantity, sides, dice, engine);
			RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, engine);
		}

		public static void RollKeepHighest(int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice, IRandomEngine engine)
		{
			Roll(keepQuantity, sides, dice, engine);
			RollAdditionalKeepHighest(quantity - keepQuantity, sides, dice, discardedDice, engine);
		}

		public static int[] RollKeepLowest(int quantity, int sides, int keepQuantity, IRandomEngine engine)
		{
			int[] dice = Roll(keepQuantity, sides, engine);
			RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, engine);
			return dice;
		}

		public static void RollKeepLowest(int quantity, int sides, int keepQuantity, int[] dice, IRandomEngine engine)
		{
			Roll(keepQuantity, sides, dice, engine);
			RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, engine);
		}

		public static void RollKeepLowest(int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice, IRandomEngine engine)
		{
			Roll(keepQuantity, sides, dice, engine);
			RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, discardedDice, engine);
		}

		public static void RollKeepLowest(int quantity, int sides, int keepQuantity, List<int> dice, IRandomEngine engine)
		{
			Roll(keepQuantity, sides, dice, engine);
			RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, engine);
		}

		public static void RollKeepLowest(int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice, IRandomEngine engine)
		{
			Roll(keepQuantity, sides, dice, engine);
			RollAdditionalKeepLowest(quantity - keepQuantity, sides, dice, discardedDice, engine);
		}

		public static int[] RollDropHighest(int quantity, int sides, int dropQuantity, IRandomEngine engine)
		{
			return RollKeepLowest(quantity, sides, quantity - dropQuantity, engine);
		}

		public static void RollDropHighest(int quantity, int sides, int dropQuantity, int[] dice, IRandomEngine engine)
		{
			RollKeepLowest(quantity, sides, quantity - dropQuantity, dice, engine);
		}

		public static void RollDropHighest(int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice, IRandomEngine engine)
		{
			RollKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice, engine);
		}

		public static void RollDropHighest(int quantity, int sides, int dropQuantity, List<int> dice, IRandomEngine engine)
		{
			RollKeepLowest(quantity, sides, quantity - dropQuantity, dice, engine);
		}

		public static void RollDropHighest(int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice, IRandomEngine engine)
		{
			RollKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice, engine);
		}

		public static int[] RollDropLowest(int quantity, int sides, int dropQuantity, IRandomEngine engine)
		{
			return RollKeepHighest(quantity, sides, quantity - dropQuantity, engine);
		}

		public static void RollDropLowest(int quantity, int sides, int dropQuantity, int[] dice, IRandomEngine engine)
		{
			RollKeepHighest(quantity, sides, quantity - dropQuantity, dice, engine);
		}

		public static void RollDropLowest(int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice, IRandomEngine engine)
		{
			RollKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice, engine);
		}

		public static void RollDropLowest(int quantity, int sides, int dropQuantity, List<int> dice, IRandomEngine engine)
		{
			RollKeepHighest(quantity, sides, quantity - dropQuantity, dice, engine);
		}

		public static void RollDropLowest(int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice, IRandomEngine engine)
		{
			RollKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice, engine);
		}

		#endregion

		#region SumRollKeep/Drop

		public static int SumRollKeepHighest(int quantity, int sides, int keepQuantity, IRandomEngine engine)
		{
			int[] dice = RollKeepHighest(quantity, sides, keepQuantity, engine);
			return Sum(dice);
		}

		public static int SumRollKeepHighest(int quantity, int sides, int keepQuantity, out int[] dice, IRandomEngine engine)
		{
			dice = RollKeepHighest(quantity, sides, keepQuantity, engine);
			return Sum(dice);
		}

		public static int SumRollKeepHighest(int quantity, int sides, int keepQuantity, out int[] dice, out int[] discardedDice, IRandomEngine engine)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			RollKeepHighest(quantity, sides, keepQuantity, dice, discardedDice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepHighest(int quantity, int sides, int keepQuantity, int[] dice, IRandomEngine engine)
		{
			RollKeepHighest(quantity, sides, keepQuantity, dice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepHighest(int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice, IRandomEngine engine)
		{
			RollKeepHighest(quantity, sides, keepQuantity, dice, discardedDice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepHighest(int quantity, int sides, int keepQuantity, List<int> dice, IRandomEngine engine)
		{
			RollKeepHighest(quantity, sides, keepQuantity, dice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepHighest(int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice, IRandomEngine engine)
		{
			RollKeepHighest(quantity, sides, keepQuantity, dice, discardedDice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepLowest(int quantity, int sides, int keepQuantity, IRandomEngine engine)
		{
			int[] dice = RollKeepLowest(quantity, sides, keepQuantity, engine);
			return Sum(dice);
		}

		public static int SumRollKeepLowest(int quantity, int sides, int keepQuantity, out int[] dice, IRandomEngine engine)
		{
			dice = RollKeepLowest(quantity, sides, keepQuantity, engine);
			return Sum(dice);
		}

		public static int SumRollKeepLowest(int quantity, int sides, int keepQuantity, out int[] dice, out int[] discardedDice, IRandomEngine engine)
		{
			dice = new int[keepQuantity];
			discardedDice = new int[quantity - keepQuantity];
			RollKeepLowest(quantity, sides, keepQuantity, dice, discardedDice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepLowest(int quantity, int sides, int keepQuantity, int[] dice, IRandomEngine engine)
		{
			RollKeepLowest(quantity, sides, keepQuantity, dice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepLowest(int quantity, int sides, int keepQuantity, int[] dice, int[] discardedDice, IRandomEngine engine)
		{
			RollKeepLowest(quantity, sides, keepQuantity, dice, discardedDice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepLowest(int quantity, int sides, int keepQuantity, List<int> dice, IRandomEngine engine)
		{
			RollKeepLowest(quantity, sides, keepQuantity, dice, engine);
			return Sum(dice);
		}

		public static int SumRollKeepLowest(int quantity, int sides, int keepQuantity, List<int> dice, List<int> discardedDice, IRandomEngine engine)
		{
			RollKeepLowest(quantity, sides, keepQuantity, dice, discardedDice, engine);
			return Sum(dice);
		}

		public static int SumRollDropHighest(int quantity, int sides, int dropQuantity, IRandomEngine engine)
		{
			return SumRollKeepLowest(quantity, sides, quantity - dropQuantity, engine);
		}

		public static int SumRollDropHighest(int quantity, int sides, int dropQuantity, out int[] dice, IRandomEngine engine)
		{
			return SumRollKeepLowest(quantity, sides, quantity - dropQuantity, out dice, engine);
		}

		public static int SumRollDropHighest(int quantity, int sides, int dropQuantity, out int[] dice, out int[] discardedDice, IRandomEngine engine)
		{
			return SumRollKeepLowest(quantity, sides, quantity - dropQuantity, out dice, out discardedDice, engine);
		}

		public static int SumRollDropHighest(int quantity, int sides, int dropQuantity, int[] dice, IRandomEngine engine)
		{
			return SumRollKeepLowest(quantity, sides, quantity - dropQuantity, dice, engine);
		}

		public static int SumRollDropHighest(int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice, IRandomEngine engine)
		{
			return SumRollKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice, engine);
		}

		public static int SumRollDropHighest(int quantity, int sides, int dropQuantity, List<int> dice, IRandomEngine engine)
		{
			return SumRollKeepLowest(quantity, sides, quantity - dropQuantity, dice, engine);
		}

		public static int SumRollDropHighest(int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice, IRandomEngine engine)
		{
			return SumRollKeepLowest(quantity, sides, quantity - dropQuantity, dice, discardedDice, engine);
		}

		public static int SumRollDropLowest(int quantity, int sides, int dropQuantity, IRandomEngine engine)
		{
			return SumRollKeepHighest(quantity, sides, quantity - dropQuantity, engine);
		}

		public static int SumRollDropLowest(int quantity, int sides, int dropQuantity, out int[] dice, IRandomEngine engine)
		{
			return SumRollKeepHighest(quantity, sides, quantity - dropQuantity, out dice, engine);
		}

		public static int SumRollDropLowest(int quantity, int sides, int dropQuantity, out int[] dice, out int[] discardedDice, IRandomEngine engine)
		{
			return SumRollKeepHighest(quantity, sides, quantity - dropQuantity, out dice, out discardedDice, engine);
		}

		public static int SumRollDropLowest(int quantity, int sides, int dropQuantity, int[] dice, IRandomEngine engine)
		{
			return SumRollKeepHighest(quantity, sides, quantity - dropQuantity, dice, engine);
		}

		public static int SumRollDropLowest(int quantity, int sides, int dropQuantity, int[] dice, int[] discardedDice, IRandomEngine engine)
		{
			return SumRollKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice, engine);
		}

		public static int SumRollDropLowest(int quantity, int sides, int dropQuantity, List<int> dice, IRandomEngine engine)
		{
			return SumRollKeepHighest(quantity, sides, quantity - dropQuantity, dice, engine);
		}

		public static int SumRollDropLowest(int quantity, int sides, int dropQuantity, List<int> dice, List<int> discardedDice, IRandomEngine engine)
		{
			return SumRollKeepHighest(quantity, sides, quantity - dropQuantity, dice, discardedDice, engine);
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

		public static int SumRoll(string dNotation, IRandomEngine engine)
		{
			return Prepare(dNotation)(engine);
		}

		public delegate int DiceDelegate(IRandomEngine engine);

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
								return (IRandomEngine engine) => Roll(sides, engine);
							}
							else
							{
								return (IRandomEngine engine) => SumRoll(quantity, sides, engine);
							}
						}
						else
						{
							if (quantity == 1)
							{
								return (IRandomEngine engine) => Roll(sides, engine) + add;
							}
							else
							{
								return (IRandomEngine engine) => SumRoll(quantity, sides, engine) + add;
							}
						}
					}
					else if (mul != 1)
					{
						if (add == 0)
						{
							if (quantity == 1)
							{
								return (IRandomEngine engine) => Roll(sides, engine) * mul;
							}
							else
							{
								return (IRandomEngine engine) => SumRoll(quantity, sides, engine) * mul;
							}
						}
						else
						{
							if (quantity == 1)
							{
								return (IRandomEngine engine) => Roll(sides, engine) * mul + add;
							}
							else
							{
								return (IRandomEngine engine) => SumRoll(quantity, sides, engine) * mul + add;
							}
						}
					}
					else
					{
						if (add == 0)
						{
							if (quantity == 1)
							{
								return (IRandomEngine engine) => Roll(sides, engine) / div;
							}
							else
							{
								return (IRandomEngine engine) => SumRoll(quantity, sides, engine) / div;
							}
						}
						else
						{
							if (quantity == 1)
							{
								return (IRandomEngine engine) => Roll(sides, engine) / div + add;
							}
							else
							{
								return (IRandomEngine engine) => SumRoll(quantity, sides, engine) / div + add;
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
								return (IRandomEngine engine) => SumRollKeepHighest(quantity, sides, keepQuantity, engine);
							}
							else
							{
								return (IRandomEngine engine) => SumRollKeepLowest(quantity, sides, keepQuantity, engine);
							}
						}
						else
						{
							if (keepHigh)
							{
								return (IRandomEngine engine) => SumRollKeepHighest(quantity, sides, keepQuantity, engine) + add;
							}
							else
							{
								return (IRandomEngine engine) => SumRollKeepLowest(quantity, sides, keepQuantity, engine) + add;
							}
						}
					}
					else if (mul != 1)
					{
						if (add == 0)
						{
							if (keepHigh)
							{
								return (IRandomEngine engine) => SumRollKeepHighest(quantity, sides, keepQuantity, engine) * mul;
							}
							else
							{
								return (IRandomEngine engine) => SumRollKeepLowest(quantity, sides, keepQuantity, engine) * mul;
							}
						}
						else
						{
							if (keepHigh)
							{
								return (IRandomEngine engine) => SumRollKeepHighest(quantity, sides, keepQuantity, engine) * mul + add;
							}
							else
							{
								return (IRandomEngine engine) => SumRollKeepLowest(quantity, sides, keepQuantity, engine) * mul + add;
							}
						}
					}
					else
					{
						if (add == 0)
						{
							if (keepHigh)
							{
								return (IRandomEngine engine) => SumRollKeepHighest(quantity, sides, keepQuantity, engine) / div;
							}
							else
							{
								return (IRandomEngine engine) => SumRollKeepLowest(quantity, sides, keepQuantity, engine) / div;
							}
						}
						else
						{
							if (keepHigh)
							{
								return (IRandomEngine engine) => SumRollKeepHighest(quantity, sides, keepQuantity, engine) / div + add;
							}
							else
							{
								return (IRandomEngine engine) => SumRollKeepLowest(quantity, sides, keepQuantity, engine) / div + add;
							}
						}
					}
					#endregion
				}
			}

			throw new System.ArgumentException();
		}

		public static int D4(IRandomEngine engine)
		{
			return (int)engine.NextLessThan(4) + 1;
		}

		public static int D6(IRandomEngine engine)
		{
			return (int)engine.NextLessThan(6) + 1;
		}

		public static int D8(IRandomEngine engine)
		{
			return (int)engine.NextLessThan(8) + 1;
		}

		public static int D10(IRandomEngine engine)
		{
			return (int)engine.NextLessThan(10) + 1;
		}

		public static int D12(IRandomEngine engine)
		{
			return (int)engine.NextLessThan(12) + 1;
		}

		public static int D20(IRandomEngine engine)
		{
			return (int)engine.NextLessThan(20) + 1;
		}
	}
}
