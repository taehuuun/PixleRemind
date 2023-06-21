using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class UIHelper
{
    /// <summary>
    /// 초를 문자열 시분초 형태로 출력 60 -> "01:00"
    /// </summary>
    /// <param name="totalSeconds">초 값</param>
    /// <returns>시분초 형태의 문자열</returns>
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

            color.a = type == FadeType.In
                ? Mathf.Lerp(0, 1, elapsedTime / duration)
                : Mathf.Lerp(1, 0, elapsedTime / duration);

            ui.color = color;
            yield return null;
        }
    }
}