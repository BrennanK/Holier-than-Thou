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
    public float blastedDuration;
    public bool untouchable;
    public bool inivisible;
    public bool ballOfSteel;
    public Material startMaterial;

    public int health = 3;

    TrailRenderer trails;
    Rigidbody myRB;

    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 bounce;
        bounce = transform.position - collision.transform.position;
        if (collision.gameObject.GetComponent<Rigidbody>() && collision.gameObject.GetComponent<Competitor>())
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-bounce * 40 * GetComponent<Rigidbody>().velocity.magnitude);
        }
    }
    

    private void Awake()
    {
        this.transform.localScale = new Vector3(.025f, .025f, .025f);
        origin = this.transform;
        navMeshOff = false;
        untouchable = false;
        inivisible = false;
        myRB = GetComponent<Rigidbody>();
        trails = GetComponent<TrailRenderer>();
    }

    public void Start()
    {
        if (transform.childCount > 1)
        {
            Transform t = transform.GetChild(1);
            if (t.childCount > 0)
            {
                t = t.GetChild(0);
                startMaterial = t.GetComponent<MeshRenderer>().material;
                return;
            }
        }
        startMaterial = transform.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (myRB.velocity.magnitude > 25)
        {
            trails.material.color = Color.red;
            trails.enabled = true;
        }
        else if (myRB.velocity.magnitude > 20)
        {
            trails.material.color = Color.green;
            trails.enabled = true;
        }
        else if(myRB.velocity.magnitude > 15)
        {
            trails.material.color = Color.blue;
            trails.enabled = true;
        }
        else
        {
            trails.enabled = false;
        }
    }

    public bool ScoredGoal
    {
        get { return scoredGoal; }

        set { scoredGoal = value; }
    }

    public void BallOfSteel(Transform origin, float duration)
    {
        StartCoroutine(Unbouncable(origin, duration));
    }

    public void BeenBlasted()
    {
        StartCoroutine(TurnNavMeshBackOn(blastedDuration));
    }

    public void BeenChilled(Competitor competitor, float duration)
    {
        StartCoroutine(TurnMovementControlBackOn(competitor, duration));
    }

    public void CantTouchMe(float duration)
    {
        StartCoroutine(Untouchable(duration));
    }

    public void CantFindMe(float duration)
    {
        StartCoroutine(Invis(duration));
    }

    public void WentFast(Transform origin, float duration, float speedMultiplier)
    {
        StartCoroutine(ResetSpeed(origin, duration, speedMultiplier));
    }

    public void BeenSlowed(Competitor competitor, float duration, float speedMultiplier)
    {
        StartCoroutine(ReverseMovementSpeed(competitor, duration, speedMultiplier));
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

    private IEnumerator Unbouncable(Transform origin, float duration)
    {
        ballOfSteel = true;
        yield return new WaitForSeconds(duration);


        if (origin.tag == "Player")
        {
            origin.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material = startMaterial;
            origin.GetComponent<BounceFunction>().enabled = true;
            ballOfSteel = false;
        }
        else
        {
            origin.GetComponent<MeshRenderer>().material = startMaterial;
            origin.GetComponentInParent<BounceFunction>().enabled = true;
            ballOfSteel = false;
        }
    }

    private IEnumerator TurnNavMeshBackOn(float duration)
    {
        yield return new WaitForSeconds(duration);
        navMeshOff = false;

    }

    private IEnumerator TurnMovementControlBackOn(Competitor competitor, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (competitor.GetComponent<RigidBodyControl>())
        {
            competitor.GetComponent<Rigidbody>().freezeRotation = false;
            competitor.GetComponent<RigidBodyControl>().enabled = true;
        }
        else
        {
            competitor.GetComponent<Rigidbody>().freezeRotation = false;
            competitor.GetComponent<AIStateMachine>().enabled = true;
        }

    }

    private IEnumerator ResetSpeed(Transform origin, float duration, float speedMultiplier)
    {
        yield return new WaitForSeconds(duration);

        if (origin.GetComponent<RigidBodyControl>())
        {
            origin.GetComponent<RigidBodyControl>().speed /= speedMultiplier;
        }
        else
        {
            origin.GetComponent<AIStateMachine>().Velocity /= speedMultiplier;
        }
    }

    private IEnumerator ReverseMovementSpeed(Competitor competitor, float duration, float speedMultiplier)
    {
        yield return new WaitForSeconds(duration);

        if (competitor.GetComponent<RigidBodyControl>())
        {
            competitor.GetComponent<RigidBodyControl>().speed /= speedMultiplier;
        }
        else
        {
            competitor.GetComponent<AIStateMachine>().Velocity /= speedMultiplier;
        }
    }
}
