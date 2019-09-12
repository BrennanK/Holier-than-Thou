using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private SpawnPointManager spawnPointManager;
    private ScoreManager scoreManager;

    private int point = 1;

    private void Start()
    {
        spawnPointManager = gameObject.GetComponent<SpawnPointManager>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var _competitor = other.gameObject.GetComponent<Competitor>();

        if(_competitor)
        {
            scoreManager.UpdateScore(_competitor.Name, point);
            Debug.Log(_competitor.Name + " is in the Goal!");
            spawnPointManager.RespawnPlayer();
        }
    }
}
