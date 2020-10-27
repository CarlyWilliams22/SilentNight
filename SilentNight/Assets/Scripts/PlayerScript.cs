using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rbody;

    public int walkSpeed;
    public int runSpeed;
    public int sneakSpeed;
    Animator movement;

    public GameObject bridgeCanvas;
    int curSpeed;
    bool on;

    public bool running = false;
    public bool walking = true;
    public bool sneaking = false;

    public Level1ManagerScript l1ms;

    GameObject flashlight;
    AudioSource sound;
    public AudioClip flashlightOn;
    public AudioClip flashlightOff;

    SpriteRenderer srender;
    string lastSprite = "PlayerSpriteSheet_3";
    public AudioClip footstep;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Animator>();
        curSpeed = walkSpeed; //start out walking
        on = false;
        flashlight = transform.GetChild(0).gameObject;
        flashlight.SetActive(on);
        sound = GetComponent<AudioSource>();
        srender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {        
        

        if (Input.GetMouseButtonDown(0))
        {
            if (on)
            {
                on = false;
                sound.PlayOneShot(flashlightOff);
            }
            else
            {
                on = true;
                sound.PlayOneShot(flashlightOn);
            }
            flashlight.SetActive(on);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(curSpeed == runSpeed)
            {
                curSpeed = walkSpeed;
                walking = true;
                running = false;
                sneaking = false;
            }
            else
            {
                curSpeed = runSpeed;
                running = true;
                walking = false;
                sneaking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (curSpeed == sneakSpeed)
            {
                curSpeed = walkSpeed;
                walking = true;
                sneaking = false;
                running = false;
            }
            else
            {
                curSpeed = sneakSpeed;
                walking = false;
                sneaking = true;
                running = false;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rbody.velocity = new Vector2(x, y);
        Vector2 vel = new Vector2(x, y);
        if(vel.magnitude < 1)
        {
            rbody.velocity = curSpeed * vel;
        }
        else
        {
            rbody.velocity = curSpeed * vel.normalized;
        }

        if(rbody.velocity == Vector2.zero)
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

        if (srender.sprite.name == "PlayerSpriteSheet_1" && lastSprite == "PlayerSpriteSheet_3"){
            lastSprite = "PlayerSpriteSheet_1";
            sound.PlayOneShot(footstep);
        }
        if (srender.sprite.name == "PlayerSpriteSheet_3" && lastSprite == "PlayerSpriteSheet_1")
        {
            lastSprite = "PlayerSpriteSheet_3";
            sound.PlayOneShot(footstep);
        }

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 d = mousePos - transform.position;/*new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y);*/

        transform.up = d;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("DetectionArea"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        
        //player runs off bridge
        if (collision.gameObject.tag.Equals("Respawn"))
        {
            bridgeCanvas.SetActive(true);
            l1ms.deathByBridge();
            Destroy(gameObject);
        }

        //player reaches the cave
        if (collision.gameObject.tag.Equals("Finish"))
        {
            l1ms.nextLevel();
        }
    }
}
