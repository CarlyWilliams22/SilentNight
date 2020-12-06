using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Echolocator: MonoBehaviour
{
    public bool running, blinded, sneaking, walking;
    public int bulletNum;
    public int MAX_BULLETS = 4;

    public bool isBlinded()
    {
        return blinded;
    }

    public void addBullet()
    {
        if (bulletNum < MAX_BULLETS)
        {
            bulletNum++;
        }
    }
}
