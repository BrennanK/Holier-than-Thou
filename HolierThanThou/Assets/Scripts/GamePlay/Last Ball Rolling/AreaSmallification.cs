using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSmallification : MonoBehaviour
{
    GameManager gM;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale -= new Vector3(.02f, 0, .02f);
        if(gM.matchTimer > 3)
        {
            transform.localScale = new Vector3(0.0002f, transform.localScale.y/gM.matchTimer, 0.0002f) * gM.matchTimer;
        }
    }
}
