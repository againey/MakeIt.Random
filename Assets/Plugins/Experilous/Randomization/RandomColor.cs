/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;

namespace Experilous.Randomization
{
	public struct RandomColor
	{
		private IRandomEngine _random;

		public RandomColor(IRandomEngine random)
		{
			_random = random;
		}

		#region Specific Colors

		public Color Gray()
		{
			float value = _random.Unit().ClosedFloat();
			return new Color(value, value, value);
		}

		public Color DarkRed()
		{
			return new Color(_random.Unit().ClosedFloat(), 0f, 0f);
		}

		public Color DarkGreen()
		{
			return new Color(0f, _random.Unit().ClosedFloat(), 0f);
		}

		public Color DarkBlue()
		{
			return new Color(0f, 0f, _random.Unit().ClosedFloat());
		}

		public Color LightRed()
		{
			float value = _random.Unit().ClosedFloat();
			return new Color(1f, value, value);
		}

		public Color LightGreen()
		{
			float value = _random.Unit().ClosedFloat();
			return new Color(value, 1f, value);
		}

		public Color LightBlue()
		{
			float value = _random.Unit().ClosedFloat();
			return new Color(value, value, 1f);
		}

		public Color AnyRed()
		{
			float value = _random.Unit().ClosedFloat();
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

		public Color AnyGreen()
		{
			float value = _random.Unit().ClosedFloat();
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

		public Color AnyBlue()
		{
			float value = _random.Unit().ClosedFloat();
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

		public Color Darken(Color rgb)
		{
			float t = _random.Unit().ClosedFloat();
			return new Color(rgb.r * t, rgb.g * t, rgb.b * t, rgb.a);
		}

		public Color Lighten(Color rgb)
		{
			float t = _random.Unit().ClosedFloat();
			return new Color((1f - rgb.r) * t + rgb.r, (1f - rgb.g) * t + rgb.g, (1f - rgb.b) * t + rgb.b, rgb.a);
		}

		#endregion

		#region RGB

		public Color RGB()
		{
			return new Color(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), 1f);
		}

		public Color RGB(float a)
		{
			return new Color(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), a);
		}

		public Color RGBA()
		{
			return new Color(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat());
		}

		public Color ChangeRed(Color rgb)
		{
			return new Color(_random.Unit().ClosedFloat(), rgb.g, rgb.b, rgb.a);
		}

		public Color ChangeRed(Color rgb, float maxChange)
		{
			return new Color(ChangeClamped(rgb.r, maxChange), rgb.g, rgb.b, rgb.a);
		}

		public Color ChangeGreen(Color rgb)
		{
			return new Color(rgb.r, _random.Unit().ClosedFloat(), rgb.b, rgb.a);
		}

		public Color ChangeGreen(Color rgb, float maxChange)
		{
			return new Color(rgb.r, ChangeClamped(rgb.g, maxChange), rgb.b, rgb.a);
		}

		public Color ChangeBlue(Color rgb)
		{
			return new Color(rgb.r, rgb.g, _random.Unit().ClosedFloat(), rgb.a);
		}

		public Color ChangeBlue(Color rgb, float maxChange)
		{
			return new Color(rgb.r, rgb.g, ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		public Color ChangeAlpha(Color rgb)
		{
			return new Color(rgb.r, rgb.g, rgb.b, _random.Unit().ClosedFloat());
		}

		public Color ChangeAlpha(Color rgb, float maxChange)
		{
			return new Color(rgb.r, rgb.g, rgb.b, ChangeClamped(rgb.a, maxChange));
		}

		public Color ChangeRedGreen(Color rgb)
		{
			return new Color(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), rgb.b, rgb.a);
		}

		public Color ChangeRedGreen(Color rgb, float maxChange)
		{
			return new Color(ChangeClamped(rgb.r, maxChange), ChangeClamped(rgb.g, maxChange), rgb.b, rgb.a);
		}

		public Color ChangeRedGreen(Color rgb, float maxRedChange, float maxGreenChange)
		{
			return new Color(ChangeClamped(rgb.r, maxRedChange), ChangeClamped(rgb.g, maxGreenChange), rgb.b, rgb.a);
		}

		public Color ChangeRedBlue(Color rgb)
		{
			return new Color(_random.Unit().ClosedFloat(), rgb.g, _random.Unit().ClosedFloat(), rgb.a);
		}

		public Color ChangeRedBlue(Color rgb, float maxChange)
		{
			return new Color(ChangeClamped(rgb.r, maxChange), rgb.g, ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		public Color ChangeRedBlue(Color rgb, float maxRedChange, float maxBlueChange)
		{
			return new Color(ChangeClamped(rgb.r, maxRedChange), rgb.g, ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		public Color ChangeGreenBlue(Color rgb)
		{
			return new Color(rgb.r, _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), rgb.a);
		}

		public Color ChangeGreenBlue(Color rgb, float maxChange)
		{
			return new Color(rgb.r, ChangeClamped(rgb.g, maxChange), ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		public Color ChangeGreenBlue(Color rgb, float maxGreenChange, float maxBlueChange)
		{
			return new Color(rgb.r, ChangeClamped(rgb.g, maxGreenChange), ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		public Color ChangeRedGreenBlue(Color rgb)
		{
			return new Color(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), rgb.a);
		}

		public Color ChangeRedGreenBlue(Color rgb, float maxChange)
		{
			return new Color(ChangeClamped(rgb.r, maxChange), ChangeClamped(rgb.g, maxChange), ChangeClamped(rgb.b, maxChange), rgb.a);
		}

		public Color ChangeRedGreenBlue(Color rgb, float maxRedChange, float maxGreenChange, float maxBlueChange)
		{
			return new Color(ChangeClamped(rgb.r, maxRedChange), ChangeClamped(rgb.g, maxGreenChange), ChangeClamped(rgb.b, maxBlueChange), rgb.a);
		}

		public Color ChangeAll(Color rgb)
		{
			return new Color(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat());
		}

		public Color ChangeAll(Color rgb, float maxChange)
		{
			return new Color(ChangeClamped(rgb.r, maxChange), ChangeClamped(rgb.g, maxChange), ChangeClamped(rgb.b, maxChange), ChangeClamped(rgb.a, maxChange));
		}

		public Color ChangeAll(Color rgb, float maxChange, float maxAlphaChange)
		{
			return new Color(ChangeClamped(rgb.r, maxChange), ChangeClamped(rgb.g, maxChange), ChangeClamped(rgb.b, maxChange), ChangeClamped(rgb.a, maxAlphaChange));
		}

		public Color ChangeAll(Color rgb, float maxRedChange, float maxGreenChange, float maxBlueChange, float maxAlphaChange)
		{
			return new Color(ChangeClamped(rgb.r, maxRedChange), ChangeClamped(rgb.g, maxGreenChange), ChangeClamped(rgb.b, maxBlueChange), ChangeClamped(rgb.a, maxAlphaChange));
		}

		#endregion

		#region HSV

		public ColorHSV HSV()
		{
			return HSV(1f);
		}

		public ColorHSV HSV(float a)
		{
			float hue = _random.Unit().HalfOpenFloat();
			Vector2 chromaValue = _random.Vector().PointWithinTriangle(new Vector2(1f, 1f), new Vector2(0f, 1f));
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

		public ColorHSV HSVA()
		{
			return HSV(_random.Unit().ClosedFloat());
		}

		public ColorHSV UnbiasedHSV()
		{
			return UnbiasedHSV(1f);
		}

		public ColorHSV UnbiasedHSV(float a)
		{
			throw new System.NotImplementedException();
		}

		public ColorHSV UnbiasedHSVA()
		{
			return UnbiasedHSV(_random.Unit().ClosedFloat());
		}

		public ColorHSV ChangeHue(ColorHSV hsv)
		{
			return new ColorHSV(_random.Unit().ClosedFloat(), hsv.s, hsv.v, hsv.a);
		}

		public ColorHSV ChangeHue(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange), hsv.s, hsv.v, hsv.a);
		}

		public ColorHSV ChangeSat(ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, _random.Unit().ClosedFloat(), hsv.v, hsv.a);
		}

		public ColorHSV ChangeSat(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, ChangeClamped(hsv.s, maxChange), hsv.v, hsv.a);
		}

		public ColorHSV ChangeValue(ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, hsv.s, _random.Unit().ClosedFloat(), hsv.a);
		}

		public ColorHSV ChangeValue(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, hsv.s, ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		public ColorHSV ChangeAlpha(ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, hsv.s, hsv.v, _random.Unit().ClosedFloat());
		}

		public ColorHSV ChangeAlpha(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, hsv.s, hsv.v, ChangeClamped(hsv.a, maxChange));
		}

		public ColorHSV ChangeHueSat(ColorHSV hsv)
		{
			return new ColorHSV(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), hsv.v, hsv.a);
		}

		public ColorHSV ChangeHueSat(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange), ChangeClamped(hsv.s, maxChange), hsv.v, hsv.a);
		}

		public ColorHSV ChangeHueSat(ColorHSV hsv, float maxHueChange, float maxSatChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxHueChange), ChangeClamped(hsv.s, maxSatChange), hsv.v, hsv.a);
		}

		public ColorHSV ChangeHueValue(ColorHSV hsv)
		{
			return new ColorHSV(_random.Unit().ClosedFloat(), hsv.s, _random.Unit().ClosedFloat(), hsv.a);
		}

		public ColorHSV ChangeHueValue(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange), hsv.s, ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		public ColorHSV ChangeHueValue(ColorHSV hsv, float maxHueChange, float maxValueChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxHueChange), hsv.s, ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		public ColorHSV ChangeSatValue(ColorHSV hsv)
		{
			return new ColorHSV(hsv.h, _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), hsv.a);
		}

		public ColorHSV ChangeSatValue(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(hsv.h, ChangeClamped(hsv.s, maxChange), ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		public ColorHSV ChangeSatValue(ColorHSV hsv, float maxSatChange, float maxValueChange)
		{
			return new ColorHSV(hsv.h, ChangeClamped(hsv.s, maxSatChange), ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		public ColorHSV ChangeHueSatValue(ColorHSV hsv)
		{
			return new ColorHSV(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), hsv.a);
		}

		public ColorHSV ChangeHueSatValue(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange), ChangeClamped(hsv.s, maxChange), ChangeClamped(hsv.v, maxChange), hsv.a);
		}

		public ColorHSV ChangeHueSatValue(ColorHSV hsv, float maxHueChange, float maxSatChange, float maxValueChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxHueChange), ChangeClamped(hsv.s, maxSatChange), ChangeClamped(hsv.v, maxValueChange), hsv.a);
		}

		public ColorHSV ChangeAll(ColorHSV hsv)
		{
			return new ColorHSV(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat());
		}

		public ColorHSV ChangeAll(ColorHSV hsv, float maxChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange), ChangeClamped(hsv.s, maxChange), ChangeClamped(hsv.v, maxChange), ChangeClamped(hsv.a, maxChange));
		}

		public ColorHSV ChangeAll(ColorHSV hsv, float maxChange, float maxAlphaChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxChange), ChangeClamped(hsv.s, maxChange), ChangeClamped(hsv.v, maxChange), ChangeClamped(hsv.a, maxAlphaChange));
		}

		public ColorHSV ChangeAll(ColorHSV hsv, float maxHueChange, float maxSatChange, float maxValueChange, float maxAlphaChange)
		{
			return new ColorHSV(ChangeRepeated(hsv.h, maxHueChange), ChangeClamped(hsv.s, maxSatChange), ChangeClamped(hsv.v, maxValueChange), ChangeClamped(hsv.a, maxAlphaChange));
		}

		#endregion

		#region HSL

		public ColorHSL HSL()
		{
			return HSL(1f);
		}

		public ColorHSL HSL(float a)
		{
			float hue = _random.Unit().HalfOpenFloat();
			Vector2 chromaLightness = _random.Vector().PointWithinTriangle(new Vector2(1f, 0.5f), new Vector2(0f, 1f));
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

		public ColorHSL HSLA()
		{
			return HSL(_random.Unit().ClosedFloat());
		}

		public ColorHSL UnbiasedHSL()
		{
			return UnbiasedHSL(1f);
		}

		public ColorHSL UnbiasedHSL(float a)
		{
			throw new System.NotImplementedException();
		}

		public ColorHSL UnbiasedHSLA()
		{
			return UnbiasedHSL(_random.Unit().ClosedFloat());
		}

		public ColorHSL ChangeHue(ColorHSL hsl)
		{
			return new ColorHSL(_random.Unit().ClosedFloat(), hsl.s, hsl.l, hsl.a);
		}

		public ColorHSL ChangeHue(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange), hsl.s, hsl.l, hsl.a);
		}

		public ColorHSL ChangeSat(ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, _random.Unit().ClosedFloat(), hsl.l, hsl.a);
		}

		public ColorHSL ChangeSat(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, ChangeClamped(hsl.s, maxChange), hsl.l, hsl.a);
		}

		public ColorHSL ChangeLight(ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, hsl.s, _random.Unit().ClosedFloat(), hsl.a);
		}

		public ColorHSL ChangeLight(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, hsl.s, ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		public ColorHSL ChangeAlpha(ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, hsl.s, hsl.l, _random.Unit().ClosedFloat());
		}

		public ColorHSL ChangeAlpha(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, hsl.s, hsl.l, ChangeClamped(hsl.a, maxChange));
		}

		public ColorHSL ChangeHueSat(ColorHSL hsl)
		{
			return new ColorHSL(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), hsl.l, hsl.a);
		}

		public ColorHSL ChangeHueSat(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange), ChangeClamped(hsl.s, maxChange), hsl.l, hsl.a);
		}

		public ColorHSL ChangeHueSat(ColorHSL hsl, float maxHueChange, float maxSatChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxHueChange), ChangeClamped(hsl.s, maxSatChange), hsl.l, hsl.a);
		}

		public ColorHSL ChangeHueLight(ColorHSL hsl)
		{
			return new ColorHSL(_random.Unit().ClosedFloat(), hsl.s, _random.Unit().ClosedFloat(), hsl.a);
		}

		public ColorHSL ChangeHueLight(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange), hsl.s, ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		public ColorHSL ChangeHueLight(ColorHSL hsl, float maxHueChange, float maxLightChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxHueChange), hsl.s, ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		public ColorHSL ChangeSatLight(ColorHSL hsl)
		{
			return new ColorHSL(hsl.h, _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), hsl.a);
		}

		public ColorHSL ChangeSatLight(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(hsl.h, ChangeClamped(hsl.s, maxChange), ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		public ColorHSL ChangeSatLight(ColorHSL hsl, float maxSatChange, float maxLightChange)
		{
			return new ColorHSL(hsl.h, ChangeClamped(hsl.s, maxSatChange), ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		public ColorHSL ChangeHueSatLight(ColorHSL hsl)
		{
			return new ColorHSL(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), hsl.a);
		}

		public ColorHSL ChangeHueSatLight(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange), ChangeClamped(hsl.s, maxChange), ChangeClamped(hsl.l, maxChange), hsl.a);
		}

		public ColorHSL ChangeHueSatLight(ColorHSL hsl, float maxHueChange, float maxSatChange, float maxLightChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxHueChange), ChangeClamped(hsl.s, maxSatChange), ChangeClamped(hsl.l, maxLightChange), hsl.a);
		}

		public ColorHSL ChangeAll(ColorHSL hsl)
		{
			return new ColorHSL(_random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat(), _random.Unit().ClosedFloat());
		}

		public ColorHSL ChangeAll(ColorHSL hsl, float maxChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange), ChangeClamped(hsl.s, maxChange), ChangeClamped(hsl.l, maxChange), ChangeClamped(hsl.a, maxChange));
		}

		public ColorHSL ChangeAll(ColorHSL hsl, float maxChange, float maxAlphaChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxChange), ChangeClamped(hsl.s, maxChange), ChangeClamped(hsl.l, maxChange), ChangeClamped(hsl.a, maxAlphaChange));
		}

		public ColorHSL ChangeAll(ColorHSL hsl, float maxHueChange, float maxSatChange, float maxLightChange, float maxAlphaChange)
		{
			return new ColorHSL(ChangeRepeated(hsl.h, maxHueChange), ChangeClamped(hsl.s, maxSatChange), ChangeClamped(hsl.l, maxLightChange), ChangeClamped(hsl.a, maxAlphaChange));
		}

		#endregion

		#region HCY

		public ColorHCY HCY()
		{
			return HCY(1f);
		}

		public ColorHCY HCY(float a)
		{
			float hue = _random.Unit().HalfOpenFloat();
			Vector2 chromaLuma = _random.Vector().PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hue)), new Vector2(0f, 1f));
			return new ColorHCY(hue, chromaLuma.x, chromaLuma.y, a);
		}

		public ColorHCY HCYA()
		{
			return HCY(_random.Unit().ClosedFloat());
		}

		public ColorHCY ChangeHue(ColorHCY hcy)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc._random.Unit().ClosedFloat(), hcy.c, hcy.y, hcy.a));
		}

		public ColorHCY ChangeHue(ColorHCY hcy, float maxChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxChange), hcy.c, hcy.y, hcy.a));
		}

		public ColorHCY ChangeChroma(ColorHCY hcy)
		{
			return new ColorHCY(hcy.h, _random.Range().Closed(ColorHCY.GetMaxChroma(hcy.h, hcy.y)), hcy.y, hcy.a);
		}

		public ColorHCY ChangeChroma(ColorHCY hcy, float maxChange)
		{
			return new ColorHCY(hcy.h, ChangeClamped(hcy.c, maxChange, 0f, ColorHCY.GetMaxChroma(hcy.h, hcy.y)), hcy.y, hcy.a);
		}

		public ColorHCY ChangeLuma(ColorHCY hcy)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(hcy.h, hcy.c, out yMin, out yMax);
			return new ColorHCY(hcy.h, hcy.c, _random.Range().Closed(yMin, yMax), hcy.a);
		}

		public ColorHCY ChangeLuma(ColorHCY hcy, float maxChange)
		{
			float yMin, yMax;
			ColorHCY.GetMinMaxLuma(hcy.h, hcy.c, out yMin, out yMax);
			return new ColorHCY(hcy.h, hcy.c, ChangeClamped(hcy.y, maxChange, yMin, yMax), hcy.a);
		}

		public ColorHCY ChangeAlpha(ColorHCY hcy)
		{
			return new ColorHCY(hcy.h, hcy.c, hcy.y, _random.Unit().ClosedFloat());
		}

		public ColorHCY ChangeAlpha(ColorHCY hcy, float maxChange)
		{
			return new ColorHCY(hcy.h, hcy.c, hcy.y, ChangeClamped(hcy.a, maxChange));
		}

		public ColorHCY ChangeHueChroma(ColorHCY hcy)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc._random.Unit().ClosedFloat(), rc._random.Unit().ClosedFloat(), hcy.y, hcy.a));
		}

		public ColorHCY ChangeHueChroma(ColorHCY hcy, float maxChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxChange), rc.ChangeClamped(hcy.c, maxChange), hcy.y, hcy.a));
		}

		public ColorHCY ChangeHueChroma(ColorHCY hcy, float maxHueChange, float maxChromaChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxHueChange), rc.ChangeClamped(hcy.c, maxChromaChange), hcy.y, hcy.a));
		}

		public ColorHCY ChangeHueLuma(ColorHCY hcy)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc._random.Unit().ClosedFloat(), hcy.c, rc._random.Unit().ClosedFloat(), hcy.a));
		}

		public ColorHCY ChangeHueLuma(ColorHCY hcy, float maxChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxChange), hcy.c, rc.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		public ColorHCY ChangeHueLuma(ColorHCY hcy, float maxHueChange, float maxLumaChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxHueChange), hcy.c, rc.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		public ColorHCY ChangeChromaLuma(ColorHCY hcy)
		{
			Vector2 chromaLuma = _random.Vector().PointWithinTriangle(new Vector2(1f, ColorHCY.GetLumaAtMaxChroma(hcy.h)), new Vector2(0f, 1f));
			return new ColorHCY(hcy.h, chromaLuma.x, chromaLuma.y, hcy.a);
		}

		public ColorHCY ChangeChromaLuma(ColorHCY hcy, float maxChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(hcy.h, rc.ChangeClamped(hcy.c, maxChange), rc.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		public ColorHCY ChangeChromaLuma(ColorHCY hcy, float maxChromaChange, float maxLumaChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(hcy.h, rc.ChangeClamped(hcy.c, maxChromaChange), rc.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		public ColorHCY ChangeHueChromaLuma(ColorHCY hcy)
		{
			return HCY(hcy.a);
		}

		public ColorHCY ChangeHueChromaLuma(ColorHCY hcy, float maxChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxChange), rc.ChangeClamped(hcy.c, maxChange), rc.ChangeClamped(hcy.y, maxChange), hcy.a));
		}

		public ColorHCY ChangeHueChromaLuma(ColorHCY hcy, float maxHueChange, float maxChromaChange, float maxLumaChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxHueChange), rc.ChangeClamped(hcy.c, maxChromaChange), rc.ChangeClamped(hcy.y, maxLumaChange), hcy.a));
		}

		public ColorHCY ChangeAll(ColorHCY hcy)
		{
			return HCYA();
		}

		public ColorHCY ChangeAll(ColorHCY hcy, float maxChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxChange), rc.ChangeClamped(hcy.c, maxChange), rc.ChangeClamped(hcy.y, maxChange), rc.ChangeClamped(hcy.a, maxChange)));
		}

		public ColorHCY ChangeAll(ColorHCY hcy, float maxChange, float maxAlphaChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxChange), rc.ChangeClamped(hcy.c, maxChange), rc.ChangeClamped(hcy.y, maxChange), rc.ChangeClamped(hcy.a, maxAlphaChange)));
		}

		public ColorHCY ChangeAll(ColorHCY hcy, float maxHueChange, float maxChromaChange, float maxLumaChange, float maxAlphaChange)
		{
			return Change(hcy, (RandomColor rc) => new ColorHCY(rc.ChangeRepeated(hcy.h, maxHueChange), rc.ChangeClamped(hcy.c, maxChromaChange), rc.ChangeClamped(hcy.y, maxLumaChange), rc.ChangeClamped(hcy.a, maxAlphaChange)));
		}

		private ColorHCY Change(ColorHCY hcy, System.Func<RandomColor, ColorHCY> generator)
		{
			int maxIterations = hcy.canConvertToRGB ? 100 : 5; // If the input color already can't convert to RGB, then there's no guarantee that the generator will produce a convertible color, so be much more eager to give up in that case.
			int iterations = 0;

			ColorHCY hcyRandom;
			do
			{
				hcyRandom = generator(this);
				++iterations;
			}
			while (!hcyRandom.canConvertToRGB && iterations < maxIterations);

			return hcyRandom;
		}

		#endregion

		#region Private Helper Functions

		private float ChangeClamped(float original, float maxChange)
		{
			return _random.Range().Closed(Mathf.Max(0f, original - maxChange), Mathf.Min(original + maxChange, 1f));
		}

		private float ChangeClamped(float original, float maxChange, float min, float max)
		{
			return _random.Range().Closed(Mathf.Max(min, original - maxChange), Mathf.Min(original + maxChange, max));
		}

		private float ChangeRepeated(float original, float maxChange)
		{
			return Mathf.Repeat(original + _random.Range().Closed(-maxChange, +maxChange), 1f);
		}

		#endregion
	}

	public static class RandomColorExtensions
	{
		public static RandomColor Color(this IRandomEngine random)
		{
			return new RandomColor(random);
		}
	}
}
