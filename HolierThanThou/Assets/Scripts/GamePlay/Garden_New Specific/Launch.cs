using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    private Competitor competitor;
    private Rigidbody rb;
    public float launchValue;
    private Goal goal;
    

    // Start is called before the first frame update
    void Start()
    {
        goal = FindObjectOfType<Goal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.gameRunning)
        if (other.GetComponentInParent<Competitor>())
        {
            rb = other.GetComponentInParent<Rigidbody>();
            var heading = goal.GetComponent<Transform>().position - rb.GetComponentInParent<Transform>().position; 
            rb.velocity = new Vector3(heading[0]/(5/2), heading[1]/(5/2), heading[2]/(5/2));
        }
    }
}
