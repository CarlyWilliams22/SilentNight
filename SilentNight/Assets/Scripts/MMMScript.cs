using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMMScript : MonoBehaviour
{
    public GameObject titleCanvas, instructionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        titleCanvas.SetActive(true);
        instructionCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Lives", 5);    
    }
    
    public void startButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void instructionButton()
    {
        titleCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }

    public void quitButtion()
    {
        Application.Quit();
    }

    public void menuButton()
    {
        titleCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
    }
}
