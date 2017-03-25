/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;
using UnityEngine;
using System;

namespace Experilous.MakeItRandom.Tests
{
	class RandomBitsTests
	{
		private const string seed = "random seed";

		private void ValidateBitDistribution32(int count, Func<uint> generator, int bitCount, float tolerance, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

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

		private void ValidateBitDistribution64(int count, Func<ulong> generator, int bitCount, float tolerance, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

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

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void UniformBit(int sampleSizePercentage)
		{
			var random = XorShift128Plus.Create(seed);
			ValidateBitDistribution32(10000, () => random.Bit(), 1, 0.1f, sampleSizePercentage);
			var generator = XorShift128Plus.Create(seed).MakeBitGenerator();
			ValidateBitDistribution32(10000, () => generator.Next(), 1, 0.1f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void UniformBits32(int sampleSizePercentage)
		{
			for (int i = 1; i <= 32; ++i)
			{
				var random = XorShift128Plus.Create(seed);
				ValidateBitDistribution32(10000, () => random.Bits32(i), i, 0.1f, sampleSizePercentage);
				var generator = XorShift128Plus.Create(seed).MakeBits32Generator(i);
				ValidateBitDistribution32(10000, () => generator.Next(), i, 0.1f, sampleSizePercentage);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void UniformBits64(int sampleSizePercentage)
		{
			for (int i = 1; i <= 64; ++i)
			{
				var random = XorShift128Plus.Create(seed);
				ValidateBitDistribution64(10000, () => random.Bits64(i), i, 0.1f, sampleSizePercentage);
				var generator = XorShift128Plus.Create(seed).MakeBits64Generator(i);
				ValidateBitDistribution64(10000, () => generator.Next(), i, 0.1f, sampleSizePercentage);
			}
		}
	}
}
#endif
