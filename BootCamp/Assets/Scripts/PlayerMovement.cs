using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 previousMousePos;
    private Rigidbody rigidBody;
    private Animator animator;

    public bool isRunning = false;
    [SerializeField] public float Multiplier = 1f;
    [SerializeField] public float sideWaySpeed = 1f;
    [SerializeField] public float forwardSpeed = 10f;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        ForwardMovement();
        SideWayMovement();
        LockHorizontalPosition();
        LockPlayerRotation();
    }

    public void ForwardMovement()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            transform.position = transform.position + Vector3.forward * forwardSpeed * Time.deltaTime;
        }
    }
    public void SideWayMovement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            previousMousePos = Input.mousePosition;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
            var newpos = Input.mousePosition;
            var difpos = newpos - previousMousePos;
            var horizontal = difpos.x * Time.deltaTime * Multiplier;
            transform.position = transform.position + transform.right * horizontal;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
    }

    public void LockHorizontalPosition()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -3, 3);
        transform.position = pos;
    }
    
    public void LockPlayerRotation()
    {
        var rotation = transform.rotation;
        rotation.z = Mathf.Clamp(transform.rotation.z, 0, 0);
        transform.rotation = rotation;
    }
}