using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
using System.Linq;

public class AIBehavior : MonoBehaviour
{
    private BehaviorTree.BehaviorTree _behaviorTree;
    public float behaviorTreeRefreshRate = 0.1f;
    private NavMeshAgent navMeshAgent;
    private Competitor competitor;
    private Rigidbody rb;
    public GameObject body;
    


    //AI BlackBoard
    private float checkRadius = 5f;
    private Transform goalPos;
    private Transform powerUpPos;
    private Transform competitorPos;
    public PowerUp slot1;
    public PowerUp slot2;
    private bool ableToGrab;
    private float powerUpCooldown;
    private bool canAttack;
    private float attackCooldown;
    private bool isBeingKnockedback;
    private bool goalCloser;
    private bool powerUpCloser;
    private bool competitorCloser;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        competitor = GetComponent<Competitor>();
        rb = GetComponent<Rigidbody>();
        goalPos = GameObject.FindGameObjectWithTag("Goal").transform;
        canAttack = true;
        ableToGrab = true;
        //This is an Example of how to build the tree
        //_behaviorTree = new BehaviorTree.BehaviorTree(new BehaviorTreeBuilder().Selector("Test Selector").Action("Test Action", TestFunction)
        //    .End()
        //    .Build()
        //    );

        _behaviorTree = new BehaviorTree.BehaviorTree(new BehaviorTreeBuilder()
            .Sequence("Game Play")
                .Condition("Is being knocked back", CheckForKnockBack)
                .Condition("Is the Game Running", CheckForGameRunning)
                .Sequence("Find objectives")                   
                    .Action("Do I have a Goal", FindTheGoal)                  
                    .Selector("Prioritize Objectives")
                        .Sequence("Goal Prioritization")
                            .Condition("Can I score", CheckIfScoredGoal)
                            .Condition("Check distance to Goal", CompareDistanceGoal)
                            .Action("Move to Goal", MoveToGoal)
                            .Action("Score Goal", ScoreGoal)
                        .End()
                        .Sequence("Power Up Prioritization")
                            .Condition("Can I Grab a Power Up", CheckAbleToGrabPowerUp)
                            //.Condition("Is the Power up active?", CheckforPowerupActive)
                            .Condition("Check distantce to power ups", CompareDistancePowerUp)
                            .Action("Move to Powerup", GoForPowerUp)
                            .Action("Grab Power up", GrabPowerUp)
                        .End()
                        .Sequence("Attack Player")
                            .Condition("Can I hit somebody?", CheckToHitSomeone)
                            .Condition("is the competitor closer", CheckCompetitorDistance)
                            .Action("Move to Attack", GoForCompetitor)
                            .Action("Attack Competitor", AttackCompetitor)
                        .End()
                    .End()
                .End()
            .End()
            .Build());

        InvokeRepeating("UpdateTree", 0f, behaviorTreeRefreshRate);
    }

    private void Update()
    {
        CheckForCompetitors();
        CheckForPowerUps();
        CompareDistances();

        if (competitor.navMeshOff)
        {
            navMeshAgent.updatePosition = false;
        }
        else
            navMeshAgent.updatePosition = true;

        body.transform.Rotate(Vector3.forward, 90 * Time.deltaTime * navMeshAgent.speed);
    }


    private EReturnStatus CheckForKnockBack()
    {
        if (isBeingKnockedback)
        {
            return EReturnStatus.FAILURE;
        }
        else return EReturnStatus.SUCCESS;
    }

    private EReturnStatus CheckForGameRunning()
    {
        if (GameManager.gameRunning)
        {
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.ResetPath();
            return EReturnStatus.FAILURE;
        }
    }

    private EReturnStatus CheckIfScoredGoal()
    {
        if (competitor.ScoredGoal == true)
            return EReturnStatus.FAILURE;
        else
            return EReturnStatus.SUCCESS;
    }

    private EReturnStatus FindTheGoal()
    {
        if (goalPos == null)
        {
            goalPos = GameObject.FindGameObjectWithTag("Goal").transform;
            return EReturnStatus.FAILURE;
        }
        else
        {

            return EReturnStatus.SUCCESS;
        }
    }

    private EReturnStatus MoveToGoal()
    {

        if (competitorCloser)
        {
            navMeshAgent.SetDestination(competitorPos.position);
            return EReturnStatus.FAILURE;
        }
        else if (powerUpCloser)
        {
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.FAILURE;
        }
        else if (Vector3.Distance(transform.position, goalPos.position) <= 5f)
        {
            navMeshAgent.SetDestination(goalPos.position);
            return EReturnStatus.SUCCESS;
        }
        else
        {
            Debug.Log(competitor.Name + " Targeting Goal");
            navMeshAgent.SetDestination(goalPos.position);
            return EReturnStatus.FAILURE;
        }
       
    }




    /*private EReturnStatus MoveToGoal()
    {
        
        if (competitorPos != null && (Vector3.Distance(transform.position, competitorPos.position) < Vector3.Distance(transform.position, goalPos.position)))
        {
            Debug.Log("other competitor is closer");
            return EReturnStatus.FAILURE;
        }
        else if (powerUpPos != null && Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, goalPos.position))
        {
            Debug.Log("Power Up is closer");
            return EReturnStatus.FAILURE;
        }
        else if (Vector3.Distance(transform.position, goalPos.position) <= 5f)
        {
            Debug.Log("CLoser to the goal");
            return EReturnStatus.SUCCESS;
        }
        else
        {
            Debug.Log("running for goal");
            navMeshAgent.SetDestination(goalPos.position);
            return EReturnStatus.RUNNING;
        }
    }*/

    private EReturnStatus ScoreGoal()
    {
        if (transform.position == goalPos.position)
        {
            navMeshAgent.ResetPath();
            goalPos = null;
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(goalPos.position);
            return EReturnStatus.RUNNING;
        }
    }
    
   /*private EReturnStatus LocatePowerUp()
    {
        if(slot1 != null && slot2 != null)
            {
            return EReturnStatus.FAILURE;
            }
        if (powerUpPos == null)
        {
            List<Collider> hitColliders = Physics.OverlapSphere(transform.position, 25f).ToList();
            for (int i = 0; i < hitColliders.Count; i++)
            {
                if (hitColliders[i].tag != "ItemBox")
                {
                    hitColliders.Remove(hitColliders[i]);
                    i--;
                }
            }

            if (hitColliders.Count == 0)
            {
                return EReturnStatus.SUCCESS;
            }
            else
            {


                powerUpPos = hitColliders[0].transform;

                foreach (Collider _collider in hitColliders)
                {
                    if (Vector3.Distance(transform.position, powerUpPos.position) > Vector3.Distance(transform.position, _collider.transform.position))
                    {
                        powerUpPos = _collider.transform;
                    }
                }
                return EReturnStatus.SUCCESS;
            }
        }
        else
        {
            return EReturnStatus.SUCCESS;
        }
               
    }*/
    
    /*private EReturnStatus LocateCompetitors()
    {
        if (competitorPos == null)
        {
            List<Collider> hitCompetitors = Physics.OverlapSphere(transform.position, 5f).ToList();
            for (int i = 0; i < hitCompetitors.Count; i++)
            {
               
                if (!hitCompetitors[i].GetComponent<Competitor>() || hitCompetitors[i].transform == this.transform)
                {
                    hitCompetitors.Remove(hitCompetitors[i]);
                    i--;
                }
            }

            if (hitCompetitors.Count == 0)
            {
                navMeshAgent.SetDestination(goalPos.position);
                return EReturnStatus.SUCCESS;
            }
            else
            {

                competitorPos = hitCompetitors[0].transform;

                foreach (Collider competitor in hitCompetitors)
                {
                    if (competitor.transform == this.transform) continue;

                    if (Vector3.Distance(transform.position, competitorPos.position) > Vector3.Distance(transform.position, competitor.transform.position))
                    {
                        competitorPos = competitor.transform;
                    }
                }

                return EReturnStatus.SUCCESS;
            }
        }
        else
        {
            return EReturnStatus.SUCCESS;
        }
    }*/

    private EReturnStatus CompareDistanceGoal()
    {
        if(competitorPos == null && powerUpPos == null)
        {
            return EReturnStatus.SUCCESS;
        }
        if(competitorPos == null)
        {
            if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
            {
                return EReturnStatus.SUCCESS;
            }
            else
            {
                return EReturnStatus.FAILURE;
            }
        }
        if(powerUpPos == null)
        {
            if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position))
            {
                return EReturnStatus.SUCCESS;
            }
            else
            {
                return EReturnStatus.FAILURE;
            }
        }
        if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position) && Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position))
        {
            return EReturnStatus.SUCCESS;
        }
        else
        {
            return EReturnStatus.FAILURE;
        }
    }

    private EReturnStatus CompareDistancePowerUp()
    {
        if (powerUpPos == null)
        {
            return EReturnStatus.FAILURE;
        }

        if(competitorPos == null)
        {
            return EReturnStatus.SUCCESS;
        }

        if (Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, competitorPos.position))
        {
            return EReturnStatus.SUCCESS;
        }
        else
        {
            return EReturnStatus.FAILURE;
        }
    }

    private EReturnStatus CheckCompetitorDistance()
    {
        if(competitorPos == null || powerUpPos == null)
        {
            return EReturnStatus.FAILURE;
        }

        if (Vector3.Distance(transform.position, competitorPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
        {
            return EReturnStatus.SUCCESS;
        }
        else return EReturnStatus.SUCCESS;
    }

    private EReturnStatus GoForCompetitor()
    {
        if (Vector3.Distance(transform.position, competitorPos.position) <= 5f)
        {
            navMeshAgent.SetDestination(competitorPos.position);
            return EReturnStatus.SUCCESS;
        }
        else if(goalCloser)
        {
            navMeshAgent.SetDestination(goalPos.position);
            return EReturnStatus.FAILURE;
        }
        else if(powerUpCloser)
        {
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.FAILURE;
        }
        else
        {
            Debug.Log(competitor.Name + " Targeting  opponent");
            navMeshAgent.SetDestination(competitorPos.position);
            return EReturnStatus.FAILURE;
        }
            
    }

    private EReturnStatus AttackCompetitor()
    {
        if(transform.position == competitorPos.position)
        {
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(competitorPos.position);
            return EReturnStatus.RUNNING;
        }
    }
    
    private EReturnStatus GoForPowerUp()
    {
        if(Vector3.Distance(transform.position, powerUpPos.position) <= 5f)
        {
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.SUCCESS;
        }
        else if(goalCloser)
        {
            navMeshAgent.SetDestination(goalPos.position);
            return EReturnStatus.FAILURE;
        }
        else if(competitorCloser)
        {
            navMeshAgent.SetDestination(competitorPos.position);
            return EReturnStatus.FAILURE;
        }
        else
        {
            Debug.Log(competitor.Name + " Targeting Power up");
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.FAILURE;
        }
    }

    private EReturnStatus GrabPowerUp()
    {
        if(transform.position == powerUpPos.position)
        {
            powerUpPos = null;
            StartCoroutine(PUCooldown());
            navMeshAgent.ResetPath();
            navMeshAgent.SetDestination(goalPos.position);
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.RUNNING;
        }
    }

    IEnumerator PUCooldown()
    {
        ableToGrab = false;
        yield return new WaitForSeconds(5f);
        ableToGrab = true;
    }

    private EReturnStatus CheckAbleToGrabPowerUp()
    {
        if (ableToGrab)
        {
            return EReturnStatus.SUCCESS;
        }
        else return EReturnStatus.FAILURE;
    }

    private EReturnStatus CheckToHitSomeone()
    {
        if (canAttack)
        {
            return EReturnStatus.SUCCESS;
        }
        else return EReturnStatus.FAILURE;
    }

    void UpdateTree()
    {
        _behaviorTree.Update();
    }


    // Example of a Function for the Behavior Tree
    //private EReturnStatus TestFunction()
    //{
    //    return EReturnStatus.SUCCESS;

    //}

    IEnumerator Knockback(GameObject enemy)
    {
        isBeingKnockedback = true;
        if(navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.ResetPath();
        }
        navMeshAgent.enabled = false;
        Vector3 moveDirection = transform.position - enemy.transform.position;
        GetComponent<Rigidbody>().velocity = moveDirection.normalized * 5;
        canAttack = false;
        yield return new WaitForSeconds(.5f);
        navMeshAgent.enabled = true;
        UpdateTree();
        isBeingKnockedback = false;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Competitor>())
        {
            StartCoroutine(Knockback(other.gameObject));
        }
    }

    private void CheckForCompetitors()
    {
        List<Collider> hitCompetitors = Physics.OverlapSphere(transform.position, checkRadius).ToList();
        for (int i = 0; i < hitCompetitors.Count; i++)
        {

            if (!hitCompetitors[i].GetComponent<Competitor>() || hitCompetitors[i].transform == this.transform)
            {
                hitCompetitors.Remove(hitCompetitors[i]);
                i--;
            }
        }
        if (hitCompetitors.Count == 0)
        {
            competitorPos = null;
        }
        else
        {

            competitorPos = hitCompetitors[0].transform;

            foreach (Collider competitor in hitCompetitors)
            {
                if (competitor.transform == this.transform) continue;

                if (Vector3.Distance(transform.position, competitorPos.position) > Vector3.Distance(transform.position, competitor.transform.position))
                {
                    competitorPos = competitor.transform;
                }
            }
        }
    }

    void CheckForPowerUps()
    {
        if (slot1 != null && slot2 != null)
        {
            return;
        }
            List<Collider> hitColliders = Physics.OverlapSphere(transform.position, checkRadius).ToList();
            for (int i = 0; i < hitColliders.Count; i++)
            {
            
                if (hitColliders[i].tag != "ItemBox")
                {
                    hitColliders.Remove(hitColliders[i]);
                    i--;
                }
            }

            if (hitColliders.Count == 0)
            {
            // powerUpPos = null;
            }
            else
            {
                powerUpPos = hitColliders[0].transform;

                foreach (Collider _collider in hitColliders)
                {
                    if (Vector3.Distance(transform.position, powerUpPos.position) > Vector3.Distance(transform.position, _collider.transform.position))
                    {
                        powerUpPos = _collider.transform;
                    }

                }
            }
        }

    void CompareDistances()
    {
        if (competitorPos != null)
        {
            if (powerUpPos != null)
            {
                if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position) &&
                Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
                {
                    goalCloser = true;
                    powerUpCloser = false;
                    competitorCloser = false;
                }
                else if (Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, goalPos.position) &&
                    Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, competitorPos.position))
                {
                    goalCloser = false;
                    powerUpCloser = true;
                    competitorCloser = false;
                }
                else
                {
                    goalCloser = false;
                    powerUpCloser = false;
                    competitorCloser = true;
                }
            }
            else if (powerUpPos == null)
            {
                if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position))
                {
                    goalCloser = true;
                    powerUpCloser = false;
                    competitorCloser = false;
                }
                else
                {
                    goalCloser = false;
                    powerUpCloser = false;
                    competitorCloser = true;
                }
            }
        }
        else if (powerUpPos != null)
        {
            if (competitorPos != null)
            {
                if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position) &&
                Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
                {
                    goalCloser = true;
                    powerUpCloser = false;
                    competitorCloser = false;
                }
                else if (Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, goalPos.position) &&
                    Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, competitorPos.position))
                {
                    goalCloser = false;
                    powerUpCloser = true;
                    competitorCloser = false;
                }
                else
                {
                    goalCloser = false;
                    powerUpCloser = false;
                    competitorCloser = true;
                }
            }
            else if (competitorPos == null)
            {
                if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
                {
                    goalCloser = true;
                    powerUpCloser = false;
                    competitorCloser = false;
                }
                else
                {
                    goalCloser = false;
                    powerUpCloser = true;
                    competitorCloser = false;
                }
            }
        }
        else if (competitorPos == null && powerUpPos == null)
        {
            goalCloser = true;
            powerUpCloser = false;
            competitorCloser = false;
        }
        else
            Debug.Log(competitor.Name + " This shit don't work!!!");



        
        /*if (powerUpPos != null && competitor == null)
        {
            if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
            {
                goalCloser = true;
                powerUpCloser = false;
                competitorCloser = false;
            }
            else
            {
                goalCloser = false;
                powerUpCloser = true;
                competitorCloser = false;
            }
        }
        else if( powerUpPos == null && competitorPos == null)
        {
            goalCloser = true;
            powerUpCloser = false;
            competitorCloser = false;
        }
        else if(powerUpPos == null && competitorPos != null)
        {
            if(Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position))
            {
                goalCloser = true;
                powerUpCloser = false;
                competitorCloser = false;
            }
            else
            {
                goalCloser = false;
                powerUpCloser = false;
                competitorCloser = true;
            }
        }
        else if(powerUpPos != null && competitorPos != null)
        {
            if(Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position) && 
                Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
            {
                goalCloser = true;
                powerUpCloser = false;
                competitorCloser = false;
            }
            else if(Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, goalPos.position) && 
                Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, competitorPos.position))
            {
                goalCloser = false;
                powerUpCloser = true;
                competitorCloser = false;
            }
            else
            {
                goalCloser = false;
                powerUpCloser = false;
                competitorCloser = true;
            }
        }
        else if (competitorPos == null)
        {
            Debug.Log(competitor.Name + " this shit dont work" + " Goal Pos: " + goalPos + " Competitor Pos: " + competitorPos + " Power up Pos: " + powerUpPos);

        }*/

    }
}
