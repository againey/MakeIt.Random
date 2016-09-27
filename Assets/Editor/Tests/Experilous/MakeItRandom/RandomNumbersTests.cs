/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;
using UnityEngine;
using System;

namespace Experilous.MakeItRandom.Tests
{
	class RandomNumbersTests
	{
		private const string seed = "random seed";

		private void ValidateBitDistribution32(int count, Func<uint> generator, int bitCount, float tolerance)
		{
			uint mask = 0xFFFFFFFFU >> (32 - bitCount);
			var buckets = new int[bitCount];
			for (int i = 0; i < count; ++i)
			{
				uint b = generator();
				if (b > mask) Assert.Fail(string.Format("Bit Count = {0}, Bits = 0x{1:X8}", bitCount, b));
				uint m = 1U;
				for (int j = 0; j < bitCount; ++j)
				{
					if ((b & m) != 0U)
					{
						buckets[j] += 1;
					}
					m = m << 1;
				}
			}

			var sb = new System.Text.StringBuilder();
			sb.AppendFormat("Bit Count = {0}, Buckets = [ {1}", bitCount, buckets[0]);
			for (int i = 1; i < bitCount; ++i)
			{
				sb.AppendFormat(", {0}", buckets[i]);
			}
			sb.Append(" ]");

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, count / 2), tolerance * count / 2, sb.ToString());
		}

		private void ValidateBitDistribution64(int count, Func<ulong> generator, int bitCount, float tolerance)
		{
			ulong mask = 0xFFFFFFFFFFFFFFFFUL >> (64 - bitCount);
			var buckets = new int[bitCount];
			for (int i = 0; i < count; ++i)
			{
				ulong b = generator();
				if (b > mask) Assert.Fail(string.Format("Bit Count = {0}, Bits = 0x{1:X16}", bitCount, b));
				ulong m = 1U;
				for (int j = 0; j < bitCount; ++j)
				{
					if ((b & m) != 0U)
					{
						buckets[j] += 1;
					}
					m = m << 1;
				}
			}

			var sb = new System.Text.StringBuilder();
			sb.AppendFormat("Bit Count = {0}, Buckets = [ {1}", bitCount, buckets[0]);
			for (int i = 1; i < bitCount; ++i)
			{
				sb.AppendFormat(", {0}", buckets[i]);
			}
			sb.Append(" ]");

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, count / 2), tolerance * count / 2, sb.ToString());
		}

		private void ValidateBinaryDistribution(int count, Func<int> generator, int first, int second, float expectedFirst, float tolerance)
		{
			int firstCount = 0;
			for (int i = 0; i < count; ++i)
			{
				int n = generator();
				if (n != first && n != second) Assert.Fail(n.ToString());
				firstCount += (n == first) ? 1 : 0;
			}

			Assert.LessOrEqual(Mathf.Abs((float)firstCount / count - expectedFirst), tolerance,
				string.Format("First Frequency = {0:F3}, Second Frequency = {1:F3}", (float)firstCount / count, (float)(count - firstCount) / count));
		}

		private void ValidateTrinaryDistribution(int count, Func<int> generator, int first, int second, int third, float expectedFirst, float expectedSecond, float tolerance)
		{
			int firstCount = 0;
			int secondCount = 0;
			for (int i = 0; i < count; ++i)
			{
				int n = generator();
				if (n != first && n != second && n != third) Assert.Fail(n.ToString());
				firstCount += (n == first) ? 1 : 0;
				secondCount += (n == second) ? 1 : 0;
			}

			var message = string.Format("First Frequency = {0:F3}, Second Frequency = {1:F3}, Third Frequency = {2}", (float)firstCount / count, (float)secondCount / count, (float)(count - firstCount - secondCount) / count);
			Assert.LessOrEqual(Mathf.Abs((float)firstCount / count - expectedFirst), tolerance, message);
			Assert.LessOrEqual(Mathf.Abs((float)secondCount / count - expectedSecond), tolerance, message);
		}

		private void ValidateAngleRange(int count, Func<float> generator, float min, float max, Action<float, float> assertLowerBoundary, Action<float, float> assertUpperBoundary)
		{
			for (int i = 0; i < count; ++i)
			{
				float a = generator();
				assertLowerBoundary(a, min);
				assertUpperBoundary(a, max);
			}
		}

		[Test]
		public void UniformBit()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBitDistribution32(10000, () => random.Bit(), 1, 0.1f);
			var generator = XorShift128Plus.Create(seed).MakeBitGenerator();
			ValidateBitDistribution32(10000, () => generator.Next(), 1, 0.1f);
		}

		[Test]
		public void UniformBits32()
		{
			for (int i = 1; i <= 32; ++i)
			{
				var random = XorShift128Plus.Create(seed);
				ValidateBitDistribution32(10000, () => random.Bits32(i), i, 0.1f);
				var generator = XorShift128Plus.Create(seed).MakeBits32Generator(i);
				ValidateBitDistribution32(10000, () => generator.Next(), i, 0.1f);
			}
		}

		[Test]
		public void UniformBits64()
		{
			for (int i = 1; i <= 64; ++i)
			{
				var random = XorShift128Plus.Create(seed);
				ValidateBitDistribution64(10000, () => random.Bits64(i), i, 0.1f);
				var generator = XorShift128Plus.Create(seed).MakeBits64Generator(i);
				ValidateBitDistribution64(10000, () => generator.Next(), i, 0.1f);
			}
		}

		[Test]
		public void UniformOneOrZero()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneOrZero(), 1, 0, 0.5f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneOrZeroGenerator();
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.5f, 0.02f);
		}

		[Test]
		public void UniformSign()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.Sign(), 1, -1, 0.5f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignGenerator();
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.5f, 0.02f);
		}

		[Test]
		public void UniformSignOrZero()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignOrZero(), 1, -1, 0, 0.3333333333f, 0.3333333333f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignOrZeroGenerator();
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3333333333f, 0.3333333333f, 0.02f);
		}

		[Test]
		public void WeightedOneOrZeroInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneOrZero(49, 5), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneOrZeroGenerator(49, 5);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.Sign(49, 5), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignGenerator(49, 5);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignOrZeroInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignOrZero(17, 32, 5), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignOrZeroGenerator(17, 32, 5);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneOrZeroUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneOrZero(49U, 5U), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneOrZeroGenerator(49U, 5U);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.Sign(49U, 5U), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignGenerator(49U, 5U);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignOrZeroUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignOrZero(17U, 32U, 5U), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignOrZeroGenerator(17U, 32U, 5U);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneOrZeroInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneOrZero(49L, 5L), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneOrZeroGenerator(49L, 5L);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.Sign(49L, 5L), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignGenerator(49L, 5L);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignOrZeroInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignOrZero(17L, 32L, 5L), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignOrZeroGenerator(17L, 32L, 5L);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneOrZeroUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneOrZero(49UL, 5UL), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneOrZeroGenerator(49UL, 5UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.Sign(49UL, 5UL), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignGenerator(49UL, 5UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignOrZeroUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignOrZero(17UL, 32UL, 5UL), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignOrZeroGenerator(17UL, 32UL, 5UL);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneOrZeroFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneOrZero(49f, 5f), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneOrZeroGenerator(49f, 5f);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.Sign(49f, 5f), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignGenerator(49f, 5f);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignOrZeroFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignOrZero(17f, 32f, 5f), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignOrZeroGenerator(17f, 32f, 5f);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneOrZeroDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneOrZero(49d, 5d), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneOrZeroGenerator(49d, 5d);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.Sign(49d, 5d), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignGenerator(49d, 5d);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignOrZeroDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignOrZero(17d, 32d, 5d), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignOrZeroGenerator(17d, 32d, 5d);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityNumeratorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(1948642569), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(1948642569);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityNumeratorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(1948642569), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(1948642569);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityNumeratorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(1948642569), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(1948642569);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityNumeratorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(1948642569), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(1948642569);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityNumeratorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(676059667, 1272582903), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(676059667, 1272582903);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityNumeratorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(3897285139U), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(3897285139U);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityNumeratorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(3897285139U), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(3897285139U);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityNumeratorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(3897285139U), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(3897285139U);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityNumeratorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(3897285139U), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(3897285139U);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityNumeratorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(1352119334U, 2545165805U), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(1352119334U, 2545165805U);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityNumeratorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(8369356107516370641L), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(8369356107516370641L);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityNumeratorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(8369356107516370641L), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(8369356107516370641L);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityNumeratorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(8369356107516370641L), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(8369356107516370641L);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityNumeratorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(8369356107516370641L), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(8369356107516370641L);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityNumeratorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(2903654159750577569L, 5465701947765793071L), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(2903654159750577569L, 5465701947765793071L);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityNumeratorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(16738712215032741281UL), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(16738712215032741281UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityNumeratorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(16738712215032741281UL), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(16738712215032741281UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityNumeratorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(16738712215032741281UL), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(16738712215032741281UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityNumeratorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(16738712215032741281UL), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(16738712215032741281UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityNumeratorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(5807308319501155138UL, 10931403895531586143UL), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(5807308319501155138UL, 10931403895531586143UL);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityNumeratorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(0.9074074f), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(0.9074074f);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityNumeratorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(0.9074074f), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(0.9074074f);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityNumeratorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(0.9074074f), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(0.9074074f);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityNumeratorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(0.9074074f), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(0.9074074f);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityNumeratorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(0.3148148f, 0.5925926f), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(0.3148148f, 0.5925926f);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityNumeratorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(0.9074074074074074074d), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(0.9074074074074074074d);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityNumeratorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(0.9074074074074074074d), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(0.9074074074074074074d);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityNumeratorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(0.9074074074074074074d), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(0.9074074074074074074d);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityNumeratorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(0.9074074074074074074d), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(0.9074074074074074074d);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityNumeratorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(0.3148148148148148148d, 0.5925925925925925926d), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(0.3148148148148148148d, 0.5925925925925925926d);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityDenominatorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(49, 54), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(49, 54);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityDenominatorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(49, 54), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(49, 54);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityDenominatorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(49, 54), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(49, 54);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityDenominatorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(49, 54), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(49, 54);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityDenominatorInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(17, 32, 54), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(17, 32, 54);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityDenominatorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(49U, 54U), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(49U, 54U);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityDenominatorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(49U, 54U), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(49U, 54U);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityDenominatorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(49U, 54U), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(49U, 54U);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityDenominatorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(49U, 54U), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(49U, 54U);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityDenominatorUInt32()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(17U, 32U, 54U), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(17U, 32U, 54U);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityDenominatorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(49L, 54L), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(49L, 54L);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityDenominatorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(49L, 54L), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(49L, 54L);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityDenominatorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(49L, 54L), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(49L, 54L);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityDenominatorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(49L, 54L), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(49L, 54L);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityDenominatorInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(17L, 32L, 54L), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(17L, 32L, 54L);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityDenominatorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(49UL, 54UL), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(49UL, 54UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityDenominatorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(49UL, 54UL), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(49UL, 54UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityDenominatorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(49UL, 54UL), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(49UL, 54UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityDenominatorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(49UL, 54UL), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(49UL, 54UL);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityDenominatorUInt64()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(17UL, 32UL, 54UL), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(17UL, 32UL, 54UL);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityDenominatorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(49f, 54f), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(49f, 54f);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityDenominatorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(49f, 54f), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(49f, 54f);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityDenominatorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(49f, 54f), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(49f, 54f);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityDenominatorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(49f, 54f), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(49f, 54f);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityDenominatorFloat()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(17f, 32f, 54f), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(17f, 32f, 54f);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void WeightedOneProbabilityDenominatorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.OneProbability(49d, 54d), 1, 0, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeOneProbabilityGenerator(49d, 54d);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, 0, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedZeroProbabilityDenominatorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.ZeroProbability(49d, 54d), 0, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeZeroProbabilityGenerator(49d, 54d);
			ValidateBinaryDistribution(10000, () => generator.Next(), 0, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedPositiveProbabilityDenominatorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.PositiveProbability(49d, 54d), 1, -1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakePositiveProbabilityGenerator(49d, 54d);
			ValidateBinaryDistribution(10000, () => generator.Next(), 1, -1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedNegativeProbabilityDenominatorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBinaryDistribution(10000, () => random.NegativeProbability(49d, 54d), -1, 1, 0.9074074f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeNegativeProbabilityGenerator(49d, 54d);
			ValidateBinaryDistribution(10000, () => generator.Next(), -1, 1, 0.9074074f, 0.02f);
		}

		[Test]
		public void WeightedSignProbabilityDenominatorDouble()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateTrinaryDistribution(10000, () => random.SignProbability(17d, 32d, 54d), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
			var generator = XorShift128Plus.Create(seed).MakeSignProbabilityGenerator(17d, 32d, 54d);
			ValidateTrinaryDistribution(10000, () => generator.Next(), 1, -1, 0, 0.3148148f, 0.5925925926f, 0.02f);
		}

		[Test]
		public void ValidateAngleDeg()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleDegOO(), 0f, 360f, Assert.Greater, Assert.Less);
			var generator = XorShift128Plus.Create(seed).MakeAngleDegOOGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, 360f, Assert.Greater, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleDegCO(), 0f, 360f, Assert.GreaterOrEqual, Assert.Less);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCOGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, 360f, Assert.GreaterOrEqual, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleDegOC(), 0f, 360f, Assert.Greater, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleDegOCGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, 360f, Assert.Greater, Assert.LessOrEqual);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleDegCC(), 0f, 360f, Assert.GreaterOrEqual, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCCGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, 360f, Assert.GreaterOrEqual, Assert.LessOrEqual);
		}

		[Test]
		public void ValidateAngleRad()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleRadOO(), 0f, Mathf.PI * 2f, Assert.Greater, Assert.Less);
			var generator = XorShift128Plus.Create(seed).MakeAngleRadOOGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, Mathf.PI * 2f, Assert.Greater, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleRadCO(), 0f, Mathf.PI * 2f, Assert.GreaterOrEqual, Assert.Less);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCOGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, Mathf.PI * 2f, Assert.GreaterOrEqual, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleRadOC(), 0f, Mathf.PI * 2f, Assert.Greater, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleRadOCGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, Mathf.PI * 2f, Assert.Greater, Assert.LessOrEqual);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.AngleRadCC(), 0f, Mathf.PI * 2f, Assert.GreaterOrEqual, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCCGenerator(false, false);
			ValidateAngleRange(10000, () => generator.Next(), 0f, Mathf.PI * 2f, Assert.GreaterOrEqual, Assert.LessOrEqual);
		}

		[Test]
		public void ValidateSignedAngleDeg()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleDegOO(), -180f, +180f, Assert.Greater, Assert.Less);
			var generator = XorShift128Plus.Create(seed).MakeAngleDegOOGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -180f, +180f, Assert.Greater, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleDegCO(), -180f, +180f, Assert.GreaterOrEqual, Assert.Less);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCOGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -180f, +180f, Assert.GreaterOrEqual, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleDegOC(), -180f, +180f, Assert.Greater, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleDegOCGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -180f, +180f, Assert.Greater, Assert.LessOrEqual);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleDegCC(), -180f, +180f, Assert.GreaterOrEqual, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCCGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -180f, +180f, Assert.GreaterOrEqual, Assert.LessOrEqual);
		}

		[Test]
		public void ValidateSignedAngleRad()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleRadOO(), -Mathf.PI, +Mathf.PI, Assert.Greater, Assert.Less);
			var generator = XorShift128Plus.Create(seed).MakeAngleRadOOGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI, +Mathf.PI, Assert.Greater, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleRadCO(), -Mathf.PI, +Mathf.PI, Assert.GreaterOrEqual, Assert.Less);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCOGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI, +Mathf.PI, Assert.GreaterOrEqual, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleRadOC(), -Mathf.PI, +Mathf.PI, Assert.Greater, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleRadOCGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI, +Mathf.PI, Assert.Greater, Assert.LessOrEqual);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedAngleRadCC(), -Mathf.PI, +Mathf.PI, Assert.GreaterOrEqual, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCCGenerator(true, false);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI, +Mathf.PI, Assert.GreaterOrEqual, Assert.LessOrEqual);
		}

		[Test]
		public void ValidateHalfAngleDeg()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleDegOO(), 0f, +180f, Assert.Greater, Assert.Less);
			var generator = XorShift128Plus.Create(seed).MakeAngleDegOOGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +180f, Assert.Greater, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleDegCO(), 0f, +180f, Assert.GreaterOrEqual, Assert.Less);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCOGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +180f, Assert.GreaterOrEqual, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleDegOC(), 0f, +180f, Assert.Greater, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleDegOCGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +180f, Assert.Greater, Assert.LessOrEqual);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleDegCC(), 0f, +180f, Assert.GreaterOrEqual, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCCGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +180f, Assert.GreaterOrEqual, Assert.LessOrEqual);
		}

		[Test]
		public void ValidateHalfAngleRad()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleRadOO(), 0f, +Mathf.PI, Assert.Greater, Assert.Less);
			var generator = XorShift128Plus.Create(seed).MakeAngleRadOOGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +Mathf.PI, Assert.Greater, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleRadCO(), 0f, +Mathf.PI, Assert.GreaterOrEqual, Assert.Less);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCOGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +Mathf.PI, Assert.GreaterOrEqual, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleRadOC(), 0f, +Mathf.PI, Assert.Greater, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleRadOCGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +Mathf.PI, Assert.Greater, Assert.LessOrEqual);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.HalfAngleRadCC(), 0f, +Mathf.PI, Assert.GreaterOrEqual, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCCGenerator(false, true);
			ValidateAngleRange(10000, () => generator.Next(), 0f, +Mathf.PI, Assert.GreaterOrEqual, Assert.LessOrEqual);
		}

		[Test]
		public void ValidateSignedHalfAngleDeg()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleDegOO(), -90f, +90f, Assert.Greater, Assert.Less);
			var generator = XorShift128Plus.Create(seed).MakeAngleDegOOGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -90f, +90f, Assert.Greater, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleDegCO(), -90f, +90f, Assert.GreaterOrEqual, Assert.Less);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCOGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -90f, +90f, Assert.GreaterOrEqual, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleDegOC(), -90f, +90f, Assert.Greater, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleDegOCGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -90f, +90f, Assert.Greater, Assert.LessOrEqual);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleDegCC(), -90f, +90f, Assert.GreaterOrEqual, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleDegCCGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -90f, +90f, Assert.GreaterOrEqual, Assert.LessOrEqual);
		}

		[Test]
		public void ValidateSignedHalfAngleRad()
		{
			var random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleRadOO(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.Greater, Assert.Less);
			var generator = XorShift128Plus.Create(seed).MakeAngleRadOOGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.Greater, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleRadCO(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.GreaterOrEqual, Assert.Less);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCOGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.GreaterOrEqual, Assert.Less);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleRadOC(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.Greater, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleRadOCGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.Greater, Assert.LessOrEqual);

			random = XorShift128Plus.Create(seed);
			ValidateAngleRange(10000, () => random.SignedHalfAngleRadCC(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.GreaterOrEqual, Assert.LessOrEqual);
			generator = XorShift128Plus.Create(seed).MakeAngleRadCCGenerator(true, true);
			ValidateAngleRange(10000, () => generator.Next(), -Mathf.PI / 2f, +Mathf.PI / 2f, Assert.GreaterOrEqual, Assert.LessOrEqual);
		}
	}
}
#endif
