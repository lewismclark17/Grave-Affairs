using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 moveVec, forward, right, heading;

    Vector2 move;
    
    public float speed = 5.0f;

    public GameObject interactor, dragspot;

    Corpse carriedCorpse = null;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Update()
    {
        if (carriedCorpse)
        {
            carriedCorpse.dragspot.MovePosition(dragspot.transform.position);
        }
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        move = inputVec;

        moveVec = new Vector3(inputVec.x, 0, inputVec.y);

        Vector3 direction = new Vector3(move.x, 0, move.y) * speed * Time.deltaTime;
        Vector3 rightMovement = right * speed * Time.deltaTime * move.x;
        Vector3 upMovement = forward * speed * Time.deltaTime * move.y;

        heading = Vector3.Normalize(rightMovement + upMovement);

        
        if(move.magnitude > 0.6f) 
        {
            
            transform.rotation = Quaternion.LookRotation(heading, Vector3.up);
        
            //float angle = Vector3.SignedAngle(forward, heading, Vector3.up);
        }

        //transform.position += rightMovement;
        //transform.position += upMovement;
        
    }

    Corpse GetCorpse()
    {
        Collider[] hitcolliders = Physics.OverlapBox(
            interactor.transform.position, 
            interactor.transform.localScale / 2, 
            interactor.transform.rotation);
        
        foreach (Collider col in hitcolliders)
        {
            Corpse target = col.gameObject.GetComponentInParent<Corpse>(); 
            if (target != null)
            {
                return target;
            }
        }
        return null;
    }

    public void OnInteract()
    {
        if (carriedCorpse == null)
        {
            Corpse corpse = GetCorpse();
            if (corpse != null)
            {
                Debug.Log("CORPSE" + corpse.name);
                carriedCorpse = corpse;
                carriedCorpse.dragspot.GetComponent<Rigidbody>().isKinematic = true;
                /*SpringJoint sj = dragspot.AddComponent<SpringJoint>();
                sj.connectedBody = corpse.dragspot;
                /*Rigidbody rigy = corpse.GetComponent<Rigidbody>();
                rigy.useGravity = false;
                rigy.freezeRotation = true;
                rigy.isKinematic = true;
                corpse.transform.position = dragspot.transform.position;
                corpse.transform.parent = dragspot.transform;*/
            } 
        }
        else
        {
            /*Rigidbody rigy = carriedCorpse.GetComponent<Rigidbody>();
            rigy.useGravity = true;
            rigy.freezeRotation = false;
            rigy.isKinematic = false;
            carriedCorpse.transform.parent = null;*/
            carriedCorpse.dragspot.GetComponent<Rigidbody>().isKinematic = false;
            carriedCorpse = null;
        }
    }
    

    public void FixedUpdate() 
    {
        transform.position += heading * speed * Time.fixedDeltaTime;
    }
}
