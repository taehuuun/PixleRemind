using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PixelArtHelper
{
    /// <summary>
    /// 입력 받은 텍스쳐에서 PixelColorData를 추출하는 메서드
    /// </summary>
    /// <param name="extractTarget">추출할 대상 텍스쳐 이미지</param>
    /// <returns>PixelColorData</returns>
    private static PixelColorData ExtractPixelData(Texture2D extractTarget)
    {
        PixelColorData pixelColorData = new PixelColorData();
        
        // 중복 컬러값을 방지 하기 위한 딕셔너리 사용
        Dictionary<string, CustomPixel> colorGroups = new Dictionary<string, CustomPixel>();
        
        // 텍스쳐의 높이 만큼 반복
        for (int y = 0; y < extractTarget.height; y++)
        {
            // 텍스쳐의 너비 만큼 반복
            for (int x = 0; x < extractTarget.width; x++)
            {
                // 텍스쳐의 x,y값에 대한 컬러값 추출
                Color getPixelColor = extractTarget.GetPixel(x, y);
                
                // 픽셀 컬러값중 알파값이 1이상인 경우
                if (getPixelColor.a >= 1)
                {
                    // 각 컬러의 rgba값을 이용한 키 생성
                    string colorKey = $"{getPixelColor.r}_{getPixelColor.g}_{getPixelColor.b}_{getPixelColor.a}";
                    
                    // 딕셔너리에 colorKey가 포함되어 있지 않은 경우만
                    if (!colorGroups.ContainsKey(colorKey))
                    {
                        // Firestore 업로드를 위한 CustomPixel 생성후 추가
                        CustomPixel customPixel = new CustomPixel(getPixelColor.r, getPixelColor.g, getPixelColor.b,
                            getPixelColor.a);
                        colorGroups[colorKey] = customPixel;
                    }
                    
                    // 딕셔너리에 해당하는 키의 픽셀 좌표 리스트 추가
                    PixelCoord coord = new PixelCoord(x, y);
                    colorGroups[colorKey].PixelCoords.Add(coord);
                    pixelColorData.RemainingPixels++;
                }
            }
        }
        
        // 추가된 딕셔너리의 Value값들을 리스트로 변환        
        pixelColorData.CustomPixels = colorGroups.Values.ToList();
        
        return pixelColorData;
    }
    
    /// <summary>
    /// 입력 텍스쳐에서 썸네일을 추출하는 메서드
    /// </summary>
    /// <param name="pixelArtThumbnail">입력 텍스쳐</param>
    /// <returns>변환 된 썸네일 데이터</returns>
    public static string ExtractThumbnailData(Texture2D pixelArtThumbnail)
    {
        pixelArtThumbnail.filterMode = FilterMode.Point;
        byte[] bytes = pixelArtThumbnail.EncodeToPNG();
        string textureData = Convert.ToBase64String(bytes);
        return textureData;
    }
    
    /// <summary>
    /// Sprite를 Texture2D로 변환 해주는 메서드
    /// </summary>
    /// <param name="sprite">변환할 Sprite</param>
    /// <returns>변환된 Texture2D</returns>
    public static Texture2D SpriteToTexture2D(Sprite sprite)
    {
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height);
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
    
    /// <summary>
    /// 입력 받은 텍스쳐에서 흑백 썸네일을 추출하는 메서드
    /// </summary>
    /// <param name="pixelArtThumbnail">입력 텍스쳐</param>
    /// <returns>변환 된 썸네일 데이터</returns>
    private static string ExtractGrayThumbnailData(Texture2D pixelArtThumbnail)
    {
        Texture2D grayThumbnail =
            new Texture2D(pixelArtThumbnail.width, pixelArtThumbnail.height, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };

        for (int y = 0; y < pixelArtThumbnail.height; y++)
        {
            for (int x = 0; x < pixelArtThumbnail.width; x++)
            {
                Color originalColor = pixelArtThumbnail.GetPixel(x, y);

                float grayValue = (originalColor.r + originalColor.g + originalColor.b) / 3;
                Color grayColor = new Color(grayValue, grayValue, grayValue, originalColor.a);

                grayThumbnail.SetPixel(x, y, grayColor);
            }
        }

        grayThumbnail.Apply();
        byte[] bytes = grayThumbnail.EncodeToPNG();
        string textureData = Convert.ToBase64String(bytes);
        return textureData;
    }
    
    /// <summary>
    /// 썸네일 데이터를 Sprite로 변환 하는 메서드
    /// </summary>
    /// <param name="thumbNailData">썸네일 데이터</param>
    /// <param name="size">썸네일의 크기</param>
    /// <returns>변환된 썸네일</returns>
    public static Sprite MakeThumbnail(string thumbNailData, int size)
    {
        Texture2D thumbNail = new Texture2D(size, size, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Point
        };

        byte[] bytes = Convert.FromBase64String(thumbNailData);

        thumbNail.LoadImage(bytes);
        Sprite newSprite = Sprite.Create(thumbNail, new Rect(0, 0, thumbNail.width, thumbNail.height),
            new Vector2(0.5f, 0.5f));

        return newSprite;
    }
    
    /// <summary>
    /// PixelArtData를 추출하는 메서드
    /// </summary>
    /// <param name="titleid">픽셀 아트의 타이틀</param>
    /// <param name="pixelArtImg">픽셀 아트 텍스쳐</param>
    /// <param name="difficulty">난이도</param>
    /// <returns>PixelArtData</returns>
    public static PixelArtData ExportPixelData(string titleid,string description, Texture2D pixelArtImg, Difficulty difficulty)
    {
        PixelColorData pixelColorData = ExtractPixelData(pixelArtImg);
        string thumbData = ExtractGrayThumbnailData(pixelArtImg);
        PixelArtData newData = new PixelArtData(titleid, thumbData,description, 0, pixelArtImg.width, false, difficulty,
            pixelColorData);
        return newData;
    }
}