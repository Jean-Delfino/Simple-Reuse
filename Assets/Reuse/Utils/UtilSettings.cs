using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilSettings
    {
        public static void SetQualitySettings(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
    }
}