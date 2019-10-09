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

    private void Start() {
        m_rigidbody = GetComponent<Rigidbody>();
        navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, goal.transform.position, NavMesh.AllAreas, navMeshPath);

        foreach(Vector3 cornerPosition in navMeshPath.corners) {
            m_cornersStack.Enqueue(cornerPosition);
        }

        RecalculatePath();
    }

    private void Update() {
        if(Vector3.Distance(transform.position, m_currentGoal) < stoppingDistance) {
            RecalculatePath();
        }

        m_rigidbody.velocity = (m_currentGoal - transform.position).normalized * velocity;
    }

    void RecalculatePath() {
        if(m_cornersStack.Count == 0) {
            if(Vector3.Distance(transform.position, goal.transform.position) >= stoppingDistance) {
                if (Mathf.Abs(goal.transform.position.y - transform.position.y) > jumpingDistance) {
                    Debug.Log("Jumping...");
                    m_rigidbody.AddForce(new Vector3(goal.transform.position.x - transform.position.x * 1000f, 10000f, goal.transform.position.z - transform.position.z * 1000f));
                    m_currentGoal = goal.transform.position;
                }
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
