using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class BlastZone : PowerUp
{
    private float power;
    private float upwardForce;




    public BlastZone(bool _hasDuration, float _duration, float _radius, float _power, float _upwardForce) : base(_hasDuration, _duration, _radius)
    {
        power = _power;
        upwardForce = _upwardForce;
    }

    public override void ActivatePowerUp(string name, Transform origin)
    {
        base.ActivatePowerUp(name, origin);



        List<Collider> enemies = Physics.OverlapSphere(origin.position, radius).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            if(!enemies[i].GetComponent<Competitor>() || enemies[i].transform == origin)
            {
                enemies.Remove(enemies[i]);
                i--;
            }
        }
        if(enemies.Count == 0)
        {
            return;
        }
        else
        {
            foreach(Collider enemy in enemies)
            {
                var rb = enemy.GetComponent<Rigidbody>();
                var competitor = enemy.GetComponent<Competitor>();

                if (!competitor.untouchable)
                {
                    enemy.GetComponent<MeshRenderer>().material.color = Color.red;
                    competitor.navMeshOff = true;
                    competitor.BeenBlasted();
                    rb.AddExplosionForce(power, origin.position, radius, upwardForce);
                }
            }
        }








        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (var enemy in enemies)
        //{
        //    var rb = enemy.GetComponent<Rigidbody>();

        //    if (radius >= Vector3.Distance(player.transform.position, enemy.transform.position))
        //    {
        //        enemy.GetComponent<MeshRenderer>().material.color = Color.red;
        //        rb.AddExplosionForce(power, player.transform.position, radius, 0);
        //    }
                
        //}

        Debug.Log("Blast Zone Power Up Used!");
        
    }
}
