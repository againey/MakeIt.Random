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

		public ColorComponentSlider firstAbsoluteColorComponentSlider;
		public ColorComponentSlider secondAbsoluteColorComponentSlider;
		public ColorComponentSlider thirdAbsoluteColorComponentSlider;
		public ColorComponentSlider fourthAbsoluteColorComponentSlider;

		public ColorComponentSlider relativeColorComponentSlider;

		public RectTransform generateRedsPanel;
		public RectTransform generateGreensPanel;
		public RectTransform generateBluesPanel;

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
				ColorCMY cmy = ColorCMY.FromRGB(rgb);
				UpdateSelectedColorFields(cmy.c, cmy.m, cmy.y);
			}
			else if (cmykToggle.isOn)
			{
				ColorCMYK cmyk = ColorCMYK.FromRGB(rgb);
				UpdateSelectedColorFields(cmyk.c, cmyk.m, cmyk.y, cmyk.k);
			}
			else if (hsvToggle.isOn)
			{
				ColorHSV hsv = ColorHSV.FromRGB(rgb);
				UpdateSelectedColorFields(hsv.h, hsv.s, hsv.v);
			}
			else if (hslToggle.isOn)
			{
				ColorHSL hsl = ColorHSL.FromRGB(rgb);
				UpdateSelectedColorFields(hsl.h, hsl.s, hsl.l);
			}
			else if (hsyToggle.isOn)
			{
				ColorHSY hsy = ColorHSY.FromRGB(rgb);
				UpdateSelectedColorFields(hsy.h, hsy.s, hsy.y);
			}
			else if (hcvToggle.isOn)
			{
				ColorHCV hcv = ColorHCV.FromRGB(rgb);
				UpdateSelectedColorFields(hcv.h, hcv.c, hcv.v);
			}
			else if (hclToggle.isOn)
			{
				ColorHCL hcl = ColorHCL.FromRGB(rgb);
				UpdateSelectedColorFields(hcl.h, hcl.c, hcl.l);
			}
			else if (hcyToggle.isOn)
			{
				ColorHCY hcy = ColorHCY.FromRGB(rgb);
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
				if (toggle == rgbToggle)
				{
					generateRedsPanel.gameObject.SetActive(true);
					generateGreensPanel.gameObject.SetActive(true);
					generateBluesPanel.gameObject.SetActive(true);
				}
				else
				{
					generateRedsPanel.gameObject.SetActive(false);
					generateGreensPanel.gameObject.SetActive(false);
					generateBluesPanel.gameObject.SetActive(false);
				}

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
			System.Func<Color> generator;

			if (rgbToggle.isOn)
			{
				generator = () => _random.RGB();
			}
			else if (cmyToggle.isOn)
			{
				generator = () => (Color)_random.CMY();
			}
			else if (cmykToggle.isOn)
			{
				generator = () => (Color)_random.CMYK();
			}
			else if (hsvToggle.isOn)
			{
				generator = () => (Color)_random.HSV();
			}
			else if (hslToggle.isOn)
			{
				generator = () => (Color)_random.HSL();
			}
			else if (hsyToggle.isOn)
			{
				generator = () => (Color)_random.HSY();
			}
			else if (hcvToggle.isOn)
			{
				generator = () => (Color)_random.HCV();
			}
			else if (hclToggle.isOn)
			{
				generator = () => (Color)_random.HCL();
			}
			else if (hcyToggle.isOn)
			{
				generator = () => (Color)_random.HCY();
			}
			else
			{
				throw new System.NotImplementedException();
			}

			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = generator();
			}

			UpdateSelectedColor();
		}

		public void GenerateAnyRed()
		{
			foreach (ColorToggleButton colorButton in _colorButtons)
			{
				colorButton.colorImage.color = RandomColor.Red(_random);
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
				colorButton.colorImage.color = RandomColor.Green(_random);
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
				colorButton.colorImage.color = RandomColor.Blue(_random);
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

		public void GenerateChangeColors()
		{
			System.Func<Color, Color> generator;

			float maxChange0 = firstAbsoluteColorComponentSlider.gameObject.activeSelf ? firstAbsoluteColorComponentSlider.value : 0f;
			float maxChange1 = secondAbsoluteColorComponentSlider.gameObject.activeSelf ? secondAbsoluteColorComponentSlider.value : 0f;
			float maxChange2 = thirdAbsoluteColorComponentSlider.gameObject.activeSelf ? thirdAbsoluteColorComponentSlider.value : 0f;
			float maxChange3 = fourthAbsoluteColorComponentSlider.gameObject.activeSelf ? fourthAbsoluteColorComponentSlider.value : 0f;

			if (rgbToggle.isOn)
			{
				generator = (Color color) => _random.RGBShift(color, maxChange0, maxChange1, maxChange2);
			}
			else if (cmyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.CMYShift((ColorCMY)color, maxChange0, maxChange1, maxChange2);
			}
			else if (cmykToggle.isOn)
			{
				generator = (Color color) => (Color)_random.CMYKShift((ColorCMYK)color, maxChange0, maxChange1, maxChange2, maxChange3);
			}
			else if (hsvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.HSVShift((ColorHSV)color, maxChange0, maxChange1, maxChange2);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.HSLShift((ColorHSL)color, maxChange0, maxChange1, maxChange2);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.HSYShift((ColorHSY)color, maxChange0, maxChange1, maxChange2);
			}
			else if (hcvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.HCVShift((ColorHCV)color, maxChange0, maxChange1, maxChange2);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.HCLShift((ColorHCL)color, maxChange0, maxChange1, maxChange2);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.HCYShift((ColorHCY)color, maxChange0, maxChange1, maxChange2);
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
				generator = (Color color) => _random.IntensitySpread(color, 0f, maxProportion);
			}
			else if (cmyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.IntensitySpread((ColorCMY)color, 0f, maxProportion);
			}
			else if (cmykToggle.isOn)
			{
				generator = (Color color) => (Color)_random.KeySpread((ColorCMYK)color, -maxProportion, 0f);
			}
			else if (hsvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ValueSpread((ColorHSV)color, 0f, maxProportion);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.LightnessSpread((ColorHSL)color, 0f, maxProportion);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.LumaSpread((ColorHSY)color, 0f, maxProportion);
			}
			else if (hcvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ValueSpread((ColorHCV)color, 0f, maxProportion);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.LightnessSpread((ColorHCL)color, 0f, maxProportion);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.LumaSpread((ColorHCY)color, 0f, maxProportion);
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
				generator = (Color color) => _random.IntensitySpread(color, -maxProportion, 0f);
			}
			else if (cmyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.IntensitySpread((ColorCMY)color, -maxProportion, 0f);
			}
			else if (cmykToggle.isOn)
			{
				generator = (Color color) => (Color)_random.KeySpread((ColorCMYK)color, 0f, maxProportion);
			}
			else if (hsvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ValueSpread((ColorHSV)color, -maxProportion, 0f);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.LightnessSpread((ColorHSL)color, -maxProportion, 0f);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.LumaSpread((ColorHSY)color, -maxProportion, 0f);
			}
			else if (hcvToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ValueSpread((ColorHCV)color, -maxProportion, 0f);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.LightnessSpread((ColorHCL)color, -maxProportion, 0f);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.LumaSpread((ColorHCY)color, -maxProportion, 0f);
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
				generator = (Color color) => (Color)_random.SaturationSpread((ColorHSV)color, 0f, maxProportion);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.SaturationSpread((ColorHSL)color, 0f, maxProportion);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.SaturationSpread((ColorHSY)color, 0f, maxProportion);
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
				generator = (Color color) => (Color)_random.SaturationSpread((ColorHSV)color, -maxProportion, 0f);
			}
			else if (hslToggle.isOn)
			{
				generator = (Color color) => (Color)_random.SaturationSpread((ColorHSL)color, -maxProportion, 0f);
			}
			else if (hsyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.SaturationSpread((ColorHSY)color, -maxProportion, 0f);
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
				generator = (Color color) => (Color)_random.ChromaSpread((ColorHCV)color, 0f, maxProportion);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ChromaSpread((ColorHCL)color, 0f, maxProportion);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ChromaSpread((ColorHCY)color, 0f, maxProportion);
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
				generator = (Color color) => (Color)_random.ChromaSpread((ColorHCV)color, -maxProportion, 0f);
			}
			else if (hclToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ChromaSpread((ColorHCL)color, -maxProportion, 0f);
			}
			else if (hcyToggle.isOn)
			{
				generator = (Color color) => (Color)_random.ChromaSpread((ColorHCY)color, -maxProportion, 0f);
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
