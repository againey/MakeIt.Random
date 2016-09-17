/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using Experilous.MakeItColorful;

namespace Experilous.MakeItRandom
{
	/// <summary>
	/// A static class of extension methods for generating random colors in various color spaces.
	/// </summary>
	public static class RandomColor
	{
		#region Specific Colors

		/// <summary>
		/// Generates a random grayscale color, ranging all the way from completely black to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque grayscale color.</returns>
		public static Color Gray(this IRandom random)
		{
			float value = random.FloatCC();
			return new Color(value, value, value);
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from completely black to red.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark red color.</returns>
		public static Color DarkRed(this IRandom random)
		{
			return new Color(random.FloatCC(), 0f, 0f);
		}

		/// <summary>
		/// Generates a random green color, ranging all the way from completely black to green.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark green color.</returns>
		public static Color DarkGreen(this IRandom random)
		{
			return new Color(0f, random.FloatCC(), 0f);
		}

		/// <summary>
		/// Generates a random blue color, ranging all the way from completely black to blue.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark blue color.</returns>
		public static Color DarkBlue(this IRandom random)
		{
			return new Color(0f, 0f, random.FloatCC());
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from red to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light red color.</returns>
		public static Color LightRed(this IRandom random)
		{
			float value = random.FloatCC();
			return new Color(1f, value, value);
		}

		/// <summary>
		/// Generates a random green color, ranging all the way from green to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light green color.</returns>
		public static Color LightGreen(this IRandom random)
		{
			float value = random.FloatCC();
			return new Color(value, 1f, value);
		}

		/// <summary>
		/// Generates a random blue color, ranging all the way from blue to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light blue color.</returns>
		public static Color LightBlue(this IRandom random)
		{
			float value = random.FloatCC();
			return new Color(value, value, 1f);
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from completely black, through red, to complete white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque red color.</returns>
		public static Color Red(this IRandom random)
		{
			float value = random.FloatCC();
			if (value <= 0.5f)
			{
				return new Color(value * 2f, 0f, 0f);
			}
			else
			{
				value = value * 2f - 1f;
				return new Color(1f, value, value);
			}
		}

		/// <summary>
		/// Generates a random green color, ranging all the way from completely black, through green, to complete white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque green color.</returns>
		public static Color Green(this IRandom random)
		{
			float value = random.FloatCC();
			if (value <= 0.5f)
			{
				return new Color(0f, value * 2f, 0f);
			}
			else
			{
				value = value * 2f - 1f;
				return new Color(value, 1f, value);
			}
		}

		/// <summary>
		/// Generates a random blue color, ranging all the way from completely black, through blue, to complete white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque blue color.</returns>
		public static Color Blue(this IRandom random)
		{
			float value = random.FloatCC();
			if (value <= 0.5f)
			{
				return new Color(0f, 0f, value * 2f);
			}
			else
			{
				value = value * 2f - 1f;
				return new Color(value, value, 1f);
			}
		}

		#endregion

		#region RGB

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the red/green/blue color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static Color RGB(this IRandom random)
		{
			return new Color(random.FloatCC(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the red/green/blue color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static Color RGB(this IRandom random, float a)
		{
			return new Color(random.FloatCC(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the red/green/blue color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static Color RGBA(this IRandom random)
		{
			return new Color(random.FloatCC(), random.FloatCC(), random.FloatCC(), random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteRedShift">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteGreenShift">The largest amount by which the green channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteBlueShift">The largest amount by which the blue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static Color RGBShift(this IRandom random, Color original, float maxAbsoluteRedShift, float maxAbsoluteGreenShift, float maxAbsoluteBlueShift)
		{
			return new Color(
				maxAbsoluteRedShift != 0f ? random.Shift(original.r, maxAbsoluteRedShift) : original.r,
				maxAbsoluteGreenShift != 0f ? random.Shift(original.g, maxAbsoluteGreenShift) : original.g,
				maxAbsoluteBlueShift != 0f ? random.Shift(original.b, maxAbsoluteBlueShift) : original.b,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteRedShift">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteGreenShift">The largest amount by which the green channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteBlueShift">The largest amount by which the blue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static Color RGBAShift(this IRandom random, Color original, float maxAbsoluteRedShift, float maxAbsoluteGreenShift, float maxAbsoluteBlueShift, float maxAbsoluteAlphaShift)
		{
			return new Color(
				maxAbsoluteRedShift != 0f ? random.Shift(original.r, maxAbsoluteRedShift) : original.r,
				maxAbsoluteGreenShift != 0f ? random.Shift(original.g, maxAbsoluteGreenShift) : original.g,
				maxAbsoluteBlueShift != 0f ? random.Shift(original.b, maxAbsoluteBlueShift) : original.b,
				maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minRedShift">The minimum end of the range offset from the current value within which the red channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxRedShift"/>.</param>
		/// <param name="maxRedShift">The maximum end of the range offset from the current value within which the red channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minRedShift"/>.</param>
		/// <param name="minGreenShift">The minimum end of the range offset from the current value within which the green channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxGreenShift"/>.</param>
		/// <param name="maxGreenShift">The maximum end of the range offset from the current value within which the green channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minGreenShift"/>.</param>
		/// <param name="minBlueShift">The minimum end of the range offset from the current value within which the blue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxBlueShift"/>.</param>
		/// <param name="maxBlueShift">The maximum end of the range offset from the current value within which the blue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minBlueShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static Color RGBShift(this IRandom random, Color original, float minRedShift, float maxRedShift, float minGreenShift, float maxGreenShift, float minBlueShift, float maxBlueShift)
		{
			return new Color(
				minRedShift != 0f || maxRedShift != 0f ? random.Shift(original.r, minRedShift, maxRedShift) : original.r,
				minGreenShift != 0f || maxGreenShift != 0f ? random.Shift(original.g, minGreenShift, maxGreenShift) : original.g,
				minBlueShift != 0f || maxBlueShift != 0f ? random.Shift(original.b, minBlueShift, maxBlueShift) : original.b,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minRedShift">The minimum end of the range offset from the current value within which the red channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxRedShift"/>.</param>
		/// <param name="maxRedShift">The maximum end of the range offset from the current value within which the red channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minRedShift"/>.</param>
		/// <param name="minGreenShift">The minimum end of the range offset from the current value within which the green channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxGreenShift"/>.</param>
		/// <param name="maxGreenShift">The maximum end of the range offset from the current value within which the green channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minGreenShift"/>.</param>
		/// <param name="minBlueShift">The minimum end of the range offset from the current value within which the blue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxBlueShift"/>.</param>
		/// <param name="maxBlueShift">The maximum end of the range offset from the current value within which the blue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minBlueShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static Color RGBAShift(this IRandom random, Color original, float minRedShift, float maxRedShift, float minGreenShift, float maxGreenShift, float minBlueShift, float maxBlueShift, float minAlphaShift, float maxAlphaShift)
		{
			return new Color(
				minRedShift != 0f || maxRedShift != 0f ? random.Shift(original.r, minRedShift, maxRedShift) : original.r,
				minGreenShift != 0f || maxGreenShift != 0f ? random.Shift(original.g, minGreenShift, maxGreenShift) : original.g,
				minBlueShift != 0f || maxBlueShift != 0f ? random.Shift(original.b, minBlueShift, maxBlueShift) : original.b,
				minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteRedSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the red channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteGreenSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the green channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteBlueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the blue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static Color RGBSpread(this IRandom random, Color original, float maxAbsoluteRedSpread, float maxAbsoluteGreenSpread, float maxAbsoluteBlueSpread)
		{
			return new Color(
				maxAbsoluteRedSpread != 0f ? random.Spread(original.r, maxAbsoluteRedSpread) : original.r,
				maxAbsoluteGreenSpread != 0f ? random.Spread(original.g, maxAbsoluteGreenSpread) : original.g,
				maxAbsoluteBlueSpread != 0f ? random.Spread(original.b, maxAbsoluteBlueSpread) : original.b,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteRedSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the red channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteGreenSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the green channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteBlueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the blue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static Color RGBASpread(this IRandom random, Color original, float maxAbsoluteRedSpread, float maxAbsoluteGreenSpread, float maxAbsoluteBlueSpread, float maxAbsoluteAlphaSpread)
		{
			return new Color(
				maxAbsoluteRedSpread != 0f ? random.Spread(original.r, maxAbsoluteRedSpread) : original.r,
				maxAbsoluteGreenSpread != 0f ? random.Spread(original.g, maxAbsoluteGreenSpread) : original.g,
				maxAbsoluteBlueSpread != 0f ? random.Spread(original.b, maxAbsoluteBlueSpread) : original.b,
				maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minRedSpread">The minimum end of the proportional range within which the red channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxRedSpread"/>.</param>
		/// <param name="maxRedSpread">The maximum end of the proportional range within which the red channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minRedSpread"/>.</param>
		/// <param name="minGreenSpread">The minimum end of the proportional range within which the green channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxGreenSpread"/>.</param>
		/// <param name="maxGreenSpread">The maximum end of the proportional range within which the green channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minGreenSpread"/>.</param>
		/// <param name="minBlueSpread">The minimum end of the proportional range within which the blue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxBlueSpread"/>.</param>
		/// <param name="maxBlueSpread">The maximum end of the proportional range within which the blue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minBlueSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static Color RGBSpread(this IRandom random, Color original, float minRedSpread, float maxRedSpread, float minGreenSpread, float maxGreenSpread, float minBlueSpread, float maxBlueSpread)
		{
			return new Color(
				minRedSpread != 0f || maxRedSpread != 0f ? random.Spread(original.r, minRedSpread, maxRedSpread) : original.r,
				minGreenSpread != 0f || maxGreenSpread != 0f ? random.Spread(original.g, minGreenSpread, maxGreenSpread) : original.g,
				minBlueSpread != 0f || maxBlueSpread != 0f ? random.Spread(original.b, minBlueSpread, maxBlueSpread) : original.b,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minRedSpread">The minimum end of the proportional range within which the red channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxRedSpread"/>.</param>
		/// <param name="maxRedSpread">The maximum end of the proportional range within which the red channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minRedSpread"/>.</param>
		/// <param name="minGreenSpread">The minimum end of the proportional range within which the green channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxGreenSpread"/>.</param>
		/// <param name="maxGreenSpread">The maximum end of the proportional range within which the green channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minGreenSpread"/>.</param>
		/// <param name="minBlueSpread">The minimum end of the proportional range within which the blue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxBlueSpread"/>.</param>
		/// <param name="maxBlueSpread">The maximum end of the proportional range within which the blue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minBlueSpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static Color RGBASpread(this IRandom random, Color original, float minRedSpread, float maxRedSpread, float minGreenSpread, float maxGreenSpread, float minBlueSpread, float maxBlueSpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return new Color(
				minRedSpread != 0f || maxRedSpread != 0f ? random.Spread(original.r, minRedSpread, maxRedSpread) : original.r,
				minGreenSpread != 0f || maxGreenSpread != 0f ? random.Spread(original.g, minGreenSpread, maxGreenSpread) : original.g,
				minBlueSpread != 0f || maxBlueSpread != 0f ? random.Spread(original.b, minBlueSpread, maxBlueSpread) : original.b,
				minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetRed">The far end of the range within which the red channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetGreen">The far end of the range within which the green channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetBlue">The far end of the range within which the blue channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static Color RGBLerp(this IRandom random, Color original, float targetRed, float targetGreen, float targetBlue)
		{
			return new Color(
				random.RangeCC(original.r, targetRed),
				random.RangeCC(original.g, targetGreen),
				random.RangeCC(original.b, targetBlue),
				original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetRed">The far end of the range within which the red channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetGreen">The far end of the range within which the green channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetBlue">The far end of the range within which the blue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static Color RGBALerp(this IRandom random, Color original, float targetRed, float targetGreen, float targetBlue, float targetAlpha)
		{
			return new Color(
				random.RangeCC(original.r, targetRed),
				random.RangeCC(original.g, targetGreen),
				random.RangeCC(original.b, targetBlue),
				random.RangeCC(original.a, targetAlpha));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static Color RGBALerp(this IRandom random, Color original, Color target)
		{
			return new Color(
				random.RangeCC(original.r, target.r),
				random.RangeCC(original.g, target.g),
				random.RangeCC(original.b, target.b),
				random.RangeCC(original.a, target.a));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the intensity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static Color IntensityShift(this IRandom random, Color original, float maxAbsoluteShift)
		{
			float oldIntensity = (original.r + original.g + original.b) / 3f;
			float newIntensity = random.Shift(oldIntensity, maxAbsoluteShift);
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the intensity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the intensity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static Color IntensityShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			float oldIntensity = (original.r + original.g + original.b) / 3f;
			float newIntensity = random.Shift(oldIntensity, minShift, maxShift);
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static Color IntensitySpread(this IRandom random, Color original)
		{
			float oldIntensity = (original.r + original.g + original.b) / 3f;
			float newIntensity = random.FloatCC();
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the intensity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static Color IntensitySpread(this IRandom random, Color original, float maxAbsoluteSpread)
		{
			float oldIntensity = (original.r + original.g + original.b) / 3f;
			float newIntensity = random.Spread(oldIntensity, maxAbsoluteSpread);
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the intensity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the intensity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static Color IntensitySpread(this IRandom random, Color original, float minSpread, float maxSpread)
		{
			float oldIntensity = (original.r + original.g + original.b) / 3f;
			float newIntensity = random.Spread(oldIntensity, minSpread, maxSpread);
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the collective intensity of the color channels toward the specified target by a random amount while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="target">The far end of the range within which the collective intensity of the color channels can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the collective intensity of the color channels randomized.</returns>
		public static Color IntensityLerp(this IRandom random, Color original, float target)
		{
			float oldIntensity = (original.r + original.g + original.b) / 3f;
			float newIntensity = Mathf.Lerp(oldIntensity, target, random.FloatCC());
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the collective intensity of the color channels toward the specified target by a random amount while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="target">The color whose color channels indicates the far end of the range within which the collective intensity of the color channels can change.</param>
		/// <returns>A color derived from the original color but with the collective intensity of the color channels randomized.</returns>
		public static Color IntensityLerp(this IRandom random, Color original, Color target)
		{
			return random.IntensityLerp(original, (target.r + target.g + target.b) / 3f);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the red channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color RedShift(this IRandom random, Color original, float maxAbsoluteShift)
		{
			return new Color(random.Shift(original.r, maxAbsoluteShift), original.g, original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the red channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the red channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the red channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color RedShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			return new Color(random.Shift(original.r, minShift, maxShift), original.g, original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the red channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color RedSpread(this IRandom random, Color original)
		{
			return new Color(random.FloatCC(), original.g, original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the red channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the red channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color RedSpread(this IRandom random, Color original, float maxAbsoluteSpread)
		{
			return new Color(random.Spread(original.r, maxAbsoluteSpread), original.g, original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the red channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the red channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the red channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color RedSpread(this IRandom random, Color original, float minSpread, float maxSpread)
		{
			return new Color(random.Spread(original.r, minSpread, maxSpread), original.g, original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the red channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <param name="target">The far end of the range within which the red channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color RedLerp(this IRandom random, Color original, float target)
		{
			return new Color(random.RangeCC(original.r, target), original.g, original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the red channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <param name="target">The color whose red channel indicates the far end of the range within which the red channel can change.</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color RedLerp(this IRandom random, Color original, Color target)
		{
			return new Color(random.RangeCC(original.r, target.r), original.g, original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the green channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose green channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the green channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the green channel randomized.</returns>
		public static Color GreenShift(this IRandom random, Color original, float maxAbsoluteShift)
		{
			return new Color(original.r, random.Shift(original.g, maxAbsoluteShift), original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the green channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose green channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the green channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the green channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the green channel randomized.</returns>
		public static Color GreenShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			return new Color(original.r, random.Shift(original.g, minShift, maxShift), original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the green channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose green channel will be altered.</param>
		/// <returns>A color derived from the original color but with the green channel randomized.</returns>
		public static Color GreenSpread(this IRandom random, Color original)
		{
			return new Color(original.r, random.FloatCC(), original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the green channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose green channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the green channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the green channel randomized.</returns>
		public static Color GreenSpread(this IRandom random, Color original, float maxAbsoluteSpread)
		{
			return new Color(original.r, random.Spread(original.g, maxAbsoluteSpread), original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the green channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose green channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the green channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the green channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the green channel randomized.</returns>
		public static Color GreenSpread(this IRandom random, Color original, float minSpread, float maxSpread)
		{
			return new Color(original.r, random.Spread(original.g, minSpread, maxSpread), original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the green channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose green channel will be altered.</param>
		/// <param name="target">The far end of the range within which the green channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the green channel randomized.</returns>
		public static Color GreenLerp(this IRandom random, Color original, float target)
		{
			return new Color(original.r, random.RangeCC(original.g, target), original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the green channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose green channel will be altered.</param>
		/// <param name="target">The color whose green channel indicates the far end of the range within which the green channel can change.</param>
		/// <returns>A color derived from the original color but with the green channel randomized.</returns>
		public static Color GreenLerp(this IRandom random, Color original, Color target)
		{
			return new Color(original.r, random.RangeCC(original.g, target.g), original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the blue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose blue channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the blue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the blue channel randomized.</returns>
		public static Color BlueShift(this IRandom random, Color original, float maxAbsoluteShift)
		{
			return new Color(original.r, original.g, random.Shift(original.b, maxAbsoluteShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the blue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose blue channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the blue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the blue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the blue channel randomized.</returns>
		public static Color BlueShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			return new Color(original.r, original.g, random.Shift(original.b, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the blue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose blue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the blue channel randomized.</returns>
		public static Color BlueSpread(this IRandom random, Color original)
		{
			return new Color(original.r, original.g, random.FloatCC(), original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the blue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose blue channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the blue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the blue channel randomized.</returns>
		public static Color BlueSpread(this IRandom random, Color original, float maxAbsoluteSpread)
		{
			return new Color(original.r, original.g, random.Spread(original.b, maxAbsoluteSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the blue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose blue channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the blue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the blue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the blue channel randomized.</returns>
		public static Color BlueSpread(this IRandom random, Color original, float minSpread, float maxSpread)
		{
			return new Color(original.r, original.g, random.Spread(original.b, minSpread, maxSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the blue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose blue channel will be altered.</param>
		/// <param name="target">The far end of the range within which the blue channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the blue channel randomized.</returns>
		public static Color BlueLerp(this IRandom random, Color original, float target)
		{
			return new Color(original.r, original.g, random.RangeCC(original.b, target), original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the blue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose blue channel will be altered.</param>
		/// <param name="target">The color whose blue channel indicates the far end of the range within which the blue channel can change.</param>
		/// <returns>A color derived from the original color but with the blue channel randomized.</returns>
		public static Color BlueLerp(this IRandom random, Color original, Color target)
		{
			return new Color(original.r, original.g, random.RangeCC(original.b, target.b), original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static Color AlphaShift(this IRandom random, Color original, float maxAbsoluteShift)
		{
			return new Color(original.r, original.g, original.b, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static Color AlphaShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			return new Color(original.r, original.g, original.b, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static Color AlphaSpread(this IRandom random, Color original)
		{
			return new Color(original.r, original.g, original.b, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static Color AlphaSpread(this IRandom random, Color original, float maxAbsoluteSpread)
		{
			return new Color(original.r, original.g, original.b, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static Color AlphaSpread(this IRandom random, Color original, float minSpread, float maxSpread)
		{
			return new Color(original.r, original.g, original.b, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static Color AlphaLerp(this IRandom random, Color original, float target)
		{
			return new Color(original.r, original.g, original.b, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static Color AlphaLerp(this IRandom random, Color original, Color target)
		{
			return new Color(original.r, original.g, original.b, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region CMY

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorCMY CMY(this IRandom random)
		{
			return new ColorCMY(random.FloatCC(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorCMY CMY(this IRandom random, float a)
		{
			return new ColorCMY(random.FloatCC(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorCMY CMYA(this IRandom random)
		{
			return new ColorCMY(random.FloatCC(), random.FloatCC(), random.FloatCC(), random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteCyanShift">The largest amount by which the cyan channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteMagentaShift">The largest amount by which the magenta channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteYellowShift">The largest amount by which the yellow channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMY CMYShift(this IRandom random, ColorCMY original, float maxAbsoluteCyanShift, float maxAbsoluteMagentaShift, float maxAbsoluteYellowShift)
		{
			return new ColorCMY(
				maxAbsoluteCyanShift != 0f ? random.Shift(original.c, maxAbsoluteCyanShift) : original.c,
				maxAbsoluteMagentaShift != 0f ? random.Shift(original.m, maxAbsoluteMagentaShift) : original.m,
				maxAbsoluteYellowShift != 0f ? random.Shift(original.y, maxAbsoluteYellowShift) : original.y,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteCyanShift">The largest amount by which the cyan channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteMagentaShift">The largest amount by which the magenta channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteYellowShift">The largest amount by which the yellow channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMY CMYAShift(this IRandom random, ColorCMY original, float maxAbsoluteCyanShift, float maxAbsoluteMagentaShift, float maxAbsoluteYellowShift, float maxAbsoluteAlphaShift)
		{
			return new ColorCMY(
				maxAbsoluteCyanShift != 0f ? random.Shift(original.c, maxAbsoluteCyanShift) : original.c,
				maxAbsoluteMagentaShift != 0f ? random.Shift(original.m, maxAbsoluteMagentaShift) : original.m,
				maxAbsoluteYellowShift != 0f ? random.Shift(original.y, maxAbsoluteYellowShift) : original.y,
				maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minCyanShift">The minimum end of the range offset from the current value within which the cyan channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxCyanShift"/>.</param>
		/// <param name="maxCyanShift">The maximum end of the range offset from the current value within which the cyan channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minCyanShift"/>.</param>
		/// <param name="minMagentaShift">The minimum end of the range offset from the current value within which the magenta channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxMagentaShift"/>.</param>
		/// <param name="maxMagentaShift">The maximum end of the range offset from the current value within which the magenta channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minMagentaShift"/>.</param>
		/// <param name="minYellowShift">The minimum end of the range offset from the current value within which the yellow channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxYellowShift"/>.</param>
		/// <param name="maxYellowShift">The maximum end of the range offset from the current value within which the yellow channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minYellowShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMY CMYShift(this IRandom random, ColorCMY original, float minCyanShift, float maxCyanShift, float minMagentaShift, float maxMagentaShift, float minYellowShift, float maxYellowShift)
		{
			return new ColorCMY(
				minCyanShift != 0f || maxCyanShift != 0f ? random.Shift(original.c, minCyanShift, maxCyanShift) : original.c,
				minMagentaShift != 0f || maxMagentaShift != 0f ? random.Shift(original.m, minMagentaShift, maxMagentaShift) : original.m,
				minYellowShift != 0f || maxYellowShift != 0f ? random.Shift(original.y, minYellowShift, maxYellowShift) : original.y,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minCyanShift">The minimum end of the range offset from the current value within which the cyan channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxCyanShift"/>.</param>
		/// <param name="maxCyanShift">The maximum end of the range offset from the current value within which the cyan channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minCyanShift"/>.</param>
		/// <param name="minMagentaShift">The minimum end of the range offset from the current value within which the magenta channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxMagentaShift"/>.</param>
		/// <param name="maxMagentaShift">The maximum end of the range offset from the current value within which the magenta channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minMagentaShift"/>.</param>
		/// <param name="minYellowShift">The minimum end of the range offset from the current value within which the yellow channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxYellowShift"/>.</param>
		/// <param name="maxYellowShift">The maximum end of the range offset from the current value within which the yellow channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minYellowShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMY CMYAShift(this IRandom random, ColorCMY original, float minCyanShift, float maxCyanShift, float minMagentaShift, float maxMagentaShift, float minYellowShift, float maxYellowShift, float minAlphaShift, float maxAlphaShift)
		{
			return new ColorCMY(
				minCyanShift != 0f || maxCyanShift != 0f ? random.Shift(original.c, minCyanShift, maxCyanShift) : original.c,
				minMagentaShift != 0f || maxMagentaShift != 0f ? random.Shift(original.m, minMagentaShift, maxMagentaShift) : original.m,
				minYellowShift != 0f || maxYellowShift != 0f ? random.Shift(original.y, minYellowShift, maxYellowShift) : original.y,
				minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteCyanSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the cyan channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteMagentaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the magenta channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteYellowSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the yellow channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMY CMYSpread(this IRandom random, ColorCMY original, float maxAbsoluteCyanSpread, float maxAbsoluteMagentaSpread, float maxAbsoluteYellowSpread)
		{
			return new ColorCMY(
				maxAbsoluteCyanSpread != 0f ? random.Spread(original.c, maxAbsoluteCyanSpread) : original.c,
				maxAbsoluteMagentaSpread != 0f ? random.Spread(original.m, maxAbsoluteMagentaSpread) : original.m,
				maxAbsoluteYellowSpread != 0f ? random.Spread(original.y, maxAbsoluteYellowSpread) : original.y,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteCyanSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the cyan channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteMagentaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the magenta channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteYellowSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the yellow channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMY CMYASpread(this IRandom random, ColorCMY original, float maxAbsoluteCyanSpread, float maxAbsoluteMagentaSpread, float maxAbsoluteYellowSpread, float maxAbsoluteAlphaSpread)
		{
			return new ColorCMY(
				maxAbsoluteCyanSpread != 0f ? random.Spread(original.c, maxAbsoluteCyanSpread) : original.c,
				maxAbsoluteMagentaSpread != 0f ? random.Spread(original.m, maxAbsoluteMagentaSpread) : original.m,
				maxAbsoluteYellowSpread != 0f ? random.Spread(original.y, maxAbsoluteYellowSpread) : original.y,
				maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minCyanSpread">The minimum end of the proportional range within which the cyan channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxCyanSpread"/>.</param>
		/// <param name="maxCyanSpread">The maximum end of the proportional range within which the cyan channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minCyanSpread"/>.</param>
		/// <param name="minMagentaSpread">The minimum end of the proportional range within which the magenta channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxMagentaSpread"/>.</param>
		/// <param name="maxMagentaSpread">The maximum end of the proportional range within which the magenta channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minMagentaSpread"/>.</param>
		/// <param name="minYellowSpread">The minimum end of the proportional range within which the yellow channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxYellowSpread"/>.</param>
		/// <param name="maxYellowSpread">The maximum end of the proportional range within which the yellow channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minYellowSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMY CMYSpread(this IRandom random, ColorCMY original, float minCyanSpread, float maxCyanSpread, float minMagentaSpread, float maxMagentaSpread, float minYellowSpread, float maxYellowSpread)
		{
			return new ColorCMY(
				minCyanSpread != 0f || maxCyanSpread != 0f ? random.Spread(original.c, minCyanSpread, maxCyanSpread) : original.c,
				minMagentaSpread != 0f || maxMagentaSpread != 0f ? random.Spread(original.m, minMagentaSpread, maxMagentaSpread) : original.m,
				minYellowSpread != 0f || maxYellowSpread != 0f ? random.Spread(original.y, minYellowSpread, maxYellowSpread) : original.y,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minCyanSpread">The minimum end of the proportional range within which the cyan channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxCyanSpread"/>.</param>
		/// <param name="maxCyanSpread">The maximum end of the proportional range within which the cyan channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minCyanSpread"/>.</param>
		/// <param name="minMagentaSpread">The minimum end of the proportional range within which the magenta channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxMagentaSpread"/>.</param>
		/// <param name="maxMagentaSpread">The maximum end of the proportional range within which the magenta channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minMagentaSpread"/>.</param>
		/// <param name="minYellowSpread">The minimum end of the proportional range within which the yellow channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxYellowSpread"/>.</param>
		/// <param name="maxYellowSpread">The maximum end of the proportional range within which the yellow channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minYellowSpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMY CMYASpread(this IRandom random, ColorCMY original, float minCyanSpread, float maxCyanSpread, float minMagentaSpread, float maxMagentaSpread, float minYellowSpread, float maxYellowSpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return new ColorCMY(
				minCyanSpread != 0f || maxCyanSpread != 0f ? random.Spread(original.c, minCyanSpread, maxCyanSpread) : original.c,
				minMagentaSpread != 0f || maxMagentaSpread != 0f ? random.Spread(original.m, minMagentaSpread, maxMagentaSpread) : original.m,
				minYellowSpread != 0f || maxYellowSpread != 0f ? random.Spread(original.y, minYellowSpread, maxYellowSpread) : original.y,
				minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetCyan">The far end of the range within which the cyan channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetMagenta">The far end of the range within which the magenta channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetYellow">The far end of the range within which the yellow channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMY CMYLerp(this IRandom random, ColorCMY original, float targetCyan, float targetMagenta, float targetYellow)
		{
			return new ColorCMY(
				random.RangeCC(original.c, targetCyan),
				random.RangeCC(original.m, targetMagenta),
				random.RangeCC(original.y, targetYellow),
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetCyan">The far end of the range within which the cyan channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetMagenta">The far end of the range within which the magenta channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetYellow">The far end of the range within which the yellow channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMY CMYALerp(this IRandom random, ColorCMY original, float targetCyan, float targetMagenta, float targetYellow, float targetAlpha)
		{
			return new ColorCMY(
				random.RangeCC(original.c, targetCyan),
				random.RangeCC(original.m, targetMagenta),
				random.RangeCC(original.y, targetYellow),
				random.RangeCC(original.a, targetAlpha));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMY CMYALerp(this IRandom random, ColorCMY original, ColorCMY target)
		{
			return new ColorCMY(
				random.RangeCC(original.c, target.c),
				random.RangeCC(original.m, target.m),
				random.RangeCC(original.y, target.y),
				random.RangeCC(original.a, target.a));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the intensity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static ColorCMY IntensityShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
		{
			float oldIntensity = (original.c + original.m + original.y) / 3f;
			float newIntensity = random.Shift(oldIntensity, maxAbsoluteShift);
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the intensity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the intensity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static ColorCMY IntensityShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			float oldIntensity = (original.c + original.m + original.y) / 3f;
			float newIntensity = random.Shift(oldIntensity, minShift, maxShift);
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static ColorCMY IntensitySpread(this IRandom random, ColorCMY original)
		{
			float oldIntensity = (original.c + original.m + original.y) / 3f;
			float newIntensity = random.FloatCC();
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the intensity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static ColorCMY IntensitySpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
		{
			float oldIntensity = (original.c + original.m + original.y) / 3f;
			float newIntensity = random.Spread(oldIntensity, maxAbsoluteSpread);
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the collective intensity of the color channels toward its minimum or maximum possible value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the intensity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the intensity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels proportionally randomized.</returns>
		public static ColorCMY IntensitySpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
		{
			float oldIntensity = (original.c + original.m + original.y) / 3f;
			float newIntensity = random.Spread(oldIntensity, minSpread, maxSpread);
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the collective intensity of the color channels toward the specified target by a random amount while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="target">The far end of the range within which the collective intensity of the color channels can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the collective intensity of the color channels randomized.</returns>
		public static ColorCMY IntensityLerp(this IRandom random, ColorCMY original, float target)
		{
			float oldIntensity = (original.c + original.m + original.y) / 3f;
			float newIntensity = Mathf.Lerp(oldIntensity, target, random.FloatCC());
			return ChangeIntensity(original, oldIntensity, newIntensity);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the collective intensity of the color channels toward the specified target by a random amount while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="target">The color whose color channels indicates the far end of the range within which the collective intensity of the color channels can change.</param>
		/// <returns>A color derived from the original color but with the collective intensity of the color channels randomized.</returns>
		public static ColorCMY IntensityLerp(this IRandom random, ColorCMY original, ColorCMY target)
		{
			return random.IntensityLerp(original, (target.c + target.m + target.y) / 3f);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the cyan channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the cyan channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY CyanShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
		{
			return new ColorCMY(random.Shift(original.c, maxAbsoluteShift), original.m, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the cyan channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the cyan channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the cyan channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY CyanShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			return new ColorCMY(random.Shift(original.c, minShift, maxShift), original.m, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the cyan channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY CyanSpread(this IRandom random, ColorCMY original)
		{
			return new ColorCMY(random.FloatCC(), original.m, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the cyan channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the cyan channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY CyanSpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
		{
			return new ColorCMY(random.Spread(original.c, maxAbsoluteSpread), original.m, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the cyan channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the cyan channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the cyan channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY CyanSpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
		{
			return new ColorCMY(random.Spread(original.c, minSpread, maxSpread), original.m, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the cyan channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="target">The far end of the range within which the cyan channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY CyanLerp(this IRandom random, ColorCMY original, float target)
		{
			return new ColorCMY(random.RangeCC(original.c, target), original.m, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the cyan channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="target">The color whose cyan channel indicates the far end of the range within which the cyan channel can change.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY CyanLerp(this IRandom random, ColorCMY original, ColorCMY target)
		{
			return new ColorCMY(random.RangeCC(original.c, target.c), original.m, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the magenta channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the magenta channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMY MagentaShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
		{
			return new ColorCMY(original.c, random.Shift(original.m, maxAbsoluteShift), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the magenta channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the magenta channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the magenta channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMY MagentaShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			return new ColorCMY(original.c, random.Shift(original.m, minShift, maxShift), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the magenta channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMY MagentaSpread(this IRandom random, ColorCMY original)
		{
			return new ColorCMY(original.c, random.FloatCC(), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the magenta channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the magenta channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMY MagentaSpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
		{
			return new ColorCMY(original.c, random.Spread(original.m, maxAbsoluteSpread), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the magenta channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the magenta channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the magenta channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMY MagentaSpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
		{
			return new ColorCMY(original.c, random.Spread(original.m, minSpread, maxSpread), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the magenta channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="target">The far end of the range within which the magenta channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMY MagentaLerp(this IRandom random, ColorCMY original, float target)
		{
			return new ColorCMY(original.c, random.RangeCC(original.m, target), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the magenta channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="target">The color whose magenta channel indicates the far end of the range within which the magenta channel can change.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMY MagentaLerp(this IRandom random, ColorCMY original, ColorCMY target)
		{
			return new ColorCMY(original.c, random.RangeCC(original.m, target.m), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the yellow channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the yellow channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMY YellowShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
		{
			return new ColorCMY(original.c, original.m, random.Shift(original.y, maxAbsoluteShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the yellow channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the yellow channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the yellow channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMY YellowShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			return new ColorCMY(original.c, original.m, random.Shift(original.y, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the yellow channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMY YellowSpread(this IRandom random, ColorCMY original)
		{
			return new ColorCMY(original.c, original.m, random.FloatCC(), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the yellow channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the yellow channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMY YellowSpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
		{
			return new ColorCMY(original.c, original.m, random.Spread(original.y, maxAbsoluteSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the yellow channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the yellow channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the yellow channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMY YellowSpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
		{
			return new ColorCMY(original.c, original.m, random.Spread(original.y, minSpread, maxSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the yellow channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="target">The far end of the range within which the yellow channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMY YellowLerp(this IRandom random, ColorCMY original, float target)
		{
			return new ColorCMY(original.c, original.m, random.RangeCC(original.y, target), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the yellow channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="target">The color whose yellow channel indicates the far end of the range within which the yellow channel can change.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMY YellowLerp(this IRandom random, ColorCMY original, ColorCMY target)
		{
			return new ColorCMY(original.c, original.m, random.RangeCC(original.y, target.y), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMY AlphaShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
		{
			return new ColorCMY(original.c, original.m, original.y, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMY AlphaShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			return new ColorCMY(original.c, original.m, original.y, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMY AlphaSpread(this IRandom random, ColorCMY original)
		{
			return new ColorCMY(original.c, original.m, original.y, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMY AlphaSpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
		{
			return new ColorCMY(original.c, original.m, original.y, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMY AlphaSpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
		{
			return new ColorCMY(original.c, original.m, original.y, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMY AlphaLerp(this IRandom random, ColorCMY original, float target)
		{
			return new ColorCMY(original.c, original.m, original.y, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMY AlphaLerp(this IRandom random, ColorCMY original, ColorCMY target)
		{
			return new ColorCMY(original.c, original.m, original.y, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region CMYK

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow/key color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorCMYK CMYK(this IRandom random)
		{
			return new ColorCMYK(random.FloatCC(), random.FloatCC(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow/key color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorCMYK CMYK(this IRandom random, float a)
		{
			return new ColorCMYK(random.FloatCC(), random.FloatCC(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow/key color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorCMYK CMYKA(this IRandom random)
		{
			return new ColorCMYK(random.FloatCC(), random.FloatCC(), random.FloatCC(), random.FloatCC(), random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteCyanShift">The largest amount by which the cyan channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteMagentaShift">The largest amount by which the magenta channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteYellowShift">The largest amount by which the yellow channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteKeyShift">The largest amount by which the key channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMYK CMYKShift(this IRandom random, ColorCMYK original, float maxAbsoluteCyanShift, float maxAbsoluteMagentaShift, float maxAbsoluteYellowShift, float maxAbsoluteKeyShift)
		{
			return new ColorCMYK(
				maxAbsoluteCyanShift != 0f ? random.Shift(original.c, maxAbsoluteCyanShift) : original.c,
				maxAbsoluteMagentaShift != 0f ? random.Shift(original.m, maxAbsoluteMagentaShift) : original.m,
				maxAbsoluteYellowShift != 0f ? random.Shift(original.y, maxAbsoluteYellowShift) : original.y,
				maxAbsoluteKeyShift != 0f ? random.Shift(original.k, maxAbsoluteKeyShift) : original.k,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteCyanShift">The largest amount by which the cyan channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteMagentaShift">The largest amount by which the magenta channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteYellowShift">The largest amount by which the yellow channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteKeyShift">The largest amount by which the key channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMYK CMYKAShift(this IRandom random, ColorCMYK original, float maxAbsoluteCyanShift, float maxAbsoluteMagentaShift, float maxAbsoluteYellowShift, float maxAbsoluteKeyShift, float maxAbsoluteAlphaShift)
		{
			return new ColorCMYK(
				maxAbsoluteCyanShift != 0f ? random.Shift(original.c, maxAbsoluteCyanShift) : original.c,
				maxAbsoluteMagentaShift != 0f ? random.Shift(original.m, maxAbsoluteMagentaShift) : original.m,
				maxAbsoluteYellowShift != 0f ? random.Shift(original.y, maxAbsoluteYellowShift) : original.y,
				maxAbsoluteKeyShift != 0f ? random.Shift(original.k, maxAbsoluteKeyShift) : original.k,
				maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minCyanShift">The minimum end of the range offset from the current value within which the cyan channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxCyanShift"/>.</param>
		/// <param name="maxCyanShift">The maximum end of the range offset from the current value within which the cyan channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minCyanShift"/>.</param>
		/// <param name="minMagentaShift">The minimum end of the range offset from the current value within which the magenta channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxMagentaShift"/>.</param>
		/// <param name="maxMagentaShift">The maximum end of the range offset from the current value within which the magenta channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minMagentaShift"/>.</param>
		/// <param name="minYellowShift">The minimum end of the range offset from the current value within which the yellow channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxYellowShift"/>.</param>
		/// <param name="maxYellowShift">The maximum end of the range offset from the current value within which the yellow channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minYellowShift"/>.</param>
		/// <param name="minKeyShift">The minimum end of the range offset from the current value within which the key channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxKeyShift"/>.</param>
		/// <param name="maxKeyShift">The maximum end of the range offset from the current value within which the key channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minKeyShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMYK CMYKShift(this IRandom random, ColorCMYK original, float minCyanShift, float maxCyanShift, float minMagentaShift, float maxMagentaShift, float minYellowShift, float maxYellowShift, float minKeyShift, float maxKeyShift)
		{
			return new ColorCMYK(
				minCyanShift != 0f || maxCyanShift != 0f ? random.Shift(original.c, minCyanShift, maxCyanShift) : original.c,
				minMagentaShift != 0f || maxMagentaShift != 0f ? random.Shift(original.m, minMagentaShift, maxMagentaShift) : original.m,
				minYellowShift != 0f || maxYellowShift != 0f ? random.Shift(original.y, minYellowShift, maxYellowShift) : original.y,
				minKeyShift != 0f || maxKeyShift != 0f ? random.Shift(original.k, minKeyShift, maxKeyShift) : original.k,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minCyanShift">The minimum end of the range offset from the current value within which the cyan channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxCyanShift"/>.</param>
		/// <param name="maxCyanShift">The maximum end of the range offset from the current value within which the cyan channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minCyanShift"/>.</param>
		/// <param name="minMagentaShift">The minimum end of the range offset from the current value within which the magenta channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxMagentaShift"/>.</param>
		/// <param name="maxMagentaShift">The maximum end of the range offset from the current value within which the magenta channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minMagentaShift"/>.</param>
		/// <param name="minYellowShift">The minimum end of the range offset from the current value within which the yellow channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxYellowShift"/>.</param>
		/// <param name="maxYellowShift">The maximum end of the range offset from the current value within which the yellow channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minYellowShift"/>.</param>
		/// <param name="minKeyShift">The minimum end of the range offset from the current value within which the key channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxKeyShift"/>.</param>
		/// <param name="maxKeyShift">The maximum end of the range offset from the current value within which the key channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minKeyShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMYK CMYKAShift(this IRandom random, ColorCMYK original, float minCyanShift, float maxCyanShift, float minMagentaShift, float maxMagentaShift, float minYellowShift, float maxYellowShift, float minKeyShift, float maxKeyShift, float minAlphaShift, float maxAlphaShift)
		{
			return new ColorCMYK(
				minCyanShift != 0f || maxCyanShift != 0f ? random.Shift(original.c, minCyanShift, maxCyanShift) : original.c,
				minMagentaShift != 0f || maxMagentaShift != 0f ? random.Shift(original.m, minMagentaShift, maxMagentaShift) : original.m,
				minYellowShift != 0f || maxYellowShift != 0f ? random.Shift(original.y, minYellowShift, maxYellowShift) : original.y,
				minKeyShift != 0f || maxKeyShift != 0f ? random.Shift(original.k, minKeyShift, maxKeyShift) : original.k,
				minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteCyanSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the cyan channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteMagentaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the magenta channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteYellowSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the yellow channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteKeySpread">The largest proportion from the current value toward the minimum or maximum possible value by which the key channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMYK CMYKSpread(this IRandom random, ColorCMYK original, float maxAbsoluteCyanSpread, float maxAbsoluteMagentaSpread, float maxAbsoluteYellowSpread, float maxAbsoluteKeySpread)
		{
			return new ColorCMYK(
				maxAbsoluteCyanSpread != 0f ? random.Spread(original.c, maxAbsoluteCyanSpread) : original.c,
				maxAbsoluteMagentaSpread != 0f ? random.Spread(original.m, maxAbsoluteMagentaSpread) : original.m,
				maxAbsoluteYellowSpread != 0f ? random.Spread(original.y, maxAbsoluteYellowSpread) : original.y,
				maxAbsoluteKeySpread != 0f ? random.Spread(original.k, maxAbsoluteKeySpread) : original.k,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteCyanSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the cyan channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteMagentaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the magenta channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteYellowSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the yellow channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteKeySpread">The largest proportion from the current value toward the minimum or maximum possible value by which the key channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMYK CMYKASpread(this IRandom random, ColorCMYK original, float maxAbsoluteCyanSpread, float maxAbsoluteMagentaSpread, float maxAbsoluteYellowSpread, float maxAbsoluteKeySpread, float maxAbsoluteAlphaSpread)
		{
			return new ColorCMYK(
				maxAbsoluteCyanSpread != 0f ? random.Spread(original.c, maxAbsoluteCyanSpread) : original.c,
				maxAbsoluteMagentaSpread != 0f ? random.Spread(original.m, maxAbsoluteMagentaSpread) : original.m,
				maxAbsoluteYellowSpread != 0f ? random.Spread(original.y, maxAbsoluteYellowSpread) : original.y,
				maxAbsoluteKeySpread != 0f ? random.Spread(original.k, maxAbsoluteKeySpread) : original.k,
				maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minCyanSpread">The minimum end of the proportional range within which the cyan channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxCyanSpread"/>.</param>
		/// <param name="maxCyanSpread">The maximum end of the proportional range within which the cyan channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minCyanSpread"/>.</param>
		/// <param name="minMagentaSpread">The minimum end of the proportional range within which the magenta channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxMagentaSpread"/>.</param>
		/// <param name="maxMagentaSpread">The maximum end of the proportional range within which the magenta channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minMagentaSpread"/>.</param>
		/// <param name="minYellowSpread">The minimum end of the proportional range within which the yellow channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxYellowSpread"/>.</param>
		/// <param name="maxYellowSpread">The maximum end of the proportional range within which the yellow channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minYellowSpread"/>.</param>
		/// <param name="minKeySpread">The minimum end of the proportional range within which the key channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxKeySpread"/>.</param>
		/// <param name="maxKeySpread">The maximum end of the proportional range within which the key channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minKeySpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMYK CMYKSpread(this IRandom random, ColorCMYK original, float minCyanSpread, float maxCyanSpread, float minMagentaSpread, float maxMagentaSpread, float minYellowSpread, float maxYellowSpread, float minKeySpread, float maxKeySpread)
		{
			return new ColorCMYK(
				minCyanSpread != 0f || maxCyanSpread != 0f ? random.Spread(original.c, minCyanSpread, maxCyanSpread) : original.c,
				minMagentaSpread != 0f || maxMagentaSpread != 0f ? random.Spread(original.m, minMagentaSpread, maxMagentaSpread) : original.m,
				minYellowSpread != 0f || maxYellowSpread != 0f ? random.Spread(original.y, minYellowSpread, maxYellowSpread) : original.y,
				minKeySpread != 0f || maxKeySpread != 0f ? random.Spread(original.k, minKeySpread, maxKeySpread) : original.k,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minCyanSpread">The minimum end of the proportional range within which the cyan channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxCyanSpread"/>.</param>
		/// <param name="maxCyanSpread">The maximum end of the proportional range within which the cyan channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minCyanSpread"/>.</param>
		/// <param name="minMagentaSpread">The minimum end of the proportional range within which the magenta channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxMagentaSpread"/>.</param>
		/// <param name="maxMagentaSpread">The maximum end of the proportional range within which the magenta channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minMagentaSpread"/>.</param>
		/// <param name="minYellowSpread">The minimum end of the proportional range within which the yellow channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxYellowSpread"/>.</param>
		/// <param name="maxYellowSpread">The maximum end of the proportional range within which the yellow channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minYellowSpread"/>.</param>
		/// <param name="minKeySpread">The minimum end of the proportional range within which the key channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxKeySpread"/>.</param>
		/// <param name="maxKeySpread">The maximum end of the proportional range within which the key channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minKeySpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMYK CMYKASpread(this IRandom random, ColorCMYK original, float minCyanSpread, float maxCyanSpread, float minMagentaSpread, float maxMagentaSpread, float minYellowSpread, float maxYellowSpread, float minKeySpread, float maxKeySpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return new ColorCMYK(
				minCyanSpread != 0f || maxCyanSpread != 0f ? random.Spread(original.c, minCyanSpread, maxCyanSpread) : original.c,
				minMagentaSpread != 0f || maxMagentaSpread != 0f ? random.Spread(original.m, minMagentaSpread, maxMagentaSpread) : original.m,
				minYellowSpread != 0f || maxYellowSpread != 0f ? random.Spread(original.y, minYellowSpread, maxYellowSpread) : original.y,
				minKeySpread != 0f || maxKeySpread != 0f ? random.Spread(original.k, minKeySpread, maxKeySpread) : original.k,
				minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetCyan">The far end of the range within which the cyan channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetMagenta">The far end of the range within which the magenta channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetYellow">The far end of the range within which the yellow channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetKey">The far end of the range within which the key channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorCMYK CMYKLerp(this IRandom random, ColorCMYK original, float targetCyan, float targetMagenta, float targetYellow, float targetKey)
		{
			return new ColorCMYK(
				random.RangeCC(original.c, targetCyan),
				random.RangeCC(original.m, targetMagenta),
				random.RangeCC(original.y, targetYellow),
				random.RangeCC(original.k, targetKey),
				original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetCyan">The far end of the range within which the cyan channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetMagenta">The far end of the range within which the magenta channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetYellow">The far end of the range within which the yellow channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetKey">The far end of the range within which the key channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMYK CMYKALerp(this IRandom random, ColorCMYK original, float targetCyan, float targetMagenta, float targetYellow, float targetKey, float targetAlpha)
		{
			return new ColorCMYK(
				random.RangeCC(original.c, targetCyan),
				random.RangeCC(original.m, targetMagenta),
				random.RangeCC(original.y, targetYellow),
				random.RangeCC(original.k, targetKey),
				random.RangeCC(original.a, targetAlpha));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorCMYK CMYKALerp(this IRandom random, ColorCMYK original, ColorCMYK target)
		{
			return new ColorCMYK(
				random.RangeCC(original.c, target.c),
				random.RangeCC(original.m, target.m),
				random.RangeCC(original.y, target.y),
				random.RangeCC(original.k, target.k),
				random.RangeCC(original.a, target.a));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the cyan channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the cyan channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMYK CyanShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
		{
			return new ColorCMYK(random.Shift(original.c, maxAbsoluteShift), original.m, original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the cyan channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the cyan channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the cyan channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMYK CyanShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(random.Shift(original.c, minShift, maxShift), original.m, original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the cyan channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMYK CyanSpread(this IRandom random, ColorCMYK original)
		{
			return new ColorCMYK(random.FloatCC(), original.m, original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the cyan channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the cyan channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMYK CyanSpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
		{
			return new ColorCMYK(random.Spread(original.c, maxAbsoluteSpread), original.m, original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the cyan channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the cyan channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the cyan channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMYK CyanSpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
		{
			return new ColorCMYK(random.Spread(original.c, minSpread, maxSpread), original.m, original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the cyan channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="target">The far end of the range within which the cyan channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMYK CyanLerp(this IRandom random, ColorCMYK original, float target)
		{
			return new ColorCMYK(random.RangeCC(original.c, target), original.m, original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the cyan channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="target">The color whose cyan channel indicates the far end of the range within which the cyan channel can change.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMYK CyanLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
		{
			return new ColorCMYK(random.RangeCC(original.c, target.c), original.m, original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the magenta channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the magenta channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMYK MagentaShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
		{
			return new ColorCMYK(original.c, random.Shift(original.m, maxAbsoluteShift), original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the magenta channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the magenta channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the magenta channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMYK MagentaShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(original.c, random.Shift(original.m, minShift, maxShift), original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the magenta channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMYK MagentaSpread(this IRandom random, ColorCMYK original)
		{
			return new ColorCMYK(original.c, random.FloatCC(), original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the magenta channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the magenta channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMYK MagentaSpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
		{
			return new ColorCMYK(original.c, random.Spread(original.m, maxAbsoluteSpread), original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the magenta channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the magenta channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the magenta channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMYK MagentaSpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
		{
			return new ColorCMYK(original.c, random.Spread(original.m, minSpread, maxSpread), original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the magenta channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="target">The far end of the range within which the magenta channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMYK MagentaLerp(this IRandom random, ColorCMYK original, float target)
		{
			return new ColorCMYK(original.c, random.RangeCC(original.m, target), original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the magenta channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <param name="target">The color whose magenta channel indicates the far end of the range within which the magenta channel can change.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMYK MagentaLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
		{
			return new ColorCMYK(original.c, random.RangeCC(original.m, target.m), original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the yellow channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the yellow channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMYK YellowShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
		{
			return new ColorCMYK(original.c, original.m, random.Shift(original.y, maxAbsoluteShift), original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the yellow channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the yellow channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the yellow channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMYK YellowShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(original.c, original.m, random.Shift(original.y, minShift, maxShift), original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the yellow channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMYK YellowSpread(this IRandom random, ColorCMYK original)
		{
			return new ColorCMYK(original.c, original.m, random.FloatCC(), original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the yellow channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the yellow channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMYK YellowSpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
		{
			return new ColorCMYK(original.c, original.m, random.Spread(original.y, maxAbsoluteSpread), original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the yellow channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the yellow channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the yellow channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMYK YellowSpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
		{
			return new ColorCMYK(original.c, original.m, random.Spread(original.y, minSpread, maxSpread), original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the yellow channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="target">The far end of the range within which the yellow channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMYK YellowLerp(this IRandom random, ColorCMYK original, float target)
		{
			return new ColorCMYK(original.c, original.m, random.RangeCC(original.y, target), original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the yellow channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <param name="target">The color whose yellow channel indicates the far end of the range within which the yellow channel can change.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMYK YellowLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
		{
			return new ColorCMYK(original.c, original.m, random.RangeCC(original.y, target.y), original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the key channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose key channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the key channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the key channel randomized.</returns>
		public static ColorCMYK KeyShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Shift(original.k, maxAbsoluteShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the key channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose key channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the key channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the key channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the key channel randomized.</returns>
		public static ColorCMYK KeyShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Shift(original.k, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the key channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose key channel will be altered.</param>
		/// <returns>A color derived from the original color but with the key channel randomized.</returns>
		public static ColorCMYK KeySpread(this IRandom random, ColorCMYK original)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.FloatCC(), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the key channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose key channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the key channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the key channel randomized.</returns>
		public static ColorCMYK KeySpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Spread(original.k, maxAbsoluteSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the key channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose key channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the key channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the key channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the key channel randomized.</returns>
		public static ColorCMYK KeySpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Spread(original.k, minSpread, maxSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the key channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose key channel will be altered.</param>
		/// <param name="target">The far end of the range within which the key channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the key channel randomized.</returns>
		public static ColorCMYK KeyLerp(this IRandom random, ColorCMYK original, float target)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.RangeCC(original.k, target), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the key channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose key channel will be altered.</param>
		/// <param name="target">The color whose key channel indicates the far end of the range within which the key channel can change.</param>
		/// <returns>A color derived from the original color but with the key channel randomized.</returns>
		public static ColorCMYK KeyLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.RangeCC(original.k, target.k), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMYK AlphaShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMYK AlphaShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMYK AlphaSpread(this IRandom random, ColorCMYK original)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMYK AlphaSpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMYK AlphaSpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMYK AlphaLerp(this IRandom random, ColorCMYK original, float target)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMYK AlphaLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region HSV

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/value color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHSV HSV(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/value color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHSV HSV(this IRandom random, float a)
		{
			return new ColorHSV(random.FloatCO(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/value color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHSV HSVA(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.FloatCC(), random.FloatCC(), random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteSaturationShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteValueShift">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSV HSVShift(this IRandom random, ColorHSV original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteValueShift)
		{
			return new ColorHSV(
				maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
				maxAbsoluteSaturationShift != 0f ? random.Shift(original.s, maxAbsoluteSaturationShift) : original.s,
				maxAbsoluteValueShift != 0f ? random.Shift(original.v, maxAbsoluteValueShift) : original.v,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteSaturationShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteValueShift">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSV HSVAShift(this IRandom random, ColorHSV original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteValueShift, float maxAbsoluteAlphaShift)
		{
			return new ColorHSV(
				maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
				maxAbsoluteSaturationShift != 0f ? random.Shift(original.s, maxAbsoluteSaturationShift) : original.s,
				maxAbsoluteValueShift != 0f ? random.Shift(original.v, maxAbsoluteValueShift) : original.v,
				maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minSaturationShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxSaturationShift"/>.</param>
		/// <param name="maxSaturationShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minSaturationShift"/>.</param>
		/// <param name="minValueShift">The minimum end of the range offset from the current value within which the value channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxValueShift"/>.</param>
		/// <param name="maxValueShift">The maximum end of the range offset from the current value within which the value channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minValueShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSV HSVShift(this IRandom random, ColorHSV original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minValueShift, float maxValueShift)
		{
			return new ColorHSV(
				minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
				minSaturationShift != 0f || maxSaturationShift != 0f ? random.Shift(original.s, minSaturationShift, maxSaturationShift) : original.s,
				minValueShift != 0f || maxValueShift != 0f ? random.Shift(original.v, minValueShift, maxValueShift) : original.v,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minSaturationShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxSaturationShift"/>.</param>
		/// <param name="maxSaturationShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minSaturationShift"/>.</param>
		/// <param name="minValueShift">The minimum end of the range offset from the current value within which the value channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxValueShift"/>.</param>
		/// <param name="maxValueShift">The maximum end of the range offset from the current value within which the value channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minValueShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSV HSVAShift(this IRandom random, ColorHSV original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minValueShift, float maxValueShift, float minAlphaShift, float maxAlphaShift)
		{
			return new ColorHSV(
				minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
				minSaturationShift != 0f || maxSaturationShift != 0f ? random.Shift(original.s, minSaturationShift, maxSaturationShift) : original.s,
				minValueShift != 0f || maxValueShift != 0f ? random.Shift(original.v, minValueShift, maxValueShift) : original.v,
				minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteSaturationSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteValueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the value channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSV HSVSpread(this IRandom random, ColorHSV original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteValueSpread)
		{
			return new ColorHSV(
				maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
				maxAbsoluteSaturationSpread != 0f ? random.Spread(original.s, maxAbsoluteSaturationSpread) : original.s,
				maxAbsoluteValueSpread != 0f ? random.Spread(original.v, maxAbsoluteValueSpread) : original.v,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteSaturationSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteValueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the value channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSV HSVASpread(this IRandom random, ColorHSV original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteValueSpread, float maxAbsoluteAlphaSpread)
		{
			return new ColorHSV(
				maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
				maxAbsoluteSaturationSpread != 0f ? random.Spread(original.s, maxAbsoluteSaturationSpread) : original.s,
				maxAbsoluteValueSpread != 0f ? random.Spread(original.v, maxAbsoluteValueSpread) : original.v,
				maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minSaturationSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSaturationSpread"/>.</param>
		/// <param name="maxSaturationSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSaturationSpread"/>.</param>
		/// <param name="minValueSpread">The minimum end of the proportional range within which the value channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxValueSpread"/>.</param>
		/// <param name="maxValueSpread">The maximum end of the proportional range within which the value channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minValueSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSV HSVSpread(this IRandom random, ColorHSV original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minValueSpread, float maxValueSpread)
		{
			return new ColorHSV(
				minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
				minSaturationSpread != 0f || maxSaturationSpread != 0f ? random.Spread(original.s, minSaturationSpread, maxSaturationSpread) : original.s,
				minValueSpread != 0f || maxValueSpread != 0f ? random.Spread(original.v, minValueSpread, maxValueSpread) : original.v,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minSaturationSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSaturationSpread"/>.</param>
		/// <param name="maxSaturationSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSaturationSpread"/>.</param>
		/// <param name="minValueSpread">The minimum end of the proportional range within which the value channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxValueSpread"/>.</param>
		/// <param name="maxValueSpread">The maximum end of the proportional range within which the value channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minValueSpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSV HSVASpread(this IRandom random, ColorHSV original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minValueSpread, float maxValueSpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return new ColorHSV(
				minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
				minSaturationSpread != 0f || maxSaturationSpread != 0f ? random.Spread(original.s, minSaturationSpread, maxSaturationSpread) : original.s,
				minValueSpread != 0f || maxValueSpread != 0f ? random.Spread(original.v, minValueSpread, maxValueSpread) : original.v,
				minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetSaturation">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetValue">The far end of the range within which the value channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSV HSVLerp(this IRandom random, ColorHSV original, float targetHue, float targetSaturation, float targetValue)
		{
			return new ColorHSV(
				random.LerpRepeated(original.h, targetHue),
				random.RangeCC(original.s, targetSaturation),
				random.RangeCC(original.v, targetValue),
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetSaturation">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetValue">The far end of the range within which the value channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSV HSVALerp(this IRandom random, ColorHSV original, float targetHue, float targetSaturation, float targetValue, float targetAlpha)
		{
			return new ColorHSV(
				random.LerpRepeated(original.h, targetHue),
				random.RangeCC(original.s, targetSaturation),
				random.RangeCC(original.v, targetValue),
				random.RangeCC(original.a, targetAlpha));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSV HSVALerp(this IRandom random, ColorHSV original, ColorHSV target)
		{
			return new ColorHSV(
				random.LerpRepeated(original.h, target.h),
				random.RangeCC(original.s, target.s),
				random.RangeCC(original.v, target.v),
				random.RangeCC(original.a, target.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSV HueShift(this IRandom random, ColorHSV original, float maxAbsoluteShift)
		{
			return new ColorHSV(random.ShiftRepeated(original.h, maxAbsoluteShift), original.s, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSV HueShift(this IRandom random, ColorHSV original, float minShift, float maxShift)
		{
			return new ColorHSV(random.ShiftRepeated(original.h, minShift, maxShift), original.s, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSV HueSpread(this IRandom random, ColorHSV original)
		{
			return new ColorHSV(random.FloatCO(), original.s, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSV HueSpread(this IRandom random, ColorHSV original, float maxAbsoluteSpread)
		{
			return new ColorHSV(random.SpreadRepeated(original.h, maxAbsoluteSpread), original.s, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSV HueSpread(this IRandom random, ColorHSV original, float minSpread, float maxSpread)
		{
			return new ColorHSV(random.SpreadRepeated(original.h, minSpread, maxSpread), original.s, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSV HueLerp(this IRandom random, ColorHSV original, float target)
		{
			return new ColorHSV(random.LerpRepeated(original.h, target), original.s, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The color whose hue channel indicates the far end of the range within which the hue channel can change.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSV HueLerp(this IRandom random, ColorHSV original, ColorHSV target)
		{
			return new ColorHSV(random.LerpRepeated(original.h, target.h), original.s, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for the saturation channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSV SaturationShift(this IRandom random, ColorHSV original, float maxAbsoluteShift)
		{
			return new ColorHSV(original.h, random.Shift(original.s, maxAbsoluteShift), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for the saturation channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSV SaturationShift(this IRandom random, ColorHSV original, float minShift, float maxShift)
		{
			return new ColorHSV(original.h, random.Shift(original.s, minShift, maxShift), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSV SaturationSpread(this IRandom random, ColorHSV original)
		{
			return new ColorHSV(original.h, random.FloatCC(), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSV SaturationSpread(this IRandom random, ColorHSV original, float maxAbsoluteSpread)
		{
			return new ColorHSV(original.h, random.Spread(original.s, maxAbsoluteSpread), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSV SaturationSpread(this IRandom random, ColorHSV original, float minSpread, float maxSpread)
		{
			return new ColorHSV(original.h, random.Spread(original.s, minSpread, maxSpread), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the saturation channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="target">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSV SaturationLerp(this IRandom random, ColorHSV original, float target)
		{
			return new ColorHSV(original.h, random.RangeCC(original.s, target), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the saturation channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="target">The color whose saturation channel indicates the far end of the range within which the saturation channel can change.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSV SaturationLerp(this IRandom random, ColorHSV original, ColorHSV target)
		{
			return new ColorHSV(original.h, random.RangeCC(original.s, target.s), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for the value channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHSV ValueShift(this IRandom random, ColorHSV original, float maxAbsoluteShift)
		{
			return new ColorHSV(original.h, original.s, random.Shift(original.v, maxAbsoluteShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for the value channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the value channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the value channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHSV ValueShift(this IRandom random, ColorHSV original, float minShift, float maxShift)
		{
			return new ColorHSV(original.h, original.s, random.Shift(original.v, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHSV ValueSpread(this IRandom random, ColorHSV original)
		{
			return new ColorHSV(original.h, original.s, random.FloatCC(), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the value channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHSV ValueSpread(this IRandom random, ColorHSV original, float maxAbsoluteSpread)
		{
			return new ColorHSV(original.h, original.s, random.Spread(original.v, maxAbsoluteSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the value channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the value channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHSV ValueSpread(this IRandom random, ColorHSV original, float minSpread, float maxSpread)
		{
			return new ColorHSV(original.h, original.s, random.Spread(original.v, minSpread, maxSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the value channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="target">The far end of the range within which the value channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHSV ValueLerp(this IRandom random, ColorHSV original, float target)
		{
			return new ColorHSV(original.h, original.s, random.RangeCC(original.v, target), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the value channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="target">The color whose value channel indicates the far end of the range within which the value channel can change.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHSV ValueLerp(this IRandom random, ColorHSV original, ColorHSV target)
		{
			return new ColorHSV(original.h, original.s, random.RangeCC(original.v, target.v), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSV AlphaShift(this IRandom random, ColorHSV original, float maxAbsoluteShift)
		{
			return new ColorHSV(original.h, original.s, original.v, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSV AlphaShift(this IRandom random, ColorHSV original, float minShift, float maxShift)
		{
			return new ColorHSV(original.h, original.s, original.v, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSV AlphaSpread(this IRandom random, ColorHSV original)
		{
			return new ColorHSV(original.h, original.s, original.v, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSV AlphaSpread(this IRandom random, ColorHSV original, float maxAbsoluteSpread)
		{
			return new ColorHSV(original.h, original.s, original.v, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSV AlphaSpread(this IRandom random, ColorHSV original, float minSpread, float maxSpread)
		{
			return new ColorHSV(original.h, original.s, original.v, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSV AlphaLerp(this IRandom random, ColorHSV original, float target)
		{
			return new ColorHSV(original.h, original.s, original.v, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSV AlphaLerp(this IRandom random, ColorHSV original, ColorHSV target)
		{
			return new ColorHSV(original.h, original.s, original.v, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region HCV

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/value color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHCV HCV(this IRandom random)
		{
			return random.HCV(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/value color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHCV HCV(this IRandom random, float a)
		{
			float hue = random.FloatCO();
			Vector2 chromaValue = random.PointWithinTriangle(new Vector2(1f, ColorHCV.GetValueAtMaxChroma()), new Vector2(0f, 1f));
			return new ColorHCV(hue, chromaValue.x, chromaValue.y, a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/value color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHCV HCVA(this IRandom random)
		{
			return random.HCV(random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteChromaShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteValueShift">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCV HCVShift(this IRandom random, ColorHCV original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteValueShift)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
					maxAbsoluteChromaShift != 0f ? random.Shift(original.c, maxAbsoluteChromaShift) : original.c,
					maxAbsoluteValueShift != 0f ? random.Shift(original.v, maxAbsoluteValueShift) : original.v,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteChromaShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteValueShift">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCV HCVAShift(this IRandom random, ColorHCV original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteValueShift, float maxAbsoluteAlphaShift)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
					maxAbsoluteChromaShift != 0f ? random.Shift(original.c, maxAbsoluteChromaShift) : original.c,
					maxAbsoluteValueShift != 0f ? random.Shift(original.v, maxAbsoluteValueShift) : original.v,
					maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minChromaShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxChromaShift"/>.</param>
		/// <param name="maxChromaShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minChromaShift"/>.</param>
		/// <param name="minValueShift">The minimum end of the range offset from the current value within which the value channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxValueShift"/>.</param>
		/// <param name="maxValueShift">The maximum end of the range offset from the current value within which the value channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minValueShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCV HCVShift(this IRandom random, ColorHCV original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minValueShift, float maxValueShift)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
					minChromaShift != 0f || maxChromaShift != 0f ? random.Shift(original.c, minChromaShift, maxChromaShift) : original.c,
					minValueShift != 0f || maxValueShift != 0f ? random.Shift(original.v, minValueShift, maxValueShift) : original.v,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minChromaShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxChromaShift"/>.</param>
		/// <param name="maxChromaShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minChromaShift"/>.</param>
		/// <param name="minValueShift">The minimum end of the range offset from the current value within which the value channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxValueShift"/>.</param>
		/// <param name="maxValueShift">The maximum end of the range offset from the current value within which the value channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minValueShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCV HCVAShift(this IRandom random, ColorHCV original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minValueShift, float maxValueShift, float minAlphaShift, float maxAlphaShift)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
					minChromaShift != 0f || maxChromaShift != 0f ? random.Shift(original.c, minChromaShift, maxChromaShift) : original.c,
					minValueShift != 0f || maxValueShift != 0f ? random.Shift(original.v, minValueShift, maxValueShift) : original.v,
					minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteChromaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteValueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the value channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCV HCVSpread(this IRandom random, ColorHCV original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteValueSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
					maxAbsoluteChromaSpread != 0f ? random.Spread(original.c, maxAbsoluteChromaSpread) : original.c,
					maxAbsoluteValueSpread != 0f ? random.Spread(original.v, maxAbsoluteValueSpread) : original.v,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteChromaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteValueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the value channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCV HCVASpread(this IRandom random, ColorHCV original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteValueSpread, float maxAbsoluteAlphaSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
					maxAbsoluteChromaSpread != 0f ? random.Spread(original.c, maxAbsoluteChromaSpread) : original.c,
					maxAbsoluteValueSpread != 0f ? random.Spread(original.v, maxAbsoluteValueSpread) : original.v,
					maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minChromaSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxChromaSpread"/>.</param>
		/// <param name="maxChromaSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minChromaSpread"/>.</param>
		/// <param name="minValueSpread">The minimum end of the proportional range within which the value channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxValueSpread"/>.</param>
		/// <param name="maxValueSpread">The maximum end of the proportional range within which the value channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minValueSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCV HCVSpread(this IRandom random, ColorHCV original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minValueSpread, float maxValueSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
					minChromaSpread != 0f || maxChromaSpread != 0f ? random.Spread(original.c, minChromaSpread, maxChromaSpread) : original.c,
					minValueSpread != 0f || maxValueSpread != 0f ? random.Spread(original.v, minValueSpread, maxValueSpread) : original.v,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minChromaSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxChromaSpread"/>.</param>
		/// <param name="maxChromaSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minChromaSpread"/>.</param>
		/// <param name="minValueSpread">The minimum end of the proportional range within which the value channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxValueSpread"/>.</param>
		/// <param name="maxValueSpread">The maximum end of the proportional range within which the value channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minValueSpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCV HCVASpread(this IRandom random, ColorHCV original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minValueSpread, float maxValueSpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
					minChromaSpread != 0f || maxChromaSpread != 0f ? random.Spread(original.c, minChromaSpread, maxChromaSpread) : original.c,
					minValueSpread != 0f || maxValueSpread != 0f ? random.Spread(original.v, minValueSpread, maxValueSpread) : original.v,
					minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetChroma">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetValue">The far end of the range within which the value channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCV HCVLerp(this IRandom random, ColorHCV original, float targetHue, float targetChroma, float targetValue)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					random.LerpRepeated(original.h, targetHue),
					random.RangeCC(original.c, targetChroma),
					random.RangeCC(original.v, targetValue),
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetChroma">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetValue">The far end of the range within which the value channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCV HCVALerp(this IRandom random, ColorHCV original, float targetHue, float targetChroma, float targetValue, float targetAlpha)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					random.LerpRepeated(original.h, targetHue),
					random.RangeCC(original.c, targetChroma),
					random.RangeCC(original.v, targetValue),
					random.RangeCC(original.a, targetAlpha));
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCV HCVALerp(this IRandom random, ColorHCV original, ColorHCV target)
		{
			return Change(original, () =>
			{
				return new ColorHCV(
					random.LerpRepeated(original.h, target.h),
					random.RangeCC(original.c, target.c),
					random.RangeCC(original.v, target.v),
					random.RangeCC(original.a, target.a));
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCV HueShift(this IRandom random, ColorHCV original, float maxAbsoluteShift)
		{
			return new ColorHCV(random.ShiftRepeated(original.h, maxAbsoluteShift), original.c, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCV HueShift(this IRandom random, ColorHCV original, float minShift, float maxShift)
		{
			return new ColorHCV(random.ShiftRepeated(original.h, minShift, maxShift), original.c, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCV HueSpread(this IRandom random, ColorHCV original)
		{
			return new ColorHCV(random.FloatCO(), original.c, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCV HueSpread(this IRandom random, ColorHCV original, float maxAbsoluteSpread)
		{
			return new ColorHCV(random.SpreadRepeated(original.h, maxAbsoluteSpread), original.c, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCV HueSpread(this IRandom random, ColorHCV original, float minSpread, float maxSpread)
		{
			return new ColorHCV(random.SpreadRepeated(original.h, minSpread, maxSpread), original.c, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCV HueLerp(this IRandom random, ColorHCV original, float target)
		{
			return new ColorHCV(random.LerpRepeated(original.h, target), original.c, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The color whose hue channel indicates the far end of the range within which the hue channel can change.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCV HueLerp(this IRandom random, ColorHCV original, ColorHCV target)
		{
			return new ColorHCV(random.LerpRepeated(original.h, target.h), original.c, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ChromaShift(this IRandom random, ColorHCV original, float maxAbsoluteShift)
		{
			return new ColorHCV(original.h, random.ShiftClamped(original.c, maxAbsoluteShift, 0f, ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ChromaShift(this IRandom random, ColorHCV original, float minShift, float maxShift)
		{
			return new ColorHCV(original.h, random.ShiftClamped(original.c, minShift, maxShift, 0f, ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ChromaSpread(this IRandom random, ColorHCV original)
		{
			return new ColorHCV(original.h, random.RangeCC(0f, ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ChromaSpread(this IRandom random, ColorHCV original, float maxAbsoluteSpread)
		{
			return new ColorHCV(original.h, random.SpreadClamped(original.c, maxAbsoluteSpread, 0f, ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ChromaSpread(this IRandom random, ColorHCV original, float minSpread, float maxSpread)
		{
			return new ColorHCV(original.h, random.SpreadClamped(original.c, minSpread, maxSpread, 0f, ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ChromaLerp(this IRandom random, ColorHCV original, float target)
		{
			return new ColorHCV(original.h, random.RangeCC(original.c, Mathf.Min(target, ColorHCV.GetMaxChroma(original.v))), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The color whose chroma channel indicates the far end of the range within which the chroma channel can change.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ChromaLerp(this IRandom random, ColorHCV original, ColorHCV target)
		{
			return new ColorHCV(original.h, random.RangeCC(original.c, Mathf.Min(target.c, ColorHCV.GetMaxChroma(original.v))), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the value channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ValueShift(this IRandom random, ColorHCV original, float maxAbsoluteShift)
		{
			float lMin, lMax;
			ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.ShiftClamped(original.v, maxAbsoluteShift, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the value channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the value channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the value channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ValueShift(this IRandom random, ColorHCV original, float minShift, float maxShift)
		{
			float lMin, lMax;
			ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.ShiftClamped(original.v, minShift, maxShift, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ValueSpread(this IRandom random, ColorHCV original)
		{
			float lMin, lMax;
			ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.RangeCC(lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the value channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ValueSpread(this IRandom random, ColorHCV original, float maxAbsoluteSpread)
		{
			float lMin, lMax;
			ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.SpreadClamped(original.v, maxAbsoluteSpread, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the value channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the value channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ValueSpread(this IRandom random, ColorHCV original, float minSpread, float maxSpread)
		{
			float lMin, lMax;
			ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.SpreadClamped(original.v, minSpread, maxSpread, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the value channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="target">The far end of the range within which the value channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ValueLerp(this IRandom random, ColorHCV original, float target)
		{
			float lMin, lMax;
			ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.RangeCC(Mathf.Max(lMin, original.v), Mathf.Min(target, lMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the value channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="target">The color whose value channel indicates the far end of the range within which the value channel can change.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ValueLerp(this IRandom random, ColorHCV original, ColorHCV target)
		{
			float lMin, lMax;
			ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.RangeCC(Mathf.Max(lMin, original.v), Mathf.Min(target.v, lMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV AlphaShift(this IRandom random, ColorHCV original, float maxAbsoluteShift)
		{
			return new ColorHCV(original.h, original.c, original.v, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV AlphaShift(this IRandom random, ColorHCV original, float minShift, float maxShift)
		{
			return new ColorHCV(original.h, original.c, original.v, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV AlphaSpread(this IRandom random, ColorHCV original)
		{
			return new ColorHCV(original.h, original.c, original.v, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV AlphaSpread(this IRandom random, ColorHCV original, float maxAbsoluteSpread)
		{
			return new ColorHCV(original.h, original.c, original.v, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV AlphaSpread(this IRandom random, ColorHCV original, float minSpread, float maxSpread)
		{
			return new ColorHCV(original.h, original.c, original.v, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV AlphaLerp(this IRandom random, ColorHCV original, float target)
		{
			return new ColorHCV(original.h, original.c, original.v, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV AlphaLerp(this IRandom random, ColorHCV original, ColorHCV target)
		{
			return new ColorHCV(original.h, original.c, original.v, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region HSL

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/lightness color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHSL HSL(this IRandom random)
		{
			return new ColorHSL(random.FloatCO(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/lightness color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHSL HSL(this IRandom random, float a)
		{
			return new ColorHSL(random.FloatCO(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/lightness color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHSL HSLA(this IRandom random)
		{
			return new ColorHSL(random.FloatCO(), random.FloatCC(), random.FloatCC(), random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteSaturationShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteLightnessShift">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSL HSLShift(this IRandom random, ColorHSL original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteLightnessShift)
		{
			return new ColorHSL(
				maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
				maxAbsoluteSaturationShift != 0f ? random.Shift(original.s, maxAbsoluteSaturationShift) : original.s,
				maxAbsoluteLightnessShift != 0f ? random.Shift(original.l, maxAbsoluteLightnessShift) : original.l,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteSaturationShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteLightnessShift">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSL HSLAShift(this IRandom random, ColorHSL original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteLightnessShift, float maxAbsoluteAlphaShift)
		{
			return new ColorHSL(
				maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
				maxAbsoluteSaturationShift != 0f ? random.Shift(original.s, maxAbsoluteSaturationShift) : original.s,
				maxAbsoluteLightnessShift != 0f ? random.Shift(original.l, maxAbsoluteLightnessShift) : original.l,
				maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minSaturationShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxSaturationShift"/>.</param>
		/// <param name="maxSaturationShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minSaturationShift"/>.</param>
		/// <param name="minLightnessShift">The minimum end of the range offset from the current value within which the lightness channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxLightnessShift"/>.</param>
		/// <param name="maxLightnessShift">The maximum end of the range offset from the current value within which the lightness channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minLightnessShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSL HSLShift(this IRandom random, ColorHSL original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minLightnessShift, float maxLightnessShift)
		{
			return new ColorHSL(
				minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
				minSaturationShift != 0f || maxSaturationShift != 0f ? random.Shift(original.s, minSaturationShift, maxSaturationShift) : original.s,
				minLightnessShift != 0f || maxLightnessShift != 0f ? random.Shift(original.l, minLightnessShift, maxLightnessShift) : original.l,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minSaturationShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxSaturationShift"/>.</param>
		/// <param name="maxSaturationShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minSaturationShift"/>.</param>
		/// <param name="minLightnessShift">The minimum end of the range offset from the current value within which the lightness channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxLightnessShift"/>.</param>
		/// <param name="maxLightnessShift">The maximum end of the range offset from the current value within which the lightness channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minLightnessShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSL HSLAShift(this IRandom random, ColorHSL original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minLightnessShift, float maxLightnessShift, float minAlphaShift, float maxAlphaShift)
		{
			return new ColorHSL(
				minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
				minSaturationShift != 0f || maxSaturationShift != 0f ? random.Shift(original.s, minSaturationShift, maxSaturationShift) : original.s,
				minLightnessShift != 0f || maxLightnessShift != 0f ? random.Shift(original.l, minLightnessShift, maxLightnessShift) : original.l,
				minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteSaturationSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteLightnessSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the lightness channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSL HSLSpread(this IRandom random, ColorHSL original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteLightnessSpread)
		{
			return new ColorHSL(
				maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
				maxAbsoluteSaturationSpread != 0f ? random.Spread(original.s, maxAbsoluteSaturationSpread) : original.s,
				maxAbsoluteLightnessSpread != 0f ? random.Spread(original.l, maxAbsoluteLightnessSpread) : original.l,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteSaturationSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteLightnessSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the lightness channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSL HSLASpread(this IRandom random, ColorHSL original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteLightnessSpread, float maxAbsoluteAlphaSpread)
		{
			return new ColorHSL(
				maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
				maxAbsoluteSaturationSpread != 0f ? random.Spread(original.s, maxAbsoluteSaturationSpread) : original.s,
				maxAbsoluteLightnessSpread != 0f ? random.Spread(original.l, maxAbsoluteLightnessSpread) : original.l,
				maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minSaturationSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSaturationSpread"/>.</param>
		/// <param name="maxSaturationSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSaturationSpread"/>.</param>
		/// <param name="minLightnessSpread">The minimum end of the proportional range within which the lightness channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxLightnessSpread"/>.</param>
		/// <param name="maxLightnessSpread">The maximum end of the proportional range within which the lightness channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minLightnessSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSL HSLSpread(this IRandom random, ColorHSL original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minLightnessSpread, float maxLightnessSpread)
		{
			return new ColorHSL(
				minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
				minSaturationSpread != 0f || maxSaturationSpread != 0f ? random.Spread(original.s, minSaturationSpread, maxSaturationSpread) : original.s,
				minLightnessSpread != 0f || maxLightnessSpread != 0f ? random.Spread(original.l, minLightnessSpread, maxLightnessSpread) : original.l,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minSaturationSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSaturationSpread"/>.</param>
		/// <param name="maxSaturationSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSaturationSpread"/>.</param>
		/// <param name="minLightnessSpread">The minimum end of the proportional range within which the lightness channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxLightnessSpread"/>.</param>
		/// <param name="maxLightnessSpread">The maximum end of the proportional range within which the lightness channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minLightnessSpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSL HSLASpread(this IRandom random, ColorHSL original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minLightnessSpread, float maxLightnessSpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return new ColorHSL(
				minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
				minSaturationSpread != 0f || maxSaturationSpread != 0f ? random.Spread(original.s, minSaturationSpread, maxSaturationSpread) : original.s,
				minLightnessSpread != 0f || maxLightnessSpread != 0f ? random.Spread(original.l, minLightnessSpread, maxLightnessSpread) : original.l,
				minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetSaturation">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetLightness">The far end of the range within which the lightness channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSL HSLLerp(this IRandom random, ColorHSL original, float targetHue, float targetSaturation, float targetLightness)
		{
			return new ColorHSL(
				random.LerpRepeated(original.h, targetHue),
				random.RangeCC(original.s, targetSaturation),
				random.RangeCC(original.l, targetLightness),
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetSaturation">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetLightness">The far end of the range within which the lightness channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSL HSLALerp(this IRandom random, ColorHSL original, float targetHue, float targetSaturation, float targetLightness, float targetAlpha)
		{
			return new ColorHSL(
				random.LerpRepeated(original.h, targetHue),
				random.RangeCC(original.s, targetSaturation),
				random.RangeCC(original.l, targetLightness),
				random.RangeCC(original.a, targetAlpha));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSL HSLALerp(this IRandom random, ColorHSL original, ColorHSL target)
		{
			return new ColorHSL(
				random.LerpRepeated(original.h, target.h),
				random.RangeCC(original.s, target.s),
				random.RangeCC(original.l, target.l),
				random.RangeCC(original.a, target.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSL HueShift(this IRandom random, ColorHSL original, float maxAbsoluteShift)
		{
			return new ColorHSL(random.ShiftRepeated(original.h, maxAbsoluteShift), original.s, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSL HueShift(this IRandom random, ColorHSL original, float minShift, float maxShift)
		{
			return new ColorHSL(random.ShiftRepeated(original.h, minShift, maxShift), original.s, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSL HueSpread(this IRandom random, ColorHSL original)
		{
			return new ColorHSL(random.FloatCO(), original.s, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSL HueSpread(this IRandom random, ColorHSL original, float maxAbsoluteSpread)
		{
			return new ColorHSL(random.SpreadRepeated(original.h, maxAbsoluteSpread), original.s, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSL HueSpread(this IRandom random, ColorHSL original, float minSpread, float maxSpread)
		{
			return new ColorHSL(random.SpreadRepeated(original.h, minSpread, maxSpread), original.s, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSL HueLerp(this IRandom random, ColorHSL original, float target)
		{
			return new ColorHSL(random.LerpRepeated(original.h, target), original.s, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The color whose hue channel indicates the far end of the range within which the hue channel can change.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSL HueLerp(this IRandom random, ColorHSL original, ColorHSL target)
		{
			return new ColorHSL(random.LerpRepeated(original.h, target.h), original.s, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for the saturation channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSL SaturationShift(this IRandom random, ColorHSL original, float maxAbsoluteShift)
		{
			return new ColorHSL(original.h, random.Shift(original.s, maxAbsoluteShift), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for the saturation channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSL SaturationShift(this IRandom random, ColorHSL original, float minShift, float maxShift)
		{
			return new ColorHSL(original.h, random.Shift(original.s, minShift, maxShift), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSL SaturationSpread(this IRandom random, ColorHSL original)
		{
			return new ColorHSL(original.h, random.FloatCC(), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSL SaturationSpread(this IRandom random, ColorHSL original, float maxAbsoluteSpread)
		{
			return new ColorHSL(original.h, random.Spread(original.s, maxAbsoluteSpread), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSL SaturationSpread(this IRandom random, ColorHSL original, float minSpread, float maxSpread)
		{
			return new ColorHSL(original.h, random.Spread(original.s, minSpread, maxSpread), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the saturation channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="target">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSL SaturationLerp(this IRandom random, ColorHSL original, float target)
		{
			return new ColorHSL(original.h, random.RangeCC(original.s, target), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the saturation channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="target">The color whose saturation channel indicates the far end of the range within which the saturation channel can change.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSL SaturationLerp(this IRandom random, ColorHSL original, ColorHSL target)
		{
			return new ColorHSL(original.h, random.RangeCC(original.s, target.s), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for the lightness channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHSL LightnessShift(this IRandom random, ColorHSL original, float maxAbsoluteShift)
		{
			return new ColorHSL(original.h, original.s, random.Shift(original.l, maxAbsoluteShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for the lightness channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the lightness channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the lightness channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHSL LightnessShift(this IRandom random, ColorHSL original, float minShift, float maxShift)
		{
			return new ColorHSL(original.h, original.s, random.Shift(original.l, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHSL LightnessSpread(this IRandom random, ColorHSL original)
		{
			return new ColorHSL(original.h, original.s, random.FloatCC(), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the lightness channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHSL LightnessSpread(this IRandom random, ColorHSL original, float maxAbsoluteSpread)
		{
			return new ColorHSL(original.h, original.s, random.Spread(original.l, maxAbsoluteSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the lightness channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the lightness channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHSL LightnessSpread(this IRandom random, ColorHSL original, float minSpread, float maxSpread)
		{
			return new ColorHSL(original.h, original.s, random.Spread(original.l, minSpread, maxSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the lightness channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="target">The far end of the range within which the lightness channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHSL LightnessLerp(this IRandom random, ColorHSL original, float target)
		{
			return new ColorHSL(original.h, original.s, random.RangeCC(original.l, target), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the lightness channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="target">The color whose lightness channel indicates the far end of the range within which the lightness channel can change.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHSL LightnessLerp(this IRandom random, ColorHSL original, ColorHSL target)
		{
			return new ColorHSL(original.h, original.s, random.RangeCC(original.l, target.l), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSL AlphaShift(this IRandom random, ColorHSL original, float maxAbsoluteShift)
		{
			return new ColorHSL(original.h, original.s, original.l, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSL AlphaShift(this IRandom random, ColorHSL original, float minShift, float maxShift)
		{
			return new ColorHSL(original.h, original.s, original.l, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSL AlphaSpread(this IRandom random, ColorHSL original)
		{
			return new ColorHSL(original.h, original.s, original.l, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSL AlphaSpread(this IRandom random, ColorHSL original, float maxAbsoluteSpread)
		{
			return new ColorHSL(original.h, original.s, original.l, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSL AlphaSpread(this IRandom random, ColorHSL original, float minSpread, float maxSpread)
		{
			return new ColorHSL(original.h, original.s, original.l, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSL AlphaLerp(this IRandom random, ColorHSL original, float target)
		{
			return new ColorHSL(original.h, original.s, original.l, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSL AlphaLerp(this IRandom random, ColorHSL original, ColorHSL target)
		{
			return new ColorHSL(original.h, original.s, original.l, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region HCL

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/lightness color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHCL HCL(this IRandom random)
		{
			return random.HCL(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/lightness color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHCL HCL(this IRandom random, float a)
		{
			float hue = random.FloatCO();
			Vector2 chromaLightness = random.PointWithinTriangle(new Vector2(1f, ColorHCL.GetLightnessAtMaxChroma()), new Vector2(0f, 1f));
			return new ColorHCL(hue, chromaLightness.x, chromaLightness.y, a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/lightness color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHCL HCLA(this IRandom random)
		{
			return random.HCL(random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteChromaShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteLightnessShift">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCL HCLShift(this IRandom random, ColorHCL original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteLightnessShift)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
					maxAbsoluteChromaShift != 0f ? random.Shift(original.c, maxAbsoluteChromaShift) : original.c,
					maxAbsoluteLightnessShift != 0f ? random.Shift(original.l, maxAbsoluteLightnessShift) : original.l,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteChromaShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteLightnessShift">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCL HCLAShift(this IRandom random, ColorHCL original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteLightnessShift, float maxAbsoluteAlphaShift)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
					maxAbsoluteChromaShift != 0f ? random.Shift(original.c, maxAbsoluteChromaShift) : original.c,
					maxAbsoluteLightnessShift != 0f ? random.Shift(original.l, maxAbsoluteLightnessShift) : original.l,
					maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minChromaShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxChromaShift"/>.</param>
		/// <param name="maxChromaShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minChromaShift"/>.</param>
		/// <param name="minLightnessShift">The minimum end of the range offset from the current value within which the lightness channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxLightnessShift"/>.</param>
		/// <param name="maxLightnessShift">The maximum end of the range offset from the current value within which the lightness channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minLightnessShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCL HCLShift(this IRandom random, ColorHCL original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minLightnessShift, float maxLightnessShift)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
					minChromaShift != 0f || maxChromaShift != 0f ? random.Shift(original.c, minChromaShift, maxChromaShift) : original.c,
					minLightnessShift != 0f || maxLightnessShift != 0f ? random.Shift(original.l, minLightnessShift, maxLightnessShift) : original.l,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minChromaShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxChromaShift"/>.</param>
		/// <param name="maxChromaShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minChromaShift"/>.</param>
		/// <param name="minLightnessShift">The minimum end of the range offset from the current value within which the lightness channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxLightnessShift"/>.</param>
		/// <param name="maxLightnessShift">The maximum end of the range offset from the current value within which the lightness channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minLightnessShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCL HCLAShift(this IRandom random, ColorHCL original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minLightnessShift, float maxLightnessShift, float minAlphaShift, float maxAlphaShift)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
					minChromaShift != 0f || maxChromaShift != 0f ? random.Shift(original.c, minChromaShift, maxChromaShift) : original.c,
					minLightnessShift != 0f || maxLightnessShift != 0f ? random.Shift(original.l, minLightnessShift, maxLightnessShift) : original.l,
					minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteChromaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteLightnessSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the lightness channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCL HCLSpread(this IRandom random, ColorHCL original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteLightnessSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
					maxAbsoluteChromaSpread != 0f ? random.Spread(original.c, maxAbsoluteChromaSpread) : original.c,
					maxAbsoluteLightnessSpread != 0f ? random.Spread(original.l, maxAbsoluteLightnessSpread) : original.l,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteChromaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteLightnessSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the lightness channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCL HCLASpread(this IRandom random, ColorHCL original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteLightnessSpread, float maxAbsoluteAlphaSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
					maxAbsoluteChromaSpread != 0f ? random.Spread(original.c, maxAbsoluteChromaSpread) : original.c,
					maxAbsoluteLightnessSpread != 0f ? random.Spread(original.l, maxAbsoluteLightnessSpread) : original.l,
					maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minChromaSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxChromaSpread"/>.</param>
		/// <param name="maxChromaSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minChromaSpread"/>.</param>
		/// <param name="minLightnessSpread">The minimum end of the proportional range within which the lightness channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxLightnessSpread"/>.</param>
		/// <param name="maxLightnessSpread">The maximum end of the proportional range within which the lightness channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minLightnessSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCL HCLSpread(this IRandom random, ColorHCL original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minLightnessSpread, float maxLightnessSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
					minChromaSpread != 0f || maxChromaSpread != 0f ? random.Spread(original.c, minChromaSpread, maxChromaSpread) : original.c,
					minLightnessSpread != 0f || maxLightnessSpread != 0f ? random.Spread(original.l, minLightnessSpread, maxLightnessSpread) : original.l,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minChromaSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxChromaSpread"/>.</param>
		/// <param name="maxChromaSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minChromaSpread"/>.</param>
		/// <param name="minLightnessSpread">The minimum end of the proportional range within which the lightness channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxLightnessSpread"/>.</param>
		/// <param name="maxLightnessSpread">The maximum end of the proportional range within which the lightness channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minLightnessSpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCL HCLASpread(this IRandom random, ColorHCL original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minLightnessSpread, float maxLightnessSpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
					minChromaSpread != 0f || maxChromaSpread != 0f ? random.Spread(original.c, minChromaSpread, maxChromaSpread) : original.c,
					minLightnessSpread != 0f || maxLightnessSpread != 0f ? random.Spread(original.l, minLightnessSpread, maxLightnessSpread) : original.l,
					minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetChroma">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetLightness">The far end of the range within which the lightness channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCL HCLLerp(this IRandom random, ColorHCL original, float targetHue, float targetChroma, float targetLightness)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					random.LerpRepeated(original.h, targetHue),
					random.RangeCC(original.c, targetChroma),
					random.RangeCC(original.l, targetLightness),
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetChroma">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetLightness">The far end of the range within which the lightness channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCL HCLALerp(this IRandom random, ColorHCL original, float targetHue, float targetChroma, float targetLightness, float targetAlpha)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					random.LerpRepeated(original.h, targetHue),
					random.RangeCC(original.c, targetChroma),
					random.RangeCC(original.l, targetLightness),
					random.RangeCC(original.a, targetAlpha));
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCL HCLALerp(this IRandom random, ColorHCL original, ColorHCL target)
		{
			return Change(original, () =>
			{
				return new ColorHCL(
					random.LerpRepeated(original.h, target.h),
					random.RangeCC(original.c, target.c),
					random.RangeCC(original.l, target.l),
					random.RangeCC(original.a, target.a));
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCL HueShift(this IRandom random, ColorHCL original, float maxAbsoluteShift)
		{
			return new ColorHCL(random.ShiftRepeated(original.h, maxAbsoluteShift), original.c, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCL HueShift(this IRandom random, ColorHCL original, float minShift, float maxShift)
		{
			return new ColorHCL(random.ShiftRepeated(original.h, minShift, maxShift), original.c, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCL HueSpread(this IRandom random, ColorHCL original)
		{
			return new ColorHCL(random.FloatCO(), original.c, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCL HueSpread(this IRandom random, ColorHCL original, float maxAbsoluteSpread)
		{
			return new ColorHCL(random.SpreadRepeated(original.h, maxAbsoluteSpread), original.c, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCL HueSpread(this IRandom random, ColorHCL original, float minSpread, float maxSpread)
		{
			return new ColorHCL(random.SpreadRepeated(original.h, minSpread, maxSpread), original.c, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCL HueLerp(this IRandom random, ColorHCL original, float target)
		{
			return new ColorHCL(random.LerpRepeated(original.h, target), original.c, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The color whose hue channel indicates the far end of the range within which the hue channel can change.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCL HueLerp(this IRandom random, ColorHCL original, ColorHCL target)
		{
			return new ColorHCL(random.LerpRepeated(original.h, target.h), original.c, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ChromaShift(this IRandom random, ColorHCL original, float maxAbsoluteShift)
		{
			return new ColorHCL(original.h, random.ShiftClamped(original.c, maxAbsoluteShift, 0f, ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ChromaShift(this IRandom random, ColorHCL original, float minShift, float maxShift)
		{
			return new ColorHCL(original.h, random.ShiftClamped(original.c, minShift, maxShift, 0f, ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ChromaSpread(this IRandom random, ColorHCL original)
		{
			return new ColorHCL(original.h, random.RangeCC(0f, ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ChromaSpread(this IRandom random, ColorHCL original, float maxAbsoluteSpread)
		{
			return new ColorHCL(original.h, random.SpreadClamped(original.c, maxAbsoluteSpread, 0f, ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ChromaSpread(this IRandom random, ColorHCL original, float minSpread, float maxSpread)
		{
			return new ColorHCL(original.h, random.SpreadClamped(original.c, minSpread, maxSpread, 0f, ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ChromaLerp(this IRandom random, ColorHCL original, float target)
		{
			return new ColorHCL(original.h, random.RangeCC(original.c, Mathf.Min(target, ColorHCL.GetMaxChroma(original.l))), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The color whose chroma channel indicates the far end of the range within which the chroma channel can change.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ChromaLerp(this IRandom random, ColorHCL original, ColorHCL target)
		{
			return new ColorHCL(original.h, random.RangeCC(original.c, Mathf.Min(target.c, ColorHCL.GetMaxChroma(original.l))), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the lightness channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL LightnessShift(this IRandom random, ColorHCL original, float maxAbsoluteShift)
		{
			float lMin, lMax;
			ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.ShiftClamped(original.l, maxAbsoluteShift, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the lightness channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the lightness channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the lightness channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL LightnessShift(this IRandom random, ColorHCL original, float minShift, float maxShift)
		{
			float lMin, lMax;
			ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.ShiftClamped(original.l, minShift, maxShift, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL LightnessSpread(this IRandom random, ColorHCL original)
		{
			float lMin, lMax;
			ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.RangeCC(lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the lightness channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL LightnessSpread(this IRandom random, ColorHCL original, float maxAbsoluteSpread)
		{
			float lMin, lMax;
			ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.SpreadClamped(original.l, maxAbsoluteSpread, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the lightness channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the lightness channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL LightnessSpread(this IRandom random, ColorHCL original, float minSpread, float maxSpread)
		{
			float lMin, lMax;
			ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.SpreadClamped(original.l, minSpread, maxSpread, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the lightness channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="target">The far end of the range within which the lightness channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL LightnessLerp(this IRandom random, ColorHCL original, float target)
		{
			float lMin, lMax;
			ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.RangeCC(Mathf.Max(lMin, original.l), Mathf.Min(target, lMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the lightness channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="target">The color whose lightness channel indicates the far end of the range within which the lightness channel can change.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL LightnessLerp(this IRandom random, ColorHCL original, ColorHCL target)
		{
			float lMin, lMax;
			ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.RangeCC(Mathf.Max(lMin, original.l), Mathf.Min(target.l, lMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL AlphaShift(this IRandom random, ColorHCL original, float maxAbsoluteShift)
		{
			return new ColorHCL(original.h, original.c, original.l, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL AlphaShift(this IRandom random, ColorHCL original, float minShift, float maxShift)
		{
			return new ColorHCL(original.h, original.c, original.l, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL AlphaSpread(this IRandom random, ColorHCL original)
		{
			return new ColorHCL(original.h, original.c, original.l, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL AlphaSpread(this IRandom random, ColorHCL original, float maxAbsoluteSpread)
		{
			return new ColorHCL(original.h, original.c, original.l, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL AlphaSpread(this IRandom random, ColorHCL original, float minSpread, float maxSpread)
		{
			return new ColorHCL(original.h, original.c, original.l, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL AlphaLerp(this IRandom random, ColorHCL original, float target)
		{
			return new ColorHCL(original.h, original.c, original.l, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL AlphaLerp(this IRandom random, ColorHCL original, ColorHCL target)
		{
			return new ColorHCL(original.h, original.c, original.l, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region HSY

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/luma color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHSY HSY(this IRandom random)
		{
			return new ColorHSY(random.FloatCO(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/luma color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHSY HSY(this IRandom random, float a)
		{
			return new ColorHSY(random.FloatCO(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/luma color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHSY HSYA(this IRandom random)
		{
			return new ColorHSY(random.FloatCO(), random.FloatCC(), random.FloatCC(), random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteSaturationShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteLumaShift">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSY HSYShift(this IRandom random, ColorHSY original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteLumaShift)
		{
			return new ColorHSY(
				maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
				maxAbsoluteSaturationShift != 0f ? random.Shift(original.s, maxAbsoluteSaturationShift) : original.s,
				maxAbsoluteLumaShift != 0f ? random.Shift(original.y, maxAbsoluteLumaShift) : original.y,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteSaturationShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteLumaShift">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSY HSYAShift(this IRandom random, ColorHSY original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteLumaShift, float maxAbsoluteAlphaShift)
		{
			return new ColorHSY(
				maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
				maxAbsoluteSaturationShift != 0f ? random.Shift(original.s, maxAbsoluteSaturationShift) : original.s,
				maxAbsoluteLumaShift != 0f ? random.Shift(original.y, maxAbsoluteLumaShift) : original.y,
				maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minSaturationShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxSaturationShift"/>.</param>
		/// <param name="maxSaturationShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minSaturationShift"/>.</param>
		/// <param name="minLumaShift">The minimum end of the range offset from the current value within which the luma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxLumaShift"/>.</param>
		/// <param name="maxLumaShift">The maximum end of the range offset from the current value within which the luma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minLumaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSY HSYShift(this IRandom random, ColorHSY original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minLumaShift, float maxLumaShift)
		{
			return new ColorHSY(
				minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
				minSaturationShift != 0f || maxSaturationShift != 0f ? random.Shift(original.s, minSaturationShift, maxSaturationShift) : original.s,
				minLumaShift != 0f || maxLumaShift != 0f ? random.Shift(original.y, minLumaShift, maxLumaShift) : original.y,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minSaturationShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxSaturationShift"/>.</param>
		/// <param name="maxSaturationShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minSaturationShift"/>.</param>
		/// <param name="minLumaShift">The minimum end of the range offset from the current value within which the luma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxLumaShift"/>.</param>
		/// <param name="maxLumaShift">The maximum end of the range offset from the current value within which the luma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minLumaShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSY HSYAShift(this IRandom random, ColorHSY original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minLumaShift, float maxLumaShift, float minAlphaShift, float maxAlphaShift)
		{
			return new ColorHSY(
				minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
				minSaturationShift != 0f || maxSaturationShift != 0f ? random.Shift(original.s, minSaturationShift, maxSaturationShift) : original.s,
				minLumaShift != 0f || maxLumaShift != 0f ? random.Shift(original.y, minLumaShift, maxLumaShift) : original.y,
				minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteSaturationSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteLumaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the luma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSY HSYSpread(this IRandom random, ColorHSY original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteLumaSpread)
		{
			return new ColorHSY(
				maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
				maxAbsoluteSaturationSpread != 0f ? random.Spread(original.s, maxAbsoluteSaturationSpread) : original.s,
				maxAbsoluteLumaSpread != 0f ? random.Spread(original.y, maxAbsoluteLumaSpread) : original.y,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteSaturationSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteLumaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the luma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSY HSYASpread(this IRandom random, ColorHSY original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteLumaSpread, float maxAbsoluteAlphaSpread)
		{
			return new ColorHSY(
				maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
				maxAbsoluteSaturationSpread != 0f ? random.Spread(original.s, maxAbsoluteSaturationSpread) : original.s,
				maxAbsoluteLumaSpread != 0f ? random.Spread(original.y, maxAbsoluteLumaSpread) : original.y,
				maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minSaturationSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSaturationSpread"/>.</param>
		/// <param name="maxSaturationSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSaturationSpread"/>.</param>
		/// <param name="minLumaSpread">The minimum end of the proportional range within which the luma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxLumaSpread"/>.</param>
		/// <param name="maxLumaSpread">The maximum end of the proportional range within which the luma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minLumaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSY HSYSpread(this IRandom random, ColorHSY original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minLumaSpread, float maxLumaSpread)
		{
			return new ColorHSY(
				minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
				minSaturationSpread != 0f || maxSaturationSpread != 0f ? random.Spread(original.s, minSaturationSpread, maxSaturationSpread) : original.s,
				minLumaSpread != 0f || maxLumaSpread != 0f ? random.Spread(original.y, minLumaSpread, maxLumaSpread) : original.y,
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minSaturationSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSaturationSpread"/>.</param>
		/// <param name="maxSaturationSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSaturationSpread"/>.</param>
		/// <param name="minLumaSpread">The minimum end of the proportional range within which the luma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxLumaSpread"/>.</param>
		/// <param name="maxLumaSpread">The maximum end of the proportional range within which the luma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minLumaSpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSY HSYASpread(this IRandom random, ColorHSY original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minLumaSpread, float maxLumaSpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return new ColorHSY(
				minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
				minSaturationSpread != 0f || maxSaturationSpread != 0f ? random.Spread(original.s, minSaturationSpread, maxSaturationSpread) : original.s,
				minLumaSpread != 0f || maxLumaSpread != 0f ? random.Spread(original.y, minLumaSpread, maxLumaSpread) : original.y,
				minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetSaturation">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetLuma">The far end of the range within which the luma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHSY HSYLerp(this IRandom random, ColorHSY original, float targetHue, float targetSaturation, float targetLuma)
		{
			return new ColorHSY(
				random.LerpRepeated(original.h, targetHue),
				random.RangeCC(original.s, targetSaturation),
				random.RangeCC(original.y, targetLuma),
				original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetSaturation">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetLuma">The far end of the range within which the luma channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSY HSYALerp(this IRandom random, ColorHSY original, float targetHue, float targetSaturation, float targetLuma, float targetAlpha)
		{
			return new ColorHSY(
				random.LerpRepeated(original.h, targetHue),
				random.RangeCC(original.s, targetSaturation),
				random.RangeCC(original.y, targetLuma),
				random.RangeCC(original.a, targetAlpha));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHSY HSYALerp(this IRandom random, ColorHSY original, ColorHSY target)
		{
			return new ColorHSY(
				random.LerpRepeated(original.h, target.h),
				random.RangeCC(original.s, target.s),
				random.RangeCC(original.y, target.y),
				random.RangeCC(original.a, target.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSY HueShift(this IRandom random, ColorHSY original, float maxAbsoluteShift)
		{
			return new ColorHSY(random.ShiftRepeated(original.h, maxAbsoluteShift), original.s, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSY HueShift(this IRandom random, ColorHSY original, float minShift, float maxShift)
		{
			return new ColorHSY(random.ShiftRepeated(original.h, minShift, maxShift), original.s, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSY HueSpread(this IRandom random, ColorHSY original)
		{
			return new ColorHSY(random.FloatCO(), original.s, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSY HueSpread(this IRandom random, ColorHSY original, float maxAbsoluteSpread)
		{
			return new ColorHSY(random.SpreadRepeated(original.h, maxAbsoluteSpread), original.s, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSY HueSpread(this IRandom random, ColorHSY original, float minSpread, float maxSpread)
		{
			return new ColorHSY(random.SpreadRepeated(original.h, minSpread, maxSpread), original.s, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSY HueLerp(this IRandom random, ColorHSY original, float target)
		{
			return new ColorHSY(random.LerpRepeated(original.h, target), original.s, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The color whose hue channel indicates the far end of the range within which the hue channel can change.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSY HueLerp(this IRandom random, ColorHSY original, ColorHSY target)
		{
			return new ColorHSY(random.LerpRepeated(original.h, target.h), original.s, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for the saturation channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSY SaturationShift(this IRandom random, ColorHSY original, float maxAbsoluteShift)
		{
			return new ColorHSY(original.h, random.Shift(original.s, maxAbsoluteShift), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for the saturation channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the saturation channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the saturation channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSY SaturationShift(this IRandom random, ColorHSY original, float minShift, float maxShift)
		{
			return new ColorHSY(original.h, random.Shift(original.s, minShift, maxShift), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSY SaturationSpread(this IRandom random, ColorHSY original)
		{
			return new ColorHSY(original.h, random.FloatCC(), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the saturation channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSY SaturationSpread(this IRandom random, ColorHSY original, float maxAbsoluteSpread)
		{
			return new ColorHSY(original.h, random.Spread(original.s, maxAbsoluteSpread), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the saturation channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the saturation channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSY SaturationSpread(this IRandom random, ColorHSY original, float minSpread, float maxSpread)
		{
			return new ColorHSY(original.h, random.Spread(original.s, minSpread, maxSpread), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the saturation channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="target">The far end of the range within which the saturation channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSY SaturationLerp(this IRandom random, ColorHSY original, float target)
		{
			return new ColorHSY(original.h, random.RangeCC(original.s, target), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the saturation channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <param name="target">The color whose saturation channel indicates the far end of the range within which the saturation channel can change.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSY SaturationLerp(this IRandom random, ColorHSY original, ColorHSY target)
		{
			return new ColorHSY(original.h, random.RangeCC(original.s, target.s), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for the luma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHSY LumaShift(this IRandom random, ColorHSY original, float maxAbsoluteShift)
		{
			return new ColorHSY(original.h, original.s, random.Shift(original.y, maxAbsoluteShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for the luma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the luma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the luma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHSY LumaShift(this IRandom random, ColorHSY original, float minShift, float maxShift)
		{
			return new ColorHSY(original.h, original.s, random.Shift(original.y, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHSY LumaSpread(this IRandom random, ColorHSY original)
		{
			return new ColorHSY(original.h, original.s, random.FloatCC(), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the luma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHSY LumaSpread(this IRandom random, ColorHSY original, float maxAbsoluteSpread)
		{
			return new ColorHSY(original.h, original.s, random.Spread(original.y, maxAbsoluteSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the luma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the luma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHSY LumaSpread(this IRandom random, ColorHSY original, float minSpread, float maxSpread)
		{
			return new ColorHSY(original.h, original.s, random.Spread(original.y, minSpread, maxSpread), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the luma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the luma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHSY LumaLerp(this IRandom random, ColorHSY original, float target)
		{
			return new ColorHSY(original.h, original.s, random.RangeCC(original.y, target), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the luma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="target">The color whose luma channel indicates the far end of the range within which the luma channel can change.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHSY LumaLerp(this IRandom random, ColorHSY original, ColorHSY target)
		{
			return new ColorHSY(original.h, original.s, random.RangeCC(original.y, target.y), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSY AlphaShift(this IRandom random, ColorHSY original, float maxAbsoluteShift)
		{
			return new ColorHSY(original.h, original.s, original.y, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSY AlphaShift(this IRandom random, ColorHSY original, float minShift, float maxShift)
		{
			return new ColorHSY(original.h, original.s, original.y, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSY AlphaSpread(this IRandom random, ColorHSY original)
		{
			return new ColorHSY(original.h, original.s, original.y, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSY AlphaSpread(this IRandom random, ColorHSY original, float maxAbsoluteSpread)
		{
			return new ColorHSY(original.h, original.s, original.y, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSY AlphaSpread(this IRandom random, ColorHSY original, float minSpread, float maxSpread)
		{
			return new ColorHSY(original.h, original.s, original.y, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSY AlphaLerp(this IRandom random, ColorHSY original, float target)
		{
			return new ColorHSY(original.h, original.s, original.y, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSY AlphaLerp(this IRandom random, ColorHSY original, ColorHSY target)
		{
			return new ColorHSY(original.h, original.s, original.y, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region HCY

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/luma color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHCY HCY(this IRandom random)
		{
			return random.HCY(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/luma color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHCY HCY(this IRandom random, float a)
		{
			float hue = random.FloatCO();
			Vector2 chromaLuma = random.PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hue)), new Vector2(0f, 1f));
			return new ColorHCY(hue, chromaLuma.x, chromaLuma.y, a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/luma color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHCY HCYA(this IRandom random)
		{
			return random.HCY(random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteChromaShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteLumaShift">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCY HCYShift(this IRandom random, ColorHCY original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteLumaShift)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
					maxAbsoluteChromaShift != 0f ? random.Shift(original.c, maxAbsoluteChromaShift) : original.c,
					maxAbsoluteLumaShift != 0f ? random.Shift(original.y, maxAbsoluteLumaShift) : original.y,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteChromaShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteLumaShift">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAbsoluteAlphaShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCY HCYAShift(this IRandom random, ColorHCY original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteLumaShift, float maxAbsoluteAlphaShift)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					maxAbsoluteHueShift != 0f ? random.ShiftRepeated(original.h, maxAbsoluteHueShift) : original.h,
					maxAbsoluteChromaShift != 0f ? random.Shift(original.c, maxAbsoluteChromaShift) : original.c,
					maxAbsoluteLumaShift != 0f ? random.Shift(original.y, maxAbsoluteLumaShift) : original.y,
					maxAbsoluteAlphaShift != 0f ? random.Shift(original.a, maxAbsoluteAlphaShift) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly and independently selecting new values for the color channels while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minChromaShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxChromaShift"/>.</param>
		/// <param name="maxChromaShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minChromaShift"/>.</param>
		/// <param name="minLumaShift">The minimum end of the range offset from the current value within which the luma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxLumaShift"/>.</param>
		/// <param name="maxLumaShift">The maximum end of the range offset from the current value within which the luma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minLumaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCY HCYShift(this IRandom random, ColorHCY original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minLumaShift, float maxLumaShift)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
					minChromaShift != 0f || maxChromaShift != 0f ? random.Shift(original.c, minChromaShift, maxChromaShift) : original.c,
					minLumaShift != 0f || maxLumaShift != 0f ? random.Shift(original.y, minLumaShift, maxLumaShift) : original.y,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly and independently selecting new values for the color channels and opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxHueShift"/>.</param>
		/// <param name="maxHueShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minHueShift"/>.</param>
		/// <param name="minChromaShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxChromaShift"/>.</param>
		/// <param name="maxChromaShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minChromaShift"/>.</param>
		/// <param name="minLumaShift">The minimum end of the range offset from the current value within which the luma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxLumaShift"/>.</param>
		/// <param name="maxLumaShift">The maximum end of the range offset from the current value within which the luma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minLumaShift"/>.</param>
		/// <param name="minAlphaShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxAlphaShift"/>.</param>
		/// <param name="maxAlphaShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minAlphaShift"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCY HCYAShift(this IRandom random, ColorHCY original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minLumaShift, float maxLumaShift, float minAlphaShift, float maxAlphaShift)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					minHueShift != 0f || maxHueShift != 0f ? random.ShiftRepeated(original.h, minHueShift, maxHueShift) : original.h,
					minChromaShift != 0f || maxChromaShift != 0f ? random.Shift(original.c, minChromaShift, maxChromaShift) : original.c,
					minLumaShift != 0f || maxLumaShift != 0f ? random.Shift(original.y, minLumaShift, maxLumaShift) : original.y,
					minAlphaShift != 0f || maxAlphaShift != 0f ? random.Shift(original.a, minAlphaShift, maxAlphaShift) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteChromaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteLumaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the luma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCY HCYSpread(this IRandom random, ColorHCY original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteLumaSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
					maxAbsoluteChromaSpread != 0f ? random.Spread(original.c, maxAbsoluteChromaSpread) : original.c,
					maxAbsoluteLumaSpread != 0f ? random.Spread(original.y, maxAbsoluteLumaSpread) : original.y,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxAbsoluteHueSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteChromaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteLumaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the luma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <param name="maxAbsoluteAlphaSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCY HCYASpread(this IRandom random, ColorHCY original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteLumaSpread, float maxAbsoluteAlphaSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					maxAbsoluteHueSpread != 0f ? random.SpreadRepeated(original.h, maxAbsoluteHueSpread) : original.h,
					maxAbsoluteChromaSpread != 0f ? random.Spread(original.c, maxAbsoluteChromaSpread) : original.c,
					maxAbsoluteLumaSpread != 0f ? random.Spread(original.y, maxAbsoluteLumaSpread) : original.y,
					maxAbsoluteAlphaSpread != 0f ? random.Spread(original.a, maxAbsoluteAlphaSpread) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly and independently spreading the color channels toward their minimum or maximum possible values while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minChromaSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxChromaSpread"/>.</param>
		/// <param name="maxChromaSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minChromaSpread"/>.</param>
		/// <param name="minLumaSpread">The minimum end of the proportional range within which the luma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxLumaSpread"/>.</param>
		/// <param name="maxLumaSpread">The maximum end of the proportional range within which the luma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minLumaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCY HCYSpread(this IRandom random, ColorHCY original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minLumaSpread, float maxLumaSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
					minChromaSpread != 0f || maxChromaSpread != 0f ? random.Spread(original.c, minChromaSpread, maxChromaSpread) : original.c,
					minLumaSpread != 0f || maxLumaSpread != 0f ? random.Spread(original.y, minLumaSpread, maxLumaSpread) : original.y,
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly and independently spreading the color channels and opacity toward their minimum or maximum possible values.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="minHueSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxHueSpread"/>.</param>
		/// <param name="maxHueSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minHueSpread"/>.</param>
		/// <param name="minChromaSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxChromaSpread"/>.</param>
		/// <param name="maxChromaSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minChromaSpread"/>.</param>
		/// <param name="minLumaSpread">The minimum end of the proportional range within which the luma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxLumaSpread"/>.</param>
		/// <param name="maxLumaSpread">The maximum end of the proportional range within which the luma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minLumaSpread"/>.</param>
		/// <param name="minAlphaSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxAlphaSpread"/>.</param>
		/// <param name="maxAlphaSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minAlphaSpread"/>.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCY HCYASpread(this IRandom random, ColorHCY original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minLumaSpread, float maxLumaSpread, float minAlphaSpread, float maxAlphaSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					minHueSpread != 0f || maxHueSpread != 0f ? random.SpreadRepeated(original.h, minHueSpread, maxHueSpread) : original.h,
					minChromaSpread != 0f || maxChromaSpread != 0f ? random.Spread(original.c, minChromaSpread, maxChromaSpread) : original.c,
					minLumaSpread != 0f || maxLumaSpread != 0f ? random.Spread(original.y, minLumaSpread, maxLumaSpread) : original.y,
					minAlphaSpread != 0f || maxAlphaSpread != 0f ? random.Spread(original.a, minAlphaSpread, maxAlphaSpread) : original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the color channels toward the specified targets by independently random amounts while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetChroma">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetLuma">The far end of the range within which the luma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels independently randomized.</returns>
		public static ColorHCY HCYLerp(this IRandom random, ColorHCY original, float targetHue, float targetChroma, float targetLuma)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					random.LerpRepeated(original.h, targetHue),
					random.RangeCC(original.c, targetChroma),
					random.RangeCC(original.y, targetLuma),
					original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="targetHue">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetChroma">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetLuma">The far end of the range within which the luma channel can change.  Must be in the range [0, +1].</param>
		/// <param name="targetAlpha">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCY HCYALerp(this IRandom random, ColorHCY original, float targetHue, float targetChroma, float targetLuma, float targetAlpha)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					random.LerpRepeated(original.h, targetHue),
					random.RangeCC(original.c, targetChroma),
					random.RangeCC(original.y, targetLuma),
					random.RangeCC(original.a, targetAlpha));
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the color channels and opacity toward the specified targets by independently random amounts.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose color channels and opacity will be altered.</param>
		/// <param name="target">The color whose channels indicate the far end of the ranges within which the channels can change.</param>
		/// <returns>A color derived from the original color but with the color channels and opacity independently randomized.</returns>
		public static ColorHCY HCYALerp(this IRandom random, ColorHCY original, ColorHCY target)
		{
			return Change(original, () =>
			{
				return new ColorHCY(
					random.LerpRepeated(original.h, target.h),
					random.RangeCC(original.c, target.c),
					random.RangeCC(original.y, target.y),
					random.RangeCC(original.a, target.a));
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCY HueShift(this IRandom random, ColorHCY original, float maxAbsoluteShift)
		{
			return Change(original, () =>
			{
				return new ColorHCY(random.ShiftRepeated(original.h, maxAbsoluteShift), original.c, original.y, original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the hue channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the hue channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the hue channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCY HueShift(this IRandom random, ColorHCY original, float minShift, float maxShift)
		{
			return Change(original, () =>
			{
				return new ColorHCY(random.ShiftRepeated(original.h, minShift, maxShift), original.c, original.y, original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCY HueSpread(this IRandom random, ColorHCY original)
		{
			return Change(original, () =>
			{
				return new ColorHCY(random.FloatCO(), original.c, original.y, original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the hue channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCY HueSpread(this IRandom random, ColorHCY original, float maxAbsoluteSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCY(random.SpreadRepeated(original.h, maxAbsoluteSpread), original.c, original.y, original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the hue channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the hue channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCY HueSpread(this IRandom random, ColorHCY original, float minSpread, float maxSpread)
		{
			return Change(original, () =>
			{
				return new ColorHCY(random.SpreadRepeated(original.h, minSpread, maxSpread), original.c, original.y, original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The far end of the range within which the hue channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCY HueLerp(this IRandom random, ColorHCY original, float target)
		{
			return Change(original, () =>
			{
				return new ColorHCY(random.LerpRepeated(original.h, target), original.c, original.y, original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the hue channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <param name="target">The color whose hue channel indicates the far end of the range within which the hue channel can change.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCY HueLerp(this IRandom random, ColorHCY original, ColorHCY target)
		{
			return Change(original, () =>
			{
				return new ColorHCY(random.LerpRepeated(original.h, target.h), original.c, original.y, original.a);
			});
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the chroma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ChromaShift(this IRandom random, ColorHCY original, float maxAbsoluteShift)
		{
			return new ColorHCY(original.h, random.ShiftClamped(original.c, maxAbsoluteShift, 0f, ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ChromaShift(this IRandom random, ColorHCY original, float minShift, float maxShift)
		{
			return new ColorHCY(original.h, random.ShiftClamped(original.c, minShift, maxShift, 0f, ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ChromaSpread(this IRandom random, ColorHCY original)
		{
			return new ColorHCY(original.h, random.RangeCC(0f, ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ChromaSpread(this IRandom random, ColorHCY original, float maxAbsoluteSpread)
		{
			return new ColorHCY(original.h, random.SpreadClamped(original.c, maxAbsoluteSpread, 0f, ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ChromaSpread(this IRandom random, ColorHCY original, float minSpread, float maxSpread)
		{
			return new ColorHCY(original.h, random.SpreadClamped(original.c, minSpread, maxSpread, 0f, ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ChromaLerp(this IRandom random, ColorHCY original, float target)
		{
			return new ColorHCY(original.h, random.RangeCC(original.c, Mathf.Min(target, ColorHCY.GetMaxChroma(original.h, original.y))), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The color whose chroma channel indicates the far end of the range within which the chroma channel can change.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ChromaLerp(this IRandom random, ColorHCY original, ColorHCY target)
		{
			return new ColorHCY(original.h, random.RangeCC(original.c, Mathf.Min(target.c, ColorHCY.GetMaxChroma(original.h, original.y))), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the luma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY LumaShift(this IRandom random, ColorHCY original, float maxAbsoluteShift)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.ShiftClamped(original.y, maxAbsoluteShift, yMin, yMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the luma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the luma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the luma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY LumaShift(this IRandom random, ColorHCY original, float minShift, float maxShift)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.ShiftClamped(original.y, minShift, maxShift, yMin, yMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY LumaSpread(this IRandom random, ColorHCY original)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.RangeCC(yMin, yMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the luma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY LumaSpread(this IRandom random, ColorHCY original, float maxAbsoluteSpread)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.SpreadClamped(original.y, maxAbsoluteSpread, yMin, yMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the luma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the luma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY LumaSpread(this IRandom random, ColorHCY original, float minSpread, float maxSpread)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.SpreadClamped(original.y, minSpread, maxSpread, yMin, yMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the luma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the luma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY LumaLerp(this IRandom random, ColorHCY original, float target)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.RangeCC(Mathf.Max(yMin, original.y), Mathf.Min(target, yMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the luma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="target">The color whose luma channel indicates the far end of the range within which the luma channel can change.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY LumaLerp(this IRandom random, ColorHCY original, ColorHCY target)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.RangeCC(Mathf.Max(yMin, original.y), Mathf.Min(target.y, yMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY AlphaShift(this IRandom random, ColorHCY original, float maxAbsoluteShift)
		{
			return new ColorHCY(original.h, original.c, original.y, random.Shift(original.a, maxAbsoluteShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the opacity can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the opacity can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY AlphaShift(this IRandom random, ColorHCY original, float minShift, float maxShift)
		{
			return new ColorHCY(original.h, original.c, original.y, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY AlphaSpread(this IRandom random, ColorHCY original)
		{
			return new ColorHCY(original.h, original.c, original.y, random.FloatCC());
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the opacity can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY AlphaSpread(this IRandom random, ColorHCY original, float maxAbsoluteSpread)
		{
			return new ColorHCY(original.h, original.c, original.y, random.Spread(original.a, maxAbsoluteSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the opacity can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the opacity can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY AlphaSpread(this IRandom random, ColorHCY original, float minSpread, float maxSpread)
		{
			return new ColorHCY(original.h, original.c, original.y, random.Spread(original.a, minSpread, maxSpread));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The far end of the range within which the opacity can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY AlphaLerp(this IRandom random, ColorHCY original, float target)
		{
			return new ColorHCY(original.h, original.c, original.y, random.RangeCC(original.a, target));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the opacity toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="target">The color whose opacity indicates the far end of the range within which the opacity can change.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY AlphaLerp(this IRandom random, ColorHCY original, ColorHCY target)
		{
			return new ColorHCY(original.h, original.c, original.y, random.RangeCC(original.a, target.a));
		}

		#endregion

		#region Private Helper Functions

		private static float Shift(this IRandom random, float original, float maxAbsoluteShift)
		{
			return random.RangeCC(Mathf.Max(0f, original - maxAbsoluteShift), Mathf.Min(original + maxAbsoluteShift, 1f));
		}

		private static float Shift(this IRandom random, float original, float minShift, float maxShift)
		{
			return random.RangeCC(Mathf.Clamp01(original + minShift), Mathf.Clamp01(original + maxShift));
		}

		private static float ShiftClamped(this IRandom random, float original, float maxAbsoluteShift, float minValue, float maxValue)
		{
			return random.RangeCC(Mathf.Max(minValue, original - maxAbsoluteShift), Mathf.Min(original + maxAbsoluteShift, maxValue));
		}

		private static float ShiftClamped(this IRandom random, float original, float minShift, float maxShift, float minValue, float maxValue)
		{
			return random.RangeCC(Mathf.Clamp(original + minShift, minValue, maxValue), Mathf.Clamp(original + maxShift, minValue, maxValue));
		}

		private static float ShiftRepeated(this IRandom random, float original, float maxAbsoluteShift)
		{
			return Mathf.Repeat(random.RangeCC(original - maxAbsoluteShift, original + maxAbsoluteShift), 1f);
		}

		private static float ShiftRepeated(this IRandom random, float original, float minShift, float maxShift)
		{
			return Mathf.Repeat(random.RangeCC(original + minShift, original + maxShift), 1f);
		}

		private static float Spread(this IRandom random, float original, float maxAbsoluteSpread)
		{
			return random.RangeCC(original * (1f - maxAbsoluteSpread), original + (1f - original) * maxAbsoluteSpread);
		}

		private static float Spread(this IRandom random, float original, float minSpread, float maxSpread)
		{
			float min = minSpread > 0f ? original + (1f - original) * minSpread : original * (1f + minSpread);
			float max = maxSpread > 0f ? original + (1f - original) * maxSpread : original * (1f + maxSpread);
			return random.RangeCC(min, max);
		}

		private static float SpreadClamped(this IRandom random, float original, float maxAbsoluteSpread, float minValue, float maxValue)
		{
			return random.RangeCC(original * (1f - maxAbsoluteSpread), original + (1f - original) * maxAbsoluteSpread);
		}

		private static float SpreadClamped(this IRandom random, float original, float minSpread, float maxSpread, float minValue, float maxValue)
		{
			float min = minSpread > 0f ? original + (1f - original) * minSpread : original * (1f + minSpread);
			float max = maxSpread > 0f ? original + (1f - original) * maxSpread : original * (1f + maxSpread);
			return random.RangeCC(Mathf.Max(minValue, min), Mathf.Min(max, maxValue));
		}

		private static float SpreadRepeated(this IRandom random, float original, float maxAbsoluteSpread)
		{
			if (maxAbsoluteSpread != 0.5f)
			{
				return Mathf.Repeat(random.RangeCC(original - maxAbsoluteSpread, original + maxAbsoluteSpread), 1f);
			}
			else
			{
				return random.FloatCO();
			}
		}

		private static float SpreadRepeated(this IRandom random, float original, float minSpread, float maxSpread)
		{
			if (Mathf.Abs(maxSpread - minSpread) < 1f)
			{
				return Mathf.Repeat(random.RangeCC(original + minSpread, original + maxSpread), 1f);
			}
			else
			{
				return random.FloatCO();
			}
		}

		private static float LerpRepeated(this IRandom random, float original, float target)
		{
			float delta = target - original;
			if (delta > 0f)
			{
				if (delta < 0.5f)
				{
					return random.RangeCC(original, target);
				}
				else if (delta > 0.5f)
				{
					return Mathf.Repeat(random.RangeCC(target, original + 1f), 1f);
				}
				else
				{
					return random.FloatCO();
				}
			}
			else if (delta < 0f)
			{
				if (delta > -0.5f)
				{
					return random.RangeCC(target, original);
				}
				else if (delta < -0.5f)
				{
					return Mathf.Repeat(random.RangeCC(original, target + 1f), 1f);
				}
				else
				{
					return random.FloatCO();
				}
			}
			else
			{
				return original;
			}
		}

		private static Color ChangeIntensity(Color original, float oldIntensity, float newIntensity)
		{
			if (newIntensity < oldIntensity)
			{
				float multiplier = newIntensity / oldIntensity;
				return new Color(
					original.r * multiplier,
					original.g * multiplier,
					original.b * multiplier,
					original.a);
			}
			else if (newIntensity > oldIntensity)
			{
				float multiplier = (newIntensity - 1f) / (oldIntensity - 1f);
				return new Color(
					(original.r - 1f) * multiplier + 1f,
					(original.g - 1f) * multiplier + 1f,
					(original.b - 1f) * multiplier + 1f,
					original.a);
			}
			else
			{
				return original;
			}
		}

		private static ColorHCV Change(ColorHCV hcv, System.Func<ColorHCV> generator)
		{
			int maxIterations = hcv.canConvertToRGB ? 100 : 5; // If the input color already can't convert to RGB, then there's no guarantee that the generator will produce a convertible color, so be much more eager to give up in that case.
			int iterations = 0;

			ColorHCV hcvRandom;
			do
			{
				hcvRandom = generator();
				++iterations;
			}
			while (!hcvRandom.canConvertToRGB && iterations < maxIterations);

			return hcvRandom;
		}

		private static ColorCMY ChangeIntensity(ColorCMY original, float oldIntensity, float newIntensity)
		{
			if (newIntensity < oldIntensity)
			{
				float multiplier = newIntensity / oldIntensity;
				return new ColorCMY(
					(original.c - 1f) * multiplier + 1f,
					(original.m - 1f) * multiplier + 1f,
					(original.y - 1f) * multiplier + 1f,
					original.a);

			}
			else if (newIntensity > oldIntensity)
			{
				float multiplier = (newIntensity - 1f) / (oldIntensity - 1f);
				return new ColorCMY(
					original.c * multiplier,
					original.m * multiplier,
					original.y * multiplier,
					original.a);
			}
			else
			{
				return original;
			}
		}

		private static ColorHCL Change(ColorHCL hcl, System.Func<ColorHCL> generator)
		{
			int maxIterations = hcl.canConvertToRGB ? 100 : 5; // If the input color already can't convert to RGB, then there's no guarantee that the generator will produce a convertible color, so be much more eager to give up in that case.
			int iterations = 0;

			ColorHCL hclRandom;
			do
			{
				hclRandom = generator();
				++iterations;
			}
			while (!hclRandom.canConvertToRGB && iterations < maxIterations);

			return hclRandom;
		}

		private static ColorHCY Change(ColorHCY hcy, System.Func<ColorHCY> generator)
		{
			int maxIterations = hcy.canConvertToRGB ? 100 : 5; // If the input color already can't convert to RGB, then there's no guarantee that the generator will produce a convertible color, so be much more eager to give up in that case.
			int iterations = 0;

			ColorHCY hcyRandom;
			do
			{
				hcyRandom = generator();
				++iterations;
			}
			while (!hcyRandom.canConvertToRGB && iterations < maxIterations);

			return hcyRandom;
		}

		#endregion
	}
}
