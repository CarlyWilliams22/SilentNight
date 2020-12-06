using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOMScript : MonoBehaviour
{

    /*Starts the game from the main menu*/
    public void playAgain()
    {
        SceneManager.LoadScene(0);
    }

    /*Quits the Application*/
    public void quit()
    {
        Application.Quit();
    }
}
