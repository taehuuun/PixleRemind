using LTH.PixelRemind.Data;
using LTH.PixelRemind.Utill;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.PixelRemind.UI.Slots
{
    public class UpdateSlot : MonoBehaviour
    {
        public Image thumbnailImg;
        public Text titleText;
        public Text introductionText;
        public GameObject checkIcon;
        public GameObject updateIcon;
        public GameObject missingIcon;
        
        private TopicData _topicData;
        
        public void SetSlot(TopicData topicData, bool update, bool missing)
        {
            thumbnailImg.sprite = PixelArtUtill.MakeThumbnail(topicData.ThumbData, topicData.ThumbSize);
            titleText.text = $"{topicData.Title} 픽셀아트 {topicData.TotalCount}개";
            introductionText.text = topicData.Introduction;
            updateIcon.SetActive(update);
            missingIcon.SetActive(missing);
        }
    }
}
