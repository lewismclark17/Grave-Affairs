using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    public Score score;

    void OnTriggerStay (Collider col)
    {
        if(col.gameObject.tag == "corpse")
        {
            Corpse corpse = col.gameObject.GetComponentInParent<Corpse>();
            if (corpse.gameObject.tag == "corpse")
            {
                if (!corpse.IsBeingCarried())
                {
                    corpse.Burn();
                    corpse.gameObject.tag = "burntcorpse";
                    if (col.gameObject.GetComponentInParent<Corpse>().canBeIncinerated)
                    {
                        score.AddToScore();
                    }
                }
            }
        }
    }
}
