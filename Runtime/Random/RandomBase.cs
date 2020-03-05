/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace MakeIt.Random
{
	/// <summary>
	/// An abstract base class that eases the implementation of a random engine.
	/// </summary>
	/// <remarks>
	/// <para>By implementing most random-producing functions in terms of a smaller core set of random-producing
	/// functions, the final implementation becomes easier to write.  As a consequence, however, this is likely to be
	/// less efficient due to potentially throwing away some random bits.  For random engines that generate random bits
	/// very quickly but in specific chunk sizes, this is okay and probably even preferable for performance.  But for
	/// random engines that are slow about generating random bits (such as a cryptographically secure PRNG), this base
	/// class is not ideal.</para>
	/// </remarks>
	/// <seealso cref="IRandom"/>
	public abstract class RandomBase : IRandom
	{
		/// <summary>
		/// Saves the pseudo-random engine's internal state as a byte array, which can be restored later.
		/// </summary>
		/// <returns>The internal state as a byte array.</returns>
		public virtual byte[] SaveState()
		{
			throw new System.NotSupportedException("This random engine is unable to save its state to a byte array.");
		}

		/// <summary>
		/// Restores the pseudo-random engine's internal state from a byte array which had been previously saved.
		/// </summary>
		/// <param name="stateArray">State data generated from an earlier call to <see cref="SaveState()" /> on a binary-compatible type of random engine.</param>
		public virtual void RestoreState(byte[] stateArray)
		{
			throw new System.NotSupportedException("This random engine is unable to restore its state from a byte array.");
		}

		/// <summary>
		/// Reseed the pseudo-random engine with a transient value (such as system time).
		/// </summary>
		/// <seealso cref="RandomStateGenerator()"/>
		public virtual void Seed()
		{
			Seed(new RandomStateGenerator());
		}

		/// <summary>
		/// Reseed the pseudo-random engine with the supplied integer value.
		/// </summary>
		/// <param name="seed">An integer value used to indirectly determine the new state of the random engine.</param>
		/// <seealso cref="RandomStateGenerator(int)"/>
		public virtual void Seed(int seed)
		{
			Seed(new RandomStateGenerator(seed));
		}

		/// <summary>
		/// Reseed the pseudo-random engine with the supplied array of integer values.
		/// </summary>
		/// <param name="seed">An array of integer values used to indirectly determine the new state of the random engine.</param>
		/// <seealso cref="RandomStateGenerator(int[])"/>
		public virtual void Seed(params int[] seed)
		{
			Seed(new RandomStateGenerator(seed));
		}

		/// <summary>
		/// Reseed the pseudo-random engine with the supplied string.
		/// </summary>
		/// <param name="seed">A string value used to indirectly determine the new state of the random engine.</param>
		/// <seealso cref="RandomStateGenerator(string)"/>
		public virtual void Seed(string seed)
		{
			Seed(new RandomStateGenerator(seed));
		}

		/// <summary>
		/// Reseed the pseudo-random engine with the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public abstract void Seed(IBitGenerator bitGenerator);

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and a transient value (such as system time).
		/// </summary>
		/// <seealso cref="RandomStateGenerator()"/>
		public virtual void MergeSeed()
		{
			MergeSeed(new RandomStateGenerator());
		}

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and the supplied integer value.
		/// </summary>
		/// <param name="seed">An integer value used, in conjuction with the current state, to indirectly determine the new state of the random engine.</param>
		/// <seealso cref="RandomStateGenerator(int)"/>
		public virtual void MergeSeed(int seed)
		{
			MergeSeed(new RandomStateGenerator(seed));
		}

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and the supplied array of integer values.
		/// </summary>
		/// <param name="seed">An array of integer values used, in conjuction with the current state, to indirectly determine the new state of the random engine.</param>
		/// <seealso cref="RandomStateGenerator(int[])"/>
		public virtual void MergeSeed(params int[] seed)
		{
			MergeSeed(new RandomStateGenerator(seed));
		}

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and the supplied string.
		/// </summary>
		/// <param name="seed">An string value used, in conjuction with the current state, to indirectly determine the new state of the random engine.</param>
		/// <seealso cref="RandomStateGenerator(string)"/>
		public virtual void MergeSeed(string seed)
		{
			MergeSeed(new RandomStateGenerator(seed));
		}

		/// <summary>
		/// Reseed the pseudo-random engine with a combination of its current state and the supplied bit generator.
		/// </summary>
		/// <param name="bitGenerator">A supplier of bits used, in conjuction with the current state, to directly determine the new state of the random engine.</param>
		/// <seealso cref="IRandom"/>
		/// <seealso cref="RandomStateGenerator"/>
		public abstract void MergeSeed(IBitGenerator bitGenerator);

		/// <summary>
		/// The number of bits that the psuedo-random engine naturally produces in a single step.
		/// </summary>
		public abstract int stepBitCount { get; }

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		public virtual void Step()
		{
			Next64();
		}

		/// <summary>
		/// Get the next 32 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer representing the next 32 bits of pseudo-random generated data.</returns>
		public virtual uint Next32()
		{
			return (uint)Next64();
		}

		/// <summary>
		/// Get the next 64 bits of pseudo-random generated data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer representing the next 64 bits of pseudo-random generated data.</returns>
		public abstract ulong Next64();

		/// <summary>
		/// Get the next 64 bits of generated data as two 32-bit values.
		/// </summary>
		/// <param name="lower">The lower 32 bits of generated data.</param>
		/// <param name="upper">The upper 32 bits of generated data.</param>
		public virtual void Next64(out uint lower, out uint upper)
		{
			ulong next = Next64();
			lower = (uint)next;
			upper = (uint)(next >> 32);
		}

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipAhead"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipAhead()"/> is guaranteed to skip forward at least <code>2^skipAheadMagnitude</code> steps each time it is called.</para>
		/// <para>This property is guaranteed to be non-negative.  A value of 0 indicates that <see cref="SkipAhead()"/> is not guaranteed to do anything
		/// other than advance the state one single iteration.</para>
		/// </remarks>
		public virtual int skipAheadMagnitude { get { return 0; } }

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipBack"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipBack()"/> is guaranteed to skip backward at least <code>2^skipBackMagnitude</code> steps each time it is called.</para>
		/// <para>This property may be negative.  A value of 0 indicates that <see cref="SkipBack()"/> is not guaranteed to do anything other than reverse
		/// the state one single iteration.  A negative value indicates that <see cref="SkipBack()"/> is not supported and will throw an exception.</para>
		/// </remarks>
		public virtual int skipBackMagnitude { get { return -1; } }

		/// <summary>
		/// Advances the state forward by a fixed number of iterations, generally in logarithmic time.
		/// </summary>
		/// <remarks>
		/// <para>For some pseudo-random number generators, their state can be advanced by a large number of steps relatively quickly, generally in
		/// logarithmic time based on the size of the skip interval.  This can be useful for independently accessing multiple subsequences within a
		/// long sequence initialized by a single seed, often but not exclusively for the purpose of parallel computation.</para>
		/// </remarks>
		public virtual void SkipAhead() { Step(); }

		/// <summary>
		/// Reverses the state backward by a fixed number of iterations, generally in logarithmic time.
		/// </summary>
		/// <remarks>
		/// <para>For some pseudo-random number generators, their state can be reversed by a large number of steps relatively quickly, generally in
		/// logarithmic time based on the size of the skip interval.  This can be useful for independently accessing multiple subsequences within a
		/// long sequence initialized by a single seed, often but not exclusively for the purpose of parallel computation.</para>
		/// </remarks>
		public virtual void SkipBack() { throw new System.NotSupportedException("Skipping backward in the sequence is not supported by this random engine."); }

		/// <summary>
		/// Adapts the random engine to the interface provided by <see cref="System.Random"/>, for use in interfaces that require this common .NET type.
		/// </summary>
		/// <returns>An adapting wrapper around this random engine which is derived from <see cref="System.Random"/>.</returns>
		/// <seealso cref="System.Random"/>
		/// <seealso cref="SystemRandomWrapper"/>
		public virtual System.Random AsSystemRandom()
		{
			return new SystemRandomWrapper(this);
		}

		/// <summary>
		/// A helper function for derived classes implementing <see cref="IRandom.SaveState()"/>, to write a byte of saved state data to a binary stream.
		/// </summary>
		/// <param name="writer">The binary stream to which the random engine's state is being saved.</param>
		/// <param name="stateElement">The byte of data to be saved, which is part of the random engine's state.</param>
		protected static void SaveState(System.IO.BinaryWriter writer, byte stateElement)
		{
			writer.Write(stateElement);
		}

		/// <summary>
		/// A helper function for derived classes implementing <see cref="IRandom.SaveState()"/>, to write an unsigned integer of saved state data to a binary stream.
		/// </summary>
		/// <param name="writer">The binary stream to which the random engine's state is being saved.</param>
		/// <param name="stateElement">The unsigned integer of data to be saved, which is part of the random engine's state.</param>
		protected static void SaveState(System.IO.BinaryWriter writer, uint stateElement)
		{
			for (int i = sizeof(uint) * 8 - 8; i >= 0; i -= 8)
				writer.Write((byte)(stateElement >> i));
		}

		/// <summary>
		/// A helper function for derived classes implementing <see cref="IRandom.SaveState()"/>, to write an unsigned long of saved state data to a binary stream.
		/// </summary>
		/// <param name="writer">The binary stream to which the random engine's state is being saved.</param>
		/// <param name="stateElement">The unsigned long of data to be saved, which is part of the random engine's state.</param>
		protected static void SaveState(System.IO.BinaryWriter writer, ulong stateElement)
		{
			for (int i = sizeof(ulong) * 8 - 8; i >= 0; i -= 8)
				writer.Write((byte)(stateElement >> i));
		}

		/// <summary>
		/// A helper function for derived classes implementing <see cref="IRandom.RestoreState(byte[])"/>, to read a byte of saved state data from a binary stream.
		/// </summary>
		/// <param name="reader">The binary stream from which the random engine's state is being restored.</param>
		/// <param name="stateElement">The byte of data to be restored, which is part of the random engine's state.</param>
		protected static void RestoreState(System.IO.BinaryReader reader, out byte stateElement)
		{
			stateElement = reader.ReadByte();
		}

		/// <summary>
		/// A helper function for derived classes implementing <see cref="IRandom.RestoreState(byte[])"/>, to read an unsigned integer of saved state data from a binary stream.
		/// </summary>
		/// <param name="reader">The binary stream from which the random engine's state is being restored.</param>
		/// <param name="stateElement">The unsigned integer of data to be restored, which is part of the random engine's state.</param>
		protected static void RestoreState(System.IO.BinaryReader reader, out uint stateElement)
		{
			stateElement = 0;
			for (int i = sizeof(uint) * 8 - 8; i >= 0; i -= 8)
				stateElement |= ((uint)reader.ReadByte()) << i;
		}

		/// <summary>
		/// A helper function for derived classes implementing <see cref="IRandom.RestoreState(byte[])"/>, to read an unsigned long of saved state data from a binary stream.
		/// </summary>
		/// <param name="reader">The binary stream from which the random engine's state is being restored.</param>
		/// <param name="stateElement">The unsigned long of data to be restored, which is part of the random engine's state.</param>
		protected static void RestoreState(System.IO.BinaryReader reader, out ulong stateElement)
		{
			stateElement = 0;
			for (int i = sizeof(ulong) * 8 - 8; i >= 0; i -= 8)
				stateElement |= ((ulong)reader.ReadByte()) << i;
		}
	}
}
