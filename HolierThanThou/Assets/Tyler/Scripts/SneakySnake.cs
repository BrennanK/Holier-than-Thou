using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakySnake : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("Sneaky Snake Power Up Used!");

    }

    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("Sneaky Snake Power Up Used!");

    }
}
