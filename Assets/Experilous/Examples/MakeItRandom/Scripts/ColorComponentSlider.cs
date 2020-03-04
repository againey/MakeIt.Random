/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Experilous.Examples.MakeItRandom
{
	public class ColorComponentSlider : MonoBehaviour
	{
		public Text labelField;
		public Text valueField;
		public Slider slider;
		public bool isPercentage;

		public float value
		{
			get
			{
				if (slider.minValue == 0f)
				{
					if (slider.maxValue == 1f)
					{
						return slider.value * slider.value;
					}
					else if (slider.maxValue == 0.5f)
					{
						return slider.value * slider.value * 2f;
					}
					else
					{
						float normalized = slider.value / slider.maxValue;
						return normalized * normalized * slider.maxValue;
					}
				}
				else
				{
					float range = slider.maxValue - slider.minValue;
					float normalized = (slider.value - slider.minValue) / range;
					return normalized * normalized * range + slider.minValue;
				}
			}
		}

		protected void Start()
		{
			OnValueChanged();
		}

		public void OnValueChanged()
		{
			if (isPercentage)
			{
				valueField.text = Mathf.RoundToInt(value * 100).ToString() + "%";
			}
			else
			{
				valueField.text = value.ToString("F3");
			}
		}
	}
}
