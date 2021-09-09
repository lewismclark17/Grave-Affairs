using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingPrompt : MonoBehaviour
{
    bool isFlashing;
    public float flashTime;
    float time;
    public GameObject target;

    void Start()
    {
        StopFlash();
    }

    public void StartFlash()
    {
        target.SetActive(true);
        isFlashing = true;
        time = flashTime;
    }

    public void StopFlash()
    {
        target.SetActive(false);
        isFlashing = false;
    }

    void Update()
    {
        if (isFlashing)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                target.SetActive(!target.activeSelf);
                time = flashTime;
            }
        }
    }
}
