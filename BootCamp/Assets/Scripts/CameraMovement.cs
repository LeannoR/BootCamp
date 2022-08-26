using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    public Transform player;
    public void Update()
    {
        CameraPosition();
    }

    public void CameraPosition()
    {
        var cameraPos = player.position + offset;
        //cameraPos.x = Mathf.Clamp(0,0,0);
        transform.position = cameraPos;
    }
}
