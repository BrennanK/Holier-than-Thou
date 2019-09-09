using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTracker : MonoBehaviour
{
    public PowerUp slot1;
    public PowerUp slot2;

    private bool activated;
    private float powerTimer;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Slot 1: " + slot1 + "\nSlot 2: " + slot2);
        }

        if(activated)
        {
            powerTimer -= Time.deltaTime;
        }

        if(powerTimer <= 0 && activated)
        {
            activated = false;
            slot1.ResetEffects();
            slot1 = null;
        }        

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (slot1 != null)
            {

                if (slot1.hasDuration)
                {
                    powerTimer = slot1.duration;
                    activated = true;
                    slot1.ActivatePowerUp();
                }
                else
                {
                    slot1.ActivatePowerUp();
                    slot1 = null;
                }
            }
            else
                Debug.Log("No Power up in slot 1!");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (slot2 != null)
            {
                slot2.ActivatePowerUp();
                slot2 = null;
            }
            else
                Debug.Log("No Power up in slot 2!");
        }
    }


}
