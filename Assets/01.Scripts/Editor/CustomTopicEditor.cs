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
            newTopicData.SetTitle(_firestoreTopicDatas.Count.ToString());
            newTopicData.SetID($"_tempTopic_{_firestoreTopicDatas.Count.ToString()}");
            _firestoreTopicDatas.Add(newTopicData);
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

        GUI.enabled = false;
        EditorGUILayout.TextField("ID", topicData.ID);
        GUI.enabled = true;
        topicData.SetTitle(EditorGUILayout.TextField("Title", topicData.Title));
        EditorGUILayout.LabelField("Description");
        topicData.SetDescription(EditorGUILayout.TextArea(topicData.Description, GUILayout.Height(100)));
        GUI.enabled = false;
        EditorGUILayout.TextField("ThumbData", topicData.ThumbnailData);
        EditorGUILayout.TextField("CompleteCount", topicData.CompleteCount.ToString());
        EditorGUILayout.TextField("TotalCount", topicData.GetPixelArtsCount().ToString());
        EditorGUILayout.Toggle("Complete", topicData.Complete);
        GUI.enabled = true;
        topicData.SetUpdateAble(EditorGUILayout.Toggle("Updateable", topicData.Updateable));
        topicData.SetLock(EditorGUILayout.Toggle("IsLocked", topicData.IsLocked));
        GUI.enabled = false;
        EditorGUILayout.TextField("LastUpdated", topicData.LastUpdated.ToString());
        GUI.enabled = true;

        if (topicData.IsLocked)
        {
            ShowUnlockCondition(topicData.UnlockCondition);
        }

        if (topicData.GetPixelArtList() != null && topicData.GetPixelArtsCount() > 0)
        {
            var pixelArtDatasCopy = new List<PixelArtData>(topicData.GetPixelArtList());
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
                topicData.AddPixelArt(newPixelArtData);
                _foldOutPixelStatus[newPixelArtData] = true;

                if (string.IsNullOrEmpty(topicData.ThumbnailData) && newPixelArtData.ThumbnailData.Length > 0)
                {
                    string thumbnailData = topicData.GetPixelArt(0).ThumbnailData;
                    int thumbnailSize = topicData.ThumbnailSize;
                    topicData.UpdateThumbnailData(thumbnailData, thumbnailSize);
                }

                Repaint();
            });
        }

        EditorGUILayout.EndHorizontal();

        if (_deletePixelArtData != null)
        {
            if (topicData.GetPixelArtsCount() > 0)
            {
                topicData.RemovePixelArt(_deletePixelArtData);
                _foldOutPixelStatus.Remove(_deletePixelArtData);
                _deletePixelArtData = null; // Don't forget to reset the reference
            }
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

        GUI.enabled = false;
        pixelArtData.SetID(EditorGUILayout.TextField("ID", pixelArtData.ID));
        pixelArtData.SetTitle(EditorGUILayout.TextField("Title", pixelArtData.Title)); 
        EditorGUILayout.TextField("ThumbnailData", pixelArtData.ThumbnailData);
        EditorGUILayout.TextField("PlayTime", pixelArtData.PlayTime.ToString());
        EditorGUILayout.TextField("ThumbnailSize", pixelArtData.ThumbnailSize.ToString());
        EditorGUILayout.Toggle("IsCompleted", pixelArtData.IsCompleted);
        EditorGUILayout.IntField("Remaining Pixels", pixelArtData.PixelColorData.RemainingPixels);
        EditorGUILayout.LabelField("Description");
        EditorGUILayout.TextArea(pixelArtData.Description,GUILayout.Height(100));
        pixelArtData.SetDifficulty((Difficulty)EditorGUILayout.EnumPopup("Difficulty", pixelArtData.Difficulty));
        GUI.enabled = true;
    }

    #region FireStore

    private async Task LoadTopicDatas()
    {
        _firestoreTopicDatas = await _firestore.GetAllData<TopicData>(FirestoreCollections.GalleryData);

        if (_firestoreTopicDatas == null)
        {
            _firestoreTopicDatas = new List<TopicData>();
        }
        _firestoreTopicDatas.Sort(new TopicDataListComparer());
    }

    private async void DeleteTopicData(string id)
    {
        if (await _firestore.CheckDocumentExists(FirestoreCollections.GalleryData, id))
        {
            await _firestore.DeleteData(FirestoreCollections.GalleryData, id);
        }
    }

    private async void UploadToFirestore()
    {
        foreach (var topicData in _firestoreTopicDatas)
        {
            topicData.SetLastUpdate(DateTime.Now);

            if (topicData.ID.StartsWith("_tempTopic_"))
            {
                // Generate a hash for the topic data ID
                string hashInput = $"{topicData.Title}{topicData.Description}{topicData.ThumbnailData}{topicData.CompleteCount}{topicData.GetPixelArtsCount()}{topicData.Complete}{topicData.Updateable}{topicData.IsLocked}{topicData.LastUpdated}";
                string generatedHash = HashGenerator.GenerateHash(hashInput);
                topicData.SetID(generatedHash);

                // Only add the data with the hashed ID
                await _firestore.AddData(FirestoreCollections.GalleryData, topicData.ID,topicData);
            }
            else
            {
                await _firestore.UpdateData(FirestoreCollections.GalleryData, topicData.ID, topicData);
            }
        }

        _firestoreTopicDatas.Clear();
        await LoadTopicDatas();
    }


    #endregion
}