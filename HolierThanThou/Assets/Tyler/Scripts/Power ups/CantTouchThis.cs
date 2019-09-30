using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantTouchThis : PowerUp
{
    public CantTouchThis(bool _hasDuration, float _duration, float _radius) : base(_hasDuration, _duration, _radius)
    {

    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);
        Debug.Log("Can't Touch This! Power Up Used!");

    }

    
}
