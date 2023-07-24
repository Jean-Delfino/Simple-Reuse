using UnityEngine;
using Reuse.CameraControl;

namespace Reuse.Utils
{
    public static class UtilMouse
    {
        public static Vector3 GetMousePos(Vector3 position)
        {
            return CameraController.GetMainCamera().WorldToScreenPoint(position);
        }
    }
}
