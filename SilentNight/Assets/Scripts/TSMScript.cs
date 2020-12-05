using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TSMScript : MonoBehaviour
{
    public GameObject lhhud, rhhud, pauseMenu, Exit, bulletH, batteryH;
    public Text batteryCountH, bulletCountH;
    int numBatteries, numBullets;
    public GameObject Textbox, openingDialog, foundBulletsDialog, foundBatteriesDialog, foundGunDialog, foundFlashlightDialog;
    bool foundBullets, foundBatteries, foundGun, foundFlashlight;
    public GameObject bulletC, batteryC, gunC, flashlightC;
    public SoundPlayerScript player;
    bool paused, once = false;
    public Slider lives;
    public LevelLoaderScript levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        lives.value = PlayerPrefs.GetInt("Lives");

        foundBatteries = foundBullets = foundFlashlight = foundGun = false;
        numBatteries = 0;
        numBullets = 0;
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
        else
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

        
        if (Input.GetKeyDown(KeyCode.Return)) //hit enter
        {
            if (openingDialog.activeInHierarchy)
            {
                openingDialog.SetActive(false);
                Textbox.SetActive(false);
                player.gameObject.SetActive(true);
            }

            if (foundBulletsDialog.activeInHierarchy)
            {
                foundBulletsDialog.SetActive(false);
                Textbox.SetActive(false);
            }
            if (foundBatteriesDialog.activeInHierarchy)
            {
                foundBatteriesDialog.SetActive(false);
                Textbox.SetActive(false);
            }
            if (foundGunDialog.activeInHierarchy)
            {
                foundGunDialog.SetActive(false);
                Textbox.SetActive(false);
            }
            if (foundFlashlightDialog.activeInHierarchy)
            {
                foundFlashlightDialog.SetActive(false);
                Textbox.SetActive(false);
            }


            if(foundBullets && foundBatteries && foundGun && foundFlashlight)
            {
                Exit.GetComponent<SpriteRenderer>().color = Color.white;
                Exit.tag = "Exit";
            }

            bulletCountH.text = "x" + numBullets;
            batteryCountH.text = "x" + numBatteries;

        }
    }

    public void BulletsFound()
    {
        foundBullets = true;
        foundBulletsDialog.SetActive(true);
        bulletH.GetComponent<SpriteRenderer>().color = Color.white;
        bulletCountH.color = Color.white;
        numBullets++;
    }

    public void BatteryFound()
    {
        foundBatteries = true;
        foundBatteriesDialog.SetActive(true);
        batteryH.GetComponent<SpriteRenderer>().color = Color.white;
        batteryCountH.color = Color.white;
        numBatteries++;
    }

    public void GunFound()
    {
        foundGun = true;
        foundGunDialog.SetActive(true);
    }


    public void FlashlightFound()
    {
        foundFlashlight = true;
        foundFlashlightDialog.SetActive(true);
    }


    public void NextLevel()
    {
        levelLoader.LoadNextLevel("Level3");
    }
}
