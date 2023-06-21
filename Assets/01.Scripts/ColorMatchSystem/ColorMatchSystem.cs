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
    
    // _disSimilarRange 감소 값
    public float decRangeValue = 0.05f;
    public int maxLife;
    public float maxDisSimilarRange;
    
    public int Score
    {
        // _score 반환
        get => _score;
        
        // _score수정 후 옵저버에게 변경을 일림
        private set
        {
            _score = value;
            NotifyObservers();
        }
    }
    public int Life
    {
        // _life 반환
        get => _life;
        
        // _life 수정 후 옵저버에게 변경을 알림
        private set
        {
            _life = value;
            NotifyObservers();
        }
    }
    public float DisSimilarRange
    {
        // _disSimilarRange 반환
        get => _disSimilarRange;
        
        // _disSimilarRange 수정 후 옵저버에게 변경을 알림
        private set
        {
            _disSimilarRange = value;
            NotifyObservers();
        }
    }
    public bool IsGameOver
    {
        // _isGameOver 반환
        get => _isGameOver;
        
        // _isGameOver 수정 후 옵저버에게 변경을 알림
        set
        {
            _isGameOver = value;
            NotifyObservers();
        }
    }

    /// <summary>
    /// 게임 재시작 메서드
    /// </summary>
    public void ReStart()
    {
        ResetStats();
        SetNextColorSlot();
    }

    /// <summary>
    /// 슬롯의 컬러가 현재 타겟 컬러와 일치하는지 체크하는 메서드
    /// </summary>
    /// <param name="slot">클릭된 슬롯</param>
    /// <returns>일치하는지 여부 반환</returns>
    public bool CheckMatch(ColorSlot slot)
    {
        // 타겟 컬러 슬롯과 선택 슬롯의 컬러가 일치하는지 비교
        bool isMatched = (slot.slotImage.color == targetColorSlot.slotImage.color);
        
        // 일치 할경우 OnCorrectMatch 메서드 진행
        // 일치 하지 않을 경고, 기회가 남았을 때 OnIncorrectMatch 메서드 진행
        if (isMatched)
        {
            OnCorrectMatch();
        }
        else if (Life > 0)
        {
            OnIncorrectMatch();
        }
        
        // 일치 여부 반환
        return isMatched;
    }
    
    /// <summary>
    /// 컬러값이 일치할 경우 호출되는 메서드
    /// </summary>
    private void OnCorrectMatch()
    {
        IncreaseScore();
        SetNextColorSlot();
    }
  
    /// <summary>
    /// 컬러값이 일치하지 않는 경우 호출 메서드
    /// </summary>
    private void OnIncorrectMatch()
    {
        // 진동 효과
        Handheld.Vibrate();
        
        // life 감소
        DecreaseLife();
    }
   
    /// <summary>
    /// ColorMatchSystem의 상태를 리셋하는 메서드
    /// </summary>
    private void ResetStats()
    {
        _isGameOver = false;
        Life = maxLife;
        Score = 0;
        DisSimilarRange = maxDisSimilarRange;
    }
   
    /// <summary>
    /// Score를 증가시키는 메서드
    /// </summary>
    private void IncreaseScore()
    {
        Score++;
        
        // DisSimilarRange 값을 감소
        DisSimilarRange = Mathf.Clamp(DisSimilarRange - decRangeValue, 5, 100);
    }
   
    /// <summary>
    /// Life를 감소시키는 메서도
    /// </summary>
    private void DecreaseLife()
    {
        // Life가 0이 아니면 감소
        // Life가 0일 때 IsGameOver프로퍼티 true 변경
        if (Life > 0)
        {
            Life--;

            if (Life == 0)
            {
                IsGameOver = true;
            }
        }
    }
   
    /// <summary>
    /// 현재 컬러 슬롯들의 컬러들을 새로 변경
    /// </summary>
    private void SetNextColorSlot()
    {
        SetRandomTargetColorFromPixelArtData();
        SetRandomSelectColorSlots();
    }
    
    /// <summary>
    /// 현재 픽셀 아트의 컬러중 랜덤하게 선택하여 타겟 컬러를 변경하는 메서드
    /// </summary>
    private void SetRandomTargetColorFromPixelArtData()
    {
        if (pixelColorData == null || pixelColorData.CustomPixels.Count == 0)
        {
            // Debug.LogWarning("No Pixel Art Data Found, Using Random Color");
            // SetRandomTargetColor();
            return;
        }
        
        int randIdx = Random.Range(0, pixelColorData.CustomPixels.Count);
        
        // 랜덤 선택된 CustomPixels의 원본 색상값을 colorValue에 대입
        ColorValue colorValue = pixelColorData.CustomPixels[randIdx].OriginalColor;
        
        // 원본 색상값(ColorValue)을 Color 타입으로 생성
        Color randColor = new Color(colorValue.R, colorValue.G, colorValue.B, colorValue.A);
        
        // 명도 값 조절을 위한 -0.1 ~ 0.1 값을 세팅
        float brightVar = Random.Range(-0.1f, 0.1f);
        
        // randColor의 밝기를 조절
        randColor = AdjustBrightness(randColor, brightVar);
        
        // targetColor 슬롯의 컬러를 수정함
        targetColorSlot.SetSlot(randColor);
    }
    
    private void SetRandomTargetColor()
    {
        var randColor = Random.ColorHSV();
        targetColorSlot.SetSlot(randColor);
    }
    
    /// <summary>
    /// 입력받은 Color의 명도를 조절하는 메서드
    /// </summary>
    /// <param name="color">명도를 수정할 타켓 컬러</param>
    /// <param name="brightnessVar">수정할 명도 값</param>
    /// <returns>명도가 수정된 컬러</returns>
    private Color AdjustBrightness(Color color, float brightnessVar)
    {
        // 색조(Hue), 채도(Saturation), 명도(Value)
        float h, s, v;
        
        // 입력 Color의 RGB값을 HSV로 변환
        Color.RGBToHSV(color, out h, out s, out v);
        
        // v 값에 입력받은 명도 값을 더함 (범위, 0.0 ~ 1.0)
        v = Mathf.Clamp(v + brightnessVar, 0f, 1f);
        
        // HSV값을 RGB로 변환후 newColor에 대입
        Color newColor = Color.HSVToRGB(h, s, v);
        
        // 수정된 컬러를 반환
        return newColor;
    }
    
    /// <summary>
    /// 선택 슬롯들의 컬러 값을 랜덤하게 수정하는 메서드
    /// </summary>
    private void SetRandomSelectColorSlots()
    {
        // 현재 추가 되어있는 선택 슬롯들중 한개 index 선택
        int answerIdx = Random.Range(0, selectColorSlots.Count);
        
        for (int i = 0; i < selectColorSlots.Count; i++)
        {
            Color randColor;
            
            // 선택된 idx의 슬롯은 타겟 슬롯의 컬러로 대입
            // 나머지는 타겟 슬롯의 컬러와 유사한 색으로 랜덤 대입
            if (i == answerIdx)
            {
                randColor = targetColorSlot.slotImage.color;
            }
            else
            {
                randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color, DisSimilarRange);
                
                // 만약 랜덤 컬러가 타겟 슬롯 컬러와 같을 경우 컬러 재설정
                while (randColor == targetColorSlot.slotImage.color)
                {
                    randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color, DisSimilarRange);
                }
            }   
            
            // randColor로 해당하는 슬롯 세팅
            selectColorSlots[i].SetSlot(randColor);
        }
    }
    
    /// <summary>
    /// 타겟 슬롯 컬러와 유사한 색을 반환하는 메서드
    /// </summary>
    /// <param name="targetColor">타겟의 컬러</param>
    /// <param name="variance">유사 정도 값</param>
    /// <returns>타겟 컬러와 유사한 컬러 반환</returns>
    private Color GetRandomSimilarColor(Color targetColor, float variance)
    {
        // 매개변수의 값은 0 ~ 100값이기 때문에 0.01를 곱한다
        float range = variance * 0.01f;
        
        // 각 요소별 값에서 range만큼 뺀값을 최소, 더한값을 최대로 하여 핸덤하게 값을 설정 (범위, 0.0 ~ 0.1)
        float r = Mathf.Clamp(Random.Range(targetColor.r - range, targetColor.r + range), 0f, 1f);
        float g = Mathf.Clamp(Random.Range(targetColor.g - range, targetColor.g + range), 0f, 1f);
        float b = Mathf.Clamp(Random.Range(targetColor.b - range, targetColor.b + range), 0f, 1f);
        
        // 계산된 rgb 값을 이용하여 새로운 컬러를 반환
        return new Color(r, g, b);
    }
    
    /// <summary>
    /// 옵저버를 등록하는 메서드
    /// </summary>
    /// <param name="observer">등록할 옵저버</param>
    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }
    
    /// <summary>
    /// 옵저버를 제거하는 메서드
    /// </summary>
    /// <param name="observer">제거할 옵저버</param>
    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    /// <summary>
    /// 등록된 옵저버들에게 상태를 업데이트 하는 메서드
    /// </summary>
    public void NotifyObservers()
    {
        foreach (IObserver observer in observers)
        {
            observer.UpdateSubjectState();
        }
    }
}