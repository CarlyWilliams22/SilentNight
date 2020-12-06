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
    public GameObject lhhud, rhhud, pauseMenu, achievementBox, achievement2txt, bulletPrefab, batteryPrefab;
    public LevelLoaderScript levelLoader;
    public PlayableDirector timeline;
    public AudioClip achievement;
    public FinalBossScript boss;
    bool paused, once, setup, playOnce = false;

    ObjectPool poolBattery, poolBullet;

    GameObject bullet, battery;

    // Start is called before the first frame update
    void Start()
    {
        poolBattery = new ObjectPool(batteryPrefab, 3, false);
        poolBullet = new ObjectPool(bulletPrefab, 5, false);

        battery = poolBattery.getObject();
        battery.transform.position = new Vector3(-20, -6, 0);
        battery = poolBattery.getObject();
        battery.transform.position = new Vector3(4, -6, 0);
        battery = poolBattery.getObject();
        battery.transform.position = new Vector3(-1, -20, 0);

        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(20, -6, 0);
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(-4, -6, 0);
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(1, -20, 0);
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(-20, -20, 0);
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(20, -20, 0);


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

        if(PlayerPrefs.GetInt("hits") >= 3 && !playOnce)
        {
            playOnce = true;
            boss.CancelInvoke();    //Stop shooting cuz its dead
            boss.audio.PlayOneShot(boss.death);
            levelLoader.LoadNextLevel("EndScene");
        }

    }

    private void FixedUpdate()
    {
        bullet = poolBullet.getObject();
        if (bullet)
        {
            float x = Random.Range(-20f, 20f);
            float y = Random.Range(-20f, -6f);
            bullet.transform.position = new Vector3(x, y, 0);
        }

        battery = poolBattery.getObject();
        if (battery)
        {
            float x = Random.Range(-20f, 20f);
            float y = Random.Range(-20f, -6f);
            battery.transform.position = new Vector3(x, y, 0);
        }
    }

    public void Play()
    {
        paused = false;
    }
}
