using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float ballDefaultSpeed = 10f;
    [Range(0f, 1f)] [SerializeField] private float playerBias = 0.25f;

    private Rigidbody rb;
    private Ship player;
    private Transform playerTransform;

    private Vector3 initialVel;
    private Vector3 lastFrameVel;

    private float ballSpeed;
    private float startYPos;
    private bool wasShot;

    public Ship Player { set => player = value; }
    public Transform PlayerTransform { set => playerTransform = value; }

    public float BallDefaultSpeed { get => ballDefaultSpeed; }
    public float BallSpeed { get => ballSpeed; set => ballSpeed = value; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        startYPos = transform.position.y;
        //playerTransform = transform.parent;
        //player = playerTransform.GetComponent<Ship>();
    }

    // Calculates Vector3 to shoot at and give the ball a velocity
    public void Shoot(float _minStartAngleOffset, float _maxStartAngleOffset)
    {
        if (wasShot) { return; }
        wasShot = true;
        transform.parent = player.BallParent;
        rb.isKinematic = false;
        float _randSign = Mathf.Sign(Random.Range(-1f, 1f));
        float _angle = _randSign * Random.Range(_minStartAngleOffset, _maxStartAngleOffset);
        Quaternion _rot = Quaternion.AngleAxis(_angle, Vector3.forward);
        rb.velocity = (_rot * Vector3.up) * ballSpeed;
    }

    void Update()
    {
        lastFrameVel = rb.velocity;
    }

    // Calls Reset() when the ball hits a block
    void OnCollisionEnter(Collision collision)
    {
        bool _withPlayer = collision.collider.CompareTag("Player");
        Bounce(collision.GetContact(0).normal, _withPlayer);
    }

    // calls Reset() 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("void"))
        {
            Reset();
        }
    }

    // Calculates where the ball bounces off to
    void Bounce(Vector3 _normal, bool _withPlayer)
    {
        Vector3 _dir = Vector3.Reflect(lastFrameVel.normalized, _normal.normalized);
        float _bias = _withPlayer ? playerBias : 0f;
        Vector3 _newDir = Vector3.Lerp(_dir, player.currentState.Velocity.normalized, _bias).normalized;
        rb.velocity = _newDir * ballSpeed;
    }

    // Resets values of the ball
    public void Reset()
    {
        ballSpeed = ballDefaultSpeed;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(playerTransform.position.x, startYPos, playerTransform.position.z);
        transform.parent = playerTransform;
        wasShot = false;
    }
}
