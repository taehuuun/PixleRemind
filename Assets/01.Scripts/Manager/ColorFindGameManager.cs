using System.Collections.Generic;
using UnityEngine;
using LTH.ColorMatch.UI;

namespace LTH.ColorMatch.Managers
{
    public class ColorFindGameManager : MonoBehaviour
    {
        public ColorSlot targetColorSlot;
        public List<ColorSlot> selectColorSlots;
        public ColorFindGameUI ui;
        
        public int score;
        public float similarRange = 80f;
        public float decRangeValue = 0.05f;
        public int life = 3;

        public static bool isGameOver = false;
        
        private void Start()
        {
            GenerateNewPuzzle();
            ui.UpdateSimilarity(similarRange);
            ui.UpdateLife(life);
        }

        public void ReStart()
        {
            life = 3;
            score = 0;
            similarRange = 80f;
            
            ui.UpdateScore(score);
            ui.UpdateSimilarity(similarRange);
            ui.UpdateLife(life);
            ui.gameOverPopup.gameObject.SetActive(false);
            GenerateNewPuzzle();
        }
        
        public void SelectSlot(ColorSlot slot)
        {
            if (slot.slotImage.color == targetColorSlot.slotImage.color)
            {
                score++;
                similarRange = Mathf.Clamp(similarRange - decRangeValue, 5, 100);
                ui.UpdateScore(score);
                ui.UpdateSimilarity(similarRange);
                GenerateNewPuzzle();
            }
            else
            {
                if (life > 0)
                {
                    Debug.Log("Not Match");
                    life--;
                    ui.UpdateLife(life);

                    if (life == 0)
                    {
                        Debug.Log("GameOver");
                        isGameOver = false;
                        ui.gameOverPopScoreText.text = $"이번 점수 : {score.ToString()}점!!";
                        ui.OpenPopup(ui.gameOverPopup);
                    }
                }
            }
        }
       
        private void GenerateNewPuzzle()
        {
            SetRandomTargetColor();
            SetRandomSelectColorSlots();            
        }
        private void SetRandomTargetColor()
        {
            var randColor = Random.ColorHSV();
            targetColorSlot.SetSlot(randColor);
            ui.targetColorHexCodeText.text = targetColorSlot.slotHexCode;
        }
        private void SetRandomSelectColorSlots()
        {
            int answerIdx = Random.Range(0, selectColorSlots.Count);
            
            for (int i = 0; i < selectColorSlots.Count; i++)
            {
                Color randColor = new Color();

                if (i == answerIdx)
                {
                    randColor = targetColorSlot.slotImage.color;
                }
                else
                {
                    randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color,similarRange) ;

                    while (randColor == targetColorSlot.slotImage.color)
                    {
                        randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color,similarRange) ;
                    }
                }
                
                selectColorSlots[i].SetSlot(randColor);
            }
        }
        
        private Color GetRandomSimilarColor(Color targetColor, float variance)
        {
            float range = variance * 0.01f;

            float r = Mathf.Clamp(Random.Range(targetColor.r - range, targetColor.r + range), 0f, 1f);
            float g = Mathf.Clamp(Random.Range(targetColor.g - range, targetColor.g + range), 0f, 1f);
            float b = Mathf.Clamp(Random.Range(targetColor.b - range, targetColor.b + range), 0f, 1f);
            return new Color(r, g, b);
        }
    }
}
