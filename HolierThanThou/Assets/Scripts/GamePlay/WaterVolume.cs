using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterVolume : MonoBehaviour
{
    public float waterResistance;

    private float playerDrag;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Rigidbody>();

        if(player)
        {
            // players drag when entering thr volume is stored as a variable to be used later
            playerDrag = player.drag;
            //a modifier is applied to the drag to increase it
            player.drag = player.drag * waterResistance;

            Debug.Log("player drag when entering: " + playerDrag + "\nPlayer current drag: " + player.drag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<Rigidbody>();

        if (player)
        {
            // player drag is reset to the value it entered with when exiting.
            player.drag = playerDrag;
        }
    }
}
