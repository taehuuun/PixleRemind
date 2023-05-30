using UnityEngine;

namespace LTH.PixelRemind.UI
{
    public class Popup : CloseAbleUI
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
