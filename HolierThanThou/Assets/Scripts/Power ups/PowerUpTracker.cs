using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTracker : MonoBehaviour
{
    public PowerUp slot1;
    public PowerUp slot2;

    private bool activated1;
    private bool activated2;
    private bool canActivate1;
    private bool canActivate2;

    private float powerTimer1;
    private float powerTimer2;

    private GameObject _player;

    public Text itemButton1;
    public Text itemButton2;
    private Competitor competitor;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        canActivate1 = true;
        canActivate2 = true;
        UpdateUI();
        competitor = GetComponent<Competitor>();
    }


    private void Update()
    {

        if(activated1)
        {
            canActivate1 = false;
            powerTimer1 -= Time.deltaTime;
            UpdateUI();
        }

        if(powerTimer1 <= 0 && activated1)
        {
            activated1 = false;
            slot1.ResetEffects(competitor.Name);
            slot1 = null;
            canActivate1 = true;
            UpdateUI();
        }        

        if(activated2)
        {
            canActivate2 = false;
            powerTimer2 -= Time.deltaTime;
            UpdateUI();
        }

        if(powerTimer2 <= 0 && activated2)
        {
            activated2 = false;
            slot2.ResetEffects(competitor.Name);
            slot2 = null;
            canActivate2 = true;
            UpdateUI();
        }        
        
    }

    public void UseItem1()
    {
        if (canActivate1)
        {
            if (slot1 != null)
            {

                if (slot1.hasDuration)
                {
                    powerTimer1 = slot1.duration;
                    activated1 = true;
                    slot1.ActivatePowerUp(competitor.Name, competitor.origin);
                }
                else
                {
                    slot1.ActivatePowerUp(competitor.Name, competitor.origin);
                    slot1 = null;

                }
                UpdateUI();
            }
            else
                Debug.Log("No Power up in slot 1!");
        }
    }

    public void UseItem2()
    {
        if (canActivate2)
        {
            if (slot2 != null)
            {

                if (slot2.hasDuration)
                {
                    powerTimer2 = slot2.duration;
                    activated2 = true;
                    slot2.ActivatePowerUp(competitor.Name, competitor.origin);
                }
                else
                {
                    slot2.ActivatePowerUp(competitor.Name, competitor.origin);
                    slot2 = null;
                }
                UpdateUI();
            }
            else
                Debug.Log("No Power up in slot 2!");
        }
    }

    public void UpdateUI()
    {
        if (slot1 != null)
        {
            if(activated1)
            {
                itemButton1.text = slot1.ToString() + " " + Mathf.Round(powerTimer1);
            }
            else
            itemButton1.text = slot1.ToString();
        }
        else
            itemButton1.text = "No Item";

        if(slot2!=null)
        {
            if(activated2)
            {
                itemButton2.text = slot2.ToString() + " " + Mathf.Round(powerTimer2);
            }
            else
            itemButton2.text = slot2.ToString();
        }
        else
            itemButton2.text = "No Item";
    }
}
