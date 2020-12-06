using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectablesScript : MonoBehaviour
{
    Animator animator;
    Echolocator player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Echolocator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Level3"))){
            animator.SetBool("Dark", player.isBlinded());
        }
        else
        {
            animator.SetBool("Dark", true);
        }
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
            gameObject.SetActive(false);
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
