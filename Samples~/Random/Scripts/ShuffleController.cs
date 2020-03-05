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
using System.Collections;
using System.Collections.Generic;
using MakeIt.Random;

namespace MakeIt.Random.Samples
{
	public class ShuffleController : MonoBehaviour
	{
		public WeightedShuffleItemPanel weightedShuffleItemPrefab;

		public Slider quantityOfItemsSlider;
		public Text quantityOfItemsText;

		public Slider selectionFrequencySlider;

		public RectTransform itemsPanel;

		public GameObject selectRandomButton;
		public GameObject stopSelectingRandomButton;
		public GameObject selectWeightedRandomButton;
		public GameObject stopSelectingWeightedRandomButton;

		private IRandom _random;

		private readonly List<WeightedShuffleItemPanel> _itemPanels = new List<WeightedShuffleItemPanel>();
		private WeightedShuffleItemPanel _selectedItemPanel;
		private Coroutine _selectionCoroutine = null;

		private struct WeightedValue
		{
			public int value;
			public float weight;

			public WeightedValue(int value, float weight)
			{
				this.value = value;
				this.weight = weight;
			}
		}

		private readonly List<WeightedValue> _items = new List<WeightedValue>();

		protected void Start()
		{
			_random = MIRandom.CreateStandard();

			OnQuantityOfItemsChanged();
			OnCreateConstantWeight();
		}

		private int quantityOfItems { get { return (int)quantityOfItemsSlider.value; } }

		public void OnQuantityOfItemsChanged()
		{
			quantityOfItemsText.text = quantityOfItems.ToString();
		}

		private void CreateOrDestroyAndSetItems(System.Func<int, int> valueGenerator, System.Func<int, int, float> weightGenerator)
		{
			if (_selectedItemPanel != null)
			{
				_selectedItemPanel.selected = false;
			}

			while (_itemPanels.Count < quantityOfItems)
			{
				WeightedShuffleItemPanel itemPanel = Instantiate(weightedShuffleItemPrefab);
				itemPanel.transform.SetParent(itemsPanel, false);
				_itemPanels.Add(itemPanel);
			}

			while (_itemPanels.Count > quantityOfItems)
			{
				Destroy(_itemPanels[_itemPanels.Count - 1].gameObject);
				_itemPanels.RemoveAt(_itemPanels.Count - 1);
			}

			_items.Clear();

			for (int i = 0; i < _itemPanels.Count; ++i)
			{
				int value = valueGenerator(i);
				float weight = weightGenerator(i, value);

				WeightedShuffleItemPanel itemPanel = _itemPanels[i];
				itemPanel.SetValue(value, _itemPanels.Count);
				itemPanel.SetWeight(Mathf.RoundToInt(weight), 99);

				_items.Add(new WeightedValue(value, weight));
			}
		}

		public void OnCreateConstantWeight()
		{
			CreateOrDestroyAndSetItems(
				(int i) => i + 1,
				(int i, int value) => 50f);
		}

		public void OnCreateLinearWeight()
		{
			CreateOrDestroyAndSetItems(
				(int i) => i + 1,
				(int i, int value) => value * 99f / _itemPanels.Count);
		}

		public void OnCreateLogarithmicWeight()
		{
			float min = 2.7182818f;
			float max = 54.598150f;

			CreateOrDestroyAndSetItems(
				(int i) => i + 1,
				(int i, int value) => (Mathf.Log((float)i / (_itemPanels.Count - 1) * (max - min) + min) - 1f) * 32.6667f + 1f);
		}

		public void OnCreateExponentialWeight()
		{
			float range = 4.59512f;
			CreateOrDestroyAndSetItems(
				(int i) => i + 1,
				(int i, int value) => Mathf.Exp((float)i / (_itemPanels.Count - 1) * range));
		}

		private float Gaussian(float x, float variance)
		{
			return Mathf.Exp(x * x / (-2f * variance)) / Mathf.Sqrt(2f * variance * Mathf.PI);
		}

		public void OnCreateGaussianWeight()
		{
			CreateOrDestroyAndSetItems(
				(int i) => i + 1,
				(int i, int value) => Gaussian(((float)i / (_itemPanels.Count - 1) - 0.5f) * 6f, 1f) * 247.5f);
		}

		public void OnShuffle()
		{
			if (_selectedItemPanel != null)
			{
				_selectedItemPanel.selected = false;
			}

			_random.Shuffle(_items);

			for (int i = 0; i < _itemPanels.Count; ++i)
			{
				WeightedShuffleItemPanel itemPanel = _itemPanels[i];
				itemPanel.SetValue(_items[i].value, _itemPanels.Count);
				itemPanel.SetWeight(Mathf.RoundToInt(_items[i].weight), 99);
			}
		}

		public void StopSelectingRandomItems()
		{
			if (_selectionCoroutine != null)
			{
				StopCoroutine(_selectionCoroutine);
				_selectionCoroutine = null;

				selectRandomButton.SetActive(true);
				selectWeightedRandomButton.SetActive(true);
				stopSelectingRandomButton.SetActive(false);
				stopSelectingWeightedRandomButton.SetActive(false);
			}
		}

		public void OnSelectRandomItems()
		{
			StopSelectingRandomItems();

			_selectionCoroutine = StartCoroutine(SelectRandomItem());
			selectRandomButton.SetActive(false);
			stopSelectingRandomButton.SetActive(true);
		}

		public void OnSelectWeightedRandomItems()
		{
			StopSelectingRandomItems();

			_selectionCoroutine = StartCoroutine(SelectWeightedRandomItem());
			selectWeightedRandomButton.SetActive(false);
			stopSelectingWeightedRandomButton.SetActive(true);
		}

		private YieldInstruction Delay()
		{
			float delay = 1f / Mathf.Pow(120f, selectionFrequencySlider.value);
			if (delay > Time.smoothDeltaTime)
			{
				return new WaitForSeconds(delay);
			}
			else
			{
				return null;
			}
		}

		private IEnumerator SelectRandomItem()
		{
			while (true)
			{
				WeightedShuffleItemPanel itemPanel = _itemPanels.RandomElement(_random);
				if (itemPanel != _selectedItemPanel)
				{
					if (_selectedItemPanel != null)
					{
						_selectedItemPanel.selected = false;
					}
					_selectedItemPanel = itemPanel;
					_selectedItemPanel.selected = true;
				}

				yield return Delay();
			}
		}

		private IEnumerator SelectWeightedRandomItem()
		{
			while (true)
			{
				int index = _random.WeightedIndex(_items.Count, (int i) => _items[i].weight);
				WeightedShuffleItemPanel itemPanel = _itemPanels[index];
				if (itemPanel != _selectedItemPanel)
				{
					if (_selectedItemPanel != null)
					{
						_selectedItemPanel.selected = false;
					}
					_selectedItemPanel = itemPanel;
					_selectedItemPanel.selected = true;
				}

				yield return Delay();
			}
		}
	}
}
