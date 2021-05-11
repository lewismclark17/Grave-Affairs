using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 180f;
    public Image winScreen;
    public Image loseScreen;
    public Text score;
    public GameObject exitButton;

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
        winScreen.enabled = false;
        loseScreen.enabled = false;
        score.enabled = false;
        exitButton.SetActive(false);
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
        
            if (Score.currentScore > 9 && PlayerController.numberOfPlayers == 1)
            {
                winScreen.enabled = true;
                score.enabled = true;
                exitButton.SetActive(true);
            }
            else if (Score.currentScore > 14 && PlayerController.numberOfPlayers == 2)
            {
                winScreen.enabled = true;
                score.enabled = true;
                exitButton.SetActive(true);
            }
            else if (Score.currentScore > 19 && PlayerController.numberOfPlayers == 3)
            {
                winScreen.enabled = true;
                score.enabled = true;
                exitButton.SetActive(true);
            }
            else if (Score.currentScore > 24 && PlayerController.numberOfPlayers == 4)
            {
                winScreen.enabled = true;
                score.enabled = true;
                exitButton.SetActive(true); 
            } 
            else
            {
                loseScreen.enabled = true;
                score.enabled = true;
                exitButton.SetActive(true);
            }
        }
    }
}

