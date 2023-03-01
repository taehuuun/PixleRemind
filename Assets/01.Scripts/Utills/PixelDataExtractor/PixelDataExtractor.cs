using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using Newtonsoft.Json;

namespace LTH.ColorMatch.Utill
{
    public class PixelDataExtractor : MonoBehaviour
    {
        private static ColorData ExtractPixelData(Texture2D extractTarget)
        {
            ColorData pixelDatas = new ColorData();
            
            for (int y = 0; y < extractTarget.height; y++)
            {
                List<CustomColor> rowPixelDatas = new List<CustomColor>();
                
                for (int x = 0; x < extractTarget.width; x++)
                {
                    CustomColor customColor = new CustomColor(extractTarget.GetPixel(x, y));
                    rowPixelDatas.Add(customColor);
                }
                
                pixelDatas.Pixels.Add(rowPixelDatas);
            }
            
            pixelDatas.Pixels.Reverse();
            
            return pixelDatas;
        }
        private static ColorData ExtractPixelBlackWhiteData(Texture2D extractTarget)
        {
            ColorData pixelDatas = new ColorData();
            for (int y = 0; y < extractTarget.height; y++)
            {
                List<CustomColor> rowPixelDatas = new List<CustomColor>();
                    
                for (int x = 0; x < extractTarget.width; x++)
                {
                    Color getPixelColor = extractTarget.GetPixel(x, y);
                    float grayValue = (getPixelColor.r + getPixelColor.g + getPixelColor.b) / 3;
                    rowPixelDatas.Add(new CustomColor(grayValue,grayValue,grayValue,getPixelColor.a));
                }
                    
                pixelDatas.Pixels.Add(rowPixelDatas);
            }
            pixelDatas.Pixels.Reverse();
            return pixelDatas;
        }

        public static PixelArtData ExportPixelData(GalleryTopic topic, string title, Texture2D pixelArtImg, Difficulty difficulty)
        {
            ColorData colorData = ExtractPixelData(pixelArtImg);
            ColorData blackWhiteData = ExtractPixelBlackWhiteData(pixelArtImg);
            PixelArtData newData = new PixelArtData(topic, title, colorData, blackWhiteData, colorData.Pixels.Count, Difficulty.Easy);
            
            return newData;
        }
        
        // public static Texture2D ResizeTexture(Texture2D originalTexture, int width, int height) {
        //     Texture2D resizedTexture = new Texture2D(width, height);
        //     Color[] resizedPixels = new Color[width * height];
        //     float ratioX = (float)originalTexture.width / width;
        //     float ratioY = (float)originalTexture.height / height;
        //     for (int x = 0; x < width; x++) {
        //         for (int y = 0; y < height; y++) {
        //             int index = y * width + x;
        //             float u = x * ratioX;
        //             float v = y * ratioY;
        //             int x1 = Mathf.FloorToInt(u);
        //             int y1 = Mathf.FloorToInt(v);
        //             int x2 = Mathf.Min(x1 + 1, originalTexture.width - 1);
        //             int y2 = Mathf.Min(y1 + 1, originalTexture.height - 1);
        //             Color color1 = originalTexture.GetPixel(x1, y1);
        //             Color color2 = originalTexture.GetPixel(x2, y1);
        //             Color color3 = originalTexture.GetPixel(x1, y2);
        //             Color color4 = originalTexture.GetPixel(x2, y2);
        //             Color finalColor = (color1 + color2 + color3 + color4) / 4;
        //             resizedPixels[index] = finalColor;
        //         }
        //     }
        //     resizedTexture.SetPixels(resizedPixels);
        //     resizedTexture.Apply();
        //     return resizedTexture;
        // }
    }
}