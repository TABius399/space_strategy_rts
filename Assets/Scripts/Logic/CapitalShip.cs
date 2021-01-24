using UnityEngine;

public class CapitalShip : Ship
{
    public CapitalShip(Vector3 position) : base(position)
    {
    }

    public override string name => "Capital Ship";
}