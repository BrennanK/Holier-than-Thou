using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownBoxScript : MonoBehaviour
{
    private List<GameObject> CrownSpawns;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject crownBoxSpawn in GameObject.FindGameObjectsWithTag("CrownSpawn"))
        {
            CrownSpawns.Add(crownBoxSpawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RespawnCheck();
    }

    void RespawnCheck()
    {
        var crownSpawn = CrownSpawns[Random.Range(0, 2)];
        var crownBox = GameObject.FindGameObjectWithTag("CrownBox");
        
        if (crownBox = null)
        {
            var spawnLoc = crownSpawn.GetComponent<Vector3>();
            var spawnRot = crownSpawn.GetComponent<Quaternion>();
            Instantiate(crownBox, spawnLoc, spawnRot);  
        }
        else
        {
            return;
        }
    }
}
