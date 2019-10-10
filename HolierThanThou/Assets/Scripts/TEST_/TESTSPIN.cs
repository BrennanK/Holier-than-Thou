using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSPIN : MonoBehaviour
{
    bool canStart;
    public Vector3 MOVErATE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            transform.Rotate(MOVErATE);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            canStart = true;
        }
    }
}
