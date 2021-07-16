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
    [SerializeField] private float speed = 10f;
    [SerializeField] private float dashSpeed = 75f;
    [SerializeField] private float dashTime = 0.1f;

    [SerializeField] private MovementState movementState = MovementState.Idle;
    [SerializeField] private InteractionState interactionState = InteractionState.None;


    private bool _isAttacking = false;
    private bool _isInteracting = false;

    private Rigidbody2D _rb;


    private Vector2 _moveDir;
    private Vector2 _dashDir;

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
                Debug.Log("hello");
                _rb.velocity = _moveDir * speed;
                break;
            case MovementState.Sprinting:
                break;
            case MovementState.Dashing:
                StartCoroutine(Dash());
                break;
            default:
                break;
        }
    }

    private IEnumerator Dash()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddRelativeForce(_dashDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);

        movementState = MovementState.Idle;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector2>();
        movementState = context.performed ? MovementState.Walking : MovementState.Idle;
        Debug.LogWarning(movementState);
        
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started) _dashDir = context.ReadValue<Vector2>();
        
        movementState = context.performed ? MovementState.Dashing : MovementState.Idle;
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