using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
