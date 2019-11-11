using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTBELT : MonoBehaviour
{
    public float push;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DisMineExplosion>())
        {
            return;
        }
        else
        {
            Vector3 _temp = other.GetComponent<Rigidbody>().velocity;

            other.GetComponent<Rigidbody>().velocity = _temp + (transform.forward * push);
        }
    }
}
