using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FancyCustomization = FancyScrollView.CustomizationMenu.CustomizationManager;

//README Make sure that the order is the same for every customization Scroller, category, customizationSwitcher, etc. 
//(hat, body) will mess up if paired with (body, hat)
public class CustomizationController : MonoBehaviour
{
    [SerializeField] private GameObject[] panels; // FancyCustomizations / CustomizationManagers 
    [SerializeField] private Texture[] categories;
    [SerializeField] private GameObject categoriesIcon;
    [SerializeField] private Text currencyTextBox;
    [SerializeField] private GameObject confirmDialogue;
    [SerializeField] private GameObject priceDialog;
    [SerializeField] Text selectedItemInfo = default;
    [SerializeField] Text itemInfoText = default;
    [SerializeField] Text priceWarningText = default;
    [SerializeField] InputField namingUser;
    [SerializeField] Text namingText;

    private List<GameObject> equipmentSlots; // CustomizationSwitchers 
    private GameObject player;
    private int panelIndex = 0;
    private int[] panelIndices; //the currently selected indices of each panel. 

    #region Initializing
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        panelIndices = new int[panels.Length];

        namingText.text = PlayerPrefs.GetString("PLAYER_INPUT_NAME", "Input Name");

        InitializeObjectSlots();
		UpdateCurrencyText();

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
                GameObject[] initArray = panels[i].GetComponent<FancyCustomization>().getCustomizations();
                if (initArray == null)
                {
                    initArray = panels[i].GetComponent<FancyCustomization>().InitializeCustomizationArray();
                }
                child.GetComponent<CustomizationSwitcher>().Initialize(initArray);
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
		//int _currency = player.GetComponent<PlayerCustomization>().currency;
		int _currency =	PlayerPrefs.GetInt("Currency", player.GetComponent<PlayerCustomization>().currency);
		currencyTextBox.text = _currency.ToString();
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
        else if (index == 0)
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
        equipmentSlots[panelIndex].GetComponent<CustomizationSwitcher>().SwitchCustomization(index);
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

        panelIndex++;
        panelIndex %= panels.Length;

        ChangePanel(panelIndices[panelIndex]);
    }

    public void Previous()
    {
        RevertItemSelection();

        panelIndex--;
        if (panelIndex < 0)
        {
            panelIndex = panels.Length - 1;
        }

        ChangePanel(panelIndices[panelIndex]);
    }

    //When you switch between panels, this should keep you equipped items on, but allow you to customize on your currently selected menu.
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
        equipmentSlots[panelIndex].GetComponent<CustomizationSwitcher>().SwitchCustomization(equipIndex);

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
            int playerMoney = PlayerPrefs.GetInt("Currency", player.GetComponent<PlayerCustomization>().currency);
            int price = GetPrice(panelIndices[panelIndex]);

            if (playerMoney - price < 0)
            {
                //This is where the warning text box will be triggered
                //priceWarningText.text = string.Format("You need {0:g} more to purchase that item!", (price - playerMoney)); //Set warning text to display amount needed
                priceWarningText.text = string.Format("{0:g}", (price - playerMoney));
                priceDialog.SetActive(true);
            }
            else
            {
                itemInfoText.text = selectedItemInfo.text;
                confirmDialogue.SetActive(true);
            }
        }

    }
    public void TurnOffDialogue()
    {
        confirmDialogue.SetActive(false);
    }

    public void ClosePriceWarning()
    {
        priceDialog.SetActive(false);
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


		Equip();
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

		}
		else //if it is purchased, the player can equip it.
		{
			string itemName = panels[panelIndex].GetComponent<FancyCustomization>().getCustomizations()[panelIndices[panelIndex]].name;
			bool isEquipped = player.GetComponent<PlayerCustomization>().CheckEquippedItem(panelIndex, itemName);
			if (isEquipped)
			{

			}
			else
			{
				// if it is not currently equipped, equip it. 
				player.GetComponent<PlayerCustomization>().SetEquippedItem(panelIndex, itemName);
				UpdateInfoText(panelIndices[panelIndex]);
			}
		}
	}

    //Naming System: To send the customized name to the character
    //And load the saved name from the playerprefs on Awake
    public void UpdateCharacterName()
    {
        string playerName = namingUser.text.ToString();
        namingText.text = playerName;
        PlayerPrefs.SetString("PLAYER_INPUT_NAME", playerName);

    }
}
