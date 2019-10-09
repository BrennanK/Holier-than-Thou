using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var AI = other.gameObject.GetComponent<AIBehavior>();

        if (AI)
        {
            AI.EnteredGoalArea();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var AI = other.gameObject.GetComponent<AIBehavior>();

        if(AI)
        {
            AI.ExitedGoalArea();
        }
    }
}
