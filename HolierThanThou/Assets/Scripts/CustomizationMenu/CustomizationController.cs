//09-19: Use to switch the prefab under the player gameobject.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FancyCustomization = FancyScrollView.CustomizationMenu.CustomizationManager;

//README Make sure that the order is the same for every customization scroller, category, customizationswitcher, etc. 
//(hat, body) will mess up if paired with (body, hat)
public class CustomizationController : MonoBehaviour
{
	[SerializeField] private GameObject[] panels; // FancyCustomizations / CustomizationManagers 
	[SerializeField] private Texture[] categories;
	[SerializeField] private GameObject categoriesIcon;
	[SerializeField] private Transform currencyTextBox;
	[SerializeField] private GameObject confirmDialogue;
	[SerializeField] Text selectedItemInfo = default;
	[SerializeField] Text itemInfoText = default;

	private List<GameObject> equipmentSlots; // CustomizationSwitchers 
											 //private List<int>[] purchases;
	private GameObject player;
	private int panelIndex = 0;
	private int[] panelIndices; //the currently selected indices of each panel. 
	private CustomizationSwitcher customSwitcher;

	#region Initializing
	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		panelIndices = new int[panels.Length];

		InitializeObjectSlots();
	}
	private void Start()
	{
		//TODO Call CustomizationSwitcher.SwitchCustomization() to the correct equipped item for each type of item.
		SwitchCustomization(panelIndices[panelIndex]);
	}

	//README Make sure that your customization slots are in the same order (first at top).
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
	private bool CheckPlayerInventory()
	{
		string name = panels[panelIndex].GetComponent<FancyCustomization>().getCustomizations()[panelIndices[panelIndex]].name;
		return player.GetComponent<PlayerCustomization>().CheckUnlockedItems(panelIndex, name);
	}

	#region UIManagement
	public void UpdateCurrencyText()
	{
		currencyTextBox.GetComponent<Text>().text =
			player.GetComponent<PlayerCustomization>().currency.ToString();
	}
	private void UpdateInfoText(int index)
	{
		GameObject obj = panels[panelIndex].GetComponent<FancyCustomization>().getCustomizations()[index];
		Item thing = obj.GetComponent<Item>();
		if (!CheckPlayerInventory())
		{
			//unpurchased items
			selectedItemInfo.text = thing.getInfo();
		}
		else if (player.GetComponent<PlayerCustomization>().CheckEquippedItem(panelIndex, obj.name))
		{
			//equipped items
			selectedItemInfo.text = $"{thing.getName()}: Equipped";
		}
		else if(index == 0)
		{
			//default items
			selectedItemInfo.text = thing.getName();
		}
		else
		{
			//owned items
			selectedItemInfo.text = $"{thing.getName()}: Owned";
		}
	}

	public void SwitchCustomization(int index)
	{
		//update the preview.
		customSwitcher = equipmentSlots[panelIndex].GetComponent<CustomizationSwitcher>();
		customSwitcher.SwitchCustomization(index);
		panelIndices[panelIndex] = index;
		// Fill the selectedItemInfo text with the customization's info.
		UpdateInfoText(index);
	}

	public void SwitchPanelCustomization(int pIndex, int index)
	{
		//set the index to the incoming index.
		if ((pIndex >= 0) && (pIndex < panels.Length))
		{
			panelIndex = pIndex;
		}
		ChangePanel(index);
		panels[panelIndex].GetComponent<FancyCustomization>().GetScrollView().SelectCell(index);
	}

	public void Next()
	{
		RevertItemSelection();

		panelIndex ++;
		panelIndex %= panels.Length;

		ChangePanel(panelIndices[panelIndex]);
	}

	public void Previous()
	{
		RevertItemSelection();

		panelIndex --;
		panelIndex %= panels.Length;

		ChangePanel(panelIndices[panelIndex]);
	}
	
	//When you switch between panels, this should keep you equipped items on, but allow you to cutomize on your currently selected menu.
	private void RevertItemSelection()
	{
		string equippedItemName = player.GetComponent<PlayerCustomization>().equippedItems[panelIndex];
		GameObject[] currentScrollMenu = panels[panelIndex].GetComponent<FancyCustomization>().getCustomizations();

		//Find out where the equipped item is stored in the customization hierarchy.
		int equipIndex = 0;
		for (; equipIndex < currentScrollMenu.Length; equipIndex++)
		{
			if (currentScrollMenu[equipIndex].name == equippedItemName)
			{
				break;
			}
		}

		//Change preview
		customSwitcher = equipmentSlots[panelIndex].GetComponent<CustomizationSwitcher>();
		customSwitcher.SwitchCustomization(equipIndex);

		//Make sure the current panel is set to the equipped item
		panelIndices[panelIndex] = equipIndex;
		panels[panelIndex].GetComponent<FancyCustomization>().GetScrollView().SelectCell(equipIndex);
	}

	public void ChangePanel(int index)
	{
		foreach (GameObject panel in panels)
		{
			panel.SetActive(false);
		}
		panels[panelIndex].SetActive(true);

		if (categoriesIcon.GetComponent<RawImage>())
		{
			categoriesIcon.GetComponent<RawImage>().texture = categories[panelIndex];
		}
		SwitchCustomization(index);
	}

	#endregion /UIManagement

	#region Purchasing

	public void InitializePurchase()
	{
		if (!CheckPlayerInventory()) //If it's not in the player inventory, continue purchasing.
		{
			int playerMoney = player.GetComponent<PlayerCustomization>().currency;  
			int price = GetPrice(panelIndices[panelIndex]);

			if (playerMoney - price < 0)
			{
				Debug.Log("You need " + (price - playerMoney) + " more coins to purchase.");
			}
			else
			{
				itemInfoText.text = selectedItemInfo.text;
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

		//Update the purchases
		string name = panels[panelIndex].GetComponent<FancyCustomization>().getCustomizations()[panelIndices[panelIndex]].name;
		bool result = player.GetComponent<PlayerCustomization>().AddUnlockedItem(panelIndex, name);

		UpdateInfoText(panelIndices[panelIndex]);

		Debug.Log($"{result}: You bought a NO.{panelIndices[panelIndex]} equipment");

	}

	private int GetPrice(int index)
	{
		return panels[panelIndex].GetComponent<FancyCustomization>().GetNeededCoins(index);
	}

	#endregion /Purchasing

	public void Equip()
	{
		// if it not purchased, tell player how much it is to purchase.
		if (!CheckPlayerInventory()) // if it has not been purchased
		{
			int playerMoney = player.GetComponent<PlayerCustomization>().currency; //TODO replace with player profile's currency. 
			int price = GetPrice(panelIndices[panelIndex]);
			if (playerMoney - price < 0)
			{
				Debug.Log("You need " + (price - playerMoney) + " more coins to purchase.");
			}
			else
			{
				Debug.Log("You may purchase this item for " + price + " coins.");
			}
		}
		else //if it is purchased, the player can equip it.
		{
			string itemName = panels[panelIndex].GetComponent<FancyCustomization>().getCustomizations()[panelIndices[panelIndex]].name;
			bool isEquipped = player.GetComponent<PlayerCustomization>().CheckEquippedItem(panelIndex, itemName);
			if (isEquipped)
			{
				// if it is currently equipped, do not equip it. 
				Debug.Log("You have already equipped the NO." + panelIndices[panelIndex] + " equipment");
			}
			else
			{
				// if it is not currently equipped, equip it. 
				player.GetComponent<PlayerCustomization>().SetEquippedItem(panelIndex, itemName);
				Debug.Log("You equip a NO." + panelIndices[panelIndex] + " equipment");
				UpdateInfoText(panelIndices[panelIndex]);
			}
		}
	}
}
