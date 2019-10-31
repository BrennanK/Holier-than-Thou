using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSmallification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= new Vector3(.02f, 0, .02f);
    }
}
