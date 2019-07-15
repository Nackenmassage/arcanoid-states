using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private float xInput;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float strength = 1f;

    private Transform ball;
    private Rigidbody rb;
    private State currentState;

    void Start()
    {
        ball = transform.GetChild(0);
        rb = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentState.OnUpdate();
        Movement();
        ShootBall();
    }

    void Movement()
    {
        xInput = Input.GetAxis("Horizontal");
        Vector3 newPos = transform.position;

        newPos += xInput * Time.deltaTime * speed * Vector3.right;
        if (newPos.x < -4)
        {
            newPos = new Vector3(-4f, transform.position.y, transform.position.z);

        }
        else if (newPos.x > 4)
        {
            newPos = new Vector3(4f, transform.position.y, transform.position.z);
        }
        transform.position = newPos;
    }

    void ShootBall()
    {
        if (Input.GetButtonDown("Jump"))
        {
            float _randomXOffset = Random.Range(-0.5f, 0.5f);
            Vector3 _pos = transform.position;
            Vector3 _randomDir = new Vector3(_pos.x + _randomXOffset, _pos.y + 1f, _pos.z);
            ball.parent = null;
            rb.isKinematic = false;
            rb.AddForce(_randomDir.normalized * strength, ForceMode.Impulse);
        }
    }
}
