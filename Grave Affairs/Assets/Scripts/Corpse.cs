using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    public Transform Interact;

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
}
