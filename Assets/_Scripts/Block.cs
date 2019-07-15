using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

