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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BulletC")
        {
            manager.BulletsFound();
        }
        else if (collision.gameObject.tag == "Gun")
        {
            manager.GunFound();
        }
        else if(collision.gameObject.tag == "Exit")
        {
            manager.NextLevel();
        }
    }
}
