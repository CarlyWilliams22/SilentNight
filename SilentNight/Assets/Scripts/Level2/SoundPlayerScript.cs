using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class SoundPlayerScript : Echolocator
{
    Rigidbody2D rbody;
    AudioSource audioSource;

    public GameObject walkingH, sneakingH;
    public ParticleSystem clap;
    public AudioClip clapSound;
    public LevelLoaderScript levelLoader;

    public int walkSpeed, sneakSpeed;

    int curSpeed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody2D>();
        curSpeed = walkSpeed; //start out walking

        sneaking = false;
        walking = true;
    }

    private void Update()
    {
        //toggle sneaking
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //walk if already sneaking
            if (curSpeed == sneakSpeed)
            {
                curSpeed = walkSpeed;
                walking = true;
                sneaking = false;
            }
            else //start sneaking
            {
                curSpeed = sneakSpeed;
                walking = false;
                sneaking = true;
            }
        }

        //update hud with current type of movement
        walkingH.SetActive(walking);
        sneakingH.SetActive(sneaking);

        //clap when left mouse button is clicked and player is not already clapping
        if (!clap.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audioSource.PlayOneShot(clapSound);
                clap.Play();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
        //load the next scene if the player reaches the end of the cave
        if (collision.gameObject.tag.Equals("Finish"))
        {
            levelLoader.LoadNextLevel("Level3Transition");
        }
    }
}