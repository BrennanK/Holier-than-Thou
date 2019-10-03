using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationSwitcher : MonoBehaviour
{
    private GameObject[] Customization;

    // Start is called before the first frame update
    void Start()
    {
        Transform customizations = GetComponent<Transform>();
        Customization = new GameObject[customizations.childCount];
        int counter = 0;
        foreach (Transform customization in customizations)
        {
            Customization[counter] = customization.gameObject;
            counter++;
        }
    }

    public void SwitchHatEntity(int index)
    {
        foreach(GameObject hat in Customization)
        {
            hat.SetActive(false);
        }
        Customization[index].SetActive(true);
    }
}
