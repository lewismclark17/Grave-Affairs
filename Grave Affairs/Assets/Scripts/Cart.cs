using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cart : MonoBehaviour
{
    public GameObject cartCorpses;

    public GameObject priestRef, soldierRef, plagueRef;

    public float sec = 5f;

    public float rowSpacing, colSpacing, verSpacing;
    
    public int rowSize, colSize;

    float timeOfLastWave;

    public float timeBetweenWaves;
    Animator animator;

    Vector3 CartCorpsesOriginalPosition;

    Quaternion CartCorpsesOriginalRotation;

    void Start()
    {
        CartCorpsesOriginalPosition = cartCorpses.transform.position;
        CartCorpsesOriginalRotation = cartCorpses.transform.rotation;
        GenerateCorpses(2, 3, 3, 5, 1, 2);
        Corpse.corpseCart = this;
        animator = GetComponentInParent<Animator>();
        timeOfLastWave = Time.time;
    }

    void Update()
    {
        if (Time.time - timeOfLastWave > timeBetweenWaves)
        {
            ActivateNextWave();
        }
    }

    public void CorpseMagic()
    {
        cartCorpses.transform.parent = null;       
        Debug.Log("corpse magic TADA");
    }

    void GenerateCorpses(int minNumSoldiers, int maxNumSoldiers, int minNumPlagued, int maxNumPlagued, int minNumPriests, int maxNumPriests)
    {
        int numSoldiers = Random.Range(minNumSoldiers, maxNumSoldiers + 1);
        int numPlagued = Random.Range(minNumPlagued, maxNumPlagued + 1);
        int numPriests = Random.Range(minNumPriests, maxNumPriests + 1);

        List<GameObject> corpses = new List<GameObject>();
        for (int i = 0; i < numSoldiers; i++)
        { 
            corpses.Add(Instantiate(soldierRef, cartCorpses.transform));     
        }
        for (int i = 0; i < numPlagued; i++)
        {
            corpses.Add(Instantiate(plagueRef, cartCorpses.transform));     
        }
        for (int i = 0; i < numPriests; i++)
        {
            corpses.Add(Instantiate(priestRef, cartCorpses.transform));     
        }
        corpses = corpses.OrderBy(a => Random.value).ToList();
        for (int i = 0; i < corpses.Count; i++)
        {
            corpses[i].transform.localPosition = new Vector3(i%rowSize*rowSpacing, i/(rowSize*colSize)*verSpacing, i/rowSize%colSize*colSpacing);
        }
    }

    public void ActivateNextWave()
    {
        //Debug.Log("activatenextwave called");
        timeOfLastWave = Time.time;
        animator.SetTrigger("NeedMoreCorpses");
        cartCorpses.transform.DetachChildren();
        cartCorpses.transform.position = CartCorpsesOriginalPosition;
        cartCorpses.transform.rotation = CartCorpsesOriginalRotation;
        cartCorpses.transform.parent = transform;
        GenerateCorpses(2, 3, 3, 5, 1, 2);
    }
}
