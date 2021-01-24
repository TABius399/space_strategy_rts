using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name;
    public float resources;
    public float orbitalResources;
    public int upgrades;
    public List<Ship> ships;
    public Vector3 spawnerPosition;

    public event System.Action<Ship> onShipSpawned;

    public void SpawnShip(ShipType shipType)
    {   
        Ship ship = null;

        switch (shipType)
        {
            case ShipType.Capital: 
                ship = new CapitalShip(this.spawnerPosition);
                break;
            case ShipType.Test:
                ship = new TestShip(this.spawnerPosition);
                break;
        }

        this.onShipSpawned?.Invoke(ship);

    }
}