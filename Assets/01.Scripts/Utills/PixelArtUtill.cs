using System;
using System.Collections.Generic;
using System.Linq;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using UnityEngine;

namespace LTH.ColorMatch.Utill
{
    public class PixelArtUtill
    {
        private static PixelColorData ExtractPixelData(Texture2D extractTarget)
        {
            PixelColorData pixelColorData = new PixelColorData();
            Dictionary<string, CustomPixel> colorGroups = new Dictionary<string, CustomPixel>();

            for (int y = 0; y < extractTarget.height; y++)
            {
                for (int x = 0; x < extractTarget.width; x++)
                {
                    Color getPixelColor = extractTarget.GetPixel(x, y);
                    
                    if (getPixelColor.a >= 1)
                    {
                        string colorKey = $"{getPixelColor.r}_{getPixelColor.g}_{getPixelColor.b}_{getPixelColor.a}";

                        if (!colorGroups.ContainsKey(colorKey))
                        {
                            CustomPixel customPixel = new CustomPixel(getPixelColor.r, getPixelColor.g, getPixelColor.b,
                                getPixelColor.a);
                            colorGroups[colorKey] = customPixel;
                        }

                        PixelCoord coord = new PixelCoord(x, y);
                        colorGroups[colorKey].PixelCoords.Add(coord);
                        pixelColorData.RemainingPixels++;
                    }
                }
            }

            pixelColorData.CustomPixels = colorGroups.Values.ToList();
            
            return pixelColorData;
        }
        public static string ExtractThumbnailData(Texture2D pixelArtThumbnail)
        {
            pixelArtThumbnail.filterMode = FilterMode.Point;
            byte[] bytes = pixelArtThumbnail.EncodeToPNG();
            string textureData = Convert.ToBase64String(bytes);
            return textureData;
        }
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
                    
                    grayThumbnail.SetPixel(x,y,grayColor);
                }
            }
            
            grayThumbnail.Apply();
            byte[] bytes = grayThumbnail.EncodeToPNG();
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
            PixelColorData pixelColorData = ExtractPixelData(pixelArtImg);
            string thumbData = ExtractGrayThumbnailData(pixelArtImg);
            PixelArtData newData = new PixelArtData(topic, title, thumbData, pixelArtImg.width, 0, false, difficulty,
                pixelColorData);
            return newData;
        }
    }
}
