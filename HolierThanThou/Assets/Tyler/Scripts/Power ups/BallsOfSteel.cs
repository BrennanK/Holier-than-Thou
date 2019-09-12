using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsOfSteel : PowerUp
{
    public BallsOfSteel(bool _hasDuration, float _duration, float _radius) : base(_hasDuration, _duration, _radius)
    {

    }

    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("Balls of Steel Power Up Used!");

    }

    
}
