using System;
using TMPro;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilTextMeshPro
    {
        [Serializable]
        public class SidedGradient
        {
            public Color defaultColor;

            public ColorMode colorMode;
            public Color topLeft;
            public Color topRight;
            public Color bottomLeft;
            public Color bottomRight;
        }
        
        public static void SetGradient(TextMeshProUGUI text, SidedGradient sidedGradient)
        {
            text.enableVertexGradient = true;
            text.color = sidedGradient.defaultColor;

            TMP_ColorGradient gradient = ScriptableObject.CreateInstance<TMP_ColorGradient>();

            gradient.colorMode = sidedGradient.colorMode;
            gradient.topLeft = sidedGradient.topLeft;
            gradient.topRight = sidedGradient.topRight;
            gradient.bottomLeft = sidedGradient.bottomLeft;
            gradient.bottomLeft = sidedGradient.bottomRight;
            
            text.colorGradientPreset = gradient;
        }
    }
}