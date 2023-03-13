using System.Collections.Generic;
using System.IO;
using Google.MiniJSON;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Managers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch.UI
{
    public class GalleryPixelArtPage : Page
    {
        [SerializeField] private GalleryUI ui;
        
        public PixelArtSlot pixelArtSlotPrefab;
        public Transform slotGenTrans;

        private List<PixelArtSlot> _pixelArtSlots = new List<PixelArtSlot>();

        private void OnEnable()
        {
            SetPage();
        }

        private void OnDisable()
        {
            foreach (var slot in _pixelArtSlots)
            {
                Destroy(slot.gameObject);
            }

            _pixelArtSlots.Clear();
        }

        private void SetPage()
        {
            CreatePixelArtSlot();
        }

        private void CreatePixelArtSlot()
        {
           foreach (var pixelArtData in GalleryManager.ins.pixelArtDatas)
            {
                PixelArtSlot newPixelArtSlot = Instantiate(pixelArtSlotPrefab, slotGenTrans);
                string path = Path.Combine(DataManager.GalleryDataPath, GalleryManager.ins.selectedTopic);
                newPixelArtSlot.pixelData = pixelArtData;
                newPixelArtSlot.SetSlot();
                newPixelArtSlot.GetComponent<Button>().onClick.AddListener(() => ui.SelectPage(GalleryPage.ColorMatch));
                _pixelArtSlots.Add(newPixelArtSlot);
            }
        }
    }
}
