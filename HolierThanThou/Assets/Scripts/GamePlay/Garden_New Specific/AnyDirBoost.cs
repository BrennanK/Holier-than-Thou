using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDirBoost : MonoBehaviour
{
    private Rigidbody rb;
    public float boostVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        rb = other.GetComponentInParent<Rigidbody>();
        rb.AddForce(rb.velocity * boostVal);
    }
}
