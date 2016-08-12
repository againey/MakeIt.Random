/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using Experilous.Colors;

namespace Experilous.MakeIt.Random
{
	public static class RandomColor
	{
		#region Specific Colors

		public static Color Gray(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
			return new Color(value, value, value);
		}

		public static Color DarkRed(this IRandom random)
		{
			return new Color(random.ClosedFloatUnit(), 0f, 0f);
		}

		public static Color DarkGreen(this IRandom random)
		{
			return new Color(0f, random.ClosedFloatUnit(), 0f);
		}

		public static Color DarkBlue(this IRandom random)
		{
			return new Color(0f, 0f, random.ClosedFloatUnit());
		}

		public static Color LightRed(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
			return new Color(1f, value, value);
		}

		public static Color LightGreen(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
			return new Color(value, 1f, value);
		}

		public static Color LightBlue(this IRandom random)
		{
			float value = random.ClosedFloatUnit();
			return new Color(value, value, 1f);
		}

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

		public static Color Darken(this IRandom random, Color rgb)
		{
			float t = random.ClosedFloatUnit();
			return new Color(rgb.r * t, rgb.g * t, rgb.b * t, rgb.a);
		}

		public static Color Lighten(this IRandom random, Color rgb)
		{
			float t = random.ClosedFloatUnit();
			return new Color((1f - rgb.r) * t + rgb.r, (1f - rgb.g) * t + rgb.g, (1f - rgb.b) * t + rgb.b, rgb.a);
		}

		#endregion

		#region RGB

		public static Color RGB(this IRandom random)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), 1f);
		}

		public static Color RGB(this IRandom random, float a)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), a);
		}

		public static Color RGBA(this IRandom random)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit());
		}

		public static Color ChangeRed(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), rgb.g, rgb.b, rgb.a);
		}

		public static Color ChangeRed(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), rgb.g, rgb.b, rgb.a);
		}

		public static Color ChangeGreen(this IRandom random, Color rgb)
		{
			return new Color(rgb.r, random.ClosedFloatUnit(), rgb.b, rgb.a);
		}

		public static Color ChangeGreen(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(rgb.r, random.ChangeClamped(rgb.g, maxChange), rgb.b, rgb.a);
		}

		public static Color ChangeBlue(this IRandom random, Color rgb)
		{
			return new Color(rgb.r, rgb.g, random.ClosedFloatUnit(), rgb.a);
		}

		public static Color ChangeBlue(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(rgb.r, rgb.g, random.ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		public static Color ChangeAlpha(this IRandom random, Color rgb)
		{
			return new Color(rgb.r, rgb.g, rgb.b, random.ClosedFloatUnit());
		}

		public static Color ChangeAlpha(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(rgb.r, rgb.g, rgb.b, random.ChangeClamped(rgb.a, maxChange));
		}

		public static Color ChangeRedGreen(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), rgb.b, rgb.a);
		}

		public static Color ChangeRedGreen(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), random.ChangeClamped(rgb.g, maxChange), rgb.b, rgb.a);
		}

		public static Color ChangeRedGreen(this IRandom random, Color rgb, float maxRedChange, float maxGreenChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxRedChange), random.ChangeClamped(rgb.g, maxGreenChange), rgb.b, rgb.a);
		}

		public static Color ChangeRedBlue(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), rgb.g, random.ClosedFloatUnit(), rgb.a);
		}

		public static Color ChangeRedBlue(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), rgb.g, random.ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		public static Color ChangeRedBlue(this IRandom random, Color rgb, float maxRedChange, float maxBlueChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxRedChange), rgb.g, random.ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		public static Color ChangeGreenBlue(this IRandom random, Color rgb)
		{
			return new Color(rgb.r, random.ClosedFloatUnit(), random.ClosedFloatUnit(), rgb.a);
		}

		public static Color ChangeGreenBlue(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(rgb.r, random.ChangeClamped(rgb.g, maxChange), random.ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		public static Color ChangeGreenBlue(this IRandom random, Color rgb, float maxGreenChange, float maxBlueChange)
		{
			return new Color(rgb.r, random.ChangeClamped(rgb.g, maxGreenChange), random.ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		public static Color ChangeRedGreenBlue(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), rgb.a);
		}

		public static Color ChangeRedGreenBlue(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), random.ChangeClamped(rgb.g, maxChange), random.ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		public static Color ChangeRedGreenBlue(this IRandom random, Color rgb, float maxRedChange, float maxGreenChange, float maxBlueChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxRedChange), random.ChangeClamped(rgb.g, maxGreenChange), random.ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		public static Color ChangeRedGreenBlueAlpha(this IRandom random, Color rgb)
		{
			return new Color(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit());
		}

		public static Color ChangeRedGreenBlueAlpha(this IRandom random, Color rgb, float maxChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), random.ChangeClamped(rgb.g, maxChange), random.ChangeClamped(rgb.b, maxChange), random.ChangeClamped(rgb.a, maxChange));
		}

		public static Color ChangeRedGreenBlueAlpha(this IRandom random, Color rgb, float maxChange, float maxAlphaChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxChange), random.ChangeClamped(rgb.g, maxChange), random.ChangeClamped(rgb.b, maxChange), random.ChangeClamped(rgb.a, maxAlphaChange));
		}

		public static Color ChangeRedGreenBlueAlpha(this IRandom random, Color rgb, float maxRedChange, float maxGreenChange, float maxBlueChange, float maxAlphaChange)
		{
			return new Color(random.ChangeClamped(rgb.r, maxRedChange), random.ChangeClamped(rgb.g, maxGreenChange), random.ChangeClamped(rgb.b, maxBlueChange), random.ChangeClamped(rgb.a, maxAlphaChange));
		}

		#endregion

		#region HSV

		public static ColorHSV HSV(this IRandom random)
		{
			return random.HSV(1f);
		}

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

		public static ColorHSV HSVA(this IRandom random)
		{
			return random.HSV(random.ClosedFloatUnit());
		}

		public static ColorHSV UnbiasedHSV(this IRandom random)
		{
			return random.UnbiasedHSV(1f);
		}

		public static ColorHSV UnbiasedHSV(this IRandom random, float a)
		{
			throw new System.NotImplementedException();
		}

		public static ColorHSV UnbiasedHSVA(this IRandom random)
		{
			return random.UnbiasedHSV(random.ClosedFloatUnit());
		}

		public static ColorHSV ChangeHue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), hsv.s, hsv.v, hsv.a);
		}

		public static ColorHSV ChangeHue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), hsv.s, hsv.v, hsv.a);
		}

		public static ColorHSV ChangeSat(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, random.ClosedFloatUnit(), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeSat(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, random.ChangeClamped(hsv.s, maxChange), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeValue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, hsv.s, random.ClosedFloatUnit(), hsv.a);
		}

		public static ColorHSV ChangeValue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, hsv.s, random.ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		public static ColorHSV ChangeAlpha(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, hsv.s, hsv.v, random.ClosedFloatUnit());
		}

		public static ColorHSV ChangeAlpha(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, hsv.s, hsv.v, random.ChangeClamped(hsv.a, maxChange));
		}

		public static ColorHSV ChangeHueSat(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeHueSat(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), random.ChangeClamped(hsv.s, maxChange), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeHueSat(this IRandom random, ColorHSV hsv, float maxHueChange, float maxSatChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxHueChange), random.ChangeClamped(hsv.s, maxSatChange), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeHueValue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), hsv.s, random.ClosedFloatUnit(), hsv.a);
		}

		public static ColorHSV ChangeHueValue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), hsv.s, random.ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		public static ColorHSV ChangeHueValue(this IRandom random, ColorHSV hsv, float maxHueChange, float maxValueChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxHueChange), hsv.s, random.ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		public static ColorHSV ChangeSatValue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsv.a);
		}

		public static ColorHSV ChangeSatValue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, random.ChangeClamped(hsv.s, maxChange), random.ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		public static ColorHSV ChangeSatValue(this IRandom random, ColorHSV hsv, float maxSatChange, float maxValueChange)
		{
			return new ColorHSV(hsv.h, random.ChangeClamped(hsv.s, maxSatChange), random.ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		public static ColorHSV ChangeHueSatValue(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsv.a);
		}

		public static ColorHSV ChangeHueSatValue(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), random.ChangeClamped(hsv.s, maxChange), random.ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		public static ColorHSV ChangeHueSatValue(this IRandom random, ColorHSV hsv, float maxHueChange, float maxSatChange, float maxValueChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxHueChange), random.ChangeClamped(hsv.s, maxSatChange), random.ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		public static ColorHSV ChangeHueSatValueAlpha(this IRandom random, ColorHSV hsv)
		{
			return new ColorHSV(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit());
		}

		public static ColorHSV ChangeHueSatValueAlpha(this IRandom random, ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), random.ChangeClamped(hsv.s, maxChange), random.ChangeClamped(hsv.v, maxChange), random.ChangeClamped(hsv.a, maxChange));
		}

		public static ColorHSV ChangeHueSatValueAlpha(this IRandom random, ColorHSV hsv, float maxChange, float maxAlphaChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxChange), random.ChangeClamped(hsv.s, maxChange), random.ChangeClamped(hsv.v, maxChange), random.ChangeClamped(hsv.a, maxAlphaChange));
		}

		public static ColorHSV ChangeHueSatValueAlpha(this IRandom random, ColorHSV hsv, float maxHueChange, float maxSatChange, float maxValueChange, float maxAlphaChange)
		{
			return new ColorHSV(random.ChangeRepeated(hsv.h, maxHueChange), random.ChangeClamped(hsv.s, maxSatChange), random.ChangeClamped(hsv.v, maxValueChange), random.ChangeClamped(hsv.a, maxAlphaChange));
		}

		#endregion

		#region HSL

		public static ColorHSL HSL(this IRandom random)
		{
			return random.HSL(1f);
		}

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

		public static ColorHSL HSLA(this IRandom random)
		{
			return random.HSL(random.ClosedFloatUnit());
		}

		public static ColorHSL UnbiasedHSL(this IRandom random)
		{
			return random.UnbiasedHSL(1f);
		}

		public static ColorHSL UnbiasedHSL(this IRandom random, float a)
		{
			throw new System.NotImplementedException();
		}

		public static ColorHSL UnbiasedHSLA(this IRandom random)
		{
			return random.UnbiasedHSL(random.ClosedFloatUnit());
		}

		public static ColorHSL ChangeHue(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), hsl.s, hsl.l, hsl.a);
		}

		public static ColorHSL ChangeHue(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), hsl.s, hsl.l, hsl.a);
		}

		public static ColorHSL ChangeSat(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, random.ClosedFloatUnit(), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeSat(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, random.ChangeClamped(hsl.s, maxChange), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeLight(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, hsl.s, random.ClosedFloatUnit(), hsl.a);
		}

		public static ColorHSL ChangeLight(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, hsl.s, random.ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		public static ColorHSL ChangeAlpha(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, hsl.s, hsl.l, random.ClosedFloatUnit());
		}

		public static ColorHSL ChangeAlpha(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, hsl.s, hsl.l, random.ChangeClamped(hsl.a, maxChange));
		}

		public static ColorHSL ChangeHueSat(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeHueSat(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), random.ChangeClamped(hsl.s, maxChange), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeHueSat(this IRandom random, ColorHSL hsl, float maxHueChange, float maxSatChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxHueChange), random.ChangeClamped(hsl.s, maxSatChange), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeHueLight(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), hsl.s, random.ClosedFloatUnit(), hsl.a);
		}

		public static ColorHSL ChangeHueLight(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), hsl.s, random.ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		public static ColorHSL ChangeHueLight(this IRandom random, ColorHSL hsl, float maxHueChange, float maxLightChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxHueChange), hsl.s, random.ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		public static ColorHSL ChangeSatLight(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsl.a);
		}

		public static ColorHSL ChangeSatLight(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, random.ChangeClamped(hsl.s, maxChange), random.ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		public static ColorHSL ChangeSatLight(this IRandom random, ColorHSL hsl, float maxSatChange, float maxLightChange)
		{
			return new ColorHSL(hsl.h, random.ChangeClamped(hsl.s, maxSatChange), random.ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		public static ColorHSL ChangeHueSatLight(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), hsl.a);
		}

		public static ColorHSL ChangeHueSatLight(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), random.ChangeClamped(hsl.s, maxChange), random.ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		public static ColorHSL ChangeHueSatLight(this IRandom random, ColorHSL hsl, float maxHueChange, float maxSatChange, float maxLightChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxHueChange), random.ChangeClamped(hsl.s, maxSatChange), random.ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		public static ColorHSL ChangeHueSatLightAlpha(this IRandom random, ColorHSL hsl)
		{
			return new ColorHSL(random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit(), random.ClosedFloatUnit());
		}

		public static ColorHSL ChangeHueSatLightAlpha(this IRandom random, ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), random.ChangeClamped(hsl.s, maxChange), random.ChangeClamped(hsl.l, maxChange), random.ChangeClamped(hsl.a, maxChange));
		}

		public static ColorHSL ChangeHueSatLightAlpha(this IRandom random, ColorHSL hsl, float maxChange, float maxAlphaChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxChange), random.ChangeClamped(hsl.s, maxChange), random.ChangeClamped(hsl.l, maxChange), random.ChangeClamped(hsl.a, maxAlphaChange));
		}

		public static ColorHSL ChangeHueSatLightAlpha(this IRandom random, ColorHSL hsl, float maxHueChange, float maxSatChange, float maxLightChange, float maxAlphaChange)
		{
			return new ColorHSL(random.ChangeRepeated(hsl.h, maxHueChange), random.ChangeClamped(hsl.s, maxSatChange), random.ChangeClamped(hsl.l, maxLightChange), random.ChangeClamped(hsl.a, maxAlphaChange));
		}

		#endregion

		#region HCY

		public static ColorHCY HCY(this IRandom random)
		{
			return random.HCY(1f);
		}

		public static ColorHCY HCY(this IRandom random, float a)
		{
			float hue = random.HalfOpenFloatUnit();
			Vector2 chromaLuma = random.PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hue)), new Vector2(0f, 1f));
			return new ColorHCY(hue, chromaLuma.x, chromaLuma.y, a);
		}

		public static ColorHCY HCYA(this IRandom random)
		{
			return random.HCY(random.ClosedFloatUnit());
		}

		public static ColorHCY ChangeHue(this IRandom random, ColorHCY hcy)
		{
			return Change(hcy, () => new ColorHCY(random.ClosedFloatUnit(), hcy.c, hcy.y, hcy.a));
		}

		public static ColorHCY ChangeHue(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), hcy.c, hcy.y, hcy.a));
		}

		public static ColorHCY ChangeChroma(this IRandom random, ColorHCY hcy)
		{
			return new ColorHCY(hcy.h, random.ClosedRange(ColorHCY.GetMaxChroma(hcy.h, hcy.y)), hcy.y, hcy.a);
		}

		public static ColorHCY ChangeChroma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return new ColorHCY(hcy.h, random.ChangeClamped(hcy.c, maxChange, 0f, ColorHCY.GetMaxChroma(hcy.h, hcy.y)), hcy.y, hcy.a);
		}

		public static ColorHCY ChangeLuma(this IRandom random, ColorHCY hcy)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(hcy.h, hcy.c, out yMin, out yMax);
			return new ColorHCY(hcy.h, hcy.c, random.ClosedRange(yMin, yMax), hcy.a);
		}

		public static ColorHCY ChangeLuma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(hcy.h, hcy.c, out yMin, out yMax);
			return new ColorHCY(hcy.h, hcy.c, random.ChangeClamped(hcy.y, maxChange, yMin, yMax), hcy.a);
		}

		public static ColorHCY ChangeAlpha(this IRandom random, ColorHCY hcy)
		{
			return new ColorHCY(hcy.h, hcy.c, hcy.y, random.ClosedFloatUnit());
		}

		public static ColorHCY ChangeAlpha(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return new ColorHCY(hcy.h, hcy.c, hcy.y, random.ChangeClamped(hcy.a, maxChange));
		}

		public static ColorHCY ChangeHueChroma(this IRandom random, ColorHCY hcy)
		{
			return Change(hcy, () => new ColorHCY(random.ClosedFloatUnit(), random.ClosedFloatUnit(), hcy.y, hcy.a));
		}

		public static ColorHCY ChangeHueChroma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), random.ChangeClamped(hcy.c, maxChange), hcy.y, hcy.a));
		}

		public static ColorHCY ChangeHueChroma(this IRandom random, ColorHCY hcy, float maxHueChange, float maxChromaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxHueChange), random.ChangeClamped(hcy.c, maxChromaChange), hcy.y, hcy.a));
		}

		public static ColorHCY ChangeHueLuma(this IRandom random, ColorHCY hcy)
		{
			return Change(hcy, () => new ColorHCY(random.ClosedFloatUnit(), hcy.c, random.ClosedFloatUnit(), hcy.a));
		}

		public static ColorHCY ChangeHueLuma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), hcy.c, random.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		public static ColorHCY ChangeHueLuma(this IRandom random, ColorHCY hcy, float maxHueChange, float maxLumaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxHueChange), hcy.c, random.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		public static ColorHCY ChangeChromaLuma(this IRandom random, ColorHCY hcy)
		{
			Vector2 chromaLuma = random.PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hcy.h)), new Vector2(0f, 1f));
			return new ColorHCY(hcy.h, chromaLuma.x, chromaLuma.y, hcy.a);
		}

		public static ColorHCY ChangeChromaLuma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(hcy.h, random.ChangeClamped(hcy.c, maxChange), random.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		public static ColorHCY ChangeChromaLuma(this IRandom random, ColorHCY hcy, float maxChromaChange, float maxLumaChange)
		{
			return Change(hcy, () => new ColorHCY(hcy.h, random.ChangeClamped(hcy.c, maxChromaChange), random.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		public static ColorHCY ChangeHueChromaLuma(this IRandom random, ColorHCY hcy)
		{
			return random.HCY(hcy.a);
		}

		public static ColorHCY ChangeHueChromaLuma(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), random.ChangeClamped(hcy.c, maxChange), random.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		public static ColorHCY ChangeHueChromaLuma(this IRandom random, ColorHCY hcy, float maxHueChange, float maxChromaChange, float maxLumaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxHueChange), random.ChangeClamped(hcy.c, maxChromaChange), random.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		public static ColorHCY ChangeHueChromaLumaAlpha(this IRandom random, ColorHCY hcy)
		{
			return HCYA(random);
		}

		public static ColorHCY ChangeHueChromaLumaAlpha(this IRandom random, ColorHCY hcy, float maxChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), random.ChangeClamped(hcy.c, maxChange), random.ChangeClamped(hcy.y, maxChange), random.ChangeClamped(hcy.a, maxChange)));
		}

		public static ColorHCY ChangeHueChromaLumaAlpha(this IRandom random, ColorHCY hcy, float maxChange, float maxAlphaChange)
		{
			return Change(hcy, () => new ColorHCY(random.ChangeRepeated(hcy.h, maxChange), random.ChangeClamped(hcy.c, maxChange), random.ChangeClamped(hcy.y, maxChange), random.ChangeClamped(hcy.a, maxAlphaChange)));
		}

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
