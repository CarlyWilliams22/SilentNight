using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STMScript : MonoBehaviour
{
    public SoundwaveScript soundwavePrototype;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            sneakSoundwave();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            walkSoundwave();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            runSoundwave();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            clapSoundwave();
        }
    }

    void sneakSoundwave()
    {
        SoundwaveScript s = Instantiate(soundwavePrototype, new Vector2(0, 0), Quaternion.identity);
        s.setSize(1);
    }

    void walkSoundwave()
    {
        SoundwaveScript s = Instantiate(soundwavePrototype, new Vector2(0, 0), Quaternion.identity);
        s.setSize((float).45);
    }

    void runSoundwave()
    {
        SoundwaveScript s = Instantiate(soundwavePrototype, new Vector2(0, 0), Quaternion.identity);
        s.setSize((float).175);
    }

    void clapSoundwave()
    {
        SoundwaveScript s = Instantiate(soundwavePrototype, new Vector2(0, 0), Quaternion.identity);
        s.setSize((float).1);
    }
}
