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

    public float radius;
    public float disToGround;
    public float power;
    public float upwardForce;
    public float playerPower;
    public float playerUpwardForce;
    public LayerMask ground;
    Vector3 explodePosition;

    private int point = 1;

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

        if (_competitor)
        {
            _competitor.ScoredGoal = true;
            scoreManager.UpdateScore(_competitor.Name, point);
            StartCoroutine(spawnPointManager.RespawnTimer(_competitor.Name));
            StartCoroutine(spawnPointManager.PauseRigidBodyControl(_competitor, 2f));
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
                    rb.AddExplosionForce(playerPower, explodePosition, radius, playerUpwardForce, ForceMode.Impulse);
                }
                else
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

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(explodePosition, new Vector3(1, 1, 1));
    }
}
