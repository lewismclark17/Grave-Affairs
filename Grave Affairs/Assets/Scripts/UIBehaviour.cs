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

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
        winScreen.enabled = false;
        loseScreen.enabled = false;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            //countdownText.text = currentTime.ToString ("f0");
        }
        else
        {
        
            if (Score.currentScore > 14)
            {
                winScreen.enabled = true;
            }
                
            else
            {
                loseScreen.enabled = true;
            }
        }
    }
}

