using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    public Transform Interact;
    public Rigidbody dragspot;

    /*void FixedUpdate()
    {
        dragspot.MovePosition(dragspot.position + Vector3.up * Time.deltaTime);
    }*/

    public void OnPress()
    {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = Interact.position;
        this.transform.parent = GameObject.Find("Interactor").transform;

    }

    public void OnRelease()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void DropCorpse()
    {
        StartCoroutine(DropAfterDelay(0.1f));
    }

    IEnumerator DropAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dragspot.GetComponent<Rigidbody>().isKinematic = false;
    }
}
