using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESManager : MonoBehaviour
{

    public GameObject achievementBox, achievement3txt, achievement6txt;
    private void Start()
    {
        //PlayerPrefs.SetInt("Trophy3", 0);
        PlayerPrefs.SetInt("Trophy6", 0);

        if (PlayerPrefs.GetInt("Trophy3") == 0)
        {
            PlayerPrefs.SetInt("Trophy3", 1);
            achievementBox.SetActive(true);
            achievement3txt.SetActive(true);
            if((PlayerPrefs.GetInt("Trophy6")==0) && (PlayerPrefs.GetInt("Lives") == 5))
            {
                PlayerPrefs.SetInt("Trophy6", 1);
                Invoke("Trophy6Wait", 3);
            }
        }

        if((PlayerPrefs.GetInt("Trophy6") == 0) && (PlayerPrefs.GetInt("Lives") == 5))
        {
            PlayerPrefs.SetInt("Trophy6", 1);
            achievementBox.SetActive(true);
            achievement6txt.SetActive(true);

        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Trophy6Wait()
    {
        achievementBox.SetActive(false);
        achievementBox.SetActive(true);
        achievement6txt.SetActive(true);
    }
}
