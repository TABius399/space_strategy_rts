using System.Collections.Generic;
using UnityEngine;

public class World
{   
    private List<Ship> ships = new List<Ship>();
    private List<Player> players = new List<Player>();
    public Vector2 size;
    private Color[] playersColors = new Color[]
    {
        new Color(1f, 0f, 0f),
        new Color(0f, 1f, 0f),
        new Color(0f, 0f, 1f),
        new Color(1f, 1f, 0f),
        new Color(1f, 0f, 1f),
        new Color(0f, 1f, 1f),
        new Color(0f, 0f, 0f),
        new Color(1f, 1f, 1f)
    };
    
    public event System.Action<Ship> onShipAdded;

    public World(Vector2 size)
    {
        this.size = size;
    }

    public Player AddPlayer()
    {
        Color color = playersColors[this.players.Count];
        Player player = new Player(this, color);
        this.players.Add(player);
        return player;
    }

    public void AddShip(Ship ship)
    {
        this.ships.Add(ship);
        this.onShipAdded?.Invoke(ship);
    }

}