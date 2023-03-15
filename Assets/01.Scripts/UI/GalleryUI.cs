using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Managers;
using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class GalleryUI : BodyUI
    {
        public Page[] pages;
        
        
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !GalleryManager.ins.IsMatching)
            {
                Close();
            }
        }
        public void SelectPage(GalleryPage page)
        {
            foreach (var Page in pages)
            {
                if (Page.gameObject.activeSelf)
                {
                    Page.gameObject.SetActive(false);
                }
            }
            
            pages[(int)page].gameObject.SetActive(true);
        }
        public void Close()
        {
            if(activePopups.Count > 0)
            {
                print($"활성화된 페이지가 있음 : {activePopups.Count}");

                for (int i = 0; i < activePopups.Count; i++)
                {
                    ClosePopup();
                }
                
                SelectPage(GalleryManager.ins.CurPage - 1);
            }
            else
            {
                MoveScene("MainScene");
            }
        }
    }
}
