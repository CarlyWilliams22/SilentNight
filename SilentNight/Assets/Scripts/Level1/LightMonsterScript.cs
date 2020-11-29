using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using Pathfinding;

public class LightMonsterScript : MonoBehaviour
{
    private AIDestinationSetter destinationScript;
    public PlayerScript playerLocation;
    Transform runAwayLocation;
    public bool chasing = false;
    public bool runningAway = false;
    public float runDistance = 3;   //how far the monster runs when hit by the flashlight
    public AudioClip screech;

    // Start is called before the first frame update
    void Start()
    {
        runAwayLocation = transform.parent.GetChild(2);
        destinationScript = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if it is close to a runaway location
        if (Mathf.Pow((transform.position.x - runAwayLocation.position.x), 2) < 0.1)
        {
            //checks if it is close to player and will chase if close enough
            if (playerLocation)
            {
                destinationScript.target = playerLocation.transform;
                runningAway = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Runs away when hit by flashlight
        if (collision.gameObject.tag.Equals("Light"))
        {
            runningAway = true;
            chasing = false;
            RunAway();
        }

        //if collides with player takes away one life and destorys the player object
        else if (gameObject.tag.Equals("Monster") && collision.gameObject.tag.Equals("Player"))
        {
            PlayerPrefs.SetInt("Lives", (PlayerPrefs.GetInt("Lives") - 1));
            Destroy(collision.transform.parent.gameObject);
        }
    }

    /*monster runs in opposite direction of the player*/
    private void RunAway()
    {
        if (playerLocation)
        {
            float playerX = playerLocation.transform.position.x;
            float playerY = playerLocation.transform.position.y;
            Vector2 playerPos = playerLocation.transform.position;
            float monsterX = transform.position.x;
            float monsterY = transform.position.y;
            Vector2 monsterPos = transform.position;
            float diffX = playerX - monsterX;
            float diffY = playerY - monsterY;
            Vector2 runPos = monsterPos + (monsterPos - playerPos).normalized * runDistance;
            runAwayLocation.position = runPos;
            print("player at " + playerPos + ", monster at " + monsterPos + ", running to " + runPos);
            destinationScript.target = runAwayLocation;
        }
    }

    //plays sound when it tragets the player
    public void screechPlay()
    {
        AudioSource a = GetComponent<AudioSource>();
        a.PlayOneShot(screech);
    }
}
