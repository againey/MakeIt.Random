/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using NUnit.Framework;
using NSubstitute;

namespace Experilous.Randomization.Tests
{
	class DiceTests
	{
		[Test]
		public void TwoSidedDie()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(2U).Returns(0U, 1U);
			Assert.AreEqual(1, Dice.Roll(2, random));
			random.Received().NextLessThan(2U);
			Assert.AreEqual(2, Dice.Roll(2, random));
			random.Received().NextLessThan(2U);
		}

		[Test]
		public void ThreeSidedDie()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(3U).Returns(0U, 1U, 2U);
			Assert.AreEqual(1, Dice.Roll(3, random));
			random.Received().NextLessThan(3U);
			Assert.AreEqual(2, Dice.Roll(3, random));
			random.Received().NextLessThan(3U);
			Assert.AreEqual(3, Dice.Roll(3, random));
			random.Received().NextLessThan(3U);
		}

		[Test]
		public void SixSidedDie()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(6U).Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, Dice.Roll(6, random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(2, Dice.Roll(6, random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(3, Dice.Roll(6, random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(4, Dice.Roll(6, random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(5, Dice.Roll(6, random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(6, Dice.Roll(6, random));
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void TwentySidedDie()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(20U).Returns(0U, 1U, 9U, 10U, 18U, 19U);
			Assert.AreEqual(1, Dice.Roll(20, random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(2, Dice.Roll(20, random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(10, Dice.Roll(20, random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(11, Dice.Roll(20, random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(19, Dice.Roll(20, random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(20, Dice.Roll(20, random));
			random.Received().NextLessThan(20U);
		}

		[Test]
		public void D4()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(4U).Returns(0U, 1U, 2U, 3U);
			Assert.AreEqual(1, Dice.D4(random));
			random.Received().NextLessThan(4U);
			Assert.AreEqual(2, Dice.D4(random));
			random.Received().NextLessThan(4U);
			Assert.AreEqual(3, Dice.D4(random));
			random.Received().NextLessThan(4U);
			Assert.AreEqual(4, Dice.D4(random));
			random.Received().NextLessThan(4U);
		}

		[Test]
		public void D6()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(6U).Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, Dice.D6(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(2, Dice.D6(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(3, Dice.D6(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(4, Dice.D6(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(5, Dice.D6(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(6, Dice.D6(random));
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void D8()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(8U).Returns(0U, 1U, 3U, 4U, 6U, 7U);
			Assert.AreEqual(1, Dice.D8(random));
			random.Received().NextLessThan(8U);
			Assert.AreEqual(2, Dice.D8(random));
			random.Received().NextLessThan(8U);
			Assert.AreEqual(4, Dice.D8(random));
			random.Received().NextLessThan(8U);
			Assert.AreEqual(5, Dice.D8(random));
			random.Received().NextLessThan(8U);
			Assert.AreEqual(7, Dice.D8(random));
			random.Received().NextLessThan(8U);
			Assert.AreEqual(8, Dice.D8(random));
			random.Received().NextLessThan(8U);
		}

		[Test]
		public void D10()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(10U).Returns(0U, 1U, 4U, 5U, 8U, 9U);
			Assert.AreEqual(1, Dice.D10(random));
			random.Received().NextLessThan(10U);
			Assert.AreEqual(2, Dice.D10(random));
			random.Received().NextLessThan(10U);
			Assert.AreEqual(5, Dice.D10(random));
			random.Received().NextLessThan(10U);
			Assert.AreEqual(6, Dice.D10(random));
			random.Received().NextLessThan(10U);
			Assert.AreEqual(9, Dice.D10(random));
			random.Received().NextLessThan(10U);
			Assert.AreEqual(10, Dice.D10(random));
			random.Received().NextLessThan(10U);
		}

		[Test]
		public void D12()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(12U).Returns(0U, 1U, 5U, 6U, 10U, 11U);
			Assert.AreEqual(1, Dice.D12(random));
			random.Received().NextLessThan(12U);
			Assert.AreEqual(2, Dice.D12(random));
			random.Received().NextLessThan(12U);
			Assert.AreEqual(6, Dice.D12(random));
			random.Received().NextLessThan(12U);
			Assert.AreEqual(7, Dice.D12(random));
			random.Received().NextLessThan(12U);
			Assert.AreEqual(11, Dice.D12(random));
			random.Received().NextLessThan(12U);
			Assert.AreEqual(12, Dice.D12(random));
			random.Received().NextLessThan(12U);
		}

		[Test]
		public void D20()
		{
			var random = Substitute.For<IRandomEngine>();
			random.NextLessThan(20U).Returns(0U, 1U, 9U, 10U, 18U, 19U);
			Assert.AreEqual(1, Dice.D20(random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(2, Dice.D20(random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(10, Dice.D20(random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(11, Dice.D20(random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(19, Dice.D20(random));
			random.Received().NextLessThan(20U);
			Assert.AreEqual(20, Dice.D20(random));
			random.Received().NextLessThan(20U);
		}

		[Test]
		public void TwoTwoSidedDice()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(2U).Returns(0U, 0U);
			Assert.AreEqual(2, Dice.Roll(2, 2, random));
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);

			random.NextLessThan(2U).Returns(0U, 1U);
			Assert.AreEqual(3, Dice.Roll(2, 2, random));
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);

			random.NextLessThan(2U).Returns(1U, 0U);
			Assert.AreEqual(3, Dice.Roll(2, 2, random));
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);

			random.NextLessThan(2U).Returns(1U, 1U);
			Assert.AreEqual(4, Dice.Roll(2, 2, random));
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
		}

		[Test]
		public void TwoThreeSidedDice()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(3U).Returns(0U, 0U);
			Assert.AreEqual(2, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);

			random.NextLessThan(3U).Returns(0U, 1U);
			Assert.AreEqual(3, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);

			random.NextLessThan(3U).Returns(0U, 2U);
			Assert.AreEqual(4, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);

			random.NextLessThan(3U).Returns(1U, 0U);
			Assert.AreEqual(3, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);

			random.NextLessThan(3U).Returns(1U, 1U);
			Assert.AreEqual(4, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);

			random.NextLessThan(3U).Returns(1U, 2U);
			Assert.AreEqual(5, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);

			random.NextLessThan(3U).Returns(2U, 0U);
			Assert.AreEqual(4, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);

			random.NextLessThan(3U).Returns(2U, 1U);
			Assert.AreEqual(5, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);

			random.NextLessThan(3U).Returns(2U, 2U);
			Assert.AreEqual(6, Dice.Roll(2, 3, random));
			random.Received().NextLessThan(3U);
			random.Received().NextLessThan(3U);
		}

		[Test]
		public void TwoSixSidedDice()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U);
			Assert.AreEqual(2, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(0U, 2U);
			Assert.AreEqual(4, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(0U, 5U);
			Assert.AreEqual(7, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(2U, 0U);
			Assert.AreEqual(4, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(2U, 3U);
			Assert.AreEqual(7, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(2U, 5U);
			Assert.AreEqual(9, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 0U);
			Assert.AreEqual(7, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 2U);
			Assert.AreEqual(9, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U);
			Assert.AreEqual(12, Dice.Roll(2, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void TwoTwentySidedDice()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(20U).Returns(0U, 0U);
			Assert.AreEqual(2, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);

			random.NextLessThan(20U).Returns(0U, 4U);
			Assert.AreEqual(6, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);

			random.NextLessThan(20U).Returns(0U, 19U);
			Assert.AreEqual(21, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);

			random.NextLessThan(20U).Returns(11U, 0U);
			Assert.AreEqual(13, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);

			random.NextLessThan(20U).Returns(11U, 7U);
			Assert.AreEqual(20, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);

			random.NextLessThan(20U).Returns(11U, 19U);
			Assert.AreEqual(32, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);

			random.NextLessThan(20U).Returns(19U, 0U);
			Assert.AreEqual(21, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);

			random.NextLessThan(20U).Returns(19U, 12U);
			Assert.AreEqual(33, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);

			random.NextLessThan(20U).Returns(19U, 19U);
			Assert.AreEqual(40, Dice.Roll(2, 20, random));
			random.Received().NextLessThan(20U);
			random.Received().NextLessThan(20U);
		}

		[Test]
		public void FiveTwoSidedDice()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(2U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(5, Dice.Roll(5, 2, random));
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);

			random.NextLessThan(2U).Returns(1U, 0U, 1U, 1U, 0U);
			Assert.AreEqual(8, Dice.Roll(5, 2, random));
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);

			random.NextLessThan(2U).Returns(1U, 1U, 1U, 1U, 1U);
			Assert.AreEqual(10, Dice.Roll(5, 2, random));
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
			random.Received().NextLessThan(2U);
		}

		[Test]
		public void FiveSixSidedDice()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(5, Dice.Roll(5, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(21, Dice.Roll(5, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(30, Dice.Roll(5, 6, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void FiveSixSidedDiceKeepOneHighest()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, Dice.RollKeepHighest(5, 6, 1, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(6, Dice.RollKeepHighest(5, 6, 1, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 3U, 4U, 4U, 2U);
			Assert.AreEqual(5, Dice.RollKeepHighest(5, 6, 1, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(6, Dice.RollKeepHighest(5, 6, 1, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void FiveSixSidedDiceKeepOneLowest()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, Dice.RollKeepLowest(5, 6, 1, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(2, Dice.RollKeepLowest(5, 6, 1, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(4U, 3U, 5U, 4U, 3U);
			Assert.AreEqual(4, Dice.RollKeepLowest(5, 6, 1, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(6, Dice.RollKeepLowest(5, 6, 1, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void FiveSixSidedDiceKeepTwoHighest()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, Dice.RollKeepHighest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(11, Dice.RollKeepHighest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 3U, 4U, 4U, 2U);
			Assert.AreEqual(10, Dice.RollKeepHighest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, Dice.RollKeepHighest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void FiveSixSidedDiceKeepTwoLowest()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, Dice.RollKeepLowest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(5, Dice.RollKeepLowest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(4U, 3U, 5U, 4U, 3U);
			Assert.AreEqual(8, Dice.RollKeepLowest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, Dice.RollKeepLowest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void FiveSixSidedDiceKeepFourHighest()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, Dice.RollKeepHighest(5, 6, 4, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(19, Dice.RollKeepHighest(5, 6, 4, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 1U, 4U, 4U, 1U);
			Assert.AreEqual(14, Dice.RollKeepHighest(5, 6, 4, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(24, Dice.RollKeepHighest(5, 6, 4, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void FiveSixSidedDiceKeepFourLowest()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, Dice.RollKeepLowest(5, 6, 4, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(15, Dice.RollKeepLowest(5, 6, 4, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(4U, 3U, 4U, 4U, 0U);
			Assert.AreEqual(15, Dice.RollKeepLowest(5, 6, 4, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(24, Dice.RollKeepLowest(5, 6, 4, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void FiveSixSidedDiceDropTwoHighest()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, Dice.RollDropHighest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(10, Dice.RollDropHighest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 1U, 4U, 4U, 1U);
			Assert.AreEqual(6, Dice.RollDropHighest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, Dice.RollDropHighest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void FiveSixSidedDiceDropTwoLowest()
		{
			var random = Substitute.For<IRandomEngine>();

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, Dice.RollDropLowest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(16, Dice.RollDropLowest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(1U, 3U, 4U, 1U, 0U);
			Assert.AreEqual(11, Dice.RollDropLowest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);

			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, Dice.RollDropLowest(5, 6, 2, random));
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void Notation_d6()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("d6");

			random.NextLessThan(6U).Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(2, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(3, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(4, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(5, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(6, dice(random));
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void Notation_D6()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("D6");

			random.NextLessThan(6U).Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(2, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(3, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(4, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(5, dice(random));
			random.Received().NextLessThan(6U);
			Assert.AreEqual(6, dice(random));
			random.Received().NextLessThan(6U);
		}

		[Test]
		public void Notation_3d4()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("3d4");

			random.NextLessThan(4U).Returns(0U, 0U, 0U);
			Assert.AreEqual(3, dice(random));
			random.NextLessThan(4U).Returns(1U, 3U, 0U);
			Assert.AreEqual(7, dice(random));
			random.NextLessThan(4U).Returns(3U, 3U, 3U);
			Assert.AreEqual(12, dice(random));
		}

		[Test]
		public void Notation_2d72()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("2d72");

			random.NextLessThan(72U).Returns(0U, 0U);
			Assert.AreEqual(2, dice(random));
			random.NextLessThan(72U).Returns(22U, 51U);
			Assert.AreEqual(75, dice(random));
			random.NextLessThan(72U).Returns(71U, 71U);
			Assert.AreEqual(144, dice(random));
		}

		[Test]
		public void Notation_1d8_Add1()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("1d8+1");

			random.NextLessThan(8U).Returns(0U);
			Assert.AreEqual(2, dice(random));
			random.NextLessThan(8U).Returns(3U);
			Assert.AreEqual(5, dice(random));
			random.NextLessThan(8U).Returns(7U);
			Assert.AreEqual(9, dice(random));
		}

		[Test]
		public void Notation_2d4_Sub3()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("2d4-3");

			random.NextLessThan(4U).Returns(0U, 0U);
			Assert.AreEqual(-1, dice(random));
			random.NextLessThan(4U).Returns(2U, 0U);
			Assert.AreEqual(1, dice(random));
			random.NextLessThan(4U).Returns(3U, 3U);
			Assert.AreEqual(5, dice(random));
		}

		[Test]
		public void Notation_d2_Mul10()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("d2 x10");

			random.NextLessThan(2U).Returns(0U);
			Assert.AreEqual(10, dice(random));
			random.NextLessThan(2U).Returns(1U);
			Assert.AreEqual(20, dice(random));
		}

		[Test]
		public void Notation_5d4_Div5()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d4 / 5");

			random.NextLessThan(4U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, dice(random));
			random.NextLessThan(4U).Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(2, dice(random));
			random.NextLessThan(4U).Returns(3U, 2U, 3U, 3U, 3U);
			Assert.AreEqual(3, dice(random));
			random.NextLessThan(4U).Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(4, dice(random));
		}

		[Test]
		public void Notation_2d3_Mul3Add1()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("2d3*3+1");

			random.NextLessThan(3U).Returns(0U, 0U);
			Assert.AreEqual(7, dice(random));
			random.NextLessThan(3U).Returns(2U, 1U);
			Assert.AreEqual(16, dice(random));
			random.NextLessThan(3U).Returns(2U, 2U);
			Assert.AreEqual(19, dice(random));
		}

		[Test]
		public void Notation_10d8_Div5Sub1()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("10d8 / 5 - 1");

			random.NextLessThan(8U).Returns(0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, dice(random));
			random.NextLessThan(8U).Returns(1U, 5U, 0U, 3U, 6U, 6U, 2U, 3U, 1U, 7U);
			Assert.AreEqual(7, dice(random));
			random.NextLessThan(8U).Returns(3U, 2U, 2U, 4U, 5U, 0U, 7U, 6U, 7U, 1U);
			Assert.AreEqual(8, dice(random));
			random.NextLessThan(8U).Returns(7U, 7U, 7U, 7U, 7U, 7U, 7U, 7U, 7U, 7U);
			Assert.AreEqual(15, dice(random));
		}

		[Test]
		public void Notation_5d4_KeepHighest()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d4kH");

			random.NextLessThan(4U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, dice(random));
			random.NextLessThan(4U).Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(4, dice(random));
			random.NextLessThan(4U).Returns(0U, 2U, 1U, 0U, 2U);
			Assert.AreEqual(3, dice(random));
			random.NextLessThan(4U).Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(4, dice(random));
		}

		[Test]
		public void Notation_5d6_Keep3Highest()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d6k3h");

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, dice(random));
			random.NextLessThan(6U).Returns(1U, 0U, 0U, 3U, 0U);
			Assert.AreEqual(7, dice(random));
			random.NextLessThan(6U).Returns(5U, 2U, 1U, 0U, 4U);
			Assert.AreEqual(14, dice(random));
			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, dice(random));
		}

		[Test]
		public void Notation_5d4_KeepLowest()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d4kL");

			random.NextLessThan(4U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, dice(random));
			random.NextLessThan(4U).Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(1, dice(random));
			random.NextLessThan(4U).Returns(3U, 2U, 1U, 3U, 2U);
			Assert.AreEqual(2, dice(random));
			random.NextLessThan(4U).Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(4, dice(random));
		}

		[Test]
		public void Notation_5d6_Keep3Lowest()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d6k3l");

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, dice(random));
			random.NextLessThan(6U).Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(4, dice(random));
			random.NextLessThan(6U).Returns(5U, 2U, 1U, 0U, 4U);
			Assert.AreEqual(6, dice(random));
			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, dice(random));
		}

		[Test]
		public void Notation_5d4_DropHighest()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d4-H");

			random.NextLessThan(4U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, dice(random));
			random.NextLessThan(4U).Returns(1U, 2U, 0U, 3U, 1U);
			Assert.AreEqual(8, dice(random));
			random.NextLessThan(4U).Returns(0U, 2U, 1U, 0U, 2U);
			Assert.AreEqual(7, dice(random));
			random.NextLessThan(4U).Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(16, dice(random));
		}

		[Test]
		public void Notation_5d6_Drop3Highest()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d6d3h");

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, dice(random));
			random.NextLessThan(6U).Returns(1U, 2U, 0U, 3U, 2U);
			Assert.AreEqual(3, dice(random));
			random.NextLessThan(6U).Returns(5U, 3U, 1U, 3U, 5U);
			Assert.AreEqual(6, dice(random));
			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, dice(random));
		}

		[Test]
		public void Notation_5d4_DropLowest()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d4dL");

			random.NextLessThan(4U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, dice(random));
			random.NextLessThan(4U).Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(10, dice(random));
			random.NextLessThan(4U).Returns(3U, 2U, 1U, 3U, 2U);
			Assert.AreEqual(14, dice(random));
			random.NextLessThan(4U).Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(16, dice(random));
		}

		[Test]
		public void Notation_5d6_Drop3Lowest()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("5d6-3l");

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, dice(random));
			random.NextLessThan(6U).Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(7, dice(random));
			random.NextLessThan(6U).Returns(5U, 2U, 1U, 0U, 4U);
			Assert.AreEqual(11, dice(random));
			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, dice(random));
		}

		[Test]
		public void Notation_4d6_Drop1Lowest_Sub1()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("4d6-1l-1");

			random.NextLessThan(6U).Returns(0U, 0U, 0U, 0U);
			Assert.AreEqual(2, dice(random));
			random.NextLessThan(6U).Returns(4U, 1U, 2U, 0U);
			Assert.AreEqual(9, dice(random));
			random.NextLessThan(6U).Returns(5U, 5U, 5U, 5U);
			Assert.AreEqual(17, dice(random));
		}

		[Test]
		public void Notation_8d20_Keep5Highest_Div2_Plus8()
		{
			var random = Substitute.For<IRandomEngine>();
			var dice = Dice.Prepare("8d20 k5h /2 +8");

			random.NextLessThan(20U).Returns(0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(10, dice(random));
			random.NextLessThan(20U).Returns(2U, 3U, 17U, 11U, 14U, 0U, 14U, 7U);
			Assert.AreEqual(42, dice(random));
			random.NextLessThan(20U).Returns(19U, 19U, 19U, 19U, 19U, 19U, 19U, 19U);
			Assert.AreEqual(58, dice(random));
		}
	}
}
#endif
