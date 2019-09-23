using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : MonoBehaviour
{
    public string Name;

    public int Score;

    private bool scoredGoal;

    public bool ScoredGoal
    {
        get { return scoredGoal; }

        set { scoredGoal = value; }

    }

}
