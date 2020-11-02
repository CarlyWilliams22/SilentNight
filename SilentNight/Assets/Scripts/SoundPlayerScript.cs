﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class SoundPlayerScript : MonoBehaviour
{
    Rigidbody2D rbody;
    public ParticleSystem particleSystem;
    ParticleSystem.MainModule clapping;
    AudioSource audioSource;
    public AudioClip clapSound;

    public int walkSpeed;
    public int runSpeed;
    public int sneakSpeed;
    int curSpeed;

    public bool running = false;
    public bool walking = true;
    public bool sneaking = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody2D>();
        curSpeed = walkSpeed; //start out walking
        clapping = particleSystem.main;
    }

    private void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (curSpeed == runSpeed)
            {
                curSpeed = walkSpeed;
                walking = true;
                sneaking = false;
                running = false;
            }
            else
            {
                curSpeed = runSpeed;
                walking = false;
                sneaking = false;
                running = true;
            }
        }

        if (!particleSystem.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                audioSource.PlayOneShot(clapSound);
                particleSystem.Play();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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