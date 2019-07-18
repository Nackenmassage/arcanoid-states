using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	[System.Serializable]
	private class Settings
	{
		public Ball BallPrefab;

		public Vector3 ShipStartPos;

		public int StartNumShips = 3;
		public int NumMultiBalls = 3;
		public float BallYOffset = 0.5f;
	}
	[SerializeField] private Settings settings;

	public Stack<Ball> BallPool { get; set; } = new Stack<Ball>(4);

	private Ship ship;
	private Drain drain;
	private Transform activeBalls;

	private int score;
	private int numShipsLeft;

	void Awake()
	{
		Init();
	}

	void Init()
	{
		activeBalls = GameObject.Find("BallParent").transform;
		drain = FindObjectOfType<Drain>();
		drain.BallInDrain += OnBallInDrain;
		ship = FindObjectOfType<Ship>();
		ShipStateMultiBall.MultiBallTriggered += OnMultiBallTriggered;
		ship.Init(settings.ShipStartPos);
		numShipsLeft = settings.StartNumShips;
		for (int i = 0; i < settings.NumMultiBalls + 1; i++)
		{
			Ball _ball = Instantiate<Ball>(settings.BallPrefab);
			_ball.Ship = ship;
			_ball.transform.parent = ship.transform;
			BallPool.Push(_ball);
		}
		SpawnOnShip(BallPool.Pop());
	}

	void OnDisable()
	{
		drain.BallInDrain -= OnBallInDrain;
		ShipStateMultiBall.MultiBallTriggered -= OnMultiBallTriggered;
	}

	public void OnBallInDrain(Ball _b)
	{
		if (BallPool.Count < settings.NumMultiBalls)
		{
			_b.Init();
			_b.gameObject.SetActive(false);
			BallPool.Push(_b);
			_b.transform.parent = ship.transform;

			if (BallPool.Count == settings.NumMultiBalls)
			{
				ship.Ball = activeBalls.GetComponentInChildren<Ball>();
			}
		}
		else if (numShipsLeft > 0)
		{
			numShipsLeft--;
			ship.Init(settings.ShipStartPos);
			_b.Init();
			SpawnOnShip(_b);
		}
		else
		{
			// Game Over
		}
	}

	public void SpawnOnShip(Ball _ball)
	{
		ship.Ball = _ball;
		_ball.gameObject.SetActive(true);
		_ball.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y + settings.BallYOffset, ship.transform.position.z);
		_ball.transform.parent = ship.transform;
	}

	public void OnMultiBallTriggered()
	{
		for (int i = 0; i < settings.NumMultiBalls; i++)
		{
			Ball _mb = BallPool.Pop();
			_mb.gameObject.SetActive(true);
			_mb.transform.position = ship.Ball.transform.position;
			_mb.transform.parent = activeBalls;
			_mb.Shoot(0f, 360f);
		}
	}
}

