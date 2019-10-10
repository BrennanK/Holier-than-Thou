using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTGRAVITY : MonoBehaviour
{
    public float pull;
    Rigidbody myRb;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {

            float _tempDis = Vector3.Distance(transform.position, other.transform.position);
            float _tempForce = (0.0000000667f * (myRb.mass * other.GetComponent<Rigidbody>().mass)) / (_tempDis * _tempDis);


            other.GetComponent<Rigidbody>().AddForce((transform.position - other.transform.position) * _tempForce);
        }


    }
}
