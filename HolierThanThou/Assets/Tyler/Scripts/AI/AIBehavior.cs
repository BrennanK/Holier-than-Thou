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


    //AI BlackBoard
    private Transform goalPos;
    private Transform powerUpPos;
    private Transform competitorPos;
    public PowerUp slot1;
    public PowerUp slot2;
    private bool ableToGrab;
    private float powerUpCooldown;
    private bool canAttack;
    private float attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        competitor = GetComponent<Competitor>();
        //This is an Example of how to build the tree
        //_behaviorTree = new BehaviorTree.BehaviorTree(new BehaviorTreeBuilder().Selector("Test Selector").Action("Test Action", TestFunction)
        //    .End()
        //    .Build()
        //    );

        _behaviorTree = new BehaviorTree.BehaviorTree(new BehaviorTreeBuilder()
            .Sequence("Game Play")
                .Condition("Is the Game Running", CheckForGameRunning)
                .Sequence("Find objectives")                   
                    .Action("Do I have a Goal", FindTheGoal)                  
                    .Action("Power up targeted?", LocatePowerUp)
                    .Action("Player Targeted?", LocateCompetitors)
                    .Selector("Prioritize Objectives")
                        .Sequence("Goal Prioritization")
                            .Condition("Can I score", CheckIfScoredGoal)
                            .Condition("Check distance to Goal", CompareDistanceGoal)
                            .Action("Move to Goal", MoveToGoal)
                            .Action("Score Goal", ScoreGoal)
                        .End()
                        .Sequence("Power Up Prioritization")
                            .Condition("Can I Grab a Power Up", CheckAbleToGrabPowerUp)
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
        if (!canAttack)
        {
            attackCooldown -= Time.deltaTime;
        }
        if(attackCooldown <= 0)
        {
            canAttack = true;
        }
        if(!ableToGrab)
        {
            powerUpCooldown -= Time.deltaTime;
        }
        if(powerUpCooldown <= 0)
        {
            ableToGrab = true;
        }

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
            Debug.Log("Goal has been found");
            return EReturnStatus.FAILURE;
        }
        else
        {
           navMeshAgent.SetDestination(goalPos.position);
            Debug.Log("Goal already located");
            return EReturnStatus.SUCCESS;
        }
    }

    private EReturnStatus MoveToGoal()
    {
        if (Vector3.Distance(transform.position, goalPos.position) <= 5f)
        {
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(goalPos.position);
            return EReturnStatus.RUNNING;
        }
    }

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
    
    private EReturnStatus LocatePowerUp()
    {
        if(slot1 != null && slot2 != null)
            {
            Debug.Log("Power ups full");
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
                Debug.Log("Locate power up failure");
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
                Debug.Log("power up located");
                return EReturnStatus.SUCCESS;
            }
        }
        else
        {
            Debug.Log("Power up already known");
            return EReturnStatus.SUCCESS;
        }
               
    }
    
    private EReturnStatus LocateCompetitors()
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
                Debug.Log("No competitors found");
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

                Debug.Log("Competitor located");
                return EReturnStatus.SUCCESS;
            }
        }
        else
        {
            Debug.Log("Competitor already found");
            return EReturnStatus.SUCCESS;
        }
    }

    private EReturnStatus CompareDistanceGoal()
    {
        if(competitorPos == null && powerUpPos == null)
        {
            Debug.Log("No power up or competitor detected");
            return EReturnStatus.SUCCESS;
        }
        if(competitorPos == null)
        {
            if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
            {
                Debug.Log("Goal is closer than the powerup");
                return EReturnStatus.SUCCESS;
            }
            else
            {
                Debug.Log("Power up is closer than the goal");
                return EReturnStatus.FAILURE;
            }
        }
        if(powerUpPos == null)
        {
            if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position))
            {
                Debug.Log("Goal is closer than competitors");
                return EReturnStatus.SUCCESS;
            }
            else
            {
                Debug.Log("Competitors are closer than the goal");
                return EReturnStatus.FAILURE;
            }
        }
        if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position) && Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position))
        {
            Debug.Log("Goal is closer than power ups and competitors");
            return EReturnStatus.SUCCESS;
        }
        else
        {
            Debug.Log("Power up or competitor is closer than the goal");
            return EReturnStatus.FAILURE;
        }
    }

    private EReturnStatus CompareDistancePowerUp()
    {
        if (powerUpPos == null)
        {
            Debug.Log("No powerup to target");
            return EReturnStatus.FAILURE;
        }

        if(competitorPos == null)
        {
            return EReturnStatus.SUCCESS;
        }

        if (Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, competitorPos.position))
        {
            Debug.Log("Powerup is closer than competitor");
            return EReturnStatus.SUCCESS;
        }
        else
        {
            Debug.Log("Power up is not closer than competitor");
            return EReturnStatus.FAILURE;
        }
    }

    private EReturnStatus CheckCompetitorDistance()
    {
        if(competitorPos == null && powerUpPos == null)
        {
            Debug.Log("nothing to target");
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
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(competitorPos.position);
            return EReturnStatus.RUNNING;
        }
            
    }

    private EReturnStatus AttackCompetitor()
    {
        if(transform.position == competitorPos.position)
        {
            Debug.Log("Resetting path");
            navMeshAgent.ResetPath();
            competitorPos = null;
            attackCooldown = 5f;
            canAttack = false;
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
            return EReturnStatus.SUCCESS;
        }
        else
        {
            Debug.Log(competitor.Name + " Targeting PowerUp");
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.RUNNING;
        }
    }

    private EReturnStatus GrabPowerUp()
    {
        if(transform.position == powerUpPos.position)
        {
            Debug.Log(competitor.Name + " Grabbed power up");
            Debug.Log("resetting path");
            navMeshAgent.ResetPath();
            powerUpPos = null;
            powerUpCooldown = 5f;
            ableToGrab = false;
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.RUNNING;
        }
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

    

    private void OnCollisionEnter(Collision other)
    {
        
    }

   
}
