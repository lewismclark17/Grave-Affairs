using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public GameObject menuFrame1, menuFrame2, menuFrame3, menuFrame4, menuFrame5;

    public bool loopCheck = true;

    public float sec = 0.01f;

    void Awake()
    {
        menuFrame1.SetActive(true);
        loopCheck = true;
        StartCoroutine(FrameShift());
    }

    IEnumerator FrameShift()
    {
        while (loopCheck == true)
        {
            yield return new WaitForSeconds(sec);
            menuFrame1.SetActive(false);
            menuFrame2.SetActive(true);
            yield return new WaitForSeconds(sec);
            menuFrame2.SetActive(false);
            menuFrame3.SetActive(true);
            yield return new WaitForSeconds(sec);
            menuFrame3.SetActive(false);
            menuFrame4.SetActive(true);
            yield return new WaitForSeconds(sec);
            menuFrame4.SetActive(false);
            menuFrame5.SetActive(true);
            yield return new WaitForSeconds(sec);
            menuFrame5.SetActive(false);
            menuFrame1.SetActive(true);
        }
    }
}
