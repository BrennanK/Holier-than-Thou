using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
	public int currency { get; private set; } = 900;
	public int[] equippedItems { get; private set; }
	[SerializeField] private int max_currency = 9999;

	private List<int>[] unlockedItems;
	private string toParse = "";

	public void Start()
	{
		Load();
	}

	public void OnDestroy()
	{
		Save();
	}

	#region loading

	private void Load()
	{
		currency = PlayerPrefs.GetInt("Currency", 1000);
		FindObjectOfType<CustomizationController>().UpdateCurrencyText();
		LoadUnlockedItems();
		LoadEquippedItems();
	}

	private void LoadUnlockedItems()
	{
		toParse = PlayerPrefs.GetString("UnlockedItems", "0.0.");
		int numTypes = System.Enum.GetNames(typeof(ClothingOptions)).Length;
		char[] newLines = { '.' };
		char[] commas = { ',' };
		string[] categories = toParse.Split(newLines, System.StringSplitOptions.RemoveEmptyEntries);
		unlockedItems = new List<int>[System.Enum.GetNames(typeof(ClothingOptions)).Length];
		for (int i = 0; i < unlockedItems.Length; i++)
		{
			unlockedItems[i] = new List<int>();
		}
		for (int i = 0; i < categories.Length; i++)
		{
			string[] category = categories[i].Split(commas, System.StringSplitOptions.RemoveEmptyEntries);
			for (int j = 0; j < category.Length; j++)
			{
				unlockedItems[i].Add(System.Convert.ToInt32(category[j]));
			}
		}
		int debug = 0;
		debug++;
	}

	private void LoadEquippedItems()
	{
		toParse = PlayerPrefs.GetString("EquippedItems", "0,0.");
		char[] separators = { ',', '.' };
		string[] nums = toParse.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
		equippedItems = new int[System.Enum.GetNames(typeof(ClothingOptions)).Length];
		for (int i = 0; i < nums.Length; i++)
		{
			equippedItems[i] = System.Convert.ToInt32(nums[i]);
		}
		int debug = 0;
		debug++;
	}

#endregion /loading

	#region saving

	private void Save()
	{
		PlayerPrefs.SetInt("Currency", currency);
		SaveListOfArrays(unlockedItems, "UnlockedItems");
		SaveArray(equippedItems, "EquippedItems");
		PlayerPrefs.Save();
	}

	//TODO test and refine saving unlocked and equipped items.
	private void SaveListOfArrays(List<int>[] arr, string name = "UnlockedItems")
	{
		//create string of comma separated values for all values of the unlockedItems array of lists.
		string saveData = "";
		for (int i = 0; i < arr.Length; i++)
		{
			List<int> someItems = arr[i];
			for (int j = 0; j < someItems.Count; j++)
			{
				saveData += someItems[j].ToString();
				if(j < someItems.Count - 1)
				{
					saveData += ",";
				}
			}
			//Create each list of values, separated by /n
			saveData += ".";
		}
		PlayerPrefs.SetString(name, saveData);
	}
	private void SaveArray(int[] arr, string name = "EquippedItems")
	{
		string saveData = "";
		for (int i = 0; i < arr.Length; i++)
		{
			saveData += arr[i].ToString();
			if(i < arr.Length - 1)
			{
				saveData += ',';
			}
			else
			{
				saveData += '.';
			}
		}
		PlayerPrefs.SetString(name, saveData);
	}

	#endregion /saving

	#region currency

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
	#endregion /currency

}
