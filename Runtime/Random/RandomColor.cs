/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

#if MAKEIT_COLORFUL_2_0_OR_NEWER

using UnityEngine;
using MakeIt.Colorful;

namespace MakeIt.Random
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
		public static Color ColorGray(this IRandom random)
		{
			float value = random.FloatCC();
			return new Color(value, value, value);
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from completely black to red.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark red color.</returns>
		public static Color ColorDarkRed(this IRandom random)
		{
			return new Color(random.FloatCC(), 0f, 0f);
		}

		/// <summary>
		/// Generates a random green color, ranging all the way from completely black to green.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark green color.</returns>
		public static Color ColorDarkGreen(this IRandom random)
		{
			return new Color(0f, random.FloatCC(), 0f);
		}

		/// <summary>
		/// Generates a random blue color, ranging all the way from completely black to blue.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark blue color.</returns>
		public static Color ColorDarkBlue(this IRandom random)
		{
			return new Color(0f, 0f, random.FloatCC());
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from red to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light red color.</returns>
		public static Color ColorLightRed(this IRandom random)
		{
			float value = random.FloatCC();
			return new Color(1f, value, value);
		}

		/// <summary>
		/// Generates a random green color, ranging all the way from green to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light green color.</returns>
		public static Color ColorLightGreen(this IRandom random)
		{
			float value = random.FloatCC();
			return new Color(value, 1f, value);
		}

		/// <summary>
		/// Generates a random blue color, ranging all the way from blue to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light blue color.</returns>
		public static Color ColorLightBlue(this IRandom random)
		{
			float value = random.FloatCC();
			return new Color(value, value, 1f);
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from completely black, through red, to complete white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque red color.</returns>
		public static Color ColorRed(this IRandom random)
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
		public static Color ColorGreen(this IRandom random)
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
		public static Color ColorBlue(this IRandom random)
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

		#region Color Categories

		/// <summary>
		/// Generates a random bold color, one that is at maximum saturation and value.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque bold color.</returns>
		public static Color ColorBold(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), 1f, 1f);
		}

		/// <summary>
		/// Generates a random festive color, one that has a high saturation and value.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque festive color.</returns>
		public static Color ColorFestive(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.RangeOC(0.7f, 1f), random.RangeOC(0.8f, 1f));
		}

		/// <summary>
		/// Generates a random pastel color, one that has a low saturation and high value.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque pastel color.</returns>
		public static Color ColorPastel(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.RangeOC(0.2f, 0.4f), random.RangeOC(0.8f, 1f));
		}

		/// <summary>
		/// Generates a random pale color, one that has a very low saturation and moderately high value.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque pale color.</returns>
		public static Color ColorPale(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.RangeOC(0.05f, 0.2f), random.RangeOC(0.6f, 0.8f));
		}

		/// <summary>
		/// Generates a random neutral color, one that has an extremely low saturation.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque neutral color.</returns>
		public static Color ColorNeutral(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.RangeOC(0.05f, 0.1f), random.RangeOC(0.2f, 1f));
		}

		/// <summary>
		/// Generates a random mellow color, one that has a low to moderate saturation and moderate to high value.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque mellow color.</returns>
		public static Color ColorMellow(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.RangeOC(0.3f, 0.5f), random.RangeOC(0.5f, 0.9f));
		}

		/// <summary>
		/// Generates a random somber color, one that has a moderate saturation and lower to moderate value.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque somber color.</returns>
		public static Color ColorSomber(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.RangeOC(0.2f, 0.8f), random.RangeOC(0.2f, 0.6f));
		}

		/// <summary>
		/// Generates a random subdued color, one that has a low to moderate saturation and moderate value.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque subdued color.</returns>
		public static Color ColorSubdued(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.RangeOC(0.1f, 0.5f), random.RangeOC(0.3f, 0.7f));
		}

		/// <summary>
		/// Generates a random deep color, one that has a moderate to high saturation and low to moderate value.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque deep color.</returns>
		public static Color ColorDeep(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.RangeOC(0.6f, 1f), random.RangeOC(0.1f, 0.5f));
		}

		/// <summary>
		/// Generates a random warm color, one that has a hue loosely between red and yellow.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque warm color.</returns>
		public static Color ColorWarm(this IRandom random)
		{
			return new ColorHSV(random.RangeCO(-0.1f, 0.2f), random.RangeOC(0.25f, 1f), random.RangeOC(0.25f, 1f));
		}

		/// <summary>
		/// Generates a random hot color, one that has a hue strictly between red and yellow, with maximum saturation and moderate to high lightness.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque hot color.</returns>
		public static Color ColorHot(this IRandom random)
		{
			return new ColorHSL(random.RangeCO(0f, 0.16667f), 1f, random.RangeOC(0.5f, 1f));
		}

		/// <summary>
		/// Generates a random cool color, one that has a hue loosely between cyan and blue.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque cool color.</returns>
		public static Color ColorCool(this IRandom random)
		{
			return new ColorHSV(random.RangeCO(0.4f, 0.7f), random.RangeOC(0.25f, 1f), random.RangeOC(0.25f, 1f));
		}

		/// <summary>
		/// Generates a random cold color, one that is strictly between cyan and blue, with maximum saturation and low to moderate lightness.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque cold color.</returns>
		public static Color ColorCold(this IRandom random)
		{
			return new ColorHSL(random.RangeCO(0.5f, 0.66667f), 1f, random.RangeOC(0.1f, 0.5f));
		}

		#endregion

		#region RGB

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the red/green/blue color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static Color ColorRGB(this IRandom random)
		{
			return new Color(random.FloatCC(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the red/green/blue color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static Color ColorRGB(this IRandom random, float a)
		{
			return new Color(random.FloatCC(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the red/green/blue color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static Color ColorRGBA(this IRandom random)
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
		public static Color ColorRGBShift(this IRandom random, Color original, float maxAbsoluteRedShift, float maxAbsoluteGreenShift, float maxAbsoluteBlueShift)
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
		public static Color ColorRGBAShift(this IRandom random, Color original, float maxAbsoluteRedShift, float maxAbsoluteGreenShift, float maxAbsoluteBlueShift, float maxAbsoluteAlphaShift)
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
		public static Color ColorRGBShift(this IRandom random, Color original, float minRedShift, float maxRedShift, float minGreenShift, float maxGreenShift, float minBlueShift, float maxBlueShift)
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
		public static Color ColorRGBAShift(this IRandom random, Color original, float minRedShift, float maxRedShift, float minGreenShift, float maxGreenShift, float minBlueShift, float maxBlueShift, float minAlphaShift, float maxAlphaShift)
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
		public static Color ColorRGBSpread(this IRandom random, Color original, float maxAbsoluteRedSpread, float maxAbsoluteGreenSpread, float maxAbsoluteBlueSpread)
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
		public static Color ColorRGBASpread(this IRandom random, Color original, float maxAbsoluteRedSpread, float maxAbsoluteGreenSpread, float maxAbsoluteBlueSpread, float maxAbsoluteAlphaSpread)
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
		public static Color ColorRGBSpread(this IRandom random, Color original, float minRedSpread, float maxRedSpread, float minGreenSpread, float maxGreenSpread, float minBlueSpread, float maxBlueSpread)
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
		public static Color ColorRGBASpread(this IRandom random, Color original, float minRedSpread, float maxRedSpread, float minGreenSpread, float maxGreenSpread, float minBlueSpread, float maxBlueSpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static Color ColorRGBLerp(this IRandom random, Color original, float targetRed, float targetGreen, float targetBlue)
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
		public static Color ColorRGBALerp(this IRandom random, Color original, float targetRed, float targetGreen, float targetBlue, float targetAlpha)
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
		public static Color ColorRGBALerp(this IRandom random, Color original, Color target)
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
		public static Color ColorIntensityShift(this IRandom random, Color original, float maxAbsoluteShift)
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
		public static Color ColorIntensityShift(this IRandom random, Color original, float minShift, float maxShift)
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
		public static Color ColorIntensitySpread(this IRandom random, Color original)
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
		public static Color ColorIntensitySpread(this IRandom random, Color original, float maxAbsoluteSpread)
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
		public static Color ColorIntensitySpread(this IRandom random, Color original, float minSpread, float maxSpread)
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
		public static Color ColorIntensityLerp(this IRandom random, Color original, float target)
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
		public static Color ColorIntensityLerp(this IRandom random, Color original, Color target)
		{
			return random.ColorIntensityLerp(original, (target.r + target.g + target.b) / 3f);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for the red channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color ColorRedShift(this IRandom random, Color original, float maxAbsoluteShift)
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
		public static Color ColorRedShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			return new Color(random.Shift(original.r, minShift, maxShift), original.g, original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the red channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose red channel will be altered.</param>
		/// <returns>A color derived from the original color but with the red channel randomized.</returns>
		public static Color ColorRedSpread(this IRandom random, Color original)
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
		public static Color ColorRedSpread(this IRandom random, Color original, float maxAbsoluteSpread)
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
		public static Color ColorRedSpread(this IRandom random, Color original, float minSpread, float maxSpread)
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
		public static Color ColorRedLerp(this IRandom random, Color original, float target)
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
		public static Color ColorRedLerp(this IRandom random, Color original, Color target)
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
		public static Color ColorGreenShift(this IRandom random, Color original, float maxAbsoluteShift)
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
		public static Color ColorGreenShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			return new Color(original.r, random.Shift(original.g, minShift, maxShift), original.b, original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the green channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose green channel will be altered.</param>
		/// <returns>A color derived from the original color but with the green channel randomized.</returns>
		public static Color ColorGreenSpread(this IRandom random, Color original)
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
		public static Color ColorGreenSpread(this IRandom random, Color original, float maxAbsoluteSpread)
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
		public static Color ColorGreenSpread(this IRandom random, Color original, float minSpread, float maxSpread)
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
		public static Color ColorGreenLerp(this IRandom random, Color original, float target)
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
		public static Color ColorGreenLerp(this IRandom random, Color original, Color target)
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
		public static Color ColorBlueShift(this IRandom random, Color original, float maxAbsoluteShift)
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
		public static Color ColorBlueShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			return new Color(original.r, original.g, random.Shift(original.b, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the blue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose blue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the blue channel randomized.</returns>
		public static Color ColorBlueSpread(this IRandom random, Color original)
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
		public static Color ColorBlueSpread(this IRandom random, Color original, float maxAbsoluteSpread)
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
		public static Color ColorBlueSpread(this IRandom random, Color original, float minSpread, float maxSpread)
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
		public static Color ColorBlueLerp(this IRandom random, Color original, float target)
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
		public static Color ColorBlueLerp(this IRandom random, Color original, Color target)
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
		public static Color ColorAlphaShift(this IRandom random, Color original, float maxAbsoluteShift)
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
		public static Color ColorAlphaShift(this IRandom random, Color original, float minShift, float maxShift)
		{
			return new Color(original.r, original.g, original.b, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static Color ColorAlphaSpread(this IRandom random, Color original)
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
		public static Color ColorAlphaSpread(this IRandom random, Color original, float maxAbsoluteSpread)
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
		public static Color ColorAlphaSpread(this IRandom random, Color original, float minSpread, float maxSpread)
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
		public static Color ColorAlphaLerp(this IRandom random, Color original, float target)
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
		public static Color ColorAlphaLerp(this IRandom random, Color original, Color target)
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
		public static ColorCMY ColorCMY(this IRandom random)
		{
			return new ColorCMY(random.FloatCC(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorCMY ColorCMY(this IRandom random, float a)
		{
			return new ColorCMY(random.FloatCC(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorCMY ColorCMYA(this IRandom random)
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
		public static ColorCMY ColorCMYShift(this IRandom random, ColorCMY original, float maxAbsoluteCyanShift, float maxAbsoluteMagentaShift, float maxAbsoluteYellowShift)
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
		public static ColorCMY ColorCMYAShift(this IRandom random, ColorCMY original, float maxAbsoluteCyanShift, float maxAbsoluteMagentaShift, float maxAbsoluteYellowShift, float maxAbsoluteAlphaShift)
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
		public static ColorCMY ColorCMYShift(this IRandom random, ColorCMY original, float minCyanShift, float maxCyanShift, float minMagentaShift, float maxMagentaShift, float minYellowShift, float maxYellowShift)
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
		public static ColorCMY ColorCMYAShift(this IRandom random, ColorCMY original, float minCyanShift, float maxCyanShift, float minMagentaShift, float maxMagentaShift, float minYellowShift, float maxYellowShift, float minAlphaShift, float maxAlphaShift)
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
		public static ColorCMY ColorCMYSpread(this IRandom random, ColorCMY original, float maxAbsoluteCyanSpread, float maxAbsoluteMagentaSpread, float maxAbsoluteYellowSpread)
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
		public static ColorCMY ColorCMYASpread(this IRandom random, ColorCMY original, float maxAbsoluteCyanSpread, float maxAbsoluteMagentaSpread, float maxAbsoluteYellowSpread, float maxAbsoluteAlphaSpread)
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
		public static ColorCMY ColorCMYSpread(this IRandom random, ColorCMY original, float minCyanSpread, float maxCyanSpread, float minMagentaSpread, float maxMagentaSpread, float minYellowSpread, float maxYellowSpread)
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
		public static ColorCMY ColorCMYASpread(this IRandom random, ColorCMY original, float minCyanSpread, float maxCyanSpread, float minMagentaSpread, float maxMagentaSpread, float minYellowSpread, float maxYellowSpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static ColorCMY ColorCMYLerp(this IRandom random, ColorCMY original, float targetCyan, float targetMagenta, float targetYellow)
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
		public static ColorCMY ColorCMYALerp(this IRandom random, ColorCMY original, float targetCyan, float targetMagenta, float targetYellow, float targetAlpha)
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
		public static ColorCMY ColorCMYALerp(this IRandom random, ColorCMY original, ColorCMY target)
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
		public static ColorCMY ColorIntensityShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
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
		public static ColorCMY ColorIntensityShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
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
		public static ColorCMY ColorIntensitySpread(this IRandom random, ColorCMY original)
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
		public static ColorCMY ColorIntensitySpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
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
		public static ColorCMY ColorIntensitySpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
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
		public static ColorCMY ColorIntensityLerp(this IRandom random, ColorCMY original, float target)
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
		public static ColorCMY ColorIntensityLerp(this IRandom random, ColorCMY original, ColorCMY target)
		{
			return random.ColorIntensityLerp(original, (target.c + target.m + target.y) / 3f);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly selecting a new value for the cyan channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the cyan channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY ColorCyanShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
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
		public static ColorCMY ColorCyanShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			return new ColorCMY(random.Shift(original.c, minShift, maxShift), original.m, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the cyan channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMY ColorCyanSpread(this IRandom random, ColorCMY original)
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
		public static ColorCMY ColorCyanSpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
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
		public static ColorCMY ColorCyanSpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
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
		public static ColorCMY ColorCyanLerp(this IRandom random, ColorCMY original, float target)
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
		public static ColorCMY ColorCyanLerp(this IRandom random, ColorCMY original, ColorCMY target)
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
		public static ColorCMY ColorMagentaShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
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
		public static ColorCMY ColorMagentaShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			return new ColorCMY(original.c, random.Shift(original.m, minShift, maxShift), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the magenta channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMY ColorMagentaSpread(this IRandom random, ColorCMY original)
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
		public static ColorCMY ColorMagentaSpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
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
		public static ColorCMY ColorMagentaSpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
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
		public static ColorCMY ColorMagentaLerp(this IRandom random, ColorCMY original, float target)
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
		public static ColorCMY ColorMagentaLerp(this IRandom random, ColorCMY original, ColorCMY target)
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
		public static ColorCMY ColorYellowShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
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
		public static ColorCMY ColorYellowShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			return new ColorCMY(original.c, original.m, random.Shift(original.y, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the yellow channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMY ColorYellowSpread(this IRandom random, ColorCMY original)
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
		public static ColorCMY ColorYellowSpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
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
		public static ColorCMY ColorYellowSpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
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
		public static ColorCMY ColorYellowLerp(this IRandom random, ColorCMY original, float target)
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
		public static ColorCMY ColorYellowLerp(this IRandom random, ColorCMY original, ColorCMY target)
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
		public static ColorCMY ColorAlphaShift(this IRandom random, ColorCMY original, float maxAbsoluteShift)
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
		public static ColorCMY ColorAlphaShift(this IRandom random, ColorCMY original, float minShift, float maxShift)
		{
			return new ColorCMY(original.c, original.m, original.y, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMY ColorAlphaSpread(this IRandom random, ColorCMY original)
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
		public static ColorCMY ColorAlphaSpread(this IRandom random, ColorCMY original, float maxAbsoluteSpread)
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
		public static ColorCMY ColorAlphaSpread(this IRandom random, ColorCMY original, float minSpread, float maxSpread)
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
		public static ColorCMY ColorAlphaLerp(this IRandom random, ColorCMY original, float target)
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
		public static ColorCMY ColorAlphaLerp(this IRandom random, ColorCMY original, ColorCMY target)
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
		public static ColorCMYK ColorCMYK(this IRandom random)
		{
			return new ColorCMYK(random.FloatCC(), random.FloatCC(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow/key color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorCMYK ColorCMYK(this IRandom random, float a)
		{
			return new ColorCMYK(random.FloatCC(), random.FloatCC(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cyan/magenta/yellow/key color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorCMYK ColorCMYKA(this IRandom random)
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
		public static ColorCMYK ColorCMYKShift(this IRandom random, ColorCMYK original, float maxAbsoluteCyanShift, float maxAbsoluteMagentaShift, float maxAbsoluteYellowShift, float maxAbsoluteKeyShift)
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
		public static ColorCMYK ColorCMYKAShift(this IRandom random, ColorCMYK original, float maxAbsoluteCyanShift, float maxAbsoluteMagentaShift, float maxAbsoluteYellowShift, float maxAbsoluteKeyShift, float maxAbsoluteAlphaShift)
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
		public static ColorCMYK ColorCMYKShift(this IRandom random, ColorCMYK original, float minCyanShift, float maxCyanShift, float minMagentaShift, float maxMagentaShift, float minYellowShift, float maxYellowShift, float minKeyShift, float maxKeyShift)
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
		public static ColorCMYK ColorCMYKAShift(this IRandom random, ColorCMYK original, float minCyanShift, float maxCyanShift, float minMagentaShift, float maxMagentaShift, float minYellowShift, float maxYellowShift, float minKeyShift, float maxKeyShift, float minAlphaShift, float maxAlphaShift)
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
		public static ColorCMYK ColorCMYKSpread(this IRandom random, ColorCMYK original, float maxAbsoluteCyanSpread, float maxAbsoluteMagentaSpread, float maxAbsoluteYellowSpread, float maxAbsoluteKeySpread)
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
		public static ColorCMYK ColorCMYKASpread(this IRandom random, ColorCMYK original, float maxAbsoluteCyanSpread, float maxAbsoluteMagentaSpread, float maxAbsoluteYellowSpread, float maxAbsoluteKeySpread, float maxAbsoluteAlphaSpread)
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
		public static ColorCMYK ColorCMYKSpread(this IRandom random, ColorCMYK original, float minCyanSpread, float maxCyanSpread, float minMagentaSpread, float maxMagentaSpread, float minYellowSpread, float maxYellowSpread, float minKeySpread, float maxKeySpread)
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
		public static ColorCMYK ColorCMYKASpread(this IRandom random, ColorCMYK original, float minCyanSpread, float maxCyanSpread, float minMagentaSpread, float maxMagentaSpread, float minYellowSpread, float maxYellowSpread, float minKeySpread, float maxKeySpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static ColorCMYK ColorCMYKLerp(this IRandom random, ColorCMYK original, float targetCyan, float targetMagenta, float targetYellow, float targetKey)
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
		public static ColorCMYK ColorCMYKALerp(this IRandom random, ColorCMYK original, float targetCyan, float targetMagenta, float targetYellow, float targetKey, float targetAlpha)
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
		public static ColorCMYK ColorCMYKALerp(this IRandom random, ColorCMYK original, ColorCMYK target)
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
		public static ColorCMYK ColorCyanShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
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
		public static ColorCMYK ColorCyanShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(random.Shift(original.c, minShift, maxShift), original.m, original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the cyan channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose cyan channel will be altered.</param>
		/// <returns>A color derived from the original color but with the cyan channel randomized.</returns>
		public static ColorCMYK ColorCyanSpread(this IRandom random, ColorCMYK original)
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
		public static ColorCMYK ColorCyanSpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
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
		public static ColorCMYK ColorCyanSpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
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
		public static ColorCMYK ColorCyanLerp(this IRandom random, ColorCMYK original, float target)
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
		public static ColorCMYK ColorCyanLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
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
		public static ColorCMYK ColorMagentaShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
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
		public static ColorCMYK ColorMagentaShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(original.c, random.Shift(original.m, minShift, maxShift), original.y, original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the magenta channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose magenta channel will be altered.</param>
		/// <returns>A color derived from the original color but with the magenta channel randomized.</returns>
		public static ColorCMYK ColorMagentaSpread(this IRandom random, ColorCMYK original)
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
		public static ColorCMYK ColorMagentaSpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
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
		public static ColorCMYK ColorMagentaSpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
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
		public static ColorCMYK ColorMagentaLerp(this IRandom random, ColorCMYK original, float target)
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
		public static ColorCMYK ColorMagentaLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
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
		public static ColorCMYK ColorYellowShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
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
		public static ColorCMYK ColorYellowShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(original.c, original.m, random.Shift(original.y, minShift, maxShift), original.k, original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the yellow channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose yellow channel will be altered.</param>
		/// <returns>A color derived from the original color but with the yellow channel randomized.</returns>
		public static ColorCMYK ColorYellowSpread(this IRandom random, ColorCMYK original)
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
		public static ColorCMYK ColorYellowSpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
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
		public static ColorCMYK ColorYellowSpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
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
		public static ColorCMYK ColorYellowLerp(this IRandom random, ColorCMYK original, float target)
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
		public static ColorCMYK ColorYellowLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
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
		public static ColorCMYK ColorKeyShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
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
		public static ColorCMYK ColorKeyShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Shift(original.k, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the key channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose key channel will be altered.</param>
		/// <returns>A color derived from the original color but with the key channel randomized.</returns>
		public static ColorCMYK ColorKeySpread(this IRandom random, ColorCMYK original)
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
		public static ColorCMYK ColorKeySpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
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
		public static ColorCMYK ColorKeySpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
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
		public static ColorCMYK ColorKeyLerp(this IRandom random, ColorCMYK original, float target)
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
		public static ColorCMYK ColorKeyLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
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
		public static ColorCMYK ColorAlphaShift(this IRandom random, ColorCMYK original, float maxAbsoluteShift)
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
		public static ColorCMYK ColorAlphaShift(this IRandom random, ColorCMYK original, float minShift, float maxShift)
		{
			return new ColorCMYK(original.c, original.m, original.y, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the cyan/magenta/yellow/key color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorCMYK ColorAlphaSpread(this IRandom random, ColorCMYK original)
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
		public static ColorCMYK ColorAlphaSpread(this IRandom random, ColorCMYK original, float maxAbsoluteSpread)
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
		public static ColorCMYK ColorAlphaSpread(this IRandom random, ColorCMYK original, float minSpread, float maxSpread)
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
		public static ColorCMYK ColorAlphaLerp(this IRandom random, ColorCMYK original, float target)
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
		public static ColorCMYK ColorAlphaLerp(this IRandom random, ColorCMYK original, ColorCMYK target)
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
		public static ColorHSV ColorHSV(this IRandom random)
		{
			return new ColorHSV(random.FloatCO(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/value color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHSV ColorHSV(this IRandom random, float a)
		{
			return new ColorHSV(random.FloatCO(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/value color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHSV ColorHSVA(this IRandom random)
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
		public static ColorHSV ColorHSVShift(this IRandom random, ColorHSV original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteValueShift)
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
		public static ColorHSV ColorHSVAShift(this IRandom random, ColorHSV original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteValueShift, float maxAbsoluteAlphaShift)
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
		public static ColorHSV ColorHSVShift(this IRandom random, ColorHSV original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minValueShift, float maxValueShift)
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
		public static ColorHSV ColorHSVAShift(this IRandom random, ColorHSV original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minValueShift, float maxValueShift, float minAlphaShift, float maxAlphaShift)
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
		public static ColorHSV ColorHSVSpread(this IRandom random, ColorHSV original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteValueSpread)
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
		public static ColorHSV ColorHSVASpread(this IRandom random, ColorHSV original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteValueSpread, float maxAbsoluteAlphaSpread)
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
		public static ColorHSV ColorHSVSpread(this IRandom random, ColorHSV original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minValueSpread, float maxValueSpread)
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
		public static ColorHSV ColorHSVASpread(this IRandom random, ColorHSV original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minValueSpread, float maxValueSpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static ColorHSV ColorHSVLerp(this IRandom random, ColorHSV original, float targetHue, float targetSaturation, float targetValue)
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
		public static ColorHSV ColorHSVALerp(this IRandom random, ColorHSV original, float targetHue, float targetSaturation, float targetValue, float targetAlpha)
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
		public static ColorHSV ColorHSVALerp(this IRandom random, ColorHSV original, ColorHSV target)
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
		public static ColorHSV ColorHueShift(this IRandom random, ColorHSV original, float maxAbsoluteShift)
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
		public static ColorHSV ColorHueShift(this IRandom random, ColorHSV original, float minShift, float maxShift)
		{
			return new ColorHSV(random.ShiftRepeated(original.h, minShift, maxShift), original.s, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSV ColorHueSpread(this IRandom random, ColorHSV original)
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
		public static ColorHSV ColorHueSpread(this IRandom random, ColorHSV original, float maxAbsoluteSpread)
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
		public static ColorHSV ColorHueSpread(this IRandom random, ColorHSV original, float minSpread, float maxSpread)
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
		public static ColorHSV ColorHueLerp(this IRandom random, ColorHSV original, float target)
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
		public static ColorHSV ColorHueLerp(this IRandom random, ColorHSV original, ColorHSV target)
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
		public static ColorHSV ColorSaturationShift(this IRandom random, ColorHSV original, float maxAbsoluteShift)
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
		public static ColorHSV ColorSaturationShift(this IRandom random, ColorHSV original, float minShift, float maxShift)
		{
			return new ColorHSV(original.h, random.Shift(original.s, minShift, maxShift), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSV ColorSaturationSpread(this IRandom random, ColorHSV original)
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
		public static ColorHSV ColorSaturationSpread(this IRandom random, ColorHSV original, float maxAbsoluteSpread)
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
		public static ColorHSV ColorSaturationSpread(this IRandom random, ColorHSV original, float minSpread, float maxSpread)
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
		public static ColorHSV ColorSaturationLerp(this IRandom random, ColorHSV original, float target)
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
		public static ColorHSV ColorSaturationLerp(this IRandom random, ColorHSV original, ColorHSV target)
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
		public static ColorHSV ColorValueShift(this IRandom random, ColorHSV original, float maxAbsoluteShift)
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
		public static ColorHSV ColorValueShift(this IRandom random, ColorHSV original, float minShift, float maxShift)
		{
			return new ColorHSV(original.h, original.s, random.Shift(original.v, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHSV ColorValueSpread(this IRandom random, ColorHSV original)
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
		public static ColorHSV ColorValueSpread(this IRandom random, ColorHSV original, float maxAbsoluteSpread)
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
		public static ColorHSV ColorValueSpread(this IRandom random, ColorHSV original, float minSpread, float maxSpread)
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
		public static ColorHSV ColorValueLerp(this IRandom random, ColorHSV original, float target)
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
		public static ColorHSV ColorValueLerp(this IRandom random, ColorHSV original, ColorHSV target)
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
		public static ColorHSV ColorAlphaShift(this IRandom random, ColorHSV original, float maxAbsoluteShift)
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
		public static ColorHSV ColorAlphaShift(this IRandom random, ColorHSV original, float minShift, float maxShift)
		{
			return new ColorHSV(original.h, original.s, original.v, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSV ColorAlphaSpread(this IRandom random, ColorHSV original)
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
		public static ColorHSV ColorAlphaSpread(this IRandom random, ColorHSV original, float maxAbsoluteSpread)
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
		public static ColorHSV ColorAlphaSpread(this IRandom random, ColorHSV original, float minSpread, float maxSpread)
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
		public static ColorHSV ColorAlphaLerp(this IRandom random, ColorHSV original, float target)
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
		public static ColorHSV ColorAlphaLerp(this IRandom random, ColorHSV original, ColorHSV target)
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
		public static ColorHCV ColorHCV(this IRandom random)
		{
			return random.ColorHCV(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/value color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHCV ColorHCV(this IRandom random, float a)
		{
			float hue = random.FloatCO();
			Vector2 chromaValue = random.PointWithinTriangle(new Vector2(1f, Colorful.ColorHCV.GetValueAtMaxChroma()), new Vector2(0f, 1f));
			return new ColorHCV(hue, chromaValue.x, chromaValue.y, a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/value color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHCV ColorHCVA(this IRandom random)
		{
			return random.ColorHCV(random.FloatCC());
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
		public static ColorHCV ColorHCVShift(this IRandom random, ColorHCV original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteValueShift)
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
		public static ColorHCV ColorHCVAShift(this IRandom random, ColorHCV original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteValueShift, float maxAbsoluteAlphaShift)
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
		public static ColorHCV ColorHCVShift(this IRandom random, ColorHCV original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minValueShift, float maxValueShift)
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
		public static ColorHCV ColorHCVAShift(this IRandom random, ColorHCV original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minValueShift, float maxValueShift, float minAlphaShift, float maxAlphaShift)
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
		public static ColorHCV ColorHCVSpread(this IRandom random, ColorHCV original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteValueSpread)
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
		public static ColorHCV ColorHCVASpread(this IRandom random, ColorHCV original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteValueSpread, float maxAbsoluteAlphaSpread)
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
		public static ColorHCV ColorHCVSpread(this IRandom random, ColorHCV original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minValueSpread, float maxValueSpread)
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
		public static ColorHCV ColorHCVASpread(this IRandom random, ColorHCV original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minValueSpread, float maxValueSpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static ColorHCV ColorHCVLerp(this IRandom random, ColorHCV original, float targetHue, float targetChroma, float targetValue)
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
		public static ColorHCV ColorHCVALerp(this IRandom random, ColorHCV original, float targetHue, float targetChroma, float targetValue, float targetAlpha)
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
		public static ColorHCV ColorHCVALerp(this IRandom random, ColorHCV original, ColorHCV target)
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
		public static ColorHCV ColorHueShift(this IRandom random, ColorHCV original, float maxAbsoluteShift)
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
		public static ColorHCV ColorHueShift(this IRandom random, ColorHCV original, float minShift, float maxShift)
		{
			return new ColorHCV(random.ShiftRepeated(original.h, minShift, maxShift), original.c, original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCV ColorHueSpread(this IRandom random, ColorHCV original)
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
		public static ColorHCV ColorHueSpread(this IRandom random, ColorHCV original, float maxAbsoluteSpread)
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
		public static ColorHCV ColorHueSpread(this IRandom random, ColorHCV original, float minSpread, float maxSpread)
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
		public static ColorHCV ColorHueLerp(this IRandom random, ColorHCV original, float target)
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
		public static ColorHCV ColorHueLerp(this IRandom random, ColorHCV original, ColorHCV target)
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
		public static ColorHCV ColorChromaShift(this IRandom random, ColorHCV original, float maxAbsoluteShift)
		{
			return new ColorHCV(original.h, random.ShiftClamped(original.c, maxAbsoluteShift, 0f, Colorful.ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ColorChromaShift(this IRandom random, ColorHCV original, float minShift, float maxShift)
		{
			return new ColorHCV(original.h, random.ShiftClamped(original.c, minShift, maxShift, 0f, Colorful.ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ColorChromaSpread(this IRandom random, ColorHCV original)
		{
			return new ColorHCV(original.h, random.RangeCC(0f, Colorful.ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ColorChromaSpread(this IRandom random, ColorHCV original, float maxAbsoluteSpread)
		{
			return new ColorHCV(original.h, random.SpreadClamped(original.c, maxAbsoluteSpread, 0f, Colorful.ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ColorChromaSpread(this IRandom random, ColorHCV original, float minSpread, float maxSpread)
		{
			return new ColorHCV(original.h, random.SpreadClamped(original.c, minSpread, maxSpread, 0f, Colorful.ColorHCV.GetMaxChroma(original.v)), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ColorChromaLerp(this IRandom random, ColorHCV original, float target)
		{
			return new ColorHCV(original.h, random.RangeCC(original.c, Mathf.Min(target, Colorful.ColorHCV.GetMaxChroma(original.v))), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The color whose chroma channel indicates the far end of the range within which the chroma channel can change.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCV ColorChromaLerp(this IRandom random, ColorHCV original, ColorHCV target)
		{
			return new ColorHCV(original.h, random.RangeCC(original.c, Mathf.Min(target.c, Colorful.ColorHCV.GetMaxChroma(original.v))), original.v, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the value channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ColorValueShift(this IRandom random, ColorHCV original, float maxAbsoluteShift)
		{
			float lMin, lMax;
			Colorful.ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
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
		public static ColorHCV ColorValueShift(this IRandom random, ColorHCV original, float minShift, float maxShift)
		{
			float lMin, lMax;
			Colorful.ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.ShiftClamped(original.v, minShift, maxShift, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ColorValueSpread(this IRandom random, ColorHCV original)
		{
			float lMin, lMax;
			Colorful.ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.RangeCC(lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the value channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the value channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ColorValueSpread(this IRandom random, ColorHCV original, float maxAbsoluteSpread)
		{
			float lMin, lMax;
			Colorful.ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
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
		public static ColorHCV ColorValueSpread(this IRandom random, ColorHCV original, float minSpread, float maxSpread)
		{
			float lMin, lMax;
			Colorful.ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.SpreadClamped(original.v, minSpread, maxSpread, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the value channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="target">The far end of the range within which the value channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ColorValueLerp(this IRandom random, ColorHCV original, float target)
		{
			float lMin, lMax;
			Colorful.ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.RangeCC(Mathf.Max(lMin, original.v), Mathf.Min(target, lMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by linearly interpolating the value channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose value channel will be altered.</param>
		/// <param name="target">The color whose value channel indicates the far end of the range within which the value channel can change.</param>
		/// <returns>A color derived from the original color but with the value channel randomized.</returns>
		public static ColorHCV ColorValueLerp(this IRandom random, ColorHCV original, ColorHCV target)
		{
			float lMin, lMax;
			Colorful.ColorHCV.GetMinMaxValue(original.c, out lMin, out lMax);
			return new ColorHCV(original.h, original.c, random.RangeCC(Mathf.Max(lMin, original.v), Mathf.Min(target.v, lMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV ColorAlphaShift(this IRandom random, ColorHCV original, float maxAbsoluteShift)
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
		public static ColorHCV ColorAlphaShift(this IRandom random, ColorHCV original, float minShift, float maxShift)
		{
			return new ColorHCV(original.h, original.c, original.v, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/value color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCV ColorAlphaSpread(this IRandom random, ColorHCV original)
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
		public static ColorHCV ColorAlphaSpread(this IRandom random, ColorHCV original, float maxAbsoluteSpread)
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
		public static ColorHCV ColorAlphaSpread(this IRandom random, ColorHCV original, float minSpread, float maxSpread)
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
		public static ColorHCV ColorAlphaLerp(this IRandom random, ColorHCV original, float target)
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
		public static ColorHCV ColorAlphaLerp(this IRandom random, ColorHCV original, ColorHCV target)
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
		public static ColorHSL ColorHSL(this IRandom random)
		{
			return new ColorHSL(random.FloatCO(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/lightness color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHSL ColorHSL(this IRandom random, float a)
		{
			return new ColorHSL(random.FloatCO(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/lightness color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHSL ColorHSLA(this IRandom random)
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
		public static ColorHSL ColorHSLShift(this IRandom random, ColorHSL original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteLightnessShift)
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
		public static ColorHSL ColorHSLAShift(this IRandom random, ColorHSL original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteLightnessShift, float maxAbsoluteAlphaShift)
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
		public static ColorHSL ColorHSLShift(this IRandom random, ColorHSL original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minLightnessShift, float maxLightnessShift)
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
		public static ColorHSL ColorHSLAShift(this IRandom random, ColorHSL original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minLightnessShift, float maxLightnessShift, float minAlphaShift, float maxAlphaShift)
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
		public static ColorHSL ColorHSLSpread(this IRandom random, ColorHSL original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteLightnessSpread)
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
		public static ColorHSL ColorHSLASpread(this IRandom random, ColorHSL original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteLightnessSpread, float maxAbsoluteAlphaSpread)
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
		public static ColorHSL ColorHSLSpread(this IRandom random, ColorHSL original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minLightnessSpread, float maxLightnessSpread)
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
		public static ColorHSL ColorHSLASpread(this IRandom random, ColorHSL original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minLightnessSpread, float maxLightnessSpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static ColorHSL ColorHSLLerp(this IRandom random, ColorHSL original, float targetHue, float targetSaturation, float targetLightness)
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
		public static ColorHSL ColorHSLALerp(this IRandom random, ColorHSL original, float targetHue, float targetSaturation, float targetLightness, float targetAlpha)
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
		public static ColorHSL ColorHSLALerp(this IRandom random, ColorHSL original, ColorHSL target)
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
		public static ColorHSL ColorHueShift(this IRandom random, ColorHSL original, float maxAbsoluteShift)
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
		public static ColorHSL ColorHueShift(this IRandom random, ColorHSL original, float minShift, float maxShift)
		{
			return new ColorHSL(random.ShiftRepeated(original.h, minShift, maxShift), original.s, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSL ColorHueSpread(this IRandom random, ColorHSL original)
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
		public static ColorHSL ColorHueSpread(this IRandom random, ColorHSL original, float maxAbsoluteSpread)
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
		public static ColorHSL ColorHueSpread(this IRandom random, ColorHSL original, float minSpread, float maxSpread)
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
		public static ColorHSL ColorHueLerp(this IRandom random, ColorHSL original, float target)
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
		public static ColorHSL ColorHueLerp(this IRandom random, ColorHSL original, ColorHSL target)
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
		public static ColorHSL ColorSaturationShift(this IRandom random, ColorHSL original, float maxAbsoluteShift)
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
		public static ColorHSL ColorSaturationShift(this IRandom random, ColorHSL original, float minShift, float maxShift)
		{
			return new ColorHSL(original.h, random.Shift(original.s, minShift, maxShift), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSL ColorSaturationSpread(this IRandom random, ColorHSL original)
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
		public static ColorHSL ColorSaturationSpread(this IRandom random, ColorHSL original, float maxAbsoluteSpread)
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
		public static ColorHSL ColorSaturationSpread(this IRandom random, ColorHSL original, float minSpread, float maxSpread)
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
		public static ColorHSL ColorSaturationLerp(this IRandom random, ColorHSL original, float target)
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
		public static ColorHSL ColorSaturationLerp(this IRandom random, ColorHSL original, ColorHSL target)
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
		public static ColorHSL ColorLightnessShift(this IRandom random, ColorHSL original, float maxAbsoluteShift)
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
		public static ColorHSL ColorLightnessShift(this IRandom random, ColorHSL original, float minShift, float maxShift)
		{
			return new ColorHSL(original.h, original.s, random.Shift(original.l, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHSL ColorLightnessSpread(this IRandom random, ColorHSL original)
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
		public static ColorHSL ColorLightnessSpread(this IRandom random, ColorHSL original, float maxAbsoluteSpread)
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
		public static ColorHSL ColorLightnessSpread(this IRandom random, ColorHSL original, float minSpread, float maxSpread)
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
		public static ColorHSL ColorLightnessLerp(this IRandom random, ColorHSL original, float target)
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
		public static ColorHSL ColorLightnessLerp(this IRandom random, ColorHSL original, ColorHSL target)
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
		public static ColorHSL ColorAlphaShift(this IRandom random, ColorHSL original, float maxAbsoluteShift)
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
		public static ColorHSL ColorAlphaShift(this IRandom random, ColorHSL original, float minShift, float maxShift)
		{
			return new ColorHSL(original.h, original.s, original.l, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSL ColorAlphaSpread(this IRandom random, ColorHSL original)
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
		public static ColorHSL ColorAlphaSpread(this IRandom random, ColorHSL original, float maxAbsoluteSpread)
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
		public static ColorHSL ColorAlphaSpread(this IRandom random, ColorHSL original, float minSpread, float maxSpread)
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
		public static ColorHSL ColorAlphaLerp(this IRandom random, ColorHSL original, float target)
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
		public static ColorHSL ColorAlphaLerp(this IRandom random, ColorHSL original, ColorHSL target)
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
		public static ColorHCL ColorHCL(this IRandom random)
		{
			return random.ColorHCL(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/lightness color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHCL ColorHCL(this IRandom random, float a)
		{
			float hue = random.FloatCO();
			Vector2 chromaLightness = random.PointWithinTriangle(new Vector2(1f, Colorful.ColorHCL.GetLightnessAtMaxChroma()), new Vector2(0f, 1f));
			return new ColorHCL(hue, chromaLightness.x, chromaLightness.y, a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/lightness color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHCL ColorHCLA(this IRandom random)
		{
			return random.ColorHCL(random.FloatCC());
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
		public static ColorHCL ColorHCLShift(this IRandom random, ColorHCL original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteLightnessShift)
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
		public static ColorHCL ColorHCLAShift(this IRandom random, ColorHCL original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteLightnessShift, float maxAbsoluteAlphaShift)
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
		public static ColorHCL ColorHCLShift(this IRandom random, ColorHCL original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minLightnessShift, float maxLightnessShift)
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
		public static ColorHCL ColorHCLAShift(this IRandom random, ColorHCL original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minLightnessShift, float maxLightnessShift, float minAlphaShift, float maxAlphaShift)
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
		public static ColorHCL ColorHCLSpread(this IRandom random, ColorHCL original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteLightnessSpread)
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
		public static ColorHCL ColorHCLASpread(this IRandom random, ColorHCL original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteLightnessSpread, float maxAbsoluteAlphaSpread)
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
		public static ColorHCL ColorHCLSpread(this IRandom random, ColorHCL original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minLightnessSpread, float maxLightnessSpread)
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
		public static ColorHCL ColorHCLASpread(this IRandom random, ColorHCL original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minLightnessSpread, float maxLightnessSpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static ColorHCL ColorHCLLerp(this IRandom random, ColorHCL original, float targetHue, float targetChroma, float targetLightness)
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
		public static ColorHCL ColorHCLALerp(this IRandom random, ColorHCL original, float targetHue, float targetChroma, float targetLightness, float targetAlpha)
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
		public static ColorHCL ColorHCLALerp(this IRandom random, ColorHCL original, ColorHCL target)
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
		public static ColorHCL ColorHueShift(this IRandom random, ColorHCL original, float maxAbsoluteShift)
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
		public static ColorHCL ColorHueShift(this IRandom random, ColorHCL original, float minShift, float maxShift)
		{
			return new ColorHCL(random.ShiftRepeated(original.h, minShift, maxShift), original.c, original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHCL ColorHueSpread(this IRandom random, ColorHCL original)
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
		public static ColorHCL ColorHueSpread(this IRandom random, ColorHCL original, float maxAbsoluteSpread)
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
		public static ColorHCL ColorHueSpread(this IRandom random, ColorHCL original, float minSpread, float maxSpread)
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
		public static ColorHCL ColorHueLerp(this IRandom random, ColorHCL original, float target)
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
		public static ColorHCL ColorHueLerp(this IRandom random, ColorHCL original, ColorHCL target)
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
		public static ColorHCL ColorChromaShift(this IRandom random, ColorHCL original, float maxAbsoluteShift)
		{
			return new ColorHCL(original.h, random.ShiftClamped(original.c, maxAbsoluteShift, 0f, Colorful.ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ColorChromaShift(this IRandom random, ColorHCL original, float minShift, float maxShift)
		{
			return new ColorHCL(original.h, random.ShiftClamped(original.c, minShift, maxShift, 0f, Colorful.ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ColorChromaSpread(this IRandom random, ColorHCL original)
		{
			return new ColorHCL(original.h, random.RangeCC(0f, Colorful.ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ColorChromaSpread(this IRandom random, ColorHCL original, float maxAbsoluteSpread)
		{
			return new ColorHCL(original.h, random.SpreadClamped(original.c, maxAbsoluteSpread, 0f, Colorful.ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ColorChromaSpread(this IRandom random, ColorHCL original, float minSpread, float maxSpread)
		{
			return new ColorHCL(original.h, random.SpreadClamped(original.c, minSpread, maxSpread, 0f, Colorful.ColorHCL.GetMaxChroma(original.l)), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ColorChromaLerp(this IRandom random, ColorHCL original, float target)
		{
			return new ColorHCL(original.h, random.RangeCC(original.c, Mathf.Min(target, Colorful.ColorHCL.GetMaxChroma(original.l))), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The color whose chroma channel indicates the far end of the range within which the chroma channel can change.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCL ColorChromaLerp(this IRandom random, ColorHCL original, ColorHCL target)
		{
			return new ColorHCL(original.h, random.RangeCC(original.c, Mathf.Min(target.c, Colorful.ColorHCL.GetMaxChroma(original.l))), original.l, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the lightness channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL ColorLightnessShift(this IRandom random, ColorHCL original, float maxAbsoluteShift)
		{
			float lMin, lMax;
			Colorful.ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
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
		public static ColorHCL ColorLightnessShift(this IRandom random, ColorHCL original, float minShift, float maxShift)
		{
			float lMin, lMax;
			Colorful.ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.ShiftClamped(original.l, minShift, maxShift, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL ColorLightnessSpread(this IRandom random, ColorHCL original)
		{
			float lMin, lMax;
			Colorful.ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.RangeCC(lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the lightness channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the lightness channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL ColorLightnessSpread(this IRandom random, ColorHCL original, float maxAbsoluteSpread)
		{
			float lMin, lMax;
			Colorful.ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
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
		public static ColorHCL ColorLightnessSpread(this IRandom random, ColorHCL original, float minSpread, float maxSpread)
		{
			float lMin, lMax;
			Colorful.ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.SpreadClamped(original.l, minSpread, maxSpread, lMin, lMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the lightness channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="target">The far end of the range within which the lightness channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL ColorLightnessLerp(this IRandom random, ColorHCL original, float target)
		{
			float lMin, lMax;
			Colorful.ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.RangeCC(Mathf.Max(lMin, original.l), Mathf.Min(target, lMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by linearly interpolating the lightness channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose lightness channel will be altered.</param>
		/// <param name="target">The color whose lightness channel indicates the far end of the range within which the lightness channel can change.</param>
		/// <returns>A color derived from the original color but with the lightness channel randomized.</returns>
		public static ColorHCL ColorLightnessLerp(this IRandom random, ColorHCL original, ColorHCL target)
		{
			float lMin, lMax;
			Colorful.ColorHCL.GetMinMaxLightness(original.c, out lMin, out lMax);
			return new ColorHCL(original.h, original.c, random.RangeCC(Mathf.Max(lMin, original.l), Mathf.Min(target.l, lMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL ColorAlphaShift(this IRandom random, ColorHCL original, float maxAbsoluteShift)
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
		public static ColorHCL ColorAlphaShift(this IRandom random, ColorHCL original, float minShift, float maxShift)
		{
			return new ColorHCL(original.h, original.c, original.l, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/lightness color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCL ColorAlphaSpread(this IRandom random, ColorHCL original)
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
		public static ColorHCL ColorAlphaSpread(this IRandom random, ColorHCL original, float maxAbsoluteSpread)
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
		public static ColorHCL ColorAlphaSpread(this IRandom random, ColorHCL original, float minSpread, float maxSpread)
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
		public static ColorHCL ColorAlphaLerp(this IRandom random, ColorHCL original, float target)
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
		public static ColorHCL ColorAlphaLerp(this IRandom random, ColorHCL original, ColorHCL target)
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
		public static ColorHSY ColorHSY(this IRandom random)
		{
			return new ColorHSY(random.FloatCO(), random.FloatCC(), random.FloatCC(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/luma color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHSY ColorHSY(this IRandom random, float a)
		{
			return new ColorHSY(random.FloatCO(), random.FloatCC(), random.FloatCC(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/saturation/luma color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHSY ColorHSYA(this IRandom random)
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
		public static ColorHSY ColorHSYShift(this IRandom random, ColorHSY original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteLumaShift)
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
		public static ColorHSY ColorHSYAShift(this IRandom random, ColorHSY original, float maxAbsoluteHueShift, float maxAbsoluteSaturationShift, float maxAbsoluteLumaShift, float maxAbsoluteAlphaShift)
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
		public static ColorHSY ColorHSYShift(this IRandom random, ColorHSY original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minLumaShift, float maxLumaShift)
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
		public static ColorHSY ColorHSYAShift(this IRandom random, ColorHSY original, float minHueShift, float maxHueShift, float minSaturationShift, float maxSaturationShift, float minLumaShift, float maxLumaShift, float minAlphaShift, float maxAlphaShift)
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
		public static ColorHSY ColorHSYSpread(this IRandom random, ColorHSY original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteLumaSpread)
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
		public static ColorHSY ColorHSYASpread(this IRandom random, ColorHSY original, float maxAbsoluteHueSpread, float maxAbsoluteSaturationSpread, float maxAbsoluteLumaSpread, float maxAbsoluteAlphaSpread)
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
		public static ColorHSY ColorHSYSpread(this IRandom random, ColorHSY original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minLumaSpread, float maxLumaSpread)
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
		public static ColorHSY ColorHSYASpread(this IRandom random, ColorHSY original, float minHueSpread, float maxHueSpread, float minSaturationSpread, float maxSaturationSpread, float minLumaSpread, float maxLumaSpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static ColorHSY ColorHSYLerp(this IRandom random, ColorHSY original, float targetHue, float targetSaturation, float targetLuma)
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
		public static ColorHSY ColorHSYALerp(this IRandom random, ColorHSY original, float targetHue, float targetSaturation, float targetLuma, float targetAlpha)
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
		public static ColorHSY ColorHSYALerp(this IRandom random, ColorHSY original, ColorHSY target)
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
		public static ColorHSY ColorHueShift(this IRandom random, ColorHSY original, float maxAbsoluteShift)
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
		public static ColorHSY ColorHueShift(this IRandom random, ColorHSY original, float minShift, float maxShift)
		{
			return new ColorHSY(random.ShiftRepeated(original.h, minShift, maxShift), original.s, original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the hue channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose hue channel will be altered.</param>
		/// <returns>A color derived from the original color but with the hue channel randomized.</returns>
		public static ColorHSY ColorHueSpread(this IRandom random, ColorHSY original)
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
		public static ColorHSY ColorHueSpread(this IRandom random, ColorHSY original, float maxAbsoluteSpread)
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
		public static ColorHSY ColorHueSpread(this IRandom random, ColorHSY original, float minSpread, float maxSpread)
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
		public static ColorHSY ColorHueLerp(this IRandom random, ColorHSY original, float target)
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
		public static ColorHSY ColorHueLerp(this IRandom random, ColorHSY original, ColorHSY target)
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
		public static ColorHSY ColorSaturationShift(this IRandom random, ColorHSY original, float maxAbsoluteShift)
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
		public static ColorHSY ColorSaturationShift(this IRandom random, ColorHSY original, float minShift, float maxShift)
		{
			return new ColorHSY(original.h, random.Shift(original.s, minShift, maxShift), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the saturation channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose saturation channel will be altered.</param>
		/// <returns>A color derived from the original color but with the saturation channel randomized.</returns>
		public static ColorHSY ColorSaturationSpread(this IRandom random, ColorHSY original)
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
		public static ColorHSY ColorSaturationSpread(this IRandom random, ColorHSY original, float maxAbsoluteSpread)
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
		public static ColorHSY ColorSaturationSpread(this IRandom random, ColorHSY original, float minSpread, float maxSpread)
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
		public static ColorHSY ColorSaturationLerp(this IRandom random, ColorHSY original, float target)
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
		public static ColorHSY ColorSaturationLerp(this IRandom random, ColorHSY original, ColorHSY target)
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
		public static ColorHSY ColorLumaShift(this IRandom random, ColorHSY original, float maxAbsoluteShift)
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
		public static ColorHSY ColorLumaShift(this IRandom random, ColorHSY original, float minShift, float maxShift)
		{
			return new ColorHSY(original.h, original.s, random.Shift(original.y, minShift, maxShift), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHSY ColorLumaSpread(this IRandom random, ColorHSY original)
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
		public static ColorHSY ColorLumaSpread(this IRandom random, ColorHSY original, float maxAbsoluteSpread)
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
		public static ColorHSY ColorLumaSpread(this IRandom random, ColorHSY original, float minSpread, float maxSpread)
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
		public static ColorHSY ColorLumaLerp(this IRandom random, ColorHSY original, float target)
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
		public static ColorHSY ColorLumaLerp(this IRandom random, ColorHSY original, ColorHSY target)
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
		public static ColorHSY ColorAlphaShift(this IRandom random, ColorHSY original, float maxAbsoluteShift)
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
		public static ColorHSY ColorAlphaShift(this IRandom random, ColorHSY original, float minShift, float maxShift)
		{
			return new ColorHSY(original.h, original.s, original.y, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHSY ColorAlphaSpread(this IRandom random, ColorHSY original)
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
		public static ColorHSY ColorAlphaSpread(this IRandom random, ColorHSY original, float maxAbsoluteSpread)
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
		public static ColorHSY ColorAlphaSpread(this IRandom random, ColorHSY original, float minSpread, float maxSpread)
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
		public static ColorHSY ColorAlphaLerp(this IRandom random, ColorHSY original, float target)
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
		public static ColorHSY ColorAlphaLerp(this IRandom random, ColorHSY original, ColorHSY target)
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
		public static ColorHCY ColorHCY(this IRandom random)
		{
			return random.ColorHCY(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/luma color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHCY ColorHCY(this IRandom random, float a)
		{
			float hue = random.FloatCO();
			Vector2 chromaLuma = random.PointWithinTriangle(new Vector2(1f, Colorful.ColorHCY.GetLumaAtMaxChroma(hue)), new Vector2(0f, 1f));
			return new ColorHCY(hue, chromaLuma.x, chromaLuma.y, a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the hue/chroma/luma color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHCY ColorHCYA(this IRandom random)
		{
			return random.ColorHCY(random.FloatCC());
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
		public static ColorHCY ColorHCYShift(this IRandom random, ColorHCY original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteLumaShift)
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
		public static ColorHCY ColorHCYAShift(this IRandom random, ColorHCY original, float maxAbsoluteHueShift, float maxAbsoluteChromaShift, float maxAbsoluteLumaShift, float maxAbsoluteAlphaShift)
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
		public static ColorHCY ColorHCYShift(this IRandom random, ColorHCY original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minLumaShift, float maxLumaShift)
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
		public static ColorHCY ColorHCYAShift(this IRandom random, ColorHCY original, float minHueShift, float maxHueShift, float minChromaShift, float maxChromaShift, float minLumaShift, float maxLumaShift, float minAlphaShift, float maxAlphaShift)
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
		public static ColorHCY ColorHCYSpread(this IRandom random, ColorHCY original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteLumaSpread)
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
		public static ColorHCY ColorHCYASpread(this IRandom random, ColorHCY original, float maxAbsoluteHueSpread, float maxAbsoluteChromaSpread, float maxAbsoluteLumaSpread, float maxAbsoluteAlphaSpread)
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
		public static ColorHCY ColorHCYSpread(this IRandom random, ColorHCY original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minLumaSpread, float maxLumaSpread)
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
		public static ColorHCY ColorHCYASpread(this IRandom random, ColorHCY original, float minHueSpread, float maxHueSpread, float minChromaSpread, float maxChromaSpread, float minLumaSpread, float maxLumaSpread, float minAlphaSpread, float maxAlphaSpread)
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
		public static ColorHCY ColorHCYLerp(this IRandom random, ColorHCY original, float targetHue, float targetChroma, float targetLuma)
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
		public static ColorHCY ColorHCYALerp(this IRandom random, ColorHCY original, float targetHue, float targetChroma, float targetLuma, float targetAlpha)
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
		public static ColorHCY ColorHCYALerp(this IRandom random, ColorHCY original, ColorHCY target)
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
		public static ColorHCY ColorHueShift(this IRandom random, ColorHCY original, float maxAbsoluteShift)
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
		public static ColorHCY ColorHueShift(this IRandom random, ColorHCY original, float minShift, float maxShift)
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
		public static ColorHCY ColorHueSpread(this IRandom random, ColorHCY original)
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
		public static ColorHCY ColorHueSpread(this IRandom random, ColorHCY original, float maxAbsoluteSpread)
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
		public static ColorHCY ColorHueSpread(this IRandom random, ColorHCY original, float minSpread, float maxSpread)
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
		public static ColorHCY ColorHueLerp(this IRandom random, ColorHCY original, float target)
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
		public static ColorHCY ColorHueLerp(this IRandom random, ColorHCY original, ColorHCY target)
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
		public static ColorHCY ColorChromaShift(this IRandom random, ColorHCY original, float maxAbsoluteShift)
		{
			return new ColorHCY(original.h, random.ShiftClamped(original.c, maxAbsoluteShift, 0f, Colorful.ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the chroma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minShift">The minimum end of the range offset from the current value within which the chroma channel can change.  Specify a negative value to allow for a downward shift, or a positive value to force an upward shift.  Must be less than or equal to <paramref name="maxShift"/>.</param>
		/// <param name="maxShift">The maximum end of the range offset from the current value within which the chroma channel can change.  Specify a positive value to allow for an upward shift, or a negative value to force a downward shift.  Must be greater than or equal to <paramref name="minShift"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ColorChromaShift(this IRandom random, ColorHCY original, float minShift, float maxShift)
		{
			return new ColorHCY(original.h, random.ShiftClamped(original.c, minShift, maxShift, 0f, Colorful.ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ColorChromaSpread(this IRandom random, ColorHCY original)
		{
			return new ColorHCY(original.h, random.RangeCC(0f, Colorful.ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the chroma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ColorChromaSpread(this IRandom random, ColorHCY original, float maxAbsoluteSpread)
		{
			return new ColorHCY(original.h, random.SpreadClamped(original.c, maxAbsoluteSpread, 0f, Colorful.ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the chroma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="minSpread">The minimum end of the proportional range within which the chroma channel can change.  Specify a negative value to allow for a downward spread, or a positive value to force an upward spread.  Must be in the range [-1, +1] and less than or equal to <paramref name="maxSpread"/>.</param>
		/// <param name="maxSpread">The maximum end of the proportional range within which the chroma channel can change.  Specify a positive value to allow for an upward spread, or a negative value to force a downward spread.  Must be in the range [-1, +1] and greater than or equal to <paramref name="minSpread"/>.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ColorChromaSpread(this IRandom random, ColorHCY original, float minSpread, float maxSpread)
		{
			return new ColorHCY(original.h, random.SpreadClamped(original.c, minSpread, maxSpread, 0f, Colorful.ColorHCY.GetMaxChroma(original.h, original.y)), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the chroma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ColorChromaLerp(this IRandom random, ColorHCY original, float target)
		{
			return new ColorHCY(original.h, random.RangeCC(original.c, Mathf.Min(target, Colorful.ColorHCY.GetMaxChroma(original.h, original.y))), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the chroma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose chroma channel will be altered.</param>
		/// <param name="target">The color whose chroma channel indicates the far end of the range within which the chroma channel can change.</param>
		/// <returns>A color derived from the original color but with the chroma channel randomized.</returns>
		public static ColorHCY ColorChromaLerp(this IRandom random, ColorHCY original, ColorHCY target)
		{
			return new ColorHCY(original.h, random.RangeCC(original.c, Mathf.Min(target.c, Colorful.ColorHCY.GetMaxChroma(original.h, original.y))), original.y, original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the luma channel while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY ColorLumaShift(this IRandom random, ColorHCY original, float maxAbsoluteShift)
		{
			float yMin, yMax;
			Colorful.ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
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
		public static ColorHCY ColorLumaShift(this IRandom random, ColorHCY original, float minShift, float maxShift)
		{
			float yMin, yMax;
			Colorful.ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.ShiftClamped(original.y, minShift, maxShift, yMin, yMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY ColorLumaSpread(this IRandom random, ColorHCY original)
		{
			float yMin, yMax;
			Colorful.ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.RangeCC(yMin, yMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the luma channel toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="maxAbsoluteSpread">The largest proportion from the current value toward the minimum or maximum possible value by which the luma channel can change, up or down.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY ColorLumaSpread(this IRandom random, ColorHCY original, float maxAbsoluteSpread)
		{
			float yMin, yMax;
			Colorful.ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
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
		public static ColorHCY ColorLumaSpread(this IRandom random, ColorHCY original, float minSpread, float maxSpread)
		{
			float yMin, yMax;
			Colorful.ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.SpreadClamped(original.y, minSpread, maxSpread, yMin, yMax), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the luma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="target">The far end of the range within which the luma channel can change.  Must be in the range [0, +1].</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY ColorLumaLerp(this IRandom random, ColorHCY original, float target)
		{
			float yMin, yMax;
			Colorful.ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.RangeCC(Mathf.Max(yMin, original.y), Mathf.Min(target, yMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by linearly interpolating the luma channel toward the specified target by a random amount while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose luma channel will be altered.</param>
		/// <param name="target">The color whose luma channel indicates the far end of the range within which the luma channel can change.</param>
		/// <returns>A color derived from the original color but with the luma channel randomized.</returns>
		public static ColorHCY ColorLumaLerp(this IRandom random, ColorHCY original, ColorHCY target)
		{
			float yMin, yMax;
			Colorful.ColorHCY.GetMinMaxLuma(original.h, original.c, out yMin, out yMax);
			return new ColorHCY(original.h, original.c, random.RangeCC(Mathf.Max(yMin, original.y), Mathf.Min(target.y, yMax)), original.a);
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly selecting a new value for the opacity while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <param name="maxAbsoluteShift">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY ColorAlphaShift(this IRandom random, ColorHCY original, float maxAbsoluteShift)
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
		public static ColorHCY ColorAlphaShift(this IRandom random, ColorHCY original, float minShift, float maxShift)
		{
			return new ColorHCY(original.h, original.c, original.y, random.Shift(original.a, minShift, maxShift));
		}

		/// <summary>
		/// Generates a random color in the hue/chroma/luma color space by randomly spreading the opacity toward its minimum or maximum possible value while keeping all other channels the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="original">The original color whose opacity will be altered.</param>
		/// <returns>A color derived from the original color but with the opacity randomized.</returns>
		public static ColorHCY ColorAlphaSpread(this IRandom random, ColorHCY original)
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
		public static ColorHCY ColorAlphaSpread(this IRandom random, ColorHCY original, float maxAbsoluteSpread)
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
		public static ColorHCY ColorAlphaSpread(this IRandom random, ColorHCY original, float minSpread, float maxSpread)
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
		public static ColorHCY ColorAlphaLerp(this IRandom random, ColorHCY original, float target)
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
		public static ColorHCY ColorAlphaLerp(this IRandom random, ColorHCY original, ColorHCY target)
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

		private static ColorHCV Change(ColorHCV hcv, System.Func<ColorHCV> generator)
		{
			int maxIterations = hcv.IsValid() ? 100 : 5; // If the input color already can't convert to RGB, then there's no guarantee that the generator will produce a convertible color, so be much more eager to give up in that case.
			int iterations = 0;

			ColorHCV hcvRandom;
			do
			{
				hcvRandom = generator();
				++iterations;
			}
			while (!hcvRandom.IsValid() && iterations < maxIterations);

			return hcvRandom.GetNearestValid();
		}

		private static ColorHCL Change(ColorHCL hcl, System.Func<ColorHCL> generator)
		{
			int maxIterations = hcl.IsValid() ? 100 : 5; // If the input color already can't convert to RGB, then there's no guarantee that the generator will produce a convertible color, so be much more eager to give up in that case.
			int iterations = 0;

			ColorHCL hclRandom;
			do
			{
				hclRandom = generator();
				++iterations;
			}
			while (!hclRandom.IsValid() && iterations < maxIterations);

			return hclRandom.GetNearestValid();
		}

		private static ColorHCY Change(ColorHCY hcy, System.Func<ColorHCY> generator)
		{
			int maxIterations = hcy.IsValid() ? 100 : 5; // If the input color already can't convert to RGB, then there's no guarantee that the generator will produce a convertible color, so be much more eager to give up in that case.
			int iterations = 0;

			ColorHCY hcyRandom;
			do
			{
				hcyRandom = generator();
				++iterations;
			}
			while (!hcyRandom.IsValid() && iterations < maxIterations);

			return hcyRandom.GetNearestValid();
		}

		#endregion
	}
}

#endif
