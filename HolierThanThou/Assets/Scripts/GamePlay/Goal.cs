using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private SpawnPointManager spawnPointManager;
    private ScoreManager scoreManager;
    [SerializeField]
    private PowerUpEditor powerUpEditor;
    private PowerUp powerUp;
    private PointTracker pointTracker;
    

    public float radius;
    public float disToGround;
    public float power;
    public float upwardForce;
    public float playerPower;
    public LayerMask ground;

    private int point;

    private void Start()
    {
        spawnPointManager = gameObject.GetComponent<SpawnPointManager>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
        powerUp = new BlastZone(false, powerUpEditor.BZ_hasDuration, powerUpEditor.BZ_duration, radius, power, upwardForce);
    }


    private void OnTriggerEnter(Collider other)
    {
        var _competitor = other.gameObject.GetComponent<Competitor>();

        if(_competitor)
        {
            pointTracker = _competitor.GetComponentInParent<PointTracker>();
            point = pointTracker.PointVal();
            _competitor.ScoredGoal = true;
            pointTracker.ResetMult();
            pointTracker.ResetBasePoints();
            scoreManager.UpdateScore(_competitor.Name, point);
            StartCoroutine(spawnPointManager.RespawnTimer(_competitor.Name));
            Explosion();
        }

    }

    void Explosion()
    {
        //powerUp.ActivatePowerUp(gameObject.name, this.transform);

        List<Collider> enemies = Physics.OverlapSphere(transform.position, radius).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].GetComponent<Competitor>() || enemies[i].transform == transform)
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


                if(enemy.tag == "Player")
                {
                    rb.AddExplosionForce(playerPower, transform.position, radius, upwardForce, ForceMode.Impulse);
                }
                else
                {
                    competitor.navMeshOff = true;
                    rb.AddExplosionForce(power, transform.position, radius, upwardForce, ForceMode.Impulse);

                    if (Physics.Raycast(competitor.transform.position, Vector3.down, disToGround, ground) == true)
                    {
                        competitor.BeenBlasted();
                    }
                }


                    
                
            }
        }
    }
}
