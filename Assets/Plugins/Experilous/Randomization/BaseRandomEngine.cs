using System;
using UnityEngine;

namespace Experilous.Randomization
{
	/// <summary>
	/// An abstract base class that eases the implementation of a random engine.
	/// </summary>
	/// <remarks>
	/// <para>By implementing most random-producing functions in terms of a smaller core set of random-producing
	/// functions, the final implementation becomes easier to right.  As a consequence, however, this is likely to be
	/// less efficient due to potentially throwing away some random bits.  For random engines that generate random bits
	/// very quickly but in specific chunk sizes, this is okay and probably even preferable for performance.  But for
	/// random engines that are slow about generating random bits (such as a cryptographically strong PRNG), this base
	/// class is not ideal.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	public abstract class BaseRandomEngine : ScriptableObject, IRandomEngine
	{
		public abstract void Seed();
		public abstract void Seed(int seed);
		public abstract void Seed(params int[] seed);
		public abstract void Seed(string seed);
		public abstract void Seed(IRandomEngine seeder);

		public abstract void MergeSeed();
		public abstract void MergeSeed(int seed);
		public abstract void MergeSeed(params int[] seed);
		public abstract void MergeSeed(string seed);
		public abstract void MergeSeed(IRandomEngine seeder);

		public abstract uint Next32();
		public abstract ulong Next64();

		public uint Next32(int bitCount)
		{
			if (bitCount == 0) return 0U;
			return Next32() & (uint.MaxValue >> (32 - bitCount));
		}

		public ulong Next64(int bitCount)
		{
			if (bitCount == 0) return 0UL;
			return Next64() & (ulong.MaxValue >> (64 - bitCount));
		}

		public uint NextLessThan(uint upperBound)
		{
			if (upperBound == 0) throw new System.ArgumentOutOfRangeException("upperBound");
			var bitsNeeded = MathUtility.Log2Ceil(upperBound);
			uint random;
			do
			{
				random = Next32(bitsNeeded);
			}
			while (random >= upperBound);
			return random;
		}

		public uint NextLessThanOrEqual(uint upperBound)
		{
			var bitsNeeded = MathUtility.Plus1Log2Ceil(upperBound);
			uint random;
			do
			{
				random = Next32(bitsNeeded);
			}
			while (random > upperBound);
			return random;
		}

		public ulong NextLessThan(ulong upperBound)
		{
			if (upperBound == 0) throw new System.ArgumentOutOfRangeException("upperBound");
			var bitsNeeded = MathUtility.Log2Ceil(upperBound);
			ulong random;
			do
			{
				random = Next64(bitsNeeded);
			}
			while (random >= upperBound);
			return random;
		}

		public ulong NextLessThanOrEqual(ulong upperBound)
		{
			var bitsNeeded = MathUtility.Plus1Log2Ceil(upperBound);
			ulong random;
			do
			{
				random = Next64(bitsNeeded);
			}
			while (random > upperBound);
			return random;
		}

		public abstract System.Random AsSystemRandom();
	}
}
