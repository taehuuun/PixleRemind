using System;
using System.Collections.Generic;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using UnityEngine;

namespace LTH.ColorMatch.Utill
{
    public class PixelArtUtill
    {
        private static PixelColorData ExtractPixelData(Texture2D extractTarget)
        {
            PixelColorData pixelDatas = new PixelColorData();
            
            for (int y = 0; y < extractTarget.height; y++)
            {
                // List<CustomColor> rowPixelDatas = new List<CustomColor>();
                
                for (int x = 0; x < extractTarget.width; x++)
                {
                    Color getPixelColor = extractTarget.GetPixel(x, y);
                    
                    if (getPixelColor.a >= 1)
                    {
                        CustomPixel customPixel = new CustomPixel(getPixelColor.r,getPixelColor.g,getPixelColor.b,getPixelColor.a,x,y);

                        pixelDatas.CustomPixels.Add(customPixel);
                        pixelDatas.RemainingPixels++;
                    }
                }

                // if (rowPixelDatas.Count > 0)
                // {
                //     pixelDatas.Pixels.Add(rowPixelDatas);
                // }
            }
            
            // pixelDatas.Pixels.Reverse();
            
            return pixelDatas;
        }

        public static string ExtractThumbnailData(PixelColorData pixelColorData, int size)
        {
            Texture2D thumbNail = new Texture2D(size, size, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };
            
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    thumbNail.SetPixel(x, y, new Color(0,0,0,0));
                }
            }

            for (int i = 0; i < pixelColorData.CustomPixels.Count; i++)
            {
                CustomPixel customPixel = pixelColorData.CustomPixels[i];
                ColorMatchColor pixelColor = customPixel.IsFeel
                    ? customPixel.OriginColorMatchColor
                    : customPixel.GrayColorMatchColor;
                Color setColor = new Color(pixelColor.R, pixelColor.G, pixelColor.B, pixelColor.A);

                thumbNail.SetPixel(customPixel.X, customPixel.Y, setColor);
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
        // ReSharper disable Unity.PerformanceAnalysis
        public static PixelArtData ExportPixelData(GalleryTopic topic, string title, Texture2D pixelArtImg, Difficulty difficulty)
        {
            PixelColorData pixelColorData = ExtractPixelData(pixelArtImg);
            string thumbData = ExtractThumbnailData(pixelColorData, pixelArtImg.width);
            PixelArtData newData = new PixelArtData(topic, title, thumbData, pixelArtImg.width, 0, false, difficulty,
                pixelColorData);
            return newData;
        }
    }
}
