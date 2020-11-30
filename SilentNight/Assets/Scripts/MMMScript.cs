using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMMScript : MonoBehaviour
{
    public GameObject titleCanvas, instructionsCanvas, achievementsCanvas;

    // Start is called before the first frame update
    //game starts with menu up and instructions down
    void Start()
    {
        titleCanvas.SetActive(true);
        instructionsCanvas.SetActive(false);
        achievementsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //sets inital amount of lives
        PlayerPrefs.SetInt("Lives", 5);
        PlayerPrefs.SetInt("firstTimeBlockade", 1);
    }
    
    /*starts the game from level 1*/
    public void startButton()
    {
        SceneManager.LoadScene("Level1");
    }

    /*takes down the menu and puts the game's instructions on the screen*/
    public void instructionsButton()
    {
        titleCanvas.SetActive(false);
        instructionsCanvas.SetActive(true);
    }

    /*takes down the menu and puts the game's acheivements on the screen*/
    public void achievementsButton()
    {
        titleCanvas.SetActive(false);
        achievementsCanvas.SetActive(true);
    }


    /*Quits the application*/
    public void quitButtion()
    {
        Application.Quit();
    }

    /*takes down other canvases and puts the menu on the screen*/
    public void menuButton()
    {
        instructionsCanvas.SetActive(false);
        achievementsCanvas.SetActive(false);
        titleCanvas.SetActive(true);
    }
}
