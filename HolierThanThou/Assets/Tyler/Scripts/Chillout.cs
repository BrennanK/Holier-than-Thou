using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chillout : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        Debug.Log("Chillout Power Up Used!");
        
    }

    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("Chillout Power Up Used!");
  
    }
}
