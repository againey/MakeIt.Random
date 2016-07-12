/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Experilous.Randomization;

namespace Experilous.Examples.Randomization
{
	public class StringsController : MonoBehaviour
	{
		public Slider stringLengthSlider;
		public Text stringLengthText;

		public Slider quantityOfStringsSlider;
		public Text quantityOfStringsText;

		public Toggle upperCaseToggle;
		public Toggle lowerCaseToggle;

		public Toggle includeSpacesToggle;

		public Slider spaceFrequencySlider;
		public Text spaceFrequencyText;

		public Color sliderBackgroundNormalColor = new Color(0.75f, 0.75f, 0.75f);
		public Color sliderBackgroundDisabledColor = new Color(0.5f, 0.5f, 0.5f);

		public InputField stringsInputField;

		private IRandomEngine _random;
		private readonly System.Text.StringBuilder _sb = new System.Text.StringBuilder();

		protected void Start()
		{
			_random = XorShift128Plus.Create();

			OnStringLengthChanged();
			OnQuantityOfStrings();
			OnIncludeSpacesChanged();
			OnSpaceFrequencyChanged();
		}

		private int stringLength { get { return (int)stringLengthSlider.value; } }
		private int quantityOfStrings { get { return (int)quantityOfStringsSlider.value; } }
		private bool hasCasing { get { return upperCaseToggle.isOn || lowerCaseToggle.isOn; } }
		private RandomString.Casing casing { get { return upperCaseToggle.isOn ? RandomString.Casing.Upper : RandomString.Casing.Lower; } }
		private float spaceFrequency { get { return spaceFrequencySlider.value; } }

		public void OnStringLengthChanged()
		{
			stringLengthText.text = stringLength.ToString();
		}

		public void OnQuantityOfStrings()
		{
			quantityOfStringsText.text = quantityOfStrings.ToString();
		}

		private void EnableSlider(Slider slider)
		{
			if (slider.interactable == false)
			{
				slider.interactable = true;
				slider.fillRect.GetComponent<Image>().color = slider.colors.normalColor;
				slider.transform.Find("Background").GetComponent<Image>().color = sliderBackgroundNormalColor;
			}
		}

		private void DisableSlider(Slider slider)
		{
			if (slider.interactable == true)
			{
				slider.interactable = false;
				slider.fillRect.GetComponent<Image>().color = slider.colors.disabledColor;
				slider.transform.Find("Background").GetComponent<Image>().color = sliderBackgroundDisabledColor;
			}
		}

		public void OnIncludeSpacesChanged()
		{
			if (includeSpacesToggle.isOn)
			{
				EnableSlider(spaceFrequencySlider);
			}
			else
			{
				DisableSlider(spaceFrequencySlider);
			}
		}

		public void OnSpaceFrequencyChanged()
		{
			spaceFrequencyText.text = spaceFrequency.ToString("P1").Replace(" %", "%");
		}

		private void GenerateStrings(System.Func<string> generator)
		{
			_sb.Length = 0;
			for (int i = 0; i < quantityOfStrings; ++i)
			{
				_sb.AppendLine(generator());
			}
			stringsInputField.text = _sb.ToString();
		}

		public void OnGenerateBinaryStrings()
		{
			GenerateStrings(() => RandomString.Binary(stringLength, _random));
		}

		public void OnGenerateOctalStrings()
		{
			GenerateStrings(() => RandomString.Octal(stringLength, _random));
		}

		public void OnGenerateDecimalStrings()
		{
			GenerateStrings(() => RandomString.Decimal(stringLength, _random));
		}

		public void OnGenerateHexadecimalStrings()
		{
			if (hasCasing)
			{
				GenerateStrings(() => RandomString.Hexadecimal(stringLength, casing, _random));
			}
			else
			{
				GenerateStrings(() => RandomString.Hexadecimal(stringLength, RandomString.Casing.Upper, _random));
			}
		}

		public void OnGenerateBase64Strings()
		{
			GenerateStrings(() => RandomString.Base64(stringLength, _random));
		}

		public void OnGenerateAlphaNumericStrings()
		{
			if (includeSpacesToggle.isOn)
			{
				if (hasCasing)
				{
					GenerateStrings(() => RandomString.AlphaNumericWithSpaces(stringLength, spaceFrequency, casing, _random));
				}
				else
				{
					GenerateStrings(() => RandomString.AlphaNumericWithSpaces(stringLength, spaceFrequency, _random));
				}
			}
			else
			{
				if (hasCasing)
				{
					GenerateStrings(() => RandomString.AlphaNumeric(stringLength, casing, _random));
				}
				else
				{
					GenerateStrings(() => RandomString.AlphaNumeric(stringLength, _random));
				}
			}
		}

		public void OnGenerateAlphabeticStrings()
		{
			if (includeSpacesToggle.isOn)
			{
				if (hasCasing)
				{
					GenerateStrings(() => RandomString.AlphabeticWithSpaces(stringLength, spaceFrequency, casing, _random));
				}
				else
				{
					GenerateStrings(() => RandomString.AlphabeticWithSpaces(stringLength, spaceFrequency, _random));
				}
			}
			else
			{
				if (hasCasing)
				{
					GenerateStrings(() => RandomString.Alphabetic(stringLength, casing, _random));
				}
				else
				{
					GenerateStrings(() => RandomString.Alphabetic(stringLength, _random));
				}
			}
		}

		public void OnGenerateIdentifiers()
		{
			if (includeSpacesToggle.isOn)
			{
				if (hasCasing)
				{
					GenerateStrings(() => RandomString.IdentifierWithUnderscores(stringLength, spaceFrequency, casing, _random));
				}
				else
				{
					GenerateStrings(() => RandomString.IdentifierWithUnderscores(stringLength, spaceFrequency, _random));
				}
			}
			else
			{
				if (hasCasing)
				{
					GenerateStrings(() => RandomString.Identifier(stringLength, casing, _random));
				}
				else
				{
					GenerateStrings(() => RandomString.Identifier(stringLength, _random));
				}
			}
		}
	}
}
