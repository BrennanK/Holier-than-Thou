using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp 
{
    public bool hasDuration;
    public float duration;
    public float radius;

    public PowerUp(bool _hasDuration, float _duration, float _radius)
    {
        hasDuration = _hasDuration;
        duration = _duration;
        radius = _radius;

    }

    public virtual void ActivatePowerUp(string name, Transform origin)
    {
   
    }

    public virtual void ResetEffects(string name)
    {

    }
}
