using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    public Transform Interact;
    public Rigidbody dragspot;
    public Material burnt;
    public string actualBodyName;
    public bool canBeIncinerated, canBeBuried, canBePrepared, isClean, isDealtWith;
    public enum CorpseType {None, King, Noble, Priest};
    public CorpseType corpseType;
    public static Cart corpseCart;
    public float timeSinceLastInteracted;
    public float deactivateDelay;

    /*void FixedUpdate()
    {
        dragspot.MovePosition(dragspot.position + Vector3.up * Time.deltaTime);
    }*/

    void Start()
    {
        timeSinceLastInteracted = 0f;
        //DeactivateRagdoll();
    }

    void Update()
    {
        timeSinceLastInteracted += Time.deltaTime;
        if (timeSinceLastInteracted > deactivateDelay)
        {
            DeactivateRagdoll();
        }
    }

    public void Burn()
    {
        GameObject actualBody = transform.Find(actualBodyName).gameObject;
        actualBody.GetComponent<Renderer>().material = burnt;
        StartCoroutine(DestroyAfterDelay());
    }

    public bool IsBeingCarried()
    {
        return dragspot.GetComponent<Rigidbody>().isKinematic;
    }

    public void DropCorpse()
    {
        StartCoroutine(DropAfterDelay(0.1f));
    }

    public static int GetNumValidCorpses()
    {
        Corpse[] corpses = FindObjectsOfType<Corpse>(false);

        int numOfCorpses = 0;

        foreach (Corpse corpse in corpses)
        {
            if (!corpse.isDealtWith)
            {
                numOfCorpses++;
            }
        }
        return numOfCorpses;
    }

    public void SummonCorpseCartIfNeeded()
    {
        if (GetNumValidCorpses() <= 3)
        {
            corpseCart.ActivateNextWave();
        }
    }

    public void ActivateRagdoll()
    {
        timeSinceLastInteracted = 0f;
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
    }

    public void DeactivateRagdoll()
    {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
    }

    IEnumerator DropAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (dragspot != null)
        {
            dragspot.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        isDealtWith = true;
        SummonCorpseCartIfNeeded();
        Destroy(gameObject);
    }
}
