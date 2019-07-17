using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStateFastBall : ShipStateDefault
{
    public override void OnStateEnter()
    {
        Ship.Ball.BallSpeed *= 2f;
    }

    public override void OnStateExit()
    {
        Ship.Ball.BallSpeed = Ship.Ball.BallDefaultSpeed;
    }
}
