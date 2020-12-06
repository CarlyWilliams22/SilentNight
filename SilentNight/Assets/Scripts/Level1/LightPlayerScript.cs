 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LightPlayerScript : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator movement;
    GameObject flashlight;
    AudioSource sound;
    SpriteRenderer srender;

    public LevelLoaderScript levelLoader;
    public Level1ManagerScript l1ms;
    public GameObject walkingH, runningH, bridgeCanvas;
    public Slider stamina, battery;
    public Text batteries;
    public AudioClip flashlightOnClip, flashlightOffClip, footstep;

    public int walkSpeed, runSpeed;
    public bool tired, running, currRunnning, flashlightDead, flashlightOn = false;
    public bool walking = true;
    public float maxStamina = 5;

    int curSpeed;
    int batteryNum = 0;
    float batteryLevel = 30;
    float batteryStart;
    string lastSprite = "PlayerSpriteSheet_3";


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        srender = GetComponent<SpriteRenderer>();

        curSpeed = walkSpeed; //start out walking

        flashlight = transform.GetChild(0).gameObject;
        flashlight.SetActive(flashlightOn);

        stamina.maxValue = maxStamina;
        stamina.value = maxStamina;
        
    }

    // Update is called once per frame
    void Update()
    {
 
        batteries.text = "x" + batteryNum.ToString();

        if (flashlightOn)
        {
            battery.value = batteryLevel - (int)(Time.time - batteryStart);
        }

        if (battery.value == 0)
        {
            if (batteryNum == 0)
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
            if (Input.GetKeyDown(KeyCode.Mouse1))
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

        runningH.SetActive(running);
        walkingH.SetActive(walking);

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
            }
            else //start running
            {
                curSpeed = runSpeed;
                running = true;
                walking = false;
            }
        }

        if (tired)
        {
            curSpeed = walkSpeed;
            walking = true;
            running = false;
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
        Quaternion dest = Quaternion.AngleAxis(Mathf.Atan2(diff.y, diff.x) * 180 / Mathf.PI - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, dest, 6 * Time.deltaTime);
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
            levelLoader.LoadNextLevel("Level2");
        }
    }

    void NewBattery()
    {
        batteryStart = Time.time;
        batteryLevel = 30;
    }
}
