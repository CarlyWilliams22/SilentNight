using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TSMScript : MonoBehaviour
{
    public GameObject lhhud, rhhud, pauseMenu, Exit, bulletH, achievementBox, achievement2txt;
    public Text bulletCountH;
    public GameObject Textbox, openingDialog, foundBulletsDialog, foundGunDialog;
    public GameObject bulletC, gunC;
    public SoundPlayerScript player;
    public Slider lives;
    public LevelLoaderScript levelLoader;
    public AudioClip achievement;

    int numBullets;
    bool foundBullets, foundGun;


    // Start is called before the first frame update
    void Start()
    {
        //update lives in hud
        lives.value = PlayerPrefs.GetInt("Lives");
       
        foundBullets = foundGun = false;
        numBullets = 0;

        //unlock finished level 2 achievement
        if (PlayerPrefs.GetInt("Trophy2") == 0)
        {
            PlayerPrefs.SetInt("Trophy2", 1);
            achievementBox.SetActive(true);
            achievement2txt.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(achievement);
        }

        //allow the scene to transition before freezing the player
        Invoke("waitForFade", 1);
    }

    // Update is called once per frame
    void Update()
    {
        //pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.enabled = false;
            Time.timeScale = 0;
            lhhud.SetActive(false);
            rhhud.SetActive(false);
            pauseMenu.SetActive(true);
        }

        //update number of bullets in hud
        bulletCountH.text = "x" + numBullets;

        //exit from dialog boxes
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

            //if both bullets and gun found, allow the player to move on to level 3
            if (foundBullets && foundGun)
            {
                Exit.GetComponent<SpriteRenderer>().color = Color.white;
                Exit.tag = "Exit";
            }

        }
    }

    //Player found the bullet collectable so activate bullets
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

    //player found the gun
    public void GunFound()
    {
        foundGun = true;
        foundGunDialog.SetActive(true);
        Textbox.SetActive(true);
        Time.timeScale = 0;

    }
    
    //allow scene to fade in before pausing for the opening dialog
    void waitForFade()
    {
        Time.timeScale = 0;
    }

    //load the next level
    public void NextLevel()
    {
        levelLoader.LoadNextLevel("Level3");
    }

    //unpause the game
    public void Play()
    {
        player.enabled = true;
        Time.timeScale = 1;
        lhhud.SetActive(true);
        rhhud.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
