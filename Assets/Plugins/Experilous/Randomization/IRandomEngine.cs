/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	/// <summary>
	/// Interface for a basic engine for generating sequences of raw pseudo-random bits.
	/// </summary>
	/// <remarks>
	/// This interface offers only basic access to a random sequence of bits, letting the implementations
	/// focus entirely on the generation of pseudo-random data.
	/// </remarks>
	public interface IRandomEngine
	{
		/// <summary>
		/// Saves the pseudo-random sequence's internal state as a byte array, which can be restored later.
		/// </summary>
		byte[] SaveState();

		/// <summary>
		/// Restores the pseudo-random sequence's internal state from a byte array which had been previously saved.
		/// </summary>
		void RestoreState(byte[] stateArray);

		/// <summary>
		/// Reseed the pseudo-random sequence with a transient value (such as system time).
		/// </summary>
		void Seed();

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied integer value.
		/// </summary>
		/// <param name="seed"></param>
		void Seed(int seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied array of integer values.
		/// </summary>
		/// <param name="seed"></param>
		void Seed(params int[] seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied string.
		/// </summary>
		/// <param name="seed"></param>
		void Seed(string seed);

		/// <summary>
		/// Reseed the psueod-random sequence with the supplied random state generator.
		/// </summary>
		/// <param name="stateGenerator"></param>
		void Seed(RandomStateGenerator stateGenerator);

		/// <summary>
		/// Reseed the psueod-random sequence with random data pulled from the supplied random engine.
		/// </summary>
		/// <param name="seeder"></param>
		void Seed(IRandomEngine seeder);

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and a transient value (such as system time).
		/// </summary>
		void MergeSeed();

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied integer value.
		/// </summary>
		/// <param name="seed"></param>
		void MergeSeed(int seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied array of integer values.
		/// </summary>
		/// <param name="seed"></param>
		void MergeSeed(params int[] seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied string.
		/// </summary>
		/// <param name="seed"></param>
		void MergeSeed(string seed);

		/// <summary>
		/// Reseed the psueod-random sequence with a combination of its current state and the supplied random state generator.
		/// </summary>
		/// <param name="stateGenerator"></param>
		void MergeSeed(RandomStateGenerator stateGenerator);

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and random data pulled from the supplied random engine.
		/// </summary>
		/// <param name="seeder"></param>
		void MergeSeed(IRandomEngine seeder);

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		void Step();

		/// <summary>
		/// Get the next 32 bits of random data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer full of pseudo-random bits.</returns>
		uint Next32();

		/// <summary>
		/// <summary>
		/// Get the next 64 bits of random data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer full of pseudo-random bits.</returns>
		ulong Next64();

		/// <summary>
		/// Get a uniformly distributed random unsigned integer greater than or equal to 0 and less than <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The exclusive upper bound of the range in which a pseudo-random integer should be generated.</param>
		/// <returns>A uniformly distributed random 32-bit unsigned integer greater than or equal to 0 and less than <paramref name="upperBound"/>.</returns>
		uint NextLessThan(uint upperBound);

		/// <summary>
		/// Get a uniformly distributed random unsigned integer greater than or equal to 0 and less than or equal to <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The inclusive upper bound of the range in which a pseudo-random integer should be generated.</param>
		/// <returns>A uniformly distributed random 32-bit unsigned integer greater than or equal to 0 and less than or equal to <paramref name="upperBound"/>.</returns>
		uint NextLessThanOrEqual(uint upperBound);

		/// <summary>
		/// Get a uniformly distributed random unsigned integer greater than or equal to 0 and less than <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The exclusive upper bound of the range in which a pseudo-random integer should be generated.</param>
		/// <returns>A uniformly distributed random 64-bit unsigned integer greater than or equal to 0 and less than <paramref name="upperBound"/>.</returns>
		ulong NextLessThan(ulong upperBound);

		/// <summary>
		/// Get a uniformly distributed random unsigned integer greater than or equal to 0 and less than or equal to <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The inclusive upper bound of the range in which a pseudo-random integer should be generated.</param>
		/// <returns>A uniformly distributed random 64-bit unsigned integer greater than or equal to 0 and less than or equal to <paramref name="upperBound"/>.</returns>
		ulong NextLessThanOrEqual(ulong upperBound);

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipAhead"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipAhead"/>() is guaranteed to skip forward at least <code>2^skipAheadMagnitude</code> steps each time it is called.</para>
		/// <para>This property is guaranteed to be non-negative.  A value of 0 indicates that <see cref="SkipAhead"/>() is not guaranteed to do anything
		/// other than advance the state one single iteration.</para>
		/// </remarks>
		int skipAheadMagnitude { get; }

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipBack"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipBack"/>() is guaranteed to skip backward at least <code>2^skipBackMagnitude</code> steps each time it is called.</para>
		/// <para>This property may be negative.  A value of 0 indicates that <see cref="SkipBack"/>() is not guaranteed to do anything other than reverse
		/// the state one single iteration.  A negative value indicates that <see cref="SkipBack"/>() is not supported and will throw an exception.</para>
		/// </remarks>
		int skipBackMagnitude { get; }

		/// <summary>
		/// Advances the state forward by a fixed number of iterations, generally in logarithmic time.
		/// </summary>
		/// <remarks>
		/// <para>For some pseudo-random number generators, their state can be advanced by a large number of steps relatively quickly, generally in
		/// logarithmic time based on the size of the skip interval.  This can be useful for independently accessing multiple subsequences within a
		/// long sequence initialized by a single seed, often but not exclusively for the purpose of parallel computation.</para>
		/// </remarks>
		void SkipAhead();

		/// <summary>
		/// Reverses the state backward by a fixed number of iterations, generally in logarithmic time.
		/// </summary>
		/// <remarks>
		/// <para>For some pseudo-random number generators, their state can be reversed by a large number of steps relatively quickly, generally in
		/// logarithmic time based on the size of the skip interval.  This can be useful for independently accessing multiple subsequences within a
		/// long sequence initialized by a single seed, often but not exclusively for the purpose of parallel computation.</para>
		/// </remarks>
		void SkipBack();

		/// <summary>
		/// Adapts the random engine to the interface provided by <see cref="System.Random"/>, for use in interfaces that require this common .NET type.
		/// </summary>
		/// <returns>An adapting wrapper around this random engine which is derived from <see cref="System.Random"/>.</returns>
		/// <seealso cref="System.Random"/>
		/// <seealso cref="SystemRandomWrapper32"/>
		/// <seealso cref="SystemRandomWrapper64"/>
		System.Random AsSystemRandom();
	}
}
