﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float m_fMaxPlayerSpeed = 6.0f;
    public float m_fAccelerationRate = 0.05f;
    public float m_fSeekDistance = 5.0f;
    public float m_fDecelerationRate = 1.0f;
    private Vector3 m_vecVelocity = Vector3.zero;
    private Vector3 m_vecAcceleration = Vector3.zero;
    private Vector3 m_vecTarget = Vector3.zero;
    Camera m_MainCamera;

    private bool bClicked = false;

    void Start(){
        if (Camera.main != null)
        {
            m_MainCamera = Camera.main;
        }
        m_vecTarget = transform.position;
    }

    void Update() 
    {
        Debug.DrawLine(transform.position, transform.position + m_vecVelocity, Color.red);
        m_vecAcceleration = Vector3.zero;
        Vector3 vecDirection;

        
        //If the main camera is invalid or there are no keys being pressed, dont run update
        if (!m_MainCamera || !Input.anyKey) return;

        bClicked = Input.GetMouseButton(0) ? true : false;
        
        if (bClicked)
        {
            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000000.0f))
            {
                vecDirection = hit.point - transform.position;
                float fMagnitude = vecDirection.magnitude;
                fMagnitude = Mathf.Clamp(fMagnitude, 0.0f, m_fMaxPlayerSpeed);
                m_vecAcceleration = vecDirection.normalized * fMagnitude;
                return;
            }
        } 
        else 
        {
            //Get the input from the game engine and calculating the desired target location for the player
            float fHorizontalSpeed = Input.GetAxis("Horizontal");
            float fVerticalSpeed = Input.GetAxis("Vertical");
            Vector3 vecCameraForward2D = Vector3.Scale(m_MainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f));
            Vector3 vecCameraRight2D = Vector3.Scale(m_MainCamera.transform.right, new Vector3(1.0f, 0.0f, 1.0f));

            vecDirection = ((vecCameraForward2D * fVerticalSpeed) + (vecCameraRight2D * fHorizontalSpeed)).normalized;
            m_vecAcceleration = vecDirection * m_fMaxPlayerSpeed;
        }

        
    }

    void FixedUpdate()
    {
        m_vecVelocity += m_vecAcceleration * m_fAccelerationRate;
        m_vecVelocity = Vector3.ClampMagnitude(m_vecVelocity, m_fMaxPlayerSpeed);
        transform.position += m_vecVelocity * Time.deltaTime;

        m_vecVelocity *= m_fDecelerationRate;
    }
}
