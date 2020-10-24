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
    public float runDistance = 3;

    // Start is called before the first frame update
    void Start()
    {
        runAwayLocation = transform.parent.GetChild(2);
        destinationScript = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Pow((transform.position.x - runAwayLocation.position.x), 2) < 0.1)
        {
            destinationScript.target = playerLocation.transform;
            runningAway = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Light"))
        {
            runningAway = true;
            chasing = false;
            RunAway();
        }

        else if (gameObject.tag.Equals("Monster") && collision.gameObject.tag.Equals("Player"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

    private void RunAway()
    {
        float playerX = playerLocation.transform.position.x;
        float playerY = playerLocation.transform.position.y;
        Vector2 playerPos = playerLocation.transform.position;
        float monsterX = transform.position.x;
        float monsterY = transform.position.y;
        Vector2 monsterPos = transform.position;
        float diffX = playerX - monsterX;
        float diffY = playerY - monsterY;
        //runAwayLocation.position = new Vector3((monsterX - diffX) * runDistance, (monsterY - diffY) * runDistance, -1);
        Vector2 runPos = monsterPos + (monsterPos - playerPos).normalized * runDistance;
        runAwayLocation.position = runPos;
        print("player at " + playerPos + ", monster at " + monsterPos + ", running to " + runPos);
        destinationScript.target = runAwayLocation;
    }
}
