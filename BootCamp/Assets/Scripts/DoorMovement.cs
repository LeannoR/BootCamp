using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorMovement : MonoBehaviour
{
    public Ease easeType;
    public float wantedZPos;
    public float wantedTime;
    DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> tweener;

    private void Start()
    {
        tweener = transform.DOLocalMoveZ(wantedZPos,wantedTime,false).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }
}
