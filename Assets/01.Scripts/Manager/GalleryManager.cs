using System;
using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace LTH.ColorMatch.Managers
{
    // 갤러리의 ColorMathch 시스템을 관리
    public class GalleryManager : MonoBehaviour
    {
        public static GalleryManager ins;
        // TopicPage 에서 선택한 토픽을 저장
        public string topic;
        
        // PixelArtPage 에서 선택한 PixelArt를 저장
        public string pixelArt;
        public bool isMatching;
        
        private void Awake()
        {
            if (ins == null)
            {
                ins = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public List<string> GetTopicDatas()
        {
            if (topic == null)
            {
                Debug.LogError("잘못된 Topic");
                return null;
            }

            return DataManager.GetDirectorys();
        }
        public List<string> GetPixelArtDatas()
        {
            if (topic == null)
            {
                Debug.LogError("잘못된 Topic");
                return null;
            }

            if (pixelArt == null)
            {
                Debug.LogError("잘못된 PixelArt");
                return null;
            }
            
            return DataManager.GetFiles(GalleryManager.ins.topic);
        }
        public PixelArtData GetPixelArtData()
        {
            if (topic == null)
            {
                Debug.LogError("잘못된 Topic");
                return null;
            }

            if (pixelArt == null)
            {
                Debug.LogError("잘못된 PixelArt");
                return null;
            }

            string path = Path.Combine(topic, pixelArt);
            
            return DataManager.LoadJsonData<PixelArtData>(path);
        }
        public void SavePixelArtData(PixelArtData data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            string path = Path.Combine(topic, pixelArt);
            DataManager.SaveJsonData(jsonData,path);
        }
    }
}