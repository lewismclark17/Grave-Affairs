using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseHole : MonoBehaviour
{
    public Score score;

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "corpse")
        {
            Corpse corpse = col.gameObject.GetComponentInParent<Corpse>();
            corpse.isDealtWith = true;
            corpse.SummonCorpseCartIfNeeded();
            if(corpse.canBeBuried)
            {
                score.AddToScore();
                col.gameObject.tag ="corpseinhole";
            }
        }
    }
}
