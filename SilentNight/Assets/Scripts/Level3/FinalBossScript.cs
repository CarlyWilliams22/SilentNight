using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{
    public GameObject snotShotPrefab, bullet;
    public Transform playerPos, firePoint;
    public int snotSpeed = 5;

    Animator animator;

    private void Start()
    {
        InvokeRepeating("ShootSnot", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorToTarget = playerPos.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1f);


    }

    void ShootSnot()
    {
        GameObject snotShot = Instantiate(snotShotPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRbody = snotShot.GetComponent<Rigidbody2D>();
        float angle = transform.rotation.eulerAngles.z - 90;
        bulletRbody.rotation = angle;
        angle = (angle + 270) * Mathf.Deg2Rad;
        bulletRbody.velocity = new Vector2(snotSpeed * Mathf.Cos(angle), snotSpeed * Mathf.Sin(angle));
    }
}
