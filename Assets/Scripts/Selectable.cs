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
        Vector3 direction = (this.target - this.transform.position);
        if (direction.magnitude > 0.5f)
        {
            this.transform.position += direction.normalized * Time.deltaTime;
        }
    }

    public void GoTo(Vector2 target)
    {
        Vector3 target3d = new Vector3(target.x, this.transform.position.y, target.y); //Raycast hit location
        var originalRotation = this.transform.rotation;
        this.transform.LookAt(target3d);
        var targetRotation = this.transform.rotation;
        Debug.Log(originalRotation);
        Debug.Log(targetRotation);
        this.transform.rotation = originalRotation;
        while (targetRotation != originalRotation) {
            originalRotation = this.transform.rotation;
            this.transform.rotation = Quaternion.RotateTowards(originalRotation, targetRotation, rotateSpeed * Time.deltaTime);
            bool rotMatch = Math.Abs(targetRotation.y - originalRotation.y) < 0.1f;  
            if ( rotMatch )
            {
                originalRotation = targetRotation;
            }
        }
        this.target = target3d;
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
