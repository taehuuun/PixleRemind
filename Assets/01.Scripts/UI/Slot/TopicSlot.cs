using LTH.PixelRemind.Data;
using LTH.PixelRemind.Managers.Gallery;
using LTH.PixelRemind.Utill;
using TMPro;
using UnityEngine;

namespace LTH.PixelRemind.UI
{
    public class TopicSlot : GallerySlot
    {
        public TMP_Text completeCountText;
        public GameObject completeMark;
        public TopicData data;
        public override void OnSlotClick()
        {
            foreach (var pixelArtData in data.PixelArtDatas)
            {
                GalleryManager.ins.PixelArtDatas.Add(pixelArtData);
            }
        }

        public override void SetSlot()
        {
            titleText.text = data.ID;
            completeMark.SetActive(data.Complete);
            completeCountText.text = $"{data.CompleteCount} / {data.TotalCount}";
            thumbnailImb.sprite = PixelArtUtill.MakeThumbnail(data.ThumbData, data.ThumbSize);
        }
    }
}
