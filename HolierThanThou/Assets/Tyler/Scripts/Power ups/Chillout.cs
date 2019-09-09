using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chillout : PowerUp
{
    public override void CheckForDuration()
    {
        base.CheckForDuration();
        hasDuration = false;
        radius = 10f;
    }

    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();

        var player = GameObject.FindGameObjectWithTag("Player");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            var rb = enemy.GetComponent<Rigidbody>();

            if (radius >= Vector3.Distance(player.transform.position, enemy.transform.position))
            {
                enemy.GetComponent<MeshRenderer>().material.color = Color.blue;                
            }

        }
        Debug.Log("Chillout Power Up Used!");
        
    }

    
}
