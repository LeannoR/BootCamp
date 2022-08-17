using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float sideWaySpeed;
    public float forwardSpeed = 10f;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        ForwardMovement();
    }

    public void ForwardMovement()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.position = transform.position + Vector3.forward * forwardSpeed * Time.deltaTime;
        }
    }
}