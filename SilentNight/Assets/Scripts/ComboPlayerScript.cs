using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboPlayerScript : Echolocator
{
    Rigidbody2D rbody;

    public int walkSpeed = 2;
    public int runSpeed = 4;
    public int sneakSpeed = 1;
    Animator movement;

    int curSpeed;
   // public bool running = false;
  //  public bool walking = true;
  //  public bool sneaking = false;

    GameObject flashlight;
    bool flashlightOn;
    bool flashlightDead = false;
    float batteryLevel = 40;
    AudioSource sound;
    public AudioClip flashlightOnClip;
    public AudioClip flashlightOffClip;

    SpriteRenderer srender;
    string lastSprite = "PlayerSpriteSheet_3";
    public AudioClip footstep;

    public bool blinded = false;
    GameObject soundwaves;

    ParticleSystem clap;
    public AudioClip clapSound;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        srender = GetComponent<SpriteRenderer>();

        curSpeed = walkSpeed; //start out walking
        running = sneaking = false;
        walking = true;

        flashlightOn = false;
        flashlight = transform.GetChild(0).gameObject;
        flashlight.SetActive(flashlightOn);

        soundwaves = transform.GetChild(2).gameObject;
        clap = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blinded)
        {
            soundwaves.SetActive(true);
            flashlight.SetActive(false);
            srender.enabled = false;
        }
        else
        {
            soundwaves.SetActive(false);
            flashlight.SetActive(true);
            srender.enabled = true;

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
        }

        if (flashlightOn && !flashlightDead)
        {
            batteryLevel -= Time.deltaTime;
            if (batteryLevel <= 0)
            {
                flashlightDead = true;
                flashlight.SetActive(false);
            }
        }

        //toggle flashlight
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (flashlightOn)
            {
                flashlightOn = false;
                sound.PlayOneShot(flashlightOffClip);
            }
            else
            {
                flashlightOn = true;
                sound.PlayOneShot(flashlightOnClip);
            }
            if (!flashlightDead)
            {
                flashlight.SetActive(flashlightOn);
            }
        }

        //toggle running
        if (Input.GetKeyDown(KeyCode.R))
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

        


        //sync player footsteps to player animation
        if (srender.sprite.name == "PlayerSpriteSheet_1" && lastSprite == "PlayerSpriteSheet_3")
        {
            lastSprite = "PlayerSpriteSheet_1";
            sound.PlayOneShot(footstep);
        }
        if (srender.sprite.name == "PlayerSpriteSheet_3" && lastSprite == "PlayerSpriteSheet_1")
        {
            lastSprite = "PlayerSpriteSheet_3";
            sound.PlayOneShot(footstep);
        }

        //make the player face the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = mousePos - transform.position;
        transform.up = diff;

        //clap when left mouse button is clicked and player is not already clapping
        if (!clap.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                sound.PlayOneShot(clapSound);
                clap.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootGun();
        }
    }

    private void FixedUpdate()
    {
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

    private void LateUpdate()
    {
        //camera follows the player
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void ShootGun()
    {

    }
}
