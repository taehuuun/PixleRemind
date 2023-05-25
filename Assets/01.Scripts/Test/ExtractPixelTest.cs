// using System;
// using System.Collections.Generic;
// using LTH.PixelRemind.Data;
// using LTH.PixelRemind.Enums;
// using LTH.PixelRemind.Managers.Data;
// using LTH.PixelRemind.Managers.Firebase;
// using LTH.PixelRemind.Managers.Firebase.Collections;
// using LTH.PixelRemind.Utill;
// using Newtonsoft.Json;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace LTH.PixelRemind.Test
// {
//     public enum TestMode  {Upload,Load};
//     public class ExtractPixelTest : MonoBehaviour
//     {
//         public TestMode mode;
//         public Image board;
//         public List<Texture2D> pixelArts;
//         public string id;
//         public Difficulty difficulty;
//         public List<TopicData> testData;
//         
//         private async void Start()
//         {
//             string cummonPath = DataManager.GalleryDataPath;
//             if (mode == TestMode.Load)
//             {
//                 if (DataManager.LocalDirectoryExists(cummonPath))
//                 {
//                     List<string> topics = DataManager.GetTargetFolderFileNames(cummonPath);
//
//                     for (int i = 0; i < topics.Count; i++)
//                     {
//                         TopicData loadTopicData = DataManager.LoadJsonData<TopicData>(cummonPath, topics[i]);
//                         testData.Add(loadTopicData);
//                     }
//                 }
//                 else
//                 {
//                     if (await FirebaseManager.ins.Firestore.CheckCollectionExists(FirestoreCollections.GalleryData))
//                     {
//                         List<TopicData> topicDataList = await FirebaseManager.ins.Firestore.GetAllData<TopicData>(FirestoreCollections.GalleryData);
//                         testData.AddRange(topicDataList);
//
//                         foreach (var topicData in topicDataList)
//                         {
//                             DataManager.SaveJsonData(cummonPath, topicData.ID,JsonConvert.SerializeObject(topicData));
//                         }
//                     }
//                 }
//             }
//             else
//             {
//                 string topicThumbData = "";
//                 int topicThumbSize = 0;
//
//                 List<PixelArtData> pixelArtDatas = new List<PixelArtData>();
//                 
//                 for (int i = 0; i < pixelArts.Count; i++)
//                 {
//                     PixelArtData newPixelArtData =
//                         PixelArtUtill.ExportPixelData(id, pixelArts[i].name, pixelArts[i], difficulty);
//                     pixelArtDatas.Add(newPixelArtData);
//
//                     if (i == 0)
//                     {
//                         topicThumbData = newPixelArtData.ThumbnailData;
//                         topicThumbSize = newPixelArtData.Size;
//                     }
//                 }
//
//                 TopicData newTopicData = new TopicData(id, topicThumbData, 0, pixelArtDatas.Count,
//                     topicThumbSize, false,false,DateTime.Now, pixelArtDatas);
//
//                 await FirebaseManager.ins.Firestore.AddData(FirestoreCollections.GalleryData, id, newTopicData);
//             }
//         }
//     }
// }
