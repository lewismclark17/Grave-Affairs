using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Vector3 moveVec, forward, right, heading;

    Vector2 move;
    
    public float speed = 5.0f;

    public static int numberOfPlayers;

    public GameObject interactor, dragspot, throwspot;

    public GameObject variant1, variant2, variant3, variant4;

    //public GameObject pauseMenu;

    //public bool isPaused;

    Corpse carriedCorpse = null;

    [SerializeField] TMPro.TextMeshProUGUI instructions;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        numberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        SetVariant(numberOfPlayers);
        //pauseMenu.SetActive(false);
        //isPaused = false;
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
            } 
        }
        else
        {
            carriedCorpse.dragspot.MovePosition(throwspot.transform.position);
            carriedCorpse.DropCorpse();
            carriedCorpse = null;
        }
    }

    public void OnAltInteract()
    {
        PrepTable prepTable = GetPrepTable();
        BoatCoffin boat = GetBoatCoffin();

        if (prepTable != null)
        {
            if (prepTable.hasBody && !carriedCorpse)
            {
                carriedCorpse = prepTable.GetCleanBody();
                carriedCorpse.gameObject.SetActive(true);
                carriedCorpse.dragspot.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (!prepTable.hasBody && carriedCorpse && carriedCorpse.canBePrepared)
            {
                prepTable.PlaceBody(carriedCorpse);
                carriedCorpse.gameObject.SetActive(false);
                carriedCorpse = null;
            }
        }

        if (boat != null)
        {
            if (!boat.hasBody && carriedCorpse)
            {
                boat.PlaceBody(carriedCorpse);
                carriedCorpse.gameObject.SetActive(false);
                carriedCorpse = null;
            }
        }
    }

    PrepTable GetPrepTable()
    {
        Collider[] hitcolliders = Physics.OverlapBox(
            interactor.transform.position, 
            interactor.transform.localScale / 2, 
            interactor.transform.rotation);
        
        foreach (Collider col in hitcolliders)
        {
            PrepTable target = col.gameObject.GetComponentInParent<PrepTable>(); 
            if (target != null)
            {
                return target;
            }
        }
        return null;
    }

    BoatCoffin GetBoatCoffin()
    {
        Collider[] hitcolliders = Physics.OverlapBox(
            interactor.transform.position, 
            interactor.transform.localScale / 2, 
            interactor.transform.rotation);
        
        foreach (Collider col in hitcolliders)
        {
            BoatCoffin target = col.gameObject.GetComponentInParent<BoatCoffin>(); 
            if (target != null)
            {
                return target;
            }
        }
        return null;
    }

    /*public void onPause()
    {
        if (isPaused == false)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    } 

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false); 
    }*/ 

    public void FixedUpdate() 
    {
        transform.position += heading * speed * Time.fixedDeltaTime;
    }

    void SetVariant(int variant)
    {
        if (variant == 1)
        {
            variant1.SetActive(true);
        }
        else if (variant == 2)
        {
            variant2.SetActive(true);
            gameObject.transform.position += Vector3.back*3;
        }
        else if (variant == 3)
        {
            variant3.SetActive(true);
            gameObject.transform.position += Vector3.back*6;
        }
        else if (variant == 4)
        {
            variant4.SetActive(true);
            gameObject.transform.position += Vector3.back*9;
        }
    }
}
