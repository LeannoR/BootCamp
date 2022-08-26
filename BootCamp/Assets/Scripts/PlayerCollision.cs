using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Trap")
        {
            Debug.Log("You dead");
        }
    }
}
