using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using Newtonsoft.Json;

public class TopicDataEditor : EditorWindow
{
    private List<Texture2D> _textureList = new List<Texture2D>();
    private GalleryTopic _topicType;
    private Difficulty _topicDifficulty = Difficulty.Easy;

    [MenuItem("Window/Topic Data Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TopicDataEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Topic Data Editor", EditorStyles.boldLabel);

        GUILayout.Space(10f);

        _topicType = (GalleryTopic)EditorGUILayout.EnumPopup("Topic Title", _topicType);
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
        
        GUI.enabled = (_topicType != GalleryTopic.None); // TopicType이 None이 아니면 버튼 활성화

        if (GUILayout.Button("Check"))
        {
            string path = Path.Combine(DataManager.GalleryDataPath, "Topics");
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            TopicData topicData = DataManager.LoadJsonData<TopicData>(path, _topicType.ToString());
            
            if (topicData != null)
            {
                // topicData = (TopicData)EditorGUILayout.ObjectField("Topic Data", topicData, typeof(TopicData), false);

            }
        }

        GUI.enabled = true; // 버튼 활성화

        GUILayout.Space(10f);

        if (GUILayout.Button("Create"))
        {
            List<PixelArtData> pixelArtDataList = new List<PixelArtData>();

            for (int i = 0; i < _textureList.Count; i++)
            {
                PixelArtData pixelArtData = PixelArtUtill.ExportPixelData(new GalleryTopic(), _textureList[i].name, _textureList[i], _topicDifficulty);
                pixelArtDataList.Add(pixelArtData);
            }

            // TopicData topicData = new TopicData();
            // topicData.topic = _topicType;
            // topicData.thumbData = pixelArtDataList[0].thumbData;
            // topicData.completeCount = 0;
            // topicData.totalCount = pixelArtDataList.Count;
            // topicData.complete = false;
            // topicData.pixelArtDatas = pixelArtDataList;
            //
            // string json = JsonConvert.SerializeObject(topicData);
            
            string path = Path.Combine(DataManager.GalleryDataPath, "Topics");

            if (path.Length > 0)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                // DataManager.SaveJsonData(path,_topicType.ToString() ,json);
                AssetDatabase.Refresh();
            }
        }
    }
}
