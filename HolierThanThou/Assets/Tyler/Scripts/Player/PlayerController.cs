﻿using System.Collections;
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

    private void Awake()
    {
        m_characterControllerReference = GetComponent<CharacterController>();
        m_currentState = ECharacterState.Moving;
        m_digitalJoystickReference = FindObjectOfType<DigitalJoystick>();
        m_joyButtonReference = FindObjectOfType<JumpButton>();
        m_mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
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
