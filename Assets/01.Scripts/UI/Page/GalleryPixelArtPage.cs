using LTH.PixelRemind.Enums;
using LTH.PixelRemind.Managers.Gallery;
using LTH.PixelRemind.UI.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.PixelRemind.UI
{
    public class GalleryPixelArtPage : Page
    {
        [SerializeField] private GalleryUI ui;
        
        public PixelArtSlot pixelArtSlotPrefab;
        public Transform slotGenTrans;

        [SerializeField] private PixelArtSlot[] _pixelArtSlots;

        private void OnEnable()
        {
           if (
               GalleryManager.ins.TopicDatas.Count > 0 && GalleryManager.ins.SelTopicIdx >= 0 &&
                GalleryManager.ins.SelTopicIdx < GalleryManager.ins.TopicDatas.Count)
            {
                var selectTopicData = GalleryManager.ins.TopicDatas[GalleryManager.ins.SelTopicIdx];
                GalleryManager.ins.LoadPixelDataForTopic(selectTopicData);
                SetPage();
            }
            else
            {
                Debug.LogError("Invalid TopicData index");
                return;
            }
        }

        private void OnDisable()
        {
            foreach (var pixelArtSlot in _pixelArtSlots)
            {
                pixelArtSlot.gameObject.SetActive(false);
            }
        }

        private void SetPage()
        {
            GalleryManager.ins.CurPage = GalleryPage.PixelArt;
            SetPixelArtSlot();
        }

        private void SetPixelArtSlot()
        {
            for (int i = 0 ; i < GalleryManager.ins.PixelArtDatas.Count; i++)
            {
                _pixelArtSlots[i].gameObject.SetActive(true);
                _pixelArtSlots[i].pixelData = GalleryManager.ins.PixelArtDatas[i];
                _pixelArtSlots[i].SetSlot();
                
                var pixelArtDataIdx = i;
                _pixelArtSlots[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    GalleryManager.ins.SelPixelArtIdx = pixelArtDataIdx;
                    ui.SelectPage(GalleryPage.ColorMatch);
                });
            }
        }
    }
}
