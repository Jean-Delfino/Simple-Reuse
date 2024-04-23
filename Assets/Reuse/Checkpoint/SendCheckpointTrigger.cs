using Reuse.Utils;
using UnityEngine;

namespace Reuse.Checkpoint
{
    public class SendCheckpointTrigger : MonoBehaviour
    {
        [SerializeField] private string triggerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            GameObject player = null;

            if (!other.CompareTag("Player"))
            {
                player = UtilGameObject.FindParentWithTag(other.gameObject, triggerTag);
            }
            else
            {
                player = other.gameObject;
            }
            
            if(player != null)
            {

                player.GetComponent<TeleportTarget>().Teleport(CheckpointManager.GetLastCheckpoint());
                
                return;
            }
            
            var self = other.GetComponent<SelfCheckpointObject>();
            
            if(self != null)
            {
                self.ReturnToCheckpoint();
            }
        }
    }   
}