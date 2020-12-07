using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnotShotScript : MonoBehaviour
{
    //Destroy object when it hits something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
