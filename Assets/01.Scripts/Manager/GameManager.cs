using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ColorMatchSystem colorMatchSystem;
    [SerializeField] private PlayUI playUI;

    private UserData _userData;
    private PixelArtData _selectPixelArtData;
    private TopicData _selectTopicData;
    
    private readonly WaitForSeconds _timerDelay = new WaitForSeconds(1f);
    
    private int _playTime;
    private string _lastThumbnailData = string.Empty;
    
    private void Start()
    {
        Init();
    }

    public void StartGame()
    {
        _playTime = 0;
        colorMatchSystem.ReStart();
        StartCoroutine(Plying());
        StartCoroutine(PixelArtUpdate());
        playUI.ShowUI();
    }
    
    private void Init()
    {
        _userData = DataManager.userData;
        _selectTopicData = DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, _userData.SelectTopicID);
        _selectPixelArtData = _selectTopicData.PixelArtDatas.Find((pixelArtData) => pixelArtData.ID == DataManager.userData.SelectPixelArtID);
        playUI.UpdatePixelArt(_selectPixelArtData.ThumbnailData,_selectPixelArtData.Size);
        playUI.SetPlayButton(_selectPixelArtData.IsCompleted);
    }
    
    private void SaveData()
    {
        DataManager.SaveJsonData(DataPath.GalleryDataPath,_userData.SelectTopicID, _selectTopicData);
    }
    
    private IEnumerator Plying()
    {
        while (!colorMatchSystem.IsGameOver())
        {
            yield return _timerDelay;
            _playTime++;
            playUI.UpdatePlayTime(_playTime);
        }

        if (_selectPixelArtData.IsCompleted)
        {
            _selectTopicData.CompleteCount++;
        }
        
        SaveData();
        playUI.HideUI();
    }
    private IEnumerator PixelArtUpdate()
    {
        while (!colorMatchSystem.IsGameOver())
        {
            yield return new WaitUntil(() => _lastThumbnailData != _selectPixelArtData.ThumbnailData);
            _lastThumbnailData = _selectPixelArtData.ThumbnailData;
            playUI.UpdateRemainPixelCount(_selectPixelArtData.PixelColorData.RemainingPixels);
            playUI.UpdatePixelArt(_lastThumbnailData, _selectPixelArtData.Size);
        }
    }
}