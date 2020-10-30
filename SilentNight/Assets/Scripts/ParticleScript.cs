using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    ParticleSystem soundWaves;
    ParticleSystem.MainModule main;
    SoundPlayerScript player;
    AudioSource audioSource;

    bool walkOnce, runOnce, sneakOnce, playOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundWaves = GetComponent<ParticleSystem>();
        main = soundWaves.main;
        player = transform.parent.GetComponent<SoundPlayerScript>();
        soundWaves.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1 && !playOnce)
        {
            soundWaves.Play();
            audioSource.loop = true;
            audioSource.Play();
            playOnce = true;
        }
        else if (player.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1)
        {
            soundWaves.Stop();
            audioSource.loop = false;
            playOnce = false;
        }

        if (player.walking && !walkOnce)
        {
            walkOnce = true;
            runOnce = sneakOnce = false;
            walk();
        }
        else if (player.running && !runOnce)
        {
            runOnce = true;
            walkOnce = sneakOnce = false;
            run();
        }
        else if (player.sneaking && !sneakOnce)
        {
            sneakOnce = true;
            walkOnce = runOnce = false;
            sneak();
        }
    }

    public void walk()
    {
        audioSource.pitch = 1f;
        main.startSpeed = 3;
        main.duration = .7f;
    }

    public void run()
    {
        main.startSpeed = 6;
        main.duration = .5f;
    }

    public void sneak()
    {
        audioSource.pitch = .7f;
        main.startSpeed = 1;
        main.duration = 1;
    }
}
