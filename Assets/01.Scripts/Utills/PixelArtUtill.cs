using System;
using System.Collections.Generic;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using UnityEngine;

namespace LTH.ColorMatch.Utill
{
    public class PixelArtUtill
    {
        private static ColorData ExtractPixelData(Texture2D extractTarget)
        {
            ColorData pixelDatas = new ColorData();
            
            for (int y = 0; y < extractTarget.height; y++)
            {
                List<CustomColor> rowPixelDatas = new List<CustomColor>();
                
                for (int x = 0; x < extractTarget.width; x++)
                {
                    Color getPixelColor = extractTarget.GetPixel(x, y);

                    CustomColor customColor = new CustomColor(getPixelColor.r,getPixelColor.g,getPixelColor.b,getPixelColor.a);
                    rowPixelDatas.Add(customColor);

                    if (getPixelColor.a >= 1)
                        pixelDatas.remainPixel++;
                }
                
                if(rowPixelDatas.Count > 0)                
                    pixelDatas.Pixels.Add(rowPixelDatas);
            }
            
            pixelDatas.Pixels.Reverse();
            
            return pixelDatas;
        }

        public static string ExtractThumbnailData(ColorData colorData, int size)
        {
            Texture2D thumbNail = new Texture2D(size, size, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };
            
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    CustomColor customColor = colorData.Pixels[y][x];
                    ColorMatchColor pixelColor = customColor.isFeel
                        ? customColor.originColorMatchColor
                        : customColor.grayColorMatchColor;
                    Color setColor = new Color(pixelColor.r, pixelColor.g, pixelColor.b, pixelColor.a);
                    thumbNail.SetPixel(x, y, setColor);
                }
            }
            
            thumbNail.Apply();
            
            byte[] bytes = thumbNail.EncodeToPNG();
            string textureData = Convert.ToBase64String(bytes);
            return textureData;
        }
        public static Sprite MakeThumbnail(string thumbNailData, int size)
        {
            Texture2D thumbNail = new Texture2D(size, size, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };

            byte[] bytes = Convert.FromBase64String(thumbNailData);

            thumbNail.LoadImage(bytes);
            Sprite newSprite = Sprite.Create(thumbNail, new Rect(0, 0, thumbNail.width, thumbNail.height), new Vector2(0.5f,0.5f));
            
            return newSprite;
        }
        public static PixelArtData ExportPixelData(GalleryTopic topic, string title, Texture2D pixelArtImg, Difficulty difficulty)
        {
            ColorData colorData = ExtractPixelData(pixelArtImg);
            string thumbData = ExtractThumbnailData(colorData, pixelArtImg.width);
            PixelArtData newData = new PixelArtData(topic, title, thumbData, pixelArtImg.width, 0, false, difficulty,
                colorData);
            
            return newData;
        }
    }
}
