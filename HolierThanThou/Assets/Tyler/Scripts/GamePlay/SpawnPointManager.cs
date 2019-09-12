using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    private GameObject[] spawnPoints;

    private GameObject player;

    List<GameObject> startPoints = new List<GameObject>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            startPoints.Add(spawnPoints[i]);
        }
        Debug.Log("Number of Spawnpoints: " + spawnPoints.Length);
        SpawnPlayers();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    private void SpawnPlayers()
    {
        player.transform.position = startPoints[Random.Range(0, startPoints.Count)].transform.position;
    }

    public void RespawnPlayer()
    {

        player.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }



}
