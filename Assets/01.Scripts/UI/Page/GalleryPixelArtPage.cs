using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Managers;
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
            List<string> pixelArts = GalleryManager.ins.GetPixelArts();
            
            foreach (var pixelArt in pixelArts)
            {
                PixelArtSlot newPixelArtSlot = Instantiate(pixelArtSlotPrefab, slotGenTrans);
                string path = Path.Combine(DataManager.GalleryDataPath, GalleryManager.ins.selectedTopic);
                newPixelArtSlot.pixelData =
                    DataManager.LoadJsonData<PixelArtData>(path, pixelArt);
                newPixelArtSlot.SetSlot();
                newPixelArtSlot.GetComponent<Button>().onClick.AddListener(() => ui.SelectPage(GalleryPage.ColorMatch));
                _pixelArtSlots.Add(newPixelArtSlot);
            }
        }
    }
}
