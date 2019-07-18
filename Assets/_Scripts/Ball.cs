using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[Range(0f, 1f)] [SerializeField] private float shipBias = 0.25f;
	[SerializeField] private float maxStartAngleOffset = 5f;
	[SerializeField] private float minStartAngleOffset = 15f;
	[SerializeField] private float ballDefaultSpeed = 10f;

	private Rigidbody rb;
	private Ship ship;
	private Transform playerTransform;

	private Vector3 initialVel;
	private Vector3 lastFrameVel;

	private float startYPos;
	private bool wasShot;

	public Ship Ship { set => ship = value; }

	public float BallDefaultSpeed { get => ballDefaultSpeed; }
	public float BallSpeed { get; set; } = 10f;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		lastFrameVel = rb.velocity;
	}

	// Calls Reset() when the ball hits a block
	void OnCollisionEnter(Collision collision)
	{
		bool _withShip = collision.collider.CompareTag("Player");
		Bounce(collision.GetContact(0).normal, _withShip);
	}

	public void Shoot()
	{
		Shoot(minStartAngleOffset, maxStartAngleOffset);
	}

	// Calculates Vector3 to shoot at and give the ball a velocity
	public void Shoot(float minStartAngleOffset, float maxStartAngleOffset)
	{
		if (wasShot) { return; }
		wasShot = true;
		transform.parent = GameObject.Find("BallParent").transform;
		rb.isKinematic = false;
		Vector3 _dir = RotateBallRandDir(Vector3.up, minStartAngleOffset, maxStartAngleOffset);
		rb.velocity = _dir * BallSpeed;
	}

	Vector3 RotateBallRandDir(Vector3 _dir, float minStartAngleOffset, float maxStartAngleOffset)
	{
		float _randSign = Mathf.Sign(Random.Range(-1f, 1f));
		float _angle = _randSign * Random.Range(minStartAngleOffset, maxStartAngleOffset);
		Quaternion _rot = Quaternion.AngleAxis(_angle, Vector3.forward);
		return _rot * _dir;
	}

	// Calculates where the ball bounces off to
	void Bounce(Vector3 _normal, bool _withPlayer)
	{
		Vector3 _dir = Vector3.Reflect(lastFrameVel.normalized, _normal.normalized);
		float _bias = _withPlayer ? shipBias : 0f;
		Vector3 _newDir = Vector3.Lerp(_dir, ship.currentState.Velocity.normalized, _bias).normalized;
		rb.velocity = _newDir * BallSpeed;
	}

	public void Init()
	{
		BallSpeed = ballDefaultSpeed;
		rb.isKinematic = true;
		rb.velocity = Vector3.zero;
		wasShot = false;
	}
}
