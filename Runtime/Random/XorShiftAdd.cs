/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

/******************************************************************************\
* The following license applies to the contents
* of the Next32, Next64, and SkipAhead functions.
*
* XORSHIFT-ADD(xsadd) pseudo random number generator
*
* Copyright (c) 2014
* Mutsuo Saito, Makoto Matsumoto, Manieth Corp.,
* and Hiroshima University.
* All rights reserved.
*
* Permission is hereby granted, free of charge, to any person
* obtaining a copy of this software and associated documentation
* files (the "Software"), to deal in the Software without
* restriction, including without limitation the rights to use, copy,
* modify, merge, publish, distribute, sublicense, and/or sell copies
* of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be
* included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
* MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
* NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
* BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
* ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
\******************************************************************************/

using UnityEngine;

namespace MakeIt.Random
{
	/// <summary>
	/// An implementation of the <see cref="IRandom"/> interface using the XorShift-Add (also know as XSadd) generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG is based on Marsaglia's XorShift class of generators, and modified by Mutsuo Saito and
	/// Makoto Matsumoto as described <a href="http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/XSADD/">here</a>.
	/// </para>
	/// <para>This random engine maintains 128 bits of state.  It natively generates 32 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IBitGenerator"/>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	[System.Serializable]
	public sealed class XorShiftAdd : RandomBase, System.IEquatable<XorShiftAdd>
	{
		[SerializeField] private uint _state0 = 0U;
		[SerializeField] private uint _state1 = 0U;
		[SerializeField] private uint _state2 = 0U;
		[SerializeField] private uint _state3 = 1U; //to avoid ever having an invalid all 0-bit state

		private XorShiftAdd() { }

		private static XorShiftAdd CreateUninitialized()
		{
			return new XorShiftAdd();
		}

		/// <summary>
		/// Creates an instance of the XorShift-Add engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the XorShift-Add engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		/// <seealso cref="RandomBase.Seed()"/>
		public static XorShiftAdd Create()
		{
			var instance = CreateUninitialized();
			instance.Seed();
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift-Add engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift-Add engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		/// <seealso cref="RandomBase.Seed(int)"/>
		public static XorShiftAdd Create(int seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift-Add engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift-Add engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		/// <seealso cref="RandomBase.Seed(int[])"/>
		public static XorShiftAdd Create(params int[] seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift-Add engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift-Add engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		/// <seealso cref="RandomBase.Seed(string)"/>
		public static XorShiftAdd Create(string seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift-Add engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift-Add engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static XorShiftAdd Create(IBitGenerator bitGenerator)
		{
			var instance = CreateUninitialized();
			instance.Seed(bitGenerator);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift-Add engine using the provided <paramref name="stateArray"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="IRandom.SaveState()"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XorShift-Add engine.</returns>
		public static XorShiftAdd CreateWithState(byte[] stateArray)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(stateArray);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift-Add engine using the provided <paramref name="state0"/> through <paramref name="state3"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="state0">The first 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state2">The third 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state3">The fourth 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XorShift-Add engine.</returns>
		public static XorShiftAdd CreateWithState(uint state0, uint state1, uint state2, uint state3)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(state0, state1, state2, state3);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this XorShift-Add engine.</returns>
		public XorShiftAdd Clone()
		{
			var instance = CreateUninitialized();
			instance.CopyStateFrom(this);
			return instance;
		}

		/// <summary>
		/// Copies the state of the <paramref name="source"/> XorShift-Add engine into this engine, so that this engine will independently generate the same sequence of random values as the source.
		/// </summary>
		/// <param name="source"></param>
		/// <remarks>The source engine is not altered.</remarks>
		public void CopyStateFrom(XorShiftAdd source)
		{
			_state0 = source._state0;
			_state1 = source._state1;
			_state2 = source._state2;
			_state3 = source._state3;
		}

		/// <summary>
		/// Saves the XorShift-Add engine's internal state as a byte array, which can be restored later.
		/// </summary>
		/// <returns>The internal state as a byte array.</returns>
		public override byte[] SaveState()
		{
			var stateArray = new byte[sizeof(uint) * 4];
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var writer = new System.IO.BinaryWriter(stream))
				{
					SaveState(writer, _state0);
					SaveState(writer, _state1);
					SaveState(writer, _state2);
					SaveState(writer, _state3);
				}
			}
			return stateArray;
		}

		/// <summary>
		/// Saves the XorShift-Add engine's internal state as a pair of unsigned 64-bit integers, which can be restored later.
		/// </summary>
		/// <param name="state0">The first 32 bits of state data to be saved.</param>
		/// <param name="state1">The second 32 bits of state data to be saved.</param>
		/// <param name="state2">The third 32 bits of state data to be saved.</param>
		/// <param name="state3">The fourth 32 bits of state data to be saved.</param>
		public void SaveState(out uint state0, out uint state1, out uint state2, out uint state3)
		{
			state0 = _state0;
			state1 = _state1;
			state2 = _state2;
			state3 = _state3;
		}

		/// <summary>
		/// Restores the XorShift-Add engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public override void RestoreState(byte[] stateArray)
		{
			uint state0;
			uint state1;
			uint state2;
			uint state3;
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var reader = new System.IO.BinaryReader(stream))
				{
					RestoreState(reader, out state0);
					RestoreState(reader, out state1);
					RestoreState(reader, out state2);
					RestoreState(reader, out state3);
				}
			}
			RestoreState(state0, state1, state2, state3);
		}

		/// <summary>
		/// Restores the XorShift-Add engine's internal state from a pair of unsigned 64-bit integers which had been previously saved.
		/// </summary>
		/// <param name="state0">The first 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state2">The third 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state3">The fourth 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		public void RestoreState(uint state0, uint state1, uint state2, uint state3)
		{
			if (state0 == 0 && state1 == 0 && state2 == 0 && state3 == 0)
			{
				throw new System.ArgumentException("All 0 bits is an invalid state for the XorShift-Add random number generator.");
			}
			_state0 = state0;
			_state1 = state1;
			_state2 = state2;
			_state3 = state3;
		}

		/// <summary>
		/// Reseed the XorShift-Add engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void Seed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
				uint state0 = bitGenerator.Next32();
				uint state1 = bitGenerator.Next32();
				uint state2 = bitGenerator.Next32();
				uint state3 = bitGenerator.Next32();
				if (state0 != 0 || state1 != 0 || state2 != 0 || state3 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					_state2 = state2;
					_state3 = state3;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		/// <summary>
		/// Reseed the XorShift-Add engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
				uint state0 = _state0 ^ bitGenerator.Next32();
				uint state1 = _state1 ^ bitGenerator.Next32();
				uint state2 = _state2 ^ bitGenerator.Next32();
				uint state3 = _state3 ^ bitGenerator.Next32();
				if (state0 != 0 || state1 != 0 || state2 != 0 || state3 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					_state2 = state2;
					_state3 = state3;
					return;
				}
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		/// <summary>
		/// The number of bits that the XorShift-Add engine naturally produces in a single step.
		/// </summary>
		public override int stepBitCount { get { return 32; } }

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		/// <remarks>32 bits of data are generated and thrown away by this call.</remarks>
		public override void Step()
		{
			uint t = _state0;
			t ^= t << 15;
			t ^= t >> 18;
			t ^= _state3 << 11;
			_state0 = _state1;
			_state1 = _state2;
			_state2 = _state3;
			_state3 = t;
		}

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		public override uint Next32()
		{
			uint next = _state2 + _state3;
			uint t = _state0;
			t ^= t << 15;
			t ^= t >> 18;
			t ^= _state3 << 11;
			_state0 = _state1;
			_state1 = _state2;
			_state2 = _state3;
			_state3 = t;
			return next;
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		/// <remarks>This random engine natively only generates 32 bits of data per step, but this function requires 64 bits.
		/// Thus, a single call to this method leaves the random engine in the same state as two calls to <see cref="Next32()"/>.</remarks>
		public override ulong Next64()
		{
			uint t = _state0;
			t ^= t << 15;
			t ^= t >> 18;
			t ^= _state3 << 11;
			ulong next = (_state2 + _state3) | ((ulong)(_state3 + t) << 32);
			uint s = _state1;
			s ^= s << 15;
			s ^= s >> 18;
			s ^= t << 11;
			_state0 = _state2;
			_state1 = _state3;
			_state2 = t;
			_state3 = s;
			return next;
		}

		/// <summary>
		/// Get the next 64 bits of generated data as two 32-bit values.
		/// </summary>
		/// <param name="lower">The lower 32 bits of generated data.</param>
		/// <param name="upper">The upper 32 bits of generated data.</param>
		/// <remarks>This random engine natively only generates 32 bits of data per step, but this function requires 64 bits.
		/// Thus, a single call to this method leaves the random engine in the same state as two calls to <see cref="Next32()"/>.</remarks>
		public override void Next64(out uint lower, out uint upper)
		{
			lower = Next32();
			upper = Next32();
		}

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipAhead()"/> skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipAhead()"/> will skip forward by approximately <code>2^65</code> (exactly <code>3^41</code>) steps each time it is called.</para>
		/// </remarks>
		public override int skipAheadMagnitude { get { return 65; } }

		private void SkipAhead(uint b, ref uint x0, ref uint x1, ref uint x2, ref uint x3)
		{
			for (int i = 0; i < 32; ++i)
			{
				if ((b & 1UL) != 0UL)
				{
					x0 ^= _state0;
					x1 ^= _state1;
					x2 ^= _state2;
					x3 ^= _state3;
				}
				b >>= 1;
				Step();
			}
		}

		/// <summary>
		/// Quickly advances the state forward by 3^41 steps.
		/// </summary>
		/// <seealso cref="skipAheadMagnitude"/>
		public override void SkipAhead()
		{
			uint x0 = 0;
			uint x1 = 0;
			uint y0 = 0;
			uint y1 = 0;

			SkipAhead(0x2340BA2AU, ref x0, ref x1, ref y0, ref y1);
			SkipAhead(0xD36FBF89U, ref x0, ref x1, ref y0, ref y1);
			SkipAhead(0xDD20910CU, ref x0, ref x1, ref y0, ref y1);
			SkipAhead(0x7FCC01E3U, ref x0, ref x1, ref y0, ref y1);

			_state0 = x0;
			_state1 = x1;
			_state2 = y0;
			_state3 = y1;
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
		public static bool operator ==(XorShiftAdd lhs, XorShiftAdd rhs)
		{
			return lhs != null && lhs.Equals(rhs) || lhs == null && rhs == null;
		}

		/// <summary>
		/// Checks to see if the state of two random engines are not equal.
		/// </summary>
		/// <param name="lhs">The first random engine whose state is to be compared.</param>
		/// <param name="rhs">The second random engine whose state is to be compared.</param>
		/// <returns>Returns false if neither random engine is null and both have the same state, or if both are null, true otherwise.</returns>
		public static bool operator !=(XorShiftAdd lhs, XorShiftAdd rhs)
		{
			return lhs != null && !lhs.Equals(rhs) || lhs == null && rhs != null;
		}

		/// <summary>
		/// Checks if the specified random engine has the same state as this one.
		/// </summary>
		/// <param name="other">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and both random engines have the same state, false otherwise.</returns>
		public bool Equals(XorShiftAdd other)
		{
			return other != null && _state0 == other._state0 && _state1 == other._state1 && _state2 == other._state2 && _state3 == other._state3;
		}

		/// <summary>
		/// Checks if the specified random engine is the same type and has the same state as this one.
		/// </summary>
		/// <param name="obj">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and is the same type and has the same state as this one, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as XorShiftAdd);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return _state0.GetHashCode() ^ _state1.GetHashCode();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return string.Format("XorShiftAdd {{ 0x{0:X8}, 0x{1:X8}, 0x{2:X8}, 0x{3:X8} }}", _state0, _state1, _state2, _state3);
		}
	}
}
