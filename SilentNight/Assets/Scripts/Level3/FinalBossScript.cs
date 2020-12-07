using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{
    Animator animator;

    public GameObject snotShotPrefab, bullet;
    public Transform playerPos, firePoint;
    public AudioSource audio;
    public AudioClip death;

    public int snotSpeed = 5;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        
        //Start shooting after the intro scene is over
        InvokeRepeating("ShootSnot", 5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //While the player is still alive...
        if (playerPos)
        {
            //Rotate his body to always face and shoot at the player
            Vector3 vectorToTarget = playerPos.position - transform.GetChild(0).transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.GetChild(0).transform.rotation = Quaternion.Slerp(transform.GetChild(0).transform.rotation, q, 1f);
        }
    }

    //Shoot at the player and rotate the shots towards the player
    void ShootSnot()
    {
        GameObject snotShot = Instantiate(snotShotPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRbody = snotShot.GetComponent<Rigidbody2D>();
        float angle = transform.GetChild(0).transform.rotation.eulerAngles.z + 90;
        bulletRbody.rotation = angle;
        angle = (angle + 270) * Mathf.Deg2Rad;
        bulletRbody.velocity = new Vector2(snotSpeed * Mathf.Cos(angle), snotSpeed * Mathf.Sin(angle));
    }

    //The boss is about to die so he begins shooting rapidly
    public void LastResort()
    {
        CancelInvoke();
        InvokeRepeating("ShootSnot", 0f, 0.5f);
    }
}
