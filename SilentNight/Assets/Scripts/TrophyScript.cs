using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyScript : MonoBehaviour
{

    public GameObject trophy1, trophy2, trophy3, trophy4, trophy5, trophy6;
    public Text trophy1txt, trophy2txt, trophy3txt, trophy4txt, trophy5txt, trophy6txt;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Trophy1"))
        {
            PlayerPrefs.SetInt("Trophy1", 0);
        }
        if (!PlayerPrefs.HasKey("Trophy2"))
        {
            PlayerPrefs.SetInt("Trophy2", 0);
        }
        if (!PlayerPrefs.HasKey("Trophy3"))
        {
            PlayerPrefs.SetInt("Trophy3", 0);
        }
        if (!PlayerPrefs.HasKey("Trophy4"))
        {
            PlayerPrefs.SetInt("Trophy4", 0);
        }
        if (!PlayerPrefs.HasKey("Trophy5"))
        {
            PlayerPrefs.SetInt("Trophy5", 0);
        }
        if (!PlayerPrefs.HasKey("Trophy6"))
        {
            PlayerPrefs.SetInt("Trophy6", 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("Trophy1") == 1)
        {
            SpriteRenderer r = trophy1.GetComponent<SpriteRenderer>();
            r.color = new Vector4(r.color.r, r.color.g, r.color.b, 1);
            trophy1txt.color = new Vector4(1, 1, 1, 1);
        }

        if (PlayerPrefs.GetInt("Trophy2") == 1)
        {
            SpriteRenderer r = trophy2.GetComponent<SpriteRenderer>();
            r.color = new Vector4(r.color.r, r.color.g, r.color.b, 1);
            trophy2txt.color = new Vector4(1, 1, 1, 1);
        }

        if (PlayerPrefs.GetInt("Trophy3") == 1)
        {
            SpriteRenderer r = trophy3.GetComponent<SpriteRenderer>();
            r.color = new Vector4(r.color.r, r.color.g, r.color.b, 1);
            trophy3txt.color = new Vector4(1, 1, 1, 1);
        }

        if (PlayerPrefs.GetInt("Trophy4") == 1)
        {
            SpriteRenderer r = trophy4.GetComponent<SpriteRenderer>();
            r.color = new Vector4(r.color.r, r.color.g, r.color.b, 1);
            trophy4txt.color = new Vector4(1, 1, 1, 1);
        }

        if (PlayerPrefs.GetInt("Trophy5") == 1)
        {
            trophy5.SetActive(true);
            trophy5txt.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Trophy6") == 1)
        {
            SpriteRenderer r = trophy6.GetComponent<SpriteRenderer>();
            r.color = new Vector4(r.color.r, r.color.g, r.color.b, 1);
            trophy6txt.color = new Vector4(1, 1, 1, 1);
        }

    }
}
