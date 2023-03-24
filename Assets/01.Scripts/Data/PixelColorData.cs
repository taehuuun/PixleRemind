using System.Collections.Generic;
using Firebase.Firestore;

namespace LTH.ColorMatch.Data
{
    [FirestoreData]
    public class PixelColorData
    {
        [FirestoreProperty]
        public int RemainPixel { get; set; }

        [FirestoreProperty]
        public List<CustomColor> Pixels { get; set; } = new List<CustomColor>();

        public Dictionary<string, object> ToDictionary()
        {
            var dictionary = new Dictionary<string, object>
            {
                { "RemainPixel", RemainPixel },
                {"Pixels",Pixels}
            };
            return dictionary;
        }
    }
}
