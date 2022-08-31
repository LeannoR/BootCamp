using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject finish;
    private Animator animator;

    private bool isLevelFinished = false;
    private int currentSceneIndex = 0;

    public void Start()
    {
        animator = GetComponent<Animator>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        finish.SetActive(false);
    }

    public void Update()
    {
        LoadNextScene();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            isLevelFinished = true;
            finish.SetActive(true);
        }
    }

    public void LoadNextScene()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isLevelFinished == true)
        {
            int nextSceneIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
    }

}
