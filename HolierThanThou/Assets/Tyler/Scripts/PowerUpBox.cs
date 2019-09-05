using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    public PowerUp[] powerups = new PowerUp[] { new Chillout(), new BlastZone() };

    private void Start()
    {
        Debug.Log(powerups[1]);
    }


    
}
