//09-19: Use to switch the prefab under the player gameobject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationController : MonoBehaviour
{
    //Prefab for Hat and Body
    private GameObject hatSlot;
    private GameObject bodySlot;

    public GameObject[] hatEntity;
    public GameObject[] bodyEntity;

    private void Start()
    {
        InitialObjectSlot();
    }

    private void InitialObjectSlot()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform child in player.transform)
        {
            if (child.name == "Hat")
            {
                hatSlot = child.gameObject;
            }
            if (child.name == "Body")
            {
                bodySlot = child.gameObject;
            }
        }
    }



    // Snap the option menu, and switch the customized enetity automatically.
    public void SwitchHatEntity(int index)
    {
        HatSwitcher hatSwitcher = hatSlot.GetComponent<HatSwitcher>();
        hatSwitcher.SwitchHatEntity(index);
    }

    private void Next()
    {

    }
    
    private void Last()
    {

    }

}
