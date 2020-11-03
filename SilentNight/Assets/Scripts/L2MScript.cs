using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L2MScript : MonoBehaviour
{
    public GameObject gameOverCanvas, openingDialog, player, monster1, monster2;
    public Text openingDialogText, instructionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (openingDialogText.gameObject.activeInHierarchy)
            {
                openingDialogText.gameObject.SetActive(false);
                instructionText.gameObject.SetActive(true);
            }
            else if (instructionText.gameObject.activeInHierarchy)
            {
                openingDialog.SetActive(false);
                instructionText.gameObject.SetActive(false);
                player.SetActive(true);
                monster1.SetActive(true);
                monster2.SetActive(true);
            }
            
        }
    }

    public void Death()
    {
        gameOverCanvas.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
