using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster")){
            PlayerPrefs.SetInt("hits", PlayerPrefs.GetInt("hits") + 1);
        }

        Destroy(gameObject);
    }
}
