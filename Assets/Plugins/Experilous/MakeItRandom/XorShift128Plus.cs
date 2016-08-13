/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

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
	/// 
	/// <para>As its name implies, it maintains 128 bits of state.  It natively generates 64 bits of pseudo-
	/// random data at a time.</para>
	/// </remarks>
	/// <seealso cref="IBitGenerator"/>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	public sealed class XorShift128Plus : RandomBase
	{
		[SerializeField] private ulong _state0 = 0UL;
		[SerializeField] private ulong _state1 = 1UL; //to avoid ever having an invalid all 0-bit state

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		public static XorShift128Plus Create()
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed();
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		public static XorShift128Plus Create(int seed)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		public static XorShift128Plus Create(params int[] seed)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		public static XorShift128Plus Create(string seed)
		{
			var instance = CreateInstance<XorShift128Plus>();
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
			var instance = CreateInstance<XorShift128Plus>();
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
			var instance = CreateInstance<XorShift128Plus>();
			instance.RestoreState(stateArray);
			return instance;
		}

		/// <summary>
		/// Creates an instance of the XorShift128+ engine using the provided <paramref name="state0"/> and <paramref name="state1"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="state0">The first element of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second element of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the XorShift128+ engine.</returns>
		public static XorShift128Plus CreateWithState(ulong state0, ulong state1)
		{
			var instance = CreateInstance<XorShift128Plus>();
			instance.RestoreState(state0, state1);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this XorShift128+ engine.</returns>
		public XorShift128Plus Clone()
		{
			var instance = CreateInstance<XorShift128Plus>();
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
			_state0 = source._state0;
			_state1 = source._state1;
		}

		/// <summary>
		/// Saves the XorShift128+ engine's internal state as a byte array, which can be restored later.
		/// </summary>
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
		/// Saves the XorShift128+ engine's internal state as a pair of unsigned 64-bit integers, which can be restored later.
		/// </summary>
		/// <param name="state0">The first element of state data to be saved.</param>
		/// <param name="state1">The second element of state data to be saved.</param>
		public void SaveState(out ulong state0, out ulong state1)
		{
			state0 = _state0;
			state1 = _state1;
		}

		/// <summary>
		/// Restores the XorShift128+ engine's internal state from a byte array which had been previously saved.
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
		/// Restores the XorShift128+ engine's internal state from a pair of unsigned 64-bit integers which had been previously saved.
		/// </summary>
		/// <param name="state0">The first element of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		/// <param name="state1">The second element of state data generated from an earlier call to <see cref="SaveState(out ulong, out ulong)"/> on a binary-compatible type of random engine.</param>
		public void RestoreState(ulong state0, ulong state1)
		{
			if (state0 == 0 && state1 == 0)
			{
				throw new System.ArgumentException("All 0 bits is an invalid state for the XorShift128+ random number generator.");
			}
			_state0 = state0;
			_state1 = state1;
		}

#if MAKEITRANDOM_BACK_COMPAT_V0_1
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

#if MAKEITRANDOM_BACK_COMPAT_V0_1
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
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		/// <remarks>64 bits of data are generated and thrown away by this call.</remarks>
		public override void Step()
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
#else
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5); //earlier versions used values 17 and 26; changed to 18 and 5 as justified at http://v8project.blogspot.com/2015/12/theres-mathrandom-and-then-theres.html?showComment=1450389868643#c2004131565745698275
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
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
			return (uint)(_state1 + y);
#else
			var x = _state0;
			var y = _state1;
			var next = (uint)(x + y);
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5); //earlier versions used values 17 and 26; changed to 18 and 5 as justified at http://v8project.blogspot.com/2015/12/theres-mathrandom-and-then-theres.html?showComment=1450389868643#c2004131565745698275
			return next;
#endif
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		public override ulong Next64()
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var x = _state0;
			var y = _state1;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 17) ^ (y >> 26);
			return _state1 + y;
#else
			var x = _state0;
			var y = _state1;
			var next = x + y;
			_state0 = y;
			x ^= x << 23;
			_state1 = x ^ y ^ (x >> 18) ^ (y >> 5); //earlier versions used values 17 and 26; changed to 18 and 5 as justified at http://v8project.blogspot.com/2015/12/theres-mathrandom-and-then-theres.html?showComment=1450389868643#c2004131565745698275
			return next;
#endif
		}

#if !MAKEITRANDOM_BACK_COMPAT_V0_1
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

			ulong b = 0x8A5CD789635D2DFFUL;
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

			b = 0x121FD2155C472F96UL;
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
#endif

		/// <summary>
		/// Adapts the random engine to the interface provided by <see cref="System.Random"/>, for use in interfaces that require this common .NET type.
		/// </summary>
		/// <returns>An adapting wrapper around this random engine which is derived from <see cref="System.Random"/>.</returns>
		/// <seealso cref="System.Random"/>
		/// <seealso cref="SystemRandomWrapper64"/>
		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper64(this);
		}
	}
}
