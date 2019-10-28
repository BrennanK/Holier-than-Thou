using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotaltBallCarnage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            if(GetComponent<Rigidbody>().velocity.magnitude > collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
            {
                Destroy(collision.gameObject);

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
