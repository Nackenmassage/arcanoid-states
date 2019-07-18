using System;
using UnityEngine;

public class Drain : MonoBehaviour
{
	public event Action<Ball> BallInDrain;

	void OnTriggerEnter(Collider other)
	{
		var _ball = other.GetComponent<Ball>();
		BallInDrain?.Invoke(_ball);
	}
}
