using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationSwitcher : MonoBehaviour, IInitializer<CustomizationSwitcher>
{
	[SerializeField] private ClothingOptions customizationType = default;
	private GameObject[] customizations;

	public void Start()
	{
		for (int i = 0; i < customizations.Length; i++)
		{
			GameObject option = Instantiate(customizations[i]);
			option.SetActive(false);
			option.transform.parent = transform;
			option.transform.localPosition += option.transform.parent.transform.position;
		}
		if (transform.childCount > 0)
		{
			SwitchCustomization(0);
		}
	}

	//this method is required for IInitializer.
	public bool Equals(CustomizationSwitcher switcher)
	{
		return this.customizations == switcher.customizations &&
			this.customizationType == switcher.customizationType;
	}

	//this method is required for IInitializer.
	public void Initialize(GameObject[] obj) 
	{
		customizations = obj;
	}

	public GameObject[] GetCustomizations()
	{
		return customizations;
	}

	public void SwitchCustomization(int index)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
		if (transform.childCount > 0)
		{
			transform.GetChild(index).gameObject.SetActive(true);
		}
	}
}
