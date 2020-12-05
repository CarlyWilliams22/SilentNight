using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour
{
    Animator animator;
    public GameObject soundPlayer;
    public GameObject comboPlayer;
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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            soundPlayer.SetActive(false);
            comboPlayer.transform.position = soundPlayer.transform.position;
            comboPlayer.SetActive(true);
            boss.SetActive(true);
            timeline.SetActive(true);
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
