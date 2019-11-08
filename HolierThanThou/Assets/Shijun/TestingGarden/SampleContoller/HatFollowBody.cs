using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatFollowBody : MonoBehaviour
{
	public Transform bodyTransform;
	public Transform parentTransform;
	public float scaleFactor = 0.025f;

	private float radius = 0.5f;
	private Transform hatTransform;
	private float waitTime = 0.0001f;
	private IEnumerator coroutine;

	private float rotationSpeed = 5f;
	private Vector3 currentPosition = Vector3.zero;
	private Vector3 prevPosition = Vector3.zero;
	private Vector3 newForward = Vector3.zero;
	private Vector3 currentForward = Vector3.zero;

	private void Start()
	{
		hatTransform = GetComponent<Transform>();

		coroutine = SetRadius(waitTime);
		StartCoroutine(coroutine);
	}

	IEnumerator SetRadius(float Count)
	{
		yield return new WaitForSeconds(Count);

		if (bodyTransform.childCount > 0)
		{
			radius = parentTransform.GetComponent<MeshFilter>().mesh.bounds.extents.x * scaleFactor;
		}
		yield return null;
	}

	private void Update()
	{
		currentPosition.x = parentTransform.position.x;
		currentPosition.z = parentTransform.position.z;
		newForward = currentPosition - prevPosition;

		currentForward = new Vector3(hatTransform.forward.x, 0, hatTransform.forward.z);

		float step = rotationSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(currentForward, newForward, step, 0.0f);

		hatTransform.rotation = Quaternion.LookRotation(newDir);
		hatTransform.position = new Vector3(parentTransform.position.x, parentTransform.position.y + radius, parentTransform.position.z);
		prevPosition = currentPosition;
	}
}
