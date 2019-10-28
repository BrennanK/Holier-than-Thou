using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanosGlove : MonoBehaviour
{
    public GameObject poof;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disintegrate"))
        {
            if (poof.gameObject != null)
            {
                Instantiate(poof, other.transform.position, Quaternion.Euler(Vector3.zero));
            }
            Destroy(other.gameObject);
        }
    }
}
