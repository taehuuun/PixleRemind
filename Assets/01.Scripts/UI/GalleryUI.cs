using System.Collections.Generic;
using LTH.ColorMatch.Enums;
using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class GalleryUI : BodyUI
    {
        public Page[] pages;
        
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }
        public void SelectPage(GalleryPage page)
        {
            if(activePopups.Count > 0)
            {
                ClosePopup();
            }
            
            OpenPopup(pages[(int)page]);
        }
        public void Close()
        {
            if(activePopups.Count > 0)
            {
                ClosePopup();
            }
            else
            {
                MoveScene("MainScene");
            }
        }
    }
}
