using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationSwitcher : MonoBehaviour
{
    private GameObject[] customizations;

    // Start is called before the first frame update
    void Start()
    {
		InitializeCustomizations();
    }

	private void InitializeCustomizations()
	{
		Transform children = GetComponent<Transform>();
        customizations = new GameObject[children.childCount];
        int counter = 0;
        foreach (Transform child in children)
        {
            customizations[counter] = child.gameObject;
            counter++;
        }
	}

    public void SwitchCustomization(int index)
    {
        foreach(GameObject option in customizations)
        {
            option.SetActive(false);
        }
        customizations[index].SetActive(true);
    }
}
