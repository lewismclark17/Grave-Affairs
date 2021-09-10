using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBubbles : MonoBehaviour
{
    public GameObject plagueBubble;
    public GameObject soldierBubble;
    PlayerController[] players = null;

    void Start()
    {
        Debug.Log("test");
        plagueBubble.SetActive(false);
        soldierBubble.SetActive(false);
    }

    public void GetPlayers()
    {
        players = FindObjectsOfType<PlayerController>();
    }

    void Update()
    {
        if (players != null)
        {
            if (PlayerCarryingSoldier())
            {
                soldierBubble.SetActive(true);
            }
            else
            {
                soldierBubble.SetActive(false);
            }

            if (PlayerCarryingPlague())
            {
                plagueBubble.SetActive(true);
            }
            else
            {
                plagueBubble.SetActive(false);
            }
        }
    }

    bool PlayerCarryingSoldier()
    {
        foreach (PlayerController player in players)
        {
            if (player.HasSoldierCorpse())
            {
                return true;
            }
        }
        return false;
    }

    bool PlayerCarryingPlague()
    {
        foreach (PlayerController player in players)
        {
            if (player.HasPlagueCorpse())
            {
                return true;
            }
        }
        return false;
    }
}
