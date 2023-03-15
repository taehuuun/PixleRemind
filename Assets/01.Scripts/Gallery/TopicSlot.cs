using System.Linq;
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
            // GalleryManager.ins.PixelArtDatas.AddRange(data.PixelArtDatas.Select(JsonConvert.DeserializeObject<PixelArtData>));
            for (int i = 0; i < data.PixelArtDatas.Count; i++)
            {
                Debug.Log(data.PixelArtDatas[i]);
                GalleryManager.ins.PixelArtDatas.Add(data.PixelArtDatas[i]);
            }
            GalleryManager.ins.CurrentTopicArt = data;
        }

        public override void SetSlot()
        {
            titleText.text = data.Topic.ToString();
            thumbnailImb.sprite = PixelArtUtill.MakeThumbnail(data.ThumbData, data.ThumbSize);
        }
    }
}
