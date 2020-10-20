﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rbody;

    public int walkSpeed;
    public int runSpeed;
    public int sneakSpeed;
    int curSpeed;
    bool on;
    

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        curSpeed = walkSpeed; //start out walking
        on = false;
        transform.GetChild(0).gameObject.SetActive(on);
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (Input.GetMouseButtonDown(0))
        {
            if (on)
            {
                on = false;
                transform.GetChild(0).gameObject.SetActive(on);
            }
            else
            {
                on = true;
                transform.GetChild(0).gameObject.SetActive(on);


            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(curSpeed == runSpeed)
            {
                curSpeed = walkSpeed;
            }
            else
            {
                curSpeed = runSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (curSpeed == sneakSpeed)
            {
                curSpeed = walkSpeed;
            }
            else
            {
                curSpeed = sneakSpeed;
            }
        }

        float x = curSpeed * Input.GetAxis("Horizontal");
        float y = curSpeed * Input.GetAxis("Vertical");
        rbody.velocity = new Vector2(x, y);
        //Vector2 vel = new Vector2(x, y);
        //rbody.velocity = curSpeed * vel.normalized;

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 d = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y);

        transform.up = d;
    }
}
