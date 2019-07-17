using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStateWide : ShipStateDefault
{
    public override void OnStateEnter()
    {
        shipTransform.localScale = new Vector3(2f, 0.3f, 1f);
    }

    public override void OnStateExit()
    {
        //shipTransform = Ship.DefaultShipTransform.transform;
        shipTransform.localScale = new Vector3(1f, 0.3f, 1f);
    }
}
