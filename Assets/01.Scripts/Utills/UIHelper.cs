using System.Collections;
using LTH.ColorMatch.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch.Utill
{
    public class UIHelper
    {
        public static string FormatSecondsToTimeString(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}"; 
        }
        public static IEnumerator FadeEffect(Graphic ui, float duration, FadeType type)
        {
            float elapsedTime = 0f;
            Color color = ui.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                color.a = type == FadeType.In ? Mathf.Lerp(0, 1, elapsedTime / duration) : Mathf.Lerp(1, 0, elapsedTime / duration);
                
                ui.color = color;
                yield return null;
            }
        }
    }
}
