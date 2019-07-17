using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform ballParent;
    [SerializeField] private float defaultSpeed = 20f;
    [SerializeField] private float maxBallAngleOffset = 5f;
    [SerializeField] private float minBallAngleOffset = 15f;

    private Vector3 startPos;

    public ShipState currentState { get; private set; }
    public Ball Ball { get; private set; }
    public Ball BallPrefab { get => ballPrefab; }
    public Transform BallParent { get => ballParent; }
    public Transform ShipTransform { get; private set; }

    public float DefaultSpeed { get => defaultSpeed; }
    public float MaxBallAngleOffset => maxBallAngleOffset;
    public float MinBallAngleOffset => minBallAngleOffset;

    void Start()
    {
        Ball = GetComponentInChildren<Ball>();
        Ball.Player = this;
        Ball.PlayerTransform = transform;
        Ball.Reset();
        GameObject _shipObject = GameObject.Find("ShipMesh");
        ShipTransform = _shipObject.transform;
        ChangeState(new ShipStateDefault());
        startPos = transform.position;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("expandPU"))
        {

        }
    }

    public void ChangeState(ShipState _state)
    {
        currentState?.OnStateExit();
        currentState = _state;
        currentState.Ship = this;
        currentState.OnStateEnter();
    }

    public void Reset()
    {
        transform.position = startPos;
        Ball.Reset();
    }
}
