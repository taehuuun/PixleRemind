using System.Linq;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using Newtonsoft.Json;

namespace LTH.ColorMatch.UI
{
    public class TopicSlot : GallerySlot
    {
        public TopicData data;
        public override void OnSlotClick()
        {
            GalleryManager.ins.pixelArtDatas.AddRange(data.pixelArtDatas.Select(JsonConvert.DeserializeObject<PixelArtData>));
            GalleryManager.ins.currentTopicArt = data;
        }

        public override void SetSlot()
        {
            titleText.text = data.topic.ToString();
            thumbnailImb.sprite = PixelArtUtill.MakeThumbnail(data.thumbData, data.thumbSize);
        }
    }
}
