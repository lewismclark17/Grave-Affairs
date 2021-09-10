using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCoffin : MonoBehaviour
{
    public GameObject priestBody, nobleBody, kingBody;

    public GameObject redBook, blueBook, greenBook;
        
    public bool hasItem;

    public bool hasBody;

    public bool hasBeenClosed;

    public BoatSpawner boatSpawner;

    public GameObject speechBubble, priestPic, redBookPic, buttonPrompt;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        speechBubble.SetActive(false);
        priestPic.SetActive(false);
        redBookPic.SetActive(false);
        buttonPrompt.SetActive(false);
    }

    public void PlaceBody(Corpse corpse)
    {
        hasBody = true;
        priestPic.SetActive(false);
        redBookPic.SetActive(true);
        FindObjectsOfType<BookPile>()[0].Highlight();

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

    public void PlaceItem(Item item)
    {
        hasItem = true;
        speechBubble.SetActive(false);
        priestPic.SetActive(false);
        redBookPic.SetActive(false);
        buttonPrompt.SetActive(true);
        FindObjectsOfType<BookPile>()[0].StopHighlight();

        if (item.itemType == Item.ItemType.RedBook)
        {
            redBook.SetActive(true);
        }
        else if (item.itemType == Item.ItemType.BlueBook)
        {
            blueBook.SetActive(true);
        }
        else if (item.itemType == Item.ItemType.GreenBook)
        {
            greenBook.SetActive(true);
        }  
    }

    public void CloseLid()
    {
        Leave();
        boatSpawner.ScorePoints();
        hasBeenClosed = true;
        buttonPrompt.SetActive(false);
    }

    void Leave()
    { 
        animator.SetTrigger("LidClosed");
        Debug.Log("boat should leave now");
    }

    void RemoveBoat()
    {
        boatSpawner.OnBoatDestroyed();
        Destroy(gameObject);
    } 

    public void EnableUI()
    {
        speechBubble.SetActive(true);
        priestPic.SetActive(true);
    }  
}
