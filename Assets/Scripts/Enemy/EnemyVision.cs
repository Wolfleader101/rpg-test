using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyVision : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float visionConeArc = 60f;
        [SerializeField] private float lineOfSightDistance = 100f;

        #region Callbacks

        public event Action<Vector2> ONVisible;

        #endregion


        private bool _hasLos = false;
        private bool _checkThisFrame = false;

        void Start()
        {
            _checkThisFrame = (Random.Range(0, 100) % 2) == 0;

            if (target == null)
            {
                // this should always evaluate
                target = GameObject.FindWithTag("Player");
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 targetPos = target.transform.position;

            Debug.DrawRay(transform.position, transform.up, Color.red);
            if (InVisionCone(targetPos) && HasLos(targetPos))
            {
                ONVisible?.Invoke(targetPos);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
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

            var position = transform.position;
            var rayDir = point - position;
            
            RaycastHit2D hit = Physics2D.Raycast(position, rayDir, lineOfSightDistance);
            Debug.DrawRay(position, rayDir, Color.cyan);

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


    }
}