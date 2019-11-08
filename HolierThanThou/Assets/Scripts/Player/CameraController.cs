using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    GameObject myPLayer;
    CinemachineFreeLook myCamera;

    // Start is called before the first frame update
    void Start()
    {
        myPLayer = GetComponentInParent<Competitor>().gameObject;
        myCamera = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitSphere;
        if(Physics.SphereCast(myPLayer.transform.position, 1f,Vector3.up, out hitSphere,10f) || Physics.SphereCast(myCamera.transform.position, 1f,Vector3.up, out hitSphere, 10f) || Physics.SphereCast(new Vector3(myCamera.transform.position.x, myPLayer.transform.position.y, myCamera.transform.position.z), 1f,Vector3.up, out hitSphere, 10f))
        {
            myCamera.m_YAxis.Value = Mathf.Lerp(myCamera.m_YAxis.Value, .5f, .1f);
        }
        else
        {
            myCamera.m_YAxis.Value = Mathf.Lerp(myCamera.m_YAxis.Value, 1, .1f); ;
        }
    }
    
}
