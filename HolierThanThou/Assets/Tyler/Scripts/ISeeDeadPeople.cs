using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISeeDeadPeople : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("I See Dead People Power Up Used!");

    }

    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("I See Dead People Power Up Used!");

    }
}
