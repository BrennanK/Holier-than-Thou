using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
	public int currency { get; private set; } = 900;

	[SerializeField] private int max_currency = 9999;
	private List<GameObject>[] unlockedItems;

	

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
