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
            if (Input.GetKeyDown(KeyCode.Escape) && !GalleryManager.ins.isMatching)
            {
                Close();
            }
        }
        public void SelectPage(GalleryPage page)
        {
            // if(activePopups.Count > 0)
            // {
            //     ClosePopup();
            // }
            //
            OpenPopup(pages[(int)page]);
        }
        public void Close()
        {
            if(activePopups.Count > 0)
            {
                print($"활성화된 페이지가 있음 : {activePopups.Count}");
                ClosePopup();
            }
            else
            {
                MoveScene("MainScene");
            }
        }
    }
}
