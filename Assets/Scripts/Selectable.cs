using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public string name = "";

    private Vector3 target;
    private float rotateSpeed = 250f;

    private void Start()
    {
        this.target = this.transform.position;
    }

    void Update()
    {        
        Vector3 lookVector = (this.target - this.transform.position);
        float angle = Vector3.SignedAngle(this.transform.forward, lookVector, Vector3.up);
        float direction = Vector3.SignedAngle(Vector3.forward, this.transform.forward, Vector3.up);

        if (Math.Abs(angle) < 0.2f)
        {
            if (lookVector.magnitude > 0.5f)
            {
                this.transform.position += lookVector.normalized * Time.deltaTime;
            }
        } 
        else 
        {
            var maxRotation = 360f * Time.deltaTime;
            direction += Mathf.Clamp(angle, -maxRotation, maxRotation);
            this.transform.rotation = Quaternion.AngleAxis(direction, Vector3.up);
        }
    }

    public void GoTo(Vector2 target)
    {
        this.target = new Vector3(target.x, this.transform.position.y, target.y); //Raycast hit location;
    }

    public void OnSelect()
    {
        Debug.Log(name + " has been selected");
    }

    public void OnDeselect()
    {
        Debug.Log(name + " has been deselected");
    }
}
