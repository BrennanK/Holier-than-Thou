using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyControl : MonoBehaviour
{
    // Movement Variables
    public float speed;
    public float jumpForce;
    public float downAccel;
    public float airFriction;
    public float disToGround;
    public float inputDelay;
    public LayerMask ground;
    Rigidbody rBody;
    Vector3 direction, jumpDirection;

    //Button Function Variable
    private DigitalJoystick m_digitalJoystickReference;
    private JumpButton m_joyButtonReference;

    //Camera Function Variable
    private Camera m_mainCamera;

    void Start()
    {
        //Initialization
        rBody = GetComponent<Rigidbody>();
        direction = Vector3.zero;
        m_digitalJoystickReference = FindObjectOfType<DigitalJoystick>();
        m_joyButtonReference = FindObjectOfType<JumpButton>();
        m_mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        GetInput();
    }

    void FixedUpdate()
    {
        Run();
        Jump();
    }

    void GetInput()
    {
        jumpDirection = new Vector3(0.0f, 1.0f, 0.0f);
    }

    void Run()
    {
        if (Mathf.Abs(direction.magnitude) > inputDelay && Grounded())
        { 
            rBody.AddForce(direction, ForceMode.Acceleration);
        } 
        else if ((Mathf.Abs(direction.magnitude) > inputDelay && !Grounded()))
        {
            rBody.AddForce(direction / airFriction, ForceMode.Acceleration);
        }
    }

    void Jump()
    {
        if (m_joyButtonReference.pressed && Grounded()) 
        {
            rBody.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            Debug.Log("Y velocity While Jump: " + rBody.velocity.y);

        }
        else {
            rBody.AddForce(Physics.gravity * downAccel, ForceMode.Force);
        }
    }

    //Ground Check
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, disToGround, ground);
    }

    //Movement Handle and Camera Handle from Old Script
    private void HandleMovement()
    {
        Vector3 t_cameraForward = Vector3.ProjectOnPlane(m_mainCamera.transform.forward, Vector3.up);
        Vector3 t_cameraRight = Vector3.ProjectOnPlane(m_mainCamera.transform.right, Vector3.up);
        Vector3 t_movementDirectionInRelationToCamera = (t_cameraForward * Input.GetAxis("Vertical")) + (t_cameraRight * Input.GetAxis("Horizontal"));


        float previousYVelocity = direction.y;
        direction = t_cameraForward * m_digitalJoystickReference.Vertical * speed;
        direction += t_cameraRight * m_digitalJoystickReference.Horizontal * speed;
        direction.y = previousYVelocity;

        transform.LookAt(transform.position + direction);
    }
}

