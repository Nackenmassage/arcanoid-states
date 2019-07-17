using UnityEngine;

public class ShipStateFast : ShipStateDefault
{
    public override void OnStateEnter()
    {
        speed = 20f;
    }

    public override void OnStateExit()
    {
        speed = Ship.DefaultSpeed;
    }
}
