/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// Adapts the standard Unity random class <see cref="UnityEngine.Random"/> to the <see cref="IRandom"/> interface.
	/// </summary>
	/// <remarks>
	/// <para>Because consumers of <see cref="IRandom"/> expect to get 32 or 64 uniformly distributed bits at a time, but
	/// <see cref="UnityEngine.Random"/> does offer such functionality natively, this wrapper inevitably introduces a substantial reduction in
	/// performance relative to using <c>UnityEngine.Random</c> directly.</para>
	/// <para><c>UnityEngine.Random</c> is a static class, so there is no way to create independent instances of the Unity random engine.
	/// Although this adapter class presents the engine as distinct instances, they all reference the same underlying instance that
	/// Unity uses internally, and so all instances of <c>UnityRandom</c> essentially share the same internal state.</para>
	/// <para>Another consequence of defering to <c>UnityEngine.Random</c> internally is that this class cannot be used on any thread
	/// other than the main thread, while most other implementations of <see cref="IRandom"/> do not care which thread they are used on,
	/// as long as they are not accessed in a thread-unsafe way.</para>
	/// </remarks>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	/// <seealso cref="UnityEngine.Random"/>
	public sealed class UnityRandom : RandomBase
	{
		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		public static UnityRandom Create()
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed();
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		public static UnityRandom Create(int seed)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		public static UnityRandom Create(params int[] seed)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		public static UnityRandom Create(string seed)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static UnityRandom Create(IBitGenerator bitGenerator)
		{
			var instance = CreateInstance<UnityRandom>();
			instance.Seed(bitGenerator);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this Unity Random engine.</returns>
		public UnityRandom Clone()
		{
			var instance = CreateInstance<UnityRandom>();
			return instance;
		}

		/// <summary>
		/// Copies the state of the <paramref name="source"/> Unity Random engine into this engine, so that this engine will independently generate the same sequence of random values as the source.
		/// </summary>
		/// <param name="source"></param>
		/// <remarks>The source engine is not altered.</remarks>
		public void CopyStateFrom(UnityRandom source)
		{
			// Since Unity uses a shared global instance, this function is a no-op.
			// Maintained for consistency with other random engine classes.
		}

		/// <summary>
		/// Saves the Unity Random engine's internal state as a byte array, which can be restored later.
		/// </summary>
		public override byte[] SaveState()
		{
			throw new NotSupportedException("The Unity Random class does not expose any method to save its state.");
		}

		/// <summary>
		/// Restores Unity Random engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public override void RestoreState(byte[] stateArray)
		{
			throw new NotSupportedException("The Unity Random class does not expose any method to restore its state.");
		}

		/// <summary>
		/// Reseed the Unity Random engine with the supplied integer value.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <remarks>This function will seed the engine state in exactly the same way as assignment to the <see cref="UnityEngine.Random.seed"/> field would.</remarks>
		/// <seealso cref="UnityEngine.Random.seed"/>
		public override void Seed(int seed)
		{
			UnityEngine.Random.seed = seed;
		}

		/// <summary>
		/// Reseed the Unity Random engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void Seed(IBitGenerator bitGenerator)
		{
			UnityEngine.Random.seed = (int)bitGenerator.Next32();
		}

		/// <summary>
		/// Reseed the Unity Random engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			UnityEngine.Random.seed ^= (int)bitGenerator.Next32();
		}

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		/// <remarks>32 bits of data are generated and thrown away by this call.</remarks>
		public override void Step()
		{
#pragma warning disable 0219
			float throwaway = UnityEngine.Random.value;
#pragma warning restore 0219
		}

		private uint Next16()
		{
			return (uint)UnityEngine.Random.Range(0, 65536);
		}

		private uint Next24()
		{
			return (uint)UnityEngine.Random.Range(0, 16777216);
		}

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		/// <remarks>64 bits of data are generated by this call; 32 bits are returned, while the other 32 bits are thrown away.
		/// Thus, a single call to this method leaves the random engine in the same state as a single call to <see cref="Next64()"/>.</remarks>
		public override uint Next32()
		{
			// UnityEngine.Random.Range() cannot quite provide 31 uniform bits, so we use Range(0, 0x40000000) to get 30 bits at a time.
			return
				(uint)UnityEngine.Random.Range(0, 0x40000000) << 2 & 0xFFFF0000U | // Use the upper 16 bits of the first 30 bits generated.
				(uint)UnityEngine.Random.Range(0, 0x40000000)      & 0x0000FFFFU;  // Use the lower 16 bits of the final 30 bits generated.
		}

		public override ulong Next64()
		{
			// UnityEngine.Random.Range() cannot quite provide 31 uniform bits, so we use Range(0, 0x40000000) to get 30 bits at a time.
			return
				(ulong)UnityEngine.Random.Range(0, 0x40000000) << 34 & 0xFFFFF80000000000UL | // Use the upper 21 bits of the first 30 bits generated.
				(ulong)UnityEngine.Random.Range(0, 0x40000000) << 17 & 0x000007FFFFE00000UL | // Use the middle 22 bits of the next 30 bits generated.
				(ulong)UnityEngine.Random.Range(0, 0x40000000)       & 0x00000000001FFFFFUL;  // Use the lower 21 bits of the final 30 bits generated.
		}

		/// <summary>
		/// Adapts the random engine to the interface provided by <see cref="System.Random"/>, for use in interfaces that require this common .NET type.
		/// </summary>
		/// <returns>An adapting wrapper around this random engine which is derived from <see cref="System.Random"/>.</returns>
		/// <seealso cref="System.Random"/>
		/// <seealso cref="SystemRandomWrapper32"/>
		public override System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper32(this);
		}
	}
}
