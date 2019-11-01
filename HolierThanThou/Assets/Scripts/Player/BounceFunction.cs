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
        straightBounceDegree = sideBounceDegree = rBody.velocity.magnitude;
    }
    
    void LateUpdate()
    {
        /*
        // Bounce When Hitting Forward
        if (Physics.Raycast(transform.position, Vector3.forward, disToBouncable, bouncable))
        {
            rBody.AddForce(0.0f, 0.0f, -bounciness * straightBounceDegree, ForceMode.Impulse);
            Debug.Log($"Bounce Back with force {-bounciness * straightBounceDegree}");
            StartCoroutine(pauseControl());
        }  
        // Bounce When Hitting Backward
        else if (Physics.Raycast(transform.position, Vector3.back, disToBouncable, bouncable))
        {
            rBody.AddForce(0.0f, 0.0f, bounciness * straightBounceDegree, ForceMode.Impulse);
            Debug.Log($"Bounce Forward with force {bounciness * straightBounceDegree}");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Left
        else if (Physics.Raycast(transform.position, Vector3.left, disToBouncable, bouncable))
        {
            rBody.AddForce(bounciness * sideBounceDegree, 0.0f, 0.0f, ForceMode.Impulse);
            Debug.Log($"Bounce Right with force {bounciness * sideBounceDegree}");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Right
        else if (Physics.Raycast(transform.position, Vector3.right, disToBouncable, bouncable))
        {
            rBody.AddForce(-bounciness * sideBounceDegree, 0.0f, 0.0f, ForceMode.Impulse);
            Debug.Log($"Bounce Left with force {-bounciness * sideBounceDegree}");
            StartCoroutine(pauseControl());
        }
        //Bounce When Hitting Forward Right
        else if (Physics.Raycast(transform.position, Vector3.Lerp(Vector3.forward, Vector3.right, 0.6f), disToBouncable, bouncable))
        {
            rBody.AddForce(-bounciness/2 * sideBounceDegree, 0.0f, -bounciness/2 * straightBounceDegree, ForceMode.Impulse);
            Debug.Log($"Bounce left back with forces {-bounciness / 2 * sideBounceDegree} and {-bounciness / 2 * straightBounceDegree}");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Back Right
        else if (Physics.Raycast(transform.position, Vector3.Lerp(Vector3.back, Vector3.right, 0.6f), disToBouncable, bouncable))
        {
            rBody.AddForce(-bounciness / 2 * sideBounceDegree, 0.0f, bounciness / 2 * straightBounceDegree, ForceMode.Impulse);
            Debug.Log($"Bounce left front with forces {-bounciness / 2 * sideBounceDegree} and {bounciness / 2 * straightBounceDegree}");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Forward Left
        else if (Physics.Raycast(transform.position, Vector3.Lerp(Vector3.forward, Vector3.left, 0.6f), disToBouncable, bouncable))
        {
            rBody.AddForce(bounciness / 2 * sideBounceDegree, 0.0f, -bounciness / 2 * straightBounceDegree, ForceMode.Impulse);
            Debug.Log($"Bounce right back with forces {bounciness / 2 * sideBounceDegree} and {-bounciness / 2 * straightBounceDegree}");
            StartCoroutine(pauseControl());
        }
        // Bounce When Hitting Back Left
        else if (Physics.Raycast(transform.position, Vector3.Lerp(Vector3.back, Vector3.left, 0.6f), disToBouncable, bouncable))
        {
            rBody.AddForce(bounciness / 2 * sideBounceDegree, 0.0f, bounciness / 2 * straightBounceDegree, ForceMode.Impulse);
            Debug.Log($"Bounce right front with {bounciness / 2 * sideBounceDegree} and {bounciness / 2 * straightBounceDegree}");
            StartCoroutine(pauseControl());
        }
        */

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


