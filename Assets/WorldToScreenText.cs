using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreenText : MonoBehaviour
{
    public GameObject Parent;
    public Camera Cam;

    private TMPro.TextMeshProUGUI TextMesh;

    // Start is called before the first frame update
    void Start()
    {
        //Cam = Camera.main;
        TextMesh = GetComponent<TMPro.TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenpos = Cam.WorldToScreenPoint(Parent.transform.position);
        TextMesh.transform.position = screenpos + new Vector3(0.0f, 50.0f, 0.0f);
    }
}
