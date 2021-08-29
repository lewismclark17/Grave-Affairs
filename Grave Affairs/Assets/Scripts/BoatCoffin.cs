using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCoffin : MonoBehaviour
{
    public GameObject priestBody, nobleBody, kingBody;

    public bool hasBody;

    public void PlaceBody(Corpse corpse)
    {
        hasBody = true;

        if (corpse.corpseType == Corpse.CorpseType.King)
        {
            kingBody.SetActive(true);
        }
        else if (corpse.corpseType == Corpse.CorpseType.Noble)
        {
            nobleBody.SetActive(true);
        }
        else if (corpse.corpseType == Corpse.CorpseType.Priest)
        {
            priestBody.SetActive(true);
        }
        else if (corpse.corpseType == Corpse.CorpseType.None) //shouldn't be possible -_-
        {
            Debug.Log("canBePrepared variable is true but CorpseType is None");
        }
    }
    
}
