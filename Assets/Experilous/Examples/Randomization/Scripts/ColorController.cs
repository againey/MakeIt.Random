/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Experilous.Randomization;

namespace Experilous.Examples.Randomization
{
	public class ColorController : MonoBehaviour
	{
		public Image selectedColorImage;
		public ToggleGroup colorGrid;
		public ColorToggleButton firstColorButton;

		public UnitComponentSlider rgbMaxRedDeltaSlider;
		public UnitComponentSlider rgbMaxGreenDeltaSlider;
		public UnitComponentSlider rgbMaxBlueDeltaSlider;

		public UnitComponentSlider hsvMaxHueDeltaSlider;
		public UnitComponentSlider hsvMaxSatDeltaSlider;
		public UnitComponentSlider hsvMaxValueDeltaSlider;

		public UnitComponentSlider hslMaxHueDeltaSlider;
		public UnitComponentSlider hslMaxSatDeltaSlider;
		public UnitComponentSlider hslMaxLightDeltaSlider;

		public UnitComponentSlider hcyMaxHueDeltaSlider;
		public UnitComponentSlider hcyMaxChromaDeltaSlider;
		public UnitComponentSlider hcyMaxLumaDeltaSlider;

		public Text rgbSelectedColorText;
		public Text hsvSelectedColorText;
		public Text hslSelectedColorText;
		public Text hcySelectedColorText;

		private ColorToggleButton _selectedColorButton;
		private ColorToggleButton[] _colorButtons;

		private IRandomEngine _random;

		protected void Start()
		{
			_random = XorShift128Plus.Create();

			_colorButtons = colorGrid.GetComponentsInChildren<ColorToggleButton>();

			SelectColorButton(firstColorButton);
		}

		private Image GetColorButtonImage(Toggle colorButton)
		{
			return colorButton.transform.Find("Color Image").GetComponent<Image>();
		}

		private void SelectColorButton(ColorToggleButton colorButton)
		{
			firstColorButton.GetComponent<Toggle>().isOn = true;
		}

		private void SelectColorButton(ColorToggleButton colorButton, Color color)
		{
			firstColorButton.colorImage.color = color;
			firstColorButton.GetComponent<Toggle>().isOn = true;
		}

		private int ComponentsAs24BitInt(float a, float b, float c)
		{
			int ia = Mathf.FloorToInt(a * 255.99f);
			int ib = Mathf.FloorToInt(b * 255.99f);
			int ic = Mathf.FloorToInt(c * 255.99f);
			return ia * 65536 + ib * 256 + ic;
		}

		private void SelectColor(Color rgb)
		{
			selectedColorImage.color = rgb;

			ColorHSV hsv = ColorHSV.FromRGB(rgb);
			ColorHSL hsl = ColorHSL.FromRGB(rgb);
			ColorHCY hcy = ColorHCY.FromRGB(rgb);

			rgbSelectedColorText.text = string.Format("RGB({0:F3}, {1:F3}, {2:F3}), #{3:X6}", rgb.r, rgb.g, rgb.b, ComponentsAs24BitInt(rgb.r, rgb.g, rgb.b));
			hsvSelectedColorText.text = string.Format("HSV({0:F3}, {1:F3}, {2:F3}), #{3:X6}", hsv.h, hsv.s, hsv.v, ComponentsAs24BitInt(hsv.h, hsv.s, hsv.v));
			hslSelectedColorText.text = string.Format("HSL({0:F3}, {1:F3}, {2:F3}), #{3:X6}", hsl.h, hsl.s, hsl.l, ComponentsAs24BitInt(hsl.h, hsl.s, hsl.l));
			hcySelectedColorText.text = string.Format("HCY({0:F3}, {1:F3}, {2:F3}), #{3:X6}", hcy.h, hcy.c, hcy.y, ComponentsAs24BitInt(hcy.h, hcy.c, hcy.y));
		}

		private void UpdateSelectedColor()
		{
			SelectColor(_selectedColorButton.colorImage.color);
		}

		public void ColorToggleButtonValueChanged(Toggle toggle)
		{
			if (toggle.isOn)
			{
				_selectedColorButton = toggle.GetComponent<ColorToggleButton>();
				SelectColor(_selectedColorButton.colorImage.color);
			}
		}

		public void GenerateRGB()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.RGB(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateHSV()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = (Color)RandomColor.HSV(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateHSL()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = (Color)RandomColor.HSL(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateHCY()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = (Color)RandomColor.HCY(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateAnyRed()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.AnyRed(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateDarkRed()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.DarkRed(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateLightRed()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.LightRed(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateAnyGreen()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.AnyGreen(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateDarkGreen()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.DarkGreen(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateLightGreen()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.LightGreen(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateAnyBlue()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.AnyBlue(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateDarkBlue()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.DarkBlue(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateLightBlue()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.LightBlue(_random);
			}

			UpdateSelectedColor();
		}

		public void GenerateDarken()
		{
			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = RandomColor.Darken(selectedColor, _random);
				}
			}
		}

		public void GenerateLighten()
		{
			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = RandomColor.Lighten(selectedColor, _random);
				}
			}
		}

		public void GenerateChangeRGB()
		{
			Color selectedColor = _selectedColorButton.colorImage.color;

			float maxRedChange = rgbMaxRedDeltaSlider.value;
			float maxGreenChange = rgbMaxGreenDeltaSlider.value;
			float maxBlueChange = rgbMaxBlueDeltaSlider.value;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					if (maxRedChange > 0f)
					{
						if (maxGreenChange > 0f)
						{
							if (maxBlueChange > 0f)
							{
								colorButton.colorImage.color = RandomColor.ChangeRedGreenBlue(selectedColor, maxRedChange, maxGreenChange, maxBlueChange, _random);
							}
							else
							{
								colorButton.colorImage.color = RandomColor.ChangeRedGreen(selectedColor, maxRedChange, maxGreenChange, _random);
							}
						}
						else
						{
							if (maxBlueChange > 0f)
							{
								colorButton.colorImage.color = RandomColor.ChangeRedBlue(selectedColor, maxRedChange, maxBlueChange, _random);
							}
							else
							{
								colorButton.colorImage.color = RandomColor.ChangeRed(selectedColor, maxRedChange, _random);
							}
						}
					}
					else
					{
						if (maxGreenChange > 0f)
						{
							if (maxBlueChange > 0f)
							{
								colorButton.colorImage.color = RandomColor.ChangeGreenBlue(selectedColor, maxGreenChange, maxBlueChange, _random);
							}
							else
							{
								colorButton.colorImage.color = RandomColor.ChangeGreen(selectedColor, maxGreenChange, _random);
							}
						}
						else
						{
							if (maxBlueChange > 0f)
							{
								colorButton.colorImage.color = RandomColor.ChangeBlue(selectedColor, maxBlueChange, _random);
							}
							else
							{
								colorButton.colorImage.color = selectedColor;
							}
						}
					}
				}
			}
		}

		public void GenerateChangeHSV()
		{
			ColorHSV selectedColor = (ColorHSV)_selectedColorButton.colorImage.color;

			float maxHueChange = hsvMaxHueDeltaSlider.value;
			float maxSatChange = hsvMaxSatDeltaSlider.value;
			float maxValueChange = hsvMaxValueDeltaSlider.value;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					if (maxHueChange > 0f)
					{
						if (maxSatChange > 0f)
						{
							if (maxValueChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueSatValue(selectedColor, maxHueChange, maxSatChange, maxValueChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueSat(selectedColor, maxHueChange, maxSatChange, _random);
							}
						}
						else
						{
							if (maxValueChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueValue(selectedColor, maxHueChange, maxValueChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHue(selectedColor, maxHueChange, _random);
							}
						}
					}
					else
					{
						if (maxSatChange > 0f)
						{
							if (maxValueChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeSatValue(selectedColor, maxSatChange, maxValueChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeSat(selectedColor, maxSatChange, _random);
							}
						}
						else
						{
							if (maxValueChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeValue(selectedColor, maxValueChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)selectedColor;
							}
						}
					}
				}
			}
		}

		public void GenerateChangeHSL()
		{
			ColorHSL selectedColor = (ColorHSL)_selectedColorButton.colorImage.color;

			float maxHueChange = hslMaxHueDeltaSlider.value;
			float maxSatChange = hslMaxSatDeltaSlider.value;
			float maxLightChange = hslMaxLightDeltaSlider.value;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					if (maxHueChange > 0f)
					{
						if (maxSatChange > 0f)
						{
							if (maxLightChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueSatLight(selectedColor, maxHueChange, maxSatChange, maxLightChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueSat(selectedColor, maxHueChange, maxSatChange, _random);
							}
						}
						else
						{
							if (maxLightChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueLight(selectedColor, maxHueChange, maxLightChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHue(selectedColor, maxHueChange, _random);
							}
						}
					}
					else
					{
						if (maxSatChange > 0f)
						{
							if (maxLightChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeSatLight(selectedColor, maxSatChange, maxLightChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeSat(selectedColor, maxSatChange, _random);
							}
						}
						else
						{
							if (maxLightChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeLight(selectedColor, maxLightChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)selectedColor;
							}
						}
					}
				}
			}
		}

		public void GenerateChangeHCY()
		{
			ColorHCY selectedColor = (ColorHCY)_selectedColorButton.colorImage.color;

			float maxHueChange = hcyMaxHueDeltaSlider.value;
			float maxChromaChange = hcyMaxChromaDeltaSlider.value;
			float maxLumaChange = hcyMaxLumaDeltaSlider.value;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					if (maxHueChange > 0f)
					{
						if (maxChromaChange > 0f)
						{
							if (maxLumaChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueChromaLuma(selectedColor, maxHueChange, maxChromaChange, maxLumaChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueChroma(selectedColor, maxHueChange, maxChromaChange, _random);
							}
						}
						else
						{
							if (maxLumaChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHueLuma(selectedColor, maxHueChange, maxLumaChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeHue(selectedColor, maxHueChange, _random);
							}
						}
					}
					else
					{
						if (maxChromaChange > 0f)
						{
							if (maxLumaChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeChromaLuma(selectedColor, maxChromaChange, maxLumaChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeChroma(selectedColor, maxChromaChange, _random);
							}
						}
						else
						{
							if (maxLumaChange > 0f)
							{
								colorButton.colorImage.color = (Color)RandomColor.ChangeLuma(selectedColor, maxLumaChange, _random);
							}
							else
							{
								colorButton.colorImage.color = (Color)selectedColor;
							}
						}
					}
				}
			}
		}
	}
}