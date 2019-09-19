using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatFollowBody : MonoBehaviour
{
    private Transform hatTransform;
    public Transform bodyTransform;
    public float hatTransformY = 0.5f;

    private void Start()
    {
        hatTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        hatTransform.position = new Vector3(bodyTransform.position.x, bodyTransform.position.y + hatTransformY, bodyTransform.position.z);
        //hatTransform.position.x = bodyTransform.position.x;
    }
}
