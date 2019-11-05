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
    public bool goal;
    public string goalName;

    public float radius;
    public float disToGround;
    public float power;
    public float upwardForce;
    public float playerPower;
    public float playerUpwardForce;
    public LayerMask ground;
    Vector3 explodePosition;

    private float point;

    private void Start()
    {
        spawnPointManager = gameObject.GetComponent<SpawnPointManager>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
        //powerUp = new BlastZone(false, powerUpEditor.BZ_hasDuration, powerUpEditor.BZ_duration, radius, power, upwardForce);
        explodePosition = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        var _competitor = other.gameObject.GetComponent<Competitor>();
        if(other.tag == "Player")
        {
            goal = true;
        }
        if(other.tag == "Enemy")
        {
            goalName = other.GetComponent<Competitor>().name;
        }

        if (_competitor)
        {
            pointTracker = _competitor.GetComponentInParent<PointTracker>();
            point = pointTracker.PointVal();
            _competitor.ScoredGoal = true;
            _competitor.GetComponentInParent<Crown>().resetCrown();
            pointTracker.ResetMult();
            pointTracker.ResetBasePoints();
            scoreManager.UpdateScore(_competitor.Name, (int)point);
            StartCoroutine(spawnPointManager.RespawnTimer(_competitor.Name));
            StartCoroutine(spawnPointManager.PauseRigidBodyControl(_competitor, 2f));
            StartCoroutine(spawnPointManager.PauseCamera(_competitor));
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

                if (enemy.tag == "Player")
                {
                    if(goal != true)
                    {
                        rb.AddExplosionForce(playerPower, explodePosition, radius, playerUpwardForce, ForceMode.Impulse);
                    }
                    
                }
                else
                {
                    if(enemy.GetComponent<Competitor>().name != goalName)
                    {
                        competitor.navMeshOff = true;
                        rb.AddExplosionForce(power, explodePosition, radius, upwardForce, ForceMode.Impulse);

                        if (Physics.Raycast(competitor.transform.position, Vector3.down, disToGround, ground) == true)
                        {
                            competitor.BeenBlasted();
                        }
                    }
                    
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(explodePosition, new Vector3(1, 1, 1));
    }
}
