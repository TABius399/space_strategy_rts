using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{   
    [SerializeField]
    private RectTransform container = null;
    
    [SerializeField]
    private MinimapEntity entityBase = null;

    private World world;

    private Dictionary<Ship, MinimapEntity> entities = new Dictionary<Ship, MinimapEntity>();

    void Start()
    {
        this.entityBase.gameObject.SetActive(false);
    }

    public Vector2 worldSize => this.world.size;
    public Vector2 containerSize => this.container.rect.size;

    public void SetWorld(World world)
    {
        this.world = world;
    }

    public void AddShip(Ship ship)
    {
        MinimapEntity entity = Object.Instantiate(this.entityBase);
        entity.transform.SetParent(this.container);
        entity.Init(this, ship);
        entity.gameObject.SetActive(true);
        this.entities.Add(ship, entity);
    }

    public void RemoveShip(Ship ship)
    {
        var entity = this.entities[ship];
        Object.Destroy(entity);
        this.entities[ship] = null;
    }
}