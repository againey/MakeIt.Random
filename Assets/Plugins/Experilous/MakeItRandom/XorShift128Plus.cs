/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// An implementation of the <see cref="IRandom"/> interface using the 128-bit XorShift+ generator.
	/// </summary>
	/// <remarks>
	/// <para>This PRNG is based on Marsaglia's XorShift class of generators, and modified by Sebastiano Vigna
	/// in his paper <a href="http://vigna.di.unimi.it/ftp/papers/xorshiftplus.pdf">Further scramblings of
	/// Marsaglia's xorshift generators</a>.</para>
	/// <para>As its name implies, it maintains 128 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IBitGenerator"/>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	[System.Serializable]
	public sealed class XorShift128Plus : RandomBase
	{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
		[SerializeField] private uint _state0 = 0U;
		[SerializeField] private uint _state1 = 0U;
		[SerializeField] private uint _state2 = 0U;
		[SerializeField] private uint _state3 = 1U; //to avoid ever having an invalid all 0-bit state
#else
		[SerializeField] private ulong _state0 = 0UL;
		[SerializeField] private ulong _state1 = 1UL; //to avoid ever having an invalid all 0-bit state
#endif

		private XorShift128Plus() { }

		private static XorShift128Plus CreateUninitialized()
		{
			return new XorShift128Plus();
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		/// <seealso cref="RandomBase.Seed()"/>
		public static XorShift128Plus Create()
		{
			var instance = CreateUninitialized();
			instance.Seed();
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		/// <seealso cref="RandomBase.Seed(int)"/>
		public static XorShift128Plus Create(int seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		/// <seealso cref="RandomBase.Seed(int[])"/>
		public static XorShift128Plus Create(params int[] seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		/// <seealso cref="RandomBase.Seed(string)"/>
		public static XorShift128Plus Create(string seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static XorShift128Plus Create(IBitGenerator bitGenerator)
		{
			var instance = CreateUninitialized();
			instance.Seed(bitGenerator);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="stateArray"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="IRandom.SaveState()"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		public static XorShift128Plus CreateWithState(byte[] stateArray)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(stateArray);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="state0"/> through <paramref name="state3"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="state0">The first 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state2">The third 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state3">The fourth 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		public static XorShift128Plus CreateWithState(uint state0, uint state1, uint state2, uint state3)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(state0, state1, state2, state3);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="state0"/> and <paramref name="state1"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="state0">The first 64 bits of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second 64 bits of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		public static XorShift128Plus CreateWithState(ulong state0, ulong state1)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(state0, state1);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this XorShift128+ engine.</returns>
		public XorShift128Plus Clone()
		{
			var instance = CreateUninitialized();
			instance.CopyStateFrom(this);
			return instance;
		}

		/// <summary>
		/// Copies the state of the <paramref name="source"/> XorShift128+ engine into this engine, so that this engine will independently generate the same sequence of random values as the source.
		/// </summary>
		/// <param name="source"></param>
		/// <remarks>The source engine is not altered.</remarks>
		public void CopyStateFrom(XorShift128Plus source)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			_state0 = source._state0;
			_state1 = source._state1;
			_state2 = source._state2;
			_state3 = source._state3;
#else
			_state0 = source._state0;
			_state1 = source._state1;
#endif
		}

		/// <summary>
		/// Saves the XorShift128+ engine's internal state as a byte array, which can be restored later.
		/// </summary>
		/// <returns>The internal state as a byte array.</returns>
		public override byte[] SaveState()
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
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
#else
			var stateArray = new byte[sizeof(ulong) * 2];
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				using (var writer = new System.IO.BinaryWriter(stream))
				{
					SaveState(writer, _state0);
					SaveState(writer, _state1);
				}
			}
#endif
			return stateArray;
		}

		/// <summary>
		/// Saves the XorShift128+ engine's internal state as a pair of unsigned 64-bit integers, which can be restored later.
		/// </summary>
		/// <param name="state0">The first 32 bits of state data to be saved.</param>
		/// <param name="state1">The second 32 bits of state data to be saved.</param>
		/// <param name="state2">The third 32 bits of state data to be saved.</param>
		/// <param name="state3">The fourth 32 bits of state data to be saved.</param>
		public void SaveState(out uint state0, out uint state1, out uint state2, out uint state3)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			state0 = _state0;
			state1 = _state1;
			state2 = _state2;
			state3 = _state3;
#else
			state0 = (uint)(_state0 & 0xFFFFFFFFUL);
			state1 = (uint)(_state0 >> 32);
			state2 = (uint)(_state1 & 0xFFFFFFFFUL);
			state3 = (uint)(_state1 >> 32);
#endif
		}

		/// <summary>
		/// Saves the XorShift128+ engine's internal state as a pair of unsigned 64-bit integers, which can be restored later.
		/// </summary>
		/// <param name="state0">The first 64 bits of state data to be saved.</param>
		/// <param name="state1">The second 64 bits of state data to be saved.</param>
		public void SaveState(out ulong state0, out ulong state1)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			state0 = _state0 | ((ulong)_state1 << 32);
			state1 = _state2 | ((ulong)_state3 << 32);
#else
			state0 = _state0;
			state1 = _state1;
#endif
		}

		/// <summary>
		/// Restores the XorShift128+ engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public override void RestoreState(byte[] stateArray)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
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
#else
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
#endif
		}

		/// <summary>
		/// Restores the XorShift128+ engine's internal state from a pair of unsigned 64-bit integers which had been previously saved.
		/// </summary>
		/// <param name="state0">The first 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state2">The third 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state3">The fourth 32 bits of state data generated from an earlier call to <see cref="SaveState(out uint, out uint, out uint, out uint)"/> on a binary-compatible type of random engine.</param>
		public void RestoreState(uint state0, uint state1, uint state2, uint state3)
		{
			if (state0 == 0 && state1 == 0 && state2 == 0 && state3 == 0)
			{
				throw new System.ArgumentException("All 0 bits is an invalid state for the XorShift128+ random number generator.");
			}
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			_state0 = state0;
			_state1 = state1;
			_state2 = state2;
			_state3 = state3;
#else
			_state0 = state0 | ((ulong)state1 << 32);
			_state1 = state2 | ((ulong)state3 << 32);
#endif
		}

		/// <summary>
		/// Restores the XorShift128+ engine's internal state from a pair of unsigned 64-bit integers which had been previously saved.
		/// </summary>
		/// <param name="state0">The first 64 bits of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second 64 bits of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		public void RestoreState(ulong state0, ulong state1)
		{
			if (state0 == 0 && state1 == 0)
			{
				throw new System.ArgumentException("All 0 bits is an invalid state for the XorShift128+ random number generator.");
			}
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			_state0 = (uint)(state0 & 0xFFFFFFFFUL);
			_state1 = (uint)(state0 >> 32);
			_state2 = (uint)(state1 & 0xFFFFFFFFUL);
			_state3 = (uint)(state1 >> 32);
#else
			_state0 = state0;
			_state1 = state1;
#endif
		}

#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
		public override void Seed()
		{
			Seed(SplitMix64.Create());
		}

		public override void Seed(int seed)
		{
			Seed(SplitMix64.Create(seed));
		}

		public override void Seed(params int[] seed)
		{
			Seed(SplitMix64.Create(seed));
		}

		public override void Seed(string seed)
		{
			Seed(SplitMix64.Create(seed));
		}
#endif

		/// <summary>
		/// Reseed the XorShift128+ engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void Seed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
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
#else
				ulong state0 = bitGenerator.Next64();
				ulong state1 = bitGenerator.Next64();
				if (state0 != 0 || state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
#endif
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
		public override void MergeSeed()
		{
			MergeSeed(SplitMix64.Create());
		}

		public override void MergeSeed(int seed)
		{
			MergeSeed(SplitMix64.Create(seed));
		}

		public override void MergeSeed(params int[] seed)
		{
			MergeSeed(SplitMix64.Create(seed));
		}

		public override void MergeSeed(string seed)
		{
			MergeSeed(SplitMix64.Create(seed));
		}
#endif

		/// <summary>
		/// Reseed the XorShift128+ engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			int tryCount = 0;

			do
			{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
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
#else
				ulong state0 = _state0 ^ bitGenerator.Next64();
				ulong state1 = _state1 ^ bitGenerator.Next64();
				if (state0 != 0 || state1 != 0)
				{
					_state0 = state0;
					_state1 = state1;
					return;
				}
#endif
			} while (++tryCount < 4);

			throw new System.ArgumentException("The provided bit generator was unable to generate a non-zero state, which is required by this random engine.");
		}

		/// <summary>
		/// The number of bits that the XorShift128+ engine naturally produces in a single step.
		/// </summary>
		public override int stepBitCount { get { return 64; } }

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		/// <remarks>64 bits of data are generated and thrown away by this call.</remarks>
		public override void Step()
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint x0 = _state0;
			uint x1 = _state1;
			uint y0 = _state2;
			uint y1 = _state3;
			_state0 = y0;
			_state1 = y1;
			x1 ^= (x1 << 23) | (x0 >> 9);
			x0 ^= x0 << 23;
			_state2 = x0 ^ y0 ^ ((x0 >> 18) | (x1 << 14)) ^ ((y0 >> 5) | (y1 << 27));
			_state3 = x1 ^ y1 ^ (x1 >> 18) ^ (y1 >> 5);
#else
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5); //earlier versions used values 17 and 26; changed to 18 and 5 as justified at http://v8project.blogspot.com/2015/12/theres-mathrandom-and-then-theres.html?showComment=1450389868643#c2004131565745698275
#endif
#endif
		}

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		/// <remarks>64 bits of data are generated by this call; 32 bits are returned, while the other 32 bits are thrown away.
		/// Thus, a single call to this method leaves the random engine in the same state as a single call to <see cref="Next64()"/>.</remarks>
		public override uint Next32()
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
			return (uint)(_state1 + y);
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint x0 = _state0;
			uint x1 = _state1;
			uint y0 = _state2;
			uint y1 = _state3;
			uint next = x0 + y0;
			_state0 = y0;
			_state1 = y1;
			x1 ^= (x1 << 23) | (x0 >> 9);
			x0 ^= x0 << 23;
			_state2 = x0 ^ y0 ^ ((x0 >> 18) | (x1 << 14)) ^ ((y0 >> 5) | (y1 << 27));
			_state3 = x1 ^ y1 ^ (x1 >> 18) ^ (y1 >> 5);
#else
			var x = _state0;
			var y = _state1;
			var next = (uint)(x + y);
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5); //earlier versions used values 17 and 26; changed to 18 and 5 as justified at http://v8project.blogspot.com/2015/12/theres-mathrandom-and-then-theres.html?showComment=1450389868643#c2004131565745698275
#endif
			return next;
#endif
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		public override ulong Next64()
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
			return _state1 + y;
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint x0 = _state0;
			uint x1 = _state1;
			uint y0 = _state2;
			uint y1 = _state3;
			uint next0 = x0 + y0;
			uint next1 = x1 + y1 + ((next0 < x0 || next0 < y0) ? 1U : 0U);
			ulong next = next0 | ((ulong)next1 << 32);
			_state0 = y0;
			_state1 = y1;
			x1 ^= (x1 << 23) | (x0 >> 9);
			x0 ^= x0 << 23;
			_state2 = x0 ^ y0 ^ ((x0 >> 18) | (x1 << 14)) ^ ((y0 >> 5) | (y1 << 27));
			_state3 = x1 ^ y1 ^ (x1 >> 18) ^ (y1 >> 5);
#else
			var x = _state0;
			var y = _state1;
			var next = x + y;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5); //earlier versions used values 17 and 26; changed to 18 and 5 as justified at http://v8project.blogspot.com/2015/12/theres-mathrandom-and-then-theres.html?showComment=1450389868643#c2004131565745698275
#endif
			return next;
#endif
		}

		/// <summary>
		/// Get the next 64 bits of generated data as two 32-bit values.
		/// </summary>
		/// <param name="lower">The lower 32 bits of generated data.</param>
		/// <param name="upper">The upper 32 bits of generated data.</param>
		public override void Next64(out uint lower, out uint upper)
		{
#if MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
			var next = _state1 + y;
			lower = (uint)next;
			upper = (uint)(next >> 32);
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint x0 = _state0;
			uint x1 = _state1;
			uint y0 = _state2;
			uint y1 = _state3;
			lower = x0 + y0;
			upper = x1 + y1 + ((lower < x0 || lower < y0) ? 1U : 0U);
			_state0 = y0;
			_state1 = y1;
			x1 ^= (x1 << 23) | (x0 >> 9);
			x0 ^= x0 << 23;
			_state2 = x0 ^ y0 ^ ((x0 >> 18) | (x1 << 14)) ^ ((y0 >> 5) | (y1 << 27));
			_state3 = x1 ^ y1 ^ (x1 >> 18) ^ (y1 >> 5);
#else
			var x = _state0;
			var y = _state1;
			var next = x + y;
			lower = (uint)next;
			upper = (uint)(next >> 32);
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5); //earlier versions used values 17 and 26; changed to 18 and 5 as justified at http://v8project.blogspot.com/2015/12/theres-mathrandom-and-then-theres.html?showComment=1450389868643#c2004131565745698275
#endif
#endif
		}

#if !MAKEITRANDOM_BACKWARD_COMPATIBLE_V0_1
		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipAhead()"/> skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipAhead()"/> will skip forward by exactly <code>2^64</code> steps each time it is called.</para>
		/// </remarks>
		public override int skipAheadMagnitude { get { return 64; } }

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
		private void SkipAhead(uint b, ref uint x0, ref uint x1, ref uint y0, ref uint y1)
		{
			for (int i = 0; i < 32; ++i)
			{
				if ((b & 1UL) != 0UL)
				{
					x0 ^= _state0;
					x1 ^= _state1;
					y0 ^= _state2;
					y1 ^= _state3;
				}
				b >>= 1;
				Step();
			}
		}
#else
		private void SkipAhead(ulong b, ref ulong x, ref ulong y)
		{
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
		}
#endif

		/// <summary>
		/// Quickly advances the state forward by 2^64 steps.
		/// </summary>
		/// <seealso cref="skipAheadMagnitude"/>
		public override void SkipAhead()
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint x0 = 0;
			uint x1 = 0;
			uint y0 = 0;
			uint y1 = 0;

			SkipAhead(0x635D2DFFU, ref x0, ref x1, ref y0, ref y1);
			SkipAhead(0x8A5CD789U, ref x0, ref x1, ref y0, ref y1);
			SkipAhead(0x5C472F96U, ref x0, ref x1, ref y0, ref y1);
			SkipAhead(0x121FD215U, ref x0, ref x1, ref y0, ref y1);

			_state0 = x0;
			_state1 = x1;
			_state2 = y0;
			_state3 = y1;
#else
			ulong x = 0;
			ulong y = 0;

			SkipAhead(0x8A5CD789635D2DFFUL, ref x, ref y);
			SkipAhead(0x121FD2155C472F96UL, ref x, ref y);

			_state0 = x;
			_state1 = y;
#endif
		}
#endif

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
	}
}
