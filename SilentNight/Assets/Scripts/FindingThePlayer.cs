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

    bool playerFound = false;

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
        transform.position = monster.transform.position;

        if (!playerFound && !monster.runningAway)   //Player not found, follow patrol
        {
            pathScript.maxSpeed = walkingSpeed;
            patrolScript.enabled = true;
            destinationScript.enabled = false;
        }
        else if (playerFound && !monster.runningAway)  //Player found, chase them!
        {
            monster.chasing = true;
            pathScript.maxSpeed = chasingSpeed;
            patrolScript.enabled = false;
            destinationScript.enabled = true;
        }
        else if (monster.runningAway)
        {
            pathScript.maxSpeed = runningAwaySpeed;
            destinationScript.enabled = true;
            patrolScript.enabled = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerFound = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerFound = false;
            monster.chasing = false;
        }
    }
}
