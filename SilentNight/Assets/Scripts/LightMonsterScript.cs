using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using Pathfinding;

public class LightMonsterScript : MonoBehaviour
{
    private AIDestinationSetter destinationScript;
    Transform newLocation;

    //int monsterSpeed = 4;
    //public PlayerScript player;
    //const int CHASE_DIST = 5;
    bool chasing = false;
    //Rigidbody2D rbody;
    public bool running = false;
    float runStartTime;

    // Start is called before the first frame update
    void Start()
    {
        destinationScript = GetComponent<AIDestinationSetter>();
        //rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 monsterPos = transform.position;
        Vector2 playerPos;
        if (player) {
            playerPos = player.transform.position;
        }
        else
        {
            playerPos = monsterPos;
        }
        Vector2 diff = playerPos - monsterPos;

        if (!chasing && !running)
        {
            rbody.velocity = monsterSpeed * (rbody.velocity + new Vector2(Random.Range(.1f, 1), Random.Range(.1f, 1))).normalized;
            //if player is close enough, chase him
            chasing = diff.magnitude < CHASE_DIST;
        }
        if (chasing)
        {
            rbody.velocity = monsterSpeed * diff.normalized;
        }
        if (running)
        {
            //velocity stays the same

            if(Time.time - runStartTime > 30)
            {
                running = false;
            }
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Light"))
        {
            RunAway();
        }
        if (gameObject.tag.Equals("Monster") && collision.gameObject.tag.Equals("Player"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Light"))
        {
            running = false;
        }
    }

    private void RunAway()
    {
        //rbody.velocity = -rbody.velocity;
        //runStartTime = Time.time;
        running = true;
        chasing = false;
        float playerX = destinationScript.transform.position.x;
        float playerY = destinationScript.transform.position.y;
        float monsterX = transform.position.x;
        float monsterY = transform.position.y;
        float diffX = playerX - monsterX;
        float diffY = playerY - monsterY;
        Vector3 runAwayLocation = new Vector3((monsterX - diffX), (monsterY - diffY), 0);
        destinationScript.target = Instantiate(newLocation, runAwayLocation, Quaternion.identity);
    }
}
