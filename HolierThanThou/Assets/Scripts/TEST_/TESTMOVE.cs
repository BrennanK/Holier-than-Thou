using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTMOVE : MonoBehaviour
{

    Vector3 startPos;
    Vector3 endPos;
    public float moveSpeed;

    bool testStart;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + new Vector3(0, 0, 200f);
    }

    // Update is called once per frame
    void Update()
    {
        if(testStart)
        {
            transform.position = Vector3.MoveTowards(startPos, endPos, 0f);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            testStart = true;
        }
    }
}
