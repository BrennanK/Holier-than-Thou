using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NavMesh -> AINavigation
public class AIFindPath : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.gameObject.tag == "Ground")
                {
                    navMeshAgent.SetDestination(raycastHit.point);
                }
            }
        }
    }
}
