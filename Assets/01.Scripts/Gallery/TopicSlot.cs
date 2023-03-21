using LTH.ColorMatch.Data;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using TMPro;
using UnityEngine;

namespace LTH.ColorMatch.UI
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
            titleText.text = data.Topic.ToString();
            completeMark.SetActive(data.Complete);
            completeCountText.text = $"{data.CompleteCount} / {data.TotalCount}";
            thumbnailImb.sprite = PixelArtUtill.MakeThumbnail(data.ThumbData, data.ThumbSize);
        }
    }
}
