using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NeverStopRolling : MonoBehaviour
{

    Rigidbody myRB;
    float points;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        points += (myRB.mass * points) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            if(myRB.mass > .8f)
            {
                myRB.mass -= .1f;
            }

            Destroy(other.gameObject);

        }
        else if (other.CompareTag("Boost"))
        {
            Collider otherCollider = other.gameObject.GetComponent<Collider>();

            if (Vector3.Distance(otherCollider.bounds.center, transform.position) < 2f)
            {
                Debug.LogWarning("Perfect!");
                myRB.mass += .1f;
            }
            else if (Vector3.Distance(transform.position, other.transform.position) < 2.5f)
            {
                Debug.LogWarning("Good!");
                myRB.mass += .05f;
            }
            else
            {
                Debug.LogWarning("Good Enough");
            }

            Destroy(other.gameObject);
        }
    }
}
