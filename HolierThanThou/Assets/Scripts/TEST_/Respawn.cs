using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.position = transform.position;
        }
    }
}
