using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingThePlayer : MonoBehaviour
{
    private Patrol patrolScript;
    private AIDestinationSetter destinationScript;
    private AIPath pathScript;
    public LightMonsterScript monster;

    bool playerFound = false;   //If monster has found player in their range
    bool soundCheck = false;

    //Different speeds for the monster in different states
    public int chasingSpeed = 6;
    public int walkingSpeed = 1;
    public int runningAwaySpeed = 6;

    private void Start()
    {
        pathScript = transform.parent.GetChild(1).gameObject.GetComponent<AIPath>();
        patrolScript = transform.parent.GetChild(1).gameObject.GetComponent<Patrol>();
        destinationScript = transform.parent.GetChild(1).GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        //Update the detection areas position to stay with the monster
        transform.position = monster.transform.position;

        if (!playerFound && !monster.runningAway)   //Player not found, follow patrol
        {
            pathScript.maxSpeed = walkingSpeed;
            patrolScript.enabled = true;
            destinationScript.enabled = false;
            soundCheck = false;
        }
        else if (playerFound && !monster.runningAway)  //Player found, chase them!
        {
            monster.chasing = true;

            //Screech when the monster found the player
            if (!soundCheck)
            {
                monster.screechPlay();
                soundCheck = true;
            }
            pathScript.maxSpeed = chasingSpeed;
            patrolScript.enabled = false;
            destinationScript.enabled = true;
        }

        //Monster is hit by the light source and runs away from the player
        else if (monster.runningAway)
        {
            pathScript.maxSpeed = runningAwaySpeed;
            destinationScript.enabled = true;
            patrolScript.enabled = false;
        }
    }
    
    //Sets the boolean is the player is found if their collider
    //Enters the detection area of the monster
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerFound = true;
        }
        
    }

    //The player leaves the detection area so the monster
    //can return to its patrol
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerFound = false;
            monster.chasing = false;
        }
    }
}
