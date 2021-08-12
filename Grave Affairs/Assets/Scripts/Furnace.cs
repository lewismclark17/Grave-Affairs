using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    public Score score;

    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "corpse")
        {
            score.AddToScore();
            Destroy(col.gameObject);
        }
    }
}
