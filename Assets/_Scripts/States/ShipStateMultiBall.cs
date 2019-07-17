using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStateMultiBall : ShipStateDefault
{
    public override void OnStateEnter()
    {
        for (int i = 0; i < 3; i++)
        {
            Ball ball = GameObject.Instantiate<Ball>(Ship.BallPrefab, Ship.Ball.transform.position, Quaternion.identity, Ship.BallParent);
            ball.Player = Ship;
            ball.PlayerTransform = Ship.transform;
            ball.BallSpeed = 10f;
            ball.Shoot(0f, 360f);
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
