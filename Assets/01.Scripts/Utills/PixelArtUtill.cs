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

        public static PixelArtData ExportPixelData(GalleryTopic topic, string title, Texture2D pixelArtImg, Difficulty difficulty)
        {
            ColorData colorData = ExtractPixelData(pixelArtImg);
            PixelArtData newData = new PixelArtData(topic, title, colorData, pixelArtImg.width,0,false, difficulty);
            
            return newData;
        }
    }
}
