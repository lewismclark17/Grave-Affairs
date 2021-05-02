using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseHole : MonoBehaviour
{
    public Score score;

    void OnTriggerEnter (Collider col)
    {
        if(col.tag == "corpse")
        {
            score.AddToScore();
        }
    }
}
