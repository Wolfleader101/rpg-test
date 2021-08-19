using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyVision))]
    public class EnemyRotate : MonoBehaviour
    {
        [SerializeField] private EnemyVision enemyVision;
        [SerializeField] private float turnSpeed = 100f;


        private void Start()
        {
            if (enemyVision == null)
            {
                enemyVision = gameObject.GetComponent<EnemyVision>();
            }

            enemyVision.ONVisible += OnVisble;


        }

        private void OnVisble(Vector2 targetPos)
        {
            FacePoint(targetPos);
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
}