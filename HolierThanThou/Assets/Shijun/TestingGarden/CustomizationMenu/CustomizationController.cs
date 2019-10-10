//09-19: Use to switch the prefab under the player gameobject.

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using FancyCustomization = FancyScrollView.CustomizationMenu.CustomizationManager;

//README Make sure that your customization slots are in the same order (first at top) as the customization panels in the array. 
public class CustomizationController : MonoBehaviour
{
	[SerializeField] private GameObject[] panels; // CustomizationManagers
	[SerializeField] private Texture[] categories;
	[SerializeField] private GameObject categoriesIcon;
	[SerializeField] private Transform currencyTextBox;
	[SerializeField] private GameObject confirmDialogue;

	private List<GameObject> equipmentSlots; // CustomizationSwitchers
	private List<int>[] purchases;
	private GameObject player;
	//private int currentIndex;
	private int panelIndex = 0;
	private int[] panelIndices; //the currently selected indices of each panel. 
	private CustomizationSwitcher customSwitcher;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		panelIndices = new int[panels.Length];

		InitializeObjectSlots();
		InitializePurchaseList();

		UpdateCurrencyText();
	}
	#region Initializing

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
	private void InitializeObjectSlots()
	{
		equipmentSlots = new List<GameObject>();
		int i = 0;
		foreach (Transform child in player.transform)
		{
			if (child.GetComponent<CustomizationSwitcher>())
			{
				equipmentSlots.Add(child.gameObject);
				child.GetComponent<CustomizationSwitcher>().Initialize(
					panels[i].GetComponent<FancyCustomization>().getCustomizations()
					);
			}
			i++;
		}
	}
	#endregion /Initializing

	//if item in inventory, returns true;
	//TODO extrapolate this to the player class.
	private bool CheckPlayerInventory(int index)
	{
		foreach (int itemNo in purchases[panelIndex])
		{
			if (itemNo == index)
			{
				return true;
			}
		}
		return false;
	}

	#region UIManagement
	private void UpdateCurrencyText()
	{
		currencyTextBox.GetComponent<Text>().text =
			player.GetComponent<PlayerCustomization>().currency.ToString();
	}

	// TODO Refactor this so it doesn't just apply to Hats.
	// Snap the option menu, and switch the customized entity automatically.
	public void SwitchCustomization(int index)
	{
		customSwitcher = equipmentSlots[panelIndex].GetComponent<CustomizationSwitcher>();
		customSwitcher.SwitchCustomization(index);
		panelIndices[panelIndex] = index;
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
		ChangePanel();
	}

	public void Previous()
	{
		foreach (GameObject panel in panels)
		{
			panel.SetActive(false);
		}
		panelIndex--;
		if (panelIndex < 0) panelIndex = panels.Length - 1; //Don't go under the minimum.
		ChangePanel();
	}

	public void ChangePanel()
	{
		panels[panelIndex].SetActive(true);
		if (categoriesIcon.GetComponent<RawImage>())
		{
			categoriesIcon.GetComponent<RawImage>().texture = categories[panelIndex];
		}
	}

	#endregion /UIManagement

	#region Purchasing

	public void InitializePurchase()
	{
		if (!CheckPlayerInventory(panelIndices[panelIndex])) //If it's not in the player inventory, continue purchasing.
		{
			int playerMoney = player.GetComponent<PlayerCustomization>().currency; //TODO replace with player profile's currency. 
			int price = GetPrice(panelIndices[panelIndex]);

			if (playerMoney - price < 0)
			{
				Debug.Log("You need " + (price - playerMoney) + " more coins to purchase.");
			}
			else
			{
				confirmDialogue.SetActive(true);
			}
		}
		else
		{
			Debug.Log("You have already bought the NO." + panelIndices[panelIndex] + " equipment");
		}
	}
	public void TurnOffDialogue()
	{
		confirmDialogue.SetActive(false);
	}

	public void FinalizePurchase()
	{
		TurnOffDialogue();
		int price = GetPrice(panelIndices[panelIndex]);

		//Update the currency.
		player.GetComponent<PlayerCustomization>().subtractCurrency(price);
		UpdateCurrencyText();

		purchases[panelIndex].Add(panelIndices[panelIndex]);
		Debug.Log("You bought a NO." + panelIndices[panelIndex] + " equipment");

	}

	private int GetPrice(int index)
	{
		return panels[panelIndex].GetComponent<FancyCustomization>().GetNeededCoins(index);
	}

	#endregion /Purchasing

	// TODO Player inventory needs to be in the Player class. 
	public void Equip()
	{
		// if it not purchased, tell player how much it is to purchase.
		if (!CheckPlayerInventory(panelIndices[panelIndex])) // if it has been purchased, it returns true.
		{
			int playerMoney = player.GetComponent<PlayerCustomization>().currency; //TODO replace with player profile's currency. 
			int price = GetPrice(panelIndices[panelIndex]);
			if (playerMoney - price < 0)
			{
				Debug.Log("You need " + (price - playerMoney) + " more coins to purchase.");
			}
			else
			{
				Debug.Log("This item is worth " + price + " coins.");
			}
		}
		else //if it is purchased, the player can equip it.
		{
			// if it is currently equipped, do not equip it. 
			Debug.Log("You equip a NO." + panelIndices[panelIndex] + " equipment");
			// if it is not currently equipped, equip it. 
			Debug.Log("You have already equipped the NO." + panelIndices[panelIndex] + " equipment");
		}
	}
}
