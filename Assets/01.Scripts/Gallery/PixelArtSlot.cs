using System;
using LTH.ColorMatch.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LTH.ColorMatch.UI
{
    public class PixelArtSlot : GallerySlot
    {
        public TMP_Text difficultyText;
        public PixelArtData pixelData;
        public override void OnSlotClick()
        {
            SceneManager.LoadScene("GalleryMode");
        }
    }
}
