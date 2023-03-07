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

        private void Start()
        {
            SetPage();
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
                newPixelArtSlot.titleText.text = pixelArt;
                newPixelArtSlot.pixelData =
                    DataManager.LoadJsonData<PixelArtData>(Path.Combine(GalleryManager.ins.topic, pixelArt));
                newPixelArtSlot.GetComponent<Button>().onClick.AddListener(() => ui.SelectPage(GalleryPage.ColorMatch));
            }
        }
    }
}
