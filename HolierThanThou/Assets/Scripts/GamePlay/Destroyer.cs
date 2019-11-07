using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float gameObjectLifeTime;
    
    // Update is called once per frame
    void Update()
    {
        gameObjectLifeTime -= Time.deltaTime;
        if(gameObjectLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
