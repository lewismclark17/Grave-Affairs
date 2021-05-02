using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int currentScore = 0;

    [SerializeField] Text scoreText;

    public void AddToScore()
    {
        currentScore++;
        scoreText.text = "People \"laid to rest\": " +currentScore;
    }
}
