using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSpin : MonoBehaviour
{
    public float speed;
    


    
    void Update()
    {
        transform.Rotate(Vector3.up * speed, Space.Self);
    }
}
