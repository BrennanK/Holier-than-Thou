using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanosWall : MonoBehaviour
{

    public GameObject center;

    Vector3 pushBack;

    // Start is called before the first frame update
    void Start()
    {
        pushBack = center.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Competitor>())
        {
            collision.gameObject.GetComponent<Competitor>().TakeDamage();
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(pushBack * 50);
        }
    }
}
