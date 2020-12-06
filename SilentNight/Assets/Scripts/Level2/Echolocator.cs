using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Echolocator: MonoBehaviour
{
    public bool running;
    public bool sneaking;
    public bool walking;
    public bool blinded;
    public int bulletNum;

    public bool isBlinded()
    {
        return blinded;
    }

    public void addBullet()
    {
        bulletNum++;
    }

}
