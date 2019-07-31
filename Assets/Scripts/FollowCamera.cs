using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject oPlayerObject = null;
    public Vector3 vOffsetAmount = Vector3.zero;

    void LateUpdate()
    {
        transform.position = oPlayerObject.transform.position + vOffsetAmount;

        Vector3 LookAtDirection = oPlayerObject.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(LookAtDirection, Vector3.up);
    }
}
