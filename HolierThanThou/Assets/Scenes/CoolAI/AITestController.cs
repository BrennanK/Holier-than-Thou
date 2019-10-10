using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AITestController : MonoBehaviour {
    private float stoppingDistance = 1.0f;
    private float jumpingDistance = 1.0f;

    private float jumpingForce;
    public float velocity = 5f;

    public GameObject goal;
    NavMeshPath navMeshPath;
    private Rigidbody m_rigidbody;
    Queue<Vector3> m_cornersStack = new Queue<Vector3>();
    private Vector3 m_currentGoal;

    float calcTimer = 0;
    float calcRand = 0;

    private void Start() {
        m_rigidbody = GetComponent<Rigidbody>();

        // vv This chunk of code is now run in the RunPathCalculation() - Brian 10/10
        //navMeshPath = new NavMeshPath();
        //NavMesh.CalculatePath(transform.position, goal.transform.position, NavMesh.AllAreas, navMeshPath);

        //foreach(Vector3 cornerPosition in navMeshPath.corners) {
        //    m_cornersStack.Enqueue(cornerPosition);
        //}
        ResetTimer();
        RunPathCalculation();
        RecalculatePath();
    }

    private void FixedUpdate() {
        if(Vector3.Distance(transform.position, m_currentGoal) < stoppingDistance) {
            RecalculatePath();
        }

        // The movement being applied is now AddForce with a ForceMode.Force instead of hard assigning the velocity - Brian 10/10
        //m_rigidbody.velocity = (m_currentGoal - transform.position).normalized * velocity; 
        m_rigidbody.AddForce((m_currentGoal - transform.position).normalized, ForceMode.Force);

        //This timer runs so the balls can re-calculate their paths - Brian 10/10
        calcTimer += Time.deltaTime;
        if (calcTimer > calcRand)
        {
            ResetTimer();
            RunPathCalculation();
        }
    }

    void ResetTimer()
    {
        calcTimer = 0;
        calcRand = Random.Range(0.5f, 0.6f);
    }

    void RunPathCalculation()
    {
        m_cornersStack = new Queue<Vector3>();
        navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, goal.transform.position, NavMesh.AllAreas, navMeshPath);

        foreach (Vector3 cornerPosition in navMeshPath.corners)
        {
            m_cornersStack.Enqueue(cornerPosition);
        }

        RecalculatePath();
    }

    void RecalculatePath() {
        if(m_cornersStack.Count == 0) {
            if(Vector3.Distance(transform.position, goal.transform.position) >= stoppingDistance) {
                // I turned off the attempt to jump for the time being since the balls would fly away into space - Brian 10/10
                //if (Mathf.Abs(goal.transform.position.y - transform.position.y) > jumpingDistance) {
                //    Debug.Log("Jumping...");
                //    m_rigidbody.AddForce(new Vector3(goal.transform.position.x - transform.position.x * 1000f, 10000f, goal.transform.position.z - transform.position.z * 1000f));
                //    m_currentGoal = goal.transform.position;
                //}
            }
        } else {
            m_currentGoal = m_cornersStack.Dequeue();
        }
    }

    private void OnDrawGizmos() {
        if(m_cornersStack.Count > 0) {
            foreach (Vector3 corner in m_cornersStack) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(corner, 1.0f);
            }
        }
    }

}
