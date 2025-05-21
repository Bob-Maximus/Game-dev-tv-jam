using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // Start is called before the first frame update

    public float zoomSpeed = 4f;
    public float minSize = 3f;
    public float maxSize = 12f;

    private Camera cam;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = 5f; 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + zoomSpeed * Time.deltaTime, minSize, maxSize);
        }

        if (Input.GetKey(KeyCode.E))
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - zoomSpeed * Time.deltaTime, minSize, maxSize);
        }
    }
}
