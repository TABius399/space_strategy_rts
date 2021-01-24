using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float cameraSpeed = 5f;
    private float cameraMulti = 5f;
    private float deltaZoom = 300f;
    private Vector3 cameraMove;

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.cameraMove = Vector3.zero;
        Rotation();
        Zoom();
        Movement();
        this.transform.position += cameraMove * cameraSpeed * Time.deltaTime;
    }

    private void Zoom()
    {
        
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            this.cameraMove -= new Vector3(0f, this.deltaZoom * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.KeypadMinus) | Input.mouseScrollDelta.y < 0)
        {
            this.cameraMove += new Vector3(0f, this.deltaZoom * Time.deltaTime, 0f);
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            this.cameraMove -= new Vector3(0f, this.deltaZoom * Time.deltaTime * 10, 0f);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            this.cameraMove += new Vector3(0f, this.deltaZoom * Time.deltaTime * 10, 0f);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.cameraMove *= this.cameraMulti;
        }  
    }
    private void Movement()
    {
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow))
        {
            cameraMove += new Vector3(0f, 0f, 1f);

        }
        if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow))
        {
            cameraMove += new Vector3(0f, 0f, -1f);

        }
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow))
        {
            cameraMove += new Vector3(1f, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            cameraMove += new Vector3(-1f, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraMove *= cameraMulti;
        }      
    }
    private void Rotation()
    {

    }

}
