using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public GameObject boss;
    public GameObject timeline;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<SpriteRenderer>().color = Color.clear;

        //if (player.GetComponent<ComboPlayerScript>().isBlinded())
        //{
        //    //r.color = Color.clear;
        //}
        //else if(!player.GetComponent<ComboPlayerScript>().isBlinded())
        //{
        //    //r.color = Color.white;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if(gameObject.tag == "BulletC")
            {
                player.GetComponent<ComboPlayerScript>().addBullet();
            }
            else if(gameObject.tag == "BatteryC")
            {
                player.GetComponent<ComboPlayerScript>().addBattery();
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
