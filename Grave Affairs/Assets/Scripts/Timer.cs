using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject timerCircle;
    public GameObject timerHand;
    public Color timerColour1, timerColour2, timerColour3;

    public float rotationTime;
    public float currentTime;
    Image timerCircleImage;

    void Start()
    {
        timerCircleImage = timerCircle.GetComponent<Image>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        float percentage = currentTime / rotationTime;
        Quaternion rotation = Quaternion.Euler(0, 0, -360 * percentage);
        timerHand.transform.rotation = rotation;
        if (percentage > 1)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (percentage > 0.25f)
        {
            timerCircleImage.color = new Color(timerColour1.r, timerColour1.g, timerColour1.b);
        }
         if (percentage > 0.5f)
        {
            timerCircleImage.color = new Color(timerColour2.r, timerColour2.g, timerColour2.b);
        }
          if (percentage > 0.75f)
        {
            timerCircleImage.color = new Color(timerColour3.r, timerColour3.g, timerColour3.b);
        }
    }
}
