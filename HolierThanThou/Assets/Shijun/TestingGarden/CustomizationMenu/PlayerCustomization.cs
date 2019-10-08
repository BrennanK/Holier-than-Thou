using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
	public int currency { get; private set; } = 900;

	[SerializeField] private int max_currency = 9999;
	[SerializeField] private string[] slots = new string[2] {"Hat", "Skin"};
	private List<int>[] unlockedItems;
	public int[] equippedItems { get; private set; }

	public void Awake()
	{
		unlockedItems = new List<int>[slots.Length];
		Load();
	}

	public void Start()
	{
		//TODO Call CustomizationSwitcher.SwitchCustomization() to the correct equipped item for each type of item.
		//CustomizationSwitcher[] switchers = FindObjectsOfType<CustomizationSwitcher>();
		//for (int i = 0; i < switchers.Length; i++)
		//{
		//	switchers[i].SwitchCustomization(equippedItems[i]);
		//}
	}

	public void OnDestroy()
	{
		Save();
	}

	private void Load()
	{
		currency = PlayerPrefs.GetInt("Currency", 1000);
	}

	private void Save()
	{
		PlayerPrefs.SetInt("Currency", currency);
		SaveUnlockedItems();
		PlayerPrefs.Save();
	}

	//TODO
	private void SaveUnlockedItems()
	{
		//create string of comma separated values for all values of the unlockedItems array of lists.
		string saveData = "";
		//Create each list of values, separated by /n
		for (int i = 0; i < unlockedItems.Length; i++)
		{
			saveData += slots[i];
			for (int j = 0; j < unlockedItems[i].Count; j++)
			{
				saveData += unlockedItems[i][j];
			}
			saveData += "\n";
		}
		int x = 0;
		x++;
		//PlayerPrefs.SetString("UnlockedItems", saveData);
	}

	public bool addCurrency(int coins)
	{
		if (currency + coins < max_currency)
		{
			currency += coins;
			return true;
		}
		currency = max_currency;
		return false;
	}

	public bool subtractCurrency(int coins)
	{
		if (currency - coins >= 0)
		{
			currency -= coins;
			return true;
		}
		return false;
	}


}
