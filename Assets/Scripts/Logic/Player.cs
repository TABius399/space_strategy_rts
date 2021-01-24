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

    private World world;

    public Player(World world)
    {
        this.world = world;
    }

    public void SpawnShip(ShipType shipType)
    {   
        Ship ship = null;

        switch (shipType)
        {
            case ShipType.Capital: 
                ship = new CapitalShip(this.spawnerPosition, this);
                break;
            case ShipType.Test:
                ship = new TestShip(this.spawnerPosition, this);
                break;
        }

        this.world.AddShip(ship);
    }
}