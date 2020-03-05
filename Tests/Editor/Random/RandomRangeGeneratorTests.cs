/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_5_3_OR_NEWER
using NUnit.Framework;
using UnityEngine;
using System;

namespace MakeIt.Random.Tests
{
	class RandomRangeGeneratorTests
	{
		private const string seed = "random seed";

		private string assertMessage;

		private void ValidateRange<TNumber>(int count, TNumber lowerBoundary, TNumber upperBoundary, IRangeGenerator<TNumber> generator, Action<TNumber, TNumber> assertLowerBoundary, Action<TNumber, TNumber> assertUpperBoundary, int sampleSizePercentage)
		{
			count = (count * sampleSizePercentage) / 100;

			for (int i = 0; i < count; ++i)
			{
				var n = generator.Next();
				assertLowerBoundary(n, lowerBoundary);
				assertUpperBoundary(n, upperBoundary);
			}
		}

		private void ValidateOpenBucketDistribution<TNumber>(IRangeGenerator<TNumber> generator, Func<TNumber, int> getBucketIndex, int bucketCount, int hitsPerBucket, float tolerance, int sampleSizePercentage)
		{
			hitsPerBucket = (hitsPerBucket * sampleSizePercentage) / 100;
			tolerance = (tolerance * 100) / sampleSizePercentage;

			var buckets = new int[bucketCount];
			for (int i = 0; i < bucketCount * hitsPerBucket; ++i)
			{
				buckets[getBucketIndex(generator.Next())] += 1;
			}

			Assert.LessOrEqual(RandomeEngineTests.CalculateStandardDeviation(buckets, hitsPerBucket), tolerance * hitsPerBucket, assertMessage);
		}

		private void AssertLess(sbyte a, sbyte b) { Assert.Less(a, b, assertMessage); }
		private void AssertLessOrEqual(sbyte a, sbyte b) { Assert.LessOrEqual(a, b, assertMessage); }
		private void AssertGreater(sbyte a, sbyte b) { Assert.Greater(a, b, assertMessage); }
		private void AssertGreaterOrEqual(sbyte a, sbyte b) { Assert.GreaterOrEqual(a, b, assertMessage); }

		private void AssertLess(byte a, byte b) { Assert.Less(a, b, assertMessage); }
		private void AssertLessOrEqual(byte a, byte b) { Assert.LessOrEqual(a, b, assertMessage); }
		private void AssertGreater(byte a, byte b) { Assert.Greater(a, b, assertMessage); }
		private void AssertGreaterOrEqual(byte a, byte b) { Assert.GreaterOrEqual(a, b, assertMessage); }

		private void AssertLess(short a, short b) { Assert.Less(a, b, assertMessage); }
		private void AssertLessOrEqual(short a, short b) { Assert.LessOrEqual(a, b, assertMessage); }
		private void AssertGreater(short a, short b) { Assert.Greater(a, b, assertMessage); }
		private void AssertGreaterOrEqual(short a, short b) { Assert.GreaterOrEqual(a, b, assertMessage); }

		private void AssertLess(ushort a, ushort b) { Assert.Less(a, b, assertMessage); }
		private void AssertLessOrEqual(ushort a, ushort b) { Assert.LessOrEqual(a, b, assertMessage); }
		private void AssertGreater(ushort a, ushort b) { Assert.Greater(a, b, assertMessage); }
		private void AssertGreaterOrEqual(ushort a, ushort b) { Assert.GreaterOrEqual(a, b, assertMessage); }

		private void ValidateRange(int count, sbyte min, sbyte range, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, 10, 0.1f, sampleSizePercentage);
		}

		private void ValidateRange(int count, sbyte min, sbyte range, int bucketCount, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, bucketCount, 2f / bucketCount, sampleSizePercentage);
		}

		private void ValidateRange(int count, sbyte min, sbyte range, int bucketCount, float tolerance, int sampleSizePercentage)
		{
			assertMessage = string.Format("Min = {0}, Range = {1}, Buckets = {2}, Tolerance = {3:F4}", min, range, bucketCount, tolerance);

			ValidateRange(count, min, (sbyte)(min + range + 1), XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, (sbyte)(min + range + 1)), AssertGreater, AssertLess, sampleSizePercentage);
			ValidateRange(count, min, (sbyte)(min + range), XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, (sbyte)(min + range)), AssertGreaterOrEqual, AssertLess, sampleSizePercentage);
			ValidateRange(count, min, (sbyte)(min + range), XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, (sbyte)(min + range)), AssertGreater, AssertLessOrEqual, sampleSizePercentage);
			ValidateRange(count, min, (sbyte)(min + range - 1), XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, (sbyte)(min + range - 1)), AssertGreaterOrEqual, AssertLessOrEqual, sampleSizePercentage);

			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, (sbyte)(min + range + 1)), (sbyte n) => (n - min - 1) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, (sbyte)(min + range)), (sbyte n) => (n - min) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, (sbyte)(min + range)), (sbyte n) => (n - min - 1) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, (sbyte)(min + range - 1)), (sbyte n) => (n - min) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
		}

		private void ValidateRange(int count, byte min, byte range, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, 10, 0.1f, sampleSizePercentage);
		}

		private void ValidateRange(int count, byte min, byte range, int bucketCount, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, bucketCount, 2f / bucketCount, sampleSizePercentage);
		}

		private void ValidateRange(int count, byte min, byte range, int bucketCount, float tolerance, int sampleSizePercentage)
		{
			assertMessage = string.Format("Min = {0}, Range = {1}, Buckets = {2}, Tolerance = {3:F4}", min, range, bucketCount, tolerance);

			ValidateRange(count, min, (byte)(min + range + 1), XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, (byte)(min + range + 1)), AssertGreater, AssertLess, sampleSizePercentage);
			ValidateRange(count, min, (byte)(min + range), XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, (byte)(min + range)), AssertGreaterOrEqual, AssertLess, sampleSizePercentage);
			ValidateRange(count, min, (byte)(min + range), XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, (byte)(min + range)), AssertGreater, AssertLessOrEqual, sampleSizePercentage);
			ValidateRange(count, min, (byte)(min + range - 1), XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, (byte)(min + range - 1)), AssertGreaterOrEqual, AssertLessOrEqual, sampleSizePercentage);

			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, (byte)(min + range + 1)), (byte n) => (n - min - 1) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, (byte)(min + range)), (byte n) => (n - min) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, (byte)(min + range)), (byte n) => (n - min - 1) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, (byte)(min + range - 1)), (byte n) => (n - min) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
		}

		private void ValidateRange(int count, short min, short range, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, 10, 0.1f, sampleSizePercentage);
		}

		private void ValidateRange(int count, short min, short range, int bucketCount, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, bucketCount, 2f / bucketCount, sampleSizePercentage);
		}

		private void ValidateRange(int count, short min, short range, int bucketCount, float tolerance, int sampleSizePercentage)
		{
			assertMessage = string.Format("Min = {0}, Range = {1}, Buckets = {2}, Tolerance = {3:F4}", min, range, bucketCount, tolerance);

			ValidateRange(count, min, (short)(min + range + 1), XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, (short)(min + range + 1)), AssertGreater, AssertLess, sampleSizePercentage);
			ValidateRange(count, min, (short)(min + range), XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, (short)(min + range)), AssertGreaterOrEqual, AssertLess, sampleSizePercentage);
			ValidateRange(count, min, (short)(min + range), XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, (short)(min + range)), AssertGreater, AssertLessOrEqual, sampleSizePercentage);
			ValidateRange(count, min, (short)(min + range - 1), XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, (short)(min + range - 1)), AssertGreaterOrEqual, AssertLessOrEqual, sampleSizePercentage);

			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, (short)(min + range + 1)), (short n) => (n - min - 1) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, (short)(min + range)), (short n) => (n - min) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, (short)(min + range)), (short n) => (n - min - 1) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, (short)(min + range - 1)), (short n) => (n - min) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
		}

		private void ValidateRange(int count, ushort min, ushort range, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, 10, 0.1f, sampleSizePercentage);
		}

		private void ValidateRange(int count, ushort min, ushort range, int bucketCount, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, bucketCount, 2f / bucketCount, sampleSizePercentage);
		}

		private void ValidateRange(int count, ushort min, ushort range, int bucketCount, float tolerance, int sampleSizePercentage)
		{
			assertMessage = string.Format("Min = {0}, Range = {1}, Buckets = {2}, Tolerance = {3:F4}", min, range, bucketCount, tolerance);

			ValidateRange(count, min, (ushort)(min + range + 1), XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, (ushort)(min + range + 1)), AssertGreater, AssertLess, sampleSizePercentage);
			ValidateRange(count, min, (ushort)(min + range), XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, (ushort)(min + range)), AssertGreaterOrEqual, AssertLess, sampleSizePercentage);
			ValidateRange(count, min, (ushort)(min + range), XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, (ushort)(min + range)), AssertGreater, AssertLessOrEqual, sampleSizePercentage);
			ValidateRange(count, min, (ushort)(min + range - 1), XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, (ushort)(min + range - 1)), AssertGreaterOrEqual, AssertLessOrEqual, sampleSizePercentage);

			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, (ushort)(min + range + 1)), (ushort n) => (n - min - 1) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, (ushort)(min + range)), (ushort n) => (n - min) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, (ushort)(min + range)), (ushort n) => (n - min - 1) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, (ushort)(min + range - 1)), (ushort n) => (n - min) * bucketCount / range, bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
		}

		private void ValidateRange(int count, int min, int range, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, 10, 0.1f, sampleSizePercentage);
		}

		private void ValidateRange(int count, int min, int range, int bucketCount, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, bucketCount, 2f / bucketCount, sampleSizePercentage);
		}

		private void ValidateRange(int count, int min, int range, int bucketCount, float tolerance, int sampleSizePercentage)
		{
			assertMessage = string.Format("Min = {0}, Range = {1}, Buckets = {2}, Tolerance = {3:F4}", min, range, bucketCount, tolerance);

			ValidateRange(count, min, min + range + 1, XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, min + range + 1), Assert.Greater, Assert.Less, sampleSizePercentage);
			ValidateRange(count, min, min + range, XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, min + range), Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			ValidateRange(count, min, min + range, XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, min + range), Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			ValidateRange(count, min, min + range - 1, XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, min + range - 1), Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);

			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, min + range + 1), (int n) => (int)((n - min - 1L) * (long)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, min + range), (int n) => (int)((n - min) * (long)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, min + range), (int n) => (int)((n - min - 1L) * (long)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, min + range - 1), (int n) => (int)((n - min) * (long)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
		}

		private void ValidateRange(int count, uint min, uint range, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, 10, 0.1f, sampleSizePercentage);
		}

		private void ValidateRange(int count, uint min, uint range, int bucketCount, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, bucketCount, 2f / bucketCount, sampleSizePercentage);
		}

		private void ValidateRange(int count, uint min, uint range, int bucketCount, float tolerance, int sampleSizePercentage)
		{
			assertMessage = string.Format("Min = {0}, Range = {1}, Buckets = {2}, Tolerance = {3:F4}", min, range, bucketCount, tolerance);

			ValidateRange(count, min, min + range + 1U, XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, min + range + 1U), Assert.Greater, Assert.Less, sampleSizePercentage);
			ValidateRange(count, min, min + range, XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, min + range), Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			ValidateRange(count, min, min + range, XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, min + range), Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			ValidateRange(count, min, min + range - 1U, XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, min + range - 1U), Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);

			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, min + range + 1U), (uint n) => (int)((n - min - 1UL) * (ulong)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, min + range), (uint n) => (int)((n - min) * (ulong)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, min + range), (uint n) => (int)((n - min - 1UL) * (ulong)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, min + range - 1U), (uint n) => (int)((n - min) * (ulong)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
		}

		private void ValidateRange(int count, long min, long range, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, 10, 0.1f, sampleSizePercentage);
		}

		private void ValidateRange(int count, long min, long range, int bucketCount, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, bucketCount, 2f / bucketCount, sampleSizePercentage);
		}

		private void ValidateRange(int count, long min, long range, int bucketCount, float tolerance, int sampleSizePercentage)
		{
			assertMessage = string.Format("Min = {0}, Range = {1}, Buckets = {2}, Tolerance = {3:F4}", min, range, bucketCount, tolerance);

			ValidateRange(count, min, min + range + 1L, XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, min + range + 1L), Assert.Greater, Assert.Less, sampleSizePercentage);
			ValidateRange(count, min, min + range, XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, min + range), Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			ValidateRange(count, min, min + range, XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, min + range), Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			ValidateRange(count, min, min + range - 1L, XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, min + range - 1L), Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);

			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, min + range + 1L), (long n) => (int)((n - min - 1L) * (double)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, min + range), (long n) => (int)((n - min) * (double)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, min + range), (long n) => (int)((n - min - 1L) * (double)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, min + range - 1L), (long n) => (int)((n - min) * (double)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
		}

		private void ValidateRange(int count, ulong min, ulong range, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, 10, 0.1f, sampleSizePercentage);
		}

		private void ValidateRange(int count, ulong min, ulong range, int bucketCount, int sampleSizePercentage)
		{
			ValidateRange(count, min, range, bucketCount, 2f / bucketCount, sampleSizePercentage);
		}

		private void ValidateRange(int count, ulong min, ulong range, int bucketCount, float tolerance, int sampleSizePercentage)
		{
			assertMessage = string.Format("Min = {0}, Range = {1}, Buckets = {2}, Tolerance = {3:F4}", min, range, bucketCount, tolerance);

			ValidateRange(count, min, min + range + 1UL, XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, min + range + 1UL), Assert.Greater, Assert.Less, sampleSizePercentage);
			ValidateRange(count, min, min + range, XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, min + range), Assert.GreaterOrEqual, Assert.Less, sampleSizePercentage);
			ValidateRange(count, min, min + range, XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, min + range), Assert.Greater, Assert.LessOrEqual, sampleSizePercentage);
			ValidateRange(count, min, min + range - 1UL, XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, min + range - 1UL), Assert.GreaterOrEqual, Assert.LessOrEqual, sampleSizePercentage);

			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOOGenerator(min, min + range + 1UL), (ulong n) => (int)((n - min - 1UL) * (double)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCOGenerator(min, min + range), (ulong n) => (int)((n - min) * (double)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeOCGenerator(min, min + range), (ulong n) => (int)((n - min - 1UL) * (double)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
			ValidateOpenBucketDistribution(XorShift128Plus.Create(seed).MakeRangeCCGenerator(min, min + range - 1UL), (ulong n) => (int)((n - min) * (double)bucketCount / range), bucketCount, count / bucketCount, tolerance, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PowPow2RangeInt8(int sampleSizePercentage)
		{
			ValidateRange(10000, (sbyte)((1 << 1) / 7 + 44), (sbyte)(1 << 1), 2, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (sbyte)((1 << 2) / 7 + 44), (sbyte)(1 << 2), 4, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (sbyte)((1 << 4) / 7 + 44), (sbyte)(1 << 4), 8, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void Pow2RangeInt8(int sampleSizePercentage)
		{
			ValidateRange(10000, (sbyte)((1 << 3) / 7 + 44), (sbyte)(1 << 3), 8, sampleSizePercentage);
			ValidateRange(10000, (sbyte)((1 << 5) / 7 + 44), (sbyte)(1 << 5), 8, sampleSizePercentage);
			ValidateRange(10000, (sbyte)((1 << 6) / 7 + 44), (sbyte)(1 << 6), 8, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void AnyRangeInt8(int sampleSizePercentage)
		{
			for (int i = 1; i < 3; ++i)
			{
				sbyte min = (sbyte)Math.Round(Math.Pow(5, i) / 3 + 44);
				sbyte range = (sbyte)Math.Round(Math.Pow(3, i) + Math.Pow(5, i) / 7);
				if (range <= 20) ValidateRange(10000, min, range, range, 0.05f, sampleSizePercentage);
				else if (range <= 40) ValidateRange(10000, min, range, range / 2, 2f / range, sampleSizePercentage);
				else if (range <= 80) ValidateRange(10000, min, range, range / 4, 4f / range, sampleSizePercentage);
				else ValidateRange(10000, min, range, sampleSizePercentage);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PowPow2RangeUInt8(int sampleSizePercentage)
		{
			ValidateRange(10000, (byte)((1 << 1) / 7 + 44), (byte)(1 << 1), 2, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (byte)((1 << 2) / 7 + 44), (byte)(1 << 2), 4, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (byte)((1 << 4) / 7 + 44), (byte)(1 << 4), 8, 0.015f, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void Pow2RangeUInt8(int sampleSizePercentage)
		{
			ValidateRange(10000, (byte)((1 << 3) / 7 + 44), (byte)(1 << 3), 8, sampleSizePercentage);
			ValidateRange(10000, (byte)((1 << 5) / 7 + 44), (byte)(1 << 5), 8, sampleSizePercentage);
			ValidateRange(10000, (byte)((1 << 6) / 7 + 44), (byte)(1 << 6), 8, sampleSizePercentage);
			ValidateRange(10000, (byte)((1 << 7) / 7 + 44), (byte)(1 << 7), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void AnyRangeUInt8(int sampleSizePercentage)
		{
			for (int i = 1; i < 4; ++i)
			{
				byte min = (byte)Math.Round(Math.Pow(5, i) / 3 + 44);
				byte range = (byte)Math.Round(Math.Pow(3, i) + Math.Pow(5, i) / 7);
				if (range <= 20) ValidateRange(10000, min, range, range, 0.05f, sampleSizePercentage);
				else if (range <= 40) ValidateRange(10000, min, range, range / 2, 2f / range, sampleSizePercentage);
				else if (range <= 80) ValidateRange(10000, min, range, range / 4, 4f / range, sampleSizePercentage);
				else ValidateRange(10000, min, range, sampleSizePercentage);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PowPow2RangeInt16(int sampleSizePercentage)
		{
			ValidateRange(10000, (short)((1 << 1) / 7 + 13894), (short)(1 << 1), 2, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 2) / 7 + 13894), (short)(1 << 2), 4, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 4) / 7 + 13894), (short)(1 << 4), 8, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 8) / 7 + 13894), (short)(1 << 8), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void Pow2RangeInt16(int sampleSizePercentage)
		{
			ValidateRange(10000, (short)((1 << 3) / 7 + 13894), (short)(1 << 3), 8, sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 5) / 7 + 13894), (short)(1 << 5), 8, sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 6) / 7 + 13894), (short)(1 << 6), 8, sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 7) / 7 + 13894), (short)(1 << 7), sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 9) / 7 + 13894), (short)(1 << 9), sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 10) / 7 + 13894), (short)(1 << 10), sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 11) / 7 + 13894), (short)(1 << 11), sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 12) / 7 + 13894), (short)(1 << 12), sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 13) / 7 + 13894), (short)(1 << 13), sampleSizePercentage);
			ValidateRange(10000, (short)((1 << 14) / 7 + 13894), (short)(1 << 14), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void AnyRangeInt16(int sampleSizePercentage)
		{
			for (int i = 1; i < 7; ++i)
			{
				short min = (short)Math.Round(Math.Pow(5, i) / 3 + 13894);
				short range = (short)Math.Round(Math.Pow(3, i) + Math.Pow(5, i) / 7);
				if (range <= 20) ValidateRange(10000, min, range, range, 0.05f, sampleSizePercentage);
				else if (range <= 40) ValidateRange(10000, min, range, range / 2, 2f / range, sampleSizePercentage);
				else if (range <= 80) ValidateRange(10000, min, range, range / 4, 4f / range, sampleSizePercentage);
				else ValidateRange(10000, min, range, sampleSizePercentage);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PowPow2RangeUInt16(int sampleSizePercentage)
		{
			ValidateRange(10000, (ushort)((1 << 1) / 7 + 13894), (ushort)(1 << 1), 2, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 2) / 7 + 13894), (ushort)(1 << 2), 4, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 4) / 7 + 13894), (ushort)(1 << 4), 8, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 8) / 7 + 13894), (ushort)(1 << 8), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void Pow2RangeUInt16(int sampleSizePercentage)
		{
			ValidateRange(10000, (ushort)((1 << 3) / 7 + 13894), (ushort)(1 << 3), 8, sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 5) / 7 + 13894), (ushort)(1 << 5), 8, sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 6) / 7 + 13894), (ushort)(1 << 6), 8, sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 7) / 7 + 13894), (ushort)(1 << 7), sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 9) / 7 + 13894), (ushort)(1 << 9), sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 10) / 7 + 13894), (ushort)(1 << 10), sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 11) / 7 + 13894), (ushort)(1 << 11), sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 12) / 7 + 13894), (ushort)(1 << 12), sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 13) / 7 + 13894), (ushort)(1 << 13), sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 14) / 7 + 13894), (ushort)(1 << 14), sampleSizePercentage);
			ValidateRange(10000, (ushort)((1 << 15) / 7 + 13894), (ushort)(1 << 15), sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void AnyRangeUInt16(int sampleSizePercentage)
		{
			for (int i = 1; i < 8; ++i)
			{
				ushort min = (ushort)Math.Round(Math.Pow(5, i) / 3 + 13894);
				ushort range = (ushort)Math.Round(Math.Pow(3, i) + Math.Pow(5, i) / 7);
				if (range <= 20) ValidateRange(10000, min, range, range, 0.05f, sampleSizePercentage);
				else if (range <= 40) ValidateRange(10000, min, range, range / 2, 2f / range, sampleSizePercentage);
				else if (range <= 80) ValidateRange(10000, min, range, range / 4, 4f / range, sampleSizePercentage);
				else ValidateRange(10000, min, range, sampleSizePercentage);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PowPow2RangeInt32(int sampleSizePercentage)
		{
			ValidateRange(10000, (1 << 1) / 7 + 40722, 1 << 1, 2, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1 << 2) / 7 + 40722, 1 << 2, 4, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1 << 4) / 7 + 40722, 1 << 4, 8, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1 << 8) / 7 + 40722, 1 << 8, sampleSizePercentage);
			ValidateRange(10000, (1 << 16) / 7 + 40722, 1 << 16, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void Pow2RangeInt32(int sampleSizePercentage)
		{
			ValidateRange(10000, (1 << 3) / 7 + 40722, 1 << 3, 8, sampleSizePercentage);
			ValidateRange(10000, (1 << 5) / 7 + 40722, 1 << 5, 8, sampleSizePercentage);
			ValidateRange(10000, (1 << 6) / 7 + 40722, 1 << 6, 8, sampleSizePercentage);
			ValidateRange(10000, (1 << 7) / 7 + 40722, 1 << 7, sampleSizePercentage);
			ValidateRange(10000, (1 << 9) / 7 + 40722, 1 << 9, sampleSizePercentage);
			ValidateRange(10000, (1 << 10) / 7 + 40722, 1 << 10, sampleSizePercentage);
			ValidateRange(10000, (1 << 11) / 7 + 40722, 1 << 11, sampleSizePercentage);
			ValidateRange(10000, (1 << 12) / 7 + 40722, 1 << 12, sampleSizePercentage);
			ValidateRange(10000, (1 << 13) / 7 + 40722, 1 << 13, sampleSizePercentage);
			ValidateRange(10000, (1 << 14) / 7 + 40722, 1 << 14, sampleSizePercentage);
			ValidateRange(10000, (1 << 15) / 7 + 40722, 1 << 15, sampleSizePercentage);
			ValidateRange(10000, (1 << 17) / 7 + 40722, 1 << 17, sampleSizePercentage);
			ValidateRange(10000, (1 << 18) / 7 + 40722, 1 << 18, sampleSizePercentage);
			ValidateRange(10000, (1 << 19) / 7 + 40722, 1 << 19, sampleSizePercentage);
			ValidateRange(10000, (1 << 20) / 7 + 40722, 1 << 20, sampleSizePercentage);
			ValidateRange(10000, (1 << 21) / 7 + 40722, 1 << 21, sampleSizePercentage);
			ValidateRange(10000, (1 << 22) / 7 + 40722, 1 << 22, sampleSizePercentage);
			ValidateRange(10000, (1 << 23) / 7 + 40722, 1 << 23, sampleSizePercentage);
			ValidateRange(10000, (1 << 24) / 7 + 40722, 1 << 24, sampleSizePercentage);
			ValidateRange(10000, (1 << 25) / 7 + 40722, 1 << 25, sampleSizePercentage);
			ValidateRange(10000, (1 << 26) / 7 + 40722, 1 << 26, sampleSizePercentage);
			ValidateRange(10000, (1 << 27) / 7 + 40722, 1 << 27, sampleSizePercentage);
			ValidateRange(10000, (1 << 28) / 7 + 40722, 1 << 28, sampleSizePercentage);
			ValidateRange(10000, (1 << 29) / 7 + 40722, 1 << 29, sampleSizePercentage);
			ValidateRange(10000, (1 << 30) / 7 + 40722, 1 << 30, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void AnyRangeInt32(int sampleSizePercentage)
		{
			for (int i = 1; i < 14; ++i)
			{
				int min = (int)Math.Round(Math.Pow(5, i) / 3 + 40722);
				int range = (int)Math.Round(Math.Pow(3, i) + Math.Pow(5, i) / 7);
				if (range <= 20) ValidateRange(10000, min, range, range, 0.05f, sampleSizePercentage);
				else if (range <= 40) ValidateRange(10000, min, range, range / 2, 2f / range, sampleSizePercentage);
				else if (range <= 80) ValidateRange(10000, min, range, range / 4, 4f / range, sampleSizePercentage);
				else ValidateRange(10000, min, range, sampleSizePercentage);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PowPow2RangeUInt32(int sampleSizePercentage)
		{
			ValidateRange(10000, (1U << 1) / 7 + 40722, 1U << 1, 2, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1U << 2) / 7 + 40722, 1U << 2, 4, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1U << 4) / 7 + 40722, 1U << 4, 8, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1U << 8) / 7 + 40722, 1U << 8, sampleSizePercentage);
			ValidateRange(10000, (1U << 16) / 7 + 40722, 1U << 16, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void Pow2RangeUInt32(int sampleSizePercentage)
		{
			ValidateRange(10000, (1U << 3) / 7 + 40722, 1U << 3, 8, sampleSizePercentage);
			ValidateRange(10000, (1U << 5) / 7 + 40722, 1U << 5, 8, sampleSizePercentage);
			ValidateRange(10000, (1U << 6) / 7 + 40722, 1U << 6, 8, sampleSizePercentage);
			ValidateRange(10000, (1U << 7) / 7 + 40722, 1U << 7, sampleSizePercentage);
			ValidateRange(10000, (1U << 9) / 7 + 40722, 1U << 9, sampleSizePercentage);
			ValidateRange(10000, (1U << 10) / 7 + 40722, 1U << 10, sampleSizePercentage);
			ValidateRange(10000, (1U << 11) / 7 + 40722, 1U << 11, sampleSizePercentage);
			ValidateRange(10000, (1U << 12) / 7 + 40722, 1U << 12, sampleSizePercentage);
			ValidateRange(10000, (1U << 13) / 7 + 40722, 1U << 13, sampleSizePercentage);
			ValidateRange(10000, (1U << 14) / 7 + 40722, 1U << 14, sampleSizePercentage);
			ValidateRange(10000, (1U << 15) / 7 + 40722, 1U << 15, sampleSizePercentage);
			ValidateRange(10000, (1U << 17) / 7 + 40722, 1U << 17, sampleSizePercentage);
			ValidateRange(10000, (1U << 18) / 7 + 40722, 1U << 18, sampleSizePercentage);
			ValidateRange(10000, (1U << 19) / 7 + 40722, 1U << 19, sampleSizePercentage);
			ValidateRange(10000, (1U << 20) / 7 + 40722, 1U << 20, sampleSizePercentage);
			ValidateRange(10000, (1U << 21) / 7 + 40722, 1U << 21, sampleSizePercentage);
			ValidateRange(10000, (1U << 22) / 7 + 40722, 1U << 22, sampleSizePercentage);
			ValidateRange(10000, (1U << 23) / 7 + 40722, 1U << 23, sampleSizePercentage);
			ValidateRange(10000, (1U << 24) / 7 + 40722, 1U << 24, sampleSizePercentage);
			ValidateRange(10000, (1U << 25) / 7 + 40722, 1U << 25, sampleSizePercentage);
			ValidateRange(10000, (1U << 26) / 7 + 40722, 1U << 26, sampleSizePercentage);
			ValidateRange(10000, (1U << 27) / 7 + 40722, 1U << 27, sampleSizePercentage);
			ValidateRange(10000, (1U << 28) / 7 + 40722, 1U << 28, sampleSizePercentage);
			ValidateRange(10000, (1U << 29) / 7 + 40722, 1U << 29, sampleSizePercentage);
			ValidateRange(10000, (1U << 30) / 7 + 40722, 1U << 30, sampleSizePercentage);
			ValidateRange(10000, (1U << 31) / 7 + 40722, 1U << 31, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void AnyRangeUInt32(int sampleSizePercentage)
		{
			for (int i = 1; i < 15; ++i)
			{
				uint min = (uint)Math.Round(Math.Pow(5, i) / 3 + 40722);
				uint range = (uint)Math.Round(Math.Pow(3, i) + Math.Pow(5, i) / 7);
				if (range <= 20) ValidateRange(10000, min, range, (int)range, 0.05f, sampleSizePercentage);
				else if (range <= 40) ValidateRange(10000, min, range, (int)range / 2, 2f / range, sampleSizePercentage);
				else if (range <= 80) ValidateRange(10000, min, range, (int)range / 4, 4f / range, sampleSizePercentage);
				else ValidateRange(10000, min, range, sampleSizePercentage);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PowPow2RangeInt64(int sampleSizePercentage)
		{
			ValidateRange(10000, (1L << 1) / 7L + 274566545L, 1L << 1, 2, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1L << 2) / 7L + 274566545L, 1L << 2, 4, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1L << 4) / 7L + 274566545L, 1L << 4, 8, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1L << 8) / 7L + 274566545L, 1L << 8, sampleSizePercentage);
			ValidateRange(10000, (1L << 16) / 7L + 274566545L, 1L << 16, sampleSizePercentage);
			ValidateRange(10000, (1L << 32) / 7L + 274566545L, 1L << 32, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void Pow2RangeInt64(int sampleSizePercentage)
		{
			ValidateRange(10000, (1L << 3) / 7 + 274566545, 1L << 3, 8, sampleSizePercentage);
			ValidateRange(10000, (1L << 5) / 7 + 274566545, 1L << 5, 8, sampleSizePercentage);
			ValidateRange(10000, (1L << 6) / 7 + 274566545, 1L << 6, 8, sampleSizePercentage);
			ValidateRange(10000, (1L << 7) / 7 + 274566545, 1L << 7, sampleSizePercentage);
			ValidateRange(10000, (1L << 9) / 7 + 274566545, 1L << 9, sampleSizePercentage);
			ValidateRange(10000, (1L << 10) / 7 + 274566545, 1L << 10, sampleSizePercentage);
			ValidateRange(10000, (1L << 11) / 7 + 274566545, 1L << 11, sampleSizePercentage);
			ValidateRange(10000, (1L << 12) / 7 + 274566545, 1L << 12, sampleSizePercentage);
			ValidateRange(10000, (1L << 13) / 7 + 274566545, 1L << 13, sampleSizePercentage);
			ValidateRange(10000, (1L << 14) / 7 + 274566545, 1L << 14, sampleSizePercentage);
			ValidateRange(10000, (1L << 15) / 7 + 274566545, 1L << 15, sampleSizePercentage);
			ValidateRange(10000, (1L << 17) / 7 + 274566545, 1L << 17, sampleSizePercentage);
			ValidateRange(10000, (1L << 18) / 7 + 274566545, 1L << 18, sampleSizePercentage);
			ValidateRange(10000, (1L << 19) / 7 + 274566545, 1L << 19, sampleSizePercentage);
			ValidateRange(10000, (1L << 20) / 7 + 274566545, 1L << 20, sampleSizePercentage);
			ValidateRange(10000, (1L << 21) / 7 + 274566545, 1L << 21, sampleSizePercentage);
			ValidateRange(10000, (1L << 22) / 7 + 274566545, 1L << 22, sampleSizePercentage);
			ValidateRange(10000, (1L << 23) / 7 + 274566545, 1L << 23, sampleSizePercentage);
			ValidateRange(10000, (1L << 24) / 7 + 274566545, 1L << 24, sampleSizePercentage);
			ValidateRange(10000, (1L << 25) / 7 + 274566545, 1L << 25, sampleSizePercentage);
			ValidateRange(10000, (1L << 26) / 7 + 274566545, 1L << 26, sampleSizePercentage);
			ValidateRange(10000, (1L << 27) / 7 + 274566545, 1L << 27, sampleSizePercentage);
			ValidateRange(10000, (1L << 28) / 7 + 274566545, 1L << 28, sampleSizePercentage);
			ValidateRange(10000, (1L << 29) / 7 + 274566545, 1L << 29, sampleSizePercentage);
			ValidateRange(10000, (1L << 30) / 7 + 274566545, 1L << 30, sampleSizePercentage);
			ValidateRange(10000, (1L << 31) / 7 + 274566545, 1L << 31, sampleSizePercentage);
			ValidateRange(10000, (1L << 33) / 7 + 274566545, 1L << 33, sampleSizePercentage);
			ValidateRange(10000, (1L << 34) / 7 + 274566545, 1L << 34, sampleSizePercentage);
			ValidateRange(10000, (1L << 35) / 7 + 274566545, 1L << 35, sampleSizePercentage);
			ValidateRange(10000, (1L << 36) / 7 + 274566545, 1L << 36, sampleSizePercentage);
			ValidateRange(10000, (1L << 37) / 7 + 274566545, 1L << 37, sampleSizePercentage);
			ValidateRange(10000, (1L << 38) / 7 + 274566545, 1L << 38, sampleSizePercentage);
			ValidateRange(10000, (1L << 39) / 7 + 274566545, 1L << 39, sampleSizePercentage);
			ValidateRange(10000, (1L << 40) / 7 + 274566545, 1L << 40, sampleSizePercentage);
			ValidateRange(10000, (1L << 41) / 7 + 274566545, 1L << 41, sampleSizePercentage);
			ValidateRange(10000, (1L << 42) / 7 + 274566545, 1L << 42, sampleSizePercentage);
			ValidateRange(10000, (1L << 43) / 7 + 274566545, 1L << 43, sampleSizePercentage);
			ValidateRange(10000, (1L << 44) / 7 + 274566545, 1L << 44, sampleSizePercentage);
			ValidateRange(10000, (1L << 45) / 7 + 274566545, 1L << 45, sampleSizePercentage);
			ValidateRange(10000, (1L << 46) / 7 + 274566545, 1L << 46, sampleSizePercentage);
			ValidateRange(10000, (1L << 47) / 7 + 274566545, 1L << 47, sampleSizePercentage);
			ValidateRange(10000, (1L << 48) / 7 + 274566545, 1L << 48, sampleSizePercentage);
			ValidateRange(10000, (1L << 49) / 7 + 274566545, 1L << 49, sampleSizePercentage);
			ValidateRange(10000, (1L << 50) / 7 + 274566545, 1L << 50, sampleSizePercentage);
			ValidateRange(10000, (1L << 51) / 7 + 274566545, 1L << 51, sampleSizePercentage);
			ValidateRange(10000, (1L << 52) / 7 + 274566545, 1L << 52, sampleSizePercentage);
			ValidateRange(10000, (1L << 53) / 7 + 274566545, 1L << 53, sampleSizePercentage);
			ValidateRange(10000, (1L << 54) / 7 + 274566545, 1L << 54, sampleSizePercentage);
			ValidateRange(10000, (1L << 55) / 7 + 274566545, 1L << 55, sampleSizePercentage);
			ValidateRange(10000, (1L << 56) / 7 + 274566545, 1L << 56, sampleSizePercentage);
			ValidateRange(10000, (1L << 57) / 7 + 274566545, 1L << 57, sampleSizePercentage);
			ValidateRange(10000, (1L << 58) / 7 + 274566545, 1L << 58, sampleSizePercentage);
			ValidateRange(10000, (1L << 59) / 7 + 274566545, 1L << 59, sampleSizePercentage);
			ValidateRange(10000, (1L << 60) / 7 + 274566545, 1L << 60, sampleSizePercentage);
			ValidateRange(10000, (1L << 61) / 7 + 274566545, 1L << 61, sampleSizePercentage);
			ValidateRange(10000, (1L << 62) / 7 + 274566545, 1L << 62, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void AnyRangeInt64(int sampleSizePercentage)
		{
			for (int i = 1; i < 29; ++i)
			{
				long min = (long)Math.Round(Math.Pow(5, i) / 3 + 274566545);
				long range = (long)Math.Round(Math.Pow(3, i) + Math.Pow(5, i) / 7);
				if (range <= 20) ValidateRange(10000, min, range, (int)range, 0.05f, sampleSizePercentage);
				else if (range <= 40) ValidateRange(10000, min, range, (int)range / 2, 2f / range, sampleSizePercentage);
				else if (range <= 80) ValidateRange(10000, min, range, (int)range / 4, 4f / range, sampleSizePercentage);
				else ValidateRange(10000, min, range, sampleSizePercentage);
			}
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void PowPow2RangeUInt64(int sampleSizePercentage)
		{
			ValidateRange(10000, (1UL  << 1) / 7L + 274566545L, 1UL  << 1, 2, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1UL  << 2) / 7L + 274566545L, 1UL  << 2, 4, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1UL  << 4) / 7L + 274566545L, 1UL  << 4, 8, 0.015f, sampleSizePercentage);
			ValidateRange(10000, (1UL  << 8) / 7L + 274566545L, 1UL  << 8, sampleSizePercentage);
			ValidateRange(10000, (1UL  << 16) / 7L + 274566545L, 1UL  << 16, sampleSizePercentage);
			ValidateRange(10000, (1UL  << 32) / 7L + 274566545L, 1UL  << 32, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void Pow2RangeUInt64(int sampleSizePercentage)
		{
			ValidateRange(10000, (1UL << 3) / 7 + 274566545, 1UL << 3, 8, sampleSizePercentage);
			ValidateRange(10000, (1UL << 5) / 7 + 274566545, 1UL << 5, 8, sampleSizePercentage);
			ValidateRange(10000, (1UL << 6) / 7 + 274566545, 1UL << 6, 8, sampleSizePercentage);
			ValidateRange(10000, (1UL << 7) / 7 + 274566545, 1UL << 7, sampleSizePercentage);
			ValidateRange(10000, (1UL << 9) / 7 + 274566545, 1UL << 9, sampleSizePercentage);
			ValidateRange(10000, (1UL << 10) / 7 + 274566545, 1UL << 10, sampleSizePercentage);
			ValidateRange(10000, (1UL << 11) / 7 + 274566545, 1UL << 11, sampleSizePercentage);
			ValidateRange(10000, (1UL << 12) / 7 + 274566545, 1UL << 12, sampleSizePercentage);
			ValidateRange(10000, (1UL << 13) / 7 + 274566545, 1UL << 13, sampleSizePercentage);
			ValidateRange(10000, (1UL << 14) / 7 + 274566545, 1UL << 14, sampleSizePercentage);
			ValidateRange(10000, (1UL << 15) / 7 + 274566545, 1UL << 15, sampleSizePercentage);
			ValidateRange(10000, (1UL << 17) / 7 + 274566545, 1UL << 17, sampleSizePercentage);
			ValidateRange(10000, (1UL << 18) / 7 + 274566545, 1UL << 18, sampleSizePercentage);
			ValidateRange(10000, (1UL << 19) / 7 + 274566545, 1UL << 19, sampleSizePercentage);
			ValidateRange(10000, (1UL << 20) / 7 + 274566545, 1UL << 20, sampleSizePercentage);
			ValidateRange(10000, (1UL << 21) / 7 + 274566545, 1UL << 21, sampleSizePercentage);
			ValidateRange(10000, (1UL << 22) / 7 + 274566545, 1UL << 22, sampleSizePercentage);
			ValidateRange(10000, (1UL << 23) / 7 + 274566545, 1UL << 23, sampleSizePercentage);
			ValidateRange(10000, (1UL << 24) / 7 + 274566545, 1UL << 24, sampleSizePercentage);
			ValidateRange(10000, (1UL << 25) / 7 + 274566545, 1UL << 25, sampleSizePercentage);
			ValidateRange(10000, (1UL << 26) / 7 + 274566545, 1UL << 26, sampleSizePercentage);
			ValidateRange(10000, (1UL << 27) / 7 + 274566545, 1UL << 27, sampleSizePercentage);
			ValidateRange(10000, (1UL << 28) / 7 + 274566545, 1UL << 28, sampleSizePercentage);
			ValidateRange(10000, (1UL << 29) / 7 + 274566545, 1UL << 29, sampleSizePercentage);
			ValidateRange(10000, (1UL << 30) / 7 + 274566545, 1UL << 30, sampleSizePercentage);
			ValidateRange(10000, (1UL << 31) / 7 + 274566545, 1UL << 31, sampleSizePercentage);
			ValidateRange(10000, (1UL << 33) / 7 + 274566545, 1UL << 33, sampleSizePercentage);
			ValidateRange(10000, (1UL << 34) / 7 + 274566545, 1UL << 34, sampleSizePercentage);
			ValidateRange(10000, (1UL << 35) / 7 + 274566545, 1UL << 35, sampleSizePercentage);
			ValidateRange(10000, (1UL << 36) / 7 + 274566545, 1UL << 36, sampleSizePercentage);
			ValidateRange(10000, (1UL << 37) / 7 + 274566545, 1UL << 37, sampleSizePercentage);
			ValidateRange(10000, (1UL << 38) / 7 + 274566545, 1UL << 38, sampleSizePercentage);
			ValidateRange(10000, (1UL << 39) / 7 + 274566545, 1UL << 39, sampleSizePercentage);
			ValidateRange(10000, (1UL << 40) / 7 + 274566545, 1UL << 40, sampleSizePercentage);
			ValidateRange(10000, (1UL << 41) / 7 + 274566545, 1UL << 41, sampleSizePercentage);
			ValidateRange(10000, (1UL << 42) / 7 + 274566545, 1UL << 42, sampleSizePercentage);
			ValidateRange(10000, (1UL << 43) / 7 + 274566545, 1UL << 43, sampleSizePercentage);
			ValidateRange(10000, (1UL << 44) / 7 + 274566545, 1UL << 44, sampleSizePercentage);
			ValidateRange(10000, (1UL << 45) / 7 + 274566545, 1UL << 45, sampleSizePercentage);
			ValidateRange(10000, (1UL << 46) / 7 + 274566545, 1UL << 46, sampleSizePercentage);
			ValidateRange(10000, (1UL << 47) / 7 + 274566545, 1UL << 47, sampleSizePercentage);
			ValidateRange(10000, (1UL << 48) / 7 + 274566545, 1UL << 48, sampleSizePercentage);
			ValidateRange(10000, (1UL << 49) / 7 + 274566545, 1UL << 49, sampleSizePercentage);
			ValidateRange(10000, (1UL << 50) / 7 + 274566545, 1UL << 50, sampleSizePercentage);
			ValidateRange(10000, (1UL << 51) / 7 + 274566545, 1UL << 51, sampleSizePercentage);
			ValidateRange(10000, (1UL << 52) / 7 + 274566545, 1UL << 52, sampleSizePercentage);
			ValidateRange(10000, (1UL << 53) / 7 + 274566545, 1UL << 53, sampleSizePercentage);
			ValidateRange(10000, (1UL << 54) / 7 + 274566545, 1UL << 54, sampleSizePercentage);
			ValidateRange(10000, (1UL << 55) / 7 + 274566545, 1UL << 55, sampleSizePercentage);
			ValidateRange(10000, (1UL << 56) / 7 + 274566545, 1UL << 56, sampleSizePercentage);
			ValidateRange(10000, (1UL << 57) / 7 + 274566545, 1UL << 57, sampleSizePercentage);
			ValidateRange(10000, (1UL << 58) / 7 + 274566545, 1UL << 58, sampleSizePercentage);
			ValidateRange(10000, (1UL << 59) / 7 + 274566545, 1UL << 59, sampleSizePercentage);
			ValidateRange(10000, (1UL << 60) / 7 + 274566545, 1UL << 60, sampleSizePercentage);
			ValidateRange(10000, (1UL << 61) / 7 + 274566545, 1UL << 61, sampleSizePercentage);
			ValidateRange(10000, (1UL << 62) / 7 + 274566545, 1UL << 62, sampleSizePercentage);
			ValidateRange(10000, (1UL << 63) / 7 + 274566545, 1UL << 63, sampleSizePercentage);
		}

		[TestCase(100, Category = "Statistical")]
		[TestCase(1, Category = "Statistical, Smoke")]
		public void AnyRangeUInt64(int sampleSizePercentage)
		{
			for (int i = 1; i < 29; ++i)
			{
				ulong min = (ulong)Math.Round(Math.Pow(5, i) / 3 + 274566545);
				ulong range = (ulong)Math.Round(Math.Pow(3, i) + Math.Pow(5, i) / 7);
				if (range <= 20) ValidateRange(10000, min, range, (int)range, 0.05f, sampleSizePercentage);
				else if (range <= 40) ValidateRange(10000, min, range, (int)range / 2, 2f / range, sampleSizePercentage);
				else if (range <= 80) ValidateRange(10000, min, range, (int)range / 4, 4f / range, sampleSizePercentage);
				else ValidateRange(10000, min, range, sampleSizePercentage);
			}
		}
	}
}
#endif
