using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmDown : PowerUp
{
    float speedMultiplier;

    public CalmDown(bool _hasDuration, float _duration, float _radius, float _speedMultiplier) : base(_hasDuration, _duration, _radius)
    {
        speedMultiplier = _speedMultiplier;
    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);
        Debug.Log("Calm Down Power Up Used!");

    }

   
}
