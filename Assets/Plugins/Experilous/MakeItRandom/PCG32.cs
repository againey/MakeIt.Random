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
	public sealed class PCG32 : RandomBase
	{
		[SerializeField] private ulong _state0 = 0UL;
		[SerializeField] private ulong _state1 = 1UL; //to avoid ever having an invalid all 0-bit state

		private PCG32() { }

		private static PCG32 CreateUninitialized()
		{
			return new PCG32();
		}

		/// <summary>
		/// Creates an instance of the XoroShiro128+ engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the XoroShiro128+ engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		/// <seealso cref="RandomBase.Seed()"/>
		public static PCG32 Create()
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
		public static PCG32 Create(int seed)
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
		public static PCG32 Create(params int[] seed)
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
		public static PCG32 Create(string seed)
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
		public static PCG32 Create(IBitGenerator bitGenerator)
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
		public static PCG32 CreateWithState(byte[] stateArray)
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
		public static PCG32 CreateWithState(ulong state0, ulong state1)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(state0, state1);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this XoroShiro128+ engine.</returns>
		public PCG32 Clone()
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
		public void CopyStateFrom(PCG32 source)
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
			_state0 = 0U;
			_state1 = 1U;
			Step();
			_state0 += bitGenerator.Next64();
			Step();
		}

		/// <summary>
		/// Reseed the XoroShiro128+ engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			_state0 += bitGenerator.Next64();
			Step();
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
			_state0 = _state0 * 6364136223846793005UL + _state1;
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
			_state0 = x * 6364136223846793005UL + _state1;
			var y = ((x >> 18) ^ x) >> 27;
			var z = (int)(x >> 59);
			return (uint)((y >> z) | (y << ((-z) & 0x1F)));
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		public override ulong Next64()
		{
			var x0 = _state0;
			_state0 = x0 * 6364136223846793005UL + _state1;
			var y0 = ((x0 >> 18) ^ x0) >> 27;
			var z0 = (int)(x0 >> 59);
			var x1 = _state0;
			_state0 = x1 * 6364136223846793005UL + _state1;
			var y1 = ((x1 >> 18) ^ x1) >> 27;
			var z1 = (int)(x1 >> 59);
			return ((y0 >> z0) | (y0 << ((-z0) & 0x1F))) & 0xFFFFFFFFUL | (((y1 >> z1) | (y1 << ((-z1) & 0x1F))) << 32);
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
	}
}
