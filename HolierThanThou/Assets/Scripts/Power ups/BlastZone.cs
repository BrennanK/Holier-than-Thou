using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class BlastZone : PowerUp
{
    private float power;
    private float upwardForce;
    private LayerMask ground;
    private float disToGround;
    private float playerPower;
    private float playerUpwardForce;




    public BlastZone(bool _isEnhancement, bool _hasDuration, float _duration, float _radius, float _power, float _upwardForce) : base(_isEnhancement, _hasDuration, _duration, _radius)
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

                    if (enemy.tag == "Player")
                    {
                        rb.AddExplosionForce(playerPower, origin.position, radius, playerUpwardForce, ForceMode.Impulse);
                    }
                    else
                    {
                        competitor.navMeshOff = true;
                        rb.AddExplosionForce(power, origin.position, radius, upwardForce, ForceMode.Impulse);

                        if (Physics.Raycast(competitor.transform.position, Vector3.down, disToGround, ground) == true)
                        {
                            competitor.BeenBlasted();
                        }
                    }
                }
            }
        }

        Debug.Log("Blast Zone Power Up Used by " + name);
        
    }


}
