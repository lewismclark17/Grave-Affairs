using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseHole : MonoBehaviour
{
    public Score score;

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "corpse")
        {
            Destroy(col.gameObject);
            score.AddToScore();
        }
    }
}
