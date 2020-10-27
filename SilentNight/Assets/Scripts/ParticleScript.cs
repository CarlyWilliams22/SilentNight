using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    ParticleSystem soundWaves;
    ParticleSystem.MainModule main;
    SoundPlayerScript player;
    float seconds = 0f;
    float delay = 1f;

    bool isMoving, walkOnce, runOnce, sneakOnce, playOnce, clicked = false;

    // Start is called before the first frame update
    void Start()
    {
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
            playOnce = true;
        }
        else if (player.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1)
        {
            soundWaves.Stop();
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
        main.startSpeed = 1;
        main.duration = 1;
    }
}
