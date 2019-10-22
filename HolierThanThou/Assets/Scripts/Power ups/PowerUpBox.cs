using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    public PowerUp[] powerups;

    [SerializeField]
    private PowerUpEditor PUE;

    private bool isDisabled;
    public bool IsDisabled {
        get {
            return isDisabled;
        }
    }

    [SerializeField]
    private float disableTimerStart = 5f;

    private float disableTimer;

    
    [SerializeField]
    private int itemNumber;


    private GameObject player;


    private void Start()
    {
        powerups = new PowerUp[9];

        powerups[0] = new BlastZone(false, PUE.BZ_hasDuration, PUE.BZ_duration , PUE.BZ_radius, PUE.BZ_power, PUE.BZ_upwardForce, PUE.BZ_playerPower, PUE.BZ_playerUpwardForce);
        powerups[1] = new Chillout(false, PUE.CO_hasDuration, PUE.CO_duration, PUE.CO_radius);
        powerups[2] = new GottaGoFast(true, PUE.GF_hasDuration, PUE.GF_duration, PUE.GF_radius, PUE.GF_aiSpeedMultiplier, PUE.GF_playerSpeedMultiplier);
        powerups[3] = new CantTouchThis(true, PUE.CTT_hasDuration, PUE.CTT_duration, PUE.CTT_radius);
        powerups[4] = new SneakySnake(true, PUE.SS_hasDuration, PUE.SS_duration, PUE.SS_radius);
        powerups[5] = new Thiccness(false, PUE.TH_hasDuration, PUE.TH_duration, PUE.TH_radius);
        powerups[6] = new BallsOfSteel(true, PUE.BS_hasDuration, PUE.BS_duration, PUE.BS_radius);
        powerups[7] = new ISeeDeadPeople(true, PUE.IS_hasDuration, PUE.IS_duration, PUE.IS_radius);
        powerups[8] = new CalmDown(false, PUE.CD_hasDuration, PUE.CD_duration, PUE.CD_radius, PUE.CD_aiSpeedMultiplier, PUE.CD_playerSpeedMultiplier);

        disableTimer = disableTimerStart;

        itemNumber = Mathf.Clamp(itemNumber, 0, (powerups.Length));
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
        itemNumber = Mathf.Clamp(itemNumber, 0, (powerups.Length));

        if (other.gameObject.tag == "Player")
        {

            if (_powerUpTracker.slot1 == null)
            {
                if ((itemNumber - 1) >= 0)
                {
                    _powerUpTracker.slot1 = powerups[itemNumber - 1];
                }
                else
                _powerUpTracker.slot1 = powerups[Random.Range(0, powerups.Length)] ;

                DisablePowerUp();
                _powerUpTracker.UpdateUI();
                return;
            }
            if (_powerUpTracker.slot2 == null)
            {
                if((itemNumber - 1) >= 0) 
                {
                    _powerUpTracker.slot2 = powerups[itemNumber -1];
                }
                else
                _powerUpTracker.slot2 = powerups[Random.Range(0, powerups.Length)];

                DisablePowerUp();
                _powerUpTracker.UpdateUI();
                return;
            }
            
        }
        else if(other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<AIStateMachine>();

            if (enemy.slot1 == null)
            {

                if ((itemNumber - 1) >= 0)
                {
                    enemy.slot1 = powerups[itemNumber - 1];
                }
                else
                    enemy.slot1 = powerups[Random.Range(0, powerups.Length)];

                DisablePowerUp();
                //_powerUpTracker.UpdateUI();
                return;
            }
            if (enemy.slot2 == null)
            {

                if ((itemNumber - 1) >= 0)
                {
                    enemy.slot2 = powerups[itemNumber - 1];
                }
                else
                    enemy.slot2 = powerups[Random.Range(0, powerups.Length)];
                DisablePowerUp();
                //_powerUpTracker.UpdateUI();
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
