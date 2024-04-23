using UnityEngine;

namespace Reuse.Movement
{
    public class Simple2DMovement : MonoBehaviour
    {
        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private float maxRunningTimeForFlip;
        [SerializeField] private float maxRunningTime = 3;
        [SerializeField] private float deAccelerationRatio = 2f;
        [SerializeField] private float finalSpeedMultiplier = 1.45f;
        
        private float _runningTime;
        private float _actualSpeed = 0;
        private int _runningDirection = 0;
        private float _actualFlipTime = 0;

        public bool deac;
        
        //Returns if is should make the flip animation
        public void ProcessMovement(Rigidbody character, float deltaTime, int runningDirection, bool canAccelerate, bool freezeMovement, bool nullifyMovement, bool deAcceleration)
        {
            FindSpeed(deltaTime, runningDirection, canAccelerate, freezeMovement, nullifyMovement, deAcceleration);
            MoveCharacter(character);
        }

        public void ProcessMovement(Rigidbody2D character, float deltaTime, int runningDirection, bool canAccelerate,
            bool freezeMovement, bool nullifyMovement, bool deAcceleration)
        {
            FindSpeed(deltaTime, runningDirection, canAccelerate, freezeMovement, nullifyMovement, deAcceleration);
            MoveCharacter(character);
        }

        private void FindSpeed(float deltaTime, int runningDirection, bool canAccelerate, bool freezeMovement, bool nullifyMovement, bool deAcceleration)
        {
            if (nullifyMovement)
            {
                _runningTime = 0;
                _actualSpeed = movementCurve.Evaluate(_runningTime);
                return;
            } 
            
            if (!freezeMovement && canAccelerate)
            {
                ProcessSpeed(deAcceleration, deltaTime, runningDirection);
            }
        }
        
        public bool ProcessFlipping(Rigidbody character, float deltaTime, int runningDirection, bool canFlipEvenWithHighSpeed, bool nullifyOnFlipAnimation)
        {
            
            if (_actualFlipTime <= 0)
            {
                if (_runningTime <= maxRunningTimeForFlip || canFlipEvenWithHighSpeed)
                {
                    _runningDirection = runningDirection;

                    if (nullifyOnFlipAnimation) NullifyMoveVelocity(character);
                    
                    return true;
                    
                }
            }

            _actualFlipTime -= deltaTime;
            
            return false;
        }
        
        public bool ProcessFlipping(Rigidbody2D character, float deltaTime, int runningDirection, bool canFlipEvenWithHighSpeed, bool nullifyOnFlipAnimation)
        {
            if (_actualFlipTime <= 0)
            {
                if (_runningTime <= maxRunningTimeForFlip || canFlipEvenWithHighSpeed)
                {
                    _runningDirection = runningDirection;

                    if(nullifyOnFlipAnimation) NullifyMoveVelocity(character);

                    return true;
                }
            }
            
            _actualFlipTime -= deltaTime;
            return false;
        }

        private void ProcessSpeed(bool deAcceleration, float deltaTime, int runningDirection)
        {
            if (runningDirection == 0 || deAcceleration)
            {
                deac = true;
                DeAccelerate(deltaTime);
                return;
            }
            
            if (_runningTime + deltaTime < maxRunningTime)
            {
                deac = false;
                _runningTime += deltaTime;
                _actualSpeed = movementCurve.Evaluate(_runningTime);
                return;
            }

            _actualSpeed = movementCurve.Evaluate(maxRunningTime);
        }

        private void DeAccelerate(float deltaTime)
        {
            _runningTime = Mathf.Max(0, _runningTime - deltaTime * deAccelerationRatio);
            _actualSpeed = movementCurve.Evaluate(_runningTime);
        }
        private void MoveCharacter(Rigidbody character)
        {
            var velocity = Vector3.right * ((_runningDirection * _actualSpeed * finalSpeedMultiplier));

            character.velocity = new Vector3(velocity.x, character.velocity.y, velocity.z);
        }
        
        private void MoveCharacter(Rigidbody2D character)
        {
            var velocity = Vector3.right * ((_runningDirection * _actualSpeed * finalSpeedMultiplier));

            character.velocity = new Vector3(velocity.x, character.velocity.y);
        }
        private void FlipToRotation(Transform character, int direction)
        {
            var rotation = character.rotation;
            character.rotation = Quaternion.Euler(rotation.x, 90 * direction, rotation.z);
        }

        public void ResetMoveSpeed()
        {
            _actualSpeed = _runningTime = 0;
        }
        public void NullifyMoveVelocity(Rigidbody2D character)
        {
            ResetMoveSpeed();
            character.velocity = new Vector2(0, character.velocity.y);
        }        
        
        public void NullifyMoveVelocity(Rigidbody character)
        {
            ResetMoveSpeed();
            character.velocity = new Vector3(0, character.velocity.y, 0);
        }

        public void SetRunningDirection(int runningDirection)
        {
            _runningDirection = runningDirection;
        }

        public int GetRunningDirection()
        {
            return _runningDirection;
        }

        public float GetSpeed()
        {
            return _actualSpeed * finalSpeedMultiplier;
        }

        public bool InverseFlipDirection(int runningDirection)
        {
            return _runningDirection == runningDirection * -1;
        }

        public void SetFlipTime(float flip)
        {
            _actualFlipTime = flip;
        }
    }
}