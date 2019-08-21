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
        AltJumpControl();

        if (canJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * speed, ForceMode.Impulse);
            canJump = !canJump;
        }
    }

    private bool canJump = false;

    public void PlayerJump()
    {
        if (!canJump)
        {
            canJump = !canJump;
        }
    }

    private void AltJumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //canJump = !canJump;
            //jumpButton.onClick.Invoke();

            jumpButton.Select();
        }

        

    }

}
