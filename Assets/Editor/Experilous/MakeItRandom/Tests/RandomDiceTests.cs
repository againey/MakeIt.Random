/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;
using NSubstitute;

namespace Experilous.MakeItRandom.Tests
{
	class RandomDiceTests
	{
#if !MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
		[TestCase(Category = "Normal")]
		public void TwoSidedDie()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U);
			Assert.AreEqual(1, random.RollDie(2));
			random.Received().Next32();
			Assert.AreEqual(2, random.RollDie(2));
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void ThreeSidedDie()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 2U);
			Assert.AreEqual(1, random.RollDie(3));
			random.Received().Next32();
			Assert.AreEqual(2, random.RollDie(3));
			random.Received().Next32();
			Assert.AreEqual(3, random.RollDie(3));
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void SixSidedDie()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, random.RollDie(6));
			random.Received().Next32();
			Assert.AreEqual(2, random.RollDie(6));
			random.Received().Next32();
			Assert.AreEqual(3, random.RollDie(6));
			random.Received().Next32();
			Assert.AreEqual(4, random.RollDie(6));
			random.Received().Next32();
			Assert.AreEqual(5, random.RollDie(6));
			random.Received().Next32();
			Assert.AreEqual(6, random.RollDie(6));
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void TwentySidedDie()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 9U, 10U, 18U, 19U);
			Assert.AreEqual(1, random.RollDie(20));
			random.Received().Next32();
			Assert.AreEqual(2, random.RollDie(20));
			random.Received().Next32();
			Assert.AreEqual(10, random.RollDie(20));
			random.Received().Next32();
			Assert.AreEqual(11, random.RollDie(20));
			random.Received().Next32();
			Assert.AreEqual(19, random.RollDie(20));
			random.Received().Next32();
			Assert.AreEqual(20, random.RollDie(20));
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void D4()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 2U, 3U);
			Assert.AreEqual(1, random.RollD4());
			random.Received().Next32();
			Assert.AreEqual(2, random.RollD4());
			random.Received().Next32();
			Assert.AreEqual(3, random.RollD4());
			random.Received().Next32();
			Assert.AreEqual(4, random.RollD4());
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void D6()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, random.RollD6());
			random.Received().Next32();
			Assert.AreEqual(2, random.RollD6());
			random.Received().Next32();
			Assert.AreEqual(3, random.RollD6());
			random.Received().Next32();
			Assert.AreEqual(4, random.RollD6());
			random.Received().Next32();
			Assert.AreEqual(5, random.RollD6());
			random.Received().Next32();
			Assert.AreEqual(6, random.RollD6());
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void D8()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 3U, 4U, 6U, 7U);
			Assert.AreEqual(1, random.RollD8());
			random.Received().Next32();
			Assert.AreEqual(2, random.RollD8());
			random.Received().Next32();
			Assert.AreEqual(4, random.RollD8());
			random.Received().Next32();
			Assert.AreEqual(5, random.RollD8());
			random.Received().Next32();
			Assert.AreEqual(7, random.RollD8());
			random.Received().Next32();
			Assert.AreEqual(8, random.RollD8());
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void D10()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 4U, 5U, 8U, 9U);
			Assert.AreEqual(1, random.RollD10());
			random.Received().Next32();
			Assert.AreEqual(2, random.RollD10());
			random.Received().Next32();
			Assert.AreEqual(5, random.RollD10());
			random.Received().Next32();
			Assert.AreEqual(6, random.RollD10());
			random.Received().Next32();
			Assert.AreEqual(9, random.RollD10());
			random.Received().Next32();
			Assert.AreEqual(10, random.RollD10());
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void D12()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 5U, 6U, 10U, 11U);
			Assert.AreEqual(1, random.RollD12());
			random.Received().Next32();
			Assert.AreEqual(2, random.RollD12());
			random.Received().Next32();
			Assert.AreEqual(6, random.RollD12());
			random.Received().Next32();
			Assert.AreEqual(7, random.RollD12());
			random.Received().Next32();
			Assert.AreEqual(11, random.RollD12());
			random.Received().Next32();
			Assert.AreEqual(12, random.RollD12());
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void D20()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 9U, 10U, 18U, 19U);
			Assert.AreEqual(1, random.RollD20());
			random.Received().Next32();
			Assert.AreEqual(2, random.RollD20());
			random.Received().Next32();
			Assert.AreEqual(10, random.RollD20());
			random.Received().Next32();
			Assert.AreEqual(11, random.RollD20());
			random.Received().Next32();
			Assert.AreEqual(19, random.RollD20());
			random.Received().Next32();
			Assert.AreEqual(20, random.RollD20());
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void TwoTwoSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, random.SumRollDice(2, 2));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 1U);
			Assert.AreEqual(3, random.SumRollDice(2, 2));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 0U);
			Assert.AreEqual(3, random.SumRollDice(2, 2));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U);
			Assert.AreEqual(4, random.SumRollDice(2, 2));
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void TwoThreeSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 1U);
			Assert.AreEqual(3, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 2U);
			Assert.AreEqual(4, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 0U);
			Assert.AreEqual(3, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U);
			Assert.AreEqual(4, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 2U);
			Assert.AreEqual(5, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 0U);
			Assert.AreEqual(4, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 1U);
			Assert.AreEqual(5, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 2U);
			Assert.AreEqual(6, random.SumRollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void TwoSixSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 2U);
			Assert.AreEqual(4, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 5U);
			Assert.AreEqual(7, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 0U);
			Assert.AreEqual(4, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 3U);
			Assert.AreEqual(7, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 5U);
			Assert.AreEqual(9, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 0U);
			Assert.AreEqual(7, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 2U);
			Assert.AreEqual(9, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U);
			Assert.AreEqual(12, random.SumRollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void TwoTwentySidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 4U);
			Assert.AreEqual(6, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 19U);
			Assert.AreEqual(21, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(11U, 0U);
			Assert.AreEqual(13, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(11U, 7U);
			Assert.AreEqual(20, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(11U, 19U);
			Assert.AreEqual(32, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(19U, 0U);
			Assert.AreEqual(21, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(19U, 12U);
			Assert.AreEqual(33, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(19U, 19U);
			Assert.AreEqual(40, random.SumRollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveTwoSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(5, random.SumRollDice(5, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 0U, 1U, 1U, 0U);
			Assert.AreEqual(8, random.SumRollDice(5, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U, 1U, 1U, 1U);
			Assert.AreEqual(10, random.SumRollDice(5, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(5, random.SumRollDice(5, 6));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(21, random.SumRollDice(5, 6));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(30, random.SumRollDice(5, 6));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDiceKeepOneHighest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, random.SumRollDiceKeepHighest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(6, random.SumRollDiceKeepHighest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 3U, 4U, 4U, 2U);
			Assert.AreEqual(5, random.SumRollDiceKeepHighest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(6, random.SumRollDiceKeepHighest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDiceKeepOneLowest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, random.SumRollDiceKeepLowest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(2, random.SumRollDiceKeepLowest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(4U, 3U, 5U, 4U, 3U);
			Assert.AreEqual(4, random.SumRollDiceKeepLowest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(6, random.SumRollDiceKeepLowest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDiceKeepTwoHighest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, random.SumRollDiceKeepHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(11, random.SumRollDiceKeepHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 3U, 4U, 4U, 2U);
			Assert.AreEqual(10, random.SumRollDiceKeepHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, random.SumRollDiceKeepHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDiceKeepTwoLowest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, random.SumRollDiceKeepLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(5, random.SumRollDiceKeepLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(4U, 3U, 5U, 4U, 3U);
			Assert.AreEqual(8, random.SumRollDiceKeepLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, random.SumRollDiceKeepLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDiceKeepFourHighest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, random.SumRollDiceKeepHighest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(19, random.SumRollDiceKeepHighest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U, 4U, 4U, 1U);
			Assert.AreEqual(14, random.SumRollDiceKeepHighest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(24, random.SumRollDiceKeepHighest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDiceKeepFourLowest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, random.SumRollDiceKeepLowest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(15, random.SumRollDiceKeepLowest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(4U, 3U, 4U, 4U, 0U);
			Assert.AreEqual(15, random.SumRollDiceKeepLowest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(24, random.SumRollDiceKeepLowest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDiceDropTwoHighest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, random.SumRollDiceDropHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(10, random.SumRollDiceDropHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U, 4U, 4U, 1U);
			Assert.AreEqual(6, random.SumRollDiceDropHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, random.SumRollDiceDropHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[TestCase(Category = "Normal")]
		public void FiveSixSidedDiceDropTwoLowest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, random.SumRollDiceDropLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(16, random.SumRollDiceDropLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 3U, 4U, 1U, 0U);
			Assert.AreEqual(11, random.SumRollDiceDropLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, random.SumRollDiceDropLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}
#endif
	}
}
#endif
