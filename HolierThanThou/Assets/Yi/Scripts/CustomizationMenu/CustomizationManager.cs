using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.CustomizationMenu
{
	public class CustomizationManager : MonoBehaviour
	{
		[SerializeField] private ScrollView scrollView = default;
		[SerializeField] private string equipmentType = "Body";

		private static GameObject[] prefabs;
		private GameObject[] CustomizationArray;

		void Awake()
		{
			InitializeCustomizationArray();
			ItemData[] items = CustomizationArray.Select(i => new ItemData($"${i.GetComponent<Item>().getPrice()}")).ToArray();
			scrollView.Covers = CustomizationArray.Select(i => i.GetComponent<Item>().getCover()).ToArray();
			scrollView.OnSelectionChanged(OnSelectionChanged);
			scrollView.UpdateData(items);
			scrollView.SelectCell(0);
		}

		public ScrollView GetScrollView()
		{
			return scrollView;
		}

		public GameObject[] getCustomizations()
		{
			return CustomizationArray;
		}

		void OnSelectionChanged(int index)
		{
			//selectedItemInfo.text = $"Selected item info: index {index}";
			transform.GetComponentInParent<CustomizationController>().SwitchCustomization(index);
		}

		public int GetNeededCoins(int index)
		{
			return Convert.ToInt32(CustomizationArray[index].GetComponent<Item>().getPrice());
		}

		public GameObject[] InitializeCustomizationArray()
		{
			// Make a container for the prefabs to load in.
			List<GameObject> prefabsOfCustomizationType = new List<GameObject>();
			// Get ALL of the prefabs from the prefabs folder.
			if (prefabs == null)
			{
				prefabs = Resources.LoadAll("Prefabs/").Select(p => (GameObject)p).ToArray();
			}
			//Get all prefabs of Type "equipmentType."
			foreach (GameObject prefab in prefabs)
			{
				if (prefab.name.StartsWith(equipmentType))
				{
					prefabsOfCustomizationType.Add(prefab);
				}
			}
			CustomizationArray = new GameObject[prefabsOfCustomizationType.Count];
			CustomizationArray = prefabsOfCustomizationType.ToArray();
			return CustomizationArray;
		}

	}
}