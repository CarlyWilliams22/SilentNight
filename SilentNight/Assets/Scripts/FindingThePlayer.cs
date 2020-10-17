using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingThePlayer : MonoBehaviour
{
    private Patrol patrolScript;
    private AIDestinationSetter destinationScript;

    public bool moving = true;
    public bool playerFound = false;
    bool once = true;
    Vector3 curPos, lastPos;

    private void Start()
    {
        patrolScript = this.transform.parent.gameObject.GetComponent<Patrol>();
        destinationScript = this.transform.parent.GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        //Chase the players last known position
        if (playerFound && once)
        {
            patrolScript.enabled = !patrolScript.enabled;
            destinationScript.enabled = !destinationScript.enabled;
            once = false;
        }
        moving = IsThisObjectMoving();
        //Player is not there, return to patrol
        if (!moving && !playerFound)
        {
            patrolScript.enabled = !patrolScript.enabled;
            destinationScript.enabled = !destinationScript.enabled;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerFound = true;
        once = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerFound = false;
    }

    private bool IsThisObjectMoving()
    {
        curPos = gameObject.transform.parent.gameObject.transform.position;

        if (curPos != lastPos)
        {
            lastPos = curPos;
            return true;
        }
        else
        {
            lastPos = curPos;
            return false;
        }
    }
}
