using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundwaveScript : MonoBehaviour
{

    Transform transform;
    SpriteRenderer sr;
    float xScale, yScale;
    float clear;
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
        transform.localScale = new Vector3(xScale, xScale, 0);
        xScale = yScale += (float).01;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, clear);
        clear -= (float).001;
        if(clear < .0000001)
        {
            Destroy(gameObject);
        }
    }
}
