using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionText : MonoBehaviour
{
    public TMP_Text instructions;
    private float timeToAppear = 5f;
    private float timeWhenDisappear;

    void Start()
    {
        instructions.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }

    // Update is called once per frame
    void Update()
    {
        if (instructions.enabled && (Time.time >= timeWhenDisappear))
        {
            instructions.enabled = false;
        }      
    }
}
