/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.Randomization
{
	/// <summary>
	/// Static class for easy access to the default random engine type and a global shared instance of that type.
	/// </summary>
	/// <remarks>
	/// <para>This class provides creation routines that abstract the concrete type of the default random engine.
	/// Currently, XorShift128Plus is used, as it has been shown to have good quality randomness, has a state
	/// that is sufficiently large (128 bits) for most game purposes, and is quick (slightly quicker than
	/// XoroShiro128Plus in tests, due to no native rotation operation which XoroShiro would benefit from).
	/// The underlying engine type or how it is seeded may change with any future version.</para>
	/// <para>A shared instance of this type, created on first access and seeded without parameters (thereby
	/// being based on the system time and possibly other contingent system values), is also availble, and the
	/// standard IRandomEngine methods and properties of this instance are exposed as static functions and
	/// properties in this class.</para>
	/// <para>Other randomness utility classes overload their functions with versions that do not accept an
	/// instance of IRandomEngine as a parameter.  In these cases, the functions will automatically use the
	/// shared instance of the default random engine.</para>
	/// </remarks>
	/// <seealso cref="IRandomEngine"/>
	/// <seealso cref="XorShift128Plus"/>
	public static class DefaultRandomEngine
	{
		private static IRandomEngine _instance = null;

		public static IRandomEngine sharedInstance
		{
			get
			{
				if (_instance == null)
				{
					_instance = Create();
				}
				return _instance;
			}
		}

		public static IRandomEngine Create()
		{
			return XorShift128Plus.Create();
		}

		public static IRandomEngine Create(int seed)
		{
			return XorShift128Plus.Create(seed);
		}

		public static IRandomEngine Create(params int[] seed)
		{
			return XorShift128Plus.Create(seed);
		}

		public static IRandomEngine Create(string seed)
		{
			return XorShift128Plus.Create(seed);
		}

		public static IRandomEngine Create(RandomStateGenerator stateGenerator)
		{
			return XorShift128Plus.Create(stateGenerator);
		}

		public static IRandomEngine Create(IRandomEngine seeder)
		{
			return XorShift128Plus.Create(seeder);
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with a transient value (such as system time).
		/// </summary>
		public static void Seed()
		{
			sharedInstance.Seed();
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied integer value.
		/// </summary>
		/// <param name="seed"></param>
		public static void Seed(int seed)
		{
			sharedInstance.Seed(seed);
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied array of integer values.
		/// </summary>
		/// <param name="seed"></param>
		public static void Seed(params int[] seed)
		{
			sharedInstance.Seed(seed);
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with the supplied string.
		/// </summary>
		/// <param name="seed"></param>
		public static void Seed(string seed)
		{
			sharedInstance.Seed(seed);
		}

		/// <summary>
		/// Reseed the psueod-random sequence with the supplied random state generator.
		/// </summary>
		/// <param name="stateGenerator"></param>
		public static void Seed(RandomStateGenerator stateGenerator)
		{
			sharedInstance.Seed(stateGenerator);
		}

		/// <summary>
		/// Reseed the psueod-random sequence with random data pulled from the supplied random engine.
		/// </summary>
		/// <param name="seeder"></param>
		public static void Seed(IRandomEngine seeder)
		{
			sharedInstance.Seed(seeder);
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and a transient value (such as system time).
		/// </summary>
		public static void MergeSeed()
		{
			sharedInstance.MergeSeed();
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied integer value.
		/// </summary>
		/// <param name="seed"></param>
		public static void MergeSeed(int seed)
		{
			sharedInstance.MergeSeed(seed);
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied array of integer values.
		/// </summary>
		/// <param name="seed"></param>
		public static void MergeSeed(params int[] seed)
		{
			sharedInstance.MergeSeed(seed);
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and the supplied string.
		/// </summary>
		/// <param name="seed"></param>
		public static void MergeSeed(string seed)
		{
			sharedInstance.MergeSeed(seed);
		}

		/// <summary>
		/// Reseed the psueod-random sequence with a combination of its current state and the supplied random state generator.
		/// </summary>
		/// <param name="stateGenerator"></param>
		public static void MergeSeed(RandomStateGenerator stateGenerator)
		{
			sharedInstance.MergeSeed(stateGenerator);
		}

		/// <summary>
		/// Reseed the pseudo-random sequence with a combination of its current state and random data pulled from the supplied random engine.
		/// </summary>
		/// <param name="seeder"></param>
		public static void MergeSeed(IRandomEngine seeder)
		{
			sharedInstance.MergeSeed(seeder);
		}

		/// <summary>
		/// Step the state ahead by a single iteration, and throw away the output.
		/// </summary>
		public static void Step()
		{
			sharedInstance.Step();
		}

		/// <summary>
		/// Get the next 32 bits of random data.
		/// </summary>
		/// <returns>A 32-bit unsigned integer full of pseudo-random bits.</returns>
		public static uint Next32()
		{
			return sharedInstance.Next32();
		}

		/// <summary>
		/// Get the next 64 bits of random data.
		/// </summary>
		/// <returns>A 64-bit unsigned integer full of pseudo-random bits.</returns>
		public static ulong Next64()
		{
			return sharedInstance.Next64();
		}

		/// <summary>
		/// Get a uniformly distributed random unsigned integer greater than or equal to 0 and less than <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The exclusive upper bound of the range in which a pseudo-random integer should be generated.</param>
		/// <returns>A uniformly distributed random 32-bit unsigned integer greater than or equal to 0 and less than <paramref name="upperBound"/>.</returns>
		public static uint NextLessThan(uint upperBound)
		{
			return sharedInstance.NextLessThan(upperBound);
		}

		/// <summary>
		/// Get a uniformly distributed random unsigned integer greater than or equal to 0 and less than or equal to <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The inclusive upper bound of the range in which a pseudo-random integer should be generated.</param>
		/// <returns>A uniformly distributed random 32-bit unsigned integer greater than or equal to 0 and less than or equal to <paramref name="upperBound"/>.</returns>
		public static uint NextLessThanOrEqual(uint upperBound)
		{
			return sharedInstance.NextLessThanOrEqual(upperBound);
		}

		/// <summary>
		/// Get a uniformly distributed random unsigned integer greater than or equal to 0 and less than <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The exclusive upper bound of the range in which a pseudo-random integer should be generated.</param>
		/// <returns>A uniformly distributed random 64-bit unsigned integer greater than or equal to 0 and less than <paramref name="upperBound"/>.</returns>
		public static ulong NextLessThan(ulong upperBound)
		{
			return sharedInstance.NextLessThan(upperBound);
		}

		/// <summary>
		/// Get a uniformly distributed random unsigned integer greater than or equal to 0 and less than or equal to <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The inclusive upper bound of the range in which a pseudo-random integer should be generated.</param>
		/// <returns>A uniformly distributed random 64-bit unsigned integer greater than or equal to 0 and less than or equal to <paramref name="upperBound"/>.</returns>
		public static ulong NextLessThanOrEqual(ulong upperBound)
		{
			return sharedInstance.NextLessThanOrEqual(upperBound);
		}

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipAhead"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipAhead"/>() is guaranteed to skip forward at least <code>2^skipAheadMagnitude</code> steps each time it is called.</para>
		/// <para>This property is guaranteed to be non-negative.  A value of 0 indicates that <see cref="SkipAhead"/>() is not guaranteed to do anything
		/// other than advance the state one single iteration.</para>
		/// </remarks>
		public static int skipAheadMagnitude { get { return sharedInstance.skipAheadMagnitude; } }

		/// <summary>
		/// The binary order of magnitude size of the interveral that <see cref="SkipBack"/>() skips over.
		/// </summary>
		/// <remarks>
		/// <para><see cref="SkipBack"/>() is guaranteed to skip backward at least <code>2^skipBackMagnitude</code> steps each time it is called.</para>
		/// <para>This property may be negative.  A value of 0 indicates that <see cref="SkipBack"/>() is not guaranteed to do anything other than reverse
		/// the state one single iteration.  A negative value indicates that <see cref="SkipBack"/>() is not supported and will throw an exception.</para>
		/// </remarks>
		public static int skipBackMagnitude { get { return sharedInstance.skipBackMagnitude; } }

		/// <summary>
		/// Advances the state forward by a fixed number of iterations, generally in logarithmic time.
		/// </summary>
		/// <remarks>
		/// <para>For some pseudo-random number generators, their state can be advanced by a large number of steps relatively quickly, generally in
		/// logarithmic time based on the size of the skip interval.  This can be useful for independently accessing multiple subsequences within a
		/// long sequence initialized by a single seed, often but not exclusively for the purpose of parallel computation.</para>
		/// </remarks>
		public static void SkipAhead()
		{
			sharedInstance.SkipAhead();
		}

		/// <summary>
		/// Reverses the state backward by a fixed number of iterations, generally in logarithmic time.
		/// </summary>
		/// <remarks>
		/// <para>For some pseudo-random number generators, their state can be reversed by a large number of steps relatively quickly, generally in
		/// logarithmic time based on the size of the skip interval.  This can be useful for independently accessing multiple subsequences within a
		/// long sequence initialized by a single seed, often but not exclusively for the purpose of parallel computation.</para>
		/// </remarks>
		public static void SkipBack()
		{
			sharedInstance.SkipBack();
		}

		/// <summary>
		/// Adapts the random engine to the interface provided by <see cref="System.Random"/>, for use in interfaces that require this common .NET type.
		/// </summary>
		/// <returns>An adapting wrapper around this random engine which is derived from <see cref="System.Random"/>.</returns>
		/// <seealso cref="System.Random"/>
		/// <seealso cref="SystemRandomWrapper32"/>
		/// <seealso cref="SystemRandomWrapper64"/>
		public static System.Random AsSystemRandom()
		{
			return sharedInstance.AsSystemRandom();
		}
	}
}
