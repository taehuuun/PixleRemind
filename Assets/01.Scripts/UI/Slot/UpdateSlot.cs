using LTH.PixelRemind.Data;
using LTH.PixelRemind.Utill;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LTH.PixelRemind.UI.Slots
{
    public class UpdateSlot : MonoBehaviour
    {
        public Image thumbnailImg;
        public Image iconImg;
        public Text titleText;
        public Text introductionText;
        public GameObject select;
        public Sprite updateIcon;
        public Sprite missingIcon;
        
        private TopicData _topicData;
        
        public bool IsSelected { get; private set; }
        
        public void SetSlot(TopicData topicData, bool isMissing)
        {
            _topicData = topicData;
            thumbnailImg.sprite = PixelArtUtill.MakeThumbnail(_topicData.ThumbData, _topicData.ThumbSize);
            titleText.text = $"{_topicData.Title} 픽셀아트 {_topicData.TotalCount}개";
            introductionText.text = _topicData.Introduction;
            iconImg.sprite = isMissing ? missingIcon : updateIcon;
        }

        public void ToggleSelection()
        {
            IsSelected = !IsSelected;
            select.SetActive(IsSelected);
        }

        public TopicData GetTopicData()
        {
            return _topicData;
        }
    }
}
