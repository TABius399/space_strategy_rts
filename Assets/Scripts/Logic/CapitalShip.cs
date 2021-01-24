using UnityEngine;

public class CapitalShip : Ship
{
    public CapitalShip(Vector3 position, Player player) : base(position, player)
    {
        
    }

    public override string name => "Capital Ship";
}