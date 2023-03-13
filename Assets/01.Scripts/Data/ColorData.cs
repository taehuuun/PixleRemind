using System.Collections.Generic;
using Firebase.Firestore;

namespace LTH.ColorMatch.Data
{
    public class ColorData
    {
        [FirestoreProperty]
        public int remainPixel { get; set; }

        [FirestoreProperty]
        public List<CustomColor> Pixels { get; set; } = new List<CustomColor>();

        [FirestoreProperty]
        public Dictionary<string, object> ToDictionary
        {
            get
            {
                var dictionary = new Dictionary<string, object>
                {
                    { "remainPixel", remainPixel },
                    {"Pixels",Pixels}
                };
                return dictionary;
            }
        }
    }
}
