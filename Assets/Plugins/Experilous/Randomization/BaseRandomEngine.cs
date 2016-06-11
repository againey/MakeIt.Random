/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

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
		public virtual byte[] SaveState()
		{
			throw new System.NotSupportedException("This random engine is unable to save its state to a byte array.");
		}

		public virtual void RestoreState(byte[] stateArray)
		{
			throw new System.NotSupportedException("This random engine is unable to restore its state from a byte array.");
		}

		public virtual void Seed()
		{
			Seed(new RandomStateGenerator());
		}

		public virtual void Seed(int seed)
		{
			Seed(new RandomStateGenerator(seed));
		}

		public virtual void Seed(params int[] seed)
		{
			Seed(new RandomStateGenerator(seed));
		}

		public virtual void Seed(string seed)
		{
			Seed(new RandomStateGenerator(seed));
		}

		public abstract void Seed(RandomStateGenerator stateGenerator);
		public abstract void Seed(IRandomEngine seeder);

		public virtual void MergeSeed()
		{
			MergeSeed(new RandomStateGenerator());
		}

		public virtual void MergeSeed(int seed)
		{
			MergeSeed(new RandomStateGenerator(seed));
		}

		public virtual void MergeSeed(params int[] seed)
		{
			MergeSeed(new RandomStateGenerator(seed));
		}

		public virtual void MergeSeed(string seed)
		{
			MergeSeed(new RandomStateGenerator(seed));
		}

		public abstract void MergeSeed(RandomStateGenerator stateGenerator);
		public abstract void MergeSeed(IRandomEngine seeder);

		public abstract void Step();
		public abstract uint Next32();
		public abstract ulong Next64();

		public uint Next32(int bitCount)
		{
			if (bitCount == 0) return 0U;
			return Next32() >> (32 - bitCount);
		}

		public ulong Next64(int bitCount)
		{
			if (bitCount == 0) return 0UL;
			return Next64() >> (64 - bitCount);
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

		public virtual int skipAheadMagnitude { get { return 0; } }
		public virtual int skipBackMagnitude { get { return -1; } }

		public virtual void SkipAhead() { Step(); }
		public virtual void SkipBack() { throw new System.NotSupportedException("Skipping backward in the sequence is not supported by this random engine."); }

		public abstract System.Random AsSystemRandom();

		protected static void SaveState(System.IO.BinaryWriter stream, byte stateElement)
		{
			stream.Write(stateElement);
		}

		protected static void SaveState(System.IO.BinaryWriter stream, uint stateElement)
		{
			for (int i = sizeof(uint) * 8 - 8; i >= 0; i -= 8)
				stream.Write((byte)(stateElement >> i));
		}

		protected static void SaveState(System.IO.BinaryWriter stream, ulong stateElement)
		{
			for (int i = sizeof(ulong) * 8 - 8; i >= 0; i -= 8)
				stream.Write((byte)(stateElement >> i));
		}

		protected static void RestoreState(System.IO.BinaryReader reader, out byte stateElement)
		{
			stateElement = reader.ReadByte();
		}

		protected static void RestoreState(System.IO.BinaryReader reader, out uint stateElement)
		{
			stateElement = 0;
			for (int i = sizeof(uint) * 8 - 8; i >= 0; i -= 8)
				stateElement |= ((uint)reader.ReadByte()) << i;
		}

		protected static void RestoreState(System.IO.BinaryReader reader, out ulong stateElement)
		{
			stateElement = 0;
			for (int i = sizeof(ulong) * 8 - 8; i >= 0; i -= 8)
				stateElement |= ((ulong)reader.ReadByte()) << i;
		}
	}
}
