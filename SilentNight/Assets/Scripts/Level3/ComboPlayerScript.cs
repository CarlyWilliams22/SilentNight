﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboPlayerScript : Echolocator
{
    Rigidbody2D rbody;
    Animator movement;
    AudioSource sound;
    SpriteRenderer srender;
    GameObject soundwaves;
    ParticleSystem clap;

    public GameObject walkingH, runningH, sneakingH, bulletPrefab, gunBarrel;
    public Slider stamina, playerHealth;
    public Text bullets;
    public AudioClip footstep, gunshot, clapSound;

    public int walkSpeed = 2;
    public int runSpeed = 4;
    public int sneakSpeed = 1;
    public float maxStamina = 5;
    public bool tired = false;

    int curSpeed;
    bool currRunnning, shotFired = false;
    string lastSprite = "PlayerWithGunSpriteSheet_2";

    // Start is called before the first frame update
    void Start()
    {
        bulletNum = 4;

        blinded = false;
        rbody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        srender = GetComponent<SpriteRenderer>();

        curSpeed = walkSpeed; //start out walking
        running = sneaking = false;
        walking = true;

        soundwaves = transform.GetChild(2).gameObject;
        clap = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        stamina.maxValue = maxStamina;
        stamina.value = maxStamina;

        Camera.main.backgroundColor = new Color(.25f, .25f, .25f);
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.value = PlayerPrefs.GetInt("damage");

        bullets.text = "x" + bulletNum.ToString();

        if (!blinded)
        {

            //set player animation speed based on player speed
            if (rbody.velocity == Vector2.zero)
            {
                movement.speed = 0;
            }
            else if (running)
            {
                movement.speed = .4f;
            }
            else if (walking)
            {
                movement.speed = .3f;
            }
            else
            {
                movement.speed = .2f;
            }

            //sync player footsteps to player animation
            if (srender.sprite.name == "PlayerWithGunSpriteSheet_1" && lastSprite == "PlayerWithGunSpriteSheet_2")
            {
                lastSprite = "PlayerWithGunSpriteSheet_1";
                sound.PlayOneShot(footstep);
            }
            if (srender.sprite.name == "PlayerWithGunSpriteSheet_2" && lastSprite == "PlayerWithGunSpriteSheet_1")
            {
                lastSprite = "PlayerWithGunSpriteSheet_2";
                sound.PlayOneShot(footstep);
            }
        }
        else
        {
            //clap when left mouse button is clicked and player is not already clapping
            if (!clap.isPlaying)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sound.PlayOneShot(clapSound);
                    clap.Play();
                }
            }
        }


        runningH.SetActive(running);
        walkingH.SetActive(walking);
        sneakingH.SetActive(sneaking);

        if (stamina.value < 0.00001f)
        {
            tired = true;
        }
        else if (Mathf.Abs(maxStamina - stamina.value) < 0.00001f)
        {
            tired = false;
        }

        //toggle running
        if (Input.GetKeyDown(KeyCode.R) && !tired)
        {
            //walk if already running
            if (curSpeed == runSpeed)
            {
                curSpeed = walkSpeed;
                walking = true;
                running = false;
                sneaking = false;
            }
            else //start running
            {
                curSpeed = runSpeed;
                running = true;
                walking = false;
                sneaking = false;
            }
        }

        if (tired)
        {
            curSpeed = walkSpeed;
            walking = true;
            running = false;
            sneaking = false;
        }

        //toggle sneaking
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //walk if already sneaking
            if (curSpeed == sneakSpeed)
            {
                curSpeed = walkSpeed;
                walking = true;
                sneaking = false;
                running = false;
            }
            else //start sneaking
            {
                curSpeed = sneakSpeed;
                walking = false;
                sneaking = true;
                running = false;
            }
        }

        //make the player face the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 center = transform.position + new Vector3(-.15f, -.0005f, 0);
        Vector2 diff = mousePos - center;
        Quaternion dest = Quaternion.AngleAxis(Mathf.Atan2(diff.y, diff.x) * 180 / Mathf.PI - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, dest, 5 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0) && (bulletNum != 0))
        {
            if (!shotFired)
            {
                shotFired = true;
                ShootGun();
                sound.PlayOneShot(gunshot);
                Invoke("Reload", 1);
            }
        }
    }

    private void FixedUpdate()
    {

        if (running && rbody.velocity != Vector2.zero)
        {
            if (!currRunnning)
            {
                currRunnning = true;
            }
            stamina.value -= .005f;

        }
        else
        {
            currRunnning = false;
            if (stamina.value < maxStamina)
            {
                stamina.value += .005f;
            }

        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 vel = new Vector2(x, y);

        //normalize the velocity unless he is moving slowly
        if (vel.magnitude < 1)
        {
            rbody.velocity = curSpeed * vel;
        }
        else
        {
            rbody.velocity = curSpeed * vel.normalized;
        }
    }

    private void ShootGun()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = gunBarrel.transform.position;
        Rigidbody2D bulletRbody = bullet.GetComponent<Rigidbody2D>();
        float angle = transform.rotation.eulerAngles.z + 90;
        bulletRbody.rotation = angle;
        angle *= Mathf.Deg2Rad;
        bulletRbody.velocity = new Vector2(10 * Mathf.Cos(angle), 10 * Mathf.Sin(angle));
        bulletNum--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            PlayerPrefs.SetInt("damage", PlayerPrefs.GetInt("damage") - 1);

            blinded = true;
            StartCoroutine(WhileBlinded());
        }
    }

    IEnumerator WhileBlinded()
    {
        soundwaves.SetActive(true);
        srender.enabled = false;
        Camera.main.backgroundColor = Color.black;

        yield return new WaitForSeconds(10);
        
        soundwaves.SetActive(false);
        srender.enabled = true;
        Camera.main.backgroundColor = new Color(.25f,.25f,.25f);
        blinded = false;
    }

    public void Reload()
    {
        shotFired = false;
    }
}
