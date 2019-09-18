// Temporary Methods for AI Navigation, Attack and Achieve Points.
using UnityEngine;
using UnityEngine.AI;

enum AIState
{
    Idle,
    FindPath,
    Attack,
    UsePowerUps
};

// NavMesh -> AINavigation
public class AIFindPath : MonoBehaviour
{
    private AIState state = AIState.Idle;

    private NavMeshAgent navMeshAgent;

    private new Transform destination;
    private new Transform opponent;

    private bool shouldAttack = false;
    private bool shouldFindPath = true;

    internal AIState State { get => state; set => state = value; }
    private NavMeshAgent NavMeshAgent { get => navMeshAgent; set => navMeshAgent = value; }
    public Transform Destination { private get => destination; set => destination = value; }
    public Transform Opponent { private get => opponent; set => opponent = value; }
    public bool ShouldAttack { get => shouldAttack; set => shouldAttack = value; }
    public bool ShouldFindPath { get => shouldFindPath; set => shouldFindPath = value; }

    //public void StartFindPath()
    //{
    //    ShouldFindPath = true;
    //}

    public void StartAttack()
    {
        ShouldAttack = true;
    }

    private void Awake()
    {
        NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        State = AIState.FindPath;
    }

    private void Update()
    {
        //TestSetDestination();
        //TestGoingToDestination();

        UpdateState();
    }


    private void UpdateState()
    {
        // Check current state and set the suitable goal
        if (ShouldFindPath)
        {
            State = AIState.FindPath;
            ActivateSwicher();
        }

        if (ShouldAttack)
        {
            State = AIState.Attack;
            ActivateSwicher();
        }

    }

    private void ActivateSwicher()
    {
        // Take a action based on state and goal
        switch (State)
        {
            case AIState.Idle:
                break;
            case AIState.FindPath:
                SetDestination();
                ShouldFindPath = !ShouldFindPath;
                State = AIState.Idle;
                break;
            case AIState.Attack:
                SetDestination(Opponent.localPosition);
                State = AIState.Idle;
                break;
            case AIState.UsePowerUps:
                State = AIState.Idle;
                break;
            default:
                Debug.Log("");
                break;
        }
    }

    /// <summary>
    /// Default destination is the scoring area
    /// </summary>
    private void SetDestination()
    {
        NavMeshAgent.SetDestination(Destination.localPosition);
    }

    private void SetDestination(Vector3 target)
    {
        NavMeshAgent.SetDestination(target);
    }

    private void AttackEnemy()
    {
        SetDestination(Opponent.localScale);
    }

    /// <summary>
    /// Invoke a method every inverval, recognized by a key bool.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="interval"></param>
    /// <param name="timer"></param>
    /// <param name="method"></param>
    private void SetCheckPoint(bool key, float interval, float timer, string method)
    {
        // Update the timer for whatever.
        if (key && timer <= 0)
        {
            key = !key;
            // Do something here!
            Invoke(method, 0f);
            // Reset Timer
            timer = interval;
        }
        else if (key)
        {
            timer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Invoke a method every inverval, recognized by a key counter.
    /// </summary>
    /// <param name="counter"></param>
    /// <param name="interval"></param>
    /// <param name="timer"></param>
    /// <param name="method"></param>
    private void SetCheckPoint(byte counter, float interval, float timer, string method)
    {
        // Update the timer for whatever.
        if (counter > 0 && timer <= 0)
        {
            counter -= 1;
            // Do something here!
            Invoke(method, 0f);
            // Reset Timer
            timer = interval;
        }
        else if (counter > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    // Action: PathFinding and GoingToDestination
    private void TestSetDestination()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.gameObject.tag == "Ground")
                {
                    Destination.transform.position = raycastHit.point;
                }
            }
        }
    }
}
