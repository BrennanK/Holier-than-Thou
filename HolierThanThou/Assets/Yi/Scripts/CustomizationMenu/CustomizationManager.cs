using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.CustomizationMenu
{
	public class CustomizationManager : MonoBehaviour
	{
		[SerializeField] ScrollView scrollView = default;
		//[SerializeField] string[] NeededCoinsList;
		[SerializeField] Item[] CustomizationArray;
		[SerializeField] Text selectedItemInfo = default;

		void Start()
		{
			ItemData[] items = CustomizationArray.Select(i => new ItemData($"${i.getPrice()}")).ToArray();



			scrollView.OnSelectionChanged(OnSelectionChanged);
			scrollView.UpdateData(items);
			scrollView.SelectCell(0);
		}

		void OnSelectionChanged(int index)
		{
			//selectedItemInfo.text = $"Selected item info: index {index}";
			transform.GetComponentInParent<CustomizationController>().SwitchCustomization(index);
		}

		public int GetNeededCoins(int index)
		{
			return Convert.ToInt32(CustomizationArray[index].getPrice());
		}

	}
}