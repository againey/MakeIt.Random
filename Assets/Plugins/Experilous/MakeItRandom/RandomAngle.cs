/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using System;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random angles within common ranges.
	/// </summary>
	public static class RandomAngle
	{
		#region Private Generators

		private class AngleGenerator : IRangeGenerator<float>
		{
			private IRangeGenerator<float> _rangeGenerator;
			private float _scale;

			private AngleGenerator(IRangeGenerator<float> rangeGenerator, float scale)
			{
				_rangeGenerator = rangeGenerator;
				_scale = scale;
			}

			public static AngleGenerator CreateOO(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeFloatOOGenerator(), scale);
			}

			public static AngleGenerator CreateSignedOO(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeSignedFloatOOGenerator(), scale);
			}

			public static AngleGenerator CreateCO(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeFloatCOGenerator(), scale);
			}

			public static AngleGenerator CreateSignedCO(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeSignedFloatCOGenerator(), scale);
			}

			public static AngleGenerator CreateOC(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeFloatOCGenerator(), scale);
			}

			public static AngleGenerator CreateSignedOC(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeSignedFloatOCGenerator(), scale);
			}

			public static AngleGenerator CreateCC(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeFloatCCGenerator(), scale);
			}

			public static AngleGenerator CreateSignedCC(IRandom random, float scale)
			{
				return new AngleGenerator(random.MakeSignedFloatCCGenerator(), scale);
			}

			public float Next()
			{
				return _rangeGenerator.Next() * _scale;
			}
		}

		#endregion

		private const float _floatDegreesPerTurn = 360f;
		private const float _floatDegreesPerHalfTurn = 180f;
		private const float _floatDegreesPerQuarterTurn = 90f;

		private const float _floatRadiansPerTurn = 6.283185307179586476925286766559f;
		private const float _floatRadiansPerHalfTurn = 3.1415926535897932384626433832795f;
		private const float _floatRadiansPerQuarterTurn = 1.5707963267948966192313216916398f;

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, strictly greater than 0 degrees and strictly less than 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (0, 360).</returns>
		public static float AngleDegOO(this IRandom random)
		{
			return random.FloatOO() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, strictly greater than -180 degrees and strictly less than +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (-180, +180).</returns>
		public static float SignedAngleDegOO(this IRandom random)
		{
			return random.SignedFloatOO() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, strictly greater than 0 degrees and strictly less than 180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (0, 180).</returns>
		public static float HalfAngleDegOO(this IRandom random)
		{
			return random.FloatOO() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, half a turn in either direction, strictly greater than -90 degrees and strictly less than +90 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (-90, +90).</returns>
		public static float SignedHalfAngleDegOO(this IRandom random)
		{
			return random.SignedFloatOO() * _floatDegreesPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random degree values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <returns>An angle generator producing random degree values within the specified range.</returns>
		/// <seealso cref="AngleDegOO(IRandom)"/>
		/// <seealso cref="SignedAngleDegOO(IRandom)"/>
		/// <seealso cref="HalfAngleDegOO(IRandom)"/>
		/// <seealso cref="SignedHalfAngleDegOO(IRandom)"/>
		public static IRangeGenerator<float> MakeAngleDegOOGenerator(this IRandom random, bool signed = false, bool half = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedOO(random, half ? _floatDegreesPerQuarterTurn : _floatDegreesPerHalfTurn);
			}
			else
			{
				return AngleGenerator.CreateOO(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, strictly greater than 0 radians and strictly less than 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (0, 2π).</returns>
		public static float AngleRadOO(this IRandom random)
		{
			return random.FloatOO() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, strictly greater than -π radians and strictly less than +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (-π, +π).</returns>
		public static float SignedAngleRadOO(this IRandom random)
		{
			return random.SignedFloatOO() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, strictly greater than 0 radians and strictly less than π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (0, π).</returns>
		public static float HalfAngleRadOO(this IRandom random)
		{
			return random.FloatOO() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, half a turn in either direction, strictly greater than -π/2 radians and strictly less than +π/2 radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (-π/2, +π/2).</returns>
		public static float SignedHalfAngleRadOO(this IRandom random)
		{
			return random.SignedFloatOO() * _floatRadiansPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random radian values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <returns>An angle generator producing random radian values within the specified range.</returns>
		/// <seealso cref="AngleRadOO(IRandom)"/>
		/// <seealso cref="SignedAngleRadOO(IRandom)"/>
		/// <seealso cref="HalfAngleRadOO(IRandom)"/>
		/// <seealso cref="SignedHalfAngleRadOO(IRandom)"/>
		public static IRangeGenerator<float> MakeAngleRadOOGenerator(this IRandom random, bool signed = false, bool half = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedOO(random, half ? _floatRadiansPerQuarterTurn : _floatRadiansPerHalfTurn);
			}
			else
			{
				return AngleGenerator.CreateOO(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, greater than or equal to 0 degrees and strictly less than 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 360).</returns>
		public static float AngleDegCO(this IRandom random)
		{
			return random.FloatCO() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, greater than or equal to -180 degrees and strictly less than +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-180, +180).</returns>
		public static float SignedAngleDegCO(this IRandom random)
		{
			return random.SignedFloatCO() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, greater than or equal to 0 degrees and strictly less than 180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 180).</returns>
		public static float HalfAngleDegCO(this IRandom random)
		{
			return random.FloatCO() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, half a turn in either direction, greater than or equal to -90 degrees and strictly less than +90 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-90, +90).</returns>
		public static float SignedHalfAngleDegCO(this IRandom random)
		{
			return random.SignedFloatCO() * _floatDegreesPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random degree values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <returns>An angle generator producing random degree values within the specified range.</returns>
		/// <seealso cref="AngleDegCO(IRandom)"/>
		/// <seealso cref="SignedAngleDegCO(IRandom)"/>
		/// <seealso cref="HalfAngleDegCO(IRandom)"/>
		/// <seealso cref="SignedHalfAngleDegCO(IRandom)"/>
		public static IRangeGenerator<float> MakeAngleDegCOGenerator(this IRandom random, bool signed = false, bool half = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedCO(random, half ? _floatDegreesPerQuarterTurn : _floatDegreesPerHalfTurn);
			}
			else
			{
				return AngleGenerator.CreateCO(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, greater than or equal to 0 radians and strictly less than 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, 2π).</returns>
		public static float AngleRadCO(this IRandom random)
		{
			return random.FloatCO() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, greater than or equal to -π radians and strictly less than +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π, +π).</returns>
		public static float SignedAngleRadCO(this IRandom random)
		{
			return random.SignedFloatCO() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, greater than or equal to 0 radians and strictly less than π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, π).</returns>
		public static float HalfAngleRadCO(this IRandom random)
		{
			return random.FloatCO() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, half a turn in either direction, greater than or equal to -π/2 radians and strictly less than +π/2 radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π/2, +π/2).</returns>
		public static float SignedHalfAngleRadCO(this IRandom random)
		{
			return random.SignedFloatCO() * _floatRadiansPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random radian values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <returns>An angle generator producing random radian values within the specified range.</returns>
		/// <seealso cref="AngleRadCO(IRandom)"/>
		/// <seealso cref="SignedAngleRadCO(IRandom)"/>
		/// <seealso cref="HalfAngleRadCO(IRandom)"/>
		/// <seealso cref="SignedHalfAngleRadCO(IRandom)"/>
		public static IRangeGenerator<float> MakeAngleRadCOGenerator(this IRandom random, bool signed = false, bool half = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedCO(random, half ? _floatRadiansPerQuarterTurn : _floatRadiansPerHalfTurn);
			}
			else
			{
				return AngleGenerator.CreateCO(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, strictly greater than 0 degrees and less than or equal to 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (0, 360].</returns>
		public static float AngleDegOC(this IRandom random)
		{
			return random.FloatOC() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, strictly greater than -180 degrees and less than or equal to +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (-180, +180].</returns>
		public static float SignedAngleDegOC(this IRandom random)
		{
			return random.SignedFloatOC() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, strictly greater than 0 degrees and less than or equal to 180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (0, 180].</returns>
		public static float HalfAngleDegOC(this IRandom random)
		{
			return random.FloatOC() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, half a turn in either direction, strictly greater than -90 degrees and less than or equal to +90 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range (-90, +90].</returns>
		public static float SignedHalfAngleDegOC(this IRandom random)
		{
			return random.SignedFloatOC() * _floatDegreesPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random degree values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <returns>An angle generator producing random degree values within the specified range.</returns>
		/// <seealso cref="AngleDegOC(IRandom)"/>
		/// <seealso cref="SignedAngleDegOC(IRandom)"/>
		/// <seealso cref="HalfAngleDegOC(IRandom)"/>
		/// <seealso cref="SignedHalfAngleDegOC(IRandom)"/>
		public static IRangeGenerator<float> MakeAngleDegOCGenerator(this IRandom random, bool signed = false, bool half = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedOC(random, half ? _floatDegreesPerQuarterTurn : _floatDegreesPerHalfTurn);
			}
			else
			{
				return AngleGenerator.CreateOC(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, strictly greater than 0 radians and less than or equal to 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (0, 2π].</returns>
		public static float AngleRadOC(this IRandom random)
		{
			return random.FloatOC() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, strictly greater than -π radians and less than or equal to +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (-π, +π].</returns>
		public static float SignedAngleRadOC(this IRandom random)
		{
			return random.SignedFloatOC() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, strictly greater than 0 radians and less than or equal to π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (0, π].</returns>
		public static float HalfAngleRadOC(this IRandom random)
		{
			return random.FloatOC() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, half a turn in either direction, strictly greater than -π/2 radians and less than or equal to +π/2 radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range (-π/2, +π/2].</returns>
		public static float SignedHalfAngleRadOC(this IRandom random)
		{
			return random.SignedFloatOC() * _floatRadiansPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random radian values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <returns>An angle generator producing random radian values within the specified range.</returns>
		/// <seealso cref="AngleRadOC(IRandom)"/>
		/// <seealso cref="SignedAngleRadOC(IRandom)"/>
		/// <seealso cref="HalfAngleRadOC(IRandom)"/>
		/// <seealso cref="SignedHalfAngleRadOC(IRandom)"/>
		public static IRangeGenerator<float> MakeAngleRadOCGenerator(this IRandom random, bool signed = false, bool half = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedOC(random, half ? _floatRadiansPerQuarterTurn : _floatRadiansPerHalfTurn);
			}
			else
			{
				return AngleGenerator.CreateOC(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, greater than or equal to 0 degrees and less than or equal to 360 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 360].</returns>
		public static float AngleDegCC(this IRandom random)
		{
			return random.FloatCC() * _floatDegreesPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from the full range of rotation, half a turn in either direction, greater than or equal to -180 degrees and less than or equal to +180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-180, +180].</returns>
		public static float SignedAngleDegCC(this IRandom random)
		{
			return random.SignedFloatCC() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, greater than or equal to 0 degrees and less than or equal to 180 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [0, 180].</returns>
		public static float HalfAngleDegCC(this IRandom random)
		{
			return random.FloatCC() * _floatDegreesPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in degrees from only half of the full range of rotation, half a turn in either direction, greater than or equal to -90 degrees and less than or equal to +90 degrees.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in degrees in the range [-90, +90].</returns>
		public static float SignedHalfAngleDegCC(this IRandom random)
		{
			return random.SignedFloatCC() * _floatDegreesPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random degree values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <returns>An angle generator producing random degree values within the specified range.</returns>
		/// <seealso cref="AngleDegCC(IRandom)"/>
		/// <seealso cref="SignedAngleDegCC(IRandom)"/>
		/// <seealso cref="HalfAngleDegCC(IRandom)"/>
		/// <seealso cref="SignedHalfAngleDegCC(IRandom)"/>
		public static IRangeGenerator<float> MakeAngleDegCCGenerator(this IRandom random, bool signed = false, bool half = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedCC(random, half ? _floatDegreesPerQuarterTurn : _floatDegreesPerHalfTurn);
			}
			else
			{
				return AngleGenerator.CreateCC(random, half ? _floatDegreesPerHalfTurn : _floatDegreesPerTurn);
			}
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, greater than or equal to 0 radians and less than or equal to 2π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, 2π].</returns>
		public static float AngleRadCC(this IRandom random)
		{
			return random.FloatCC() * _floatRadiansPerTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from the full range of rotation, half a turn in either direction, greater than or equal to -π radians and less than or equal to +π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π, +π].</returns>
		public static float SignedAngleRadCC(this IRandom random)
		{
			return random.SignedFloatCC() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, greater than or equal to 0 radians and less than or equal to π radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [0, π].</returns>
		public static float HalfAngleRadCC(this IRandom random)
		{
			return random.FloatCC() * _floatRadiansPerHalfTurn;
		}

		/// <summary>
		/// Returns a random angle measured in radians from only half of the full range of rotation, half a turn in either direction, greater than or equal to -π/2 radians and less than or equal to +π/2 radians.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random angle measured in radians in the range [-π/2, +π/2].</returns>
		public static float SignedHalfAngleRadCC(this IRandom random)
		{
			return random.SignedFloatCC() * _floatRadiansPerQuarterTurn;
		}

		/// <summary>
		/// Returns an angle generator which will produce random radian values within the specified range.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the generator's return values are derived.</param>
		/// <param name="signed">Indicates if the range of angles generated should be centered at 0 or have its lower bound be at zero.</param>
		/// <param name="half">Indicates if the range of angles generated should cover only half of a revolution or a full revolution.</param>
		/// <returns>An angle generator producing random radian values within the specified range.</returns>
		/// <seealso cref="AngleRadCC(IRandom)"/>
		/// <seealso cref="SignedAngleRadCC(IRandom)"/>
		/// <seealso cref="HalfAngleRadCC(IRandom)"/>
		/// <seealso cref="SignedHalfAngleRadCC(IRandom)"/>
		public static IRangeGenerator<float> MakeAngleRadCCGenerator(this IRandom random, bool signed = false, bool half = false)
		{
			if (signed)
			{
				return AngleGenerator.CreateSignedCC(random, half ? _floatRadiansPerQuarterTurn : _floatRadiansPerHalfTurn);
			}
			else
			{
				return AngleGenerator.CreateCC(random, half ? _floatRadiansPerHalfTurn : _floatRadiansPerTurn);
			}
		}
	}
}
