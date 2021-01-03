using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public float cameraSpeedMulti = 5f;
    private List<Selectable> currentSelection = new List<Selectable>();

    private Vector2 moveOrderPos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraMove = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            cameraMove += new Vector3(0f, 0f, 1f);

        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraMove += new Vector3(0f, 0f, -1f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraMove += new Vector3(1f, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraMove += new Vector3(-1f, 0f, 0f);

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraMove *= cameraSpeedMulti;
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick();
        }
        this.transform.position += cameraMove * cameraSpeed * Time.deltaTime;
    }

    void OnRightClick()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            moveOrderPos = new Vector2(hitInfo.point.x, hitInfo.point.z);
        }


        foreach (Selectable selectable in currentSelection)
        {
            selectable.GoTo(moveOrderPos);
        }
    }

    void OnLeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Selectable selectable = hitInfo.transform.GetComponent<Selectable>();

            List<Selectable> oldSelection = new List<Selectable>(currentSelection);
            if (currentSelection.Count > 0)
            {
                foreach (var s in currentSelection)
                {
                    s.OnDeselect();
                }
                currentSelection.Clear();
            }
            
            if (selectable != null && ! oldSelection.Contains(selectable))
            {
                this.currentSelection.Add(selectable);
                selectable.OnSelect();
            }
        }
    }
}
