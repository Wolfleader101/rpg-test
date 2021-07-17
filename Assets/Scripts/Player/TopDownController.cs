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
    Attacking, // 
}

// eventually flesh this out more with scriptable objects
public enum AttackType
{
    Melee,
    Magic,
    Gun
}

[RequireComponent(typeof(Rigidbody2D), typeof(Stats))]
public class TopDownController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sprintSpeed = 15f;
    [SerializeField] private float dashSpeed = 75f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;

    [SerializeField] private MovementState movementState = MovementState.Idle;
    [SerializeField] private InteractionState interactionState = InteractionState.None;


    private bool _isAttacking = false;
    private bool _isInteracting = false;

    private Rigidbody2D _rb;
    private Stats _stats;

    private float _dashCooldownTime = 0f;
    private Vector2 _moveDir = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<Stats>();
        
        _dashCooldownTime = Time.time + dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        switch (interactionState)
        {
            case InteractionState.None:
                break;
            case InteractionState.Interacting:
                break;
            case InteractionState.Attacking:
                break;
        }
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
                StartCoroutine(Dash());
                break;
        }
    }

    private IEnumerator Dash()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddRelativeForce(_moveDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);

        movementState = MovementState.Idle;
    }

    private void Move(Vector2 dir, float speed)
    {
        _rb.velocity = dir * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started) _moveDir = context.ReadValue<Vector2>();

        if (context.interaction is MultiTapInteraction)
        {
            if (!context.performed) return;

            if (_dashCooldownTime <= Time.time)
            {
                _dashCooldownTime = Time.time + dashCooldown;
                movementState = context.performed ? MovementState.Dashing : MovementState.Idle;
            }
        }
        else
        {
            if (movementState == MovementState.Dashing) return;

            movementState = context.performed ? MovementState.Walking : MovementState.Idle;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (movementState == MovementState.Walking || movementState == MovementState.Sprinting)
            movementState = context.performed ? MovementState.Sprinting : MovementState.Walking;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (interactionState == InteractionState.None || interactionState == InteractionState.Attacking)
        {
            interactionState = context.performed ? InteractionState.Attacking : InteractionState.None;
                //_stats.StaminaBuff =- 2.5f;
        }
            
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
    }
}