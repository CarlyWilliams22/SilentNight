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
    public GameObject textbox;
    bool bridgeDeath;
    public GameObject caveBlockade;

    // Start is called before the first frame update
    void Start()
    {
        deadDeer = bridge = blockade = false;
        bridgeDeath = false;
        gameOverCanvas.SetActive(false);

        Time.timeScale = 0;
        beginningText.gameObject.SetActive(true);
        textbox.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player && !bridgeDeath)
        {
            gameOverCanvas.SetActive(true);
        }
        else
        {
            if (player)
            {
                float y = player.gameObject.transform.position.y;
                float x = player.gameObject.transform.position.x;
                if ((-34 < y && y < -31 && 32 < x) && !bridge)
                {
                    textbox.SetActive(true);
                    Time.timeScale = 0;
                    bridgeText.gameObject.SetActive(true);
                    bridge = true;
                }
                if ((-34 < y && y < -31 && 2.5 > x) && !deadDeer)
                {
                    textbox.SetActive(true);
                    Time.timeScale = 0;
                    deadDeerText.gameObject.SetActive(true);
                    deadDeer = true;
                }
                if((y < -33.5 && x > 14 && x < 18) && !blockade)
                {
                    textbox.SetActive(true);
                    Time.timeScale = 0;
                    bridgeReminderText.gameObject.SetActive(true);
                    blockade = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
            else if (Instructions2Text.gameObject.activeInHierarchy)
            {
                Instructions2Text.gameObject.SetActive(false);
                Instructions3Text.gameObject.SetActive(true);
            }
            else if (bridgeText.gameObject.activeInHierarchy)
            {
                blockade = true;
                bridgeText.gameObject.SetActive(false);
                caveBlockade.SetActive(false);
                textbox.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                Instructions3Text.gameObject.SetActive(false);
                bridgeText.gameObject.SetActive(false);
                deadDeerText.gameObject.SetActive(false);
                bridgeReminderText.gameObject.SetActive(false);
                textbox.SetActive(false);
                Time.timeScale = 1;
            }
            
        }
        
        
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

    public void Blockade()
    {

    }
}
