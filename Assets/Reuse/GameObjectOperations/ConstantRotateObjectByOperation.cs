using System;
using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class ConstantRotateObjectByOperation : DoOperation
    {
        [SerializeField] private float speed = 90.0f;

        [SerializeField] private Vector3 rotateDirection = Vector3.up;

        public override void DoUpdateOperation()
        {
            transform.Rotate(rotateDirection, speed * Time.deltaTime);
        }
    }
}