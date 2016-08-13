/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Experilous.Examples.MakeItRandom
{
	public class UnitComponentSlider : MonoBehaviour
	{
		public Text textField;

		private Slider _slider;

		public float value
		{
			get
			{
				if (_slider.minValue == 0f)
				{
					if (_slider.maxValue == 1f)
					{
						return _slider.value * _slider.value;
					}
					else if (_slider.maxValue == 0.5f)
					{
						return _slider.value * _slider.value * 2f;
					}
					else
					{
						float normalized = _slider.value / _slider.maxValue;
						return normalized * normalized * _slider.maxValue;
					}
				}
				else
				{
					float range = _slider.maxValue - _slider.minValue;
					float normalized = (_slider.value - _slider.minValue) / range;
					return normalized * normalized * range + _slider.minValue;
				}
			}
		}

		protected void Start()
		{
			_slider = GetComponent<Slider>();
			OnValueChanged();
		}

		public void OnValueChanged()
		{
			textField.text = value.ToString("F3");
		}
	}
}
