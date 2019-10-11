using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
	public int currency { get; private set; } = 900;
	public string[] equippedItems { get; private set; }
	[SerializeField] private int max_currency = 9999;
	[SerializeField] private int defaultMoney = 10;

	private List<string>[] unlockedItems;
	private string defaultHat = "Hat Cowboy";
	private string defaultBody = "Body AskingQs";
	private string toParse = "";
	private char separatorChar = ',';
	private char newLineChar = '.';

	public void Start()
	{

		currency = PlayerPrefs.GetInt("Currency", defaultMoney);
		FindObjectOfType<CustomizationController>().UpdateCurrencyText();
		LoadUnlockedItems();
		LoadEquippedItems();
		EquipItems();
	}

	public void OnDestroy()
	{
		PlayerPrefs.SetInt("Currency", currency);
		SaveListOfArrays(unlockedItems, "UnlockedItems");
		SaveArray(equippedItems, "EquippedItems");
		PlayerPrefs.Save();
	}

	#region loading

	private void LoadUnlockedItems()
	{
		toParse = PlayerPrefs.GetString("UnlockedItems", $"Hat 0{separatorChar}{defaultHat}{newLineChar}Body{separatorChar}{defaultBody}{newLineChar}");
		int numTypes = System.Enum.GetNames(typeof(ClothingOptions)).Length;
		char[] newLines = { newLineChar };
		char[] commas = { separatorChar };
		string[] categories = toParse.Split(newLines, System.StringSplitOptions.RemoveEmptyEntries);
		unlockedItems = new List<string>[System.Enum.GetNames(typeof(ClothingOptions)).Length];
		for (int i = 0; i < unlockedItems.Length; i++)
		{
			unlockedItems[i] = new List<string>();
		}
		for (int i = 0; i < categories.Length; i++)
		{
			string[] category = categories[i].Split(commas, System.StringSplitOptions.RemoveEmptyEntries);
			for (int j = 0; j < category.Length; j++)
			{
				unlockedItems[i].Add(category[j]);
			}
		}
		int debug = 0;
		debug++;
	}

	private void LoadEquippedItems()
	{
		toParse = PlayerPrefs.GetString("EquippedItems", $"{defaultHat}{separatorChar}{defaultBody}{newLineChar}");
		char[] separators = { separatorChar, newLineChar };
		string[] nums = toParse.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
		equippedItems = new string[System.Enum.GetNames(typeof(ClothingOptions)).Length];
		for (int i = 0; i < nums.Length; i++)
		{
			equippedItems[i] = nums[i];
		}
		int debug = 0;
		debug++;
	}

	#endregion /loading

	#region saving

	//TODO test and refine saving unlocked and equipped items.
	private void SaveListOfArrays(List<string>[] arr, string name = "UnlockedItems")
	{
		//create string of comma separated values for all values of the unlockedItems array of lists.
		string saveData = "";
		for (int i = 0; i < arr.Length; i++)
		{
			List<string> someItems = arr[i];
			for (int j = 0; j < someItems.Count; j++)
			{
				saveData += someItems[j].ToString();
				if (j < someItems.Count - 1)
				{
					saveData += separatorChar;
				}
			}
			saveData += newLineChar;
		}
		PlayerPrefs.SetString(name, saveData);
	}
	private void SaveArray(string[] arr, string name = "EquippedItems")
	{
		string saveData = "";
		for (int i = 0; i < arr.Length; i++)
		{
			saveData += arr[i].ToString();
			if (i < arr.Length - 1)
			{
				saveData += separatorChar;
			}
			else
			{
				saveData += newLineChar;
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

	public bool AddUnlockedItem(int index, string item)
	{
		if (!unlockedItems[index].Contains(item))
		{
			unlockedItems[index].Add(item);
			return true;
		}
		return false;
	}

	public bool SetEquippedItem(int index, string item)
	{
		equippedItems[index] = item;
		return false;
	}
	private void EquipItems()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 2)
		{
			int i = 0;
			foreach (string item in equippedItems)
			{
				//find prefab of specific name
				GameObject option = Instantiate((GameObject)Resources.Load($"Prefabs/{item}"));
				option.SetActive(true);
				option.transform.parent = transform.GetChild(i);
				option.transform.localPosition += option.transform.parent.transform.position;
				i++;
			}
		}
		else
		{
			CustomizationController customizationController = FindObjectOfType<CustomizationController>();
			//Go backwards through the children so that the customizations menu always starts off on the first menu.
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				//find out 
				CustomizationSwitcher cSwitcher = transform.GetChild(i).GetComponent<CustomizationSwitcher>();
				GameObject[] options = cSwitcher.GetCustomizations();
				for (int j = 0; j < options.Length; j++)
				{
					if (options[j].name == equippedItems[i])
					{
						customizationController.SwitchPanelCustomization(i, j);
						break;
					}
				}
			}
		}
	}

}
