using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBallRolling : MonoBehaviour
{
    //used for bonus money
    int numberOut;

    bool playerOut;
    bool gameCompleted;

    Competitor[] allPlayers;
    ScoreManager scoreManRef;

    public GameObject[] winnersCircle;

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
            other.transform.position = winnersCircle[numberOut].transform.position;
            numberOut++;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().angularDrag = 50;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<SphereCollider>().enabled = false;
            other.GetComponent<Bounce>().enabled = false;
            other.transform.rotation = Quaternion.Euler(Vector3.back);
            other.gameObject.GetComponentInChildren<HatFollowBody>().transform.rotation = Quaternion.Euler(new Vector3(0,-180,0));
            
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
            transform.position = new Vector3(0, 10, 0);
            LastBallRollingComplete();
        }
    }

    public void LastBallRollingComplete()
    {
        gameCompleted = true;
        
        FindObjectOfType<GameManager>().matchTimer = 3f;
        
    }
}
