using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseHole : MonoBehaviour
{
    public Score score;

    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "corpse")
        {
            score.AddToScore();
            col.gameObject.tag ="corpseinhole";
        }
    }
}
