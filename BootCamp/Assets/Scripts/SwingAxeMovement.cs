using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class SwingAxeMovement : MonoBehaviour
{
    public float wantedTime;
    public Ease easeType;

    private Vector3 wantedValue;
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> tweener;


    public void Start()
    {
        wantedValue = new Vector3(0, -180, -25);
        tweener = transform.DOLocalRotate(wantedValue, wantedTime, RotateMode.Fast).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }

    public void Update()
    {
        LockYRotation();
    }

    public void LockYRotation()
    {
        var rotation = transform.rotation;
        rotation.y = Mathf.Clamp(transform.rotation.y, -180, 0);
        transform.rotation = rotation;
    }
}
