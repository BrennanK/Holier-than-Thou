using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thiccness : PowerUp
{

    public Thiccness(bool _hasDuration, float _duration, float _radius) : base(_hasDuration, _duration, _radius)
    {

    }



    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.transform.localScale = new Vector3(2, 2, 2);
        }

        Debug.Log("Thiccness Power Up Used!");

    }

    public override void ResetEffects(string name)
    {
        base.ResetEffects(name);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
