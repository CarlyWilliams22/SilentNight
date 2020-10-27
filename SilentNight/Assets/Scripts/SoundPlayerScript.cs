using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class SoundPlayerScript : MonoBehaviour
{
    Rigidbody2D rbody;
    public ParticleSystem clap;
    ParticleSystem.MainModule clapping;

    public int walkSpeed;
    public int runSpeed;
    public int sneakSpeed;
    public GameObject gameOverCanvas;
    int curSpeed;

    public bool running = false;
    public bool walking = true;
    public bool sneaking = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        curSpeed = walkSpeed; //start out walking
        clapping = clap.main;
    }

    // Update is called once per frame
    void Update()
    {

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (curSpeed == runSpeed)
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

        if (!clap.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                clap.Play();
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rbody.velocity = new Vector2(x, y);
        Vector2 vel = new Vector2(x, y);
        if (vel.magnitude < 1)
        {
            rbody.velocity = curSpeed * vel;
        }
        else
        {
            rbody.velocity = curSpeed * vel.normalized;
        }
    }
}