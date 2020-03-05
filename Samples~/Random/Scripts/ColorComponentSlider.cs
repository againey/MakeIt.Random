/******************************************************************************\
* Copyright Andy Gainey                                                        *
*                                                                              *
* Licensed under the Apache License, Version 2.0 (the "License");              *
* you may not use this file except in compliance with the License.             *
* You may obtain a copy of the License at                                      *
*                                                                              *
*     http://www.apache.org/licenses/LICENSE-2.0                               *
*                                                                              *
* Unless required by applicable law or agreed to in writing, software          *
* distributed under the License is distributed on an "AS IS" BASIS,            *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.     *
* See the License for the specific language governing permissions and          *
* limitations under the License.                                               *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace MakeIt.Random.Samples
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
