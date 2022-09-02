using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapDemo : MonoBehaviour {

    public Animator spikeTrapAnim;

    void Awake()
    {
        spikeTrapAnim = GetComponent<Animator>();
        StartCoroutine(OpenCloseTrap());
    }


    IEnumerator OpenCloseTrap()
    {
        spikeTrapAnim.SetTrigger("open");
        yield return new WaitForSeconds(2);
        spikeTrapAnim.SetTrigger("close");
        yield return new WaitForSeconds(2);
        StartCoroutine(OpenCloseTrap());

    }
}