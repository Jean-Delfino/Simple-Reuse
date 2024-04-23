using UnityEngine;

namespace Reuse.Checkpoint
{
    public class SimpleTeleportTarget : TeleportTarget
    {
        public override void Teleport(Vector3 teleportPosition)
        {
            this.transform.position = teleportPosition;
        }
    }
}