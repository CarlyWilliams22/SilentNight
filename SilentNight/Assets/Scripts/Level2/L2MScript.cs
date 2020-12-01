﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L2MScript : MonoBehaviour
{
    public GameObject gameOverCanvas, openingDialog, monster1, monster2, lhhud, rhhud, pauseMenu;
    public Text openingDialogText, instructionText;
    public SoundPlayerScript player;
    bool paused = false;
    public Slider lives;

    // Update is called once per frame
    void Update()
    {

        if (paused)
        {
            Time.timeScale = 0;
            lhhud.SetActive(false);
            rhhud.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else if(!paused && !openingDialog.active)
        {
            Time.timeScale = 1;
            lhhud.SetActive(true);
            rhhud.SetActive(true);
            pauseMenu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            player.pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
            player.unpause();
        }

        lives.value = PlayerPrefs.GetInt("Lives");

        if (PlayerPrefs.GetInt("Lives") == 0)
        {
            gameOver();
        }

        //Opening dialog for Level 2 with instructions and story
        if (Input.GetKeyDown(KeyCode.Return)) //hit enter
        {

            //Cycle through the different dialogs
            if (openingDialogText.gameObject.activeInHierarchy)
            {
                openingDialogText.gameObject.SetActive(false);
                instructionText.gameObject.SetActive(true);
            }
            else if (instructionText.gameObject.activeInHierarchy)
            {
                openingDialog.SetActive(false);
                instructionText.gameObject.SetActive(false);

                //Activate the moving objects after the player finishes the dialog
                //"Pauses" the game giving the player time to read
                player.gameObject.SetActive(true);
                monster1.SetActive(true);
                monster2.SetActive(true);
            }
            
        }
    }

    //Show the death screen when the player dies
    public void Death()
    {
        gameOverCanvas.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Exit() 
    {
        Application.Quit();
    }

    private void gameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Play()
    {
        paused = false;
        player.unpause();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
