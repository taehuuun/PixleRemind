using System.Collections.Generic;
using System.Text.RegularExpressions;
using LTH.PixelRemind.Data;

namespace LTH.PixelRemind.Comparer
{
    public class TopicDataListComparer : IComparer<TopicData>
    {
        public int Compare(TopicData x, TopicData y)
        {
            string titleX = Regex.Replace(x.Title, @"\d", "");
            string titleY = Regex.Replace(y.Title, @"\d", "");

            int numberX = 0;
            int.TryParse(Regex.Match(x.Title, @"\d+").Value, out numberX);

            int numberY = 0;
            int.TryParse(Regex.Match(y.Title, @"\d+").Value, out numberY);

            int titleComparison = titleX.CompareTo(titleY);
            if (titleComparison == 0)
            {
                // If titles are the same, compare by number
                return numberX.CompareTo(numberY);
            }
            else
            {
                return titleComparison;
            }
        }
    }
}
