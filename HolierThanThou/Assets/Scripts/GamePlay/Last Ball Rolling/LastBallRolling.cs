using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBallRolling : MonoBehaviour
{
    //used for bonus money
    int numberOut;
    float xOffset;
    float yOffset;
    float zOffset;

    bool playerOut;
    bool gameCompleted;

    Competitor[] allPlayers;
    ScoreManager scoreManRef;
    

    private void Start()
    {
        allPlayers = new Competitor[8];
        allPlayers = FindObjectsOfType<Competitor>();
        scoreManRef = FindObjectOfType<ScoreManager>();
        numberOut = 0;
        playerOut = false;
        gameCompleted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bounce>())
        {
            //turn off a whole bunch of stuff to stop errors from poping up
            numberOut++;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().angularDrag = 50;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<SphereCollider>().enabled = false;
            other.transform.position = new Vector3(xOffset, -200 + yOffset, zOffset);
            xOffset += 3f;
            other.GetComponent<Bounce>().enabled = false;
            
            for(int i = 0; i < allPlayers.Length; i++)
            {
                if(allPlayers[i] != null && allPlayers[i].Name == other.GetComponent<Competitor>().Name)
                {
                    allPlayers[i] = null;
                }
                else
                {
                    if(allPlayers[i] != null)
                    {
                        allPlayers[i].Score += 1;
                        scoreManRef.UpdateScore(allPlayers[i].Name, allPlayers[i].Score);
                    }
                }
            }

            if (other.GetComponent<AIStateMachine>())
            {
                other.GetComponent<AIStateMachine>().enabled = false;
            }

            if (other.GetComponent<RigidBodyControl>())
            {
                other.GetComponent<Gravity>().enabled = false;
                other.GetComponent<RigidBodyControl>().enabled = false;
                other.GetComponentInChildren<Camera>().enabled = false;
                playerOut = true;
            }
        }

        if(numberOut >= 7)
        {
            LastBallRollingComplete();
        }
    }

    public void LastBallRollingComplete()
    {
        gameCompleted = true;
        if(FindObjectOfType<GameManager>().matchTimer > 3f)
        {
            FindObjectOfType<GameManager>().matchTimer = 3f;
        }

    }
}
