using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCamera : MonoBehaviour
{
    public GameObject camera;
    void Start()
    {
        Instantiate(camera, transform);
        transform.GetComponentInChildren<Cinemachine.CinemachineFreeLook>().Follow = transform;
        transform.GetComponentInChildren<Cinemachine.CinemachineFreeLook>().LookAt = transform;
    }
}
