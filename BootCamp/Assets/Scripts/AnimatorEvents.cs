using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    private MeshCollider meshColliderParent;
    private BoxCollider boxColliderParent;

    private bool IsCrouched = false;

    public void Start()
    {
        meshColliderParent = GetComponentInParent<MeshCollider>();
        boxColliderParent = GetComponentInParent<BoxCollider>();
    }
    public void AnimationCrouched()
    {
        IsCrouched = true;
        meshColliderParent.enabled = !meshColliderParent.enabled;
        boxColliderParent.enabled = !boxColliderParent.enabled;
    }

    public void AnimationIdleOrRunning()
    {
        if(IsCrouched == true)
        {
            IsCrouched = false;
            meshColliderParent.enabled = !meshColliderParent.enabled;
            boxColliderParent.enabled = !boxColliderParent.enabled;
        }
    }
}
