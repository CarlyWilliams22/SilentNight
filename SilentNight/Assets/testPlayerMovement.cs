using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerMovement : MonoBehaviour
{
    Rigidbody2D rbody;
    public int maxSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = maxSpeed * Input.GetAxis("Horizontal");
        float y = maxSpeed * Input.GetAxis("Vertical");
        rbody.velocity = new Vector2(x, y);
    }
}
