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

    public float deadZone = 0.1f;

    public static int numberOfPlayers;

    public GameObject interactor, dragspot, throwspot, itemholdspot;

    public GameObject variant1, variant2, variant3, variant4;

    public TutorialBubbles tutorialBubbles;

    Animator animator;

    //public GameObject pauseMenu;

    //public bool isPaused;

    Corpse carriedCorpse = null;

    Item carriedItem = null;

    [SerializeField] TMPro.TextMeshProUGUI instructions;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        numberOfPlayers = GameObject.FindObjectsOfType<PlayerController>().Length;
        SetVariant(numberOfPlayers);
        
        //pauseMenu.SetActive(false);
        //isPaused = false;
    }

    void Update()
    {
        if (carriedCorpse)
        {
            carriedCorpse.dragspot.MovePosition(dragspot.transform.position);
            carriedCorpse.timeSinceLastInteracted = 0f;
        }

        if (carriedItem)
        {
            carriedItem.GetComponent<Rigidbody>().MovePosition(itemholdspot.transform.position);
            carriedItem.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, -90);
        }
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        //move = inputVec;

        move = new Vector3( 
            (Mathf.Abs(inputVec.x) > deadZone)?inputVec.x:0.0f,  
            (Mathf.Abs(inputVec.y) > deadZone)?inputVec.y:0.0f,
            0) // 360 controller fix
        ;

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

    Item GetItem()
    {
        Collider[] hitcolliders = Physics.OverlapBox(
            interactor.transform.position, 
            interactor.transform.localScale / 2, 
            interactor.transform.rotation);
        
        foreach (Collider col in hitcolliders)
        {
            Item target = col.gameObject.GetComponentInParent<Item>(); 
            if (target != null)
            {
                return target;
            }
        }
        return null;
    }

    public void OnInteract()
    {
        BoatCoffin boat = GetBoatCoffin(); 

        if (carriedCorpse == null)
        {
            Corpse corpse = GetCorpse();
            if (corpse != null)
            {
                corpse.ActivateRagdoll();
                carriedCorpse = corpse;
                carriedCorpse.dragspot.GetComponent<Rigidbody>().isKinematic = true;
                if (carriedItem)
                {
                    carriedItem.GetComponent<Rigidbody>().MovePosition(throwspot.transform.position);
                    carriedItem.GetComponent<Rigidbody>().isKinematic = false;
                    carriedItem = null;
                }
            }
        }
        else
        {
            carriedCorpse.dragspot.MovePosition(throwspot.transform.position);
            carriedCorpse.DropCorpse();
            carriedCorpse = null;
        }

        if (carriedItem == null)
        {
            Item item = GetItem();
            if (item != null)
            {
                Debug.Log("ITEM" + item.name);
                carriedItem = item;
                carriedItem.GetComponent<Rigidbody>().isKinematic = true;
                if (carriedCorpse)
                {
                    carriedCorpse.dragspot.MovePosition(throwspot.transform.position);
                    carriedCorpse.DropCorpse();
                    carriedCorpse = null;
                }
            } 
        }
        else
        {      
            carriedItem.GetComponent<Rigidbody>().MovePosition(throwspot.transform.position);
            carriedItem.GetComponent<Rigidbody>().isKinematic = false;
            carriedItem = null;

        }  
    }

    public void OnAltInteract()
    {
        PrepTable prepTable = GetPrepTable();
        BoatCoffin boat = GetBoatCoffin();
        BookPile bookPile = GetBookPile();
        Furnace furnace = GetFurnace();

        if (prepTable != null)
        {
            if (prepTable.hasBody && !carriedCorpse)
            {
                carriedCorpse = prepTable.GetCleanBody();
                if (carriedCorpse != null)
                {
                    carriedCorpse.gameObject.SetActive(true);
                    carriedCorpse.dragspot.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            else if (!prepTable.hasBody && carriedCorpse && carriedCorpse.canBePrepared && !carriedCorpse.isClean)
            {
                prepTable.PlaceBody(carriedCorpse);
                carriedCorpse.gameObject.SetActive(false);
                carriedCorpse = null;
            }
        }

        if (furnace != null)
        {
            if (carriedCorpse && carriedCorpse.canBeIncinerated)
            {
                furnace.PlaceBody(carriedCorpse);
                carriedCorpse.gameObject.SetActive(false);
                carriedCorpse = null;
            }
        }

        if (bookPile != null)
        {
            if (!carriedCorpse && !carriedItem)
            {
                carriedItem = bookPile.GetBook();
                carriedItem.GetComponent<Rigidbody>().isKinematic = true;

            }
        }

        if (boat != null)
        {
            if (!boat.hasBody && carriedCorpse)
            {
                boat.PlaceBody(carriedCorpse);
                carriedCorpse.gameObject.SetActive(false);
                carriedCorpse.SummonCorpseCartIfNeeded();
                carriedCorpse = null;
            }
            else if (boat.hasBody && !boat.hasItem && carriedItem) 
            {
                boat.PlaceItem(carriedItem);
                carriedItem.gameObject.SetActive(false);
                carriedItem = null;
            }
            else if (boat.hasBody && boat.hasItem && !boat.hasBeenClosed)
            {
                boat.CloseLid();
            }
        }
    }

    public void OnMinigameButton()
    {
        Furnace furnace = GetFurnace();
        PrepTable prepTable = GetPrepTable();
        if (prepTable && prepTable.hasBody)
        {
            prepTable.CleanCorpse();
        }
        else if (furnace && furnace.hasBody)
        {
            furnace.PushCorpse();
        }
    }

    public void OnDebugUp()
    {
        foreach (Corpse corpse in FindObjectsOfType<Corpse>())
        {
            corpse.ActivateRagdoll();
        }
        Debug.Log("it activated");
    }

    public void OnDebugDown()
    {
        foreach (Corpse corpse in FindObjectsOfType<Corpse>())
        {
            corpse.DeactivateRagdoll();
        }
        Debug.Log("it deactivated");
    }

    BookPile GetBookPile()
    {
        Collider[] hitcolliders = Physics.OverlapBox(
            interactor.transform.position, 
            interactor.transform.localScale / 2, 
            interactor.transform.rotation);
        
        foreach (Collider col in hitcolliders)
        {
            BookPile target = col.gameObject.GetComponentInParent<BookPile>(); 
            if (target != null)
            {
                return target;
            }
        }
        return null;
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

    Furnace GetFurnace()
    {
        Collider[] hitcolliders = Physics.OverlapBox(
            interactor.transform.position, 
            interactor.transform.localScale / 2, 
            interactor.transform.rotation);
        
        foreach (Collider col in hitcolliders)
        {
            Furnace target = col.gameObject.GetComponentInParent<Furnace>(); 
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
        //animator.SetBool("IsMoving", false);
        if (heading != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void SetVariant(int variant)
    {
        if (variant == 1)
        {
            variant1.SetActive(true);
            animator = variant1.GetComponent<Animator>();
        }
        else if (variant == 2)
        {
            variant2.SetActive(true);
            gameObject.transform.position += Vector3.back*3;
            animator = variant2.GetComponent<Animator>();
        }
        else if (variant == 3)
        {
            variant3.SetActive(true);
            gameObject.transform.position += Vector3.back*6;
            animator = variant3.GetComponent<Animator>();
        }
        else if (variant == 4)
        {
            variant4.SetActive(true);
            gameObject.transform.position += Vector3.back*9;
            animator = variant4.GetComponent<Animator>();
        }
    }

    public bool HasDirtyCorpse()
    {
        return carriedCorpse != null && carriedCorpse.canBePrepared && !carriedCorpse.isClean;
    }

    public bool HasSoldierCorpse()
    {
        return carriedCorpse != null && carriedCorpse.canBeBuried;
    }

    public bool HasPlagueCorpse()
    {
        return carriedCorpse != null && carriedCorpse.canBeIncinerated;
    }
}
