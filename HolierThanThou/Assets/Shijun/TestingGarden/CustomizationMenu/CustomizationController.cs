//09-19: Use to switch the prefab under the player gameobject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationController : MonoBehaviour
{
    //Prefab for Hat and Body
    private GameObject hatEntity;
    private GameObject bodyEntity;

    //GameObject for visualization.
    private Transform hat;
    private Transform body;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform child in player.GetComponent<Transform>())
        {
            if (child.name == "Hat")
            {
                hat = child;
            }
            if (child.name == "Body")
            {
                body = child;
            }
        }
    }

    // Snap the option menu, and switch the customized enetity automatically.
    private void SwitchHatEntity()
    {
        
    }

}
