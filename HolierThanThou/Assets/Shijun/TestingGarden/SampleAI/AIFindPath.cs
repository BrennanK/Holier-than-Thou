// Temporary Methods for AI Navigation, Attack and Achieve Points.
using UnityEngine;
using UnityEngine.AI;

enum IState
{
    Idle,
    PathFind,
    Attack,
    UsePowerUps
};

// NavMesh -> AINavigation
public class AIFindPath : MonoBehaviour
{
    IState state = IState.Idle;

    private NavMeshAgent navMeshAgent;

    public new Transform targetDestination;
    public new Transform targetEnemy;

    private void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        state = IState.PathFind;
    }

    private void Update()
    {
        //TestSetDestination();
        //TestGoingToDestination();

        UpdateState();
    }


    public bool canAttack = false;
    public bool canFindPath = true;
    private void UpdateState()
    {
        // Check current state and set the suitable goal
        if (canFindPath)
        {
            state = IState.PathFind;
            ActivateSwicher();
        }

        if (canAttack)
        {
            state = IState.Attack;
            ActivateSwicher();
        }

    }

    private void ActivateSwicher()
    {
        // Take a action based on state and goal
        switch (state)
        {
            case IState.Idle:
                break;
            case IState.PathFind:
                SetDestination();
                canFindPath = !canFindPath;
                state = IState.Idle;
                break;
            case IState.Attack:
                SetDestination(targetEnemy.localPosition);
                state = IState.Idle;
                break;
            case IState.UsePowerUps:
                state = IState.Idle;
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
        navMeshAgent.SetDestination(targetDestination.localPosition);
    }

    private void SetDestination(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

    private void AttackEnemy()
    {
        SetDestination(targetEnemy.localScale);
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
                    targetDestination.transform.position = raycastHit.point;
                }
            }
        }
    }
}
