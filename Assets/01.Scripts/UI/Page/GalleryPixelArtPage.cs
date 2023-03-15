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

        public List<PixelArtSlot> _pixelArtSlots = new List<PixelArtSlot>();

        private void OnEnable()
        {
            SetPage();
        }

        private void OnDisable()
        {
            Debug.Log("Disable");
            foreach (var slot in _pixelArtSlots)
            {
                Destroy(slot.gameObject);
                _pixelArtSlots.Remove(slot);
            }
            _pixelArtSlots.Clear();
            GalleryManager.ins.pixelArtDatas.Clear();
        }

        private void SetPage()
        {
            GalleryManager.ins.curPage = GalleryPage.PixelArt;
            CreatePixelArtSlot();
        }

        private void CreatePixelArtSlot()
        {
            if (_pixelArtSlots.Count != GalleryManager.ins.pixelArtDatas.Count)
            {
               for (int i = 0 ; i < GalleryManager.ins.pixelArtDatas.Count; i++)
                {
                    PixelArtSlot newPixelArtSlot = Instantiate(pixelArtSlotPrefab, slotGenTrans);
                    string path = Path.Combine(DataManager.GalleryDataPath, GalleryManager.ins.pixelArtDatas[i].topic.ToString());
                    newPixelArtSlot.pixelData = GalleryManager.ins.pixelArtDatas[i];
                    newPixelArtSlot.SetSlot();
                    var i1 = i;
                    newPixelArtSlot.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        ui.SelectPage(GalleryPage.ColorMatch);
                        GalleryManager.ins.selPixelArtIdx = i1;
                    });
                    _pixelArtSlots.Add(newPixelArtSlot);
                }
            }
        }
    }
}
