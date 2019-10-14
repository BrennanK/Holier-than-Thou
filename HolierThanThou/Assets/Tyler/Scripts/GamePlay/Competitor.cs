using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : MonoBehaviour
{
    //Name and scoring Variables
    public string Name;
    public int Score;
    private bool scoredGoal;

    //Powerup constructor intakes
    public Transform origin;
    public bool navMeshOff;

    //Variables for power up effects
    public Material startingMat;
    public float blastedDuration;
    public bool untouchable;
    public bool inivisible;
    public bool ballOfSteel;
    

    private void Awake()
    {
        origin = this.transform;
        navMeshOff = false;
        untouchable = false;
        inivisible = false;
    }

    private void Update()
    {
       
    }

    public bool ScoredGoal
    {
        get { return scoredGoal; }

        set { scoredGoal = value; }
    }

    public void BallOfSteel(float duration, BounceFunction bounce)
    {
        StartCoroutine(Unbouncable(duration, bounce));
    }

    public void BeenBlasted()
    {
        StartCoroutine(TurnNavMeshBackOn(blastedDuration));
    }

    public void BeenChilled(float duration)
    {
        StartCoroutine(TurnNavMeshBackOn(duration));
    }

    public void CantTouchMe(float duration)
    {           
        StartCoroutine(Untouchable(duration));      
    }

    public void CantFindMe(float duration)
    {
        StartCoroutine(Invis(duration));
    }

    IEnumerator Invis(float duration)
    {
        inivisible = true;
        //GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(duration);
        inivisible = false;
        //GetComponent<MeshRenderer>().enabled = true;
    }

    private IEnumerator Untouchable(float duration)
    {
        untouchable = true;
        yield return new WaitForSeconds(duration);
        untouchable = false;

    }

    private IEnumerator Unbouncable(float duration, BounceFunction bounce)
    {
        ballOfSteel = true;
        bounce.enabled = false;
        yield return new WaitForSeconds(duration);
        ballOfSteel = false;
        bounce.enabled = true;

    }

    private IEnumerator TurnNavMeshBackOn(float duration)
    {
        yield return new WaitForSeconds(duration);
        navMeshOff = false;

    }

}
