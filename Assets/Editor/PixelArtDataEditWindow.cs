using System;
using UnityEditor;
using UnityEngine;

public class PixelArtDataEditWindow : EditorWindow
{
    private string _tmpTitleID;
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
        _tmpTitleID = EditorGUILayout.TextField("TitleID", _tmpTitleID);
        _tmpPixelArt = (Texture2D)EditorGUILayout.ObjectField("Pixel Art", _tmpPixelArt, typeof(Texture2D));
        _tmpDifficulty = (Difficulty)EditorGUILayout.EnumPopup("Difficulty", _tmpDifficulty);

        if (GUILayout.Button("Confirm"))
        {
            if (_tmpPixelArt != null && !string.IsNullOrEmpty(_tmpTitleID))
            {
                var newPixelArtData = PixelArtUtil.ExportPixelData(_tmpTitleID, _tmpPixelArt, _tmpDifficulty);
                _onConfirm?.Invoke(newPixelArtData);
                Close();
            }
        }
    }
}