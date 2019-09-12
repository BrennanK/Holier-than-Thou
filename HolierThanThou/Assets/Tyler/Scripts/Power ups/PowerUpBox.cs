using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    public PowerUp[] powerups;
    public PowerUpEditor PUE;

    private bool isDisabled;

    [SerializeField]
    private float disableTimerStart = 5f;

    private float disableTimer;

    public int itemNumber;


    private GameObject player;


    private void Start()
    {
        powerups = new PowerUp[9];

        powerups[0] = new BlastZone(PUE.BZ_hasDuration, PUE.BZ_duration , PUE.BZ_radius, PUE.BZ_power);
        powerups[1] = new Chillout(PUE.CO_hasDuration, PUE.CO_duration, PUE.CO_radius);
        powerups[2] = new GottaGoFast(PUE.GF_hasDuration, PUE.GF_duration, PUE.GF_radius, PUE.GF_speedMultiplier);
        powerups[3] = new CantTouchThis(PUE.CTT_hasDuration, PUE.CTT_duration, PUE.CTT_radius);
        powerups[4] = new SneakySnake(PUE.SS_hasDuration, PUE.SS_duration, PUE.SS_radius);
        powerups[5] = new Thiccness(PUE.TH_hasDuration, PUE.TH_duration, PUE.TH_radius);
        powerups[6] = new BallsOfSteel(PUE.BS_hasDuration, PUE.BS_duration, PUE.BS_radius);
        powerups[7] = new ISeeDeadPeople(PUE.IS_hasDuration, PUE.IS_duration, PUE.IS_radius);
        powerups[8] = new CalmDown(PUE.CD_hasDuration, PUE.CD_duration, PUE.CD_radius, PUE.CD_speedMultiplier);

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
                //_powerUpTracker.slot1 = powerups[1];
                _powerUpTracker.slot1 = powerups[Random.Range(0, powerups.Length)] ;
                DisablePowerUp();
                _powerUpTracker.UpdateUI();
                return;
            }
            if (_powerUpTracker.slot2 == null)
            {
                _powerUpTracker.slot2 = powerups[5];
                //_powerUpTracker.slot2 = powerups[Random.Range(0, powerups.Length)];
                DisablePowerUp();
                _powerUpTracker.UpdateUI();
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
