/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

#if (UNITY_64 || MAKEITRANDOM_64) && !MAKEITRANDOM_32
#define OPTIMIZE_FOR_64
#else
#define OPTIMIZE_FOR_32
#endif

using System.Runtime.InteropServices;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random numbers between zero and one.
	/// </summary>
	public static class RandomUnit
	{
		#region Private Helper Structs

		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseFloat
		{
			[FieldOffset(0)]
			public uint bits;
			[FieldOffset(0)]
			public float number;
		}

#if OPTIMIZE_FOR_32
		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseDouble
		{
			[FieldOffset(0)]
			public uint lowerBits;
			[FieldOffset(4)]
			public uint upperBits;
			[FieldOffset(0)]
			public double number;
		}
#else
		[StructLayout(LayoutKind.Explicit)]
		private struct BitwiseDouble
		{
			[FieldOffset(0)]
			public ulong bits;
			[FieldOffset(0)]
			public double number;
		}
#endif

		#endregion

		#region Private Generator Classes

		private class FloatOOGenerator : IFloatGenerator
		{
			private IRandom _random;
			public FloatOOGenerator(IRandom random) { _random = random; }
			public float Next() { return _random.FloatOO(); }
		}

		private class FloatCOGenerator : IFloatGenerator
		{
			private IRandom _random;
			public FloatCOGenerator(IRandom random) { _random = random; }
			public float Next() { return _random.FloatCO(); }
		}

		private class FloatOCGenerator : IFloatGenerator
		{
			private IRandom _random;
			public FloatOCGenerator(IRandom random) { _random = random; }
			public float Next() { return _random.FloatOC(); }
		}

		private class FloatCCGenerator : IFloatGenerator
		{
			private IRandom _random;
			public FloatCCGenerator(IRandom random) { _random = random; }
			public float Next() { return _random.FloatCC(); }
		}

		private class DoubleOOGenerator : IDoubleGenerator
		{
			private IRandom _random;
			public DoubleOOGenerator(IRandom random) { _random = random; }
			public double Next() { return _random.DoubleOO(); }
		}

		private class DoubleCOGenerator : IDoubleGenerator
		{
			private IRandom _random;
			public DoubleCOGenerator(IRandom random) { _random = random; }
			public double Next() { return _random.DoubleCO(); }
		}

		private class DoubleOCGenerator : IDoubleGenerator
		{
			private IRandom _random;
			public DoubleOCGenerator(IRandom random) { _random = random; }
			public double Next() { return _random.DoubleOC(); }
		}

		private class DoubleCCGenerator : IDoubleGenerator
		{
			private IRandom _random;
			public DoubleCCGenerator(IRandom random) { _random = random; }
			public double Next() { return _random.DoubleCC(); }
		}

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
				n = random.Next32() & 0x007FFFFFU;
			} while (n == 0U);

			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | n;
			return value.number - 1f;
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1).</returns>
		/// <seealso cref="RandomRange.FloatOO(IRandom)"/>
		public static IFloatGenerator MakeFloatOOGenerator(IRandom random)
		{
			return new FloatOOGenerator(random);
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
#if OPTIMIZE_FOR_32
			uint lower, upper;
			do
			{
				random.Next64(out lower, out upper);
				upper &= 0x000FFFFFU;
			} while ((lower | upper) == 0U);

			BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = 0x3FF00000U | upper;
			return value.number - 1d;
#else
			ulong n;
			do
			{
				n = random.Next64() & 0x000FFFFFFFFFFFFFUL;
			} while (n == 0UL);

			BitwiseDouble value;
			value.number = 0d;
			value.bits = 0x3FF0000000000000UL | n;
			return value.number - 1d;
#endif
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1).</returns>
		/// <seealso cref="RandomRange.DoubleOO(IRandom)"/>
		public static IDoubleGenerator MakeDoubleOOGenerator(IRandom random)
		{
			return new DoubleOOGenerator(random);
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
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & random.Next32();
			return value.number - 1f;
#endif
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1).</returns>
		/// <seealso cref="RandomRange.FloatCO(IRandom)"/>
		public static IFloatGenerator MakeFloatCOGenerator(IRandom random)
		{
			return new FloatCOGenerator(random);
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
#if OPTIMIZE_FOR_32
			uint lower, upper;
			random.Next64(out lower, out upper);
			BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = 0x3FF00000U | 0x000FFFFFU & upper;
			return value.number - 1d;
#else
			BitwiseDouble value;
			value.number = 0d;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64();
			return value.number - 1d;
#endif
#endif
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1).</returns>
		/// <seealso cref="RandomRange.DoubleCO(IRandom)"/>
		public static IDoubleGenerator MakeDoubleCOGenerator(IRandom random)
		{
			return new DoubleCOGenerator(random);
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
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & random.Next32();
			return 2f - value.number;
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1].</returns>
		/// <seealso cref="RandomRange.FloatOC(IRandom)"/>
		public static IFloatGenerator MakeFloatOCGenerator(IRandom random)
		{
			return new FloatOCGenerator(random);
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
#if OPTIMIZE_FOR_32
			uint lower, upper;
			random.Next64(out lower, out upper);
			BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = 0x3FF00000U | 0x000FFFFFU & upper;
			return 2d - value.number;
#else
			BitwiseDouble value;
			value.number = 0d;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & random.Next64();
			return 2d - value.number;
#endif
		}

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1].</returns>
		/// <seealso cref="RandomRange.DoubleOC(IRandom)"/>
		public static IDoubleGenerator MakeDoubleOCGenerator(IRandom random)
		{
			return new DoubleOCGenerator(random);
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
		/// random engine.  Only one in 2048 calls will also require a call to <see cref="RandomRange.RangeCO(IRandom, uint)"/>,
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
			// that had a 2^23 in 2^23 + 1 chance of passing, and on failure resulted in a value of 1d being returned
			// instead of the originally generated number, then all values would have exactly the correct target probability.

			// To achieve this while still avoiding that second random check in most cases, the excess 11 bits generated by
			// a call to Next32() are used to perform part of that second random check.  This check has a 2^11 - 1 in 2^11
			// chance of passing, in which case the original number is returned.  On that rare 1 in 2048 case that it fails,
			// then another random check is performed which has a 2^23 - 2^11 + 1 in 2^23 + 1 chance of passing.  If it still
			// passes, then the original number is still returned; otherwise, 1d is returned.  The effect is that this pair
			// of secondary random checks additively has the requisite 2^23 in 2^23 + 1 chance of passing, but over 99.95%
			// of the time only one call to Next32() is ever executed.  In the remaining few cases, on average about two
			// additional calls will be required, or one call and some integer multiplication/division/remainder, depending
			// on how RandomRange.HalfOpen() is implemented.

			uint n = random.Next32();
			BitwiseFloat value;
			value.number = 0f;
			value.bits = 0x3F800000U | 0x007FFFFFU & n;

			if ((n & 0xFF800000U) != 0xFF800000U || random.RangeCO(0x00800001U) < 0x007FF801U)
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
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1].</returns>
		/// <seealso cref="RandomRange.FloatCC(IRandom)"/>
		public static IFloatGenerator MakeFloatCCGenerator(IRandom random)
		{
			return new FloatCCGenerator(random);
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
		/// random engine.  Only one in 4096 calls will also require a call to <see cref="RandomRange.RangeCO(IRandom, ulong)"/>,
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
			// that had a 2^52 in 2^52 + 1 chance of passing, and on failure resulted in a value of 1d being returned
			// instead of the originally generated number, then all values would have exactly the correct target probability.

			// To achieve this while still avoiding that second random check in most cases, the excess 12 bits generated by
			// a call to Next64() are used to perform part of that second random check.  This check has a 2^12 - 1 in 2^12
			// chance of passing, in which case the original number is returned.  On that rare 1 in 4096 case that it fails,
			// then another random check is performed which has a 2^52 - 2^12 + 1 in 2^52 + 1 chance of passing.  If it still
			// passes, then the original number is still returned; otherwise, 1d is returned.  The effect is that this pair
			// of secondary random checks additively has the requisite 2^52 in 2^52 + 1 chance of passing, but over 99.97%
			// of the time only one call to Next64() is ever executed.  In the remaining few cases, on average about two
			// additional calls will be required, or one call and some integer multiplication/division/remainder, depending
			// on how RandomRange.HalfOpen() is implemented.

#if OPTIMIZE_FOR_32
			uint lower, upper;
			random.Next64(out lower, out upper);
			BitwiseDouble value;
			value.number = 0d;
			value.lowerBits = lower;
			value.upperBits = 0x3FF00000U | 0x000FFFFFU & upper;
			if ((upper & 0xFFF00000U) != 0xFFF00000U || random.RangeCO(0x0010000000000001UL) < 0x000FFFFFFFFFF001UL)
			{
				return value.number - 1d;
			}
			else
			{
				return 1d;
			}
#else
			ulong n = random.Next64();
			BitwiseDouble value;
			value.number = 0d;
			value.bits = 0x3FF0000000000000UL | 0x000FFFFFFFFFFFFFUL & n;

			if ((n & 0xFFF0000000000000UL) != 0xFFF0000000000000UL || random.HalfOpenRange(0x0010000000000001UL) < 0x000FFFFFFFFFF001UL)
			{
				return value.number - 1d;
			}
			else
			{
				return 1d;
			}
#endif
#endif
		}

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1].</returns>
		/// <seealso cref="RandomRange.DoubleCC(IRandom)"/>
		public static IDoubleGenerator MakeDoubleCCGenerator(IRandom random)
		{
			return new DoubleCCGenerator(random);
		}

		#endregion
	}
}
