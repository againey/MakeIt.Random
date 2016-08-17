/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.MakeItRandom.Tests
{
	class RandomeEngineTests
	{
		private const string seed = "random seed";

		public static float CalculateStandardDeviation(int[] buckets, int hitsPerBucket)
		{
			float devianceSum = 0f;

			foreach (var bucket in buckets)
			{
				float difference = bucket - hitsPerBucket;
				devianceSum += difference * difference;
			}

			return Mathf.Sqrt(devianceSum / buckets.Length);
		}

		public static void ValidateNext32BitsDistribution(IRandom random, int bitCount, int hitsPerBucket, float tolerance)
		{
			int bucketCount = 1;
			for (int i = 0; i < bitCount; ++i) bucketCount = bucketCount * 2;
			var buckets = new int[bucketCount];
			uint mask = ~(0xFFFFFFFFU << bitCount);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.Next32() & mask;
				buckets[n] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateNext64BitsDistribution(IRandom random, int bitCount, int hitsPerBucket, float tolerance)
		{
			int bucketCount = 1;
			for (int i = 0; i < bitCount; ++i) bucketCount = bucketCount * 2;
			var buckets = new int[bucketCount];
			ulong mask = ~(0xFFFFFFFFFFFFFFFFUL << bitCount);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var n = random.Next64() & mask;
				buckets[n] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateChanceDistribution(IRandom random, int numerator, int denominator, int trialCount, float tolerance)
		{
			int falseCount = 0;
			int trueCount = 0;
			for (int i = 0; i < trialCount; ++i)
			{
				if (random.Probability(numerator, denominator))
				{
					++trueCount;
				}
				else
				{
					++falseCount;
				}
			}

			Assert.LessOrEqual(Mathf.Abs((float)numerator / denominator - (float)trueCount / trialCount), tolerance, string.Format("{0}/{1}", numerator, denominator));
		}

		[Test]
		public void Next4BitsDistribution()
		{
			ValidateNext32BitsDistribution(SplitMix64.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(SystemRandom.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(UnityRandom.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(XoroShiro128Plus.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(XorShift1024Star.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(XorShift128Plus.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(XorShiftAdd.Create(seed), 4, 10000, 0.02f);

			ValidateNext64BitsDistribution(SplitMix64.Create(seed), 4, 10000, 0.02f);
			ValidateNext64BitsDistribution(SystemRandom.Create(seed), 4, 10000, 0.02f);
			ValidateNext64BitsDistribution(UnityRandom.Create(seed), 4, 10000, 0.02f);
			ValidateNext64BitsDistribution(XoroShiro128Plus.Create(seed), 4, 10000, 0.02f);
			ValidateNext64BitsDistribution(XorShift1024Star.Create(seed), 4, 10000, 0.02f);
			ValidateNext64BitsDistribution(XorShift128Plus.Create(seed), 4, 10000, 0.02f);
			ValidateNext64BitsDistribution(XorShiftAdd.Create(seed), 4, 10000, 0.02f);
		}

		[Test]
		public void Next5BitsDistribution()
		{
			ValidateNext32BitsDistribution(SplitMix64.Create(seed), 5, 10000, 0.03f);
			ValidateNext32BitsDistribution(SystemRandom.Create(seed), 5, 10000, 0.03f);
			ValidateNext32BitsDistribution(UnityRandom.Create(seed), 5, 10000, 0.03f);
			ValidateNext32BitsDistribution(XoroShiro128Plus.Create(seed), 5, 10000, 0.03f);
			ValidateNext32BitsDistribution(XorShift1024Star.Create(seed), 5, 10000, 0.03f);
			ValidateNext32BitsDistribution(XorShift128Plus.Create(seed), 5, 10000, 0.03f);
			ValidateNext32BitsDistribution(XorShiftAdd.Create(seed), 5, 10000, 0.03f);

			ValidateNext64BitsDistribution(SplitMix64.Create(seed), 5, 10000, 0.03f);
			ValidateNext64BitsDistribution(SystemRandom.Create(seed), 5, 10000, 0.03f);
			ValidateNext64BitsDistribution(UnityRandom.Create(seed), 5, 10000, 0.03f);
			ValidateNext64BitsDistribution(XoroShiro128Plus.Create(seed), 5, 10000, 0.03f);
			ValidateNext64BitsDistribution(XorShift1024Star.Create(seed), 5, 10000, 0.03f);
			ValidateNext64BitsDistribution(XorShift128Plus.Create(seed), 5, 10000, 0.03f);
			ValidateNext64BitsDistribution(XorShiftAdd.Create(seed), 5, 10000, 0.03f);
		}

		[Test]
		public void ChancePowerOfTwoDenominatorDistribution()
		{
			ValidateChanceDistribution(SplitMix64.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(SystemRandom.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(UnityRandom.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(XoroShiro128Plus.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(XorShift1024Star.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(XorShift128Plus.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(XorShiftAdd.Create(seed), 25, 32, 100000, 0.002f);
		}

		[Test]
		public void ChanceNonPowerOfTwoDenominatorDistribution()
		{
			ValidateChanceDistribution(SplitMix64.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(SystemRandom.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(UnityRandom.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(XoroShiro128Plus.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(XorShift1024Star.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(XorShift128Plus.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(XorShiftAdd.Create(seed), 17, 25, 100000, 0.003f);
		}

		[Test]
		public void ChanceDistribution()
		{
			System.Action<int, int> validateRatio = (int numerator, int denominator) =>
			{
				ValidateChanceDistribution(SplitMix64.Create(seed), numerator, denominator, 1000, 0.04f);
				ValidateChanceDistribution(SystemRandom.Create(seed), numerator, denominator, 1000, 0.04f);
				ValidateChanceDistribution(UnityRandom.Create(seed), numerator, denominator, 10000, 0.04f);
				ValidateChanceDistribution(XoroShiro128Plus.Create(seed), numerator, denominator, 10000, 0.04f);
				ValidateChanceDistribution(XorShift1024Star.Create(seed), numerator, denominator, 10000, 0.04f);
				ValidateChanceDistribution(XorShift128Plus.Create(seed), numerator, denominator, 10000, 0.04f);
				ValidateChanceDistribution(XorShiftAdd.Create(seed), numerator, denominator, 10000, 0.04f);
			};

			for (int denominator = 1; denominator < 20; ++denominator)
			{
				for (int numerator = 0; numerator <= denominator; ++numerator)
				{
					validateRatio(numerator, denominator);
				}
			}

			System.Action<int, int> validateDenominatorRange = (int averageDenominator, int range) =>
			{
				for (int denominator = averageDenominator - range; denominator <= averageDenominator + range; ++denominator)
				{
					for (int numerator = 0; numerator <= range * 2; ++numerator)
					{
						validateRatio(numerator, denominator);
					}

					for (int numerator = averageDenominator / 5; numerator <= averageDenominator / 5 + range * 2; ++numerator)
					{
						validateRatio(numerator, denominator);
					}

					for (int numerator = averageDenominator / 2 - range; numerator <= averageDenominator / 2 + range; ++numerator)
					{
						validateRatio(numerator, denominator);
					}

					for (int numerator = averageDenominator * 4 / 5 - range * 2; numerator <= averageDenominator * 4 / 5; ++numerator)
					{
						validateRatio(numerator, denominator);
					}

					for (int numerator = averageDenominator - range * 2; numerator <= averageDenominator; ++numerator)
					{
						validateRatio(numerator, denominator);
					}
				}
			};

			validateDenominatorRange(197, 2);
			validateDenominatorRange(544, 2);
			validateDenominatorRange(1934, 2);
			validateDenominatorRange(70000, 2);
			validateDenominatorRange(16000000, 2);
			validateDenominatorRange(17000000, 2);
			validateDenominatorRange(500000000, 2);
		}

		[Test]
		public void First32_64_SplitMix64()
		{
			SplitMix64 random;

			random = SplitMix64.CreateWithState(0x0000000000000000UL);
			Assert.AreEqual(0xE220A8397B1DCDAFUL, random.Next64());
			Assert.AreEqual(0x6E789E6AA1B965F4UL, random.Next64());
			Assert.AreEqual(0x06C45D188009454FUL, random.Next64());
			Assert.AreEqual(0xF88BB8A8724C81ECUL, random.Next64());
			Assert.AreEqual(0x1B39896A51A8749BUL, random.Next64());
			Assert.AreEqual(0x53CB9F0C747EA2EAUL, random.Next64());
			Assert.AreEqual(0x2C829ABE1F4532E1UL, random.Next64());
			Assert.AreEqual(0xC584133AC916AB3CUL, random.Next64());
			Assert.AreEqual(0x3EE5789041C98AC3UL, random.Next64());
			Assert.AreEqual(0xF3B8488C368CB0A6UL, random.Next64());
			Assert.AreEqual(0x657EECDD3CB13D09UL, random.Next64());
			Assert.AreEqual(0xC2D326E0055BDEF6UL, random.Next64());
			Assert.AreEqual(0x8621A03FE0BBDB7BUL, random.Next64());
			Assert.AreEqual(0x8E1F7555983AA92FUL, random.Next64());
			Assert.AreEqual(0xB54E0F1600CC4D19UL, random.Next64());
			Assert.AreEqual(0x84BB3F97971D80ABUL, random.Next64());
			Assert.AreEqual(0x7D29825C75521255UL, random.Next64());
			Assert.AreEqual(0xC3CF17102B7F7F86UL, random.Next64());
			Assert.AreEqual(0x3466E9A083914F64UL, random.Next64());
			Assert.AreEqual(0xD81A8D2B5A4485ACUL, random.Next64());
			Assert.AreEqual(0xDB01602B100B9ED7UL, random.Next64());
			Assert.AreEqual(0xA9038A921825F10DUL, random.Next64());
			Assert.AreEqual(0xEDF5F1D90DCA2F6AUL, random.Next64());
			Assert.AreEqual(0x54496AD67BD2634CUL, random.Next64());
			Assert.AreEqual(0xDD7C01D4F5407269UL, random.Next64());
			Assert.AreEqual(0x935E82F1DB4C4F7BUL, random.Next64());
			Assert.AreEqual(0x69B82EBC92233300UL, random.Next64());
			Assert.AreEqual(0x40D29EB57DE1D510UL, random.Next64());
			Assert.AreEqual(0xA2F09DABB45C6316UL, random.Next64());
			Assert.AreEqual(0xEE521D7A0F4D3872UL, random.Next64());
			Assert.AreEqual(0xF16952EE72F3454FUL, random.Next64());
			Assert.AreEqual(0x377D35DEA8E40225UL, random.Next64());

			random = SplitMix64.CreateWithState(0xBFE039B631439F10UL);
			Assert.AreEqual(0x8BD814C4F78A92AAUL, random.Next64());
			Assert.AreEqual(0x53F878933D986DA9UL, random.Next64());
			Assert.AreEqual(0x411655BC8B4B086AUL, random.Next64());
			Assert.AreEqual(0x69A9026B806CC78AUL, random.Next64());
			Assert.AreEqual(0xCAB888EB2968D7ABUL, random.Next64());
			Assert.AreEqual(0x02B49D11F9B16233UL, random.Next64());
			Assert.AreEqual(0x1589D8752CC5F881UL, random.Next64());
			Assert.AreEqual(0xF3ACB90DCB4260E4UL, random.Next64());
			Assert.AreEqual(0x2DAFEBDAAA673C3FUL, random.Next64());
			Assert.AreEqual(0xFDF2C53DED054777UL, random.Next64());
			Assert.AreEqual(0x0C0B1FA1D1B3BE94UL, random.Next64());
			Assert.AreEqual(0xC37FB540E3C5F2B4UL, random.Next64());
			Assert.AreEqual(0x79C0175F4E05E9DEUL, random.Next64());
			Assert.AreEqual(0x01CA25C2706DEBACUL, random.Next64());
			Assert.AreEqual(0xD57127C870E56006UL, random.Next64());
			Assert.AreEqual(0x45E7660BD15907A6UL, random.Next64());
			Assert.AreEqual(0x3D25F5D1C63F1630UL, random.Next64());
			Assert.AreEqual(0xB73BF1CD37356BEFUL, random.Next64());
			Assert.AreEqual(0x2F7D9A997CDE36C5UL, random.Next64());
			Assert.AreEqual(0xBC5CD5B2233F13C9UL, random.Next64());
			Assert.AreEqual(0xB3AD43ABA225EC27UL, random.Next64());
			Assert.AreEqual(0xF21E0B7759313E5DUL, random.Next64());
			Assert.AreEqual(0x36B8FF720B2CA095UL, random.Next64());
			Assert.AreEqual(0x32FE225672665C35UL, random.Next64());
			Assert.AreEqual(0xF335F267163644C0UL, random.Next64());
			Assert.AreEqual(0x4026941109B4A9D1UL, random.Next64());
			Assert.AreEqual(0x00E852A92213B15EUL, random.Next64());
			Assert.AreEqual(0x25947D969BA28545UL, random.Next64());
			Assert.AreEqual(0x4A7F1845F970A2E2UL, random.Next64());
			Assert.AreEqual(0xB04B35347CF097DFUL, random.Next64());
			Assert.AreEqual(0x869B268FB833241AUL, random.Next64());
			Assert.AreEqual(0xC4DEBB5DFAE41E1AUL, random.Next64());

			random = SplitMix64.CreateWithState(0xFFFFFFFFFFFFFFFFUL);
			Assert.AreEqual(0xE4D971771B652C20UL, random.Next64());
			Assert.AreEqual(0xE99FF867DBF682C9UL, random.Next64());
			Assert.AreEqual(0x382FF84CB27281E9UL, random.Next64());
			Assert.AreEqual(0x6D1DB36CCBA982D2UL, random.Next64());
			Assert.AreEqual(0xB4A0472E578069AEUL, random.Next64());
			Assert.AreEqual(0xD31DADBDA438BB33UL, random.Next64());
			Assert.AreEqual(0xF14F2CF802083FA5UL, random.Next64());
			Assert.AreEqual(0x405DA438A39E8064UL, random.Next64());
			Assert.AreEqual(0xC4FEA708156E0C84UL, random.Next64());
			Assert.AreEqual(0x031E50FE7BBD6E1CUL, random.Next64());
			Assert.AreEqual(0x03B234961E71CF15UL, random.Next64());
			Assert.AreEqual(0xCE755952D3025DA7UL, random.Next64());
			Assert.AreEqual(0x01C9558BD006BADBUL, random.Next64());
			Assert.AreEqual(0xDD90E10F6F7C1C8AUL, random.Next64());
			Assert.AreEqual(0x354D0DF8B25878C1UL, random.Next64());
			Assert.AreEqual(0xACEEA13CA07E34E8UL, random.Next64());
			Assert.AreEqual(0x6887829E84A5E267UL, random.Next64());
			Assert.AreEqual(0x312B54A59456E8D2UL, random.Next64());
			Assert.AreEqual(0x2310BD4ABE96EA03UL, random.Next64());
			Assert.AreEqual(0x6C5EDF5C23BE2179UL, random.Next64());
			Assert.AreEqual(0x5D4BEE7ACCC8783DUL, random.Next64());
			Assert.AreEqual(0xAC2CC6679BA3863FUL, random.Next64());
			Assert.AreEqual(0x78A0A3EB16C17603UL, random.Next64());
			Assert.AreEqual(0x31BAFC61D077172EUL, random.Next64());
			Assert.AreEqual(0x2F36EB3751D114E2UL, random.Next64());
			Assert.AreEqual(0x9862F4D38A9CDC26UL, random.Next64());
			Assert.AreEqual(0xD870587C607F67B4UL, random.Next64());
			Assert.AreEqual(0x728BBE64523CED67UL, random.Next64());
			Assert.AreEqual(0xC1AF2B37C863DA48UL, random.Next64());
			Assert.AreEqual(0x0477DE91B96B7B43UL, random.Next64());
			Assert.AreEqual(0x24BDF605EE188704UL, random.Next64());
			Assert.AreEqual(0xDE2B5DB652A541FEUL, random.Next64());
		}

		[Test]
		public void First32_64_XoroShiro128Plus()
		{
			XoroShiro128Plus random;

			random = XoroShiro128Plus.CreateWithState(0x0000000000000000UL, 0x0000000000000001UL);
			Assert.AreEqual(0x0000000000000001UL, random.Next64());
			Assert.AreEqual(0x0000001000004001UL, random.Next64());
			Assert.AreEqual(0x0088002010000121UL, random.Next64());
			Assert.AreEqual(0x1000581020404122UL, random.Next64());
			Assert.AreEqual(0x999062240009A201UL, random.Next64());
			Assert.AreEqual(0x015C042372058451UL, random.Next64());
			Assert.AreEqual(0x2930726029AE01D4UL, random.Next64());
			Assert.AreEqual(0x5F6C375452499B74UL, random.Next64());
			Assert.AreEqual(0xFA13D453113CE773UL, random.Next64());
			Assert.AreEqual(0x9043B5E86E239DBEUL, random.Next64());
			Assert.AreEqual(0xF84ABC29464D8327UL, random.Next64());
			Assert.AreEqual(0xCC1D8ACF3A3D783DUL, random.Next64());
			Assert.AreEqual(0x53CF5B70C3B1F6D2UL, random.Next64());
			Assert.AreEqual(0x7D69BCA468B504AFUL, random.Next64());
			Assert.AreEqual(0x336F46B72D7A9192UL, random.Next64());
			Assert.AreEqual(0xDBF962DF3AC076C7UL, random.Next64());
			Assert.AreEqual(0x6627B3338E5CF5F9UL, random.Next64());
			Assert.AreEqual(0x9F2730512C7AFAF8UL, random.Next64());
			Assert.AreEqual(0xA79E61499C376171UL, random.Next64());
			Assert.AreEqual(0xA9E62D60D13FA6A2UL, random.Next64());
			Assert.AreEqual(0x883AAEE84A6B94EBUL, random.Next64());
			Assert.AreEqual(0xA87E2F5A8EFE1226UL, random.Next64());
			Assert.AreEqual(0x924213FF8122A58DUL, random.Next64());
			Assert.AreEqual(0xA5DBB63B3A7AC7D2UL, random.Next64());
			Assert.AreEqual(0x79E33B9967AE39DAUL, random.Next64());
			Assert.AreEqual(0xDD240C5E70712A84UL, random.Next64());
			Assert.AreEqual(0x879653738FDAC4C2UL, random.Next64());
			Assert.AreEqual(0xEC5921EB0EFFBE45UL, random.Next64());
			Assert.AreEqual(0xEDBA70B6E829646AUL, random.Next64());
			Assert.AreEqual(0x549CBBA2662A093FUL, random.Next64());
			Assert.AreEqual(0x06BFA4A6BFD76D03UL, random.Next64());
			Assert.AreEqual(0x941CFAEA4D28CC8BUL, random.Next64());

			random = XoroShiro128Plus.CreateWithState(0xB8B90B6C08E2904FUL, 0x1C8B7DFC92AA64DDUL);
			Assert.AreEqual(0xD54489689B8CF52CUL, random.Next64());
			Assert.AreEqual(0xC2D955B1548F6EE3UL, random.Next64());
			Assert.AreEqual(0x2B980174774773A6UL, random.Next64());
			Assert.AreEqual(0x1F089072CFC2CBBEUL, random.Next64());
			Assert.AreEqual(0x02717573AB8B114BUL, random.Next64());
			Assert.AreEqual(0x24A2169428DF3493UL, random.Next64());
			Assert.AreEqual(0x045FD5BBE8EE3DC4UL, random.Next64());
			Assert.AreEqual(0x99AF2626ADEF2005UL, random.Next64());
			Assert.AreEqual(0x489928B7877A4028UL, random.Next64());
			Assert.AreEqual(0x6038D08D7AE8E40BUL, random.Next64());
			Assert.AreEqual(0xB4495532A3E0F72DUL, random.Next64());
			Assert.AreEqual(0x04DE7428FF791D8AUL, random.Next64());
			Assert.AreEqual(0xA8B479BC31B62F99UL, random.Next64());
			Assert.AreEqual(0xD0D633CAC63E62EEUL, random.Next64());
			Assert.AreEqual(0x2A217F9A72BF03AAUL, random.Next64());
			Assert.AreEqual(0x2A8AE37FC8181818UL, random.Next64());
			Assert.AreEqual(0xA20F9F3BB33ABF09UL, random.Next64());
			Assert.AreEqual(0xC3EAE68FEF20C26AUL, random.Next64());
			Assert.AreEqual(0x76BA24DDFD032E72UL, random.Next64());
			Assert.AreEqual(0x06D8404BE687C4F1UL, random.Next64());
			Assert.AreEqual(0x5C7290316BC2C0B8UL, random.Next64());
			Assert.AreEqual(0xAB742778CC86A00DUL, random.Next64());
			Assert.AreEqual(0x8A00597AFC7EB62CUL, random.Next64());
			Assert.AreEqual(0x08747249D348FEE6UL, random.Next64());
			Assert.AreEqual(0x9B196B0FE7C02C3DUL, random.Next64());
			Assert.AreEqual(0x2B5988606F3FC702UL, random.Next64());
			Assert.AreEqual(0x6C1B4A7740ABA647UL, random.Next64());
			Assert.AreEqual(0xCFEFF7210A1432A9UL, random.Next64());
			Assert.AreEqual(0x396DB7B623620C62UL, random.Next64());
			Assert.AreEqual(0xCF299BE1B37B3570UL, random.Next64());
			Assert.AreEqual(0x75FEA95C742E9BC7UL, random.Next64());
			Assert.AreEqual(0x3AFCBD29A23CD8CEUL, random.Next64());

			random = XoroShiro128Plus.CreateWithState(0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFFFFFFFFFFUL);
			Assert.AreEqual(0xFFFFFFFFFFFFFFFEUL, random.Next64());
			Assert.AreEqual(0xFFFFFFFFFFFFFFFFUL, random.Next64());
			Assert.AreEqual(0xFFFFFFFFFFFFBFFFUL, random.Next64());
			Assert.AreEqual(0x0083FFEFF000001FUL, random.Next64());
			Assert.AreEqual(0x0FFFC5E017BFC11EUL, random.Next64());
			Assert.AreEqual(0x74F44DEEFFFC5F00UL, random.Next64());
			Assert.AreEqual(0xFEC9E2153F7F3E0EUL, random.Next64());
			Assert.AreEqual(0x07ABED300464C092UL, random.Next64());
			Assert.AreEqual(0x39D3E8A15100D022UL, random.Next64());
			Assert.AreEqual(0xB5DD54A468AFFFE6UL, random.Next64());
			Assert.AreEqual(0xF4CEC65D65DE3946UL, random.Next64());
			Assert.AreEqual(0x0B2B57DD060DAA46UL, random.Next64());
			Assert.AreEqual(0xA43AB8CA4596F07CUL, random.Next64());
			Assert.AreEqual(0x61B5F66D1E59A41EUL, random.Next64());
			Assert.AreEqual(0xE201B47793B6DB7BUL, random.Next64());
			Assert.AreEqual(0x26899A44FE253152UL, random.Next64());
			Assert.AreEqual(0x974C507C5ED90A28UL, random.Next64());
			Assert.AreEqual(0xD9006D6B8382AA9CUL, random.Next64());
			Assert.AreEqual(0x5B67815578F05CDFUL, random.Next64());
			Assert.AreEqual(0x07C516F49CC406B2UL, random.Next64());
			Assert.AreEqual(0xDD123CA98EDDB13DUL, random.Next64());
			Assert.AreEqual(0xA60315FDA71FE0E0UL, random.Next64());
			Assert.AreEqual(0x99C809EA1C7EF8F2UL, random.Next64());
			Assert.AreEqual(0xD4A07D7645990824UL, random.Next64());
			Assert.AreEqual(0xE6D2ABA7F5980045UL, random.Next64());
			Assert.AreEqual(0x1381E2A3689B70C8UL, random.Next64());
			Assert.AreEqual(0x3246A89C7EF62423UL, random.Next64());
			Assert.AreEqual(0xDDCFA65A9F734DF1UL, random.Next64());
			Assert.AreEqual(0xD82A272886C3AFE2UL, random.Next64());
			Assert.AreEqual(0xD32E0B7BE7905204UL, random.Next64());
			Assert.AreEqual(0x1644D3F20833324AUL, random.Next64());
			Assert.AreEqual(0x1DE5438192A8A3AEUL, random.Next64());
		}

		[Test]
		public void First32_64_XorShift128Plus()
		{
			XorShift128Plus random;

			random = XorShift128Plus.CreateWithState(0x0000000000000000UL, 0x0000000000000001UL);
			Assert.AreEqual(0x0000000000000001UL, random.Next64());
			Assert.AreEqual(0x0000000000000002UL, random.Next64());
			Assert.AreEqual(0x0000000000800021UL, random.Next64());
			Assert.AreEqual(0x0000000000840020UL, random.Next64());
			Assert.AreEqual(0x0000400000882400UL, random.Next64());
			Assert.AreEqual(0x0000800000882921UL, random.Next64());
			Assert.AreEqual(0x000080120008864AUL, random.Next64());
			Assert.AreEqual(0x0000402210048549UL, random.Next64());
			Assert.AreEqual(0x0900401224902429UL, random.Next64());
			Assert.AreEqual(0x0A40801418C04532UL, random.Next64());
			Assert.AreEqual(0x0A8088140A352536UL, random.Next64());
			Assert.AreEqual(0x0A885854168A2915UL, random.Next64());
			Assert.AreEqual(0x0A49289462A9A974UL, random.Next64());
			Assert.AreEqual(0x2A0A609912A5A8B2UL, random.Next64());
			Assert.AreEqual(0x29324A72D0A260A7UL, random.Next64());
			Assert.AreEqual(0x0A2A42706132D965UL, random.Next64());
			Assert.AreEqual(0x063199B2E8A9BD49UL, random.Next64());
			Assert.AreEqual(0x3068727959AC65ACUL, random.Next64());
			Assert.AreEqual(0xB25A7F77065D2396UL, random.Next64());
			Assert.AreEqual(0xA96CB462D77A242BUL, random.Next64());
			Assert.AreEqual(0xAD9AC24E781D976AUL, random.Next64());
			Assert.AreEqual(0x38519D8F283F4B23UL, random.Next64());
			Assert.AreEqual(0xB0D4314BED8F7182UL, random.Next64());
			Assert.AreEqual(0x12381B19E0ED7F5FUL, random.Next64());
			Assert.AreEqual(0x1EFD40A68CE8AEBEUL, random.Next64());
			Assert.AreEqual(0x99B2B3A4080ACD77UL, random.Next64());
			Assert.AreEqual(0xD393DBC0DBCD4A83UL, random.Next64());
			Assert.AreEqual(0x11EA260281015E9EUL, random.Next64());
			Assert.AreEqual(0x18CFAA480171E939UL, random.Next64());
			Assert.AreEqual(0x00E61E553236C3B5UL, random.Next64());
			Assert.AreEqual(0xCD0FFCE83AFD7AEEUL, random.Next64());
			Assert.AreEqual(0xB8B137C50C30EE59UL, random.Next64());

			random = XorShift128Plus.CreateWithState(0xFE59757D17BCB419UL, 0x7A8282D265985C33UL);
			Assert.AreEqual(0x78DBF84F7D55104CUL, random.Next64());
			Assert.AreEqual(0xB406B0A9ACDA4837UL, random.Next64());
			Assert.AreEqual(0x657C7480DA1E8C55UL, random.Next64());
			Assert.AreEqual(0x247883DE0B209858UL, random.Next64());
			Assert.AreEqual(0x78F5491CA4272108UL, random.Next64());
			Assert.AreEqual(0x673FB3B84230FFBFUL, random.Next64());
			Assert.AreEqual(0x7C66BD5A19519290UL, random.Next64());
			Assert.AreEqual(0x358D8D377D1C5EB3UL, random.Next64());
			Assert.AreEqual(0x6B84F40E18F6C985UL, random.Next64());
			Assert.AreEqual(0x50170A8D2D2D9E7AUL, random.Next64());
			Assert.AreEqual(0xFFFF4E05B08F51D1UL, random.Next64());
			Assert.AreEqual(0x65E0AEDAC64463ACUL, random.Next64());
			Assert.AreEqual(0x65423CB8829A8D99UL, random.Next64());
			Assert.AreEqual(0x8E9A9DEA8E4D694FUL, random.Next64());
			Assert.AreEqual(0xC64FE49D49A87C68UL, random.Next64());
			Assert.AreEqual(0x6FFF1E3B05DAD4D4UL, random.Next64());
			Assert.AreEqual(0xFC462ABBCEE91F33UL, random.Next64());
			Assert.AreEqual(0xA9CFA67F4FC22882UL, random.Next64());
			Assert.AreEqual(0x8D3F7DAD9B4FCBE6UL, random.Next64());
			Assert.AreEqual(0xA624F727BD937974UL, random.Next64());
			Assert.AreEqual(0x2DB710F9435C65A2UL, random.Next64());
			Assert.AreEqual(0x52A5423142526EF6UL, random.Next64());
			Assert.AreEqual(0x9A18022E23FF7C57UL, random.Next64());
			Assert.AreEqual(0x6ED9A1AB08D392ADUL, random.Next64());
			Assert.AreEqual(0xFA2D955879B375A5UL, random.Next64());
			Assert.AreEqual(0x5FD79CC1FBC9F73AUL, random.Next64());
			Assert.AreEqual(0x36033BF5E602DAC2UL, random.Next64());
			Assert.AreEqual(0x78A296F2E9F9B496UL, random.Next64());
			Assert.AreEqual(0x37E42556E2A8320BUL, random.Next64());
			Assert.AreEqual(0x34874B7F0EB8DC16UL, random.Next64());
			Assert.AreEqual(0xC9D7A9E63E6F9608UL, random.Next64());
			Assert.AreEqual(0x50A0545BC6E6EF28UL, random.Next64());

			random = XorShift128Plus.CreateWithState(0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFFFFFFFFFFUL);
			Assert.AreEqual(0xFFFFFFFFFFFFFFFEUL, random.Next64());
			Assert.AreEqual(0xF8000000007FFFDFUL, random.Next64());
			Assert.AreEqual(0xF7C000000083FFDFUL, random.Next64());
			Assert.AreEqual(0xFFFE01FFFF87E3FEUL, random.Next64());
			Assert.AreEqual(0x003DD1FFFF07E0FEUL, random.Next64());
			Assert.AreEqual(0x07C19E7E70037DE6UL, random.Next64());
			Assert.AreEqual(0x0FC19C8A70FF6106UL, random.Next64());
			Assert.AreEqual(0x47399E08206BFF26UL, random.Next64());
			Assert.AreEqual(0x7E79BFFC2C7FBB26UL, random.Next64());
			Assert.AreEqual(0x3F2FF63E2B7B9502UL, random.Next64());
			Assert.AreEqual(0xC726364DA258D4E2UL, random.Next64());
			Assert.AreEqual(0xE926FC81D0ED1A19UL, random.Next64());
			Assert.AreEqual(0x01F9BEC08F7BD943UL, random.Next64());
			Assert.AreEqual(0xDFE7B3D33116A51EUL, random.Next64());
			Assert.AreEqual(0x3EE6CD460D87E9C1UL, random.Next64());
			Assert.AreEqual(0x42611AB67005356EUL, random.Next64());
			Assert.AreEqual(0xE01DD1C32117EC81UL, random.Next64());
			Assert.AreEqual(0x809801461FDFD6CDUL, random.Next64());
			Assert.AreEqual(0xC0C1C87E427920BCUL, random.Next64());
			Assert.AreEqual(0xA0E153D2B0E6B2BFUL, random.Next64());
			Assert.AreEqual(0x1DEDA63F6E07ECE8UL, random.Next64());
			Assert.AreEqual(0x92939B50C685453AUL, random.Next64());
			Assert.AreEqual(0x5889CF5A7AE4270CUL, random.Next64());
			Assert.AreEqual(0x2A8EB0CCD4B2F9A6UL, random.Next64());
			Assert.AreEqual(0x77A72B7133172B12UL, random.Next64());
			Assert.AreEqual(0xF7CE609F53308AB1UL, random.Next64());
			Assert.AreEqual(0xC0BF4CB730FA5B06UL, random.Next64());
			Assert.AreEqual(0xF9D4526B847ADD53UL, random.Next64());
			Assert.AreEqual(0xF24C98F4B28186D0UL, random.Next64());
			Assert.AreEqual(0xE61D601A6E79438CUL, random.Next64());
			Assert.AreEqual(0x722E24535F5E184FUL, random.Next64());
			Assert.AreEqual(0xA130FC9973B788EAUL, random.Next64());
		}

		[Test]
		public void First32_64_XorShift1024Star()
		{
			XorShift1024Star random;

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000001UL,
			}, 0);

			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0x0000000000000000UL, random.Next64());
			Assert.AreEqual(0xD7F7D22EAFE7FDB5UL, random.Next64());
			Assert.AreEqual(0xF8C4E5D75917F91FUL, random.Next64());
			Assert.AreEqual(0xD7F7D22EAFE7FDB5UL, random.Next64());
			Assert.AreEqual(0xF8C4E5D75917F91FUL, random.Next64());
			Assert.AreEqual(0xD7F7D22EAFE7FDB5UL, random.Next64());
			Assert.AreEqual(0xF8C4E5D75917F91FUL, random.Next64());
			Assert.AreEqual(0xD7F7D22EAFE7FDB5UL, random.Next64());
			Assert.AreEqual(0xF8C4E5D75917F91FUL, random.Next64());
			Assert.AreEqual(0xD7F7D22EAFE7FDB5UL, random.Next64());
			Assert.AreEqual(0xF8C4E5D75917F91FUL, random.Next64());
			Assert.AreEqual(0xD7F7D22EAFE7FDB5UL, random.Next64());
			Assert.AreEqual(0xF8C4E5D75917F91FUL, random.Next64());
			Assert.AreEqual(0xD7F7D22EAFE7FDB5UL, random.Next64());
			Assert.AreEqual(0xF8C4E5D75917F91FUL, random.Next64());
			Assert.AreEqual(0xD7F7D22EAFE7FDB5UL, random.Next64());
			Assert.AreEqual(0xF8C4E5D75917F91FUL, random.Next64());
			Assert.AreEqual(0xCF5D5C83AFFB6A00UL, random.Next64());
			Assert.AreEqual(0xACA9B680C67ED1B5UL, random.Next64());

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0x66C6A7A04D98CEA4UL,
				0x70446B75A1D77957UL,
				0x42CD0EB869C9AAEFUL,
				0x14DD9C68AD992037UL,
				0x6B222E53825DFFADUL,
				0x99E6D9D773339DB6UL,
				0x710DF769DCEEA754UL,
				0xBA50D0E157F68E29UL,
				0xEC49132D75F4BF16UL,
				0x31A9C3994EDAED8DUL,
				0xA50FD919BA8E2134UL,
				0xE9FB0C8D321CEDBFUL,
				0x466FDC75B9E79489UL,
				0x001C3B8801D51E1BUL,
				0x6F519A67AD2AA194UL,
				0x12D6ED86D7D63A91UL,
			}, 0);

			Assert.AreEqual(0x79C88B8358900701UL, random.Next64());
			Assert.AreEqual(0xD79B4704CEA4CFAFUL, random.Next64());
			Assert.AreEqual(0x5C13F8C1E8494B92UL, random.Next64());
			Assert.AreEqual(0x8143C6DC5BF97C6FUL, random.Next64());
			Assert.AreEqual(0x78DBDC974CE26986UL, random.Next64());
			Assert.AreEqual(0xA28D9372CD801BBDUL, random.Next64());
			Assert.AreEqual(0x4CAA87EE9D84037CUL, random.Next64());
			Assert.AreEqual(0xFE99396E0C151AEAUL, random.Next64());
			Assert.AreEqual(0x8AA44913E4BD3111UL, random.Next64());
			Assert.AreEqual(0xCA30BA0B0586DCB1UL, random.Next64());
			Assert.AreEqual(0xB9BC75D8841DB44EUL, random.Next64());
			Assert.AreEqual(0x70A9B2A092421F93UL, random.Next64());
			Assert.AreEqual(0x19D6D7E1FA00BB6DUL, random.Next64());
			Assert.AreEqual(0x060675CE015CB2ADUL, random.Next64());
			Assert.AreEqual(0x296A71EA7F56810EUL, random.Next64());
			Assert.AreEqual(0xA73F65E17CA6995AUL, random.Next64());
			Assert.AreEqual(0x5D2E6BFE966D73B7UL, random.Next64());
			Assert.AreEqual(0xAA6247FE7709C43EUL, random.Next64());
			Assert.AreEqual(0x51932E349264AFEBUL, random.Next64());
			Assert.AreEqual(0x99B7C8D321D4A7E9UL, random.Next64());
			Assert.AreEqual(0x7E7856E6B9B73F40UL, random.Next64());
			Assert.AreEqual(0xC64A542C54CBD15FUL, random.Next64());
			Assert.AreEqual(0xC2F4D98E29DE452AUL, random.Next64());
			Assert.AreEqual(0x56747394F7977BFAUL, random.Next64());
			Assert.AreEqual(0x7E227DD7D160675CUL, random.Next64());
			Assert.AreEqual(0x2F430C9907BCD471UL, random.Next64());
			Assert.AreEqual(0x5F063840BB94DF53UL, random.Next64());
			Assert.AreEqual(0x0F9EB48238123C5EUL, random.Next64());
			Assert.AreEqual(0x8BFBF3A769585263UL, random.Next64());
			Assert.AreEqual(0x5ADB1240DEE70F02UL, random.Next64());
			Assert.AreEqual(0x9D4313697BDC1FD3UL, random.Next64());
			Assert.AreEqual(0xC3CD3D243769BE35UL, random.Next64());

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
			}, 0);

			Assert.AreEqual(0x3AA6BE86A4B00000UL, random.Next64());
			Assert.AreEqual(0x09FDEC8F0B182265UL, random.Next64());
			Assert.AreEqual(0x2042482344FFDFE6UL, random.Next64());
			Assert.AreEqual(0xEF99762BAB68024BUL, random.Next64());
			Assert.AreEqual(0x3AA6BE86A4B00000UL, random.Next64());
			Assert.AreEqual(0x09FDEC8F0B182265UL, random.Next64());
			Assert.AreEqual(0x2042482344FFDFE6UL, random.Next64());
			Assert.AreEqual(0xEF99762BAB68024BUL, random.Next64());
			Assert.AreEqual(0x3AA6BE86A4B00000UL, random.Next64());
			Assert.AreEqual(0x09FDEC8F0B182265UL, random.Next64());
			Assert.AreEqual(0x2042482344FFDFE6UL, random.Next64());
			Assert.AreEqual(0xEF99762BAB68024BUL, random.Next64());
			Assert.AreEqual(0x3AA6BE86A4B00000UL, random.Next64());
			Assert.AreEqual(0x09FDEC8F0B182265UL, random.Next64());
			Assert.AreEqual(0x2042482344FFDFE6UL, random.Next64());
			Assert.AreEqual(0xEF99762BAB68024BUL, random.Next64());
			Assert.AreEqual(0x1428597B2A849600UL, random.Next64());
			Assert.AreEqual(0xE030CFE76034BAB0UL, random.Next64());
			Assert.AreEqual(0x26AAE5688E7FBB36UL, random.Next64());
			Assert.AreEqual(0xE930D8E661E826FBUL, random.Next64());
			Assert.AreEqual(0x1A90F6C074047150UL, random.Next64());
			Assert.AreEqual(0xD9C832A216B4DF60UL, random.Next64());
			Assert.AreEqual(0x2042482344FFDFE6UL, random.Next64());
			Assert.AreEqual(0xEF99762BAB68024BUL, random.Next64());
			Assert.AreEqual(0x1428597B2A849600UL, random.Next64());
			Assert.AreEqual(0xE030CFE76034BAB0UL, random.Next64());
			Assert.AreEqual(0x26AAE5688E7FBB36UL, random.Next64());
			Assert.AreEqual(0xE930D8E661E826FBUL, random.Next64());
			Assert.AreEqual(0x1A90F6C074047150UL, random.Next64());
			Assert.AreEqual(0xD9C832A216B4DF60UL, random.Next64());
			Assert.AreEqual(0x2042482344FFDFE6UL, random.Next64());
			Assert.AreEqual(0xEF99762BAB68024BUL, random.Next64());
		}

		[Test]
		public void First16_64_XorShiftAdd()
		{
			XorShiftAdd random;

			random = XorShiftAdd.CreateWithState(0x00000000U, 0x00000000U, 0x00000000U, 0x00000001U);
			Assert.AreEqual(0x0000080100000001UL, random.Next64());
			Assert.AreEqual(0x0040000000400800UL, random.Next64());
			Assert.AreEqual(0x0000810100008001UL, random.Next64());
			Assert.AreEqual(0x4048801000480110UL, random.Next64());
			Assert.AreEqual(0x4400192184009001UL, random.Next64());
			Assert.AreEqual(0x4809800000090920UL, random.Next64());
			Assert.AreEqual(0x4888830590010301UL, random.Next64());
			Assert.AreEqual(0x00099202C0910006UL, random.Next64());
			Assert.AreEqual(0x8D9109874D109361UL, random.Next64());
			Assert.AreEqual(0x8849B040C8CA2826UL, random.Next64());
			Assert.AreEqual(0x19A4C30B49231249UL, random.Next64());
			Assert.AreEqual(0xCA51085ADAC34714UL, random.Next64());
			Assert.AreEqual(0x5AB79D02C8445248UL, random.Next64());
			Assert.AreEqual(0x54360645E5A35142UL, random.Next64());
			Assert.AreEqual(0x6626680F513A4212UL, random.Next64());
			Assert.AreEqual(0x4054820056422802UL, random.Next64());

			random = XorShiftAdd.CreateWithState(0xFE59757DU, 0x17BCB419U, 0x7A8282D2U, 0x65985C33U);
			Assert.AreEqual(0xEB9ED877E01ADF05UL, random.Next64());
			Assert.AreEqual(0x2A222B9D045883B9UL, random.Next64());
			Assert.AreEqual(0x86F359207670B2FBUL, random.Next64());
			Assert.AreEqual(0x59FF6268A80DBA5CUL, random.Next64());
			Assert.AreEqual(0xB4B559B11CA057FAUL, random.Next64());
			Assert.AreEqual(0x11EDBC635E7748F0UL, random.Next64());
			Assert.AreEqual(0x2ABCAD87DD875206UL, random.Next64());
			Assert.AreEqual(0x7D43857B9F7B1B7FUL, random.Next64());
			Assert.AreEqual(0x4C9D69E3E06C7C3EUL, random.Next64());
			Assert.AreEqual(0x59C456FCD19C080EUL, random.Next64());
			Assert.AreEqual(0x217B5E0C847459D1UL, random.Next64());
			Assert.AreEqual(0xEAAFEBCBCF757EB1UL, random.Next64());
			Assert.AreEqual(0xFAF2D2E6E2ED8FD2UL, random.Next64());
			Assert.AreEqual(0x7092DA9DA74772AAUL, random.Next64());
			Assert.AreEqual(0xA042B22E76988405UL, random.Next64());
			Assert.AreEqual(0x8EF2E349AA914565UL, random.Next64());

			random = XorShiftAdd.CreateWithState(0xFFFFFFFFU, 0xFFFFFFFFU, 0xFFFFFFFFU, 0xFFFFFFFFU);
			Assert.AreEqual(0xFFFF87FEFFFFFFFEUL, random.Next64());
			Assert.AreEqual(0xF87F0FFEFC3F0FFEUL, random.Next64());
			Assert.AreEqual(0xBC7F78FEF87F0FFEUL, random.Next64());
			Assert.AreEqual(0xC088601E8087E10EUL, random.Next64());
			Assert.AreEqual(0x780076FE3C80E01EUL, random.Next64());
			Assert.AreEqual(0xB8EE0C3D74370D0EUL, random.Next64());
			Assert.AreEqual(0x3D3679E03CEE073CUL, random.Next64());
			Assert.AreEqual(0x377CC781C17E3183UL, random.Next64());
			Assert.AreEqual(0x193D8615C34C1CE0UL, random.Next64());
			Assert.AreEqual(0x64C31AADB4110FEBUL, random.Next64());
			Assert.AreEqual(0x72FF76E7C4ED077EUL, random.Next64());
			Assert.AreEqual(0x94E84A89DE7FAEF0UL, random.Next64());
			Assert.AreEqual(0x5D93DC7543625B76UL, random.Next64());
			Assert.AreEqual(0x32528C0119245640UL, random.Next64());
			Assert.AreEqual(0xA4E9CCD77AB4C8E4UL, random.Next64());
			Assert.AreEqual(0x73D211F5F40250A8UL, random.Next64());
		}

		[Test]
		public void First32_32_XorShiftAdd()
		{
			XorShiftAdd random;

			random = XorShiftAdd.CreateWithState(0x00000000U, 0x00000000U, 0x00000000U, 0x00000001U);
			Assert.AreEqual(0x00000001U, random.Next32());
			Assert.AreEqual(0x00000801U, random.Next32());
			Assert.AreEqual(0x00400800U, random.Next32());
			Assert.AreEqual(0x00400000U, random.Next32());
			Assert.AreEqual(0x00008001U, random.Next32());
			Assert.AreEqual(0x00008101U, random.Next32());
			Assert.AreEqual(0x00480110U, random.Next32());
			Assert.AreEqual(0x40488010U, random.Next32());
			Assert.AreEqual(0x84009001U, random.Next32());
			Assert.AreEqual(0x44001921U, random.Next32());
			Assert.AreEqual(0x00090920U, random.Next32());
			Assert.AreEqual(0x48098000U, random.Next32());
			Assert.AreEqual(0x90010301U, random.Next32());
			Assert.AreEqual(0x48888305U, random.Next32());
			Assert.AreEqual(0xC0910006U, random.Next32());
			Assert.AreEqual(0x00099202U, random.Next32());
			Assert.AreEqual(0x4D109361U, random.Next32());
			Assert.AreEqual(0x8D910987U, random.Next32());
			Assert.AreEqual(0xC8CA2826U, random.Next32());
			Assert.AreEqual(0x8849B040U, random.Next32());
			Assert.AreEqual(0x49231249U, random.Next32());
			Assert.AreEqual(0x19A4C30BU, random.Next32());
			Assert.AreEqual(0xDAC34714U, random.Next32());
			Assert.AreEqual(0xCA51085AU, random.Next32());
			Assert.AreEqual(0xC8445248U, random.Next32());
			Assert.AreEqual(0x5AB79D02U, random.Next32());
			Assert.AreEqual(0xE5A35142U, random.Next32());
			Assert.AreEqual(0x54360645U, random.Next32());
			Assert.AreEqual(0x513A4212U, random.Next32());
			Assert.AreEqual(0x6626680FU, random.Next32());
			Assert.AreEqual(0x56422802U, random.Next32());
			Assert.AreEqual(0x40548200U, random.Next32());

			random = XorShiftAdd.CreateWithState(0xFE59757DU, 0x17BCB419U, 0x7A8282D2U, 0x65985C33U);
			Assert.AreEqual(0xE01ADF05U, random.Next32());
			Assert.AreEqual(0xEB9ED877U, random.Next32());
			Assert.AreEqual(0x045883B9U, random.Next32());
			Assert.AreEqual(0x2A222B9DU, random.Next32());
			Assert.AreEqual(0x7670B2FBU, random.Next32());
			Assert.AreEqual(0x86F35920U, random.Next32());
			Assert.AreEqual(0xA80DBA5CU, random.Next32());
			Assert.AreEqual(0x59FF6268U, random.Next32());
			Assert.AreEqual(0x1CA057FAU, random.Next32());
			Assert.AreEqual(0xB4B559B1U, random.Next32());
			Assert.AreEqual(0x5E7748F0U, random.Next32());
			Assert.AreEqual(0x11EDBC63U, random.Next32());
			Assert.AreEqual(0xDD875206U, random.Next32());
			Assert.AreEqual(0x2ABCAD87U, random.Next32());
			Assert.AreEqual(0x9F7B1B7FU, random.Next32());
			Assert.AreEqual(0x7D43857BU, random.Next32());
			Assert.AreEqual(0xE06C7C3EU, random.Next32());
			Assert.AreEqual(0x4C9D69E3U, random.Next32());
			Assert.AreEqual(0xD19C080EU, random.Next32());
			Assert.AreEqual(0x59C456FCU, random.Next32());
			Assert.AreEqual(0x847459D1U, random.Next32());
			Assert.AreEqual(0x217B5E0CU, random.Next32());
			Assert.AreEqual(0xCF757EB1U, random.Next32());
			Assert.AreEqual(0xEAAFEBCBU, random.Next32());
			Assert.AreEqual(0xE2ED8FD2U, random.Next32());
			Assert.AreEqual(0xFAF2D2E6U, random.Next32());
			Assert.AreEqual(0xA74772AAU, random.Next32());
			Assert.AreEqual(0x7092DA9DU, random.Next32());
			Assert.AreEqual(0x76988405U, random.Next32());
			Assert.AreEqual(0xA042B22EU, random.Next32());
			Assert.AreEqual(0xAA914565U, random.Next32());
			Assert.AreEqual(0x8EF2E349U, random.Next32());

			random = XorShiftAdd.CreateWithState(0xFFFFFFFFU, 0xFFFFFFFFU, 0xFFFFFFFFU, 0xFFFFFFFFU);
			Assert.AreEqual(0xFFFFFFFEU, random.Next32());
			Assert.AreEqual(0xFFFF87FEU, random.Next32());
			Assert.AreEqual(0xFC3F0FFEU, random.Next32());
			Assert.AreEqual(0xF87F0FFEU, random.Next32());
			Assert.AreEqual(0xF87F0FFEU, random.Next32());
			Assert.AreEqual(0xBC7F78FEU, random.Next32());
			Assert.AreEqual(0x8087E10EU, random.Next32());
			Assert.AreEqual(0xC088601EU, random.Next32());
			Assert.AreEqual(0x3C80E01EU, random.Next32());
			Assert.AreEqual(0x780076FEU, random.Next32());
			Assert.AreEqual(0x74370D0EU, random.Next32());
			Assert.AreEqual(0xB8EE0C3DU, random.Next32());
			Assert.AreEqual(0x3CEE073CU, random.Next32());
			Assert.AreEqual(0x3D3679E0U, random.Next32());
			Assert.AreEqual(0xC17E3183U, random.Next32());
			Assert.AreEqual(0x377CC781U, random.Next32());
			Assert.AreEqual(0xC34C1CE0U, random.Next32());
			Assert.AreEqual(0x193D8615U, random.Next32());
			Assert.AreEqual(0xB4110FEBU, random.Next32());
			Assert.AreEqual(0x64C31AADU, random.Next32());
			Assert.AreEqual(0xC4ED077EU, random.Next32());
			Assert.AreEqual(0x72FF76E7U, random.Next32());
			Assert.AreEqual(0xDE7FAEF0U, random.Next32());
			Assert.AreEqual(0x94E84A89U, random.Next32());
			Assert.AreEqual(0x43625B76U, random.Next32());
			Assert.AreEqual(0x5D93DC75U, random.Next32());
			Assert.AreEqual(0x19245640U, random.Next32());
			Assert.AreEqual(0x32528C01U, random.Next32());
			Assert.AreEqual(0x7AB4C8E4U, random.Next32());
			Assert.AreEqual(0xA4E9CCD7U, random.Next32());
			Assert.AreEqual(0xF40250A8U, random.Next32());
			Assert.AreEqual(0x73D211F5U, random.Next32());
		}

		[Test]
		public void First32_32x2_SplitMix64()
		{
			SplitMix64 random;

			uint lower, upper;

			random = SplitMix64.CreateWithState(0x0000000000000000UL);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7B1DCDAFU, lower); Assert.AreEqual(0xE220A839U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA1B965F4U, lower); Assert.AreEqual(0x6E789E6AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8009454FU, lower); Assert.AreEqual(0x06C45D18U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x724C81ECU, lower); Assert.AreEqual(0xF88BB8A8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x51A8749BU, lower); Assert.AreEqual(0x1B39896AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x747EA2EAU, lower); Assert.AreEqual(0x53CB9F0CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x1F4532E1U, lower); Assert.AreEqual(0x2C829ABEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC916AB3CU, lower); Assert.AreEqual(0xC584133AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x41C98AC3U, lower); Assert.AreEqual(0x3EE57890U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x368CB0A6U, lower); Assert.AreEqual(0xF3B8488CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3CB13D09U, lower); Assert.AreEqual(0x657EECDDU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x055BDEF6U, lower); Assert.AreEqual(0xC2D326E0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE0BBDB7BU, lower); Assert.AreEqual(0x8621A03FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x983AA92FU, lower); Assert.AreEqual(0x8E1F7555U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00CC4D19U, lower); Assert.AreEqual(0xB54E0F16U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x971D80ABU, lower); Assert.AreEqual(0x84BB3F97U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x75521255U, lower); Assert.AreEqual(0x7D29825CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2B7F7F86U, lower); Assert.AreEqual(0xC3CF1710U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x83914F64U, lower); Assert.AreEqual(0x3466E9A0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5A4485ACU, lower); Assert.AreEqual(0xD81A8D2BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x100B9ED7U, lower); Assert.AreEqual(0xDB01602BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x1825F10DU, lower); Assert.AreEqual(0xA9038A92U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0DCA2F6AU, lower); Assert.AreEqual(0xEDF5F1D9U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7BD2634CU, lower); Assert.AreEqual(0x54496AD6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF5407269U, lower); Assert.AreEqual(0xDD7C01D4U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xDB4C4F7BU, lower); Assert.AreEqual(0x935E82F1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x92233300U, lower); Assert.AreEqual(0x69B82EBCU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7DE1D510U, lower); Assert.AreEqual(0x40D29EB5U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB45C6316U, lower); Assert.AreEqual(0xA2F09DABU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0F4D3872U, lower); Assert.AreEqual(0xEE521D7AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x72F3454FU, lower); Assert.AreEqual(0xF16952EEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA8E40225U, lower); Assert.AreEqual(0x377D35DEU, upper);

			random = SplitMix64.CreateWithState(0xBFE039B631439F10UL);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF78A92AAU, lower); Assert.AreEqual(0x8BD814C4U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3D986DA9U, lower); Assert.AreEqual(0x53F87893U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8B4B086AU, lower); Assert.AreEqual(0x411655BCU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x806CC78AU, lower); Assert.AreEqual(0x69A9026BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2968D7ABU, lower); Assert.AreEqual(0xCAB888EBU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF9B16233U, lower); Assert.AreEqual(0x02B49D11U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2CC5F881U, lower); Assert.AreEqual(0x1589D875U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCB4260E4U, lower); Assert.AreEqual(0xF3ACB90DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAA673C3FU, lower); Assert.AreEqual(0x2DAFEBDAU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xED054777U, lower); Assert.AreEqual(0xFDF2C53DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD1B3BE94U, lower); Assert.AreEqual(0x0C0B1FA1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE3C5F2B4U, lower); Assert.AreEqual(0xC37FB540U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x4E05E9DEU, lower); Assert.AreEqual(0x79C0175FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x706DEBACU, lower); Assert.AreEqual(0x01CA25C2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x70E56006U, lower); Assert.AreEqual(0xD57127C8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD15907A6U, lower); Assert.AreEqual(0x45E7660BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC63F1630U, lower); Assert.AreEqual(0x3D25F5D1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x37356BEFU, lower); Assert.AreEqual(0xB73BF1CDU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7CDE36C5U, lower); Assert.AreEqual(0x2F7D9A99U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x233F13C9U, lower); Assert.AreEqual(0xBC5CD5B2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA225EC27U, lower); Assert.AreEqual(0xB3AD43ABU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x59313E5DU, lower); Assert.AreEqual(0xF21E0B77U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0B2CA095U, lower); Assert.AreEqual(0x36B8FF72U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x72665C35U, lower); Assert.AreEqual(0x32FE2256U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x163644C0U, lower); Assert.AreEqual(0xF335F267U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x09B4A9D1U, lower); Assert.AreEqual(0x40269411U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2213B15EU, lower); Assert.AreEqual(0x00E852A9U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9BA28545U, lower); Assert.AreEqual(0x25947D96U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF970A2E2U, lower); Assert.AreEqual(0x4A7F1845U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7CF097DFU, lower); Assert.AreEqual(0xB04B3534U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB833241AU, lower); Assert.AreEqual(0x869B268FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFAE41E1AU, lower); Assert.AreEqual(0xC4DEBB5DU, upper);

			random = SplitMix64.CreateWithState(0xFFFFFFFFFFFFFFFFUL);
			random.Next64(out lower, out upper); Assert.AreEqual(0x1B652C20U, lower); Assert.AreEqual(0xE4D97177U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xDBF682C9U, lower); Assert.AreEqual(0xE99FF867U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB27281E9U, lower); Assert.AreEqual(0x382FF84CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCBA982D2U, lower); Assert.AreEqual(0x6D1DB36CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x578069AEU, lower); Assert.AreEqual(0xB4A0472EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA438BB33U, lower); Assert.AreEqual(0xD31DADBDU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x02083FA5U, lower); Assert.AreEqual(0xF14F2CF8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA39E8064U, lower); Assert.AreEqual(0x405DA438U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x156E0C84U, lower); Assert.AreEqual(0xC4FEA708U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7BBD6E1CU, lower); Assert.AreEqual(0x031E50FEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x1E71CF15U, lower); Assert.AreEqual(0x03B23496U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD3025DA7U, lower); Assert.AreEqual(0xCE755952U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD006BADBU, lower); Assert.AreEqual(0x01C9558BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6F7C1C8AU, lower); Assert.AreEqual(0xDD90E10FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB25878C1U, lower); Assert.AreEqual(0x354D0DF8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA07E34E8U, lower); Assert.AreEqual(0xACEEA13CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x84A5E267U, lower); Assert.AreEqual(0x6887829EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9456E8D2U, lower); Assert.AreEqual(0x312B54A5U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xBE96EA03U, lower); Assert.AreEqual(0x2310BD4AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x23BE2179U, lower); Assert.AreEqual(0x6C5EDF5CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCCC8783DU, lower); Assert.AreEqual(0x5D4BEE7AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9BA3863FU, lower); Assert.AreEqual(0xAC2CC667U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x16C17603U, lower); Assert.AreEqual(0x78A0A3EBU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD077172EU, lower); Assert.AreEqual(0x31BAFC61U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x51D114E2U, lower); Assert.AreEqual(0x2F36EB37U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8A9CDC26U, lower); Assert.AreEqual(0x9862F4D3U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x607F67B4U, lower); Assert.AreEqual(0xD870587CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x523CED67U, lower); Assert.AreEqual(0x728BBE64U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC863DA48U, lower); Assert.AreEqual(0xC1AF2B37U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB96B7B43U, lower); Assert.AreEqual(0x0477DE91U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xEE188704U, lower); Assert.AreEqual(0x24BDF605U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x52A541FEU, lower); Assert.AreEqual(0xDE2B5DB6U, upper);
		}

		[Test]
		public void First32_32x2_XoroShiro128Plus()
		{
			XoroShiro128Plus random;

			uint lower, upper;

			random = XoroShiro128Plus.CreateWithState(0x0000000000000000UL, 0x0000000000000001UL);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000001U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00004001U, lower); Assert.AreEqual(0x00000010U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x10000121U, lower); Assert.AreEqual(0x00880020U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x20404122U, lower); Assert.AreEqual(0x10005810U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0009A201U, lower); Assert.AreEqual(0x99906224U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x72058451U, lower); Assert.AreEqual(0x015C0423U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x29AE01D4U, lower); Assert.AreEqual(0x29307260U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x52499B74U, lower); Assert.AreEqual(0x5F6C3754U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x113CE773U, lower); Assert.AreEqual(0xFA13D453U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6E239DBEU, lower); Assert.AreEqual(0x9043B5E8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x464D8327U, lower); Assert.AreEqual(0xF84ABC29U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3A3D783DU, lower); Assert.AreEqual(0xCC1D8ACFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC3B1F6D2U, lower); Assert.AreEqual(0x53CF5B70U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x68B504AFU, lower); Assert.AreEqual(0x7D69BCA4U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2D7A9192U, lower); Assert.AreEqual(0x336F46B7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3AC076C7U, lower); Assert.AreEqual(0xDBF962DFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8E5CF5F9U, lower); Assert.AreEqual(0x6627B333U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2C7AFAF8U, lower); Assert.AreEqual(0x9F273051U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9C376171U, lower); Assert.AreEqual(0xA79E6149U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD13FA6A2U, lower); Assert.AreEqual(0xA9E62D60U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x4A6B94EBU, lower); Assert.AreEqual(0x883AAEE8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8EFE1226U, lower); Assert.AreEqual(0xA87E2F5AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8122A58DU, lower); Assert.AreEqual(0x924213FFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3A7AC7D2U, lower); Assert.AreEqual(0xA5DBB63BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x67AE39DAU, lower); Assert.AreEqual(0x79E33B99U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x70712A84U, lower); Assert.AreEqual(0xDD240C5EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8FDAC4C2U, lower); Assert.AreEqual(0x87965373U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0EFFBE45U, lower); Assert.AreEqual(0xEC5921EBU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE829646AU, lower); Assert.AreEqual(0xEDBA70B6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x662A093FU, lower); Assert.AreEqual(0x549CBBA2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xBFD76D03U, lower); Assert.AreEqual(0x06BFA4A6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x4D28CC8BU, lower); Assert.AreEqual(0x941CFAEAU, upper);

			random = XoroShiro128Plus.CreateWithState(0xB8B90B6C08E2904FUL, 0x1C8B7DFC92AA64DDUL);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9B8CF52CU, lower); Assert.AreEqual(0xD5448968U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x548F6EE3U, lower); Assert.AreEqual(0xC2D955B1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x774773A6U, lower); Assert.AreEqual(0x2B980174U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCFC2CBBEU, lower); Assert.AreEqual(0x1F089072U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAB8B114BU, lower); Assert.AreEqual(0x02717573U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x28DF3493U, lower); Assert.AreEqual(0x24A21694U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE8EE3DC4U, lower); Assert.AreEqual(0x045FD5BBU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xADEF2005U, lower); Assert.AreEqual(0x99AF2626U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x877A4028U, lower); Assert.AreEqual(0x489928B7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7AE8E40BU, lower); Assert.AreEqual(0x6038D08DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA3E0F72DU, lower); Assert.AreEqual(0xB4495532U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFF791D8AU, lower); Assert.AreEqual(0x04DE7428U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x31B62F99U, lower); Assert.AreEqual(0xA8B479BCU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC63E62EEU, lower); Assert.AreEqual(0xD0D633CAU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x72BF03AAU, lower); Assert.AreEqual(0x2A217F9AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC8181818U, lower); Assert.AreEqual(0x2A8AE37FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB33ABF09U, lower); Assert.AreEqual(0xA20F9F3BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xEF20C26AU, lower); Assert.AreEqual(0xC3EAE68FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFD032E72U, lower); Assert.AreEqual(0x76BA24DDU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE687C4F1U, lower); Assert.AreEqual(0x06D8404BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6BC2C0B8U, lower); Assert.AreEqual(0x5C729031U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCC86A00DU, lower); Assert.AreEqual(0xAB742778U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFC7EB62CU, lower); Assert.AreEqual(0x8A00597AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD348FEE6U, lower); Assert.AreEqual(0x08747249U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE7C02C3DU, lower); Assert.AreEqual(0x9B196B0FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6F3FC702U, lower); Assert.AreEqual(0x2B598860U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x40ABA647U, lower); Assert.AreEqual(0x6C1B4A77U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0A1432A9U, lower); Assert.AreEqual(0xCFEFF721U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x23620C62U, lower); Assert.AreEqual(0x396DB7B6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB37B3570U, lower); Assert.AreEqual(0xCF299BE1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x742E9BC7U, lower); Assert.AreEqual(0x75FEA95CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA23CD8CEU, lower); Assert.AreEqual(0x3AFCBD29U, upper);

			random = XoroShiro128Plus.CreateWithState(0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFFFFFFFFFFUL);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFFFFFFFEU, lower); Assert.AreEqual(0xFFFFFFFFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFFFFFFFFU, lower); Assert.AreEqual(0xFFFFFFFFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFFFFBFFFU, lower); Assert.AreEqual(0xFFFFFFFFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF000001FU, lower); Assert.AreEqual(0x0083FFEFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x17BFC11EU, lower); Assert.AreEqual(0x0FFFC5E0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFFFC5F00U, lower); Assert.AreEqual(0x74F44DEEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3F7F3E0EU, lower); Assert.AreEqual(0xFEC9E215U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0464C092U, lower); Assert.AreEqual(0x07ABED30U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5100D022U, lower); Assert.AreEqual(0x39D3E8A1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x68AFFFE6U, lower); Assert.AreEqual(0xB5DD54A4U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x65DE3946U, lower); Assert.AreEqual(0xF4CEC65DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x060DAA46U, lower); Assert.AreEqual(0x0B2B57DDU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x4596F07CU, lower); Assert.AreEqual(0xA43AB8CAU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x1E59A41EU, lower); Assert.AreEqual(0x61B5F66DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x93B6DB7BU, lower); Assert.AreEqual(0xE201B477U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFE253152U, lower); Assert.AreEqual(0x26899A44U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5ED90A28U, lower); Assert.AreEqual(0x974C507CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8382AA9CU, lower); Assert.AreEqual(0xD9006D6BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x78F05CDFU, lower); Assert.AreEqual(0x5B678155U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9CC406B2U, lower); Assert.AreEqual(0x07C516F4U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8EDDB13DU, lower); Assert.AreEqual(0xDD123CA9U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA71FE0E0U, lower); Assert.AreEqual(0xA60315FDU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x1C7EF8F2U, lower); Assert.AreEqual(0x99C809EAU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x45990824U, lower); Assert.AreEqual(0xD4A07D76U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF5980045U, lower); Assert.AreEqual(0xE6D2ABA7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x689B70C8U, lower); Assert.AreEqual(0x1381E2A3U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7EF62423U, lower); Assert.AreEqual(0x3246A89CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9F734DF1U, lower); Assert.AreEqual(0xDDCFA65AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x86C3AFE2U, lower); Assert.AreEqual(0xD82A2728U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE7905204U, lower); Assert.AreEqual(0xD32E0B7BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0833324AU, lower); Assert.AreEqual(0x1644D3F2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x92A8A3AEU, lower); Assert.AreEqual(0x1DE54381U, upper);
		}

		[Test]
		public void First32_32x2_XorShift128Plus()
		{
			XorShift128Plus random;

			uint lower, upper;

			random = XorShift128Plus.CreateWithState(0x0000000000000000UL, 0x0000000000000001UL);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000001U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000002U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00800021U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00840020U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00882400U, lower); Assert.AreEqual(0x00004000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00882921U, lower); Assert.AreEqual(0x00008000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0008864AU, lower); Assert.AreEqual(0x00008012U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x10048549U, lower); Assert.AreEqual(0x00004022U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x24902429U, lower); Assert.AreEqual(0x09004012U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x18C04532U, lower); Assert.AreEqual(0x0A408014U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0A352536U, lower); Assert.AreEqual(0x0A808814U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x168A2915U, lower); Assert.AreEqual(0x0A885854U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x62A9A974U, lower); Assert.AreEqual(0x0A492894U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x12A5A8B2U, lower); Assert.AreEqual(0x2A0A6099U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD0A260A7U, lower); Assert.AreEqual(0x29324A72U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6132D965U, lower); Assert.AreEqual(0x0A2A4270U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE8A9BD49U, lower); Assert.AreEqual(0x063199B2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x59AC65ACU, lower); Assert.AreEqual(0x30687279U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x065D2396U, lower); Assert.AreEqual(0xB25A7F77U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD77A242BU, lower); Assert.AreEqual(0xA96CB462U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x781D976AU, lower); Assert.AreEqual(0xAD9AC24EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x283F4B23U, lower); Assert.AreEqual(0x38519D8FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xED8F7182U, lower); Assert.AreEqual(0xB0D4314BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE0ED7F5FU, lower); Assert.AreEqual(0x12381B19U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8CE8AEBEU, lower); Assert.AreEqual(0x1EFD40A6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x080ACD77U, lower); Assert.AreEqual(0x99B2B3A4U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xDBCD4A83U, lower); Assert.AreEqual(0xD393DBC0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x81015E9EU, lower); Assert.AreEqual(0x11EA2602U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0171E939U, lower); Assert.AreEqual(0x18CFAA48U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3236C3B5U, lower); Assert.AreEqual(0x00E61E55U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3AFD7AEEU, lower); Assert.AreEqual(0xCD0FFCE8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0C30EE59U, lower); Assert.AreEqual(0xB8B137C5U, upper);

			random = XorShift128Plus.CreateWithState(0xFE59757D17BCB419UL, 0x7A8282D265985C33UL);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7D55104CU, lower); Assert.AreEqual(0x78DBF84FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xACDA4837U, lower); Assert.AreEqual(0xB406B0A9U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xDA1E8C55U, lower); Assert.AreEqual(0x657C7480U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0B209858U, lower); Assert.AreEqual(0x247883DEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA4272108U, lower); Assert.AreEqual(0x78F5491CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x4230FFBFU, lower); Assert.AreEqual(0x673FB3B8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x19519290U, lower); Assert.AreEqual(0x7C66BD5AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7D1C5EB3U, lower); Assert.AreEqual(0x358D8D37U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x18F6C985U, lower); Assert.AreEqual(0x6B84F40EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2D2D9E7AU, lower); Assert.AreEqual(0x50170A8DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB08F51D1U, lower); Assert.AreEqual(0xFFFF4E05U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC64463ACU, lower); Assert.AreEqual(0x65E0AEDAU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x829A8D99U, lower); Assert.AreEqual(0x65423CB8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8E4D694FU, lower); Assert.AreEqual(0x8E9A9DEAU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x49A87C68U, lower); Assert.AreEqual(0xC64FE49DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x05DAD4D4U, lower); Assert.AreEqual(0x6FFF1E3BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCEE91F33U, lower); Assert.AreEqual(0xFC462ABBU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x4FC22882U, lower); Assert.AreEqual(0xA9CFA67FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9B4FCBE6U, lower); Assert.AreEqual(0x8D3F7DADU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xBD937974U, lower); Assert.AreEqual(0xA624F727U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x435C65A2U, lower); Assert.AreEqual(0x2DB710F9U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x42526EF6U, lower); Assert.AreEqual(0x52A54231U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x23FF7C57U, lower); Assert.AreEqual(0x9A18022EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x08D392ADU, lower); Assert.AreEqual(0x6ED9A1ABU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x79B375A5U, lower); Assert.AreEqual(0xFA2D9558U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFBC9F73AU, lower); Assert.AreEqual(0x5FD79CC1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE602DAC2U, lower); Assert.AreEqual(0x36033BF5U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE9F9B496U, lower); Assert.AreEqual(0x78A296F2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE2A8320BU, lower); Assert.AreEqual(0x37E42556U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0EB8DC16U, lower); Assert.AreEqual(0x34874B7FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3E6F9608U, lower); Assert.AreEqual(0xC9D7A9E6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC6E6EF28U, lower); Assert.AreEqual(0x50A0545BU, upper);

			random = XorShift128Plus.CreateWithState(0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFFFFFFFFFFUL);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFFFFFFFEU, lower); Assert.AreEqual(0xFFFFFFFFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x007FFFDFU, lower); Assert.AreEqual(0xF8000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0083FFDFU, lower); Assert.AreEqual(0xF7C00000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFF87E3FEU, lower); Assert.AreEqual(0xFFFE01FFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFF07E0FEU, lower); Assert.AreEqual(0x003DD1FFU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x70037DE6U, lower); Assert.AreEqual(0x07C19E7EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x70FF6106U, lower); Assert.AreEqual(0x0FC19C8AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x206BFF26U, lower); Assert.AreEqual(0x47399E08U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2C7FBB26U, lower); Assert.AreEqual(0x7E79BFFCU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2B7B9502U, lower); Assert.AreEqual(0x3F2FF63EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA258D4E2U, lower); Assert.AreEqual(0xC726364DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD0ED1A19U, lower); Assert.AreEqual(0xE926FC81U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8F7BD943U, lower); Assert.AreEqual(0x01F9BEC0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3116A51EU, lower); Assert.AreEqual(0xDFE7B3D3U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0D87E9C1U, lower); Assert.AreEqual(0x3EE6CD46U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7005356EU, lower); Assert.AreEqual(0x42611AB6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2117EC81U, lower); Assert.AreEqual(0xE01DD1C3U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x1FDFD6CDU, lower); Assert.AreEqual(0x80980146U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x427920BCU, lower); Assert.AreEqual(0xC0C1C87EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB0E6B2BFU, lower); Assert.AreEqual(0xA0E153D2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6E07ECE8U, lower); Assert.AreEqual(0x1DEDA63FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC685453AU, lower); Assert.AreEqual(0x92939B50U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7AE4270CU, lower); Assert.AreEqual(0x5889CF5AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD4B2F9A6U, lower); Assert.AreEqual(0x2A8EB0CCU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x33172B12U, lower); Assert.AreEqual(0x77A72B71U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x53308AB1U, lower); Assert.AreEqual(0xF7CE609FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x30FA5B06U, lower); Assert.AreEqual(0xC0BF4CB7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x847ADD53U, lower); Assert.AreEqual(0xF9D4526BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB28186D0U, lower); Assert.AreEqual(0xF24C98F4U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6E79438CU, lower); Assert.AreEqual(0xE61D601AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5F5E184FU, lower); Assert.AreEqual(0x722E2453U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x73B788EAU, lower); Assert.AreEqual(0xA130FC99U, upper);
		}

		[Test]
		public void First32_32x2_XorShift1024Star()
		{
			XorShift1024Star random;

			uint lower, upper;

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000001UL,
			}, 0);

			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000000U, lower); Assert.AreEqual(0x00000000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFE7FDB5U, lower); Assert.AreEqual(0xD7F7D22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5917F91FU, lower); Assert.AreEqual(0xF8C4E5D7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFE7FDB5U, lower); Assert.AreEqual(0xD7F7D22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5917F91FU, lower); Assert.AreEqual(0xF8C4E5D7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFE7FDB5U, lower); Assert.AreEqual(0xD7F7D22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5917F91FU, lower); Assert.AreEqual(0xF8C4E5D7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFE7FDB5U, lower); Assert.AreEqual(0xD7F7D22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5917F91FU, lower); Assert.AreEqual(0xF8C4E5D7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFE7FDB5U, lower); Assert.AreEqual(0xD7F7D22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5917F91FU, lower); Assert.AreEqual(0xF8C4E5D7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFE7FDB5U, lower); Assert.AreEqual(0xD7F7D22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5917F91FU, lower); Assert.AreEqual(0xF8C4E5D7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFE7FDB5U, lower); Assert.AreEqual(0xD7F7D22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5917F91FU, lower); Assert.AreEqual(0xF8C4E5D7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFE7FDB5U, lower); Assert.AreEqual(0xD7F7D22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5917F91FU, lower); Assert.AreEqual(0xF8C4E5D7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAFFB6A00U, lower); Assert.AreEqual(0xCF5D5C83U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC67ED1B5U, lower); Assert.AreEqual(0xACA9B680U, upper);

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0x66C6A7A04D98CEA4UL,
				0x70446B75A1D77957UL,
				0x42CD0EB869C9AAEFUL,
				0x14DD9C68AD992037UL,
				0x6B222E53825DFFADUL,
				0x99E6D9D773339DB6UL,
				0x710DF769DCEEA754UL,
				0xBA50D0E157F68E29UL,
				0xEC49132D75F4BF16UL,
				0x31A9C3994EDAED8DUL,
				0xA50FD919BA8E2134UL,
				0xE9FB0C8D321CEDBFUL,
				0x466FDC75B9E79489UL,
				0x001C3B8801D51E1BUL,
				0x6F519A67AD2AA194UL,
				0x12D6ED86D7D63A91UL,
			}, 0);

			random.Next64(out lower, out upper); Assert.AreEqual(0x58900701U, lower); Assert.AreEqual(0x79C88B83U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCEA4CFAFU, lower); Assert.AreEqual(0xD79B4704U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE8494B92U, lower); Assert.AreEqual(0x5C13F8C1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5BF97C6FU, lower); Assert.AreEqual(0x8143C6DCU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x4CE26986U, lower); Assert.AreEqual(0x78DBDC97U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCD801BBDU, lower); Assert.AreEqual(0xA28D9372U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9D84037CU, lower); Assert.AreEqual(0x4CAA87EEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0C151AEAU, lower); Assert.AreEqual(0xFE99396EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE4BD3111U, lower); Assert.AreEqual(0x8AA44913U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0586DCB1U, lower); Assert.AreEqual(0xCA30BA0BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x841DB44EU, lower); Assert.AreEqual(0xB9BC75D8U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x92421F93U, lower); Assert.AreEqual(0x70A9B2A0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFA00BB6DU, lower); Assert.AreEqual(0x19D6D7E1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x015CB2ADU, lower); Assert.AreEqual(0x060675CEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7F56810EU, lower); Assert.AreEqual(0x296A71EAU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7CA6995AU, lower); Assert.AreEqual(0xA73F65E1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x966D73B7U, lower); Assert.AreEqual(0x5D2E6BFEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7709C43EU, lower); Assert.AreEqual(0xAA6247FEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9264AFEBU, lower); Assert.AreEqual(0x51932E34U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x21D4A7E9U, lower); Assert.AreEqual(0x99B7C8D3U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB9B73F40U, lower); Assert.AreEqual(0x7E7856E6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x54CBD15FU, lower); Assert.AreEqual(0xC64A542CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x29DE452AU, lower); Assert.AreEqual(0xC2F4D98EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF7977BFAU, lower); Assert.AreEqual(0x56747394U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD160675CU, lower); Assert.AreEqual(0x7E227DD7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x07BCD471U, lower); Assert.AreEqual(0x2F430C99U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xBB94DF53U, lower); Assert.AreEqual(0x5F063840U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x38123C5EU, lower); Assert.AreEqual(0x0F9EB482U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x69585263U, lower); Assert.AreEqual(0x8BFBF3A7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xDEE70F02U, lower); Assert.AreEqual(0x5ADB1240U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7BDC1FD3U, lower); Assert.AreEqual(0x9D431369U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3769BE35U, lower); Assert.AreEqual(0xC3CD3D24U, upper);

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
			}, 0);

			random.Next64(out lower, out upper); Assert.AreEqual(0xA4B00000U, lower); Assert.AreEqual(0x3AA6BE86U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0B182265U, lower); Assert.AreEqual(0x09FDEC8FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x44FFDFE6U, lower); Assert.AreEqual(0x20424823U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAB68024BU, lower); Assert.AreEqual(0xEF99762BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA4B00000U, lower); Assert.AreEqual(0x3AA6BE86U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0B182265U, lower); Assert.AreEqual(0x09FDEC8FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x44FFDFE6U, lower); Assert.AreEqual(0x20424823U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAB68024BU, lower); Assert.AreEqual(0xEF99762BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA4B00000U, lower); Assert.AreEqual(0x3AA6BE86U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0B182265U, lower); Assert.AreEqual(0x09FDEC8FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x44FFDFE6U, lower); Assert.AreEqual(0x20424823U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAB68024BU, lower); Assert.AreEqual(0xEF99762BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA4B00000U, lower); Assert.AreEqual(0x3AA6BE86U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x0B182265U, lower); Assert.AreEqual(0x09FDEC8FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x44FFDFE6U, lower); Assert.AreEqual(0x20424823U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAB68024BU, lower); Assert.AreEqual(0xEF99762BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2A849600U, lower); Assert.AreEqual(0x1428597BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6034BAB0U, lower); Assert.AreEqual(0xE030CFE7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8E7FBB36U, lower); Assert.AreEqual(0x26AAE568U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x61E826FBU, lower); Assert.AreEqual(0xE930D8E6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x74047150U, lower); Assert.AreEqual(0x1A90F6C0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x16B4DF60U, lower); Assert.AreEqual(0xD9C832A2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x44FFDFE6U, lower); Assert.AreEqual(0x20424823U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAB68024BU, lower); Assert.AreEqual(0xEF99762BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x2A849600U, lower); Assert.AreEqual(0x1428597BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x6034BAB0U, lower); Assert.AreEqual(0xE030CFE7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8E7FBB36U, lower); Assert.AreEqual(0x26AAE568U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x61E826FBU, lower); Assert.AreEqual(0xE930D8E6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x74047150U, lower); Assert.AreEqual(0x1A90F6C0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x16B4DF60U, lower); Assert.AreEqual(0xD9C832A2U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x44FFDFE6U, lower); Assert.AreEqual(0x20424823U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAB68024BU, lower); Assert.AreEqual(0xEF99762BU, upper);
		}

		[Test]
		public void First16_32x2_XorShiftAdd()
		{
			XorShiftAdd random;

			uint lower, upper;

			random = XorShiftAdd.CreateWithState(0x00000000U, 0x00000000U, 0x00000000U, 0x00000001U);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00000001U, lower); Assert.AreEqual(0x00000801U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00400800U, lower); Assert.AreEqual(0x00400000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00008001U, lower); Assert.AreEqual(0x00008101U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00480110U, lower); Assert.AreEqual(0x40488010U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x84009001U, lower); Assert.AreEqual(0x44001921U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x00090920U, lower); Assert.AreEqual(0x48098000U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x90010301U, lower); Assert.AreEqual(0x48888305U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC0910006U, lower); Assert.AreEqual(0x00099202U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x4D109361U, lower); Assert.AreEqual(0x8D910987U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC8CA2826U, lower); Assert.AreEqual(0x8849B040U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x49231249U, lower); Assert.AreEqual(0x19A4C30BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xDAC34714U, lower); Assert.AreEqual(0xCA51085AU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC8445248U, lower); Assert.AreEqual(0x5AB79D02U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE5A35142U, lower); Assert.AreEqual(0x54360645U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x513A4212U, lower); Assert.AreEqual(0x6626680FU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x56422802U, lower); Assert.AreEqual(0x40548200U, upper);

			random = XorShiftAdd.CreateWithState(0xFE59757DU, 0x17BCB419U, 0x7A8282D2U, 0x65985C33U);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE01ADF05U, lower); Assert.AreEqual(0xEB9ED877U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x045883B9U, lower); Assert.AreEqual(0x2A222B9DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7670B2FBU, lower); Assert.AreEqual(0x86F35920U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA80DBA5CU, lower); Assert.AreEqual(0x59FF6268U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x1CA057FAU, lower); Assert.AreEqual(0xB4B559B1U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x5E7748F0U, lower); Assert.AreEqual(0x11EDBC63U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xDD875206U, lower); Assert.AreEqual(0x2ABCAD87U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x9F7B1B7FU, lower); Assert.AreEqual(0x7D43857BU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE06C7C3EU, lower); Assert.AreEqual(0x4C9D69E3U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xD19C080EU, lower); Assert.AreEqual(0x59C456FCU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x847459D1U, lower); Assert.AreEqual(0x217B5E0CU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xCF757EB1U, lower); Assert.AreEqual(0xEAAFEBCBU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xE2ED8FD2U, lower); Assert.AreEqual(0xFAF2D2E6U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xA74772AAU, lower); Assert.AreEqual(0x7092DA9DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x76988405U, lower); Assert.AreEqual(0xA042B22EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xAA914565U, lower); Assert.AreEqual(0x8EF2E349U, upper);

			random = XorShiftAdd.CreateWithState(0xFFFFFFFFU, 0xFFFFFFFFU, 0xFFFFFFFFU, 0xFFFFFFFFU);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFFFFFFFEU, lower); Assert.AreEqual(0xFFFF87FEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xFC3F0FFEU, lower); Assert.AreEqual(0xF87F0FFEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF87F0FFEU, lower); Assert.AreEqual(0xBC7F78FEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x8087E10EU, lower); Assert.AreEqual(0xC088601EU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3C80E01EU, lower); Assert.AreEqual(0x780076FEU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x74370D0EU, lower); Assert.AreEqual(0xB8EE0C3DU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x3CEE073CU, lower); Assert.AreEqual(0x3D3679E0U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC17E3183U, lower); Assert.AreEqual(0x377CC781U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC34C1CE0U, lower); Assert.AreEqual(0x193D8615U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xB4110FEBU, lower); Assert.AreEqual(0x64C31AADU, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xC4ED077EU, lower); Assert.AreEqual(0x72FF76E7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xDE7FAEF0U, lower); Assert.AreEqual(0x94E84A89U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x43625B76U, lower); Assert.AreEqual(0x5D93DC75U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x19245640U, lower); Assert.AreEqual(0x32528C01U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0x7AB4C8E4U, lower); Assert.AreEqual(0xA4E9CCD7U, upper);
			random.Next64(out lower, out upper); Assert.AreEqual(0xF40250A8U, lower); Assert.AreEqual(0x73D211F5U, upper);
		}

		[Test]
		public void SkipAhead1_First8_XoroShiro128Plus()
		{
			XoroShiro128Plus random;

			random = XoroShiro128Plus.CreateWithState(0x0000000000000000UL, 0x0000000000000001UL);
			random.SkipAhead();
			Assert.AreEqual(0x7658278B2B8C3EC2UL, random.Next64());
			Assert.AreEqual(0xA7B78AA84F8EAFFBUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x54B49498DCE59827UL, random.Next64());
			Assert.AreEqual(0x6CE005251E887588UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x443C4068B228EA99UL, random.Next64());
			Assert.AreEqual(0x3A1CEE2F5D3970EEUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x2FC7A951A12D6160UL, random.Next64());
			Assert.AreEqual(0x0C09F7D881AE5F15UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x4688D490BCFCD7DEUL, random.Next64());
			Assert.AreEqual(0x868103051201B1F9UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x577B634970C07968UL, random.Next64());
			Assert.AreEqual(0x46E0BAAD9D75C689UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xE61F35ABD20FE4F7UL, random.Next64());
			Assert.AreEqual(0xFE7B828561605259UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xB97229CD3B99127FUL, random.Next64());
			Assert.AreEqual(0x315BE2755FB37BC3UL, random.Next64());

			random = XoroShiro128Plus.CreateWithState(0xB8B90B6C08E2904FUL, 0x1C8B7DFC92AA64DDUL);
			random.SkipAhead();
			Assert.AreEqual(0xB27EA813BD71AB47UL, random.Next64());
			Assert.AreEqual(0xC7F0E98FAC036A5BUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x6D4C9B5A2EEB6D32UL, random.Next64());
			Assert.AreEqual(0x906816CCE208A5A7UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xA648D2FAD32FA1FAUL, random.Next64());
			Assert.AreEqual(0x740106A7EDF20416UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x423720B2BCF182B9UL, random.Next64());
			Assert.AreEqual(0xA3E5A79BE45F298EUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x223426B9B1E883A5UL, random.Next64());
			Assert.AreEqual(0xF84C2A719802918EUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xC3AC668138E9D557UL, random.Next64());
			Assert.AreEqual(0x921AA31DE8A7F038UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x2DCD5452E49AD728UL, random.Next64());
			Assert.AreEqual(0x685A6524593CB8C6UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x20E3E9B20AC7EE02UL, random.Next64());
			Assert.AreEqual(0x284016DB68CD4C66UL, random.Next64());

			random = XoroShiro128Plus.CreateWithState(0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFFFFFFFFFFUL);
			random.SkipAhead();
			Assert.AreEqual(0xAAEB8F0A7C8CCC75UL, random.Next64());
			Assert.AreEqual(0xCB6A34DA1237619AUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x1A2D14B91AFA0ED4UL, random.Next64());
			Assert.AreEqual(0x1AB68A04BC7B471FUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x206B55DB71468353UL, random.Next64());
			Assert.AreEqual(0xF421DCF15080B198UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xC354FC0FEC8F1134UL, random.Next64());
			Assert.AreEqual(0x2C004EB5BF4B05D2UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x45180109032520D4UL, random.Next64());
			Assert.AreEqual(0xDEDB70D7E0926EE9UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xC4D325D0233BABA7UL, random.Next64());
			Assert.AreEqual(0x09323A22D9A7A0E8UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x2AC7E7A470C2EB69UL, random.Next64());
			Assert.AreEqual(0x7ACA50540842F60EUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xD7CB3E72C7F5E9A0UL, random.Next64());
			Assert.AreEqual(0xD0299BC8E22F5280UL, random.Next64());
		}

		[Test]
		public void SkipAhead1_First32_XorShift128Plus()
		{
			XorShift128Plus random;

			random = XorShift128Plus.CreateWithState(0x0000000000000000UL, 0x0000000000000001UL);
			random.SkipAhead();
			Assert.AreEqual(0xFA38403C1F5A2BADUL, random.Next64());
			Assert.AreEqual(0x2D63F6825713A04CUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x1078E6D94A48CA72UL, random.Next64());
			Assert.AreEqual(0x9732466808B45E0EUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xD144E7056F0550E2UL, random.Next64());
			Assert.AreEqual(0x907253A7EE200DC0UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xEC082D0D755AEE95UL, random.Next64());
			Assert.AreEqual(0x96DBC5FF86A38E21UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x02FDC0E55DC2A9BFUL, random.Next64());
			Assert.AreEqual(0x10B317C035704875UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x87EE649A3F4B9204UL, random.Next64());
			Assert.AreEqual(0x2B58A51E375B4AC3UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x5BB77CBF4CDE6664UL, random.Next64());
			Assert.AreEqual(0x6795052541337777UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x88654935CA778EE1UL, random.Next64());
			Assert.AreEqual(0x68657888C5DEF08AUL, random.Next64());

			random = XorShift128Plus.CreateWithState(0xFE59757D17BCB419UL, 0x7A8282D265985C33UL);
			random.SkipAhead();
			Assert.AreEqual(0x429B4FCB2534BEA2UL, random.Next64());
			Assert.AreEqual(0x45F407B1FF5B2F0AUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x5EC5DB69CDE1C941UL, random.Next64());
			Assert.AreEqual(0x2AFE34670DEF34B1UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x240DDCE9599718C8UL, random.Next64());
			Assert.AreEqual(0xB314B08A9A75702DUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xBDCFAF8A986D6277UL, random.Next64());
			Assert.AreEqual(0x5AE67A21AEAC0E45UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x77BEBD9DC5BB6A20UL, random.Next64());
			Assert.AreEqual(0xA3643FB855D1B36DUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x52933B6BCC9C0865UL, random.Next64());
			Assert.AreEqual(0x35F83DF8C23F23E8UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xD8ACB3C046C41934UL, random.Next64());
			Assert.AreEqual(0x6B526C58005BE131UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xC1843907E09B0B7DUL, random.Next64());
			Assert.AreEqual(0x06FF99B615F7E871UL, random.Next64());

			random = XorShift128Plus.CreateWithState(0xFFFFFFFFFFFFFFFFUL, 0xFFFFFFFFFFFFFFFFUL);
			random.SkipAhead();
			Assert.AreEqual(0x10D444864D13C379UL, random.Next64());
			Assert.AreEqual(0xFB7DD1B869E01613UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x140426D70C1F4AF0UL, random.Next64());
			Assert.AreEqual(0xF5D73A1035931DF8UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xF74D3A6E01C9A8F4UL, random.Next64());
			Assert.AreEqual(0xC95E87B0635184B7UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xBF20D23D2A5AA967UL, random.Next64());
			Assert.AreEqual(0xC0F7CE60CB4F4794UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x9340EABE5A9760B3UL, random.Next64());
			Assert.AreEqual(0x92F4AE6713C2089FUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xA9786C120A70EDC6UL, random.Next64());
			Assert.AreEqual(0xBEF1EB035AB12A65UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xBFB9C6CF4481C83BUL, random.Next64());
			Assert.AreEqual(0x0CDDCD45C9703D53UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xE659AD0A8487F68CUL, random.Next64());
			Assert.AreEqual(0x141C3F5D2C9AF0E6UL, random.Next64());
		}

		[Test]
		public void SkipAhead1_First32_XorShift1024Star()
		{
			XorShift1024Star random;

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000000UL,
				0x0000000000000001UL,
			}, 0);

			random.SkipAhead();
			Assert.AreEqual(0xEC05BB3B626154ACUL, random.Next64());
			Assert.AreEqual(0x3C3B84EBFC569783UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x2627DE42065C7101UL, random.Next64());
			Assert.AreEqual(0x9FDDB33E5F868D13UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xECD2CD719E058DE4UL, random.Next64());
			Assert.AreEqual(0xA0F3547520F75F0EUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x158471BB32EA288AUL, random.Next64());
			Assert.AreEqual(0x2BB89256AEA09702UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x442466D5BE83209BUL, random.Next64());
			Assert.AreEqual(0x928DEA276A215441UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x72D30B14D2467A9AUL, random.Next64());
			Assert.AreEqual(0xA84318DBEA9FBA04UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xAF343D3D762634C8UL, random.Next64());
			Assert.AreEqual(0x7C733825059259A1UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xE290C8E16D76EFEAUL, random.Next64());
			Assert.AreEqual(0x3690F217F2954EB6UL, random.Next64());

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0x66C6A7A04D98CEA4UL,
				0x70446B75A1D77957UL,
				0x42CD0EB869C9AAEFUL,
				0x14DD9C68AD992037UL,
				0x6B222E53825DFFADUL,
				0x99E6D9D773339DB6UL,
				0x710DF769DCEEA754UL,
				0xBA50D0E157F68E29UL,
				0xEC49132D75F4BF16UL,
				0x31A9C3994EDAED8DUL,
				0xA50FD919BA8E2134UL,
				0xE9FB0C8D321CEDBFUL,
				0x466FDC75B9E79489UL,
				0x001C3B8801D51E1BUL,
				0x6F519A67AD2AA194UL,
				0x12D6ED86D7D63A91UL,
			}, 0);

			random.SkipAhead();
			Assert.AreEqual(0xCE7F10DD870F2FDEUL, random.Next64());
			Assert.AreEqual(0x351BED61B930230BUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x044E68C23599B883UL, random.Next64());
			Assert.AreEqual(0xDD57BB472A0A3A52UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xC7E9D35CB337F78AUL, random.Next64());
			Assert.AreEqual(0x4AE4C08EEDB097E4UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xF52F4AB07A94186EUL, random.Next64());
			Assert.AreEqual(0xBAC8FCD952FD509BUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x8DE32A1EBBF8298FUL, random.Next64());
			Assert.AreEqual(0xFD8FAF330C0C51F7UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xCE487F71C0158276UL, random.Next64());
			Assert.AreEqual(0x2D7A03A30EC64FBEUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xE648CED50094234BUL, random.Next64());
			Assert.AreEqual(0xB6784EF74203C8F9UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x8C995E238327D7FCUL, random.Next64());
			Assert.AreEqual(0x6DBE7EE0E9B89096UL, random.Next64());

			random = XorShift1024Star.CreateWithState(new ulong[]
			{
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
				0xFFFFFFFFFFFFFFFFUL,
			}, 0);

			random.SkipAhead();
			Assert.AreEqual(0x0CE0C8D7CAF61EBBUL, random.Next64());
			Assert.AreEqual(0xDBAFDF21E6E39D49UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x7A597B5C84B31FA4UL, random.Next64());
			Assert.AreEqual(0xEE79F1E08A7BBAD6UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x9D0D8BF41F0EFA4FUL, random.Next64());
			Assert.AreEqual(0x46CD37AF0BE67A2DUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x5625321E0EE52DECUL, random.Next64());
			Assert.AreEqual(0x48415D1031A8A246UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x5337375721E5C341UL, random.Next64());
			Assert.AreEqual(0x95DD36F91B425DA1UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x5C0F6288D293E529UL, random.Next64());
			Assert.AreEqual(0x736231EFC1BE930BUL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0xCDEDCFD586191FA3UL, random.Next64());
			Assert.AreEqual(0xFE556C35D5444695UL, random.Next64());
			random.SkipAhead();
			Assert.AreEqual(0x490D0A3CD6656E93UL, random.Next64());
			Assert.AreEqual(0xFC3A23CD083143BFUL, random.Next64());
		}

		[Test]
		public void SkipAhead1_First32_XorShiftAdd()
		{
			XorShiftAdd random;

			random = XorShiftAdd.CreateWithState(0x00000000U, 0x00000000U, 0x00000000U, 0x00000001U);
			random.SkipAhead();
			Assert.AreEqual(0xA187C2CBU, random.Next32());
			Assert.AreEqual(0x3129497BU, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x31BEBAB5U, random.Next32());
			Assert.AreEqual(0xBD4C9935U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x10E09EB5U, random.Next32());
			Assert.AreEqual(0x58ED750EU, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x2D90F860U, random.Next32());
			Assert.AreEqual(0x83AE3253U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x1F93EF9CU, random.Next32());
			Assert.AreEqual(0xDADB1978U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x47E5498CU, random.Next32());
			Assert.AreEqual(0x7AE57905U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x124D2C77U, random.Next32());
			Assert.AreEqual(0x4952C161U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0xD8461447U, random.Next32());
			Assert.AreEqual(0x173FA7D0U, random.Next32());

			random = XorShiftAdd.CreateWithState(0xFE59757DU, 0x17BCB419U, 0x7A8282D2U, 0x65985C33U);
			random.SkipAhead();
			Assert.AreEqual(0x5221A8CBU, random.Next32());
			Assert.AreEqual(0xABEFEA16U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x3855FEAFU, random.Next32());
			Assert.AreEqual(0x9836DBDEU, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0xC50AC593U, random.Next32());
			Assert.AreEqual(0x59E2F572U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x40041D74U, random.Next32());
			Assert.AreEqual(0xAE1F214FU, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x542D3E22U, random.Next32());
			Assert.AreEqual(0x489BB31CU, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0xDFB8FDC2U, random.Next32());
			Assert.AreEqual(0x30222BD1U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x3CD97409U, random.Next32());
			Assert.AreEqual(0xA0DD8542U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0xFD86CAAFU, random.Next32());
			Assert.AreEqual(0x61509B2DU, random.Next32());

			random = XorShiftAdd.CreateWithState(0xFFFFFFFFU, 0xFFFFFFFFU, 0xFFFFFFFFU, 0xFFFFFFFFU);
			random.SkipAhead();
			Assert.AreEqual(0xA5D300B1U, random.Next32());
			Assert.AreEqual(0x5471CF4FU, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x5D70D62EU, random.Next32());
			Assert.AreEqual(0x7809FEA1U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x6B424C61U, random.Next32());
			Assert.AreEqual(0x27A0FB75U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0xF77CCFDFU, random.Next32());
			Assert.AreEqual(0x425E7267U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x140C2B37U, random.Next32());
			Assert.AreEqual(0x905A67C3U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x368EA743U, random.Next32());
			Assert.AreEqual(0x7CE648E9U, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x9307AE2CU, random.Next32());
			Assert.AreEqual(0x29CCD2BAU, random.Next32());
			random.SkipAhead();
			Assert.AreEqual(0x5F3128FAU, random.Next32());
			Assert.AreEqual(0xF35517B4U, random.Next32());
		}

		[Test]
		public void SkipAhead_Next_MixedOrdering_XoroShiro128Plus()
		{
			var random0 = XoroShiro128Plus.CreateWithState(0xB8B90B6C08E2904FUL, 0x1C8B7DFC92AA64DDUL);
			var random1 = random0.Clone();
			var random2 = random0.Clone();
			var random3 = random0.Clone();
			var random4 = random0.Clone();

			random0.Next64();
			random0.Next64();
			random0.Next64();
			random0.Next64();
			random0.SkipAhead();
			random0.SkipAhead();
			random0.SkipAhead();
			random0.SkipAhead();

			random1.SkipAhead();
			random1.SkipAhead();
			random1.SkipAhead();
			random1.SkipAhead();
			random1.Next64();
			random1.Next64();
			random1.Next64();
			random1.Next64();

			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();

			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();

			random4.Next64();
			random4.SkipAhead();
			random4.SkipAhead();
			random4.Next64();
			random4.Next64();
			random4.SkipAhead();
			random4.SkipAhead();
			random4.Next64();

			var next0 = random0.Next64();
			var next1 = random1.Next64();
			var next2 = random2.Next64();
			var next3 = random3.Next64();
			var next4 = random4.Next64();

			Assert.AreEqual(next0, next1);
			Assert.AreEqual(next0, next2);
			Assert.AreEqual(next0, next3);
			Assert.AreEqual(next0, next4);
		}

		[Test]
		public void SkipAhead_Next_MixedOrdering_XorShift128Plus()
		{
			var random0 = XorShift128Plus.CreateWithState(0xFE59757D17BCB419UL, 0x7A8282D265985C33UL);
			var random1 = random0.Clone();
			var random2 = random0.Clone();
			var random3 = random0.Clone();
			var random4 = random0.Clone();

			random0.Next64();
			random0.Next64();
			random0.Next64();
			random0.Next64();
			random0.SkipAhead();
			random0.SkipAhead();
			random0.SkipAhead();
			random0.SkipAhead();

			random1.SkipAhead();
			random1.SkipAhead();
			random1.SkipAhead();
			random1.SkipAhead();
			random1.Next64();
			random1.Next64();
			random1.Next64();
			random1.Next64();

			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();

			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();

			random4.Next64();
			random4.SkipAhead();
			random4.SkipAhead();
			random4.Next64();
			random4.Next64();
			random4.SkipAhead();
			random4.SkipAhead();
			random4.Next64();

			var next0 = random0.Next64();
			var next1 = random1.Next64();
			var next2 = random2.Next64();
			var next3 = random3.Next64();
			var next4 = random4.Next64();

			Assert.AreEqual(next0, next1);
			Assert.AreEqual(next0, next2);
			Assert.AreEqual(next0, next3);
			Assert.AreEqual(next0, next4);
		}

		[Test]
		public void SkipAhead_Next_MixedOrdering_XorShift1024Star()
		{
			var random0 = XorShift1024Star.CreateWithState(new ulong[]
			{
				0x66C6A7A04D98CEA4UL,
				0x70446B75A1D77957UL,
				0x42CD0EB869C9AAEFUL,
				0x14DD9C68AD992037UL,
				0x6B222E53825DFFADUL,
				0x99E6D9D773339DB6UL,
				0x710DF769DCEEA754UL,
				0xBA50D0E157F68E29UL,
				0xEC49132D75F4BF16UL,
				0x31A9C3994EDAED8DUL,
				0xA50FD919BA8E2134UL,
				0xE9FB0C8D321CEDBFUL,
				0x466FDC75B9E79489UL,
				0x001C3B8801D51E1BUL,
				0x6F519A67AD2AA194UL,
				0x12D6ED86D7D63A91UL,
			}, 0);
			var random1 = random0.Clone();
			var random2 = random0.Clone();
			var random3 = random0.Clone();
			var random4 = random0.Clone();

			random0.Next64();
			random0.Next64();
			random0.Next64();
			random0.Next64();
			random0.SkipAhead();
			random0.SkipAhead();
			random0.SkipAhead();
			random0.SkipAhead();

			random1.SkipAhead();
			random1.SkipAhead();
			random1.SkipAhead();
			random1.SkipAhead();
			random1.Next64();
			random1.Next64();
			random1.Next64();
			random1.Next64();

			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();

			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();

			random4.Next64();
			random4.SkipAhead();
			random4.SkipAhead();
			random4.Next64();
			random4.Next64();
			random4.SkipAhead();
			random4.SkipAhead();
			random4.Next64();

			var next0 = random0.Next64();
			var next1 = random1.Next64();
			var next2 = random2.Next64();
			var next3 = random3.Next64();
			var next4 = random4.Next64();

			Assert.AreEqual(next0, next1);
			Assert.AreEqual(next0, next2);
			Assert.AreEqual(next0, next3);
			Assert.AreEqual(next0, next4);
		}

		[Test]
		public void SkipAhead_Next_MixedOrdering_XorShiftAdd()
		{
			var random0 = XorShiftAdd.CreateWithState(0xFE59757DU, 0x17BCB419U, 0x7A8282D2U, 0x65985C33U);
			var random1 = random0.Clone();
			var random2 = random0.Clone();
			var random3 = random0.Clone();
			var random4 = random0.Clone();

			random0.Next64();
			random0.Next64();
			random0.Next64();
			random0.Next64();
			random0.SkipAhead();
			random0.SkipAhead();
			random0.SkipAhead();
			random0.SkipAhead();

			random1.SkipAhead();
			random1.SkipAhead();
			random1.SkipAhead();
			random1.SkipAhead();
			random1.Next64();
			random1.Next64();
			random1.Next64();
			random1.Next64();

			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();
			random2.Next64();
			random2.SkipAhead();

			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();
			random3.SkipAhead();
			random3.Next64();

			random4.Next64();
			random4.SkipAhead();
			random4.SkipAhead();
			random4.Next64();
			random4.Next64();
			random4.SkipAhead();
			random4.SkipAhead();
			random4.Next64();

			var next0 = random0.Next64();
			var next1 = random1.Next64();
			var next2 = random2.Next64();
			var next3 = random3.Next64();
			var next4 = random4.Next64();

			Assert.AreEqual(next0, next1);
			Assert.AreEqual(next0, next2);
			Assert.AreEqual(next0, next3);
			Assert.AreEqual(next0, next4);
		}
	}
}
#endif
