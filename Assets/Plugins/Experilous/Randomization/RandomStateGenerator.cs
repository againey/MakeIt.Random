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
	public class RandomStateGenerator
	{
		private byte[] _seedData;
		private int _seedOffset;

		public RandomStateGenerator()
		{
			_seedData = BitConverter.GetBytes(Environment.TickCount);
		}

		public RandomStateGenerator(int seed)
		{
			_seedData = BitConverter.GetBytes(seed);
		}

		public RandomStateGenerator(uint seed)
		{
			_seedData = BitConverter.GetBytes(seed);
		}

		public RandomStateGenerator(long seed)
		{
			_seedData = BitConverter.GetBytes(seed);
		}

		public RandomStateGenerator(ulong seed)
		{
			_seedData = BitConverter.GetBytes(seed);
		}

		public RandomStateGenerator(float seed)
		{
			_seedData = BitConverter.GetBytes(seed);
		}

		public RandomStateGenerator(double seed)
		{
			_seedData = BitConverter.GetBytes(seed);
		}

		public RandomStateGenerator(params int[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(int)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
		}

		public RandomStateGenerator(params uint[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(uint)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
		}

		public RandomStateGenerator(params long[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(long)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
		}

		public RandomStateGenerator(params ulong[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(ulong)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
		}

		public RandomStateGenerator(params float[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(float)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
		}

		public RandomStateGenerator(params double[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(double)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
		}

		public RandomStateGenerator(byte[] seedData)
		{
			if (seedData == null || seedData.Length == 0) throw new ArgumentException();
			_seedData = seedData;
		}

		public RandomStateGenerator(string seed)
		{
			if (seed == null || seed.Length == 0) throw new ArgumentException();
			_seedData = new UTF8Encoding().GetBytes(seed);
		}

		public int seedLength { get { return _seedData.Length; } }

		public int ComputeIdealOffsetIncrement(int hashCount)
		{
			return seedLength / hashCount;
		}

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
		public uint Next32(int offsetIncrement = 1)
		{
			uint h = 2166136261U;
			for (int i = _seedOffset; i < _seedData.Length; ++i)
			{
				h = (h ^ _seedData[i]) * 16777619U;
			}
			for (int i = 0; i < _seedOffset; ++i)
			{
				h = (h ^ _seedData[i]) * 16777619U;
			}
			_seedOffset = (_seedOffset + offsetIncrement) % _seedData.Length;
			return h;
		}

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
		public ulong Next64(int offsetIncrement = 1)
		{
			ulong h = 14695981039346656037UL;
			for (int i = _seedOffset; i < _seedData.Length; ++i)
			{
				h = (h ^ _seedData[i]) * 1099511628211UL;
			}
			for (int i = 0; i < _seedOffset; ++i)
			{
				h = (h ^ _seedData[i]) * 1099511628211UL;
			}
			_seedOffset = (_seedOffset + offsetIncrement) % _seedData.Length;
			return h;
		}
	}
}
