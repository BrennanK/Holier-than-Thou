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
                GameObject instantiatedPoofRefrence = Instantiate(poof, other.transform.position, poof.transform.rotation);
                instantiatedPoofRefrence.AddComponent<Destroyer>();
                instantiatedPoofRefrence.GetComponent<Destroyer>().gameObjectLifeTime = 1f;
            }
            Destroy(other.gameObject);
        }
    }
}
