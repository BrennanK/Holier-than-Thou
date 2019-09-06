using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmDown : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("Calm Down Power Up Used!");

    }

    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("Calm Down Power Up Used!");

    }
}
