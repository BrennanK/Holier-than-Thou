using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour
{
    float timer;

    public GameObject north;
    public GameObject south;
    public GameObject east;
    public GameObject west;

    public GameObject wallTarget;
    Vector3 trueDes;

    // Start is called before the first frame update
    void Start()
    {
        trueDes = new Vector3(wallTarget.transform.position.x, transform.position.y, wallTarget.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        north.transform.position += Vector3.MoveTowards(transform.position, trueDes, .25f * Time.deltaTime);
        east.transform.position += Vector3.MoveTowards(transform.position, trueDes, .25f * Time.deltaTime);
        west.transform.position += Vector3.MoveTowards(transform.position, trueDes, .25f * Time.deltaTime);
        south.transform.position += Vector3.MoveTowards(transform.position, trueDes, .25f * Time.deltaTime);

    }
}
