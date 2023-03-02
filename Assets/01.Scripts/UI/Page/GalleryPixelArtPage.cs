using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
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
        
        private List<string> _pixelArts = new List<string>();

        private void Start()
        {
            SetPage();
        }

        private void SetPage()
        {
            _pixelArts = DataManager.GetFiles(GalleryManager.topic);

            CreateTopicSlot();
        }

        private void CreateTopicSlot()
        {
            foreach (var pixelArt in _pixelArts)
            {
                PixelArtSlot newPixelArtSlot = Instantiate(pixelArtSlotPrefab, slotGenTrans);
                newPixelArtSlot.titleText.text = pixelArt;
                newPixelArtSlot.pixelData =
                    DataManager.LoadJsonData<PixelArtData>(Path.Combine(GalleryManager.topic, pixelArt));
                newPixelArtSlot.GetComponent<Button>().onClick.AddListener(() => ui.SelectPage(ui.pixelArtPage));
                Debug.Log($"Pixel Data Path : {GalleryManager.topic + "/"+pixelArt}");
            }
        }
    }
}
