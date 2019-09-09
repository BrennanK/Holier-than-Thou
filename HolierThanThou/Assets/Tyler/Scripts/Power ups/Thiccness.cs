using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thiccness : PowerUp
{

    public override void CheckForDuration()
    {
        base.CheckForDuration();
        hasDuration = true;
        duration = 5f;
    }

    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.transform.localScale = new Vector3(2, 2, 2);
        }

        Debug.Log("Thiccness Power Up Used!");

    }

    public override void ResetEffects()
    {
        base.ResetEffects();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
