using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleScript : MonoBehaviour
{
    ParticleSystem soundWaves;
    ParticleSystem.MainModule main;
    Echolocator player;
    AudioSource audioSource;

    bool walkOnce, sneakOnce, playOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundWaves = GetComponent<ParticleSystem>();
        main = soundWaves.main;

        if (SceneManager.GetActiveScene().name.Equals("Level2")){
            player = transform.parent.GetComponent<SoundPlayerScript>();
        }
        else
        {
            player = transform.parent.GetComponent<ComboPlayerScript>();
        }
        soundWaves.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //plays soundwaves and walking sounds while the player is moving 
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1 && !playOnce)
        {
            soundWaves.Play();
            audioSource.loop = true;
            audioSource.Play();
            playOnce = true;
        }
        //if player is still no soundwaves
        else if (player.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1)
        {
            soundWaves.Stop();
            audioSource.loop = false;
            playOnce = false;
        }
        //if player is walking make normal soundwaves
        if (player.walking && !walkOnce)
        {
            walkOnce = true;
            sneakOnce = false;
            walk();
        }
        //if player is sneaking play small soundwaves
        else if (player.sneaking && !sneakOnce)
        {
            sneakOnce = true;
            walkOnce = false;
            sneak();
        }
    }

    /*Sets the sound and duration of the walking soundwaves*/
    public void walk()
    {
        audioSource.pitch = 1f;
        audioSource.volume = .7f;
        main.startSpeed = 3;
        main.duration = .7f;
    }

    /*Sets the sound and duration of the sneaking soundwaves*/
    public void sneak()
    {
        audioSource.pitch = .7f;
        audioSource.volume = .5f;
        main.startSpeed = .7f;
        main.duration = 1;
    }
}
