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
    public Slider lives, bossHealthS;
    public GameObject lhhud, rhhud, pauseMenu, achievementBox, achievement2txt, bulletPrefab, batteryPrefab;
    public LevelLoaderScript levelLoader;
    public PlayableDirector timeline;   //Intro scene controller
    public AudioClip achievement;
    public FinalBossScript boss;

    public int bossHealth = 10;

    bool setup, playOnce, nearDeath = false;

    ObjectPool poolBullet;
    GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {

        //Make our collectable object pool
        poolBullet = new ObjectPool(bulletPrefab, 5, false);

        //Manually spawn the first collectable bullets
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(14, -6, 0);
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(-4, -6, 0);
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(1, -11, 0);
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(-14, -11, 0);
        bullet = poolBullet.getObject();
        bullet.transform.position = new Vector3(14, -11, 0);

        //Set the players life amount and how many times they have been hit
        PlayerPrefs.SetInt("damage", 3);
        PlayerPrefs.SetInt("hits", 0);

        //Disable the hub while the intro plays
        lhhud.SetActive(false);
        rhhud.SetActive(false);
        player.enabled = false;
        playerAnimator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the bosses health to the previous value from the last fight
        bossHealthS.value = bossHealth - PlayerPrefs.GetInt("hits"); 

        //Enter only if the intro is done playing
        if (timeline.state != PlayState.Playing)
        {
            //Show the hub and re-enable the player
            if (!setup)
            {
                setup = true;
                player.enabled = true;
                playerAnimator.enabled = true;
                lhhud.SetActive(true);
                rhhud.SetActive(true);
            }

            //Access the pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                lhhud.SetActive(false);
                rhhud.SetActive(false);
                pauseMenu.SetActive(true);
                player.enabled = false;
                Time.timeScale = 0;
            }
        }

        //Get the players life count
        lives.value = PlayerPrefs.GetInt("Lives");

        //Reload the scene if the player dies
        if (PlayerPrefs.GetInt("damage") <= 0)
        {
            levelLoader.LoadNextLevel("Level3");
        }

        //Activate the bosses "Last Resort" when reaching below 5 health
        if (PlayerPrefs.GetInt("hits") >= 5 && !nearDeath)
        {
            nearDeath = true;
            boss.LastResort();
        }

        //Enter when the player kills the boss
        if(PlayerPrefs.GetInt("hits") >= bossHealth && !playOnce)
        {
            playOnce = true;
            boss.CancelInvoke();    //Stop shooting cuz its dead
            boss.audio.PlayOneShot(boss.death);
            levelLoader.LoadNextLevel("EndScene");
        }

    }

    private void FixedUpdate()
    {
        //Respawn a bullet collectable if there is one available
        bullet = poolBullet.getObject();
        if (bullet)
        {
            float x = Random.Range(-15f, 15f);
            float y = Random.Range(-11f, 0f);
            bullet.transform.position = new Vector3(x, y, 0);
        }
    }

    //Unpause the game from the pause menu
    public void Play()
    {
        lhhud.SetActive(true);
        rhhud.SetActive(true);
        pauseMenu.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1;
    }
}
