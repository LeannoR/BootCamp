using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator animator;
    private bool isLevelFinished = false;
    private bool isPlayerInCrouchingArea = false;
    private bool isPlayerDead = false;
    public PlayerMovement playerMovement;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        CameraSwitch();
    }

    public void CameraSwitch()
    {
        isPlayerDead = playerMovement.isPlayerDead;
        isLevelFinished = playerMovement.isLevelFinished;
        isPlayerInCrouchingArea = playerMovement.isPlayerInCrouchingArea;

        if(!isLevelFinished && !isPlayerInCrouchingArea)
        {
            animator.SetBool("sideLookCamera", false);
        }
        else if(!isLevelFinished && isPlayerInCrouchingArea)
        {
            animator.SetBool("sideLookCamera", true);
        }
        else if(isLevelFinished)
        {
            animator.SetBool("finishLevelCamera", true);
        }
    }
}
