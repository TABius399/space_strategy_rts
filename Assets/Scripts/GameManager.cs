using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SelectionBox selectionBox;
    public UIManager uIManager;

    private List<Selectable> currentSelection = new List<Selectable>();
    private List<Selectable> allSelectables = new List<Selectable>();
    
    private Vector2 moveOrderPos;
    
    public Selectable testShipPF;
    public Selectable capitalShipPF;
    public Selectable targetPF;

    public Transform spawner = null;

    private Player player;
    private World world;
    // Start is called before the first frame update
    void Start()
    {
        this.world = new World(new Vector2(300, 300));
        this.player = this.world.AddPlayer();
        this.selectionBox = null;
        this.uIManager.spawnShip += this.SpawnShip;
        this.uIManager.SetWorld(this.world);
        this.player.spawnerPosition = this.spawner.position;
        this.world.onShipAdded += this.OnShipCreated;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.selectionBox = new SelectionBox(Camera.main);
            this.selectionBox.SetInit(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {

            this.selectionBox.SetEnd(Input.mousePosition);
            if((this.selectionBox.initPoint - this.selectionBox.endPoint).magnitude < 0.5f)
            { 
                LeftClickSelection();
            } 
            else 
            {
                foreach(Selectable selectable in this.currentSelection)
                {
                    selectable.OnDeselect();
                }

                this.currentSelection = this.selectionBox.GetSelectablesInside(allSelectables);

                foreach(Selectable selectable in this.currentSelection)
                {
                    selectable.OnSelect();
                }
            }
            this.selectionBox = null;
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spaced TestShip");
            this.SpawnShip(ShipType.Test);
        }
    }
    void OnRightClick()
    {
        Vector2 moveOrderPos = this.GetMouseWorldPosition();   

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

    public void SpawnShip(ShipType shipType)
    {   
        this.player.SpawnShip(shipType);
    }

    
    public void OnShipCreated(Ship ship)
    {
        Selectable prefab = null;
        if (ship is CapitalShip) 
        {
            prefab = this.capitalShipPF;
        } else if (ship is TestShip) {
            prefab = this.testShipPF;
        }

        Selectable newShip = Object.Instantiate<Selectable>(prefab);
        newShip.SetShip(ship);
        this.allSelectables.Add(newShip);
        ship.GoTo(this.GetMouseWorldPosition());
    }

    private Vector2 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return new Vector2(hitInfo.point.x, hitInfo.point.z);
        }
        return Vector2.zero;
    }

}
