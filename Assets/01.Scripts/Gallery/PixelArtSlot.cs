using LTH.ColorMatch.Data;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using TMPro;

namespace LTH.ColorMatch.UI
{
    public class PixelArtSlot : GallerySlot
    {
        public TMP_Text difficultyText;
        public PixelArtData pixelData;
        
        public override void OnSlotClick()
        {
            // GalleryManager.ins.selPixelArtIdx = pixelData;
        }

        public override void SetSlot()
        {
            difficultyText.text = pixelData.difficulty.ToString();
            titleText.text = pixelData.title;
            thumbnailImb.sprite = PixelArtUtill.MakeThumbnail(pixelData.thumbData, pixelData.size);
        }
    }
}
