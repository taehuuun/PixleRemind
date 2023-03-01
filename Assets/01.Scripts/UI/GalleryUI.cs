using System.Collections.Generic;
using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class GalleryUI : BodyUI
    {
        public Page pixelArtPage;
        
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }
        public void SelectPage(Page page)
        {
            if(activePopups.Count > 0)
            {
                ClosePopup();
            }
            
            OpenPopup(page);
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
