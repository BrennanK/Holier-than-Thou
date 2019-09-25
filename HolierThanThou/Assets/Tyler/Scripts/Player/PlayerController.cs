using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ECharacterState
    {
        Moving,
        Attacking,
        KnockedBack
    }

    private Camera m_mainCamera;


    //Ground Check SphereCast Variables
    public Transform origin;
    public LayerMask layerMask;
    public float radius;

    //Jump Variables
    public float jumpHeight = 5;
    public float timeToJump = .5f;
    private float gravity;
    private float jumpVelocity;
    private bool canJump;


    private float m_characterSpeed = 10f;
    [Header("Ground Movement")]
    [Range(0, 1)]
    public float groundDamping;

    // Cached References
    private CharacterController m_characterControllerReference;
    private Vector3 m_movementVector;
    private DigitalJoystick m_digitalJoystickReference;
    private JumpButton m_joyButtonReference;
    private Transform m_whoIsTag;

    // Tracking Current State
    private ECharacterState m_currentState;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(origin.position, radius);
    }

    private void Awake()
    {
        m_characterControllerReference = GetComponent<CharacterController>();
        m_currentState = ECharacterState.Moving;
        m_digitalJoystickReference = FindObjectOfType<DigitalJoystick>();
        m_joyButtonReference = FindObjectOfType<JumpButton>();
        m_mainCamera = FindObjectOfType<Camera>();

        //Calculating Jump
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJump, 2);
        jumpVelocity = Mathf.Abs(gravity * timeToJump);
    }

    private void Update()
    { 
        RaycastHit hit;

        if (Physics.SphereCast(origin.position, radius, Vector3.down, out hit, 1f, layerMask))
        {
            canJump = true;
        }
        else
            canJump = false;
            
        if(m_joyButtonReference.pressed && canJump)
        {
            Debug.Log("Can Jump");
            m_movementVector.y = jumpVelocity;
        }
        

        


        switch (m_currentState)
        {
            case ECharacterState.Moving:
                HandleMovement();
                break;
            case ECharacterState.Attacking:
                m_movementVector.x = 0;
                m_movementVector.z = 0;
                break;
        }

        float dampingMultiplier = 1f;
        if (m_currentState == ECharacterState.Attacking)
        {
            dampingMultiplier *= 2;
        }

        m_movementVector.x = Mathf.Lerp(m_characterControllerReference.velocity.x, m_movementVector.x, groundDamping * dampingMultiplier);
        m_movementVector.y = 0;
        m_movementVector.z = Mathf.Lerp(m_characterControllerReference.velocity.z, m_movementVector.z, groundDamping * dampingMultiplier);

        if (m_characterControllerReference.enabled)
        {
            m_characterControllerReference.SimpleMove(m_movementVector);
        }
    }

    private void HandleMovement()
    {
        Vector3 t_cameraForward = Vector3.ProjectOnPlane(m_mainCamera.transform.forward, Vector3.up);
        Vector3 t_cameraRight = Vector3.ProjectOnPlane(m_mainCamera.transform.right, Vector3.up);
        Vector3 t_movementDirectionInRelationToCamera = (t_cameraForward * Input.GetAxis("Vertical")) + (t_cameraRight * Input.GetAxis("Horizontal"));
        //t_movementDirectionInRelationToCamera *= movementSpeed;

        m_movementVector = t_cameraForward * m_digitalJoystickReference.Vertical * m_characterSpeed;
        m_movementVector += t_cameraRight * m_digitalJoystickReference.Horizontal * m_characterSpeed;
        transform.LookAt(transform.position + new Vector3(m_movementVector.x, 0f, m_movementVector.z));
    }
}
