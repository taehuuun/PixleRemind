using System;
using Firebase.Firestore;

/// <summary>
/// Firestore 업로드를 위한 커스텀 Color 클래스
/// </summary>
[FirestoreData, Serializable]
public class CustomRGBA
{
    [FirestoreProperty] public float R { get; set; }
    [FirestoreProperty] public float G { get; set; }
    [FirestoreProperty] public float B { get; set; }
    [FirestoreProperty] public float A { get; set; }

    public CustomRGBA()
    {
        R = 0;
        G = 0;
        B = 0;
        A = 0;
    }

    public CustomRGBA(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}
