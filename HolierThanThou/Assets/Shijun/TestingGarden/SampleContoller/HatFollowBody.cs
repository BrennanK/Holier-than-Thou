using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatFollowBody : MonoBehaviour
{
    private Transform hatTransform;
    public Transform bodyTransform;
    public float hatTransformY;

    private void Start()
    {
        hatTransform = GetComponent<Transform>();
        hatTransformY = hatTransform.localPosition.y;
    }

    private void Update()
    {
        hatTransform.localPosition = new Vector3(bodyTransform.localPosition.x, bodyTransform.localPosition.y + hatTransformY, bodyTransform.localPosition.z);
    }
}
