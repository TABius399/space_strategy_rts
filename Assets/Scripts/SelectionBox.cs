using System.Collections.Generic;
using UnityEngine;
//using System.Collections.Generic;
public class SelectionBox
{
    public Vector3 initPoint {get; private set;}
    public Vector3 endPoint {get; private set;}
    public Camera camera {get; private set;}

    public SelectionBox(Camera camera)
    {
        this.camera = camera;
    }

    // Start is called before the first frame update
    private Vector3 BoxRayHit(Vector3 screenPoint)
    {
        Ray ray = this.camera.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out RaycastHit hitPoint))
        {
            return hitPoint.point;
        }
        return Vector3.zero;
    }

    public void SetInit(Vector3 screenPoint)
    {
        this.initPoint = BoxRayHit(screenPoint);
    }

    public void SetEnd(Vector3 screenPoint)
    {
        this.endPoint = BoxRayHit(screenPoint);
    }


    public List<Selectable> GetSelectablesInside(IReadOnlyList<Selectable> allSelectables)
    {
        Vector3 init;
        Vector3 end;
        init.x = this.initPoint.x < this.endPoint.x ? this.initPoint.x : this.endPoint.x;
        init.z = this.initPoint.z < this.endPoint.z ? this.initPoint.z : this.endPoint.z;
        end.x  = this.initPoint.x > this.endPoint.x ? this.initPoint.x : this.endPoint.x;
        end.z  = this.initPoint.z > this.endPoint.z ? this.initPoint.z : this.endPoint.z;

        List<Selectable> boxedSelectables = new List<Selectable>();  
        foreach(Selectable selectable in allSelectables)
        {
            Vector3 pos = selectable.transform.position;
            if(init.x < pos.x && pos.x < end.x && init.z < pos.z && pos.z < end.z)
            {
                boxedSelectables.Add(selectable);
            }

        }
        return boxedSelectables;
    }
}
