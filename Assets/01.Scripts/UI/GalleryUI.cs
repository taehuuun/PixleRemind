using System.Collections.Generic;
using LTH.PixelRemind.Enums;
using LTH.PixelRemind.Managers;
using UnityEngine;

namespace LTH.PixelRemind.UI
{
    public class GalleryUI : BodyUI
    {
        public GameObject[] pages;
        public Stack<GalleryPage> pageHistory;

        private void Start()
        {
            pageHistory = new Stack<GalleryPage>();
            SelectPage(GalleryPage.Topic);
        }
        protected override void Update()
        {
            // base.Update();
            if (Input.GetKeyDown(KeyCode.Escape) && !GalleryManager.ins.IsMatching && pageHistory.Count > 0)
            {
                Close();
            }
        }
        public void SelectPage(GalleryPage page)
        {
            if (pageHistory.Count > 0 && pageHistory.Peek() == page)
            {
                return;
            }
            
            if (pageHistory.Count > 0)
            {
                pages[(int)pageHistory.Peek()].SetActive(false);
            }
            
            pages[(int)page].SetActive(true);
            pageHistory.Push(page);
            
            Debug.Log(pageHistory.Count);
        }
        public void Close()
        {
            if (pageHistory.Count > 1)
            {
                pages[(int)pageHistory.Pop()].SetActive(false);
                pages[(int)pageHistory.Peek()].SetActive(true);
            }
            else
            {
                MoveScene("MainScene");
            }
            
            Debug.Log(pageHistory.Count);
        }
    }
}
