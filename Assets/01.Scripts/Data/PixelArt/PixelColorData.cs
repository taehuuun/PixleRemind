using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData]
public class PixelColorData
{
    [FirestoreProperty] public int RemainingPixels { get; private set; }

    [FirestoreProperty] public List<CustomPixel> CustomPixels { private get; set; }

    public PixelColorData()
    {
        RemainingPixels = 0;
        CustomPixels = new List<CustomPixel>();
    }

    public void SetRemainingPixelCount(int count)
    {
        RemainingPixels = count;
    }

    public void DecreaseRemainingPixelCount()
    {
        if (RemainingPixels == 0)
        {
            return;
        }

        RemainingPixels--;
    }
    public void RemoveCustomPixel(int index)
    {
        if(!IsValidIndexRange(index))
            return;
        
        CustomPixels.RemoveAt(index);
    }
    public int GetCustomPixelCount()
    {
        return CustomPixels.Count;
    }
    public CustomPixel GetCustomPixel(int index)
    {
        if (!IsValidIndexRange(index))
        {
            return null;
        }

        return CustomPixels[index];
    }
    private bool IsValidIndexRange(int index)
    {
        return index >= 0 && index < CustomPixels.Count;
    }
}