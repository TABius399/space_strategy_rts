using UnityEngine;

public class TestShip : Ship
{
    public TestShip(Vector3 position, Player player) : base(position, player)
    {
        
    }

    public override string name => "Test Ship";
}