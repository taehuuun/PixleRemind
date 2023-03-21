using LTH.ColorMatch.Data;
using LTH.ColorMatch.Utill;
using TMPro;
using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class PixelArtSlot : GallerySlot
    {
        public TMP_Text difficultyText;
        public PixelArtData pixelData;
        public GameObject completeMark;
        
        public override void OnSlotClick()
        {
            // GalleryManager.ins.selPixelArtIdx = pixelData;
        }

        public override void SetSlot()
        {
            difficultyText.text = pixelData.Difficulty.ToString();
            titleText.text = pixelData.Title;
            thumbnailImb.sprite = PixelArtUtill.MakeThumbnail(pixelData.ThumbData, pixelData.Size);
            completeMark.SetActive(pixelData.Complete);
        }
    }
}
