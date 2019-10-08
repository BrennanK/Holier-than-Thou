using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private int price = default;
	[SerializeField] private ClothingOptions option = default;
	[SerializeField] private string itemName = default;
	[SerializeField] private Sprite cover = default;

	Item() { }
	Item(ClothingOptions Option)
	{
		option = Option;
	}

	public int getPrice()
	{
		return price;
	}
};
