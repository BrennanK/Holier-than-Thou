using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    public PowerUp[] powerups;

    private bool isDisabled;

    [SerializeField]
    private float disableTimerStart = 5f;

    private float disableTimer;


    private GameObject player;


    private void Start()
    {
        powerups = new PowerUp[9];

        powerups[0] = new BlastZone();
        powerups[1] = new Chillout();
        powerups[2] = new GottaGoFast();
        powerups[3] = new CantTouchThis();
        powerups[4] = new SneakySnake();
        powerups[5] = new Thiccness();
        powerups[6] = new BallsOfSteel();
        powerups[7] = new ISeeDeadPeople();
        powerups[8] = new CalmDown();

        disableTimer = disableTimerStart;
    }


    private void Update()
    {
       
        if (isDisabled)
        {
            disableTimer -= Time.deltaTime;
        }
        if (disableTimer <= 0)
        {
            EnablePowerUp();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        var _powerUpTracker = other.gameObject.GetComponent<PowerUpTracker>();

        if (other.gameObject.tag == "Player")
        {

            if (_powerUpTracker.slot1 == null)
            {
                _powerUpTracker.slot1 = powerups[0];
                //_powerUpTracker.slot1 = powerups[Random.Range(0, powerups.Length)] ;
                _powerUpTracker.slot1.CheckForDuration();
                DisablePowerUp();
                Debug.Log("Slot one filled with " + _powerUpTracker.slot1);
                return;
            }
            if (_powerUpTracker.slot2 == null)
            {
                _powerUpTracker.slot1 = powerups[2];
                //_powerUpTracker.slot2 = powerups[Random.Range(0, powerups.Length)];
                _powerUpTracker.slot1.CheckForDuration();
                DisablePowerUp();
                Debug.Log("Slot two filled with " + _powerUpTracker.slot2);
                return;
            }
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


}
