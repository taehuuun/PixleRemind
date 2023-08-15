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
        var selectTopicID = DataManager.LocalData.SelectTopicDataID;
        if (string.IsNullOrEmpty(selectTopicID))
        {
            SetButtonInactive("토픽 선택");
            return;
        }

        _lastTopicData = DataManager.LocalData.LocalTopicData[DataManager.LocalData.SelectTopicDataID];
        _lastPixelArtData = _lastTopicData?.PixelArtDataList.Find(pixelArtData => pixelArtData.ID == DataManager.LocalData.SelectPixelArtDataID);
    }

    private async Task UpdateUserDataOnFirebase()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
        string FUID = "Test";
#endif
        await FirebaseManager.ins.Firestore.UpdateData(FirestoreCollections.UserData, FUID, DataManager.LocalData);
    }

    private void ConfigurePlayButton()
    {
        if (_lastTopicData == null)
        {
            playButton.interactable = false;
            return;
        }

        if (_lastPixelArtData.IsCompleted)
        {
            DataManager.LocalData.SelectPixelArtDataID = string.Empty;
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
            DataManager.LocalData.SelectTopicDataID = string.Empty;
            SetButtonInactive("토픽 선택");
        }
        else
        {
            buttonText.text = _lastTopicData.Title;
            buttonImage.gameObject.SetActive(false);
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
