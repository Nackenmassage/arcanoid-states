using UnityEngine;
using System;

public abstract class ShipState
{
    public virtual Ship Ship { get; set; }
    public virtual Vector3 Velocity { get; protected set; }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnUpdate();
}

public class ShipStateDefault : ShipState
{
	protected Transform shipTransform;

	protected float speed = 10f;
	protected float xInput;
	protected float inputMultiplier = 1f;

	public override Vector3 Velocity { get; protected set; }

	public override void OnStateEnter()
	{
	}

	public override void OnStateExit()
	{

	}

	public override void OnUpdate()
	{
		Movement();
		ShootBall();
	}

	void Movement()
	{
		xInput = inputMultiplier * Input.GetAxis("Horizontal");
		Vector3 _newPos = Ship.transform.position;

		Velocity = xInput * Time.deltaTime * speed * Vector3.right;
		_newPos += Velocity;
		if (_newPos.x < -4)
		{
			_newPos = new Vector3(-4f, Ship.transform.position.y, Ship.transform.position.z);

		}
		else if (_newPos.x > 4)
		{
			_newPos = new Vector3(4f, Ship.transform.position.y, Ship.transform.position.z);
		}
		Ship.transform.position = _newPos;
	}

	void ShootBall()
	{
		if (Input.GetButtonDown("Jump"))
		{
			Ship.Ball.Shoot();
		}
	}
}

public class ShipStateWide : ShipStateDefault
{
	public override void OnStateEnter()
	{
		Ship.ShipTransform.transform.localScale += new Vector3(1f, 0f, 0f);
	}

	public override void OnStateExit()
	{
		Ship.ShipTransform.transform.localScale = Ship.DefaultShipScale;
	}
}

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

public class ShipStateFast : ShipStateDefault
{
	public override void OnStateEnter()
	{
		speed = 40f;
	}

	public override void OnStateExit()
	{
		speed = Ship.ShipDefaultSpeed;
	}
}

public class ShipStateInputSwitch : ShipStateDefault
{
	public override void OnStateEnter()
	{
		inputMultiplier = -1f;
	}

	public override void OnStateExit()
	{
		inputMultiplier = 1f;
	}
}

public class ShipStateMultiBall : ShipStateDefault
{
	public static event Action MultiBallTriggered;
	public override void OnStateEnter()
	{
		MultiBallTriggered?.Invoke();
	}
	//public override void OnStateEnter()
	//{
	//	for (int i = 0; i < 3; i++)
	//	{
	//		Ball ball = GameObject.Instantiate<Ball>(Ship.BallPrefab, Ship.Ball.transform.position, Quaternion.identity, Ship.BallParent);
	//		ball.Player = Ship;
	//		ball.PlayerTransform = Ship.transform;
	//		ball.BallSpeed = 10f;
	//		ball.Shoot(0f, 360f);
	//	}
	//}

	//public override void OnStateExit()
	//{
	//	base.OnStateExit();
	//}
}
