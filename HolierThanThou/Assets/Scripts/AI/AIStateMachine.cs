using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStateMachine : MonoBehaviour {
    public enum EAIState {
        FINDING_OBJECTIVE,
        USING_POWERUP,
        MOVING_TO_GOAL,
        SCORING_GOAL,
        GRABBING_POWERUP,
        ATTACKING_PLAYER
    }

    // TODO introduce the state stack...
    private Stack<EAIState> m_stateStack = new Stack<EAIState>();

    private EAIState m_currentState;
    private bool m_isGameRunning;

    // Cached Components
    private Competitor m_competitor;
    private Rigidbody m_rigidbody;

    // AI Pathfinding
    private float m_stoppingDistance = 10.0f;
    private float m_jumpingDistance = 1.0f;
    private float m_jumpingForce;

    private float velocity = 10f;


    private float m_speedBoost;
    private NavMeshPath m_navMeshPath;
    private Queue<Vector3> m_cornersQueue = new Queue<Vector3>();
    private Vector3 m_currentGoal;
    [SerializeField] private Transform target;

    // AI Blackboard
    private float m_powerUpCheck = 10f;
    private float m_competitorCheck = 10f;
    private Transform m_goalTransform;

    public PowerUp slot1;
    public PowerUp slot2;

    private float m_usePowerUpStart = 10f;
    private float m_usePowerUp1;
    private float m_usePowerUp2;
    private bool m_enoughRange1;
    private bool m_enoughRange2;
    private bool m_canActivate1;
    private bool m_canActivate2;
    private bool m_canGrabPowerUp;
    private float m_powerUpCooldown;
    private bool m_canAttack;
    private float m_attackCooldown = 5f;
    private bool m_isBeingKnockedback;
    private bool m_goalCloser;
    private bool m_powerUpCloser;
    private bool m_competitorCloser;
    private bool m_attackSuccess;
    private bool m_isBully = false;
    private bool m_isItemHog = false;

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_powerUpCheck);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, m_competitorCheck);
        if (m_cornersQueue.Count > 0) {
            foreach (Vector3 corner in m_cornersQueue) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(corner, 1.0f);
            }
        }
    }

    private void Start() {
        m_competitor = GetComponent<Competitor>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_goalTransform = GameObject.FindGameObjectWithTag("Goal").transform;
        target = m_goalTransform;
        m_canAttack = true;
        m_canGrabPowerUp = true;
        m_canActivate1 = false;
        m_canActivate2 = false;
        m_currentState = EAIState.FINDING_OBJECTIVE;
    }

    public void MakeBully() {
        m_competitorCheck = 50f;
        m_attackCooldown = 2f;
        m_powerUpCheck = 5f;
        m_isBully = true;
    }

    public void MakeItemHog() {
        m_competitorCheck = 5f;
        m_powerUpCheck = 50f;
        m_isItemHog = true;
    }

    private void Update() {
        switch(m_currentState) {
            case EAIState.MOVING_TO_GOAL:
                MoveToGoalState();
                break;
            case EAIState.SCORING_GOAL:
                ScoreGoalState();
                break;
            case EAIState.FINDING_OBJECTIVE:
                FindObjectiveState();
                break;
            case EAIState.GRABBING_POWERUP:
                GrabbingPowerUpState();
                break;
            case EAIState.ATTACKING_PLAYER:
                AttackingPlayerState();
                break;
        }
    }

    // ---------------------------------------------------------------------
    // ---------------------------------------------------------------------
    // ---------------------------------------------------------------------
    // ---------------------------------------------------------------------
    // ---------------------------------------------------------------------
    // Handling AI States

    #region Finding Objective
    private void FindObjectiveState() {
        target = null;
        Transform targetToFollow;

        if(CanGetPowerUp(out targetToFollow)) {
            Debug.Log($"Getting Power Up!");
            target = targetToFollow;
            m_currentState = EAIState.GRABBING_POWERUP;
        } else if(CanAttackOtherCompetitor(out targetToFollow)) {
            target = targetToFollow;
            m_currentState = EAIState.ATTACKING_PLAYER;
            return;
        }

        RunPathCalculation();
    }

    private bool CanGetPowerUp(out Transform targetToFollow) {
        if(slot1 != null && slot2 != null) {
            targetToFollow = null;
            return false;
        }

        PowerUpBox[] powerUpBoxes = FindObjectsOfType<PowerUpBox>();
        List<PowerUpBox> powerUpBoxesWithinDistance = new List<PowerUpBox>();

        foreach(PowerUpBox box in powerUpBoxes) {
            if(Vector3.Distance(transform.position, box.transform.position) < m_powerUpCheck && !box.IsDisabled) {
                powerUpBoxesWithinDistance.Add(box);
            }
        }

        if(powerUpBoxesWithinDistance.Count == 0) {
            targetToFollow = null;
            return false;
        }

        PowerUpBox closestBox = powerUpBoxesWithinDistance[0];
        for(int i = 1; i < powerUpBoxesWithinDistance.Count; i++) {
            if(Vector3.Distance(transform.position, closestBox.transform.position) > Vector3.Distance(transform.position, powerUpBoxesWithinDistance[i].transform.position)) {
                closestBox = powerUpBoxesWithinDistance[i];
            }
        }

        targetToFollow = closestBox.transform;
        return true;
    }

    private bool CanAttackOtherCompetitor(out Transform competitorToFollow) {
        Competitor[] allCompetitors = FindObjectsOfType<Competitor>();
        List<Competitor> allCompetitorsWithinDistance = new List<Competitor>();

        foreach(Competitor competitor in allCompetitors) {
            if(competitor == this.m_competitor) {
                continue;
            }

            // check if it is within distance...
            if(Vector3.Distance(transform.position, competitor.transform.position) < m_competitorCheck) {
                allCompetitorsWithinDistance.Add(competitor);
            }
        }

        if(allCompetitorsWithinDistance.Count == 0) {
            competitorToFollow = null;
            return false;
        }

        // set target to closest competitor
        Competitor closestCompetitor = allCompetitorsWithinDistance[0];
        for(int i = 1; i < allCompetitorsWithinDistance.Count; i++) {
            if(Vector3.Distance(transform.position, closestCompetitor.transform.position) > Vector3.Distance(transform.position, allCompetitorsWithinDistance[i].transform.position)) {
                closestCompetitor = allCompetitors[i];
            }
        }

        competitorToFollow = closestCompetitor.transform;
        return true;
    }
    #endregion

    #region Grabbing Power Up
    private void GrabbingPowerUpState() {
        Debug.Log($"Grabbing Power Up State");

        // TODO Handle ways for the AI to leave the grabbing power up state


        MoveTowardsCorner();
    }

    private void AttackingPlayerState() {
        Debug.Log($"{transform.name} has velocity magnitude of {m_rigidbody.velocity.magnitude}");
        if(Vector3.Distance(target.position, transform.position) > m_competitorCheck) {
            m_currentState = EAIState.FINDING_OBJECTIVE;
            return;
        }

        // If we are too slow it is not interesting to attack other balls because we will not knockback them and won't get any multiplier...
        if(m_rigidbody.velocity.magnitude < 5.0f) {
            target = null;
            Transform newTarget;
            if(CanGetPowerUp(out newTarget)) {
                target = newTarget;
                m_currentState = EAIState.GRABBING_POWERUP;
            }

            RunPathCalculation();
            return;
        }

        // TODO handle ways for the AI to leave the attacking player state

        Debug.Log($"Attacking Player State");
        HardFollowTarget();

    }

    private void MoveToGoalState() {
        Transform powerUpToGet;
        Transform playerToAttack;

        if(CanGetPowerUp(out powerUpToGet)) {
            if (Vector3.Distance(transform.position, powerUpToGet.position) < Vector3.Distance(transform.position, target.position)) {
                target = powerUpToGet;
                m_currentState = EAIState.GRABBING_POWERUP;
                RunPathCalculation();
                return;
            }
        } if(CanAttackOtherCompetitor(out playerToAttack)) {
            if(Vector3.Distance(transform.position, playerToAttack.position) < Vector3.Distance(transform.position, target.position)) {
                target = playerToAttack;
                m_currentState = EAIState.ATTACKING_PLAYER;
                return;
            }
        }

        // We are too close to the goal so now we commit to getting into it!!
        if(Vector3.Distance(transform.position, target.position) < 5.0f) {
            m_currentState = EAIState.SCORING_GOAL;
            return;
        }

        MoveTowardsCorner();
    }

    private void ScoreGoalState() {
        Debug.Log($"IMMA SCORE!!");
        HardFollowTarget();
    }
    #endregion


    // ---------------------------------------------------------------------
    // ---------------------------------------------------------------------
    // ---------------------------------------------------------------------
    // ---------------------------------------------------------------------
    // ---------------------------------------------------------------------

    #region AI Pathfinding
    private void MoveTowardsCorner() {
        if(Vector3.Distance(transform.position, m_currentGoal) < m_stoppingDistance) {
            RecalculatePath();
        }

        Debug.DrawRay(transform.position, (m_currentGoal - transform.position), Color.red, Time.deltaTime);
        m_rigidbody.AddForce((m_currentGoal - transform.position).normalized * (velocity + m_speedBoost), ForceMode.Force);
    }

    private void HardFollowTarget() {
        Debug.DrawRay(transform.position, (target.position - transform.position), Color.green, Time.deltaTime);
        m_rigidbody.AddForce((target.position - transform.position).normalized * velocity, ForceMode.Force);
    }

    public void RunPathCalculation() {
        m_cornersQueue = new Queue<Vector3>();
        m_navMeshPath = new NavMeshPath();

        if(target == null) {
            Debug.Log($"Target is null! Setting as the goal!");
            target = m_goalTransform;
            m_currentState = EAIState.MOVING_TO_GOAL;
        }

        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, m_navMeshPath);
        Debug.Log($"Navmesh Calculated Path between {transform} and {target}, has {m_navMeshPath.corners.Length} corners");

        foreach(Vector2 cornerPosition in m_navMeshPath.corners) {
            m_cornersQueue.Enqueue(cornerPosition);
        }
        m_cornersQueue.Enqueue(target.position);

        RecalculatePath();
    }

    public void RecalculatePath() {
        if(m_cornersQueue.Count == 0) {
            Debug.Log($"AI has 0 Corners!");
            target = null;
            m_currentState = EAIState.FINDING_OBJECTIVE;
        } else {
            Vector3 previousDirection = m_currentGoal - transform.position;
            m_currentGoal = m_cornersQueue.Dequeue();
            Vector3 currentDirection = m_currentGoal - transform.position;

            if(Vector3.Angle(previousDirection, currentDirection) > 15f) {
                Debug.Log($"Big angle from changing direction, adding artificial speed boost");
            }
        }
    }

    private IEnumerator SpeedBoostRoutine() {
        m_speedBoost = 10f;
        float timeToDecay = 3f;
        float speedBoostPass = m_speedBoost / timeToDecay;

        for(float i = 0; i < timeToDecay; i += Time.deltaTime) {
            m_speedBoost -= speedBoostPass * Time.deltaTime;
            yield return null;
        }

        m_speedBoost = 0f;
    }

    public void ClearPath() {
        m_cornersQueue.Clear();
    }
    #endregion
}
