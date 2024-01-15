using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class SimpleLocalFollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
   
        private void Update()
        {
            transform.localPosition = followTarget.position;
        }

        public void SetFollowTarget(Transform target)
        {
            followTarget = target;
        }
    }
}
