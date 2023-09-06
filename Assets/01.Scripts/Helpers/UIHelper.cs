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
}