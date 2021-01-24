using System.Collections.Generic;
using UnityEngine;

public class World
{   
    private List<Ship> ships = new List<Ship>();
    private List<Player> players = new List<Player>();
    public Vector2 size;
    
    public event System.Action<Ship> onShipAdded;

    public World(Vector2 size)
    {
        this.size = size;
    }

    public Player AddPlayer()
    {
        Player player = new Player(this);
        this.players.Add(player);
        return player;
    }

    public void AddShip(Ship ship)
    {
        this.ships.Add(ship);
        this.onShipAdded?.Invoke(ship);
    }

}