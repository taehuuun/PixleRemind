using System;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Managers;
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
            GalleryManager.ins.selectedPixelArt = titleText.text;
        }
    }
}
