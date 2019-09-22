using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 OffsetBase;
    private Vector3 ModifiedOffset;
    public float ZoomAmount = 1.0f;
    public float smoothening = 5.0f;
    public GameObject FollowObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FollowObject) 
        {
            ModifiedOffset = OffsetBase * ZoomAmount;
            Vector3 targetPos = FollowObject.transform.position + ModifiedOffset;
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothening * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(-OffsetBase.normalized);
        }
    }
}
