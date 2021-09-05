using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public BoatCoffin boatman;
    bool boatExists;
    int boatsNeeded;
    public Score score;

    void SpawnBoatman()
    {
        BoatCoffin boat = Instantiate(boatman, transform.position, transform.rotation, transform.parent);
        boat.boatSpawner = this;
        boatExists = true;
    }

    void Start()
    {
        //SpawnBoatman();
    }

    public void RequestBoat()
    {
        if (boatExists)
        {
            boatsNeeded++;
        }
        else
        {
            SpawnBoatman();
        }
    }

    public void OnBoatDestroyed()
    {
        boatExists = false;
        if (boatsNeeded > 0)
        {
            SpawnBoatman();
            boatsNeeded--;
        }
    }

    public void ScorePoints()
    {
        score.AddToScore(5);
    }
}

