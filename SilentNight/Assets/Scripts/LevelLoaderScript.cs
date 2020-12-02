using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel(string nextLevel)
    {
        StartCoroutine(LoadLevel(nextLevel));
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void LoadLevel1()
    {
        StartCoroutine(LoadLevel("Level1"));
    }

    public void LoadLevel2()
    {
        StartCoroutine(LoadLevel("Level2"));
    }

    public void LoadLevel3()
    {
        StartCoroutine(LoadLevel("Level3"));
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
