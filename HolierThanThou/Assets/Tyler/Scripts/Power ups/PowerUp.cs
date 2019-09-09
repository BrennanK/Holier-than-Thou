using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp 
{
    public bool hasDuration;
    public float duration;
    public float radius;

    public virtual void CheckForDuration()
    {

    }


    public virtual void ActivatePowerUp()
    {
   
    }

    public virtual void ResetEffects()
    {

    }
}
