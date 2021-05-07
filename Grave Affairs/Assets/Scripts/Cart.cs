using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public GameObject CartCorpses;

    public GameObject ActualCorpses;

    public float sec = 5.5f;

    void Start()
    {
        StartCoroutine(CorpseMagic());
    }

    IEnumerator CorpseMagic()
    {
        yield return new WaitForSeconds(sec);
        CartCorpses.SetActive(false);
        ActualCorpses.SetActive(true);
    }
}
