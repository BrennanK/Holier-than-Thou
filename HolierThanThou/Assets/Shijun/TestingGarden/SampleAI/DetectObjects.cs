using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    private AIFindPath aiFindPath;

    private void Start()
    {
        aiFindPath = gameObject.GetComponentInParent<AIFindPath>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name + " is detected");
        if (other.gameObject.tag == "Player")
        {
            aiFindPath.StartAttack();
            aiFindPath.Opponent = other.gameObject.transform;
        }
    }
}
