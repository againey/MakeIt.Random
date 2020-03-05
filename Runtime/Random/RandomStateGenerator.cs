/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

using System;
using System.Text;

namespace MakeIt.Random
{
	/// <summary>
	/// A static utility class to make it easier to seed PRNGs from a variety of common seed formats.
	/// </summary>
	public class RandomStateGenerator : IBitGenerator, IEquatable<RandomStateGenerator>
	{
		private byte[] _seedData;
		private int _seedOffset;
		private int _seedOffsetIncrement;
		private int _callSeed;
		private static int _unstableSeed = 0;

		private const int _internalSeedIncrement = -1511514573; //the signed representation of 2783452723, a semi-arbitrary prime number acquired with the help of http://compoasso.free.fr/primelistweb/page/prime/liste_online_en.php

		/// <summary>
		/// Constructs a random state generator which is initialized with unstable data, appropriate for seeding a random engine with unpredictable data.
		/// </summary>
		/// <remarks>
		/// This constructor uses a combination of various transient values to initialize the seed data, such
		/// as the system time, the time since startup, process id and other process information, and a private,
		/// global, and thread-safe numeric value which changes each time this constructor is called.
		/// </remarks>
		public RandomStateGenerator()
		{
#if UNITY_STANDALONE_WIN && UNITY_5_4_OR_NEWER
			System.IO.MemoryStream stream = new System.IO.MemoryStream(sizeof(int) * 3 + sizeof(long) * 4);
#else
			System.IO.MemoryStream stream = new System.IO.MemoryStream(sizeof(int) * 3 + sizeof(long) * 3);
#endif
			System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
			System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
			writer.Write(DateTime.UtcNow.Ticks);
			writer.Write(Environment.TickCount);
			writer.Write(process.Id);
			writer.Write(process.UserProcessorTime.Ticks);
			writer.Write(process.PrivilegedProcessorTime.Ticks);
#if UNITY_STANDALONE_WIN && UNITY_5_4_OR_NEWER
			writer.Write(process.MainModule.BaseAddress.ToInt64());
#endif
			writer.Write(System.Threading.Interlocked.Add(ref _unstableSeed, _internalSeedIncrement));
			_seedData = stream.GetBuffer();
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seed"/>.
		/// </summary>
		/// <param name="seed">The integer value which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(int seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seed"/>.
		/// </summary>
		/// <param name="seed">The unsigned integer value which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(uint seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seed"/>.
		/// </summary>
		/// <param name="seed">The long value which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(long seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seed"/>.
		/// </summary>
		/// <param name="seed">The unsigned long value which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(ulong seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seed"/>.
		/// </summary>
		/// <param name="seed">The float value which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(float seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seed"/>.
		/// </summary>
		/// <param name="seed">The double value which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(double seed)
		{
			_seedData = BitConverter.GetBytes(seed);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seeds"/>.
		/// </summary>
		/// <param name="seeds">The array of integer values which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(params int[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(int)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seeds"/>.
		/// </summary>
		/// <param name="seeds">The array of unsigned integer values which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(params uint[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(uint)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seeds"/>.
		/// </summary>
		/// <param name="seeds">The array of long values which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(params long[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(long)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seeds"/>.
		/// </summary>
		/// <param name="seeds">The array of unsigned long values which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(params ulong[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(ulong)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seeds"/>.
		/// </summary>
		/// <param name="seeds">The array of float values which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(params float[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(float)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seeds"/>.
		/// </summary>
		/// <param name="seeds">The array of double values which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(params double[] seeds)
		{
			if (seeds == null || seeds.Length == 0) throw new ArgumentException();
			_seedData = new byte[seeds.Length * sizeof(double)];
			Buffer.BlockCopy(seeds, 0, _seedData, 0, _seedData.Length);
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seedData"/>.
		/// </summary>
		/// <param name="seedData">The array of bytes which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
		public RandomStateGenerator(byte[] seedData)
		{
			if (seedData == null || seedData.Length == 0) throw new ArgumentException();
			_seedData = seedData;
			_seedOffsetIncrement = GetSeedOffsetIncrement(_seedData.Length);
		}

		/// <summary>
		/// Constructs a random state generator which is initialized with the binary data of the supplied <paramref name="seed"/>.
		/// </summary>
		/// <param name="seed">The string value which will be used to indirectly determine the sequence of values returned by <see cref="Next32()"/> or <see cref="Next64()"/>.</param>
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
		/// <returns>A 32-bit unsigned integer based on a hash of the seed bytes, as well as the current call count.</returns>
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
			h = (h ^ (uint)_callSeed) * _hashMultiplier32;
			_callSeed += _internalSeedIncrement;
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
		/// <returns>A 64-bit unsigned integer based on a hash of the seed bytes, as well as the current call count.</returns>
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
			h = (h ^ (ulong)_callSeed) * _hashMultiplier64;
			_callSeed += _internalSeedIncrement;
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

		/// <summary>
		/// Get the next 64 bits of generated data as two 32-bit values.
		/// Generate the next 64 bits of hashed data as two 32-bit unsigned integers for use as part of a PRNG's initial state.
		/// </summary>
		/// <param name="lower">The lower 32 bits of hashed data.</param>
		/// <param name="upper">The upper 32 bits of hashed data.</param>
		public virtual void Next64(out uint lower, out uint upper)
		{
			ulong next = Next64();
			lower = (uint)next;
			upper = (uint)(next >> 32);
		}

		/// <summary>
		/// Checks to see if the state of two random state generators are equal.
		/// </summary>
		/// <param name="lhs">The first random state generator whose state is to be compared.</param>
		/// <param name="rhs">The second random state generator whose state is to be compared.</param>
		/// <returns>Returns true if neither random state generator is null and both have the same state, or if both are null, false otherwise.</returns>
		public static bool operator ==(RandomStateGenerator lhs, RandomStateGenerator rhs)
		{
			return lhs != null && lhs.Equals(rhs) || lhs == null && rhs == null;
		}

		/// <summary>
		/// Checks to see if the state of two random state generators are not equal.
		/// </summary>
		/// <param name="lhs">The first random state generator whose state is to be compared.</param>
		/// <param name="rhs">The second random state generator whose state is to be compared.</param>
		/// <returns>Returns false if neither random state generator is null and both have the same state, or if both are null, true otherwise.</returns>
		public static bool operator !=(RandomStateGenerator lhs, RandomStateGenerator rhs)
		{
			return lhs != null && !lhs.Equals(rhs) || lhs == null && rhs != null;
		}

		/// <summary>
		/// Checks if the specified random state generator has the same state as this one.
		/// </summary>
		/// <param name="other">The other random state generator whose state is to be compared.</param>
		/// <returns>Returns true if the other random state generator is not null and both state generators have the same state, false otherwise.</returns>
		public bool Equals(RandomStateGenerator other)
		{
			if (other == null) return false;
			if (_seedOffset != other._seedOffset) return false;
			if (_seedOffsetIncrement != other._seedOffsetIncrement) return false;
			if (_callSeed != other._callSeed) return false;
			for (int i = 0; i < _seedData.Length; ++i)
			{
				if (_seedData[i] != other._seedData[i]) return false;
			}
			return true;
		}

		/// <summary>
		/// Checks if the specified random state generator is the same type and has the same state as this one.
		/// </summary>
		/// <param name="obj">The other random state generator whose state is to be compared.</param>
		/// <returns>Returns true if the other random state generator is not null and is the same type and has the same state as this one, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as RandomStateGenerator);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			uint hashCode = (uint)(_seedOffset.GetHashCode() ^ _seedOffsetIncrement.GetHashCode() ^ _callSeed.GetHashCode());
			for (int i = 0; i < _seedData.Length; ++i)
			{
				hashCode = ((hashCode << 8) | (hashCode >> 24)) ^ (uint)_seedData[i].GetHashCode();
			}
			return (int)hashCode;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			switch (_seedData.Length)
			{
				case 1:
						return string.Format("RandomStateGenerator {{ 0x{0:X2}, {1}, {2}, {3} }}", _seedData[0], _seedOffset, _seedOffsetIncrement, _callSeed);
				case 2:
						return string.Format("RandomStateGenerator {{ 0x{0:X2}, 0x{1:X2}, {2}, {3}, {4} }}", _seedData[0], _seedData[1], _seedOffset, _seedOffsetIncrement, _callSeed);
				case 3:
						return string.Format("RandomStateGenerator {{ 0x{0:X2}, 0x{1:X2}, 0x{2:X2}, {3}, {4}, {5} }}", _seedData[0], _seedData[1], _seedData[2], _seedOffset, _seedOffsetIncrement, _callSeed);
				case 4:
						return string.Format("RandomStateGenerator {{ 0x{0:X2}, 0x{1:X2}, 0x{2:X2}, 0x{3:X2}, {4}, {5}, {6} }}", _seedData[0], _seedData[1], _seedData[2], _seedData[3], _seedOffset, _seedOffsetIncrement, _callSeed);
				default:
					if (_seedData.Length > 4)
					{
						return string.Format("RandomStateGenerator {{ 0x{0:X2}, 0x{1:X2}, 0x{2:X2}, 0x{3:X2}, ..., {4}, {5}, {6} }}", _seedData[0], _seedData[1], _seedData[2], _seedData[3], _seedOffset, _seedOffsetIncrement, _callSeed);
					}
					else
					{
						return string.Format("RandomStateGenerator {{ {0}, {1}, {2} }}", _seedOffset, _seedOffsetIncrement, _callSeed);
					}
			}
		}
	}
}
