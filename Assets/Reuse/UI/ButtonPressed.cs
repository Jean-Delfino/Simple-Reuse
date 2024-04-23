using UnityEngine;
using UnityEngine.EventSystems;

namespace Reuse.UI
{
    //https://www.youtube.com/watch?v=03mCu-l8u28&ab_channel=MuhammadShahzaib
    public class ButtonPressed : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        private bool _buttonPressed;

        public bool Pressed => _buttonPressed;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _buttonPressed = true;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _buttonPressed = false;
        }

    }
}