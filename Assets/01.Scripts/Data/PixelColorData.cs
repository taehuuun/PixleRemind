using System.Collections.Generic;
using Firebase.Firestore;

namespace LTH.PixelRemind.Data
{
    [FirestoreData]
    public class PixelColorData
    {
        [FirestoreProperty]
        public int RemainingPixels { get; set; }

        [FirestoreProperty]
        public List<CustomPixel> CustomPixels { get; set; } = new List<CustomPixel>();

        public Dictionary<string, object> ToDictionary()
        {
            var dictionary = new Dictionary<string, object>
            {
                { "RemainingPixels", RemainingPixels },
                { "CustomPixels", CustomPixels }
            };
            return dictionary;
        }
    }
}