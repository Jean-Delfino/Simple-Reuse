using System.Collections.Generic;
using Reuse.Patterns;

namespace Reuse.GameObjectOperations
{
    public class DoOperationsController : Singleton<DoOperationsController>
    {
        private HashSet<DoOperation> _operations = new();
        public void Update()
        {
            foreach (var operation in _operations)
            {
                if (operation == null)
                {
                    Unsubscribe(operation);
                    continue;
                }
                
                operation.DoUpdateOperation();
            }
        }

        public void Subscribe(DoOperation operation)
        {
            _operations.Add(operation);
        }
        
        public void Unsubscribe(DoOperation operation)
        {
            _operations.Remove(operation);
        }
    }
}