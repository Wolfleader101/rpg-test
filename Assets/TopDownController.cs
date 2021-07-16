using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float dashSpeed = 75f;
    [SerializeField] private float dashTime = 0.75f;

    [Serializable]
    private enum MovementState
    {
        
    }
    
    
    [Serializable]
    private enum AttackState
    {
        
    }


    private bool _isAttacking = false;
    private bool _isInteracting = false;
    private bool _isDashing = false;
    private bool _isSprinting = false;


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
        if (_isDashing)
        {
            StartCoroutine(Dash());
        }
        else
        {
            _rb.velocity = _moveDir * speed;
        }
    }

    private IEnumerator Dash()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddRelativeForce(_dashDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        _rb.velocity = Vector2.zero;
        _isDashing = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started) _dashDir = context.ReadValue<Vector2>();

        _isDashing = context.performed;
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