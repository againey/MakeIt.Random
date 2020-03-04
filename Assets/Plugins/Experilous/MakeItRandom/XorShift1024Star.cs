/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// An implementation of the <see cref="IRandom"/> interface using the 1024-bit XorShift* generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG is based on Sebastiano Vigna's xorshift1024* generator, adapted from a
	/// <a href="http://xoroshiro.di.unimi.it/xorshift1024star.c">C code reference implementation</a>.</para>
	/// <para>As its name implies, it maintains 1024 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IBitGenerator"/>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	[System.Serializable]
	public sealed class XorShift1024Star : RandomBase, System.IEquatable<XorShift1024Star>
	{
		[SerializeField] private ulong[] _state = new ulong[] { 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 0UL, 1UL, };
		[SerializeField] private int _offset;

		private XorShift1024Star() { }

		private static XorShift1024Star CreateUninitialized()
		{
			return new XorShift1024Star();
		}

		/// <summary>
		/// Creates an instance of the XorShift1024* engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the XorShift1024* engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		/// <seealso cref="RandomBase.Seed()"/>
		public static XorShift1024Star Create()
		{
			var instance = CreateUninitialized();
			instance.Seed();
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift1024* engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift1024* engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		/// <seealso cref="RandomBase.Seed(int)"/>
		public static XorShift1024Star Create(int seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift1024* engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift1024* engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		/// <seealso cref="RandomBase.Seed(int[])"/>
		public static XorShift1024Star Create(params int[] seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift1024* engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift1024* engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		/// <seealso cref="RandomBase.Seed(string)"/>
		public static XorShift1024Star Create(string seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift1024* engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift1024* engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static XorShift1024Star Create(IBitGenerator bitGenerator)
		{
			var instance = CreateUninitialized();
			instance.Seed(bitGenerator);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift1024* engine using the provided <paramref name="stateArray"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="IRandom.SaveState()"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XorShift1024* engine.</returns>
		public static XorShift1024Star CreateWithState(byte[] stateArray)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(stateArray);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift1024* engine using the provided <paramref name="state"/> and <paramref name="offset"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="state">The primary elements of state data generated from an earlier call to <see cref="SaveState(out ulong[], out int)"/> on a binary-compatible type of random engine.</param>
		/// <param name="offset">The offset index state generated from an earlier call to <see cref="SaveState(out ulong[], out int)"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XorShift1024* engine.</returns>
		public static XorShift1024Star CreateWithState(ulong[] state, int offset)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(state, offset);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this XorShift1024* engine.</returns>
		public XorShift1024Star Clone()
		{
			var instance = CreateUninitialized();
			instance.CopyStateFrom(this);
			return instance;
		}

		/// <summary>
		/// Copies the state of the <paramref name="source"/> XorShift1024* engine into this engine, so that this engine will independently generate the same sequence of random values as the source.
		/// </summary>
		/// <param name="source"></param>
		/// <remarks>The source engine is not altered.</remarks>
		public void CopyStateFrom(XorShift1024Star source)
		{
			for (int i = 0; i < 16; ++i)
			{
				_state[i] = source._state[i];
			}
			_offset = source._offset;
		}

		/// <summary>
		/// Saves the XorShift1024* engine's internal state as a byte array, which can be restored later.
		/// </summary>
		/// <returns>The internal state as a byte array.</returns>
		public override byte[] SaveState()
		{
			var stateArray = new byte[sizeof(ulong) * _state.Length + sizeof(byte)];
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var writer = new System.IO.BinaryWriter(stream))
				{
					for (int i = 0; i < _state.Length; ++i)
					{
						SaveState(writer, _state[i]);
					}
					SaveState(writer, (byte)_offset);
				}
			}
			return stateArray;
		}

		/// <summary>
		/// Saves the XorShift1024* engine's internal state as a pair of unsigned 64-bit integers, which can be restored later.
		/// </summary>
		/// <param name="state">The primary elements of state data to be saved.</param>
		/// <param name="offset">The offset index state to be saved.</param>
		public void SaveState(out ulong[] state, out int offset)
		{
			state = _state.Clone() as ulong[];
			offset = _offset;
		}

		/// <summary>
		/// Restores the XorShift1024* engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public override void RestoreState(byte[] stateArray)
		{
			ulong[] state = new ulong[16];
			byte offset;
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var reader = new System.IO.BinaryReader(stream))
				{
					for (int i = 0; i < state.Length; ++i)
					{
						RestoreState(reader, out state[i]);
					}
					RestoreState(reader, out offset);
				}
			}
			RestoreState(state, offset);
		}

		/// <summary>
		/// Restores the XorShift1024* engine's internal state from a pair of unsigned 64-bit integers which had been previously saved.
		/// </summary>
		/// <param name="state">The primary elements of state data generated from an earlier call to <see cref="SaveState(out ulong[], out int)"/> on a binary-compatible type of random engine.</param>
		/// <param name="offset">The offset index state generated from an earlier call to <see cref="SaveState(out ulong[], out int)"/> on a binary-compatible type of random engine.</param>
		public void RestoreState(ulong[] state, int offset)
		{
			if (state == null) throw new System.ArgumentNullException("state");
			if (state.Length != 16) throw new System.ArgumentException("The provided state array must have a length of exactly 16.", "state");
			if (offset < 0 || offset >= 16) throw new System.ArgumentOutOfRangeException("offset", offset, "The provided offset must be within the range [0, 15].");
			for (int i = 0; i < 16; ++i)
			{
				if (state[i] != 0UL)
				{
					for (i = 0; i < 16; ++i)
					{
						_state[i] = state[i];
					}
					_offset = offset;
					return;
				}
			}
			throw new System.ArgumentException("All 0 bits is an invalid state for the XorShift1024* random number generator.");
		}

		/// <summary>
		/// Reseed the XorShift1024* engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void Seed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
				ulong[] state = new ulong[16];
				bool allZeroes = true;
				for (int i = 0; i < 16; ++i)
				{
					state[i] = bitGenerator.Next64();
					allZeroes = allZeroes && state[i] == 0;
				}
				if (!allZeroes)
				{
					_state = state;
					_offset = 0;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		/// <summary>
		/// Reseed the XorShift1024* engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
				ulong[] state = new ulong[16];
				bool allZeroes = true;
				for (int i = 0; i < 16; ++i)
				{
					state[i] = _state[i] ^ bitGenerator.Next64();
					allZeroes = allZeroes && state[i] == 0;
				}
				if (!allZeroes)
				{
					_state = state;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		/// <summary>
		/// The number of bits that the XorShift1024* engine naturally produces in a single step.
		/// </summary>
		public override int stepBitCount { get { return 64; } }

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		/// <remarks>64 bits of data are generated and thrown away by this call.</remarks>
		public override void Step()
		{
			var x = _state[_offset];
			_offset = (_offset + 1) & 0xF;
			var y = _state[_offset];
			y ^= y << 31;
			_state[_offset] = y ^ x ^ (y >> 11) ^ (x >> 30);
		}

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		/// <remarks>64 bits of data are generated by this call; 32 bits are returned, while the other 32 bits are thrown away.
		/// Thus, a single call to this method leaves the random engine in the same state as a single call to <see cref="Next64()"/>.</remarks>
		public override uint Next32()
		{
			var x = _state[_offset];
			_offset = (_offset + 1) & 0xF;
			var y = _state[_offset];
			y ^= y << 31;
			_state[_offset] = y ^ x ^ (y >> 11) ^ (x >> 30);
			return (uint)(_state[_offset] * 1181783497276652981UL);
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		public override ulong Next64()
		{
			var x = _state[_offset];
			_offset = (_offset + 1) & 0xF;
			var y = _state[_offset];
			y ^= y << 31;
			_state[_offset] = y ^ x ^ (y >> 11) ^ (x >> 30);
			return _state[_offset] * 1181783497276652981UL;
		}

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipAhead"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipAhead()"/> will skip forward by exactly <code>2^512</code> steps each time it is called.</para>
		/// </remarks>
		public override int skipAheadMagnitude { get { return 512; } }

		private static readonly ulong[] _jumpTable = new ulong[]
		{
			0x84242F96ECA9C41DUL, 0xA3C65B8776F96855UL, 0x5B34A39F070B5837UL, 0x4489AFFCE4F31A1EUL,
			0x2FFEEB0A48316F40UL, 0xDC2D9891FE68C022UL, 0x3659132BB12FEA70UL, 0xAAC17D8EFA43CAB8UL,
			0xC4CB815590989B13UL, 0x5EE975283D71C93BUL, 0x691548C86C1BD540UL, 0x7910C41D10A1E6A5UL,
			0x0B5FC64563B3E2A8UL, 0x047F7684E9FC949DUL, 0xB99181F2D8F685CAUL, 0x284600E3F30E38C3UL,
		};

		/// <summary>
		/// Quickly advances the state forward by 2^512 steps.
		/// </summary>
		/// <seealso cref="skipAheadMagnitude"/>
		public override void SkipAhead()
		{
			ulong[] t = new ulong[16];
			for (int i = 0; i < 16; ++i)
			{
				for (int b = 0; b < 64; ++b)
				{
					if ((_jumpTable[i] & (1UL << b)) != 0UL)
					{
						for (int j = 0; j < 16; ++j)
						{
							t[j] ^= _state[(j + _offset) & 0xF];
						}
					}
					Step();
				}
			}

			for (int j = 0; j < 16; ++j)
			{
				_state[(j + _offset) & 0xF] = t[j];
			}
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
		public static bool operator ==(XorShift1024Star lhs, XorShift1024Star rhs)
		{
			return lhs != null && lhs.Equals(rhs) || lhs == null && rhs == null;
		}

		/// <summary>
		/// Checks to see if the state of two random engines are not equal.
		/// </summary>
		/// <param name="lhs">The first random engine whose state is to be compared.</param>
		/// <param name="rhs">The second random engine whose state is to be compared.</param>
		/// <returns>Returns false if neither random engine is null and both have the same state, or if both are null, true otherwise.</returns>
		public static bool operator !=(XorShift1024Star lhs, XorShift1024Star rhs)
		{
			return lhs != null && !lhs.Equals(rhs) || lhs == null && rhs != null;
		}

		/// <summary>
		/// Checks if the specified random engine has the same state as this one.
		/// </summary>
		/// <param name="other">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and both random engines have the same state, false otherwise.</returns>
		public bool Equals(XorShift1024Star other)
		{
			if (other == null) return false;
			for (int i = 0; i < 16; ++i)
			{
				if (_state[i] != other._state[i]) return false;
			}
			return _offset == other._offset;
		}

		/// <summary>
		/// Checks if the specified random engine is the same type and has the same state as this one.
		/// </summary>
		/// <param name="obj">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and is the same type and has the same state as this one, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as XorShift1024Star);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = _offset.GetHashCode();
			for (int i = 0; i < 16; ++i)
			{
				hashCode ^= _state[i].GetHashCode();
			}
			return hashCode;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return string.Format("XorShift1024Star {{ 0x{0:X16}, ..., {1} }}", _state[0], _offset);
		}
	}
}
