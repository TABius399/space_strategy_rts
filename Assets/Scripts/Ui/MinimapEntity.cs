using UnityEngine;
using UnityEngine.UI;

public class MinimapEntity : MonoBehaviour
{   
    private RectTransform rectTransform;

    private Minimap minimap;

    private Ship ship;

    public void Awake()
    {   
        this.rectTransform = this.GetComponent<RectTransform>();
    }

    public void Init(Minimap minimap, Ship ship)
    {
        this.minimap = minimap;
        this.ship = ship;
    }

    public void Update()
    {
        if (this.minimap == null) return;
        if (this.ship == null) return;

        var pos = new Vector2(this.ship.position.x, this.ship.position.z);

        pos = pos / this.minimap.worldSize;
        pos.y = -(1f - pos.y);
        this.rectTransform.anchoredPosition = pos * this.minimap.containerSize;
    }
}