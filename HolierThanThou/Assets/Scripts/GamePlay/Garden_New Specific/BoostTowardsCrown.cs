using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTowardsCrown : MonoBehaviour
{
    private Rigidbody rb;
    public float launchValue;
    public string Name;
    private Transform Target;
    private GameObject boosty;
 
    //Dylan Wrote This

    //Note: LaunchValue works backwards here: this will launch you DIRECTLY towards a crown; launch value reduces the value of that vector 

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find(Name).transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
            if (other.GetComponentInParent<Competitor>())
            {
                rb = other.GetComponentInParent<Rigidbody>();
                var heading = Target.GetComponent<Transform>().position - rb.GetComponentInParent<Transform>().position;
                rb.velocity = new Vector3(heading[0] / launchValue, heading[1] / launchValue, heading[2] / launchValue);
            }
    }
}