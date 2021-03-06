﻿/******************************************************************************\
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

using UnityEngine;

namespace MakeIt.Random
{
	/// <summary>
	/// An implementation of the <see cref="IRandom"/> interface using the 64-bit SplitMix generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG implements the SplitMix64 algorithm provided by Sebastiano Vigna, based on the
	/// following <a href="http://xorshift.di.unimi.it/splitmix64.c">implementation in C</a>.</para>
	/// <para>As its name implies, it maintains 64 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IBitGenerator"/>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	[System.Serializable]
	public sealed class SplitMix64 : RandomBase, System.IEquatable<SplitMix64>
	{
		[SerializeField] private ulong _state;

		private SplitMix64() { }

		private static SplitMix64 CreateUninitialized()
		{
			return new SplitMix64();
		}

		/// <summary>
		/// Creates an instance of the SplitMix64 engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the SplitMix64 engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		/// <seealso cref="RandomBase.Seed()"/>
		public static SplitMix64 Create()
		{
			var instance = CreateUninitialized();
			instance.Seed();
			return instance;
		}

		/// <summary>
		/// Creates an instance of the SplitMix64 engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the SplitMix64 engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		/// <seealso cref="RandomBase.Seed(int)"/>
		public static SplitMix64 Create(int seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the SplitMix64 engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the SplitMix64 engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		/// <seealso cref="RandomBase.Seed(int[])"/>
		public static SplitMix64 Create(params int[] seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the SplitMix64 engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the SplitMix64 engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		/// <seealso cref="RandomBase.Seed(string)"/>
		public static SplitMix64 Create(string seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the SplitMix64 engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the SplitMix64 engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static SplitMix64 Create(IBitGenerator bitGenerator)
		{
			var instance = CreateUninitialized();
			instance.Seed(bitGenerator);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the SplitMix64 engine using the provided <paramref name="stateArray"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="IRandom.SaveState()"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the SplitMix64 engine.</returns>
		public static SplitMix64 CreateWithState(byte[] stateArray)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(stateArray);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the SplitMix64 engine using the provided <paramref name="state"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="state">The element of state data generated from an earlier call to <see cref="SaveState(out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the SplitMix64 engine.</returns>
		public static SplitMix64 CreateWithState(ulong state)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(state);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this SplitMix64 engine.</returns>
		public SplitMix64 Clone()
		{
			var instance = CreateUninitialized();
			instance.CopyStateFrom(this);
			return instance;
		}

		/// <summary>
		/// Copies the state of the <paramref name="source"/> SplitMix64 engine into this engine, so that this engine will independently generate the same sequence of random values as the source.
		/// </summary>
		/// <param name="source"></param>
		/// <remarks>The source engine is not altered.</remarks>
		public void CopyStateFrom(SplitMix64 source)
		{
			_state = source._state;
		}

		/// <summary>
		/// Saves the SplitMix64 engine's internal state as a byte array, which can be restored later.
		/// </summary>
		/// <returns>The internal state as a byte array.</returns>
		public override byte[] SaveState()
		{
			var stateArray = new byte[sizeof(ulong)];
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var writer = new System.IO.BinaryWriter(stream))
				{
					SaveState(writer, _state);
				}
			}
			return stateArray;
		}

		/// <summary>
		/// Saves the SplitMix64 engine's internal state as an unsigned 64-bit integer, which can be restored later.
		/// </summary>
		/// <param name="state">The element of state data to be saved.</param>
		public void SaveState(out ulong state)
		{
			state = _state;
		}

		/// <summary>
		/// Restores the SplitMix64 engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public override void RestoreState(byte[] stateArray)
		{
			ulong state;
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var reader = new System.IO.BinaryReader(stream))
				{
					RestoreState(reader, out state);
				}
			}
			RestoreState(state);
		}

		/// <summary>
		/// Restores the SplitMix64 engine's internal state from an unsigned 64-bit integer which had been previously saved.
		/// </summary>
		/// <param name="state">The element of state data generated from an earlier call to <see cref="SaveState(out ulong)"/> on a binary-compatible type of random engine.</param>
		public void RestoreState(ulong state)
		{
			_state = state;
		}

#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
		private static ulong Hash(byte[] seed)
		{
			ulong h = 14695981039346656037UL;
			for (int i = 0; i < seed.Length; ++i)
			{
				h = (h ^ seed[i]) * 1099511628211UL;
			}
			return h;
		}

		public override void Seed()
		{
			_state = Hash(System.BitConverter.GetBytes(System.Environment.TickCount));
		}

		public override void Seed(int seed)
		{
			_state = Hash(System.BitConverter.GetBytes(seed));
		}

		public override void Seed(params int[] seed)
		{
			var byteData = new byte[seed.Length * 4];
			System.Buffer.BlockCopy(seed, 0, byteData, 0, byteData.Length);
			_state = Hash(byteData);
		}

		public override void Seed(string seed)
		{
			_state = Hash(new System.Text.UTF8Encoding().GetBytes(seed));
		}

		public override void Seed(IBitGenerator bitGenerator)
		{
			_state = bitGenerator.Next64();
		}

		public override void MergeSeed()
		{
			_state ^= Hash(System.BitConverter.GetBytes(System.Environment.TickCount));
		}

		public override void MergeSeed(int seed)
		{
			_state ^= Hash(System.BitConverter.GetBytes(seed));
		}

		public override void MergeSeed(params int[] seed)
		{
			var byteData = new byte[seed.Length * 4];
			System.Buffer.BlockCopy(seed, 0, byteData, 0, byteData.Length);
			_state ^= Hash(byteData);
		}

		public override void MergeSeed(string seed)
		{
			_state ^= Hash(new System.Text.UTF8Encoding().GetBytes(seed));
		}

		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			_state ^= bitGenerator.Next64();
		}
#else
		/// <summary>
		/// Reseed the SplitMix64 engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void Seed(IBitGenerator bitGenerator)
		{
			_state = bitGenerator.Next64();
		}

		/// <summary>
		/// Reseed the SplitMix64 engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			_state ^= bitGenerator.Next64();
		}
#endif

		/// <summary>
		/// The number of bits that the SplitMix64 engine naturally produces in a single step.
		/// </summary>
		public override int stepBitCount { get { return 64; } }

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		/// <remarks>64 bits of data are generated and thrown away by this call.</remarks>
		public override void Step()
		{
			_state += 0x9E3779B97F4A7C15UL;
		}

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		/// <remarks>64 bits of data are generated by this call; 32 bits are returned, while the other 32 bits are thrown away.
		/// Thus, a single call to this method leaves the random engine in the same state as a single call to <see cref="Next64()"/>.</remarks>
		public override uint Next32()
		{
			_state += 0x9E3779B97F4A7C15UL;
			var z = _state;
			z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
			z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
			return (uint)(z ^ (z >> 31));
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		public override ulong Next64()
		{
			_state += 0x9E3779B97F4A7C15UL;
			var z = _state;
			z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
			z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
			return z ^ (z >> 31);
		}

		/// <summary>
		/// Adapts the random engine to the interface provided by <see cref="System.Random"/>, for use in interfaces that require this common .NET type.
		/// </summary>
		/// <returns>An adapting wrapper around this random engine which is derived from <see cref="System.Random"/>.</returns>
		/// <seealso cref="System.Random"/>
		/// <seealso cref="SystemRandomWrapper"/>
		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper(this);
		}

		/// <summary>
		/// Checks to see if the state of two random engines are equal.
		/// </summary>
		/// <param name="lhs">The first random engine whose state is to be compared.</param>
		/// <param name="rhs">The second random engine whose state is to be compared.</param>
		/// <returns>Returns true if neither random engine is null and both have the same state, or if both are null, false otherwise.</returns>
		public static bool operator ==(SplitMix64 lhs, SplitMix64 rhs)
		{
			return lhs != null && lhs.Equals(rhs) || lhs == null && rhs == null;
		}

		/// <summary>
		/// Checks to see if the state of two random engines are not equal.
		/// </summary>
		/// <param name="lhs">The first random engine whose state is to be compared.</param>
		/// <param name="rhs">The second random engine whose state is to be compared.</param>
		/// <returns>Returns false if neither random engine is null and both have the same state, or if both are null, true otherwise.</returns>
		public static bool operator !=(SplitMix64 lhs, SplitMix64 rhs)
		{
			return lhs != null && !lhs.Equals(rhs) || lhs == null && rhs != null;
		}

		/// <summary>
		/// Checks if the specified random engine has the same state as this one.
		/// </summary>
		/// <param name="other">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and both random engines have the same state, false otherwise.</returns>
		public bool Equals(SplitMix64 other)
		{
			return other != null && _state == other._state;
		}

		/// <summary>
		/// Checks if the specified random engine is the same type and has the same state as this one.
		/// </summary>
		/// <param name="obj">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and is the same type and has the same state as this one, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as SplitMix64);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return _state.GetHashCode();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return string.Format("SplitMix64 {{ 0x{0:X16} }}", _state);
		}
	}
}
