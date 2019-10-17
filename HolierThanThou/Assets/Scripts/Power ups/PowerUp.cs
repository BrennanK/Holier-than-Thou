using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp 
{
    public bool hasDuration;
    public float duration;
    public float radius;
    public bool isEnhancement;

    public PowerUp(bool _isEnhancement, bool _hasDuration, float _duration, float _radius)
    {
        isEnhancement = _isEnhancement;
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
