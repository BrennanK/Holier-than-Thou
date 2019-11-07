using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DisMineExplosion : MonoBehaviour
{

    public PowerUpEditor PUE;

    private void OnTriggerEnter(Collider other)
    {
        var _competitor = other.gameObject.GetComponent<Competitor>();

        if (_competitor)
        {
            Explosion();
        }
    }

    void Explosion()
    {
        List<Collider> enemies = Physics.OverlapSphere(this.gameObject.transform.position, PUE.DM_radius).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].GetComponent<Competitor>() || enemies[i].transform == this.gameObject.transform)
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

                if (!competitor.untouchable)
                {

                    if (enemy.tag == "Player")
                    {
                        rb.AddExplosionForce(PUE.DM_playerPower, transform.position, PUE.DM_radius, PUE.DM_playerUpwardForce, ForceMode.Impulse);
                        Destroy(transform.parent.gameObject);
                    }
                    else
                    {
                        competitor.navMeshOff = true;
                        rb.AddExplosionForce(PUE.DM_power, transform.position, PUE.DM_radius, PUE.DM_upwardForce, ForceMode.Impulse);
                        Destroy(transform.parent.gameObject);

                        if (Physics.Raycast(competitor.transform.position, Vector3.down, PUE.DM_disToGround, PUE.DM_ground) == true)
                        {
                            competitor.BeenBlasted();
                        }
                    }
                }
            }
        }
    }
}
