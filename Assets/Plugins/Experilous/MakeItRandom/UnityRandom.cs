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
		private UnityRandom() { }

		private static UnityRandom CreateUninitialized()
		{
			return new UnityRandom();
		}

		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		/// <seealso cref="RandomBase.Seed()"/>
		public static UnityRandom Create()
		{
			var instance = CreateUninitialized();
			instance.Seed();
			return instance;
		}

#if UNITY_5_4_OR_NEWER
		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		/// <seealso cref="UnityRandom.Seed(int)"/>
		/// <seealso cref="UnityEngine.Random.InitState(int)"/>
		public static UnityRandom Create(int seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}
#else
		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		/// <seealso cref="UnityRandom.Seed(int)"/>
		/// <seealso cref="UnityEngine.Random.seed"/>
		public static UnityRandom Create(int seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}
#endif

		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		/// <seealso cref="RandomBase.Seed(int[])"/>
		public static UnityRandom Create(params int[] seed)
		{
			var instance = CreateUninitialized();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of a wrapper around the <c>UnityEngine.Random</c> engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		/// <seealso cref="RandomBase.Seed(string)"/>
		public static UnityRandom Create(string seed)
		{
			var instance = CreateUninitialized();
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
			var instance = CreateUninitialized();
			instance.Seed(bitGenerator);
			return instance;
		}

#if UNITY_5_4_OR_NEWER
		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="stateArray"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="IRandom.SaveState()"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the <c>UnityEngine.Random</c> engine.</returns>
		public static UnityRandom CreateWithState(byte[] stateArray)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(stateArray);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the <c>UnityEngine.Random</c> engine using the provided <paramref name="state"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="state">State data generated from an earlier call to <see cref="IRandom.SaveState()"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the <c>UnityEngine.Random</c> engine.</returns>
		public static UnityRandom CreateWithState(UnityEngine.Random.State state)
		{
			var instance = CreateUninitialized();
			instance.RestoreState(state);
			return instance;
		}
#endif

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this Unity Random engine.</returns>
		public UnityRandom Clone()
		{
			var instance = CreateUninitialized();
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
		/// <returns>The internal state as a byte array.</returns>
		public override byte[] SaveState()
		{
#if UNITY_5_4_OR_NEWER
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream())
			{
				binaryFormatter.Serialize(stream, UnityEngine.Random.state);
				return stream.ToArray();
			}
#else
			throw new NotSupportedException("The Unity Random class does not expose any method to save its state.");
#endif
		}

#if UNITY_5_4_OR_NEWER
		/// <summary>
		/// Saves the Unity Random engine's internal state using Unity's own state structure, which can be restored later.
		/// </summary>
		/// <param name="state">The state instance into which the current state will be written.</param>
		public void SaveState(out UnityEngine.Random.State state)
		{
			state = UnityEngine.Random.state;
		}
#endif

		/// <summary>
		/// Restores Unity Random engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public override void RestoreState(byte[] stateArray)
		{
#if UNITY_5_4_OR_NEWER
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				UnityEngine.Random.state = (UnityEngine.Random.State)binaryFormatter.Deserialize(stream);
			}
#else
			throw new NotSupportedException("The Unity Random class does not expose any method to restore its state.");
#endif
		}

#if UNITY_5_4_OR_NEWER
		/// <summary>
		/// Restores Unity Random engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="state">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public void RestoreState(UnityEngine.Random.State state)
		{
			UnityEngine.Random.state = state;
		}
#endif

#if UNITY_5_4_OR_NEWER
		/// <summary>
		/// Reseed the Unity Random engine with the supplied integer value.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <remarks>This function will seed the engine state in exactly the same way as calling <see cref="UnityEngine.Random.InitState(int)"/> would.</remarks>
		/// <seealso cref="UnityEngine.Random.InitState(int)"/>
		public override void Seed(int seed)
		{
			UnityEngine.Random.InitState(seed);
		}
#else
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
#endif

		/// <summary>
		/// Reseed the Unity Random engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void Seed(IBitGenerator bitGenerator)
		{
#if UNITY_5_4_OR_NEWER
			UnityEngine.Random.InitState((int)bitGenerator.Next32());
#else
			UnityEngine.Random.seed = (int)bitGenerator.Next32();
#endif
		}

		/// <summary>
		/// Reseed the Unity Random engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
#if UNITY_5_4_OR_NEWER
			IBitGenerator currentState = new RandomStateGenerator(SaveState());
			UnityEngine.Random.InitState((int)(currentState.Next32() ^ bitGenerator.Next32()));
#else
			UnityEngine.Random.seed ^= (int)bitGenerator.Next32();
#endif
		}

		/// <summary>
		/// The number of bits that the Unity Random engine naturally produces in a single step.
		/// </summary>
		public override int stepBitCount { get { return 30; } }

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

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		/// <remarks><see cref="UnityEngine.Random"/> does not expose a public interface for getting 32 bits of uniformly distributed bits in a single call,
		/// so instead, two calls to <see cref="UnityEngine.Random.Range(int, int)"/> are required to get a total of 32 bits.</remarks>
		public override uint Next32()
		{
			// UnityEngine.Random.Range() cannot quite provide 31 uniform bits, so we use Range(0, 0x40000000) to get 30 bits at a time.
			return
				(uint)UnityEngine.Random.Range(0, 0x40000000) << 2 & 0xFFFF0000U | // Use the upper 16 bits of the first 30 bits generated.
				(uint)UnityEngine.Random.Range(0, 0x40000000)      & 0x0000FFFFU;  // Use the lower 16 bits of the final 30 bits generated.
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		/// <remarks><see cref="UnityEngine.Random"/> does not expose a public interface for getting 64 bits of uniformly distributed bits in a single call,
		/// so instead, three calls to <see cref="UnityEngine.Random.Range(int, int)"/> are required to get a total of 64 bits.</remarks>
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
		/// <remarks>Since Unity's random engine is a static global, all instances of the <see cref="UnityRandom"/> wrapper reference
		/// the same underlying state and are therefore always equal.</remarks>
		public static bool operator ==(UnityRandom lhs, UnityRandom rhs)
		{
			return (lhs == null) == (rhs == null);
		}

		/// <summary>
		/// Checks to see if the state of two random engines are not equal.
		/// </summary>
		/// <param name="lhs">The first random engine whose state is to be compared.</param>
		/// <param name="rhs">The second random engine whose state is to be compared.</param>
		/// <returns>Returns false if neither random engine is null and both have the same state, or if both are null, true otherwise.</returns>
		/// <remarks>Since Unity's random engine is a static global, all instances of the <see cref="UnityRandom"/> wrapper reference
		/// the same underlying state and are therefore always equal.</remarks>
		public static bool operator !=(UnityRandom lhs, UnityRandom rhs)
		{
			return (lhs == null) != (rhs == null);
		}

		/// <summary>
		/// Checks if the specified random engine has the same state as this one.
		/// </summary>
		/// <param name="other">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and both random engines have the same state, false otherwise.</returns>
		/// <remarks>Since Unity's random engine is a static global, all instances of the <see cref="UnityRandom"/> wrapper reference
		/// the same underlying state and are therefore always equal.</remarks>
		public bool Equals(UnityRandom other)
		{
			return other != null;
		}

		/// <summary>
		/// Checks if the specified random engine is the same type and has the same state as this one.
		/// </summary>
		/// <param name="obj">The other random engine whose state is to be compared.</param>
		/// <returns>Returns true if the other random engine is not null and is the same type and has the same state as this one, false otherwise.</returns>
		/// <remarks>Since Unity's random engine is a static global, all instances of the <see cref="UnityRandom"/> wrapper reference
		/// the same underlying state and are therefore always equal.</remarks>
		public override bool Equals(object obj)
		{
			return Equals(obj as UnityRandom);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
#if UNITY_5_4_OR_NEWER
			var state = SaveState();
			uint hashCode = 0;
			for (int i = 0; i < state.Length; ++i)
			{
				hashCode = ((hashCode << 8) | (hashCode >> 24)) ^ (uint)state[i].GetHashCode();
			}
			return (int)hashCode;
#else
			throw new NotSupportedException("The Unity Random class does not expose any method to access its state.");
#endif
		}

		/// <inheritdoc />
		public override string ToString()
		{
#if UNITY_5_4_OR_NEWER
			return string.Format("UnityRandom {{ 0x{0:X8} }}", GetHashCode());
#else
			return "UnityRandom";
#endif
		}
	}
}
