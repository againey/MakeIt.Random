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

		private class FloatC1O2Generator : IFloatGenerator
		{
			private IRandom _random;
			public FloatC1O2Generator(IRandom random) { _random = random; }
			public float Next() { return _random.FloatC1O2(); }
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

		private class DoubleC1O2Generator : IDoubleGenerator
		{
			private IRandom _random;
			public DoubleC1O2Generator(IRandom random) { _random = random; }
			public double Next() { return _random.DoubleC1O2(); }
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
				n = random.Next32();
			} while (n <= 0x1FFU); // While the upper 23 bits are all zeros.
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & (n >> 9);
			return value.number - 1f;
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1).</returns>
		/// <seealso cref="RandomRange.FloatOO(IRandom)"/>
		public static IFloatGenerator MakeFloatOOGenerator(this IRandom random)
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

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1).</returns>
		/// <seealso cref="RandomRange.DoubleOO(IRandom)"/>
		public static IDoubleGenerator MakeDoubleOOGenerator(this IRandom random)
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
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & (random.Next32());
			return value.number - 1f;
#endif
		}

		/// <summary>
		/// Returns a range generator which will produce floats greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1).</returns>
		/// <seealso cref="RandomRange.FloatCO(IRandom)"/>
		public static IFloatGenerator MakeFloatCOGenerator(this IRandom random)
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

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and strictly less than one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1).</returns>
		/// <seealso cref="RandomRange.DoubleCO(IRandom)"/>
		public static IDoubleGenerator MakeDoubleCOGenerator(this IRandom random)
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
			Detail.FloatingPoint.BitwiseFloat value;
			value.number = 0f;
			value.bits = Detail.FloatingPoint.floatOne | Detail.FloatingPoint.floatMantissaMask & (random.Next32());
			return 2f - value.number;
		}

		/// <summary>
		/// Returns a range generator which will produce floats strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range (0, 1].</returns>
		/// <seealso cref="RandomRange.FloatOC(IRandom)"/>
		public static IFloatGenerator MakeFloatOCGenerator(this IRandom random)
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

		/// <summary>
		/// Returns a range generator which will produce doubles strictly greater than zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range (0, 1].</returns>
		/// <seealso cref="RandomRange.DoubleOC(IRandom)"/>
		public static IDoubleGenerator MakeDoubleOCGenerator(this IRandom random)
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
		/// Returns a range generator which will produce floats greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [0, 1].</returns>
		/// <seealso cref="RandomRange.FloatCC(IRandom)"/>
		public static IFloatGenerator MakeFloatCCGenerator(this IRandom random)
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

#if OPTIMIZE_FOR_32
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

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to zero and less than or equal to one.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [0, 1].</returns>
		/// <seealso cref="RandomRange.DoubleCC(IRandom)"/>
		public static IDoubleGenerator MakeDoubleCCGenerator(this IRandom random)
		{
			return new DoubleCCGenerator(random);
		}

		#endregion

		#region Unit Closed/Open [1, 2)

		/// <summary>
		/// Returns a random floating point number greater than or equal to one and strictly less than two.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random floating point number in the range [0, 1).</returns>
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
		/// Returns a range generator which will produce floats greater than or equal to one and strictly less than two.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random floats in the range [1, 2).</returns>
		/// <remarks><para>Given the implementation details, this function is slightly faster than the other unit ranges.</para></remarks>
		/// <seealso cref="RandomRange.FloatC1O2(IRandom)"/>
		public static IFloatGenerator MakeFloatC1O2Generator(this IRandom random)
		{
			return new FloatC1O2Generator(random);
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
#if OPTIMIZE_FOR_32
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

		/// <summary>
		/// Returns a range generator which will produce doubles greater than or equal to one and strictly less than two.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <returns>A range generator producing random doubles in the range [1, 2).</returns>
		/// <remarks><para>Given the implementation details, this function is slightly faster than the other unit ranges.</para></remarks>
		/// <seealso cref="RandomRange.DoubleC1O2(IRandom)"/>
		public static IDoubleGenerator MakeDoubleC1O2Generator(this IRandom random)
		{
			return new DoubleC1O2Generator(random);
		}

		#endregion
	}
}
