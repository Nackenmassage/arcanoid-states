//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public class ShipStateDefault : ShipState
//{
//    protected Transform shipTransform;

//    protected float speed = 10f;
//    protected float xInput;
//    protected float inputMultiplier = 1f;

//    public override Vector3 Velocity { get; protected set; }

//    public override void OnStateEnter()
//    {
//        Ship.Ball.BallSpeed = Ship.Ball.BallDefaultSpeed;
//        shipTransform = Ship.ShipTransform.transform;
//    }

//    public override void OnStateExit()
//    {

//    }

//    public override void OnUpdate()
//    {
//        Movement();
//        ShootBall();
//    }

//    void Movement()
//    {
//        xInput = inputMultiplier * Input.GetAxis("Horizontal");
//        Vector3 _newPos = Ship.transform.position;

//        Velocity = xInput * Time.deltaTime * speed * Vector3.right;
//        _newPos += Velocity;
//        if (_newPos.x < -4)
//        {
//            _newPos = new Vector3(-4f, Ship.transform.position.y, Ship.transform.position.z);

//        }
//        else if (_newPos.x > 4)
//        {
//            _newPos = new Vector3(4f, Ship.transform.position.y, Ship.transform.position.z);
//        }
//        Ship.transform.position = _newPos;
//    }

//    void ShootBall()
//    {
//        if (Input.GetButtonDown("Jump"))
//        {
//            Ship.Ball.Shoot(Ship.MinBallAngleOffset, Ship.MaxBallAngleOffset);
//        }
//    }
//}
