 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rbody;

    public int walkSpeed;
    public int runSpeed;
    public int sneakSpeed;
    Animator movement;
    
    int curSpeed;
    public bool running = false;
    public bool walking = true;
    public bool sneaking = false;

    public Level1ManagerScript l1ms;
    public GameObject bridgeCanvas;

    GameObject flashlight;
    public Slider battery;
    bool flashlightOn;
    bool flashlightDead = false;
    float batteryLevel = 30;
    int batteryNum = 2;
    float batteryStart;
    public Text batteries;

    AudioSource sound;
    public AudioClip flashlightOnClip;
    public AudioClip flashlightOffClip;

    public GameObject walkingH, runningH, sneakingH;

    SpriteRenderer srender;
    string lastSprite = "PlayerSpriteSheet_3";
    public AudioClip footstep;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        srender = GetComponent<SpriteRenderer>();

        curSpeed = walkSpeed; //start out walking

        flashlightOn = false;
        flashlight = transform.GetChild(0).gameObject;
        flashlight.SetActive(flashlightOn);
 
    }

    // Update is called once per frame
    void Update()
    {
        runningH.SetActive(running);
        walkingH.SetActive(walking);
        sneakingH.SetActive(sneaking);

        batteries.text = "x" + batteryNum.ToString();

        if (flashlightOn)
        {
            battery.value = batteryLevel - (int)(Time.time - batteryStart);
        }

        if(battery.value == 0)
        {
            if(batteryNum == 0)
            {
                flashlightDead = true;
                flashlight.SetActive(false);
            }
            else
            {
                batteryNum--;
                NewBattery();
            }

        }

        //toggle flashlight
        if (!flashlightDead)
        {
            if (Input.GetMouseButtonDown(0) /*|| Input.GetKeyDown(KeyCode.Space)*/)
            {
                if (flashlightOn)
                {
                    flashlightOn = false;
                    sound.PlayOneShot(flashlightOffClip);
                    batteryLevel = battery.value;
                }
                else
                {
                    batteryStart = Time.time;
                    flashlightOn = true;
                    sound.PlayOneShot(flashlightOnClip);
                }
                flashlight.SetActive(flashlightOn);
            }
        }

        //toggle running
        if (Input.GetKeyDown(KeyCode.R))
        {
            //walk if already running
            if(curSpeed == runSpeed) 
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
        //transform.up = diff;
        Quaternion dest = Quaternion.AngleAxis(Mathf.Atan2(diff.y, diff.x) * 180 / Mathf.PI - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, dest, 5 * Time.deltaTime);
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ignore collision with monster detection area
        if (collision.gameObject.tag.Equals("DetectionArea"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        
        //player runs off bridge and dies
        if (collision.gameObject.tag.Equals("Respawn"))
        {
            bridgeCanvas.SetActive(true);
            l1ms.deathByBridge();
            PlayerPrefs.SetInt("Lives", (PlayerPrefs.GetInt("Lives") - 1));
            Destroy(gameObject);
        }

        //player reaches the cave and moves on to level 2
        if (collision.gameObject.tag.Equals("Finish"))
        {
            l1ms.nextLevel();
        }
    }

    void NewBattery()
    {
        batteryStart = Time.time;
        batteryLevel = 30;
    }
}
