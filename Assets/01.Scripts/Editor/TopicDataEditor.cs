using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTH.PixelRemind.Data;
using LTH.PixelRemind.Enums;
using Firebase.Firestore;

public class TopicDataEditor : EditorWindow
{
    private List<Texture2D> _textureList = new List<Texture2D>();
    private string _topicId;
    private Difficulty _topicDifficulty = Difficulty.Easy;
    private FirebaseFirestore db;
    private TopicData _topicData;
    private bool _topicDataExists;
    
    [MenuItem("Window/Topic Data Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TopicDataEditor));
    }

    private async void OnGUI()
    {
        db = FirebaseFirestore.DefaultInstance;

        if (string.IsNullOrEmpty(_topicId))
        {
            _topicDataExists = false;
        }
        
        GUILayout.Label("Topic Data Editor", EditorStyles.boldLabel);

        GUILayout.Space(10f);

        // _topicId = EditorGUILayout.Popup("Topic Title", _topicId);
        _topicDifficulty = (Difficulty)EditorGUILayout.EnumPopup("Difficulty", _topicDifficulty);

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("Add Textures to Convert");

        EditorGUI.indentLevel++;

        for (int i = 0; i < _textureList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            _textureList[i] = (Texture2D)EditorGUILayout.ObjectField(_textureList[i], typeof(Texture2D), false);

            if (GUILayout.Button("Remove"))
            {
                _textureList.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUI.indentLevel--;

        GUILayout.Space(10f);

        if (GUILayout.Button("Add Texture"))
        {
            _textureList.Add(null);
        }

        GUILayout.Space(10f);

        GUI.enabled = !string.IsNullOrEmpty(_topicId); // TopicType이 None이 아니면 버튼 활성화

        if (GUILayout.Button("Check"))
        {
            // Check버튼을 통해 해당하는 TopicData가 Firestore에 존재하는 체크 하는 코드
            EditorUtility.DisplayProgressBar("Checking", "Checking Topic Data...",0f);

            _topicDataExists = await CheckDocumentExists("GalleryData", _topicId.ToString());      

            Debug.Log(_topicDataExists);

            EditorUtility.ClearProgressBar();
        }

        GUI.enabled = (_topicDataExists); // 버튼 활성화

        GUILayout.Space(10f);

        if (GUILayout.Button("View"))
        {
            if (_topicDataExists)
            {
                EditorUtility.DisplayProgressBar("Loading", "Loading Topic Data...",0f);
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                _topicData = await GetTopicData("GalleryData", _topicId.ToString());
                EditorUtility.ClearProgressBar();

                GUILayout.Label("Topic Data Editor", EditorStyles.boldLabel);

                if (_topicData != null)
                {
                    EditorGUILayout.LabelField("Topic Name", _topicData.ID);

                    // GUILayout.Label("Textures", EditorStyles.boldLabel);
                    //
                    // foreach (var pixelArtData in _topicData.pixelArtDatas)
                    // {
                    //     GUILayout.Label(pixelArtData);
                    // }
                }
                else
                {
                    EditorGUILayout.HelpBox("Failed to load topic data.", MessageType.Info);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Please select a topic and click the Check button.", MessageType.Info);
            }
        }
    }
    private async Task<bool> CheckDocumentExists(string collection, string document)
    {
        var docRef = db.Collection(collection).Document(document);
        var docSnapShot = await docRef.GetSnapshotAsync();

        return docSnapShot.Exists;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private async Task<TopicData> GetTopicData(string collection, string document)
    {
        var docRef = db.Collection(collection).Document(document);
        var docSnapShot = await docRef.GetSnapshotAsync();

        if (!docSnapShot.Exists)
        {
            Debug.LogError("Document does not Exists");
            return null;
        }

        TopicData newLoadData = docSnapShot.ConvertTo<TopicData>(); 
       
        return newLoadData;
    }
}
