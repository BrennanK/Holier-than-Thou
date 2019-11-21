using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool goal;
    public string goalName;

    public float duration = 0.8f;
    public float radius;
    public float disToGround;
    public float power;
    public float upwardForce;
    public float playerPower;
    public float playerUpwardForce;
    public LayerMask ground;
    Vector3 explodePosition;

    private SpawnPointManager spawnPointManager;
    private ScoreManager scoreManager;
    private PointTracker pointTracker;
	private ExplosionEffect explosion;
	private float explosionYOffset = 3.8f;
    private float point;

    private void Start()
    {
        spawnPointManager = gameObject.GetComponent<SpawnPointManager>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
        explodePosition = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
		explosion = gameObject.AddComponent<ExplosionEffect>();
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
            if(other.tag == "Player")
            {
                var playerAchievementTracker = other.GetComponent<PlayerAchievementTracker>();
                var crowntracker = other.GetComponent<Crown>();

                if(point >= 100)
                {
                    playerAchievementTracker.playerDoDaBump = true;
                    playerAchievementTracker.playerMoBump = true;
                    playerAchievementTracker.playerBump = true;
                }
                if(point == 66)
                {
                    playerAchievementTracker.playerGeorgiaGoals += 1;
                    playerAchievementTracker.playerMoBump = true;
                    playerAchievementTracker.playerBump = true;
                }
                else if (point >= 50)
                {
                    playerAchievementTracker.playerMoBump = true;
                    playerAchievementTracker.playerBump = true;
                }
                else if (point >= 10)
                {
                    playerAchievementTracker.playerBump = true;
                }

                if (crowntracker.getCrownVal() >= 5)
                {
                    playerAchievementTracker.playerAllHail = true;
                    playerAchievementTracker.playerMakeWay = true;
                }
                else if(crowntracker.getCrownVal() >= 1)
                {
                    playerAchievementTracker.playerMakeWay = true;
                }             
            }
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
        List<Collider> enemies = Physics.OverlapSphere(transform.position, radius).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].GetComponent<Competitor>() || enemies[i].transform == transform)
            {
                enemies.Remove(enemies[i]);
                i--;
            }
		}
		//explosion.StartExplosion(duration, Vector3.zero, "PE_GoalExplosion", explosionYOffset);
		explosion.StartExplosion(duration, Vector3.zero, "PE_BlastOut");
		explosion.StartExplosion(duration, Vector3.zero, "PE_BlastOut");
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
                        competitor.GetComponent<AIStateMachine>().enabled = false;
                        rb.AddExplosionForce(power, explodePosition, radius, upwardForce, ForceMode.Impulse);
                        competitor.Blast(competitor.transform, duration);

                    }
                    
                }
            }
        }
    }
}
