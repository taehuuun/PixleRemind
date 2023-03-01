using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Enums;

namespace LTH.ColorMatch.UI
{
    public class SmoothImageColorChange : MonoBehaviour
    {
        public Image backGroundImg;
        public float minBackChangeColorValue = 0.8f;
        public float changeBackGroundColorValue = 0.05f;
        private bool _isBackGroundChange = false;
        public ChangeColorType curChangeColorType = ChangeColorType.Red;

        private void Start()
        {
            StartCoroutine(BackgroundChanger());
        }
        
        private IEnumerator BackgroundChanger()
        {
            while (!ColorFindGameManager.isGameOver)
            {
                if (!_isBackGroundChange)
                {
                    switch (curChangeColorType)
                    {
                        case ChangeColorType.Red:
                            if (backGroundImg.color.r >= 1f)
                            {
                                StartCoroutine(DecColorChangeValue());
                            }
                            else
                            {
                                StartCoroutine(IncColorChangeValue());
                            }
                            break;
                        case ChangeColorType.Green:
                            if (backGroundImg.color.g >= 1f)
                            {
                                StartCoroutine(DecColorChangeValue());
                            }
                            else
                            {
                                StartCoroutine(IncColorChangeValue());
                            }
                            break;
                        case ChangeColorType.Blue:
                            if (backGroundImg.color.b >= 1f)
                            {
                                StartCoroutine(DecColorChangeValue());
                            }
                            else
                            {
                                StartCoroutine(IncColorChangeValue());
                            }
                            break;
                    }
                }
                
                yield return new WaitUntil(() => _isBackGroundChange);
            }
        }
        private IEnumerator IncColorChangeValue()
        {
            Color tmpColor = new Color();
            
            _isBackGroundChange = true;
            
            while(_isBackGroundChange)
            {
                tmpColor = backGroundImg.color;
                switch (curChangeColorType)
                {
                    case ChangeColorType.Red:
                        tmpColor.r += changeBackGroundColorValue;
                        break;
                    case ChangeColorType.Green:
                        tmpColor.g += changeBackGroundColorValue;
                        break;
                    case ChangeColorType.Blue:
                        tmpColor.b += changeBackGroundColorValue;
                        break;
                }
                backGroundImg.color = tmpColor;

                if (curChangeColorType == ChangeColorType.Red)
                {
                    if (backGroundImg.color.r >= 1f)
                    {
                        curChangeColorType = ChangeColorType.Green;
                        break;
                    }
                }
                else if (curChangeColorType == ChangeColorType.Green)
                {
                    if (backGroundImg.color.g >= 1f)
                    {
                        curChangeColorType = ChangeColorType.Blue;
                        break;
                    }
                }
                else
                {
                    if (backGroundImg.color.b >= 1f)
                    {
                        curChangeColorType = ChangeColorType.Red;
                        break;
                    }
                }

                yield return new WaitForFixedUpdate();
            }
            _isBackGroundChange = false;
        }
        private IEnumerator DecColorChangeValue()
        {
            Color tmpColor = new Color();
            
            _isBackGroundChange = true;
            
            while(_isBackGroundChange)
            {
                tmpColor = backGroundImg.color;
                
                switch (curChangeColorType)
                {
                    case ChangeColorType.Red:
                        tmpColor.r -= changeBackGroundColorValue;
                        break;
                    case ChangeColorType.Green:
                        tmpColor.g -= changeBackGroundColorValue;
                        break;
                    case ChangeColorType.Blue:
                        tmpColor.b -= changeBackGroundColorValue;
                        break;
                }
                
                backGroundImg.color = tmpColor;

                if (curChangeColorType == ChangeColorType.Red)
                {
                    if (backGroundImg.color.r <= minBackChangeColorValue)
                    {
                        curChangeColorType = ChangeColorType.Green;
                        break;
                    }
                }
                else if (curChangeColorType == ChangeColorType.Green)
                {
                    if (backGroundImg.color.g <= minBackChangeColorValue)
                    {
                        curChangeColorType = ChangeColorType.Blue;
                        break;
                    }
                }
                else
                {
                    if (backGroundImg.color.b <= minBackChangeColorValue)
                    {
                        curChangeColorType = ChangeColorType.Red;
                        break;
                    }
                }
            
                yield return new WaitForFixedUpdate();
            }

            _isBackGroundChange = false;
        }
    }
}
