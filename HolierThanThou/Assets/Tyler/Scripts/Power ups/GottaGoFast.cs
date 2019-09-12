using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GottaGoFast : PowerUp
{ 
    private float playerStartSpeed;
    private float speedMultiplier;

    public GottaGoFast(bool _hasDuration, float _duration, float _radius, float _speedMultiplier) : base(_hasDuration, _duration, _radius)
    {
        speedMultiplier = _speedMultiplier;
    }


    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickPlayerExample>();
        player.speed = player.speed + (player.speed * speedMultiplier);

        Debug.Log("Gotta go fast! Power Up Used!");

    }


    public override void ResetEffects()
    {
        base.ResetEffects();

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickPlayerExample>();
        player.speed = playerStartSpeed;
    }

}
