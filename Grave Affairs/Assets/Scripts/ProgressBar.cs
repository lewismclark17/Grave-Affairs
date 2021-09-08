using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    float barLength;

    public GameObject sponge;

    public float spongeAnimationTime;

    float spongeTime;

    void Start()
    {
        barLength = transform.localScale.x;
        SetProgress(0);
    }

    public void SetProgress(float progress)
    {
        Vector3 scale = transform.localScale;
        scale.x = barLength * progress;
        transform.localScale = scale;
        if (progress == 0)
        {
            sponge.SetActive(false);
        }
        else
        {
            sponge.SetActive(true);
            spongeTime = spongeAnimationTime;
        }
        
    }

    void Update()
    {
        spongeTime -= Time.deltaTime;
        if (spongeTime < 0)
        {
            sponge.SetActive(false);
        }
    }
}
