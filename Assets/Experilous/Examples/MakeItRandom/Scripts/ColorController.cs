/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using Experilous.MakeItRandom;
using Experilous.MakeItColorful;

namespace Experilous.Examples.MakeItRandom
{
	public class ColorController : MonoBehaviour
	{
		public Image selectedColorImage;
		public ToggleGroup colorGrid;
		public ColorToggleButton firstColorButton;

		public Toggle rgbToggle;
		public Toggle cmyToggle;
		public Toggle cmykToggle;
		public Toggle hsvToggle;
		public Toggle hslToggle;
		public Toggle hsyToggle;
		public Toggle hcvToggle;
		public Toggle hclToggle;
		public Toggle hcyToggle;

		public ToggleGroup colorCategoriesToggleGroup;

		public ColorComponentSlider firstAbsoluteColorComponentSlider;
		public ColorComponentSlider secondAbsoluteColorComponentSlider;
		public ColorComponentSlider thirdAbsoluteColorComponentSlider;
		public ColorComponentSlider fourthAbsoluteColorComponentSlider;

		public ColorComponentSlider relativeColorComponentSlider;

		public RectTransform saturationPanel;
		public RectTransform hardnessPanel;
		public RectTransform lightnessPanel;

		public Text firstColorComponentLabel;
		public Text secondColorComponentLabel;
		public Text thirdColorComponentLabel;
		public Text fourthColorComponentLabel;

		public Text firstColorComponentValue;
		public Text secondColorComponentValue;
		public Text thirdColorComponentValue;
		public Text fourthColorComponentValue;
		public Text colorHexadecimalValue;

		private ColorToggleButton _selectedColorButton;
		private ColorToggleButton[] _colorButtons;

		private IRandom _random;

		protected void Start()
		{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
			colorCategoriesToggleGroup.transform.parent.GetComponent<ScrollRect>().scrollSensitivity = 25;
#endif

			_random = XorShift128Plus.Create();

			_colorButtons = colorGrid.GetComponentsInChildren<ColorToggleButton>();

			SelectColorButton(firstColorButton);
			OnColorSpaceToggle(rgbToggle);
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

		private uint ComponentsAs24BitInt(float a, float b, float c)
		{
			uint ia = (uint)Mathf.FloorToInt(a * 255.99f);
			uint ib = (uint)Mathf.FloorToInt(b * 255.99f);
			uint ic = (uint)Mathf.FloorToInt(c * 255.99f);
			return ia * 65536U + ib * 256U + ic;
		}

		private uint ComponentsAs32BitInt(float a, float b, float c, float d)
		{
			uint ia = (uint)Mathf.FloorToInt(a * 255.99f);
			uint ib = (uint)Mathf.FloorToInt(b * 255.99f);
			uint ic = (uint)Mathf.FloorToInt(c * 255.99f);
			uint id = (uint)Mathf.FloorToInt(d * 255.99f);
			return ia * 16777216U + ib * 65536U + ic * 256U + id;
		}

		private void SelectColor(Color rgb)
		{
			selectedColorImage.color = rgb;

			if (rgbToggle.isOn)
			{
				UpdateSelectedColorFields(rgb.r, rgb.g, rgb.b);
			}
			else if (cmyToggle.isOn)
			{
				var cmy = (ColorCMY)rgb;
				UpdateSelectedColorFields(cmy.c, cmy.m, cmy.y);
			}
			else if (cmykToggle.isOn)
			{
				var cmyk = (ColorCMYK)rgb;
				UpdateSelectedColorFields(cmyk.c, cmyk.m, cmyk.y, cmyk.k);
			}
			else if (hsvToggle.isOn)
			{
				var hsv = (ColorHSV)rgb;
				UpdateSelectedColorFields(hsv.h, hsv.s, hsv.v);
			}
			else if (hslToggle.isOn)
			{
				var hsl = (ColorHSL)rgb;
				UpdateSelectedColorFields(hsl.h, hsl.s, hsl.l);
			}
			else if (hsyToggle.isOn)
			{
				var hsy = (ColorHSY)rgb;
				UpdateSelectedColorFields(hsy.h, hsy.s, hsy.y);
			}
			else if (hcvToggle.isOn)
			{
				var hcv = (ColorHCV)rgb;
				UpdateSelectedColorFields(hcv.h, hcv.c, hcv.v);
			}
			else if (hclToggle.isOn)
			{
				var hcl = (ColorHCL)rgb;
				UpdateSelectedColorFields(hcl.h, hcl.c, hcl.l);
			}
			else if (hcyToggle.isOn)
			{
				var hcy = (ColorHCY)rgb;
				UpdateSelectedColorFields(hcy.h, hcy.c, hcy.y);
			}
			else
			{
				throw new System.NotImplementedException();
			}
		}

		private void UpdateSelectedColorFields(float first, float second, float third)
		{
			firstColorComponentValue.text = first.ToString("F3");
			secondColorComponentValue.text = second.ToString("F3");
			thirdColorComponentValue.text = third.ToString("F3");
			colorHexadecimalValue.text = '#' + ComponentsAs24BitInt(first, second, third).ToString("X6");
		}

		private void UpdateSelectedColorFields(float first, float second, float third, float fourth)
		{
			firstColorComponentValue.text = first.ToString("F3");
			secondColorComponentValue.text = second.ToString("F3");
			thirdColorComponentValue.text = third.ToString("F3");
			fourthColorComponentValue.text = fourth.ToString("F3");
			colorHexadecimalValue.text = '#' + ComponentsAs32BitInt(first, second, third, fourth).ToString("X8");
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

		public void OnColorSpaceToggle(Toggle toggle)
		{
			if (toggle.isOn)
			{
				if (toggle == cmykToggle)
				{
					fourthAbsoluteColorComponentSlider.gameObject.SetActive(true);
					fourthColorComponentLabel.transform.parent.gameObject.SetActive(true);
					fourthAbsoluteColorComponentSlider.labelField.text = fourthColorComponentLabel.text = "Key";

				}
				else
				{
					fourthAbsoluteColorComponentSlider.gameObject.SetActive(false);
					fourthColorComponentLabel.transform.parent.gameObject.SetActive(false);
				}

				if (toggle == hsvToggle || toggle == hslToggle || toggle == hsyToggle)
				{
					saturationPanel.gameObject.SetActive(true);
				}
				else
				{
					saturationPanel.gameObject.SetActive(false);
				}

				if (toggle == hcvToggle || toggle == hclToggle || toggle == hcyToggle)
				{
					hardnessPanel.gameObject.SetActive(true);
				}
				else
				{
					hardnessPanel.gameObject.SetActive(false);
				}

				if (toggle == rgbToggle)
				{
					firstAbsoluteColorComponentSlider.labelField.text = firstColorComponentLabel.text = "Red";
					secondAbsoluteColorComponentSlider.labelField.text = secondColorComponentLabel.text = "Green";
					thirdAbsoluteColorComponentSlider.labelField.text = thirdColorComponentLabel.text = "Blue";
				}
				else if (toggle == cmyToggle || toggle == cmykToggle)
				{
					firstAbsoluteColorComponentSlider.labelField.text = firstColorComponentLabel.text = "Cyan";
					secondAbsoluteColorComponentSlider.labelField.text = secondColorComponentLabel.text = "Magenta";
					thirdAbsoluteColorComponentSlider.labelField.text = thirdColorComponentLabel.text = "Yellow";
				}
				else
				{
					firstAbsoluteColorComponentSlider.labelField.text = firstColorComponentLabel.text = "Hue";
					if (toggle == hsvToggle || toggle == hslToggle || toggle == hsyToggle)
					{
						secondAbsoluteColorComponentSlider.labelField.text = secondColorComponentLabel.text = "Saturation";
					}
					else
					{
						secondAbsoluteColorComponentSlider.labelField.text = secondColorComponentLabel.text = "Chroma";
					}

					if (toggle == hsvToggle || toggle == hcvToggle)
					{
						thirdAbsoluteColorComponentSlider.labelField.text = thirdColorComponentLabel.text = "Value";
					}
					else if (toggle == hslToggle || toggle == hclToggle)
					{
						thirdAbsoluteColorComponentSlider.labelField.text = thirdColorComponentLabel.text = "Lightness";
					}
					else
					{
						thirdAbsoluteColorComponentSlider.labelField.text = thirdColorComponentLabel.text = "Luma";
					}
				}

				UpdateSelectedColor();
			}
		}

		public void GenerateRandomColors()
		{
			System.Func<Color> generator = null;

			var options = colorCategoriesToggleGroup.GetComponentsInChildren<Toggle>();
			foreach (var option in options)
			{
				if (option.isOn)
				{
					if (option.name == "Completely Random Toggle")
					{
						if (rgbToggle.isOn)
						{
							generator = () => _random.ColorRGB();
						}
						else if (cmyToggle.isOn)
						{
							generator = () => _random.ColorCMY();
						}
						else if (cmykToggle.isOn)
						{
							generator = () => _random.ColorCMYK();
						}
						else if (hsvToggle.isOn)
						{
							generator = () => _random.ColorHSV();
						}
						else if (hslToggle.isOn)
						{
							generator = () => _random.ColorHSL();
						}
						else if (hsyToggle.isOn)
						{
							generator = () => _random.ColorHSY();
						}
						else if (hcvToggle.isOn)
						{
							generator = () => _random.ColorHCV();
						}
						else if (hclToggle.isOn)
						{
							generator = () => _random.ColorHCL();
						}
						else if (hcyToggle.isOn)
						{
							generator = () => _random.ColorHCY();
						}
						else
						{
							throw new System.NotImplementedException();
						}
					}
					else if (option.name == "Red Toggle")
					{
						if (hsyToggle.isOn || hcyToggle.isOn)
						{
							generator = () => _random.ColorLumaLerp(new ColorHSY(0f, 1f, 0f), new ColorHSY(0f, 1f, 1f));
						}
						else
						{
							generator = () => _random.ColorRed();
						}
					}
					else if (option.name == "Dark Red Toggle")
					{
						generator = () => _random.ColorDarkRed();
					}
					else if (option.name == "Light Red Toggle")
					{
						generator = () => _random.ColorLightRed();
					}
					else if (option.name == "Green Toggle")
					{
						if (hsyToggle.isOn || hcyToggle.isOn)
						{
							generator = () => _random.ColorLumaLerp(new ColorHSY(1f / 3f, 1f, 0f), new ColorHSY(1f / 3f, 1f, 1f));
						}
						else
						{
							generator = () => _random.ColorGreen();
						}
					}
					else if (option.name == "Dark Green Toggle")
					{
						generator = () => _random.ColorDarkGreen();
					}
					else if (option.name == "Light Green Toggle")
					{
						generator = () => _random.ColorLightGreen();
					}
					else if (option.name == "Blue Toggle")
					{
						if (hsyToggle.isOn || hcyToggle.isOn)
						{
							generator = () => _random.ColorLumaLerp(new ColorHSY(2f / 3f, 1f, 0f), new ColorHSY(2f / 3f, 1f, 1f));
						}
						else
						{
							generator = () => _random.ColorBlue();
						}
					}
					else if (option.name == "Dark Blue Toggle")
					{
						generator = () => _random.ColorDarkBlue();
					}
					else if (option.name == "Light Blue Toggle")
					{
						generator = () => _random.ColorLightBlue();
					}
					else if (option.name == "Bold Toggle")
					{
						generator = () => _random.ColorBold();
					}
					else if (option.name == "Festive Toggle")
					{
						generator = () => _random.ColorFestive();
					}
					else if (option.name == "Pastel Toggle")
					{
						generator = () => _random.ColorPastel();
					}
					else if (option.name == "Pale Toggle")
					{
						generator = () => _random.ColorPale();
					}
					else if (option.name == "Neutral Toggle")
					{
						generator = () => _random.ColorNeutral();
					}
					else if (option.name == "Mellow Toggle")
					{
						generator = () => _random.ColorMellow();
					}
					else if (option.name == "Somber Toggle")
					{
						generator = () => _random.ColorSomber();
					}
					else if (option.name == "Subdued Toggle")
					{
						generator = () => _random.ColorSubdued();
					}
					else if (option.name == "Deep Toggle")
					{
						generator = () => _random.ColorDeep();
					}
					else if (option.name == "Warm Toggle")
					{
						generator = () => _random.ColorWarm();
					}
					else if (option.name == "Hot Toggle")
					{
						generator = () => _random.ColorHot();
					}
					else if (option.name == "Cool Toggle")
					{
						generator = () => _random.ColorCool();
					}
					else if (option.name == "Cold Toggle")
					{
						generator = () => _random.ColorCold();
					}

					break;
				}
			}

			if (generator != null)
			{
				foreach (ColorToggleButton colorButton in _colorButtons)
				{
					colorButton.colorImage.color = generator();
				}

				UpdateSelectedColor();
			}
		}

		public void GenerateChangeColors()
		{
			System.Func<Color, Color> generator;

			float maxChange0 = firstAbsoluteColorComponentSlider.gameObject.activeSelf ? firstAbsoluteColorComponentSlider.value : 0f;
			float maxChange1 = secondAbsoluteColorComponentSlider.gameObject.activeSelf ? secondAbsoluteColorComponentSlider.value : 0f;
			float maxChange2 = thirdAbsoluteColorComponentSlider.gameObject.activeSelf ? thirdAbsoluteColorComponentSlider.value : 0f;
			float maxChange3 = fourthAbsoluteColorComponentSlider.gameObject.activeSelf ? fourthAbsoluteColorComponentSlider.value : 0f;

			if (rgbToggle.isOn)
			{
				generator = (Color color) => _random.ColorRGBShift(color, maxChange0, maxChange1, maxChange2);
			}
			else if (cmyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorCMYShift(color, maxChange0, maxChange1, maxChange2);
			}
			else if (cmykToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorCMYKShift(color, maxChange0, maxChange1, maxChange2, maxChange3);
			}
			else if (hsvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorHSVShift(color, maxChange0, maxChange1, maxChange2);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorHSLShift(color, maxChange0, maxChange1, maxChange2);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorHSYShift(color, maxChange0, maxChange1, maxChange2);
			}
			else if (hcvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorHCVShift(color, maxChange0, maxChange1, maxChange2);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorHCLShift(color, maxChange0, maxChange1, maxChange2);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorHCYShift(color, maxChange0, maxChange1, maxChange2);
			}
			else
			{
				throw new System.NotImplementedException();
			}

			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = generator(selectedColor);
				}
			}
		}

		public void GenerateLighten()
		{
			System.Func<Color, Color> generator;

			float maxProportion = relativeColorComponentSlider.value;

			if (rgbToggle.isOn)
			{
				generator = (Color color) => _random.ColorIntensitySpread(color, 0f, maxProportion);
			}
			else if (cmyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorIntensitySpread((ColorCMY)color, 0f, maxProportion);
			}
			else if (cmykToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorKeySpread((ColorCMYK)color, -maxProportion, 0f);
			}
			else if (hsvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorValueSpread((ColorHSV)color, 0f, maxProportion);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorLightnessSpread((ColorHSL)color, 0f, maxProportion);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorLumaSpread((ColorHSY)color, 0f, maxProportion);
			}
			else if (hcvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorValueSpread((ColorHCV)color, 0f, maxProportion);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorLightnessSpread((ColorHCL)color, 0f, maxProportion);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorLumaSpread((ColorHCY)color, 0f, maxProportion);
			}
			else
			{
				throw new System.NotImplementedException();
			}

			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = generator(selectedColor);
				}
			}
		}

		public void GenerateDarken()
		{
			System.Func<Color, Color> generator;

			float maxProportion = relativeColorComponentSlider.value;

			if (rgbToggle.isOn)
			{
				generator = (Color color) => _random.ColorIntensitySpread(color, -maxProportion, 0f);
			}
			else if (cmyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorIntensitySpread((ColorCMY)color, -maxProportion, 0f);
			}
			else if (cmykToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorKeySpread((ColorCMYK)color, 0f, maxProportion);
			}
			else if (hsvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorValueSpread((ColorHSV)color, -maxProportion, 0f);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorLightnessSpread((ColorHSL)color, -maxProportion, 0f);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorLumaSpread((ColorHSY)color, -maxProportion, 0f);
			}
			else if (hcvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorValueSpread((ColorHCV)color, -maxProportion, 0f);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorLightnessSpread((ColorHCL)color, -maxProportion, 0f);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorLumaSpread((ColorHCY)color, -maxProportion, 0f);
			}
			else
			{
				throw new System.NotImplementedException();
			}

			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = generator(selectedColor);
				}
			}
		}

		public void GenerateSaturate()
		{
			System.Func<Color, Color> generator;

			float maxProportion = relativeColorComponentSlider.value;

			if (hsvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorSaturationSpread((ColorHSV)color, 0f, maxProportion);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorSaturationSpread((ColorHSL)color, 0f, maxProportion);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorSaturationSpread((ColorHSY)color, 0f, maxProportion);
			}
			else
			{
				throw new System.NotImplementedException();
			}

			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = generator(selectedColor);
				}
			}
		}

		public void GenerateDesaturate()
		{
			System.Func<Color, Color> generator;

			float maxProportion = relativeColorComponentSlider.value;

			if (hsvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorSaturationSpread((ColorHSV)color, -maxProportion, 0f);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorSaturationSpread((ColorHSL)color, -maxProportion, 0f);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorSaturationSpread((ColorHSY)color, -maxProportion, 0f);
			}
			else
			{
				throw new System.NotImplementedException();
			}

			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = generator(selectedColor);
				}
			}
		}

		public void GenerateHarden()
		{
			System.Func<Color, Color> generator;

			float maxProportion = relativeColorComponentSlider.value;

			if (hcvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorChromaSpread((ColorHCV)color, 0f, maxProportion);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorChromaSpread((ColorHCL)color, 0f, maxProportion);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorChromaSpread((ColorHCY)color, 0f, maxProportion);
			}
			else
			{
				throw new System.NotImplementedException();
			}

			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = generator(selectedColor);
				}
			}
		}

		public void GenerateSoften()
		{
			System.Func<Color, Color> generator;

			float maxProportion = relativeColorComponentSlider.value;

			if (hcvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorChromaSpread((ColorHCV)color, -maxProportion, 0f);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorChromaSpread((ColorHCL)color, -maxProportion, 0f);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ColorChromaSpread((ColorHCY)color, -maxProportion, 0f);
			}
			else
			{
				throw new System.NotImplementedException();
			}

			Color selectedColor = _selectedColorButton.colorImage.color;

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				if (colorButton != _selectedColorButton)
				{
					colorButton.colorImage.color = generator(selectedColor);
				}
			}
		}
	}
}
