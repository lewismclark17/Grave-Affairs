using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 300f;
    public GameObject winScreen;
    public GameObject loseScreen;
    public Text score;
    public GameObject exitButton, continueButton;

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        score.enabled = false;
        exitButton.SetActive(false);
        continueButton.SetActive(false);
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            //countdownText.text = currentTime.ToString ("f0");
            score.text = Score.currentScore + " bodies";
        }
        else
        {
        
            if (Score.currentScore > 19 && PlayerController.numberOfPlayers == 1)
            {
                ShowWinScreen();
            }
            else if (Score.currentScore > 34 && PlayerController.numberOfPlayers == 2)
            {
                ShowWinScreen();
            }
            else if (Score.currentScore > 49 && PlayerController.numberOfPlayers == 3)
            {
                ShowWinScreen();
            }
            else if (Score.currentScore > 69 && PlayerController.numberOfPlayers == 4)
            {
                ShowWinScreen();
            } 
            else
            {
                ShowLoseScreen();
            }
        }
    }

    void ShowWinScreen()
    {
        winScreen.SetActive(true);
        score.enabled = true;
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            exitButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }

    void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        score.enabled = true;
        exitButton.SetActive(true);
    }
}

