using System;
using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        
        [Header("TRAVEL VARIABLE")]
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private float rotationSpeed = 5.0f;

        private void Update()
        {
            if(target == null) return;
            MoveTowardsTarget();
        }

        protected bool MoveTowardsTarget(float followDistance = 0.0f)
        {
            var position = transform.position;
            var direction = target.position - position;
            var distance = direction.magnitude;

            if (distance > followDistance)
            {
                Vector3.MoveTowards(position, target.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                return false;
            }

            return true;
        }
    }
}