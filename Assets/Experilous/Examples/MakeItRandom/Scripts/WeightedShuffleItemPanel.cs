/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Experilous.Examples.Randomization
{
	public class WeightedShuffleItemPanel : MonoBehaviour
	{
		public Color normalColor;
		public Color selectedColor;

		public Image backgroundImage;

		public LayoutElement valueEmptyLayout;
		public LayoutElement valueFullLayout;
		public Text valueText;

		public LayoutElement weightEmptyLayout;
		public LayoutElement weightFullLayout;
		public Text weightText;

		private bool _selected;
		public bool selected
		{
			get
			{
				return _selected;
			}
			set
			{
				if (_selected != value)
				{
					_selected = value;
					backgroundImage.color = _selected ? selectedColor : normalColor;
				}
			}
		}

		public void SetValue(int value, int maxValue)
		{
			if (maxValue > 0)
			{
				valueEmptyLayout.flexibleHeight = maxValue - value;
				valueFullLayout.flexibleHeight = value;
			}
			else
			{
				valueEmptyLayout.flexibleHeight = 0;
				valueFullLayout.flexibleHeight = 1;
			}
			valueText.text = value.ToString();
		}

		public void SetWeight(int weight, int maxWeight)
		{
			if (maxWeight > 0)
			{
				weightEmptyLayout.flexibleHeight = maxWeight - weight;
				weightFullLayout.flexibleHeight = weight;
			}
			else
			{
				weightEmptyLayout.flexibleHeight = 0;
				weightFullLayout.flexibleHeight = 1;
			}
			weightText.text = weight.ToString();
		}
	}
}
