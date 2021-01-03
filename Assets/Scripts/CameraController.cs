using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public float cameraSpeedMulti = 5f;
    private List<Selectable> currentSelection = new List<Selectable>();
    public List<Selectable> allSelectables;
    private SelectionBox selectionBox;
    private Vector2 moveOrderPos;

    public Selectable shipPrefab;

    void Start()
    {
        this.selectionBox = null;
        Selectable miNuevaNave = Object.Instantiate<Selectable>(this.shipPrefab, new Vector3(0,0,0), Quaternion.identity);
        miNuevaNave.GoTo
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
            Debug.Log("DOWNNN");
            selectionBox = new SelectionBox(Camera.main);
            selectionBox.SetInit(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {

            selectionBox.SetEnd(Input.mousePosition);
            Debug.Log((selectionBox.initPoint - selectionBox.endPoint).magnitude);
            if((selectionBox.initPoint - selectionBox.endPoint).magnitude < 0.5f){ 
                LeftClickSelection();
            } else {
                currentSelection = selectionBox.GetSelectablesInside(allSelectables);
            }
            selectionBox = null;
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


        for (int i = 0; i<currentSelection.Count; i++)
        {
            int side = i % 2;
            int column = i / 2;
            currentSelection[i].GoTo(moveOrderPos + new Vector2(side * 5f, column * 5f));
        }
    }

    void LeftClickSelection()
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

    void LeftClickSelectionBox()
    {

    }
}
