using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCharacter : MonoBehaviour
{

    public float m_fMaxPlayerSpeed = 6.0f;
    public float m_fAccelerationRate = 0.05f;
    public float m_fSeekDistance = 5.0f;
    public float m_fDecelerationRate = 1.0f;
    private Vector3 m_vecVelocity = Vector3.zero;
    private Vector3 m_vecAcceleration = Vector3.zero;
    private Vector3 m_vecTarget = Vector3.zero;
    private bool bClicked = false;
    public bool bAnchorDropped = false;

    public Camera m_MainCamera;
    private Rigidbody m_PlayerRigidBody;
    private CameraFollow CameraFollowScript;
    public GameObject CurrentFishingSpot;

    public EDamageType m_PlayerDamageType = EDamageType.NONE;
    
    void Start(){
        if (Camera.main != null && m_MainCamera == null)
        {
            m_MainCamera = Camera.main;
        }
        m_vecTarget = transform.position;

        m_PlayerRigidBody = GetComponent<Rigidbody>();
        if (!m_PlayerRigidBody)
        {
            Debug.Log("No rigidBody");
        }

        CameraFollowScript = m_MainCamera.GetComponent<CameraFollow>();
        bAnchorDropped = false;
    }

    void Update()
    {
        m_vecAcceleration = Vector3.zero;
        ProcessPlayerMovement();
        ProcessAcceleration();
    }

    private void ProcessPlayerMovement()
    {
        Vector3 vecDirection;

        if (bAnchorDropped == true) return;

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            bClicked = Input.GetMouseButton(0) ? true : false;

            if (bClicked)
            {

                Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000000.0f, -5, QueryTriggerInteraction.Ignore))
                {
                    vecDirection = hit.point - transform.position;
                    vecDirection.y = 0.0f;
                    float fMagnitude = vecDirection.magnitude;
                    fMagnitude = Mathf.Clamp(fMagnitude, 0.0f, m_fMaxPlayerSpeed);
                    m_vecAcceleration = vecDirection.normalized * fMagnitude;
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

        if (m_vecVelocity.normalized.magnitude != 0.0)
        {
            m_PlayerRigidBody.MoveRotation(Quaternion.LookRotation(m_vecVelocity.normalized));
        }

    }

    private void ProcessAcceleration()
    {
        m_vecVelocity += m_vecAcceleration * m_fAccelerationRate;
        m_vecVelocity = Vector3.ClampMagnitude(m_vecVelocity, m_fMaxPlayerSpeed);
        transform.position += (m_vecVelocity * Time.deltaTime);

        if (bAnchorDropped == true) m_vecVelocity *= (m_fDecelerationRate / 1.1f);
        else m_vecVelocity *= m_fDecelerationRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FishingSpot")
        {
            CameraFollowScript.ZoomAmount = 2.0f;
            CameraFollowScript.FollowObject = other.gameObject;
            CurrentFishingSpot = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FishingSpot")
        {
            CameraFollowScript.ZoomAmount = 1.0f;
            CameraFollowScript.FollowObject = gameObject;

            CurrentFishingSpot = null;
        }
    }

}
