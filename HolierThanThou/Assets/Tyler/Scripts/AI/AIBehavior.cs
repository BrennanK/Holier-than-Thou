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
    [SerializeField]
    public PowerUp slot1;
    [SerializeField]
    public PowerUp slot2;
    private float UsePowerUpStart = 10f;
    private float UsePoweruUp1;
    private float UsePowerUp2;
    private bool enoughRange1;
    private bool enoughRange2;
    private bool canActivate1;
    private bool canActivate2;
    private bool ableToGrab;
    private float powerUpCooldown;
    private bool canAttack;
    private float attackCooldown;
    private bool isBeingKnockedback;
    private bool goalCloser;
    private bool powerUpCloser;
    private bool competitorCloser;
    private bool attackSuccess;


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
        canActivate1 = true;
        canActivate2 = true;
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
                        .Sequence("Enhancement Powerups")
                            .Condition("Do I have an enhancement power up", CheckForEnhancement)
                            .Action("Use the power up", UseEnhancement)
                            .End()
                        .Sequence("Goal Prioritization")
                            .Condition("Can I score", CheckIfScoredGoal)
                            .Condition("Check distance to Goal", CompareDistanceGoal)
                            .Action("Move to Goal", MoveToGoal)
                            .Action("Score Goal", ScoreGoal)
                        .End()
                        .Sequence("Power Up Prioritization")
                            .Condition("Can I Grab a power up", CheckAbleToGrabPowerUp)
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
        PowerUpCountdown();

        if (competitor.navMeshOff)
        {
            navMeshAgent.updatePosition = false;
        }
        else
            navMeshAgent.updatePosition = true;

        if (navMeshAgent.velocity != Vector3.zero)
        {
            body.transform.Rotate(Vector3.right, 90 * Time.deltaTime * navMeshAgent.speed);
        }

        if(UsePoweruUp1 <= 0)
        {
            slot1.ActivatePowerUp(competitor.Name, competitor.origin);
            slot1 = null;
        }
        
        if(UsePowerUp2 <= 0)
        {
            slot2.ActivatePowerUp(competitor.Name, competitor.origin);
            slot2 = null;
        }

        if(slot1 != null && !slot1.isEnhancement)
        {
            CheckInRange(slot1.radius, true);
            if(enoughRange1)
            {
                slot1.ActivatePowerUp(competitor.Name, competitor.origin);
                slot1 = null;

            }
        }

        if (slot2 != null && !slot2.isEnhancement)
        {
            CheckInRange(slot2.radius, false);
            if (enoughRange2)
            {
                slot2.ActivatePowerUp(competitor.Name, competitor.origin);
                slot2 = null;

            }
        }
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

    private EReturnStatus CheckForEnhancement()
    {
        if (slot1 != null && slot2 != null)
        {
            if (slot1.isEnhancement || slot2.isEnhancement)
            {
                return EReturnStatus.SUCCESS;
            }
            else
                return EReturnStatus.FAILURE;
        }
        else if (slot1 != null && slot2 == null)
        {
            if (slot1.isEnhancement)
            {
                return EReturnStatus.SUCCESS;
            }
            else
                return EReturnStatus.FAILURE;
        }
        else if (slot1 == null && slot2 != null)
        {
            if (slot2.isEnhancement)
            {
                return EReturnStatus.SUCCESS;
            }
            else
                return EReturnStatus.FAILURE;
        }
        else
            return EReturnStatus.FAILURE;
    }

    private EReturnStatus UseEnhancement()
    {
        if (slot1 != null && slot2 != null)
        {
            if (slot1.isEnhancement && slot2.isEnhancement)
            {
                if (canActivate1 && canActivate2)
                {
                    StartCoroutine(Enhancement(true));
                    StartCoroutine(Enhancement(false));
                    return EReturnStatus.SUCCESS;
                }
                else if (canActivate1 && !canActivate2)
                {
                    StartCoroutine(Enhancement(true));
                    return EReturnStatus.SUCCESS;
                }
                else if (!canActivate1 && canActivate2)
                {
                    StartCoroutine(Enhancement(false));
                    return EReturnStatus.SUCCESS;
                }
                else return EReturnStatus.FAILURE;

            }
            else if (slot1.isEnhancement && !slot2.isEnhancement)
            {
                if (canActivate1)
                {
                    StartCoroutine(Enhancement(true));
                    return EReturnStatus.SUCCESS;
                }
                else return EReturnStatus.FAILURE;
            }
            else if (!slot1.isEnhancement && slot2.isEnhancement)
            {
                if (canActivate2)
                {
                    StartCoroutine(Enhancement(false));
                    return EReturnStatus.SUCCESS;
                }
                else return EReturnStatus.FAILURE;
            }
            else
                return EReturnStatus.FAILURE;
        }
        else if (slot1 != null && slot2 == null)
        {
            if (canActivate1)
            {
                StartCoroutine(Enhancement(true));
                return EReturnStatus.SUCCESS;
            }
            else return EReturnStatus.FAILURE;
        }
        else if (slot1 == null && slot2 != null)
        {
            if (canActivate2)
            {
                StartCoroutine(Enhancement(false));
                return EReturnStatus.SUCCESS;
            }
            else return EReturnStatus.FAILURE;
        }
        else
            return EReturnStatus.FAILURE;
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
        if(attackSuccess)
        {
            competitorPos = null;
            navMeshAgent.ResetPath();
            navMeshAgent.SetDestination(goalPos.position);
            Debug.Log("hit somebody");
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(competitorPos.position);
            return EReturnStatus.FAILURE;
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
        if(transform.position.x == powerUpPos.position.x && transform.position.z == powerUpPos.position.z)
        {
            powerUpPos = null;
            navMeshAgent.ResetPath();
            navMeshAgent.SetDestination(goalPos.position);
            StartCoroutine(PUCooldown());
            Debug.Log("Grabbed the power up");
            return EReturnStatus.SUCCESS;
        }
        else
        {
            navMeshAgent.SetDestination(powerUpPos.position);
            return EReturnStatus.FAILURE;
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

    IEnumerator PUCooldown()
    {
        ableToGrab = false;
        yield return new WaitForSeconds(5f);
        ableToGrab = true;
    }

    /*IEnumerator Knockback(GameObject enemy)
    {
        attackSuccess = true;
        isBeingKnockedback = true;
        if(navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.ResetPath();
        }
        navMeshAgent.enabled = false;
        Vector3 moveDirection = transform.position - enemy.transform.position;
        GetComponent<Rigidbody>().velocity = moveDirection.normalized * 5;
        canAttack = false;
        StartCoroutine(AttackCooldown());
        yield return new WaitForSeconds(.5f);
        navMeshAgent.enabled = true;
        UpdateTree();
        isBeingKnockedback = false;
    }*/

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(5f);
        attackSuccess = false;
        canAttack = true;
    }

    IEnumerator Enhancement(bool slot1PU)
    {
        if(slot1PU)
        {
            canActivate1 = false;
            slot1.ActivatePowerUp(competitor.Name, competitor.origin);
            yield return new WaitForSeconds(slot1.duration);
            slot1.ResetEffects(competitor.Name);
            slot1 = null;
            canActivate1 = true;
        }
        else
        {
            canActivate2 = false;
            slot2.ActivatePowerUp(competitor.Name, competitor.origin);
            yield return new WaitForSeconds(slot2.duration);
            slot2.ResetEffects(competitor.Name);
            slot2 = null;
            canActivate2 = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Competitor>())
        {
           // StartCoroutine(Knockback(other.gameObject));
        }
    }

    private void CheckForCompetitors()
    {
        if (canAttack)
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
        else competitorPos = null;
    }

    void CheckForPowerUps()
    {
        if (ableToGrab)
        {
            if (slot1 == null && slot2 == null)
            {

                List<Collider> hitColliders = Physics.OverlapSphere(transform.position, checkRadius).ToList();
                for (int i = 0; i < hitColliders.Count; i++)
                {

                    if (hitColliders[i].tag != "ItemBox" || hitColliders[i].enabled == false)
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
            else powerUpPos = null;
        }
        else powerUpPos = null;
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


    }

    private void PowerUpCountdown()
    {
        if (slot1 == null && slot2 == null)
        {
            UsePoweruUp1 = UsePowerUpStart;
            UsePowerUp2 = UsePowerUpStart;
        }
        else if (slot1 != null && slot2 == null)
        {
            if (!slot1.isEnhancement)
            {
                UsePoweruUp1 -= Time.deltaTime;
                UsePowerUp2 = UsePowerUpStart;
            }
            else
            {
                UsePoweruUp1 = UsePowerUpStart;
                UsePowerUp2 = UsePowerUpStart;
            }
        }
        else if (slot1 == null && slot2 != null)
        {
            if(!slot2.isEnhancement)
            {
                UsePowerUp2 -= Time.deltaTime;
                UsePoweruUp1 = UsePowerUpStart;
            }
            else
            {
                UsePoweruUp1 = UsePowerUpStart;
                UsePowerUp2 = UsePowerUpStart;
            }
        }
        else if(slot1 != null && slot2 != null)
        {
            if (slot1.isEnhancement && !slot2.isEnhancement)
            {
                UsePoweruUp1 = UsePowerUpStart;
                UsePowerUp2 -= Time.deltaTime;
            }
            else if (!slot1.isEnhancement && slot2.isEnhancement)
            {
                UsePoweruUp1 -= Time.deltaTime;
                UsePowerUp2 = UsePowerUpStart;
            }
            else if(!slot1.isEnhancement && !slot2.isEnhancement)
            {
                UsePoweruUp1 -= Time.deltaTime;
                UsePowerUp2 -= Time.deltaTime;
            }
            else
            {
                UsePoweruUp1 = UsePowerUpStart;
                UsePowerUp2 = UsePowerUpStart;
            }
        }
    }


    private void CheckInRange(float radius, bool slot1)
    {
        List<Collider> hitCompetitors = Physics.OverlapSphere(transform.position, radius).ToList();
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
            return;
        }
        else
        {
            if (slot1 && hitCompetitors.Count >= 2)
            {
                enoughRange1 = true;
                Debug.Log("Enough in Range");
            }
            else if (!slot1 && hitCompetitors.Count >= 2)
            {
                enoughRange2 = true;
                Debug.Log("Enough in Range");
            }
            else return;
        }
              
    }
}
