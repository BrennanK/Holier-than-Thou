//09-19: Use to switch the prefab under the player gameobject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationController : MonoBehaviour
{
    //Prefab for Hat and Body
    private GameObject hatEntity;
    private GameObject bodyEntity;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform child in player.transform)
        {
            if (child.name == "Hat")
            {
                hatEntity = child.gameObject;
            }
            if (child.name == "Body")
            {
                bodyEntity = child.gameObject;
            }
        }
    }

    // Snap the option menu, and switch the customized enetity automatically.
    private void SwitchHatEntity()
    {
        
    }

    private void Next()
    {

    }
    
    private void Last()
    {

    }

}
