using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    public void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        rigidBody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Platform" || collider.gameObject.tag == "Player")
        {
            particleSystem.Play();
            Destroy(rigidBody);
            Destroy(meshRenderer);
            Destroy(meshCollider);
            StartCoroutine(WaitForDestroy(2));
        }
    }
    public IEnumerator WaitForDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
