using System;
using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public abstract class DoOperation : MonoBehaviour
    {
        public abstract void DoUpdateOperation();

        private void OnEnable()
        {
            DoOperationsController.Instance.Subscribe(this);

        }

        private void OnDisable()
        {
            DoOperationsController.Instance.Unsubscribe(this);
        }
    }
}