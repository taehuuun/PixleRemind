using System.Collections.Generic;
using System.Linq;
using LTH.ColorMatch.Interfaces;
using UnityEngine;
using LTH.ColorMatch.UI;

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
            set
            {
                _score = value;
                NotifyObservers();
            }
        }
        public int Life
        {
            get { return _life; }
            set
            {
                _life = value;
                NotifyObservers();
            }
        }
        public float SimilarRange
        {
            get { return _similarRange; }
            set
            {
                _similarRange = value;
                NotifyObservers();
            }
        }
        public bool IsGameOver
        {
            get { return _isGameOver; }
            set
            {
                _isGameOver = value;
                NotifyObservers();
            }
        }
        
        
        private void Start()
        {
            ReStart();
        }

        public void ReStart()
        {
            Life = maxLife;
            Score = 0;
            SimilarRange = maxSimilarRange;
            IsGameOver = false;

            GenerateNewPuzzle();
        }
        
        public void SelectSlot(ColorSlot slot)
        {
            if (slot.slotImage.color == targetColorSlot.slotImage.color)
            {
                Score++;
                SimilarRange = Mathf.Clamp(SimilarRange - decRangeValue, 5, 100);
                GenerateNewPuzzle();
            }
            else
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
                Color randColor = new Color();

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
