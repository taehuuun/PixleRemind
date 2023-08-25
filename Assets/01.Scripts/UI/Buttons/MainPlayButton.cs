using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPlayButton : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_Text buttonText;

    private TopicData _lastTopicData;
    private PixelArtData _lastPixelArtData;
    
    private async void Start()
    {
        LoadData();
        ConfigurePlayButton();
        await UpdateUserDataOnFirebase();
    }

    private void LoadData()
    {
        var selectTopicID = DataManager.LocalData.GetTopicID();
        if (string.IsNullOrEmpty(selectTopicID))
        {
            SetButtonInactive("토픽 선택");
            return;
        }

        _lastTopicData = DataManager.LocalData.GetTopicData(selectTopicID);
        _lastPixelArtData = _lastTopicData?.GetPixelArt(DataManager.LocalData.GetPixelArtID());
    }

    private async Task UpdateUserDataOnFirebase()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
        string FUID = "Test";
#endif
        await FirebaseManager.ins.Firestore.UpdateData(FirestoreCollections.UserData, FUID, DataManager.UserData);
    }

    private void ConfigurePlayButton()
    {
        if (_lastTopicData == null)
        {
            playButton.interactable = false;
            return;
        }
        
        if (_lastPixelArtData == null ||_lastPixelArtData.IsCompleted)
        {
            DataManager.LocalData.SetPixelArtDataID(string.Empty);
            ConfigurePlayButtonForCompletedPixelArt();
        }
        else
        {
            ConfigurePlayButtonForIncompletePixelArt();
        }
    }

    private void ConfigurePlayButtonForCompletedPixelArt()
    {
        if (_lastTopicData.Complete)
        {
            DataManager.LocalData.SetTopicDataID(string.Empty);
            SetButtonInactive("토픽 선택");
        }
        else
        {
            buttonText.text = _lastTopicData.Title;
            buttonImage.sprite =
                PixelArtHelper.MakeThumbnail(_lastTopicData.ThumbnailData, _lastTopicData.ThumbnailSize);
            SetButtonActive(SceneManager.LoadScene, SceneNames.GalleryScene);
        }
    }

    private void ConfigurePlayButtonForIncompletePixelArt()
    {
        buttonImage.sprite = PixelArtHelper.MakeThumbnail(_lastPixelArtData.ThumbnailData, _lastPixelArtData.ThumbnailSize);
        buttonText.text = "이어하기";
        SetButtonActive(SceneManager.LoadScene, SceneNames.PlayScene);
    }

    private void SetButtonActive(Action<string> action, string argument)
    {
        playButton.interactable = true;
        playButton.onClick.AddListener(() => action(argument));
    }

    private void SetButtonInactive(string text)
    {
        buttonImage.gameObject.SetActive(false);
        buttonText.text = text;
        playButton.interactable = false;
    }
}
