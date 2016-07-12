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

		private static readonly byte[] _maskShiftTable32 = new byte[]
		{
			31, 22, 30, 21, 18, 10, 29,  2, 20, 17, 15, 13,  9,  6, 28,  1,
			23, 19, 11,  3, 16, 14,  7, 24, 12,  4,  8, 25,  5, 26, 27,  0,
		};

		private static readonly byte[] _log2CeilTable32 = new byte[]
		{
			 1, 10,  2, 11, 14, 22,  3, 30, 12, 15, 17, 19, 23, 26,  4, 31,
			 9, 13, 21, 29, 16, 18, 25,  8, 20, 28, 24,  7, 27,  6,  5, 32,
		};

		private static readonly uint[] _maskTable32 = new uint[]
		{
			0x00000001U,
			0x000003FFU,
			0x00000003U,
			0x000007FFU,
			0x00003FFFU,
			0x003FFFFFU,
			0x00000007U,
			0x3FFFFFFFU,
			0x00000FFFU,
			0x00007FFFU,
			0x0001FFFFU,
			0x0007FFFFU,
			0x007FFFFFU,
			0x03FFFFFFU,
			0x0000000FU,
			0x7FFFFFFFU,
			0x000001FFU,
			0x00001FFFU,
			0x001FFFFFU,
			0x1FFFFFFFU,
			0x0000FFFFU,
			0x0003FFFFU,
			0x01FFFFFFU,
			0x000000FFU,
			0x000FFFFFU,
			0x0FFFFFFFU,
			0x00FFFFFFU,
			0x0000007FU,
			0x07FFFFFFU,
			0x0000003FU,
			0x0000001FU,
			0xFFFFFFFFU,
		};

		private static readonly byte[] _shiftTable64 = new byte[]
		{
			63,  5, 62,  4, 16, 10, 61,  3, 24, 15, 36,  9, 30, 21, 60,  2,
			12, 26, 23, 14, 45, 35, 43,  8, 33, 29, 52, 20, 49, 41, 59,  1,
			 6, 17, 11, 25, 37, 31, 22, 13, 27, 46, 44, 34, 53, 50, 42,  7,
			18, 38, 32, 28, 47, 54, 51, 19, 39, 48, 55, 40, 56, 57, 58,  0, 
		};

		public virtual uint NextLessThan(uint upperBound)
		{
			if (upperBound == 0) throw new System.ArgumentOutOfRangeException("upperBound");

			uint deBruijn = upperBound - 1U;
			deBruijn |= deBruijn >> 1;
			deBruijn |= deBruijn >> 2;
			deBruijn |= deBruijn >> 4;
			deBruijn |= deBruijn >> 8;
			deBruijn |= deBruijn >> 16;
			deBruijn = deBruijn * 0x07C4ACDDU >> 27;

			uint mask = _maskTable32[deBruijn];
			int bitsNeeded = _log2CeilTable32[deBruijn];

			Start:

			int bitsRemaining = 32;
			uint random = Next32();
			uint randomMasked = random & mask;

			while (randomMasked >= upperBound)
			{
				bitsRemaining -= bitsNeeded;
				if (bitsRemaining < bitsNeeded)
				{
					goto Start;
				}
				else
				{
					random = random >> bitsNeeded;
					randomMasked = random & mask;
				}
			}

			return randomMasked;
		}

		public virtual uint NextLessThanOrEqual(uint upperBound)
		{
			uint deBruijn = upperBound;
			deBruijn |= deBruijn >> 1;
			deBruijn |= deBruijn >> 2;
			deBruijn |= deBruijn >> 4;
			deBruijn |= deBruijn >> 8;
			deBruijn |= deBruijn >> 16;
			int rightShift = _maskShiftTable32[deBruijn * 0x07C4ACDDU >> 27];
			uint random;
			do
			{
				random = Next32() >> rightShift;
			}
			while (random > upperBound);
			return random;
		}

		public virtual ulong NextLessThan(ulong upperBound)
		{
			if (upperBound == 0) throw new System.ArgumentOutOfRangeException("upperBound");
			ulong deBruijn = upperBound - 1UL;
			deBruijn |= deBruijn >> 1;
			deBruijn |= deBruijn >> 2;
			deBruijn |= deBruijn >> 4;
			deBruijn |= deBruijn >> 8;
			deBruijn |= deBruijn >> 16;
			deBruijn |= deBruijn >> 32;
			int rightShift = _shiftTable64[deBruijn * 0x03F6EAF2CD271461UL >> 58];
			ulong random;
			do
			{
				random = Next64() >> rightShift;
			}
			while (random >= upperBound);
			return random;
		}

		public virtual ulong NextLessThanOrEqual(ulong upperBound)
		{
			ulong deBruijn = upperBound;
			deBruijn |= deBruijn >> 1;
			deBruijn |= deBruijn >> 2;
			deBruijn |= deBruijn >> 4;
			deBruijn |= deBruijn >> 8;
			deBruijn |= deBruijn >> 16;
			deBruijn |= deBruijn >> 32;
			int rightShift = _shiftTable64[deBruijn * 0x03F6EAF2CD271461UL >> 58];
			ulong random;
			do
			{
				random = Next64() >> rightShift;
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
