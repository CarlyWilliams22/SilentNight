using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SoundMonsterScript : MonoBehaviour
{
    AIDestinationSetter destinationSetter;
    Patrol patrol;
    public Transform lastKnownPosition;

    public L2MScript l2ms;

    AudioSource audioSource;
    public AudioClip growl;
    bool isGrowling  = false;
    bool alive;

    // Start is called before the first frame update
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        patrol = GetComponent<Patrol>();
        audioSource = GetComponent<AudioSource>();
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Go back to patrolling if desetination target reached
        if(transform.position == destinationSetter.target.position)
        {
            patrol.enabled = true;
            destinationSetter.enabled = false;
        }

        //Growl 5 percent of the time
        if(!isGrowling && (Random.Range(0, 100) < 5))
        {
            audioSource.PlayOneShot(growl);
            isGrowling = true;
        }

        //set isGrowling to false once the audio stops playing
        if (!audioSource.isPlaying)
        {
            isGrowling = false;

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player dies if monster collides with him
        if (collision.gameObject.tag.Equals("Player"))
        {
            int currLives = PlayerPrefs.GetInt("Lives");
            if (alive)
            {
                PlayerPrefs.SetInt("Lives", (currLives - 1));
                alive = false;
            }
            Destroy(collision.gameObject);
            l2ms.Death();
        }
    }

    private void OnParticleCollision(GameObject collision)
    {
        //if monster hears the player, stop patrolling and go to his last known position
        if (collision.gameObject.tag.Equals("Soundwave"))
        {
            patrol.enabled = false;
            destinationSetter.enabled = true;

            float x = collision.transform.position.x;
            float y = collision.transform.position.y;
            lastKnownPosition.position = new Vector3(x, y, 0);
            destinationSetter.target = lastKnownPosition;
        }
    }
}
