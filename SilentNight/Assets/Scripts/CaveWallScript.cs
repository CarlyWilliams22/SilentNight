using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWallScript : MonoBehaviour
{

    SpriteRenderer sr;
    float a, r, g, b;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        a = 0;
        r = sr.color.r;
        g = sr.color.g;
        b = sr.color.b;
        sr.color = new Color(r, g, b, a);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (a > 0)
        {
            a -= .001f;
            sr.color = new Color(r, g, b, a);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Soundwave"))
        {
            a = 1;
            sr.color = new Color(r, g, b, a);
        }
        
    }
}
