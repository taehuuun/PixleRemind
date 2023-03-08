using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace LTH.ColorMatch.Managers
{
    public class GalleryManager : MonoBehaviour
    {
        public static GalleryManager ins;
        public string selectedTopic;
        public string selectedPixelArt;
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

        /// <summary>
        /// 선택된 토픽의 리스트를 가져오는 함수
        /// </summary>
        /// <returns>토픽 리스트</returns>
        public List<string> GetTopics()
        {
            return DataManager.GetTopics();
        }

        /// <summary>
        /// 선택된 토픽의 픽셀 아트 리스트를 가져오는 함수
        /// </summary>
        /// <returns>픽셀 아트 리스트</returns>
        public List<string> GetPixelArts()
        {
            if (string.IsNullOrEmpty(selectedTopic))
            {
                Debug.LogWarning("선택된 Topic가 없습니다.");
                return null;
            }

            return DataManager.GetPixelArts(selectedTopic);
        }

        /// <summary>
        /// 선택된 토픽과 픽셀 아트의 데이터를 가져오는 함수
        /// </summary>
        /// <returns>PixelArtData 객체</returns>
        public PixelArtData LoadPixelArtData()
        {
            if (string.IsNullOrEmpty(selectedTopic) || string.IsNullOrEmpty(selectedPixelArt))
            {
                Debug.LogWarning("선택된 Topic 또는 PixelArt가 없습니다.");
                return null;
            }

            string path = Path.Combine(selectedTopic, selectedPixelArt);
            return DataManager.LoadJsonData<PixelArtData>(path);
        }

        /// <summary>
        /// 픽셀 아트 데이터를 저장하는 함수
        /// </summary>
        /// <param name="data">저장할 PixelArtData 객체</param>
        public void SavePixelArtData(PixelArtData data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            string path = Path.Combine(selectedTopic, selectedPixelArt);
            DataManager.SaveJsonData(selectedTopic, selectedPixelArt, jsonData);
        }
    }
}