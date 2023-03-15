using System.Collections.Generic;
using LTH.ColorMatch.Interfaces;
using UnityEngine;

namespace LTH.ColorMatch.Managers
{
    public class ColorMatchSystem : MonoBehaviour, ISubject
    {
        public ColorSlot targetColorSlot;
        public List<ColorSlot> selectColorSlots;

        public List<IObserver> observers = new List<IObserver>();
        private int _score;
        private int _life;
        private float _similarRange;
        private bool _isGameOver = false;
        
        public float decRangeValue = 0.05f;
        public int maxLife;
        public float maxSimilarRange;

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                NotifyObservers();
            }
        }
        public int Life
        {
            get => _life;
            private set
            {
                _life = value;
                NotifyObservers();
            }
        }
        public float SimilarRange
        {
            get => _similarRange;
            private set
            {
                _similarRange = value;
                NotifyObservers();
            }
        }
        public bool IsGameOver
        {
            get => _isGameOver;
            private set
            {
                _isGameOver = value;
                NotifyObservers();
            }
        }

        public void ReStart()
        {
            ResetStats();
            GenerateNewPuzzle();
        }
        
        public bool CheckMatch(ColorSlot slot)
        {
            bool isMatched = (slot.slotImage.color == targetColorSlot.slotImage.color);
            
            if (isMatched)
            {
                OnCorrectMatch();
            }
            else if (Life > 0)
            {
                OnIncorrectMatch();
            }

            return isMatched;
        }

        private void OnCorrectMatch()
        {
            IncreaseScore();
            GenerateNewPuzzle();
        }
        private void OnIncorrectMatch()
        {
            Handheld.Vibrate();
            DecreaseLife();
        }
        private void ResetStats()
        {
            Life = maxLife;
            Score = 0;
            SimilarRange = maxSimilarRange;
            _isGameOver = false;
        }
        private void IncreaseScore()
        {
            Score++;
            SimilarRange = Mathf.Clamp(SimilarRange - decRangeValue, 5, 100);
        }
        private void DecreaseLife()
        {
            if (Life > 0)
            {
                Life--;

                if (Life == 0)
                {
                    IsGameOver = true;
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
        }
        private void SetRandomSelectColorSlots()
        {
            int answerIdx = Random.Range(0, selectColorSlots.Count);
            
            for (int i = 0; i < selectColorSlots.Count; i++)
            {
                Color randColor;

                if (i == answerIdx)
                {
                    randColor = targetColorSlot.slotImage.color;
                }
                else
                {
                    randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color,SimilarRange) ;

                    while (randColor == targetColorSlot.slotImage.color)
                    {
                        randColor = GetRandomSimilarColor(targetColorSlot.slotImage.color,SimilarRange) ;
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

        public void RegisterObserver(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }
        public void RemoveObserver(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.UpdateSubjectState();
            }
        }
    }
}
