using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class L3MScript : MonoBehaviour
{
    public ComboPlayerScript player;
    public Animator playerAnimator;
    public Slider lives;
    public GameObject lhhud, rhhud, pauseMenu;
    public LevelLoaderScript levelLoader;
    public PlayableDirector timeline;
    bool paused, once, setup = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("damage", 3);
        PlayerPrefs.SetInt("hits", 0);
        lhhud.SetActive(false);
        rhhud.SetActive(false);
        player.enabled = false;
        playerAnimator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeline.state != PlayState.Playing)
        {
            if (!setup)
            {
                setup = true;
                player.enabled = true;
                playerAnimator.enabled = true;
                lhhud.SetActive(true);
                rhhud.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
            }

            if (paused)
            {
                if (!once)
                {
                    once = true;
                    lhhud.SetActive(false);
                    rhhud.SetActive(false);
                    pauseMenu.SetActive(true);
                    player.enabled = false;
                    Time.timeScale = 0;
                }
            }
            else
            {
                once = false;
                lhhud.SetActive(true);
                rhhud.SetActive(true);
                pauseMenu.SetActive(false);
                player.enabled = true;
                Time.timeScale = 1;
            }
        }

        lives.value = PlayerPrefs.GetInt("Lives");

        if (PlayerPrefs.GetInt("damage") <= 0)
        {
            levelLoader.LoadNextLevel("Level3");
        }

        if(PlayerPrefs.GetInt("hits") >= 3)
        {
            levelLoader.LoadNextLevel("EndScene");
        }
    }

    public void Play()
    {
        paused = false;
    }
}
