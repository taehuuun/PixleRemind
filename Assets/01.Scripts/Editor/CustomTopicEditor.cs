using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class CustomTopicEditor : EditorWindow
{
    private FirestoreHandler _firestore;

    private Task _task;
    private PixelArtData _deletePixelArtData = null;
    private Vector2 _scrollPosition;

    private List<TopicData> _firestoreTopicDatas;
    private readonly Dictionary<string, bool> _foldOutTopicStatus = new Dictionary<string, bool>();
    private readonly Dictionary<PixelArtData, bool> _foldOutPixelStatus = new Dictionary<PixelArtData, bool>();

    // 새로운 PixelArtData 생성을 위한 임시 필드
    private string _tmpTitleId;
    private Texture2D _tmpTexture;
    private Difficulty _tmpDifficulty;
    private bool _isAddingPixelArtData;

    [MenuItem("Topic Editor/Editor")]
    private static void Init()
    {
        CustomTopicEditor window = (CustomTopicEditor)GetWindow(typeof(CustomTopicEditor));
        window.titleContent.text = "Topic Editor";
        window.maxSize = new Vector2(600, 600);
        window.Show();
    }

    private async void OnEnable()
    {
        _firestore = new FirestoreHandler();
        await LoadTopicDatas();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Upload to Firestore"))
        {
            UploadToFirestore();
        }

        if (GUILayout.Button("Add TopicData"))
        {
            var newTopicData = new TopicData();
            _firestoreTopicDatas.Add(newTopicData);
            newTopicData.Title = _firestoreTopicDatas.Count.ToString();
            newTopicData.ID = $"_tempTopic_{_firestoreTopicDatas.Count.ToString()}";
            _foldOutTopicStatus[newTopicData.ID] = true;
        }

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        if (_firestoreTopicDatas != null && _firestoreTopicDatas.Count > 0)
        {
            var firestoreTopicDataCopy = new List<TopicData>(_firestoreTopicDatas);

            foreach (var topicData in firestoreTopicDataCopy)
            {
                if (!_foldOutTopicStatus.ContainsKey(topicData.ID))
                {
                    _foldOutTopicStatus[topicData.ID] = false;
                }

                _foldOutTopicStatus[topicData.ID] =
                    EditorGUILayout.BeginFoldoutHeaderGroup(_foldOutTopicStatus[topicData.ID], topicData.Title);

                if (_foldOutTopicStatus[topicData.ID])
                {
                    ShowTopicData(topicData);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.Space();
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void ShowTopicData(TopicData topicData)
    {
        EditorGUI.indentLevel++;

        EditorGUILayout.TextField("ID", topicData.ID);
        topicData.Title = EditorGUILayout.TextField("Title", topicData.Title);
        EditorGUILayout.TextField("ThumbData", topicData.ThumbData);
        EditorGUILayout.TextField("CompleteCount", topicData.CompleteCount.ToString());
        EditorGUILayout.TextField("TotalCount", topicData.TotalCount.ToString());
        topicData.ThumbData = EditorGUILayout.TextField("ThumbData", topicData.ThumbData);
        EditorGUILayout.Toggle("Complete", topicData.Complete);
        topicData.Updateable = EditorGUILayout.Toggle("Updateable", topicData.Updateable);
        topicData.IsLocked = EditorGUILayout.Toggle("IsLocked", topicData.IsLocked);
        EditorGUILayout.TextField("LastUpdated", topicData.LastUpdated.ToString());

        if (topicData.IsLocked)
        {
            ShowUnlockCondition(topicData.UnlockCondition);
        }

        if (topicData.PixelArtDatas != null && topicData.PixelArtDatas.Count > 0)
        {
            var pixelArtDatasCopy = new List<PixelArtData>(topicData.PixelArtDatas);
            foreach (var pixelArtData in pixelArtDatasCopy)
            {
                if (!_foldOutPixelStatus.ContainsKey(pixelArtData))
                {
                    _foldOutPixelStatus[pixelArtData] = false;
                }

                _foldOutPixelStatus[pixelArtData] =
                    EditorGUILayout.Foldout(_foldOutPixelStatus[pixelArtData], pixelArtData.Title);

                if (_foldOutPixelStatus[pixelArtData])
                {
                    EditorGUI.indentLevel++;
                    ShowPixelArtData(pixelArtData);
                    EditorGUI.indentLevel--;
                }
            }
        }

        // TopicData 삭제 버튼
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Delete"))
        {
            DeleteTopicData(topicData.ID);
            _firestoreTopicDatas.Remove(topicData);
            _foldOutTopicStatus.Remove(topicData.ID);
            Repaint();

            EditorGUILayout.EndHorizontal();
            return;
        }

        // PixelArtData 추가 버튼
        if (GUILayout.Button("Add PixelArtData"))
        {
            // _isAddingPixelArtData = true;
            PixelArtDataEditWindow.Open(newPixelArtData =>
            {
                topicData.PixelArtDatas.Add(newPixelArtData);
                _foldOutPixelStatus[newPixelArtData] = true;

                if (string.IsNullOrEmpty(topicData.ThumbData) && newPixelArtData.ThumbnailData.Length > 0)
                {
                    topicData.ThumbData = topicData.PixelArtDatas[0].ThumbnailData;
                }

                topicData.TotalCount++;
                Repaint();
            });
        }

        EditorGUILayout.EndHorizontal();

        if (_deletePixelArtData != null)
        {
            if (topicData.TotalCount > 0)
            {
                topicData.TotalCount--;
            }

            topicData.PixelArtDatas.Remove(_deletePixelArtData);
            _foldOutPixelStatus.Remove(_deletePixelArtData);
            _deletePixelArtData = null; // Don't forget to reset the reference
            Repaint();
        }

        EditorGUI.indentLevel--;
    }

    private void ShowUnlockCondition(UnlockCondition unlockCondition)
    {
        unlockCondition.Type = (UnlockType)EditorGUILayout.EnumPopup("Unlock Type", unlockCondition.Type);
        unlockCondition.Count = EditorGUILayout.IntField("Unlock Count", unlockCondition.Count);
    }

    private void ShowPixelArtData(PixelArtData pixelArtData)
    {
        if (GUILayout.Button("Delete PixelArtData"))
        {
            _deletePixelArtData = pixelArtData;
            return;
        }

        pixelArtData.Title = EditorGUILayout.TextField("Title", pixelArtData.Title);
        EditorGUILayout.TextField("ThumbnailData", pixelArtData.ThumbnailData);
        EditorGUILayout.TextField("PlayTime", pixelArtData.PlayTime.ToString());
        EditorGUILayout.TextField("Size", pixelArtData.Size.ToString());
        EditorGUILayout.Toggle("IsCompleted", pixelArtData.IsCompleted);
        EditorGUILayout.LabelField("Description");
        EditorGUILayout.TextArea(pixelArtData.Description,GUILayout.Height(100));
        pixelArtData.Difficulty = (Difficulty)EditorGUILayout.EnumPopup("Difficulty", pixelArtData.Difficulty);
        ShowPixelColorData(pixelArtData.PixelColorData);
    }

    private void ShowPixelColorData(PixelColorData pixelColorData)
    {
        pixelColorData.RemainingPixels =
            EditorGUILayout.IntField("Remaining Pixels", pixelColorData.RemainingPixels);
    }

    #region FireStore

    private async Task LoadTopicDatas()
    {
        _firestoreTopicDatas = await _firestore.GetAllData<TopicData>(FirestoreCollections.GalleryData);
        _firestoreTopicDatas.Sort(new TopicDataListComparer());
        Debug.Log("Data Load");
        Debug.Log($"Load Topic Data Count : {_firestoreTopicDatas.Count}");
    }

    private async void DeleteTopicData(string id)
    {
        if (await _firestore.CheckDocumentExists(FirestoreCollections.GalleryData, id))
        {
            Debug.Log($"토픽 데이터 제거 완료 ID : {id}");
            await _firestore.DeleteData(FirestoreCollections.GalleryData, id);
        }
    }

    private async void UploadToFirestore()
    {
        foreach (var topicData in _firestoreTopicDatas)
        {
            topicData.LastUpdated = DateTime.Now;

            if (topicData.ID.StartsWith("_tempTopic_"))
            {
                var docRef = await _firestore.AddData<TopicData>(FirestoreCollections.GalleryData, topicData);
                topicData.ID = docRef.Id;
                Debug.Log($"{topicData.ID} 추가 완료");

                await _firestore.UpdateData<TopicData>(FirestoreCollections.GalleryData, topicData.ID, topicData);
            }
            else
            {
                await _firestore.UpdateData<TopicData>(FirestoreCollections.GalleryData, topicData.ID, topicData);
            }
        }

        _firestoreTopicDatas.Clear();
        await LoadTopicDatas();
    }

    #endregion
}