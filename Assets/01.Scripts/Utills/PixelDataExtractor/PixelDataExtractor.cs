using System.Collections.Generic;
using UnityEngine;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;

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
                    Color getPixelColor = extractTarget.GetPixel(x, y);
                    CustomColor customColor = new CustomColor(getPixelColor.r,getPixelColor.g,getPixelColor.b,getPixelColor.a);
                    rowPixelDatas.Add(customColor);
                }
                
                pixelDatas.Pixels.Add(rowPixelDatas);
            }
            
            pixelDatas.Pixels.Reverse();
            
            return pixelDatas;
        }

        public static PixelArtData ExportPixelData(GalleryTopic topic, string title, Texture2D pixelArtImg, Difficulty difficulty)
        {
            ColorData colorData = ExtractPixelData(pixelArtImg);
            PixelArtData newData = new PixelArtData(topic, title, colorData, colorData.Pixels.Count, Difficulty.Easy);
            
            return newData;
        }
    }
}