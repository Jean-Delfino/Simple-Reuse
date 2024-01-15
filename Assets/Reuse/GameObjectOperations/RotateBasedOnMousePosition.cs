using Reuse.Utils;
using UnityEngine;

public class RotateBasedOnMousePosition : MonoBehaviour
{
    [SerializeField] private float additionalRotation = 0f;
    [SerializeField] private float rotationEndMultiplier = -1f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    protected Vector3 ActualMousePos;
    protected float ActualAngle;

    private Vector2 screen = new();
    void Update()
    {
        (screen.x, screen.y) = (Screen.width, Screen.height);

        ActualAngle = (UtilMathOperations.CalculateAtan2BasedOnConfinedDimension(Input.mousePosition, screen) * Mathf.Rad2Deg) + additionalRotation;
        
        transform.rotation = Quaternion.AngleAxis(rotationEndMultiplier * ActualAngle, rotationAxis);
    }
}