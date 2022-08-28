using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isPlayerDead = false;

    public Transform player;
    public Rigidbody rigidBody;
    public Vector3 startingPosition = new Vector3(0, 1, -45);
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Trap" && isPlayerDead == false)
        {
            Invoke("Respawn", 2);
            isPlayerDead = true;
            Debug.Log("You dead");
        }
    }
    
    public void Respawn()
    {
        player.position = startingPosition;
        player.rotation = Quaternion.Euler(Vector3.zero);
        isPlayerDead = false;
    }
}
