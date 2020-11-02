using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParticleScript : MonoBehaviour
{

    ParticleSystem soundWaves;
    ParticleSystem.MainModule main;
    SoundMonsterScript monster;
    AudioSource audioSource;
    float time, waitTime;
    bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundWaves = GetComponent<ParticleSystem>();
        main = soundWaves.main;
        monster = transform.parent.GetComponent<SoundMonsterScript>();
        soundWaves.Stop();
        time = 0;
        waitTime = Random.Range(2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        print(time);
        if(!playing && time > waitTime)
        {
            soundWaves.Play();
            audioSource.Play();
            playing = true;
            time = 0;
        }

        if (playing && time > .3)
        {
            soundWaves.Stop();
            playing = false;
            time = 0;
            waitTime = Random.Range(2f, 5f);
        }
    }
}
