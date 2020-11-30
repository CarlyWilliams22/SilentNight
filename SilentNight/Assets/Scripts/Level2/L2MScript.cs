using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L2MScript : MonoBehaviour
{
    public GameObject gameOverCanvas, openingDialog, player, monster1, monster2;
    public Text openingDialogText, instructionText, livesText;

    // Update is called once per frame
    void Update()
    {

        livesText.text = "Lives: " + PlayerPrefs.GetInt("Lives").ToString();
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
                player.SetActive(true);
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
}
