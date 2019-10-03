//09-19: Use to switch the prefab under the player gameobject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FancyCustomization = FancyScrollView.CustomizationMenu.CustomizationManager;

//README Make sure that your customization slots are in the same order (first at top) as the customization panels in the array. 
public class CustomizationController : MonoBehaviour
{
	[SerializeField] private GameObject[] panels;
	[SerializeField] private Transform currencyTextBox;
	//Prefab for Hat and Body
	[SerializeField] private GameObject hatSlot;
	[SerializeField] private GameObject bodySlot;
	[SerializeField] private List<GameObject> slots;

	private List<int>[] purchases;
	private GameObject player;
	//private int currentIndex;
	private int panelIndex = 0;
	private int[] panelIndices; //the currently selected indices of each panel. 
	private CustomizationSwitcher customSwitcher;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		panelIndices = new int[panels.Length];

		InitializePurchaseList();
		InitializeObjectSlot();
		UpdateCurrencyText();
	}

	private void InitializePurchaseList()
	{
		purchases = new List<int>[panels.Length];
		for (int i = 0; i < purchases.Length; i++)
		{
			purchases[i] = new List<int>();
		}
		//TODO: Populate store purchases with prior purchases from Player Profile.
	}

	//README Make sure that your customization slots are in the same order (first at top) as the customization panels in the array. 
	private void InitializeObjectSlot()
	{
		int i = 0;
		slots = new List<GameObject>();
		foreach (Transform child in player.transform)
		{
			if (child.GetComponent<CustomizationSwitcher>())
			{
				slots.Add(child.gameObject);
			}
			i++;
		}
	}

	private void UpdateCurrencyText()
	{
		currencyTextBox.GetComponent<Text>().text =
			player.GetComponent<CustomizationSaving>().currency.ToString();
		// TODO Player Character needs to keep track of how much currency the player has, 
		// CustomizationSaving should only occur when changes to the player's settings are made. 
		//currencyText.text = player.GetComponent<PlayerCustomization>().currency.ToString();
	}

	// TODO Refactor this so it doesn't just apply to Hats.
	// Snap the option menu, and switch the customized entity automatically.
	public void SwitchCustomization(int index)
	{
		customSwitcher = hatSlot.GetComponent<CustomizationSwitcher>();
		customSwitcher.SwitchCustomization(index);
		panelIndices[panelIndex] = index;
	}

	private bool CheckInventory(int index)
	{
		foreach (int itemNo in purchases[panelIndex])
		{
			if (itemNo == index)
			{
				return false;
			}
		}
		return true;
	}

	public void Next()
	{
		//Set all panels to false.
		foreach (GameObject panel in panels)
		{
			panel.SetActive(false);
		}
		panelIndex++;
		panelIndex %= panels.Length; //Don't go over the maximum.
		panels[panelIndex].SetActive(true);
	}

	public void Previous()
	{
		foreach (GameObject panel in panels)
		{
			panel.SetActive(false);
		}
		panelIndex--;
		if (panelIndex < 0) panelIndex = panels.Length - 1; //Don't go under the minimum.
		panels[panelIndex].SetActive(true);
	}

	public void Buy()
	{
		if (CheckInventory(panelIndices[panelIndex])) //If it's not in the index, it returns true.
		{
			int p_currency = player.GetComponent<CustomizationSaving>().currency;
			int price = GetPrice(panelIndices[panelIndex]);
			if (p_currency - price < 0)
			{
				Debug.Log("You need " + (price - p_currency) + " more coins to purchase.");
			}
			//If player has enough money, they can buy. 
			else
			{
				//Update the currency.
				p_currency -= price;
				player.GetComponent<CustomizationSaving>().currency = p_currency;
				UpdateCurrencyText();

				purchases[panelIndex].Add(panelIndices[panelIndex]);
				Debug.Log("You bought a NO." + panelIndices[panelIndex] + " Equipment");
			}
		}
		else
		{
			Debug.Log("You have already bought the NO." + panelIndices[panelIndex] + " Equipment");
		}
	}

	private int GetPrice(int index)
	{
		int ret = panels[panelIndex].GetComponent<FancyCustomization>().GetNeededCoins(index);
		return ret;
	}

	// Not same
	// need to change
	public void Equip()
	{
		if (CheckInventory(panelIndices[panelIndex])) // if it is in the index, it returns true.
		{
			purchases[panelIndex].Add(panelIndices[panelIndex]);
			Debug.Log("You Equip a NO." + panelIndices[panelIndex] + " Equipment");
		}
		else
		{
			Debug.Log("You have Equiped the NO." + panelIndices[panelIndex] + " Equipment");
		}
	}
}
