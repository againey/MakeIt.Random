﻿/******************************************************************************\
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

		private const uint _deBruijnMultiplier32 = 0x07C4ACDDU;
		private const int _deBruijnShift32 = 27;

		private const ulong _deBruijnMultiplier64 = 0x03F6EAF2CD271461UL;
		private const int _deBruijnShift64 = 58;

		#endregion
#endif

		#region Range Open/Open (lowerExclusive, upperExclusive)

		/// <summary>
		/// Returns a random signed byte strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random signed byte in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static sbyte RangeOO(this IRandom random, sbyte lowerExclusive, sbyte upperExclusive)
		{
			return (sbyte)(RangeCO(random, (byte)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1);
		}

		/// <summary>
		/// Returns a random signed byte strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random signed byte in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// (0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static sbyte RangeOO(this IRandom random, sbyte upperExclusive)
		{
			return (sbyte)(RangeCO(random, (byte)(upperExclusive - 1)) + 1);
		}

		/// <summary>
		/// Returns a random byte strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random byte in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static byte RangeOO(this IRandom random, byte lowerExclusive, byte upperExclusive)
		{
			return (byte)(RangeCO(random, (byte)(upperExclusive - lowerExclusive - 1U)) + lowerExclusive + 1U);
		}

		/// <summary>
		/// Returns a random byte strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random byte in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// (0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static byte RangeOO(this IRandom random, byte upperExclusive)
		{
			return (byte)(RangeCO(random, (byte)(upperExclusive - 1U)) + 1U);
		}

		/// <summary>
		/// Returns a random short integer strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random short integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static short RangeOO(this IRandom random, short lowerExclusive, short upperExclusive)
		{
			return (short)(RangeCO(random, (ushort)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1);
		}

		/// <summary>
		/// Returns a random short integer strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random short integer in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static short RangeOO(this IRandom random, short upperExclusive)
		{
			return (short)(RangeCO(random, (ushort)(upperExclusive - 1)) + 1);
		}

		/// <summary>
		/// Returns a random unsigned short integer strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned short integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ushort RangeOO(this IRandom random, ushort lowerExclusive, ushort upperExclusive)
		{
			return (ushort)(RangeCO(random, (ushort)(upperExclusive - lowerExclusive - 1U)) + lowerExclusive + 1U);
		}

		/// <summary>
		/// Returns a random unsigned short integer strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned short integer in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ushort RangeOO(this IRandom random, ushort upperExclusive)
		{
			return (ushort)(RangeCO(random, (ushort)(upperExclusive - 1U)) + 1U);
		}

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
		public static int RangeOO(this IRandom random, int lowerExclusive, int upperExclusive)
		{
			return (int)RangeCO(random, (uint)(upperExclusive - lowerExclusive - 1)) + lowerExclusive + 1;
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
		public static int RangeOO(this IRandom random, int upperExclusive)
		{
			return (int)RangeCO(random, (uint)(upperExclusive - 1)) + 1;
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
		public static uint RangeOO(this IRandom random, uint lowerExclusive, uint upperExclusive)
		{
			return RangeCO(random, upperExclusive - lowerExclusive - 1U) + lowerExclusive + 1U;
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
		public static uint RangeOO(this IRandom random, uint upperExclusive)
		{
			return RangeCO(random, upperExclusive - 1U) + 1U;
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
		public static long RangeOO(this IRandom random, long lowerExclusive, long upperExclusive)
		{
			return (long)RangeCO(random, (ulong)(upperExclusive - lowerExclusive - 1L)) + lowerExclusive + 1L;
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
		public static long RangeOO(this IRandom random, long upperExclusive)
		{
			return (long)RangeCO(random, (ulong)(upperExclusive - 1L)) + 1L;
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
		public static ulong RangeOO(this IRandom random, ulong lowerExclusive, ulong upperExclusive)
		{
			return RangeCO(random, upperExclusive - lowerExclusive - 1UL) + lowerExclusive + 1UL;
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
		public static ulong RangeOO(this IRandom random, ulong upperExclusive)
		{
			return RangeCO(random, upperExclusive - 1UL) + 1UL;
		}

		/// <summary>
		/// Returns a random float strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.FloatOO(IRandom)"/>.</remarks>
		public static float RangeOO(this IRandom random, float lowerExclusive, float upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * random.FloatOO() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.FloatOO(IRandom)"/>.</remarks>
		public static float RangeOO(this IRandom random, float upperExclusive)
		{
			return upperExclusive * random.FloatOO();
		}

		/// <summary>
		/// Returns a random double strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.DoubleOO(IRandom)"/>.</remarks>
		public static double RangeOO(this IRandom random, double lowerExclusive, double upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * random.DoubleOO() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.DoubleOO(IRandom)"/>.</remarks>
		public static double RangeOO(this IRandom random, double upperExclusive)
		{
			return upperExclusive * random.DoubleOO();
		}

		#endregion

		#region Range Closed/Open [lowerInclusive, upperExclusive)

		/// <summary>
		/// Returns a random signed byte greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random signed byte in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static sbyte RangeCO(this IRandom random, sbyte lowerInclusive, sbyte upperExclusive)
		{
			return (sbyte)(RangeCO(random, (byte)(upperExclusive - lowerInclusive)) + lowerInclusive);
		}

		/// <summary>
		/// Returns a random signed byte greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random signed byte in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// [0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static sbyte RangeCO(this IRandom random, sbyte upperExclusive)
		{
			return (sbyte)RangeCO(random, (byte)upperExclusive);
		}

		/// <summary>
		/// Returns a random byte greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random byte in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static byte RangeCO(this IRandom random, byte lowerInclusive, byte upperExclusive)
		{
			return (byte)(RangeCO(random, (byte)(upperExclusive - lowerInclusive)) + lowerInclusive);
		}

		/// <summary>
		/// Returns a random byte greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random byte in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// [0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static byte RangeCO(this IRandom random, byte upperExclusive)
		{
			if (upperExclusive == 0U) throw new System.ArgumentOutOfRangeException("upperBound");
			uint mask = upperExclusive - 1U;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			uint n;
			do
			{
				n = random.Next32() & mask;
			}
			while (n >= upperExclusive);
			return (byte)n;
		}

		/// <summary>
		/// Returns a random short integer greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random short integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static short RangeCO(this IRandom random, short lowerInclusive, short upperExclusive)
		{
			return (short)(RangeCO(random, (ushort)(upperExclusive - lowerInclusive)) + lowerInclusive);
		}

		/// <summary>
		/// Returns a random short integer greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random short integer in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static short RangeCO(this IRandom random, short upperExclusive)
		{
			return (short)RangeCO(random, (ushort)upperExclusive);
		}

		/// <summary>
		/// Returns a random unsigned short integer greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned short integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ushort RangeCO(this IRandom random, ushort lowerInclusive, ushort upperExclusive)
		{
			return (ushort)(RangeCO(random, (ushort)(upperExclusive - lowerInclusive)) + lowerInclusive);
		}

		/// <summary>
		/// Returns a random unsigned short integer greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random unsigned short integer in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperExclusive"/>) with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ushort RangeCO(this IRandom random, ushort upperExclusive)
		{
			if (upperExclusive == 0U) throw new System.ArgumentOutOfRangeException("upperBound");
			uint mask = upperExclusive - 1U;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			uint n;
			do
			{
				n = random.Next32() & mask;
			}
			while (n >= upperExclusive);
			return (ushort)n;
		}

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
		public static int RangeCO(this IRandom random, int lowerInclusive, int upperExclusive)
		{
			return (int)RangeCO(random, (uint)(upperExclusive - lowerInclusive)) + lowerInclusive;
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
		public static int RangeCO(this IRandom random, int upperExclusive)
		{
			return (int)RangeCO(random, (uint)upperExclusive);
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
		public static uint RangeCO(this IRandom random, uint lowerInclusive, uint upperExclusive)
		{
			return RangeCO(random, upperExclusive - lowerInclusive) + lowerInclusive;
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
		public static uint RangeCO(this IRandom random, uint upperExclusive)
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
			int rightShift = _shiftTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];
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
		public static long RangeCO(this IRandom random, long lowerInclusive, long upperExclusive)
		{
			return (long)RangeCO(random, (ulong)(upperExclusive - lowerInclusive)) + lowerInclusive;
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
		public static long RangeCO(this IRandom random, long upperExclusive)
		{
			return (long)RangeCO(random, (ulong)upperExclusive);
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
		public static ulong RangeCO(this IRandom random, ulong lowerInclusive, ulong upperExclusive)
		{
			return RangeCO(random, upperExclusive - lowerInclusive) + lowerInclusive;
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
		public static ulong RangeCO(this IRandom random, ulong upperExclusive)
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
			int rightShift = _shiftTable64[mask * _deBruijnMultiplier64 >> _deBruijnShift64];
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.FloatCO(IRandom)"/>.</remarks>
		public static float RangeCO(this IRandom random, float lowerInclusive, float upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * random.FloatCO() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random float greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.FloatCO(IRandom)"/>.</remarks>
		public static float RangeCO(this IRandom random, float upperExclusive)
		{
			return upperExclusive * random.FloatCO();
		}

		/// <summary>
		/// Returns a random double greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.DoubleCO(IRandom)"/>.</remarks>
		public static double RangeCO(this IRandom random, double lowerInclusive, double upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * random.DoubleCO() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.DoubleCO(IRandom)"/>.</remarks>
		public static double RangeCO(this IRandom random, double upperExclusive)
		{
			return upperExclusive * random.DoubleCO();
		}

		#endregion

		#region Range Open/Closed (lowerExclusive, upperInclusive]

		/// <summary>
		/// Returns a random signed byte strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random signed byte in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static sbyte RangeOC(this IRandom random, sbyte lowerExclusive, sbyte upperInclusive)
		{
			return (sbyte)(RangeCO(random, (byte)(upperInclusive - lowerExclusive)) + lowerExclusive + 1);
		}

		/// <summary>
		/// Returns a random signed byte strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random signed byte in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// (0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static sbyte RangeOC(this IRandom random, sbyte upperInclusive)
		{
			return (sbyte)(RangeCO(random, (byte)upperInclusive) + 1);
		}

		/// <summary>
		/// Returns a random byte strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random byte in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static byte RangeOC(this IRandom random, byte lowerExclusive, byte upperInclusive)
		{
			return (byte)(RangeCO(random, (byte)(upperInclusive - lowerExclusive)) + lowerExclusive + 1U);
		}

		/// <summary>
		/// Returns a random byte strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random byte in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// (0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static byte RangeOC(this IRandom random, byte upperInclusive)
		{
			return (byte)(RangeCO(random, (byte)upperInclusive) + 1U);
		}

		/// <summary>
		/// Returns a random short integer strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random short integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static short RangeOC(this IRandom random, short lowerExclusive, short upperInclusive)
		{
			return (short)(RangeCO(random, (ushort)(upperInclusive - lowerExclusive)) + lowerExclusive + 1);
		}

		/// <summary>
		/// Returns a random short integer strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random short integer in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static short RangeOC(this IRandom random, short upperInclusive)
		{
			return (short)(RangeCO(random, (ushort)upperInclusive) + 1);
		}

		/// <summary>
		/// Returns a random unsigned short short integer strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned short short integer in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ushort RangeOC(this IRandom random, ushort lowerExclusive, ushort upperInclusive)
		{
			return (ushort)(RangeCO(random, (ushort)(upperInclusive - lowerExclusive)) + lowerExclusive + 1U);
		}

		/// <summary>
		/// Returns a random unsigned short short integer strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned short short integer in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// (0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ushort RangeOC(this IRandom random, ushort upperInclusive)
		{
			return (ushort)(RangeCO(random, (ushort)upperInclusive) + 1U);
		}

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
		public static int RangeOC(this IRandom random, int lowerExclusive, int upperInclusive)
		{
			return (int)RangeCO(random, (uint)(upperInclusive - lowerExclusive)) + lowerExclusive + 1;
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
		public static int RangeOC(this IRandom random, int upperInclusive)
		{
			return (int)RangeCO(random, (uint)upperInclusive) + 1;
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
		public static uint RangeOC(this IRandom random, uint lowerExclusive, uint upperInclusive)
		{
			return RangeCO(random, upperInclusive - lowerExclusive) + lowerExclusive + 1U;
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
		public static uint RangeOC(this IRandom random, uint upperInclusive)
		{
			return RangeCO(random, upperInclusive) + 1U;
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
		public static long RangeOC(this IRandom random, long lowerExclusive, long upperInclusive)
		{
			return (long)RangeCO(random, (ulong)(upperInclusive - lowerExclusive)) + lowerExclusive + 1L;
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
		public static long RangeOC(this IRandom random, long upperInclusive)
		{
			return (long)RangeCO(random, (ulong)upperInclusive) + 1L;
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
		public static ulong RangeOC(this IRandom random, ulong lowerExclusive, ulong upperInclusive)
		{
			return RangeCO(random, upperInclusive - lowerExclusive) + lowerExclusive + 1UL;
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
		public static ulong RangeOC(this IRandom random, ulong upperInclusive)
		{
			return RangeCO(random, upperInclusive) + 1UL;
		}

		/// <summary>
		/// Returns a random float strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.FloatOC(IRandom)"/>.</remarks>
		public static float RangeOC(this IRandom random, float lowerExclusive, float upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * random.FloatOC() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.FloatOC(IRandom)"/>.</remarks>
		public static float RangeOC(this IRandom random, float upperInclusive)
		{
			return upperInclusive * random.FloatOC();
		}

		/// <summary>
		/// Returns a random double strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.DoubleOC(IRandom)"/>.</remarks>
		public static double RangeOC(this IRandom random, double lowerExclusive, double upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * random.DoubleOC() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random double strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.DoubleOC(IRandom)"/>.</remarks>
		public static double RangeOC(this IRandom random, double upperInclusive)
		{
			return upperInclusive * random.DoubleOC();
		}

		#endregion

		#region Range Closed/Closed [lowerInclusive, upperInclusive]

		/// <summary>
		/// Returns a random signed byte greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random signed byte in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static sbyte RangeCC(this IRandom random, sbyte lowerInclusive, sbyte upperInclusive)
		{
			return (sbyte)(RangeCC(random, (byte)(upperInclusive - lowerInclusive)) + lowerInclusive);
		}

		/// <summary>
		/// Returns a random signed byte greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random signed byte in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// [0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static sbyte RangeCC(this IRandom random, sbyte upperInclusive)
		{
			return (sbyte)RangeCC(random, (byte)upperInclusive);
		}

		/// <summary>
		/// Returns a random byte greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random byte in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static byte RangeCC(this IRandom random, byte lowerInclusive, byte upperInclusive)
		{
			return (byte)(RangeCC(random, (byte)(upperInclusive - lowerInclusive)) + lowerInclusive);
		}

		/// <summary>
		/// Returns a random byte greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random byte in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates bytes in the range
		/// [0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static byte RangeCC(this IRandom random, byte upperInclusive)
		{
			uint mask = upperInclusive;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			uint n;
			do
			{
				n = random.Next32() & mask;
			}
			while (n > upperInclusive);
			return (byte)n;
		}

		/// <summary>
		/// Returns a random short integer greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random short integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static short RangeCC(this IRandom random, short lowerInclusive, short upperInclusive)
		{
			return (short)(RangeCC(random, (ushort)(upperInclusive - lowerInclusive)) + lowerInclusive);
		}

		/// <summary>
		/// Returns a random short integer greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random short integer in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static short RangeCC(this IRandom random, short upperInclusive)
		{
			return (short)RangeCC(random, (ushort)upperInclusive);
		}

		/// <summary>
		/// Returns a random unsigned short integer greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned short integer in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ushort RangeCC(this IRandom random, ushort lowerInclusive, ushort upperInclusive)
		{
			return (ushort)(RangeCC(random, (ushort)(upperInclusive - lowerInclusive)) + lowerInclusive);
		}

		/// <summary>
		/// Returns a random unsigned short integer greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random unsigned short integer in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates integers in the range
		/// [0, <paramref name="upperInclusive"/>] with perfect distribution.</para>
		/// <para>Depending on the size of the range requested, this function may need to call <see cref="IBitGenerator.Next32()"/> more than once
		/// on the supplied random engine.  For the worst case range sizes, the average number of calls to <see cref="IBitGenerator.Next32()"/>
		/// will be just under two, though the maximum number of calls for any single call to this function is theoretically unbounded.
		/// The closer the range size is to the next larger power of two, the more efficient the function will be on average.</para>
		/// </remarks>
		public static ushort RangeCC(this IRandom random, ushort upperInclusive)
		{
			uint mask = upperInclusive;
			mask |= mask >> 1;
			mask |= mask >> 2;
			mask |= mask >> 4;
			mask |= mask >> 8;
			uint n;
			do
			{
				n = random.Next32() & mask;
			}
			while (n > upperInclusive);
			return (ushort)n;
		}

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
		public static int RangeCC(this IRandom random, int lowerInclusive, int upperInclusive)
		{
			return (int)RangeCC(random, (uint)(upperInclusive - lowerInclusive)) + lowerInclusive;
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
		public static int RangeCC(this IRandom random, int upperInclusive)
		{
			return (int)RangeCC(random, (uint)upperInclusive);
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
		public static uint RangeCC(this IRandom random, uint lowerInclusive, uint upperInclusive)
		{
			return RangeCC(random, upperInclusive - lowerInclusive) + lowerInclusive;
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
		public static uint RangeCC(this IRandom random, uint upperInclusive)
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
			int rightShift = _shiftTable32[mask * _deBruijnMultiplier32 >> _deBruijnShift32];
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
		public static long RangeCC(this IRandom random, long lowerInclusive, long upperInclusive)
		{
			return (long)RangeCC(random, (ulong)(upperInclusive - lowerInclusive)) + lowerInclusive;
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
		public static long RangeCC(this IRandom random, long upperInclusive)
		{
			return (long)RangeCC(random, (ulong)upperInclusive);
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
		public static ulong RangeCC(this IRandom random, ulong lowerInclusive, ulong upperInclusive)
		{
			return RangeCC(random, upperInclusive - lowerInclusive) + lowerInclusive;
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
		public static ulong RangeCC(this IRandom random, ulong upperInclusive)
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
			int rightShift = _shiftTable64[mask * _deBruijnMultiplier64 >> _deBruijnShift64];
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.FloatCC(IRandom)"/>.</remarks>
		public static float RangeCC(this IRandom random, float lowerInclusive, float upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * random.FloatCC() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random float greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.FloatCC(IRandom)"/>.</remarks>
		public static float RangeCC(this IRandom random, float upperInclusive)
		{
			return upperInclusive * random.FloatCC();
		}

		/// <summary>
		/// Returns a random double greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The exclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.DoubleCC(IRandom)"/>.</remarks>
		public static double RangeCC(this IRandom random, double lowerInclusive, double upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * random.DoubleCC() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomUnit.DoubleCC(IRandom)"/>.</remarks>
		public static double RangeCC(this IRandom random, double upperInclusive)
		{
			return upperInclusive * random.DoubleCC();
		}

		#endregion
	}
}
