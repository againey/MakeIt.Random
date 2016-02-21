namespace Experilous.Randomization
{
	/// <summary>
	/// Interface for a basic engine for generating sequences of raw pseudo-random bits.
	/// </summary>
	/// <remarks>
	/// This interface offers only basic access to a random sequence of bits, letting the implementations
	/// focus entirely on the generation of pseudo-random data.  For more advanced generation of various
	/// kinds of values, see <see cref="RandomUtility"/> which builds on top of random data produced by
	/// random engines implementing this interface.
	/// </remarks>
	/// <seealso cref="RandomUtility"/>
	public interface IRandomEngine
	{
		/// <summary>
		/// Reseed the pseudo-random sequence with a transient value (such as system time).
		/// </summary>
		/// <seealso cref="RandomSeedUtility"/>
		void Seed();

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied integer value.
		/// </summary>
		/// <param name="seed"></param>
		/// <seealso cref="RandomSeedUtility"/>
		void Seed(int seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied array of integer values.
		/// </summary>
		/// <param name="seed"></param>
		/// <seealso cref="RandomSeedUtility"/>
		void Seed(params int[] seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied string.
		/// </summary>
		/// <param name="seed"></param>
		/// <seealso cref="RandomSeedUtility"/>
		void Seed(string seed);

		/// <summary>
		/// Reseed the psueod-random sequence with random data pulled from the supplied random engine.
		/// </summary>
		/// <param name="seeder"></param>
		/// <seealso cref="RandomSeedUtility"/>
		void Seed(IRandomEngine seeder);

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and a transient value (such as system time).
		/// </summary>
		/// <seealso cref="RandomSeedUtility"/>
		void MergeSeed();

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied integer value.
		/// </summary>
		/// <param name="seed"></param>
		/// <seealso cref="RandomSeedUtility"/>
		void MergeSeed(int seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied array of integer values.
		/// </summary>
		/// <param name="seed"></param>
		/// <seealso cref="RandomSeedUtility"/>
		void MergeSeed(params int[] seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied string.
		/// </summary>
		/// <param name="seed"></param>
		/// <seealso cref="RandomSeedUtility"/>
		void MergeSeed(string seed);

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and random data pulled from the supplied random engine.
		/// </summary>
		/// <param name="seeder"></param>
		/// <seealso cref="RandomSeedUtility"/>
		void MergeSeed(IRandomEngine seeder);

		/// <summary>
		/// Get the next 32 bits of random data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer full of pseudo-random bits.</returns>
		uint Next32();

		/// <summary>
		/// Get the next <paramref name="bitCount"/> bits of random data, up to 32 bits.
		/// </summary>
		/// <param name="bitCount">The number of bits of random data desired.</param>
		/// <returns>A 32-bit unsigned integer, where the lowest <paramref name="bitCount"/> bits are random, and the remaining high bits are all 0.</returns>
		uint Next32(int bitCount);

		/// <summary>
		/// Get the next 64 bits of random data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer full of pseudo-random bits.</returns>
		ulong Next64();

		/// <summary>
		/// Get the next <paramref name="bitCount"/> bits of random data, up to 64 bits.
		/// </summary>
		/// <param name="bitCount">The number of bits of random data desired.</param>
		/// <returns>A 64-bit unsigned integer, where the lowest <paramref name="bitCount"/> bits are random, and the remaining high bits are all 0.</returns>
		ulong Next64(int bitCount);

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
		/// Adapts the random engine to the interface provided by <see cref="System.Random"/>, for use in interfaces that require this common .NET type.
		/// </summary>
		/// <returns>An adapting wrapper around this random engine which is derived from <see cref="System.Random"/>.</returns>
		/// <seealso cref="System.Random"/>
		/// <seealso cref="SystemRandomWrapper32"/>
		/// <seealso cref="SystemRandomWrapper64"/>
		System.Random AsSystemRandom();
	}
}
