using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //Add one to the number of hits the boss has retained when a bullet hits him
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster")){
            PlayerPrefs.SetInt("hits", PlayerPrefs.GetInt("hits") + 1);
        }

        Destroy(gameObject);
    }
}
