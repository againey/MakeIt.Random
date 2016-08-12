/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using System.Text;

namespace Experilous.Randomization
{
	/// <summary>
	/// A static utility class to make it easier to seed PRNGs from a variety of common seed formats.
	/// </summary>
	public class RandomStateGenerator : IBitGenerator
	{
		private byte[] _seedData;
		private int _seedOffset;
		private int _seedOffsetIncrement;
		private int _callCount;

		public RandomStateGenerator()
		{
			_seedData = BitConverter.GetBytes(Environment.TickCount);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(int seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(uint seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(long seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(ulong seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(float seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(double seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(params int[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(int)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(params uint[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(uint)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(params long[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(long)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(params ulong[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(ulong)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(params float[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(float)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(params double[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(double)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(byte[] seedData)
		{
			if (seedData == null || seedData.Length == 0) throw new ArgumentException();
			_seedData = seedData;
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		public RandomStateGenerator(string seed)
		{
			if (seed == null || seed.Length == 0) throw new ArgumentException();
			_seedData = new UTF8Encoding().GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		private static readonly int[] _primeNumbers = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };

		private static int GetSeedOffsetIncrement(int seedLength)
		{
			if (seedLength == 1) return 0;

			int seedOffsetIncrement = 1;
			foreach (int primeNumber in _primeNumbers)
			{
				if (primeNumber * primeNumber >= seedLength) break;
				if (seedLength % primeNumber != 0) seedOffsetIncrement = primeNumber;
			}

			return seedOffsetIncrement;
		}

		private const uint _hashInitializer32 = 2166136261U;
		private const uint _hashMultiplier32 = 16777619U;

		/// <summary>
		/// Generate the next 32-bit unsigned integer for use as part of a PRNG's initial state.
		/// </summary>
		/// <returns>A 32-bit unsigned integer based on a hash of the seed bytes.</returns>
		/// <remarks>
		/// <para>Uses the <a href="http://www.isthe.com/chongo/tech/comp/fnv/">FNV-1a</a> hash function,
		/// developed by Glenn Fowler, Phong Vo, and Landon Curt Noll.</para>
		/// <para>Starts from the current seed offset index, and then wraps around to the beginning of the
		/// seed data array and continues back to just before where it started.  Afterwards, the seed offset
		/// is incremented so that subsequent calls produce a different hash value.</para>
		/// </remarks>
		public uint Next32()
		{
			uint h = _hashInitializer32;
			h = (h ^ (uint)(_callCount++)) * _hashMultiplier32;
			for (int i = _seedOffset; i < _seedData.Length; ++i)
			{
				h = (h ^ _seedData[i]) * _hashMultiplier32;
			}
			for (int i = 0; i < _seedOffset; ++i)
			{
				h = (h ^ _seedData[i]) * _hashMultiplier32;
			}
			_seedOffset = (_seedOffset + _seedOffsetIncrement) % _seedData.Length;
			return h;
		}

		private const ulong _hashInitializer64 = 14695981039346656037UL;
		private const ulong _hashMultiplier64 = 1099511628211UL;

		/// <summary>
		/// Generate the next 64-bit unsigned integer for use as part of a PRNG's initial state.
		/// </summary>
		/// <returns>A 64-bit unsigned integer based on a hash of the seed bytes.</returns>
		/// <remarks>
		/// <para>Uses the <a href="http://www.isthe.com/chongo/tech/comp/fnv/">FNV-1a</a> hash function,
		/// developed by Glenn Fowler, Phong Vo, and Landon Curt Noll.</para>
		/// <para>Starts from the current seed offset index, and then wraps around to the beginning of the
		/// seed data array and continues back to just before where it started.  Afterwards, the seed offset
		/// is incremented so that subsequent calls produce a different hash value.</para>
		/// </remarks>
		public ulong Next64()
		{
			ulong h = _hashInitializer64;
			h = (h ^ (ulong)(_callCount++)) * _hashMultiplier64;
			for (int i = _seedOffset; i < _seedData.Length; ++i)
			{
				h = (h ^ _seedData[i]) * _hashMultiplier64;
			}
			for (int i = 0; i < _seedOffset; ++i)
			{
				h = (h ^ _seedData[i]) * _hashMultiplier64;
			}
			_seedOffset = (_seedOffset + _seedOffsetIncrement) % _seedData.Length;
			return h;
		}
	}
}
