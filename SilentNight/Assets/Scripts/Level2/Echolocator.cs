using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Echolocator: MonoBehaviour
{
    public bool running, sneaking, walking, blinded;
    public int bulletNum;
    public int MAX_BULLETS = 4;

    //is the player in echolocator mode or not?
    public bool isBlinded()
    {
        return blinded;
    }

    public void addBullet()
    {
        //if the player has less than the max number of bullets, increment bulletNum
        if (bulletNum < MAX_BULLETS)
        {
            bulletNum++;
        }
    }
}
