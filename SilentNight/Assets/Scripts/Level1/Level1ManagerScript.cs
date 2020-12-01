using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1ManagerScript : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public PlayerScript player;
    bool deadDeer, bridge, blockade;
    public Text beginningText, InstructionsText, Instructions2Text, Instructions3Text;
    public Text deadDeerText, bridgeText, bridgeReminderText;
    public Slider lives;
    public GameObject textbox, lhhud, rhhud, pauseMenu;
    bool bridgeDeath, paused = false;
    public GameObject caveBlockade;

    // Start is called before the first frame update
    void Start()
    {
        deadDeer = bridge = blockade = false; //Keep track of what area has been visited
        bridgeDeath = false; //Tracks if player has died from falling of the bridge
        gameOverCanvas.SetActive(false); //Hide the death screen

        
        //only show the beginning dialog if first time
        if (PlayerPrefs.GetInt("Lives") == 5)
        {
            Time.timeScale = 0;
            beginningText.gameObject.SetActive(true); //Show the opening dialog and instructions
            textbox.SetActive(true); //Background red box for dialog
        }

        //don't block path to caves if player visited bridge on previous life
        if (PlayerPrefs.GetInt("firstTimeBlockade") == 0)
        {
            blockade = bridge = true;
            caveBlockade.SetActive(false);
        }
    }

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
        else if (!paused && !textbox.active)
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
        else if(Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
            player.unpause();
        }

        lives.value = PlayerPrefs.GetInt("Lives");

        if (PlayerPrefs.GetInt("Lives") == 0)
        {
            gameOver();
        }

        //Show game over screen if player dies by the bridge
        if (!player && !bridgeDeath)
        {
            gameOverCanvas.SetActive(true);
        }
        else
        {
            //While the player is still alive..
            if (player)
            {
                float y = player.gameObject.transform.position.y;
                float x = player.gameObject.transform.position.x;

                //Show the bridge dialog if the player has not been there yet
                if ((-34 < y && y < -31 && 32 < x) && !bridge)
                {
                    textbox.SetActive(true);
                    Time.timeScale = 0;
                    bridgeText.gameObject.SetActive(true);
                    bridge = true;
                    PlayerPrefs.SetInt("firstTimeBlockade", 0);
                }

                //Show the deer dialog if the player has not been there yet
                if ((-34 < y && y < -31 && 2.5 > x) && !deadDeer)
                {
                    textbox.SetActive(true);
                    Time.timeScale = 0;
                    deadDeerText.gameObject.SetActive(true);
                    deadDeer = true;
                }

                //Remind the player to visit the bridge first
                if ((y < -33.5 && x > 14 && x < 18) && !blockade)
                {
                    textbox.SetActive(true);
                    Time.timeScale = 0;
                    bridgeReminderText.gameObject.SetActive(true);
                    blockade = true;
                }
            }
        }

        //Cycles through the different dialogs
        if (Input.GetKeyDown(KeyCode.Return)) //hit enter
        {
            //Opening dialog + instructions
            if (beginningText.gameObject.activeInHierarchy)
            {
                beginningText.gameObject.SetActive(false);
                InstructionsText.gameObject.SetActive(true);
            }
            else if (InstructionsText.gameObject.activeInHierarchy)
            {
                InstructionsText.gameObject.SetActive(false);
                Instructions2Text.gameObject.SetActive(true);
            }

            //Tells the player to go to the bridge first before the cave
            else if (bridgeText.gameObject.activeInHierarchy)
            {
                blockade = true;
                bridgeText.gameObject.SetActive(false);
                caveBlockade.SetActive(false);
                textbox.SetActive(false);
                Time.timeScale = 1;
            }

            //Remove bridge dialog from the screen
            else
            {
                Instructions2Text.gameObject.SetActive(false);
                bridgeText.gameObject.SetActive(false);
                deadDeerText.gameObject.SetActive(false);
                bridgeReminderText.gameObject.SetActive(false);
                textbox.SetActive(false);
                Time.timeScale = 1;
            }
            
        }
    }

    private void gameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void nextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void deathByBridge()
    {
        bridgeDeath = true;
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
