using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void SpawnShipCallback(ShipType ship);

    public event SpawnShipCallback spawnShip;
    public Button spawnButton;
    [SerializeField]
    private Minimap minimap = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnButtonPressed()
    {
        this.spawnButton.interactable = false;  
        StartCoroutine(ActivateButton(this.spawnButton, 5f));
    }

    public void SpawnCapitalButtonPressed()
    {
        this.spawnShip?.Invoke(ShipType.Capital);
        this.SpawnButtonPressed();
    }

    public void SpawnTestShipButtonPressed()
    {
        this.spawnShip?.Invoke(ShipType.Test);
        this.SpawnButtonPressed();
    }

    public void SetWorld(World world)
    {
        this.minimap.SetWorld(world);
        world.onShipAdded += this.minimap.AddShip;
    }

    private IEnumerator ActivateButton(Button button, float time)
    {
        yield return new WaitForSeconds(time);

        button.interactable = true;
    }

}
