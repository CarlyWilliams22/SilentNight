using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L3MScript : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("damage", 3);
        PlayerPrefs.SetInt("hits", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("damage") <= 0)
        {
            SceneManager.LoadScene("Level3");
        }

        if(PlayerPrefs.GetInt("hits") >= 3)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
