using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 previousMousePos;
    private Rigidbody rigidBody;
    private Animator animator;
    private Vector3 startingPosition;

    public bool isPlayerInCrouchingArea = false;
    [SerializeField] public bool isPlayerDead = false;
    [SerializeField] public bool isLevelFinished = false;
    [SerializeField] public bool isRunning = false;
    [SerializeField] public float Multiplier = 1f;
    [SerializeField] public float sideWaySpeed = 1f;
    [SerializeField] public float forwardSpeed = 10f;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        startingPosition = transform.position;
    }

    public void Update()
    {
        ForwardMovement();
        SideWayMovement();
        Crouch();
        LockHorizontalPosition();
        LockPlayerRotation();
    }

    public void ForwardMovement()
    {
        if(Input.GetKey(KeyCode.Mouse0) && isLevelFinished == false && isPlayerDead == false)
        {
            transform.position = transform.position + Vector3.forward * forwardSpeed * Time.deltaTime;
        }
    }
    public void SideWayMovement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isLevelFinished == false && isPlayerDead == false)
        {
            previousMousePos = Input.mousePosition;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && isLevelFinished == false && isPlayerDead == false)
        {
            isRunning = true;

            if(isPlayerInCrouchingArea)
            {
                animator.SetBool("StandToCrouch", false);
            }
            else if(!isPlayerInCrouchingArea)
            {
                animator.SetBool("isRunning", true);
            }

            var newpos = Input.mousePosition;
            var difpos = newpos - previousMousePos;
            var horizontal = difpos.x * Time.deltaTime * Multiplier;
            transform.position = transform.position + transform.right * horizontal;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0) && isLevelFinished == false && isPlayerInCrouchingArea == false && isPlayerDead == false)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
    }
    public void Crouch()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && isLevelFinished == false && isPlayerInCrouchingArea == true && isPlayerDead == false)
        {
            isRunning = false;
            animator.SetBool("StandToCrouch", true);
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

    public void Respawn()
    {
        transform.position = startingPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        isPlayerDead = false;
        isPlayerInCrouchingArea = false;
        isRunning = false;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Finish" && isPlayerDead == false)
        {
            isLevelFinished = true;
            animator.SetBool("isLevelFinished", true);
        }

        else if (collider.gameObject.tag == "Knife" && isPlayerDead == false)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
            StartCoroutine(WaitForRespawn(2));
            isPlayerDead = true;
        }

        else if (collider.gameObject.tag == "Rock" && isPlayerDead == false)
        {
            isRunning = false;
            StartCoroutine(WaitForRespawn(2));
            isPlayerDead = true;
        }

        else if(collider.gameObject.tag == "EnteringCrouchArea" && isPlayerInCrouchingArea == false)
        {
            isPlayerInCrouchingArea = true;
        }

        else if(collider.gameObject.tag == "ExitingCrouchArea" && isPlayerInCrouchingArea == true)
        {
            isPlayerInCrouchingArea = false;
        }
    }

    public IEnumerator WaitForRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        Respawn();
    }

}