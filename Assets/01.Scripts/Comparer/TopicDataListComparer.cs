using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TopicDataListComparer : IComparer<TopicData>
{
    /// <summary>
    /// 토픽 데이터를 비교하기 위한 Compare 메서드
    /// </summary>
    /// <param name="x">비교할 토픽 데이터 X</param>
    /// <param name="y">비교할 토픽 데이터 Y</param>
    /// <returns>비교 결과</returns>
    public int Compare(TopicData x, TopicData y)
    {
        // 각 타이틀에서 타이틀만 추출 (숫자 제거)
        string titleX = Regex.Replace(x.Title, @"\d+", "").Trim();
        string titleY = Regex.Replace(y.Title, @"\d+", "").Trim();

        // 각 타이틀에서 숫자만 추출
        int.TryParse(Regex.Match(x.Title, @"\d+").Value, out var numberX);
        int.TryParse(Regex.Match(y.Title, @"\d+").Value, out var numberY);

        // 우선 타이틀 부분을 한글 사전 순으로 비교
        int titleComparison = String.Compare(titleX, titleY, StringComparison.CurrentCulture);

        // 타이틀이 동일 하면 숫자를 비교 하여 반환, 다르다면 타이틀 비교 결과를 반환
        return titleComparison == 0 ? numberX.CompareTo(numberY) : titleComparison;
    }
}