using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingWall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<LastBallRolling>())
        {
            collision.gameObject.GetComponent<LastBallRolling>().TakeDamage();
        }
    }
}
