using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Chillout : PowerUp
{
    public Chillout(bool _isEnhancement, bool _hasDuration, float _duration, float _radius) : base(_isEnhancement, _hasDuration, _duration, _radius)
    {

    }



    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);

        List<Collider> enemies = Physics.OverlapSphere(origin.position, radius).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].GetComponent<Competitor>() || enemies[i].transform == origin)
            {
                enemies.Remove(enemies[i]);
                i--;
            }
        }
        if (enemies.Count == 0)
        {
            return;
        }
        else
        {
            foreach (Collider enemy in enemies)
            {
                var rb = enemy.GetComponent<Rigidbody>();
                var competitor = enemy.GetComponent<Competitor>();

                
                competitor.navMeshOff = true;
                if (competitor.navMeshOff == true)
                {
                    rb.velocity = Vector3.zero;
                }
                competitor.BeenChilled(duration);
            }
        }

        Debug.Log("Chillout power up used by " + name);

    }
}
