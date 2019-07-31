using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float m_fPlayerSpeed = 6.0f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal") * m_fPlayerSpeed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * m_fPlayerSpeed * Time.deltaTime;

        Vector3 MovementDirection = new Vector3(h, 0.0f, v);

        transform.position += MovementDirection;
    }
}
