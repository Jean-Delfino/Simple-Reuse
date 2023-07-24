using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Reuse.Utils
{
    public static class UtilVisuals
    {
        public static IEnumerator Fade(Image image, float finalValue, float duration)
        {
            var color = image.color;
            var startValue = color.a;
            for (var i = 0f; i < duration; i+= Time.deltaTime)
            {
                color.a = Mathf.Lerp(startValue, finalValue, i / duration);
                image.color = color;
                yield return new WaitForEndOfFrame();
            }

            color.a = finalValue;
            image.color = color;
        }
        
        public static IEnumerator Fade(SpriteRenderer image, float finalValue, float duration)
        {
            var color = image.color;
            var startValue = color.a;
            for (var i = 0f; i < duration; i+= Time.deltaTime)
            {
                color.a = Mathf.Lerp(startValue, finalValue, i / duration);
                image.color = color;
                yield return new WaitForEndOfFrame();
            }

            color.a = finalValue;
            image.color = color;
        }

        public static void ChangeImageAlpha(Image image, float value)
        {
            var color = image.color;
            color.a = value;
            image.color = color;
        }

        public static void ChangeImageAlpha(SpriteRenderer image, float value)
        {
            var color = image.color;
            color.a = value;
            image.color = color;
        }
        
        public static IEnumerator Fade(CanvasGroup canvasGroup, float finalValue, float duration)
        {
            var actualAlpha = canvasGroup.alpha;

            for (var i = 0f; i < duration; i+= Time.deltaTime)
            {
                canvasGroup.alpha = Mathf.Lerp(actualAlpha, finalValue, i / duration);
                yield return new WaitForEndOfFrame();
            }

            canvasGroup.alpha = finalValue;
        }

        public static void RevertFade(Image image, float finalValue)
        {
            var color = image.color;
            color.a = finalValue;
            image.color = color;
        }
        
        public static Color RandomSolidColor(){
            int count = 1;
            int i = 0;
            float[] tableTruthFillet = new float[3];
            System.Random rand = new System.Random();

            tableTruthFillet[rand.Next(0,3)] = 1f;

            while(i < 3 && count < 2){
                if(tableTruthFillet[i] != 0){
                    i++;
                    continue;
                }
            
                tableTruthFillet[i] = (float) rand.NextDouble(); 

                if(tableTruthFillet[i] != 0){
                    count++;
                }

                i++;
            }

            return new Color(tableTruthFillet[0], tableTruthFillet[1], tableTruthFillet[2]);
        }

        public static Color CreateNewDifferentColor(Color actualColor){
            Color newColor;

            do{
                newColor = RandomSolidColor();
            }while(newColor == actualColor);//Generates a new color

            return newColor;
        }
    }
}