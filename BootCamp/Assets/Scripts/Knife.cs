using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForDestroy(5));
    }

    public IEnumerator WaitForDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
