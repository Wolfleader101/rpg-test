using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public enum MovementState
{
    Idle,
    Walking,
    Sprinting,
    Dashing
}

public enum InteractionState
{
    None,
    Interacting, // looting, opening doors etc??
    Attacking,
}

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sprintSpeed = 15f;
    [SerializeField] private float dashSpeed = 75f;
    [SerializeField] private float dashTime = 0.1f;

    [SerializeField] private MovementState movementState = MovementState.Idle;
    [SerializeField] private InteractionState interactionState = InteractionState.None;


    private bool _isAttacking = false;
    private bool _isInteracting = false;

    private Rigidbody2D _rb;


    private Vector2 _moveDir;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        switch (movementState)
        {
            case MovementState.Idle:
                _rb.velocity = Vector2.zero;
                break;
            case MovementState.Walking:
                Move(_moveDir, walkSpeed);
                break;
            case MovementState.Sprinting:
                Move(_moveDir, sprintSpeed);
                break;
            case MovementState.Dashing:
                break;
            default:
                break;
        }
    }

    private IEnumerator Dash()
    {
        var prevMovementState = movementState;
        movementState = MovementState.Dashing;
        
        _rb.velocity = Vector2.zero;
        _rb.AddRelativeForce(_moveDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        
        movementState = prevMovementState;
    }

    private void Move(Vector2 dir, float speed)
    {
        _rb.velocity = dir * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.started) _moveDir = context.ReadValue<Vector2>();

        if (context.interaction is MultiTapInteraction)
        {
            if (!context.performed) return;
            
            StartCoroutine(Dash());
            //movementState = context.performed ? MovementState.Dashing : MovementState.Idle;
        }
        else
        {

            if (movementState == MovementState.Dashing) return;
           
            movementState = context.performed ? MovementState.Walking : MovementState.Idle;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        movementState = context.started ? MovementState.Sprinting : movementState;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        _isAttacking = context.started;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _isInteracting = context.started;
    }
}