using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantTouchThis : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("Can't Touch This! Power Up Used!");

    }

    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("Can't Touch This! Power Up Used!");

    }
}
