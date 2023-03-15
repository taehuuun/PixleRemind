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
            GalleryManager.ins.PixelArtDatas.AddRange(data.PixelArtDatas.Select(JsonConvert.DeserializeObject<PixelArtData>));
            GalleryManager.ins.CurrentTopicArt = data;
        }

        public override void SetSlot()
        {
            titleText.text = data.Topic.ToString();
            thumbnailImb.sprite = PixelArtUtill.MakeThumbnail(data.ThumbData, data.ThumbSize);
        }
    }
}
