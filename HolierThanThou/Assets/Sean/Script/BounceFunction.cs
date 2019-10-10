using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFunction : MonoBehaviour
{   
    //Bounce Variables
    public float straightBounceDegree, verticalBounceDegree, sideBounceDegree;
    public float disToBouncable;
    public float bounciness;
    public LayerMask bouncable;
    //Rigid Body for Speed
    Rigidbody rBody;
    //Timer for re-enabling Rigidbody Control after bounce
    public float timer;


    void Start()
    {
        //Initiate Rigidbody
        rBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //Using Player Velocity to determine how hard to bounce
        straightBounceDegree = sideBounceDegree = rBody.velocity.magnitude * bounciness;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        // Bounce When Hitting Forward
        if (Physics.Raycast(transform.position, Vector3.forward, disToBouncable, bouncable))
        {
            rBody.AddForce(0.0f, 0.0f, -1f * straightBounceDegree, ForceMode.Impulse);
            Debug.Log("Bounce Back");
            StartCoroutine(pauseControl());
        }  
        // Bounce When Hitting Backward
        else if (Physics.Raycast(transform.position, Vector3.back, disToBouncable, bouncable))
        {
            rBody.AddForce(0.0f, 0.0f, 1f * straightBounceDegree, ForceMode.Impulse);
            Debug.Log("Bounce Forward");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Left
        else if (Physics.Raycast(transform.position, Vector3.left, disToBouncable, bouncable))
        {
            rBody.AddForce(1f * sideBounceDegree, 0.0f, 0.0f, ForceMode.Impulse);
            Debug.Log("Bounce Right");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Right
        else if (Physics.Raycast(transform.position, Vector3.right, disToBouncable, bouncable))
        {
            rBody.AddForce(-1f * sideBounceDegree, 0.0f, 0.0f, ForceMode.Impulse);
            Debug.Log("Bounce Left");
            StartCoroutine(pauseControl());
        }
        //Bounce When Hitting Forward Right
        else if (Physics.Raycast(transform.position, Vector3.Lerp(Vector3.forward, Vector3.right, 0.6f), disToBouncable, bouncable))
        {
            rBody.AddForce(-1f * sideBounceDegree/2, 0.0f, -1f * straightBounceDegree/2, ForceMode.Impulse);
            Debug.Log("Bounce left back");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Back Right
        else if (Physics.Raycast(transform.position, Vector3.Lerp(Vector3.back, Vector3.right, 0.6f), disToBouncable, bouncable))
        {
            rBody.AddForce(-1f * sideBounceDegree / 2, 0.0f, 1f * straightBounceDegree / 2, ForceMode.Impulse);
            Debug.Log("Bounce left front");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Forward Left
        else if (Physics.Raycast(transform.position, Vector3.Lerp(Vector3.forward, Vector3.left, 0.6f), disToBouncable, bouncable))
        {
            rBody.AddForce(1f * sideBounceDegree / 2, 0.0f, -1f * straightBounceDegree / 2, ForceMode.Impulse);
            Debug.Log("Bounce right back");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Back Left
        else if (Physics.Raycast(transform.position, Vector3.Lerp(Vector3.back, Vector3.left, 0.6f), disToBouncable, bouncable))
        {
            rBody.AddForce(1f * sideBounceDegree / 2, 0.0f, 1f * straightBounceDegree / 2, ForceMode.Impulse);
            Debug.Log("Bounce right front");
            StartCoroutine(pauseControl());
        }

    }
    // Disable and re-enable RigidBodyControl after bounce
    IEnumerator pauseControl()
    {
        if (transform.GetComponent<RigidBodyControl>()) {
            transform.GetComponent<RigidBodyControl>().enabled = false;
            yield return new WaitForSeconds(timer);
            transform.GetComponent<RigidBodyControl>().enabled = true;
        }
    }

    
}


