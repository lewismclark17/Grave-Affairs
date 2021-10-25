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
    public int scoreReq1P, scoreReq2P, scoreReq3P, scoreReq4P;

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
        
            if (Score.currentScore >= scoreReq1P && PlayerController.numberOfPlayers == 1)
            {
                ShowWinScreen();
            }
            else if (Score.currentScore >= scoreReq2P && PlayerController.numberOfPlayers == 2)
            {
                ShowWinScreen();
            }
            else if (Score.currentScore >= scoreReq3P && PlayerController.numberOfPlayers == 3)
            {
                ShowWinScreen();
            }
            else if (Score.currentScore >= scoreReq4P && PlayerController.numberOfPlayers == 4)
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
        if (SceneManager.GetActiveScene().name == "Level3")
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

