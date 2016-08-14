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
			float value = random.ClosedFloatUnit();
			return new Color(value, value, value);
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from completely black to red.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark red color.</returns>
		public static Color DarkRed(this IRandom random)
		{
			return new Color(random.ClosedFloatUnit(), 0f, 0f);
		}

		/// <summary>
		/// Generates a random green color, ranging all the way from completely black to green.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark green color.</returns>
		public static Color DarkGreen(this IRandom random)
		{
			return new Color(0f, random.ClosedFloatUnit(), 0f);
		}

		/// <summary>
		/// Generates a random blue color, ranging all the way from completely black to blue.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque dark blue color.</returns>
		public static Color DarkBlue(this IRandom random)
		{
			return new Color(0f, 0f, random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from red to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light red color.</returns>
		public static Color LightRed(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
			return new Color(1f, value, value);
		}

		/// <summary>
		/// Generates a random green color, ranging all the way from green to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light green color.</returns>
		public static Color LightGreen(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
			return new Color(value, 1f, value);
		}

		/// <summary>
		/// Generates a random blue color, ranging all the way from blue to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque light blue color.</returns>
		public static Color LightBlue(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
			return new Color(value, value, 1f);
		}

		/// <summary>
		/// Generates a random red color, ranging all the way from completely black, through red, to complete white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque red color.</returns>
		public static Color AnyRed(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
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
		public static Color AnyGreen(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
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
		public static Color AnyBlue(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
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

		/// <summary>
		/// Generates a darker color based on the color provided, ranging all the way from completely black up to but not including the original color.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color that is darker than the color provided.</returns>
		public static Color Darken(this IRandom random, Color rgb)
		{
			float t = random.HalfOpenFloatUnit();
			return new Color(rgb.r * t, rgb.g * t, rgb.b * t, rgb.a);
		}

		/// <summary>
		/// Generates a lighter color based on the color provided, ranging all the way from but not including the original color up to completely white.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color that is lighter than the color provided.</returns>
		public static Color Lighten(this IRandom random, Color rgb)
		{
			float t = random.HalfClosedFloatUnit();
			return new Color((1f - rgb.r) * t + rgb.r, (1f - rgb.g) * t + rgb.g, (1f - rgb.b) * t + rgb.b, rgb.a);
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
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), 1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the red/green/blue color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static Color RGB(this IRandom random, float a)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the red/green/blue color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static Color RGBA(this IRandom random)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for red while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red channel will be altered.</param>
		/// <returns>The original color with the red channel randomized.</returns>
		public static Color ChangeRed(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), rgb.g, rgb.b, rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for red while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the red channel randomized.</returns>
		public static Color ChangeRed(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), rgb.g, rgb.b, rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for green while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose green channel will be altered.</param>
		/// <returns>The original color with the green channel randomized.</returns>
		public static Color ChangeGreen(this IRandom random, Color rgb)
		{
			return new Color(rgb.r, random.ClosedFloatUnit(), rgb.b, rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for green while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose green channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the green channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the green channel randomized.</returns>
		public static Color ChangeGreen(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(rgb.r, random.ChangeClamped(rgb.g, maxChange), rgb.b, rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for blue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose blue channel will be altered.</param>
		/// <returns>The original color with the blue channel randomized.</returns>
		public static Color ChangeBlue(this IRandom random, Color rgb)
		{
			return new Color(rgb.r, rgb.g, random.ClosedFloatUnit(), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for blue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose blue channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the blue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the blue channel randomized.</returns>
		public static Color ChangeBlue(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(rgb.r, rgb.g, random.ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for opacity while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose opacity will be altered.</param>
		/// <returns>The original color with the opacity randomized.</returns>
		public static Color ChangeAlpha(this IRandom random, Color rgb)
		{
			return new Color(rgb.r, rgb.g, rgb.b, random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting a new value for opacity while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the opacity randomized.</returns>
		public static Color ChangeAlpha(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(rgb.r, rgb.g, rgb.b, random.ChangeClamped(rgb.a, maxChange));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red and green while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red and green channels will be altered.</param>
		/// <returns>The original color with the red and green channels randomized.</returns>
		public static Color ChangeRedGreen(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), rgb.b, rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red and green while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red and green channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the red and green channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the red and green channels randomized.</returns>
		public static Color ChangeRedGreen(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), random.ChangeClamped(rgb.g, maxChange), rgb.b, rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red and green while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red and green channels will be altered.</param>
		/// <param name="maxRedChange">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxGreenChange">The largest amount by which the green channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the red and green channels randomized.</returns>
		public static Color ChangeRedGreen(this IRandom random, Color rgb, float maxRedChange, float maxGreenChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxRedChange), random.ChangeClamped(rgb.g, maxGreenChange), rgb.b, rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red and blue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red and blue channels will be altered.</param>
		/// <returns>The original color with the red and blue channels randomized.</returns>
		public static Color ChangeRedBlue(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), rgb.g, random.ClosedFloatUnit(), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red and blue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red and blue channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the red and blue channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the red and blue channels randomized.</returns>
		public static Color ChangeRedBlue(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), rgb.g, random.ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red and blue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red and blue channels will be altered.</param>
		/// <param name="maxRedChange">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxBlueChange">The largest amount by which the blue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the red and blue channels randomized.</returns>
		public static Color ChangeRedBlue(this IRandom random, Color rgb, float maxRedChange, float maxBlueChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxRedChange), rgb.g, random.ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for green and blue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose green and blue channels will be altered.</param>
		/// <returns>The original color with the green and blue channels randomized.</returns>
		public static Color ChangeGreenBlue(this IRandom random, Color rgb)
		{
			return new Color(rgb.r, random.ClosedFloatUnit(), random.ClosedFloatUnit(), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for green and blue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose green and blue channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the green and blue channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the green and blue channels randomized.</returns>
		public static Color ChangeGreenBlue(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(rgb.r, random.ChangeClamped(rgb.g, maxChange), random.ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for green and blue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose green and blue channels will be altered.</param>
		/// <param name="maxGreenChange">The largest amount by which the green channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxBlueChange">The largest amount by which the blue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the green and blue channels randomized.</returns>
		public static Color ChangeGreenBlue(this IRandom random, Color rgb, float maxGreenChange, float maxBlueChange)
		{
			return new Color(rgb.r, random.ChangeClamped(rgb.g, maxGreenChange), random.ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red, green, and blue while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red, green, and blue channels will be altered.</param>
		/// <returns>The original color with the red, green, and blue channels randomized.</returns>
		public static Color ChangeRedGreenBlue(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red, green, and blue while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red, green, and blue channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the red, green, and blue channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the red, green, and blue channels randomized.</returns>
		public static Color ChangeRedGreenBlue(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), random.ChangeClamped(rgb.g, maxChange), random.ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for red, green, and blue while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose red, green, and blue channels will be altered.</param>
		/// <param name="maxRedChange">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxGreenChange">The largest amount by which the green channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxBlueChange">The largest amount by which the blue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the red, green, and blue channels randomized.</returns>
		public static Color ChangeRedGreenBlue(this IRandom random, Color rgb, float maxRedChange, float maxGreenChange, float maxBlueChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxRedChange), random.ChangeClamped(rgb.g, maxGreenChange), random.ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the color channels and opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static Color ChangeRedGreenBlueAlpha(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), random.ChangeClamped(rgb.g, maxChange), random.ChangeClamped(rgb.b, maxChange), random.ChangeClamped(rgb.a, maxChange));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the color channels can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAlphaChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static Color ChangeRedGreenBlueAlpha(this IRandom random, Color rgb, float maxChange, float maxAlphaChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), random.ChangeClamped(rgb.g, maxChange), random.ChangeClamped(rgb.b, maxChange), random.ChangeClamped(rgb.a, maxAlphaChange));
		}

		/// <summary>
		/// Generates a random color in the red/green/blue color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="rgb">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxRedChange">The largest amount by which the red channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxGreenChange">The largest amount by which the green channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxBlueChange">The largest amount by which the blue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAlphaChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static Color ChangeRedGreenBlueAlpha(this IRandom random, Color rgb, float maxRedChange, float maxGreenChange, float maxBlueChange, float maxAlphaChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxRedChange), random.ChangeClamped(rgb.g, maxGreenChange), random.ChangeClamped(rgb.b, maxBlueChange), random.ChangeClamped(rgb.a, maxAlphaChange));
		}

		#endregion

		#region HSV

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cylindrical hue/saturation/value color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHSV HSV(this IRandom random)
		{
			return random.HSV(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cylindrical hue/saturation/value color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHSV HSV(this IRandom random, float a)
		{
			float hue = random.HalfOpenFloatUnit();
			Vector2 chromaValue = random.PointWithinTriangle(new Vector2(1f, 1f), new Vector2(0f, 1f));
			if (chromaValue.y > 0f)
			{
				float saturation = chromaValue.x / chromaValue.y;
				return new ColorHSV(hue, saturation, chromaValue.y, a);
			}
			else
			{
				return new ColorHSV(hue, 0f, 0f, a);
			}
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cylindrical hue/saturation/value color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHSV HSVA(this IRandom random)
		{
			return random.HSV(random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the conic variant of the hue/saturation/value color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		/// <remarks>By sampling the conic variant of the hue/saturation/value color space, the bias toward darker colors is reduced.</remarks>
		public static ColorHSV UnbiasedHSV(this IRandom random)
		{
			return random.UnbiasedHSV(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the conic variant of the hue/saturation/value color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		/// <remarks>By sampling the conic variant of the hue/saturation/value color space, the bias toward darker colors is reduced.</remarks>
		public static ColorHSV UnbiasedHSV(this IRandom random, float a)
		{
			//TODO
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the conic variant of the hue/saturation/value color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		/// <remarks>By sampling the conic variant of the hue/saturation/value color space, the bias toward darker colors is reduced.</remarks>
		public static ColorHSV UnbiasedHSVA(this IRandom random)
		{
			return random.UnbiasedHSV(random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for hue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue channel will be altered.</param>
		/// <returns>The original color with the hue channel randomized.</returns>
		public static ColorHSV ChangeHue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), hsv.s, hsv.v, hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for hue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue channel randomized.</returns>
		public static ColorHSV ChangeHue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), hsv.s, hsv.v, hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation channel will be altered.</param>
		/// <returns>The original color with the saturation channel randomized.</returns>
		public static ColorHSV ChangeSat(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, random.ClosedFloatUnit(), hsv.v, hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation channel randomized.</returns>
		public static ColorHSV ChangeSat(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, random.ChangeClamped(hsv.s, maxChange), hsv.v, hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose value channel will be altered.</param>
		/// <returns>The original color with the value channel randomized.</returns>
		public static ColorHSV ChangeValue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, hsv.s, random.ClosedFloatUnit(), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose value channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the value channel randomized.</returns>
		public static ColorHSV ChangeValue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, hsv.s, random.ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for opacity while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose opacity will be altered.</param>
		/// <returns>The original color with the opacity randomized.</returns>
		public static ColorHSV ChangeAlpha(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, hsv.s, hsv.v, random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting a new value for opacity while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the opacity randomized.</returns>
		public static ColorHSV ChangeAlpha(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, hsv.s, hsv.v, random.ChangeClamped(hsv.a, maxChange));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHSV ChangeHueSat(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsv.v, hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue and saturation channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHSV ChangeHueSat(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), random.ChangeClamped(hsv.s, maxChange), hsv.v, hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHSV ChangeHueSat(this IRandom random, ColorHSV hsv, float maxHueChange, float maxSatChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxHueChange), random.ChangeClamped(hsv.s, maxSatChange), hsv.v, hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and value channels will be altered.</param>
		/// <returns>The original color with the hue and value channels randomized.</returns>
		public static ColorHSV ChangeHueValue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), hsv.s, random.ClosedFloatUnit(), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and value channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue and value channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and value channels randomized.</returns>
		public static ColorHSV ChangeHueValue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), hsv.s, random.ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and value channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxValueChange">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and value channels randomized.</returns>
		public static ColorHSV ChangeHueValue(this IRandom random, ColorHSV hsv, float maxHueChange, float maxValueChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxHueChange), hsv.s, random.ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and value channels will be altered.</param>
		/// <returns>The original color with the saturation and value channels randomized.</returns>
		public static ColorHSV ChangeSatValue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and value channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the saturation and value channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation and value channels randomized.</returns>
		public static ColorHSV ChangeSatValue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, random.ChangeClamped(hsv.s, maxChange), random.ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and value channels will be altered.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxValueChange">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation and value channels randomized.</returns>
		public static ColorHSV ChangeSatValue(this IRandom random, ColorHSV hsv, float maxSatChange, float maxValueChange)
		{
			return new ColorHSV(hsv.h, random.ChangeClamped(hsv.s, maxSatChange), random.ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and value channels will be altered.</param>
		/// <returns>The original color with the hue, saturation, and value channels randomized.</returns>
		public static ColorHSV ChangeHueSatValue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and value channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue, saturation, and value channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue, saturation, and value channels randomized.</returns>
		public static ColorHSV ChangeHueSatValue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), random.ChangeClamped(hsv.s, maxChange), random.ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and value channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxValueChange">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue, saturation, and value channels randomized.</returns>
		public static ColorHSV ChangeHueSatValue(this IRandom random, ColorHSV hsv, float maxHueChange, float maxSatChange, float maxValueChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxHueChange), random.ChangeClamped(hsv.s, maxSatChange), random.ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the color channels and opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHSV ChangeHueSatValueAlpha(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), random.ChangeClamped(hsv.s, maxChange), random.ChangeClamped(hsv.v, maxChange), random.ChangeClamped(hsv.a, maxChange));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the color channels can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAlphaChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHSV ChangeHueSatValueAlpha(this IRandom random, ColorHSV hsv, float maxChange, float maxAlphaChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), random.ChangeClamped(hsv.s, maxChange), random.ChangeClamped(hsv.v, maxChange), random.ChangeClamped(hsv.a, maxAlphaChange));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/value color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxValueChange">The largest amount by which the value channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAlphaChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHSV ChangeHueSatValueAlpha(this IRandom random, ColorHSV hsv, float maxHueChange, float maxSatChange, float maxValueChange, float maxAlphaChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxHueChange), random.ChangeClamped(hsv.s, maxSatChange), random.ChangeClamped(hsv.v, maxValueChange), random.ChangeClamped(hsv.a, maxAlphaChange));
		}

		#endregion

		#region HSL

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cylindrical hue/saturation/lightness color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHSL HSL(this IRandom random)
		{
			return random.HSL(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cylindrical hue/saturation/lightness color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHSL HSL(this IRandom random, float a)
		{
			float hue = random.HalfOpenFloatUnit();
			Vector2 chromaLightness = random.PointWithinTriangle(new Vector2(1f, 0.5f), new Vector2(0f, 1f));
			if (chromaLightness.y > 0f && chromaLightness.y < 1f)
			{
				float saturation = chromaLightness.x / (1f - Mathf.Abs(2f * chromaLightness.y - 1f));
				return new ColorHSL(hue, saturation, chromaLightness.y, a);
			}
			else
			{
				return new ColorHSL(hue, 0f, chromaLightness.y, a);
			}
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the cylindrical hue/saturation/lightness color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHSL HSLA(this IRandom random)
		{
			return random.HSL(random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the biconic variant of the hue/saturation/lightness color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		/// <remarks>By sampling the biconic variant of the hue/saturation/lightness color space, the bias toward ligher and darker colors is reduced.</remarks>
		public static ColorHSL UnbiasedHSL(this IRandom random)
		{
			return random.UnbiasedHSL(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the biconic variant of the hue/saturation/lightness color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		/// <remarks>By sampling the biconic variant of the hue/saturation/lightness color space, the bias toward ligher and darker colors is reduced.</remarks>
		public static ColorHSL UnbiasedHSL(this IRandom random, float a)
		{
			//TODO
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the biconic variant of the hue/saturation/lightness color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		/// <remarks>By sampling the biconic variant of the hue/saturation/lightness color space, the bias toward ligher and darker colors is reduced.</remarks>
		public static ColorHSL UnbiasedHSLA(this IRandom random)
		{
			return random.UnbiasedHSL(random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for hue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue channel will be altered.</param>
		/// <returns>The original color with the hue channel randomized.</returns>
		public static ColorHSL ChangeHue(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), hsl.s, hsl.l, hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for hue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue channel randomized.</returns>
		public static ColorHSL ChangeHue(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), hsl.s, hsl.l, hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation channel will be altered.</param>
		/// <returns>The original color with the saturation channel randomized.</returns>
		public static ColorHSL ChangeSat(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, random.ClosedFloatUnit(), hsl.l, hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation channel randomized.</returns>
		public static ColorHSL ChangeSat(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, random.ChangeClamped(hsl.s, maxChange), hsl.l, hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose lightness channel will be altered.</param>
		/// <returns>The original color with the lightness channel randomized.</returns>
		public static ColorHSL ChangeLight(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, hsl.s, random.ClosedFloatUnit(), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose lightness channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the lightness channel randomized.</returns>
		public static ColorHSL ChangeLight(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, hsl.s, random.ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for opacity while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose opacity will be altered.</param>
		/// <returns>The original color with the opacity randomized.</returns>
		public static ColorHSL ChangeAlpha(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, hsl.s, hsl.l, random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting a new value for opacity while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the opacity randomized.</returns>
		public static ColorHSL ChangeAlpha(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, hsl.s, hsl.l, random.ChangeClamped(hsl.a, maxChange));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHSL ChangeHueSat(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsl.l, hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue and saturation channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHSL ChangeHueSat(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), random.ChangeClamped(hsl.s, maxChange), hsl.l, hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHSL ChangeHueSat(this IRandom random, ColorHSL hsl, float maxHueChange, float maxSatChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxHueChange), random.ChangeClamped(hsl.s, maxSatChange), hsl.l, hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and lightness channels will be altered.</param>
		/// <returns>The original color with the hue and lightness channels randomized.</returns>
		public static ColorHSL ChangeHueLight(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), hsl.s, random.ClosedFloatUnit(), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and lightness channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue and lightness channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and lightness channels randomized.</returns>
		public static ColorHSL ChangeHueLight(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), hsl.s, random.ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and lightness channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxLightChange">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and lightness channels randomized.</returns>
		public static ColorHSL ChangeHueLight(this IRandom random, ColorHSL hsl, float maxHueChange, float maxLightChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxHueChange), hsl.s, random.ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and lightness channels will be altered.</param>
		/// <returns>The original color with the saturation and lightness channels randomized.</returns>
		public static ColorHSL ChangeSatLight(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and lightness channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the saturation and lightness channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation and lightness channels randomized.</returns>
		public static ColorHSL ChangeSatLight(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, random.ChangeClamped(hsl.s, maxChange), random.ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and lightness channels will be altered.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxLightChange">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation and lightness channels randomized.</returns>
		public static ColorHSL ChangeSatLight(this IRandom random, ColorHSL hsl, float maxSatChange, float maxLightChange)
		{
			return new ColorHSL(hsl.h, random.ChangeClamped(hsl.s, maxSatChange), random.ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and lightness channels will be altered.</param>
		/// <returns>The original color with the hue, saturation, and lightness channels randomized.</returns>
		public static ColorHSL ChangeHueSatLight(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and lightness channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue, saturation, and lightness channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue, saturation, and lightness channels randomized.</returns>
		public static ColorHSL ChangeHueSatLight(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), random.ChangeClamped(hsl.s, maxChange), random.ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and lightness channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxLightChange">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue, saturation, and lightness channels randomized.</returns>
		public static ColorHSL ChangeHueSatLight(this IRandom random, ColorHSL hsl, float maxHueChange, float maxSatChange, float maxLightChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxHueChange), random.ChangeClamped(hsl.s, maxSatChange), random.ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the color channels and opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHSL ChangeHueSatLightAlpha(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), random.ChangeClamped(hsl.s, maxChange), random.ChangeClamped(hsl.l, maxChange), random.ChangeClamped(hsl.a, maxChange));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the color channels can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAlphaChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHSL ChangeHueSatLightAlpha(this IRandom random, ColorHSL hsl, float maxChange, float maxAlphaChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), random.ChangeClamped(hsl.s, maxChange), random.ChangeClamped(hsl.l, maxChange), random.ChangeClamped(hsl.a, maxAlphaChange));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/lightness color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxLightChange">The largest amount by which the lightness channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAlphaChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHSL ChangeHueSatLightAlpha(this IRandom random, ColorHSL hsl, float maxHueChange, float maxSatChange, float maxLightChange, float maxAlphaChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxHueChange), random.ChangeClamped(hsl.s, maxSatChange), random.ChangeClamped(hsl.l, maxLightChange), random.ChangeClamped(hsl.a, maxAlphaChange));
		}

		#endregion

		#region HCY

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the skewed biconic hue/saturation/luma color space.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random fully opaque color.</returns>
		public static ColorHCY HCY(this IRandom random)
		{
			return random.HCY(1f);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the skewed biconic hue/saturation/luma color space, with a specified opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="a">The opacity value to give to the randomly generated color.</param>
		/// <returns>A random color with the opacity set to <paramref name="a"/>.</returns>
		public static ColorHCY HCY(this IRandom random, float a)
		{
			float hue = random.HalfOpenFloatUnit();
			Vector2 chromaLuma = random.PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hue)), new Vector2(0f, 1f));
			return new ColorHCY(hue, chromaLuma.x, chromaLuma.y, a);
		}

		/// <summary>
		/// Generates a random color selected from a uniform distribution of the skewed biconic hue/saturation/luma color space, with a random opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <returns>A random color with a random opacity.</returns>
		public static ColorHCY HCYA(this IRandom random)
		{
			return random.HCY(random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for hue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue channel will be altered.</param>
		/// <returns>The original color with the hue channel randomized.</returns>
		public static ColorHCY ChangeHue(this IRandom random, ColorHCY hcy)
		{
			return Change(hcy, () => new ColorHCY(random.ClosedFloatUnit(), hcy.c, hcy.y, hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for hue while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue channel randomized.</returns>
		public static ColorHCY ChangeHue(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), hcy.c, hcy.y, hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation channel will be altered.</param>
		/// <returns>The original color with the saturation channel randomized.</returns>
		public static ColorHCY ChangeChroma(this IRandom random, ColorHCY hcy)
		{
			return new ColorHCY(hcy.h, random.ClosedRange(ColorHCY.GetMaxChroma(hcy.h, hcy.y)), hcy.y, hcy.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation channel randomized.</returns>
		public static ColorHCY ChangeChroma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return new ColorHCY(hcy.h, random.ChangeClamped(hcy.c, maxChange, 0f, ColorHCY.GetMaxChroma(hcy.h, hcy.y)), hcy.y, hcy.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose luma channel will be altered.</param>
		/// <returns>The original color with the luma channel randomized.</returns>
		public static ColorHCY ChangeLuma(this IRandom random, ColorHCY hcy)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(hcy.h, hcy.c, out yMin, out yMax);
			return new ColorHCY(hcy.h, hcy.c, random.ClosedRange(yMin, yMax), hcy.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose luma channel will be altered.</param>
		/// <param name="maxChange">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the luma channel randomized.</returns>
		public static ColorHCY ChangeLuma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(hcy.h, hcy.c, out yMin, out yMax);
			return new ColorHCY(hcy.h, hcy.c, random.ChangeClamped(hcy.y, maxChange, yMin, yMax), hcy.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for opacity while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose opacity will be altered.</param>
		/// <returns>The original color with the opacity randomized.</returns>
		public static ColorHCY ChangeAlpha(this IRandom random, ColorHCY hcy)
		{
			return new ColorHCY(hcy.h, hcy.c, hcy.y, random.ClosedFloatUnit());
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting a new value for opacity while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the opacity randomized.</returns>
		public static ColorHCY ChangeAlpha(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return new ColorHCY(hcy.h, hcy.c, hcy.y, random.ChangeClamped(hcy.a, maxChange));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHCY ChangeHueChroma(this IRandom random, ColorHCY hcy)
		{
			return Change(hcy, () => new ColorHCY(random.ClosedFloatUnit(), random.ClosedFloatUnit(), hcy.y, hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue and saturation channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHCY ChangeHueChroma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), random.ChangeClamped(hcy.c, maxChange), hcy.y, hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue and saturation while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and saturation channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and saturation channels randomized.</returns>
		public static ColorHCY ChangeHueChroma(this IRandom random, ColorHCY hcy, float maxHueChange, float maxChromaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxHueChange), random.ChangeClamped(hcy.c, maxChromaChange), hcy.y, hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and luma channels will be altered.</param>
		/// <returns>The original color with the hue and luma channels randomized.</returns>
		public static ColorHCY ChangeHueLuma(this IRandom random, ColorHCY hcy)
		{
			return Change(hcy, () => new ColorHCY(random.ClosedFloatUnit(), hcy.c, random.ClosedFloatUnit(), hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and luma channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue and luma channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and luma channels randomized.</returns>
		public static ColorHCY ChangeHueLuma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), hcy.c, random.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue and luma channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxLumaChange">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue and luma channels randomized.</returns>
		public static ColorHCY ChangeHueLuma(this IRandom random, ColorHCY hcy, float maxHueChange, float maxLumaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxHueChange), hcy.c, random.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and luma channels will be altered.</param>
		/// <returns>The original color with the saturation and luma channels randomized.</returns>
		public static ColorHCY ChangeChromaLuma(this IRandom random, ColorHCY hcy)
		{
			Vector2 chromaLuma = random.PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hcy.h)), new Vector2(0f, 1f));
			return new ColorHCY(hcy.h, chromaLuma.x, chromaLuma.y, hcy.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and luma channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the saturation and luma channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation and luma channels randomized.</returns>
		public static ColorHCY ChangeChromaLuma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(hcy.h, random.ChangeClamped(hcy.c, maxChange), random.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for saturation and value while keeping all other values the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose saturation and luma channels will be altered.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxLumaChange">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the saturation and luma channels randomized.</returns>
		public static ColorHCY ChangeChromaLuma(this IRandom random, ColorHCY hcy, float maxChromaChange, float maxLumaChange)
		{
			return Change(hcy, () => new ColorHCY(hcy.h, random.ChangeClamped(hcy.c, maxChromaChange), random.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and luma channels will be altered.</param>
		/// <returns>The original color with the hue, saturation, and luma channels randomized.</returns>
		public static ColorHCY ChangeHueChromaLuma(this IRandom random, ColorHCY hcy)
		{
			return random.HCY(hcy.a);
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and luma channels will be altered.</param>
		/// <param name="maxChange">The largest amount by which the hue, saturation, and luma channels can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue, saturation, and luma channels randomized.</returns>
		public static ColorHCY ChangeHueChromaLuma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), random.ChangeClamped(hcy.c, maxChange), random.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for hue, saturation, and value while keeping the opacity the same.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose hue, saturation, and luma channels will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxLumaChange">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the hue, saturation, and luma channels randomized.</returns>
		public static ColorHCY ChangeHueChromaLuma(this IRandom random, ColorHCY hcy, float maxHueChange, float maxChromaChange, float maxLumaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxHueChange), random.ChangeClamped(hcy.c, maxChromaChange), random.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the color channels and opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHCY ChangeHueChromaLumaAlpha(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), random.ChangeClamped(hcy.c, maxChange), random.ChangeClamped(hcy.y, maxChange), random.ChangeClamped(hcy.a, maxChange)));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxChange">The largest amount by which the color channels can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAlphaChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHCY ChangeHueChromaLumaAlpha(this IRandom random, ColorHCY hcy, float maxChange, float maxAlphaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), random.ChangeClamped(hcy.c, maxChange), random.ChangeClamped(hcy.y, maxChange), random.ChangeClamped(hcy.a, maxAlphaChange)));
		}

		/// <summary>
		/// Generates a random color in the hue/saturation/luma color space by randomly selecting new values for all channels, including opacity.
		/// </summary>
		/// <param name="random">The pseudo-random engine that will be used to generate bits from which the return value is derived.</param>
		/// <param name="hsv">The original color whose color channels and opacity will be altered.</param>
		/// <param name="maxHueChange">The largest amount by which the hue channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxSatChange">The largest amount by which the saturation channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxLumaChange">The largest amount by which the luma channel can change, up or down.  Must be non-negative.</param>
		/// <param name="maxAlphaChange">The largest amount by which the opacity can change, up or down.  Must be non-negative.</param>
		/// <returns>The original color with the color channels and opacity randomized.</returns>
		public static ColorHCY ChangeHueChromaLumaAlpha(this IRandom random, ColorHCY hcy, float maxHueChange, float maxChromaChange, float maxLumaChange, float maxAlphaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxHueChange), random.ChangeClamped(hcy.c, maxChromaChange), random.ChangeClamped(hcy.y, maxLumaChange), random.ChangeClamped(hcy.a, maxAlphaChange)));
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

		#region Private Helper Functions

		private static float ChangeClamped(this IRandom random, float original, float maxChange)
		{
			return random.ClosedRange(Mathf.Max(0f, original - maxChange), Mathf.Min(original + maxChange, 1f));
		}

		private static float ChangeClamped(this IRandom random, float original, float maxChange, float min, float max)
		{
			return random.ClosedRange(Mathf.Max(min, original - maxChange), Mathf.Min(original + maxChange, max));
		}

		private static float ChangeRepeated(this IRandom random, float original, float maxChange)
		{
			return Mathf.Repeat(original + random.ClosedRange(-maxChange, +maxChange), 1f);
		}

		#endregion
	}
}
