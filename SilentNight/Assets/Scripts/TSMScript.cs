using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TSMScript : MonoBehaviour
{
    public GameObject lhhud, rhhud, pauseMenu, Exit, bulletH, achievementBox, achievement2txt;
    public Text bulletCountH;
    int numBatteries, numBullets;
    public GameObject Textbox, openingDialog, foundBulletsDialog,foundGunDialog;
    bool foundBullets, foundGun;
    public GameObject bulletC, gunC;
    public SoundPlayerScript player;
    bool paused, once = false;
    public Slider lives;
    public LevelLoaderScript levelLoader;
    public AudioClip achievement;

    // Start is called before the first frame update
    void Start()
    {

        lives.value = PlayerPrefs.GetInt("Lives");

        foundBullets = foundGun = false;
        numBullets = 0;

        if (PlayerPrefs.GetInt("Trophy2") == 0)
        {
            PlayerPrefs.SetInt("Trophy2", 1);
            achievementBox.SetActive(true);
            achievement2txt.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(achievement);
        }

        Invoke("waitForFade", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            if (!once)
            {
                once = true;
                player.enabled = false;
                Time.timeScale = 0;
                lhhud.SetActive(false);
                rhhud.SetActive(false);
                pauseMenu.SetActive(true);
            }
        }
        else if(!Textbox.activeInHierarchy)
        {
            once = false;
            player.enabled = true;
            Time.timeScale = 1;
            lhhud.SetActive(true);
            rhhud.SetActive(true);
            pauseMenu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        bulletCountH.text = "x" + numBullets;

        if (Input.GetKeyDown(KeyCode.Return)) //hit enter
        {
            if (openingDialog.activeInHierarchy)
            {
                openingDialog.SetActive(false);
                Textbox.SetActive(false);
                player.gameObject.SetActive(true);
                Time.timeScale = 1;
            }

            if (foundBulletsDialog.activeInHierarchy)
            {
                foundBulletsDialog.SetActive(false);
                Textbox.SetActive(false);
                Time.timeScale = 1;
            }
            if (foundGunDialog.activeInHierarchy)
            {
                foundGunDialog.SetActive(false);
                Textbox.SetActive(false);
                Time.timeScale = 1;
            }


            if (foundBullets && foundGun)
            {
                Exit.GetComponent<SpriteRenderer>().color = Color.white;
                Exit.tag = "Exit";
            }

        }
    }

    public void BulletsFound()
    {
        foundBullets = true;
        foundBulletsDialog.SetActive(true);
        Textbox.SetActive(true);
        bulletH.GetComponent<SpriteRenderer>().color = Color.white;
        bulletCountH.color = Color.white;
        numBullets+=4;
        Time.timeScale = 0;
    }


    public void GunFound()
    {
        foundGun = true;
        foundGunDialog.SetActive(true);
        Textbox.SetActive(true);
        Time.timeScale = 0;

    }

    void waitForFade()
    {
        Time.timeScale = 0;
    }


    public void NextLevel()
    {
        levelLoader.LoadNextLevel("Level3");
    }

    public void Play()
    {
        paused = false;
    }
}
