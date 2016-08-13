/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random numbers within custom ranges.
	/// </summary>
	public static class RandomRange
	{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
		#region Private Helper Tables

		private static readonly byte[] _shiftTable32 = new byte[]
		{
			31, 22, 30, 21, 18, 10, 29,  2, 20, 17, 15, 13,  9,  6, 28,  1,
			23, 19, 11,  3, 16, 14,  7, 24, 12,  4,  8, 25,  5, 26, 27,  0,
		};

		private static readonly byte[] _shiftTable64 = new byte[]
		{
			63,  5, 62,  4, 16, 10, 61,  3, 24, 15, 36,  9, 30, 21, 60,  2,
			12, 26, 23, 14, 45, 35, 43,  8, 33, 29, 52, 20, 49, 41, 59,  1,
			 6, 17, 11, 25, 37, 31, 22, 13, 27, 46, 44, 34, 53, 50, 42,  7,
			18, 38, 32, 28, 47, 54, 51, 19, 39, 48, 55, 40, 56, 57, 58,  0, 
		};

		#endregion
#endif

		#region Open

		/// <summary>
		/// Returns a random integer strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static int OpenRange(this IRandom random, int lowerExclusive, int upperExclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1;
		}

		/// <summary>
		/// Returns a random integer strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random integer in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static int OpenRange(this IRandom random, int upperExclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperExclusive - 1)) + 1;
		}

		/// <summary>
		/// Returns a random unsigned integer strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static uint OpenRange(this IRandom random, uint lowerExclusive, uint upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - lowerExclusive - 1U) + lowerExclusive + 1U;
		}

		/// <summary>
		/// Returns a random unsigned integer strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned integer in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static uint OpenRange(this IRandom random, uint upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - 1U) + 1U;
		}

		/// <summary>
		/// Returns a random long integer strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random long integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static long OpenRange(this IRandom random, long lowerExclusive, long upperExclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperExclusive - lowerExclusive - 1L)) + lowerExclusive + 1L;
		}

		/// <summary>
		/// Returns a random long integer strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random long integer in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static long OpenRange(this IRandom random, long upperExclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperExclusive - 1L)) + 1L;
		}

		/// <summary>
		/// Returns a random unsigned long integer strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned long integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ulong OpenRange(this IRandom random, ulong lowerExclusive, ulong upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - lowerExclusive - 1UL) + lowerExclusive + 1UL;
		}

		/// <summary>
		/// Returns a random unsigned long integer strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned long integer in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ulong OpenRange(this IRandom random, ulong upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - 1UL) + 1UL;
		}

		/// <summary>
		/// Returns a random float strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.OpenFloatUnit(IRandom)"/>.</remarks>
		public static float OpenRange(this IRandom random, float lowerExclusive, float upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * random.OpenFloatUnit() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.OpenFloatUnit(IRandom)"/>.</remarks>
		public static float OpenRange(this IRandom random, float upperExclusive)
		{
			return upperExclusive * random.OpenFloatUnit();
		}

		/// <summary>
		/// Returns a random double strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.OpenDoubleUnit(IRandom)"/>.</remarks>
		public static double OpenRange(this IRandom random, double lowerExclusive, double upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * random.OpenDoubleUnit() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.OpenDoubleUnit(IRandom)"/>.</remarks>
		public static double OpenRange(this IRandom random, double upperExclusive)
		{
			return upperExclusive * random.OpenDoubleUnit();
		}

		#endregion

		#region HalfOpen

		/// <summary>
		/// Returns a random integer greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static int HalfOpenRange(this IRandom random, int lowerInclusive, int upperExclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		/// <summary>
		/// Returns a random integer greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random integer in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static int HalfOpenRange(this IRandom random, int upperExclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperExclusive));
		}

		/// <summary>
		/// Returns a random unsigned integer greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static uint HalfOpenRange(this IRandom random, uint lowerInclusive, uint upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - lowerInclusive) + lowerInclusive;
		}

		/// <summary>
		/// Returns a random unsigned integer greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned integer in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static uint HalfOpenRange(this IRandom random, uint upperExclusive)
		{
			if (upperExclusive == 0U) throw new System.ArgumentOutOfRangeException("upperBound");
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			if (upperExclusive == 1U) return 0U;
#endif
			uint mask = upperExclusive - 1U;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			int rightShift = _shiftTable32[mask * 0x07C4ACDDU >> 27];
#endif
			uint n;
			do
			{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
				n = random.Next32() >> rightShift;
#else
				n = random.Next32() & mask;
#endif
			}
			while (n >= upperExclusive);
			return n;
		}

		/// <summary>
		/// Returns a random long integer greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random long integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static long HalfOpenRange(this IRandom random, long lowerInclusive, long upperExclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperExclusive - lowerInclusive)) + lowerInclusive;
		}

		/// <summary>
		/// Returns a random long integer greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random long integer in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static long HalfOpenRange(this IRandom random, long upperExclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperExclusive));
		}

		/// <summary>
		/// Returns a random unsigned long integer greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned long integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ulong HalfOpenRange(this IRandom random, ulong lowerInclusive, ulong upperExclusive)
		{
			return HalfOpenRange(random, upperExclusive - lowerInclusive) + lowerInclusive;
		}

		/// <summary>
		/// Returns a random unsigned long integer greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned long integer in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ulong HalfOpenRange(this IRandom random, ulong upperExclusive)
		{
			if (upperExclusive == 0UL) throw new System.ArgumentOutOfRangeException("upperBound");
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			if (upperExclusive == 1UL) return 0UL;
#endif
			ulong mask = upperExclusive - 1UL;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			int rightShift = _shiftTable32[mask * 0x03F6EAF2CD271461UL >> 58];
#endif
			ulong n;
			do
			{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
				n = random.Next64() >> rightShift;
#else
				n = random.Next64() & mask;
#endif
			}
			while (n >= upperExclusive);
			return n;
		}

		/// <summary>
		/// Returns a random float greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.HalfOpenFloatUnit(IRandom)"/>.</remarks>
		public static float HalfOpenRange(this IRandom random, float lowerInclusive, float upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * random.HalfOpenFloatUnit() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random float greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.HalfOpenFloatUnit(IRandom)"/>.</remarks>
		public static float HalfOpenRange(this IRandom random, float upperExclusive)
		{
			return upperExclusive * random.HalfOpenFloatUnit();
		}

		/// <summary>
		/// Returns a random double greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.HalfOpenDoubleUnit(IRandom)"/>.</remarks>
		public static double HalfOpenRange(this IRandom random, double lowerInclusive, double upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * random.HalfOpenDoubleUnit() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.HalfOpenDoubleUnit(IRandom)"/>.</remarks>
		public static double HalfOpenRange(this IRandom random, double upperExclusive)
		{
			return upperExclusive * random.HalfOpenDoubleUnit();
		}

		#endregion

		#region HalfClosed

		/// <summary>
		/// Returns a random integer strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static int HalfClosedRange(this IRandom random, int lowerExclusive, int upperInclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperInclusive - lowerExclusive)) + lowerExclusive + 1;
		}

		/// <summary>
		/// Returns a random integer strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random integer in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static int HalfClosedRange(this IRandom random, int upperInclusive)
		{
			return (int)HalfOpenRange(random, (uint)(upperInclusive)) + 1;
		}

		/// <summary>
		/// Returns a random unsigned integer strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static uint HalfClosedRange(this IRandom random, uint lowerExclusive, uint upperInclusive)
		{
			return HalfOpenRange(random, upperInclusive - lowerExclusive) + lowerExclusive + 1U;
		}

		/// <summary>
		/// Returns a random unsigned integer strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned integer in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static uint HalfClosedRange(this IRandom random, uint upperInclusive)
		{
			return HalfOpenRange(random, upperInclusive) + 1U;
		}

		/// <summary>
		/// Returns a random long integer strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random long integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static long HalfClosedRange(this IRandom random, long lowerExclusive, long upperInclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperInclusive - lowerExclusive)) + lowerExclusive + 1L;
		}

		/// <summary>
		/// Returns a random long integer strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random long integer in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static long HalfClosedRange(this IRandom random, long upperInclusive)
		{
			return (long)HalfOpenRange(random, (ulong)(upperInclusive)) + 1L;
		}

		/// <summary>
		/// Returns a random unsigned long integer strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned long integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ulong HalfClosedRange(this IRandom random, ulong lowerExclusive, ulong upperInclusive)
		{
			return HalfOpenRange(random, upperInclusive - lowerExclusive) + lowerExclusive + 1UL;
		}

		/// <summary>
		/// Returns a random unsigned long integer strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned long integer in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ulong HalfClosedRange(this IRandom random, ulong upperInclusive)
		{
			return HalfOpenRange(random, upperInclusive) + 1UL;
		}

		/// <summary>
		/// Returns a random float strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.HalfClosedFloatUnit(IRandom)"/>.</remarks>
		public static float HalfClosedRange(this IRandom random, float lowerExclusive, float upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * random.HalfClosedFloatUnit() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.HalfClosedFloatUnit(IRandom)"/>.</remarks>
		public static float HalfClosedRange(this IRandom random, float upperInclusive)
		{
			return upperInclusive * random.HalfClosedFloatUnit();
		}

		/// <summary>
		/// Returns a random double strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.HalfClosedDoubleUnit(IRandom)"/>.</remarks>
		public static double HalfClosedRange(this IRandom random, double lowerExclusive, double upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * random.HalfClosedDoubleUnit() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random double strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.HalfClosedDoubleUnit(IRandom)"/>.</remarks>
		public static double HalfClosedRange(this IRandom random, double upperInclusive)
		{
			return upperInclusive * random.HalfClosedDoubleUnit();
		}

		#endregion

		#region Closed

		/// <summary>
		/// Returns a random integer greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static int ClosedRange(this IRandom random, int lowerInclusive, int upperInclusive)
		{
			return (int)ClosedRange(random, (uint)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		/// <summary>
		/// Returns a random integer greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random integer in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static int ClosedRange(this IRandom random, int upperInclusive)
		{
			return (int)ClosedRange(random, (uint)(upperInclusive));
		}

		/// <summary>
		/// Returns a random unsigned integer greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static uint ClosedRange(this IRandom random, uint lowerInclusive, uint upperInclusive)
		{
			return ClosedRange(random, upperInclusive - lowerInclusive) + lowerInclusive;
		}

		/// <summary>
		/// Returns a random unsigned integer greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned integer in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static uint ClosedRange(this IRandom random, uint upperInclusive)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			if (upperInclusive == 0U) return 0U;
#endif
			uint mask = upperInclusive;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			int rightShift = _shiftTable32[mask * 0x07C4ACDDU >> 27];
#endif
			uint n;
			do
			{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
				n = random.Next32() >> rightShift;
#else
				n = random.Next32() & mask;
#endif
			}
			while (n > upperInclusive);
			return n;
		}

		/// <summary>
		/// Returns a random long integer greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random long integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static long ClosedRange(this IRandom random, long lowerInclusive, long upperInclusive)
		{
			return (long)ClosedRange(random, (ulong)(upperInclusive - lowerInclusive)) + lowerInclusive;
		}

		/// <summary>
		/// Returns a random long integer greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random long integer in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static long ClosedRange(this IRandom random, long upperInclusive)
		{
			return (long)ClosedRange(random, (ulong)(upperInclusive));
		}

		/// <summary>
		/// Returns a random unsigned long integer greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned long integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ulong ClosedRange(this IRandom random, ulong lowerInclusive, ulong upperInclusive)
		{
			return ClosedRange(random, upperInclusive - lowerInclusive) + lowerInclusive;
		}

		/// <summary>
		/// Returns a random unsigned long integer greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned long integer in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next64()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next64()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ulong ClosedRange(this IRandom random, ulong upperInclusive)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			if (upperInclusive == 0UL) return 0UL;
#endif
			ulong mask = upperInclusive;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			mask |= mask >> 16;
			mask |= mask >> 32;
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			int rightShift = _shiftTable32[mask * 0x03F6EAF2CD271461UL >> 58];
#endif
			ulong n;
			do
			{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
				n = random.Next64() >> rightShift;
#else
				n = random.Next64() & mask;
#endif
			}
			while (n > upperInclusive);
			return n;
		}

		/// <summary>
		/// Returns a random float greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The exclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.ClosedFloatUnit(IRandom)"/>.</remarks>
		public static float ClosedRange(this IRandom random, float lowerInclusive, float upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * random.ClosedFloatUnit() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random float greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.ClosedFloatUnit(IRandom)"/>.</remarks>
		public static float ClosedRange(this IRandom random, float upperInclusive)
		{
			return upperInclusive * random.ClosedFloatUnit();
		}

		/// <summary>
		/// Returns a random double greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The exclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.ClosedDoubleUnit(IRandom)"/>.</remarks>
		public static double ClosedRange(this IRandom random, double lowerInclusive, double upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * random.ClosedDoubleUnit() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.ClosedDoubleUnit(IRandom)"/>.</remarks>
		public static double ClosedRange(this IRandom random, double upperInclusive)
		{
			return upperInclusive * random.ClosedDoubleUnit();
		}

		#endregion
	}
}
