using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorMatchSystem : MonoBehaviour, ISubject
{
    // 현재 보여질 픽셀 아트의 데이터
    public PixelColorData pixelColorData;
    
    // 현제 픽셀 아트 데이터의 컬러 슬롯 UI
    public ColorSlot targetColorSlot;
    
    // 컬러 슬롯 UI
    public List<ColorSlot> selectColorSlots;

    // UI 업데이트를 위한 옵저버 리스트
    public List<IObserver> observers = new List<IObserver>();
    
    private int _score;
    private int _life;
    private float _disSimilarRange;
    private bool _isGameOver = false;

    public float decRangeValue = 0.05f;
    public int maxLife;
    public float maxDisSimilarRange;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            NotifyObservers();
        }
    }

    public int Life
    {
        get => _life;
        private set
        {
            _life = value;
            NotifyObservers();
        }
    }

    public float DisSimilarRange
    {
        get => _disSimilarRange;
        private set
        {
            _disSimilarRange = value;
            NotifyObservers();
        }
    }

    public bool IsGameOver
    {
        get => _isGameOver;
        set
        {
            _isGameOver = value;
            Debug.Log($"TEST : {_isGameOver}");
            NotifyObservers();
        }
    }

    public void ReStart()
    {
        ResetStats();
        GenerateNewPuzzle();
    }

    public bool CheckMatch(ColorSlot slot)
    {
        bool isMatched = (slot.slotImage.color == targetColorSlot.slotImage.color);

        if (isMatched)
        {
            OnCorrectMatch();
        }
        else if (Life > 0)
        {
            OnIncorrectMatch();
        }

        return isMatched;
    }

    private void OnCorrectMatch()
    {
        IncreaseScore();
        GenerateNewPuzzle();
    }

    private void OnIncorrectMatch()
    {
        Handheld.Vibrate();
        DecreaseLife();
    }

    private void ResetStats()
    {
        _isGameOver = false;
        Life = maxLife;
        Score = 0;
        DisSimilarRange = maxDisSimilarRange;
    }

    private void IncreaseScore()
    {
        Score++;
        DisSimilarRange = Mathf.Clamp(DisSimilarRange - decRangeValue, 5, 100);
    }

    private void DecreaseLife()
    {
        if (Life > 0)
        {
            Life--;

            if (Life == 0)
            {
                IsGameOver = true;
            }
        }
    }

    private void GenerateNewPuzzle()
    {
        SetRandomTargetColorFromPixelArtData();
        SetRandomSelectColorSlots();
    }

    private void SetRandomTargetColorFromPixelArtData()
    {
        if (pixelColorData == null || pixelColorData.CustomPixels.Count == 0)
        {
            Debug.LogWarning("No Pixel Art Data Found, Using Random Color");
            SetRandomTargetColor();
            return;
        }

        int randIdx = Random.Range(0, pixelColorData.CustomPixels.Count);
        ColorValue colorValue = pixelColorData.CustomPixels[randIdx].OriginalColor;
        Color randColor = new Color(colorValue.R, colorValue.G, colorValue.B, colorValue.A);

        float brightVar = Random.Range(-0.1f, 0.1f);
        randColor = AdjustBrightness(randColor, brightVar);

        targetColorSlot.SetSlot(randColor);
    }

    private void SetRandomTargetColor()
    {
        var randColor = Random.ColorHSV();
        targetColorSlot.SetSlot(randColor);
    }

    private Color AdjustBrightness(Color color, float brightnessVar)
    {
        float h, s, v;

        Color.RGBToHSV(color, out h, out s, out v);
        v = Mathf.Clamp(v + brightnessVar, 0f, 1f);
        Color newColor = Color.HSVToRGB(h, s, v);

        return newColor;
    }

    private void SetRandomSelectColorSlots()
    {
        int answerIdx = Random.Range(0, selectColorSlots.Count);

        for (int i = 0; i < selectColorSlots.Count; i++)
        {
            Color randColor;

            if (i == answerIdx)
            {
                randColor = targetColorSlot.slotImage.color;
            }
            else
            {
                randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color, DisSimilarRange);

                while (randColor == targetColorSlot.slotImage.color)
                {
                    randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color, DisSimilarRange);
                }
            }

            selectColorSlots[i].SetSlot(randColor);
        }
    }

    private Color GetRandomSimilarColor(Color targetColor, float variance)
    {
        float range = variance * 0.01f;

        float r = Mathf.Clamp(Random.Range(targetColor.r - range, targetColor.r + range), 0f, 1f);
        float g = Mathf.Clamp(Random.Range(targetColor.g - range, targetColor.g + range), 0f, 1f);
        float b = Mathf.Clamp(Random.Range(targetColor.b - range, targetColor.b + range), 0f, 1f);
        return new Color(r, g, b);
    }

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in observers)
        {
            observer.UpdateSubjectState();
        }
    }
}