using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("colorMatchSystem")] [SerializeField] private ColorMatchMiniGame colorMatchMiniGame;
    [SerializeField] private PlayUI playUI;

    private LocalData _localData;
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
        colorMatchMiniGame.ReStart();
        StartCoroutine(Playing());
        StartCoroutine(PixelArtUpdate());
        playUI.ShowUI();
    }
    
    private void Init()
    {
        _localData = DataManager.LocalData;

        _selectTopicData = DataManager.LocalData.GetTopicData(DataManager.LocalData.GetTopicID());
        _selectPixelArtData = _selectTopicData.PixelArtDataList.Find((pixelArtData) => pixelArtData.ID == DataManager.LocalData.GetPixelArtID());
        colorMatchMiniGame.SetPixelArtData(_selectPixelArtData);
        playUI.UpdatePixelArt(_selectPixelArtData.ThumbnailData,_selectPixelArtData.ThumbnailSize);
        playUI.SetPlayButton(_selectPixelArtData.IsCompleted);
    }
    
    public async void SaveData()
    {
        _selectTopicData.UpdateThumbnailData(_selectPixelArtData.ThumbnailData,_selectPixelArtData.ThumbnailSize);
        DataManager.LocalData.SetLocalTopicData(DataManager.LocalData.GetTopicID(), _selectTopicData);
        DataManager.SaveLocalData();
        
                
#if UNITY_ANDROID && !UNITY_EDITOR
        string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
        string FUID = "Test";
#endif
        await FirebaseManager.ins.Firestore.UpdateData(FirestoreCollections.UserData, FUID, DataManager.UserData);
    }

    private void CollectPixelArt()
    {
        _selectTopicData.CompleteCount++;

        if (_selectTopicData.CompleteCount == _selectTopicData.TotalCount)
        {
            _selectTopicData.Complete = true;
        }
        
        _selectPixelArtData.SetPlayTime(_playTime);

        CollectedPixelArtData newCollectPixelArtData = new CollectedPixelArtData(_selectPixelArtData.ID,_selectPixelArtData.Title, _selectPixelArtData.Description, _selectPixelArtData.ThumbnailData, _selectPixelArtData.ThumbnailSize, _selectPixelArtData.PlayTime);
        
        DataManager.LocalData.AddCollectedPixelArt(DataManager.LocalData.GetTopicID(),newCollectPixelArtData);
        DataManager.UserData.AddCollectedPixelArt(DataManager.LocalData.GetTopicID(),newCollectPixelArtData);
    }
    
    private IEnumerator Playing()
    {
        while (!colorMatchMiniGame.IsGameOver())
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
        while (!colorMatchMiniGame.IsGameOver())
        {
            yield return new WaitUntil(() => _lastThumbnailData != _selectPixelArtData.ThumbnailData);
            _lastThumbnailData = _selectPixelArtData.ThumbnailData;
            playUI.UpdateRemainPixelCount(_selectPixelArtData.PixelColorData.RemainingPixels);
            playUI.UpdatePixelArt(_lastThumbnailData, _selectPixelArtData.ThumbnailSize);
        }
    }
}