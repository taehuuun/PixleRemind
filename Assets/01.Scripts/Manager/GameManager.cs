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
        StartCoroutine(Playing());
        StartCoroutine(PixelArtUpdate());
        playUI.ShowUI();
    }
    
    private void Init()
    {
        _userData = DataManager.userData;
        _selectTopicData = DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, _userData.SelectTopicID);
        _selectPixelArtData = _selectTopicData.PixelArtDatas.Find((pixelArtData) => pixelArtData.ID == DataManager.userData.SelectPixelArtID);
        colorMatchSystem.SetPixelArtData(_selectPixelArtData);
        playUI.UpdatePixelArt(_selectPixelArtData.ThumbnailData,_selectPixelArtData.ThumbnailSize);
        playUI.SetPlayButton(_selectPixelArtData.IsCompleted);
    }
    
    public async void SaveData()
    {
        _selectTopicData.ThumbnailData = _selectPixelArtData.ThumbnailData;
        _selectTopicData.ThumbnailSize = _selectPixelArtData.ThumbnailSize;
        DataManager.SaveJsonData(DataPath.GalleryDataPath,_userData.SelectTopicID, _selectTopicData);
        
                
#if UNITY_ANDROID && !UNITY_EDITOR
        string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
        string FUID = "Test";
#endif
        await FirebaseManager.ins.Firestore.UpdateData(FirestoreCollections.UserData, FUID, DataManager.userData);
    }

    private void CollectPixelArt()
    {
        _selectTopicData.CompleteCount++;

        if (_selectTopicData.CompleteCount == _selectTopicData.TotalCount)
        {
            _selectTopicData.Complete = true;
        }
        
        _selectPixelArtData.PlayTime = _playTime;

        DownloadTopicData downloadTopicData =  DataManager.userData.DownloadTopicDataList.Find((collectTopic) => collectTopic.ID == _selectTopicData.ID);
        CollectedPixelArtData newCollectPixelArtData = new CollectedPixelArtData(_selectPixelArtData.ID,_selectPixelArtData.Title, _selectPixelArtData.ThumbnailData, _selectPixelArtData.Description, _selectPixelArtData.ThumbnailSize);
        downloadTopicData.CollectedPixelArtDataList.Add(newCollectPixelArtData);
    }
    
    private IEnumerator Playing()
    {
        while (!colorMatchSystem.IsGameOver())
        {
            yield return _timerDelay;
            _playTime++;
            playUI.UpdatePlayTime(_playTime);
        }
        playUI.HideUI();

        if (_selectPixelArtData.IsCompleted)
        {
            CollectPixelArt();
        }
        
        SaveData();
    }
    private IEnumerator PixelArtUpdate()
    {
        while (!colorMatchSystem.IsGameOver())
        {
            yield return new WaitUntil(() => _lastThumbnailData != _selectPixelArtData.ThumbnailData);
            _lastThumbnailData = _selectPixelArtData.ThumbnailData;
            playUI.UpdateRemainPixelCount(_selectPixelArtData.PixelColorData.RemainingPixels);
            playUI.UpdatePixelArt(_lastThumbnailData, _selectPixelArtData.ThumbnailSize);
        }
    }
}