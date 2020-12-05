using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour
{
    public Canvas screen;
    public List<Sprite> images;
    public Animator animator;
    public LevelLoaderScript levelLoader;

    public float delay = 0.1f;
    string fullText, currentTextName;
    string currentText = "";
    bool running = false;
    int page = 1;

    private void Start()
    {
        //First image
        currentTextName = "DrivingText";
        fullText = screen.transform.Find(currentTextName).GetComponent<Text>().text;
        screen.transform.Find(currentTextName).GetComponent<Text>().text = "";
        StartCoroutine(ShowText(fullText));
    }

    private void Update()
    {
        //Player ability to skip cutscene
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadLevel1();
        }

        //Second image
        if (!running && page == 2)
        {
            currentTextName = "CabinText";
            screen.transform.Find(currentTextName).GetComponent<Text>().gameObject.SetActive(true);
            fullText = screen.transform.Find(currentTextName).GetComponent<Text>().text;
            screen.transform.Find(currentTextName).GetComponent<Text>().text = "";
            StartCoroutine(ShowText(fullText));
            screen.transform.Find("Image").GetComponent<Image>().sprite = images[1];
            animator.SetTrigger("StartPicture");
        }

        //Last image
        if (!running && page == 3)
        {
            currentTextName = "BossText";
            screen.transform.Find(currentTextName).GetComponent<Text>().gameObject.SetActive(true);
            fullText = screen.transform.Find(currentTextName).GetComponent<Text>().text;
            screen.transform.Find(currentTextName).GetComponent<Text>().text = "";
            StartCoroutine(ShowText(fullText));
            screen.transform.Find("Image").GetComponent<Image>().sprite = images[2];
            animator.SetTrigger("StartPicture");
            Invoke("LoadLevel1", 7);
        }
    }

    //Type writter text
    IEnumerator ShowText(string text)
    {
        running = true;
        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i);
            screen.transform.Find(currentTextName).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        animator.SetTrigger("ExitPicture");
        yield return new WaitForSeconds(2);
        screen.transform.Find(currentTextName).GetComponent<Text>().gameObject.SetActive(false);
        running = false;
        page++;
    }

    void LoadLevel1()
    {
        levelLoader.LoadLevel1();
    }
}
