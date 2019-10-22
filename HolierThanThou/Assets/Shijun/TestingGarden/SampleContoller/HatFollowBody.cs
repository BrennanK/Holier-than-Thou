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
	private DigitalJoystick joystick;
	private Camera camera;

	private void Start()
	{
		hatTransform = GetComponent<Transform>();
		joystick = FindObjectOfType<DigitalJoystick>();
		camera = FindObjectOfType<Camera>();

		Debug.Log("Starting: " + Time.time);
		coroutine = SetRadius(waitTime);
		StartCoroutine(coroutine);
		Debug.Log("Ending: " + Time.time);
	}

	IEnumerator SetRadius(float Count)
	{
		yield return new WaitForSeconds(Count);
		Debug.Log("Waiting: " + Time.time);
		if (bodyTransform.childCount > 0)
		{
			radius = parentTransform.GetComponent<MeshFilter>().mesh.bounds.extents.x * scaleFactor;
		}
		yield return null;
	}

	private void Update()
	{
		hatTransform.forward = parentTransform.forward;
		hatTransform.position = new Vector3(parentTransform.position.x, parentTransform.position.y + radius, parentTransform.position.z);
		hatTransform.up = Vector3.up;
	}
}
