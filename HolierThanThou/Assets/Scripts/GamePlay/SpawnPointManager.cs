using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPointManager : MonoBehaviour
{
    private GameObject[] spawnPoints;
    Goal spm;

    public List<Competitor> players = new List<Competitor>();
    List<GameObject> startPoints = new List<GameObject>();
    List<AIStateMachine> AIDudes = new List<AIStateMachine>();

    private void Start()
    {
        spm = GetComponent<Goal>();
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            startPoints.Add(spawnPoints[i]);
        }

        foreach (Competitor player in GameObject.FindObjectsOfType<Competitor>())
        {
            players.Add(player);
        }

        List<AIStateMachine> AIDudes = FindObjectsOfType<AIStateMachine>().ToList();
        int currentIndex = 0;
        int bully = Random.Range(1, 3);
        int itemHog = Random.Range(1, 3);
        int dummies = Random.Range(1, 3);

        for(int i = 0; i < dummies; i++) {
            AIDudes[currentIndex].MakeDummy();
            currentIndex++;
        }
        
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
        foreach (Competitor player in players)
        {
            var rand = Random.Range(0, startPoints.Count);

            player.gameObject.transform.position = startPoints[rand].transform.position;
            player.gameObject.transform.rotation = startPoints[rand].transform.rotation;
            startPoints.Remove(startPoints[rand]);
            StartCoroutine(PauseRigidBodyControl(player, 4f));
        }

    }

    public void RespawnPlayer(string nameX)
    {
        var _competitior = players.Find(x => x.Name == nameX);
        int r = Random.Range(0, spawnPoints.Length);
        _competitior.transform.rotation = spawnPoints[r].transform.rotation;
        _competitior.transform.position = spawnPoints[r].transform.position;


        _competitior.ScoredGoal = false;
        spm.goal = false;
        if (nameX == GameObject.FindGameObjectWithTag("Player").GetComponent<Competitor>().Name)
        {
            FindObjectOfType<AudioManager>().Play("Respawn");
        }
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
            competitor.GetComponent<TrailRenderer>().enabled = false;
        }
        else
        {
            competitor.GetComponent<AIStateMachine>().enabled = false;
            competitor.GetComponent<TrailRenderer>().enabled = false;
        }
        yield return new WaitForSeconds(duration);

        if (competitor.GetComponent<RigidBodyControl>())
        {
            competitor.GetComponent<RigidBodyControl>().enabled = true;
            competitor.GetComponent<TrailRenderer>().enabled = true;

        }
        else
        {
            competitor.GetComponent<AIStateMachine>().enabled = true;
            if(competitor.inivisible == false)
            {
                competitor.GetComponent<TrailRenderer>().enabled = true;
            }

        }
    }

    public IEnumerator PauseCamera(Competitor competitor)
    {
        if (competitor.GetComponent<RigidBodyControl>())
        {
            competitor.isTerrain = true;
        }
        
        yield return new WaitForSeconds(1.8f);
        if (competitor.GetComponent<RigidBodyControl>())
        {
            Destroy(competitor.transform.GetChild(4).gameObject);
        }
        yield return new WaitForSeconds(0.2f);
        if (competitor.GetComponent<RigidBodyControl>())
        {
            Instantiate(competitor.transform.GetComponent<LoadCamera>().camera, competitor.transform);
            competitor.transform.GetComponentInChildren<Cinemachine.CinemachineFreeLook>().Follow = competitor.transform;
            competitor.transform.GetComponentInChildren<Cinemachine.CinemachineFreeLook>().LookAt = competitor.transform;
            competitor.isTerrain = false;
        }
    }
}
