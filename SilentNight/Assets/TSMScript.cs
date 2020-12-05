using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TSMScript : MonoBehaviour
{
    public GameObject lhhud, rhhud, pauseMenu, Exit;
    public GameObject Textbox, openingDialog, foundBulletsDialog, foundBatteriesDialog, foundGunDialog, foundFlashlightDialog;
    bool foundBullets, foundBatteries, foundGun, foundFlashlight;
    public SoundPlayerScript player;
    bool paused, once = false;
    public Slider lives;

    // Start is called before the first frame update
    void Start()
    {
        lives.value = PlayerPrefs.GetInt("Lives");

        foundBatteries = foundBullets = foundFlashlight = foundGun = false;
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
                Exit.SetActive(true);
            }

        }
    }
}
