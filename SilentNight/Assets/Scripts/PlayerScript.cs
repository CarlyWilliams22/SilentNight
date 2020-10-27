using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rbody;

    public int walkSpeed;
    public int runSpeed;
    public int sneakSpeed;
    public GameObject gameOverCanvas;
    int curSpeed;
    bool on;

    public bool running = false;
    public bool walking = true;
    public bool sneaking = false;

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
                walking = true;
                running = false;
                sneaking = false;
            }
            else
            {
                curSpeed = runSpeed;
                running = true;
                walking = false;
                sneaking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (curSpeed == sneakSpeed)
            {
                curSpeed = walkSpeed;
                walking = true;
                sneaking = false;
                running = false;
            }
            else
            {
                curSpeed = sneakSpeed;
                walking = false;
                sneaking = true;
                running = false;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rbody.velocity = new Vector2(x, y);
        Vector2 vel = new Vector2(x, y);
        if(vel.magnitude < 1)
        {
            rbody.velocity = curSpeed * vel;
        }
        else
        {
            rbody.velocity = curSpeed * vel.normalized;
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 d = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y);

        transform.up = d;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("DetectionArea"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        
        //player runs off bridge
        if (collision.gameObject.tag.Equals("Respawn"))
        {
            Level1ManagerScript  l1sm = FindObjectOfType<Level1ManagerScript>();
            gameOverCanvas.SetActive(true);
            l1sm.deathByBridge();
            Destroy(gameObject);
        }

        //player reaches the cave
        if (collision.gameObject.tag.Equals("Finish"))
        {
            //SceneManager.LoadScene("");
        }
    }
}
