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
        initPoint = BoxRayHit(screenPoint);
    }

    public void SetEnd(Vector3 screenPoint)
    {
        endPoint = BoxRayHit(screenPoint);
    }



    public List<Selectable> GetSelectablesInside(IReadOnlyList<Selectable> allSelectables)
    {
        Vector3 init;
        Vector3 end;
        init.x = initPoint.x < endPoint.x ? initPoint.x : endPoint.x;
        init.z = initPoint.z < endPoint.z ? initPoint.z : endPoint.z;
        end.x  = initPoint.x > endPoint.x ? initPoint.x : endPoint.x;
        end.z  = initPoint.z > endPoint.z ? initPoint.z : endPoint.z;

        List<Selectable> boxedSelectables = new List<Selectable>();  
        foreach(Selectable selectable in allSelectables)
        {
            Vector3 pos = selectable.transform.position;
            if(init.x < pos.x && pos.x < end.x && init.z < pos.z && pos.z < end.z)
            {
                Debug.Log(selectable.name);
                boxedSelectables.Add(selectable);
            }

        }
        return boxedSelectables;
    }
}
