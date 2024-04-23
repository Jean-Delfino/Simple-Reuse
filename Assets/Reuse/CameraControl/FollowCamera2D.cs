using System;
using UnityEngine;

namespace Reuse.CameraControl
{
    //https://www.youtube.com/watch?v=GTxiCzvYNOc&ab_channel=PressStart
    //Press Start
    public class FollowCamera2D : MonoBehaviour
    {
        [SerializeField] private Transform followObject;
        [SerializeField] private Vector2 followOffset;
        [SerializeField] private float speed;
        [SerializeField] private bool setPositionInStart = true;
        
        private Vector2 _threshold;

        private void Start()
        {
            _threshold = CalculateThreshold(CameraController.GetMainCamera());

            if (setPositionInStart) SetToFollowObjectPos();
        }

        public void SetToFollowObjectPos()
        {
            transform.position = followObject.position;
        }
        
        public void UpdateCamera(float deltaTime)
        {
            var pos = transform.position;
            var followPos = followObject.position;
            var difX = Vector2.Distance(Vector2.right * pos.x, Vector2.right * followPos.x);
            var difY = Vector2.Distance(Vector2.up * pos.y, Vector2.up * followPos.y);

            transform.position = Vector3.MoveTowards(pos, FixPositionWithThresholds(pos, followPos, difX, difY), speed * deltaTime);
        }

        private Vector3 FixPositionWithThresholds(Vector3 initialPos, Vector3 follow, float difX, float difY)
        {
            Vector3 newPos = initialPos;
            if (Mathf.Abs(difX) >= _threshold.x)
            {
                newPos.x = follow.x;
            }
            
            if (Mathf.Abs(difY) >= _threshold.y)
            {
                newPos.y = follow.y;
            }

            return newPos;
        }

        private Vector3 CalculateThreshold(Camera mainCamera)
        {
            var aspect = mainCamera.pixelRect;
            var orthographic = mainCamera.orthographicSize;

            Vector2 thresh = new Vector2(
                orthographic * aspect.width / aspect.height,
                orthographic);

            thresh -= followOffset;

            return thresh;
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

#if UNITY_EDITOR
        public bool showGizmos = true;
        
        private void OnDrawGizmos()
        {
            if(!showGizmos) return;
            
            Gizmos.color = Color.blue;
            Vector2 border = CalculateThreshold(Camera.main);
            Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2 , 1));
        }
#endif

    }
}