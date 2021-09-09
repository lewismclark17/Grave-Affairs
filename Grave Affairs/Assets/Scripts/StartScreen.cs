using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public TMP_Text instructions;
    public TMP_Text join;
    public int bodyRequirement = 0;
    public GameObject playerManager;

    void Awake()
    {
        StartCoroutine(StartDelay());   
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
        join.enabled = true;
        yield return StartCoroutine(WaitForRealSeconds(10f));
        playerManager.SetActive(false);
        if (PlayerController.numberOfPlayers > 0)
        {
            Time.timeScale = 1f;
            join.enabled = false;
            Instructions();
            yield return StartCoroutine(WaitForRealSeconds(1f));
            instructions.enabled = true;
            yield return StartCoroutine(WaitForRealSeconds(5f));
            instructions.enabled = false;
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
            bodyRequirement = 10;
            instructions.text = "Deposit " + bodyRequirement +  " bodies in the mass grave before time runs out!";
        }
        else if (PlayerController.numberOfPlayers == 2)
        {
            bodyRequirement = 15;
            instructions.text = "Deposit " + bodyRequirement +  " bodies in the mass grave before time runs out!";
        }
        else if (PlayerController.numberOfPlayers == 3)
        {
            bodyRequirement = 20;
            instructions.text = "Deposit " + bodyRequirement +  " bodies in the mass grave before time runs out!"; 
        }
        else if (PlayerController.numberOfPlayers == 4)
        {
            bodyRequirement = 25;
            instructions.text = "Deposit " + bodyRequirement +  " bodies in the mass grave before time runs out!";
        }
    }
}
