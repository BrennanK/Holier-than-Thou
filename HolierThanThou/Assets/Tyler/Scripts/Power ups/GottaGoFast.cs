using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GottaGoFast : PowerUp
{ 
    private float playerStartSpeed;
    private float playerNewSpeed;


    public override void CheckForDuration()
    {
        base.CheckForDuration();
        hasDuration = true;
        duration = 5f;
    }


    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickPlayerExample>();
        playerStartSpeed = player.speed;
        player.speed = player.speed + (player.speed * .25f);

        Debug.Log("Gotta go fast! Power Up Used!");

    }


    public override void ResetEffects()
    {
        base.ResetEffects();

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickPlayerExample>();
        player.speed = playerStartSpeed;
    }

}
