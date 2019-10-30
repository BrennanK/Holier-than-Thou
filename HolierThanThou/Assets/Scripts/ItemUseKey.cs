using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseKey : MonoBehaviour
{
    PowerUpTracker tracker;

    // Update is called once per frame
    void Update()
    {
        tracker = transform.GetComponent<PowerUpTracker>();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tracker.UseItem1();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            tracker.UseItem2();
        }
    }
}
