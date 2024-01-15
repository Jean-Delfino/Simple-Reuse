using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilTransform
    {
        public static void SetChildParentToOneLevelAbove(Transform child)
        {
            child.SetParent(child.parent.parent);
        }
        
        public static float GetMiddleCenterHorizontalLayoutGroupPosition(float minValue, float maxValue, int index, int totalOfUpgrades)
        {
            var totalSize = maxValue - minValue;
            var spacing = totalSize / (totalOfUpgrades + 1); //Left and right spacing
            return minValue + ((index + 1) * spacing);
        }
        
        public static void CopyRectTransform(RectTransform destiny, RectTransform origin){
            destiny.anchorMin = origin.anchorMin;
            destiny.anchorMax = origin.anchorMax;
            destiny.anchoredPosition = origin.anchoredPosition;
            destiny.rotation = origin.rotation;
            destiny.sizeDelta = origin.sizeDelta;
            destiny.localScale = origin.localScale;
        }

        public static Quaternion GetRotationLookingTwoUpRotations(Transform toLook, Transform target)
        {
            return Quaternion.FromToRotation(toLook.up, target.up);
        }

        public static Quaternion GetRotationInverseTwoUpRotations(Transform toLook, Transform target)
        {
            var upLook = target.up;
            var upRotate = toLook.up;
            
            upLook.Normalize();
            upRotate.Normalize();
            
            return (upLook != upRotate * -1) ? Quaternion.FromToRotation(upRotate, -upLook) : Quaternion.identity;
        }

        public static Quaternion GetRotationLookingTwoRightRotations(Transform toLook, Transform target)
        {
            return Quaternion.FromToRotation(toLook.right, target.right);
        }
        
        public static Quaternion GetRotationLookingTwoForwardRotations(Transform toLook, Transform target)
        {
            return Quaternion.FromToRotation(toLook.forward, target.forward);
        }

        
        public static Quaternion GetRotationLookingAtTransform(Transform toLook, Transform target)
        {
            return GetRotationLookingTwoForwardRotations(toLook, target) * 
                   GetRotationLookingTwoRightRotations(toLook,  target) * 
                   GetRotationLookingTwoUpRotations(toLook, target);
        }
    }
}