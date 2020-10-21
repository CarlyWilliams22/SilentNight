using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingThePlayer : MonoBehaviour
{
    private Patrol patrolScript;
    private AIDestinationSetter destinationScript;
    private LightMonsterScript monsterScript;
    private AIPath pathScript;
    public LightMonsterScript monster;

    bool playerFound = false;

    public int chasingSpeed = 6;
    public int walkingSpeed = 1;

    private void Start()
    {
        pathScript = transform.parent.GetChild(0).gameObject.GetComponent<AIPath>();
        monsterScript = transform.parent.GetChild(0).gameObject.GetComponent<LightMonsterScript>();
        patrolScript = transform.parent.GetChild(0).gameObject.GetComponent<Patrol>();
        destinationScript = transform.parent.GetChild(0).GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        transform.position = monster.transform.position;

        if (!playerFound)   //Player not found, follow patrol
        {
            pathScript.maxSpeed = walkingSpeed;
            patrolScript.enabled = true;
            destinationScript.enabled = false;
        }
        else   //Player found, chase them!
        {
            pathScript.maxSpeed = chasingSpeed;
            patrolScript.enabled = false;
            destinationScript.enabled = true;
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
        }
    }
}
