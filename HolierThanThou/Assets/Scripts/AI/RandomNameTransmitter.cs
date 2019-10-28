using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameTransmitter : RandomNameGenerator
{

    public GameObject[] enemies;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Competitor>().Name = GetRandomName();
        }
    }

    private void AssignNamesForEnemies()
    {
        
    }
}
