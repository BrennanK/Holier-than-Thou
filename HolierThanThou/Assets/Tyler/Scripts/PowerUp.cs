using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float duration;

    public Sprite icon;

    private bool isDisabled;

    [SerializeField]
    private float disableTimerStart = 5f;

    private float disableTimer;


    private GameObject player;

    private void Start()
    {
        disableTimer = disableTimerStart;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var _powerUpTracker = other.gameObject.GetComponent<PowerUpTracker>();

        if (other.gameObject.tag == "Player")
        {

            if (_powerUpTracker.slot1 == null)
            {
                _powerUpTracker.slot1 = this;
                DisablePowerUp();
                Debug.Log("Slot one filled with " + _powerUpTracker.slot1);
                return;
            }
            if (_powerUpTracker.slot2 == null)
            {
                _powerUpTracker.slot2 = this;
                DisablePowerUp();
                Debug.Log("Slot two filled with " + _powerUpTracker.slot2);
                return;
            }
        }
    }


    private void Update()
    {
        

        if (isDisabled)
        {
            disableTimer -= Time.deltaTime;
        }
        if(disableTimer <= 0)
        {
            EnablePowerUp();
        }

    }

    void DisablePowerUp()
    {
        isDisabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    void EnablePowerUp()
    {
        isDisabled = false;
        disableTimer = disableTimerStart;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }


    public virtual void ActivatePowerUp()
    {
   
    }

    public virtual void ActivateSecondPowerUp()
    {
       
    }
}
