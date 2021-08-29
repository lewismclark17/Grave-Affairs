using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepTable : MonoBehaviour
{
    public GameObject priestBody, nobleBody, kingBody;

    public GameObject cleanPriestBody, cleanNobleBody, cleanKingBody;

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

    public Corpse GetCleanBody()
    {
        if (priestBody.activeSelf)
        {
            priestBody.SetActive(false);
            hasBody = false;
            GameObject cleanBody = Instantiate(cleanPriestBody, transform.position, Quaternion.identity);
            return cleanBody.GetComponent<Corpse>();
        }
        else if (kingBody.activeSelf)
        {
            kingBody.SetActive(false);
            hasBody = false;
            GameObject cleanBody = Instantiate(cleanKingBody, transform.position, Quaternion.identity);
            return cleanBody.GetComponent<Corpse>();
        }
        else if (nobleBody.activeSelf)
        {
            nobleBody.SetActive(false);
            hasBody = false;
            GameObject cleanBody = Instantiate(cleanNobleBody, transform.position, Quaternion.identity);
            return cleanBody.GetComponent<Corpse>();
        }
        return null;
    }
    
}
