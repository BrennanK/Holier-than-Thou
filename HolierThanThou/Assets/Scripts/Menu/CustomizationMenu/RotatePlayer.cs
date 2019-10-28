using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 1.0f;

    void Update()
    {
		transform.Rotate(Vector3.up, rotationSpeed);
    }
}
