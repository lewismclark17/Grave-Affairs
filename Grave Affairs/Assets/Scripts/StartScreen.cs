using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public TMP_Text instructions;
    public GameObject playerManager, tutorialScreen, joinText;
    public TutorialBubbles tutorialBubbles;
    UIBehaviour uIBehaviour;

    void Awake()
    {
        StartCoroutine(StartDelay());   
    }

    void Start()
    {
        uIBehaviour = GetComponentInChildren<UIBehaviour>();
    }

    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }

    IEnumerator StartDelay()
    {
        Time.timeScale = 0f;
        instructions.enabled = false;
        tutorialScreen.SetActive(true);
        yield return StartCoroutine(WaitForRealSeconds(1f));
        tutorialScreen.SetActive(false);
        joinText.SetActive(true);
        yield return StartCoroutine(WaitForRealSeconds(2f));
        playerManager.SetActive(false);
        if (PlayerController.numberOfPlayers > 0)
        {
            Time.timeScale = 1f;
            joinText.SetActive(false);
            Instructions();
            yield return StartCoroutine(WaitForRealSeconds(1f));
            instructions.enabled = true;
            yield return StartCoroutine(WaitForRealSeconds(5f));
            instructions.enabled = false;
            tutorialBubbles.GetPlayers();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
        }
        
    }

    void Instructions()
    {
        if (PlayerController.numberOfPlayers == 1)
        {
            instructions.text = "Dispose of " + uIBehaviour.scoreReq1P +  " bodies before time runs out";
        }
        else if (PlayerController.numberOfPlayers == 2)
        {
            instructions.text = "Dispose of " + uIBehaviour.scoreReq2P +  " bodies before time runs out";
        }
        else if (PlayerController.numberOfPlayers == 3)
        {
            instructions.text = "Dispose of " + uIBehaviour.scoreReq3P +  " bodies before time runs out"; 
        }
        else if (PlayerController.numberOfPlayers == 4)
        {
            instructions.text = "Dispose of " + uIBehaviour.scoreReq4P +  " bodies before time runs out";
        }
    }
}
