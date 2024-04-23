using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reuse.Movement
{
    //https://www.youtube.com/watch?v=RPdn3r_tqcM&ab_channel=PressStart
    //Press Start
    public class Simple2DJumping : MonoBehaviour
    {
        private const float Gravity = -9.8f; 
        
        [SerializeField] private float jumpForce = 5f;

        [SerializeField] private Transform[] lookFloor;

        [SerializeField] private float gravityMultiplier = 1;
        [SerializeField] private float buttonPressedGravityMultiplier = 0.15f;

        [SerializeField] private float distanceCheckGround = 0.6f;

        [SerializeField] private LayerMask groundLayerMask;
        private bool _onGround = false;

        [SerializeField] private SimpleJumpingCoyoteTime coyoteTime;
        
        public bool OnGround => _onGround;

        private Vector3 _velocity = Vector3.zero;

        private bool CheckIfGrounded()
        {
            foreach (var legs in lookFloor)
            {
                if (Physics.Raycast(legs.position, Vector3.down, distanceCheckGround, groundLayerMask))
                {
                    return true;
                }
            }

            return false;
        }
        
        //Easiest way of coding, even if redundant, move obvious too
        private bool CheckIfGrounded2D()
        {
            foreach (var legs in lookFloor)
            {
                if (Physics2D.Raycast(legs.position, Vector3.down, distanceCheckGround, groundLayerMask))
                {
                    return true;
                }
            }

            return false;
        }

        public float GetSpeed()
        {
            return _velocity.y;
        }

        public (bool falling, bool jumped) ProcessJump(Rigidbody characterRigidbody, bool colliding, int jump, float deltaTime)
        {
            coyoteTime.DecreaseCoyoteTime(deltaTime);
            var lastStateGrounded = _onGround;
            _onGround = CheckIfGrounded();
            SetCoyote(lastStateGrounded);

            var res =  FindMovementSpeed(characterRigidbody.velocity,deltaTime, colliding, jump);
            characterRigidbody.velocity = res.Item1;
            
            return (_velocity.y < 0, res.Item2);
        }

        public (bool falling, bool jumped) ProcessJump(Rigidbody2D characterRigidbody, bool colliding, int jump, float deltaTime)
        {
            coyoteTime.DecreaseCoyoteTime(deltaTime);
            var lastStateGrounded = _onGround;
            _onGround = CheckIfGrounded2D();
            SetCoyote(lastStateGrounded);
            
            var res =  FindMovementSpeed(characterRigidbody.velocity,deltaTime, colliding, jump);
            characterRigidbody.velocity = res.Item1;

            return (_velocity.y < 0, res.Item2);

        }

        private (Vector3, bool) FindMovementSpeed(Vector3 velocity, float deltaTime, bool colliding, int jump)
        {
            var isPressingJump = jump > 0;

            var jumped = FindVerticalVelocity(isPressingJump, colliding, deltaTime);
            return (new Vector3(velocity.x, _velocity.y, velocity.z), jumped);
        }
        private bool FindVerticalVelocity(bool isPressingJump, bool colliding, float deltaTime)
        {
            if (CanJump(isPressingJump))
            {
                ApplyJump();
                return true;
            }
            
            if (colliding && _onGround)
            {
                _velocity.y = 0;
                return false;
            }

            ApplyGravity((_velocity.y <= 0 || colliding) ? gravityMultiplier :
                isPressingJump ? buttonPressedGravityMultiplier : 
                gravityMultiplier, deltaTime);
            return false;
        }

        public bool CanJump(bool isPressingJump)
        {
            return isPressingJump && (_onGround || coyoteTime.CheckIfCanJump());
        }
        private void ApplyJump()
        {
            _velocity.y = jumpForce;
        }
        
        private void ApplyGravity(float multiplier, float deltaTime)
        {
            _velocity.y += Gravity * multiplier * deltaTime;
        }

        private void SetCoyote(bool lastStateGrounded)
        {
            if (!lastStateGrounded && _onGround)
            {
                coyoteTime.SaveGroundedMovement();
                return;
            }
            
            if (lastStateGrounded && !_onGround)
            {
                coyoteTime.SaveUngroundedMoment();
            }
        }

        public void NullifyMovement()
        {
            _velocity.y = 0;
            ResetMove();
        }
        public void ResetMove()
        {
            coyoteTime.SaveUngroundedMoment();
        }
        
#if UNITY_EDITOR
        public bool showGizmos = true;
        
        private void OnDrawGizmos()
        {
            if(!showGizmos) return;
            
            Gizmos.color = Color.red;
            
            foreach (var legs in lookFloor)
            {
                Gizmos.DrawLine(legs.position, legs.position + Vector3.down * distanceCheckGround);
            }
        }
#endif
    }
}