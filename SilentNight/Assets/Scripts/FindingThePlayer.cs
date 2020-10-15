using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingThePlayer : MonoBehaviour
{
    private Patrol patrolScript;
    private AIDestinationSetter destinationScript;

    public int foundPlayer = 0;

    private void Start()
    {
        patrolScript = this.transform.parent.gameObject.GetComponent<Patrol>();
        destinationScript = this.transform.parent.GetComponent<AIDestinationSetter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        patrolScript.enabled = !patrolScript.enabled;
        destinationScript.enabled = !destinationScript.enabled;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        patrolScript.enabled = !patrolScript.enabled;
        destinationScript.enabled = !destinationScript.enabled;
    }
}
