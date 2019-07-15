using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float strength = 10f;

    private Rigidbody rb;
    private bool didCollide;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        didCollide = true;
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * strength;
        didCollide = false;
    }
}
