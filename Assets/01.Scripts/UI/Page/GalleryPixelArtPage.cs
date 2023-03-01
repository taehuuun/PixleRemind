using System.Collections.Generic;
using LTH.ColorMatch.Managers;
using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class GalleryPixelArtPage : Page
    {
        [SerializeField] private GalleryUI ui;

        private List<string> _pixelArts = new List<string>();

        private void SetPage()
        {
            _pixelArts = DataManager.GetFiles(GalleryManager.topic);
            
            Debug.Log(_pixelArts.Count);

            foreach (var pixelArt in _pixelArts)
            {
                Debug.Log($"{pixelArt}");
            }
        }
    }
}
