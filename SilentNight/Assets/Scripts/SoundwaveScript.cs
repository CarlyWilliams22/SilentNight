using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundwaveScript : MonoBehaviour
{

    Transform transform;
    SpriteRenderer sr;
    float xScale, yScale;
    float clear;
    float scale;
    // Start is called before the first frame update
    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        xScale = yScale = 0;
        clear = sr.color.a;
        
    }

    // Update is called once per frame
    void Update()
    {
        
            xScale = yScale += (float).01;
            transform.localScale = new Vector3(xScale, yScale, 0);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, clear);
            clear -= (float).01 * scale;
            if (clear < .0000001)
            {
                Destroy(gameObject);
            }
        
        
    }

    public void setSize(float size)
    {
        scale = size;
    }
}
