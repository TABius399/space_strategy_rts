using UnityEngine;

public class TestShip : Ship
{
    public TestShip(Vector3 position) : base(position)
    {
    }

    public override string name => "Test Ship";
}