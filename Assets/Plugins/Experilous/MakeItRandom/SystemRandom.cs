/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;
using UnityEngine;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// Adapts the standard random engine class <see cref="System.Random"/> from the .NET libary to the <see cref="IRandom"/> interface.
	/// </summary>
	/// <remarks>Because consumers of <see cref="IRandom"/> expect to get 32 or 64 uniformly distributed bits at a time, but
	/// <see cref="System.Random"/> does offer such functionality natively, this wrapper inevitably introduces a substantial reduction in
	/// performance relative to using System.Random directly.</remarks>
	/// <seealso cref="IRandom"/>
	/// <seealso cref="RandomBase"/>
	/// <seealso cref="System.Random"/>
	public sealed class SystemRandom : RandomBase
	{
		[SerializeField] private System.Random _random;

		/// <summary>
		/// Creates an instance of a wrapper around the .NET System.Random engine using mildly unpredictable data to initialize the engine's state.
		/// </summary>
		/// <returns>A newly created instance of the .NET System.Random engine.</returns>
		/// <seealso cref="IRandom.Seed()"/>
		/// <seealso cref="System.Random.Random()"/>
		public static SystemRandom Create()
		{
			var instance = CreateInstance<SystemRandom>();
			instance.Seed();
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the .NET System.Random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the .NET System.Random engine.</returns>
		/// <seealso cref="IRandom.Seed(int)"/>
		/// <seealso cref="System.Random.Random(int)"/>
		public static SystemRandom Create(int seed)
		{
			var instance = CreateInstance<SystemRandom>();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the .NET System.Random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the .NET System.Random engine.</returns>
		/// <seealso cref="IRandom.Seed(int[])"/>
		public static SystemRandom Create(params int[] seed)
		{
			var instance = CreateInstance<SystemRandom>();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the .NET System.Random engine using the provided <paramref name="seed"/> to initialize the engine's state.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the .NET System.Random engine.</returns>
		/// <seealso cref="IRandom.Seed(string)"/>
		public static SystemRandom Create(string seed)
		{
			var instance = CreateInstance<SystemRandom>();
			instance.Seed(seed);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the .NET System.Random engine using the provided <paramref name="bitGenerator"/> to initialize the engine's state.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <returns>A newly created instance of the .NET System.Random engine.</returns>
		/// <seealso cref="IRandom.Seed(IBitGenerator)"/>
		/// <seealso cref="RandomStateGenerator"/>
		public static SystemRandom Create(IBitGenerator bitGenerator)
		{
			var instance = CreateInstance<SystemRandom>();
			instance.Seed(bitGenerator);
			return instance;
		}

		/// <summary>
		/// Creates an instance of a wrapper around the .NET System.Random engine using the provided <paramref name="stateArray"/> data to directly initialize the engine's state.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="IRandom.SaveState()"/> on a binary-compatible type of random engine.</param>
		/// <returns>A newly created instance of the .NET System.Random engine.</returns>
		public static SystemRandom CreateWithState(byte[] stateArray)
		{
			var instance = CreateInstance<SystemRandom>();
			instance.RestoreState(stateArray);
			return instance;
		}

		/// <summary>
		/// Creates an exact duplicate of the random engine, which will independently generate the same sequence of random values as this instance.
		/// </summary>
		/// <returns>The cloned instance of this .NET System.Random engine.</returns>
		public SystemRandom Clone()
		{
			var instance = CreateInstance<SystemRandom>();
			instance.CopyStateFrom(this);
			return instance;
		}

		/// <summary>
		/// Copies the state of the <paramref name="source"/> .NET System.Random engine into this engine, so that this engine will independently generate the same sequence of random values as the source.
		/// </summary>
		/// <param name="source"></param>
		/// <remarks>The source engine is not altered.</remarks>
		public void CopyStateFrom(SystemRandom source)
		{
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream())
			{
				binaryFormatter.Serialize(stream, source._random);
				_random = (System.Random)binaryFormatter.Deserialize(stream);
			}
		}

		/// <summary>
		/// Saves the .NET System.Random engine's internal state as a byte array, which can be restored later.
		/// </summary>
		public override byte[] SaveState()
		{
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream())
			{
				binaryFormatter.Serialize(stream, _random);
				return stream.ToArray();
			}
		}

		/// <summary>
		/// Restores the .NET System.Random engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()"/> on a binary-compatible type of random engine.</param>
		public override void RestoreState(byte[] stateArray)
		{
			var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (var stream = new System.IO.MemoryStream(stateArray))
			{
				_random = (System.Random)binaryFormatter.Deserialize(stream);
			}
		}

		/// <summary>
		/// Reseed the .NET System.Random engine with a transient value (such as system time).
		/// </summary>
		/// <remarks>This function will seed the engine state in exactly the same way as the parameterless constructor <see cref="System.Random.Random()"/> would.</remarks>
		/// <seealso cref="System.Random.Random()"/>
		public override void Seed()
		{
			_random = new System.Random();
		}

		/// <summary>
		/// Reseed the .NET System.Random engine with the supplied integer value.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <remarks>This function will seed the engine state in exactly the same way as the int parameter constructor <see cref="System.Random.Random(int)"/> would.</remarks>
		/// <seealso cref="System.Random.Random(int)"/>
		public override void Seed(int seed)
		{
			_random = new System.Random(seed);
		}

		/// <summary>
		/// Reseed the .NET System.Random engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void Seed(IBitGenerator bitGenerator)
		{
			_random = new System.Random((int)bitGenerator.Next32());
		}

		/// <summary>
		/// Reseed the .NET System.Random engine with a combination of its current state and a transient value (such as system time).
		/// </summary>
		public override void MergeSeed()
		{
			MergeSeed(Create());
		}

		/// <summary>
		/// Reseed the .NET System.Random engine with a combination of its current state and the supplied integer value.
		/// </summary>
		/// <param name="seed">An integer value used, in conjuction with the current state, to indirectly determine the new state of the random engine.</param>
		public override void MergeSeed(int seed)
		{
			MergeSeed(Create(seed));
		}

		/// <summary>
		/// Reseed the .NET System.Random engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public override void MergeSeed(IBitGenerator bitGenerator)
		{
			_random = new System.Random((int)(Next32() ^ bitGenerator.Next32()));
		}

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		/// <remarks>32 bits of data are generated and thrown away by this call.</remarks>
		public override void Step()
		{
			_random.Next();
		}

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		/// <remarks><see cref="System.Random"/> does not expose a public interface for getting 32 bits of uniformly distributed bits in a single call,
		/// so instead, two calls to <see cref="System.Random.Next(int)"/> are required to get a total of 32 bits.</remarks>
		public override uint Next32()
		{
			// System.Random.Next() cannot quite provide 31 uniform bits, so we use Next(0x40000000) to get 30 bits at a time.
			return
				(uint)_random.Next(0x40000000) << 2 & 0xFFFF0000U | // Use the upper 16 bits of the first 30 bits generated.
				(uint)_random.Next(0x40000000)      & 0x0000FFFFU;  // Use the lower 16 bits of the final 30 bits generated.
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		/// <remarks><see cref="System.Random"/> does not expose a public interface for getting 64 bits of uniformly distributed bits in a single call,
		/// so instead, three calls to <see cref="System.Random.Next(int)"/> are required to get a total of 64 bits.</remarks>
		public override ulong Next64()
		{
			// System.Random.Next() cannot quite provide 31 uniform bits, so we use Next(0x40000000) to get 30 bits at a time.
			return
				(ulong)_random.Next(0x40000000) << 34 & 0xFFFFF80000000000UL | // Use the upper 21 bits of the first 30 bits generated.
				(ulong)_random.Next(0x40000000) << 17 & 0x000007FFFFE00000UL | // Use the middle 22 bits of the next 30 bits generated.
				(ulong)_random.Next(0x40000000)       & 0x00000000001FFFFFUL;  // Use the lower 21 bits of the final 30 bits generated.
		}

		/// <summary>
		/// Returns the underlying instance of <see cref="System.Random"/>.
		/// </summary>
		/// <returns>The underlying instance of <see cref="System.Random"/>.</returns>
		/// <remarks>
		/// <note type="important">Note that while this <see cref="SystemRandom"/> instance and the <see cref="System.Random"/> instance returned
		/// from this method will normally continue refer to the same underlying random engine state, any calls to <see cref="CopyStateFrom(SystemRandom)"/>,
		/// <see cref="RestoreState(byte[])"/>, or any of the Seed() or MergeSeed() methods will break this connection, in contrast to most implementations
		/// of <see cref="IRandom"/> which return wrappers around themselves instead of a reference to their internal state.</note>
		/// </remarks>
		/// <seealso cref="System.Random"/>
		public override System.Random AsSystemRandom()
		{
			return _random;
		}
	}
}
