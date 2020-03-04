/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// An implementation of the <see cref="IRandom"/> interface using the 128-bit XoroShiro+ generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG is based on David Blackman's and Sebastiano Vigna's xoroshiro128+ generator, adapted from
	/// a <a href="http://xorshift.di.unimi.it/xoroshiro128plus.c">C code reference implementation</a>.</para>
	/// <para>As its name implies, it maintains 128 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	[System.Serializable]
	public sealed class XoroShiro128Plus : RandomBase, System.IEquatable<XoroShiro128Plus>
	{
		[SerializeField] private ulong _state0 = 0UL;
		[SerializeField] private ulong _state1 = 1UL; //to avoid ever having an invalid all 0-bit state

		private XoroShiro128Plus() { }

		private static XoroShiro128Plus CreateUninitialized()
		{
			return new XoroShiro128Plus();
		}

		/// <summary>
		/// Creates an instance of the XoroShiro128+ engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the XoroShiro128+ engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		/// <seealso cref="RandomBase.Seed()"/>
		public static XoroShiro128Plus Create()
		{
			var instance = CreateUninitialized();
			instance.Seed();
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XoroShiro128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XoroShiro128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		/// <seealso cref="RandomBase.Seed(int)"/>
		public static XoroShiro128Plus Create(int seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XoroShiro128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XoroShiro128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		/// <seealso cref="RandomBase.Seed(int[])"/>
		public static XoroShiro128Plus Create(params int[] seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XoroShiro128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XoroShiro128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		/// <seealso cref="RandomBase.Seed(string)"/>
		public static XoroShiro128Plus Create(string seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XoroShiro128+ engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XoroShiro128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static XoroShiro128Plus Create(IBitGenerator bitGenerator)
		{
			var instance = CreateUninitialized();
			instance.Seed(bitGenerator);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XoroShiro128+ engine using the provided <paramref name="stateArray"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="IRandom.SaveState()"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XoroShiro128+ engine.</returns>
		public static XoroShiro128Plus CreateWithState(byte[] stateArray)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(stateArray);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XoroShiro128+ engine using the provided <paramref name="state0"/> and <paramref name="state1"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="state0">The first element of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second element of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XoroShiro128+ engine.</returns>
		public static XoroShiro128Plus CreateWithState(ulong state0, ulong state1)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(state0, state1);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this XoroShiro128+ engine.</returns>
		public XoroShiro128Plus Clone()
		{
			var instance = CreateUninitialized();
			instance.CopyStateFrom(this);
			return instance;
		}

		/// <summary>
		/// Copies the state of the <paramref name="source"/> XoroShiro128+ engine into this engine, so that this engine will independently generate the same sequence of random values as the source.
		/// </summary>
		/// <param name="source"></param>
		/// <remarks>The source engine is not altered.</remarks>
		public void CopyStateFrom(XoroShiro128Plus source)
		{
			_state0 = source._state0;
			_state1 = source._state1;
		}

		/// <summary>
		/// Saves the XoroShiro128+ engine's internal state as a byte array, which can be restored later.
		/// </summary>
		/// <returns>The internal state as a byte array.</returns>
		public override byte[] SaveState()
		{
			var stateArray = new byte[sizeof(ulong) * 2];
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var writer = new System.IO.BinaryWriter(stream))
				{
					SaveState(writer, _state0);
					SaveState(writer, _state1);
				}
			}
			return stateArray;
		}

		/// <summary>
		/// Saves the XoroShiro128+ engine's internal state as a pair of unsigned 64-bit integers, which can be restored later.
		/// </summary>
		/// <param name="state0">The first element of state data to be saved.</param>
		/// <param name="state1">The second element of state data to be saved.</param>
		public void SaveState(out ulong state0, out ulong state1)
		{
			state0 = _state0;
			state1 = _state1;
		}

		/// <summary>
		/// Restores the XoroShiro128+ engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public override void RestoreState(byte[] stateArray)
		{
			ulong state0;
			ulong state1;
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var reader = new System.IO.BinaryReader(stream))
				{
					RestoreState(reader, out state0);
					RestoreState(reader, out state1);
				}
			}
			RestoreState(state0, state1);
		}

		/// <summary>
		/// Restores the XoroShiro128+ engine's internal state from a pair of unsigned 64-bit integers which had been previously saved.
		/// </summary>
		/// <param name="state0">The first element of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second element of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		public void RestoreState(ulong state0, ulong state1)
		{
			if (state0 == 0 && state1 == 0)
			{
				throw new System.ArgumentException("All 0 bits is an invalid state for the XoroShiro128+ random number generator.");
			}
			_state0 = state0;
			_state1 = state1;
		}

		/// <summary>
		/// Reseed the XoroShiro128+ engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void Seed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
				ulong state0 = bitGenerator.Next64();
				ulong state1 = bitGenerator.Next64();
				if (state0 != 0 && state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		/// <summary>
		/// Reseed the XoroShiro128+ engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
				ulong state0 = _state0 ^ bitGenerator.Next64();
				ulong state1 = _state1 ^ bitGenerator.Next64();
				if (state0 != 0 && state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		/// <summary>
		/// The number of bits that the XoroShiro128+ engine naturally produces in a single step.
		/// </summary>
		public override int stepBitCount { get { return 64; } }

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		/// <remarks>64 bits of data are generated and thrown away by this call.</remarks>
		public override void Step()
		{
			var x = _state0;
			var y = _state1;
			y ^= x;
			_state0 = ((x << 55) | (x >> 9)) ^ y ^ (y << 14);
			_state1 = (y << 36) | (y >> 28);
		}

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		/// <remarks>64 bits of data are generated by this call; 32 bits are returned, while the other 32 bits are thrown away.
		/// Thus, a single call to this method leaves the random engine in the same state as a single call to <see cref="Next64()"/>.</remarks>
		public override uint Next32()
		{
			var x = _state0;
			var y = _state1;
			var next = (uint)(x + y);
			y ^= x;
			_state0 = ((x << 55) | (x >> 9)) ^ y ^ (y << 14);
			_state1 = (y << 36) | (y >> 28);
			return next;
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		public override ulong Next64()
		{
			var x = _state0;
			var y = _state1;
			var next = x + y;
			y ^= x;
			_state0 = ((x << 55) | (x >> 9)) ^ y ^ (y << 14);
			_state1 = (y << 36) | (y >> 28);
			return next;
		}

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipAhead"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipAhead()"/> will skip forward by exactly <code>2^64</code> steps each time it is called.</para>
		/// </remarks>
		public override int skipAheadMagnitude { get { return 64; } }

		/// <summary>
		/// Quickly advances the state forward by 2^64 steps.
		/// </summary>
		/// <seealso cref="skipAheadMagnitude"/>
		public override void SkipAhead()
		{
			ulong x = 0;
			ulong y = 0;

			ulong b = 0xBEAC0467EBA5FACBUL;
			for (int i = 0; i < 64; ++i)
			{
				if ((b & 1UL) != 0UL)
				{
					x ^= _state0;
					y ^= _state1;
				}
				b >>= 1;
				Step();
			}

			b = 0xD86B048B86AA9922UL;
			for (int i = 0; i < 64; ++i)
			{
				if ((b & 1UL) != 0UL)
				{
					x ^= _state0;
					y ^= _state1;
				}
				b >>= 1;
				Step();
			}

			_state0 = x;
			_state1 = y;
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
		/// Checks if the specified random engine has the same state as this one.
		/// </summary>
		/// <param name="other">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and both random engines have the same state, false otherwise.</returns>
		public bool Equals(XoroShiro128Plus other)
		{
			return other != null && _state0 == other._state0 && _state1 == other._state1;
		}

		/// <summary>
		/// Checks if the specified random engine is the same type and has the same state as this one.
		/// </summary>
		/// <param name="obj">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and is the same type and has the same state as this one, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as XoroShiro128Plus);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return _state0.GetHashCode() ^ _state1.GetHashCode();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return string.Format("XoroShiro128Plus {{ 0x{0:X16}, 0x{1:X16} }}", _state0, _state1);
		}
	}
}
