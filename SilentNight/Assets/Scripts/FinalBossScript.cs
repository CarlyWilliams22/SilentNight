using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        Vector3 vectorToTarget = player.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1f);
    }
}
