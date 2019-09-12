using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButtonExample : MonoBehaviour
{

    public float speed;

    public Button jumpButton;

    private void Update()
    {
        TstJumpControl();

        if (canJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * speed, ForceMode.Impulse);
            canJump = !canJump;
        }

        TstRelocatePlayer();
    }

    private bool canJump = false;

    public void PlayerJump()
    {
        if (!canJump)
        {
            canJump = !canJump;
        }
    }

    private void TstJumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //canJump = !canJump;
            //jumpButton.onClick.Invoke();

            jumpButton.Select();
        }
    }

    private void TstRelocatePlayer()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Transform>().localPosition = Vector3.zero;
        }
    }

}
