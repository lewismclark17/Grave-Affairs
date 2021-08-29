using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    public Transform Interact;
    public Rigidbody dragspot;
    public Material burnt;
    public string actualBodyName;
    public bool canBeIncinerated, canBeBuried, canBePrepared;
    public enum CorpseType {None, King, Noble, Priest};
    public CorpseType corpseType;

    /*void FixedUpdate()
    {
        dragspot.MovePosition(dragspot.position + Vector3.up * Time.deltaTime);
    }*/
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
        Destroy(gameObject);
    }
}
