using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    float barLength;

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
    }
}
