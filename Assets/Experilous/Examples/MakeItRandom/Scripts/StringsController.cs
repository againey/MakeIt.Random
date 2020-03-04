/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using Experilous.MakeItRandom;

namespace Experilous.Examples.MakeItRandom
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

		private IRandom _random;
		private readonly System.Text.StringBuilder _sb = new System.Text.StringBuilder();

		protected void Start()
		{
			_random = MIRandom.CreateStandard();

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
			GenerateStrings(() => _random.BinaryString(stringLength));
		}

		public void OnGenerateOctalStrings()
		{
			GenerateStrings(() => _random.OctalString(stringLength));
		}

		public void OnGenerateDecimalStrings()
		{
			GenerateStrings(() => _random.DecimalString(stringLength));
		}

		public void OnGenerateHexadecimalStrings()
		{
			if (hasCasing)
			{
				GenerateStrings(() => _random.HexadecimalString(stringLength, casing));
			}
			else
			{
				GenerateStrings(() => _random.HexadecimalString(stringLength, RandomString.Casing.Upper));
			}
		}

		public void OnGenerateBase64Strings()
		{
			GenerateStrings(() => _random.Base64String(stringLength));
		}

		public void OnGenerateAlphaNumericStrings()
		{
			if (includeSpacesToggle.isOn)
			{
				if (hasCasing)
				{
					GenerateStrings(() => _random.AlphaNumericString(stringLength, casing, ' ', spaceFrequency));
				}
				else
				{
					GenerateStrings(() => _random.AlphaNumericString(stringLength, ' ', spaceFrequency));
				}
			}
			else
			{
				if (hasCasing)
				{
					GenerateStrings(() => _random.AlphaNumericString(stringLength, casing));
				}
				else
				{
					GenerateStrings(() => _random.AlphaNumericString(stringLength));
				}
			}
		}

		public void OnGenerateAlphabeticStrings()
		{
			if (includeSpacesToggle.isOn)
			{
				if (hasCasing)
				{
					GenerateStrings(() => _random.AlphabeticString(stringLength, casing, ' ', spaceFrequency));
				}
				else
				{
					GenerateStrings(() => _random.AlphabeticString(stringLength, ' ', spaceFrequency));
				}
			}
			else
			{
				if (hasCasing)
				{
					GenerateStrings(() => _random.AlphabeticString(stringLength, casing));
				}
				else
				{
					GenerateStrings(() => _random.AlphabeticString(stringLength));
				}
			}
		}

		public void OnGenerateIdentifiers()
		{
			if (includeSpacesToggle.isOn)
			{
				if (hasCasing)
				{
					GenerateStrings(() => _random.Identifier(stringLength, casing, spaceFrequency));
				}
				else
				{
					GenerateStrings(() => _random.Identifier(stringLength, spaceFrequency));
				}
			}
			else
			{
				if (hasCasing)
				{
					GenerateStrings(() => _random.Identifier(stringLength, casing));
				}
				else
				{
					GenerateStrings(() => _random.Identifier(stringLength));
				}
			}
		}
	}
}
