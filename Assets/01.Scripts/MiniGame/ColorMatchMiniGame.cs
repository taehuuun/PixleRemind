using System.Collections.Generic;
using UnityEngine;

public class ColorMatchMiniGame : MonoBehaviour
{
    [SerializeField] private ColorSlot targetColorSlot;
    [SerializeField] private List<ColorSlot> selectColorSlots;
    
    [SerializeField] private int maxLife;
    [SerializeField] private float decRangeValue = 0.05f;
    [SerializeField] private float maxDisSimilarRange;
    
    private PixelArtData _pixelArtData;

    private int _life;
    private float _disSimilarRange;

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
    public void CheckMatch(ColorSlot slot)
    {
        if (slot.slotImage.color == targetColorSlot.slotImage.color)
        {
            OnCorrectMatch();
        }
        else if (_life > 0)
        {
            OnIncorrectMatch();
        }
    }

    public bool IsGameOver()
    {
        return _life == 0 || _pixelArtData.IsCompleted;
    }

    public void SetPixelArtData(PixelArtData pixelArtData)
    {
        _pixelArtData = pixelArtData;
    }

#region 메인 미니 게임 로직

    /// <summary>
    /// 컬러값이 일치할 경우 호출되는 메서드
    /// </summary>
    private void OnCorrectMatch()
    {
        FillRandomPixel();
        DisSimilarRange();
        SetNextColorSlot();
    }
  
    /// <summary>
    /// 컬러값이 일치하지 않는 경우 호출 메서드
    /// </summary>
    private void OnIncorrectMatch()
    {
        Handheld.Vibrate();
        
        DecreaseLife();
    }

    private void FillRandomPixel()
    {
        if (NoRemainingPixels()) return;
        
        int randomPixelIndex = SelectRandomPixelIndex();
        CustomPixel selectedPixel = GetSelectedPixel(randomPixelIndex);
        int selectedCoord = Random.Range(0, selectedPixel.GetPixelCoordCount());

        Texture2D pixelArt = GetPixelArtTexture();
        Color origin = GetOriginalColor(selectedPixel);

        ApplyPixelColor(pixelArt, selectedPixel, selectedCoord, origin);
        UpdatePixelData(selectedPixel, selectedCoord, randomPixelIndex);
        UpdateThumbnailData(pixelArt);
        CheckComplete();
    }
    #region  FillRandomPixel 관련 메서드

    private bool NoRemainingPixels()
    {
        return _pixelArtData.PixelColorData.RemainingPixels <= 0;
    }

    private int SelectRandomPixelIndex()
    {
        return Random.Range(0, _pixelArtData.PixelColorData.GetCustomPixelCount());
    }

    private CustomPixel GetSelectedPixel(int index)
    {
        return _pixelArtData.PixelColorData.GetCustomPixel(index);
    }

    private Texture2D GetPixelArtTexture()
    {
        Sprite sprite = PixelArtHelper.MakeThumbnail(_pixelArtData.ThumbnailData, _pixelArtData.ThumbnailSize);
        return PixelArtHelper.SpriteToTexture2D(sprite);
    }

    private Color GetOriginalColor(CustomPixel selectedPixel)
    {
        return new Color(selectedPixel.OriginalColor.R, selectedPixel.OriginalColor.G, selectedPixel.OriginalColor.B,
            selectedPixel.OriginalColor.A);
    }

    private void ApplyPixelColor(Texture2D pixelArt, CustomPixel selectedPixel, int selectedCoord, Color origin)
    {
        PixelCoord selectedPixelCoord = selectedPixel.GetPixelCoord(selectedCoord);
        pixelArt.SetPixel(selectedPixelCoord.X,selectedPixelCoord.Y, origin);
        pixelArt.Apply();
    }

    private void UpdatePixelData(CustomPixel selectedPixel, int selectedCoord, int rnadomPixelIndex)
    {
        selectedPixel.RemovePixelCoord(selectedCoord);
        
        if (selectedPixel.GetPixelCoordCount() == 0)
        {
            _pixelArtData.PixelColorData.RemoveCustomPixel(rnadomPixelIndex);
        }

        _pixelArtData.PixelColorData.DecreaseRemainingPixelCount();
    }

    private void UpdateThumbnailData(Texture2D pixelArt)
    {
        string thumbnailData = PixelArtHelper.ExtractThumbnailData(pixelArt);
        int thumbnailSize = _pixelArtData.ThumbnailSize;
        _pixelArtData.UpdateThumbnailData(thumbnailData,thumbnailSize); 
    }

    private void CheckComplete()
    {
        if (_pixelArtData.PixelColorData.RemainingPixels == 0)
        {
            _pixelArtData.SetIsCompleted(true);
        }
    }
    #endregion
    
    /// <summary>
    /// ColorMatchSystem의 상태를 리셋하는 메서드
    /// </summary>
    private void ResetStats()
    {
        _life = maxLife;
        _disSimilarRange = maxDisSimilarRange;
    }
   
    /// <summary>
    /// DisSimilarRange를 감소 시키는 메서드
    /// </summary>
    private void DisSimilarRange()
    {
        // DisSimilarRange 값을 감소
        _disSimilarRange = Mathf.Clamp(_disSimilarRange - decRangeValue, 5, 100);
    }
   
    /// <summary>
    /// Life를 감소시키는 메서도
    /// </summary>
    private void DecreaseLife()
    {
        // Life가 0이 아니면 감소
        if (_life > 0)
        {
            _life--;
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
        if (_pixelArtData.PixelColorData == null || _pixelArtData.PixelColorData.GetCustomPixelCount() == 0)
        {
            return;
        }
        
        int randIdx = Random.Range(0, _pixelArtData.PixelColorData.GetCustomPixelCount());
        
        // 랜덤 선택된 CustomPixels의 원본 색상값을 colorValue에 대입
        CustomColor customColor = _pixelArtData.PixelColorData.GetCustomPixel(randIdx).OriginalColor;
        
        // 원본 색상값(ColorValue)을 Color 타입으로 생성
        Color randColor = new Color(customColor.R, customColor.G, customColor.B, customColor.A);
        
        // 명도 값 조절을 위한 -0.1 ~ 0.1 값을 세팅
        float brightVar = Random.Range(-0.1f, 0.1f);
        
        // randColor의 밝기를 조절
        randColor = AdjustBrightness(randColor, brightVar);
        
        // targetColor 슬롯의 컬러를 수정함
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
                randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color, _disSimilarRange);
                
                // 만약 랜덤 컬러가 타겟 슬롯 컬러와 같을 경우 컬러 재설정
                while (randColor == targetColorSlot.slotImage.color)
                {
                    randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color, _disSimilarRange);
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

    #endregion
}