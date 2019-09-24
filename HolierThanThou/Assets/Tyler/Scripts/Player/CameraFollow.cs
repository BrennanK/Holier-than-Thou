using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    private void Awake()
    {
        offset = new Vector3(target.position.x, target.transform.position.y + 20, target.position.z - 10);
    }

    private void Update()
    {
        this.transform.position = target.position - offset;
    }

    public void MoveToPlayer()
    {

    }
}
