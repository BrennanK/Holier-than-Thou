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
                    .Condition("Can I score", CheckIfScoredGoal)
                    .Action("Do I have a Goal", FindTheGoal)
                    .Action("Power up targeted?", LocatePowerUp)
                    .Action("Player Targeted?", LocateCompetitors)
                    .Selector("Prioritize Objectives")
                        .Sequence("Goal Prioritization")
                            .Condition("Check distance to Goal", CompareDistanceGoal)
                            .Action("Move to Goal", MoveToGoal)
                            .Action("Score Goal", ScoreGoal)
                        .End()
                        .Sequence("Powey Up Prioritization")
                            .Condition("Check distantce to power ups", CompareDistancePowerUp)
                            .Action("Move to Powerup", GoForPowerUp)
                            .Action("Grab Power up", GrabPowerUp)
                        .End()
                        .Sequence("Attack Player")
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

    // Update is called once per frame
    void Update()
    {
        
    }
    private EReturnStatus CheckForGameRunning()
    {
        if (GameManager.gameRunning)
        {
            Debug.Log("game is running");
            return EReturnStatus.SUCCESS;
        }
        else
        {
            Debug.Log("Game is not running");
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
            return EReturnStatus.SUCCESS;
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
            Debug.Log("locate power up null failure");
            return EReturnStatus.SUCCESS;
            }
        if (powerUpPos == null)
        {
            List<Collider> hitColliders = Physics.OverlapSphere(transform.position, 5f).ToList();
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
                if (!hitCompetitors[i].GetComponent<Competitor>())
                {
                    hitCompetitors.Remove(hitCompetitors[i]);
                    i--;
                }
            }

            if (hitCompetitors.Count == 0)
            {
                return EReturnStatus.SUCCESS;
            }
            else
            {

                competitorPos = hitCompetitors[0].transform;

                foreach (Collider competitor in hitCompetitors)
                {
                    if (Vector3.Distance(transform.position, competitorPos.position) > Vector3.Distance(transform.position, competitor.transform.position))
                    {
                        competitorPos = competitor.transform;
                    }
                }

                return EReturnStatus.SUCCESS;
            }


        }
        else return EReturnStatus.SUCCESS;
    }

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
            else return EReturnStatus.FAILURE;
        }
        if(powerUpPos == null)
        {
            if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position))
            {
                return EReturnStatus.SUCCESS;
            }
            else
                return EReturnStatus.FAILURE;
        }
        if (Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, powerUpPos.position) || Vector3.Distance(transform.position, goalPos.position) < Vector3.Distance(transform.position, competitorPos.position))
        {
            return EReturnStatus.SUCCESS;
        }
        else return EReturnStatus.FAILURE;
    }

    private EReturnStatus CompareDistancePowerUp()
    {
        if (powerUpPos == null)
        {
            return EReturnStatus.FAILURE;
        }

            if (Vector3.Distance(transform.position, powerUpPos.position) < Vector3.Distance(transform.position, powerUpPos.position))
        {
            return EReturnStatus.SUCCESS;
        }
        else return EReturnStatus.FAILURE;
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
        else return EReturnStatus.FAILURE;
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
            navMeshAgent.ResetPath();
            competitorPos = null;
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
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.RUNNING;
        }
    }

    private EReturnStatus GrabPowerUp()
    {
        if(transform.position == powerUpPos.position)
        {
            navMeshAgent.ResetPath();
            powerUpPos = null;
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.RUNNING;
        }
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




}
