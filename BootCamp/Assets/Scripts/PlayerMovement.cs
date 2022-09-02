using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 previousMousePos;
    private Vector3 startingPosition;

    private Rigidbody rigidBody;
    private Animator animator;
    private MeshCollider meshCollider;
    private BoxCollider boxCollider;

    private bool flyAnimTriggered = false;

    public bool isFlying = false;
    public bool isPlayerInCrouchingArea = false;
    public bool isPlayerDead = false;
    public bool isLevelFinished = false;
    public bool isRunning = false;
    public float Multiplier = 1f;
    public float sideWaySpeed = 1f;
    public float forwardSpeed = 10f;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        meshCollider = GetComponent<MeshCollider>();
        boxCollider = GetComponent<BoxCollider>();

        startingPosition = transform.position;
    }

    public void Update()
    {
        ForwardMovement();
        SideWayMovement();
        Crouch();
        LockHorizontalPosition();
        LockPlayerRotation();
        DeadFromHeight();
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
            isRunning = true;
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
            meshCollider.enabled = !meshCollider.enabled;
            boxCollider.enabled = !boxCollider.enabled;
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && isLevelFinished == false && isPlayerInCrouchingArea == true && isPlayerDead == false)
        {
            animator.SetBool("isRunning", true);
            meshCollider.enabled = !meshCollider.enabled;
            boxCollider.enabled = !boxCollider.enabled;
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
        if(isPlayerDead == false)
        {
            rotation.x = Mathf.Clamp(transform.rotation.z, 0, 0);
            rotation.z = Mathf.Clamp(transform.rotation.z, 0, 0);
        }
        transform.rotation = rotation;
    }
    public void DeadFromHeight()
    {
        if (transform.position.y <= -10)
        {
            StartCoroutine(WaitForRespawn(0));
        }
        else if (transform.position.y <= -1 && flyAnimTriggered == false)
        {
            flyAnimTriggered = true;
            isPlayerDead = true;
            isFlying = true;
            isRunning = false;
            animator.SetTrigger("isFlying");
        }
    }
    public void Respawn()
    {
        isPlayerInCrouchingArea = false;
        transform.position = startingPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        isPlayerDead = false;
        isRunning = false;
        isFlying = false;
        animator.SetBool("isRunning", false);
        flyAnimTriggered = false;
        forwardSpeed = 10f;
    }
    public IEnumerator WaitForRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        Respawn();
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
            isPlayerDead = true;
            animator.SetBool("isRunning", false);
            StartCoroutine(WaitForRespawn(3));
        }
        else if (collider.gameObject.tag == "Rock" && isPlayerDead == false)
        {
            isRunning = false;
            isPlayerDead = true;
            animator.SetBool("isRunning", false);
            StartCoroutine(WaitForRespawn(3));
        }
        else if (collider.gameObject.tag == "Blade" && isPlayerDead == false)
        {
            isRunning = false;
            isPlayerDead = true;
            animator.SetTrigger("SpikeDeath");
            StartCoroutine(WaitForRespawn(3));
        }
        else if (collider.gameObject.tag == "Spear" && isPlayerDead == false)
        {
            isRunning = false;
            isPlayerDead = true;
            animator.SetTrigger("SpikeDeath");
            StartCoroutine(WaitForRespawn(3));
        }
        else if (collider.gameObject.tag == "TrapDoor")
        {
            forwardSpeed = 0;
            Debug.Log("TrapDoor");
        }
        else if (collider.gameObject.tag == "Door")
        {
            Debug.Log("Door");
            forwardSpeed = 10f;
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
}