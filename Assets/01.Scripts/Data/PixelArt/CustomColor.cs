using System;
using Firebase.Firestore;
using UnityEngine;

/// <summary>
/// Firestore 업로드를 위한 커스텀 Color 클래스
/// </summary>
[FirestoreData, Serializable]
public class CustomColor
{
    [FirestoreProperty] public float R { get; set; }
    [FirestoreProperty] public float G { get; set; }
    [FirestoreProperty] public float B { get; set; }
    [FirestoreProperty] public float A { get; set; }

    public CustomColor() : this(0,0,0,0) {}
    public CustomColor(Color color) : this(color.r, color.g,color.b,color.a) {}
    public CustomColor(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}
