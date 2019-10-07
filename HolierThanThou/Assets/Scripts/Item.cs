using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
	[SerializeField] private UnityEngine.Mesh mesh = default;
	[SerializeField] private UnityEngine.Texture texture = default;
	[SerializeField] private ClothingOptions option = default;
	Item(ClothingOptions Option)
	{
		option = Option;
	}
};
