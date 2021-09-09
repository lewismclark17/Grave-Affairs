using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepTable : MonoBehaviour
{
    public GameObject priestBody, nobleBody, kingBody;

    public GameObject riggedPriestBody, riggedNobleBody, riggedKingBody;

    public GameObject cleanPriest, cleanNoble, cleanKing;

    public GameObject bar, placePrompt;
    
    public ProgressBar progressBar;

    public BoatSpawner boatSpawner;

    FlashingPrompt flashingPrompt;

    public bool hasBody, isPriestBody;

    int bodyCleanliness;

    public void Start()
    {
        bar.SetActive(false);
        flashingPrompt = GetComponent<FlashingPrompt>();
    }

    public void PlaceBody(Corpse corpse)
    {
        hasBody = true;
        bodyCleanliness = 0;
        bar.SetActive(true);
        progressBar.SetProgress(0);
        boatSpawner.RequestBoat();
        flashingPrompt.StartFlash();

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
            isPriestBody = true;
        }
        else if (corpse.corpseType == Corpse.CorpseType.None) //shouldn't be possible -_-
        {
            Debug.Log("canBePrepared variable is true but CorpseType is None");
        }
    }

    public Corpse GetCleanBody()
    {
        if (cleanPriest.activeSelf)
        {
            cleanPriest.SetActive(false);
            hasBody = false;
            GameObject cleanBody = Instantiate(riggedPriestBody, transform.position, Quaternion.identity);
            return cleanBody.GetComponent<Corpse>();
        }
        else if (cleanKing.activeSelf)
        {
            cleanKing.SetActive(false);
            hasBody = false;
            GameObject cleanBody = Instantiate(riggedKingBody, transform.position, Quaternion.identity);
            return cleanBody.GetComponent<Corpse>();
        }
        else if (cleanNoble.activeSelf)
        {
            cleanNoble.SetActive(false);
            hasBody = false;
            GameObject cleanBody = Instantiate(riggedNobleBody, transform.position, Quaternion.identity);
            return cleanBody.GetComponent<Corpse>();
        }
        return null;
    }

    public void CleanCorpse()
    {
        if (bodyCleanliness < 20)
        {
            bodyCleanliness++;
            progressBar.SetProgress(bodyCleanliness / 20.0f);
        }
        else
        {
            bar.SetActive(false);
            flashingPrompt.StopFlash();
            if (priestBody.activeSelf)
            {
                priestBody.SetActive(false);
                cleanPriest.SetActive(true);
            }
            else if (kingBody.activeSelf)
            {
                kingBody.SetActive(false);
                cleanKing.SetActive(true);
            }
            else if (nobleBody.activeSelf)
            {
                nobleBody.SetActive(false);
                cleanNoble.SetActive(true);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponentInParent<PlayerController>().HasDirtyCorpse())
        {
            placePrompt.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        placePrompt.SetActive(false);
    }
}
