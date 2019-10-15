using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVolume : MonoBehaviour
{
    Competitor[] players;
    Vector3[] startPoints;
    // Start is called before the first frame update
    void Start()
    {
        players = new Competitor[FindObjectsOfType<Competitor>().Length];
        players = FindObjectsOfType<Competitor>();

        startPoints = new Vector3[players.Length];
        for(int i = 0; i < startPoints.Length; i++)
        {
            startPoints[i] = players[i].transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < startPoints.Length; i++)
        {
            if(other.gameObject == players[i].gameObject)
            {
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.transform.position = startPoints[i];
            }
        }
    }
}
