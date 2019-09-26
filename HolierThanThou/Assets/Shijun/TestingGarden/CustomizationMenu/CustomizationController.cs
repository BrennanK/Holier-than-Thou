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
        currentIndex = index;
    }
    private int currentIndex;
    private List<int> intList = new List<int>();
    
    private bool CheckInventory(int index)
    {
        foreach(int no in intList)
        {
            if (no == index)
            {
                return false;
            }
        }
        return true;
    }

    public GameObject hatPanel, bodyPanel;
    public void Next()
    {
        if (hatPanel.activeSelf)
        {
            bodyPanel.SetActive(true);
            hatPanel.SetActive(false);
        }
        else
        {
            hatPanel.SetActive(true);
            bodyPanel.SetActive(false);
        }
    }

    public void Last()
    {
        if (hatPanel.activeSelf)
        {
            bodyPanel.SetActive(true);
            hatPanel.SetActive(false);
        }
        else
        {
            hatPanel.SetActive(true);
            bodyPanel.SetActive(false);
        }
    }

    public void Buy()
    {
        if (CheckInventory(currentIndex))
        {
            intList.Add(currentIndex);
            Debug.Log("You bought a NO." + currentIndex + " Equipment");
        }
        else
        {
            Debug.Log("You have bought the NO." + currentIndex + " Equipment");
        }

        
    }

    // Not same
    // need to change
    public void Equip()
    {
        if (CheckInventory(currentIndex))
        {
            intList.Add(currentIndex);
            Debug.Log("You Equip a NO." + currentIndex + " Equipment");
        }
        else
        {
            Debug.Log("You have Equiped the NO." + currentIndex + " Equipment");
        }
    }

}
