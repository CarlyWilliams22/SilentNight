using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOMScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
