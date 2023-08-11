using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpdateSlot : MonoBehaviour
{
    public Image thumbnailImg;
    public Image iconImg;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public GameObject select;
    public Sprite updateIcon;
    public Sprite missingIcon;

    private TopicData _topicData;

    public bool IsSelected { get; private set; }

    public void SetSlot(TopicData topicData, bool isMissing)
    {
        _topicData = topicData;
        thumbnailImg.sprite = PixelArtHelper.MakeThumbnail(_topicData.ThumbnailData, _topicData.ThumbnailSize);
        titleText.text = $"{_topicData.Title} 픽셀아트 {_topicData.TotalCount}개";
        descriptionText.text = _topicData.Description;
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