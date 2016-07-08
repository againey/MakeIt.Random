/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Experilous.Examples.Randomization
{
	public class UnitComponentSlider : MonoBehaviour
	{
		public Text textField;

		private Slider _slider;

		public float value { get { return _slider.value * _slider.value; } }

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