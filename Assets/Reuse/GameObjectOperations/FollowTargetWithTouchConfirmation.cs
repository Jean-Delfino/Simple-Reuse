using System;
using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class FollowTargetWithTouchConfirmation : FollowTarget
    {
        [SerializeField] private float followDistance = 3.0f;

        public Action TouchedTarget;

        private void Update()
        {
            if(target == null) return;
            
            if(MoveTowardsTarget(followDistance)) TouchedTarget?.Invoke();
        }
    }
}