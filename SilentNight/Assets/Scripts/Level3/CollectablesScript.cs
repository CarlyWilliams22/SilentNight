using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour
{
    Animator animator;
    ComboPlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<ComboPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    { 
        animator.SetBool("Dark", player.isBlinded());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if(gameObject.tag == "BulletC")
            {
                player.addBullet();
            }
            else if(gameObject.tag == "BatteryC")
            {
                player.addBattery();
            }
            Destroy(gameObject);
        }
    }
  
    private void OnParticleCollision(GameObject other)
    {
        animator.SetBool("Collision", true);
        StartCoroutine(ResetGlow());
    }


    IEnumerator ResetGlow()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetBool("Collision", false);
    }
}
