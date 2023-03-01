using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace LTH.ColorMatch.UI
{
    public class ColorFindGameUI : BodyUI
    {
        [Header("Popups")]
        public Popup gameOverPopup;
        
        [Header("Texts")]
        public TMP_Text scoreText;
        public TMP_Text similarText;
        public TMP_Text targetColorHexCodeText;
        public TMP_Text lifeText;
        public TMP_Text gameOverPopScoreText;
        
        private void Start()
        {
            scoreText.text = "0";
        }
        
        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }

        public void UpdateLife(int life)
        {
            lifeText.text = $"기회 : {life.ToString()}";
        }
        
        public void UpdateSimilarity(float similarity)
        {
            float result = 100 - similarity;
            similarText.text =$"유사도 : {string.Format("{0:N2}", result)} (Max : 95%)" ;
        }
    }
}
