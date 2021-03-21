using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 12f;

    PlayerControls controls;

    Vector2 move;

    Vector3 forward, right;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Enable();
        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => move = Vector3.zero;
    }
 
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

    }

    void Update()
    {
        if (move != Vector2.zero)
            OnMove();
    }

    public void OnMove()
    {
        Vector3 direction = new Vector3(move.x, 0, move.y) * moveSpeed * Time.deltaTime;
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * move.x;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * move.y;

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        if(move.magnitude > 0.6f) 
        {
            transform.rotation = Quaternion.LookRotation(heading, Vector3.up);
            float angle = Vector3.SignedAngle(forward, heading, Vector3.up);
        }

        transform.position += rightMovement;
        transform.position += upMovement;
    }

    public void Join()
    {
    }
}
