//09-19: Use to switch the prefab under the player gameobject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FancyCustomization = FancyScrollView.CustomizationMenu.CustomizationManager; 

public class CustomizationController : MonoBehaviour
{

	public GameObject[] hatEntity;
	public GameObject[] bodyEntity;
	[SerializeField] private GameObject[] panels;
	[SerializeField] private Transform currencyTextBox;

	private List<int> intList = new List<int>();
	//Prefab for Hat and Body
	private GameObject hatSlot;
	private GameObject bodySlot;
	private GameObject player;
	//private int currentIndex;
	private int panelIndex = 0;
	private int[] panelIndices; //the currently selected indices of each panel. One panel might have the third option selected, anotehr the first, etc.
	/*
	 * panelIndices[panelIndex] is the currently selected panel's... currently selected option. 
	*/

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		InitialObjectSlot();
		UpdateCurrencyText();
		panelIndices = new int[panels.Length];
	}

	private void InitialObjectSlot()
	{
		foreach (Transform child in player.transform)
		{
			if (child.name == "Hat")
			{
				hatSlot = child.gameObject;
			}
			if (child.name == "Body")
			{
				bodySlot = child.gameObject;
			}
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
		CustomizationSwitcher hatSwitcher = hatSlot.GetComponent<CustomizationSwitcher>();
		hatSwitcher.SwitchHatEntity(index);
		panelIndices[panelIndex] = index;
	}

	private bool CheckInventory(int index)
	{
		foreach (int no in intList)
		{
			if (no == index)
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

	public void Last()
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
			//TODO Keep tabs on your limit.
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

				intList.Add(panelIndices[panelIndex]);
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
		int ret;
		//TODO need to check if index is correct when switching between panels.
		ret = panels[panelIndex].GetComponent<FancyCustomization>().GetNeededCoins(index);
		return ret;
	}

	// Not same
	// need to change
	public void Equip()
	{
		if (CheckInventory(panelIndices[panelIndex])) // if it is in the index, it returns true.
		{
			intList.Add(panelIndices[panelIndex]);
			Debug.Log("You Equip a NO." + panelIndices[panelIndex] + " Equipment");
		}
		else
		{
			Debug.Log("You have Equiped the NO." + panelIndices[panelIndex] + " Equipment");
		}
	}
}
