using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTShove : MonoBehaviour
{
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        }
    }
}
