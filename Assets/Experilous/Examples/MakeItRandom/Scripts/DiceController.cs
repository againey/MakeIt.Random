/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Experilous.MakeItRandom;

namespace Experilous.Examples.MakeItRandom
{
	public class DiceController : MonoBehaviour
	{
		public DiePanel diePanelPrefab;

		public Slider quantityOfDiceSlider;
		public Text quantityOfDiceText;

		public Slider sidesPerDieSlider;
		public Text sidesPerDieText;

		public Toggle keepHighestToggle;
		public Toggle keepLowestToggle;

		public Slider quantityToKeepSlider;
		public Text quantityToKeepText;

		public Toggle dropHighestToggle;
		public Toggle dropLowestToggle;

		public Slider quantityToDropSlider;
		public Text quantityToDropText;

		public Color sliderBackgroundNormalColor = new Color(0.75f, 0.75f, 0.75f);
		public Color sliderBackgroundDisabledColor = new Color(0.5f, 0.5f, 0.5f);

		public RectTransform diceGrid;

		public Color normalDieColor = new Color(1f, 1f, 1f);
		public Color discardedDieColor = new Color(1f, 0.5f, 0.4f);

		private IRandom _random;

		private readonly List<int> _dice = new List<int>();
		private readonly List<int> _discardedDice = new List<int>();

		private static readonly int[] _sidesPerDieOptions = new int[] { 4, 6, 8, 10, 12, 20, };

		protected void Start()
		{
			_random = MIRandom.CreateStandard();

			OnQuantityOfDiceChanged();
			OnSidesPerDieChanged();
			OnQuantityToKeepChanged();
			OnQuantityToDropChanged();
			OnKeepDropToggleChanged();
		}

		private int quantityOfDice { get { return Mathf.FloorToInt(quantityOfDiceSlider.value * quantityOfDiceSlider.value * 49f) + 1; } }
		private int sidesPerDie { get { return _sidesPerDieOptions[(int)sidesPerDieSlider.value]; } }
		private int quantityToKeep { get { return (int)quantityToKeepSlider.value; } }
		private int quantityToDrop { get { return (int)quantityToDropSlider.value; } }

		public void OnQuantityOfDiceChanged()
		{
			quantityOfDiceText.text = quantityOfDice.ToString();

			if (quantityOfDice == 1)
			{
				keepHighestToggle.interactable = false;
				keepLowestToggle.interactable = false;
				dropHighestToggle.interactable = false;
				dropLowestToggle.interactable = false;
			}
			else
			{
				keepHighestToggle.interactable = true;
				keepLowestToggle.interactable = true;
				dropHighestToggle.interactable = true;
				dropLowestToggle.interactable = true;
			}

			quantityToKeepSlider.maxValue = Mathf.Max(1, quantityOfDice - 1);
			quantityToDropSlider.maxValue = Mathf.Max(1, quantityOfDice - 1);
		}

		public void OnSidesPerDieChanged()
		{
			sidesPerDieText.text = sidesPerDie.ToString();
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

		public void OnKeepDropToggleChanged()
		{
			if (keepHighestToggle.isOn && keepHighestToggle.interactable)
			{
				EnableSlider(quantityToKeepSlider);
				DisableSlider(quantityToDropSlider);
			}
			else if (keepLowestToggle.isOn && keepLowestToggle.interactable)
			{
				EnableSlider(quantityToKeepSlider);
				DisableSlider(quantityToDropSlider);
			}
			else if (dropHighestToggle.isOn && dropHighestToggle.interactable)
			{
				DisableSlider(quantityToKeepSlider);
				EnableSlider(quantityToDropSlider);
			}
			else if (dropLowestToggle.isOn && dropLowestToggle.interactable)
			{
				DisableSlider(quantityToKeepSlider);
				EnableSlider(quantityToDropSlider);
			}
			else
			{
				DisableSlider(quantityToKeepSlider);
				DisableSlider(quantityToDropSlider);
			}
		}

		public void OnQuantityToKeepChanged()
		{
			quantityToKeepText.text = quantityToKeep.ToString();
		}

		public void OnQuantityToDropChanged()
		{
			quantityToDropText.text = quantityToDrop.ToString();
		}

		public void OnRollDice()
		{
			if (keepHighestToggle.isOn && keepHighestToggle.interactable)
			{
				_random.RollDiceKeepHighest(quantityOfDice, sidesPerDie, quantityToKeep, _dice, _discardedDice);
			}
			else if (keepLowestToggle.isOn && keepLowestToggle.interactable)
			{
				_random.RollDiceKeepLowest(quantityOfDice, sidesPerDie, quantityToKeep, _dice, _discardedDice);
			}
			else if (dropHighestToggle.isOn && dropHighestToggle.interactable)
			{
				_random.RollDiceDropHighest(quantityOfDice, sidesPerDie, quantityToDrop, _dice, _discardedDice);
			}
			else if (dropLowestToggle.isOn && dropLowestToggle.interactable)
			{
				_random.RollDiceDropLowest(quantityOfDice, sidesPerDie, quantityToDrop, _dice, _discardedDice);
			}
			else
			{
				_discardedDice.Clear();
				_random.RollDice(quantityOfDice, sidesPerDie, _dice);
			}

			int dicePanelCount = diceGrid.childCount;
			for (int i = 0; i < dicePanelCount; ++i)
			{
				Destroy(diceGrid.GetChild(i).gameObject);
			}

			foreach (int die in _dice)
			{
				DiePanel diePanel = Instantiate(diePanelPrefab);
				diePanel.dieLabel.text = die.ToString();
				diePanel.diePanelImage.color = normalDieColor;
				diePanel.transform.SetParent(diceGrid, false);
			}

			_random.Shuffle(_discardedDice);

			foreach (int die in _discardedDice)
			{
				DiePanel diePanel = Instantiate(diePanelPrefab);
				diePanel.dieLabel.text = die.ToString();
				diePanel.diePanelImage.color = discardedDieColor;
				diePanel.transform.SetParent(diceGrid, false);
			}
		}
	}
}
