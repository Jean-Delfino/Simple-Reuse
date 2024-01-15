using UnityEngine;

namespace Reuse.Patterns
{
    public class MainNamedEventPublisher : MonoBehaviour
    {
        private static NamedEventPublisher instance;
        public static NamedEventPublisher Instance {
            get{
                if(instance == null) instance = FindObjectOfType<NamedEventPublisher>();

                return instance;
            }
        }


    }
}
