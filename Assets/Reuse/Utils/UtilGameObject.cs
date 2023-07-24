using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilGameObject
    {
        public static void SetActiveStateOfAllTransformChildren(Transform father, bool state)
        {
            foreach (Transform child in father)
            {
                child.gameObject.SetActive(state);
            }
        }
    }
}