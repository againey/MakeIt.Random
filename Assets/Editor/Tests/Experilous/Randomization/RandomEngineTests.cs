/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3
using UnityEngine;
using NUnit.Framework;

namespace Experilous.Randomization.Tests
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

		public static void ValidateNext32BitsDistribution(IRandomEngine random, int bitCount, int hitsPerBucket, float tolerance)
		{
			int bucketCount = 1;
			for (int i = 0; i < bitCount; ++i) bucketCount = bucketCount * 2;
			var buckets = new int[bucketCount];
			uint mask = ~(0xFFFFFFFFU << bitCount);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = random.Next32() & mask;
				buckets[random] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateNext64BitsDistribution(IRandomEngine random, int bitCount, int hitsPerBucket, float tolerance)
		{
			int bucketCount = 1;
			for (int i = 0; i < bitCount; ++i) bucketCount = bucketCount * 2;
			var buckets = new int[bucketCount];
			ulong mask = ~(0xFFFFFFFFFFFFFFFFUL << bitCount);
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				var random = random.Next64() & mask;
				buckets[random] += 1;
			}

			Assert.LessOrEqual(CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket);
		}

		public static void ValidateChanceDistribution(IRandomEngine random, int numerator, int denominator, int trialCount, float tolerance)
		{
			int falseCount = 0;
			int trueCount = 0;
			for (int i = 0; i < trialCount; ++i)
			{
				if (Chance.Probability(numerator, denominator, random))
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
			ValidateNext32BitsDistribution(SystemRandomEngine.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(SplitMix64.Create(seed), 4, 10000, 0.02f);
			ValidateNext32BitsDistribution(XorShift128Plus.Create(seed), 4, 10000, 0.02f);

			ValidateNext64BitsDistribution(SystemRandomEngine.Create(seed), 4, 10000, 0.02f);
			ValidateNext64BitsDistribution(SplitMix64.Create(seed), 4, 10000, 0.02f);
			ValidateNext64BitsDistribution(XorShift128Plus.Create(seed), 4, 10000, 0.02f);
		}

		[Test]
		public void Next5BitsDistribution()
		{
			ValidateNext32BitsDistribution(SystemRandomEngine.Create(seed), 5, 10000, 0.03f);
			ValidateNext32BitsDistribution(SplitMix64.Create(seed), 5, 10000, 0.03f);
			ValidateNext32BitsDistribution(XorShift128Plus.Create(seed), 5, 10000, 0.03f);

			ValidateNext64BitsDistribution(SystemRandomEngine.Create(seed), 5, 10000, 0.03f);
			ValidateNext64BitsDistribution(SplitMix64.Create(seed), 5, 10000, 0.03f);
			ValidateNext64BitsDistribution(XorShift128Plus.Create(seed), 5, 10000, 0.03f);
		}

		[Test]
		public void ChancePowerOfTwoDenominatorDistribution()
		{
			ValidateChanceDistribution(SystemRandomEngine.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(SplitMix64.Create(seed), 25, 32, 100000, 0.002f);
			ValidateChanceDistribution(XorShift128Plus.Create(seed), 25, 32, 100000, 0.002f);
		}

		[Test]
		public void ChanceNonPowerOfTwoDenominatorDistribution()
		{
			ValidateChanceDistribution(SystemRandomEngine.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(SplitMix64.Create(seed), 17, 25, 100000, 0.003f);
			ValidateChanceDistribution(XorShift128Plus.Create(seed), 17, 25, 100000, 0.003f);
		}

		[Test]
		public void ChanceDistribution()
		{
			System.Action<int, int> validateRatio = (int numerator, int denominator) =>
			{
				ValidateChanceDistribution(SystemRandomEngine.Create(seed), numerator, denominator, 1000, 0.04f);
				ValidateChanceDistribution(SplitMix64.Create(seed), numerator, denominator, 1000, 0.04f);
				ValidateChanceDistribution(XorShift128Plus.Create(seed), numerator, denominator, 10000, 0.04f);
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
		public void First32_SplitMix64()
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
		public void First32_XoroShiro128Plus()
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
		public void First32_XorShift128Plus()
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
		public void First32_XorShift1024Star()
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
	}
}
#endif
