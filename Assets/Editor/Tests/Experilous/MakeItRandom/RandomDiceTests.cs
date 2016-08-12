/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using NUnit.Framework;
using NSubstitute;

namespace Experilous.MakeIt.Random.Tests
{
	class RandomDiceTests
	{
		[Test]
		public void TwoSidedDie()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U);
			Assert.AreEqual(1, random.RollDice(2));
			random.Received().Next32();
			Assert.AreEqual(2, random.RollDice(2));
			random.Received().Next32();
		}

		[Test]
		public void ThreeSidedDie()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 2U);
			Assert.AreEqual(1, random.RollDice(3));
			random.Received().Next32();
			Assert.AreEqual(2, random.RollDice(3));
			random.Received().Next32();
			Assert.AreEqual(3, random.RollDice(3));
			random.Received().Next32();
		}

		[Test]
		public void SixSidedDie()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, random.RollDice(6));
			random.Received().Next32();
			Assert.AreEqual(2, random.RollDice(6));
			random.Received().Next32();
			Assert.AreEqual(3, random.RollDice(6));
			random.Received().Next32();
			Assert.AreEqual(4, random.RollDice(6));
			random.Received().Next32();
			Assert.AreEqual(5, random.RollDice(6));
			random.Received().Next32();
			Assert.AreEqual(6, random.RollDice(6));
			random.Received().Next32();
		}

		[Test]
		public void TwentySidedDie()
		{
			var random = Substitute.For<IRandom>();
			random.Next32().Returns(0U, 1U, 9U, 10U, 18U, 19U);
			Assert.AreEqual(1, random.RollDice(20));
			random.Received().Next32();
			Assert.AreEqual(2, random.RollDice(20));
			random.Received().Next32();
			Assert.AreEqual(10, random.RollDice(20));
			random.Received().Next32();
			Assert.AreEqual(11, random.RollDice(20));
			random.Received().Next32();
			Assert.AreEqual(19, random.RollDice(20));
			random.Received().Next32();
			Assert.AreEqual(20, random.RollDice(20));
			random.Received().Next32();
		}

		[Test]
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

		[Test]
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

		[Test]
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

		[Test]
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

		[Test]
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

		[Test]
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

		[Test]
		public void TwoTwoSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, random.RollDice(2, 2));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 1U);
			Assert.AreEqual(3, random.RollDice(2, 2));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 0U);
			Assert.AreEqual(3, random.RollDice(2, 2));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U);
			Assert.AreEqual(4, random.RollDice(2, 2));
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void TwoThreeSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 1U);
			Assert.AreEqual(3, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 2U);
			Assert.AreEqual(4, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 0U);
			Assert.AreEqual(3, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U);
			Assert.AreEqual(4, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 2U);
			Assert.AreEqual(5, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 0U);
			Assert.AreEqual(4, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 1U);
			Assert.AreEqual(5, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 2U);
			Assert.AreEqual(6, random.RollDice(2, 3));
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void TwoSixSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 2U);
			Assert.AreEqual(4, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 5U);
			Assert.AreEqual(7, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 0U);
			Assert.AreEqual(4, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 3U);
			Assert.AreEqual(7, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(2U, 5U);
			Assert.AreEqual(9, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 0U);
			Assert.AreEqual(7, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 2U);
			Assert.AreEqual(9, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U);
			Assert.AreEqual(12, random.RollDice(2, 6));
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void TwoTwentySidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 4U);
			Assert.AreEqual(6, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(0U, 19U);
			Assert.AreEqual(21, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(11U, 0U);
			Assert.AreEqual(13, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(11U, 7U);
			Assert.AreEqual(20, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(11U, 19U);
			Assert.AreEqual(32, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(19U, 0U);
			Assert.AreEqual(21, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(19U, 12U);
			Assert.AreEqual(33, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(19U, 19U);
			Assert.AreEqual(40, random.RollDice(2, 20));
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveTwoSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(5, random.RollDice(5, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 0U, 1U, 1U, 0U);
			Assert.AreEqual(8, random.RollDice(5, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U, 1U, 1U, 1U);
			Assert.AreEqual(10, random.RollDice(5, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDice()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(5, random.RollDice(5, 6));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(21, random.RollDice(5, 6));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(30, random.RollDice(5, 6));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDiceKeepOneHighest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, random.RollDiceKeepHighest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(6, random.RollDiceKeepHighest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 3U, 4U, 4U, 2U);
			Assert.AreEqual(5, random.RollDiceKeepHighest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(6, random.RollDiceKeepHighest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDiceKeepOneLowest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, random.RollDiceKeepLowest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(2, random.RollDiceKeepLowest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(4U, 3U, 5U, 4U, 3U);
			Assert.AreEqual(4, random.RollDiceKeepLowest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(6, random.RollDiceKeepLowest(5, 6, 1));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDiceKeepTwoHighest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, random.RollDiceKeepHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(11, random.RollDiceKeepHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 3U, 4U, 4U, 2U);
			Assert.AreEqual(10, random.RollDiceKeepHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, random.RollDiceKeepHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDiceKeepTwoLowest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, random.RollDiceKeepLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(5, random.RollDiceKeepLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(4U, 3U, 5U, 4U, 3U);
			Assert.AreEqual(8, random.RollDiceKeepLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, random.RollDiceKeepLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDiceKeepFourHighest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, random.RollDiceKeepHighest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(19, random.RollDiceKeepHighest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U, 4U, 4U, 1U);
			Assert.AreEqual(14, random.RollDiceKeepHighest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(24, random.RollDiceKeepHighest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDiceKeepFourLowest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, random.RollDiceKeepLowest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(15, random.RollDiceKeepLowest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(4U, 3U, 4U, 4U, 0U);
			Assert.AreEqual(15, random.RollDiceKeepLowest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(24, random.RollDiceKeepLowest(5, 6, 4));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDiceDropTwoHighest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, random.RollDiceDropHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(10, random.RollDiceDropHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 1U, 4U, 4U, 1U);
			Assert.AreEqual(6, random.RollDiceDropHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, random.RollDiceDropHighest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void FiveSixSidedDiceDropTwoLowest()
		{
			var random = Substitute.For<IRandom>();

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, random.RollDiceDropLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 5U, 4U, 4U, 2U);
			Assert.AreEqual(16, random.RollDiceDropLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(1U, 3U, 4U, 1U, 0U);
			Assert.AreEqual(11, random.RollDiceDropLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();

			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, random.RollDiceDropLowest(5, 6, 2));
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
			random.Received().Next32();
		}

		[Test]
		public void Notation_d6()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("d6");

			random.Next32().Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, dice(random));
			random.Received().Next32();
			Assert.AreEqual(2, dice(random));
			random.Received().Next32();
			Assert.AreEqual(3, dice(random));
			random.Received().Next32();
			Assert.AreEqual(4, dice(random));
			random.Received().Next32();
			Assert.AreEqual(5, dice(random));
			random.Received().Next32();
			Assert.AreEqual(6, dice(random));
			random.Received().Next32();
		}

		[Test]
		public void Notation_D6()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("D6");

			random.Next32().Returns(0U, 1U, 2U, 3U, 4U, 5U);
			Assert.AreEqual(1, dice(random));
			random.Received().Next32();
			Assert.AreEqual(2, dice(random));
			random.Received().Next32();
			Assert.AreEqual(3, dice(random));
			random.Received().Next32();
			Assert.AreEqual(4, dice(random));
			random.Received().Next32();
			Assert.AreEqual(5, dice(random));
			random.Received().Next32();
			Assert.AreEqual(6, dice(random));
			random.Received().Next32();
		}

		[Test]
		public void Notation_3d4()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("3d4");

			random.Next32().Returns(0U, 0U, 0U);
			Assert.AreEqual(3, dice(random));
			random.Next32().Returns(1U, 3U, 0U);
			Assert.AreEqual(7, dice(random));
			random.Next32().Returns(3U, 3U, 3U);
			Assert.AreEqual(12, dice(random));
		}

		[Test]
		public void Notation_2d72()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("2d72");

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(2, dice(random));
			random.Next32().Returns(22U, 51U);
			Assert.AreEqual(75, dice(random));
			random.Next32().Returns(71U, 71U);
			Assert.AreEqual(144, dice(random));
		}

		[Test]
		public void Notation_1d8_Add1()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("1d8+1");

			random.Next32().Returns(0U);
			Assert.AreEqual(2, dice(random));
			random.Next32().Returns(3U);
			Assert.AreEqual(5, dice(random));
			random.Next32().Returns(7U);
			Assert.AreEqual(9, dice(random));
		}

		[Test]
		public void Notation_2d4_Sub3()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("2d4-3");

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(-1, dice(random));
			random.Next32().Returns(2U, 0U);
			Assert.AreEqual(1, dice(random));
			random.Next32().Returns(3U, 3U);
			Assert.AreEqual(5, dice(random));
		}

		[Test]
		public void Notation_d2_Mul10()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("d2 x10");

			random.Next32().Returns(0U);
			Assert.AreEqual(10, dice(random));
			random.Next32().Returns(1U);
			Assert.AreEqual(20, dice(random));
		}

		[Test]
		public void Notation_5d4_Div5()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d4 / 5");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, dice(random));
			random.Next32().Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(2, dice(random));
			random.Next32().Returns(3U, 2U, 3U, 3U, 3U);
			Assert.AreEqual(3, dice(random));
			random.Next32().Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(4, dice(random));
		}

		[Test]
		public void Notation_2d3_Mul3Add1()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("2d3*3+1");

			random.Next32().Returns(0U, 0U);
			Assert.AreEqual(7, dice(random));
			random.Next32().Returns(2U, 1U);
			Assert.AreEqual(16, dice(random));
			random.Next32().Returns(2U, 2U);
			Assert.AreEqual(19, dice(random));
		}

		[Test]
		public void Notation_10d8_Div5Sub1()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("10d8 / 5 - 1");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, dice(random));
			random.Next32().Returns(1U, 5U, 0U, 3U, 6U, 6U, 2U, 3U, 1U, 7U);
			Assert.AreEqual(7, dice(random));
			random.Next32().Returns(3U, 2U, 2U, 4U, 5U, 0U, 7U, 6U, 7U, 1U);
			Assert.AreEqual(8, dice(random));
			random.Next32().Returns(7U, 7U, 7U, 7U, 7U, 7U, 7U, 7U, 7U, 7U);
			Assert.AreEqual(15, dice(random));
		}

		[Test]
		public void Notation_5d4_KeepHighest()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d4kH");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, dice(random));
			random.Next32().Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(4, dice(random));
			random.Next32().Returns(0U, 2U, 1U, 0U, 2U);
			Assert.AreEqual(3, dice(random));
			random.Next32().Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(4, dice(random));
		}

		[Test]
		public void Notation_5d6_Keep3Highest()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d6k3h");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, dice(random));
			random.Next32().Returns(1U, 0U, 0U, 3U, 0U);
			Assert.AreEqual(7, dice(random));
			random.Next32().Returns(5U, 2U, 1U, 0U, 4U);
			Assert.AreEqual(14, dice(random));
			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, dice(random));
		}

		[Test]
		public void Notation_5d4_KeepLowest()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d4kL");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(1, dice(random));
			random.Next32().Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(1, dice(random));
			random.Next32().Returns(3U, 2U, 1U, 3U, 2U);
			Assert.AreEqual(2, dice(random));
			random.Next32().Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(4, dice(random));
		}

		[Test]
		public void Notation_5d6_Keep3Lowest()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d6k3l");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(3, dice(random));
			random.Next32().Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(4, dice(random));
			random.Next32().Returns(5U, 2U, 1U, 0U, 4U);
			Assert.AreEqual(6, dice(random));
			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(18, dice(random));
		}

		[Test]
		public void Notation_5d4_DropHighest()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d4-H");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, dice(random));
			random.Next32().Returns(1U, 2U, 0U, 3U, 1U);
			Assert.AreEqual(8, dice(random));
			random.Next32().Returns(0U, 2U, 1U, 0U, 2U);
			Assert.AreEqual(7, dice(random));
			random.Next32().Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(16, dice(random));
		}

		[Test]
		public void Notation_5d6_Drop3Highest()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d6d3h");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, dice(random));
			random.Next32().Returns(1U, 2U, 0U, 3U, 2U);
			Assert.AreEqual(3, dice(random));
			random.Next32().Returns(5U, 3U, 1U, 3U, 5U);
			Assert.AreEqual(6, dice(random));
			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, dice(random));
		}

		[Test]
		public void Notation_5d4_DropLowest()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d4dL");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(4, dice(random));
			random.Next32().Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(10, dice(random));
			random.Next32().Returns(3U, 2U, 1U, 3U, 2U);
			Assert.AreEqual(14, dice(random));
			random.Next32().Returns(3U, 3U, 3U, 3U, 3U);
			Assert.AreEqual(16, dice(random));
		}

		[Test]
		public void Notation_5d6_Drop3Lowest()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("5d6-3l");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(2, dice(random));
			random.Next32().Returns(1U, 2U, 0U, 3U, 0U);
			Assert.AreEqual(7, dice(random));
			random.Next32().Returns(5U, 2U, 1U, 0U, 4U);
			Assert.AreEqual(11, dice(random));
			random.Next32().Returns(5U, 5U, 5U, 5U, 5U);
			Assert.AreEqual(12, dice(random));
		}

		[Test]
		public void Notation_4d6_Drop1Lowest_Sub1()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("4d6-1l-1");

			random.Next32().Returns(0U, 0U, 0U, 0U);
			Assert.AreEqual(2, dice(random));
			random.Next32().Returns(4U, 1U, 2U, 0U);
			Assert.AreEqual(9, dice(random));
			random.Next32().Returns(5U, 5U, 5U, 5U);
			Assert.AreEqual(17, dice(random));
		}

		[Test]
		public void Notation_8d20_Keep5Highest_Div2_Plus8()
		{
			var random = Substitute.For<IRandom>();
			var dice = RandomDice.Prepare("8d20 k5h /2 +8");

			random.Next32().Returns(0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U);
			Assert.AreEqual(10, dice(random));
			random.Next32().Returns(2U, 3U, 17U, 11U, 14U, 0U, 14U, 7U);
			Assert.AreEqual(42, dice(random));
			random.Next32().Returns(19U, 19U, 19U, 19U, 19U, 19U, 19U, 19U);
			Assert.AreEqual(58, dice(random));
		}
	}
}
#endif
