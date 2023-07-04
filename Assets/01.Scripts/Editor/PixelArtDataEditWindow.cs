using System;
using UnityEditor;
using UnityEngine;

public class PixelArtDataEditWindow : EditorWindow
{
    private string _tmpTitleID;
    private string _tmpDescription;
    private Texture2D _tmpPixelArt;
    private Difficulty _tmpDifficulty;
    private Action<PixelArtData> _onConfirm;

    public static void Open(Action<PixelArtData> onConfirm)
    {
        PixelArtDataEditWindow window = (PixelArtDataEditWindow)GetWindow(typeof(PixelArtDataEditWindow));
        window.titleContent.text = "Add PixelArtData";
        window._onConfirm = onConfirm;
        window.Show();
    }

    private void OnGUI()
    {
        _tmpTitleID = EditorGUILayout.TextField("Title", _tmpTitleID);
        _tmpPixelArt = (Texture2D)EditorGUILayout.ObjectField("Pixel Art", _tmpPixelArt, typeof(Texture2D));
        _tmpDifficulty = (Difficulty)EditorGUILayout.EnumPopup("Difficulty", _tmpDifficulty);
        
        EditorGUILayout.LabelField("Description");
        _tmpDescription = EditorGUILayout.TextArea(_tmpDescription, GUILayout.Height(100));

        if (GUILayout.Button("Confirm"))
        {
            if (string.IsNullOrEmpty(_tmpTitleID) || string.IsNullOrEmpty(_tmpDescription) || _tmpPixelArt == null || _tmpDifficulty == null)
            {
                EditorUtility.DisplayDialog("실패", "각 필드의 값들을 모두 채워 주세요", "확인");
            }
            else
            {
                var newPixelArtData = PixelArtHelper.ExportPixelData(_tmpTitleID, _tmpDescription, _tmpPixelArt, _tmpDifficulty);
                _onConfirm?.Invoke(newPixelArtData);
                Close();
            }
        }
    }
}