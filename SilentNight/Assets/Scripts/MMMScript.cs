using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMMScript : MonoBehaviour
{
    public TCScript titleCanvas;
    public ICScript instructionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        titleCanvas.gameObject.SetActive(true);
        instructionCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void startButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void instructionButton()
    {
        titleCanvas.gameObject.SetActive(false);
        instructionCanvas.gameObject.SetActive(true);
    }

    public void quitButtion()
    {
        Application.Quit();
    }

    public void menuButton()
    {
        titleCanvas.gameObject.SetActive(true);
        instructionCanvas.gameObject.SetActive(false);
    }
}
