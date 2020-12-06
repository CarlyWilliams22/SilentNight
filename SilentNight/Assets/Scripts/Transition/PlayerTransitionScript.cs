using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransitionScript : MonoBehaviour
{
    TSMScript manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<TSMScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //tell manager if player found the bullets
        if (collision.gameObject.tag == "BulletC")
        {
            manager.BulletsFound();
        }
        //tell manager if player found the gun
        else if (collision.gameObject.tag == "Gun")
        {
            manager.GunFound();
        }
        //move on to the next level if exit is found
        else if(collision.gameObject.tag == "Exit")
        {
            manager.NextLevel();
        }
    }
}
