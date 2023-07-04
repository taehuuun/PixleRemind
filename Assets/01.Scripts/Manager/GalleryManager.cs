using Newtonsoft.Json;
using UnityEngine;

public class GalleryManager : MonoBehaviour
{
    public static GalleryManager ins;

    public GalleryPage CurPage { get; set; }
    public TopicData SelTopicData { get; set; }
    public PixelArtData SelPixelArtData { get; set; }

    public bool IsMatching { get; set; }
    
    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
            DontDestroyOnLoad(this);
        }
    }
    
    /// <summary>
    /// 플레이가 끝난 PixelArtData를 업데이트 및 저장하는 메서드
    /// </summary>
    /// <param name="updateData">업데이트 할 PixelArtData</param>
    public void UpdateAndSavePixelArtData(PixelArtData updateData)
    {
        TopicData saveTopic = SelTopicData;
        int idx = SelTopicData.PixelArtDatas.IndexOf(SelPixelArtData);
        saveTopic.PixelArtDatas[idx] = updateData;
        saveTopic.Complete = (saveTopic.CompleteCount == saveTopic.TotalCount);
        saveTopic.ThumbData = updateData.ThumbnailData;
        SavePixelArtData(saveTopic);
    }

    public async void UpdateCollectPixelArtData(CollectPixelArtData data)
    {
        Debug.Log($"{data.Title}");
        string collection = FirestoreCollections.UserData;
        
#if UNITY_EDITOR
        string fuid = "Test";
#else
        string fuid = FirebaseManager.ins.FireAuth.FUID;
#endif

        await FirebaseManager.ins.Firestore.UpdateData(collection, fuid, data);
    }
    /// <summary>
    /// 데이터를 Json으로 변환 후 실제 로컬에 저장하는 메서드
    /// </summary>
    /// <param name="data">변환 할 데이터</param>
    private void SavePixelArtData(TopicData data)
    {
        DataManager.SaveJsonData(DataPath.GalleryDataPath, data.ID, data);
    }

    private static TopicData GetTopicData(string topicName)
    {
        return DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, topicName);
    }
}