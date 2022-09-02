using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorDemo : MonoBehaviour {

    public Animator TrapDoorAnim;

    void Awake()
    {
        TrapDoorAnim = GetComponent<Animator>();
        StartCoroutine(OpenCloseTrap());
    }


    IEnumerator OpenCloseTrap()
    {
        TrapDoorAnim.SetTrigger("open");
        yield return new WaitForSeconds(2);
        TrapDoorAnim.SetTrigger("close");
        yield return new WaitForSeconds(2);
        StartCoroutine(OpenCloseTrap());

    }
}