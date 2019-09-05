using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTracker : MonoBehaviour
{
    public PowerUp slot1;
    public PowerUp slot2;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Slot 1: " + slot1 + "\nSlot 2: " + slot2);
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            if (slot1 != null)
            {
                slot1.ActivatePowerUp();
                slot1 = null;
            }
            else
                Debug.Log("No Power up in slot 1!");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (slot2 != null)
            {
                slot2.ActivateSecondPowerUp();
                slot2 = null;
            }
            else
                Debug.Log("No Power up in slot 2!");
        }
    }


}
