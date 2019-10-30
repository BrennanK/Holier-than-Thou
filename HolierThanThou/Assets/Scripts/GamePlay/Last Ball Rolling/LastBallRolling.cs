using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBallRolling : MonoBehaviour
{
    int health = 3;
    
    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
