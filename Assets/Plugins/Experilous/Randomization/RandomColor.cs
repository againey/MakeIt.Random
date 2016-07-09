/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public static class RandomColor
	{
		#region Specific Colors

		public static Color Gray(IRandomEngine engine)
		{
			float value = RandomUnit.ClosedFloat(engine);
			return new Color(value, value, value);
		}

		public static Color DarkRed(IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), 0f, 0f);
		}

		public static Color DarkGreen(IRandomEngine engine)
		{
			return new Color(0f, RandomUnit.ClosedFloat(engine), 0f);
		}

		public static Color DarkBlue(IRandomEngine engine)
		{
			return new Color(0f, 0f, RandomUnit.ClosedFloat(engine));
		}

		public static Color LightRed(IRandomEngine engine)
		{
			float value = RandomUnit.ClosedFloat(engine);
			return new Color(1f, value, value);
		}

		public static Color LightGreen(IRandomEngine engine)
		{
			float value = RandomUnit.ClosedFloat(engine);
			return new Color(value, 1f, value);
		}

		public static Color LightBlue(IRandomEngine engine)
		{
			float value = RandomUnit.ClosedFloat(engine);
			return new Color(value, value, 1f);
		}

		public static Color AnyRed(IRandomEngine engine)
		{
			float value = RandomUnit.ClosedFloat(engine);
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

		public static Color AnyGreen(IRandomEngine engine)
		{
			float value = RandomUnit.ClosedFloat(engine);
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

		public static Color AnyBlue(IRandomEngine engine)
		{
			float value = RandomUnit.ClosedFloat(engine);
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

		public static Color Darken(Color rgb, IRandomEngine engine)
		{
			float t = RandomUnit.ClosedFloat(engine);
			return new Color(rgb.r * t, rgb.g * t, rgb.b * t, rgb.a);
		}

		public static Color Lighten(Color rgb, IRandomEngine engine)
		{
			float t = RandomUnit.ClosedFloat(engine);
			return new Color((1f - rgb.r) * t + rgb.r, (1f - rgb.g) * t + rgb.g, (1f - rgb.b) * t + rgb.b, rgb.a);
		}

		#endregion

		#region RGB

		public static Color RGB(IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), 1f);
		}

		public static Color RGB(float a, IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), a);
		}

		public static Color RGBA(IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine));
		}

		public static Color ChangeRed(Color rgb, IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), rgb.g, rgb.b, rgb.a);
		}

		public static Color ChangeRed(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxChange, engine), rgb.g, rgb.b, rgb.a);
		}

		public static Color ChangeGreen(Color rgb, IRandomEngine engine)
		{
			return new Color(rgb.r, RandomUnit.ClosedFloat(engine), rgb.b, rgb.a);
		}

		public static Color ChangeGreen(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(rgb.r, ChangeClamped(rgb.g, maxChange, engine), rgb.b, rgb.a);
		}

		public static Color ChangeBlue(Color rgb, IRandomEngine engine)
		{
			return new Color(rgb.r, rgb.g, RandomUnit.ClosedFloat(engine), rgb.a);
		}

		public static Color ChangeBlue(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(rgb.r, rgb.g, ChangeClamped(rgb.b, maxChange, engine), rgb.a);
		}

		public static Color ChangeAlpha(Color rgb, IRandomEngine engine)
		{
			return new Color(rgb.r, rgb.g, rgb.b, RandomUnit.ClosedFloat(engine));
		}

		public static Color ChangeAlpha(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(rgb.r, rgb.g, rgb.b, ChangeClamped(rgb.a, maxChange, engine));
		}

		public static Color ChangeRedGreen(Color rgb, IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), rgb.b, rgb.a);
		}

		public static Color ChangeRedGreen(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxChange, engine), ChangeClamped(rgb.g, maxChange, engine), rgb.b, rgb.a);
		}

		public static Color ChangeRedGreen(Color rgb, float maxRedChange, float maxGreenChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxRedChange, engine), ChangeClamped(rgb.g, maxGreenChange, engine), rgb.b, rgb.a);
		}

		public static Color ChangeRedBlue(Color rgb, IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), rgb.g, RandomUnit.ClosedFloat(engine), rgb.a);
		}

		public static Color ChangeRedBlue(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxChange, engine), rgb.g, ChangeClamped(rgb.b, maxChange, engine), rgb.a);
		}

		public static Color ChangeRedBlue(Color rgb, float maxRedChange, float maxBlueChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxRedChange, engine), rgb.g, ChangeClamped(rgb.b, maxBlueChange, engine), rgb.a);
		}

		public static Color ChangeGreenBlue(Color rgb, IRandomEngine engine)
		{
			return new Color(rgb.r, RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), rgb.a);
		}

		public static Color ChangeGreenBlue(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(rgb.r, ChangeClamped(rgb.g, maxChange, engine), ChangeClamped(rgb.b, maxChange, engine), rgb.a);
		}

		public static Color ChangeGreenBlue(Color rgb, float maxGreenChange, float maxBlueChange, IRandomEngine engine)
		{
			return new Color(rgb.r, ChangeClamped(rgb.g, maxGreenChange, engine), ChangeClamped(rgb.b, maxBlueChange, engine), rgb.a);
		}

		public static Color ChangeRedGreenBlue(Color rgb, IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), rgb.a);
		}

		public static Color ChangeRedGreenBlue(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxChange, engine), ChangeClamped(rgb.g, maxChange, engine), ChangeClamped(rgb.b, maxChange, engine), rgb.a);
		}

		public static Color ChangeRedGreenBlue(Color rgb, float maxRedChange, float maxGreenChange, float maxBlueChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxRedChange, engine), ChangeClamped(rgb.g, maxGreenChange, engine), ChangeClamped(rgb.b, maxBlueChange, engine), rgb.a);
		}

		public static Color ChangeAll(Color rgb, IRandomEngine engine)
		{
			return new Color(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine));
		}

		public static Color ChangeAll(Color rgb, float maxChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxChange, engine), ChangeClamped(rgb.g, maxChange, engine), ChangeClamped(rgb.b, maxChange, engine), ChangeClamped(rgb.a, maxChange, engine));
		}

		public static Color ChangeAll(Color rgb, float maxChange, float maxAlphaChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxChange, engine), ChangeClamped(rgb.g, maxChange, engine), ChangeClamped(rgb.b, maxChange, engine), ChangeClamped(rgb.a, maxAlphaChange, engine));
		}

		public static Color ChangeAll(Color rgb, float maxRedChange, float maxGreenChange, float maxBlueChange, float maxAlphaChange, IRandomEngine engine)
		{
			return new Color(ChangeClamped(rgb.r, maxRedChange, engine), ChangeClamped(rgb.g, maxGreenChange, engine), ChangeClamped(rgb.b, maxBlueChange, engine), ChangeClamped(rgb.a, maxAlphaChange, engine));
		}

		#endregion

		#region HSV

		public static ColorHSV HSV(IRandomEngine engine)
		{
			return HSV(1f, engine);
		}

		public static ColorHSV HSV(float a, IRandomEngine engine)
		{
			float hue = RandomUnit.HalfOpenFloat(engine);
			Vector2 chromaValue = RandomVector.PointWithinTriangle(new Vector2(1f, 1f), new Vector2(0f, 1f), engine);
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

		public static ColorHSV HSVA(IRandomEngine engine)
		{
			return HSV(RandomUnit.ClosedFloat(engine), engine);
		}

		public static ColorHSV UnbiasedHSV(IRandomEngine engine)
		{
			return UnbiasedHSV(1f, engine);
		}

		public static ColorHSV UnbiasedHSV(float a, IRandomEngine engine)
		{
			throw new System.NotImplementedException();
		}

		public static ColorHSV UnbiasedHSVA(IRandomEngine engine)
		{
			return UnbiasedHSV(RandomUnit.ClosedFloat(engine), engine);
		}

		public static ColorHSV ChangeHue(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(RandomUnit.ClosedFloat(engine), hsv.s, hsv.v, hsv.a);
		}

		public static ColorHSV ChangeHue(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange, engine), hsv.s, hsv.v, hsv.a);
		}

		public static ColorHSV ChangeSat(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, RandomUnit.ClosedFloat(engine), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeSat(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, ChangeClamped(hsv.s, maxChange, engine), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeValue(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, hsv.s, RandomUnit.ClosedFloat(engine), hsv.a);
		}

		public static ColorHSV ChangeValue(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, hsv.s, ChangeClamped(hsv.v, maxChange, engine), hsv.a);
		}

		public static ColorHSV ChangeAlpha(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, hsv.s, hsv.v, RandomUnit.ClosedFloat(engine));
		}

		public static ColorHSV ChangeAlpha(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, hsv.s, hsv.v, ChangeClamped(hsv.a, maxChange, engine));
		}

		public static ColorHSV ChangeHueSat(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeHueSat(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange, engine), ChangeClamped(hsv.s, maxChange, engine), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeHueSat(ColorHSV hsv, float maxHueChange, float maxSatChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxHueChange, engine), ChangeClamped(hsv.s, maxSatChange, engine), hsv.v, hsv.a);
		}

		public static ColorHSV ChangeHueValue(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(RandomUnit.ClosedFloat(engine), hsv.s, RandomUnit.ClosedFloat(engine), hsv.a);
		}

		public static ColorHSV ChangeHueValue(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange, engine), hsv.s, ChangeClamped(hsv.v, maxChange, engine), hsv.a);
		}

		public static ColorHSV ChangeHueValue(ColorHSV hsv, float maxHueChange, float maxValueChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxHueChange, engine), hsv.s, ChangeClamped(hsv.v, maxValueChange, engine), hsv.a);
		}

		public static ColorHSV ChangeSatValue(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), hsv.a);
		}

		public static ColorHSV ChangeSatValue(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, ChangeClamped(hsv.s, maxChange, engine), ChangeClamped(hsv.v, maxChange, engine), hsv.a);
		}

		public static ColorHSV ChangeSatValue(ColorHSV hsv, float maxSatChange, float maxValueChange, IRandomEngine engine)
		{
			return new ColorHSV(hsv.h, ChangeClamped(hsv.s, maxSatChange, engine), ChangeClamped(hsv.v, maxValueChange, engine), hsv.a);
		}

		public static ColorHSV ChangeHueSatValue(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), hsv.a);
		}

		public static ColorHSV ChangeHueSatValue(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange, engine), ChangeClamped(hsv.s, maxChange, engine), ChangeClamped(hsv.v, maxChange, engine), hsv.a);
		}

		public static ColorHSV ChangeHueSatValue(ColorHSV hsv, float maxHueChange, float maxSatChange, float maxValueChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxHueChange, engine), ChangeClamped(hsv.s, maxSatChange, engine), ChangeClamped(hsv.v, maxValueChange, engine), hsv.a);
		}

		public static ColorHSV ChangeAll(ColorHSV hsv, IRandomEngine engine)
		{
			return new ColorHSV(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine));
		}

		public static ColorHSV ChangeAll(ColorHSV hsv, float maxChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange, engine), ChangeClamped(hsv.s, maxChange, engine), ChangeClamped(hsv.v, maxChange, engine), ChangeClamped(hsv.a, maxChange, engine));
		}

		public static ColorHSV ChangeAll(ColorHSV hsv, float maxChange, float maxAlphaChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange, engine), ChangeClamped(hsv.s, maxChange, engine), ChangeClamped(hsv.v, maxChange, engine), ChangeClamped(hsv.a, maxAlphaChange, engine));
		}

		public static ColorHSV ChangeAll(ColorHSV hsv, float maxHueChange, float maxSatChange, float maxValueChange, float maxAlphaChange, IRandomEngine engine)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxHueChange, engine), ChangeClamped(hsv.s, maxSatChange, engine), ChangeClamped(hsv.v, maxValueChange, engine), ChangeClamped(hsv.a, maxAlphaChange, engine));
		}

		#endregion

		#region HSL

		public static ColorHSL HSL(IRandomEngine engine)
		{
			return HSL(1f, engine);
		}

		public static ColorHSL HSL(float a, IRandomEngine engine)
		{
			float hue = RandomUnit.HalfOpenFloat(engine);
			Vector2 chromaLightness = RandomVector.PointWithinTriangle(new Vector2(1f, 0.5f), new Vector2(0f, 1f), engine);
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

		public static ColorHSL HSLA(IRandomEngine engine)
		{
			return HSL(RandomUnit.ClosedFloat(engine), engine);
		}

		public static ColorHSL UnbiasedHSL(IRandomEngine engine)
		{
			return UnbiasedHSL(1f, engine);
		}

		public static ColorHSL UnbiasedHSL(float a, IRandomEngine engine)
		{
			throw new System.NotImplementedException();
		}

		public static ColorHSL UnbiasedHSLA(IRandomEngine engine)
		{
			return UnbiasedHSL(RandomUnit.ClosedFloat(engine), engine);
		}

		public static ColorHSL ChangeHue(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(RandomUnit.ClosedFloat(engine), hsl.s, hsl.l, hsl.a);
		}

		public static ColorHSL ChangeHue(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange, engine), hsl.s, hsl.l, hsl.a);
		}

		public static ColorHSL ChangeSat(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, RandomUnit.ClosedFloat(engine), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeSat(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, ChangeClamped(hsl.s, maxChange, engine), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeLight(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, hsl.s, RandomUnit.ClosedFloat(engine), hsl.a);
		}

		public static ColorHSL ChangeLight(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, hsl.s, ChangeClamped(hsl.l, maxChange, engine), hsl.a);
		}

		public static ColorHSL ChangeAlpha(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, hsl.s, hsl.l, RandomUnit.ClosedFloat(engine));
		}

		public static ColorHSL ChangeAlpha(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, hsl.s, hsl.l, ChangeClamped(hsl.a, maxChange, engine));
		}

		public static ColorHSL ChangeHueSat(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeHueSat(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange, engine), ChangeClamped(hsl.s, maxChange, engine), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeHueSat(ColorHSL hsl, float maxHueChange, float maxSatChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxHueChange, engine), ChangeClamped(hsl.s, maxSatChange, engine), hsl.l, hsl.a);
		}

		public static ColorHSL ChangeHueLight(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(RandomUnit.ClosedFloat(engine), hsl.s, RandomUnit.ClosedFloat(engine), hsl.a);
		}

		public static ColorHSL ChangeHueLight(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange, engine), hsl.s, ChangeClamped(hsl.l, maxChange, engine), hsl.a);
		}

		public static ColorHSL ChangeHueLight(ColorHSL hsl, float maxHueChange, float maxLightChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxHueChange, engine), hsl.s, ChangeClamped(hsl.l, maxLightChange, engine), hsl.a);
		}

		public static ColorHSL ChangeSatLight(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), hsl.a);
		}

		public static ColorHSL ChangeSatLight(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, ChangeClamped(hsl.s, maxChange, engine), ChangeClamped(hsl.l, maxChange, engine), hsl.a);
		}

		public static ColorHSL ChangeSatLight(ColorHSL hsl, float maxSatChange, float maxLightChange, IRandomEngine engine)
		{
			return new ColorHSL(hsl.h, ChangeClamped(hsl.s, maxSatChange, engine), ChangeClamped(hsl.l, maxLightChange, engine), hsl.a);
		}

		public static ColorHSL ChangeHueSatLight(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), hsl.a);
		}

		public static ColorHSL ChangeHueSatLight(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange, engine), ChangeClamped(hsl.s, maxChange, engine), ChangeClamped(hsl.l, maxChange, engine), hsl.a);
		}

		public static ColorHSL ChangeHueSatLight(ColorHSL hsl, float maxHueChange, float maxSatChange, float maxLightChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxHueChange, engine), ChangeClamped(hsl.s, maxSatChange, engine), ChangeClamped(hsl.l, maxLightChange, engine), hsl.a);
		}

		public static ColorHSL ChangeAll(ColorHSL hsl, IRandomEngine engine)
		{
			return new ColorHSL(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine));
		}

		public static ColorHSL ChangeAll(ColorHSL hsl, float maxChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange, engine), ChangeClamped(hsl.s, maxChange, engine), ChangeClamped(hsl.l, maxChange, engine), ChangeClamped(hsl.a, maxChange, engine));
		}

		public static ColorHSL ChangeAll(ColorHSL hsl, float maxChange, float maxAlphaChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange, engine), ChangeClamped(hsl.s, maxChange, engine), ChangeClamped(hsl.l, maxChange, engine), ChangeClamped(hsl.a, maxAlphaChange, engine));
		}

		public static ColorHSL ChangeAll(ColorHSL hsl, float maxHueChange, float maxSatChange, float maxLightChange, float maxAlphaChange, IRandomEngine engine)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxHueChange, engine), ChangeClamped(hsl.s, maxSatChange, engine), ChangeClamped(hsl.l, maxLightChange, engine), ChangeClamped(hsl.a, maxAlphaChange, engine));
		}

		#endregion

		#region HCY

		public static ColorHCY HCY(IRandomEngine engine)
		{
			return HCY(1f, engine);
		}

		public static ColorHCY HCY(float a, IRandomEngine engine)
		{
			float hue = RandomUnit.HalfOpenFloat(engine);
			Vector2 chromaLuma = RandomVector.PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hue)), new Vector2(0f, 1f), engine);
			return new ColorHCY(hue, chromaLuma.x, chromaLuma.y, a);
		}

		public static ColorHCY HCYA(IRandomEngine engine)
		{
			return HCY(RandomUnit.ClosedFloat(engine), engine);
		}

		public static ColorHCY ChangeHue(ColorHCY hcy, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(RandomUnit.ClosedFloat(engine), hcy.c, hcy.y, hcy.a));
		}

		public static ColorHCY ChangeHue(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxChange, engine), hcy.c, hcy.y, hcy.a));
		}

		public static ColorHCY ChangeChroma(ColorHCY hcy, IRandomEngine engine)
		{
			return new ColorHCY(hcy.h, RandomRange.Closed(ColorHCY.GetMaxChroma(hcy.h, hcy.y), engine), hcy.y, hcy.a);
		}

		public static ColorHCY ChangeChroma(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			return new ColorHCY(hcy.h, ChangeClamped(hcy.c, maxChange, 0f, ColorHCY.GetMaxChroma(hcy.h, hcy.y), engine), hcy.y, hcy.a);
		}

		public static ColorHCY ChangeLuma(ColorHCY hcy, IRandomEngine engine)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(hcy.h, hcy.c, out yMin, out yMax);
			return new ColorHCY(hcy.h, hcy.c, RandomRange.Closed(yMin, yMax, engine), hcy.a);
		}

		public static ColorHCY ChangeLuma(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(hcy.h, hcy.c, out yMin, out yMax);
			return new ColorHCY(hcy.h, hcy.c, ChangeClamped(hcy.y, maxChange, yMin, yMax, engine), hcy.a);
		}

		public static ColorHCY ChangeAlpha(ColorHCY hcy, IRandomEngine engine)
		{
			return new ColorHCY(hcy.h, hcy.c, hcy.y, RandomUnit.ClosedFloat(engine));
		}

		public static ColorHCY ChangeAlpha(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			return new ColorHCY(hcy.h, hcy.c, hcy.y, ChangeClamped(hcy.a, maxChange, engine));
		}

		public static ColorHCY ChangeHueChroma(ColorHCY hcy, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(RandomUnit.ClosedFloat(engine), RandomUnit.ClosedFloat(engine), hcy.y, hcy.a));
		}

		public static ColorHCY ChangeHueChroma(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxChange, engine), ChangeClamped(hcy.c, maxChange, engine), hcy.y, hcy.a));
		}

		public static ColorHCY ChangeHueChroma(ColorHCY hcy, float maxHueChange, float maxChromaChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxHueChange, engine), ChangeClamped(hcy.c, maxChromaChange, engine), hcy.y, hcy.a));
		}

		public static ColorHCY ChangeHueLuma(ColorHCY hcy, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(RandomUnit.ClosedFloat(engine), hcy.c, RandomUnit.ClosedFloat(engine), hcy.a));
		}

		public static ColorHCY ChangeHueLuma(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxChange, engine), hcy.c, ChangeClamped(hcy.y, maxChange, engine), hcy.a));
		}

		public static ColorHCY ChangeHueLuma(ColorHCY hcy, float maxHueChange, float maxLumaChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxHueChange, engine), hcy.c, ChangeClamped(hcy.y, maxLumaChange, engine), hcy.a));
		}

		public static ColorHCY ChangeChromaLuma(ColorHCY hcy, IRandomEngine engine)
		{
			Vector2 chromaLuma = RandomVector.PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hcy.h)), new Vector2(0f, 1f), engine);
			return new ColorHCY(hcy.h, chromaLuma.x, chromaLuma.y, hcy.a);
		}

		public static ColorHCY ChangeChromaLuma(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(hcy.h, ChangeClamped(hcy.c, maxChange, engine), ChangeClamped(hcy.y, maxChange, engine), hcy.a));
		}

		public static ColorHCY ChangeChromaLuma(ColorHCY hcy, float maxChromaChange, float maxLumaChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(hcy.h, ChangeClamped(hcy.c, maxChromaChange, engine), ChangeClamped(hcy.y, maxLumaChange, engine), hcy.a));
		}

		public static ColorHCY ChangeHueChromaLuma(ColorHCY hcy, IRandomEngine engine)
		{
			return HCY(hcy.a, engine);
		}

		public static ColorHCY ChangeHueChromaLuma(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxChange, engine), ChangeClamped(hcy.c, maxChange, engine), ChangeClamped(hcy.y, maxChange, engine), hcy.a));
		}

		public static ColorHCY ChangeHueChromaLuma(ColorHCY hcy, float maxHueChange, float maxChromaChange, float maxLumaChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxHueChange, engine), ChangeClamped(hcy.c, maxChromaChange, engine), ChangeClamped(hcy.y, maxLumaChange, engine), hcy.a));
		}

		public static ColorHCY ChangeAll(ColorHCY hcy, IRandomEngine engine)
		{
			return HCYA(engine);
		}

		public static ColorHCY ChangeAll(ColorHCY hcy, float maxChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxChange, engine), ChangeClamped(hcy.c, maxChange, engine), ChangeClamped(hcy.y, maxChange, engine), ChangeClamped(hcy.a, maxChange, engine)));
		}

		public static ColorHCY ChangeAll(ColorHCY hcy, float maxChange, float maxAlphaChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxChange, engine), ChangeClamped(hcy.c, maxChange, engine), ChangeClamped(hcy.y, maxChange, engine), ChangeClamped(hcy.a, maxAlphaChange, engine)));
		}

		public static ColorHCY ChangeAll(ColorHCY hcy, float maxHueChange, float maxChromaChange, float maxLumaChange, float maxAlphaChange, IRandomEngine engine)
		{
			return Change(hcy, () => new ColorHCY(ChangeRepeated(hcy.h, maxHueChange, engine), ChangeClamped(hcy.c, maxChromaChange, engine), ChangeClamped(hcy.y, maxLumaChange, engine), ChangeClamped(hcy.a, maxAlphaChange, engine)));
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

		private static float ChangeClamped(float original, float maxChange, IRandomEngine engine)
		{
			return RandomRange.Closed(Mathf.Max(0f, original - maxChange), Mathf.Min(original + maxChange, 1f), engine);
		}

		private static float ChangeClamped(float original, float maxChange, float min, float max, IRandomEngine engine)
		{
			return RandomRange.Closed(Mathf.Max(min, original - maxChange), Mathf.Min(original + maxChange, max), engine);
		}

		private static float ChangeRepeated(float original, float maxChange, IRandomEngine engine)
		{
			return Mathf.Repeat(original + RandomRange.Closed(-maxChange, +maxChange, engine), 1f);
		}

		#endregion
	}
}
