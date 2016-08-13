/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System.Runtime.InteropServices;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random numbers between zero and one.
	/// </summary>
	public static class RandomUnit
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseFloat
		{
			[FieldOffset(0)]
			public uint bits;
			[FieldOffset(0)]
			public float number;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseDouble
		{
			[FieldOffset(0)]
			public ulong bits;
			[FieldOffset(0)]
			public double number;
		}

		#region Open

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
		public static float OpenFloatUnit(this IRandom random)
		{
			uint n;
			do
			{
				n = random.Next32() & 0x007FFFFFU;
			} while (n == 0U);

			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | n;
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
		public static double OpenDoubleUnit(this IRandom random)
		{
			ulong n;
			do
			{
				n = random.Next64() & 0x000FFFFFFFFFFFFFUL;
			} while (n == 0UL);

			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | n;
			return value.number - 1.0;
		}

		#endregion

		#region HalfOpen

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
		public static float HalfOpenFloatUnit(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			return (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)random.Next32() << 29))) - 1.0f;
#else
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & random.Next32();
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
		public static double HalfOpenDoubleUnit(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			return System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64())) - 1.0;
#else
			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64();
			return value.number - 1.0;
#endif
		}

		#endregion

		#region HalfClosed

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
		public static float HalfClosedFloatUnit(this IRandom random)
		{
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & random.Next32();
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
		public static double HalfClosedDoubleUnit(this IRandom random)
		{
			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64();
			return 2.0 - value.number;
		}

		#endregion

		#region Closed

		/// <summary>
		/// Returns a random floating point number greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1].</returns>
		/// <remarks>
		/// <para>Limited only by the quality of the underlying random engine used, this method generates floats in the range [0, 1] with
		/// perfect distribution across 2^23 + 1 unique 32-bit float values which are precisely equidistant from each other in sequence.</para>
		/// <para>The vast majority of the time, this function will only need to call <see cref="IBitGenerator.Next32()"/> once on the supplied
		/// random engine.  Only one in 2048 calls will also require a call to <see cref="RandomRange.HalfOpenRange(IRandom, uint)"/>,
		/// which will involve one or more addtional calls to <see cref="IBitGenerator.Next32()"/> (on average rougly two calls will be made).</para>
		/// </remarks>
		public static float ClosedFloatUnit(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var n = random.ClosedRange(0x00800000U);
			return (n != 0x00800000U) ? (float)System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & ((ulong)n << 29))) - 1.0f : 1.0f;
#else
			// With a closed float, there are 2^23 + 1 possibilities.  A half open range contains only 2^23 possibilities,
			// with 1.0 having a 0 probability, and is very efficient to generate.  If a second random check were performed
			// that had a 2^23 in 2^23 + 1 chance of passing, and on failure resulted in a value of 1.0 being returned
			// instead of the originally generated number, then all values would have exactly the correct target probability.

			// To achieve this while still avoiding that second random check in most cases, the excess 11 bits generated by
			// a call to Next32() are used to perform part of that second random check.  This check has a 2^11 - 1 in 2^11
			// chance of passing, in which case the original number is returned.  On that rare 1 in 2048 case that it fails,
			// then another random check is performed which has a 2^23 - 2^11 + 1 in 2^23 + 1 chance of passing.  If it still
			// passes, then the original number is still returned; otherwise, 1.0 is returned.  The effect is that this pair
			// of secondary random checks additively has the requisite 2^23 in 2^23 + 1 chance of passing, but over 99.95%
			// of the time only one call to Next32() is ever executed.  In the remaining few cases, on average about two
			// additional calls will be required, or one call and some integer multiplication/division/remainder, depending
			// on how RandomRange.HalfOpen() is implemented.

			uint n = random.Next32();
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & n;

			if ((n & 0xFF800000U) != 0xFF800000U)
			{
				return value.number - 1f;
			}
			else if (random.HalfOpenRange(0x00800001U) < 0x007FF801U)
			{
				return value.number - 1f;
			}
			else
			{
				return 1f;
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
		/// random engine.  Only one in 4096 calls will also require a call to <see cref="RandomRange.HalfOpenRange(IRandom, ulong)"/>,
		/// which will involve one or more addtional calls to <see cref="IBitGenerator.Next64()"/> (on average rougly two calls will be made).</para>
		/// </remarks>
		public static double ClosedDoubleUnit(this IRandom random)
		{
#if MAKEITRANDOM_BACK_COMPAT_V0_1
			var n = random.ClosedRange(0x0010000000000000UL);
			return (n != 0x0010000000000000UL) ? System.BitConverter.Int64BitsToDouble((long)(0x3FF0000000000000UL | 0x000FFFFFE0000000UL & n)) - 1.0 : 1.0;
#else
			// With a closed double, there are 2^52 + 1 possibilities.  A half open range contains only 2^52 possibilities,
			// with 1.0 having a 0 probability, and is very efficient to generate.  If a second random check were performed
			// that had a 2^52 in 2^52 + 1 chance of passing, and on failure resulted in a value of 1.0 being returned
			// instead of the originally generated number, then all values would have exactly the correct target probability.

			// To achieve this while still avoiding that second random check in most cases, the excess 12 bits generated by
			// a call to Next64() are used to perform part of that second random check.  This check has a 2^12 - 1 in 2^12
			// chance of passing, in which case the original number is returned.  On that rare 1 in 4096 case that it fails,
			// then another random check is performed which has a 2^52 - 2^12 + 1 in 2^52 + 1 chance of passing.  If it still
			// passes, then the original number is still returned; otherwise, 1.0 is returned.  The effect is that this pair
			// of secondary random checks additively has the requisite 2^52 in 2^52 + 1 chance of passing, but over 99.97%
			// of the time only one call to Next64() is ever executed.  In the remaining few cases, on average about two
			// additional calls will be required, or one call and some integer multiplication/division/remainder, depending
			// on how RandomRange.HalfOpen() is implemented.

			ulong n = random.Next64();
			BitwiseDouble value;
			value.number = 0.0;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & n;

			if ((n & 0xFFF0000000000000UL) != 0xFFF0000000000000UL)
			{
				return value.number - 1.0;
			}
			else if (random.HalfOpenRange(0x0010000000000001UL) < 0x000FFFFFFFFFF001UL)
			{
				return value.number - 1.0;
			}
			else
			{
				return 1.0;
			}
#endif
		}

		#endregion
	}
}
