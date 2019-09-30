using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISeeDeadPeople : PowerUp
{
    public ISeeDeadPeople(bool _hasDuration, float _duration, float _radius) : base(_hasDuration, _duration, _radius)
    {

    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);
        Debug.Log("I See Dead People Power Up Used!");

    }

    
}
