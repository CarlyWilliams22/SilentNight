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
        //Check if the player is blind or not
        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Level3"))){
            animator.SetBool("Dark", player.isBlinded());
        }
        else
        {
            animator.SetBool("Dark", true);
        }
    }

    //Player hits collectable and picks up a bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.addBullet();
            gameObject.SetActive(false);
        }
    }
  
    //A soundwave hits the collectable while the player is blinded
    //and glows to reveal its location
    private void OnParticleCollision(GameObject other)
    {
        animator.SetBool("Collision", true);
        StartCoroutine(ResetGlow());
    }

    //Coroutine to make the glow go away
    IEnumerator ResetGlow()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetBool("Collision", false);
    }
}
