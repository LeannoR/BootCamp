using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera camera;
    [SerializeField] bool isLevelFinished = false;
    [SerializeField] Vector3 offsetForPlay;
    [SerializeField] Vector3 offsetForFinishingLevel;
    public Transform player;
    public PlayerMovement playerMovement;

    public void Start()
    {
        camera = Camera.main;
    }
    public void Update()
    {
        IsLevelFinished();
        CameraPositionForPlay();
        CameraPositionForFinishingLevel();
    }

    public void CameraPositionForPlay()
    {
        if(isLevelFinished == false)
        {
            camera.transform.position = player.position + offsetForPlay;
        }
    }

    public void CameraPositionForFinishingLevel()
    {
        if(isLevelFinished == true)
        {
            camera.transform.position = player.position + offsetForFinishingLevel;
            camera.transform.LookAt(player);
        }
    }

    public void IsLevelFinished()
    {
        isLevelFinished = playerMovement.isLevelFinished;
    }
}
