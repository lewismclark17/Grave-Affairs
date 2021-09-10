using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBubbles : MonoBehaviour
{
    public GameObject PlagueBubble;
    public GameObject SoldierBubble;

    void Start()
    {
        Debug.Log("test");
        PlagueBubble.SetActive(true);
        SoldierBubble.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Score.currentScore > 3)
        {
            PlagueBubble.SetActive(false);
            SoldierBubble.SetActive(false);
        }
    }
}
