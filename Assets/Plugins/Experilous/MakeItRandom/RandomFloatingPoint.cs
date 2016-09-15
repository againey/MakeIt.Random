/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if UNITY_64 && !MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#elif !MAKEITRANDOM_OPTIMIZED_FOR_64BIT
#define MAKEITRANDOM_OPTIMIZED_FOR_32BIT
#endif

using System.Runtime.InteropServices;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random floating point numbers.
	/// </summary>
	public static class RandomFloatingPoint
	{
		#region Private Constants

		private const uint _maxUnsignedFixed32LessThanFloatOne = 0xFFFFFF7FU;
		private const int _maxSignedFixed32LessThanFloatOne = 0x7FFFFFBF;

		private const uint _floatExponent31 = 0x0F800000U;
		private const uint _floatExponent32 = 0x10000000U;

		private const ulong _maxUnsignedFixed64LessThanFloatOne = 0xFFFFFFFFFFFFFBFFUL;
		private const long _maxSignedFixed64LessThanFloatOne = 0x7FFFFFFFFFFFFDFFL;

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
		private const uint _doubleExponent63 = 0x02000000U;
		private const uint _doubleExponent64 = 0x04000000U;
#else
		private const ulong _doubleExponent63 = 0x0200000000000000UL;
		private const ulong _doubleExponent64 = 0x0400000000000000UL;
#endif

		#endregion

		#region Unit Open/Open (0, 1)

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (0, 1).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range (0, 1) with
		/// perfect distribution across 2^23 - 1 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next32()"/> once on the supplied
		/// random engine.  Only one in 2^23 calls will require an additional call to <see cref="IBitGenerator.Next32()"/>, with the same
		/// chance for requiring indefinitely more calls.</para>
		/// </remarks>
		public static float FloatOO(this IRandom random)
		{
			uint n;
			do
			{
				n = random.Next32();
			} while (n <= 0x1FFU); // While the upper 23 bits are all zeros.
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & (n >> 9);
			return value.number - 1f;
		}

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (0, 1).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range (0, 1) with
		/// perfect distribution across 2^52 - 1 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next64()"/> once on the supplied
		/// random engine.  Only one in 2^52 calls will require an additional call to <see cref="IBitGenerator.Next64()"/>, with the same
		/// chance for requiring indefinitely more calls.</para>
		/// </remarks>
		public static double DoubleOO(this IRandom random)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			do
			{
				random.Next64(out lower, out upper);
			} while (upper == 0U && lower <= 0xFFFU); // While the upper 52 bits are all zeros.

			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = (lower >> 12) | (upper << 20);
			value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & (upper >> 12);
			return value.number - 1d;
#else
			ulong n;
			do
			{
				n = random.Next64();
			} while (n <= 0xFFFUL); // While the upper 52 bits are all zeros.
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.bits = Detail.FloatingPoint.doubleOne | Detail.FloatingPoint.doubleMantissaMask & (n >> 12);
			return value.number - 1d;
#endif
		}

		#endregion

		#region Unit Closed/Open [0, 1)

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [0, 1) with
		/// perfect distribution across 2^23 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next32()"/> once on the supplied random engine.</para>
		/// </remarks>
		public static float FloatCO(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			return (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)random.Next32() << 29))) - 1df;
#else
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & (random.Next32());
			return value.number - 1f;
#endif
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [0, 1) with
		/// perfect distribution across 2^52 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next64()"/> once on the supplied random engine.</para>
		/// </remarks>
		public static double DoubleCO(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			return System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64())) - 1d;
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & upper;
			return value.number - 1d;
#else
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.bits = Detail.FloatingPoint.doubleOne | Detail.FloatingPoint.doubleMantissaMask & random.Next64();
			return value.number - 1d;
#endif
#endif
		}

		#endregion

		#region Unit Open/Closed (0, 1]

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (0, 1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range (0, 1] with
		/// perfect distribution across 2^23 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next32()"/> once on the supplied random engine.</para>
		/// </remarks>
		public static float FloatOC(this IRandom random)
		{
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & (random.Next32());
			return 2f - value.number;
		}

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (0, 1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range (0, 1] with
		/// perfect distribution across 2^52 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next64()"/> once on the supplied random engine.</para>
		/// </remarks>
		public static double DoubleOC(this IRandom random)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & upper;
			return 2d - value.number;
#else
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.bits = Detail.FloatingPoint.doubleOne | Detail.FloatingPoint.doubleMantissaMask & random.Next64();
			return 2d - value.number;
#endif
		}

		#endregion

		#region Unit Closed/Closed [0, 1]

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [0, 1] with
		/// perfect distribution across 2^23 + 1 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next32()"/> once on the supplied
		/// random engine.  Only one in 2048 calls will also require a call to <see cref="RandomInteger.RangeCO(IRandom, uint)"/>,
		/// which will involve one or more addtional calls to <see cref="IBitGenerator.Next32()"/> (on average rougly two calls will be made).</para>
		/// </remarks>
		public static float FloatCC(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var n = random.ClosedRange(0x00800000U);
			return (n != 0x00800000U) ? (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)n << 29))) - 1df : 1df;
#else
			// With a closed float, there are 2^23 + 1 possibilities.  A half open range contains only 2^23 possibilities,
			// with 1d having a 0 probability, and is very efficient to generate.  If a second random check were performed
			// that only had a 1 in 2^23 + 1 chance of passing, and on passing resulted in a value of 1d being returned
			// instead of the originally generated number, then all values would have exactly the correct target probability.
			//
			// To achieve this while still avoiding that second random check in most cases, the excess 9 bits generated by
			// a call to Next32() are used to perform part of that second random check.  This check has a 2^9 - 1 in 2^9
			// chance of failing, in which case the original number is returned.  On that rare 1 in 512 case that it passes,
			// then another random check is performed which has a 2^9 in 2^23 + 1 chance of passing.  If that also passes
			// then 1 is returned; otherwise, the standard float value is computed and returned.  The effect is that this
			// pair of secondary random checks combined has the requisite 1 in 2^23 + 1 chance of passing, but over 99.80%
			// of the time only one call to Next32() is ever executed.  In the remaining few cases, on average about two
			// additional calls will be required, or one call and some integer multiplication/division/remainder, depending
			// on how RandomRange.RangeCO() is implemented.

			uint n = random.Next32();
			if (n >= 0xFF800000U && random.RangeCO(0x00800001U) < 0x00000200U)
			{
				return 1f;
			}
			else
			{
				Detail.FloatingPoint.BitwiseFloat value;
				value.number = 0f;
				value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & (n);
				return value.number - 1f;
			}
#endif
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [0, 1] with
		/// perfect distribution across 2^52 + 1 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next64()"/> once on the supplied
		/// random engine.  Only one in 4096 calls will also require a call to <see cref="RandomInteger.RangeCO(IRandom, ulong)"/>,
		/// which will involve one or more addtional calls to <see cref="IBitGenerator.Next64()"/> (on average rougly two calls will be made).</para>
		/// </remarks>
		public static double DoubleCC(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var n = random.ClosedRange(0x0010000000000000UL);
			return (n != 0x0010000000000000UL) ? System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & n)) - 1d : 1d;
#else
			// With a closed double, there are 2^52 + 1 possibilities.  A half open range contains only 2^52 possibilities,
			// with 1d having a 0 probability, and is very efficient to generate.  If a second random check were performed
			// that only had a 1 in 2^52 + 1 chance of passing, and on passing resulted in a value of 1d being returned
			// instead of the originally generated number, then all values would have exactly the correct target probability.
			//
			// To achieve this while still avoiding that second random check in most cases, the excess 12 bits generated by
			// a call to Next64() are used to perform part of that second random check.  This check has a 2^12 - 1 in 2^12
			// chance of failing, in which case the original number is returned.  On that rare 1 in 4096 case that it passes,
			// then another random check is performed which has a 2^12 in 2^52 + 1 chance of passing.  If that also passes
			// then 1 is returned; otherwise, the standard double value is computed and returned.  The effect is that this
			// pair of secondary random checks combined has the requisite 1 in 2^52 + 1 chance of passing, but over 99.97%
			// of the time only one call to Next64() is ever executed.  In the remaining few cases, on average about two
			// additional calls will be required, or one call and some integer multiplication/division/remainder, depending
			// on how RandomRange.RangeCO() is implemented.

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (upper >= 0xFFF00000U && random.RangeCO(0x0010000000000001UL) < 0x00001000UL)
			{
				return 1d;
			}
			else
			{
				Detail.FloatingPoint.BitwiseDouble value;
				value.number = 0d;
				value.lowerBits = lower;
				value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & upper;
				return value.number - 1d;
			}
#else
			ulong n = random.Next64();
			if (n >= 0xFFF0000000000000UL && random.RangeCO(0x0010000000000001UL) < 0x00001000UL)
			{
				return 1d;
			}
			else
			{
				Detail.FloatingPoint.BitwiseDouble value;
				value.number = 0d;
				value.bits = Detail.FloatingPoint.doubleOne | Detail.FloatingPoint.doubleMantissaMask & n;
				return value.number - 1d;
			}
#endif
#endif
		}

		#endregion

		#region Signed Range Open/Open (-1, +1)

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (-1, +1).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range (-1, +1) with
		/// perfect distribution across 2^23 - 1 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next32()"/> once on the supplied
		/// random engine.  Only one in 2^23 calls will require an additional call to <see cref="IBitGenerator.Next32()"/>, with the same
		/// chance for requiring indefinitely more calls.</para>
		/// </remarks>
		public static float SignedFloatOO(this IRandom random)
		{
			uint n;
			do
			{
				n = random.Next32();
			} while (n <= 0x1FFU); // While the upper 23 bits are all zeros.
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & (n >> 9);
			return value.number - 3f;
		}

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (-1, +1).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range (-1, +1) with
		/// perfect distribution across 2^52 - 1 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next64()"/> once on the supplied
		/// random engine.  Only one in 2^52 calls will require an additional call to <see cref="IBitGenerator.Next64()"/>, with the same
		/// chance for requiring indefinitely more calls.</para>
		/// </remarks>
		public static double SignedDoubleOO(this IRandom random)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			do
			{
				random.Next64(out lower, out upper);
			} while (upper == 0U && lower <= 0xFFFU); // While the upper 52 bits are all zeros.

			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = (lower >> 12) | (upper << 20);
			value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & (upper >> 12);
			return value.number - 1d;
#else
			ulong n;
			do
			{
				n = random.Next64();
			} while (n <= 0xFFFUL); // While the upper 52 bits are all zeros.
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.bits = Detail.FloatingPoint.doubleTwo | Detail.FloatingPoint.doubleMantissaMask & (n >> 12);
			return value.number - 3d;
#endif
		}

		#endregion

		#region Signed Range Closed/Open [-1, +1)

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [-1, +1).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [-1, +1) with
		/// perfect distribution across 2^23 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next32()"/> once on the supplied random engine.</para>
		/// </remarks>
		public static float SignedFloatCO(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			return (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)random.Next32() << 29))) - 1df;
#else
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & (random.Next32());
			return value.number - 3f;
#endif
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [-1, +1).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [-1, +1) with
		/// perfect distribution across 2^52 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next64()"/> once on the supplied random engine.</para>
		/// </remarks>
		public static double SignedDoubleCO(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			return System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64())) - 1d;
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & upper;
			return value.number - 1d;
#else
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.bits = Detail.FloatingPoint.doubleTwo | Detail.FloatingPoint.doubleMantissaMask & random.Next64();
			return value.number - 3d;
#endif
#endif
		}

		#endregion

		#region Signed Range Open/Closed (-1, +1]

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (-1, +1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range (-1, +1] with
		/// perfect distribution across 2^23 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next32()"/> once on the supplied random engine.</para>
		/// </remarks>
		public static float SignedFloatOC(this IRandom random)
		{
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & (random.Next32());
			return 3f - value.number;
		}

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (-1, +1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range (-1, +1] with
		/// perfect distribution across 2^52 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next64()"/> once on the supplied random engine.</para>
		/// </remarks>
		public static double SignedDoubleOC(this IRandom random)
		{
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & upper;
			return 2d - value.number;
#else
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.bits = Detail.FloatingPoint.doubleTwo | Detail.FloatingPoint.doubleMantissaMask & random.Next64();
			return 3d - value.number;
#endif
		}

		#endregion

		#region Signed Range Closed/Closed [-1, +1]

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [-1, +1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [-1, +1] with
		/// perfect distribution across 2^23 + 1 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next32()"/> once on the supplied
		/// random engine.  Only one in 2048 calls will also require a call to <see cref="RandomInteger.RangeCO(IRandom, uint)"/>,
		/// which will involve one or more addtional calls to <see cref="IBitGenerator.Next32()"/> (on average rougly two calls will be made).</para>
		/// </remarks>
		public static float SignedFloatCC(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var n = random.ClosedRange(0x00800000U);
			return (n != 0x00800000U) ? (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)n << 29))) - 1df : 1df;
#else
			// With a closed float, there are 2^23 + 1 possibilities.  A half open range contains only 2^23 possibilities,
			// with 1d having a 0 probability, and is very efficient to generate.  If a second random check were performed
			// that only had a 1 in 2^23 + 1 chance of passing, and on passing resulted in a value of 1d being returned
			// instead of the originally generated number, then all values would have exactly the correct target probability.
			//
			// To achieve this while still avoiding that second random check in most cases, the excess 9 bits generated by
			// a call to Next32() are used to perform part of that second random check.  This check has a 2^9 - 1 in 2^9
			// chance of failing, in which case the original number is returned.  On that rare 1 in 512 case that it passes,
			// then another random check is performed which has a 2^9 in 2^23 + 1 chance of passing.  If that also passes
			// then 1 is returned; otherwise, the standard float value is computed and returned.  The effect is that this
			// pair of secondary random checks combined has the requisite 1 in 2^23 + 1 chance of passing, but over 99.80%
			// of the time only one call to Next32() is ever executed.  In the remaining few cases, on average about two
			// additional calls will be required, or one call and some integer multiplication/division/remainder, depending
			// on how RandomRange.RangeCO() is implemented.

			uint n = random.Next32();
			if (n >= 0xFF800000U && random.RangeCO(0x00800001U) < 0x00000200U)
			{
				return 1f;
			}
			else
			{
				Detail.FloatingPoint.BitwiseFloat value;
				value.number = 0f;
				value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & (n);
				return value.number - 3f;
			}
#endif
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [-1, +1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [-1, +1] with
		/// perfect distribution across 2^52 + 1 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next64()"/> once on the supplied
		/// random engine.  Only one in 4096 calls will also require a call to <see cref="RandomInteger.RangeCO(IRandom, ulong)"/>,
		/// which will involve one or more addtional calls to <see cref="IBitGenerator.Next64()"/> (on average rougly two calls will be made).</para>
		/// </remarks>
		public static double SignedDoubleCC(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var n = random.ClosedRange(0x0010000000000000UL);
			return (n != 0x0010000000000000UL) ? System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & n)) - 1d : 1d;
#else
			// With a closed double, there are 2^52 + 1 possibilities.  A half open range contains only 2^52 possibilities,
			// with 1d having a 0 probability, and is very efficient to generate.  If a second random check were performed
			// that only had a 1 in 2^52 + 1 chance of passing, and on passing resulted in a value of 1d being returned
			// instead of the originally generated number, then all values would have exactly the correct target probability.
			//
			// To achieve this while still avoiding that second random check in most cases, the excess 12 bits generated by
			// a call to Next64() are used to perform part of that second random check.  This check has a 2^12 - 1 in 2^12
			// chance of failing, in which case the original number is returned.  On that rare 1 in 4096 case that it passes,
			// then another random check is performed which has a 2^12 in 2^52 + 1 chance of passing.  If that also passes
			// then 1 is returned; otherwise, the standard double value is computed and returned.  The effect is that this
			// pair of secondary random checks combined has the requisite 1 in 2^52 + 1 chance of passing, but over 99.97%
			// of the time only one call to Next64() is ever executed.  In the remaining few cases, on average about two
			// additional calls will be required, or one call and some integer multiplication/division/remainder, depending
			// on how RandomRange.RangeCO() is implemented.

#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			if (upper >= 0xFFF00000U && random.RangeCO(0x0010000000000001UL) < 0x00001000UL)
			{
				return 1d;
			}
			else
			{
				Detail.FloatingPoint.BitwiseDouble value;
				value.number = 0d;
				value.lowerBits = lower;
				value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & upper;
				return value.number - 1d;
			}
#else
			ulong n = random.Next64();
			if (n >= 0xFFF0000000000000UL && random.RangeCO(0x0010000000000001UL) < 0x00001000UL)
			{
				return 1d;
			}
			else
			{
				Detail.FloatingPoint.BitwiseDouble value;
				value.number = 0d;
				value.bits = Detail.FloatingPoint.doubleTwo | Detail.FloatingPoint.doubleMantissaMask & n;
				return value.number - 3d;
			}
#endif
#endif
		}

		#endregion

		#region Specialized Range Closed/Open [1, 2)

		/// <summary>
		/// Returns a random floating point number greater than or equal to one and strictly less than two.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [1, 2).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [0, 1) with
		/// perfect distribution across 2^23 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next32()"/> once on the supplied random engine.</para>
		/// <para>Given the implementation details, this function is slightly faster than the other unit ranges.</para>
		/// </remarks>
		public static float FloatC1O2(this IRandom random)
		{
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & (random.Next32());
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to one and strictly less than two.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [1, 2).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [1, 2) with
		/// perfect distribution across 2^52 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next64()"/> once on the supplied random engine.</para>
		/// <para>Given the implementation details, this function is slightly faster than the other unit ranges.</para>
		/// </remarks>
		public static double DoubleC1O2(this IRandom random)
		{
#if MAKEITRANDOM_BACK_C1O2MPAT_V0_1
			return System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64())) - 1d;
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & upper;
			return value.number;
#else
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.bits = Detail.FloatingPoint.doubleOne | Detail.FloatingPoint.doubleMantissaMask & random.Next64();
			return value.number;
#endif
#endif
		}

		#endregion

		#region Specialized Range Closed/Open [2, 4)

		/// <summary>
		/// Returns a random floating point number greater than or equal to two and strictly less than four.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [2, 4).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [0, 1) with
		/// perfect distribution across 2^23 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next32()"/> once on the supplied random engine.</para>
		/// <para>Given the implementation details, this function is a quick way to generate a number with a range of 2.</para>
		/// </remarks>
		public static float FloatC2O4(this IRandom random)
		{
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatTwo | Detail.FloatingPoint.floatMantissaMask & (random.Next32());
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to two and strictly less than four.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [2, 4).</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [1, 2) with
		/// perfect distribution across 2^52 unique 64-bit double values which are precisely equidistant from each other in sequence.</para>
		/// <para>This function will only ever need to call <see cref="IBitGenerator.Next64()"/> once on the supplied random engine.</para>
		/// <para>Given the implementation details, this function is a quick way to generate a number with a range of 2.</para>
		/// </remarks>
		public static double DoubleC2O4(this IRandom random)
		{
#if MAKEITRANDOM_BACK_C2O4MPAT_V0_1
			return System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64())) - 1d;
#else
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			uint lower, upper;
			random.Next64(out lower, out upper);
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = Detail.FloatingPoint.doubleOneUpper | Detail.FloatingPoint.doubleMantissaMaskUpper & upper;
			return value.number;
#else
			Detail.FloatingPoint.BitwiseDouble value;
			value.number = 0d;
			value.bits = Detail.FloatingPoint.doubleTwo | Detail.FloatingPoint.doubleMantissaMask & random.Next64();
			return value.number;
#endif
#endif
		}

		#endregion

		#region Range Open/Open (min, max)

		/// <summary>
		/// Returns a random float strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatOO(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatOO(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleOO(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleOO(IRandom)"/>.</remarks>
		public static double RangeOO(this IRandom random, double upperExclusive)
		{
			return upperExclusive * random.DoubleOO();
		}

		#endregion

		#region Range Closed/Open [min, max)

		/// <summary>
		/// Returns a random float greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatCO(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatCO(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleCO(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleCO(IRandom)"/>.</remarks>
		public static double RangeCO(this IRandom random, double upperExclusive)
		{
			return upperExclusive * random.DoubleCO();
		}

		#endregion

		#region Range Open/Closed (min, max]

		/// <summary>
		/// Returns a random float strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatOC(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatOC(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleOC(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleOC(IRandom)"/>.</remarks>
		public static double RangeOC(this IRandom random, double upperInclusive)
		{
			return upperInclusive * random.DoubleOC();
		}

		#endregion

		#region Range Closed/Closed [min, max]

		/// <summary>
		/// Returns a random float greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The exclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatCC(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatCC(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleCC(IRandom)"/>.</remarks>
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
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleCC(IRandom)"/>.</remarks>
		public static double RangeCC(this IRandom random, double upperInclusive)
		{
			return upperInclusive * random.DoubleCC();
		}

		#endregion

		#region Precise Open/Open (0, 1)

		/// <summary>
		/// Returns a random floating point number greater than zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (0, 1).</returns>
		/// <remarks>
		/// <para>Unlike <see cref="FloatOO(IRandom)"/>, this function generates values that are not necessarily
		/// equidistant from each other.  Near zero, distinct values are more densely packed, but each individual
		/// value has less probability of occuring on average when compared to values closer to one.  The overall
		/// effect is still that values are uniformly distributed between zero and one, but absolute precisions is
		/// higher for values closer to zero.</para>
		/// </remarks>
		/// <seealso cref="FloatOO(IRandom)"/>
		public static float PreciseFloatOO(this IRandom random)
		{
			uint n;
			do
			{
				n = random.Next32();
			} while (n > _maxUnsignedFixed32LessThanFloatOne || n == 0U);
			Detail.FloatingPoint.BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= _floatExponent32;
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number greater than zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (0, 1).</returns>
		/// <remarks>
		/// <para>Unlike <see cref="DoubleOO(IRandom)"/>, this function generates values that are not necessarily
		/// equidistant from each other.  Near zero, distinct values are more densely packed, but each individual
		/// value has less probability of occuring on average when compared to values closer to one.  The overall
		/// effect is still that values are uniformly distributed between zero and one, but absolute precisions is
		/// higher for values closer to zero.</para>
		/// </remarks>
		/// <seealso cref="DoubleOO(IRandom)"/>
		public static double PreciseDoubleOO(this IRandom random)
		{
			ulong n;
			do
			{
				n = random.Next64();
			} while (n > _maxUnsignedFixed64LessThanFloatOne || n == 0UL);
			Detail.FloatingPoint.BitwiseDouble value;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= _doubleExponent64;
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= _doubleExponent64;
#endif
			return value.number;
		}

		#endregion

		#region Precise Closed/Open [0, 1)

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1).</returns>
		/// <remarks>
		/// <para>Unlike <see cref="FloatCO(IRandom)"/>, this function generates values that are not necessarily
		/// equidistant from each other.  Near zero, distinct values are more densely packed, but each individual
		/// value has less probability of occuring on average when compared to values closer to one.  The overall
		/// effect is still that values are uniformly distributed between zero and one, but absolute precisions is
		/// higher for values closer to zero.</para>
		/// </remarks>
		/// <seealso cref="FloatCO(IRandom)"/>
		public static float PreciseFloatCO(this IRandom random)
		{
			uint n;
			do
			{
				n = random.Next32();
			} while (n > _maxUnsignedFixed32LessThanFloatOne);
			if (n == 0U) return 0f;
			Detail.FloatingPoint.BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= _floatExponent32;
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and strictly less than one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1).</returns>
		/// <remarks>
		/// <para>Unlike <see cref="DoubleCO(IRandom)"/>, this function generates values that are not necessarily
		/// equidistant from each other.  Near zero, distinct values are more densely packed, but each individual
		/// value has less probability of occuring on average when compared to values closer to one.  The overall
		/// effect is still that values are uniformly distributed between zero and one, but absolute precisions is
		/// higher for values closer to zero.</para>
		/// </remarks>
		/// <seealso cref="DoubleCO(IRandom)"/>
		public static double PreciseDoubleCO(this IRandom random)
		{
			ulong n;
			do
			{
				n = random.Next64();
			} while (n > _maxUnsignedFixed64LessThanFloatOne);
			if (n == 0UL) return 0d;
			Detail.FloatingPoint.BitwiseDouble value;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= _doubleExponent64;
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= _doubleExponent64;
#endif
			return value.number;
		}

		#endregion

		#region Precise Open/Closed (0, 1]

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (0, 1].</returns>
		/// <remarks>
		/// <para>Unlike <see cref="FloatOC(IRandom)"/>, this function generates values that are not necessarily
		/// equidistant from each other.  Near zero, distinct values are more densely packed, but each individual
		/// value has less probability of occuring on average when compared to values closer to one.  The overall
		/// effect is still that values are uniformly distributed between zero and one, but absolute precisions is
		/// higher for values closer to zero.</para>
		/// </remarks>
		/// <seealso cref="FloatOC(IRandom)"/>
		public static float PreciseFloatOC(this IRandom random)
		{
			uint n = random.Next32();
			if (n == 0U) return 1f;
			Detail.FloatingPoint.BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= _floatExponent32;
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number strictly greater than zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (0, 1].</returns>
		/// <remarks>
		/// <para>Unlike <see cref="DoubleOC(IRandom)"/>, this function generates values that are not necessarily
		/// equidistant from each other.  Near zero, distinct values are more densely packed, but each individual
		/// value has less probability of occuring on average when compared to values closer to one.  The overall
		/// effect is still that values are uniformly distributed between zero and one, but absolute precisions is
		/// higher for values closer to zero.</para>
		/// </remarks>
		/// <seealso cref="DoubleOC(IRandom)"/>
		public static double PreciseDoubleOC(this IRandom random)
		{
			ulong n = random.Next64();
			if (n == 0UL) return 1d;
			Detail.FloatingPoint.BitwiseDouble value;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= _doubleExponent64;
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= _doubleExponent64;
#endif
			return value.number;
		}

		#endregion

		#region Precise Closed/Closed [0, 1]

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1].</returns>
		/// <remarks>
		/// <para>Unlike <see cref="FloatCC(IRandom)"/>, this function generates values that are not necessarily
		/// equidistant from each other.  Near zero, distinct values are more densely packed, but each individual
		/// value has less probability of occuring on average when compared to values closer to one.  The overall
		/// effect is still that values are uniformly distributed between zero and one, but absolute precisions is
		/// higher for values closer to zero.</para>
		/// </remarks>
		/// <seealso cref="FloatCC(IRandom)"/>
		public static float PreciseFloatCC(this IRandom random)
		{
			// The first check has 1 in 2^32 chance of passing.  Combined with a check with probability 2^32 in 2^32 + 1 chance
			// of passing, the cummulative probability will be 1 in 2^32 + 1.  This is admittedly costly because it's adding an
			// entire extra random check for every single number, but it's necessary to keep this function perfectly in line with
			// the other precise float functions, and there are no bits to spare like with FloatCC().
			if (random.Probability(1U) && random.Probability(0x0000000100000000UL, 0x0000000100000001UL)) return 1f;
			uint n = random.Next32();
			if (n == 0U) return 0f;
			Detail.FloatingPoint.BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= _floatExponent32;
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and less than or equal to one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1].</returns>
		/// <remarks>
		/// <para>Unlike <see cref="DoubleCC(IRandom)"/>, this function generates values that are not necessarily
		/// equidistant from each other.  Near zero, distinct values are more densely packed, but each individual
		/// value has less probability of occuring on average when compared to values closer to one.  The overall
		/// effect is still that values are uniformly distributed between zero and one, but absolute precisions is
		/// higher for values closer to zero.</para>
		/// </remarks>
		/// <seealso cref="DoubleCC(IRandom)"/>
		public static double PreciseDoubleCC(this IRandom random)
		{
			// The first check has 1 in 2^63 chance of passing.  Combined with a check with probability 2^63 in 2^64 + 1 chance
			// of passing, the cummulative probability will be 1 in 2^64 + 1.  This is admittedly costly because it's adding an
			// entire extra random check for every single number, but it's necessary to keep this function perfectly in line with
			// the other precise float functions, and there are no bits to spare like with DoubleCC().  Note that the second
			// check needs to be split into two checks based on factorization, since it is beyond the 64-bit range.
			if (random.Probability(2UL) && random.Probability(0x0000200000000000UL, 67280421310721UL) && random.Probability(0x00040000U, 274177U)) return 1d;
			ulong n = random.Next64();
			if (n == 0UL) return 0d;
			Detail.FloatingPoint.BitwiseDouble value;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= _doubleExponent64;
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= _doubleExponent64;
#endif
			return value.number;
		}

		#endregion

		#region Precise Signed Open/Open (-1, +1)

		/// <summary>
		/// Returns a random floating point number strictly greater than negative one and strictly less than positive one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (-1, +1).</returns>
		/// <remarks>
		/// <para>This function generates values that are not necessarily equidistant from each other.  Near zero, distinct
		/// values are more densely packed, but each individual value has less probability of occuring on average when
		/// compared to values closer to one.  The overall effect is still that values are uniformly distributed between
		/// negative one and positive one, but absolute precisions is higher for values closer to zero.</para>
		/// <para><note type="note">When a value of zero is generated, it has a 50% chance of being a negative zero.</note></para>
		/// </remarks>
		public static float PreciseSignedFloatOO(this IRandom random)
		{
			int n;
			do
			{
				n = (int)random.Next32();
			} while (n > _maxSignedFixed32LessThanFloatOne || n < -_maxSignedFixed32LessThanFloatOne);
			if (n == 0) return random.Chance() ? +0f : -0f;
			Detail.FloatingPoint.BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= _floatExponent31;
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number strictly greater than negative one and strictly less than positive one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (-1, +1).</returns>
		/// <remarks>
		/// <para>This function generates values that are not necessarily equidistant from each other.  Near zero, distinct
		/// values are more densely packed, but each individual value has less probability of occuring on average when
		/// compared to values closer to one.  The overall effect is still that values are uniformly distributed between
		/// negative one and positive one, but absolute precisions is higher for values closer to zero.</para>
		/// <para><note type="note">When a value of zero is generated, it has a 50% chance of being a negative zero.</note></para>
		/// </remarks>
		public static double PreciseSignedDoubleOO(this IRandom random)
		{
			long n;
			do
			{
				n = (long)random.Next64();
			} while (n > _maxSignedFixed64LessThanFloatOne && n < -_maxSignedFixed64LessThanFloatOne);
			if (n == 0L) return random.Chance() ? +0d : -0d;
			Detail.FloatingPoint.BitwiseDouble value;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= _doubleExponent63;
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= _doubleExponent63;
#endif
			return value.number;
		}

		#endregion

		#region Precise Signed Closed/Open [-1, +1)

		/// <summary>
		/// Returns a random floating point number greater than or equal to negative one and strictly less than positive one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [-1, +1).</returns>
		/// <remarks>
		/// <para>This function generates values that are not necessarily equidistant from each other.  Near zero, distinct
		/// values are more densely packed, but each individual value has less probability of occuring on average when
		/// compared to values closer to one.  The overall effect is still that values are uniformly distributed between
		/// negative one and positive one, but absolute precisions is higher for values closer to zero.</para>
		/// <para><note type="note">When a value of zero is generated, it has a 50% chance of being a negative zero.</note></para>
		/// </remarks>
		public static float PreciseSignedFloatCO(this IRandom random)
		{
			int n;
			do
			{
				n = (int)random.Next32();
			} while (n > _maxSignedFixed32LessThanFloatOne);
			if (n == 0) return random.Chance() ? +0f : -0f;
			Detail.FloatingPoint.BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= _floatExponent31;
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to negative one and strictly less than positive one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [-1, +1).</returns>
		/// <remarks>
		/// <para>This function generates values that are not necessarily equidistant from each other.  Near zero, distinct
		/// values are more densely packed, but each individual value has less probability of occuring on average when
		/// compared to values closer to one.  The overall effect is still that values are uniformly distributed between
		/// negative one and positive one, but absolute precisions is higher for values closer to zero.</para>
		/// <para><note type="note">When a value of zero is generated, it has a 50% chance of being a negative zero.</note></para>
		/// </remarks>
		public static double PreciseSignedDoubleCO(this IRandom random)
		{
			long n;
			do
			{
				n = (long)random.Next64();
			} while (n > _maxSignedFixed64LessThanFloatOne);
			if (n == 0L) return random.Chance() ? +0d : -0d;
			Detail.FloatingPoint.BitwiseDouble value;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= _doubleExponent63;
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= _doubleExponent63;
#endif
			return value.number;
		}

		#endregion

		#region Precise Signed Open/Closed (-1, +1]

		/// <summary>
		/// Returns a random floating point number strictly greater than negative one and less than or equal to positive one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (-1, +1].</returns>
		/// <remarks>
		/// <para>This function generates values that are not necessarily equidistant from each other.  Near zero, distinct
		/// values are more densely packed, but each individual value has less probability of occuring on average when
		/// compared to values closer to one.  The overall effect is still that values are uniformly distributed between
		/// negative one and positive one, but absolute precisions is higher for values closer to zero.</para>
		/// <para><note type="note">When a value of zero is generated, it has a 50% chance of being a negative zero.</note></para>
		/// </remarks>
		public static float PreciseSignedFloatOC(this IRandom random)
		{
			int n;
			do
			{
				n = (int)random.Next32();
			} while (n > _maxSignedFixed32LessThanFloatOne);
			if (n == 0) return random.Chance() ? -0f : +0f;
			Detail.FloatingPoint.BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= _floatExponent31;
			return -value.number;
		}

		/// <summary>
		/// Returns a random floating point number strictly greater than negative one and less than or equal to positive one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range (-1, +1].</returns>
		/// <remarks>
		/// <para>This function generates values that are not necessarily equidistant from each other.  Near zero, distinct
		/// values are more densely packed, but each individual value has less probability of occuring on average when
		/// compared to values closer to one.  The overall effect is still that values are uniformly distributed between
		/// negative one and positive one, but absolute precisions is higher for values closer to zero.</para>
		/// <para><note type="note">When a value of zero is generated, it has a 50% chance of being a negative zero.</note></para>
		/// </remarks>
		public static double PreciseSignedDoubleOC(this IRandom random)
		{
			long n;
			do
			{
				n = (long)random.Next64();
			} while (n > _maxSignedFixed64LessThanFloatOne);
			if (n == 0L) return random.Chance() ? -0d : +0d;
			Detail.FloatingPoint.BitwiseDouble value;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= _doubleExponent63;
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= _doubleExponent63;
#endif
			return -value.number;
		}

		#endregion

		#region Precise Signed Closed/Closed [-1, +1]

		/// <summary>
		/// Returns a random floating point number greater than or equal to negative one and less than or equal to positive one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [-1, +1].</returns>
		/// <remarks>
		/// <para>This function generates values that are not necessarily equidistant from each other.  Near zero, distinct
		/// values are more densely packed, but each individual value has less probability of occuring on average when
		/// compared to values closer to one.  The overall effect is still that values are uniformly distributed between
		/// negative one and positive one, but absolute precisions is higher for values closer to zero.</para>
		/// <para><note type="note">When a value of zero is generated, it has a 50% chance of being a negative zero.</note></para>
		/// </remarks>
		public static float PreciseSignedFloatCC(this IRandom random)
		{
			// The first check has 1 in 2^32 chance of passing.  Combined with a check with probability 2^32 in 2^32 + 1 chance
			// of passing, the cummulative probability will be 1 in 2^32 + 1.  This is admittedly costly because it's adding an
			// entire extra random check for every single number, but it's necessary to keep this function perfectly in line with
			// the other precise signed float functions, and there are no bits to spare like with FloatCC().
			if (random.Probability(1U) && random.Probability(0x0000000100000000UL, 0x0000000100000001UL)) return +1f;
			int n = (int)random.Next32();
			if (n == 0) return random.Chance() ? +0f : -0f;
			Detail.FloatingPoint.BitwiseFloat value;
			value.bits = 0U;
			value.number = n;
			value.bits -= _floatExponent31;
			return value.number;
		}

		/// <summary>
		/// Returns a random floating point number greater than or equal to negative one and less than or equal to positive one,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [-1, +1].</returns>
		/// <remarks>
		/// <para>This function generates values that are not necessarily equidistant from each other.  Near zero, distinct
		/// values are more densely packed, but each individual value has less probability of occuring on average when
		/// compared to values closer to one.  The overall effect is still that values are uniformly distributed between
		/// negative one and positive one, but absolute precisions is higher for values closer to zero.</para>
		/// <para><note type="note">When a value of zero is generated, it has a 50% chance of being a negative zero.</note></para>
		/// </remarks>
		public static double PreciseSignedDoubleCC(this IRandom random)
		{
			// The first check has 1 in 2^63 chance of passing.  Combined with a check with probability 2^63 in 2^64 + 1 chance
			// of passing, the cummulative probability will be 1 in 2^64 + 1.  This is admittedly costly because it's adding an
			// entire extra random check for every single number, but it's necessary to keep this function perfectly in line with
			// the other precise signed float functions, and there are no bits to spare like with DoubleCC().  Note that the
			// second check needs to be split into two checks based on factorization, since it is beyond the 64-bit range.
			if (random.Probability(2UL) && random.Probability(0x0000200000000000UL, 67280421310721UL) && random.Probability(0x00040000U, 274177U)) return +1d;
			long n = (long)random.Next64();
			if (n == 0L) return random.Chance() ? +0d : -0d;
			Detail.FloatingPoint.BitwiseDouble value;
#if MAKEITRANDOM_OPTIMIZE_FOR_32BIT
			value.upperBits = 0U;
			value.number = n;
			value.upperBits -= _doubleExponent63;
#else
			value.bits = 0UL;
			value.number = n;
			value.bits -= _doubleExponent63;
#endif
			return value.number;
		}

		#endregion

		#region Precise Range Open/Open (min, max)

		/// <summary>
		/// Returns a random float strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatOO(IRandom)"/>.</remarks>
		public static float PreciseRangeOO(this IRandom random, float lowerExclusive, float upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * random.PreciseFloatOO() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatOO(IRandom)"/>.</remarks>
		public static float PreciseRangeOO(this IRandom random, float upperExclusive)
		{
			return upperExclusive * random.PreciseFloatOO();
		}

		/// <summary>
		/// Returns a random double strictly greater than <paramref name="lowerExclusive"/> and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range (<paramref name="lowerExclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleOO(IRandom)"/>.</remarks>
		public static double PreciseRangeOO(this IRandom random, double lowerExclusive, double upperExclusive)
		{
			return (upperExclusive - lowerExclusive) * random.PreciseDoubleOO() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range (0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleOO(IRandom)"/>.</remarks>
		public static double PreciseRangeOO(this IRandom random, double upperExclusive)
		{
			return upperExclusive * random.PreciseDoubleOO();
		}

		#endregion

		#region Precise Range Closed/Open [min, max)

		/// <summary>
		/// Returns a random float greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatCO(IRandom)"/>.</remarks>
		public static float PreciseRangeCO(this IRandom random, float lowerInclusive, float upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * random.PreciseFloatCO() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random float greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random float in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatCO(IRandom)"/>.</remarks>
		public static float PreciseRangeCO(this IRandom random, float upperExclusive)
		{
			return upperExclusive * random.PreciseFloatCO();
		}

		/// <summary>
		/// Returns a random double greater than or equal to <paramref name="lowerInclusive"/> and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The inclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range [<paramref name="lowerInclusive"/>, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleCO(IRandom)"/>.</remarks>
		public static double PreciseRangeCO(this IRandom random, double lowerInclusive, double upperExclusive)
		{
			return (upperExclusive - lowerInclusive) * random.PreciseDoubleCO() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and strictly less than <paramref name="upperExclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperExclusive">The exclusive upper bound of the custom range.  The generated number will be less than this value.</param>
		/// <returns>A random double in the range [0, <paramref name="upperExclusive"/>).</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleCO(IRandom)"/>.</remarks>
		public static double PreciseRangeCO(this IRandom random, double upperExclusive)
		{
			return upperExclusive * random.PreciseDoubleCO();
		}

		#endregion

		#region Precise Range Open/Closed (min, max]

		/// <summary>
		/// Returns a random float strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatOC(IRandom)"/>.</remarks>
		public static float PreciseRangeOC(this IRandom random, float lowerExclusive, float upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * random.PreciseFloatOC() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random float strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatOC(IRandom)"/>.</remarks>
		public static float PreciseRangeOC(this IRandom random, float upperInclusive)
		{
			return upperInclusive * random.PreciseFloatOC();
		}

		/// <summary>
		/// Returns a random double strictly greater than <paramref name="lowerExclusive"/> and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerExclusive">The exclusive lower bound of the custom range.  The generated number will be greater than this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range (<paramref name="lowerExclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleOC(IRandom)"/>.</remarks>
		public static double PreciseRangeOC(this IRandom random, double lowerExclusive, double upperInclusive)
		{
			return (upperInclusive - lowerExclusive) * random.PreciseDoubleOC() + lowerExclusive;
		}

		/// <summary>
		/// Returns a random double strictly greater than zero and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range (0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleOC(IRandom)"/>.</remarks>
		public static double PreciseRangeOC(this IRandom random, double upperInclusive)
		{
			return upperInclusive * random.PreciseDoubleOC();
		}

		#endregion

		#region Precise Range Closed/Closed [min, max]

		/// <summary>
		/// Returns a random float greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The exclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatCC(IRandom)"/>.</remarks>
		public static float PreciseRangeCC(this IRandom random, float lowerInclusive, float upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * random.PreciseFloatCC() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random float greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random float in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.FloatCC(IRandom)"/>.</remarks>
		public static float PreciseRangeCC(this IRandom random, float upperInclusive)
		{
			return upperInclusive * random.PreciseFloatCC();
		}

		/// <summary>
		/// Returns a random double greater than or equal to <paramref name="lowerInclusive"/> and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="lowerInclusive">The exclusive lower bound of the custom range.  The generated number will be greater than or equal to this value.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range [<paramref name="lowerInclusive"/>, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleCC(IRandom)"/>.</remarks>
		public static double PreciseRangeCC(this IRandom random, double lowerInclusive, double upperInclusive)
		{
			return (upperInclusive - lowerInclusive) * random.PreciseDoubleCC() + lowerInclusive;
		}

		/// <summary>
		/// Returns a random double greater than or equal to zero and less than or equal to <paramref name="upperInclusive"/>,
		/// with no precision loss as numbers get closer to zero.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="upperInclusive">The inclusive upper bound of the custom range.  The generated number will be less than or equal to this value.</param>
		/// <returns>A random double in the range [0, <paramref name="upperInclusive"/>].</returns>
		/// <remarks>This function has time complexity equivalent to <see cref="RandomFloatingPoint.DoubleCC(IRandom)"/>.</remarks>
		public static double PreciseRangeCC(this IRandom random, double upperInclusive)
		{
			return upperInclusive * random.PreciseDoubleCC();
		}

		#endregion
	}
}
