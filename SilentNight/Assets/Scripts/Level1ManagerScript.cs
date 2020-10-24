using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1ManagerScript : MonoBehaviour
{
    private Scene currentScene;
    public GameObject canvas;
    public PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            canvas.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryLevel()
    {
        if (currentScene.name.Equals("Level1"))
        {
            SceneManager.LoadScene("Level1");
        }
        else if (currentScene.name.Equals("Level2"))
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
