using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMassGain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool hitPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hitPlayer)
        {
            if(collision.gameObject.GetComponent<Rigidbody>().mass <= 1.4f)
            {
                collision.gameObject.GetComponent<Rigidbody>().mass += GetComponent<Rigidbody>().mass;
            }
            hitPlayer = true;
            StartCoroutine(WaitToPoof());
        }
    }
    
    IEnumerator WaitToPoof()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }
}
