using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMMScript : MonoBehaviour
{
    public GameObject titleCanvas, instructionCanvas;

    // Start is called before the first frame update
    //game starts with menu up and instructions down
    void Start()
    {
        titleCanvas.SetActive(true);
        instructionCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //sets inital amount of lives
        PlayerPrefs.SetInt("Lives", 5);    
    }
    
    /*starts the game from level 1*/
    public void startButton()
    {
        SceneManager.LoadScene("Level1");
    }

    /*takes down the menu and puts the game's instructions on the screen*/
    public void instructionButton()
    {
        titleCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }


    /*Quits the application*/
    public void quitButtion()
    {
        Application.Quit();
    }

    /*takes down the game's instructions and puts the menu on the screen*/
    public void menuButton()
    {
        titleCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
    }
}
