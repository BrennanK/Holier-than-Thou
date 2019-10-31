using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxChange : MonoBehaviour
{
    PowerUpBox box;

    void Update()
    {
        box = transform.GetComponent<PowerUpBox>();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            box.itemNumber = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            box.itemNumber = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            box.itemNumber = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            box.itemNumber = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            box.itemNumber = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            box.itemNumber = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            box.itemNumber = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            box.itemNumber = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            box.itemNumber = 9;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            box.itemNumber = 10;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            box.itemNumber = 0;
        }

    }
}
