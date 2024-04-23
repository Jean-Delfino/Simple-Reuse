using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class WaypointMovement : MonoBehaviour
    {

        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private int initialWaypoint;

        [SerializeField] private bool doubleDirection = false;
    
        private int _currentWaypointIndex = 0;
        private int _increment = 1;
        private void Start()
        {
            _currentWaypointIndex = initialWaypoint;
            transform.position = waypoints[_currentWaypointIndex].position;
        }

        private void Update()
        {
            if (waypoints.Length == 0)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_currentWaypointIndex].position, moveSpeed * Time.deltaTime);
        
            if (Vector3.Distance(transform.position, waypoints[_currentWaypointIndex].position) < 0.1f)
            {
                IncrementWaypoint();
            }
        }

        private void IncrementWaypoint()
        {
            if (doubleDirection)
            {
                _currentWaypointIndex += _increment;
            
                if (_currentWaypointIndex == 0 || _currentWaypointIndex == waypoints.Length - 1)
                {
                    _increment *= -1;
                }

                return;
            
            }
            _currentWaypointIndex = (_currentWaypointIndex + _increment) % waypoints.Length;
        }
    
    }
}