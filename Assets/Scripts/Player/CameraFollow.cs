using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 m_vecOffset;
    public float smoothening = 5.0f;
    public GameObject m_PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PlayerObject) 
        {
            Vector3 targetPos = m_PlayerObject.transform.position + m_vecOffset;
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothening * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(-m_vecOffset.normalized);
        }
    }
}
