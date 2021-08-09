using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VisionAgent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float visionConeArc;
    [SerializeField] private float lineOfSightDistance = 100f;
    [SerializeField] private float turnSpeed = 100f;

    private bool _hasLos = false;

    private bool _checkThisFrame = false;

    void Start()
    {
        _checkThisFrame = (Random.Range(0, 100) % 2) == 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = target.transform.position;

        
        Debug.DrawRay(transform.position, transform.up, Color.red);
        if (InVisionCone(targetPos) && HasLos(targetPos))
        {
            FacePoint(targetPos);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private bool InVisionCone(Vector2 point)
    {
        Vector2 fwd = transform.up;
        Vector2 ourPos = transform.position;
        Vector2 dirToPoint = point - ourPos;

        float angle = Vector2.Angle(dirToPoint, fwd);

        return angle <= visionConeArc / 2.0;
    }

    private bool HasLos(Vector3 point)
    {
        _checkThisFrame = !_checkThisFrame;

        if (!_checkThisFrame) return _hasLos;

        var rayPoint = transform.InverseTransformPoint(point);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, point - transform.position, lineOfSightDistance);
        Debug.DrawRay(transform.position, point - transform.position, Color.cyan);

        if (hit.collider != null && (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            _hasLos = true;
        }
        else
        {
            _hasLos = false;
        }

        return _hasLos;
    }

    private void FacePoint(Vector3 point)
    {
        var localPoint = transform.InverseTransformPoint(point);
        var turnDir = Mathf.Sign(localPoint.x);
        var turnAmount = (turnSpeed * Time.deltaTime);
        var angle = Vector3.Angle(Vector3.forward, localPoint);

        if (angle < turnAmount)
        {
            turnAmount = angle;
        }

        transform.Rotate(Vector3.forward, -turnAmount * turnDir);
    }
}