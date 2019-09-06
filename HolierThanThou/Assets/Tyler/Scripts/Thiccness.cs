using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thiccness : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("Thiccness Power Up Used!");

    }

    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("Thiccness Power Up Used!");

    }

}
