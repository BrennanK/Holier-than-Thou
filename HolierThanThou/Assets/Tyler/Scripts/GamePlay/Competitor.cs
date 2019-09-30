using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : MonoBehaviour
{
    public string Name;

    public int Score;

    private bool scoredGoal;

    public Transform origin;

    public bool naveMeshOff;

    public float blastedDuration;

    public bool untouchable;

    public Material startingMat;

    private void Awake()
    {
        origin = this.transform;
        naveMeshOff = false;
        untouchable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            naveMeshOff = !naveMeshOff;
            Debug.Log(naveMeshOff);
        }
    }

    public bool ScoredGoal
    {
        get { return scoredGoal; }

        set { scoredGoal = value; }
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

    private IEnumerator Untouchable(float duration)
    {
        untouchable = true;
        GetComponent<MeshRenderer>().material.color = Color.black;
        yield return new WaitForSeconds(duration);
        untouchable = false;
        GetComponent<MeshRenderer>().material = startingMat;

    }

    private IEnumerator TurnNavMeshBackOn(float duration)
    {
        yield return new WaitForSeconds(duration);
        naveMeshOff = false;
        GetComponent<MeshRenderer>().material = startingMat;

    }

}
