using UnityEngine;

namespace Reuse.Patterns
{
    public class NamedEventColliderTrigger : MonoBehaviour
    {
        [SerializeField] private string eventName;
        [SerializeField] private NamedEventPublisher publisher;
        [SerializeField] private string triggerTag = "Player";
        private void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag(triggerTag))
            {
                publisher.SendEvent(eventName);
            }
        }
    }
}
