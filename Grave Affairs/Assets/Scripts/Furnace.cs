using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    public GameObject bar, placePrompt;

    public GameObject furnaceCorpse;
    
    public ProgressBar progressBar;

    public Score score;

    FlashingPrompt flashingPrompt;

    float bodyPushProgress;

    public float decayRate;

    public bool hasBody;

    public float finalPushDistance;

    Vector3 corpseStartPos;

    /*void OnTriggerStay (Collider col)
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
    }*/

    public void Start()
    {
        bar.SetActive(false);
        corpseStartPos = furnaceCorpse.transform.position;
        furnaceCorpse.SetActive(false);
        flashingPrompt = GetComponent<FlashingPrompt>();
    }

    public void Update()
    {
        if (hasBody)
        {
            bodyPushProgress = Mathf.Clamp(bodyPushProgress - Time.deltaTime * decayRate, 0.0f, 20.0f);
            Vector3 pos = corpseStartPos + furnaceCorpse.transform.up * bodyPushProgress / 20.0f * finalPushDistance;
            furnaceCorpse.transform.position = pos;
            progressBar.SetProgress(bodyPushProgress / 20.0f);
        }
    }

    public void PlaceBody(Corpse corpse)
    {
        hasBody = true;
        bodyPushProgress = 0;
        bar.SetActive(true);
        furnaceCorpse.SetActive(true);
        progressBar.SetProgress(0);
        flashingPrompt.StartFlash();
        furnaceCorpse.transform.position = corpseStartPos;
    }

     public void PushCorpse()
    {
        bodyPushProgress++;
        if (bodyPushProgress < 20)
        {
            progressBar.SetProgress(bodyPushProgress / 20.0f);
            Vector3 pos = corpseStartPos + furnaceCorpse.transform.up * bodyPushProgress / 20.0f * finalPushDistance;
            furnaceCorpse.transform.position = pos;
        }
        else
        {
            bar.SetActive(false);
            flashingPrompt.StopFlash();
            furnaceCorpse.SetActive(false);
            hasBody = false;
            // put burning code here
            score.AddToScore(2);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponentInParent<PlayerController>().HasPlagueCorpse())
        {
            placePrompt.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        placePrompt.SetActive(false);
    }
}
