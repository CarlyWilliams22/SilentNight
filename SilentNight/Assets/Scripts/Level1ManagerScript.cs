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
    bool deadDeer, bridge;
    public Text beginningText, deadDeerText, bridgeText;
    public GameObject textbox;
    bool bridgeDeath;

    // Start is called before the first frame update
    void Start()
    {
        deadDeer = bridge = false;
        bridgeDeath = false;
        canvas.SetActive(false);
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player && !bridgeDeath)
        {
            canvas.SetActive(true);
        }
        else
        {
            float y = player.gameObject.transform.position.y;
            float x = player.gameObject.transform.position.x;
            if ((-34 < y && y < -31 && 32 < x) && !bridge)
            {
                textbox.SetActive(true);
                bridgeText.gameObject.SetActive(true);
                bridge = true;
            }
            if ((-34 < y && y < -31 && 2.5 > x) && !deadDeer)
            {
                textbox.SetActive(true);
                deadDeerText.gameObject.SetActive(true);
                deadDeer = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            beginningText.gameObject.SetActive(false);
            bridgeText.gameObject.SetActive(false);
            deadDeerText.gameObject.SetActive(false);
            textbox.SetActive(false);
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

    public void nextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void deathByBridge()
    {
        bridgeDeath = true;
    }
}
