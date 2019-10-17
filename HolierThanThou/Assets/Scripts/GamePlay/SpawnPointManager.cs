using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPointManager : MonoBehaviour
{
    private GameObject[] spawnPoints;



    public List<Competitor> players = new List<Competitor>();
    List<GameObject> startPoints = new List<GameObject>();
    List<AIBehavior> AIDudes = new List<AIBehavior>();

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

        List<AIBehavior> AIDudes = FindObjectsOfType<AIBehavior>().ToList();
        int currentIndex = 0;
        int bully = Random.Range(1, 3);
        int itemHog = Random.Range(1, 3);
        
        for(int i = 0; i < bully; i++)
        {
            AIDudes[currentIndex].MakeBully();
            currentIndex++;
        }

        for(int i = 0; i < itemHog; i++)
        {
            AIDudes[currentIndex].MakeItemHog();
            currentIndex++;
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


}
