using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsOfSteel : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("Balls of Steel Power Up Used!");

    }

    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("Balls of Steel Power Up Used!");

    }
}
