using UnityEngine;

namespace Reuse.Movement
{
    public class SimpleJumpingCoyoteTime : MonoBehaviour
    {
        [SerializeField] private float coyoteTime = 0.25f;
        private float _actualCoyoteTIme = 0;
        
        public void SaveUngroundedMoment()
        {
            _actualCoyoteTIme = coyoteTime;
        }

        public void SaveGroundedMovement()
        {
            if (_actualCoyoteTIme > 0) _actualCoyoteTIme = 0;
        }
        
        public void DecreaseCoyoteTime(float timeDelta)
        {
            if(_actualCoyoteTIme <= 0) return;
            
            _actualCoyoteTIme -= timeDelta;
        }

        public bool CheckIfCanJump()
        {
            return _actualCoyoteTIme > 0;
        }
    }
}
