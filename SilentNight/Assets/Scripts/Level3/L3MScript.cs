using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L3MScript : MonoBehaviour
{
    public ComboPlayerScript player;
    public Slider lives;
    public GameObject lhhud, rhhud, pauseMenu;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("damage", 3);
        PlayerPrefs.SetInt("hits", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(paused)
        {
            Time.timeScale = 0;
            lhhud.SetActive(false);
            rhhud.SetActive(false);
            pauseMenu.SetActive(true);

        }
        else
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

        if (PlayerPrefs.GetInt("damage") <= 0)
        {
            SceneManager.LoadScene("Level3");
        }

        if(PlayerPrefs.GetInt("hits") >= 3)
        {
            SceneManager.LoadScene("EndScene");
        }
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
