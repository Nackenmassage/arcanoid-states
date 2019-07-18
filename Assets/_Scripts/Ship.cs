using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
	[SerializeField] private Transform shipTransform;
	[SerializeField] private Vector3 defaultShipScale = new Vector3(1.2f, 0.3f, 1f);
	[SerializeField] private float shipDefaultSpeed = 20f;

    public ShipState currentState { get; private set; }
    public Ball Ball { get; set; }
    public Transform ShipTransform { get => shipTransform; }

	public Vector3 DefaultShipScale { get => defaultShipScale; }

	public float ShipDefaultSpeed { get => shipDefaultSpeed; }

	public void Init(Vector3 _startPos)
	{
		transform.position = _startPos;
		ChangeState(new ShipStateDefault());
	}

    void Update()
    {
        currentState?.OnUpdate();
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeState(new ShipStateFast());
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeState(new ShipStateWide());
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeState(new ShipStateFastBall());
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeState(new ShipStateInputSwitch());
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeState(new ShipStateMultiBall());
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("expandPU"))
    //    {

    //    }
    //}

    public void ChangeState(ShipState _state)
    {
        currentState?.OnStateExit();
        currentState = _state;
        currentState.Ship = this;
        currentState.OnStateEnter();
    }
}
