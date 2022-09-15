using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 startingPosition;

    private Animator animator;
    private ParticleSystem particleSystem;

    private bool flyAnimTriggered = false;

    public bool isPlayerCrouching = false;
    public bool isFlying = false;
    public bool isPlayerInCrouchingArea = false;
    public bool isPlayerDead = false;
    public bool isLevelFinished = false;
    public bool isRunning = false;
    public float Multiplier = 1f;
    public float forwardSpeed = 10f;

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();

        startingPosition = transform.position;
    }

    public void Update()
    {
        ForwardMovement();
        Crouch();
        LockPlayerRotation();
        DeadFromHeight();
    }

    public void ForwardMovement()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if(isLevelFinished == false && isPlayerDead == false)
                {
                    isRunning = true;
                    transform.position = transform.position + Vector3.forward * forwardSpeed * Time.deltaTime;
                    if (isPlayerInCrouchingArea)
                    {
                        animator.SetBool("StandToCrouch", false);
                    }
                    else if (!isPlayerInCrouchingArea)
                    {
                        animator.SetBool("isRunning", true);
                    }
                }
            }
            else if(touch.phase == TouchPhase.Ended && isLevelFinished == false && isPlayerDead == false)
            {
                if (!isPlayerInCrouchingArea)
                {
                    isRunning = false;
                    animator.SetBool("isRunning", false);
                }
                else if (isPlayerInCrouchingArea)
                {
                    isRunning = false;
                    isPlayerCrouching = true;
                    animator.SetBool("StandToCrouch", true);
                }
            }
        }
    }
    public void Crouch()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && isLevelFinished == false && isPlayerInCrouchingArea == true && isPlayerDead == false)
            {
                isPlayerCrouching = true;
                isRunning = false;
                animator.SetBool("StandToCrouch", true);
            }
            else if (touch.phase == TouchPhase.Began && isLevelFinished == false && isPlayerInCrouchingArea == true && isPlayerDead == false)
            {
                isPlayerCrouching = false;
                animator.SetBool("isRunning", true);
            }
        }
        
    }
    public void LockPlayerRotation()
    {
        var rotation = transform.rotation;
        if(isPlayerDead == false)
        {
            rotation.x = Mathf.Clamp(transform.rotation.x, 0, 0);
            rotation.z = Mathf.Clamp(transform.rotation.z, 0, 0);
        }
        transform.rotation = rotation;
    }
    public void DeadFromHeight()
    {
        if (transform.position.y <= startingPosition.y - 8)
        {
            StartCoroutine(WaitForRespawn(0));
        }
        else if (transform.position.y <= startingPosition.y - 1 && flyAnimTriggered == false && isPlayerDead == false)
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
        isPlayerCrouching = false;
        isPlayerInCrouchingArea = false;
        isPlayerDead = false;
        isRunning = false;
        isFlying = false;
        flyAnimTriggered = false;
        transform.position = startingPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        animator.SetBool("isRunning", false);
        forwardSpeed = 7f;
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
            animator.SetTrigger("SpikeDeath");
            particleSystem.Play();
            StartCoroutine(WaitForRespawn(3));
        }
        else if (collider.gameObject.tag == "Rock" && isPlayerDead == false)
        {
            isRunning = false;
            isPlayerDead = true;
            animator.SetTrigger("SpikeDeath");
            particleSystem.Play();
            StartCoroutine(WaitForRespawn(3));
        }
        else if (collider.gameObject.tag == "Blade" && isPlayerDead == false)
        {
            isRunning = false;
            isPlayerDead = true;
            animator.SetTrigger("SpikeDeath");
            particleSystem.Play();
            StartCoroutine(WaitForRespawn(3));
        }
        else if (collider.gameObject.tag == "Spear" && isPlayerDead == false)
        {
            isRunning = false;
            isPlayerDead = true;
            animator.SetTrigger("SpikeDeath");
            particleSystem.Play();
            StartCoroutine(WaitForRespawn(3));
        }
        else if (collider.gameObject.tag == "TrapDoor")
        {
            forwardSpeed = 0;
        }
        else if (collider.gameObject.tag == "Door")
        {
            forwardSpeed = 7f;
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