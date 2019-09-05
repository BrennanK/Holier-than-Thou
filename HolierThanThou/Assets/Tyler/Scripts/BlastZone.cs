using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("Blast Zone Power Up Used!");
        
    }

    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("Blast Zone Power Up Used!");
    }
}
