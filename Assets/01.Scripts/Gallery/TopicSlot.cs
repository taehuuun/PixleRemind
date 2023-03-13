using LTH.ColorMatch.Data;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using Newtonsoft.Json;
using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class TopicSlot : GallerySlot
    {
        public TopicData data;
        public override void OnSlotClick()
        {
            for (int i = 0; i < data.pixelArtDatas.Count; i++)
            {
                GalleryManager.ins.pixelArtDatas.Add(JsonConvert.DeserializeObject<PixelArtData>(data.pixelArtDatas[i]));
            }
            
            GalleryManager.ins.selectedTopic = data.topic.ToString();
        }

        public override void SetSlot()
        {
            titleText.text = data.topic.ToString();
            thumbnailImb.sprite = PixelArtUtill.MakeThumbnail(data.thumbData, data.thumbSize);
        }
    }
}
