using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    private GameObject[] spawnPoints;


    public List<Competitor> players = new List<Competitor>();
    List<GameObject> startPoints = new List<GameObject>();

    private void Start()
    {
        
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            startPoints.Add(spawnPoints[i]);
        }

        foreach (Competitor player in GameObject.FindObjectsOfType<Competitor>())
        {
            players.Add(player);
        }

        SpawnPlayers();
        
    }

    private void SpawnPlayers()
    {
        foreach(Competitor player in players)
        {
            var rand = Random.Range(0, startPoints.Count);

            player.gameObject.transform.position = startPoints[rand].transform.position;
            startPoints.Remove(startPoints[rand]);
            StartCoroutine(PauseRigidBodyControl(player, 4f));
        }
        
    }

    public void RespawnPlayer(string nameX)
    {
        var _competitior = players.Find(x => x.Name == nameX);
        _competitior.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        _competitior.ScoredGoal = false;
    }

    public IEnumerator RespawnTimer(string name)
    {
        yield return new WaitForSeconds(2f);
        RespawnPlayer(name);
    }

    public IEnumerator PauseRigidBodyControl(Competitor competitor, float duration)
    {
        if (competitor.GetComponent<RigidBodyControl>())
        {
            competitor.GetComponent<RigidBodyControl>().enabled = false;
        }
        else
        {
            competitor.GetComponent<AIBehavior>().enabled = false;
        }
        yield return new WaitForSeconds(duration);
        if (competitor.GetComponent<RigidBodyControl>())
        {
            competitor.GetComponent<RigidBodyControl>().enabled = true;
            
        }
        else
        {
            competitor.GetComponent<AIBehavior>().enabled = true;
        }
    }
}
