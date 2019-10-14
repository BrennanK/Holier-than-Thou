using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatFollowBody : MonoBehaviour
{
    public Transform bodyTransform;
	public Transform parentTransform;
	public float radius = 0.5f;

    private Transform hatTransform;

    private void Start()
    {
        hatTransform = GetComponent<Transform>();
	}

    private void Update()
    {
        hatTransform.position = new Vector3(parentTransform.position.x, parentTransform.position.y + radius, parentTransform.position.z);
		hatTransform.up = Vector3.up;
    }
}
