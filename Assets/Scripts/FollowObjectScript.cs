using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectScript : MonoBehaviour
{

    public GameObject FollowObject;
    public Vector3 OffsetValue;
    public bool FaceCamera = false;
    private Camera m_Camera;

    void Start() 
    {
        m_Camera = Camera.main;
    }

    void LateUpdate()
    {
        if(FollowObject)
        {
            transform.position = FollowObject.transform.position + OffsetValue;
            if(FaceCamera) 
            {
                transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up);
            }
        }
        
    }
}
