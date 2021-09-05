using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cart : MonoBehaviour
{
    public GameObject cartCorpses;

    public GameObject priestRef, soldierRef, plagueRef;

    public float sec = 5.5f;

    public float rowSpacing, colSpacing, verSpacing;
    
    public int rowSize, colSize;

    void Start()
    {
        StartCoroutine(CorpseMagic());
        GenerateCorpses(6, 8, 10, 12, 1, 3);
    }

    IEnumerator CorpseMagic()
    {
        yield return new WaitForSeconds(sec);
        cartCorpses.transform.parent = null;
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
}
