using UnityEngine;

public abstract class Ship
{
    public abstract string name {get;}
    public float speed;
    public float hp;
    public Vector3 position;
    public Vector3 moveTarget;
    public float direction;

    public Ship(Vector3 position)
    {
        this.position = position;
    }

    public void Tick()
    {
        Vector3 forward = Quaternion.AngleAxis(this.direction, Vector3.up) * Vector3.forward;
        Vector3 lookVector = (this.moveTarget - this.position);
        float angle = Vector3.SignedAngle(forward, lookVector, Vector3.up);
        float direction = Vector3.SignedAngle(Vector3.forward, forward, Vector3.up);
        
        if (Mathf.Abs(angle) < 0.2f)
        {
            if (lookVector.magnitude > 0.5f)
            {
                this.position += lookVector.normalized * Time.deltaTime;
            }
        } 
        else 
        {
            var maxRotation = 360f * Time.deltaTime;
            this.direction += Mathf.Clamp(angle, -maxRotation, maxRotation);
        }
    }

    public void GoTo(Vector2 target)
    {
        this.moveTarget = new Vector3(target.x, this.position.y, target.y); //Raycast hit location;
        Debug.Log(this.moveTarget);
    }
}